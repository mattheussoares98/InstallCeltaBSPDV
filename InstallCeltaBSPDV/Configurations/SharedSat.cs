using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Configurations {
    internal static class SharedSat {
        public static async Task createSharedSat(EnableConfigurations enable) {

            DialogResult createSharedSat = MessageBox.Show("Deseja criar a pasta de compartilhamento do SAT?", "Criar compartilhamento do SAT", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if(createSharedSat.Equals(DialogResult.Yes)) {
                enableIISFeatures(enable);
                await Download.downloadFileTaskAsync("deployment.zip");
                await createPathSharedSat(enable);
            }

        }
        private static async Task createPathSharedSat(EnableConfigurations enable) {
            #region directoryes
            string celtaSatPdvBin = "C:\\Celta SAT\\PDV\\Bin";
            string celtaSatPdv = "C:\\Celta SAT\\PDV";
            string celtaSat = "C:\\Celta SAT";
            string installDeployment = "C:\\Install\\Deployment";
            string installDeploymentPdv = "C:\\Install\\Deployment\\PDV";
            string installDeploymentZip = "C:\\Install\\Deployment.zip";
            string celtaSatSale = "C:\\Celta Sat\\PDV\\Sale";
            string celtaSatSat = "C:\\Celta Sat\\PDV\\Sat";

            string celtaSatPdvSalesalePath = "C:\\Celta SAT\\PDV\\Sale\\Release\\WebService";
            string celtaSatPdvSalePathBin = "C:\\Celta SAT\\PDV\\Sale\\Release\\WebService\\Bin";

            string CeltaSatPdvSatPath = "C:\\Celta SAT\\PDV\\SAT\\Release\\WebService";
            string CeltaSatPdvSatPathBin = "C:\\Celta SAT\\PDV\\SAT\\Release\\WebService\\Bin";
            #endregion


            if(!File.Exists(installDeploymentZip)) {
                //se não houver o deployment na pasta install, baixa ele novamente e chama o mesmo método para efetuar a extração dos arquivos e criação da pasta de compartilhamento do SAT
                enable.richTextBoxResults.Text += $"Como o {installDeploymentZip} não existe, a aplicação fará o download do arquivo para criar a pasta de compartilhamento do SAT atualizada\n\n";
                await Download.downloadFileTaskAsync("deployment.zip");

                await createPathSharedSat(enable);
                return;

            } else {
                //se houver o arquivo do deployment.zip na pasta install, a aplicação extrai os arquivos, exclui a pasta de compartilhamento (se houver) e cria tudo novamente com os arquivos novos
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

                    enable.checkBoxSharedPath.Checked = true;
                    //checkBoxSharedPath.ForeColor = Color.Green;
                } catch(Exception ex) {
                    MessageBox.Show(ex.Message);
                }

                try {

                    await Download.downloadFileTaskAsync("web.config", "https://drive.google.com/u/1/uc?id=19D1bDda6HU4qa7tdbVppHFRmh0SsAoem&export=download");

                    await Task.Run(() => File.Move("C:\\Install\\web.config", "C:\\Celta SAT\\PDV\\web.config", true));
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para baixar o webConfig do compartilhamento do SAT: {ex.Message}");
                }
            }
        }
        private static async Task overrideFilesInPath(string pathToRead, string destiny) {
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
        private static void enableIISFeatures(EnableConfigurations enable) {

            string command = "START /WAIT DISM /Online /Enable-Feature /FeatureName:IIS-ApplicationDevelopment /FeatureName:IIS-ASP /FeatureName:IIS-ASPNET /FeatureName:IIS-BasicAuthentication /FeatureName:IIS-CGI /FeatureName:IIS-ClientCertificateMappingAuthentication /FeatureName:IIS-CommonHttpFeatures /FeatureName:IIS-CustomLogging /FeatureName:IIS-DefaultDocument /FeatureName:IIS-DigestAuthentication /FeatureName:IIS-DirectoryBrowsing /FeatureName:IIS-FTPExtensibility /FeatureName:IIS-FTPServer /FeatureName:IIS-FTPSvc /FeatureName:IIS-HealthAndDiagnostics /FeatureName:IIS-HostableWebCore /FeatureName:IIS-HttpCompressionDynamic /FeatureName:IIS-HttpCompressionStatic /FeatureName:IIS-HttpErrors /FeatureName:IIS-HttpLogging /FeatureName:IIS-HttpRedirect /FeatureName:IIS-HttpTracing /FeatureName:IIS-IIS6ManagementCompatibility /FeatureName:IIS-IISCertificateMappingAuthentication /FeatureName:IIS-IPSecurity /FeatureName:IIS-ISAPIExtensions /FeatureName:IIS-ISAPIFilter /FeatureName:IIS-LegacyScripts /FeatureName:IIS-LegacySnapIn /FeatureName:IIS-LoggingLibraries /FeatureName:IIS-ManagementConsole /FeatureName:IIS-ManagementScriptingTools /FeatureName:IIS-ManagementService /FeatureName:IIS-Metabase /FeatureName:IIS-NetFxExtensibility /FeatureName:IIS-ODBCLogging /FeatureName:IIS-Performance /FeatureName:IIS-RequestFiltering /FeatureName:IIS-RequestMonitor /FeatureName:IIS-Security /FeatureName:IIS-ServerSideIncludes /FeatureName:IIS-StaticContent /FeatureName:IIS-URLAuthorization /FeatureName:IIS-WebDAV /FeatureName:IIS-WebServer /FeatureName:IIS-WebServerManagementTools /FeatureName:IIS-WebServerRole /FeatureName:IIS-WindowsAuthentication /FeatureName:IIS-WMICompatibility /FeatureName:WAS-ConfigurationAPI /FeatureName:WAS-NetFxEnvironment /FeatureName:WAS-ProcessModel /FeatureName:WAS-WindowsActivationService";

            ProcessStartInfo pStartInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            pStartInfo.CreateNoWindow = true;

            try {
                //await Task.Run(() => );
                Process.Start(pStartInfo);
                enable.richTextBoxResults.Text += "Adicionando recursos do IIS. Não feche a janela do CMD! Após a conclusão do CMD, confirme se instalou os recursos do IIS e caso não tenha instalado, instale manualmente\n\n";
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
