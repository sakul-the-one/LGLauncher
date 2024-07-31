using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;

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

                    if (Directory.Exists(Path.GetDirectoryName(completeFileName)) && Path.GetDirectoryName(completeFileName) != NewLocation)//
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

    [ComImport]
    [Guid("00021401-0000-0000-C000-000000000046")]
    internal class ShellLink
    {
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214F9-0000-0000-C000-000000000046")]
    internal interface IShellLink
    {
        void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
        void GetIDList(out IntPtr ppidl);
        void SetIDList(IntPtr pidl);
        void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
        void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
        void GetHotkey(out short pwHotkey);
        void SetHotkey(short wHotkey);
        void GetShowCmd(out int piShowCmd);
        void SetShowCmd(int iShowCmd);
        void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
        void Resolve(IntPtr hwnd, int fFlags);
        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }
}
