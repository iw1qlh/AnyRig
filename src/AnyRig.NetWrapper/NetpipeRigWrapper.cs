namespace AnyRigNetWrapper
{
    public class NetpipeRigWrapper : BaseRigWrapper
    {
        public override BaseAnyRigCommandWrapper InitCommandWrapper() => new NetpipeCommandWrapper();

    }
}
