using InstallCeltaBSPDV.DownloadFiles;
using Microsoft.Win32;
using System.Diagnostics;

namespace InstallCeltaBSPDV.Configurations
{
    internal class BsPdv
    {

        private readonly EnableConfigurations enable = new();

        public BsPdv(EnableConfigurations enable)
        {
            this.enable = enable;
        }

        public async Task configureBsPdv()
        {

            await downloadAndConfigurePdvPaths();

            await downloadAndInstallMongoDb();

            createPdvLinks();

            //await editMongoCfg(); //nessa função já está adicionando permissão para todos usuários na pasta do arquivo. Ele só chega nessa parte quando existe a pasta do arquivo

            installComponentsReport();

            installRoboMongo();

            await downloadAndInstallRustDesk();
        }

        #region directories
        private const string cInstall = @"C:\Install";
        private const string startupPath = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\CeltaPDV.lnk";
        private readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\CeltaPDV.lnk";
        #endregion


        private async void createPdvLinks()
        {
            if (enable.cbShortcut.Checked == true)
            {
                return;
            }

            if (File.Exists(startupPath) && File.Exists(desktopPath))
            {
                enable.cbShortcut.Checked = true;
                return;
            }
            await new Download(enable).downloadFileTaskAsync("CeltaPDV.zip", "http://187.35.140.227/downloads/lastversion/Programas");

            createStartupLink();
            createDesktopLink();

            if (File.Exists(startupPath) && File.Exists(desktopPath))
            {
                enable.cbShortcut.Checked = true;
            }
        }
        private async Task downloadAndConfigurePdvPaths()
        {
            if (enable.cbCeltaBSPDV.Checked == true)
            {
                return;
            }
            await new Download(enable).downloadFileTaskAsync(Download.installBsPdvZip, "http://187.35.140.227/downloads/lastversion");
            await new Windows(enable).enableAllPermissionsForPath("C:\\Install");
            await new Windows(enable).enableAllPermissionsForPath("C:\\Install\\PDV");
            await new Windows(enable).enableAllPermissionsForPath("C:\\Install\\PDV\\CeltaBSPDV");
            //await new Windows(enable).enableAllPermissionsForPath(Download.cInstallBsPdvZip);
            //await new Windows(enable).enableAllPermissionsForPath(Download.cInstall);

            await new Windows(enable).extractFile(Download.cInstallBsPdvZip, Download.cInstall, "installbspdv.zip", uriDownload: "http://187.35.140.227/downloads/lastversion");


            Task.Delay(7000).Wait();
            await new Windows(enable).movePdvPath(); //essencial fazer esse processo depois de baixaro arquivo installBsPdv.zip
        }
        private void createStartupLink()
        {
            if (File.Exists(startupPath))
            {
                return;
            }
            else
            {

                try
                {
                    File.Copy(cInstall + "\\CeltaPDV.lnk", startupPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro para copiar o atalho para a área de trabalho: " + ex.Message);
                }
            }
        }
        private void createDesktopLink()
        {

            if (File.Exists(desktopPath))
            {
                return;
            }
            else
            {
                try
                {
                    File.Copy(cInstall + "\\CeltaPDV.lnk", desktopPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro para copiar o atalho para a área de trabalho: " + ex.Message);
                }
            }
        }

        public bool verifyAppIsInstalled(string displayName)
        {
            // consulta no regedit se contém algum aplicativo instalado. O "displayName" será o nome que verá se está instalado
            List<string> programsDisplayName = new()
            {
            };

            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        if (subkey.GetValue("DisplayName") != null)
                        {
                            programsDisplayName.Add((string)subkey.GetValue("DisplayName"));
                        }
                    }
                }
            }

            bool appInstalled = false;
            foreach (var installed in programsDisplayName)
            {
                if (installed.Contains(displayName))
                {
                    appInstalled = true;
                }
            }
            return appInstalled;
        }

