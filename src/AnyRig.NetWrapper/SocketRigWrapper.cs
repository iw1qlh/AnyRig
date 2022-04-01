namespace AnyRigNetWrapper
{
    public class SocketRigWrapper : BaseRigWrapper
    {
        public override BaseAnyRigCommandWrapper InitCommandWrapper() => new SocketCommandWrapper();

    }
}
