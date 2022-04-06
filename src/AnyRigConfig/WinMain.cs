using AnyRigConfig.GuiExtensions;
using AnyRigConfig.Helpers;
using AnyRigLibrary;
using AnyRigBase.Helpers;
using AnyRigBase.Services;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using Terminal.Gui;
using System.Diagnostics;

namespace AnyRigConfig
{
    internal class WinMain : Window
    {
        MenuItem mnuRevert;

        CheckBox cbEnabled;
        DropDownList ddlRigType;
        DropDownList ddlSerialPort;
        DropDownList ddlBaudRates;
        DropDownList ddlDataBits;
        DropDownList ddlParity;
        DropDownList ddlStopBits;
        DropDownList ddlRtsModes;
        DropDownList ddlDtrModes;
        IntegerField ifPool;
        IntegerField ifTimeout;
        CheckBox cbSendOnAir;
        IntegerField ifSocketPort;
        CheckBox cbSocketEnabled;
        CheckBox cbNetpipeEnabled;
        IntegerField ifWebSocketPort;
        CheckBox cbWebSocketEnabled;
        ListView lvRigs;
        TextField tfHrdUser;
        TextField tfUploadCode;
        CheckBox cbViewCode;
        Button btnRestartService;
        Button btnNew;
        Button btnDelete;
        Button btnSave;
        Button btnCancel;
        Button btnSaveHrd;
        Button btnSaveSettings;
        StatusItem statusItem;

        bool adding = false;
        bool modified = false;

        AnyRigLibrary.Models.AnyRigConfig config;

        public WinMain(Toplevel top) : base("AnyRig configuration")
        {
            X = 0;
            Y = 1;
            top.Add(this);

            CopyRigsFile();

            // MENU

            mnuRevert = new MenuItem("_Revert...", "", () => { Revert_Clicked(); });
            mnuRevert.CanExecute = () => { return modified; };

            MenuBar menu = new MenuBar(new MenuBarItem[] {
				new MenuBarItem ("_File", new MenuItem [] {
					mnuRevert,
                    null,
					new MenuItem ("_Quit", "", () => { if (Quit ()) top.Running = false; })
				}),
				new MenuBarItem ("_Help", new MenuItem [] {
					new MenuItem ("_About", "", () => { About_Clicked(); }),
                    new MenuItem ("Open _rigs folder", "", () => { RigsFolder_Clicked(); }),
                    null,
					new MenuItem ("_IW1QLH", "", () => { IW1QLH_Clicked(); }),
                    new MenuItem ("_Original Omni-Rig", "", () => { Afreet_Clicked(); }),
                    null,
                    new MenuItem ("Ham365.net", "", () => { HrdlogNet_Clicked(); }),
                    null,
                    new MenuItem ("_Donate", "", () => { Donate_Clicked(); }),
				})
			});
			top.Add(menu);

            TabView tabView = new TabView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };


            // RIGS TAB

