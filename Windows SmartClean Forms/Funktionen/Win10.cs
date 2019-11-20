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
                PowerShellInstance.AddScript("Get-AppxPackage -allusers | foreach {Add-AppxPackage -register \"$($_.InstallLocation)\\appxmanifest.xml\" -DisableDevelopmentMode}");
                IAsyncResult result = PowerShellInstance.BeginInvoke();

                while (result.IsCompleted == false)
                    Thread.Sleep(10);
            }
        }   //  Ende Methode Win10_StandardApps_WiederHerstellen

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
        public string Entferne_Apps(List<string> Apps)
        {
            string Befehl = ""; string ergebnis = ""; int zaehler = Apps.Count;
            if (zaehler == 1)
                Befehl = " Get-AppxPackage -AllUsers -name *" + Apps[0] + "* | Remove-AppxPackage";
            else if(zaehler > 1)
            {
                for (int i = 0; i < Apps.Count; i++)
                {
                    if (i < Apps.Count - 1)
                        Befehl += " Get-AppxPackage -AllUsers -name *" + Apps[i] + "* | Remove-AppxPackage ;";
                    else
                        Befehl += " Get-AppxPackage -AllUsers -name *" + Apps[i] + "* | Remove-AppxPackage";
                }
            }

            using (PowerShell PowerShellInstance = PowerShell.Create())
            {

                PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();

                PowerShellInstance.AddScript(Befehl);
                IAsyncResult result = PowerShellInstance.BeginInvoke<PSObject, PSObject>(null, outputCollection);
                while (result.IsCompleted == false)
                { }
                foreach (PSObject outputItem in outputCollection)
                    ergebnis = outputItem.ToString();
            }   //  Ende using

            return ergebnis;
        }   //  Ende Methode Entferne_Apps

        public void Entferne_Alle_Apps_Fuer_Benutzer(string _Benutzer)
        {
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {

                PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();

                PowerShellInstance.AddScript("Get-AppxPackage –User " + _Benutzer + " | Remove-AppxPackage");
                IAsyncResult result = PowerShellInstance.BeginInvoke<PSObject, PSObject>(null, outputCollection);
                while (result.IsCompleted == false)
                { }
            }   //  Ende using
        }   //  Ende Methode Entferne_Alle_Apps_Fuer_Benutzer

        public bool Laden_Sicherheitsscript_herunter()
        {
            bool Ergebnis = false;

            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();
                PowerShellInstance.AddScript(@"(New-Object Net.WebClient).DownloadString('https://raw.githubusercontent.com/hahndorf/Set-Privacy/master/Set-Privacy.ps1') | out-file -filepath " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\WindowsSmartClean\\Konfigurationen\\Set-Privacy.ps1 -force ");
                IAsyncResult result = PowerShellInstance.BeginInvoke<PSObject, PSObject>(null, outputCollection);
                while (result.IsCompleted == false)
                {
                    //Console.WriteLine("Entferne StandardApps... Bitte warten");
                }
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\WindowsSmartClean\\Konfigurationen\\Set - Privacy.ps1"))
                    Ergebnis = true;
            }   //  Ende using

            return Ergebnis;
        }   //  Ende Methode Laden_Sicherheitsscript_herunter

        public void Setze_Privatsphaere(string _Modi, bool _Admin)
        {
            string admin = "";
            if (_Admin)
                admin = " -admin";
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();
                PowerShellInstance.AddScript("Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process -Force; " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\WindowsSmartClean\\Konfigurationen\\Set-Privacy.ps1 -" + _Modi + admin);
                IAsyncResult result = PowerShellInstance.BeginInvoke<PSObject, PSObject>(null, outputCollection);
                while (result.IsCompleted == false)
                {
                    //Console.WriteLine("Entferne StandardApps... Bitte warten");
                }
            }
        }   //  Ende Methode Setze_Privatsphaere
    }
}
