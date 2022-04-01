namespace AnyRigLibrary.Models
{
    public enum RigStatus
    {
        ST_NOTCONFIGURED = 0x00000000,
        ST_DISABLED = 0x00000001,
        ST_PORTBUSY = 0x00000002,
        ST_NOTRESPONDING = 0x00000003,
        ST_ONLINE = 0x00000004
    }
}
