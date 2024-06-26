﻿using System;
using System.Threading;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    internal static class Program
    {
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("Only one instance of this application is allowed to run.");
            }
        }
    }
}
