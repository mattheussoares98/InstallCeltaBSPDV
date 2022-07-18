
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

        private async Task ConfigureFirewall() {

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

            //Thread.Sleep(new TimeSpan(hours: 0, minutes: 0, seconds: 1)); //se não fizer isso, as vezes tenta criar a regra do PING enquanto tá excluindo aí da erro

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

            await Task.Run(() => firewallPolicy9092.Rules.Remove("9092"));

            await Task.Run(() => firewallPolicy9092.Rules.Add(firewallRule9092));

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

            await Task.Run(() => firewallPolicy!.Rules.Remove("27017"));
            await Task.Run(() => firewallPolicy!.Rules.Add(firewallRule27017));

            richTextBoxResults.Text += "Firewall: Regra da porta 27017 adicionada\n";
            #endregion


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

            try {
                //await Task.Run(() => );
                Process.Start(pStartInfo);
                richTextBoxResults.Text += "Adicionando recursos do IIS. Não feche a janela do CMD! Após a conclusão do CMD, confirme se instalou os recursos do IIS e caso não tenha instalado, instale manualmente";
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
            string startupUiPath = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\CeltaWare.CBS.PDV.UI.exe";
            string startupPath = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\CeltaWare.CBS.PDV.exe";

            if(File.Exists(startupPath) || File.Exists(startupUiPath)) {
                richTextBoxResults.Text += "O PDV já está na pasta de inicialização automática do windows\n";
                return;
            } else if(!File.Exists(pdvPath)) {
                MessageBox.Show($"Não foi possível encontrar o arquivo {pdvPath}");
                return;
            }

            File.CreateSymbolicLink(startupPath, pdvPath);
            richTextBoxResults.Text += "PDV adicionado com sucesso na pasta de inicialização automática  do windows\n";
        }

        private async Task downloadFileTaskAsync(string fileName, string destinyPath) {
            HttpClient client = new HttpClient();

            string sourcePath = "C:\\install";

            if(!Directory.Exists(sourcePath)) {
                Directory.CreateDirectory(sourcePath);
            }

            string fileNamePath = sourcePath + "\\" + fileName;

            string uri = "http://177.103.179.36/downloads/lastversion/" + fileName;

            #region download files
            if(!File.Exists(fileNamePath)) {
                //richTextBoxResults.Text += "Baixando o " + fileName + ". Dependendo da velocidade da internet, esse processo pode ser demorado\n";
                //só tenta baixar o arquivo se ele não existir ainda
                try {
                    using(var s = await client.GetStreamAsync(uri)) {
                        using(var fs = new FileStream(fileNamePath, FileMode.CreateNew)) {
                            await s.CopyToAsync(fs);
                        }
                    }
                } catch(Exception ex) {
                    MessageBox.Show("Erro para baixar o arquivo: " + ex.Message);
                }
                richTextBoxResults.Text += fileName + " baixado com sucesso\n";
            } else {
                richTextBoxResults.Text += $"O {fileName} já foi baixado\n";
            }
            #endregion
        }
        private async Task extractFile(string sourceFilePath, string destinyPath, string fileName) {
            if(!File.Exists(sourceFilePath)) {
                DialogResult dialogResult = MessageBox.Show($"O {sourceFilePath} não existe. Deseja baixá-lo?", "Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if(dialogResult == DialogResult.Yes) {
                    try {
                        await downloadFileTaskAsync(fileName, destinyPath);
                    } catch(Exception ex) {
                        MessageBox.Show($"Erro para baixar o {fileName}: {ex.Message}");
                    }
                } else {
                    return;
                }
            }

            if(File.Exists(sourceFilePath) && !Directory.Exists(destinyPath)) {
                try {
                    MessageBox.Show("sourceFilePath: " + sourceFilePath);
                    MessageBox.Show("destinyPath: " + destinyPath);
                    await Task.Run(() => ZipFile.ExtractToDirectory(sourceFilePath, destinyPath));
                    //richTextBoxResults.Text += $"Extraindo os arquivos do {fileName}\n";
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para extrair o {fileName}: {ex.Message}");
                }
            }
        }
        private async Task overrideFiles(string pathToRead, string destiny) {
            if(!File.Exists(pathToRead)) {
                MessageBox.Show($"Não foi possível encontrar o caminho {pathToRead}");
            }
            string[] files = Directory.GetFiles(pathToRead);
            Task.Delay(300).Wait();

            foreach(string file in files) {
                string localDestiny = file.Replace(pathToRead, destiny);
                //MessageBox.Show($"file: {file}\ndestiny = " + destiny);
                try {
                    await Task.Run(() => File.Copy(file, localDestiny));

                } catch(Exception ex) {
                    MessageBox.Show($"Erro para copiar o arquivo para o destino\norigem: {file}\ndestino: {localDestiny}\nerro: {ex.Message}");
                }
                richTextBoxResults.Text += file + "\n";
            }
        }

        private async Task movePath(string sourcePath, string destinyPath) {
            if(!Directory.Exists(sourcePath)) {
                MessageBox.Show($"Não foi possível encontrar o caminho {sourcePath}");
            }

            if(!Directory.Exists(destinyPath)) {
                await Task.Run(() => Directory.Move(sourcePath, destinyPath));
                richTextBoxResults.Text += $"{sourcePath} movido com sucesso para o caminho {destinyPath}";
            } else {
                richTextBoxResults.Text += $"O caminho {destinyPath} já existe";
            }
        }
        private async Task createPathSharedSat() {
            #region directoryes
            string celtaSatPdvBin = "C:\\Celta SAT\\PDV\\Bin";
            string celtaSatPdv = "C:\\Celta SAT\\PDV";
            string celtaSat = "C:\\Celta SAT";
            string install = "c:\\install";
            string installDeployment = "C:\\install\\deployment";
            string installDeploymentPdv = "C:\\install\\deployment\\PDV";
            string installDeploymentZip = "C:\\install\\deployment.zip";
            string celtaSatSale = "C:\\Celta Sat\\PDV\\Sale";
            string celtaSatSat = "C:\\Celta Sat\\PDV\\Sat";

            string celtaSatPdvSalesalePath = "C:\\Celta SAT\\PDV\\Sale\\Release\\WebService";
            string celtaSatPdvSalePathBin = "C:\\Celta SAT\\PDV\\Sale\\Release\\WebService\\bin";

            string CeltaSatPdvSatPath = "C:\\Celta SAT\\PDV\\Sat\\Release\\WebService";
            string CeltaSatPdvSatPathBin = "C:\\Celta SAT\\PDV\\Sat\\Release\\WebService\\bin";
            #endregion


            if(!File.Exists(installDeploymentZip)) {
                //se não houver o deployment na pasta install, baixa ele novamente e chama o mesmo método para efetuar a extração dos arquivos e criação da pasta de compartilhamento do SAT

                await downloadFileTaskAsync("deployment.zip", install);

                await createPathSharedSat();

            } else {
                //se houver o arquivo do deployment.zip na pasta install, a aplicação extrai os arquivos, exclui a pasta de compartilhamento (se houver) e cria tudo novamente com os arquivos novos
                try {

                    if(Directory.Exists(celtaSat)) {
                        await Task.Run(() => Directory.Delete(celtaSat, true));
                    }

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


                    await overrideFiles(celtaSatPdvSalePathBin, celtaSatPdvBin);
                    await overrideFiles(CeltaSatPdvSatPathBin, celtaSatPdvBin);
                    await overrideFiles(celtaSatPdvSalesalePath, celtaSatPdv);
                    await overrideFiles(CeltaSatPdvSatPath, celtaSatPdv);

                    if(Directory.Exists(celtaSatSale)) {
                        Directory.Delete(celtaSatSale, true);
                    }

                    if(Directory.Exists(celtaSatSat)) {
                        Directory.Delete(celtaSatSat, true);
                    }
                } catch(Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void buttonConfigureFirewall_Click(object sender, EventArgs e) {
            buttonConfigureFirewall.Enabled = false;
            buttonConfigureFirewall.Text = "Aguarde";
            richTextBoxResults.Text = "";
            //o download do installbspdv e deployment precisam ser antes da criação do link para iniciar o app quando ligar a máquina
            await downloadFileTaskAsync("installbspdv.zip", "C:\\Install");
            await extractFile("C:\\install\\installbspdv.zip", "C:\\Install", "installbspdv.zip");
            await movePath("C:\\install\\pdv", "C:\\");
            //createLink();

            //await downloadFileTaskAsync("deployment.zip", "C:\\Install");
            //await createPathSharedSat();

            //await ConfigureFirewall();
            //await disableSuspendUSB();
            //createTempPath();
            //neverNotifyUser();
            //setMachineName();
            //enableIISFeatures();
            //enableAllPermissionsForMongoBin();
            //editMongoCfg();
            buttonConfigureFirewall.Text = "Efetuar configurações";
            buttonConfigureFirewall.Enabled = true;
        }
    }
}