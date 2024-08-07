﻿using System;
using System.IO;
using System.Windows.Forms;

namespace LGLauncher
{
    public partial class CreateInstallationPath : Form
    {
        Form1 Daddy;
        //string CachePath = "Cache";
        string InstallationsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Installations\";
        public CreateInstallationPath(Form1 daddy)
        {
            InitializeComponent();
            Daddy = daddy;
        }

        private void createInstallFileButton_Click(object sender, EventArgs e)
        {
            // FileStream fs = new FileStream(string );
            string Name = nameTextBox.Text;
            string DownloadPath = URLTextBox.Text;
            string InstallPath = downloadLocationTextBox.Text; //Why did I choose this name?

            //Try to catch Typos
            try { 
                string[] result = Form1.readWeb(DownloadPath, 2);
            }
            catch 
            {
                MessageBox.Show("Something wrong with the URL", "Something Wrong happend", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; //Make the User do it again
            }
            if (!Directory.Exists(InstallPath)) MessageBox.Show("This Path does not exist", "Something Wrong happend", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //Do the actually Stuff
            if (Name.Length > 0 && DownloadPath.Length > 0 && InstallPath.Length > 0)
            {
                try
                {
                    FileStream fs = new FileStream(InstallationsPath + Name + ".lgif", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(DownloadPath);
                    sw.WriteLine(InstallPath);
                    sw.WriteLine("-1");
                    sw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                MessageBox.Show("Fill out all Textboxes!", "Almost wrong", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Daddy.UpdateInstalls();
            this.Close();
        }

        private void FindInstallPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            // Set validate names and check file exists to false otherwise windows will
            // not let you select "Folder Selection."
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Folder Selection.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(folderBrowser.FileName);
                downloadLocationTextBox.Text = folderPath;
            }
        }
    }
}
