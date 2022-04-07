namespace AnyRigNetWrapper
{
    public class SocketRigWrapper : BaseRigWrapper
    {

        public SocketRigWrapper() : base()
        { }

        public SocketRigWrapper(int nRig) : base(nRig)
        { 
        }

        public override BaseAnyRigCommandWrapper InitCommandWrapper() => new SocketCommandWrapper();

    }
}
