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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // "Installations" && "Cache"
            CheckFolder("Installations");
            CheckFolder("Cache");
            Application.Run(new Form1());
        }

        static bool CheckFolder(string path, bool CreateNew = true) 
        {
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    //cConsole.WriteLine("That path exists already.");
                    return true;
                }
                else
                {
                    // Try to create the directory.
                    if (!CreateNew) return false;
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    //cConsole.WriteLine("The directory was created successfully at " + Directory.GetCreationTime(path) + ".");
                    return true;
                }
            }
            catch (Exception e)
            {
                //cConsole.WriteLine("The process failed: " + e.ToString());
                return false;
            }
            return false;
        }
    }
}
