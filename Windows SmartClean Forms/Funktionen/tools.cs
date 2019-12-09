using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Windows_SmartClean_Forms.Funktionen
{
    class tools
    {
        public bool Deaktiviere_Superfetch()
        {
            bool Ergebnis = false;

            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();
                PowerShellInstance.AddScript("Stop-Service -Force -Name \"SysMain\"; Set-Service -Name \"SysMain\" -StartupType Disabled");
                IAsyncResult result = PowerShellInstance.BeginInvoke<PSObject, PSObject>(null, outputCollection);
                while (result.IsCompleted == false)
                {
                    //Console.WriteLine("Entferne StandardApps... Bitte warten");
                }
                Ergebnis = true;
            }   //  Ende using

            return Ergebnis;
        }
    }
}
