using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Windows_SmartClean_Forms
{
    public partial class HauptFenster : Form
    {
        readonly bool Minimiert = false;
        bool Stoppe_Threads = false;
        string Benutzer = Environment.UserName;
        string Benutzer_SID = "";
        string Angem_Benutzer = "";
        string Angem_Benutzer_SID = "";
        List<string> L_Benutzer;

        Thread T_Prozesse;
        Thread T_Hole_Win10_Stnd_Apps;

        Windows_SmartClean.Funktionen.Benutzerverwaltung bv = new Windows_SmartClean.Funktionen.Benutzerverwaltung();
        Windows_SmartClean.Funktionen.lesen_und_schreiben ls = new Windows_SmartClean.Funktionen.lesen_und_schreiben();
        Windows_SmartClean.Funktionen.SystemSaeuberung sauber = new Windows_SmartClean.Funktionen.SystemSaeuberung();
        Windows_SmartClean.Funktionen.Automatisches_Optimieren ao = new Windows_SmartClean.Funktionen.Automatisches_Optimieren();
        Windows_SmartClean_Forms.Funktionen.Win10 win10 = new Windows_SmartClean_Forms.Funktionen.Win10();

        List<string> Gew_Gruppen = new List<string>();

        public HauptFenster()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Count() == 2)
            {
                if (args[0] == "1")
                {
                    Minimiert = true;
                    Benutzer = args[1];
                }
            }
            else if (args.Count() == 3)
            {
                if (args[1] == "1")
                {
                    Minimiert = true;
                    Benutzer = args[2];
                }
            }
            else
                Angem_Benutzer = Benutzer;
            StartFunktionen();
        }

        void StartFunktionen()
        {
            aktualisiere_Benutzer_Und_Gruppen_in_Tree();
            T1_Suche_Nach_Ordnern_Auf_C();
            T1_Pruefe_Papierkorb();
            if (Minimiert)
                WSC_Minimiert();
            else
            {
                Einzelne_Startaufgaben();
                T_Prozesse = new Thread(aktualisiere_Prozesse);
                T_Prozesse.IsBackground = true;
                T_Prozesse.Start();
                T2_Hole_Startmenu_Apps();
                T_Hole_Win10_Stnd_Apps = new Thread(Hole_Win10_Stand_Apps);
                T_Hole_Win10_Stnd_Apps.IsBackground = true;
                T_Hole_Win10_Stnd_Apps.Start();
                T1_lbl_Gew_Speicherplatz.Text = ls.Lese_Datenzaehler().ToString("0.00") + " GB";
            }
        }

        void Hole_Win10_Stand_Apps()
        {
            List<string> Apps = win10.Hole_Win10_StandardApps(); int i = 0;
            if(Apps.Count > 0)
            {
                T2_Win10_checkedListBox_StandardApps.Items.Clear();
                List<string> Config_Apps = ls.Lese_Konfigdatei("Win10_Std_Apps_Vergleich.txt");
                foreach (string s in Apps)
                {
                    string ss = s.ToUpper(); string sss = s;
                    if (ss.Contains("MICROSOFT.WINDOWS.") || ss.Contains("WINDOWS.") || ss.Contains("MICROSOFT."))
                        ss = ss.Substring(ss.LastIndexOf('.') + 1);
                    Console.WriteLine("SS wird ausgegeben: " + ss + "\ns wird ausgegeben: " + s);
                    if (ss.Length > 2)
                        foreach (string g in Config_Apps)
                            if (g.ToUpper() == ss && !g.ToUpper().Contains("#") && !T2_Win10_checkedListBox_StandardApps.Items.Contains(ss))
                            { T2_Win10_checkedListBox_StandardApps.Items.Add(sss); i++;
                                //Console.WriteLine("ss: " + ss + "\ng: " + g);
                            }
                }
                T2_lbl_Windows_Std_Apps.Text = "Windows 10 Standard-Apps: " + i;
            }
        }   //  Ende Methode Hole_Win10_Stand_Apps

        void aktualisiere_Prozesse()
        {
            Thread.Sleep(1000);
            while (!Stoppe_Threads)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        T1_lbl_AnzahL_Prozesse.Text = ao.Zaehle_Prozesse();
                    });
                }
                else
                    T1_lbl_AnzahL_Prozesse.Text = ao.Zaehle_Prozesse();
                Thread.Sleep(500);
            }
        }

        void Einzelne_Startaufgaben()
        {
            ls.Richte_Verzeichnise_Ein();
            Gew_Gruppen.Add("Administratoren"); Gew_Gruppen.Add("Benutzer"); Gew_Gruppen.Add("HomeUsers"); Gew_Gruppen.Add("");
            T1_lbl_ausfrd_Benutzer.Text = bv.Hole_Ausfuehrenden_Benutzer();
            T1_lbl_Angem_Benutzer.Text = bv.Hole_Angemeldeten_Benutzer(); Benutzer = T1_lbl_Angem_Benutzer.Text;
            sauber.Setze_Benutzer = Benutzer;
            ls.Setze_Benutzer = Benutzer; Benutzer_SID = ls.Hole_SID(Benutzer);
            ao.Setze_Benutzer = Benutzer;
            ao.Setze_Admin_Benutzer = Benutzer;
            T1_lbl_Anzahl_Tmp_Dateien_Benutzer.Text = ls.Ermittel_Dateien_in_Verzeichnis("C:\\Users\\" + Benutzer + "\\AppData\\Local\\Temp\\").ToString();
            int a = ls.Ermittel_Dateien_in_Verzeichnis(Environment.ExpandEnvironmentVariables("%SYSTEMROOT%\\Temp")); a += ls.Ermittel_Dateien_in_Verzeichnis(Environment.ExpandEnvironmentVariables("%TMP%"));
            T1_lbl_Anzahl_Tmp_Dateien_Windows.Text = a.ToString();
            L_Benutzer = bv.Hole_Alle_Benutzer(1);
            if (L_Benutzer.Count > 0)
            {
                foreach (string s in L_Benutzer)
                    AO_comboBox_Benutzer.Items.Add(s);
                AO_comboBox_Benutzer.SelectedIndex = 0;
            }
            T1_AO_dateTimePicker_Startzeit.Format = DateTimePickerFormat.Custom;
            T1_AO_dateTimePicker_Startzeit.CustomFormat = "HH:mm";
            T1_AO_dateTimePicker_Startzeit.ShowUpDown = true;
            DateTime date = new DateTime(2015, 02, 19, 20, 0, 0);
            T1_AO_dateTimePicker_Startzeit.Value = date;
            if (ao.Ist_Task_Vorhanden())
            { 
                T1_pic_AO_Status.Image = Windows_SmartClean_Forms.Properties.Resources.task_v;
                T1_lbl_AO_laeuft_O.Text = "Es läuft eine";
            }
            else
            { T1_pic_AO_Status.Image = Windows_SmartClean_Forms.Properties.Resources.task_nv;
                T1_lbl_AO_laeuft_O.Text = "Es läuft keine";
            }
            
        }


        void WSC_Minimiert()
        {
            List<string> Konfig = ls.Lese_Konfigdatei("Minimiert.txt");
            if (Konfig.Count > 0)
            {
                foreach (string s in Konfig)
                {
                    if (s == "Unnoetige_Ordner:True")
                        T1_Ordner_Entfernen();
                    if (s == "Papierkorb_Leeren:True")
                        T1_Leere_Papierkorb();
                    if (s == "Temp_Benutzer:True")
                        T1_Loesche_Tmp_Benutzerdaten();
                    if (s == "Temp_Windows:True")
                        T1_Loesche_Windows_Tmp();
                }   //  Ende foreach
            }   //  Ende if, beinhaltet die Liste Elemente
            System.Environment.Exit(0);
        }   //  Ende Funktion WSC_Minimiert  

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //
        //  ´Tab 1, System aufräumen
        //
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        void T1_Pruefe_Papierkorb()
        {
            int zaehler = sauber.Ermittle_Dateien_In_Papierkorb(Benutzer);
            T1_lbl_Dateien_Papierkorb.Text = zaehler.ToString();
        }
        private void T1_btn_Loesche_Daten_Papierkorb_Click(object sender, RoutedEventArgs e)
        {
            T1_Leere_Papierkorb();
        }

        void T1_Leere_Papierkorb()
        {
            if (Minimiert)
                sauber.Loesche_Dateien_In_Papierkorb(Benutzer);
            else
            {
                DialogResult Abfrage = MessageBox.Show("Sicher, dass der Papierkorb von " + Benutzer + " geleert werden soll?", "Papierkorb leeren", MessageBoxButtons.YesNo);
                if (Abfrage == DialogResult.Yes)
                {
                    if (sauber.Loesche_Dateien_In_Papierkorb(Benutzer))
                        MessageBox.Show("Der Papierkorb von " + Benutzer + " wurde erfolgreich geleert!", "Erfolgreich geleert!");
                    T1_Pruefe_Papierkorb();
                }
            }
            T1_lbl_Anzahl_Tmp_Dateien_Benutzer.Text = ls.Ermittel_Dateien_in_Verzeichnis("C:\\Users\\" + Benutzer + "\\AppData\\Local\\Temp\\").ToString();
        }

        void T1_Suche_Nach_Ordnern_Auf_C()
        {
            T1_checkedListBox_Ordnern.Items.Clear();
            List<string> Ordner = sauber.Suche_Ordner_Auf_C();
            if (Ordner.Count > 0)
                foreach (string s in Ordner)
                    T1_checkedListBox_Ordnern.Items.Add(s);
            else
                T1_checkedListBox_Ordnern.Items.Add("Keine Ordner gefunden!");
        }

        void T1_Ordner_Entfernen()
        {
            int dateizaehler = 0; int g_dateizaehler = 0; long Groesse = 0; long g_Groesse = 0;
            if (!Minimiert)
            {
                string gel_Elemente = "";
                try
                {
                   if(!(T1_checkedListBox_Ordnern.SelectedItem.ToString() == "Keine Ordner gefunden!"))
                    {
                        DialogResult Abfrage = MessageBox.Show("Sicher, dass diese Ordner gelöscht werden sollen?", "Ordner löschen", MessageBoxButtons.YesNo);
                        if (Abfrage == DialogResult.Yes)
                        {
                            foreach (string s in T1_checkedListBox_Ordnern.SelectedItems)
                            {
                                if (Directory.Exists("C:\\" + s))
                                {
                                    DirectoryInfo di = new DirectoryInfo("C:\\" + s); dateizaehler = 0; Groesse = ls.Ermittle_Verzeichnisgroesse("C:\\" + s) / 1024; g_Groesse += Groesse;
                                    foreach (FileInfo file in di.GetFiles())
                                    {
                                        dateizaehler++;
                                        file.Delete();
                                    }
                                    foreach (DirectoryInfo dir in di.GetDirectories())
                                    {
                                        dir.Delete(true);
                                        dateizaehler++;
                                    }
                                    g_dateizaehler += dateizaehler;
                                    ls.Erstelle_ErgebnisBericht(Benutzer, "T1_Ordner_Entfernen_" + s, DateTime.Now.ToString(), Groesse.ToString(), dateizaehler.ToString());
                                    di.Delete();
                                    gel_Elemente += s + "\n";
                                }   //  Ende if
                            }   //  Ende foreach
                            T1_Suche_Nach_Ordnern_Auf_C();
                            MessageBox.Show("Es wurden folgende Elemente entfernt:\n\n" + gel_Elemente + "\n\nInsgesamt wurden: " + g_dateizaehler + " Ordner und Dateien,\nmit einer Gesamtgröße von: " + (g_Groesse / 1024).ToString() + " MB");
                        }
                    }
                }
                catch (Exception e_Ordner_Entfernen)
                {
                    ls.Erstelle_Fehlerbericht(Benutzer, "MainWindows.cs", "Button_Ordner_Entfernen", e_Ordner_Entfernen.ToString(), DateTime.Now.ToString());
                    MessageBox.Show("Fehler beim entfernen eines Ordners.\nDie genaue Fehlermeldung lautet:\n\n" + e_Ordner_Entfernen);
                }   //  Ende catch
            }
            else
            {
                string gel_Elemente = "";
                try
                {
                    List<string> Liste = ls.Lese_Konfigdatei("ueberfluessige Ordner.txt");
                    foreach (string s in Liste)
                    {
                        if (Directory.Exists("C:\\" + s))
                        {
                            DirectoryInfo di = new DirectoryInfo("C:\\" + s); dateizaehler = 0; Groesse = ls.Ermittle_Verzeichnisgroesse("C:\\" + s) / 1024; g_Groesse += Groesse;
                            foreach (FileInfo file in di.GetFiles())
                            {
                                dateizaehler++;
                                file.Delete();
                            }
                            foreach (DirectoryInfo dir in di.GetDirectories())
                            {
                                dir.Delete(true);
                                dateizaehler++;
                            }
                            g_dateizaehler += dateizaehler;
                            ls.Erstelle_ErgebnisBericht(Benutzer, "T1_Ordner_Entfernen_" + s, DateTime.Now.ToString(), Groesse.ToString(), dateizaehler.ToString());
                            di.Delete();
                            gel_Elemente += s + "\n";
                        }   //  Ende if
                    }   //  Ende foreach
                    T1_Suche_Nach_Ordnern_Auf_C();
                }
                catch (Exception e_Ordner_Entfernen)
                {
                    ls.Erstelle_Fehlerbericht(Benutzer, "MainWindows.cs", "Button_Ordner_Entfernen_Minimiert", e_Ordner_Entfernen.ToString(), DateTime.Now.ToString());
                }   //  Ende catch
            }

        }

        void T1_Loesche_Tmp_Benutzerdaten()
        {
            if (Minimiert)
                sauber.Loesche_Tmp_Benutzer_Daten(Benutzer);
            else
            {
                DialogResult Abfrage = MessageBox.Show("Sicher, dass die temporären Dateien von " + T1_lbl_Angem_Benutzer.Text + " gelöscht werden soll?", "Temporäre Dateien löschen", MessageBoxButtons.YesNo);
                if (Abfrage == DialogResult.Yes)
                {
                    if (sauber.Loesche_Tmp_Benutzer_Daten(Benutzer))
                        MessageBox.Show("Die temporären Dateien von " + T1_lbl_Angem_Benutzer.Text + " wurde erfolgreich gelöscht!", "Erfolgreich gelöscht!");
                }
            }
            T1_lbl_Anzahl_Tmp_Dateien_Benutzer.Text = ls.Ermittel_Dateien_in_Verzeichnis("C:\\Users\\" + Benutzer + "\\AppData\\Local\\Temp\\").ToString();
        }

        void T1_Loesche_Windows_Tmp()
        {
            if (Minimiert)
                sauber.Loesche_Windows_Tmp();
            else
            {
                DialogResult Abfrage = MessageBox.Show("Sicher, dass die temporären Windows-Dateien gelöscht werden soll?", "Temporäre Dateien löschen", MessageBoxButtons.YesNo);
                if (Abfrage == DialogResult.Yes)
                {
                    if (sauber.Loesche_Windows_Tmp())
                        MessageBox.Show("Die temporären Windows-Dateien von wurde erfolgreich gelöscht!", "Erfolgreich gelöscht!");
                }
            }
            int a = ls.Ermittel_Dateien_in_Verzeichnis(Environment.ExpandEnvironmentVariables("%SYSTEMROOT%\\Temp"));
            a += ls.Ermittel_Dateien_in_Verzeichnis(Environment.ExpandEnvironmentVariables("%TMP%"));
            T1_lbl_Anzahl_Tmp_Dateien_Windows.Text = a.ToString();
        }

        void aktualisiere_Benutzer_Und_Gruppen_in_Tree()
        {
            T1_tree_Benutzer.Nodes.Clear(); T1_comboBox_Gruppenauswahl.Items.Clear(); int i = 0;
            List<string> Gruppen = bv.Hole_Alle_Gruppen(); List<string> Gruppen_Mit = new List<string>();
            foreach (String s in Gruppen)
            {
                if (Gew_Gruppen.Contains(s))
                {
                    T1_tree_Benutzer.Nodes.Add(s);
                    T1_comboBox_Gruppenauswahl.Items.Add(s);
                    Gruppen_Mit = bv.Hole_Gruppen_Mitglieder(s);
                    foreach (string ss in Gruppen_Mit)
                    {
                        if (ss == "INTERAKTIV" || ss == "Authentifizierte Benutzer")
                            continue;
                        else
                        T1_tree_Benutzer.Nodes[i].Nodes.Add(ss);
                    }
                    i++;
                }
            }   //  Ende foreach
            T1_tree_Benutzer.ExpandAll();
            if (T1_comboBox_Gruppenauswahl.Items.Count > 0)
                T1_comboBox_Gruppenauswahl.SelectedIndex = 0;
        }   //  Ende Methode aktualisiere_Benutzer_Und_Gruppen_in_Tree

        private void T1_btn_Benutzer_Hinzufuegen_Click(object sender, EventArgs e)
        {
            if ((T1_txt_BH_Name.TextLength > 1 && T1_txt_BH_Name.TextLength < 20) && (T1_txt_BH_Pass.TextLength > 1 && T1_txt_BH_Pass.TextLength < 20) && T1_comboBox_Gruppenauswahl.SelectedIndex != -1)
            {
                DialogResult Abfrage = MessageBox.Show("Soll der Benutzer:\t" + T1_txt_BH_Name.Text + "\nder Gruppe:\t" + T1_comboBox_Gruppenauswahl.SelectedItem.ToString() + "\n\nhinzugefügt werden?", "Sicher?", MessageBoxButtons.YesNo);
                if (Abfrage == DialogResult.Yes)
                {

                    if (bv.Erstelle_Benutzer(T1_txt_BH_Name.Text, T1_txt_BH_Pass.Text, T1_comboBox_Gruppenauswahl.SelectedItem.ToString() , T1_txt_BH_Name.Text, "", true, false))
                        MessageBox.Show("Der Benutzer: " + T1_txt_BH_Name.Text + " wurde erfolgreich hinzugefügt!", "Benutzer erfolgreich hinzuefügt!");
                    else
                        MessageBox.Show("Der Benutzer: " + T1_txt_BH_Name.Text + " konnte nicht hinzugefügt werden!", "Benutzer konnte nicht hinzuefügt werden!");
                    aktualisiere_Benutzer_Und_Gruppen_in_Tree();
                }
            }   //  Ende if, ob überhaupt etwas bei Name und Passwort eingegeben wurde
            else
                MessageBox.Show("Bitte alle Angaben prüfen:\n\nBenutzername:\tzwischen 2 und 20 Zeichen\nPasswort:\t\tzwischen 2 und 20 Zeichen\nEs muss eine Gruppe ausgewählt werden, welcher der Benutzer angehöhren soll.", "Angaben prüfen");
        }

        private void T1_tree_Benutzer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!Gew_Gruppen.Contains(e.Node.Text))
            {
                DialogResult Abfrage = MessageBox.Show("Soll der Benutzer " + e.Node.Text + " entfernt werden?", "Benutzer entfernen?", MessageBoxButtons.YesNo);
                if (Abfrage == DialogResult.Yes)
                {
                    bv.Benutzer_Entfernen(e.Node.Text);
                    MessageBox.Show("Der Benutzer " + e.Node.Text + " wurde erfolgreich entfernt!","Löschung erfolgreich!");
                    aktualisiere_Benutzer_Und_Gruppen_in_Tree();
                }
            }   //  Ende if, ob keine Gruppe ausgewählt wurde
        }   //  Ende der Methode T1_tree_Benutzer_NodeMouseDoubleClick

        private void T2_btn_Alle_Windows10_Apps_Entfernen_Click(object sender, EventArgs e)
        {
            win10.Entferne_Win10_Sinnlos_apps();
        }

        public void Aktiviere_Button_Win10_Apps(int Wert)
        {
            if (Wert == 0)
                T2_btn_Alle_Windows10_Apps_Entfernen.Enabled = false;
            else
                T2_btn_Alle_Windows10_Apps_Entfernen.Enabled = true;
        }

        private void T1_btn_Unnoetige_Ordner_entfernen_Click(object sender, EventArgs e)
        {
            if(T1_checkedListBox_Ordnern.SelectedIndex != -1)
                T1_Ordner_Entfernen();
        }

        private void T1_AO_Starten_Click(object sender, EventArgs e)
        {
            Windows_SmartClean.Funktionen.Automatisches_Optimieren ao = new Windows_SmartClean.Funktionen.Automatisches_Optimieren();
            if (ao.Ist_Task_Vorhanden())
            {
                DialogResult Abfrage = MessageBox.Show("Es gibt bereits ein geplante Durchführung von Windows SmartClean.\n\nSoll diese gelöscht und dafür eine neue erstellt werden?", "Windows SmartClean bereits vorhanden", MessageBoxButtons.YesNo);
                if (Abfrage == DialogResult.Yes)
                {
                    ao.Loesche_Aufgabe();
                    AO_Erstelle_Aufgabe();
                }
            }   //  Ende if, ob schon eine Aufgabe erstellt wurde
            else
            {
                AO_Erstelle_Aufgabe();
            }
        }   //  Ende Mehtode T1_AO_Starten_Click

        void AO_Pruefe_Ob_Es_Eine_Aufgabe_Gibt()
        {
            if (ao.Ist_Task_Vorhanden())
            {
                //AO_Image_Gibt_Es_Eine_Aufgabe.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Bilder/ok.png"));
                //AO_btn_Aufgabe_Entfernen.IsEnabled = true;
            }
            else
            {
                //AO_Image_Gibt_Es_Eine_Aufgabe.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Bilder/nok.png"));
                //AO_btn_Aufgabe_Entfernen.IsEnabled = false;
            }
        }   //  Ende Mehtode AO_Pruefe_Ob_Es_Eine_Aufgabe_Gibt

        void AO_Erstelle_Aufgabe()
        {
            string datum = AO_Starttag.Value.ToString();
            if (datum.Length > 0)
            {
                if (AO_TextBlock_Passwort.Text.Length > 1)
                {
                    try
                    {
                        short interval = 1; short.TryParse(AO_textBox_Interval.Text, out interval);
                        List<string> L_CheckBox = new List<string>();
                        L_CheckBox.Add("Unnoetige_Ordner:" + AO_1_checkBox_Unnoetige_Ordner.Checked.ToString());
                        L_CheckBox.Add("Papierkorb_Leeren:" + AO_2_checkBox_Papierkorb_leeren.Checked.ToString());
                        L_CheckBox.Add("Temp_Benutzer:" + AO_3_checkBox_Temp_Benutzer_Entf.Checked.ToString());
                        L_CheckBox.Add("Temp_Windows:" + AO_4_checkBox_Temp_Windows_Entf.Checked.ToString());
                        var secure = new SecureString();
                        foreach (char c in AO_TextBlock_Passwort.Text)
                        {
                            secure.AppendChar(c);
                        }
                        if (ao.Erstelle_Aufgabe(AO_comboBox_Benutzer.SelectedItem.ToString(), T1_lbl_ausfrd_Benutzer.Text, Benutzer_SID, AO_Starttag.Value.ToString(), T1_AO_dateTimePicker_Startzeit.Text, 1, "", L_CheckBox, secure))
                            MessageBox.Show("Aufgabe wurde erfolgreich erstellt!", "Ausgabe erfolgreich erstellt!");
                        else
                            MessageBox.Show("Aufgabe konnte nicht erstellt werden!", "Fehler beim Erstellen der Aufgabe!");
                        AO_Pruefe_Ob_Es_Eine_Aufgabe_Gibt();
                    }
                    catch (Exception e_AO_Erstelle_Aufgabe)
                    {
                        ls.Erstelle_Fehlerbericht(Benutzer, "MainWindow.cs", "AO_Erstelle_Aufgabe", e_AO_Erstelle_Aufgabe.ToString(), DateTime.Now.ToString());
                    }
                }
                else
                    MessageBox.Show("Es muss ein gültiges Passwort eingegeben werden!", "Hinweis");
            }
            else
                MessageBox.Show("Es muss ein gültiges Datum ausgewählt werden!", "Hinweis");
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //
        //  ´Tab 2, Windows 10
        //
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        void T2_Hole_Startmenu_Apps()
        {
            Win10_checkedListBox_StartmenueProgramme.Items.Clear();
            List<string> Apps = new List<string>(); Apps = win10.Hole_Startmenue_Apps();
            if(Apps.Count() > 0)
            {
                foreach (string s in Apps)
                    Win10_checkedListBox_StartmenueProgramme.Items.Add(s);
            }else
                Win10_checkedListBox_StartmenueProgramme.Items.Add("Keine Einträge!");
        }

        private void T2_btn_StandardApps_Wiederherstellen_Click(object sender, EventArgs e)
        {
            win10.Win10_StandardApps_WiederHerstellen();
        }

        void Beende_Threads()
        {
            if (T_Prozesse.IsAlive)
                T_Prozesse.Abort();
        }

        private void HauptFenster_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stoppe_Threads = true;
            Thread.Sleep(200);
            Beende_Threads();
        }

        private void T2_pictureBox_Win10_StdApss_Entf_Click(object sender, EventArgs e)
        {
            if(T2_Win10_checkedListBox_StandardApps.SelectedIndex != -1)
            {
                string s = "";
                foreach(var item in T2_Win10_checkedListBox_StandardApps.CheckedItems)
                    s += item.ToString() + "\n";
                DialogResult Abfrage = MessageBox.Show("Sollen folgende Apps entfernt werden:\n\n" + s,"Sicher?",MessageBoxButtons.YesNo);
                if(Abfrage == DialogResult.Yes)
                {

                }
            }
        }

        private void T2_AO_checkBox_Alle_Std_Apps_Auswaehlen_CheckedChanged(object sender, EventArgs e)
        {
            if (T2_AO_checkBox_Alle_Std_Apps_Auswaehlen.Checked)
                for(int i = 0; i < T2_Win10_checkedListBox_StandardApps.Items.Count; i++)
                    T2_Win10_checkedListBox_StandardApps.SetItemChecked(i, true);
            else
                for (int i = 0; i < T2_Win10_checkedListBox_StandardApps.Items.Count; i++)
                    T2_Win10_checkedListBox_StandardApps.SetItemChecked(i, false);
        }
    }
}
