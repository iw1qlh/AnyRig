using AnyRigLibrary.Models;
using System;

namespace AnyRigLibrary
{
    public interface IRigCore
    {
        long Freq { get; set; }
        long FreqA { get; set; }
        long FreqB { get; set; }
        RigParam Mode { get; set; }
        int Pitch { get; set; }
        string RigType { get; }
        bool? Rit { get; set; }
        int RitOffset { get; set; }
        bool? Split { get; set; }
        bool? Tx { get; set; }
        RigParam Vfo { get; set; }
        bool? Xit { get; set; }

        void ClearRit();

        Action<int, RigParam[]> NotifyChanges { get; set; }

        void SendCustomCommand(string Command);
        void Start();
        void Stop();
    }
}

/*
                                                        RigCoreCommands
TRigParam   RigParam    IRigCore    type    RigData     Set             Get
---------   --------    --------    ----    -------     -----------------------
pmNone      UNKNOWN     -           -       -           -                   -

pmFreq      FREQ        Freq        long    Freq        F                   f
pmFreqA     FREQA       FreqA       long    FreqA       FA                  fa
pmFreqB     FREQB       FreqB       long    FreqB       FB                  fb

pmPitch     PITCH       Pitch       int     Pitch       set_level CWPITCH   get_level CWPITCH

pmRitOffset RITOFFSET   RitOffset   int     RitOffset   J                   j

pmRit0      RIT0        ClearRit()  function            clear_rit

pmVfoAA     VFOAA       Vfo         ...     Vfo         V                   v
pmVfoAB     VFOAB
pmVfoBA     VFOBA
pmVfoBB     VFOBB
pmVfoA      VFOA
pmVfoB      VFOB

pmVfoEqual  VFOEQUAL    -           -       -           -                   -
pmVfoSwap   VFOSWAP

pmSplitOn   SPLITON     Split       bool    Split       X                   x
pmSplitOff  SPLITOFF 

pmRitOn     RITON       Rit         bool    Rit         set_func RIT        get_func RIT
pmRitOff    RITOFF 

pmXitOn     XITON       Xit         bool    Xit         set_func XIT        get_func XIT
pmXitOff    XITOFF

pmRx        RX          Tx          bool    Tx          T                   t
pmTx        TX

pmCW_U      CW          Mode        ...     Mode        M                   m
pmCW_L      CWR
pmSSB_U     USB
pmSSB_L     LSB
pmDIG_U     DIGR
pmDIG_L     DIG
pmAM        AM
pmFM        FM

*/