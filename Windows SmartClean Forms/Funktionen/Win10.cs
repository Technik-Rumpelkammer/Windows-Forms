using System;
using System.Collections.Generic;
using System.IO;
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

        public List<string> Hole_Startmenue_Apps()
        {
            List<string> Apps = new List<string>();
            if(Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Microsoft\Windows\Start Menu\Programs"))
            {
                DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Microsoft\Windows\Start Menu\Programs");
                foreach(var d in dir.GetDirectories())
                {
                    Apps.Add(d.Name);
                    foreach (var f in d.GetFiles())
                    {
                        string s = "--> " + f.Name;
                        Apps.Add(s);
                    }
                }
                foreach (var f in dir.GetFiles())
                {
                    string s = "--> " + f.Name;
                    Apps.Add(s);
                }
            }
            return Apps;
        }   //  Ende Methode Hole_Startmenue_Apps

        public void Win10_StandardApps_WiederHerstellen()
        {
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {

                PowerShellInstance.AddScript(@"Get-AppxPackage | foreach {Add-AppxPackage -register „$($_.InstallLocation)\appxmanifest.xml“ -DisableDevelopmentMod}");
                IAsyncResult result = PowerShellInstance.BeginInvoke();

                while (result.IsCompleted == false)
                {
                    Console.WriteLine("Stelle Standard her... Bitte warten");
                }

                Console.WriteLine("Standard wieder hergestellt!");
            }
        }

        public List<string> Hole_Win10_StandardApps()
        {
            List<string> Apps = new List<string>();
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                
                PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();

                PowerShellInstance.AddScript("Get-AppxPackage | Select Name");
                IAsyncResult result = PowerShellInstance.BeginInvoke<PSObject, PSObject>(null, outputCollection);

                while (result.IsCompleted == false)
                {
                    //Console.WriteLine("Hole StandardApps... Bitte warten");
                }

                foreach (PSObject outputItem in outputCollection)
                {
                    //TODO: handle/process the output items if required
                    string s = outputItem.ToString();
                    s = s.Substring(s.LastIndexOf('=') + 1); s = s.Substring(0, s.LastIndexOf('}'));
                    Apps.Add(s);
                }
            }
            return Apps;
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
        public string Entferne_Apps(List<string> Apps)
        {
            string Befehl = "Get-AppxPackage"; string ergebnis = "";
            foreach(string s in Apps)
                Befehl += " | where-object {$_.name -notlike \"*" + s + "*\"}";
            Befehl += " | Remove-AppxPackage";

            using (PowerShell PowerShellInstance = PowerShell.Create())
            {

                PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();

                PowerShellInstance.AddScript(Befehl);
                IAsyncResult result = PowerShellInstance.BeginInvoke<PSObject, PSObject>(null, outputCollection);

                while (result.IsCompleted == false)
                {
                    //Console.WriteLine("Hole StandardApps... Bitte warten");
                }

                foreach (PSObject outputItem in outputCollection)
                    ergebnis = outputItem.ToString();
            }   //  Ende using

            return ergebnis;
        }   //  Ende Methode Entferne_Apps
    }
}
