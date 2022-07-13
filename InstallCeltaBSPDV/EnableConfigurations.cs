
using Microsoft.Win32;
using NetFwTypeLib;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Net;
using System.Net.Sockets;

namespace InstallCeltaBSPDV {
    public partial class EnableConfigurations: Form {
        public EnableConfigurations() {
            InitializeComponent();
        }
        private void ConfigureFirewallWithoutEdgeTransversal() {
            //dessa forma abaixo não habilita os recursos de borda, por isso deixei conforme o método ConfigureFirewall();

            // https://support.microsoft.com/en-us/help/947709/how-to-use-the-netsh-advfirewall-firewall-context-instead-of-the-netsh

            //Remove any rule with the same name. Otherwise every time you run this code a new rule is added.  
            Process removePort = new Process {
                StartInfo = {
                    FileName = "netsh",
                    Arguments = $@"advfirewall firewall delete rule name=""9092, 27017"" EdgeTraversal = true",
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                }
            };

            Process removePING = new Process {
                StartInfo = {
                    FileName = "netsh",
                    Arguments = $@"advfirewall firewall delete rule name=""PING""",
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true
                }
            };
            try {
                removePort.Start();
                removePING.Start();
                var output = removePort.StandardOutput.ReadToEnd();
                removePort.WaitForExit();
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
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

            try {
                pingProcess.Start();
                var output = pingProcess.StandardOutput.ReadToEnd();
                pingProcess.WaitForExit();
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }

            Process portProcess = new Process {
                StartInfo = {
                    FileName = "netsh",
                    Arguments = $@"advfirewall firewall add rule name=""9092, 27017"" protocol=TCP localport=9092,27017 dir=in action=allow",
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true
                }
            };

            try {
                portProcess.Start();
                var output = portProcess.StandardOutput.ReadToEnd();
                portProcess.WaitForExit();
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void ConfigureFirewall() {

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
            try {
                removePING.Start();
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
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

            INetFwPolicy2 firewallPolicyPING = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            try {
                pingProcess.Start();
                var output = pingProcess.StandardOutput.ReadToEnd();
                pingProcess.WaitForExit();



                var rule = firewallPolicyPING!.Rules.Item("PING"); // Name of your rule here
                rule.EdgeTraversal = true; // Update the rule here. Nothing else needed to persist the changes
                richTextBoxResults.Text += "Firewall: Regra de PING adicionada";
            } catch(Exception ex) {
                MessageBox.Show("Erro para editar as regras do PING: " + ex.Message);
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

            firewallPolicy9092.Rules.Remove("9092");
            firewallPolicy9092.Rules.Add(firewallRule9092);

            richTextBoxResults.Text += "\nFirewall: Regra da porta 9092 adicionada";
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

            firewallPolicy.Rules.Remove("27017");
            firewallPolicy.Rules.Add(firewallRule27017);

            richTextBoxResults.Text += "\nFirewall: Regra da porta 27017 adicionada";
            #endregion

            Task.Delay(1000); //estava dando erro as vezes pra editar a regra do PING antes de fazer isso

        }

        private void disableSuspendUSB() {
            var info = new ProcessStartInfo("cmd", "/C powercfg /change -monitor-timeout-ac 0"); //nunca desligar o vídeo
            info.WindowStyle = ProcessWindowStyle.Hidden;
            try {
                Process.Start(info);

                var one = new ProcessStartInfo("cmd", "/c powercfg /SETDCVALUEINDEX SCHEME_CURRENT 2a737441-1930-4402-8d77-b2bebba308a3 48e6b7a6-50f5-4782-a5d4-53bb8f07e226 0"); //desabilitar suspensão da USB

                var two = new ProcessStartInfo("cmd", "/c powercfg /SETACVALUEINDEX SCHEME_CURRENT 2a737441-1930-4402-8d77-b2bebba308a3 48e6b7a6-50f5-4782-a5d4-53bb8f07e226 0"); //desabilitar suspensão da USB

                Process.Start(one);
                Process.Start(two);
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }

            richTextBoxResults.Text += "\nDesabilitada a suspensão de USB para o plano de energia atual";
        }
        private void setMachineName() {
            Form FormComputerName = new ComputerName();
            FormComputerName.Show();
        }

        private void createTempPath() {
            if(!Directory.Exists(@"C:\temp")) {
                Directory.CreateDirectory(@"C:\Temp");
                MessageBox.Show("não possui mesmo");
            }
        }
        private void neverNotifyUser() {
            var info = new ProcessStartInfo("cmd", @"/c C:\Windows\System32\UserAccountControlSettings.exe");
            info.WindowStyle = ProcessWindowStyle.Hidden;
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

        private void enableIISFeatures() {
            var info = new ProcessStartInfo("cmd",
                @"/c START /WAIT DISM /Online /Enable-Feature /FeatureName:IIS-ApplicationDevelopment /FeatureName:IIS-ASP /FeatureName:IIS-ASPNET /FeatureName:IIS-BasicAuthentication /FeatureName:IIS-CGI /FeatureName:IIS-ClientCertificateMappingAuthentication /FeatureName:IIS-CommonHttpFeatures /FeatureName:IIS-CustomLogging /FeatureName:IIS-DefaultDocument /FeatureName:IIS-DigestAuthentication /FeatureName:IIS-DirectoryBrowsing /FeatureName:IIS-FTPExtensibility /FeatureName:IIS-FTPServer /FeatureName:IIS-FTPSvc /FeatureName:IIS-HealthAndDiagnostics /FeatureName:IIS-HostableWebCore /FeatureName:IIS-HttpCompressionDynamic /FeatureName:IIS-HttpCompressionStatic /FeatureName:IIS-HttpErrors /FeatureName:IIS-HttpLogging /FeatureName:IIS-HttpRedirect /FeatureName:IIS-HttpTracing /FeatureName:IIS-IIS6ManagementCompatibility /FeatureName:IIS-IISCertificateMappingAuthentication /FeatureName:IIS-IPSecurity /FeatureName:IIS-ISAPIExtensions /FeatureName:IIS-ISAPIFilter /FeatureName:IIS-LegacyScripts /FeatureName:IIS-LegacySnapIn /FeatureName:IIS-LoggingLibraries /FeatureName:IIS-ManagementConsole /FeatureName:IIS-ManagementScriptingTools /FeatureName:IIS-ManagementService /FeatureName:IIS-Metabase /FeatureName:IIS-NetFxExtensibility /FeatureName:IIS-ODBCLogging /FeatureName:IIS-Performance /FeatureName:IIS-RequestFiltering /FeatureName:IIS-RequestMonitor /FeatureName:IIS-Security /FeatureName:IIS-ServerSideIncludes /FeatureName:IIS-StaticContent /FeatureName:IIS-URLAuthorization /FeatureName:IIS-WebDAV /FeatureName:IIS-WebServer /FeatureName:IIS-WebServerManagementTools /FeatureName:IIS-WebServerRole /FeatureName:IIS-WindowsAuthentication /FeatureName:IIS-WMICompatibility /FeatureName:WAS-ConfigurationAPI /FeatureName:WAS-NetFxEnvironment /FeatureName:WAS-ProcessModel /FeatureName:WAS-WindowsActivationService"
);
            info.WindowStyle = ProcessWindowStyle.Hidden;
            try {
                Process.Start(info);
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void buttonConfigureFirewall_Click(object sender, EventArgs e) {
            ConfigureFirewall();
            enableIISFeatures();
            disableSuspendUSB();
            neverNotifyUser();
            setMachineName();
            createTempPath();
        }
    }
}