using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                write(@"Installations\"+ Path.GetFileName(path), read(path));
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
        }

        public void UpdateList(bool CheckForUpdates = true, string CheckJustThis = "") 
        {

            if(CheckForUpdates) 
            {
             //CheckAll
            }
            if(CheckJustThis != "") 
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
