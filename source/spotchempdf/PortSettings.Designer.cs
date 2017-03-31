namespace spotchempdf
{
    partial class frmSerialSettings
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
            this.portName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.baudRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataBits = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.parity = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.stopBits = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.flow = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rtsEnable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // portName
            // 
            this.portName.FormattingEnabled = true;
            this.portName.Location = new System.Drawing.Point(84, 10);
            this.portName.Name = "portName";
            this.portName.Size = new System.Drawing.Size(121, 21);
            this.portName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rýchlosť";
            // 
            // baudRate
            // 
            this.baudRate.Location = new System.Drawing.Point(84, 46);
            this.baudRate.Name = "baudRate";
            this.baudRate.ReadOnly = true;
            this.baudRate.Size = new System.Drawing.Size(121, 20);
            this.baudRate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Data-bity";
            // 
            // dataBits
            // 
            this.dataBits.Location = new System.Drawing.Point(84, 77);
            this.dataBits.Name = "dataBits";
            this.dataBits.ReadOnly = true;
            this.dataBits.Size = new System.Drawing.Size(121, 20);
            this.dataBits.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Parita";
            // 
            // parity
            // 
            this.parity.Location = new System.Drawing.Point(84, 110);
            this.parity.Name = "parity";
            this.parity.ReadOnly = true;
            this.parity.Size = new System.Drawing.Size(121, 20);
            this.parity.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Stop-bity";
            // 
            // stopBits
            // 
            this.stopBits.Location = new System.Drawing.Point(84, 143);
            this.stopBits.Name = "stopBits";
            this.stopBits.ReadOnly = true;
            this.stopBits.Size = new System.Drawing.Size(121, 20);
            this.stopBits.TabIndex = 9;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(16, 244);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(130, 244);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Zrušiť";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Flow";
            // 
            // flow
            // 
            this.flow.Location = new System.Drawing.Point(84, 178);
            this.flow.Name = "flow";
            this.flow.ReadOnly = true;
            this.flow.Size = new System.Drawing.Size(121, 20);
            this.flow.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 212);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "RTS Enable";
            // 
            // rtsEnable
            // 
            this.rtsEnable.AutoSize = true;
            this.rtsEnable.Enabled = false;
            this.rtsEnable.Location = new System.Drawing.Point(84, 212);
            this.rtsEnable.Name = "rtsEnable";
            this.rtsEnable.Size = new System.Drawing.Size(15, 14);
            this.rtsEnable.TabIndex = 15;
            this.rtsEnable.UseVisualStyleBackColor = true;
            // 
            // frmSerialSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(222, 282);
            this.Controls.Add(this.rtsEnable);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.flow);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.stopBits);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.parity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataBits);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.baudRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSerialSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Nastavenie spojenia";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox portName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox baudRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox dataBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox parity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox stopBits;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox flow;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox rtsEnable;
    }
}