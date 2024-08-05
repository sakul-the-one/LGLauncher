using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace LGLauncher
{
    public partial class Form1 : Form
    {
        Dictionary<string, Installation> _installations;
        Installation me;
        bool MeUpdate = false;
        string CachePath = "Cache";
        string InstallationsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Installations\";
        int Downloads = 0;
        bool ClosingPending = false;
        List<Installation> CurrentOnUpdate = new List<Installation>(); //Damm, this could be used insted of `int Downloads = 0;`... Anyway, maybe gonna fix this some time later
        List<Installation> StartAfterUpdate = new List<Installation>();

        void DebugOtherWindows()
        {
            DownloadForm df = new DownloadForm(new Installation(), this);
            df.Show();

            CreateInstallationPath CIP = new CreateInstallationPath(this);
            CIP.Show();
        }
        public Form1(string[] args, bool debug = false)
        {
            InitializeComponent();
            //Creation of Me
            me = new Installation(); //Me is always Hardcoded, to make it simple

            me.Name = "me";
            me.Version = "0.0.2";
            me.DownloadPath = @"https://github.com/sakul-the-one/LGLauncher/raw/main/build/LGLauncher.redir";
            me.InstallationPath = "\\" + CachePath + "\\me.zip";

            MeUpdate = me.NeedsUpdate();
            ReCheckInstlattion();

            if (MeUpdate)
            {
                launcherStatusLabel.Text = "New Update aviable: " + me.Version + " --> " + me.NewVersion;
            }
            else
            {
                selfUpdate.Visible = false;
                launcherStatusLabel.Text = me.Version;
            }

            //Intialising all Installs:
            UpdateList();
            HandleArguments(args); //needs to be after the list is updated!
            if (debug)
                DebugOtherWindows();
        }

        //SelfUpdateStuff
        private void selfUpdate_Click(object sender, EventArgs e)
        {
            ReCheckInstlattion();
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
            this.Close(); //Force Close. If you Download something else, it is kinda your problem though
        }

        public void ReCheckInstlattion()
        {
            string MePosition = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string BatContent = "@echo off\n"
                + "echo Unzipping the new Installationfile\n"
                + "powershell -Command \"Expand-Archive '" + MePosition + me.InstallationPath + "' -DestinationPath '" + MePosition + "' -Force\"\n"
                + "start \"\" \"" + MePosition + "\\LGLauncher.exe\"";
            write("UnPack.bat", BatContent);
        }

        //Really Needed Functions
        public void UpdateInstalls()
        {
            listView1.Items.Clear();
            _installations = new Dictionary<string, Installation>();
            try
            {
                string[] files = Directory.GetFiles(InstallationsPath);

                int img = 0;
                ImageList imageList = new ImageList();
                imageList.ImageSize = new Size(64, 64);

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
                    //adding it with Picture, maybe?
                    imageList.Images.Add(CreateBitmapFromText(name[0].ToString()));

                    listView1.Items.Add(Path.GetFileNameWithoutExtension(name), img);
                    _installations.Add(name, install);
                    img++;
                }

                listView1.LargeImageList = imageList;
                // comboBox1.SelectedIndex = index;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Bitmap CreateBitmapFromText(string text)
        {
            Bitmap bmp = new Bitmap(64, 64);
            RectangleF rectf = new RectangleF(10, 10, 64, 64);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                //new Color(100,112, 146, 190));

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.Clear(Color.Transparent);

                //------------Pixels: X < 32 -> Round Corners; X = 32 -> Complete Circle; X = 63 -> This wierd (But also cool) atom shape
                int CornerRadius = 16;
                CornerRadius *= 2; //Leave the *2 for ehhhh, idk, ask this guy: https://stackoverflow.com/questions/1758762/how-to-create-image-with-rounded-corners-in-c
                GraphicsPath gp = new GraphicsPath();
                Brush brush = new SolidBrush(Color.FromArgb(88, 101, 190)); //Colors: 112, 146, 190
                gp.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
                gp.AddArc(0 + 64 - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
                gp.AddArc(0 + 64 - CornerRadius, 0 + 64 - CornerRadius, CornerRadius, CornerRadius, 0, 90);
                gp.AddArc(0, 0 + 64 - CornerRadius, CornerRadius, CornerRadius, 90, 90);
                g.FillPath(brush, gp);

                using (Font font = new Font("Arial", 32))
                {
                    g.DrawString(text, font, Brushes.Black, rectf);
                }

                g.Flush();
            }

            return bmp;
        }
        // string CInstallKey;
        public void UpdateList(bool CheckForUpdates = true, string CheckJustThis = "")
        {
            UpdateInstalls();
            if (CheckForUpdates)
            {
                for (int i = 0; i < listView1.Items.Count; i++) //ListView items are sorted in Numbers, not strings like our Dictionary! => For Loop instead of foreach!
                {
                    string CurrentFile = listView1.Items[i].Text + ".lgif"; //We never changed the name of the item, but rather the Text
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
                    UpdateThis(install.Value);
                }
            }
        }

        //Used for only selected stuff
        private void downloadSelectedButton_Click(object sender, EventArgs e)
        {
            if (!(listView1.SelectedItems.Count > 0)) { NothingSelectedError(); return; }
            UpdateThis(_installations[listView1.SelectedItems[0].Text + ".lgif"]);
        }
        private void createShortcut_Click(object sender, EventArgs e)
        {
            if (!(listView1.SelectedItems.Count > 0)) { NothingSelectedError(); return; }

            string MePosition = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            IShellLink link = (IShellLink)new ShellLink();
            // setup shortcut information
            //link.SetDescription("This is the description when hovered over.");
            Installation Cinstall = _installations[listView1.SelectedItems[0].Text + ".lgif"];
            //link.SetPath(Cinstall.InstallationPath + "\\" + Path.GetFileNameWithoutExtension(Cinstall.Name) + "\\" + Path.GetFileNameWithoutExtension(Cinstall.Name) + ".exe");
            link.SetPath(MePosition + "\\LGLauncher.exe");
            link.SetArguments("-cfus " + Cinstall.Name); //cfus => Check for Update & Start//See HandleArguments() for more commands
            link.SetWorkingDirectory(MePosition);
            // save it
            IPersistFile file = (IPersistFile)link;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            file.Save(Path.Combine(desktopPath, listView1.SelectedItems[0].Text + ".lnk"), false);
        }

        private void startSelected_Click(object sender, EventArgs e)
        {
            if (!(listView1.SelectedItems.Count > 0)) { NothingSelectedError(); return; }
            PendingStart(_installations[listView1.SelectedItems[0].Text + ".lgif"]);
        }

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

        //ToolStrip
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateInstallationPath CIP = new CreateInstallationPath(this);
            CIP.Show();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportImportFile();
        }

        private void clearCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(CachePath);
            long size = 0;
            int UnitType = 0;
            string Unit;

            foreach (string file in files)
            {
                long length = new FileInfo(file).Length; //Result in Bytes (8 Bits)
                size += length;
            }

            while (size > 1024 && UnitType < 3) //No need for bigger than GB
            {
                size /= 1024;
                UnitType++;
            }
            switch (UnitType)
            {
                case 0: Unit = " Bytes"; break;
                case 1: Unit = " KB"; break;
                case 2: Unit = " MB"; break;
                case 3: Unit = " GB"; break;
                //Default
                default: Unit = " Bytes"; break;
            }

            DialogResult mb = MessageBox.Show("Do you really want to clear the Cache? (" + size + Unit + " of all files in the Cache)", "Clear Cache?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mb == DialogResult.Yes)
            {
                foreach (string file in files)
                {
                    FileInfo FI = new FileInfo(file);
                    FI.Delete();
                }
                MessageBox.Show("The Cache was cleared! (" + size + Unit + " of all files in the Cache)", "Cleared Cache", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        //This isnt copied from 2020, but still needed Function
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
        private void NothingSelectedError()
        {
            MessageBox.Show("You have no item selected!", "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void UpdateThis(Installation install)
        {
            CurrentOnUpdate.Add(install);
            DownloadForm df = new DownloadForm(install, this);
            Downloads++;
            df.Show();
            df.DownloadFormFinished += DownloadingFinished;
            df.UpdateApplication();
        }
        void DownloadingFinished(object sender, Installation install)
        {
            Downloads--;
            CurrentOnUpdate.Remove(install);
            if (StartAfterUpdate.Contains(install))
            {
                StartAfterUpdate.Remove(install);
                PendingStart(install);
            }
            if (ClosingPending) BClose();//The same as return;
        }

        void HandleArguments(string[] args, bool ProgrammStart = true)
        {
            if (args.Length == 0) return;
            if (Path.GetExtension(args[0]) == ".lgif" || Path.GetExtension(args[0]) == "lgif") ImportImportFile(args[0]); //Still dont know if it needs the dot or not
            if (args[0][0] == '-')
            {
                if (ProgrammStart) this.Hide();

                string game = "";
                for (int i = 1; i < args.Length; i++)
                {
                    game += args[i] + " ";
                }
                //MessageBox.Show(args[0] + "\n" + game, "Hi");
                Installation Cinstall = _installations[game.Remove(game.Length - 1)];

                //Process.Start("explorer.exe", Cinstall.InstallationPath + "\\" + Path.GetFileNameWithoutExtension(Cinstall.Name) + "\\");

                //MessageBox.Show((Cinstall.InstallationPath + "\\" + Path.GetFileNameWithoutExtension(Cinstall.Name) + "\\" + Path.GetFileNameWithoutExtension(Cinstall.Name) + ".exe"), "explorer.exe");
                switch (args[0])
                {
                    case "-checkForUpdateStart":
                    case "-cfus": if (Cinstall.NeedsUpdate()) { UpdateThis(Cinstall); } PendingStart(Cinstall); break; //Check for Update & Start
                    case "-checkForUpdate":
                    case "-cfu": if (Cinstall.NeedsUpdate()) { UpdateThis(Cinstall);  } break; //Check for Update
                    case "-start":
                    case "-s": PendingStart(Cinstall); break; //Start
                    case "-download":
                    case "-d": UpdateThis(Cinstall); break; //Download
                    case "-downloadStart":
                    case "-ds": UpdateThis(Cinstall); PendingStart(Cinstall); break; //Download
                }
                if(ProgrammStart)
                    BClose();
            }
        }

        void BClose() // -> BetterClose
        {
            if (Downloads <= 0) //actually, this shouldnt be under 0!
                this.Close();
            else ClosingPending = true;
        }

        void PendingStart(Installation install)
        {
            if (!CurrentOnUpdate.Contains(install))
                Process.Start(install.InstallationPath + "\\" + Path.GetFileNameWithoutExtension(install.Name) + "\\" + Path.GetFileNameWithoutExtension(install.Name) + ".exe");
            else
                StartAfterUpdate.Add(install);
        }

        void ImportImportFile(string arg = "")
        {
            if (arg != "")
            {
                FileStream fs = new FileStream(arg, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string Downloadpath = sr.ReadLine();
                sr.Close();

                string NewFileName = Path.GetFileName(arg);
                if (Path.GetExtension(arg) != ".lgif")
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
                    write(InstallationsPath + NewFileName, Downloadpath + "\n" + folderPath + "\n-1");
                }
                return;
            }
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
                    write(InstallationsPath + NewFileName, Downloadpath + "\n" + folderPath + "\n-1");
                }
            }
        }
    }
}
