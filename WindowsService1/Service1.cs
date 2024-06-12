using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        private Process _wpfProcess;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Chemin vers votre application WPF
            string pathToExe = @"D:\Bureau\dotnet\platapp\BlazorApp1\bin\Debug\net6.0\BlazorApp1.exe";
            Process.Start(pathToExe);
        }

        protected override void OnStop()
        {
            // Terminer le processus de l'application WPF
            if (_wpfProcess != null && !_wpfProcess.HasExited)
            {
                _wpfProcess.Kill();
                _wpfProcess.WaitForExit();
            }
        }
    }
}
