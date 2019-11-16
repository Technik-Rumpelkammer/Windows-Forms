using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Task = Microsoft.Win32.TaskScheduler.Task;

namespace Windows_SmartClean.Funktionen
{
    class Automatisches_Optimieren
    {
        Funktionen.lesen_und_schreiben ls = new lesen_und_schreiben();

        bool Task_Bereits_Vorhanden = false; bool Aufgabe_Gefunden = false;
        string Benutzer = "";
        string Admin_User = "";

        public string Setze_Benutzer
        {
            get => Benutzer;
            set => Benutzer = value;
        }

        public string Setze_Admin_Benutzer
        {
            get => Admin_User;
            set => Admin_User = value;
        }
        public bool Ist_Task_Vorhanden()
        {
            Pruefe_Ob_Task_Vorhanden_Ist();
            return Task_Bereits_Vorhanden;
        }

        public bool Loesche_Aufgabe()
        {
            try
            {
                using (TaskService ts = new TaskService())
                {
                    ts.RootFolder.DeleteTask("Windows SmartClean");
                    Task_Bereits_Vorhanden = false;
                    return true;
                }
            }catch(Exception e_Loesche_Aufgabe)
            {
                Funktionen.lesen_und_schreiben ls = new lesen_und_schreiben();
                ls.Erstelle_Fehlerbericht(Benutzer, "Automatisches_Optimieren.cs", "Loesche_Aufgabe", e_Loesche_Aufgabe.ToString(), DateTime.Now.ToString());
                return false;
            }
        }

        public void Pruefe_Ob_Task_Vorhanden_Ist ()
        {
            using (TaskService ts = new TaskService())
                Kontrolliere_Jeden_AufgabenOrdner(ts.RootFolder);
        }

        void Kontrolliere_Jeden_AufgabenOrdner(TaskFolder fld)
        {
            int zaehler = fld.Tasks.Count; int i = 0;
            foreach (Task task in fld.Tasks)
            {
                if (task.Name == "Windows SmartClean")
                { Task_Bereits_Vorhanden = true; Aufgabe_Gefunden = true; }

                if(zaehler == i && Aufgabe_Gefunden == false)
                    Task_Bereits_Vorhanden = false;
            }
            foreach (TaskFolder sfld in fld.SubFolders)
                Kontrolliere_Jeden_AufgabenOrdner(sfld);
            Aufgabe_Gefunden = false;
        }

        /// <summary>
        /// Mit dieser Funktion wie die Aufgabe in Windows erstellt und registriert
        /// </summary>
        /// <param name="_Benutzer">Der Benutzer, für welchen die Aufgabe laufen soll</param>
        /// <param name="_SID">Die Sicherheitsnummer dieses Benutzers</param>
        /// <param name="_Startdatum">An welchem Tag die erste Durchführung sein soll</param>
        /// <param name="_Startzeit">Um welche Uhrzeit die Durchführung passieren soll</param>
        /// <param name="_Interval">Wie oft die Aufgabe ausgeführt wird (Tage)</param>
        /// <param name="_Pfad">Pfad zur Datei</param>
        public bool Erstelle_Aufgabe(string _Ang_Benutzer, string _Admin_Benutzer, string _SID, string _Startdatum, string _Startzeit, short _Interval, string _Pfad, List<string> _Optionen, System.Security.SecureString _Passwort)
        {
            try
            {
                // Get the service on the local machine
                using (TaskService ts = new TaskService())
                {
                    // Create a new task definition and assign properties
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Author = "BaBa";
                    td.RegistrationInfo.Description = "Optimiert das System im Hintergrund";
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Principal.Id = Admin_User;
                    td.Principal.UserId = _SID;
                    td.Settings.DisallowStartIfOnBatteries = false;
                    td.Settings.StopIfGoingOnBatteries = false;
                    td.Settings.Priority = System.Diagnostics.ProcessPriorityClass.High;
                    td.Settings.ExecutionTimeLimit = TimeSpan.FromMinutes(5);

                    DailyTrigger daily = new DailyTrigger();
                    daily.StartBoundary = Convert.ToDateTime(_Startdatum.Substring(0, _Startdatum.IndexOf(" ") + 1) + " " + _Startzeit);
                    daily.DaysInterval = _Interval;

                    td.Triggers.Add(daily);

                    LogonTrigger lot = new LogonTrigger();
                    lot.Enabled = true;
                    lot.UserId = _Ang_Benutzer;
                    lot.Delay = System.TimeSpan.FromMinutes(1);
                    td.Triggers.Add(lot);

                    td.Actions.Add(new ExecAction(@"C:\Users\BaBa\source\repos\Windows SmartClean Forms\Windows SmartClean Forms\bin\Debug\Windows SmartClean Forms.exe", "1 " + _Ang_Benutzer, null));

                    string contents = null;
                    if (_Passwort != null)
                    {
                        IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(_Passwort);
                        contents = System.Runtime.InteropServices.Marshal.PtrToStringAuto(ptr);
                    }

                    ts.RootFolder.RegisterTaskDefinition(@"Windows SmartClean", td, TaskCreation.Create, _Admin_Benutzer, contents, TaskLogonType.InteractiveTokenOrPassword);

                    if (_Optionen.Count > 0)
                        ls.Schreibe_Konfigdatei("Minimiert.txt", _Optionen);
                    else
                        return false;

                    return true;
                }
            }
            catch (Exception e_Erstelle_Aufgabe)
            {
                Funktionen.lesen_und_schreiben ls = new lesen_und_schreiben();
                ls.Erstelle_Fehlerbericht(Benutzer, "Automatisches_Optimieren.cs", "Erstelle_Aufgabe", e_Erstelle_Aufgabe.ToString(), DateTime.Now.ToString());
                return false;
            }
        }   //  Ende Funktion Erstelle_Aufgabe

        public string Zaehle_Prozesse()
        {
            Process[] processes = Process.GetProcesses();
            return processes.Count().ToString();
        }
        public string CPUSpeed()
        {
            using (ManagementObject Mo = new ManagementObject("Win32_Processor.DeviceID='CPU0'"))
            {
                return (Mo["CurrentClockSpeed"]).ToString();
            }
        }
    }
}
