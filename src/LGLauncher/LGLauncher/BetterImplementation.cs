using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGLauncher
{
    internal class BetterImplementation
    {

        public event InstallProgressChangedHandler InstallProgressChanged;
        public delegate void InstallProgressChangedHandler(object sender, InstallProgressChangedEventArgs e);

        public class InstallProgressChangedEventArgs
        {
            public bool Completed = false;
            public int ProgressPercentageInt;
            public float ProgressPercentageFloat;

            public InstallProgressChangedEventArgs(int CPoints /*Current Points*/, int MPoints /*Max Points*/)
            {
                if (CPoints == MPoints) Completed = true;
                ProgressPercentageFloat = (CPoints / MPoints) * 100;
                ProgressPercentageInt = (int)ProgressPercentageFloat;
            }
        }

        //the Importing shit
        public async Task ExtractZipFileAsync(string Origin, string NewLocation)
        {
            int installMax = 0;
            int install = 0;
            float percent = 0;

            FileStream fs = new FileStream(Origin, FileMode.Open, FileAccess.Read);
            ZipArchive archive = new ZipArchive(fs);

            installMax = archive.Entries.Count * 2;

            //Make Space (Deleting old Folders, needs to be done for most Games!)
            try
            {
                foreach (ZipArchiveEntry file in archive.Entries)
                {

                    install++;
                    percent = install / installMax;
                    InstallProgressChanged?.Invoke(this, new InstallProgressChangedEventArgs(install, installMax));

                    string completeFileName = Path.GetFullPath(Path.Combine(NewLocation, file.FullName));

                    if (!completeFileName.StartsWith(NewLocation, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                    }


                    if (Directory.Exists(Path.GetDirectoryName(completeFileName)))
                        await Task.Run(() => Directory.Delete(Path.GetDirectoryName(completeFileName), true));
                }
            }
            catch { }

            //Afzer Space is there, create everything new!
            foreach (ZipArchiveEntry file in archive.Entries)
            {
                install++;
                percent = install / installMax;
                InstallProgressChanged?.Invoke(this, new InstallProgressChangedEventArgs(install, installMax));
                //MessageBox.Show("4", "Something went alright! {Download()}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string completeFileName = Path.GetFullPath(Path.Combine(NewLocation, file.FullName));

                if (!completeFileName.StartsWith(NewLocation, StringComparison.OrdinalIgnoreCase))
                {
                    throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                }
                if (!Directory.Exists(Path.GetDirectoryName(completeFileName)))//Made by myself
                    await Task.Run(() => Directory.CreateDirectory(Path.GetDirectoryName(completeFileName)));
                await Task.Run(() => file.ExtractToFile(completeFileName, true));
            }
            archive.Dispose();
        }
    }
}
