namespace LGLauncher
{
    partial class CreateInstallationPath
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.createInstallFileButton = new System.Windows.Forms.Button();
            this.URLTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.downloadLocationTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FindInstallPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createInstallFileButton
            // 
            this.createInstallFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createInstallFileButton.Location = new System.Drawing.Point(522, 415);
            this.createInstallFileButton.Name = "createInstallFileButton";
            this.createInstallFileButton.Size = new System.Drawing.Size(75, 23);
            this.createInstallFileButton.TabIndex = 0;
            this.createInstallFileButton.Text = "Create";
            this.createInstallFileButton.UseVisualStyleBackColor = true;
            this.createInstallFileButton.Click += new System.EventHandler(this.createInstallFileButton_Click);
            // 
            // URLTextBox
            // 
            this.URLTextBox.Location = new System.Drawing.Point(12, 49);
            this.URLTextBox.Name = "URLTextBox";
            this.URLTextBox.Size = new System.Drawing.Size(186, 20);
            this.URLTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL";
            // 
            // downloadLocationTextBox
            // 
            this.downloadLocationTextBox.Location = new System.Drawing.Point(12, 101);
            this.downloadLocationTextBox.Name = "downloadLocationTextBox";
            this.downloadLocationTextBox.Size = new System.Drawing.Size(186, 20);
            this.downloadLocationTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Install Path";
            // 
            // FindInstallPath
            // 
            this.FindInstallPath.Location = new System.Drawing.Point(204, 98);
            this.FindInstallPath.Name = "FindInstallPath";
            this.FindInstallPath.Size = new System.Drawing.Size(37, 23);
            this.FindInstallPath.TabIndex = 5;
            this.FindInstallPath.Text = "Find";
            this.FindInstallPath.UseVisualStyleBackColor = true;
            // 
            // CreateInstallationPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 450);
            this.Controls.Add(this.FindInstallPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.downloadLocationTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.URLTextBox);
            this.Controls.Add(this.createInstallFileButton);
            this.Name = "CreateInstallationPath";
            this.Text = "CreateInstallationPath";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createInstallFileButton;
        private System.Windows.Forms.TextBox URLTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox downloadLocationTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button FindInstallPath;
    }
}