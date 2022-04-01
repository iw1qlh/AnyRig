namespace AnyRigLibrary.Models
{
    // see TRigParam in TRigCommands
    public enum RigParam
    {
        UNKNOWN = 0x00000001,
        FREQ = 0x00000002,
        FREQA = 0x00000004,
        FREQB = 0x00000008,
        PITCH = 0x00000010,
        RITOFFSET = 0x00000020,
        RIT0 = 0x00000040,
        VFOAA = 0x00000080,
        VFOAB = 0x00000100,
        VFOBA = 0x00000200,
        VFOBB = 0x00000400,
        VFOA = 0x00000800,
        VFOB = 0x00001000,
        VFOEQUAL = 0x00002000,
        VFOSWAP = 0x00004000,
        SPLITON = 0x00008000,
        SPLITOFF = 0x00010000,
        RITON = 0x00020000,
        RITOFF = 0x00040000,
        XITON = 0x00080000,
        XITOFF = 0x00100000,
        RX = 0x00200000,
        TX = 0x00400000,
        CW = 0x00800000,
        CWR = 0x01000000,
        USB = 0x02000000,
        LSB = 0x04000000,
        DIGR = 0x08000000,
        DIG = 0x10000000,
        AM = 0x20000000,
        FM = 0x40000000
    }
}
