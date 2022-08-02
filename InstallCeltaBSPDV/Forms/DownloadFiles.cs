using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstallCeltaBSPDV.Configurations;

namespace InstallCeltaBSPDV.Forms {
    public partial class DownloadFiles: Form {
        private EnableConfigurations enableConfigurations;
        public DownloadFiles(EnableConfigurations formEnableConfigurations) {
            InitializeComponent();
            enableConfigurations = formEnableConfigurations;
        }

        private void DownloadFiles_FormClosing(object sender, FormClosingEventArgs e) {
            //enableConfigurations.Show();
        }

        private async void buttonDownloadFiles_Click(object sender, EventArgs e) {
            Download.downloadFileTaskAsync("Ultra VNC.zip", enableConfigurations, "https://drive.google.com/u/1/uc?id=1H-0CW3sUG23AI6DIpiNWzVf-C_xBZng7&export=download");

            Download.downloadFileTaskAsync("PPC 930.zip", enableConfigurations, "https://drive.google.com/u/1/uc?id=1bcWEqYNj40z3shcCfoJT4Nrs_SXhyASe&export=download");

        }
    }
}
