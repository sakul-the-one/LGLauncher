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
            Application.SetCompatibleTextRenderingDefault(false);
            CC();
            CIF();
            Application.Run(new Form1());
        }

        static void CC() //Checks if the Cachefolder exsist or not (CheckCache => CC)
        {
            string path = "Cache";//Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Vokabeln\"; //Definitly not copied from an other Programm ^^
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    //cConsole.WriteLine("That path exists already.");
                }
                else
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    //cConsole.WriteLine("The directory was created successfully at " + Directory.GetCreationTime(path) + ".");
                }
            }
            catch (Exception e)
            {
                //cConsole.WriteLine("The process failed: " + e.ToString());
            }
        }
        static void CIF() //CheckInstallationsFolder => CIF
        {
            string path = "Installations";
            try
            {
                if (Directory.Exists(path))
                {
                    //cConsole.WriteLine("That path exists already.");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    //cConsole.WriteLine("The directory was created successfully at " + Directory.GetCreationTime(path) + ".");
                }
            }
            catch (Exception e)
            {
                //cConsole.WriteLine("The process failed: " + e.ToString());
            }
        }
    }
}
