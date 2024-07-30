﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Windows.Forms;

namespace LGLauncher
{
    public partial class Form1 : Form
    {
        Dictionary<string, Installation> _installations;
        Installation me;
        bool MeUpdate = false;
        public Form1(string[] args)
        {
            InitializeComponent();
            //Creation of Me
            me = new Installation(); //Me is always Hardcoded, to make it simple

            me.Name = "me";
            me.Version = "0.0.1t2";
            me.DownloadPath = @"https://github.com/sakul-the-one/LGLauncher/raw/main/build/LGLauncher.redir";
            me.InstallationPath = "\\Cache\\me.zip";

            MeUpdate = me.NeedsUpdate();
            // MessageBox.Show(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + me.InstallationPath, "Hi!");
            ReCheckInstlattion();
            if (MeUpdate) 
            {
                launcherStatusLabel.Text = "New Update aviable: " + me.Version + " --> " + me.NewVersion;
            }
            else 
            {
                meUpdatePrcoessBar.Visible = false;
                selfUpdate.Visible = false;
                launcherStatusLabel.Text = me.Version;
            }
            //Intialising all Installs:
            UpdateList();
        }

        //SelfUpdateStuff
        private void selfUpdate_Click(object sender, EventArgs e)
        {
            ReCheckInstlattion();
            meUpdatePrcoessBar.Value = 1;
            //MessageBox.Show("Stop", "Something went right", MessageBoxButtons.OK, MessageBoxIcon.Error); //For Debugging
            //Download(me.RealDownloadPath);
            DownloadForm df = new DownloadForm(me, this);
            //df.ShowInTaskbar = true;
            df.Show();
            df.DownloadFormFinished += Finish;
            df.UpdateApplication();
        }
        void Finish(object sender, Installation install)// AsyncCompletedEventArgs e)
        {
            //MessageBox.Show("Finished Downloading");
            System.Diagnostics.Process.Start("UnPack.bat");
            this.Close();
        }

        public void ReCheckInstlattion()
        {
            string MePosition = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string BatContent = "@echo off\n"
                + "echo Unzipping the new Installationfile\n"
                + "powershell -Command \"Expand-Archive '" + MePosition + me.InstallationPath+ "' -DestinationPath '" + MePosition + "' -Force\"\n"
                + "start \"\" \"" + MePosition + "\\LGLauncher.exe\"";
            write("UnPack.bat",BatContent);
        }

        //Really Needed Functions
        public void UpdateInstalls()
        {
            listView1.Items.Clear();
            _installations = new Dictionary<string, Installation>();
            try
            {
                string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Installations\");
                //cConsole.WriteLine("Found " + files.GetLength().ToString() + " Files");

                foreach (string file in files)
                {
                    //Prepare adding it to List
                    string name = Path.GetFileName(file);
                    Installation install = new Installation();
                    install.Name = name;

                    //Get Dada from File
                    FileStream fs = new FileStream(file, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    install.DownloadPath = sr.ReadLine();
                    install.InstallationPath = sr.ReadLine();
                    install.Version = sr.ReadLine();
                    sr.Close();

                    //bool NeedsUpdate = install.NeedsUpdate(); //Removed, because it would be just to much for that Function #ICanReadMyCode
                    //adding it
                    listView1.Items.Add(name);
                    _installations.Add(name, install);
                }
                // comboBox1.SelectedIndex = index;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       // string CInstallKey;
        public void UpdateList(bool CheckForUpdates = true, string CheckJustThis = "")
        {
            UpdateInstalls();
            if (CheckForUpdates)
            {
                for (int i = 0; i < listView1.Items.Count; i++) //ListView items are sorted in Numbers, not strings like our Dictionary! => For Loop instead of foreach!
                {
                    string CurrentFile = listView1.Items[i].Text; //We never changed the name of the item, but rather the Text
                    bool Needsupdate = _installations[CurrentFile].NeedsUpdate();
                    if (Needsupdate)
                    {
                        //CInstallKey = install.Key;
                        listView1.Items[i].BackColor = Color.Yellow;
                    }
                    else listView1.Items[i].BackColor = _installations[CurrentFile].BColor;
                }
            }
            if (CheckJustThis != "")
            {
                _installations[CheckJustThis].NeedsUpdate();
            }
        }
        //Buttons
        private void updateButton_Click(object sender, EventArgs e) //UpdateALL
        {
            foreach (KeyValuePair<string, Installation> install in _installations) 
            {
                //bool Needsupdate = install.Value.NeedsUpdate(); //Changed to have less Data exchange with the Internet
                if (install.Value.NewVersion != install.Value.Version) 
                {
                    //MessageBox.Show(install.Value.Name, "Something went alright!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DownloadForm df = new DownloadForm(install.Value, this);
                    //df.ShowInTaskbar = true;
                    df.Show();
                    df.UpdateApplication();
                }
            }

        }

        //Used for only selected stuff
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(listView1.SelectedItems.Count > 0)) 
            {
                selectedLabel.Text = "Selected: ";
            }
            else 
            {
                selectedLabel.Text = "Selected: " + listView1.SelectedItems[0].Text;//listView1.Items[listView1.index].Text;
            }
        }

        private void downloadSelectedButton_Click(object sender, EventArgs e)
        {
            if (!(listView1.SelectedItems.Count > 0)) { NothingSelectedError(); return; }

            DownloadForm df = new DownloadForm(_installations[listView1.SelectedItems[0].Text], this);
            df.Show();
            df.UpdateApplication();
        }

        private void NothingSelectedError() 
        {
            MessageBox.Show("You have no item selected!", "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //ToolStrip
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateInstallationPath CIP = new CreateInstallationPath(this);
            CIP.Show();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ImportDialog = new OpenFileDialog();
            ImportDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";//= Directory.GetCurrentDirectory();
            ImportDialog.Title = "Select an Import File";
            ImportDialog.Filter = "LG Import Files(*.lgif)|*.lgif" + "|All Files(*.*)|*.*";
            if (ImportDialog.ShowDialog() == DialogResult.OK)
            {
                string path = ImportDialog.FileName;
                //write(@"Installations\"+ Path.GetFileName(path), read(path));

                FileStream fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string Downloadpath = sr.ReadLine();
                sr.Close();

                string NewFileName = Path.GetFileName(path);
                if (Path.GetExtension(path) != ".lgif")
                    NewFileName += ".lgif";


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
                    write("Installations\\" + NewFileName, Downloadpath + "\n" + folderPath + "\n-1");
                }
            }
        }

        //Code below copied from me 2020 and i have no idea if this is good or bad, but it worked then, so why not now?
        public static void write(string Path, string Text)
        {
            FileStream fs = new FileStream(Path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(Text);
            sw.Close();
        }

        public static string read(string Path)
        {
            string Texxt = string.Empty;
            FileStream fs = new FileStream(Path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string zeile;
            while (sr.Peek() != -1)
            {
                zeile = sr.ReadLine();
                Texxt += zeile + "\n";
            }
            sr.Close();
            return Texxt;
        }
        public static string[] readWeb(string DownloadPath, int HowManyLines) 
        {
            string[] Everything = new string[HowManyLines];

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(DownloadPath);
            request.UseDefaultCredentials = true;
            request.UserAgent = "LGLauncher-UpdateRequest";
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            using (StreamReader sr = new StreamReader(data))
            {
                for (int i = 0; i < Everything.Length; i++)
                    Everything[i] = sr.ReadLine();
            }
            return Everything;
        }
    }
}
