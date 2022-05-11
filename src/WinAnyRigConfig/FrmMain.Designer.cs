using System.Windows.Forms;

namespace WinAnyRigConfig
{
    partial class FrmMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbRigs = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbEnabled = new System.Windows.Forms.CheckBox();
            this.ddlRigType = new System.Windows.Forms.ComboBox();
            this.ddlSerialPort = new System.Windows.Forms.ComboBox();
            this.ddlBaudRates = new System.Windows.Forms.ComboBox();
            this.ddlDataBits = new System.Windows.Forms.ComboBox();
            this.ddlParity = new System.Windows.Forms.ComboBox();
            this.ddlStopBits = new System.Windows.Forms.ComboBox();
            this.ddlRtsModes = new System.Windows.Forms.ComboBox();
            this.ddlDtrModes = new System.Windows.Forms.ComboBox();
            this.cbSendOnAir = new System.Windows.Forms.CheckBox();
            this.nudPoll = new System.Windows.Forms.NumericUpDown();
            this.nudTimeout = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbHrdUser = new System.Windows.Forms.TextBox();
            this.tbUploadCode = new System.Windows.Forms.TextBox();
            this.cbViewCode = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveHam365 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.cbSocketEnabled = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cbNetpipeEnabled = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.nudSocketPort = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.cbWebSocketEnabled = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.nudWebSocketPort = new System.Windows.Forms.NumericUpDown();
            this.btnSaveServices = new System.Windows.Forms.Button();
            this.btnRestartService = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRevert = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRigFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuIW1QLH = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAfreet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHam365 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSocketPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWebSocketPort)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(632, 401);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Controls.Add(this.tableLayoutPanel4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(624, 375);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Rigs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lbRigs, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(618, 369);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbRigs
            // 
            this.lbRigs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRigs.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbRigs.FormattingEnabled = true;
            this.lbRigs.ItemHeight = 16;
            this.lbRigs.Location = new System.Drawing.Point(3, 3);
            this.lbRigs.Name = "lbRigs";
            this.lbRigs.Size = new System.Drawing.Size(253, 334);
            this.lbRigs.TabIndex = 2;
            this.lbRigs.SelectedIndexChanged += new System.EventHandler(this.lbRigs_SelectedIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.label12, 0, 11);
            this.tableLayoutPanel2.Controls.Add(this.cbEnabled, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.ddlRigType, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.ddlSerialPort, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.ddlBaudRates, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.ddlDataBits, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.ddlParity, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.ddlStopBits, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.ddlRtsModes, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.ddlDtrModes, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.cbSendOnAir, 1, 11);
            this.tableLayoutPanel2.Controls.Add(this.nudPoll, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.nudTimeout, 1, 10);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(262, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 14;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(253, 334);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(101, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rig";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(76, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rig type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(96, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "Port";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(69, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 27);
            this.label4.TabIndex = 3;
            this.label4.Text = "Baud rate";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(73, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 27);
            this.label5.TabIndex = 4;
            this.label5.Text = "Data bits";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(88, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 27);
            this.label6.TabIndex = 5;
            this.label6.Text = "Parity";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(74, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 27);
            this.label7.TabIndex = 6;
            this.label7.Text = "Stop bits";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Location = new System.Drawing.Point(97, 185);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 27);
            this.label8.TabIndex = 7;
            this.label8.Text = "RTS";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Location = new System.Drawing.Point(96, 212);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 27);
            this.label9.TabIndex = 8;
            this.label9.Text = "DTR";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Location = new System.Drawing.Point(61, 239);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 27);
            this.label10.TabIndex = 9;
            this.label10.Text = "Poll int [ms]";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Location = new System.Drawing.Point(54, 266);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 27);
            this.label11.TabIndex = 10;
            this.label11.Text = "Timeout [ms]";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(86, 293);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 23);
            this.label12.TabIndex = 11;
            this.label12.Text = "On-air";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbEnabled
            // 
            this.cbEnabled.AutoSize = true;
            this.cbEnabled.Location = new System.Drawing.Point(129, 3);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(64, 17);
            this.cbEnabled.TabIndex = 12;
            this.cbEnabled.Text = "Enabled";
            this.cbEnabled.UseVisualStyleBackColor = true;
            // 
            // ddlRigType
            // 
            this.ddlRigType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlRigType.FormattingEnabled = true;
            this.ddlRigType.Location = new System.Drawing.Point(129, 26);
            this.ddlRigType.Name = "ddlRigType";
            this.ddlRigType.Size = new System.Drawing.Size(121, 21);
            this.ddlRigType.TabIndex = 13;
            // 
            // ddlSerialPort
            // 
            this.ddlSerialPort.FormattingEnabled = true;
            this.ddlSerialPort.Location = new System.Drawing.Point(129, 53);
            this.ddlSerialPort.Name = "ddlSerialPort";
            this.ddlSerialPort.Size = new System.Drawing.Size(121, 21);
            this.ddlSerialPort.TabIndex = 14;
            // 
            // ddlBaudRates
            // 
            this.ddlBaudRates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlBaudRates.FormattingEnabled = true;
            this.ddlBaudRates.Location = new System.Drawing.Point(129, 80);
            this.ddlBaudRates.Name = "ddlBaudRates";
            this.ddlBaudRates.Size = new System.Drawing.Size(121, 21);
            this.ddlBaudRates.TabIndex = 15;
            // 
            // ddlDataBits
            // 
            this.ddlDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDataBits.FormattingEnabled = true;
            this.ddlDataBits.Location = new System.Drawing.Point(129, 107);
            this.ddlDataBits.Name = "ddlDataBits";
            this.ddlDataBits.Size = new System.Drawing.Size(121, 21);
            this.ddlDataBits.TabIndex = 16;
            // 
            // ddlParity
            // 
            this.ddlParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlParity.FormattingEnabled = true;
            this.ddlParity.Location = new System.Drawing.Point(129, 134);
            this.ddlParity.Name = "ddlParity";
            this.ddlParity.Size = new System.Drawing.Size(121, 21);
            this.ddlParity.TabIndex = 17;
            // 
            // ddlStopBits
            // 
            this.ddlStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStopBits.FormattingEnabled = true;
            this.ddlStopBits.Location = new System.Drawing.Point(129, 161);
            this.ddlStopBits.Name = "ddlStopBits";
            this.ddlStopBits.Size = new System.Drawing.Size(121, 21);
            this.ddlStopBits.TabIndex = 18;
            // 
            // ddlRtsModes
            // 
            this.ddlRtsModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlRtsModes.FormattingEnabled = true;
            this.ddlRtsModes.Location = new System.Drawing.Point(129, 188);
            this.ddlRtsModes.Name = "ddlRtsModes";
            this.ddlRtsModes.Size = new System.Drawing.Size(121, 21);
            this.ddlRtsModes.TabIndex = 19;
            // 
            // ddlDtrModes
            // 
            this.ddlDtrModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDtrModes.FormattingEnabled = true;
            this.ddlDtrModes.Location = new System.Drawing.Point(129, 215);
            this.ddlDtrModes.Name = "ddlDtrModes";
            this.ddlDtrModes.Size = new System.Drawing.Size(121, 21);
            this.ddlDtrModes.TabIndex = 20;
            // 
            // cbSendOnAir
            // 
            this.cbSendOnAir.AutoSize = true;
            this.cbSendOnAir.Location = new System.Drawing.Point(129, 296);
            this.cbSendOnAir.Name = "cbSendOnAir";
            this.cbSendOnAir.Size = new System.Drawing.Size(64, 17);
            this.cbSendOnAir.TabIndex = 23;
            this.cbSendOnAir.Text = "Enabled";
            this.cbSendOnAir.UseVisualStyleBackColor = true;
            // 
            // nudPoll
            // 
            this.nudPoll.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPoll.Location = new System.Drawing.Point(129, 242);
            this.nudPoll.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudPoll.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudPoll.Name = "nudPoll";
            this.nudPoll.Size = new System.Drawing.Size(120, 21);
            this.nudPoll.TabIndex = 24;
            this.nudPoll.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // nudTimeout
            // 
            this.nudTimeout.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudTimeout.Location = new System.Drawing.Point(129, 269);
            this.nudTimeout.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudTimeout.Name = "nudTimeout";
            this.nudTimeout.Size = new System.Drawing.Size(120, 21);
            this.nudTimeout.TabIndex = 25;
            this.nudTimeout.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btnNew, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnDelete, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 343);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(253, 100);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(129, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnCancel, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(521, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(94, 334);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(3, 32);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Location = new System.Drawing.Point(772, 6);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(14, 100);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 375);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ham365.net";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.label13, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(786, 369);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.label13, 2);
            this.label13.Location = new System.Drawing.Point(3, 184);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(611, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "You can publish your realtime ON-AIR status on Ham365.net/HRDLOG.net, on your own" +
    " website or on your page on QRZ.com";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.label15, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.tbHrdUser, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.tbUploadCode, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.cbViewCode, 1, 2);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 4;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(387, 178);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(187, 27);
            this.label14.TabIndex = 0;
            this.label14.Text = "Ham365 user";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(3, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(187, 27);
            this.label15.TabIndex = 1;
            this.label15.Text = "Upload code";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbHrdUser
            // 
            this.tbHrdUser.Location = new System.Drawing.Point(196, 3);
            this.tbHrdUser.Name = "tbHrdUser";
            this.tbHrdUser.Size = new System.Drawing.Size(100, 21);
            this.tbHrdUser.TabIndex = 2;
            // 
            // tbUploadCode
            // 
            this.tbUploadCode.Location = new System.Drawing.Point(196, 30);
            this.tbUploadCode.Name = "tbUploadCode";
            this.tbUploadCode.PasswordChar = '*';
            this.tbUploadCode.Size = new System.Drawing.Size(100, 21);
            this.tbUploadCode.TabIndex = 3;
            // 
            // cbViewCode
            // 
            this.cbViewCode.AutoSize = true;
            this.cbViewCode.Location = new System.Drawing.Point(196, 57);
            this.cbViewCode.Name = "cbViewCode";
            this.cbViewCode.Size = new System.Drawing.Size(74, 17);
            this.cbViewCode.TabIndex = 4;
            this.cbViewCode.Text = "View code";
            this.cbViewCode.UseVisualStyleBackColor = true;
            this.cbViewCode.CheckedChanged += new System.EventHandler(this.cbViewCode_CheckedChanged);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.Controls.Add(this.btnSaveHam365, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(396, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(387, 178);
            this.tableLayoutPanel8.TabIndex = 2;
            // 
            // btnSaveHam365
            // 
            this.btnSaveHam365.Location = new System.Drawing.Point(309, 3);
            this.btnSaveHam365.Name = "btnSaveHam365";
            this.btnSaveHam365.Size = new System.Drawing.Size(75, 23);
            this.btnSaveHam365.TabIndex = 0;
            this.btnSaveHam365.Text = "Save";
            this.btnSaveHam365.UseVisualStyleBackColor = true;
            this.btnSaveHam365.Click += new System.EventHandler(this.btnSaveHam365_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel9);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 375);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Services";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 5;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.Controls.Add(this.label16, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.cbSocketEnabled, 2, 0);
            this.tableLayoutPanel9.Controls.Add(this.label17, 1, 4);
            this.tableLayoutPanel9.Controls.Add(this.cbNetpipeEnabled, 2, 4);
            this.tableLayoutPanel9.Controls.Add(this.label18, 1, 1);
            this.tableLayoutPanel9.Controls.Add(this.nudSocketPort, 2, 1);
            this.tableLayoutPanel9.Controls.Add(this.label19, 1, 6);
            this.tableLayoutPanel9.Controls.Add(this.cbWebSocketEnabled, 2, 6);
            this.tableLayoutPanel9.Controls.Add(this.label20, 1, 7);
            this.tableLayoutPanel9.Controls.Add(this.nudWebSocketPort, 2, 7);
            this.tableLayoutPanel9.Controls.Add(this.btnSaveServices, 4, 0);
            this.tableLayoutPanel9.Controls.Add(this.btnRestartService, 2, 9);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 10;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(786, 369);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label16.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label16.Location = new System.Drawing.Point(241, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 29);
            this.label16.TabIndex = 0;
            this.label16.Text = "TCP socket port";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbSocketEnabled
            // 
            this.cbSocketEnabled.AutoSize = true;
            this.cbSocketEnabled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbSocketEnabled.Location = new System.Drawing.Point(343, 3);
            this.cbSocketEnabled.Name = "cbSocketEnabled";
            this.cbSocketEnabled.Size = new System.Drawing.Size(120, 23);
            this.cbSocketEnabled.TabIndex = 4;
            this.cbSocketEnabled.Text = "Enabled";
            this.cbSocketEnabled.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label17.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label17.Location = new System.Drawing.Point(241, 76);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(96, 23);
            this.label17.TabIndex = 3;
            this.label17.Text = "Netpipe";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbNetpipeEnabled
            // 
            this.cbNetpipeEnabled.AutoSize = true;
            this.cbNetpipeEnabled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbNetpipeEnabled.Location = new System.Drawing.Point(343, 79);
            this.cbNetpipeEnabled.Name = "cbNetpipeEnabled";
            this.cbNetpipeEnabled.Size = new System.Drawing.Size(120, 17);
            this.cbNetpipeEnabled.TabIndex = 5;
            this.cbNetpipeEnabled.Text = "Enabled";
            this.cbNetpipeEnabled.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label18.Location = new System.Drawing.Point(241, 29);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(96, 27);
            this.label18.TabIndex = 7;
            this.label18.Text = "Port";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudSocketPort
            // 
            this.nudSocketPort.Location = new System.Drawing.Point(343, 32);
            this.nudSocketPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudSocketPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSocketPort.Name = "nudSocketPort";
            this.nudSocketPort.Size = new System.Drawing.Size(120, 21);
            this.nudSocketPort.TabIndex = 6;
            this.nudSocketPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label19.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label19.Location = new System.Drawing.Point(241, 119);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(96, 23);
            this.label19.TabIndex = 8;
            this.label19.Text = "WebSocket";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbWebSocketEnabled
            // 
            this.cbWebSocketEnabled.AutoSize = true;
            this.cbWebSocketEnabled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbWebSocketEnabled.Location = new System.Drawing.Point(343, 122);
            this.cbWebSocketEnabled.Name = "cbWebSocketEnabled";
            this.cbWebSocketEnabled.Size = new System.Drawing.Size(120, 17);
            this.cbWebSocketEnabled.TabIndex = 9;
            this.cbWebSocketEnabled.Text = "Enabled";
            this.cbWebSocketEnabled.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label20.Location = new System.Drawing.Point(241, 142);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(96, 27);
            this.label20.TabIndex = 10;
            this.label20.Text = "Port";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudWebSocketPort
            // 
            this.nudWebSocketPort.Location = new System.Drawing.Point(343, 145);
            this.nudWebSocketPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudWebSocketPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWebSocketPort.Name = "nudWebSocketPort";
            this.nudWebSocketPort.Size = new System.Drawing.Size(120, 21);
            this.nudWebSocketPort.TabIndex = 11;
            this.nudWebSocketPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnSaveServices
            // 
            this.btnSaveServices.Location = new System.Drawing.Point(707, 3);
            this.btnSaveServices.Name = "btnSaveServices";
            this.btnSaveServices.Size = new System.Drawing.Size(75, 23);
            this.btnSaveServices.TabIndex = 12;
            this.btnSaveServices.Text = "Save";
            this.btnSaveServices.UseVisualStyleBackColor = true;
            this.btnSaveServices.Click += new System.EventHandler(this.btnSaveServices_Click);
            // 
            // btnRestartService
            // 
            this.btnRestartService.AutoSize = true;
            this.btnRestartService.Location = new System.Drawing.Point(343, 192);
            this.btnRestartService.Name = "btnRestartService";
            this.btnRestartService.Size = new System.Drawing.Size(91, 23);
            this.btnRestartService.TabIndex = 13;
            this.btnRestartService.Text = "Restart Service";
            this.btnRestartService.UseVisualStyleBackColor = true;
            this.btnRestartService.Click += new System.EventHandler(this.btnRestartService_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(632, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Paint += new System.Windows.Forms.PaintEventHandler(this.menuStrip1_Paint);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRevert,
            this.toolStripSeparator1,
            this.mnuQuit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuRevert
            // 
            this.mnuRevert.Name = "mnuRevert";
            this.mnuRevert.Size = new System.Drawing.Size(107, 22);
            this.mnuRevert.Text = "Revert";
            this.mnuRevert.Click += new System.EventHandler(this.mnuRevert_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(104, 6);
            // 
            // mnuQuit
            // 
            this.mnuQuit.Name = "mnuQuit";
            this.mnuQuit.Size = new System.Drawing.Size(107, 22);
            this.mnuQuit.Text = "Quit";
            this.mnuQuit.Click += new System.EventHandler(this.mnuQuit_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout,
            this.mnuRigFolder,
            this.toolStripSeparator2,
            this.mnuIW1QLH,
            this.mnuAfreet,
            this.toolStripSeparator3,
            this.mnuHam365,
            this.toolStripSeparator4,
            this.mnuDonate});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(154, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuRigFolder
            // 
            this.mnuRigFolder.Name = "mnuRigFolder";
            this.mnuRigFolder.Size = new System.Drawing.Size(154, 22);
            this.mnuRigFolder.Text = "Open Rigs folder";
            this.mnuRigFolder.Click += new System.EventHandler(this.mnuRigFolder_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(151, 6);
            // 
            // mnuIW1QLH
            // 
            this.mnuIW1QLH.Name = "mnuIW1QLH";
            this.mnuIW1QLH.Size = new System.Drawing.Size(154, 22);
            this.mnuIW1QLH.Text = "IW1QLH";
            this.mnuIW1QLH.Click += new System.EventHandler(this.mnuIW1QLH_Click);
            // 
            // mnuAfreet
            // 
            this.mnuAfreet.Name = "mnuAfreet";
            this.mnuAfreet.Size = new System.Drawing.Size(154, 22);
            this.mnuAfreet.Text = "Original Omni-rig";
            this.mnuAfreet.Click += new System.EventHandler(this.mnuAfreet_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(151, 6);
            // 
            // mnuHam365
            // 
            this.mnuHam365.Name = "mnuHam365";
            this.mnuHam365.Size = new System.Drawing.Size(154, 22);
            this.mnuHam365.Text = "Ham365.com";
            this.mnuHam365.Click += new System.EventHandler(this.mnuHam365_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(151, 6);
            // 
            // mnuDonate
            // 
            this.mnuDonate.Name = "mnuDonate";
            this.mnuDonate.Size = new System.Drawing.Size(154, 22);
            this.mnuDonate.Text = "Donate";
            this.mnuDonate.Click += new System.EventHandler(this.mnuDonate_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 431);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(632, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(109, 17);
            this.lblStatus.Text = "toolStripStatusLabel1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FrmMain";
            this.Text = "AnyRig Configuration";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSocketPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWebSocketPort)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private StatusStrip statusStrip1;
        private TableLayoutPanel tableLayoutPanel1;
        private ListBox lbRigs;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ToolStripMenuItem mnuRevert;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mnuQuit;
        private ToolStripMenuItem mnuAbout;
        private ToolStripMenuItem mnuRigFolder;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem mnuIW1QLH;
        private ToolStripMenuItem mnuAfreet;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem mnuHam365;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem mnuDonate;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private CheckBox cbEnabled;
        private ComboBox ddlRigType;
        private ComboBox ddlSerialPort;
        private ComboBox ddlBaudRates;
        private ComboBox ddlDataBits;
        private ComboBox ddlParity;
        private ComboBox ddlStopBits;
        private ComboBox ddlRtsModes;
        private ComboBox ddlDtrModes;
        private CheckBox cbSendOnAir;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btnNew;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel4;
        private Button btnSave;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label13;
        private TableLayoutPanel tableLayoutPanel7;
        private Label label14;
        private Label label15;
        private TextBox tbHrdUser;
        private TextBox tbUploadCode;
        private CheckBox cbViewCode;
        private TableLayoutPanel tableLayoutPanel8;
        private Button btnSaveHam365;
        private TableLayoutPanel tableLayoutPanel9;
        private Label label16;
        private CheckBox cbSocketEnabled;
        private Label label17;
        private CheckBox cbNetpipeEnabled;
        private Label label18;
        private NumericUpDown nudSocketPort;
        private Label label19;
        private CheckBox cbWebSocketEnabled;
        private Label label20;
        private NumericUpDown nudWebSocketPort;
        private Button btnSaveServices;
        private Button btnDelete;
        private Button btnCancel;
        private ToolStripStatusLabel lblStatus;
        private NumericUpDown nudPoll;
        private NumericUpDown nudTimeout;
        private Button btnRestartService;
    }
}