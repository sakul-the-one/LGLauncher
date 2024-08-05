namespace LGLauncher
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.launcherStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.selfUpdate = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearCacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.updateButton = new System.Windows.Forms.Button();
            this.selectedLabel = new System.Windows.Forms.Label();
            this.downloadSelectedButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.startSelected = new System.Windows.Forms.Button();
            this.createShortcut = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launcherStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 419);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(794, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // launcherStatusLabel
            // 
            this.launcherStatusLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.launcherStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.launcherStatusLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.launcherStatusLabel.Name = "launcherStatusLabel";
            this.launcherStatusLabel.Size = new System.Drawing.Size(80, 17);
            this.launcherStatusLabel.Text = "Update or not";
            // 
            // selfUpdate
            // 
            this.selfUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selfUpdate.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.selfUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.selfUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.selfUpdate.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.selfUpdate.Location = new System.Drawing.Point(719, 419);
            this.selfUpdate.Name = "selfUpdate";
            this.selfUpdate.Size = new System.Drawing.Size(75, 22);
            this.selfUpdate.TabIndex = 2;
            this.selfUpdate.Text = "Update";
            this.selfUpdate.UseVisualStyleBackColor = false;
            this.selfUpdate.Click += new System.EventHandler(this.selfUpdate_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(794, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.createToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearCacheToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(134, 6);
            // 
            // clearCacheToolStripMenuItem
            // 
            this.clearCacheToolStripMenuItem.Name = "clearCacheToolStripMenuItem";
            this.clearCacheToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.clearCacheToolStripMenuItem.Text = "Clear Cache";
            this.clearCacheToolStripMenuItem.Click += new System.EventHandler(this.clearCacheToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // mainPanel
            // 
            this.mainPanel.AutoSize = true;
            this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainPanel.Location = new System.Drawing.Point(159, 27);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(0, 0);
            this.mainPanel.TabIndex = 4;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.Color.Gray;
            this.listView1.BackgroundImageTiled = true;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(234, 66);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(524, 347);
            this.listView1.TabIndex = 5;
            this.listView1.TileSize = new System.Drawing.Size(200, 100);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(135, 66);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 6;
            this.updateButton.Text = "Update All";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // selectedLabel
            // 
            this.selectedLabel.AutoSize = true;
            this.selectedLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.selectedLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.selectedLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.selectedLabel.Location = new System.Drawing.Point(14, 14);
            this.selectedLabel.Name = "selectedLabel";
            this.selectedLabel.Size = new System.Drawing.Size(54, 15);
            this.selectedLabel.TabIndex = 7;
            this.selectedLabel.Text = "Selected:";
            // 
            // downloadSelectedButton
            // 
            this.downloadSelectedButton.Location = new System.Drawing.Point(13, 43);
            this.downloadSelectedButton.Name = "downloadSelectedButton";
            this.downloadSelectedButton.Size = new System.Drawing.Size(95, 23);
            this.downloadSelectedButton.TabIndex = 8;
            this.downloadSelectedButton.Text = "Download Selected";
            this.downloadSelectedButton.UseVisualStyleBackColor = true;
            this.downloadSelectedButton.Click += new System.EventHandler(this.downloadSelectedButton_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.startSelected);
            this.panel1.Controls.Add(this.createShortcut);
            this.panel1.Controls.Add(this.selectedLabel);
            this.panel1.Controls.Add(this.downloadSelectedButton);
            this.panel1.Location = new System.Drawing.Point(12, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 318);
            this.panel1.TabIndex = 9;
            // 
            // startSelected
            // 
            this.startSelected.Location = new System.Drawing.Point(14, 103);
            this.startSelected.Name = "startSelected";
            this.startSelected.Size = new System.Drawing.Size(94, 23);
            this.startSelected.TabIndex = 10;
            this.startSelected.Text = "Start";
            this.startSelected.UseVisualStyleBackColor = true;
            this.startSelected.Click += new System.EventHandler(this.startSelected_Click);
            // 
            // createShortcut
            // 
            this.createShortcut.Location = new System.Drawing.Point(14, 73);
            this.createShortcut.Name = "createShortcut";
            this.createShortcut.Size = new System.Drawing.Size(94, 23);
            this.createShortcut.TabIndex = 9;
            this.createShortcut.Text = "Create Shortcut";
            this.createShortcut.UseVisualStyleBackColor = true;
            this.createShortcut.Click += new System.EventHandler(this.createShortcut_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(794, 441);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.selfUpdate);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 370);
            this.Name = "Form1";
            this.Text = "Luftgleiter launcher";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel launcherStatusLabel;
        private System.Windows.Forms.Button selfUpdate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label selectedLabel;
        private System.Windows.Forms.Button downloadSelectedButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearCacheToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button createShortcut;
        private System.Windows.Forms.Button startSelected;
    }
}

