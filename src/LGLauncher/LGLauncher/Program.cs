using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGLauncher
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // "Installations" && "Cache"
            CheckFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Installations\");
            CheckFolder("Cache");
            Application.Run(new Form1(args));
        }

        public static bool CheckFolder(string path, bool CreateNew = true)
        {
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    return true;
                }
                else
                {
                    // Try to create the directory.
                    if (!CreateNew) return false;
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //return false;
        } 
    }
}
