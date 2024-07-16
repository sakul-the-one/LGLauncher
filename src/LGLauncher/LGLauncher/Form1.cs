using System;
using System.IO;
using System.Windows.Forms;

namespace LGLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                    write("Installations\\" + NewFileName, Downloadpath + "\n" + folderPath);
                }
            }
        }
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

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateInstallationPath CIP = new CreateInstallationPath(this);
            CIP.Show();
        }

        public void UpdateList(bool CheckForUpdates = true, string CheckJustThis = "")
        {

            if (CheckForUpdates)
            {
                //CheckAll
            }
            if (CheckJustThis != "")
            {
                //Check
            }
        }
    }


    public class Update
    {
        public Update(Installation install, Vector2 Pos)
        {

        }
    }
}
