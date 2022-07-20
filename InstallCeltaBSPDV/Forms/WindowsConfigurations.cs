//using NetFwTypeLib;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace InstallCeltaBSPDV.Forms {
//    internal static  class WindowsConfigurations {

//        static async Task ConfigureFirewall() {

//            #region ICMPv4 - PING

//            //quando adiciona o processo igual está adicionando a porta 9092 e 27017, não tem opção pra criar como protocolo ICMPv4. Por isso, configurei conforme abaixo e editei a permissão
//            Process removePING = new Process {
//                StartInfo = {
//                    FileName = "netsh",
//                    Arguments = $@"advfirewall firewall delete rule name=""PING""",
//                    UseShellExecute = false,
//                    WindowStyle = ProcessWindowStyle.Hidden,
//                    RedirectStandardOutput = true
//                }
//            };

//            try {
//                await Task.Run(() => removePING.Start());
//            } catch(Exception ex) {
//                MessageBox.Show("Erro para remover o PING: " + ex.Message);
//            }

//            Process pingProcess = new Process {
//                StartInfo = {
//                    FileName = "netsh",
//                    Arguments = $@"advfirewall firewall add rule name = ""PING"" protocol = ICMPv4:any,any dir =in action = allow",
//                    UseShellExecute = false,
//                    WindowStyle = ProcessWindowStyle.Hidden,
//                    RedirectStandardOutput = true
//                }
//            };

//            Task.Delay(5000).Wait();

//            try {
//                await Task.Run(() => pingProcess.Start());

//                await Task.Run(() => pingProcess.StandardOutput.ReadToEnd());

//                await Task.Run(() => pingProcess.WaitForExit());

//            } catch(Exception ex) {
//                MessageBox.Show("Erro para criar as regras do PING: " + ex.Message);
//            }

//            try {
//                INetFwPolicy2 firewallPolicyPING = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

//                var rule = firewallPolicyPING!.Rules.Item("PING"); // Name of your rule here
//                rule.EdgeTraversal = true; // Update the rule here. Nothing else needed to persist the changes
//                richTextBoxResults.Text += "Firewall: Regra de PING adicionada\n\n";
//            } catch(Exception ex) {
//                MessageBox.Show("Erro para editar o PING: " + ex.Message);
//            }
//            #endregion

//            #region porta 9092
//            INetFwRule firewallRule9092 = (INetFwRule)Activator.CreateInstance(
//    Type.GetTypeFromProgID("HNetCfg.FWRule"));
//            firewallRule9092.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
//            firewallRule9092.Description = "Serve para outras máquinas da mesma rede conseguirem acessar o site de compartilhamento do SAT que é criado por padrão com a porta 9092";
//            firewallRule9092.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
//            firewallRule9092.Enabled = true;
//            firewallRule9092.InterfaceTypes = "All";
//            firewallRule9092.Name = "9092";
//            firewallRule9092.EdgeTraversal = true;
//            firewallRule9092.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
//            firewallRule9092.LocalPorts = "9092";
//            INetFwPolicy2 firewallPolicy9092 = (INetFwPolicy2)Activator.CreateInstance(
//                Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

//            await Task.Run(() => firewallPolicy9092.Rules.Remove("9092"));

//            await Task.Run(() => firewallPolicy9092.Rules.Add(firewallRule9092));

//            richTextBoxResults.Text += "Firewall: Regra da porta 9092 adicionada\n\n";

//            #endregion

//            #region porta 27017
//            INetFwRule firewallRule27017 = (INetFwRule)Activator.CreateInstance(
//               Type.GetTypeFromProgID("HNetCfg.FWRule"));

//            firewallRule27017.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
//            firewallRule27017.Description = "Serve para conseguir acessar o banco de dados do PDV a partir de outras máquinas que estão na mesma rede";
//            firewallRule27017.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
//            firewallRule27017.Enabled = true;
//            firewallRule27017.InterfaceTypes = "All";
//            firewallRule27017.Name = "27017";
//            firewallRule27017.EdgeTraversal = true;
//            firewallRule27017.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
//            firewallRule27017.LocalPorts = "27017";
//            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(
//                Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

//            await Task.Run(() => firewallPolicy!.Rules.Remove("27017"));
//            await Task.Run(() => firewallPolicy!.Rules.Add(firewallRule27017));

//            richTextBoxResults.Text += "Firewall: Regra da porta 27017 adicionada\n\n";
//            #endregion


//        }

//    }
//}
