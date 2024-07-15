namespace LGLauncher
{
    partial class DownloadForm
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
            this.downloadBar = new System.Windows.Forms.ProgressBar();
            this.installBar = new System.Windows.Forms.ProgressBar();
            this.downloadLabel = new System.Windows.Forms.Label();
            this.InstallLabel = new System.Windows.Forms.Label();
            this.pauseButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // downloadBar
            // 
            this.downloadBar.Location = new System.Drawing.Point(12, 36);
            this.downloadBar.Name = "downloadBar";
            this.downloadBar.Size = new System.Drawing.Size(478, 23);
            this.downloadBar.TabIndex = 0;
            // 
            // installBar
            // 
            this.installBar.Location = new System.Drawing.Point(12, 98);
            this.installBar.Name = "installBar";
            this.installBar.Size = new System.Drawing.Size(478, 23);
            this.installBar.TabIndex = 1;
            // 
            // downloadLabel
            // 
            this.downloadLabel.AutoSize = true;
            this.downloadLabel.Location = new System.Drawing.Point(13, 20);
            this.downloadLabel.Name = "downloadLabel";
            this.downloadLabel.Size = new System.Drawing.Size(55, 13);
            this.downloadLabel.TabIndex = 2;
            this.downloadLabel.Text = "Download";
            // 
            // InstallLabel
            // 
            this.InstallLabel.AutoSize = true;
            this.InstallLabel.Location = new System.Drawing.Point(9, 71);
            this.InstallLabel.Name = "InstallLabel";
            this.InstallLabel.Size = new System.Drawing.Size(51, 13);
            this.InstallLabel.TabIndex = 3;
            this.InstallLabel.Text = "Installing:";
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(12, 138);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 23);
            this.pauseButton.TabIndex = 4;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point(112, 138);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 5;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 178);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.InstallLabel);
            this.Controls.Add(this.downloadLabel);
            this.Controls.Add(this.installBar);
            this.Controls.Add(this.downloadBar);
            this.Name = "DownloadForm";
            this.Text = "DownloadForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar downloadBar;
        private System.Windows.Forms.ProgressBar installBar;
        private System.Windows.Forms.Label downloadLabel;
        private System.Windows.Forms.Label InstallLabel;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button continueButton;
    }
}