        private async Task downloadAndInstallMongoDb()
        {
            string mongoDbFilePath = @"C:\\Install\\PDV\\Database\\mongodb.msi";

            // Checks if installation is requested and if the installer file already exists
            if (enable.cbMongoDB.Checked && !File.Exists(mongoDbFilePath))
            {
                return;
            }

            // Downloads the required zip file if the msi is still not found
            if (!File.Exists(mongoDbFilePath))
            {
                enable.richTextBoxResults.Text += $"O arquivo {mongoDbFilePath} não foi encontrado. Inicializando download do {Download.installBsPdvZip}. Se ficar em loop, feche com CTRL+F4 e adicione o {mongoDbFilePath} onde ele não deveria ter sido removido ou delete o {Download.installBsPdvZip}!\n\n";
                //só tenta baixar o arquivo se ele não existir ainda

                await new Download(enable).downloadFileTaskAsync(Download.installBsPdvZip, "http://187.35.140.227/downloads/lastversion/installbspdv.zip");

                // Executes the method again after downloading
                await downloadAndInstallMongoDb();
                return;
            }

            // AppDomain.CurrentDomain.BaseDirectory is the safest path to get the application's root directory
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Path.Combine automatically and safely handles directory separators
            string batFilePath = Path.Combine(appDirectory, "InstallMongoDb.bat");

            // Validates if the batch file was copied to the output directory
            if (!File.Exists(batFilePath))
            {
                MessageBox.Show($"Não foi possível encontrar o {batFilePath}. Abortando a instalação do banco de dados.");
                return;
            }

            var installMongo = new ProcessStartInfo
            {
                FileName = batFilePath,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            try
            {
                // Checks via registry if MongoDB is already installed
                bool isInstalled = verifyAppIsInstalled("Mongo");

                if (!isInstalled)
                {
                    // Starts the batch process to install MongoDB
                    Process.Start(installMongo);
                }

                while (!isInstalled)
                {
                    // Verifies periodically if the installation process has updated the registry
                    isInstalled = verifyAppIsInstalled("Mongo");

                    if (isInstalled)
                    {
                        // Exits the loop once the registry confirms the installation
                        break;
                    }

                    // Uses await instead of .Wait() to release the UI thread and improve application performance
                    await Task.Delay(3000);
                }

                if (isInstalled)
                {
                    enable.cbMongoDB.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro para instalar o MongoDB: " + ex.Message);
            }
        }

        private async Task editMongoCfg()
        {
            if (enable.cbRemoteAcces.Checked == true)
            {
                return;
            }

            string programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
            string mongoBin = programFiles += "\\MongoDB\\Server\\7.0\\bin";
            string mongoConfig = mongoBin + "\\mongod.cfg";

            if (!File.Exists(mongoConfig))
            {
                enable.richTextBoxResults.Text += $"O {mongoConfig} não existe. A aplicação fará a instalação do banco de dados para editar o acesso remoto ao banco de dados\n\n";

                await downloadAndInstallMongoDb();

                await editMongoCfg();
            }

            try
            {
                StreamReader sr = File.OpenText(mongoConfig);
                string? textoDoArquivo = sr.ReadToEnd();
                sr.Close();

                if (textoDoArquivo.Contains("0.0.0.0"))
                {

                    enable.cbRemoteAcces.Checked = true;
                    //enable.checkBoxEnableRemoteAcces.ForeColor = Color.Green;
                    enable.cbMongoDB.Checked = true;
                    //enable.checkBoxInstallMongo.ForeColor = Color.Green;
                    return;
                }

                await new Windows(enable).enableAllPermissionsForPath(Environment.ExpandEnvironmentVariables("%ProgramW6432%") + "\\MongoDB\\Server\\7.0\\bin");

                string newText = textoDoArquivo.Replace("127.0.0.1", "0.0.0.0");

                Task.Delay(3000).Wait();

                File.WriteAllText(mongoConfig, newText); //caso já tenha um arquivo com o mesmo nome, ele sobrescreve com o texto que está no segundo parâmetro

                enable.cbRemoteAcces.Checked = true;
                //enable.checkBoxEnableRemoteAcces.ForeColor = Color.Green;
                enable.cbMongoDB.Checked = true;
                //enable.checkBoxInstallMongo.ForeColor = Color.Green;

            }
            catch (Exception e)
            {
                MessageBox.Show("Erro para ler os dados do arquivo: " + e.Message);
                Console.WriteLine(e.Message);
            }


        }

        private void installRoboMongo()
        {
            if (enable.cbRoboMongo.Checked == true)
            {
                return;
            }

            string cProgramFilesRobo = "C:\\Program Files\\Robo 3T 1.4.2";
            string cInstallPdvDatabase = "C:\\Install\\PDV\\Database";
            string roboFileName = "robo3t-1.4.2-windows-x86_64-8650949.exe";
            var openInstallRoboMongo = new ProcessStartInfo("cmd", $"/c cd {cInstallPdvDatabase}&{roboFileName}");
            openInstallRoboMongo.CreateNoWindow = true;

            if (!Directory.Exists(cProgramFilesRobo))
            {
                enable.richTextBoxResults.Text += "Como o RoboMongo não está instalado, a aplicação abrirá o instalador \n\n";
                enable.cbRoboMongo.Checked = false;
                if (File.Exists(cInstallPdvDatabase + "\\" + roboFileName))
                {
                    Process.Start(openInstallRoboMongo);
                }
            }
            else
            {
                enable.cbRoboMongo.Checked = true;
            }
        }
        private void installComponentsReport()
        {
            if (enable.cbComponentsReport.Checked == true)
            {
                return;
            }

            #region Directoryes y fileNames
            string componentsDirectory = "C:\\Install\\PDV\\ComponentesReport\\x64\\";
            string componentOne = "SQLSysClrTypes.msi";
            string componentTwo = "ReportViewer.msi";
            #endregion

            #region Processes
            var installComponentOne = new ProcessStartInfo("cmd", $"/c cd {componentsDirectory}&msiexec /i SQLSysClrTypes.msi /qn");
            installComponentOne.CreateNoWindow = true;

            var installComponentTwo = new ProcessStartInfo("cmd", $"/c cd {componentsDirectory}&msiexec /i ReportViewer.msi /qn");
            installComponentTwo.CreateNoWindow = true;
            #endregion

            #region install CLR Types 2014
            if (!File.Exists(componentsDirectory + componentOne))
            {
                MessageBox.Show($"Não foi possível instalar o component {componentOne} porque o arquivo{componentsDirectory + componentOne} não existe");
                return;
            }
            else
            {

                try
                {
                    bool isInstalledComponentOne = verifyAppIsInstalled("Microsoft System CLR Types para SQL Server 2014"); //ele verifica pelo regedit se o mongo já foi instalado
                    if (!isInstalledComponentOne)
                    {
                        Process.Start(installComponentOne);
                    }
                    while (!isInstalledComponentOne)
                    {
                        //vai ficar verificando se o aplicativo já foi instalado pra somente depois que terminar a instalação, continuar a execução dos códigos
                        isInstalledComponentOne = verifyAppIsInstalled("Microsoft System CLR Types para SQL Server 2014");

                        Task.Delay(3000).Wait();
                        //coloquei pra aguardar 3 segundos pra executar novamente senão o aplicativo fica executando com muita frequência essa verificação
                        if (isInstalledComponentOne)
                        {
                            //quando o aplicativo finalmente está instalado, ele sai do laço while e continua a execução dos códigos
                            break;
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro para instalar o {componentOne}: " + ex.Message);
                }
            }
            #endregion

            #region install ReportViewer
            //motivos para não validar se esse aplicativo já foi instalado conforme o anterior para tentar instalar:
            //1: só é possível instalar ele quando o anterior é instaldo
            //2: só vai tentar instalar esse quando o anterior já foi instalado
            //3: a instalação vai funcionar
            //4: como vai funcionar a instalação, não precisa esperar o término dela
            if (!File.Exists(componentsDirectory + componentTwo))
            {
                MessageBox.Show($"Não foi possível instalar o component {componentTwo} porque o arquivo{componentsDirectory + componentTwo} não existe");
                return;
            }
            else
            {

                try
                {
                    Process.Start(installComponentTwo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro para instalar o {componentTwo}: " + ex.Message);
                }
            }
            #endregion

            //se chegar aqui é porque não deu erro em nenhuma instalação e por isso pode marcar como instalado os dois components
            enable.cbComponentsReport.Checked = true;
        }

        private async Task downloadAndInstallRustDesk()
        {
            // --- KEY CHANGE HERE ---
            // Dynamically get the path to the batch file located in the application's startup directory.
            string batFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InstallRustDesk.bat");



            // Verify the batch file actually exists alongside your .exe
            if (!File.Exists(batFilePath))
            {
                MessageBox.Show($"Erro: O script de instalação '{batFilePath}' não foi encontrado.");
                return;
            }

            var installProcessInfo = new ProcessStartInfo(batFilePath);
            installProcessInfo.CreateNoWindow = true;
            installProcessInfo.UseShellExecute = false;

            try
            {
                Process.Start(installProcessInfo);

                // This loop waits for the installation to complete.
                while (!verifyAppIsInstalled("RustDesk"))
                {
                    // Wait for a few seconds before checking again.
                    await Task.Delay(3000);
                }

                // Mark as complete.
                enable.cbRustDesk.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao executar o script de instalação do RustDesk: " + ex.Message);
            }
        }
    }
}
