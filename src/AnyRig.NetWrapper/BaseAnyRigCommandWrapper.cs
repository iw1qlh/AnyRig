using System;

namespace AnyRigNetWrapper
{
    public abstract class BaseAnyRigCommandWrapper
    {
        public Action<string> OnChange { get; set; }
        public Action<Exception> OnError { get; set; }

        public abstract string SendCommand(string command);
        public abstract void Start();
        public abstract void Stop();
    }
}
