using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyRigNetWrapper;
using AnyRigLibrary;
using AnyRigLibrary.Models;

namespace TestCat;
internal class UsageDemo
{
    bool useLibrary, useSocket, useNetpipe;

    void Demo()
    {

        IAnyRigEngine engine = null;
        IRigCore rig = null;

        if (useLibrary)
        {
            engine = new AnyRigEngine();
        }
        else if (useSocket)
        {
            engine = new SocketRigEngine();
        }
        else if (useNetpipe)
        {
            engine = new NetpipeRigEngine();
        }

        rig = engine.GetRig(0);

        rig.NotifyChanges = (rx, changed) => OnChanges(rx, changed);

        rig.Freq = 7100000;

    }

    private void OnChanges(int rx, RigParam[] changed)
    {
        throw new NotImplementedException();
    }

}
