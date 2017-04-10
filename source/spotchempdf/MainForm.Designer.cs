namespace spotchempdf
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSave = new System.Windows.Forms.Button();
            this.lstReadings = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSavePath = new System.Windows.Forms.TextBox();
            this.btnConfigSerial = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.frmFlbBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.showAfterSave = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.animalType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.animalAge = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.animalName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.clientName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.clientId = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblConnStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.rcvCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeSerialPortMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeOutputFolderMenutItem = new System.Windows.Forms.ToolStripMenuItem();
            this.providerDetailsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editRangesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadReadingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(506, 50);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(98, 41);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "&Ulož do PDF";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSavePDF_Click);
            // 
            // lstReadings
            // 
            this.lstReadings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstReadings.FormattingEnabled = true;
            this.lstReadings.Location = new System.Drawing.Point(12, 50);
            this.lstReadings.Name = "lstReadings";
            this.lstReadings.Size = new System.Drawing.Size(208, 251);
            this.lstReadings.Sorted = true;
            this.lstReadings.TabIndex = 1;
            this.lstReadings.SelectedIndexChanged += new System.EventHandler(this.lstReadings_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Načítané merania (vyber jedno)";
            // 
            // tbSavePath
            // 
            this.tbSavePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSavePath.Location = new System.Drawing.Point(12, 345);
            this.tbSavePath.Name = "tbSavePath";
            this.tbSavePath.ReadOnly = true;
            this.tbSavePath.Size = new System.Drawing.Size(463, 20);
            this.tbSavePath.TabIndex = 7;
            this.tbSavePath.TabStop = false;
            // 
            // btnConfigSerial
            // 
            this.btnConfigSerial.Location = new System.Drawing.Point(0, 0);
            this.btnConfigSerial.Margin = new System.Windows.Forms.Padding(2);
            this.btnConfigSerial.Name = "btnConfigSerial";
            this.btnConfigSerial.Size = new System.Drawing.Size(56, 19);
            this.btnConfigSerial.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 330);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Úložisko PDF";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 19);
            this.button1.TabIndex = 19;
            // 
            // showAfterSave
            // 
            this.showAfterSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showAfterSave.AutoSize = true;
            this.showAfterSave.Location = new System.Drawing.Point(512, 98);
            this.showAfterSave.Name = "showAfterSave";
            this.showAfterSave.Size = new System.Drawing.Size(77, 30);
            this.showAfterSave.TabIndex = 8;
            this.showAfterSave.Text = "Po uložení\r\notvor PDF";
            this.showAfterSave.UseVisualStyleBackColor = true;
            this.showAfterSave.CheckedChanged += new System.EventHandler(this.showAfterSave_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.animalType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.animalAge);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.animalName);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.clientName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.clientId);
            this.groupBox1.Location = new System.Drawing.Point(235, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 267);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Doplňujúce údaje";
            // 
            // animalType
            // 
            this.animalType.FormattingEnabled = true;
            this.animalType.Location = new System.Drawing.Point(7, 142);
            this.animalType.Name = "animalType";
            this.animalType.Size = new System.Drawing.Size(121, 21);
            this.animalType.TabIndex = 4;
            this.animalType.TextChanged += new System.EventHandler(this.readingModified_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Druh zvieraťa";
            // 
            // animalAge
            // 
            this.animalAge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.animalAge.Location = new System.Drawing.Point(6, 225);
            this.animalAge.MaxLength = 3;
            this.animalAge.Name = "animalAge";
            this.animalAge.Size = new System.Drawing.Size(76, 20);
            this.animalAge.TabIndex = 6;
            this.animalAge.TextChanged += new System.EventHandler(this.readingModified_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Vek zvieraťa";
            // 
            // animalName
            // 
            this.animalName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.animalName.Location = new System.Drawing.Point(6, 183);
            this.animalName.MaxLength = 50;
            this.animalName.Name = "animalName";
            this.animalName.Size = new System.Drawing.Size(227, 20);
            this.animalName.TabIndex = 5;
            this.animalName.TextChanged += new System.EventHandler(this.readingModified_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Meno zvieraťa";
            // 
            // clientName
            // 
            this.clientName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clientName.Location = new System.Drawing.Point(6, 84);
            this.clientName.MaxLength = 50;
            this.clientName.Name = "clientName";
            this.clientName.Size = new System.Drawing.Size(227, 20);
            this.clientName.TabIndex = 3;
            this.clientName.TextChanged += new System.EventHandler(this.readingModified_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Meno klienta";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Číslo klienta";
            // 
            // clientId
            // 
            this.clientId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clientId.Location = new System.Drawing.Point(7, 44);
            this.clientId.MaxLength = 10;
            this.clientId.Name = "clientId";
            this.clientId.Size = new System.Drawing.Size(100, 20);
            this.clientId.TabIndex = 2;
            this.clientId.TextChanged += new System.EventHandler(this.readingModified_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblConnStatus,
            this.toolStripStatusLabel3,
            this.rcvCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 379);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(627, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(52, 17);
            this.toolStripStatusLabel1.Text = "Spojenie";
            // 
            // lblConnStatus
            // 
            this.lblConnStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblConnStatus.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblConnStatus.Name = "lblConnStatus";
            this.lblConnStatus.Size = new System.Drawing.Size(112, 17);
            this.lblConnStatus.Text = "OK (9600-N8-One)";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(40, 17);
            this.toolStripStatusLabel3.Text = "Prijaté";
            // 
            // rcvCount
            // 
            this.rcvCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rcvCount.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rcvCount.Name = "rcvCount";
            this.rcvCount.Size = new System.Drawing.Size(14, 17);
            this.rcvCount.Text = "0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsMenuItem,
            this.loadReadingsMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(627, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeSerialPortMenuItem,
            this.changeOutputFolderMenutItem,
            this.providerDetailsMenuItem,
            this.editRangesMenuItem});
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.Size = new System.Drawing.Size(77, 20);
            this.settingsMenuItem.Text = "&Nastavenia";
            // 
            // changeSerialPortMenuItem
            // 
            this.changeSerialPortMenuItem.Name = "changeSerialPortMenuItem";
            this.changeSerialPortMenuItem.Size = new System.Drawing.Size(181, 22);
            this.changeSerialPortMenuItem.Text = "Nastav &spojenie";
            this.changeSerialPortMenuItem.Click += new System.EventHandler(this.changeSerialPortMenuItem_Click);
            // 
            // changeOutputFolderMenutItem
            // 
            this.changeOutputFolderMenutItem.Name = "changeOutputFolderMenutItem";
            this.changeOutputFolderMenutItem.Size = new System.Drawing.Size(181, 22);
            this.changeOutputFolderMenutItem.Text = "Zmeň ú&ložisko";
            this.changeOutputFolderMenutItem.Click += new System.EventHandler(this.changeOutputFolderMenutItem_Click);
            // 
            // providerDetailsMenuItem
            // 
            this.providerDetailsMenuItem.Name = "providerDetailsMenuItem";
            this.providerDetailsMenuItem.Size = new System.Drawing.Size(181, 22);
            this.providerDetailsMenuItem.Text = "Údaje &poskytovateľa";
            this.providerDetailsMenuItem.Click += new System.EventHandler(this.providerDetailsMenuItem_Click);
            // 
            // editRangesMenuItem
            // 
            this.editRangesMenuItem.Name = "editRangesMenuItem";
            this.editRangesMenuItem.Size = new System.Drawing.Size(181, 22);
            this.editRangesMenuItem.Text = "&Referenčné rozsahy";
            this.editRangesMenuItem.Click += new System.EventHandler(this.editRangesMenuItem_Click);
            // 
            // loadReadingsMenuItem
            // 
            this.loadReadingsMenuItem.Name = "loadReadingsMenuItem";
            this.loadReadingsMenuItem.Size = new System.Drawing.Size(102, 20);
            this.loadReadingsMenuItem.Text = "N&ačítaj merania";
            this.loadReadingsMenuItem.Click += new System.EventHandler(this.loadReadingsMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 401);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.showAfterSave);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnConfigSerial);
            this.Controls.Add(this.tbSavePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstReadings);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SpotchemPDF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstReadings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSavePath;
        private System.Windows.Forms.Button btnConfigSerial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog frmFlbBrowse;
        private System.Windows.Forms.CheckBox showAfterSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox clientId;
        private System.Windows.Forms.TextBox animalName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox clientName;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblConnStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel rcvCount;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeSerialPortMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeOutputFolderMenutItem;
        private System.Windows.Forms.ToolStripMenuItem loadReadingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem providerDetailsMenuItem;
        private System.Windows.Forms.ComboBox animalType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox animalAge;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem editRangesMenuItem;
    }
}

