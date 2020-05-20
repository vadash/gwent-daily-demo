using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;

namespace launcher
{
    internal static class Program
    {
        //static string path = @"C:\projects\gwent-daily-reborn\release";

        private static void Main(string[] args)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            path = Path.GetDirectoryName(path);
            var machineId = GetMachineGuid();
            Console.WriteLine();

            #region main bot process

            var oldPath1 = path + @"\bin\" + "gwent-daily-reborn.exe";
            var uniqueExeName1 = GetHash(machineId + "appOne").Substring(0, 10);
            var newPath1 = path + @"\bin\" + uniqueExeName1 + ".exe";
            RenameExe(oldPath1, newPath1);
            StartExe(newPath1);

            #endregion

        }

        private static void RenameExe(string oldPath, string newPath)
        {
            // check for old version
            if (File.Exists(newPath))
            {
                var creation1 = File.GetLastWriteTimeUtc(oldPath);
                var creation2 = File.GetLastWriteTime(newPath);
                var difference = Math.Abs((creation1 - creation2).TotalSeconds);
                if (difference > 120)
                {
                    File.Delete(newPath);
                }
            }
            // copy once if needed (cant use symbolic links)
            if (!File.Exists(newPath))
                File.Copy(oldPath, newPath);
        }

        private static void StartExe(string path)
        {
            var proc2 = new ProcessStartInfo
            {
                FileName = path,
                WorkingDirectory = Path.GetDirectoryName(path)
            };
            Process.Start(proc2);
        }

        public static string GetHash(string inputString)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                foreach (byte b in hash.ComputeHash(enc.GetBytes(inputString)))
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }

        public static string GetMachineGuid()
        {
            const string location = @"SOFTWARE\Microsoft\Cryptography";
            const string name = "MachineGuid";

            using (var localMachineX64View =
                RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (var rk = localMachineX64View.OpenSubKey(location))
                {
                    if (rk == null)
                        throw new KeyNotFoundException(
                            string.Format("Key Not Found: {0}", location));

                    var machineGuid = rk.GetValue(name);
                    if (machineGuid == null)
                        throw new IndexOutOfRangeException(
                            string.Format("Index Not Found: {0}", name));

                    return machineGuid.ToString();
                }
            }
        }
    }
}
