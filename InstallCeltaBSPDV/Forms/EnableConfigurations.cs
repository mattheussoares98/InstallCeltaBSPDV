
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
using InstallCeltaBSPDV.Forms;

namespace InstallCeltaBSPDV {
    public partial class EnableConfigurations: Form {

        public EnableConfigurations() {
            InitializeComponent();
            Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBSTOR", "Start", 4, Microsoft.Win32.RegistryValueKind.DWord);

            Task.Delay(3000).Wait();

            //Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBSTOR", "Start", 3, Microsoft.Win32.RegistryValueKind.DWord);
        }

        private void richTextBoxResults_TextChanged(object sender, EventArgs e) {
            //serve pra sempre que atualizar o texto do richTextBoxResults, navegar pro último caracter e rolar automaticamente pro final
            richTextBoxResults.SelectionStart = richTextBoxResults.Text.Length;
            richTextBoxResults.ScrollToCaret();
        }

        private async void buttonConfigurations_Click(object sender, EventArgs e) {
            #region disable components
            buttonConfigurations.Enabled = false;
            buttonConfigurations.Text = "Aguarde";
            richTextBoxResults.Text = "";
            progressBarInstall.Style = ProgressBarStyle.Marquee;
            progressBarInstall.MarqueeAnimationSpeed = 30;
            progressBarInstall.Visible = true;
            checkBoxFirewall.Enabled = false;
            checkBoxDisableSuspendUSB.Enabled = false;
            checkBoxSuspendMonitorAndPC.Enabled = false;
            checkBoxEnableFastBoot.Enabled = false;
            checkBoxTemp.Enabled = false;
            checkBoxSetHostName.Enabled = false;
            checkBoxCopyCetaBSPDV.Enabled = false;
            checkBoxPdvLink.Enabled = false;
            checkBoxInstallMongo.Enabled = false;
            checkBoxEnableRemoteAcces.Enabled = false;
            checkBoxInstallComponentsReport.Enabled = false;
            checkBoxCreateSharedSatSite.Enabled = false;
            ControlBox = false;
            #endregion

            if(!checkBoxCreateSharedSatSite.Checked) {
                new SharedSat(this).createSharedSat(); //não coloquei um await nele pra ir fazendo a instalação do site em segundo plano enquanto faz as outras configurações
            }

            await new Windows(this).configureWindows();

            await new BsPdv(this).configureBsPdv();

            if(checkBoxCreateSharedSatSite.Checked) {
                #region enable components
                buttonConfigurations.Text = "Efetuar configurações";
                buttonConfigurations.Enabled = true;
                progressBarInstall.Style = ProgressBarStyle.Continuous;
                progressBarInstall.MarqueeAnimationSpeed = 0;
                progressBarInstall.Visible = false;
                checkBoxFirewall.Enabled = true;
                checkBoxDisableSuspendUSB.Enabled = true;
                checkBoxSuspendMonitorAndPC.Enabled = true;
                checkBoxEnableFastBoot.Enabled = true;
                checkBoxTemp.Enabled = true;
                checkBoxSetHostName.Enabled = true;
                checkBoxCopyCetaBSPDV.Enabled = true;
                checkBoxPdvLink.Enabled = true;
                checkBoxInstallMongo.Enabled = true;
                checkBoxEnableRemoteAcces.Enabled = true;
                checkBoxInstallComponentsReport.Enabled = true;
                checkBoxCreateSharedSatSite.Enabled = true;
                ControlBox = true;
                #endregion
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            DownloadFilesForm downloadFiles = new(this);
            downloadFiles.ShowDialog();
        }


        private void labelDllsSat_Click(object sender, EventArgs e) {
            string windows = "C:\\Windows";
            string syswow64 = "C:\\Windows\\SysWOW64";
            string system32 = "C:\\Windows\\System32";
            string cCeltaBsPdv = "C:\\CeltaBSPDV";
            string cCeltaSatPdvBin = "C:\\CeltaSAT\\PDV\\Bin";
            if(folderBrowserDialog1.ShowDialog() != DialogResult.Cancel) {

                try {
                    new SharedSat(this).overrideFilesInPath(folderBrowserDialog1.SelectedPath, windows);
                    new SharedSat(this).overrideFilesInPath(folderBrowserDialog1.SelectedPath, syswow64);
                    new SharedSat(this).overrideFilesInPath(folderBrowserDialog1.SelectedPath, system32);
                    new SharedSat(this).overrideFilesInPath(folderBrowserDialog1.SelectedPath, cCeltaBsPdv);
                    new SharedSat(this).overrideFilesInPath(folderBrowserDialog1.SelectedPath, cCeltaSatPdvBin);

                    checkBoxCopyDllsSat.Checked = true;
                } catch(Exception ex) {
                    MessageBox.Show("Ocorreu erro para copiar as DLLs do SAT: " + ex.Message);
                }

            }
        }
    }
}