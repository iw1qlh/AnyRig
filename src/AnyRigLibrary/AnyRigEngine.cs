using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using AnyRigLibrary.Models;

namespace AnyRigLibrary
{
    public class AnyRigEngine : IAnyRigEngine
    {

        AnyRigConfig config;
        RigCore[] rigs;

        public AnyRigEngine()
        {
            config = ConfigManager.Load();
            rigs = ConfigManager.LoadRigs(config);
        }

        public IRigCore GetRig(int nRig)
        {
            if (nRig > rigs.Length)
                return null;
            return rigs[nRig];
        }

        public List<RigBaseData> GetRigsList()
        {
            List<RigBaseData> rigList = new List<RigBaseData>();
            for (int i = 0; i < rigs.Length; i++)
            {
                rigList.Add(new RigBaseData
                {
                    RigIndex = i,
                    RigType = rigs[i].RigType,
                    IsOnLine = rigs[i].Status == RigStatus.ST_ONLINE
                });
            }

            return rigList;

        }

        public void OpenSettings()
        {
            string path = Path.Combine(config.ConfigExePath, "OmniRigConfig.exe");
            Process.Start(path);
        }

    }
}
