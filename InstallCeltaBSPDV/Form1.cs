
using Microsoft.Win32;
using NetFwTypeLib;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace InstallCeltaBSPDV {
    public partial class Form1: Form {
        public Form1() {
            InitializeComponent();
        }
        private void ConfigureFirewall() {
            // https://support.microsoft.com/en-us/help/947709/how-to-use-the-netsh-advfirewall-firewall-context-instead-of-the-netsh

            //Remove any rule with the same name. Otherwise every time you run this code a new rule is added.  
            Process removePort = new Process {
                StartInfo = {
                    FileName = "netsh",
                    Arguments = $@"advfirewall firewall delete rule name=""9092, 27017""",
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true
                }
            };

            Process removePING = new Process {
                StartInfo = {
                    FileName = "netsh",
                    Arguments = $@"advfirewall firewall delete rule name=""9092, 27017""",
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true
                }
            };
            try {
                removePort.Start();
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
        private void disableSuspendUSB() {
            var info = new ProcessStartInfo("cmd", "/C powercfg /change -monitor-timeout-ac 0");
            info.WindowStyle = ProcessWindowStyle.Hidden;
            try {
                Process.Start(info);

                var two = new ProcessStartInfo("cmd", "/c powercfg /SETDCVALUEINDEX SCHEME_CURRENT 2a737441-1930-4402-8d77-b2bebba308a3 48e6b7a6-50f5-4782-a5d4-53bb8f07e226 0");

                var four = new ProcessStartInfo("cmd", "/c powercfg /SETACVALUEINDEX SCHEME_CURRENT 2a737441-1930-4402-8d77-b2bebba308a3 48e6b7a6-50f5-4782-a5d4-53bb8f07e226 0");

                Process.Start(two);
                Process.Start(four);
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private bool SetMachineName(string newName) {
            RegistryKey key = Registry.LocalMachine;

            string activeComputerName = "SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ActiveComputerName";
            RegistryKey activeCmpName = key.CreateSubKey(activeComputerName);
            activeCmpName.SetValue("ComputerName", newName);
            activeCmpName.Close();
            string computerName = "SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerName";
            RegistryKey cmpName = key.CreateSubKey(computerName);
            cmpName.SetValue("ComputerName", newName);
            cmpName.Close();
            string _hostName = "SYSTEM\\CurrentControlSet\\services\\Tcpip\\Parameters\\";
            RegistryKey hostName = key.CreateSubKey(_hostName);
            hostName.SetValue("Hostname", newName);
            hostName.SetValue("NV Hostname", newName);
            hostName.Close();
            return true;
        }
        private void buttonConfigureFirewall_Click(object sender, EventArgs e) {

            ConfigureFirewall();
            disableSuspendUSB();
            SetMachineName("Nome");
        }
    }
}