namespace AnyRigNetWrapper
{
    public static class Extensions
    {
        public static string ToOnOff(this bool value)
        {
            return value ? "ON" : "OFF";
        }

    }
}
