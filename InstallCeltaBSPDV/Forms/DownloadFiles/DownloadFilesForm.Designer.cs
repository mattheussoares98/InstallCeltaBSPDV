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
            this.buttonDownloadFiles = new System.Windows.Forms.Button();
            this.checkedListBoxUtilities = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxPrinters = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxSats = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkedListBoxPinPads = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonDownloadFiles
            // 
            this.buttonDownloadFiles.Location = new System.Drawing.Point(201, 236);
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
            this.checkedListBoxUtilities.Location = new System.Drawing.Point(12, 36);
            this.checkedListBoxUtilities.Name = "checkedListBoxUtilities";
            this.checkedListBoxUtilities.Size = new System.Drawing.Size(170, 22);
            this.checkedListBoxUtilities.TabIndex = 0;
            // 
            // checkedListBoxPrinters
            // 
            this.checkedListBoxPrinters.CheckOnClick = true;
            this.checkedListBoxPrinters.FormattingEnabled = true;
            this.checkedListBoxPrinters.Location = new System.Drawing.Point(385, 36);
            this.checkedListBoxPrinters.Name = "checkedListBoxPrinters";
            this.checkedListBoxPrinters.Size = new System.Drawing.Size(170, 22);
            this.checkedListBoxPrinters.TabIndex = 5;
            this.checkedListBoxPrinters.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // checkedListBoxSats
            // 
            this.checkedListBoxSats.CheckOnClick = true;
            this.checkedListBoxSats.FormattingEnabled = true;
            this.checkedListBoxSats.Location = new System.Drawing.Point(201, 36);
            this.checkedListBoxSats.Name = "checkedListBoxSats";
            this.checkedListBoxSats.Size = new System.Drawing.Size(170, 22);
            this.checkedListBoxSats.TabIndex = 6;
            this.checkedListBoxSats.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxSats_ItemCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Utilitários";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "SATs";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(385, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Impressoras";
            // 
            // checkedListBoxPinPads
            // 
            this.checkedListBoxPinPads.CheckOnClick = true;
            this.checkedListBoxPinPads.FormattingEnabled = true;
            this.checkedListBoxPinPads.Location = new System.Drawing.Point(574, 36);
            this.checkedListBoxPinPads.Name = "checkedListBoxPinPads";
            this.checkedListBoxPinPads.Size = new System.Drawing.Size(170, 22);
            this.checkedListBoxPinPads.TabIndex = 10;
            this.checkedListBoxPinPads.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxPinPads_ItemCheck);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(574, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "PinPads";
            // 
            // DownloadFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 314);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBoxUtilities);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkedListBoxPinPads);
            this.Controls.Add(this.checkedListBoxSats);
            this.Controls.Add(this.buttonDownloadFiles);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkedListBoxPrinters);
            this.Name = "DownloadFilesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Downloads";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button buttonDownloadFiles;
        public CheckedListBox checkedListBoxUtilities;
        public CheckedListBox checkedListBoxPrinters;
        public CheckedListBox checkedListBoxSats;
        private Label label1;
        private Label label2;
        private Label label3;
        public CheckedListBox checkedListBoxPinPads;
        private Label label4;
    }
}