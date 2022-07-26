
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
            flowLayoutPanel1.Enabled = false;
            ControlBox = false;
            #endregion

            await Windows.configureWindows(this);

            await BsPdv.configureBsPdv(this);

            await SharedSat.createSharedSat(this);

            #region enable components
            buttonConfigurations.Text = "Efetuar configurações";
            buttonConfigurations.Enabled = true;
            progressBarInstall.Style = ProgressBarStyle.Continuous;
            progressBarInstall.MarqueeAnimationSpeed = 0;
            progressBarInstall.Visible = false;
            flowLayoutPanel1.Enabled = true;
            ControlBox = true;
            #endregion
        }
    }
}