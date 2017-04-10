namespace spotchempdf
{
    partial class frmEditRanges
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
            this.animalType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addType = new System.Windows.Forms.Button();
            this.typeRanges = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rangeMax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rangeMin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rangeUnit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.deleteType = new System.Windows.Forms.Button();
            this.deleteRange = new System.Windows.Forms.Button();
            this.addRange = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rangeName = new System.Windows.Forms.TextBox();
            this.timerSaveRanges = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // animalType
            // 
            this.animalType.FormattingEnabled = true;
            this.animalType.Location = new System.Drawing.Point(69, 10);
            this.animalType.Name = "animalType";
            this.animalType.Size = new System.Drawing.Size(114, 21);
            this.animalType.TabIndex = 0;
            this.animalType.SelectionChangeCommitted += new System.EventHandler(this.animalType_SelectionChangeCommitted);
            this.animalType.TextUpdate += new System.EventHandler(this.animalType_TextUpdate);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Druh";
            // 
            // addType
            // 
            this.addType.Location = new System.Drawing.Point(208, 8);
            this.addType.Name = "addType";
            this.addType.Size = new System.Drawing.Size(75, 23);
            this.addType.TabIndex = 2;
            this.addType.Text = "Pridaj druh";
            this.addType.UseVisualStyleBackColor = true;
            this.addType.Click += new System.EventHandler(this.addType_Click);
            // 
            // typeRanges
            // 
            this.typeRanges.FormattingEnabled = true;
            this.typeRanges.Location = new System.Drawing.Point(16, 67);
            this.typeRanges.Name = "typeRanges";
            this.typeRanges.Size = new System.Drawing.Size(167, 147);
            this.typeRanges.TabIndex = 4;
            this.typeRanges.SelectedIndexChanged += new System.EventHandler(this.typeRanges_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Rozsahy";
            // 
            // rangeMax
            // 
            this.rangeMax.Enabled = false;
            this.rangeMax.Location = new System.Drawing.Point(253, 122);
            this.rangeMax.MaxLength = 10;
            this.rangeMax.Name = "rangeMax";
            this.rangeMax.Size = new System.Drawing.Size(121, 20);
            this.rangeMax.TabIndex = 7;
            this.rangeMax.TextChanged += new System.EventHandler(this.rangeMax_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(199, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Max";
            // 
            // rangeMin
            // 
            this.rangeMin.Enabled = false;
            this.rangeMin.Location = new System.Drawing.Point(253, 96);
            this.rangeMin.MaxLength = 10;
            this.rangeMin.Name = "rangeMin";
            this.rangeMin.Size = new System.Drawing.Size(121, 20);
            this.rangeMin.TabIndex = 6;
            this.rangeMin.TextChanged += new System.EventHandler(this.rangeMin_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(199, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Min";
            // 
            // rangeUnit
            // 
            this.rangeUnit.Enabled = false;
            this.rangeUnit.Location = new System.Drawing.Point(253, 149);
            this.rangeUnit.MaxLength = 10;
            this.rangeUnit.Name = "rangeUnit";
            this.rangeUnit.Size = new System.Drawing.Size(121, 20);
            this.rangeUnit.TabIndex = 8;
            this.rangeUnit.TextChanged += new System.EventHandler(this.rangeUnit_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(199, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Jednotka";
            // 
            // deleteType
            // 
            this.deleteType.Location = new System.Drawing.Point(289, 8);
            this.deleteType.Name = "deleteType";
            this.deleteType.Size = new System.Drawing.Size(75, 23);
            this.deleteType.TabIndex = 3;
            this.deleteType.Text = "Vymaž druh";
            this.deleteType.UseVisualStyleBackColor = true;
            this.deleteType.Click += new System.EventHandler(this.deleteType_Click);
            // 
            // deleteRange
            // 
            this.deleteRange.Enabled = false;
            this.deleteRange.Location = new System.Drawing.Point(108, 220);
            this.deleteRange.Name = "deleteRange";
            this.deleteRange.Size = new System.Drawing.Size(75, 24);
            this.deleteRange.TabIndex = 10;
            this.deleteRange.Text = "Vymaž test";
            this.deleteRange.UseVisualStyleBackColor = true;
            this.deleteRange.Click += new System.EventHandler(this.deleteRange_Click);
            // 
            // addRange
            // 
            this.addRange.Location = new System.Drawing.Point(13, 220);
            this.addRange.Name = "addRange";
            this.addRange.Size = new System.Drawing.Size(75, 24);
            this.addRange.TabIndex = 9;
            this.addRange.Text = "Pridaj test";
            this.addRange.UseVisualStyleBackColor = true;
            this.addRange.Click += new System.EventHandler(this.addRange_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Test";
            // 
            // rangeName
            // 
            this.rangeName.Enabled = false;
            this.rangeName.Location = new System.Drawing.Point(253, 67);
            this.rangeName.MaxLength = 10;
            this.rangeName.Name = "rangeName";
            this.rangeName.Size = new System.Drawing.Size(121, 20);
            this.rangeName.TabIndex = 5;
            this.rangeName.TextChanged += new System.EventHandler(this.rangeName_TextChanged);
            // 
            // timerSaveRanges
            // 
            this.timerSaveRanges.Interval = 1000;
            this.timerSaveRanges.Tick += new System.EventHandler(this.timerSaveRangeChanges_Tick);
            // 
            // frmEditRanges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 252);
            this.Controls.Add(this.rangeName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.deleteRange);
            this.Controls.Add(this.addRange);
            this.Controls.Add(this.deleteType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rangeUnit);
            this.Controls.Add(this.rangeMin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rangeMax);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.typeRanges);
            this.Controls.Add(this.addType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.animalType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmEditRanges";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Rozsahy hodnôt testov pre druhy zvierat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox animalType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addType;
        private System.Windows.Forms.ListBox typeRanges;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rangeMax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox rangeMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox rangeUnit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button deleteType;
        private System.Windows.Forms.Button deleteRange;
        private System.Windows.Forms.Button addRange;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox rangeName;
        private System.Windows.Forms.Timer timerSaveRanges;
    }
}