            View rigsView = new View()
            {
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            rigsView.Add(new Label(32, 3, "Rig type"));
            rigsView.Add(new Label(32, 4, "Port"));
            rigsView.Add(new Label(32, 5, "Baud rate"));
            rigsView.Add(new Label(32, 6, "Data bits"));
            rigsView.Add(new Label(32, 7, "Parity"));
            rigsView.Add(new Label(32, 8, "Stop bits"));
            rigsView.Add(new Label(32, 9, "RTS"));
            rigsView.Add(new Label(32, 10, "DTR"));
            rigsView.Add(new Label(32, 11, "Poll int [ms]"));
            rigsView.Add(new Label(32, 12, "Timeout [ms]"));

            cbEnabled = new CheckBox(46, 2, "Enabled");
            rigsView.Add(cbEnabled);

            string[] rigTypes = Directory.GetFiles(PathHelpers.GetRigsFolder(), "*.ini");
            var rigTypesList = rigTypes.Select(s => Path.GetFileNameWithoutExtension(s)).ToList();

            ddlRigType = new DropDownList(46, 3, 20, "Rig type");
            ddlRigType.Source = rigTypesList;
            rigsView.Add(ddlRigType);

            var ports = SerialPort.GetPortNames().ToList();
            ddlSerialPort = new DropDownList(46, 4, 20, "Port");
            ddlSerialPort.Source = ports;
            rigsView.Add(ddlSerialPort);

            List<string> baudrates = CommPort.SupportedBaudRates.Select(s => s.ToString()).ToList();
            ddlBaudRates = new DropDownList(46, 5, 20, "Baud rate");
            ddlBaudRates.Source = baudrates;
            rigsView.Add(ddlBaudRates);

            List<string> databits = CommPort.SupportedDataBits.Select(s => s.ToString()).ToList();
            ddlDataBits = new DropDownList(46, 6, 20, "Data bits");
            ddlDataBits.Source = databits;
            rigsView.Add(ddlDataBits);

            List<string> parities = CommPort.SupportedParities.ToList();
            ddlParity = new DropDownList(46, 7, 20, "Parity");
            ddlParity.Source = parities;
            rigsView.Add(ddlParity);

            List<string> stopBits = CommPort.SupportedStopBits.ToList();
            ddlStopBits = new DropDownList(46, 8, 20, "Stop bits");
            ddlStopBits.Source = stopBits;
            rigsView.Add(ddlStopBits);

            List<string> rtsModes = CommPort.SupportedRtsMode.ToList();
            ddlRtsModes = new DropDownList(46, 9, 20, "RTS");
            ddlRtsModes.Source = rtsModes;
            rigsView.Add(ddlRtsModes);

            List<string> dtrModes = CommPort.SupportedDtrMode.ToList();
            ddlDtrModes = new DropDownList(46, 10, 20, "DTR");
            ddlDtrModes.Source = dtrModes;
            rigsView.Add(ddlDtrModes);

            ifPool = new IntegerField() { X = 46, Y = 11, Width = 10 };
            rigsView.Add(ifPool);

            ifTimeout = new IntegerField() { X = 46, Y = 12, Width = 10 };
            rigsView.Add(ifTimeout);

            cbSendOnAir = new CheckBox(46, 13, "Send ON-AIR");
            rigsView.Add(cbSendOnAir);

            // LOAD CONFIGURATION
            config = ConfigManager.Load();            

            if (config.HrdUser == null)
                config.HrdUser = "TEST";
            if (config.UploadCode == null)
                config.UploadCode = "0000000000";

            List<string> rigs = GetRigList();

            FrameView frameView = new FrameView("Rig list")
            {
                X = 0,
                Y = 1,
                Width = 30,
                Height = 13,
                ColorScheme = Colors.Base
            };

            lvRigs = new ListView(rigs)
            {
                X = 0,
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Fill(),
            };
            lvRigs.MouseClick += (e) => { Listview_MouseClick(e); };
            lvRigs.SelectedItemChanged += (ListViewItemEventArgs e) => { RigChanged(e); };

            frameView.Add(lvRigs);
            rigsView.Add(frameView);

            btnNew = new Button(2, 14, "New");
            btnNew.Clicked += () => { BtnNew_Clicked(); };
            rigsView.Add(btnNew);

            btnDelete = new Button(12, 14, "Delete");
            btnDelete.Clicked += () => { BtnDelete_Clicked(); };
            rigsView.Add(btnDelete);

            btnSave = new Button(66, 2, "Save") { Width = 10 };
            btnSave.Clicked += () => { BtnSave_Clicked(); };
            rigsView.Add(btnSave);

            btnCancel = new Button(66, 4, "Cancel") { Width = 10 };
            btnCancel.Clicked += () => { BtnCancel_Clicked(); };
            rigsView.Add(btnCancel);

            statusItem = new StatusItem(Key.Null, "", null);
            var statusBar = new StatusBar(new StatusItem[] {
                statusItem
            });
            top.Add(statusBar);

            var rigsTab = new TabView.Tab("Rigs ", rigsView);
            tabView.AddTab(rigsTab, true);

            // HRDLOG.net

            View hrdView = new View()
            {
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            hrdView.Add(new Label(1, 1, "Ham365 user"));
            tfHrdUser = new TextField(18, 1, 12, "");
            tfHrdUser.Text = config.HrdUser ?? "TEST";
            hrdView.Add(tfHrdUser);

            hrdView.Add(new Label(1, 3, "Upload code"));
            tfUploadCode = new TextField(18, 3, 12, "") { Secret = true };
            tfUploadCode.Text = config.UploadCode ?? "0000000000";
            hrdView.Add(tfUploadCode);

            cbViewCode = new CheckBox(18, 4, "View Code");
            cbViewCode.Toggled += (previousChecked) => OnViewCodeToggled(previousChecked);
            hrdView.Add(cbViewCode);

            hrdView.Add(new Label(new Rect(1, 8, 74, 2), "You can publish your realtime ON-AIR status on Ham365/HRDLOG.net, on your own website or on your page on QRZ.com"));

            btnSaveHrd = new Button(66, 2, "Save") { Width = 10 };
            btnSaveHrd.Clicked += () => { BtnSaveHrd_Clicked(); };
            hrdView.Add(btnSaveHrd);

            var hrdTab = new TabView.Tab("Ham365.net ", hrdView);
            tabView.AddTab(hrdTab, false);


            // SERVICES TAB

            View servicesView = new View()
            {
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            servicesView.Add(new Label(1, 2, "TCP Socket port"));
            ifSocketPort = new IntegerField() { X = 18, Y = 2, Width = 10 };
            ifSocketPort.Value = config.SocketPort;
            servicesView.Add(ifSocketPort);
            cbSocketEnabled = new CheckBox(1, 3, "Enabled");
            cbSocketEnabled.Checked = config.SocketEnabled;
            servicesView.Add(cbSocketEnabled);

            cbNetpipeEnabled = new CheckBox(1, 5, "Netpipe Enabled");
            cbNetpipeEnabled.Checked = config.NetpipeEnabled;
            servicesView.Add(cbNetpipeEnabled);

            servicesView.Add(new Label(1, 7, "Web Socket port"));
            ifWebSocketPort = new IntegerField() { X = 18, Y = 7, Width = 10 };
            ifWebSocketPort.Value = config.WebSocketPort;
            servicesView.Add(ifWebSocketPort);
            cbWebSocketEnabled = new CheckBox(1, 8, "Enabled");
            cbWebSocketEnabled.Checked = config.WebSocketEnabled;
            servicesView.Add(cbWebSocketEnabled);

            btnRestartService = new Button(18, 10, "Restart service");
            btnRestartService.Clicked += () => { BtnRestartService_Clicked(); };
            btnRestartService.Enabled = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            servicesView.Add(btnRestartService);

            btnSaveSettings = new Button(66, 2, "Save") { Width = 10 };
            btnSaveSettings.Clicked += () => { BtnSaveSettings_Clicked(); };
            servicesView.Add(btnSaveSettings);

            var settingsTab = new TabView.Tab("Services ", servicesView);
            tabView.AddTab(settingsTab, false);

            this.Add(tabView);

            try
            {
                string bakFile = Path.Combine(PathHelpers.GetDataFolder(), "AnyRigLibrary.bak.json");
                File.Copy(PathHelpers.ConfigPath(), bakFile, overwrite: true);
            }
            catch { }

            adding = false;
            lvRigs.SelectedItem = 0;
            SetViews();            

        }

        private void CopyRigsFile()
        {
            try
            {
                string source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rigs");
                string dest = PathHelpers.GetRigsFolder();
                string[] files = Directory.GetFiles(source, "*.INI");
                foreach (string fileName in files)
                {
                    string dst = Path.Combine(dest, Path.GetFileName(fileName));
                    File.Copy(fileName, dst, overwrite: false);
                }
            }
            catch { }
        }

        private void RigsFolder_Clicked()
        {
            string path = PathHelpers.GetRigsFolder();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                Process.Start("explorer.exe", path);
            else
                MessageBox.Query("Open Rigs folder", path, "Ok");
        }

        private void BtnRestartService_Clicked()
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {

                if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
                {

                    ServiceController controller = new ServiceController("AnyRigService");

                    Task.Factory.StartNew(() =>
                    {
                        TimeSpan timeout = TimeSpan.FromSeconds(5);

                        try
                        {

                            if (controller.Status == ServiceControllerStatus.Running)
                            {
                                SetStatus("Stopping AnyRigService");
                                controller.Stop ();
                                controller.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                                SetStatus("AnyRigService stopped");
                                Thread.Sleep(1000);
                            }

                        }
                        catch (Exception ex)
                        {
                            SetStatus(ex.Message);
                            Thread.Sleep(5000);
                        }


                        try
                        {

                            if (controller.Status == ServiceControllerStatus.Stopped)
                            {
                                SetStatus("Starting AnyRigService");
                                controller.Start();
                                controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
                                SetStatus("AnyRigService started");
                                Thread.Sleep(1000);
                            }

                        }
                        catch (Exception ex)
                        {
                            SetStatus(ex.Message);
                            Thread.Sleep(5000);
                        }

                    });
                }
                else
                    MessageBox.Query("Warning", "You must \"Run as Administrator\" to start/stop services!", "Ok");

            }

        }

        private void BtnSaveHrd_Clicked()
        {
            if (tfUploadCode.Text.Length != 10)
            {
                SetStatus("Upload code must be 10 characters long");
                return;
            }

            try
            {
                config.HrdUser = tfHrdUser.Text.ToString();
                config.UploadCode = tfUploadCode.Text.ToString();
                SaveConfig();
            }
            catch
            {
                SetStatus("ERROR!");
            }

        }

        private void OnViewCodeToggled(bool previousChecked)
        {
            tfUploadCode.Secret = !cbViewCode.Checked;
        }

        private void BtnSaveSettings_Clicked()
        {
            try
            {
                config.SocketPort = ifSocketPort.Value;
                config.SocketEnabled = cbSocketEnabled.Checked;
                config.NetpipeEnabled = cbNetpipeEnabled.Checked;
                config.WebSocketPort = ifWebSocketPort.Value;
                config.WebSocketEnabled = cbWebSocketEnabled.Checked;
                SaveConfig();
            }
            catch
            {
                SetStatus("ERROR!");
            }
        }

        private void BtnDelete_Clicked()
        {
            try
            { 
                string rigName = lvRigs.Source.ToList()[lvRigs.SelectedItem].ToString();

                if (MessageBox.Query("Warning", $"You are deleting \"{rigName}\". Are you sure?", "Yes", "No") == 0)
                {

                    var rigsList = config.Rigs.ToList();
                    rigsList.RemoveAt(lvRigs.SelectedItem);
                    config.Rigs = rigsList.ToArray();

                    SaveConfig();

                }            
            }
            catch { }


        }

        private void BtnSave_Clicked()
        {
            try
            {
                AnyRigLibrary.Models.RigSettings rig = new AnyRigLibrary.Models.RigSettings();

                rig.Enabled = cbEnabled.Checked;
                rig.RigType = ddlRigType.Text.ToString();
                rig.Port = ddlSerialPort.Text.ToString();
                rig.BaudRate = int.Parse(ddlBaudRates.Text.ToString());
                rig.DataBits = int.Parse(ddlDataBits.Text.ToString());
                rig.Parity = ddlParity.Text.ToString().FirstOrDefault().ToString();
                rig.StopBits = ddlStopBits.Text.ToString();
                rig.RtsMode = ddlRtsModes.Text.ToString().FirstOrDefault().ToString();
                rig.DtrMode = ddlDtrModes.Text.ToString().StartsWith("H");
                rig.PollMs = ifPool.Value;
                rig.TimeoutMs = ifTimeout.Value;
                rig.SendOnAir = cbSendOnAir.Checked;

                if (adding)
                {
                    var rigsList = config.Rigs.ToList();
                    rigsList.Add(rig);
                    config.Rigs = rigsList.ToArray();
                }
                else
                {
                    config.Rigs[lvRigs.SelectedItem] = rig;
                }

                SaveConfig();

            }
            catch 
            {
                SetStatus("ERROR!");
            }

        }

        private void SaveConfig()
        {
            config.ConfigExePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            ConfigManager.Save(config);

            int s = lvRigs.SelectedItem;
            lvRigs.SetSource(GetRigList());
            lvRigs.SelectedItem = s;

            adding = false;
            modified = true;
            SetViews("Configuration has been saved.");
            lvRigs.SetFocus();

        }

        private List<string> GetRigList()
        {
            return config.Rigs.Select(s => $"{(s.Enabled ? Driver.Checked.ToString() : Driver.UnChecked.ToString()) } {s.RigType} ({s.Port})").ToList();
        }

        private void LoadRigSettings(AnyRigLibrary.Models.RigSettings rig)
        {
            cbEnabled.Checked = rig.Enabled;
            ddlRigType.Text = rig.RigType ?? "";
            ddlSerialPort.Text = rig.Port ?? "COM1";
            ddlBaudRates.Text = rig.BaudRate.ToString();
            ddlDataBits.Text = rig.DataBits.ToString();
            ddlParity.Text = CommPort.SupportedParities.FirstOrDefault(w => w.StartsWith(rig.Parity ?? "N"));
            ddlStopBits.Text = rig.StopBits ?? "0";
            ddlRtsModes.Text = CommPort.SupportedRtsMode.FirstOrDefault(w => w.StartsWith(rig.RtsMode ?? "H"));
            ddlDtrModes.Text = rig.DtrMode ? "High" : "Low";
            ifPool.Value = rig.PollMs;
            ifTimeout.Value = rig.TimeoutMs;
            cbSendOnAir.Checked = rig.SendOnAir;
        }

        private void RigChanged(ListViewItemEventArgs e)
        {
            AnyRigLibrary.Models.RigSettings rig = config.Rigs[e.Item];
            LoadRigSettings(rig);
        }


        private void BtnCancel_Clicked()
        {
            adding = false;
            SetViews();
        }

        private void Listview_MouseClick(MouseEventArgs e)
        {
            adding = false;
            SetViews();
        }

        private void BtnNew_Clicked()
        {
            adding = true;
            SetViews();
        }

        private void SetViews(string status = null)
        {
            if (lvRigs.SelectedItem < 0)
                lvRigs.SelectedItem = 0;

            if (lvRigs.Source.Count == 0)
            {
                var r = new AnyRigLibrary.Models.RigSettings();
                r.Enabled = true;
                LoadRigSettings(r);
                adding = true;
            }

            AnyRigLibrary.Models.RigSettings rig = null;
            if (config.Rigs.Length > lvRigs.SelectedItem)
                rig = config.Rigs[lvRigs.SelectedItem];

            btnNew.Enabled = (!adding && (config.Rigs.Count() < 10));
            btnDelete.Enabled = (!adding && (config.Rigs.Count() > 1) && (rig != null) && (!rig.Enabled || (config.Rigs.Count(w => w.Enabled) > 1)));
            btnSave.Text = adding ? "Add" : "Save";
            btnCancel.Visible = adding;

            cbSendOnAir.Enabled = !string.IsNullOrEmpty(config.HrdUser) && ((config.Rigs.Count(r => r.SendOnAir) == 0) || rig.SendOnAir);

            SetStatus(adding ? "Adding new Rig" : (status ?? ""));

        }

        private void SetStatus(string text)
        {
            Application.MainLoop.Invoke(() => {
                statusItem.Title = text;
            });
        }

        private void Revert_Clicked()
        {
            string bakFile = Path.Combine(PathHelpers.GetDataFolder(), "AnyRigLibrary.bak.json");
            if (File.Exists(bakFile) && MessageBox.Query("Revert to previous configuration", "Are you sure?", "Yes", "No") == 0)
            {
                try
                {
                    File.Copy(bakFile, PathHelpers.ConfigPath(), overwrite: true);
                    config = ConfigManager.Load();

                    lvRigs.SetSource(GetRigList());
                    ifSocketPort.Value = config.SocketPort;

                    SetStatus("Configuration has been reverted");

                }
                catch
                {
                    SetStatus("ERROR!");
                }

            }
        }

        private void About_Clicked()
        {
            DlgAbout w = new DlgAbout();
            Application.Run(w);
        }

        private void Afreet_Clicked()
        {
            BrowserHelpers.OpenBrowser("http://www.dxatlas.com/omnirig/");
        }

        private bool Quit()
        {
            return true;
        }

        private void Donate_Clicked()
        {
            BrowserHelpers.OpenBrowser("http://www.iw1qlh.net/donate.php?id=2200");
        }

        private void IW1QLH_Clicked()
        {
            BrowserHelpers.OpenBrowser("http://www.iw1qlh.net");
        }

        private void HrdlogNet_Clicked()
        {
            string url = "https://www.ham365.net";
            if (!string.IsNullOrEmpty(config.HrdUser))
                url = $"https://www.ham365.net/db/Logbook/{config.HrdUser}";
            BrowserHelpers.OpenBrowser(url);
        }



    }
}
