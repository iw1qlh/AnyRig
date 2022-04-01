using AnyRigLibrary;
using AnyRigLibrary.Models;

namespace AnyRigService.Services
{
    public interface IRigsMachine
    {
        public bool Started { get; }

        AnyRigConfig GetConfig();
        RigCore[] GetRigs();
        void AddNotifyChanges(Action<int, RigParam[]> act);
        void Start();
        void RegisterCheckConnections(Func<bool> f);
    }
}