namespace InstallCeltaBSPDV {
    partial class EnableConfigurations {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnableConfigurations));
            buttonConfigurations = new Button();
            flowLayoutPanelConfigurations = new FlowLayoutPanel();
            cbFirewall = new CheckBox();
            cbUSB = new CheckBox();
            cbPCAndMonitor = new CheckBox();
            cbFastBoot = new CheckBox();
            cbTemp = new CheckBox();
            cbHostname = new CheckBox();
            cbCeltaBSPDV = new CheckBox();
            cbMongoDB = new CheckBox();
            cbShortcut = new CheckBox();
            cbRemoteAcces = new CheckBox();
            cbComponentsReport = new CheckBox();
            cbTeamViewer = new CheckBox();
            cbRoboMongo = new CheckBox();
            cbRustDesk = new CheckBox();
            cbNeverNotifyUser = new CheckBox();
            cbBestPerformance = new CheckBox();
            cbPCI = new CheckBox();
            cbInitBlocks = new CheckBox();
            cbTaskManager = new CheckBox();
            cbUltraVNC = new CheckBox();
            cdDeviceManager = new CheckBox();
            cbDLLs = new CheckBox();
            cbLogo = new CheckBox();
            cbIP = new CheckBox();
            richTextBoxResults = new RichTextBox();
            progressBarInstall = new ProgressBar();
            buttonDownloadFiles = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            buttonDistributeDLLs = new Button();
            flowLayoutPanelConfigurations.SuspendLayout();
            SuspendLayout();
            // 
            // buttonConfigurations
            // 
            buttonConfigurations.Location = new Point(590, 365);
            buttonConfigurations.Name = "buttonConfigurations";
            buttonConfigurations.Size = new Size(392, 41);
            buttonConfigurations.TabIndex = 0;
            buttonConfigurations.Text = "Iniciar configurações";
            buttonConfigurations.UseVisualStyleBackColor = true;
            buttonConfigurations.Click += buttonConfigurations_Click;
            // 
            // flowLayoutPanelConfigurations
            // 
            flowLayoutPanelConfigurations.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanelConfigurations.Controls.Add(cbFirewall);
            flowLayoutPanelConfigurations.Controls.Add(cbUSB);
            flowLayoutPanelConfigurations.Controls.Add(cbPCAndMonitor);
            flowLayoutPanelConfigurations.Controls.Add(cbFastBoot);
            flowLayoutPanelConfigurations.Controls.Add(cbTemp);
            flowLayoutPanelConfigurations.Controls.Add(cbHostname);
            flowLayoutPanelConfigurations.Controls.Add(cbCeltaBSPDV);
            flowLayoutPanelConfigurations.Controls.Add(cbMongoDB);
            flowLayoutPanelConfigurations.Controls.Add(cbShortcut);
            flowLayoutPanelConfigurations.Controls.Add(cbRemoteAcces);
            flowLayoutPanelConfigurations.Controls.Add(cbComponentsReport);
            flowLayoutPanelConfigurations.Controls.Add(cbTeamViewer);
            flowLayoutPanelConfigurations.Controls.Add(cbRoboMongo);
            flowLayoutPanelConfigurations.Controls.Add(cbRustDesk);
            flowLayoutPanelConfigurations.Controls.Add(cbNeverNotifyUser);
            flowLayoutPanelConfigurations.Controls.Add(cbBestPerformance);
            flowLayoutPanelConfigurations.Controls.Add(cbPCI);
            flowLayoutPanelConfigurations.Controls.Add(cbInitBlocks);
            flowLayoutPanelConfigurations.Controls.Add(cbTaskManager);
            flowLayoutPanelConfigurations.Controls.Add(cbUltraVNC);
            flowLayoutPanelConfigurations.Controls.Add(cdDeviceManager);
            flowLayoutPanelConfigurations.Controls.Add(cbDLLs);
            flowLayoutPanelConfigurations.Controls.Add(cbLogo);
            flowLayoutPanelConfigurations.Controls.Add(cbIP);
            flowLayoutPanelConfigurations.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelConfigurations.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            flowLayoutPanelConfigurations.Location = new Point(11, 3);
            flowLayoutPanelConfigurations.Margin = new Padding(0);
            flowLayoutPanelConfigurations.Name = "flowLayoutPanelConfigurations";
            flowLayoutPanelConfigurations.Size = new Size(576, 562);
            flowLayoutPanelConfigurations.TabIndex = 2;
            // 
            // cbFirewall
            // 
            cbFirewall.AutoSize = true;
            cbFirewall.Location = new Point(3, 3);
            cbFirewall.Name = "cbFirewall";
            cbFirewall.Size = new Size(222, 17);
            cbFirewall.TabIndex = 28;
            cbFirewall.Text = "Configurar firewall (9092, 27017, ping)";
            cbFirewall.UseVisualStyleBackColor = true;
            cbFirewall.CheckedChanged += cbFirewall_CheckedChanged;
            // 
            // cbUSB
            // 
            cbUSB.AutoSize = true;
            cbUSB.Location = new Point(3, 26);
            cbUSB.Name = "cbUSB";
            cbUSB.Size = new Size(203, 17);
            cbUSB.TabIndex = 5;
            cbUSB.Text = "Desabilitar suspensão seletiva USB";
            cbUSB.UseVisualStyleBackColor = true;
            cbUSB.CheckedChanged += cbUSB_CheckedChanged;
            // 
            // cbPCAndMonitor
            // 
            cbPCAndMonitor.AutoSize = true;
            cbPCAndMonitor.Location = new Point(3, 49);
            cbPCAndMonitor.Name = "cbPCAndMonitor";
            cbPCAndMonitor.Size = new Size(337, 17);
            cbPCAndMonitor.TabIndex = 6;
            cbPCAndMonitor.Text = "Desabilitar tempo para suspensão do monitor e computador";
            cbPCAndMonitor.UseVisualStyleBackColor = true;
            cbPCAndMonitor.CheckedChanged += cbPCAndMonitor_CheckedChanged;
            // 
            // cbFastBoot
            // 
            cbFastBoot.AutoSize = true;
            cbFastBoot.Location = new Point(3, 72);
            cbFastBoot.Name = "cbFastBoot";
            cbFastBoot.Size = new Size(225, 17);
            cbFastBoot.TabIndex = 24;
            cbFastBoot.Text = "Ligar inicialiazação rápida do windows";
            cbFastBoot.UseVisualStyleBackColor = true;
            cbFastBoot.CheckedChanged += cbFastBoot_CheckedChanged;
            // 
            // cbTemp
            // 
            cbTemp.AutoSize = true;
            cbTemp.Location = new Point(3, 95);
            cbTemp.Name = "cbTemp";
            cbTemp.Size = new Size(134, 17);
            cbTemp.TabIndex = 7;
            cbTemp.Text = "Criar a pasta C:\\Temp";
            cbTemp.UseVisualStyleBackColor = true;
            cbTemp.CheckedChanged += cbTemp_CheckedChanged;
            // 
            // cbHostname
            // 
            cbHostname.AutoSize = true;
            cbHostname.Location = new Point(3, 118);
            cbHostname.Name = "cbHostname";
            cbHostname.Size = new Size(228, 17);
            cbHostname.TabIndex = 8;
            cbHostname.Text = "Hostname conforme o número do caixa";
            cbHostname.UseVisualStyleBackColor = true;
            cbHostname.CheckedChanged += cbHostname_CheckedChanged;
            // 
            // cbCeltaBSPDV
            // 
            cbCeltaBSPDV.AutoSize = true;
            cbCeltaBSPDV.Location = new Point(3, 141);
            cbCeltaBSPDV.Name = "cbCeltaBSPDV";
            cbCeltaBSPDV.Size = new Size(238, 17);
            cbCeltaBSPDV.TabIndex = 9;
            cbCeltaBSPDV.Text = "Copiar a pasta CeltaBSPDV para o disco C";
            cbCeltaBSPDV.UseVisualStyleBackColor = true;
            cbCeltaBSPDV.CheckedChanged += cbCeltaBSPDV_CheckedChanged;
            // 
            // cbMongoDB
            // 
            cbMongoDB.AutoSize = true;
            cbMongoDB.Location = new Point(3, 164);
            cbMongoDB.Name = "cbMongoDB";
            cbMongoDB.Size = new Size(129, 17);
            cbMongoDB.TabIndex = 10;
            cbMongoDB.Text = "Instalar o MongoDB";
            cbMongoDB.UseVisualStyleBackColor = true;
            cbMongoDB.CheckedChanged += cbMongoDB_CheckedChanged;
            // 
            // cbShortcut
            // 
            cbShortcut.AutoSize = true;
            cbShortcut.Location = new Point(3, 187);
            cbShortcut.Name = "cbShortcut";
            cbShortcut.Size = new Size(460, 17);
            cbShortcut.TabIndex = 11;
            cbShortcut.Text = "Adicionar atalho do PDV na pasta de inicialização do windows e na área de trabalho";
            cbShortcut.UseVisualStyleBackColor = true;
            cbShortcut.CheckedChanged += cbShortcut_CheckedChanged;
            // 
            // cbRemoteAcces
            // 
            cbRemoteAcces.AutoSize = true;
            cbRemoteAcces.Location = new Point(3, 210);
            cbRemoteAcces.Name = "cbRemoteAcces";
            cbRemoteAcces.Size = new Size(249, 17);
            cbRemoteAcces.TabIndex = 12;
            cbRemoteAcces.Text = "Habilitar acesso remoto ao banco de dados";
            cbRemoteAcces.UseVisualStyleBackColor = true;
            cbRemoteAcces.CheckedChanged += cbRemoteAcces_CheckedChanged;
            // 
            // cbComponentsReport
            // 
            cbComponentsReport.AutoSize = true;
            cbComponentsReport.Location = new Point(3, 233);
            cbComponentsReport.Name = "cbComponentsReport";
            cbComponentsReport.Size = new Size(171, 17);
            cbComponentsReport.TabIndex = 13;
            cbComponentsReport.Text = "Instalar Components Report";
            cbComponentsReport.UseVisualStyleBackColor = true;
            cbComponentsReport.CheckedChanged += cbComponentsReport_CheckedChanged;
            // 
            // cbTeamViewer
            // 
            cbTeamViewer.AutoSize = true;
            cbTeamViewer.Location = new Point(3, 256);
            cbTeamViewer.Name = "cbTeamViewer";
            cbTeamViewer.Size = new Size(273, 17);
            cbTeamViewer.TabIndex = 21;
            cbTeamViewer.Text = "Instalar o team viewer e fixar a senha \"CeltaPDV\"";
            cbTeamViewer.UseVisualStyleBackColor = true;
            cbTeamViewer.CheckedChanged += cbTeamViewer_CheckedChanged;
            // 
            // cbRoboMongo
            // 
            cbRoboMongo.AutoSize = true;
            cbRoboMongo.Location = new Point(3, 279);
            cbRoboMongo.Name = "cbRoboMongo";
            cbRoboMongo.Size = new Size(143, 17);
            cbRoboMongo.TabIndex = 25;
            cbRoboMongo.Text = "Instalar o RoboMongo";
            cbRoboMongo.UseVisualStyleBackColor = true;
            cbRoboMongo.CheckedChanged += cbRoboMongo_CheckedChanged;
            // 
            // cbRustDesk
            // 
            cbRustDesk.AutoSize = true;
            cbRustDesk.Location = new Point(3, 302);
            cbRustDesk.Name = "cbRustDesk";
            cbRustDesk.Size = new Size(115, 17);
            cbRustDesk.TabIndex = 29;
            cbRustDesk.Text = "Instalar RustDesk";
            cbRustDesk.UseVisualStyleBackColor = true;
            // 
            // cbNeverNotifyUser
            // 
            cbNeverNotifyUser.AutoSize = true;
            cbNeverNotifyUser.Location = new Point(3, 325);
            cbNeverNotifyUser.Name = "cbNeverNotifyUser";
            cbNeverNotifyUser.Size = new Size(298, 17);
            cbNeverNotifyUser.TabIndex = 14;
            cbNeverNotifyUser.Text = "Configurar controle de usuários para nunca notificar";
            cbNeverNotifyUser.UseVisualStyleBackColor = true;
            cbNeverNotifyUser.CheckedChanged += cbNeverNotifyUser_CheckedChanged;
            // 
            // cbBestPerformance
            // 
            cbBestPerformance.AutoSize = true;
            cbBestPerformance.Location = new Point(3, 348);
            cbBestPerformance.Name = "cbBestPerformance";
            cbBestPerformance.Size = new Size(371, 17);
            cbBestPerformance.TabIndex = 15;
            cbBestPerformance.Text = "Selecionar opções de desempenho para obter melhor desempenho";
            cbBestPerformance.UseVisualStyleBackColor = true;
            cbBestPerformance.CheckedChanged += cbBestPerformance_CheckedChanged;
            // 
            // cbPCI
            // 
            cbPCI.AutoSize = true;
            cbPCI.Location = new Point(3, 371);
            cbPCI.Name = "cbPCI";
            cbPCI.Size = new Size(199, 17);
            cbPCI.TabIndex = 23;
            cbPCI.Text = "Desabilitar suspensão seletiva PCI";
            cbPCI.UseVisualStyleBackColor = true;
            cbPCI.CheckedChanged += cbPCI_CheckedChanged;
            // 
            // cbInitBlocks
            // 
            cbInitBlocks.AutoSize = true;
            cbInitBlocks.Location = new Point(3, 394);
            cbInitBlocks.Name = "cbInitBlocks";
            cbInitBlocks.Size = new Size(248, 17);
            cbInitBlocks.TabIndex = 16;
            cbInitBlocks.Text = "Desativar blocos dinâmicos do menu iniciar";
            cbInitBlocks.UseVisualStyleBackColor = true;
            cbInitBlocks.CheckedChanged += cbInitBlocks_CheckedChanged;
            // 
            // cbTaskManager
            // 
            cbTaskManager.AutoSize = true;
            cbTaskManager.Location = new Point(3, 417);
            cbTaskManager.Name = "cbTaskManager";
            cbTaskManager.Size = new Size(466, 17);
            cbTaskManager.TabIndex = 17;
            cbTaskManager.Text = "Desativar a inicialização automática de APPs desnecessários no gerenciador de tarefas";
            cbTaskManager.UseVisualStyleBackColor = true;
            cbTaskManager.CheckedChanged += cbTaskManager_CheckedChanged;
            // 
            // cbUltraVNC
            // 
            cbUltraVNC.AutoSize = true;
            cbUltraVNC.Location = new Point(3, 440);
            cbUltraVNC.Name = "cbUltraVNC";
            cbUltraVNC.Size = new Size(220, 17);
            cbUltraVNC.TabIndex = 18;
            cbUltraVNC.Text = "UltraVNC instalado com a senha \"123\"";
            cbUltraVNC.UseVisualStyleBackColor = true;
            cbUltraVNC.CheckedChanged += cbUltraVNC_CheckedChanged;
            // 
            // cdDeviceManager
            // 
            cdDeviceManager.AutoSize = true;
            cdDeviceManager.Location = new Point(3, 463);
            cdDeviceManager.Name = "cdDeviceManager";
            cdDeviceManager.Size = new Size(509, 17);
            cdDeviceManager.TabIndex = 27;
            cdDeviceManager.Text = "Gerenciador de dispositivos - não permitir desativar dispositivos USB, interface humana e rede";
            cdDeviceManager.UseVisualStyleBackColor = true;
            cdDeviceManager.CheckedChanged += cdDeviceManager_CheckedChanged;
            // 
            // cbDLLs
            // 
            cbDLLs.AutoSize = true;
            cbDLLs.Location = new Point(3, 486);
            cbDLLs.Name = "cbDLLs";
            cbDLLs.Size = new Size(138, 17);
            cbDLLs.TabIndex = 26;
            cbDLLs.Text = "Copiar as DLLs do SAT";
            cbDLLs.UseVisualStyleBackColor = true;
            cbDLLs.CheckedChanged += cbDLLs_CheckedChanged;
            // 
            // cbLogo
            // 
            cbLogo.AutoSize = true;
            cbLogo.Location = new Point(3, 509);
            cbLogo.Name = "cbLogo";
            cbLogo.Size = new Size(133, 17);
            cbLogo.TabIndex = 19;
            cbLogo.Text = "Adicionar o logotipo";
            cbLogo.UseVisualStyleBackColor = true;
            cbLogo.CheckedChanged += cbLogo_CheckedChanged;
            // 
            // cbIP
            // 
            cbIP.AutoSize = true;
            cbIP.Location = new Point(3, 532);
            cbIP.Name = "cbIP";
            cbIP.Size = new Size(136, 17);
            cbIP.TabIndex = 20;
            cbIP.Text = "Fixar o IP da máquina";
            cbIP.UseVisualStyleBackColor = true;
            cbIP.CheckedChanged += cbIP_CheckedChanged;
            // 
            // richTextBoxResults
            // 
            richTextBoxResults.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            richTextBoxResults.EnableAutoDragDrop = true;
            richTextBoxResults.Location = new Point(591, 3);
            richTextBoxResults.Margin = new Padding(3, 2, 3, 2);
            richTextBoxResults.Name = "richTextBoxResults";
            richTextBoxResults.ReadOnly = true;
            richTextBoxResults.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBoxResults.Size = new Size(392, 296);
            richTextBoxResults.TabIndex = 23;
            richTextBoxResults.Text = "";
            richTextBoxResults.TextChanged += richTextBoxResults_TextChanged;
            // 
            // progressBarInstall
            // 
            progressBarInstall.Location = new Point(591, 316);
            progressBarInstall.Name = "progressBarInstall";
            progressBarInstall.Size = new Size(391, 42);
            progressBarInstall.TabIndex = 3;
            progressBarInstall.Visible = false;
            // 
            // buttonDownloadFiles
            // 
            buttonDownloadFiles.Location = new Point(590, 422);
            buttonDownloadFiles.Name = "buttonDownloadFiles";
            buttonDownloadFiles.Size = new Size(392, 41);
            buttonDownloadFiles.TabIndex = 24;
            buttonDownloadFiles.Text = "Baixar arquivos";
            buttonDownloadFiles.UseVisualStyleBackColor = true;
            buttonDownloadFiles.Click += button1_Click;
            // 
            // buttonDistributeDLLs
            // 
            buttonDistributeDLLs.Location = new Point(591, 478);
            buttonDistributeDLLs.Name = "buttonDistributeDLLs";
            buttonDistributeDLLs.Size = new Size(392, 41);
            buttonDistributeDLLs.TabIndex = 26;
            buttonDistributeDLLs.Text = "Distribuir DLLs do SAT nas pastas necessárias";
            buttonDistributeDLLs.UseVisualStyleBackColor = true;
            buttonDistributeDLLs.Click += buttonDistributeDLLs_Click;
            // 
            // EnableConfigurations
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(987, 574);
            Controls.Add(buttonDistributeDLLs);
            Controls.Add(buttonDownloadFiles);
            Controls.Add(progressBarInstall);
            Controls.Add(flowLayoutPanelConfigurations);
            Controls.Add(richTextBoxResults);
            Controls.Add(buttonConfigurations);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(1007, 673);
            Name = "EnableConfigurations";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Instalador";
            flowLayoutPanelConfigurations.ResumeLayout(false);
            flowLayoutPanelConfigurations.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private CheckBox cbTaskManager;
        private CheckBox cbInitBlocks;
        private CheckBox cbUltraVNC;
        private CheckBox cbLogo;
        private CheckBox cbIP;
        internal FlowLayoutPanel flowLayoutPanelConfigurations;
        public RichTextBox richTextBoxResults;
        public CheckBox cbCeltaBSPDV;
        public CheckBox cbShortcut;
        public CheckBox cbRemoteAcces;
        public CheckBox cbMongoDB;
        public CheckBox cbUSB;
        public CheckBox cbPCAndMonitor;
        public CheckBox cbTemp;
        public CheckBox cbComponentsReport;
        public CheckBox cbBestPerformance;
        public CheckBox cbNeverNotifyUser;
        public CheckBox cbHostname;
        private Button buttonDownloadFiles;
        public CheckBox cbFastBoot;
        public CheckBox cbPCI;
        public CheckBox cbRoboMongo;
        public ProgressBar progressBarInstall;
        public Button buttonConfigurations;
        private FolderBrowserDialog folderBrowserDialog1;
        private CheckBox cbDLLs;
        private CheckBox cdDeviceManager;
        private Button buttonDistributeDLLs;
        public CheckBox cbFirewall;
        public CheckBox cbTeamViewer;
        public CheckBox cbRustDesk;
    }
}