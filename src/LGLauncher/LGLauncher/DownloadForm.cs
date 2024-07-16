using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace LGLauncher
{
    public partial class DownloadForm : Form
    {
        Installation installation;
        Form1 daddy;
        string ChachePath;
        

        public DownloadForm(Installation install, Form1 Daddy)
        {
            this.installation = install;
            ChachePath = @"Cache\" + install.Name;//Chache Folder

            InitializeComponent();
            
        }
        public int DoStuff() 
        {
            Download(installation.DownloadPath);
            Install();
            return 0;
        }
        void Download(string website)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                 wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri(website),
                    // Param2 = Path to save
                    ChachePath
                );
            }
        }
        // Event to track the progress
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadLabel.Text = e.ProgressPercentage + "/100%";
            downloadBar.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100) Install();
        }

        void Install()
        {
            if (Path.GetExtension(ChachePath) == "zip")
            {
                //Un-Zip
                ZipFile.ExtractToDirectory(ChachePath, installation.InstallationPath);
                
            }
            else
            {
                write(installation.InstallationPath, read(ChachePath));         
            }
            installBar.Value = 100;//Another lie, but why would it take long for probably such small files? Except you use a hard-drive
            InstallLabel.Text = "Installing 100/100%";
        }


        //Biggest Lie in history :)))))
        private void continueButton_Click(object sender, EventArgs e)
        {

        }

        private void pauseButton_Click(object sender, EventArgs e)
        {

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
    }
}
