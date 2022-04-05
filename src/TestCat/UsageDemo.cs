using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyRigLibrary;
using AnyRigLibrary.Models;
using AnyRigNetWrapper;

namespace TestCat;
internal class UsageDemo
{
    bool useLibrary, useSocket, useNetpipe;

    void Demo()
    {

        IRigCore rig = null;

        if (useLibrary)
        {
            AnyRigConfig config = ConfigManager.Load();
            RigCore[] rigs = ConfigManager.LoadRigs(config);

            if (rigs.Length > 0)
            {
                rig = rigs[0];
                rig.Start();
            }
        }
        else if (useSocket)
        {
            rig = new SocketRigWrapper(0);
        }
        else if (useNetpipe)
        {
            rig = new NetpipeRigWrapper(0);
        }

        rig.NotifyChanges = (rx, changed) => OnChanges(rx, changed);

        rig.Freq = 7100000;

    }

    private void OnChanges(int rx, RigParam[] changed)
    {
        throw new NotImplementedException();
    }

}
