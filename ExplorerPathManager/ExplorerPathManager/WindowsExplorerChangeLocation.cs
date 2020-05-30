using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorerPathManager
{
    public class WindowsExplorerChangeLocation
    {
        public static void WithoutPowerShellScript(string path)
        {
            Shell32.Shell shell = new Shell32.Shell();

            dynamic windows = shell.Windows();
            int cnt = windows.Count;
            if (cnt == 0)
            {
                var application = shell.Application;
                application.Explore(path);
            }
            else
            {
                dynamic windows1 = windows[0];
                windows1.Navigate(path);

                IntPtr hwnd = (IntPtr)windows1.HWND;

                WinApp.SetForegroundWindow(hwnd);
                WinApp.ShowWindow(hwnd,9);
            }
        }

        public static void WithPowerShellScript(string path)
        {
            string dir = Directory.GetCurrentDirectory();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"powershell.exe";
            startInfo.Arguments = $"{dir}\\WindowsExplorerChangeLocation.ps1 -path {path}";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
