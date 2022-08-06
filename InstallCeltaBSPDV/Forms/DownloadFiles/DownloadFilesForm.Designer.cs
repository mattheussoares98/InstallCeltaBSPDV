namespace InstallCeltaBSPDV.Forms {
    partial class DownloadFilesForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBoxSAT = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.satSweda = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.keyboardSmak = new System.Windows.Forms.RadioButton();
            this.keyboardGertec = new System.Windows.Forms.RadioButton();
            this.buttonDownloadFiles = new System.Windows.Forms.Button();
            this.checkedListBoxUtilities = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxPrinters = new System.Windows.Forms.CheckedListBox();
            this.groupBoxSAT.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSAT
            // 
            this.groupBoxSAT.Controls.Add(this.radioButton1);
            this.groupBoxSAT.Controls.Add(this.satSweda);
            this.groupBoxSAT.Location = new System.Drawing.Point(188, 12);
            this.groupBoxSAT.Name = "groupBoxSAT";
            this.groupBoxSAT.Size = new System.Drawing.Size(190, 167);
            this.groupBoxSAT.TabIndex = 2;
            this.groupBoxSAT.TabStop = false;
            this.groupBoxSAT.Text = "SATs";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(9, 53);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(55, 19);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Tanca";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // satSweda
            // 
            this.satSweda.AutoSize = true;
            this.satSweda.Location = new System.Drawing.Point(9, 28);
            this.satSweda.Name = "satSweda";
            this.satSweda.Size = new System.Drawing.Size(59, 19);
            this.satSweda.TabIndex = 0;
            this.satSweda.TabStop = true;
            this.satSweda.Text = "Sweda";
            this.satSweda.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.keyboardSmak);
            this.groupBox1.Controls.Add(this.keyboardGertec);
            this.groupBox1.Location = new System.Drawing.Point(188, 309);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 89);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Teclados";
            // 
            // keyboardSmak
            // 
            this.keyboardSmak.AutoSize = true;
            this.keyboardSmak.Location = new System.Drawing.Point(9, 53);
            this.keyboardSmak.Name = "keyboardSmak";
            this.keyboardSmak.Size = new System.Drawing.Size(54, 19);
            this.keyboardSmak.TabIndex = 1;
            this.keyboardSmak.TabStop = true;
            this.keyboardSmak.Text = "Smak";
            this.keyboardSmak.UseVisualStyleBackColor = true;
            // 
            // keyboardGertec
            // 
            this.keyboardGertec.AutoSize = true;
            this.keyboardGertec.Location = new System.Drawing.Point(9, 28);
            this.keyboardGertec.Name = "keyboardGertec";
            this.keyboardGertec.Size = new System.Drawing.Size(59, 19);
            this.keyboardGertec.TabIndex = 0;
            this.keyboardGertec.TabStop = true;
            this.keyboardGertec.Text = "Gertec";
            this.keyboardGertec.UseVisualStyleBackColor = true;
            // 
            // buttonDownloadFiles
            // 
            this.buttonDownloadFiles.Location = new System.Drawing.Point(399, 327);
            this.buttonDownloadFiles.Name = "buttonDownloadFiles";
            this.buttonDownloadFiles.Size = new System.Drawing.Size(275, 66);
            this.buttonDownloadFiles.TabIndex = 4;
            this.buttonDownloadFiles.Text = "Iniciar downloads";
            this.buttonDownloadFiles.UseVisualStyleBackColor = true;
            this.buttonDownloadFiles.Click += new System.EventHandler(this.buttonDownloadFiles_Click);
            // 
            // checkedListBoxUtilities
            // 
            this.checkedListBoxUtilities.CheckOnClick = true;
            this.checkedListBoxUtilities.FormattingEnabled = true;
            this.checkedListBoxUtilities.Location = new System.Drawing.Point(12, 12);
            this.checkedListBoxUtilities.Name = "checkedListBoxUtilities";
            this.checkedListBoxUtilities.Size = new System.Drawing.Size(170, 22);
            this.checkedListBoxUtilities.TabIndex = 0;
            // 
            // checkedListBoxPrinters
            // 
            this.checkedListBoxPrinters.CheckOnClick = true;
            this.checkedListBoxPrinters.FormattingEnabled = true;
            this.checkedListBoxPrinters.Location = new System.Drawing.Point(399, 13);
            this.checkedListBoxPrinters.Name = "checkedListBoxPrinters";
            this.checkedListBoxPrinters.Size = new System.Drawing.Size(170, 22);
            this.checkedListBoxPrinters.TabIndex = 5;
            this.checkedListBoxPrinters.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // DownloadFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 410);
            this.Controls.Add(this.checkedListBoxPrinters);
            this.Controls.Add(this.checkedListBoxUtilities);
            this.Controls.Add(this.buttonDownloadFiles);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxSAT);
            this.Name = "DownloadFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.groupBoxSAT.ResumeLayout(false);
            this.groupBoxSAT.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBoxSAT;
        private RadioButton radioButton1;
        private RadioButton satSweda;
        private GroupBox groupBox1;
        private RadioButton keyboardSmak;
        private RadioButton keyboardGertec;
        private Button buttonDownloadFiles;
        private CheckedListBox checkedListBoxUtilities;
        private CheckedListBox checkedListBoxPrinters;
    }
}