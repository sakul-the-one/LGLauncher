using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace LGLauncher
{
    public class Installation
    {
        public string Name;
        public string DownloadPath; //Path were it should Download information to download
        public string InstallationPath;
        public string Version; //Trust me, it needs to be a string
        public string NewVersion;
        public string RealDownloadPath; //Path from where it should download
        public Color BColor = Color.White;

        public bool NeedsUpdate() //Highly Dangerous btw.
        {
            bool PC = pNeedsUpdate();//PrivateCheck => PC
            if (Color.Red == BColor) return false;
            else return PC;
        }
        private bool pNeedsUpdate()
        {
            string[] Data = getCurrentEverything(); //Get Data From Website/Server
            if (Data == null) return false;//Check for the return of the Data
            //Sets Data
            NewVersion = Data[0];
            RealDownloadPath = Data[1];
            MessageBox.Show("\"" + Data[0] + "\"" + "\n" + "\"" + Version + "\"", "Something went alright! "+Name, MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (Version != Data[0]) //Check Version
                return true;
            else BColor = Color.Green;
            return false;
        }

        string[] getCurrentEverything()
        {
            try
            {
                string[] Everything = new string[2];

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(DownloadPath);
                request.UseDefaultCredentials = true;
                request.UserAgent = "LGLauncher-UpdateRequest";
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
                using (StreamReader sr = new StreamReader(data))
                {
                    Everything[0] = sr.ReadLine();
                    Everything[1] = sr.ReadLine();
                }
                MessageBox.Show(Everything[0] + "\n" + Everything[1], "Something went alright! {getCurrentEverything} + " + DownloadPath, MessageBoxButtons.OK, MessageBoxIcon.Question );
                return Everything;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + DownloadPath, "Something went wrong {getCurrentEverything}", MessageBoxButtons.OK, MessageBoxIcon.Error); //We dont need this in the build!!
                BColor = Color.Red;
                return new string[2];
            }
        }
    }
}
//public static void ExtractToDirectory(this ZipArchive archive, string destinationDirectoryName, bool overwrite) //https://stackoverflow.com/questions/14795197/forcefully-replacing-existing-files-during-extracting-file-using-system-io-compr
//{
//    if (!overwrite)
//    {
//        archive.ExtractToDirectory(destinationDirectoryName);
//        return;
//    }

//    DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
//    string destinationDirectoryFullPath = di.FullName;

//    foreach (ZipArchiveEntry file in archive.Entries)
//    {
//        string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));

//        if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
//        {
//            throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
//        }

//        if (file.Name == "")
//        {// Assuming Empty for Directory
//            Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
//            continue;
//        }
//        file.ExtractToFile(completeFileName, true);
//    }
//}