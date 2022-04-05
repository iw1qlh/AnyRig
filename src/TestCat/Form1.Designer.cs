using AnyRigLibrary.Models;

namespace TestCat
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LblFreq = new System.Windows.Forms.Label();
            this.LblChanges = new System.Windows.Forms.Label();
            this.LblMode = new System.Windows.Forms.Label();
            this.BtnLsb = new System.Windows.Forms.Button();
            this.BtnCw = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.BtnUsb = new System.Windows.Forms.Button();
            this.BtnFm = new System.Windows.Forms.Button();
            this.BtnAm = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNetpipe = new System.Windows.Forms.RadioButton();
            this.rbSocket = new System.Windows.Forms.RadioButton();
            this.rbLibrary = new System.Windows.Forms.RadioButton();
            this.btnRit = new System.Windows.Forms.Button();
            this.btnXit = new System.Windows.Forms.Button();
            this.btnSplit = new System.Windows.Forms.Button();
            this.btnTx = new System.Windows.Forms.Button();
            this.lblFreqA = new System.Windows.Forms.Label();
            this.lblFreqB = new System.Windows.Forms.Label();
            this.btnVfoA = new System.Windows.Forms.Button();
            this.btnVfoB = new System.Windows.Forms.Button();
            this.btnClearRit = new System.Windows.Forms.Button();
            this.nudRit = new System.Windows.Forms.NumericUpDown();
            this.cbRadio = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRit)).BeginInit();
            this.SuspendLayout();
            // 
            // LblFreq
            // 
            this.LblFreq.Font = new System.Drawing.Font("Digital Readout Upright", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblFreq.ForeColor = System.Drawing.Color.DarkGreen;
            this.LblFreq.Location = new System.Drawing.Point(25, 24);
            this.LblFreq.Name = "LblFreq";
            this.LblFreq.Size = new System.Drawing.Size(158, 24);
            this.LblFreq.TabIndex = 0;
            this.LblFreq.Text = "29999";
            this.LblFreq.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LblChanges
            // 
            this.LblChanges.AutoSize = true;
            this.LblChanges.Location = new System.Drawing.Point(25, 298);
            this.LblChanges.Name = "LblChanges";
            this.LblChanges.Size = new System.Drawing.Size(35, 13);
            this.LblChanges.TabIndex = 2;
            this.LblChanges.Text = "label1";
            // 
            // LblMode
            // 
            this.LblMode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblMode.Location = new System.Drawing.Point(25, 81);
            this.LblMode.Name = "LblMode";
            this.LblMode.Size = new System.Drawing.Size(158, 21);
            this.LblMode.TabIndex = 1;
            this.LblMode.Text = "label1";
            this.LblMode.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BtnLsb
            // 
            this.BtnLsb.Location = new System.Drawing.Point(25, 159);
            this.BtnLsb.Name = "BtnLsb";
            this.BtnLsb.Size = new System.Drawing.Size(75, 23);
            this.BtnLsb.TabIndex = 3;
            this.BtnLsb.Tag = "";
            this.BtnLsb.Text = "LSB";
            this.BtnLsb.UseVisualStyleBackColor = true;
            this.BtnLsb.Click += new System.EventHandler(this.BtnMode_Click);
            // 
            // BtnCw
            // 
            this.BtnCw.Location = new System.Drawing.Point(25, 187);
            this.BtnCw.Name = "BtnCw";
            this.BtnCw.Size = new System.Drawing.Size(75, 23);
            this.BtnCw.TabIndex = 5;
            this.BtnCw.Text = "CW";
            this.BtnCw.UseVisualStyleBackColor = true;
            this.BtnCw.Click += new System.EventHandler(this.BtnMode_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BtnUsb
            // 
            this.BtnUsb.Location = new System.Drawing.Point(107, 159);
            this.BtnUsb.Name = "BtnUsb";
            this.BtnUsb.Size = new System.Drawing.Size(75, 23);
            this.BtnUsb.TabIndex = 4;
            this.BtnUsb.Text = "USB";
            this.BtnUsb.UseVisualStyleBackColor = true;
            this.BtnUsb.Click += new System.EventHandler(this.BtnMode_Click);
            // 
            // BtnFm
            // 
            this.BtnFm.Location = new System.Drawing.Point(25, 216);
            this.BtnFm.Name = "BtnFm";
            this.BtnFm.Size = new System.Drawing.Size(75, 23);
            this.BtnFm.TabIndex = 6;
            this.BtnFm.Text = "FM";
            this.BtnFm.UseVisualStyleBackColor = true;
            this.BtnFm.Click += new System.EventHandler(this.BtnMode_Click);
            // 
            // BtnAm
            // 
            this.BtnAm.Location = new System.Drawing.Point(107, 216);
            this.BtnAm.Name = "BtnAm";
            this.BtnAm.Size = new System.Drawing.Size(75, 23);
            this.BtnAm.TabIndex = 7;
            this.BtnAm.Text = "AM";
            this.BtnAm.UseVisualStyleBackColor = true;
            this.BtnAm.Click += new System.EventHandler(this.BtnMode_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbNetpipe);
            this.groupBox1.Controls.Add(this.rbSocket);
            this.groupBox1.Controls.Add(this.rbLibrary);
            this.groupBox1.Location = new System.Drawing.Point(228, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 96);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Method";
            // 
            // rbNetpipe
            // 
            this.rbNetpipe.AutoSize = true;
            this.rbNetpipe.Location = new System.Drawing.Point(6, 66);
            this.rbNetpipe.Name = "rbNetpipe";
            this.rbNetpipe.Size = new System.Drawing.Size(62, 17);
            this.rbNetpipe.TabIndex = 2;
            this.rbNetpipe.TabStop = true;
            this.rbNetpipe.Text = "Netpipe";
            this.rbNetpipe.UseVisualStyleBackColor = true;
            this.rbNetpipe.Click += new System.EventHandler(this.rbMethod_Click);
            // 
            // rbSocket
            // 
            this.rbSocket.AutoSize = true;
            this.rbSocket.Location = new System.Drawing.Point(6, 43);
            this.rbSocket.Name = "rbSocket";
            this.rbSocket.Size = new System.Drawing.Size(57, 17);
            this.rbSocket.TabIndex = 1;
            this.rbSocket.TabStop = true;
            this.rbSocket.Text = "Socket";
            this.rbSocket.UseVisualStyleBackColor = true;
            this.rbSocket.Click += new System.EventHandler(this.rbMethod_Click);
            // 
            // rbLibrary
            // 
            this.rbLibrary.AutoSize = true;
            this.rbLibrary.Location = new System.Drawing.Point(6, 20);
            this.rbLibrary.Name = "rbLibrary";
            this.rbLibrary.Size = new System.Drawing.Size(58, 17);
            this.rbLibrary.TabIndex = 0;
            this.rbLibrary.TabStop = true;
            this.rbLibrary.Text = "Library";
            this.rbLibrary.UseVisualStyleBackColor = true;
            this.rbLibrary.Click += new System.EventHandler(this.rbMethod_Click);
            // 
            // btnRit
            // 
            this.btnRit.Location = new System.Drawing.Point(25, 259);
            this.btnRit.Name = "btnRit";
            this.btnRit.Size = new System.Drawing.Size(64, 20);
            this.btnRit.TabIndex = 9;
            this.btnRit.Text = "RIT";
            this.btnRit.UseVisualStyleBackColor = true;
            this.btnRit.Click += new System.EventHandler(this.btnRit_Click);
            // 
            // btnXit
            // 
            this.btnXit.Location = new System.Drawing.Point(107, 259);
            this.btnXit.Name = "btnXit";
            this.btnXit.Size = new System.Drawing.Size(64, 20);
            this.btnXit.TabIndex = 10;
            this.btnXit.Text = "XIT";
            this.btnXit.UseVisualStyleBackColor = true;
            this.btnXit.Click += new System.EventHandler(this.btnXit_Click);
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(221, 187);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(64, 20);
            this.btnSplit.TabIndex = 11;
            this.btnSplit.Text = "SPLIT";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // btnTx
            // 
            this.btnTx.Location = new System.Drawing.Point(221, 216);
            this.btnTx.Name = "btnTx";
            this.btnTx.Size = new System.Drawing.Size(64, 20);
            this.btnTx.TabIndex = 12;
            this.btnTx.Text = "TX";
            this.btnTx.UseVisualStyleBackColor = true;
            this.btnTx.Click += new System.EventHandler(this.btnTx_Click);
            // 
            // lblFreqA
            // 
            this.lblFreqA.Font = new System.Drawing.Font("Digital Readout Upright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFreqA.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblFreqA.Location = new System.Drawing.Point(25, 58);
            this.lblFreqA.Name = "lblFreqA";
            this.lblFreqA.Size = new System.Drawing.Size(77, 11);
            this.lblFreqA.TabIndex = 13;
            this.lblFreqA.Text = "29999";
            this.lblFreqA.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblFreqB
            // 
            this.lblFreqB.Font = new System.Drawing.Font("Digital Readout Upright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFreqB.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblFreqB.Location = new System.Drawing.Point(105, 58);
            this.lblFreqB.Name = "lblFreqB";
            this.lblFreqB.Size = new System.Drawing.Size(77, 11);
            this.lblFreqB.TabIndex = 14;
            this.lblFreqB.Text = "29999";
            this.lblFreqB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnVfoA
            // 
            this.btnVfoA.Location = new System.Drawing.Point(221, 159);
            this.btnVfoA.Name = "btnVfoA";
            this.btnVfoA.Size = new System.Drawing.Size(64, 20);
            this.btnVfoA.TabIndex = 15;
            this.btnVfoA.Text = "VFO A";
            this.btnVfoA.UseVisualStyleBackColor = true;
            this.btnVfoA.Click += new System.EventHandler(this.btnVfoA_Click);
            // 
            // btnVfoB
            // 
            this.btnVfoB.Location = new System.Drawing.Point(291, 159);
            this.btnVfoB.Name = "btnVfoB";
            this.btnVfoB.Size = new System.Drawing.Size(64, 20);
            this.btnVfoB.TabIndex = 16;
            this.btnVfoB.Text = "VFO B";
            this.btnVfoB.UseVisualStyleBackColor = true;
            this.btnVfoB.Click += new System.EventHandler(this.btnVfoB_Click);
            // 
            // btnClearRit
            // 
            this.btnClearRit.Location = new System.Drawing.Point(269, 259);
            this.btnClearRit.Name = "btnClearRit";
            this.btnClearRit.Size = new System.Drawing.Size(64, 20);
            this.btnClearRit.TabIndex = 17;
            this.btnClearRit.Text = "CLEAR";
            this.btnClearRit.UseVisualStyleBackColor = true;
            this.btnClearRit.Click += new System.EventHandler(this.btnClearRit_Click);
            // 
            // nudRit
            // 
            this.nudRit.Location = new System.Drawing.Point(190, 259);
            this.nudRit.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudRit.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.nudRit.Name = "nudRit";
            this.nudRit.Size = new System.Drawing.Size(65, 21);
            this.nudRit.TabIndex = 18;
            this.nudRit.ValueChanged += new System.EventHandler(this.nudRit_ValueChanged);
            // 
            // cbRadio
            // 
            this.cbRadio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRadio.FormattingEnabled = true;
            this.cbRadio.Location = new System.Drawing.Point(228, 114);
            this.cbRadio.Name = "cbRadio";
            this.cbRadio.Size = new System.Drawing.Size(116, 21);
            this.cbRadio.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 332);
            this.Controls.Add(this.cbRadio);
            this.Controls.Add(this.nudRit);
            this.Controls.Add(this.btnClearRit);
            this.Controls.Add(this.btnVfoB);
            this.Controls.Add(this.btnVfoA);
            this.Controls.Add(this.lblFreqB);
            this.Controls.Add(this.lblFreqA);
            this.Controls.Add(this.btnTx);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.btnXit);
            this.Controls.Add(this.btnRit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnAm);
            this.Controls.Add(this.BtnFm);
            this.Controls.Add(this.BtnUsb);
            this.Controls.Add(this.BtnCw);
            this.Controls.Add(this.BtnLsb);
            this.Controls.Add(this.LblMode);
            this.Controls.Add(this.LblChanges);
            this.Controls.Add(this.LblFreq);
            this.Name = "Form1";
            this.Text = "TestCat (AnyRigLibrary)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label LblFreq;
        private Label LblChanges;
        private Label LblMode;
        private Button BtnLsb;
        private Button BtnCw;
        private System.Windows.Forms.Timer timer1;
        private Button BtnUsb;
        private Button BtnFm;
        private Button BtnAm;
        private GroupBox groupBox1;
        private RadioButton rbNetpipe;
        private RadioButton rbSocket;
        private RadioButton rbLibrary;
        private Button btnRit;
        private Button btnXit;
        private Button btnSplit;
        private Button btnTx;
        private Label lblFreqA;
        private Label lblFreqB;
        private Button btnVfoA;
        private Button btnVfoB;
        private Button btnClearRit;
        private NumericUpDown nudRit;
        private ComboBox cbRadio;
    }
}