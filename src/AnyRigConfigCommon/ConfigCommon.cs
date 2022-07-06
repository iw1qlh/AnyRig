using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AnyRigBase.Helpers;
using System.ServiceProcess;
using System.Security.Principal;
using AnyRigLibrary;
using System.Management;

namespace AnyRigConfigCommon
{
    public class ConfigCommon
    {
        public static string AFREET_URL = "http://www.dxatlas.com/omnirig/";
        public static string DONATE_URL = "http://www.iw1qlh.net/donate.php?id=2200";
        public static string IW1QLH_URL = "http://www.iw1qlh.net";

        public static void BackupConfig()
        {
            try
            {
                string bakFile = Path.Combine(PathHelpers.GetDataFolder(), "AnyRigLibrary.bak.json");
                File.Copy(PathHelpers.ConfigPath(), bakFile, overwrite: true);
            }
            catch { }
        }

        public static void CopyRigsFile()
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

        public static void OpenHrdlogNet(AnyRigLibrary.Models.AnyRigConfig config)
        {
            string url = "https://www.ham365.net";
            if (!string.IsNullOrEmpty(config.HrdUser))
                url = $"https://www.ham365.net/db/Logbook/{config.HrdUser}";
            BrowserHelpers.OpenBrowser(url);
        }

        public static void RestartWindowsService(Action<string> SetStatus)
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
                                controller.Stop();
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
                {
                    //MessageBox.Query("Warning", "You must \"Run as Administrator\" to start/stop services!", "Ok");
                    SetStatus("You must \"Run as Administrator\" to start/stop services!");
                }
            }
        }

        public static string GetServiceVersion()
        {
            string result = "Service not found";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                try
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Service");
                    ManagementObjectCollection collection = searcher.Get();
                    foreach (ManagementObject obj in collection)
                    {
                        if (obj["Name"] as string == "AnyRigService")
                        {
                            string exePath = obj["PathName"] as string;
                            result = "Service Ver " + FileVersionInfo.GetVersionInfo(exePath).ProductVersion;
                            break;
                        }
                    }
                }
                catch { }
            }

            return result;
        }

        public static void Revert(ref AnyRigLibrary.Models.AnyRigConfig config, Func<string, string, bool> Confirm, Action<string> SetStatus, Action Reload)
        {
            string bakFile = Path.Combine(PathHelpers.GetDataFolder(), "AnyRigLibrary.bak.json");
            if (File.Exists(bakFile) && Confirm("Revert to previous configuration", "Are you sure?"))
            {
                try
                {
                    File.Copy(bakFile, PathHelpers.ConfigPath(), overwrite: true);
                    config = ConfigManager.Load();

                    Reload();

                    SetStatus("Configuration has been reverted");

                }
                catch
                {
                    SetStatus("ERROR!");
                }

            }
        }



    }
}
