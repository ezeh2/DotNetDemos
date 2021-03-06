﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NLog;
using Shell32;

namespace ExplorerPathManager
{
    public class WindowsExplorerChangeLocation
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static List<string> GetFoldersOfFileExplorers()
        {
            logger.Debug("GetFoldersOfFileExplorers, begin");

            List<string> ret = new List<string>();

            Shell32.Shell shell = new Shell32.Shell();

            dynamic windows = shell.Windows();
            int cnt = windows.Count;
            for (int i = 0; i < cnt; i++)
            {
                dynamic window = windows[i];
                string locationUrl = window.LocationURL;
                logger.Debug($"locationUrl: {locationUrl}");
                if (!string.IsNullOrEmpty(locationUrl))
                {
                    Uri uri = new Uri(locationUrl);
                    logger.Debug(uri);
                    string p = uri.AbsolutePath.Replace("/", "\\");
                    logger.Debug(p);
                    p = Uri.UnescapeDataString(p);
                    logger.Debug(p);
                    ret.Add(p);
                }
                else
                {
                    logger.Error("locationUrl is empty");
                }
            }

            return ret;
        }

        /// <summary>
        /// system.dynamic.dynamicobject
        /// https://docs.microsoft.com/en-us/dotnet/api/system.dynamic.dynamicobject?view=netcore-3.1
        /// system.dynamic.dynamicmetaobject.getdynamicmembernames
        /// https://docs.microsoft.com/en-us/dotnet/api/system.dynamic.dynamicmetaobject.getdynamicmembernames?view=netcore-3.1
        /// https://bytes.com/topic/c-sharp/answers/244327-idispatch-getidsfromnames
        /// </summary>
        /// <param name="path"></param>
        public static void WithoutPowerShellScript(string path)
        {
            // X();

            logger.Debug("WithoutPowerShellScript, begin");
            logger.Debug(path);
            if (!Directory.Exists(path))
            {
                MessageBox.Show(path, "Existiert nicht", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Shell32.Shell shell = new Shell32.Shell();

                // this is NOT IDynamicMetaObjectProvider and NOT DynamicObject !
                // this is NOT IShellDispatch...IShellDispatch6
                dynamic windows = shell.Windows();
                int cnt = windows.Count;
                logger.Debug($"cnt: {cnt}");
                for (int i = 0; i < cnt; i++)
                {
                    // this is NOT IDynamicMetaObjectProvider and NOT DynamicObject !
                    // this is NOT IShellDispatch...IShellDispatch6
                    object o = windows[i];
                    // https://www.add-in-express.com/creating-addins-blog/2011/12/20/type-name-system-comobject/
                    IntPtr idispatch = Marshal.GetIDispatchForObject(o);
                    IntPtr iunknown = Marshal.GetIUnknownForObject(o);
                    logger.Debug($"{i}: {o}, IsComObject: {Marshal.IsComObject(o)}, idispatch: {idispatch}, iunknown: {iunknown} ");
                }
                if (cnt == 0)
                {
                    var application = shell.Application;
                    application.Explore(path);
                }
                else
                {
                    dynamic windows1 = windows[0];
                    windows1.Navigate(path);

                    IntPtr hwnd = (IntPtr) windows1.HWND;

                    WinApp.SetForegroundWindow(hwnd);
                    WinApp.ShowWindow(hwnd, 9);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// https://stackoverflow.com/questions/8068449/calling-a-member-of-idispatch-com-interface-from-c-sharp
        ///
        /// </summary>
        public static void GetIDispatchFromObject()
        {
            Type shellApplicationType = Type.GetTypeFromProgID("Shell.Application");
            // returns 11
            MemberInfo[] memberInfos = shellApplicationType.GetMembers((BindingFlags) 0xFF);
            object shellApplication = Activator.CreateInstance(shellApplicationType);
            bool b1 = Marshal.IsComObject(shellApplication);
            bool b2 = shellApplication is IDynamicMetaObjectProvider;
            bool b3 = shellApplication is IDispatch;
            IDispatch dispatch = shellApplication as IDispatch;
            IShellDispatch6 shellDispatch6 = shellApplication as IShellDispatch6;
            IShellDispatch6_2 shellDispatch6_2 = shellApplication as IShellDispatch6_2;
            /*
Managed Debugging Assistant 'InvalidVariant' : 'An invalid VARIANT was detected during a conversion from an unmanaged VARIANT to a managed object. Passing invalid VARIANTs to the CLR can cause unexpected exceptions, corruption or data loss.'
             */
            // dynamic windows = shellDispatch6_2.Windows();
            /*
System.AccessViolationException: 'Attempted to read or write protected memory. This is often an indication that other memory is corrupt.'
             */
            // shellDispatch6_2.ToggleDesktop();
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("00020400-0000-0000-C000-000000000046")]
        private interface IDispatch
        {
            int GetTypeInfoCount();
            [return: MarshalAs(UnmanagedType.Interface)]
            ITypeInfo GetTypeInfo([In, MarshalAs(UnmanagedType.U4)] int iTInfo, [In, MarshalAs(UnmanagedType.U4)] int lcid);
            void GetIDsOfNames([In] ref Guid riid, [In, MarshalAs(UnmanagedType.LPArray)] string[] rgszNames, [In, MarshalAs(UnmanagedType.U4)] int cNames, [In, MarshalAs(UnmanagedType.U4)] int lcid, [Out, MarshalAs(UnmanagedType.LPArray)] int[] rgDispId);
        }

        [Guid("286E6F1B-7113-4355-9562-96B7E9D64C54")]
        [TypeLibType(4176)]
        private interface IShellDispatch6_2
        {
            [DispId(1610743812)]
            dynamic Windows();

            [DispId(1610940417)]
            void ToggleDesktop();
        }

        public static void WithPowerShellScript(string path)
        {
            logger.Debug("WithPowerShellScript, begin");
            logger.Debug(path);
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
