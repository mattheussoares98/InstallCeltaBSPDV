using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstallCeltaBSPDV {
    public partial class ComputerName: Form {
        public ComputerName() {
            InitializeComponent();
            maskedTextBoxSetComputerName.Focus();
        }

        public void buttonSetComputerName_Click(object sender, EventArgs e) {
            RegistryKey key = Registry.LocalMachine;
            string newName = "PDV" + maskedTextBoxSetComputerName.Text;

            if(maskedTextBoxSetComputerName.Text.Length < 3) {
                MessageBox.Show("Digite o número do PDV com 3 números", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Warning, defaultButton: MessageBoxDefaultButton.Button1);
            } else {
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

                this.Close();
            }

        }

        private void maskedTextBoxSetComputerName_KeyUp(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter) {
                buttonSetComputerName_Click(null, null);
                this.Close();
            }
        }

        private void ComputerName_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = false;
        }
    }
}
