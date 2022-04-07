using System.Collections.Generic;
using AnyRigLibrary.Models;

namespace AnyRigLibrary
{
    public interface IAnyRigEngine
    {
        IRigCore GetRig(int nRig);
        List<RigBaseData> GetRigsList();
        void OpenSettings();
    }
}