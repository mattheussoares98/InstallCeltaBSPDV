
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
using System.Data.SQLite;
using System.Data;

namespace InstallCeltaBSPDV {
    public partial class EnableConfigurations: Form {
        public EnableConfigurations() {
            InitializeComponent();

            DatabaseLoadCheckeds.createTableAndInsertValues(this);
            DatabaseLoadCheckeds.selectAndUpdateCheckBoxValues(this);
        }

        private void richTextBoxResults_TextChanged(object sender, EventArgs e) {
            //serve pra sempre que atualizar o texto do richTextBoxResults, navegar pro �ltimo caracter e rolar automaticamente pro final
            richTextBoxResults.SelectionStart = richTextBoxResults.Text.Length;
            richTextBoxResults.ScrollToCaret();
        }

        private async void buttonConfigurations_Click(object sender, EventArgs e) {
            #region disable components
            buttonConfigurations.Enabled = false;
            buttonConfigurations.Text = "Aguarde";
            progressBarInstall.Style = ProgressBarStyle.Marquee;
            progressBarInstall.MarqueeAnimationSpeed = 30;
            progressBarInstall.Visible = true;
            cbFirewall.Enabled = false;
            cbUSB.Enabled = false;
            cbPCAndMonitor.Enabled = false;
            cbFastBoot.Enabled = false;
            cbTemp.Enabled = false;
            cbHostname.Enabled = false;
            cbCeltaBSPDV.Enabled = false;
            cbShortcut.Enabled = false;
            cbMongoDB.Enabled = false;
            cbRemoteAcces.Enabled = false;
            cbComponentsReport.Enabled = false;
            cbSite.Enabled = false;
            ControlBox = false;
            #endregion

            new SharedSat(this).createSharedSat(); //n�o coloquei um await nele pra ir fazendo a instala��o do site em segundo plano enquanto faz as outras configura��es


            await new Windows(this).configureWindows();

            await new BsPdv(this).configureBsPdv();

            if(cbSite.Checked) {
                #region enable components
                buttonConfigurations.Text = "Efetuar configura��es";
                buttonConfigurations.Enabled = true;
                progressBarInstall.Style = ProgressBarStyle.Continuous;
                progressBarInstall.MarqueeAnimationSpeed = 0;
                progressBarInstall.Visible = false;
                cbFirewall.Enabled = true;
                cbUSB.Enabled = true;
                cbPCAndMonitor.Enabled = true;
                cbFastBoot.Enabled = true;
                cbTemp.Enabled = true;
                cbHostname.Enabled = true;
                cbCeltaBSPDV.Enabled = true;
                cbShortcut.Enabled = true;
                cbMongoDB.Enabled = true;
                cbRemoteAcces.Enabled = true;
                cbComponentsReport.Enabled = true;
                cbSite.Enabled = true;
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

                    cbDLLs.Checked = true;
                } catch(Exception ex) {
                    MessageBox.Show("Ocorreu erro para copiar as DLLs do SAT: " + ex.Message);
                }

            }
        }

        #region Update CheckedBox in  Database when have changes
        private void cbDirectorySat_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbDirectorySat);

        }
        private void cbFirewall_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbFirewall);
        }

        private void cbIIS_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbIIS);

        }

        private void cbSite_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbSite);

        }

        private void cbUSB_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbUSB);

        }

        private void cbPCAndMonitor_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbPCAndMonitor);

        }

        private void cbFastBoot_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbFastBoot);

        }

        private void cbTemp_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbTemp);

        }

        private void cbHostname_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbHostname);

        }

        private void cbCeltaBSPDV_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbCeltaBSPDV);

        }

        private void cbMongoDB_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbMongoDB);

        }

        private void cbShortcut_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbShortcut);

        }

        private void cbRemoteAcces_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbRemoteAcces);

        }

        private void cbComponentsReport_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbComponentsReport);

        }

        private void cbRoboMongo_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbRoboMongo);

        }

        private void cbNeverNotifyUser_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbNeverNotifyUser);

        }

        private void cbBestPerformance_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbBestPerformance);

        }

        private void cbPCI_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbPCI);

        }

        private void cbInitBlocks_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbInitBlocks);

        }

        private void cbTaskManager_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbTaskManager);

        }

        private void cbUltraVNC_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbUltraVNC);

        }

        private void cbTeamViewer_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbTeamViewer);

        }

        private void cdDeviceManager_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cdDeviceManager);

        }

        private void cbDLLs_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbDLLs);

        }

        private void cbLogo_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbLogo);

        }

        private void cbIP_CheckedChanged(object sender, EventArgs e) {
            DatabaseLoadCheckeds.updateData(cbIP);

        }
        #endregion


    }
}