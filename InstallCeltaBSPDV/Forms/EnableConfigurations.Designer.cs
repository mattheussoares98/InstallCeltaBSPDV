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
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBoxCopyCetaBSPDV = new System.Windows.Forms.CheckBox();
            this.checkBoxPdvLink = new System.Windows.Forms.CheckBox();
            this.checkBoxInstallMongo = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableRemoteAcces = new System.Windows.Forms.CheckBox();
            this.checkBoxSharedPath = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox14 = new System.Windows.Forms.CheckBox();
            this.checkBox17 = new System.Windows.Forms.CheckBox();
            this.checkBox16 = new System.Windows.Forms.CheckBox();
            this.checkBox18 = new System.Windows.Forms.CheckBox();
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
            this.flowLayoutPanel1.Controls.Add(this.checkBox5);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxCopyCetaBSPDV);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxPdvLink);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxInstallMongo);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxEnableRemoteAcces);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSharedPath);
            this.flowLayoutPanel1.Controls.Add(this.checkBox3);
            this.flowLayoutPanel1.Controls.Add(this.checkBox8);
            this.flowLayoutPanel1.Controls.Add(this.checkBox9);
            this.flowLayoutPanel1.Controls.Add(this.checkBox14);
            this.flowLayoutPanel1.Controls.Add(this.checkBox17);
            this.flowLayoutPanel1.Controls.Add(this.checkBox16);
            this.flowLayoutPanel1.Controls.Add(this.checkBox18);
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
            this.checkBoxFirewall.TabIndex = 25;
            this.checkBoxFirewall.Text = "Configurar o firewall (9092, 27017, PING)";
            this.checkBoxFirewall.UseVisualStyleBackColor = true;
            // 
            // checkBoxSuspendUSB
            // 
            this.checkBoxSuspendUSB.AutoSize = true;
            this.checkBoxSuspendUSB.Location = new System.Drawing.Point(3, 28);
            this.checkBoxSuspendUSB.Name = "checkBoxSuspendUSB";
            this.checkBoxSuspendUSB.Size = new System.Drawing.Size(205, 19);
            this.checkBoxSuspendUSB.TabIndex = 18;
            this.checkBoxSuspendUSB.Text = "Desabilitar suspensão seletiva USB";
            this.checkBoxSuspendUSB.UseVisualStyleBackColor = true;
            // 
            // checkBoxSuspendMonitorAndPC
            // 
            this.checkBoxSuspendMonitorAndPC.AutoSize = true;
            this.checkBoxSuspendMonitorAndPC.Location = new System.Drawing.Point(3, 53);
            this.checkBoxSuspendMonitorAndPC.Name = "checkBoxSuspendMonitorAndPC";
            this.checkBoxSuspendMonitorAndPC.Size = new System.Drawing.Size(344, 19);
            this.checkBoxSuspendMonitorAndPC.TabIndex = 26;
            this.checkBoxSuspendMonitorAndPC.Text = "Desabilitar tempo para suspensão do monitor e computador";
            this.checkBoxSuspendMonitorAndPC.UseVisualStyleBackColor = true;
            // 
            // checkBoxTemp
            // 
            this.checkBoxTemp.AutoSize = true;
            this.checkBoxTemp.Location = new System.Drawing.Point(3, 78);
            this.checkBoxTemp.Name = "checkBoxTemp";
            this.checkBoxTemp.Size = new System.Drawing.Size(139, 19);
            this.checkBoxTemp.TabIndex = 22;
            this.checkBoxTemp.Text = "Criar a pasta C:\\Temp";
            this.checkBoxTemp.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(3, 103);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(238, 19);
            this.checkBox5.TabIndex = 23;
            this.checkBox5.Text = "Hostname conforme o número do caixa";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBoxCopyCetaBSPDV
            // 
            this.checkBoxCopyCetaBSPDV.AutoSize = true;
            this.checkBoxCopyCetaBSPDV.Location = new System.Drawing.Point(3, 128);
            this.checkBoxCopyCetaBSPDV.Name = "checkBoxCopyCetaBSPDV";
            this.checkBoxCopyCetaBSPDV.Size = new System.Drawing.Size(244, 19);
            this.checkBoxCopyCetaBSPDV.TabIndex = 19;
            this.checkBoxCopyCetaBSPDV.Text = "Copiar a pasta CeltaBSPDV para o disco C";
            this.checkBoxCopyCetaBSPDV.UseVisualStyleBackColor = true;
            // 
            // checkBoxPdvLink
            // 
            this.checkBoxPdvLink.AutoSize = true;
            this.checkBoxPdvLink.Location = new System.Drawing.Point(3, 153);
            this.checkBoxPdvLink.Name = "checkBoxPdvLink";
            this.checkBoxPdvLink.Size = new System.Drawing.Size(465, 19);
            this.checkBoxPdvLink.TabIndex = 28;
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
            this.checkBoxInstallMongo.TabIndex = 32;
            this.checkBoxInstallMongo.Text = "Instalar o MongoDB";
            this.checkBoxInstallMongo.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableRemoteAcces
            // 
            this.checkBoxEnableRemoteAcces.AutoSize = true;
            this.checkBoxEnableRemoteAcces.Location = new System.Drawing.Point(3, 203);
            this.checkBoxEnableRemoteAcces.Name = "checkBoxEnableRemoteAcces";
            this.checkBoxEnableRemoteAcces.Size = new System.Drawing.Size(254, 19);
            this.checkBoxEnableRemoteAcces.TabIndex = 20;
            this.checkBoxEnableRemoteAcces.Text = "Habilitar acesso remoto ao banco de dados";
            this.checkBoxEnableRemoteAcces.UseVisualStyleBackColor = true;
            // 
            // checkBoxSharedPath
            // 
            this.checkBoxSharedPath.AutoSize = true;
            this.checkBoxSharedPath.Location = new System.Drawing.Point(3, 228);
            this.checkBoxSharedPath.Name = "checkBoxSharedPath";
            this.checkBoxSharedPath.Size = new System.Drawing.Size(266, 19);
            this.checkBoxSharedPath.TabIndex = 30;
            this.checkBoxSharedPath.Text = "Criar o diretório de compartilhamento do SAT";
            this.checkBoxSharedPath.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(3, 253);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(302, 19);
            this.checkBox3.TabIndex = 21;
            this.checkBox3.Text = "Configurar controle de usuários para nunca notificar";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(3, 278);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(379, 19);
            this.checkBox8.TabIndex = 24;
            this.checkBox8.Text = "Selecionar opções de desempenho para obter melhor desempenho";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point(3, 303);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(172, 19);
            this.checkBox9.TabIndex = 27;
            this.checkBox9.Text = "Instalar componentes do IIS";
            this.checkBox9.UseVisualStyleBackColor = true;
            // 
            // checkBox14
            // 
            this.checkBox14.AutoSize = true;
            this.checkBox14.Location = new System.Drawing.Point(3, 328);
            this.checkBox14.Name = "checkBox14";
            this.checkBox14.Size = new System.Drawing.Size(239, 19);
            this.checkBox14.TabIndex = 31;
            this.checkBox14.Text = "Criar o site de compartilhamento do SAT";
            this.checkBox14.UseVisualStyleBackColor = true;
            // 
            // checkBox17
            // 
            this.checkBox17.AutoSize = true;
            this.checkBox17.Location = new System.Drawing.Point(3, 353);
            this.checkBox17.Name = "checkBox17";
            this.checkBox17.Size = new System.Drawing.Size(256, 19);
            this.checkBox17.TabIndex = 34;
            this.checkBox17.Text = "Desativar blocos dinâmicos do menu iniciar";
            this.checkBox17.UseVisualStyleBackColor = true;
            // 
            // checkBox16
            // 
            this.checkBox16.AutoSize = true;
            this.checkBox16.Location = new System.Drawing.Point(3, 378);
            this.checkBox16.Name = "checkBox16";
            this.checkBox16.Size = new System.Drawing.Size(477, 19);
            this.checkBox16.TabIndex = 35;
            this.checkBox16.Text = "Desativar a inicialização automática de APPs desnecessários no gerenciador de tar" +
    "efas";
            this.checkBox16.UseVisualStyleBackColor = true;
            // 
            // checkBox18
            // 
            this.checkBox18.AutoSize = true;
            this.checkBox18.Location = new System.Drawing.Point(3, 403);
            this.checkBox18.Name = "checkBox18";
            this.checkBox18.Size = new System.Drawing.Size(174, 19);
            this.checkBox18.TabIndex = 33;
            this.checkBox18.Text = "Instalar Components Report";
            this.checkBox18.UseVisualStyleBackColor = true;
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Location = new System.Drawing.Point(3, 428);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(227, 19);
            this.checkBox12.TabIndex = 29;
            this.checkBox12.Text = "UltraVNC instalado com a senha \"123\"";
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // checkBox19
            // 
            this.checkBox19.AutoSize = true;
            this.checkBox19.Location = new System.Drawing.Point(3, 453);
            this.checkBox19.Name = "checkBox19";
            this.checkBox19.Size = new System.Drawing.Size(135, 19);
            this.checkBox19.TabIndex = 36;
            this.checkBox19.Text = "Adicionar o logotipo";
            this.checkBox19.UseVisualStyleBackColor = true;
            // 
            // checkBox20
            // 
            this.checkBox20.AutoSize = true;
            this.checkBox20.Location = new System.Drawing.Point(3, 478);
            this.checkBox20.Name = "checkBox20";
            this.checkBox20.Size = new System.Drawing.Size(140, 19);
            this.checkBox20.TabIndex = 37;
            this.checkBox20.Text = "Fixar o IP da máquina";
            this.checkBox20.UseVisualStyleBackColor = true;
            // 
            // checkBox21
            // 
            this.checkBox21.AutoSize = true;
            this.checkBox21.Location = new System.Drawing.Point(3, 503);
            this.checkBox21.Name = "checkBox21";
            this.checkBox21.Size = new System.Drawing.Size(281, 19);
            this.checkBox21.TabIndex = 38;
            this.checkBox21.Text = "Instalar o team viewer e fixar a senha \"CeltaPDV\"";
            this.checkBox21.UseVisualStyleBackColor = true;
            // 
            // checkBox22
            // 
            this.checkBox22.AutoSize = true;
            this.checkBox22.Location = new System.Drawing.Point(3, 528);
            this.checkBox22.Name = "checkBox22";
            this.checkBox22.Size = new System.Drawing.Size(566, 19);
            this.checkBox22.TabIndex = 39;
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
            this.richTextBoxResults.TabIndex = 1;
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
            this.Text = "Form1";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button buttonConfigurations;
        private CheckBox checkBox16;
        private CheckBox checkBox17;
        private CheckBox checkBox18;
        private CheckBox checkBox14;
        private CheckBox checkBox12;
        private CheckBox checkBox9;
        private CheckBox checkBox8;
        private CheckBox checkBox3;
        private CheckBox checkBox19;
        private CheckBox checkBox20;
        private CheckBox checkBox21;
        private CheckBox checkBox22;
        private ProgressBar progressBarInstall;
        internal CheckBox checkBox5;
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
        public CheckBox checkBoxSharedPath;
    }
}