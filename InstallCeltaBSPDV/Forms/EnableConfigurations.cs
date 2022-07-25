
using Microsoft.Win32;
using NetFwTypeLib;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO.Compression;
using InstallCeltaBSPDV.Configurations;

namespace InstallCeltaBSPDV {
    public partial class EnableConfigurations: Form {

        public EnableConfigurations() {
            InitializeComponent();
        }
        private async Task ConfigureFirewall() {
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
                richTextBoxResults.Text += "Firewall: Regra de PING adicionada\n\n";
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
                richTextBoxResults.Text += "Firewall: Regra da porta 9092 adicionada\n\n";
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


            richTextBoxResults.Text += "Firewall: Regra da porta 27017 adicionada\n\n";
            #endregion

            if(pingVerify && sitePortVerify && mongoPortVerify) {
                checkBoxFirewall.Checked = true;
                //checkBoxFirewall.ForeColor = Color.Green;
                //checkBoxFirewall.ForeColor = Color.Green;
            }
        }

        private async Task disableSuspendUSB() {
            #region commands
            var hibernateAC = new ProcessStartInfo("cmd", "/c powercfg /x -hibernate-timeout-ac 0");
            var hibernateDC = new ProcessStartInfo("cmd", "/c powercfg /x -hibernate-timeout-dc 0");
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
            hibernateAC.CreateNoWindow = true;
            hibernateDC.CreateNoWindow = true;
            diskTimeOutAC.CreateNoWindow = true;
            diskTimeOutDC.CreateNoWindow = true;
            monitorTimeOutAC.CreateNoWindow = true;
            monitorTimeOutDC.CreateNoWindow = true;
            standybyTimeoutAC.CreateNoWindow = true;
            standybyTimeoutDC.CreateNoWindow = true;
            disableUsbStandbyBattery.CreateNoWindow = true;
            disableUsbStandbyPlugged.CreateNoWindow = true;
            #endregion

            try {
                await Task.Run(() => {
                    Process.Start(hibernateAC);
                    Process.Start(hibernateDC);
                    Process.Start(diskTimeOutAC);
                    Process.Start(diskTimeOutDC);
                    Process.Start(monitorTimeOutAC);
                    Process.Start(monitorTimeOutDC);
                    Process.Start(standybyTimeoutAC);
                    Process.Start(standybyTimeoutDC);
                    Process.Start(disableUsbStandbyBattery);
                    Process.Start(disableUsbStandbyPlugged);
                });
                checkBoxSuspendUSB.Checked = true;
                //checkBoxSuspendUSB.ForeColor = Color.Green;
                checkBoxSuspendMonitorAndPC.Checked = true;
                //checkBoxSuspendMonitorAndPC.ForeColor = Color.Green;
                //richTextBoxResults.Text += "Configurações de energia da USB, monitor e PCI efetuadas com sucesso\n\n";
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void setMachineName() {
            ComputerName FormComputerName = new ComputerName(this);
            FormComputerName.Show();
        }
        private void createTempPath() {
            if(!Directory.Exists("C:\\Temp")) {
                DirectoryInfo info = Directory.CreateDirectory("C:\\Temp");
                bool exists = info.Exists;

                if(exists) {
                    checkBoxTemp.Checked = true;
                    //checkBoxTemp.ForeColor = Color.Green;
                }
            } else {
                checkBoxTemp.Checked = true;
                //checkBoxTemp.ForeColor = Color.Green;
            }
        }
        private void neverNotifyUser() {
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

        private void enableIISFeatures() {

            string command = "START /WAIT DISM /Online /Enable-Feature /FeatureName:IIS-ApplicationDevelopment /FeatureName:IIS-ASP /FeatureName:IIS-ASPNET /FeatureName:IIS-BasicAuthentication /FeatureName:IIS-CGI /FeatureName:IIS-ClientCertificateMappingAuthentication /FeatureName:IIS-CommonHttpFeatures /FeatureName:IIS-CustomLogging /FeatureName:IIS-DefaultDocument /FeatureName:IIS-DigestAuthentication /FeatureName:IIS-DirectoryBrowsing /FeatureName:IIS-FTPExtensibility /FeatureName:IIS-FTPServer /FeatureName:IIS-FTPSvc /FeatureName:IIS-HealthAndDiagnostics /FeatureName:IIS-HostableWebCore /FeatureName:IIS-HttpCompressionDynamic /FeatureName:IIS-HttpCompressionStatic /FeatureName:IIS-HttpErrors /FeatureName:IIS-HttpLogging /FeatureName:IIS-HttpRedirect /FeatureName:IIS-HttpTracing /FeatureName:IIS-IIS6ManagementCompatibility /FeatureName:IIS-IISCertificateMappingAuthentication /FeatureName:IIS-IPSecurity /FeatureName:IIS-ISAPIExtensions /FeatureName:IIS-ISAPIFilter /FeatureName:IIS-LegacyScripts /FeatureName:IIS-LegacySnapIn /FeatureName:IIS-LoggingLibraries /FeatureName:IIS-ManagementConsole /FeatureName:IIS-ManagementScriptingTools /FeatureName:IIS-ManagementService /FeatureName:IIS-Metabase /FeatureName:IIS-NetFxExtensibility /FeatureName:IIS-ODBCLogging /FeatureName:IIS-Performance /FeatureName:IIS-RequestFiltering /FeatureName:IIS-RequestMonitor /FeatureName:IIS-Security /FeatureName:IIS-ServerSideIncludes /FeatureName:IIS-StaticContent /FeatureName:IIS-URLAuthorization /FeatureName:IIS-WebDAV /FeatureName:IIS-WebServer /FeatureName:IIS-WebServerManagementTools /FeatureName:IIS-WebServerRole /FeatureName:IIS-WindowsAuthentication /FeatureName:IIS-WMICompatibility /FeatureName:WAS-ConfigurationAPI /FeatureName:WAS-NetFxEnvironment /FeatureName:WAS-ProcessModel /FeatureName:WAS-WindowsActivationService";

            ProcessStartInfo pStartInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            pStartInfo.CreateNoWindow = true;

            try {
                //await Task.Run(() => );
                Process.Start(pStartInfo);
                richTextBoxResults.Text += "Adicionando recursos do IIS. Não feche a janela do CMD! Após a conclusão do CMD, confirme se instalou os recursos do IIS e caso não tenha instalado, instale manualmente\n\n";
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
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
        private async Task createPathSharedSat() {
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
                richTextBoxResults.Text += $"Como o {installDeploymentZip} não existe, a aplicação fará o download do arquivo para criar a pasta de compartilhamento do SAT atualizada\n\n";
                await Utils.downloadFileTaskAsync("deployment.zip");

                await createPathSharedSat();
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

                    checkBoxSharedPath.Checked = true;
                    //checkBoxSharedPath.ForeColor = Color.Green;
                } catch(Exception ex) {
                    MessageBox.Show(ex.Message);
                }

                try {

                    await Utils.downloadFileTaskAsync("web.config", "https://drive.google.com/u/1/uc?id=19D1bDda6HU4qa7tdbVppHFRmh0SsAoem&export=download");

                    await Task.Run(() => File.Move("C:\\Install\\web.config", "C:\\Celta SAT\\PDV\\web.config", true));
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para baixar o webConfig do compartilhamento do SAT: {ex.Message}");
                }
            }
        }
        private void openAdjustVisualEffects() {
            var adjustVisualEffects = new ProcessStartInfo("cmd", "/c %windir%\\system32\\SystemPropertiesPerformance.exe");
            adjustVisualEffects.CreateNoWindow = true;
            Process.Start(adjustVisualEffects);
        }
        private async void buttonConfigureFirewall_Click(object sender, EventArgs e) {
            buttonConfigureFirewall.Enabled = false;
            buttonConfigureFirewall.Text = "Aguarde";
            richTextBoxResults.Text = "";
            progressBarInstall.Style = ProgressBarStyle.Marquee;
            progressBarInstall.MarqueeAnimationSpeed = 30;
            progressBarInstall.Visible = true;
            flowLayoutPanel1.Enabled = false;
            this.ControlBox = false;

            #region windows
            await ConfigureFirewall();
            await disableSuspendUSB();
            openAdjustVisualEffects();
            neverNotifyUser();
            setMachineName();
            createTempPath();
            #endregion

            ////como esses processos abaixo são mais demorados e depende da velocidade da internet, deixei pra fazer por último

            await BsPdv.configureBsPdv(this);

            //coloquei essa parte pra fazer depois da parte do PDV porque ele tenta criar os atalhos do PDV e editar a conexão remota do banco

            #region sharedSAT
            DialogResult createSharedSat = MessageBox.Show("Deseja criar a pasta de compartilhamento do SAT?", "Criar compartilhamento do SAT", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if(createSharedSat.Equals(DialogResult.Yes)) {
                enableIISFeatures();
                await Utils.downloadFileTaskAsync("deployment.zip");
                await createPathSharedSat();
            }
            #endregion

            buttonConfigureFirewall.Text = "Efetuar configurações";
            buttonConfigureFirewall.Enabled = true;
            progressBarInstall.Style = ProgressBarStyle.Continuous;
            progressBarInstall.MarqueeAnimationSpeed = 0;
            progressBarInstall.Visible = false;
            flowLayoutPanel1.Enabled = true;
            this.ControlBox = true;
        }

        private void richTextBoxResults_TextChanged(object sender, EventArgs e) {
            richTextBoxResults.SelectionStart = richTextBoxResults.Text.Length;
            richTextBoxResults.ScrollToCaret();
        }
    }
}