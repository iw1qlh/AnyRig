using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using AnyRigLibrary;
using AnyRigLibrary.Models;
using AnyRigNetWrapper;

namespace AnyRigNetWrapper
{
    public class SocketRigEngine : IAnyRigEngine
    {
        public IRigCore GetRig(int nRig)
        {
            return new SocketRigWrapper(nRig);
        }

        public List<RigBaseData> GetRigsList()
        {            
            List<RigBaseData> result = null;

            try
            {
                SocketCommandWrapper wrapper = new SocketCommandWrapper();
                string json = wrapper.SendCommand(RigCoreCommands.CMD_GET_RIG_LIST);
                int pos = json.IndexOf("=");
                if (pos >= 0)
                    result = JsonSerializer.Deserialize<List<RigBaseData>>(json.Substring(pos + 1));
            }
            catch { }

            return result;

        }

        public void OpenSettings() => throw new NotImplementedException();
    }
}
