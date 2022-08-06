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
            await configureFirewall();
            await configureEnergyPlan();
            openAdjustVisualEffects();
            neverNotifyUser();
            setHostName();
            createTempPath();
            openPowerCfg();
        }

        public void openPowerCfg() {
            if(enable.checkBoxDisableSuspendPCI.Checked) {
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

            enable.richTextBoxResults.Text += $"Adicionado permissão total para todos usuários na pasta {path}\n\n";
        }

        public async Task movePdvPath(string sourcePath, string destinyPath) {
            if(enable.checkBoxCopyCetaBSPDV.Checked) {
                return;
            }

            if(!Directory.Exists(sourcePath)) {
                MessageBox.Show($"Não foi possível encontrar o caminho {sourcePath}");
                return;
            }

            if(Directory.Exists(destinyPath)) {
                enable.richTextBoxResults.Text += $"Como o diretório {destinyPath} já existe, não fará a cópia da pasta para o diretório\n\n";
                enable.checkBoxCopyCetaBSPDV.Checked = true;
                return;
            }

            try {
                await enableAllPermissionsForPath(sourcePath); //coloquei pra habilitar permissão pra todos nessa pasta porque em um teste que eu fiz, deu erro pra acessar essa pasta
                await enableAllPermissionsForPath(destinyPath); //coloquei pra habilitar permissão pra todos nessa pasta porque em um teste que eu fiz, deu erro pra acessar essa pasta

                //Directory.Move(sourcePath, destinyPath);
                Directory.Move(sourcePath, destinyPath);
                Task.Delay(7000).Wait();

                //await Task.Run(() => Directory.Move(sourcePath, destinyPath));
                enable.richTextBoxResults.Text += $"{sourcePath} movido com sucesso para o caminho {destinyPath}\n\n";
                enable.checkBoxCopyCetaBSPDV.Checked = true;
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
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

        public async Task configureFirewall() {
            if(enable.checkBoxFirewall.Checked) {
                return;
            }
            bool pingVerify = false;
            bool sitePortVerify = false;
            bool mongoPortVerify = false;

            #region ICMPv4 - PING

            //quando adiciona o processo igual está adicionando a porta 9092 e 27017, não tem opção pra criar como protocolo ICMPv4. Por isso, configurei conforme abaixo e editei a permissão
            Process removePING = new Process {
                StartInfo = {
                    FileName = "netsh",
                    Arguments = $@"advfirewall firewall delete rule name=""PING""",
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true
                }
            };
            removePING.StartInfo.CreateNoWindow = true;
            try {
                await Task.Run(() => removePING.Start());
            } catch(Exception ex) {
                MessageBox.Show("Erro para remover o PING: " + ex.Message);
            }

            Process pingProcess = new Process {
                StartInfo = {
                    FileName = "netsh",
                    Arguments = $@"advfirewall firewall add rule name = ""PING"" protocol = ICMPv4:any,any dir =in action = allow",
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true
                }
            };

            Task.Delay(5000).Wait();

            try {
                await Task.Run(() => pingProcess.Start());

                await Task.Run(() => pingProcess.StandardOutput.ReadToEnd());

                await Task.Run(() => pingProcess.WaitForExit());

            } catch(Exception ex) {
                MessageBox.Show("Erro para criar as regras do PING: " + ex.Message);
            }

            try {
                INetFwPolicy2 firewallPolicyPING = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

                var rule = firewallPolicyPING!.Rules.Item("PING"); // Name of your rule here
                rule.EdgeTraversal = true; // Update the rule here. Nothing else needed to persist the changes

                pingVerify = true;
                enable.richTextBoxResults.Text += "Firewall: Regra de PING adicionada\n\n";
            } catch(Exception ex) {
                MessageBox.Show("Erro para editar o PING: " + ex.Message);
            }
            #endregion

            #region porta 9092
            INetFwRule firewallRule9092 = (INetFwRule)Activator.CreateInstance(
    Type.GetTypeFromProgID("HNetCfg.FWRule"));
            firewallRule9092.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
            firewallRule9092.Description = "Serve para outras máquinas da mesma rede conseguirem acessar o site de compartilhamento do SAT que é criado por padrão com a porta 9092";
            firewallRule9092.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
            firewallRule9092.Enabled = true;
            firewallRule9092.InterfaceTypes = "All";
            firewallRule9092.Name = "9092";
            firewallRule9092.EdgeTraversal = true;
            firewallRule9092.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            firewallRule9092.LocalPorts = "9092";
            INetFwPolicy2 firewallPolicy9092 = (INetFwPolicy2)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            try {
                await Task.Run(() => firewallPolicy9092.Rules.Remove("9092"));

                await Task.Run(() => firewallPolicy9092.Rules.Add(firewallRule9092));

                sitePortVerify = true;
                enable.richTextBoxResults.Text += "Firewall: Regra da porta 9092 adicionada\n\n";
            } catch(Exception ex) {
                MessageBox.Show($"Erro para criar a regra 9092 do Firewall: {ex.Message}");
            }
            #endregion

            #region porta 27017
            INetFwRule firewallRule27017 = (INetFwRule)Activator.CreateInstance(
               Type.GetTypeFromProgID("HNetCfg.FWRule"));

            firewallRule27017.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
            firewallRule27017.Description = "Serve para conseguir acessar o banco de dados do PDV a partir de outras máquinas que estão na mesma rede";
            firewallRule27017.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
            firewallRule27017.Enabled = true;
            firewallRule27017.InterfaceTypes = "All";
            firewallRule27017.Name = "27017";
            firewallRule27017.EdgeTraversal = true;
            firewallRule27017.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            firewallRule27017.LocalPorts = "27017";
            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            try {
                await Task.Run(() => firewallPolicy!.Rules.Remove("27017"));
                await Task.Run(() => firewallPolicy!.Rules.Add(firewallRule27017));
                mongoPortVerify = true;
            } catch(Exception ex) {
                MessageBox.Show($"Erro para configurar a porta 27017: {ex.Message}");
            }


            enable.richTextBoxResults.Text += "Firewall: Regra da porta 27017 adicionada\n\n";
            #endregion

            if(pingVerify && sitePortVerify && mongoPortVerify) {
                enable.checkBoxFirewall.Checked = true;
            }
        }
        private async Task configureEnergyPlan() {
            if(enable.checkBoxDisableSuspendUSB.Checked && enable.checkBoxSuspendMonitorAndPC.Checked && enable.checkBoxEnableFastBoot.Checked) {
                return;
            }
            #region commands
            var turnOnFastStartup = new ProcessStartInfo("cmd", "/c REG ADD \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Power\" / V HiberbootEnabled / T REG_dWORD / D 1 / F");
            var diskTimeOutAC = new ProcessStartInfo("cmd", "/c powercfg /x -disk-timeout-ac 0");
            var diskTimeOutDC = new ProcessStartInfo("cmd", "/c powercfg /x -disk-timeout-dc 0");
            var monitorTimeOutAC = new ProcessStartInfo("cmd", "/c powercfg /x -monitor-timeout-ac 0");
            var monitorTimeOutDC = new ProcessStartInfo("cmd", "/c powercfg /x -monitor-timeout-dc 0");
            var standybyTimeoutAC = new ProcessStartInfo("cmd", "/c Powercfg /x -standby-timeout-ac 0");
            var standybyTimeoutDC = new ProcessStartInfo("cmd", "/c powercfg /x -standby-timeout-dc 0");
            var neverHibernate = new ProcessStartInfo("cmd", "/c powercfg /hibernate off");
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
            neverHibernate.CreateNoWindow = true;
            disableUsbStandbyBattery.CreateNoWindow = true;
            disableUsbStandbyPlugged.CreateNoWindow = true;
            turnOnFastStartup.CreateNoWindow = true;
            #endregion

            try {
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
                await Task.Run(() => Process.Start(neverHibernate));
                Task.Delay(2000).Wait();
                await Task.Run(() => Process.Start(disableUsbStandbyBattery));
                Task.Delay(2000).Wait();
                await Task.Run(() => Process.Start(disableUsbStandbyPlugged));
                Task.Delay(2000).Wait();
                await Task.Run(() => Process.Start(turnOnFastStartup));
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            enable.checkBoxDisableSuspendUSB.Checked = true;
            enable.checkBoxSuspendMonitorAndPC.Checked = true;
            enable.checkBoxEnableFastBoot.Checked = true;
        }
        private void openAdjustVisualEffects() {
            if(enable.checkBoxAdjustVisualEffects.Checked) {
                return;
            }
            var adjustVisualEffects = new ProcessStartInfo("cmd", "/c %windir%\\system32\\SystemPropertiesPerformance.exe");
            adjustVisualEffects.CreateNoWindow = true;
            Process.Start(adjustVisualEffects);
        }
        private void neverNotifyUser() {
            if(enable.checkBoxNeverNotifyUser.Checked) {
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
            if(enable.checkBoxSetHostName.Checked) {
                return;
            }
            ComputerName FormComputerName = new ComputerName(enable);
            FormComputerName.Show();
        }
        private void createTempPath() {
            if(enable.checkBoxTemp.Checked) {
                return;
            }
            if(!Directory.Exists("C:\\Temp")) {
                DirectoryInfo info = Directory.CreateDirectory("C:\\Temp");
                bool exists = info.Exists;

                if(exists) {
                    enable.checkBoxTemp.Checked = true;
                    //enable.checkBoxTemp.ForeColor = Color.Green;
                }
            } else {
                enable.checkBoxTemp.Checked = true;
                //enable.checkBoxTemp.ForeColor = Color.Green;
            }
        }

    }

}
