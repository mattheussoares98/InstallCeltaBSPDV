using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Configurations {
    internal class SharedSat {
        private readonly EnableConfigurations enable = new();
        private readonly Download download;
        public SharedSat(EnableConfigurations enable) {
            this.enable = enable;
            download = new(enable);
        }
        public async Task createSharedSat() {
            //no início da instalação a aplicação já pergunta se o usuário quer instalar o site de compartilhamento do SAT e caso queira, altera o valor da variável "canInstallSharedSat" para true
            if(enable.checkBoxCreateSharedSatSite.Checked == true) {
                return;
            }

            await download.downloadFileTaskAsync("deployment.zip", "http://177.103.179.36/downloads/lastversion/deployment.zip");
            await createPathSharedSat();
            await enableIISFeatures();
            await createSiteIIS();
        }
        private async Task createSiteIIS() {
            if(enable.checkBoxCreateSharedSatSite.Checked) {
                return;
            }
            //Task.Delay(20000).Wait();

            if(!Directory.Exists(@"C:\CeltaSAT\PDV")) {
                enable.richTextBoxResults.Text += "Como o diretório C:\\CeltaSAT\\PDV não existe, não tentará criar o site de compartilhamento do SAT\n\n";

            }
            enable.richTextBoxResults.Text += "Criando o site de compartilhamento do SAT no IIS\n\n";

            string joinPathIisCommands = "cd c:\\Windows\\System32\\inetsrv";

            var deleteDefaultSite = new ProcessStartInfo("cmd", $"/c {joinPathIisCommands}&appcmd delete site \"Default Web Site\"");

            var setPoolLocal = new ProcessStartInfo("cmd", $"/c {joinPathIisCommands}&appcmd set apppool \"DefaultAppPool\" -processModel.identityType:LocalSystem");

            var enable32BitsPool = new ProcessStartInfo("cmd", $"/c {joinPathIisCommands}&appcmd set apppool /apppool.name:\"DefaultAppPool\" /enable32bitapponwin64:true");

            var enableDirectoryBrowser = new ProcessStartInfo("cmd", $"/c {joinPathIisCommands}&appcmd set config /section:directoryBrowse /enabled:true");

            var addSite = new ProcessStartInfo("cmd", $"/c {joinPathIisCommands}&appcmd add site /name:CeltaSAT /id:1 /physicalPath:c:\\CeltaSAT /bindings:http/:9092:*");

            var addApp = new ProcessStartInfo("cmd", $"/c {joinPathIisCommands}&appcmd add app  /site.name:CeltaSAT /path:/PDV /physicalPath:c:\\CeltaSAT\\PDV");
            addSite.CreateNoWindow = true;
            addApp.CreateNoWindow = true;
            setPoolLocal.CreateNoWindow = true;
            enable32BitsPool.CreateNoWindow = true;
            enableDirectoryBrowser.CreateNoWindow = true;
            deleteDefaultSite.CreateNoWindow = true;

            try {
                await Task.Run(() => Process.Start(setPoolLocal));
                Task.Delay(3000).Wait();
                await Task.Run(() => Process.Start(enable32BitsPool));
                Task.Delay(3000).Wait();
                await Task.Run(() => Process.Start(enableDirectoryBrowser));
                Task.Delay(3000).Wait();
                await Task.Run(() => Process.Start(deleteDefaultSite));
                Task.Delay(3000).Wait();
                await Task.Run(() => Process.Start(addSite));
                Task.Delay(3000).Wait();
                await Task.Run(() => Process.Start(addApp));
            } catch(Exception ex) {
                MessageBox.Show($"Erro para criar o site de compartilhamento do SAT no IIS");
            }

            enable.checkBoxCreateSharedSatSite.Checked = true;
            enable.richTextBoxResults.Text += "O site foi criado com sucesso\n\n";
        }
        private async Task createPathSharedSat() {
            if(enable.checkBoxCreateSharedSatPath.Checked) {
                return;
            }
            #region directoryes
            string celtaSatPdvBin = "C:\\CeltaSAT\\PDV\\Bin";
            string celtaSatPdv = "C:\\CeltaSAT\\PDV";
            string celtaSat = "C:\\CeltaSAT";
            string installDeployment = "C:\\Install\\Deployment";
            string installDeploymentPdv = "C:\\Install\\Deployment\\PDV";
            string installDeploymentZip = "C:\\Install\\Deployment.zip";
            string celtaSatSale = "C:\\CeltaSAT\\PDV\\Sale";
            string celtaSatSat = "C:\\CeltaSAT\\PDV\\Sat";

            string celtaSatPdvSalesalePath = "C:\\CeltaSAT\\PDV\\Sale\\Release\\WebService";
            string celtaSatPdvSalePathBin = "C:\\CeltaSAT\\PDV\\Sale\\Release\\WebService\\Bin";

            string CeltaSatPdvSatPath = "C:\\CeltaSAT\\PDV\\SAT\\Release\\WebService";
            string CeltaSatPdvSatPathBin = "C:\\CeltaSAT\\PDV\\SAT\\Release\\WebService\\Bin";
            #endregion


            bool sharedPath = false;
            bool webConfig = false;

            if(!File.Exists(installDeploymentZip)) {
                //se não houver o deployment na pasta install, baixa ele novamente e chama o mesmo método para efetuar a extração dos arquivos e criação da pasta de compartilhamento do SAT
                enable.richTextBoxResults.Text += $"Como o {installDeploymentZip} não existe, a aplicação fará o download do arquivo para criar a pasta de compartilhamento do SAT atualizada\n\n";
                await download.downloadFileTaskAsync("deployment.zip", "http://177.103.179.36/downloads/lastversion/deployment.zip");

                await createPathSharedSat();
                return;

            } else {
                //se houver o arquivo do deployment.zip na pasta install, a aplicação extrai os arquivos, exclui a pasta de compartilhamento (se houver) e cria tudo novamente com os arquivos novos
                enable.richTextBoxResults.Text += $"O {installDeployment} já existe. Iniciando as configurações de criação da pasta de compartilhamento do SAT\n\n";

                await new Windows(enable).enableAllPermissionsForPath(celtaSatPdv);
                try {

                    if(Directory.Exists(installDeployment)) {
                        Directory.Delete(installDeployment, true);
                    }

                    await Task.Run(() => ZipFile.ExtractToDirectory(installDeploymentZip, installDeployment));

                    if(Directory.Exists(celtaSatPdv)) {
                        await Task.Run(() => Directory.Delete(celtaSatPdv, true));
                    }

                    if(!Directory.Exists(celtaSat)) {
                        await Task.Run(() => Directory.CreateDirectory(celtaSat));
                    }

                    await Task.Run(() => Directory.Move(installDeploymentPdv, celtaSatPdv));

                    if(!Directory.Exists(celtaSatPdvBin)) {
                        Directory.CreateDirectory(celtaSatPdvBin);
                    }

                    await overrideFilesInPath(celtaSatPdvSalePathBin, celtaSatPdvBin);
                    await overrideFilesInPath(CeltaSatPdvSatPathBin, celtaSatPdvBin);
                    await overrideFilesInPath(celtaSatPdvSalesalePath, celtaSatPdv);
                    await overrideFilesInPath(CeltaSatPdvSatPath, celtaSatPdv);

                    if(Directory.Exists(celtaSatSale)) {
                        Directory.Delete(celtaSatSale, true);
                    }

                    if(Directory.Exists(celtaSatSat)) {
                        Directory.Delete(celtaSatSat, true);
                    }

                    //checkBoxSharedPath.ForeColor = Color.Green;
                    sharedPath = true;
                } catch(Exception ex) {
                    MessageBox.Show(ex.Message);
                }

                try {

                    await download.downloadFileTaskAsync("web.config", "https://drive.google.com/u/1/uc?id=19D1bDda6HU4qa7tdbVppHFRmh0SsAoem&export=download");

                    await Task.Run(() => File.Move("C:\\Install\\web.config", "C:\\CeltaSAT\\PDV\\web.config", true));
                    webConfig = true;
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para baixar o webConfig do compartilhamento do SAT: {ex.Message}");
                }

                if(webConfig && sharedPath) {
                    enable.checkBoxCreateSharedSatPath.Checked = true;
                    enable.richTextBoxResults.Text += "Diretório de compartilhamento do SAT criado com sucesso\n\n";
                } else {
                    enable.richTextBoxResults.Text += "Ocorreu erro para criar o site de compartilhamento do SAT\n\n";

                }

            }
        }
        private async Task overrideFilesInPath(string pathToRead, string destiny) {
            if(!Directory.Exists(pathToRead)) {
                MessageBox.Show($"Não foi possível encontrar o caminho {pathToRead}");
            }
            string[] files = Directory.GetFiles(pathToRead);

            foreach(string file in files) {
                string localDestiny = file.Replace(pathToRead, destiny);
                //MessageBox.Show($"file: {file}\n\ndestiny = " + destiny);
                try {
                    await Task.Run(() => File.Copy(file, localDestiny));
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para copiar o arquivo para o destino\n\norigem: {file}\n\ndestino: {localDestiny}\n\nerro: {ex.Message}");
                }
            }
        }

        private async Task enableIISFeatures() {
            if(enable.checkBoxEnableIISComponents.Checked == true) {
                return;
            }
            enable.richTextBoxResults.Text += "Adicionando os recursos do IIS. Esse processo pode ser demorado\n\n";


            string command1 = "Dism /Online /Enable-Feature /FeatureName:IIS-DefaultDocument /All";
            string command2 = "Dism /Online /Enable-Feature /FeatureName:IIS-ASPNET /All";
            string command3 = "Dism /Online /Enable-Feature /FeatureName:IIS-ASPNET45 /All";
            string command4 = "Dism /Online /Enable-Feature /FeatureName:IIS-ApplicationInit /All";
            string command5 = "Dism /Online /Enable-Feature /FeatureName:IIS-ASP /All";
            string command6 = "Dism /Online /Enable-Feature /FeatureName:IIS-Metabase /All";

            ProcessStartInfo process1 = new ProcessStartInfo("cmd.exe", "/c " + command1);
            ProcessStartInfo process2 = new ProcessStartInfo("cmd.exe", "/c " + command2);
            ProcessStartInfo process3 = new ProcessStartInfo("cmd.exe", "/c " + command3);
            ProcessStartInfo process4 = new ProcessStartInfo("cmd.exe", "/c " + command4);
            ProcessStartInfo process5 = new ProcessStartInfo("cmd.exe", "/c " + command5);
            ProcessStartInfo process6 = new ProcessStartInfo("cmd.exe", "/c " + command6);
            process1.CreateNoWindow = true;
            process2.CreateNoWindow = true;
            process3.CreateNoWindow = true;
            process4.CreateNoWindow = true;
            process5.CreateNoWindow = true;
            process6.CreateNoWindow = true;

            try {

                var returnProcess1 = Process.Start(process1);
                var returnProcess2 = Process.Start(process2);
                var returnProcess3 = Process.Start(process3);
                var returnProcess4 = Process.Start(process4);
                var returnProcess5 = Process.Start(process5);
                var returnProcess6 = Process.Start(process6);

                await returnProcess1!.WaitForExitAsync().ConfigureAwait(true);
                await returnProcess2!.WaitForExitAsync().ConfigureAwait(true);
                await returnProcess3!.WaitForExitAsync().ConfigureAwait(true);
                await returnProcess4!.WaitForExitAsync().ConfigureAwait(true);
                await returnProcess5!.WaitForExitAsync().ConfigureAwait(true);
                await returnProcess6!.WaitForExitAsync().ConfigureAwait(true);

            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            enable.richTextBoxResults.Text += "Os recursos do IIS foram instalados\n\n";
            enable.checkBoxEnableIISComponents.Checked = true;
        }
    }
}
