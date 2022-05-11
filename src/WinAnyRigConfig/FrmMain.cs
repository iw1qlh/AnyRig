using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using AnyRigBase.Helpers;
using AnyRigBase.Services;
using AnyRigConfigCommon;
using AnyRigLibrary;
using WinAnyRigConfig.Helpers;

namespace WinAnyRigConfig
{
    public partial class FrmMain : Form
    {

        AnyRigLibrary.Models.AnyRigConfig config;

        bool adding = false;
        bool modified = false;

        public FrmMain()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData() 
        {
            string[] rigTypes = Directory.GetFiles(PathHelpers.GetRigsFolder(), "*.ini");
            var rigTypesList = rigTypes.Select(s => Path.GetFileNameWithoutExtension(s)).ToArray();
            ddlRigType.Items.AddRange(rigTypesList);

            var ports = SerialPort.GetPortNames().ToArray();
            ddlSerialPort.Items.AddRange(ports);

            var baudrates = CommPort.SupportedBaudRates.Select(s => s.ToString()).ToArray();
            ddlBaudRates.Items.AddRange(baudrates);

            var databits = CommPort.SupportedDataBits.Select(s => s.ToString()).ToArray();
            ddlDataBits.Items.AddRange(databits);

            var parities = CommPort.SupportedParities.ToArray();
            ddlParity.Items.AddRange(parities);

            var stopBits = CommPort.SupportedStopBits.ToArray();
            ddlStopBits.Items.AddRange(stopBits);

            var rtsModes = CommPort.SupportedRtsMode.ToArray();
            ddlRtsModes.Items.AddRange(rtsModes);

            var dtrModes = CommPort.SupportedDtrMode.ToArray();
            ddlDtrModes.Items.AddRange(dtrModes);

            config = ConfigManager.Load();

            Reload();

            ConfigCommon.BackupConfig();

            adding = false;
            lbRigs.SelectedItem = 0;
            SetViews();

        }

        private string[] GetRigList()
        {
            return config.Rigs.Select(s => $"{(s.Enabled ? "+" : "-")} {s.RigType} ({s.CommName})").ToArray();
        }

        private void Reload()
        {
            lbRigs.Items.Clear();
            lbRigs.Items.AddRange(GetRigList());

            tbHrdUser.Text = config.HrdUser ?? "TEST";
            tbUploadCode.Text = config.UploadCode ?? "0000000000";

            cbSocketEnabled.Checked = config.SocketEnabled;
            nudSocketPort.Value = config.SocketPort;
            cbNetpipeEnabled.Checked = config.NetpipeEnabled;
            cbWebSocketEnabled.Checked = config.WebSocketEnabled;
            nudWebSocketPort.Value = config.WebSocketPort;

            SetViews();

        }

        private void LoadRigSettings(AnyRigLibrary.Models.RigSettings rig)
        {
            cbEnabled.Checked = rig.Enabled;
            ddlRigType.Text = rig.RigType ?? "";

            ddlSerialPort.Text = "COM1";
            ddlBaudRates.Text = "9600";
            ddlParity.Text = "N";
            ddlDataBits.Text = "8";
            ddlStopBits.Text = "0";
            ddlRtsModes.Text = "H";
            ddlDtrModes.Text = "H";

            string[] p = rig.CommName.Split(',');
            for (int i = 0; i < p.Length; i++)
            {
                try
                {
                    switch (i)
                    {
                        case 0: ddlSerialPort.Text = p[i]; break;
                        case 1: ddlBaudRates.Text = p[i]; break;
                        case 2: ddlParity.Text = CommPort.SupportedParities.FirstOrDefault(w => w.StartsWith(p[i])); break;
                        case 3: ddlDataBits.Text = p[i]; break;
                        case 4: ddlStopBits.Text = p[i]; break;
                        case 5: ddlRtsModes.Text = CommPort.SupportedRtsMode.FirstOrDefault(w => w.StartsWith(p[i])); break;
                        case 6: ddlDtrModes.Text = CommPort.SupportedDtrMode.FirstOrDefault(w => w.StartsWith(p[i])); break;
                    }
                }
                catch { }
            }

            nudPoll.Value = rig.PollMs;
            nudTimeout.Value = rig.TimeoutMs;
            cbSendOnAir.Checked = rig.SendOnAir;

        }



        private void SetViews(string status = null)
        {
            if (lbRigs.SelectedIndex < 0)
                lbRigs.SelectedIndex = 0;

            if (lbRigs.Items.Count == 0)
            {
                var r = new AnyRigLibrary.Models.RigSettings();
                r.Enabled = true;
                LoadRigSettings(r);
                adding = true;
            }

            AnyRigLibrary.Models.RigSettings rig = null;
            if (config.Rigs.Length > lbRigs.SelectedIndex)
                rig = config.Rigs[lbRigs.SelectedIndex];

            btnNew.Enabled = (!adding && (config.Rigs.Count() < 10));
            btnDelete.Enabled = (!adding && (config.Rigs.Count() > 1) && (rig != null) && (!rig.Enabled || (config.Rigs.Count(w => w.Enabled) > 1)));
            btnSave.Text = adding ? "Add" : "Save";
            btnCancel.Visible = adding;

            cbSendOnAir.Enabled = !string.IsNullOrEmpty(config.HrdUser) && ((config.Rigs.Count(r => r.SendOnAir) == 0) || rig.SendOnAir);

            SetStatus(adding ? "Adding new Rig" : (status ?? ""));

        }

