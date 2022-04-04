
/* Unmerged change from project 'AnyRigLibrary (net6.0)'
Before:
using System;
After:
using AnyRigLibrary.Models;
using AnyRigBase.Helpers;
using System;
*/
using AnyRigLibrary.Models;
using AnyRigBase.Helpers;
using System.Collections.Generic;

/* Unmerged change from project 'AnyRigLibrary (net6.0)'
Before:
using System.Threading.Tasks;
using System.Text.Json;
After:
using System.Text.Json;
*/
using System.IO;
using System.Text.Json
/* Unmerged change from project 'AnyRigLibrary (net6.0)'
Before:
using AnyRigLibrary.Models;
using AnyRigBase.Helpers;
After:
using System.Threading.Tasks;
*/
;

namespace AnyRigLibrary
{
    public class ConfigManager
    {
        public static AnyRigConfig Load()
        {
            AnyRigConfig result;

            string path = PathHelpers.ConfigPath();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                result = JsonSerializer.Deserialize<AnyRigConfig>(json);
            }
            else
            {
                result = new AnyRigConfig();
                result.SocketPort = 4532;
                result.WebSocketPort = 8081;
                result.Rigs = new RigSettings[0];
            }
            return result;
        }

        public static RigCore[] LoadRigs(AnyRigConfig config)
        {
            List<RigCore> result = new List<RigCore>();

            for (int i = 0; i < config.Rigs.Length; i++)
            {
                RigCore rig = new RigCore();
                rig.SetSettings(i, config);
                //rig.SetHrdlogCredentials(new HrdlogCredentials { Callsign = config.HrdUser, UploadCode = config.UploadCode });
                result.Add(rig);

                /*
                rig = new RigCoreX();
                rig.FRig = new TRig();
                rig.FRig.FRigCommands = new TRigCommands();
                rig.FRig.FRigCommands.FromIni(@"C:\Users\Claudio\Dropbox\Git-repos\clone-AnyRigLibrary\src\Rigs\TS-590.ini");

                rig.FRig.NotifyParams = (rx, changed) => OnChanges(rx, changed);

                rig.FRig.ComPort.ConfigurePort("COM3,115200,N,8,1");
                rig.FRig.ComPort.RtsMode = true;
                rig.FRig.ComPort.DtrMode = true;

                rig.Log += (text) => Console.WriteLine(text);
                rig.FRig.Log += (text) => Console.WriteLine(text);

                rig.FRig.ComPort.OpenPort();
                rig.FRig.Start();
                */

            }

            return result.ToArray();
        }

        /*
        public static void SaveRigs(AnyRigConfig config, RigCore[] rigs)
        {

            List<RigSettings> settings = new List<RigSettings>();

            foreach (var rig in rigs)
            {
                RigSettings rs = RigSettings.CreateFromRig(rig.FRig);
                settings.Add(rs);
            }

            config.Rigs = settings.ToArray();

        }
        */

        public static void Save(AnyRigConfig config)
        {

            // C:\Users\xxx\AppData\Roaming\IW1QLH\AnyRigLibrary\AnyRigLibrary.json
            string path = PathHelpers.ConfigPath();
            string json = JsonSerializer.Serialize(config);

            File.WriteAllText(path, json);

        }

    }

}

