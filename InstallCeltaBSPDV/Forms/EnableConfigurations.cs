
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
            checkBoxInstallRoboMongo.Enabled = false;
            #endregion

            await new Windows(this).configureWindows();

            await new BsPdv(this).configureBsPdv();

            await new SharedSat(this).createSharedSat();

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
            checkBoxInstallRoboMongo.Enabled = true;
            #endregion
        }

        private void button1_Click(object sender, EventArgs e) {
            DownloadFilesForm downloadFiles = new(this);
            downloadFiles.ShowDialog();
        }
    }
}