        private void SetStatus(string text)
        {
            statusStrip1.InvokeIfRequired(c => c.Items[0].Text = text);
        }

        private void SaveConfig()
        {
            config.ConfigExePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            ConfigManager.Save(config);

            int s = lbRigs.SelectedIndex;
            lbRigs.Items.Clear();
            lbRigs.Items.AddRange(GetRigList());
            lbRigs.SelectedIndex = s;

            adding = false;
            modified = true;
            SetViews("Configuration has been saved.");
            lbRigs.Focus();

        }


        private void mnuRigFolder_Click(object sender, EventArgs e)
        {
            string path = PathHelpers.GetRigsFolder();
            Process.Start("explorer.exe", path);
        }

        private void lbRigs_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnyRigLibrary.Models.RigSettings rig = config.Rigs[lbRigs.SelectedIndex];
            LoadRigSettings(rig);
            adding = false;
            SetViews();
        }

        private void mnuQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            adding = false;
            SetViews();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            adding = true;
            SetViews();
        }

        private void mnuAfreet_Click(object sender, EventArgs e)
        {
            BrowserHelpers.OpenBrowser(ConfigCommon.AFREET_URL);
        }

        private void mnuDonate_Click(object sender, EventArgs e)
        {
            BrowserHelpers.OpenBrowser(ConfigCommon.DONATE_URL);
        }

        private void mnuIW1QLH_Click(object sender, EventArgs e)
        {
            BrowserHelpers.OpenBrowser(ConfigCommon.IW1QLH_URL);
        }

        private void mnuHam365_Click(object sender, EventArgs e)
        {
            ConfigCommon.OpenHrdlogNet(config);
        }

        private void btnSaveHam365_Click(object sender, EventArgs e)
        {
            if (tbUploadCode.Text.Length != 10)
            {
                SetStatus("Upload code must be 10 characters long");
                return;
            }

            try
            {
                config.HrdUser = tbHrdUser.Text.ToString();
                config.UploadCode = tbUploadCode.Text.ToString();
                SaveConfig();
            }
            catch
            {
                SetStatus("ERROR!");
            }
        }

        private void cbViewCode_CheckedChanged(object sender, EventArgs e)
        {
            tbUploadCode.PasswordChar = cbViewCode.Checked ? (char)0 : '*';
        }

        private void btnSaveServices_Click(object sender, EventArgs e)
        {
            try
            {
                config.SocketPort = (int)nudSocketPort.Value;
                config.SocketEnabled = cbSocketEnabled.Checked;
                config.NetpipeEnabled = cbNetpipeEnabled.Checked;
                config.WebSocketPort = (int)nudWebSocketPort.Value;
                config.WebSocketEnabled = cbWebSocketEnabled.Checked;
                SaveConfig();
            }
            catch
            {
                SetStatus("ERROR!");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AnyRigLibrary.Models.RigSettings rig = new AnyRigLibrary.Models.RigSettings();

                rig.Enabled = cbEnabled.Checked;
                rig.RigType = ddlRigType.Text.ToString();
                rig.CommName = $"{ddlSerialPort.Text},{ddlBaudRates.Text},{ddlParity.Text.ToString().FirstOrDefault()},{ddlDataBits.Text},{ddlStopBits.Text},{ddlRtsModes.Text.ToString().FirstOrDefault()},{ddlDtrModes.Text.ToString().FirstOrDefault()}";
                rig.PollMs = (int)nudPoll.Value;
                rig.TimeoutMs = (int)nudTimeout.Value;
                rig.SendOnAir = cbSendOnAir.Checked;

                if (adding)
                {
                    var rigsList = config.Rigs.ToList();
                    rigsList.Add(rig);
                    config.Rigs = rigsList.ToArray();
                }
                else
                {
                    config.Rigs[lbRigs.SelectedIndex] = rig;
                }

                SaveConfig();

            }
            catch
            {
                SetStatus("ERROR!");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string rigName = lbRigs.SelectedItem.ToString();

                if (MessageBox.Show($"You are deleting \"{rigName}\". Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    var rigsList = config.Rigs.ToList();
                    rigsList.RemoveAt(lbRigs.SelectedIndex);
                    config.Rigs = rigsList.ToArray();

                    SaveConfig();

                }
            }
            catch { }

        }

        private void btnRestartService_Click(object sender, EventArgs e)
        {
            ConfigCommon.RestartWindowsService((status) => SetStatus(status));
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            var frm = new FrmAbout();
            frm.ShowDialog();
        }

        private void mnuRevert_Click(object sender, EventArgs e)
        {
            ConfigCommon.Revert(
                ref config,
                (title, message) => (MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes),
                (status) => SetStatus(status),
                () => Reload()
            );

        }

        private void menuStrip1_Paint(object sender, PaintEventArgs e)
        {
            mnuRevert.Enabled = modified;
        }
    }
}