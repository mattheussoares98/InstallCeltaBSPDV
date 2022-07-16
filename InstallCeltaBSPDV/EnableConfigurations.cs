
using Microsoft.Win32;
using NetFwTypeLib;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO.Compression;

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
                MessageBox.Show("Erro para remover o PING: " + ex.Message);
            }

            Thread.Sleep(new TimeSpan(hours: 0, minutes: 0, seconds: 1)); //se não fizer isso, as vezes tenta criar a regra do PING enquanto tá excluindo aí da erro

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
                MessageBox.Show("Erro para criar as regras do PING: " + ex.Message);
            }

            try {
                INetFwPolicy2 firewallPolicyPING = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

                var rule = firewallPolicyPING!.Rules.Item("PING"); // Name of your rule here
                rule.EdgeTraversal = true; // Update the rule here. Nothing else needed to persist the changes
                richTextBoxResults.Text += "Firewall: Regra de PING adicionada\n";
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

            firewallPolicy9092.Rules.Remove("9092");
            firewallPolicy9092.Rules.Add(firewallRule9092);

            richTextBoxResults.Text += "Firewall: Regra da porta 9092 adicionada\n";

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

            richTextBoxResults.Text += "Firewall: Regra da porta 27017 adicionada\n";
            #endregion


        }

        private void disableSuspendUSB() {
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

            try {
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

                richTextBoxResults.Text += "Configurações de energia da USB, monitor e PCI efetuadas com sucesso\n";
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void setMachineName() {
            Form FormComputerName = new ComputerName();
            FormComputerName.Show();
        }

        private void createTempPath() {
            if(!Directory.Exists(@"C:\temp")) {
                Directory.CreateDirectory(@"C:\Temp");
            }
            richTextBoxResults.Text += "Pasta 'C:\\temp' criada\n";
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

            string command = "START /WAIT DISM /Online /Enable-Feature /FeatureName:IIS-ApplicationDevelopment /FeatureName:IIS-ASP /FeatureName:IIS-ASPNET /FeatureName:IIS-BasicAuthentication /FeatureName:IIS-CGI /FeatureName:IIS-ClientCertificateMappingAuthentication /FeatureName:IIS-CommonHttpFeatures /FeatureName:IIS-CustomLogging /FeatureName:IIS-DefaultDocument /FeatureName:IIS-DigestAuthentication /FeatureName:IIS-DirectoryBrowsing /FeatureName:IIS-FTPExtensibility /FeatureName:IIS-FTPServer /FeatureName:IIS-FTPSvc /FeatureName:IIS-HealthAndDiagnostics /FeatureName:IIS-HostableWebCore /FeatureName:IIS-HttpCompressionDynamic /FeatureName:IIS-HttpCompressionStatic /FeatureName:IIS-HttpErrors /FeatureName:IIS-HttpLogging /FeatureName:IIS-HttpRedirect /FeatureName:IIS-HttpTracing /FeatureName:IIS-IIS6ManagementCompatibility /FeatureName:IIS-IISCertificateMappingAuthentication /FeatureName:IIS-IPSecurity /FeatureName:IIS-ISAPIExtensions /FeatureName:IIS-ISAPIFilter /FeatureName:IIS-LegacyScripts /FeatureName:IIS-LegacySnapIn /FeatureName:IIS-LoggingLibraries /FeatureName:IIS-ManagementConsole /FeatureName:IIS-ManagementScriptingTools /FeatureName:IIS-ManagementService /FeatureName:IIS-Metabase /FeatureName:IIS-NetFxExtensibility /FeatureName:IIS-ODBCLogging /FeatureName:IIS-Performance /FeatureName:IIS-RequestFiltering /FeatureName:IIS-RequestMonitor /FeatureName:IIS-Security /FeatureName:IIS-ServerSideIncludes /FeatureName:IIS-StaticContent /FeatureName:IIS-URLAuthorization /FeatureName:IIS-WebDAV /FeatureName:IIS-WebServer /FeatureName:IIS-WebServerManagementTools /FeatureName:IIS-WebServerRole /FeatureName:IIS-WindowsAuthentication /FeatureName:IIS-WMICompatibility /FeatureName:WAS-ConfigurationAPI /FeatureName:WAS-NetFxEnvironment /FeatureName:WAS-ProcessModel /FeatureName:WAS-WindowsActivationService";

            ProcessStartInfo pStartInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            Process p = new Process();
            p.StartInfo = pStartInfo;
            try {
                p.Start();
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void enableAllPermissionsForMongoBin() {
            string programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
            string mongoBin = programFiles += "\\MongoDB\\Server\\4.0\\bin";

            #region Edit permissions

            const FileSystemRights rights = FileSystemRights.FullControl;

            var allUsers = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);

            // Add Access Rule to the actual directory itself
            var accessRule = new FileSystemAccessRule(
                allUsers,
                rights,
                InheritanceFlags.None,
                PropagationFlags.NoPropagateInherit,
                AccessControlType.Allow);

            if(!Directory.Exists(mongoBin)) {
                richTextBoxResults.Text += $"O caminho {mongoBin} não foi encontrado. Instale o MondoDB antes de executar o programa novamente!\n";
                return;
            }

            var info = new DirectoryInfo(mongoBin);
            var security = info.GetAccessControl(AccessControlSections.Access);

            bool result;
            security.ModifyAccessRule(AccessControlModification.Set, accessRule, out result);

            if(!result) {
                throw new InvalidOperationException("Failed to give full-control permission to all users for mongoBin " + mongoBin);
            } else {
                richTextBoxResults.Text += $"Adicionado permissão para todos usuários na pasta {mongoBin}\n";
            }

            // add inheritance
            var inheritedAccessRule = new FileSystemAccessRule(
                allUsers,
                rights,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.InheritOnly,
                AccessControlType.Allow);

            bool inheritedResult;
            security.ModifyAccessRule(AccessControlModification.Add, inheritedAccessRule, out inheritedResult);

            if(!inheritedResult) {
                throw new InvalidOperationException("Failed to give full-control permission inheritance to all users for " + programFiles);
            }

            info.SetAccessControl(security);
            #endregion
        }

        private void editMongoCfg() {
            string programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
            string mongoBin = programFiles += "\\MongoDB\\Server\\4.0\\bin";
            string newText = "";
            string mongoConfig = mongoBin + "\\mongod.cfg";

            try {
                StreamReader sr = File.OpenText(mongoConfig);
                string? textoDoArquivo = sr.ReadToEnd();
                newText = textoDoArquivo.Replace("127.0.0.1", "0.0.0.0");
                sr.Close();

                Task.Delay(3000).Wait();
                File.Delete(mongoConfig);

                Task.Delay(1000).Wait();
                File.CreateText(mongoConfig).Close();

            } catch(Exception e) {
                MessageBox.Show("Erro para ler os dados do arquivo: " + e.Message);
                Console.WriteLine(e.Message);
            }


            try {
                File.WriteAllText(mongoConfig, String.Empty);
                StreamWriter sw = new StreamWriter(mongoConfig);
                sw.WriteLine(newText);
                sw.Close();
            } catch(Exception ex) {
                MessageBox.Show("Erro para escrever os dados no mongod.cfg: " + ex.Message);
            }
        }

        private void createLink() {
            string pdvPath = "C:\\CeltaBSPDV\\CeltaWare.CBS.PDV.UI.exe";
            string startupPath = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\CeltaWare.CBS.PDV.UI.exe";

            if(!File.Exists(pdvPath)) {
                MessageBox.Show($"Não foi possível encontrar o arquivo {pdvPath}");
                return;
            } else if(File.Exists(startupPath)) {
                richTextBoxResults.Text += "PDV adicionado com sucesso na pasta de inicialização automática  do windows\n";
                return;
            }

            File.CreateSymbolicLink(startupPath, pdvPath);
            richTextBoxResults.Text += "PDV adicionado com sucesso na pasta de inicialização automática  do windows\n";
        }

        private async Task DownloadFileTaskAsync(string fileName, string destinyPath) {
            //coloquei o valor padrão como "" para quando for extrair os arquivos do PDV, não extrair para a pasta C:\install\pdv\pdv
            HttpClient client = new HttpClient();

            string sourcePath = "C:\\install";

            if(!Directory.Exists(sourcePath)) {
                Directory.CreateDirectory(sourcePath);
            }

            string fileNamePath = sourcePath + "\\" + fileName;

            string uri = "http://177.103.179.36/downloads/lastversion/" + fileName;

            #region download files
            if(!File.Exists(fileNamePath)) {
                richTextBoxResults.Text += "Baixando o " + fileName + ". Dependendo da velocidade da internet, esse processo pode ser demorado\n";
                //só tenta baixar o arquivo se ele não existir ainda
                try {
                    using(var s = await client.GetStreamAsync(uri)) {
                        using(var fs = new FileStream(fileNamePath, FileMode.CreateNew)) {
                            await s.CopyToAsync(fs);

                            s.Position = 0;
                        }
                    }
                    richTextBoxResults.Text += fileName + " baixado com sucesso\n";
                } catch(Exception ex) {
                    MessageBox.Show("Erro para baixar o arquivo: " + ex.Message);
                }
            } else {
                richTextBoxResults.Text += $"O {fileName} já foi baixado\n";
            }
            #endregion

            #region extract paths
            destinyPath = sourcePath + "\\" + destinyPath;
            if(File.Exists(fileNamePath) && !Directory.Exists(destinyPath)) {
                try {
                    ZipFile.ExtractToDirectory(fileNamePath, destinyPath);
                    richTextBoxResults.Text += $"Extraindo os arquivos do {fileName}\n";
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para extrair o {fileName}: {ex.Message}");
                }
            } else {
                richTextBoxResults.Text += $"Os arquivos do {fileName} já foram extraídos\n";
            }
            #endregion
        }

        private void readDataToCopy(string pathToRead, string destiny) {
            string[] files = Directory.GetFiles(pathToRead);
            Task.Delay(300).Wait();

            foreach(string file in files) {
                string localDestiny = file.Replace(pathToRead, destiny);
                //MessageBox.Show($"file: {file}\ndestiny = " + destiny);
                try {
                    File.Copy(file, localDestiny);
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para copiar o arquivo para o destino\norigem: {file}\ndestino: {localDestiny}\nerro: {ex.Message}");
                }
                Task.Delay(300).Wait();
                richTextBoxResults.Text += file + "\n";

            }
        }

        private async Task createPathSharedSat() {

            string celtaSatPdvBin = "C:\\Celta SAT\\PDV\\Bin";
            string celtaSatPdv = "C:\\Celta SAT\\PDV";
            string celtaSat = "C:\\Celta SAT";
            string installDeployment = "C:\\install\\deployment";
            string installDeploymentPdv = "C:\\install\\deployment\\PDV";
            string installDeploymentZip = "C:\\install\\deployment.zip";

            string celtaSatPdvSalesalePath = "C:\\Celta SAT\\PDV\\Sale\\Release\\WebService";
            string celtaSatPdvSalePathBin = "C:\\Celta SAT\\PDV\\Sale\\Release\\WebService\\bin";

            string CeltaSatPdvSatPath = "C:\\Celta SAT\\PDV\\Sat\\Release\\WebService";
            string CeltaSatPdvSatPathBin = "C:\\Celta SAT\\PDV\\Sat\\Release\\WebService\\bin";


            if(Directory.Exists(celtaSat)) {
                Directory.Delete(celtaSat, true); //se houver uma pasta com esse nome, da erro pra mover o arquivo pra essa pasta. O true serve para excluir todos arquivos que estiverem nessa pasta
            }

            Directory.CreateDirectory(celtaSat);

            if(File.Exists(installDeploymentZip)) {
                if(!Directory.Exists(celtaSat)) {
                    Directory.Delete(celtaSat);
                }

                Directory.Delete(installDeployment, true);

                ZipFile.ExtractToDirectory(installDeploymentZip, installDeployment);
                Task.Delay(13000).Wait(); //para ter certeza que vai aguardar a extração dos arquivos para fazer a cópia

                //if(!Directory.Exists(celtaSatPdvBin)) {
                //    Directory.CreateDirectory(celtaSatPdvBin);
                //}

                try {
                    Directory.Move(installDeploymentPdv, celtaSatPdv);
                    Task.Delay(900).Wait();
                } catch(Exception ex) {
                    MessageBox.Show("else if error = " + ex.Message);
                }

                if(!Directory.Exists(celtaSatPdvBin)) {
                    Directory.CreateDirectory(celtaSatPdvBin);
                }
                //readDataToCopy(celtaSatPdvSalesalePath, celtaSat);
                readDataToCopy(celtaSatPdvSalePathBin, celtaSatPdvBin);
                readDataToCopy(CeltaSatPdvSatPathBin, celtaSatPdvBin);
                readDataToCopy(celtaSatPdvSalesalePath, celtaSatPdv);
                readDataToCopy(CeltaSatPdvSatPath, celtaSatPdv);
            } else {
                MessageBox.Show("ELSEEEE!!!!!");
                if(Directory.Exists(installDeployment)) {
                    Directory.Delete(installDeployment);
                }
                await DownloadFileTaskAsync("deployment.zip", "deployment");
                await createPathSharedSat();
            }

            if(Directory.Exists("C:\\Celta SAT\\PDV\\Sale")) {
                Directory.Delete("C:\\Celta SAT\\PDV\\Sale", true);
            } 
            if(Directory.Exists("C:\\Celta SAT\\PDV\\sat")) {
                Directory.Delete("C:\\Celta SAT\\PDV\\sat", true);
            }
        }

        private async void buttonConfigureFirewall_Click(object sender, EventArgs e) {
            buttonConfigureFirewall.Enabled = false;
            buttonConfigureFirewall.Text = "Aguarde";
            richTextBoxResults.Text = "";
            //await DownloadFileTaskAsync("installbspdv.zip", "PDV");
            //await DownloadFileTaskAsync("deployment.zip", "deployment");
            await createPathSharedSat();
            //ConfigureFirewall();
            //Task.Delay(3000).Wait();
            //disableSuspendUSB();
            //Task.Delay(3000).Wait();
            //createTempPath();
            //Task.Delay(3000).Wait();
            //neverNotifyUser();
            //Task.Delay(3000).Wait();
            //setMachineName();
            //Task.Delay(3000).Wait();
            //enableIISFeatures();
            //Task.Delay(3000).Wait();
            //enableAllPermissionsForMongoBin();
            //editMongoCfg();
            //Task.Delay(3000).Wait();
            //createLink();
            buttonConfigureFirewall.Text = "Efetuar configurações";
            buttonConfigureFirewall.Enabled = true;
        }
    }
}