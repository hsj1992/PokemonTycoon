﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonTycoon
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool newProcess;
            using (Mutex mutex = new Mutex(true, "PokemonTycoon", out newProcess))
            {
                if (newProcess)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
                else
                {
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            Utils.SendMessage(process.MainWindowHandle, Utils.WM_APP_Activate, IntPtr.Zero, IntPtr.Zero);
                            break;
                        }
                    }
                }
            }
        }
    }
}
