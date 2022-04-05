namespace AnyRigNetWrapper
{
    public class NetpipeRigWrapper : BaseRigWrapper
    {

        public NetpipeRigWrapper() : base()
        { }

        public NetpipeRigWrapper(int nRig) : base(nRig)
        { }

        public override BaseAnyRigCommandWrapper InitCommandWrapper() => new NetpipeCommandWrapper();

    }
}
