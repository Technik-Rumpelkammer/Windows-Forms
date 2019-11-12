using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Windows_SmartClean_Forms.Funktionen
{
    class Win10
    {
        public void Entferne_Win10_Sinnlos_apps()
        {
            Thread T_Win10Apps = new Thread(Entferne_Apps);
            T_Win10Apps.IsBackground = true;
            T_Win10Apps.Start();
        }

        void Entferne_Apps()
        {
            // Get-AppxPackage -AllUsers | where-object {$_.name –notlike "*store*"} | Remove-AppxPackage
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                // this script has a sleep in it to simulate a long running script
                PowerShellInstance.AddScript("Get-AppxPackage -AllUsers | where-object {$_.name –notlike \"* store*\"} | Remove-AppxPackage");

                // begin invoke execution on the pipeline
                IAsyncResult result = PowerShellInstance.BeginInvoke();

                // do something else until execution has completed.
                // this could be sleep/wait, or perhaps some other work
                
                while (result.IsCompleted == false)
                {
                    
                    Console.WriteLine("Waiting for pipeline to finish...");
                    Thread.Sleep(1000);

                    // might want to place a timeout here...
                }
                Console.WriteLine("Finished!");
            }
        }
    }
}
