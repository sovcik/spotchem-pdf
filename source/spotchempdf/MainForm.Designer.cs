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
            this.label2 = new System.Windows.Forms.Label();
            this.lblConnStatus = new System.Windows.Forms.Label();
            this.tbSavePath = new System.Windows.Forms.TextBox();
            this.btnConfigSerial = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.frmFlbBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.rcvCount = new System.Windows.Forms.Label();
            this.showAfterSave = new System.Windows.Forms.CheckBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.animalName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.clientName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.clientId = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(506, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(98, 41);
            this.btnSave.TabIndex = 5;
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
            this.lstReadings.Location = new System.Drawing.Point(12, 25);
            this.lstReadings.Name = "lstReadings";
            this.lstReadings.Size = new System.Drawing.Size(208, 212);
            this.lstReadings.Sorted = true;
            this.lstReadings.TabIndex = 1;
            this.lstReadings.SelectedIndexChanged += new System.EventHandler(this.lstReadings_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Načítané merania (vyber jedno)";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 305);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Spojenie:";
            // 
            // lblConnStatus
            // 
            this.lblConnStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblConnStatus.AutoSize = true;
            this.lblConnStatus.Location = new System.Drawing.Point(66, 305);
            this.lblConnStatus.Name = "lblConnStatus";
            this.lblConnStatus.Size = new System.Drawing.Size(22, 13);
            this.lblConnStatus.TabIndex = 4;
            this.lblConnStatus.Text = "OK";
            // 
            // tbSavePath
            // 
            this.tbSavePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSavePath.Location = new System.Drawing.Point(12, 274);
            this.tbSavePath.Name = "tbSavePath";
            this.tbSavePath.ReadOnly = true;
            this.tbSavePath.Size = new System.Drawing.Size(487, 20);
            this.tbSavePath.TabIndex = 7;
            this.tbSavePath.TabStop = false;
            // 
            // btnConfigSerial
            // 
            this.btnConfigSerial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfigSerial.Location = new System.Drawing.Point(505, 300);
            this.btnConfigSerial.Name = "btnConfigSerial";
            this.btnConfigSerial.Size = new System.Drawing.Size(98, 23);
            this.btnConfigSerial.TabIndex = 9;
            this.btnConfigSerial.Text = "Nastav spojenie";
            this.btnConfigSerial.UseVisualStyleBackColor = true;
            this.btnConfigSerial.Click += new System.EventHandler(this.btnConfigSerial_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Úložisko PDF";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(505, 271);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Zmeň úložisko";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnChangePDFPath);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(253, 305);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Prijaté";
            // 
            // rcvCount
            // 
            this.rcvCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rcvCount.AutoSize = true;
            this.rcvCount.Location = new System.Drawing.Point(295, 305);
            this.rcvCount.Name = "rcvCount";
            this.rcvCount.Size = new System.Drawing.Size(13, 13);
            this.rcvCount.TabIndex = 12;
            this.rcvCount.Text = "0";
            // 
            // showAfterSave
            // 
            this.showAfterSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showAfterSave.AutoSize = true;
            this.showAfterSave.Location = new System.Drawing.Point(506, 82);
            this.showAfterSave.Name = "showAfterSave";
            this.showAfterSave.Size = new System.Drawing.Size(112, 17);
            this.showAfterSave.TabIndex = 6;
            this.showAfterSave.Text = "Zobraz po uložení";
            this.showAfterSave.UseVisualStyleBackColor = true;
            this.showAfterSave.CheckedChanged += new System.EventHandler(this.showAfterSave_CheckedChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(505, 208);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(98, 31);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Načítaj merania";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.animalName);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.clientName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.clientId);
            this.groupBox1.Location = new System.Drawing.Point(235, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 225);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Doplňujúce údaje";
            // 
            // animalName
            // 
            this.animalName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.animalName.Location = new System.Drawing.Point(6, 134);
            this.animalName.MaxLength = 50;
            this.animalName.Name = "animalName";
            this.animalName.Size = new System.Drawing.Size(227, 20);
            this.animalName.TabIndex = 4;
            this.animalName.TextChanged += new System.EventHandler(this.readingModified_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Meno zvieraťa";
            // 
            // clientName
            // 
            this.clientName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clientName.Location = new System.Drawing.Point(6, 77);
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
            this.label6.Location = new System.Drawing.Point(7, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Meno klienta";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Číslo klienta";
            // 
            // clientId
            // 
            this.clientId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clientId.Location = new System.Drawing.Point(7, 37);
            this.clientId.MaxLength = 10;
            this.clientId.Name = "clientId";
            this.clientId.Size = new System.Drawing.Size(100, 20);
            this.clientId.TabIndex = 2;
            this.clientId.TextChanged += new System.EventHandler(this.readingModified_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 330);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.showAfterSave);
            this.Controls.Add(this.rcvCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnConfigSerial);
            this.Controls.Add(this.tbSavePath);
            this.Controls.Add(this.lblConnStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstReadings);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SpotchemPDF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstReadings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblConnStatus;
        private System.Windows.Forms.TextBox tbSavePath;
        private System.Windows.Forms.Button btnConfigSerial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog frmFlbBrowse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label rcvCount;
        private System.Windows.Forms.CheckBox showAfterSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox clientId;
        private System.Windows.Forms.TextBox animalName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox clientName;
        private System.Windows.Forms.Timer timer1;
    }
}

