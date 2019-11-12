namespace Windows_SmartClean_Forms
{
    partial class HauptFenster
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HauptFenster));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.AO_comboBox_Benutzer = new System.Windows.Forms.ComboBox();
            this.T1_checkedListBox_Ordnern = new System.Windows.Forms.CheckedListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.T1_lbl_Dateien_Papierkorb = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.T1_lbl_Anzahl_Tmp_Dateien_Benutzer = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.T1_lbl_Anzahl_Tmp_Dateien_Windows = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.T1_GroupBox_Uebersicht_Nutzer_Gruppen = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.T1_tree_Benutzer = new System.Windows.Forms.TreeView();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.T1_btn_Benutzer_Hinzufuegen = new System.Windows.Forms.Button();
            this.T1_comboBox_Gruppenauswahl = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.T1_pic_Benutzer_Hinzufuegen = new System.Windows.Forms.PictureBox();
            this.T1_txt_BH_Name = new System.Windows.Forms.TextBox();
            this.T1_txt_BH_Pass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.T1_lbl_Angem_Benutzer = new System.Windows.Forms.Label();
            this.T1_lbl_ausfrd_Benutzer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.T2_btn_Alle_Windows10_Apps_Entfernen = new System.Windows.Forms.Button();
            this.AO_Starttag = new System.Windows.Forms.DateTimePicker();
            this.T1_btn_Unnoetige_Ordner_entfernen = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.T1_AO_dateTimePicker_Startzeit = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.T1_AO_Starten = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.AO_TextBlock_Passwort = new System.Windows.Forms.TextBox();
            this.AO_textBox_Interval = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.T1_GroupBox_Uebersicht_Nutzer_Gruppen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.T1_pic_Benutzer_Hinzufuegen)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(815, 442);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.T1_btn_Unnoetige_Ordner_entfernen);
            this.tabPage1.Controls.Add(this.T1_checkedListBox_Ordnern);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.T1_lbl_Dateien_Papierkorb);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.T1_lbl_Anzahl_Tmp_Dateien_Benutzer);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.T1_lbl_Anzahl_Tmp_Dateien_Windows);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.T1_GroupBox_Uebersicht_Nutzer_Gruppen);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.T1_lbl_Angem_Benutzer);
            this.tabPage1.Controls.Add(this.T1_lbl_ausfrd_Benutzer);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(807, 416);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Informationen";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // AO_comboBox_Benutzer
            // 
            this.AO_comboBox_Benutzer.FormattingEnabled = true;
            this.AO_comboBox_Benutzer.Location = new System.Drawing.Point(127, 73);
            this.AO_comboBox_Benutzer.Name = "AO_comboBox_Benutzer";
            this.AO_comboBox_Benutzer.Size = new System.Drawing.Size(200, 24);
            this.AO_comboBox_Benutzer.TabIndex = 16;
            // 
            // T1_checkedListBox_Ordnern
            // 
            this.T1_checkedListBox_Ordnern.CheckOnClick = true;
            this.T1_checkedListBox_Ordnern.FormattingEnabled = true;
            this.T1_checkedListBox_Ordnern.Location = new System.Drawing.Point(255, 13);
            this.T1_checkedListBox_Ordnern.Name = "T1_checkedListBox_Ordnern";
            this.T1_checkedListBox_Ordnern.Size = new System.Drawing.Size(186, 123);
            this.T1_checkedListBox_Ordnern.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(145, 16);
            this.label11.TabIndex = 14;
            this.label11.Text = "Dateien im Papierkorb:";
            // 
            // T1_lbl_Dateien_Papierkorb
            // 
            this.T1_lbl_Dateien_Papierkorb.AutoSize = true;
            this.T1_lbl_Dateien_Papierkorb.Location = new System.Drawing.Point(185, 124);
            this.T1_lbl_Dateien_Papierkorb.Name = "T1_lbl_Dateien_Papierkorb";
            this.T1_lbl_Dateien_Papierkorb.Size = new System.Drawing.Size(26, 16);
            this.T1_lbl_Dateien_Papierkorb.TabIndex = 13;
            this.T1_lbl_Dateien_Papierkorb.Text = "xxx";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(155, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "Benutzer Temp. Dateien:";
            // 
            // T1_lbl_Anzahl_Tmp_Dateien_Benutzer
            // 
            this.T1_lbl_Anzahl_Tmp_Dateien_Benutzer.AutoSize = true;
            this.T1_lbl_Anzahl_Tmp_Dateien_Benutzer.Location = new System.Drawing.Point(185, 98);
            this.T1_lbl_Anzahl_Tmp_Dateien_Benutzer.Name = "T1_lbl_Anzahl_Tmp_Dateien_Benutzer";
            this.T1_lbl_Anzahl_Tmp_Dateien_Benutzer.Size = new System.Drawing.Size(26, 16);
            this.T1_lbl_Anzahl_Tmp_Dateien_Benutzer.TabIndex = 11;
            this.T1_lbl_Anzahl_Tmp_Dateien_Benutzer.Text = "xxx";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(158, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "Windows Temp. Dateien:";
            // 
            // T1_lbl_Anzahl_Tmp_Dateien_Windows
            // 
            this.T1_lbl_Anzahl_Tmp_Dateien_Windows.AutoSize = true;
            this.T1_lbl_Anzahl_Tmp_Dateien_Windows.Location = new System.Drawing.Point(185, 69);
            this.T1_lbl_Anzahl_Tmp_Dateien_Windows.Name = "T1_lbl_Anzahl_Tmp_Dateien_Windows";
            this.T1_lbl_Anzahl_Tmp_Dateien_Windows.Size = new System.Drawing.Size(26, 16);
            this.T1_lbl_Anzahl_Tmp_Dateien_Windows.TabIndex = 9;
            this.T1_lbl_Anzahl_Tmp_Dateien_Windows.Text = "xxx";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 222);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(785, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "_________________________________________________________________________________" +
    "______________________________";
            // 
            // T1_GroupBox_Uebersicht_Nutzer_Gruppen
            // 
            this.T1_GroupBox_Uebersicht_Nutzer_Gruppen.Controls.Add(this.pictureBox1);
            this.T1_GroupBox_Uebersicht_Nutzer_Gruppen.Controls.Add(this.T1_tree_Benutzer);
            this.T1_GroupBox_Uebersicht_Nutzer_Gruppen.Controls.Add(this.label9);
            this.T1_GroupBox_Uebersicht_Nutzer_Gruppen.Location = new System.Drawing.Point(9, 259);
            this.T1_GroupBox_Uebersicht_Nutzer_Gruppen.Name = "T1_GroupBox_Uebersicht_Nutzer_Gruppen";
            this.T1_GroupBox_Uebersicht_Nutzer_Gruppen.Size = new System.Drawing.Size(440, 152);
            this.T1_GroupBox_Uebersicht_Nutzer_Gruppen.TabIndex = 6;
            this.T1_GroupBox_Uebersicht_Nutzer_Gruppen.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(336, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 96);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // T1_tree_Benutzer
            // 
            this.T1_tree_Benutzer.Location = new System.Drawing.Point(6, 21);
            this.T1_tree_Benutzer.Name = "T1_tree_Benutzer";
            this.T1_tree_Benutzer.Size = new System.Drawing.Size(324, 124);
            this.T1_tree_Benutzer.TabIndex = 3;
            this.T1_tree_Benutzer.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.T1_tree_Benutzer_NodeMouseDoubleClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(290, 18);
            this.label9.TabIndex = 2;
            this.label9.Text = "Übersicht der Benutzer Und Gruppen:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.T1_btn_Benutzer_Hinzufuegen);
            this.groupBox1.Controls.Add(this.T1_comboBox_Gruppenauswahl);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.T1_pic_Benutzer_Hinzufuegen);
            this.groupBox1.Controls.Add(this.T1_txt_BH_Name);
            this.groupBox1.Controls.Add(this.T1_txt_BH_Pass);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(455, 259);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 152);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // T1_btn_Benutzer_Hinzufuegen
            // 
            this.T1_btn_Benutzer_Hinzufuegen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.T1_btn_Benutzer_Hinzufuegen.Image = ((System.Drawing.Image)(resources.GetObject("T1_btn_Benutzer_Hinzufuegen.Image")));
            this.T1_btn_Benutzer_Hinzufuegen.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.T1_btn_Benutzer_Hinzufuegen.Location = new System.Drawing.Point(90, 111);
            this.T1_btn_Benutzer_Hinzufuegen.Name = "T1_btn_Benutzer_Hinzufuegen";
            this.T1_btn_Benutzer_Hinzufuegen.Size = new System.Drawing.Size(169, 34);
            this.T1_btn_Benutzer_Hinzufuegen.TabIndex = 7;
            this.T1_btn_Benutzer_Hinzufuegen.Text = "Benutzer hinzufügen";
            this.T1_btn_Benutzer_Hinzufuegen.UseVisualStyleBackColor = true;
            this.T1_btn_Benutzer_Hinzufuegen.Click += new System.EventHandler(this.T1_btn_Benutzer_Hinzufuegen_Click);
            // 
            // T1_comboBox_Gruppenauswahl
            // 
            this.T1_comboBox_Gruppenauswahl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.T1_comboBox_Gruppenauswahl.FormattingEnabled = true;
            this.T1_comboBox_Gruppenauswahl.Location = new System.Drawing.Point(90, 81);
            this.T1_comboBox_Gruppenauswahl.Name = "T1_comboBox_Gruppenauswahl";
            this.T1_comboBox_Gruppenauswahl.Size = new System.Drawing.Size(245, 24);
            this.T1_comboBox_Gruppenauswahl.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Gruppe:";
            // 
            // T1_pic_Benutzer_Hinzufuegen
            // 
            this.T1_pic_Benutzer_Hinzufuegen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("T1_pic_Benutzer_Hinzufuegen.BackgroundImage")));
            this.T1_pic_Benutzer_Hinzufuegen.Location = new System.Drawing.Point(269, 14);
            this.T1_pic_Benutzer_Hinzufuegen.Name = "T1_pic_Benutzer_Hinzufuegen";
            this.T1_pic_Benutzer_Hinzufuegen.Size = new System.Drawing.Size(64, 64);
            this.T1_pic_Benutzer_Hinzufuegen.TabIndex = 5;
            this.T1_pic_Benutzer_Hinzufuegen.TabStop = false;
            // 
            // T1_txt_BH_Name
            // 
            this.T1_txt_BH_Name.Location = new System.Drawing.Point(90, 27);
            this.T1_txt_BH_Name.Name = "T1_txt_BH_Name";
            this.T1_txt_BH_Name.Size = new System.Drawing.Size(169, 22);
            this.T1_txt_BH_Name.TabIndex = 4;
            // 
            // T1_txt_BH_Pass
            // 
            this.T1_txt_BH_Pass.Location = new System.Drawing.Point(90, 53);
            this.T1_txt_BH_Pass.Name = "T1_txt_BH_Pass";
            this.T1_txt_BH_Pass.Size = new System.Drawing.Size(169, 22);
            this.T1_txt_BH_Pass.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(214, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "Neuen Benutzer hinzufügen";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Passwort:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name:";
            // 
            // T1_lbl_Angem_Benutzer
            // 
            this.T1_lbl_Angem_Benutzer.AutoSize = true;
            this.T1_lbl_Angem_Benutzer.Location = new System.Drawing.Point(185, 41);
            this.T1_lbl_Angem_Benutzer.Name = "T1_lbl_Angem_Benutzer";
            this.T1_lbl_Angem_Benutzer.Size = new System.Drawing.Size(38, 16);
            this.T1_lbl_Angem_Benutzer.TabIndex = 4;
            this.T1_lbl_Angem_Benutzer.Text = "xxxxx";
            // 
            // T1_lbl_ausfrd_Benutzer
            // 
            this.T1_lbl_ausfrd_Benutzer.AutoSize = true;
            this.T1_lbl_ausfrd_Benutzer.Location = new System.Drawing.Point(185, 13);
            this.T1_lbl_ausfrd_Benutzer.Name = "T1_lbl_ausfrd_Benutzer";
            this.T1_lbl_ausfrd_Benutzer.Size = new System.Drawing.Size(38, 16);
            this.T1_lbl_ausfrd_Benutzer.TabIndex = 3;
            this.T1_lbl_ausfrd_Benutzer.Text = "xxxxx";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Angemeldeter Benutzer:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Benutzer mit Adminrechten:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.T2_btn_Alle_Windows10_Apps_Entfernen);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(807, 416);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Windows 10";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // T2_btn_Alle_Windows10_Apps_Entfernen
            // 
            this.T2_btn_Alle_Windows10_Apps_Entfernen.Location = new System.Drawing.Point(16, 15);
            this.T2_btn_Alle_Windows10_Apps_Entfernen.Name = "T2_btn_Alle_Windows10_Apps_Entfernen";
            this.T2_btn_Alle_Windows10_Apps_Entfernen.Size = new System.Drawing.Size(213, 43);
            this.T2_btn_Alle_Windows10_Apps_Entfernen.TabIndex = 0;
            this.T2_btn_Alle_Windows10_Apps_Entfernen.Text = "Entferne Windows 10 Apps";
            this.T2_btn_Alle_Windows10_Apps_Entfernen.UseVisualStyleBackColor = true;
            this.T2_btn_Alle_Windows10_Apps_Entfernen.Click += new System.EventHandler(this.T2_btn_Alle_Windows10_Apps_Entfernen_Click);
            // 
            // AO_Starttag
            // 
            this.AO_Starttag.Location = new System.Drawing.Point(127, 21);
            this.AO_Starttag.Name = "AO_Starttag";
            this.AO_Starttag.Size = new System.Drawing.Size(200, 22);
            this.AO_Starttag.TabIndex = 17;
            // 
            // T1_btn_Unnoetige_Ordner_entfernen
            // 
            this.T1_btn_Unnoetige_Ordner_entfernen.Location = new System.Drawing.Point(255, 142);
            this.T1_btn_Unnoetige_Ordner_entfernen.Name = "T1_btn_Unnoetige_Ordner_entfernen";
            this.T1_btn_Unnoetige_Ordner_entfernen.Size = new System.Drawing.Size(186, 40);
            this.T1_btn_Unnoetige_Ordner_entfernen.TabIndex = 18;
            this.T1_btn_Unnoetige_Ordner_entfernen.Text = "Unnötige Ordner entfernen";
            this.T1_btn_Unnoetige_Ordner_entfernen.UseVisualStyleBackColor = true;
            this.T1_btn_Unnoetige_Ordner_entfernen.Click += new System.EventHandler(this.T1_btn_Unnoetige_Ordner_entfernen_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.AO_textBox_Interval);
            this.groupBox2.Controls.Add(this.AO_TextBlock_Passwort);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.T1_AO_Starten);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.T1_AO_dateTimePicker_Startzeit);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.AO_Starttag);
            this.groupBox2.Controls.Add(this.AO_comboBox_Benutzer);
            this.groupBox2.Location = new System.Drawing.Point(455, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 213);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 16);
            this.label12.TabIndex = 20;
            this.label12.Text = "Starttag:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 16);
            this.label13.TabIndex = 22;
            this.label13.Text = "Startzeit:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(191, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 16);
            this.label14.TabIndex = 23;
            this.label14.Text = "Uhr";
            // 
            // T1_AO_dateTimePicker_Startzeit
            // 
            this.T1_AO_dateTimePicker_Startzeit.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.T1_AO_dateTimePicker_Startzeit.Location = new System.Drawing.Point(127, 47);
            this.T1_AO_dateTimePicker_Startzeit.Name = "T1_AO_dateTimePicker_Startzeit";
            this.T1_AO_dateTimePicker_Startzeit.Size = new System.Drawing.Size(58, 22);
            this.T1_AO_dateTimePicker_Startzeit.TabIndex = 24;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 76);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 16);
            this.label15.TabIndex = 25;
            this.label15.Text = "Benutzer:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.White;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(8, -2);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(206, 18);
            this.label16.TabIndex = 5;
            this.label16.Text = "Automatische Optimierung";
            // 
            // T1_AO_Starten
            // 
            this.T1_AO_Starten.Location = new System.Drawing.Point(127, 167);
            this.T1_AO_Starten.Name = "T1_AO_Starten";
            this.T1_AO_Starten.Size = new System.Drawing.Size(200, 40);
            this.T1_AO_Starten.TabIndex = 26;
            this.T1_AO_Starten.Text = "Automatische Optimierung starten";
            this.T1_AO_Starten.UseVisualStyleBackColor = true;
            this.T1_AO_Starten.Click += new System.EventHandler(this.T1_AO_Starten_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 104);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 16);
            this.label17.TabIndex = 28;
            this.label17.Text = "Passwort:";
            // 
            // AO_TextBlock_Passwort
            // 
            this.AO_TextBlock_Passwort.Location = new System.Drawing.Point(127, 99);
            this.AO_TextBlock_Passwort.Name = "AO_TextBlock_Passwort";
            this.AO_TextBlock_Passwort.PasswordChar = '*';
            this.AO_TextBlock_Passwort.Size = new System.Drawing.Size(100, 22);
            this.AO_TextBlock_Passwort.TabIndex = 29;
            // 
            // AO_textBox_Interval
            // 
            this.AO_textBox_Interval.Location = new System.Drawing.Point(127, 123);
            this.AO_textBox_Interval.Name = "AO_textBox_Interval";
            this.AO_textBox_Interval.Size = new System.Drawing.Size(46, 22);
            this.AO_textBox_Interval.TabIndex = 30;
            this.AO_textBox_Interval.Text = "1";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(9, 126);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 16);
            this.label18.TabIndex = 31;
            this.label18.Text = "Intervall:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(191, 126);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(49, 16);
            this.label19.TabIndex = 32;
            this.label19.Text = "Tag(e)";
            // 
            // HauptFenster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 450);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HauptFenster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows SmartClean";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.T1_GroupBox_Uebersicht_Nutzer_Gruppen.ResumeLayout(false);
            this.T1_GroupBox_Uebersicht_Nutzer_Gruppen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.T1_pic_Benutzer_Hinzufuegen)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label T1_lbl_Angem_Benutzer;
        private System.Windows.Forms.Label T1_lbl_ausfrd_Benutzer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox T1_txt_BH_Pass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox T1_pic_Benutzer_Hinzufuegen;
        private System.Windows.Forms.TextBox T1_txt_BH_Name;
        private System.Windows.Forms.GroupBox T1_GroupBox_Uebersicht_Nutzer_Gruppen;
        private System.Windows.Forms.TreeView T1_tree_Benutzer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox T1_comboBox_Gruppenauswahl;
        private System.Windows.Forms.Button T1_btn_Benutzer_Hinzufuegen;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button T2_btn_Alle_Windows10_Apps_Entfernen;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label T1_lbl_Anzahl_Tmp_Dateien_Windows;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label T1_lbl_Anzahl_Tmp_Dateien_Benutzer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label T1_lbl_Dateien_Papierkorb;
        private System.Windows.Forms.CheckedListBox T1_checkedListBox_Ordnern;
        private System.Windows.Forms.ComboBox AO_comboBox_Benutzer;
        private System.Windows.Forms.DateTimePicker AO_Starttag;
        private System.Windows.Forms.Button T1_btn_Unnoetige_Ordner_entfernen;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker T1_AO_dateTimePicker_Startzeit;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button T1_AO_Starten;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox AO_TextBlock_Passwort;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox AO_textBox_Interval;
    }
}

