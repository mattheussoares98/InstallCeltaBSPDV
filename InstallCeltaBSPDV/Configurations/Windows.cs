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
    public static class Windows {

        public static async Task configureWindows(EnableConfigurations enable) {
            await configureFirewall(enable);
            await disableSuspendUSB(enable);
            openAdjustVisualEffects();
            neverNotifyUser();
            setMachineName(enable);
            createTempPath(enable);
        }
        public static async Task enableAllPermissionsForPath(string path, EnableConfigurations enable) {
            if(!Directory.Exists(path)) {
                enable.richTextBoxResults.Text += $"O caminho {path} não foi encontrado para habilitar a permissão para todos usuários\n\n";
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

        public static async Task movePath(string sourcePath, string destinyPath, EnableConfigurations enable) {
            if(!Directory.Exists(sourcePath)) {
                MessageBox.Show($"Não foi possível encontrar o caminho {sourcePath}");
                return;
            }

            if(Directory.Exists(destinyPath)) {
                enable.richTextBoxResults.Text += $"Como o diretório {destinyPath} já existe, não fará a cópia da pasta para o diretório\n\n";
                return;
            }

            try {
                await enableAllPermissionsForPath(sourcePath, enable); //coloquei pra habilitar permissão pra todos nessa pasta porque em um teste que eu fiz, deu erro pra acessar essa pasta
                await enableAllPermissionsForPath(destinyPath, enable); //coloquei pra habilitar permissão pra todos nessa pasta porque em um teste que eu fiz, deu erro pra acessar essa pasta

                //Directory.Move(sourcePath, destinyPath);
                Directory.Move(sourcePath, destinyPath);
                Task.Delay(7000).Wait();
                //await Task.Run(() => Directory.Move(sourcePath, destinyPath));
                enable.richTextBoxResults.Text += $"{sourcePath} movido com sucesso para o caminho {destinyPath}\n\n";
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task extractFile(string sourceFilePath, string destinyPath, string fileName, EnableConfigurations enable, CheckBox checkBoxToMark = null) {
            if(!File.Exists(sourceFilePath)) {
                DialogResult dialogResult = MessageBox.Show($"O {sourceFilePath} não existe. Deseja baixá-lo?", "Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if(dialogResult == DialogResult.Yes) {
                    try {
                        await Download.downloadFileTaskAsync(fileName, enable);
                    } catch(Exception ex) {
                        MessageBox.Show($"Erro para baixar o {fileName}: {ex.Message}");
                    }
                } else {
                    return;
                }
            }

            if(File.Exists(sourceFilePath)) {
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

        public static async Task configureFirewall(EnableConfigurations enable) {
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
        private static async Task disableSuspendUSB(EnableConfigurations enable) {
            #region commands
            var hibernateAC = new ProcessStartInfo("cmd", "/c powercfg /x -hibernate-timeout-ac 0");
            var hibernateDC = new ProcessStartInfo("cmd", "/c powercfg /x -hibernate-timeout-dc 0");
            var diskTimeOutAC = new ProcessStartInfo("cmd", "/c powercfg /x -disk-timeout-ac 0");
            var diskTimeOutDC = new ProcessStartInfo("cmd", "/c powercfg /x -disk-timeout-dc 0");
            var monitorTimeOutAC = new ProcessStartInfo("cmd", "/c powercfg /x -monitor-timeout-ac 0");
            var monitorTimeOutDC = new ProcessStartInfo("cmd", "/c powercfg /x -monitor-timeout-dc 0");
            var standybyTimeoutAC = new ProcessStartInfo("cmd", "/c Powercfg /x -standby-timeout-ac 0");
            var standybyTimeoutDC = new ProcessStartInfo("cmd", "/c powercfg /x -standby-timeout-dc 0");
            var neverHibernate = new ProcessStartInfo("cmd", "/c powercfg /hibernate off");
            var disableUsbStandbyBattery = new ProcessStartInfo("cmd", "/c powercfg /SETDCVALUEINDEX SCHEME_CURRENT 2a737441-1930-4402-8d77-b2bebba308a3 48e6b7a6-50f5-4782-a5d4-53bb8f07e226 0"); //desabilitar suspensão da USB
            var disableUsbStandbyPlugged = new ProcessStartInfo("cmd", "/c powercfg /SETACVALUEINDEX SCHEME_CURRENT 2a737441-1930-4402-8d77-b2bebba308a3 48e6b7a6-50f5-4782-a5d4-53bb8f07e226 0"); //desabilitar suspensão da USB

            var enableMaxPerformance = new ProcessStartInfo("cmd", "/c powercfg -duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61"); //habilita o plano de desempenho máximo

            var selectMaxPerformance = new ProcessStartInfo("cmd", "/c powercfg /setactive a69d9b1c-b6ca-4e5d-a983-a3e9d2497239"); //torna o plano de desempenho máximo como ativo

            #endregion

            #region dont show a command line
            hibernateAC.CreateNoWindow = true;
            hibernateDC.CreateNoWindow = true;
            diskTimeOutAC.CreateNoWindow = true;
            diskTimeOutDC.CreateNoWindow = true;
            monitorTimeOutAC.CreateNoWindow = true;
            monitorTimeOutDC.CreateNoWindow = true;
            standybyTimeoutAC.CreateNoWindow = true;
            standybyTimeoutDC.CreateNoWindow = true;
            neverHibernate.CreateNoWindow = true;
            disableUsbStandbyBattery.CreateNoWindow = true;
            disableUsbStandbyPlugged.CreateNoWindow = true;
            enableMaxPerformance.CreateNoWindow = true;
            selectMaxPerformance.CreateNoWindow = true;
            #endregion

            try {
                await Task.Run(() => Process.Start(enableMaxPerformance));
                await Task.Run(() => Process.Start(selectMaxPerformance));
                //esses dois comandos acima precisam ser chamados antes dos posteriores, porque eles mudam o plano selecionado pro de desempenho máximo
                await Task.Run(() => Process.Start(hibernateAC));
                await Task.Run(() => Process.Start(hibernateDC));
                await Task.Run(() => Process.Start(diskTimeOutAC));
                await Task.Run(() => Process.Start(diskTimeOutDC));
                await Task.Run(() => Process.Start(monitorTimeOutAC));
                await Task.Run(() => Process.Start(monitorTimeOutDC));
                await Task.Run(() => Process.Start(standybyTimeoutAC));
                await Task.Run(() => Process.Start(standybyTimeoutDC));
                await Task.Run(() => Process.Start(neverHibernate));
                await Task.Run(() => Process.Start(disableUsbStandbyBattery));
                await Task.Run(() => Process.Start(disableUsbStandbyPlugged));
                enable.checkBoxSuspendUSB.Checked = true;
                //enable.checkBoxSuspendUSB.ForeColor = Color.Green;
                enable.checkBoxSuspendMonitorAndPC.Checked = true;
                //enable.checkBoxSuspendMonitorAndPC.ForeColor = Color.Green;
                //richTextBoxResults.Text += "Configurações de energia da USB, monitor e PCI efetuadas com sucesso\n\n";
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private static void openAdjustVisualEffects() {
            var adjustVisualEffects = new ProcessStartInfo("cmd", "/c %windir%\\system32\\SystemPropertiesPerformance.exe");
            adjustVisualEffects.CreateNoWindow = true;
            Process.Start(adjustVisualEffects);
        }
        private static void neverNotifyUser() {
            var info = new ProcessStartInfo("cmd", @"/c C:\Windows\System32\UserAccountControlSettings.exe");
            info.CreateNoWindow = true;
            try {
                Process.Start(info);



            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            //dessa forma abaixo desabilita automaticamente para não pedir permissão de admin quando executar algo como admin, mas precisa reiniciar a CPU pra aplicar as alterações. Por isso coloquei pra abrir a tela pra desabilitar manualmente conforme acima

            //try {
            //    RegistryKey key =
            //    Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true);
            //    key.SetValue("EnableLUA", "0", RegistryValueKind.DWord);
            //    key.Close();


            //} catch(Exception e) {
            //    MessageBox.Show("Error: " + e);
            //}

        }
        private static void setMachineName(EnableConfigurations enable) {
            ComputerName FormComputerName = new ComputerName(enable);
            FormComputerName.Show();
        }
        private static void createTempPath(EnableConfigurations enable) {
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
