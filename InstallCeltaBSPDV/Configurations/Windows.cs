using InstallCeltaBSPDV.DownloadFiles;
using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Configurations {
    public class Windows {
        private readonly EnableConfigurations enable = new();
        public Windows(EnableConfigurations enableConfigurations) {
            this.enable = enableConfigurations;
        }


        public async Task configureWindows() {
            await configureEnergyPlan();
            openAdjustVisualEffects();
            neverNotifyUser();
            setHostName();
            createTempPath();
            openPowerCfg();
        }

        public async Task overrideFilesInPath(string pathToRead, string destiny) {
            if(!Directory.Exists(pathToRead)) {
                MessageBox.Show($"Não foi possível encontrar o caminho {pathToRead}");
                return;
            }

            if(!Directory.Exists(destiny)) {
                MessageBox.Show($"Não foi possível encontrar o caminho: {destiny}");
                return;
            }
            string[] files = Directory.GetFiles(pathToRead);

            foreach(string file in files) {
                string localDestiny = file.Replace(pathToRead, destiny);
                //MessageBox.Show($"file: {file}\n\ndestiny = " + destiny);
                try {
                    await Task.Run(() => File.Copy(file, localDestiny, true));
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para copiar o arquivo para o destino\n\norigem: {file}\n\ndestino: {localDestiny}\n\nerro: {ex.Message}");
                }
            }
        }

        public void openPowerCfg() {
            if(enable.cbPCI.Checked) {
                return;
            }
            var openCommand = new ProcessStartInfo("cmd", $"/c control.exe powercfg.cpl,,3");
            openCommand.CreateNoWindow = true;
            Process.Start(openCommand);
        }
        public async Task enableAllPermissionsForPath(string path) {
            if(!Directory.Exists(path)) {
                //enable.richTextBoxResults.Text += $"O caminho {path} não foi encontrado para habilitar a permissão para todos usuários\n\n";
                return;
            }

            var enableForEveryone = new ProcessStartInfo("cmd", $"/c icacls {path} /remove:d Everyone /grant:r Everyone:(OI)(CI)F /T");
            var enableForTodos = new ProcessStartInfo("cmd", $"/c icacls {path} /remove:d Todos /grant:r Todos:(OI)(CI)F /T");

            enableForEveryone.CreateNoWindow = true;
            enableForTodos.CreateNoWindow = true;

            try {
                await Task.Run(() => Process.Start(enableForEveryone));
                await Task.Run(() => Process.Start(enableForTodos));
            } catch(Exception ex) {
                MessageBox.Show($"Erro para adicionar permissão para todos usuários na pasta {path}");
            }
        }

        public async Task movePdvPath() {
            if(enable.cbCeltaBSPDV.Checked) {
                return;
            }

            if(!Directory.Exists(Download.cInstallPdvCeltabspdv)) {
                MessageBox.Show($"Não foi possível encontrar o caminho {Download.cInstallPdvCeltabspdv}");
                return;
            }

            if(Directory.Exists(Download.cCeltabspdv)) {
                enable.richTextBoxResults.Text += $"Como o diretório {Download.cCeltabspdv} já existe, não fará a cópia da pasta para o diretório\n\n";
                enable.cbCeltaBSPDV.Checked = true;
                return;
            }

            await enableAllPermissionsForPath(Download.cCeltabspdv); //coloquei pra habilitar permissão pra todos nessa pasta porque em um teste que eu fiz, deu erro pra acessar essa pasta
            await enableAllPermissionsForPath(Download.cInstallPdvCeltabspdv); //coloquei pra habilitar permissão pra todos nessa pasta porque em um teste que eu fiz, deu erro pra acessar essa pasta

            Task.Delay(5000).Wait();
            try {

                Directory.Move(Download.cInstallPdvCeltabspdv, Download.cCeltabspdv);
                Task.Delay(7000).Wait();
                enable.cbCeltaBSPDV.Checked = true;
            } catch(Exception ex) {
                MessageBox.Show("Erro para copiar a pasta C:\\CeltaBSPDV: " + ex.Message);
            }
        }

        public async Task extractFile(string sourceFilePath, string destinyPath, string fileName, CheckBox checkBoxToMark = null, string uriDownload = null) {
            //coloquei o uriDownload pra se não houver o arquivo, a aplicação fazer o download dele
            if(!File.Exists(sourceFilePath)) {
                try {
                    await new Download(enable).downloadFileTaskAsync(fileName, uriDownload);
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para baixar o {fileName}: {ex.Message}");
                }
            } else {
                try {
                    await Task.Run(() => ZipFile.ExtractToDirectory(sourceFilePath, destinyPath, true));
                    //mesmo colocando o await acima, parece que estava indo pro próximo passo sem terminar a execução da extração dos arquivos
                    if(checkBoxToMark != null) {
                        checkBoxToMark.Checked = true;
                    }
                    //checkBoxToMark.ForeColor = Color.Green;
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para extrair o {fileName}: {ex.Message}");
                }
            }
        }

                private async Task configureEnergyPlan() {
            if(enable.cbUSB.Checked && enable.cbPCAndMonitor.Checked && enable.cbFastBoot.Checked) {
                return;
            }
            #region commands
            var enableHibernate = new ProcessStartInfo("cmd", "/c powercfg /hibernate on"); //só é possível iniciar a inicialização rápida do windows se a hibernação estiver habilitada
            var turnOnFastStartup = new ProcessStartInfo("cmd", "/c REG ADD \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Power\" / V HiberbootEnabled / T REG_dWORD / D 1 / F");
            var diskTimeOutAC = new ProcessStartInfo("cmd", "/c powercfg /x -disk-timeout-ac 0");
            var diskTimeOutDC = new ProcessStartInfo("cmd", "/c powercfg /x -disk-timeout-dc 0");
            var monitorTimeOutAC = new ProcessStartInfo("cmd", "/c powercfg /x -monitor-timeout-ac 0");
            var monitorTimeOutDC = new ProcessStartInfo("cmd", "/c powercfg /x -monitor-timeout-dc 0");
            var standybyTimeoutAC = new ProcessStartInfo("cmd", "/c Powercfg /x -standby-timeout-ac 0");
            var standybyTimeoutDC = new ProcessStartInfo("cmd", "/c powercfg /x -standby-timeout-dc 0");
            var disableUsbStandbyBattery = new ProcessStartInfo("cmd", "/c powercfg /SETDCVALUEINDEX SCHEME_CURRENT 2a737441-1930-4402-8d77-b2bebba308a3 48e6b7a6-50f5-4782-a5d4-53bb8f07e226 0"); //desabilitar suspensão da USB
            var disableUsbStandbyPlugged = new ProcessStartInfo("cmd", "/c powercfg /SETACVALUEINDEX SCHEME_CURRENT 2a737441-1930-4402-8d77-b2bebba308a3 48e6b7a6-50f5-4782-a5d4-53bb8f07e226 0"); //desabilitar suspensão da USB

            #endregion

            #region dont show a command line
            diskTimeOutAC.CreateNoWindow = true;
            diskTimeOutDC.CreateNoWindow = true;
            monitorTimeOutAC.CreateNoWindow = true;
            monitorTimeOutDC.CreateNoWindow = true;
            standybyTimeoutAC.CreateNoWindow = true;
            standybyTimeoutDC.CreateNoWindow = true;
            enableHibernate.CreateNoWindow = true;
            disableUsbStandbyBattery.CreateNoWindow = true;
            disableUsbStandbyPlugged.CreateNoWindow = true;
            turnOnFastStartup.CreateNoWindow = true;
            #endregion

            try {
                await Task.Run(() => Process.Start(enableHibernate));
                Task.Delay(2000).Wait();
                await Task.Run(() => Process.Start(turnOnFastStartup));
                Task.Delay(2000).Wait();
                await Task.Run(() => Process.Start(diskTimeOutAC));
                Task.Delay(2000).Wait();
                await Task.Run(() => Process.Start(diskTimeOutDC));
                Task.Delay(2000).Wait();
                await Task.Run(() => Process.Start(monitorTimeOutAC));
                Task.Delay(2000).Wait();
                await Task.Run(() => Process.Start(monitorTimeOutDC));
                Task.Delay(2000).Wait();
                await Task.Run(() => Process.Start(standybyTimeoutAC));
                Task.Delay(2000).Wait();
                await Task.Run(() => Process.Start(standybyTimeoutDC));
                Task.Delay(2000).Wait();
                await Task.Run(() => Process.Start(disableUsbStandbyBattery));
                Task.Delay(2000).Wait();
                await Task.Run(() => Process.Start(disableUsbStandbyPlugged));
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            enable.cbUSB.Checked = true;
            enable.cbPCAndMonitor.Checked = true;
            enable.cbFastBoot.Checked = true;
        }
        private void openAdjustVisualEffects() {
            if(enable.cbBestPerformance.Checked) {
                return;
            }
            var adjustVisualEffects = new ProcessStartInfo("cmd", "/c %windir%\\system32\\SystemPropertiesPerformance.exe");
            adjustVisualEffects.CreateNoWindow = true;
            Process.Start(adjustVisualEffects);
        }
        private void neverNotifyUser() {
            if(enable.cbNeverNotifyUser.Checked) {
                return;
            }
            var info = new ProcessStartInfo("cmd", @"/c C:\Windows\System32\UserAccountControlSettings.exe");
            info.CreateNoWindow = true;
            try {
                Process.Start(info);



            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void setHostName() {
            if(enable.cbHostname.Checked) {
                return;
            }
            ComputerName FormComputerName = new ComputerName(enable);
            FormComputerName.Show();
        }
        private void createTempPath() {
            if(enable.cbTemp.Checked) {
                return;
            }
            if(!Directory.Exists("C:\\Temp")) {
                DirectoryInfo info = Directory.CreateDirectory("C:\\Temp");
                bool exists = info.Exists;

                if(exists) {
                    enable.cbTemp.Checked = true;
                    //enable.checkBoxTemp.ForeColor = Color.Green;
                }
            } else {
                enable.cbTemp.Checked = true;
                //enable.checkBoxTemp.ForeColor = Color.Green;
            }
        }

    }

}
