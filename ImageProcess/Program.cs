using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
namespace ImageProcess
{
  
    static class Program
    {
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Process instance = RunningInstance();
                if (instance == null)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FrmProcess());
                }
                else
                {
                    HandleRunningInstance(instance);
                }
               

            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                MessageBox.Show(str);
            }
        }
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //Loop   through   the   running   processes   in   with   the   same   name
            foreach (Process process in processes)
            {
                //Ignore   the   current   process
                if (process.Id != current.Id)
                {
                    //Make   sure   that   the   process   is   running   from   the   exe   file.
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") ==
                        current.MainModule.FileName)
                    {
                        //Return   the   other   process   instance.
                        return process;
                    }
                }
            }

            //No   other   instance   was   found,   return   null.
            return null;
        }
        public static void HandleRunningInstance(Process instance)
        {
            //Make   sure   the   window   is   not   minimized   or   maximized
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);

            //Set   the   real   intance   to   foreground   window
            SetForegroundWindow(instance.MainWindowHandle);
        }

    }
}