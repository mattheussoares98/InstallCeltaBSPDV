using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Configurations {
    internal static class BsPdv {

        static public async Task configureBsPdv(EnableConfigurations enable) {

            await Download.downloadFileTaskAsync(Download.installBsPdvZip, enable);
            Task.Delay(700).Wait(); //só pra confirmar que realmente terminou o download do arquivo. Se o download já foi realizado, não vai tentar baixar novamente

            await Windows.extractFile(Download.cInstallBsPdvZip, Download.cInstall, Download.installBsPdvZip, enable, enable.checkBoxCopyCetaBSPDV);

            Task.Delay(700).Wait(); //para ter certeza que já terá extraído a pasta e que já terá dado permissão para todos usuários na pasta. Se não fizer isso, da erro as vezes

            await Windows.enableAllPermissionsForPath("C:\\install", enable);
            await Windows.enableAllPermissionsForPath(Download.cCeltabspdv, enable);

            installMongoDb(enable);

            if(!enable.checkBoxCopyCetaBSPDV.Checked) {
                await Windows.movePath(Download.cInstallPdvCeltabspdv, Download.cCeltabspdv, enable); //essencial fazer esse processo depois de baixaro arquivo installBsPdv.zip
            }

            await verifyPdvPathExists(enable);

            createPdvLinks(enable);

            await editMongoCfg(enable); //nessa função já está adicionando permissão para todos usuários na pasta do arquivo. Ele só chega nessa parte quando existe a pasta do arquivo

            installComponentsReport(enable);
        }

        private static void createPdvLinks(EnableConfigurations enable) {
            if(enable.checkBoxPdvLink.Checked == true) {
                return;
            }
            createStartupLink();
            createDesktopLink();

            enable.checkBoxPdvLink.Checked = true;
        }
        private static async Task verifyPdvPathExists(EnableConfigurations enable) {
            if(!File.Exists(pdvPath)) {
                enable.richTextBoxResults.Text += $"Não foi possível encontrar o arquivo {pdvPath}. Iniciando download do installbspdv.zip para fazer as configurações das pastas\n";

                await Download.downloadFileTaskAsync(Download.installBsPdvZip, enable);
                await Windows.extractFile(Download.cInstallBsPdvZip, Download.cInstall, "installbspdv.zip", enable);

                Task.Delay(7000).Wait();
                await Windows.movePath(Download.cInstallPdvCeltabspdv, Download.cCeltabspdv, enable);
                Task.Delay(7000).Wait();
            }
        }
        #region directories
        private static string pdvPath = @"C:\CeltaBSPDV\CeltaWare.CBS.PDV.UI.exe";
        private static string startupPath = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\CeltaPDV.lnk";
        private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\CeltaPDV.lnk";
        #endregion
        private static void createStartupLink() {
            if(File.Exists(startupPath)) {
                return;
            } else {
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = shell.CreateShortcut(startupPath);
                shortcut.TargetPath = pdvPath;
                shortcut.Save();
            }
        }
        private static void createDesktopLink() {

            if(File.Exists(desktopPath)) {
                return;
            } else {
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = shell.CreateShortcut(desktopPath);
                shortcut.TargetPath = pdvPath;
                shortcut.Save();
            }
        }

        public static bool verifyAppIsInstalled(string displayName) {
            // consulta no regedit se contém o Mongo instalado
            List<String> programsDisplayName = new() {
            };

            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using(RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key)) {
                foreach(string subkey_name in key.GetSubKeyNames()) {
                    using(RegistryKey subkey = key.OpenSubKey(subkey_name)) {
                        if(subkey.GetValue("DisplayName") != null) {
                            programsDisplayName.Add((string)subkey.GetValue("DisplayName"));
                        }
                    }
                }
            }

            bool appInstalled = false;
            foreach(var installed in programsDisplayName) {
                if(installed.Contains(displayName)) {
                    appInstalled = true;
                }
            }
            return appInstalled;
        }
        private static void installMongoDb(EnableConfigurations enable) {
            if(enable.checkBoxInstallMongo.Checked == true) {
                return;
            }
            string mongoDbFilePath = "C:\\Install\\PDV\\Database\\mongodb-win32-x86_64-2008plus-ssl-4.0.22-signed.msi";
            //precisa ter o arquivo C:\Install\PDV\Database\mongodb-win32-x86_64-2008plus-ssl-4.0.22-signed
            if(!File.Exists(mongoDbFilePath)) {
                MessageBox.Show($"Não foi possível instalar o banco de dados porque o arquivo{mongoDbFilePath} não existe");
                return;
            }

            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string appDirectory = Path.GetDirectoryName(appPath)!;
            var installMongo = new ProcessStartInfo("cmd", $"/c cd {appDirectory}&installMongoDB.bat");
            installMongo.CreateNoWindow = true;

            //pra funcionar esse instalador, precisa ter o BAT da instalação do banco de dados dentro da pasta onde vai executar o aplicativo com o nome "installMongoDB.bat"
            if(!File.Exists(appDirectory + "\\installMongoDB.bat")) {
                MessageBox.Show($"Não foi possível encontrar o {appDirectory}\\installMongoDB.bat. Abortando a instalação do banco de dados");
                return;
            }

            try {

                bool isInstalled = verifyAppIsInstalled("Mongo"); //ele verifica pelo regedit se o mongo já foi instalado
                if(!isInstalled) {
                    Process.Start(installMongo);
                }
                while(!isInstalled) {
                    //vai ficar verificando se o aplicativo já foi instalado pra somente depois que terminar a instalação, continuar a execução dos códigos
                    isInstalled = verifyAppIsInstalled("Mongo");

                    Task.Delay(3000).Wait();
                    //coloquei pra aguardar 3 segundos pra executar novamente senão o aplicativo fica executando com muita frequência essa execução
                    if(isInstalled) {
                        //quando o aplicativo finalmente está instalado, ele sai do laço while e continua a execução dos códigos
                        break;
                    }

                }
                if(isInstalled)
                    enable.checkBoxInstallMongo.Checked = true;

            } catch(Exception ex) {
                MessageBox.Show("Erro para instalar o MongoDB: " + ex.Message);
            }
        }
        private static async Task editMongoCfg(EnableConfigurations enable) {
            if(enable.checkBoxEnableRemoteAcces.Checked == true) {
                return;
            }

            string programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
            string mongoBin = programFiles += "\\MongoDB\\Server\\4.0\\bin";
            string mongoConfig = mongoBin + "\\mongod.cfg";

            if(!File.Exists(mongoConfig)) {
                //richTextBoxResults.Text += $"O {mongoConfig} não existe. Clique em 'YES' para tentar abrir o instalador do PDV\n\n";

                installMongoDb(enable);
                DialogResult dialogResult = MessageBox.Show("Clique em OK caso tenha terminado a instalação do MongoDB", "CONTINUAR?", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if(dialogResult == DialogResult.OK) {
                    await editMongoCfg(enable);
                }
                await editMongoCfg(enable);
            }

            try {
                StreamReader sr = File.OpenText(mongoConfig);
                string? textoDoArquivo = sr.ReadToEnd();
                sr.Close();

                if(textoDoArquivo.Contains("0.0.0.0")) {

                    enable.checkBoxEnableRemoteAcces.Checked = true;
                    //enable.checkBoxEnableRemoteAcces.ForeColor = Color.Green;
                    enable.checkBoxInstallMongo.Checked = true;
                    //enable.checkBoxInstallMongo.ForeColor = Color.Green;
                    return;
                }

                await Windows.enableAllPermissionsForPath(Environment.ExpandEnvironmentVariables("%ProgramW6432%") + "\\MongoDB\\Server\\4.0\\bin", enable);

                string newText = textoDoArquivo.Replace("127.0.0.1", "0.0.0.0");

                Task.Delay(3000).Wait();

                File.WriteAllText(mongoConfig, newText); //caso já tenha um arquivo com o mesmo nome, ele sobrescreve com o texto que está no segundo parâmetro

                enable.checkBoxEnableRemoteAcces.Checked = true;
                //enable.checkBoxEnableRemoteAcces.ForeColor = Color.Green;
                enable.checkBoxInstallMongo.Checked = true;
                //enable.checkBoxInstallMongo.ForeColor = Color.Green;

            } catch(Exception e) {
                MessageBox.Show("Erro para ler os dados do arquivo: " + e.Message);
                Console.WriteLine(e.Message);
            }


        }

        private static void installComponentsReport(EnableConfigurations enable) {
            if(enable.checkBoxInstallComponentsReport.Checked == true) {
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
            if(!File.Exists(componentsDirectory + componentOne)) {
                MessageBox.Show($"Não foi possível instalar o component {componentOne} porque o arquivo{componentsDirectory + componentOne} não existe");
                return;
            } else {

                try {
                    bool isInstalledComponentOne = verifyAppIsInstalled("Microsoft System CLR Types para SQL Server 2014"); //ele verifica pelo regedit se o mongo já foi instalado
                    if(!isInstalledComponentOne) {
                        Process.Start(installComponentOne);
                    }
                    while(!isInstalledComponentOne) {
                        //vai ficar verificando se o aplicativo já foi instalado pra somente depois que terminar a instalação, continuar a execução dos códigos
                        isInstalledComponentOne = verifyAppIsInstalled("Microsoft System CLR Types para SQL Server 2014");

                        Task.Delay(3000).Wait();
                        //coloquei pra aguardar 3 segundos pra executar novamente senão o aplicativo fica executando com muita frequência essa verificação
                        if(isInstalledComponentOne) {
                            //quando o aplicativo finalmente está instalado, ele sai do laço while e continua a execução dos códigos
                            break;
                        }

                    }
                } catch(Exception ex) {
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
            if(!File.Exists(componentsDirectory + componentTwo)) {
                MessageBox.Show($"Não foi possível instalar o component {componentTwo} porque o arquivo{componentsDirectory + componentTwo} não existe");
                return;
            } else {

                try {
                    Process.Start(installComponentTwo);
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para instalar o {componentTwo}: " + ex.Message);
                }
            }
            #endregion

            //se chegar aqui é porque não deu erro em nenhuma instalação e por isso pode marcar como instalado os dois components
            enable.checkBoxInstallComponentsReport.Checked = true;
        }
    }
}
