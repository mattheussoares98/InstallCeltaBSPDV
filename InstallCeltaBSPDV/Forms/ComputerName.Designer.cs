namespace InstallCeltaBSPDV {
    partial class ComputerName {
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
            this.labelSetComputerName = new System.Windows.Forms.Label();
            this.buttonSetComputerName = new System.Windows.Forms.Button();
            this.maskedTextBoxSetComputerName = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // labelSetComputerName
            // 
            this.labelSetComputerName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelSetComputerName.Location = new System.Drawing.Point(12, 9);
            this.labelSetComputerName.Name = "labelSetComputerName";
            this.labelSetComputerName.Size = new System.Drawing.Size(223, 110);
            this.labelSetComputerName.TabIndex = 1;
            this.labelSetComputerName.Text = "Digite o número do PDV para alterar o nome da máquina de acordo com o número do P" +
    "DV";
            // 
            // buttonSetComputerName
            // 
            this.buttonSetComputerName.Location = new System.Drawing.Point(160, 131);
            this.buttonSetComputerName.Name = "buttonSetComputerName";
            this.buttonSetComputerName.Size = new System.Drawing.Size(75, 24);
            this.buttonSetComputerName.TabIndex = 2;
            this.buttonSetComputerName.Text = "OK";
            this.buttonSetComputerName.UseVisualStyleBackColor = true;
            this.buttonSetComputerName.Click += new System.EventHandler(this.buttonSetComputerName_Click);
            // 
            // maskedTextBoxSetComputerName
            // 
            this.maskedTextBoxSetComputerName.Location = new System.Drawing.Point(12, 131);
            this.maskedTextBoxSetComputerName.Mask = "000";
            this.maskedTextBoxSetComputerName.Name = "maskedTextBoxSetComputerName";
            this.maskedTextBoxSetComputerName.Size = new System.Drawing.Size(142, 23);
            this.maskedTextBoxSetComputerName.TabIndex = 3;
            this.maskedTextBoxSetComputerName.ValidatingType = typeof(int);
            this.maskedTextBoxSetComputerName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.maskedTextBoxSetComputerName_KeyUp);
            // 
            // ComputerName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 167);
            this.Controls.Add(this.maskedTextBoxSetComputerName);
            this.Controls.Add(this.buttonSetComputerName);
            this.Controls.Add(this.labelSetComputerName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ComputerName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ComputerName";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ComputerName_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label labelSetComputerName;
        private Button buttonSetComputerName;
        private MaskedTextBox maskedTextBoxSetComputerName;
    }
}