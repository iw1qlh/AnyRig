using System.Windows.Forms;

namespace WinAnyRigConfig
{
    partial class FrmAbout
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LblName = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.LblCopyright = new System.Windows.Forms.Label();
            this.LblVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblServiceVersion = new System.Windows.Forms.Label();
            this.PictureArs = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureArs)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 175);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(431, 74);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "This software is provided \'as-is\', without any express or implied warranty. In no" +
    " event will the authors be held liable for any damages arising from the use of t" +
    "his software.";
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(358, 277);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(88, 27);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.LblName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.linkLabel2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.LblCopyright, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.LblVersion, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.LblServiceVersion, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 14);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(279, 134);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // LblName
            // 
            this.LblName.AutoSize = true;
            this.LblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LblName.Location = new System.Drawing.Point(4, 0);
            this.LblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblName.Name = "LblName";
            this.LblName.Size = new System.Drawing.Size(101, 16);
            this.LblName.TabIndex = 0;
            this.LblName.Text = "ProductName";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(4, 110);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(92, 15);
            this.linkLabel2.TabIndex = 4;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "www.iw1qlh.net";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // LblCopyright
            // 
            this.LblCopyright.AutoSize = true;
            this.LblCopyright.Location = new System.Drawing.Point(4, 66);
            this.LblCopyright.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblCopyright.Name = "LblCopyright";
            this.LblCopyright.Size = new System.Drawing.Size(123, 15);
            this.LblCopyright.TabIndex = 2;
            this.LblCopyright.Text = "Copyright by IW1QLH";
            // 
            // LblVersion
            // 
            this.LblVersion.AutoSize = true;
            this.LblVersion.Location = new System.Drawing.Point(4, 22);
            this.LblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(22, 15);
            this.LblVersion.TabIndex = 6;
            this.LblVersion.Text = "???";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 88);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Translation by IW1QLH";
            // 
            // LblServiceVersion
            // 
            this.LblServiceVersion.AutoSize = true;
            this.LblServiceVersion.Location = new System.Drawing.Point(3, 44);
            this.LblServiceVersion.Name = "LblServiceVersion";
            this.LblServiceVersion.Size = new System.Drawing.Size(22, 15);
            this.LblServiceVersion.TabIndex = 8;
            this.LblServiceVersion.Text = "---";
            // 
            // PictureArs
            // 
            this.PictureArs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureArs.Image = global::WinAnyRigConfig.Properties.Resources.HRDL_icon_256;
            this.PictureArs.Location = new System.Drawing.Point(300, 14);
            this.PictureArs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PictureArs.Name = "PictureArs";
            this.PictureArs.Size = new System.Drawing.Size(146, 134);
            this.PictureArs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureArs.TabIndex = 3;
            this.PictureArs.TabStop = false;
            this.PictureArs.DoubleClick += new System.EventHandler(this.PictureArs_DoubleClick);
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(460, 317);
            this.Controls.Add(this.PictureArs);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureArs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label LblName;
        private System.Windows.Forms.Label LblCopyright;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.PictureBox PictureArs;
        private System.Windows.Forms.Label LblVersion;
        private Label label1;
        private Label LblServiceVersion;
    }
}