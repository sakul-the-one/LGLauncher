using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Windows.Forms;

namespace LGLauncher
{
    public partial class DownloadForm : Form
    {
        Installation installation;
        Form1 daddy;
        string ChachePath;
        int error = 0;
        bool done =false;

        public DownloadForm(Installation install, Form1 Daddy)
        {
            this.installation = install;
            ChachePath = @"Cache\" + install.Name + (Path.GetExtension(install.RealDownloadPath).ToLower() == ".zip" ? ".zip" : "");
            daddy = Daddy;
            //Chache Folder
            this.Text = install.Name;
            InitializeComponent();
        }

        
        public int UpdateApplication()
        {
           // MessageBox.Show(installation.RealDownloadPath+ "\n" + installation.Name  +"\n" + installation.Version, "Something went alright! {UpdateApplication()}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Download(installation.RealDownloadPath);
            return error;
        }
        void Download(string website)
        {
            try
            {
                //MessageBox.Show(website, "Something went alright! {Download()}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                using (WebClient wc = new WebClient())
                {
                    //req.UserAgent = "[any words that is more than 5 characters]";
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileAsync(
                       // Param1 = Link of file
                       new System.Uri(website),
                       // Param2 = Path to save
                       ChachePath
                   );
                    
                    done = true;
                }
            }
            catch (Exception ex)
            {
                error += 1;
                MessageBox.Show(ex.Message + "\n" + website, "Something went wrong {Download()}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Event to track the progress
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadLabel.Text = "Downloading: " +e.ProgressPercentage + "/100%";
            downloadBar.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100) Install();
        }
        
        void Install()
        {
            try
            {
                if (Path.GetExtension(ChachePath) == ".zip")
                {
                    //Un-Zip
                    string NewPath = installation.InstallationPath + "\\" + Path.GetFileNameWithoutExtension(installation.Name);
                    Program.CheckFolder(NewPath);
                    //ZipFile.ExtractToDirectory(ChachePath, NewPath);
                    ExtractZipFile(ChachePath, NewPath);
                }
                else
                {
                    // write(installation.InstallationPath, read(ChachePath));
                    File.Copy(ChachePath, installation.InstallationPath + "\\" + installation.Name + Path.GetExtension(installation.RealDownloadPath).ToLower());
                }
                installBar.Value = 100;//Another lie, but why would it take long for probably such small files? Except you use a hard-drive
                InstallLabel.Text = "Installing: 100/100%";
                Finish();
            }
            catch (System.IO.IOException) 
            {
                //We will ignore this. You can try it out without it, but it will just annoy you and it will somehow still works sooooooooooo
            }
            catch (Exception ex)
            {
                error += 1;
                MessageBox.Show(ex.Message, "Something went wrong {Install()}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Finish()
        {
            //Update the Installation
            FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Installations\" + Path.GetFileNameWithoutExtension(installation.Name) + ".lgif", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(installation.DownloadPath);// Updater Download path
            sw.WriteLine(installation.InstallationPath);// Installpath
            sw.WriteLine(installation.NewVersion); //Version
            sw.Close();

            installation.Version = installation.NewVersion;//I hope this works, if not, then not.
            daddy.UpdateList();

            this.Close();
        }

        void ExtractZipFile(string Origin, string NewLocation) 
        {
            FileStream fs = new FileStream(Origin, FileMode.Open);
            ZipArchive archive = new ZipArchive(fs);
            //Make Space (Deleting old Folders, needs to be done for most Games!)
            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.GetFullPath(Path.Combine(NewLocation, file.FullName));

                if (!completeFileName.StartsWith(NewLocation, StringComparison.OrdinalIgnoreCase))
                {
                    throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                }

                if (file.Name == "")
                {// Assuming Empty for Directory
                    Directory.Delete(Path.GetDirectoryName(completeFileName));
                    continue;
                }
                //file.ExtractToFile(completeFileName, true);
            }

            //Afzer Space is there, create everything new!
            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.GetFullPath(Path.Combine(NewLocation, file.FullName));

                if (!completeFileName.StartsWith(NewLocation, StringComparison.OrdinalIgnoreCase))
                {
                    throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                }

                if (file.Name == "")
                {// Assuming Empty for Directory
                    Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                    continue;
                }
                file.ExtractToFile(completeFileName, true);
            }
            fs.Dispose();
        }

        //Biggest Lie in history :)))))
        private void continueButton_Click(object sender, EventArgs e)
        {

        }

        private void pauseButton_Click(object sender, EventArgs e)
        {

        }
    }
}
