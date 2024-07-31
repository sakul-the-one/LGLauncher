using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;
using static LGLauncher.BetterImplementation;

namespace LGLauncher
{
    public partial class DownloadForm : Form
    {
        Installation installation;
        Form1 daddy;
        string ChachePath;
        int error = 0;
        bool AlreadyInstalled = false;


        public event downloadFormFinished DownloadFormFinished;
        public delegate void downloadFormFinished(object sender, Installation install);

        public DownloadForm(Installation install, Form1 Daddy)
        {
            this.installation = install;
            daddy = Daddy;

            if (install.Name != null)
            {
                //Chache Folder
                ChachePath = @"Cache\" + install.Name + (Path.GetExtension(install.RealDownloadPath).ToLower() == ".zip" ? ".zip" : "");
                this.Text = install.Name; //Not Important anymore (Updated Design)
            }
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
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileCompleted += Install;
                    wc.DownloadFileAsync(
                       // Param1 = Link of file
                       new System.Uri(website),
                       // Param2 = Path to save
                       ChachePath
                   );
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
            downloadLabel.Text = "Downloading: " + e.ProgressPercentage + "/100%";
            downloadBar.Value = e.ProgressPercentage;
        }

        async void Install(object sender, AsyncCompletedEventArgs e)
        {
            UpdateInstallation(this, new InstallProgressChangedEventArgs(0, 100));
            try
            {
                if (Path.GetExtension(ChachePath) == ".zip")
                {
                    if (Path.GetFileNameWithoutExtension(installation.Name) != "me") //We dont want to unzip ourself for now!
                    {
                        //Un-Zip
                        string NewPath = installation.InstallationPath + "\\" + Path.GetFileNameWithoutExtension(installation.Name);
                        Program.CheckFolder(NewPath);
                        //ZipFile.ExtractToDirectory(ChachePath, NewPath);
                        //MessageBox.Show("0", "Something went alright! {Download()}", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        BetterImplementation bi = new BetterImplementation(); //bi like a bitc
                        bi.InstallProgressChanged += UpdateInstallation;
                        await bi.ExtractZipFileAsync(ChachePath, NewPath);
                    }
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
            catch (Exception ex)
            {
                error += 1;
                MessageBox.Show(ex.Message, "Something went wrong {Install()}", MessageBoxButtons.OK, MessageBoxIcon.Error);

                FinishWithError();
            }
        }
        void Finish()
        {
            //Update the Installation
            if (Path.GetFileNameWithoutExtension(installation.Name) != "me") //We dont want to unzip ourself for now!
            {
                FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Installations\" + Path.GetFileNameWithoutExtension(installation.Name) + ".lgif", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(installation.DownloadPath);// Updater Download path
                sw.WriteLine(installation.InstallationPath);// Installpath
                sw.WriteLine(installation.NewVersion); //Version
                sw.Close();

                installation.Version = installation.NewVersion;//I hope this works, if not, then not.
                daddy.UpdateList();
            }
            AlreadyInstalled = true;

            DownloadFormFinished?.Invoke(this, installation);
            this.Close();
        }

        void FinishWithError()
        {
            //Update the Installation
            if (Path.GetFileNameWithoutExtension(installation.Name) != "me")
            {
                FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Installations\" + Path.GetFileNameWithoutExtension(installation.Name) + ".lgif", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(installation.DownloadPath);// Updater Download path
                sw.WriteLine(installation.InstallationPath);// Installpath
                sw.WriteLine("ERROR -1"); //Version
                sw.Close();
            }

            installation.Version = "ERROR -1";//I hope this works, if not, then not.
            daddy.UpdateList();
            AlreadyInstalled = true;

            DownloadFormFinished?.Invoke(this, installation);
            this.Close();
        }

        void UpdateInstallation(object sender, InstallProgressChangedEventArgs e)
        {
            installBar.Value = e.ProgressPercentageInt;
            InstallLabel.Text = "Installing: " + e.ProgressPercentageInt + "/100%";
        }
        //Biggest Lie in history :)))))
        private void continueButton_Click(object sender, EventArgs e)
        {

        }

        private void pauseButton_Click(object sender, EventArgs e)
        {

        }

        private void arbortButton_Click(object sender, EventArgs e)
        {
            //Update the Installation
            if (Path.GetFileNameWithoutExtension(installation.Name) != "me" && installation.Name != null)
            {
                FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Installations\" + Path.GetFileNameWithoutExtension(installation.Name) + ".lgif", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(installation.DownloadPath);// Updater Download path
                sw.WriteLine(installation.InstallationPath);// Installpath
                sw.WriteLine(installation.Version); //Version
                sw.Close();
            }

            daddy.UpdateList();
            AlreadyInstalled = true;

            DownloadFormFinished?.Invoke(this, installation);
            this.Close();
        }
    }
}
