using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Windows_SmartClean.Funktionen
{
    class lesen_und_schreiben
    {
        string Benutzer = "";
        string V_Wurzel = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\WindowsSmartClean";
        string V_Fehler_Log = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\WindowsSmartClean\\FehlerLog";
        string V_Config = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\WindowsSmartClean\\Konfigurationen";
        string V_Ergebnisse = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\WindowsSmartClean\\Ergebnisse";

        public string Setze_Benutzer
        {
            get => Benutzer;
            set => Benutzer = value;
        }

        public string Hole_SID(string _Benutzer)
        {
            NTAccount f = new NTAccount(_Benutzer);
            SecurityIdentifier s = (SecurityIdentifier)f.Translate(typeof(SecurityIdentifier));
            return s.ToString();
        }

        public void Richte_Verzeichnise_Ein()
        {    
            if (!Directory.Exists(V_Wurzel))
                Directory.CreateDirectory(V_Wurzel);
            if (!Directory.Exists(V_Fehler_Log))
                Directory.CreateDirectory(V_Fehler_Log);
            if (!Directory.Exists(V_Config))
                Directory.CreateDirectory(V_Config);
            if (!Directory.Exists(V_Ergebnisse))
                Directory.CreateDirectory(V_Ergebnisse);
            if (!File.Exists(V_Config + "\\ueberfluessige Ordner.txt"))
                using(StreamWriter schreiber = new StreamWriter(V_Config + "\\ueberfluessige Ordner.txt"))
                {
                    schreiber.Write("Intel\nAMD\nNvidia\nPerfLog\nEpOutput");
                }
            if (!File.Exists(V_Config + "\\Minimiert.txt"))
                File.Create(V_Config + "\\Minimiert.txt");
            if (!File.Exists(V_Config + "\\Win10_Std_Apps_Vergleich.txt"))
                using (StreamWriter schreiber = new StreamWriter(V_Config + "\\Win10_Std_Apps_Vergleich.txt"))
                {
                    schreiber.Write("3dbuilder\n3d\n\nappconnector\nappinstaller\nbing\nbingnews\nbingsports\nBingWeather\nBrokerPlugin\nbingfinance\nconnectivitystore\ncommunicationsapps\nCortana\ncamera\nfeedback\nGetHelp\ngetstarted\nmessaging\nMicrosoftOfficeHub\nMicrosoftStickyNotes\nMSPaint\nMicrosoft3DViewer\nMicrosoftSolitaireCollection\nOneConnect\nPhotos\nPrint3D\nPeople\nskypeapp\nsway\nsoundrecorder\nwallet\nWindowsFeedbackHub\nwindowsalarms\nWindowsCamera\nwindowsmaps\nwindowscalculator\nXboxApp\nXboxGamingOverlay\nYourPhone\nzunevideo\nzunemusic\nzune");
                }
            if (!File.Exists(V_Ergebnisse + "\\Datenzaehler.txt"))
                File.Create(V_Ergebnisse + "\\Datenzaehler.txt");
        }

        public int Ermittel_Dateien_in_Verzeichnis(string _Verzeichnis)
        {
            int zaehler = 0;
            if (Directory.Exists(_Verzeichnis))
            {
                DirectoryInfo di = new DirectoryInfo(_Verzeichnis);
                foreach (FileInfo file in di.GetFiles())
                    zaehler++;
            }
            return zaehler;
        }

        public long Ermittle_Verzeichnisgroesse(string _Verzeichnis)
        {
            string[] a = Directory.GetFiles(_Verzeichnis, "*.*");
            long b = 0;
            foreach (string name in a)
            {
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            return b;
        }

        public float Lese_Datenzaehler()
        {
            float zahl = 0; float ergebnis = 0;
            try
            {
                using(StreamReader reader = new StreamReader(V_Ergebnisse + "\\Datenzaehler.txt"))
                {
                    string zeile;
                    while ((zeile = reader.ReadLine()) != null)
                    {
                        if (zeile.Contains("#"))
                        {
                            zeile = zeile.Substring(zeile.IndexOf('#') + 2);
                            zeile = zeile.Substring(zeile.IndexOf(':') + 2);
                            zeile = zeile.Substring(0, zeile.IndexOf('|'));
                            float.TryParse(zeile, out zahl);
                            ergebnis += zahl;
                        }
                    }
                    return ((ergebnis / 1024) / 1024);
                }
            }
            catch(Exception e_Lese_Datenzaehler)
            {
                Erstelle_Fehlerbericht(Benutzer, "lesen_und_schreiben.cs", "Lese_Datenzaehler", e_Lese_Datenzaehler.ToString(), DateTime.Now.ToString());
                return ergebnis;
            }
        }

        public List<string> Lese_Konfigdatei(string _Konfigdatei)
        {
            List<string> l_Ordner = new List<string>();

            if (File.Exists(V_Config + "\\" + _Konfigdatei))
            {
                using(StreamReader lesen = new StreamReader(V_Config + "\\" + _Konfigdatei))
                {
                    while (true)
                    {
                        string zeile = lesen.ReadLine();
                        if (zeile == null)
                            break;
                        l_Ordner.Add(zeile);
                    }
                }   //  Ende using
            }   //  Ende if
            return l_Ordner;
        }   //  Ende Funktion Lese_Konfig_Unnoetige_Ordner

        public void Schreibe_Konfigdatei(string _Konfigdatei, List<string> _Optionen)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(V_Config + "\\" + _Konfigdatei))
                {
                    foreach (string s in _Optionen)
                    {
                        writer.WriteLine(s);
                    }   //  Ende foreach
                }   //  Ende using
            }   //  Ende try
            catch(Exception e_Schreibe_Konfigdatei)
            {
                Erstelle_Fehlerbericht(Benutzer, "lesen_und_schreiben.cs", "Schreibe_Konfigdatei", e_Schreibe_Konfigdatei.ToString(), DateTime.Now.ToString());
            }
        }

        public bool Pruefe_Ob_Konfig_Vorhanden(string _Datei)
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\WindowsSmartClean\\Konfigurationen\\" + _Datei))
                return true;
            else
                return false;
        }

        public void Erstelle_ErgebnisBericht(string _Benutzer, string _Funktion, string _Datum, string _Groesse, string _Anzahl_Dateien)
        {
            using (StreamWriter outputFile = new StreamWriter(V_Ergebnisse + "\\Datenzaehler.txt", true))
            {
                outputFile.WriteLine("Ergebnis von: " + _Benutzer + " | in Funktion: " + _Funktion + " | am: " + _Datum + " # Größe: " + _Groesse + "| Anzahl gelöschter Dateien und Ordner: " + _Anzahl_Dateien);
            }
        }
        public void Erstelle_Fehlerbericht(string _Benutzer, string _Bereich, string _Funktion, string _Fehler, string _Datum)
        {
            using (StreamWriter outputFile = new StreamWriter(V_Fehler_Log + "\\" + _Bereich + ".txt", true))
            {
                outputFile.WriteLine("Fehler bei Benutzer: " + _Benutzer + "\t| in Funktion: " + _Funktion + " | Fehler: " + _Fehler + " | am: " + _Datum);
            }
        }
    }
}
