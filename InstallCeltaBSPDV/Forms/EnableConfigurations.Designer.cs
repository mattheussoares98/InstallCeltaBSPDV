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
        private void InitializeComponent() {
            this.buttonConfigurations = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxFirewall = new System.Windows.Forms.CheckBox();
            this.checkBoxSuspendUSB = new System.Windows.Forms.CheckBox();
            this.checkBoxSuspendMonitorAndPC = new System.Windows.Forms.CheckBox();
            this.checkBoxTemp = new System.Windows.Forms.CheckBox();
            this.checkBoxSetHostName = new System.Windows.Forms.CheckBox();
            this.checkBoxCopyCetaBSPDV = new System.Windows.Forms.CheckBox();
            this.checkBoxPdvLink = new System.Windows.Forms.CheckBox();
            this.checkBoxInstallMongo = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableRemoteAcces = new System.Windows.Forms.CheckBox();
            this.checkBoxInstallComponentsReport = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableIISComponents = new System.Windows.Forms.CheckBox();
            this.checkBoxCreateSharedSatPath = new System.Windows.Forms.CheckBox();
            this.checkBoxCreateSharedSatSite = new System.Windows.Forms.CheckBox();
            this.checkBoxNeverNotifyUser = new System.Windows.Forms.CheckBox();
            this.checkBoxAdjustVisualEffects = new System.Windows.Forms.CheckBox();
            this.checkBox17 = new System.Windows.Forms.CheckBox();
            this.checkBox16 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.checkBox19 = new System.Windows.Forms.CheckBox();
            this.checkBox20 = new System.Windows.Forms.CheckBox();
            this.checkBox21 = new System.Windows.Forms.CheckBox();
            this.checkBox22 = new System.Windows.Forms.CheckBox();
            this.richTextBoxResults = new System.Windows.Forms.RichTextBox();
            this.progressBarInstall = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonConfigurations
            // 
            this.buttonConfigurations.Location = new System.Drawing.Point(599, 497);
            this.buttonConfigurations.Name = "buttonConfigurations";
            this.buttonConfigurations.Size = new System.Drawing.Size(386, 71);
            this.buttonConfigurations.TabIndex = 0;
            this.buttonConfigurations.Text = "Iniciar configurações";
            this.buttonConfigurations.UseVisualStyleBackColor = true;
            this.buttonConfigurations.Click += new System.EventHandler(this.buttonConfigurations_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.checkBoxFirewall);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSuspendUSB);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSuspendMonitorAndPC);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxTemp);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSetHostName);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxCopyCetaBSPDV);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxPdvLink);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxInstallMongo);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxEnableRemoteAcces);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxInstallComponentsReport);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxEnableIISComponents);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxCreateSharedSatPath);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxCreateSharedSatSite);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxNeverNotifyUser);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxAdjustVisualEffects);
            this.flowLayoutPanel1.Controls.Add(this.checkBox17);
            this.flowLayoutPanel1.Controls.Add(this.checkBox16);
            this.flowLayoutPanel1.Controls.Add(this.checkBox12);
            this.flowLayoutPanel1.Controls.Add(this.checkBox19);
            this.flowLayoutPanel1.Controls.Add(this.checkBox20);
            this.flowLayoutPanel1.Controls.Add(this.checkBox21);
            this.flowLayoutPanel1.Controls.Add(this.checkBox22);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(20, 21);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(573, 550);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // checkBoxFirewall
            // 
            this.checkBoxFirewall.AutoSize = true;
            this.checkBoxFirewall.Location = new System.Drawing.Point(3, 3);
            this.checkBoxFirewall.Name = "checkBoxFirewall";
            this.checkBoxFirewall.Size = new System.Drawing.Size(238, 19);
            this.checkBoxFirewall.TabIndex = 1;
            this.checkBoxFirewall.Text = "Configurar o firewall (9092, 27017, PING)";
            this.checkBoxFirewall.UseVisualStyleBackColor = true;
            // 
            // checkBoxSuspendUSB
            // 
            this.checkBoxSuspendUSB.AutoSize = true;
            this.checkBoxSuspendUSB.Location = new System.Drawing.Point(3, 28);
            this.checkBoxSuspendUSB.Name = "checkBoxSuspendUSB";
            this.checkBoxSuspendUSB.Size = new System.Drawing.Size(205, 19);
            this.checkBoxSuspendUSB.TabIndex = 2;
            this.checkBoxSuspendUSB.Text = "Desabilitar suspensão seletiva USB";
            this.checkBoxSuspendUSB.UseVisualStyleBackColor = true;
            // 
            // checkBoxSuspendMonitorAndPC
            // 
            this.checkBoxSuspendMonitorAndPC.AutoSize = true;
            this.checkBoxSuspendMonitorAndPC.Location = new System.Drawing.Point(3, 53);
            this.checkBoxSuspendMonitorAndPC.Name = "checkBoxSuspendMonitorAndPC";
            this.checkBoxSuspendMonitorAndPC.Size = new System.Drawing.Size(344, 19);
            this.checkBoxSuspendMonitorAndPC.TabIndex = 3;
            this.checkBoxSuspendMonitorAndPC.Text = "Desabilitar tempo para suspensão do monitor e computador";
            this.checkBoxSuspendMonitorAndPC.UseVisualStyleBackColor = true;
            // 
            // checkBoxTemp
            // 
            this.checkBoxTemp.AutoSize = true;
            this.checkBoxTemp.Location = new System.Drawing.Point(3, 78);
            this.checkBoxTemp.Name = "checkBoxTemp";
            this.checkBoxTemp.Size = new System.Drawing.Size(139, 19);
            this.checkBoxTemp.TabIndex = 4;
            this.checkBoxTemp.Text = "Criar a pasta C:\\Temp";
            this.checkBoxTemp.UseVisualStyleBackColor = true;
            // 
            // checkBoxSetHostName
            // 
            this.checkBoxSetHostName.AutoSize = true;
            this.checkBoxSetHostName.Location = new System.Drawing.Point(3, 103);
            this.checkBoxSetHostName.Name = "checkBoxSetHostName";
            this.checkBoxSetHostName.Size = new System.Drawing.Size(238, 19);
            this.checkBoxSetHostName.TabIndex = 5;
            this.checkBoxSetHostName.Text = "Hostname conforme o número do caixa";
            this.checkBoxSetHostName.UseVisualStyleBackColor = true;
            // 
            // checkBoxCopyCetaBSPDV
            // 
            this.checkBoxCopyCetaBSPDV.AutoSize = true;
            this.checkBoxCopyCetaBSPDV.Location = new System.Drawing.Point(3, 128);
            this.checkBoxCopyCetaBSPDV.Name = "checkBoxCopyCetaBSPDV";
            this.checkBoxCopyCetaBSPDV.Size = new System.Drawing.Size(244, 19);
            this.checkBoxCopyCetaBSPDV.TabIndex = 6;
            this.checkBoxCopyCetaBSPDV.Text = "Copiar a pasta CeltaBSPDV para o disco C";
            this.checkBoxCopyCetaBSPDV.UseVisualStyleBackColor = true;
            // 
            // checkBoxPdvLink
            // 
            this.checkBoxPdvLink.AutoSize = true;
            this.checkBoxPdvLink.Location = new System.Drawing.Point(3, 153);
            this.checkBoxPdvLink.Name = "checkBoxPdvLink";
            this.checkBoxPdvLink.Size = new System.Drawing.Size(465, 19);
            this.checkBoxPdvLink.TabIndex = 7;
            this.checkBoxPdvLink.Text = "Adicionar atalho do PDV na pasta de inicialização do windows e na área de trabalh" +
    "o";
            this.checkBoxPdvLink.UseVisualStyleBackColor = true;
            // 
            // checkBoxInstallMongo
            // 
            this.checkBoxInstallMongo.AutoSize = true;
            this.checkBoxInstallMongo.Location = new System.Drawing.Point(3, 178);
            this.checkBoxInstallMongo.Name = "checkBoxInstallMongo";
            this.checkBoxInstallMongo.Size = new System.Drawing.Size(131, 19);
            this.checkBoxInstallMongo.TabIndex = 8;
            this.checkBoxInstallMongo.Text = "Instalar o MongoDB";
            this.checkBoxInstallMongo.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableRemoteAcces
            // 
            this.checkBoxEnableRemoteAcces.AutoSize = true;
            this.checkBoxEnableRemoteAcces.Location = new System.Drawing.Point(3, 203);
            this.checkBoxEnableRemoteAcces.Name = "checkBoxEnableRemoteAcces";
            this.checkBoxEnableRemoteAcces.Size = new System.Drawing.Size(254, 19);
            this.checkBoxEnableRemoteAcces.TabIndex = 9;
            this.checkBoxEnableRemoteAcces.Text = "Habilitar acesso remoto ao banco de dados";
            this.checkBoxEnableRemoteAcces.UseVisualStyleBackColor = true;
            // 
            // checkBoxInstallComponentsReport
            // 
            this.checkBoxInstallComponentsReport.AutoSize = true;
            this.checkBoxInstallComponentsReport.Location = new System.Drawing.Point(3, 228);
            this.checkBoxInstallComponentsReport.Name = "checkBoxInstallComponentsReport";
            this.checkBoxInstallComponentsReport.Size = new System.Drawing.Size(174, 19);
            this.checkBoxInstallComponentsReport.TabIndex = 10;
            this.checkBoxInstallComponentsReport.Text = "Instalar Components Report";
            this.checkBoxInstallComponentsReport.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableIISComponents
            // 
            this.checkBoxEnableIISComponents.AutoSize = true;
            this.checkBoxEnableIISComponents.Location = new System.Drawing.Point(3, 253);
            this.checkBoxEnableIISComponents.Name = "checkBoxEnableIISComponents";
            this.checkBoxEnableIISComponents.Size = new System.Drawing.Size(172, 19);
            this.checkBoxEnableIISComponents.TabIndex = 11;
            this.checkBoxEnableIISComponents.Text = "Instalar componentes do IIS";
            this.checkBoxEnableIISComponents.UseVisualStyleBackColor = true;
            // 
            // checkBoxCreateSharedSatPath
            // 
            this.checkBoxCreateSharedSatPath.AutoSize = true;
            this.checkBoxCreateSharedSatPath.Location = new System.Drawing.Point(3, 278);
            this.checkBoxCreateSharedSatPath.Name = "checkBoxCreateSharedSatPath";
            this.checkBoxCreateSharedSatPath.Size = new System.Drawing.Size(266, 19);
            this.checkBoxCreateSharedSatPath.TabIndex = 12;
            this.checkBoxCreateSharedSatPath.Text = "Criar o diretório de compartilhamento do SAT";
            this.checkBoxCreateSharedSatPath.UseVisualStyleBackColor = true;
            // 
            // checkBoxCreateSharedSatSite
            // 
            this.checkBoxCreateSharedSatSite.AutoSize = true;
            this.checkBoxCreateSharedSatSite.Location = new System.Drawing.Point(3, 303);
            this.checkBoxCreateSharedSatSite.Name = "checkBoxCreateSharedSatSite";
            this.checkBoxCreateSharedSatSite.Size = new System.Drawing.Size(239, 19);
            this.checkBoxCreateSharedSatSite.TabIndex = 13;
            this.checkBoxCreateSharedSatSite.Text = "Criar o site de compartilhamento do SAT";
            this.checkBoxCreateSharedSatSite.UseVisualStyleBackColor = true;
            // 
            // checkBoxNeverNotifyUser
            // 
            this.checkBoxNeverNotifyUser.AutoSize = true;
            this.checkBoxNeverNotifyUser.Location = new System.Drawing.Point(3, 328);
            this.checkBoxNeverNotifyUser.Name = "checkBoxNeverNotifyUser";
            this.checkBoxNeverNotifyUser.Size = new System.Drawing.Size(302, 19);
            this.checkBoxNeverNotifyUser.TabIndex = 14;
            this.checkBoxNeverNotifyUser.Text = "Configurar controle de usuários para nunca notificar";
            this.checkBoxNeverNotifyUser.UseVisualStyleBackColor = true;
            // 
            // checkBoxAdjustVisualEffects
            // 
            this.checkBoxAdjustVisualEffects.AutoSize = true;
            this.checkBoxAdjustVisualEffects.Location = new System.Drawing.Point(3, 353);
            this.checkBoxAdjustVisualEffects.Name = "checkBoxAdjustVisualEffects";
            this.checkBoxAdjustVisualEffects.Size = new System.Drawing.Size(379, 19);
            this.checkBoxAdjustVisualEffects.TabIndex = 15;
            this.checkBoxAdjustVisualEffects.Text = "Selecionar opções de desempenho para obter melhor desempenho";
            this.checkBoxAdjustVisualEffects.UseVisualStyleBackColor = true;
            // 
            // checkBox17
            // 
            this.checkBox17.AutoSize = true;
            this.checkBox17.Location = new System.Drawing.Point(3, 378);
            this.checkBox17.Name = "checkBox17";
            this.checkBox17.Size = new System.Drawing.Size(256, 19);
            this.checkBox17.TabIndex = 16;
            this.checkBox17.Text = "Desativar blocos dinâmicos do menu iniciar";
            this.checkBox17.UseVisualStyleBackColor = true;
            // 
            // checkBox16
            // 
            this.checkBox16.AutoSize = true;
            this.checkBox16.Location = new System.Drawing.Point(3, 403);
            this.checkBox16.Name = "checkBox16";
            this.checkBox16.Size = new System.Drawing.Size(477, 19);
            this.checkBox16.TabIndex = 17;
            this.checkBox16.Text = "Desativar a inicialização automática de APPs desnecessários no gerenciador de tar" +
    "efas";
            this.checkBox16.UseVisualStyleBackColor = true;
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Location = new System.Drawing.Point(3, 428);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(227, 19);
            this.checkBox12.TabIndex = 18;
            this.checkBox12.Text = "UltraVNC instalado com a senha \"123\"";
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // checkBox19
            // 
            this.checkBox19.AutoSize = true;
            this.checkBox19.Location = new System.Drawing.Point(3, 453);
            this.checkBox19.Name = "checkBox19";
            this.checkBox19.Size = new System.Drawing.Size(135, 19);
            this.checkBox19.TabIndex = 19;
            this.checkBox19.Text = "Adicionar o logotipo";
            this.checkBox19.UseVisualStyleBackColor = true;
            // 
            // checkBox20
            // 
            this.checkBox20.AutoSize = true;
            this.checkBox20.Location = new System.Drawing.Point(3, 478);
            this.checkBox20.Name = "checkBox20";
            this.checkBox20.Size = new System.Drawing.Size(140, 19);
            this.checkBox20.TabIndex = 20;
            this.checkBox20.Text = "Fixar o IP da máquina";
            this.checkBox20.UseVisualStyleBackColor = true;
            // 
            // checkBox21
            // 
            this.checkBox21.AutoSize = true;
            this.checkBox21.Location = new System.Drawing.Point(3, 503);
            this.checkBox21.Name = "checkBox21";
            this.checkBox21.Size = new System.Drawing.Size(281, 19);
            this.checkBox21.TabIndex = 21;
            this.checkBox21.Text = "Instalar o team viewer e fixar a senha \"CeltaPDV\"";
            this.checkBox21.UseVisualStyleBackColor = true;
            // 
            // checkBox22
            // 
            this.checkBox22.AutoSize = true;
            this.checkBox22.Location = new System.Drawing.Point(3, 528);
            this.checkBox22.Name = "checkBox22";
            this.checkBox22.Size = new System.Drawing.Size(566, 19);
            this.checkBox22.TabIndex = 22;
            this.checkBox22.Text = "Gerenciador de dispositivos > não permitir interface humana e controlador USB des" +
    "abilitar dispositivos";
            this.checkBox22.UseVisualStyleBackColor = true;
            // 
            // richTextBoxResults
            // 
            this.richTextBoxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxResults.EnableAutoDragDrop = true;
            this.richTextBoxResults.Location = new System.Drawing.Point(599, 21);
            this.richTextBoxResults.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBoxResults.Name = "richTextBoxResults";
            this.richTextBoxResults.ReadOnly = true;
            this.richTextBoxResults.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxResults.Size = new System.Drawing.Size(386, 397);
            this.richTextBoxResults.TabIndex = 23;
            this.richTextBoxResults.Text = "";
            this.richTextBoxResults.TextChanged += new System.EventHandler(this.richTextBoxResults_TextChanged);
            // 
            // progressBarInstall
            // 
            this.progressBarInstall.Location = new System.Drawing.Point(599, 436);
            this.progressBarInstall.Name = "progressBarInstall";
            this.progressBarInstall.Size = new System.Drawing.Size(386, 42);
            this.progressBarInstall.TabIndex = 3;
            this.progressBarInstall.Visible = false;
            // 
            // EnableConfigurations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(997, 580);
            this.Controls.Add(this.progressBarInstall);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.richTextBoxResults);
            this.Controls.Add(this.buttonConfigurations);
            this.Name = "EnableConfigurations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instalador";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button buttonConfigurations;
        private CheckBox checkBox16;
        private CheckBox checkBox17;
        private CheckBox checkBox12;
        private CheckBox checkBox19;
        private CheckBox checkBox20;
        private CheckBox checkBox21;
        private CheckBox checkBox22;
        private ProgressBar progressBarInstall;
        internal FlowLayoutPanel flowLayoutPanel1;
        public RichTextBox richTextBoxResults;
        public CheckBox checkBoxCopyCetaBSPDV;
        public CheckBox checkBoxPdvLink;
        public CheckBox checkBoxEnableRemoteAcces;
        public CheckBox checkBoxInstallMongo;
        public CheckBox checkBoxFirewall;
        public CheckBox checkBoxSuspendUSB;
        public CheckBox checkBoxSuspendMonitorAndPC;
        public CheckBox checkBoxTemp;
        public CheckBox checkBoxCreateSharedSatPath;
        public CheckBox checkBoxEnableIISComponents;
        public CheckBox checkBoxInstallComponentsReport;
        public CheckBox checkBoxCreateSharedSatSite;
        public CheckBox checkBoxAdjustVisualEffects;
        public CheckBox checkBoxNeverNotifyUser;
        public CheckBox checkBoxSetHostName;
    }
}