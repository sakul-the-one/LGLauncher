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
            // CreateInstallationPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 450);
            this.Controls.Add(this.createInstallFileButton);
            this.Name = "CreateInstallationPath";
            this.Text = "CreateInstallationPath";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button createInstallFileButton;
    }
}