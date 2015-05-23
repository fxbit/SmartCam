namespace MainForm
{
    partial class OpenSerialForm
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
            this.m_usertscts = new System.Windows.Forms.CheckBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_baud = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_comport = new System.Windows.Forms.ComboBox();
            this.bCancel = new System.Windows.Forms.Button();
            this.bConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_usertscts
            // 
            this.m_usertscts.AutoSize = true;
            this.m_usertscts.Location = new System.Drawing.Point(128, 184);
            this.m_usertscts.Name = "m_usertscts";
            this.m_usertscts.Size = new System.Drawing.Size(96, 17);
            this.m_usertscts.TabIndex = 88;
            this.m_usertscts.Text = "Use RTS/CTS";
            this.m_usertscts.UseVisualStyleBackColor = true;
            // 
            // comboBox6
            // 
            this.comboBox6.Enabled = false;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "8 bit"});
            this.comboBox6.Location = new System.Drawing.Point(128, 157);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(121, 21);
            this.comboBox6.TabIndex = 87;
            this.comboBox6.Text = "none";
            // 
            // comboBox5
            // 
            this.comboBox5.Enabled = false;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "8 bit"});
            this.comboBox5.Location = new System.Drawing.Point(128, 128);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(121, 21);
            this.comboBox5.TabIndex = 86;
            this.comboBox5.Text = "1 bit";
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "8 bit"});
            this.comboBox2.Location = new System.Drawing.Point(128, 99);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 85;
            this.comboBox2.Text = "none";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "8 bit"});
            this.comboBox1.Location = new System.Drawing.Point(128, 70);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 84;
            this.comboBox1.Text = "8 bit";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label11.Location = new System.Drawing.Point(35, 159);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 16);
            this.label11.TabIndex = 83;
            this.label11.Text = "Flow Control";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label10.Location = new System.Drawing.Point(35, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 16);
            this.label10.TabIndex = 82;
            this.label10.Text = "Stop";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label9.Location = new System.Drawing.Point(35, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 16);
            this.label9.TabIndex = 81;
            this.label9.Text = "Parity";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label8.Location = new System.Drawing.Point(35, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 16);
            this.label8.TabIndex = 80;
            this.label8.Text = "Data";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label5.Location = new System.Drawing.Point(35, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 79;
            this.label5.Text = "Baud Rate";
            // 
            // m_baud
            // 
            this.m_baud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_baud.FormattingEnabled = true;
            this.m_baud.Items.AddRange(new object[] {
            "921600",
            "115200",
            "57600",
            "38400",
            "19200",
            "14400",
            "9600"});
            this.m_baud.Location = new System.Drawing.Point(128, 41);
            this.m_baud.Name = "m_baud";
            this.m_baud.Size = new System.Drawing.Size(121, 21);
            this.m_baud.TabIndex = 78;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label6.Location = new System.Drawing.Point(35, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 16);
            this.label6.TabIndex = 77;
            this.label6.Text = "Serial Port";
            // 
            // m_comport
            // 
            this.m_comport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comport.FormattingEnabled = true;
            this.m_comport.Location = new System.Drawing.Point(128, 12);
            this.m_comport.Name = "m_comport";
            this.m_comport.Size = new System.Drawing.Size(121, 21);
            this.m_comport.TabIndex = 76;
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(174, 226);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 75;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bConnect
            // 
            this.bConnect.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bConnect.Location = new System.Drawing.Point(38, 226);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(75, 23);
            this.bConnect.TabIndex = 74;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // OpenSerialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.m_usertscts);
            this.Controls.Add(this.comboBox6);
            this.Controls.Add(this.comboBox5);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_baud);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_comport);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bConnect);
            this.Name = "OpenSerialForm";
            this.Text = "OpenSerialForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox m_usertscts;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox m_baud;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox m_comport;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bConnect;
    }
}