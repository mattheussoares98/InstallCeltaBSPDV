﻿using InstallCeltaBSPDV.Forms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Configurations {
    internal class BsPdv {

        private readonly EnableConfigurations enable = new();

        public BsPdv(EnableConfigurations enable) {
            this.enable = enable;
        }

        public async Task configureBsPdv() {

            await downloadAndConfigurePdvPaths();

            await downloadAndInstallMongoDb();

            createPdvLinks();

            await editMongoCfg(); //nessa função já está adicionando permissão para todos usuários na pasta do arquivo. Ele só chega nessa parte quando existe a pasta do arquivo

            installComponentsReport();

            installRoboMongo();
        }

        private async void createPdvLinks() {
            if(enable.checkBoxPdvLink.Checked == true) {
                return;
            }

            await new Download(enable).downloadFileTaskAsync("CeltaPDV.lnk", "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21144&authkey=AAjFoI8iNRefjCY");


            createStartupLink();
            createDesktopLink();

            enable.checkBoxPdvLink.Checked = true;
        }
        private async Task downloadAndConfigurePdvPaths() {
            if(enable.checkBoxCopyCetaBSPDV.Checked == true) {
                return;
            }
            await new Download(enable).downloadFileTaskAsync(Download.installBsPdvZip, "http://177.103.179.36/downloads/lastversion/installbspdv.zip");
            await new Windows(enable).enableAllPermissionsForPath("C:\\install");
            await new Windows(enable).enableAllPermissionsForPath("C:\\install\\pdv\\celtabspdv");
            //await new Windows(enable).enableAllPermissionsForPath(Download.cInstallBsPdvZip);
            //await new Windows(enable).enableAllPermissionsForPath(Download.cInstall);

            await new Windows(enable).extractFile(Download.cInstallBsPdvZip, Download.cInstall, "installbspdv.zip", uriDownload: "http://177.103.179.36/downloads/lastversion/installbspdv.zip");


            Task.Delay(7000).Wait();
            await new Windows(enable).movePdvPath(); //essencial fazer esse processo depois de baixaro arquivo installBsPdv.zip
        }
        #region directories
        private const string cInstall = @"C:\Install";
        private const string startupPath = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\CeltaPDV.lnk";
        private readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\CeltaPDV.lnk";
        #endregion
        private void createStartupLink() {
            if(File.Exists(startupPath)) {
                return;
            } else {

                try {
                    File.Copy(cInstall + "\\CeltaPDV.lnk", startupPath);
                } catch(Exception ex) {
                    MessageBox.Show("Erro para copiar o atalho para a área de trabalho: " + ex.Message);
                }
            }
        }
        private void createDesktopLink() {

            if(File.Exists(desktopPath)) {
                return;
            } else {
                //IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                //IWshRuntimeLibrary.IWshShortcut shortcut = shell.CreateShortcut(pdvPath + "\\CeltaPDV.lnk");
                //shortcut.TargetPath = pdvFilePath;
                //shortcut.Save();

                try {
                    File.Copy(cInstall + "\\CeltaPDV.lnk", desktopPath);
                } catch(Exception ex) {
                    MessageBox.Show("Erro para copiar o atalho para a área de trabalho: " + ex.Message);
                }
            }
        }

        public bool verifyAppIsInstalled(string displayName) {
            // consulta no regedit se contém o Mongo instalado
            List<string> programsDisplayName = new() {
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
        private async Task downloadAndInstallMongoDb() {
            if(enable.checkBoxInstallMongo.Checked == true) {
                return;
            }
            string mongoDbFilePath = "C:\\Install\\PDV\\Database\\mongodb-win32-x86_64-2008plus-ssl-4.0.22-signed.msi";
            //precisa ter o arquivo C:\Install\PDV\Database\mongodb-win32-x86_64-2008plus-ssl-4.0.22-signed
            if(!File.Exists(mongoDbFilePath)) {
                MessageBox.Show($"Não foi possível instalar o banco de dados porque o arquivo{mongoDbFilePath} não existe");
                await new Download(enable).downloadFileTaskAsync(Download.installBsPdvZip, "http://177.103.179.36/downloads/lastversion/installbspdv.zip");

                await downloadAndInstallMongoDb();
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
        private async Task editMongoCfg() {
            if(enable.checkBoxEnableRemoteAcces.Checked == true) {
                return;
            }

            string programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
            string mongoBin = programFiles += "\\MongoDB\\Server\\4.0\\bin";
            string mongoConfig = mongoBin + "\\mongod.cfg";

            if(!File.Exists(mongoConfig)) {
                enable.richTextBoxResults.Text += $"O {mongoConfig} não existe. A aplicação fará a instalação do banco de dados para editar o acesso remoto ao banco de dados\n\n";

                await downloadAndInstallMongoDb();

                await editMongoCfg();
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

                await new Windows(enable).enableAllPermissionsForPath(Environment.ExpandEnvironmentVariables("%ProgramW6432%") + "\\MongoDB\\Server\\4.0\\bin");

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

        private void installRoboMongo() {
            if(enable.checkBoxInstallRoboMongo.Checked == true) {
                return;
            }

            string cProgramFilesRobo = "C:\\Program Files\\Robo 3T 1.4.2";
            string cInstallPdvDatabase = "C:\\Install\\PDV\\Database";
            string roboFileName = "robo3t-1.4.2-windows-x86_64-8650949.exe";
            var openInstallRoboMongo = new ProcessStartInfo("cmd", $"/c cd {cInstallPdvDatabase}&{roboFileName}");
            openInstallRoboMongo.CreateNoWindow = true;

            if(!Directory.Exists(cProgramFilesRobo)) {
                enable.richTextBoxResults.Text += "Como o RoboMongo não está instalado, a aplicação abrirá o instalador \n\n";
                enable.checkBoxInstallRoboMongo.Checked = false;
                if(File.Exists(cInstallPdvDatabase + "\\" + roboFileName)) {
                    Process.Start(openInstallRoboMongo);
                }
            } else {
                enable.checkBoxInstallRoboMongo.Checked = true;
            }
        }
        private void installComponentsReport() {
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
