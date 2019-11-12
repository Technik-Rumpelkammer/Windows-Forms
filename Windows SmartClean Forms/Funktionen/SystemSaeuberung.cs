using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Windows_SmartClean.Funktionen
{
    class SystemSaeuberung
    {
        Funktionen.lesen_und_schreiben ls = new lesen_und_schreiben();
        string Benutzer = "";

        enum RecycleFlags : uint
        {
            SHRB_NOCONFIRMATION = 0x00000001,
            SHRB_NOPROGRESSUI = 0x00000002,
            SHRB_NOSOUND = 0x00000004
        }
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern int SHQueryRecycleBin(string pszRootPath, ref SHQUERYRBINFO pSHQueryRBInfo);

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct SHQUERYRBINFO
        {
            public int cbSize;
            public long i64Size;
            public long i64NumItems;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //
        //  Funktion, zum Finden überflüssiger Ordner auf Datenträger C
        //
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public string Setze_Benutzer
        {
            get => Benutzer;
            set => Benutzer = value;
        }

        public List<string> Suche_Ordner_Auf_C()
        {
            List<string> L_Ordner = new List<string>();
            try
            {
                string[] s_Vz_in_C = Directory.GetDirectories("C:\\");
                foreach (string s_Vz in s_Vz_in_C)
                {
                    string s_T = s_Vz.Substring(s_Vz.IndexOf("\\") + 1);
                    List<string> l_unnoetige_Ordner = ls.Lese_Konfigdatei("ueberfluessige Ordner.txt");
                    foreach(string s in l_unnoetige_Ordner)
                        if (s_T == s)
                            L_Ordner.Add(s_T);
                }
                return L_Ordner;
            }
            catch(Exception e_Suche_Ordner_Auf_C)
            {
                ls.Erstelle_Fehlerbericht(Benutzer, "SystemSaeuberung.cs", "Suche_Ordner_Auf_C", e_Suche_Ordner_Auf_C.ToString(), DateTime.Now.ToString());
                return L_Ordner;
            }
        }

        public int Ermittle_Dateien_In_Papierkorb(string _Benutzer)
        {
            string SID = ls.Hole_SID(Benutzer);
            int dateizaehler = 0; long Groesse = 0;
            DirectoryInfo di = new DirectoryInfo(@"C:\$Recycle.bin\" + SID); dateizaehler = 0; Groesse = ls.Ermittle_Verzeichnisgroesse(Environment.ExpandEnvironmentVariables(" %TMP%")) / 1024;
            foreach (FileInfo file in di.GetFiles())
                dateizaehler++;

            return dateizaehler;
        }

        public bool Loesche_Dateien_In_Papierkorb(string _Benutzer)
        {
            bool Ergebnis = false;
            try
            {
                string SID = ls.Hole_SID(Benutzer);
                int dateizaehler = 0; long Groesse = 0;
                DirectoryInfo di = new DirectoryInfo(@"C:\$Recycle.bin\" + SID); dateizaehler = 0; Groesse = ls.Ermittle_Verzeichnisgroesse(@"C:\$Recycle.bin\" + SID) / 1024;
                foreach (FileInfo file in di.GetFiles())
                    dateizaehler++;

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = @"/c del /s /f /q C:\$Recycle.bin\" + SID;
                startInfo.UseShellExecute = true;
                process.StartInfo = startInfo;
                process.Start();
                ls.Erstelle_ErgebnisBericht(Benutzer, @"Loesche_Dateien_In_Papierkorb_Von_" + _Benutzer, DateTime.Now.ToString(), Groesse.ToString(), dateizaehler.ToString());
                Ergebnis = true;
            }
            catch (Exception ex)
            {
                ls.Erstelle_Fehlerbericht(Benutzer, "SystemSaeuberung.cs", "Loesche_Dateien_In_Papierkorb", ex.ToString(), DateTime.Now.ToString());
                Ergebnis = false;
            }

            return Ergebnis;
        }

        public bool Loesche_Tmp_Benutzer_Daten(string _Benutzer)
        {
            try
            {
                int dateizaehler = 0; long Groesse = 0;
                DirectoryInfo di = new DirectoryInfo(@"C:\Users\" + _Benutzer + @"\AppData\Local\Temp"); dateizaehler = 0; Groesse = ls.Ermittle_Verzeichnisgroesse(Environment.ExpandEnvironmentVariables("%TMP%")) / 1024;
                foreach (FileInfo file in di.GetFiles())
                    dateizaehler++;

                foreach (DirectoryInfo dir in di.GetDirectories())
                    dateizaehler++;

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = @"/c del /s /f /q C:\Users\" + _Benutzer + @"\AppData\Local\Temp";
                startInfo.UseShellExecute = true;
                process.StartInfo = startInfo;
                process.Start();

                ls.Erstelle_ErgebnisBericht(Benutzer, @"T1_Ordner_Entfernen_C:\Users\" + _Benutzer + @"\AppData\Local\Temp", DateTime.Now.ToString(), Groesse.ToString(), dateizaehler.ToString());
                return true;
            }
            catch(Exception e_Loesche_Tmp_Benutzer_Daten)
            {
                ls.Erstelle_Fehlerbericht(Benutzer, "SystemSaeuberung.cs", "Loesche_Tmp_Benutzer_Daten", e_Loesche_Tmp_Benutzer_Daten.ToString(), DateTime.Now.ToString());
                return false;
            }
        }   //  Ende Funktion Loesche_Tmp_Benutzer_Daten

        public bool Loesche_Windows_Tmp()
        {
            try
            {
                int dateizaehler = 0; long Groesse = 0;
                Groesse = ls.Ermittle_Verzeichnisgroesse(Environment.ExpandEnvironmentVariables("%SYSTEMROOT%\\Temp")) / 1024;

                DirectoryInfo di2 = new DirectoryInfo(Environment.ExpandEnvironmentVariables("%SYSTEMROOT%\\Temp"));
                foreach (FileInfo file in di2.GetFiles())
                    dateizaehler++;

                foreach (DirectoryInfo dir in di2.GetDirectories())
                    dateizaehler++;

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/c del /s /f /q \"%SYSTEMROOT%\\Temp\\*.*\"";
                startInfo.UseShellExecute = true;
                process.StartInfo = startInfo;
                process.Start();

                ls.Erstelle_ErgebnisBericht(Benutzer, "T1_Ordner_Entfernen_" + Environment.ExpandEnvironmentVariables("%SYSTEMROOT%\\Temp"), DateTime.Now.ToString(), Groesse.ToString(), dateizaehler.ToString());
                return true;
            }
            catch (Exception e_Loesche_Windows_Tmp) {
                ls.Erstelle_Fehlerbericht(Benutzer, "SystemSaeuberung.cs", "Loesche_Windows_Tmp", e_Loesche_Windows_Tmp.ToString(), DateTime.Now.ToString());
                return false;
            }
        }   //  Ende Funktion Loesche_Windows_Tmp
    }
}
