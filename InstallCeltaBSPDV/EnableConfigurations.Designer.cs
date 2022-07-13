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
            this.buttonConfigureFirewall = new System.Windows.Forms.Button();
            this.richTextBoxResults = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // buttonConfigureFirewall
            // 
            this.buttonConfigureFirewall.Location = new System.Drawing.Point(100, 298);
            this.buttonConfigureFirewall.Name = "buttonConfigureFirewall";
            this.buttonConfigureFirewall.Size = new System.Drawing.Size(286, 136);
            this.buttonConfigureFirewall.TabIndex = 0;
            this.buttonConfigureFirewall.Text = "Configurar firewall";
            this.buttonConfigureFirewall.UseVisualStyleBackColor = true;
            this.buttonConfigureFirewall.Click += new System.EventHandler(this.buttonConfigureFirewall_Click);
            // 
            // richTextBoxResults
            // 
            this.richTextBoxResults.Enabled = false;
            this.richTextBoxResults.Location = new System.Drawing.Point(24, 27);
            this.richTextBoxResults.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBoxResults.Name = "richTextBoxResults";
            this.richTextBoxResults.Size = new System.Drawing.Size(512, 248);
            this.richTextBoxResults.TabIndex = 1;
            this.richTextBoxResults.Text = "";
            // 
            // EnableConfigurations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 451);
            this.Controls.Add(this.richTextBoxResults);
            this.Controls.Add(this.buttonConfigureFirewall);
            this.Name = "EnableConfigurations";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Button buttonConfigureFirewall;
        private RichTextBox richTextBoxResults;
    }
}