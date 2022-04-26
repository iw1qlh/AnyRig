using HRDLibrary;
using AnyRigLibrary.Models;
using AnyRigBase;
using AnyRigBase.Helpers;
using AnyRigBase.Models;
using AnyRigBase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnyRigLibrary
{
    public class RigCore : IRigCore
    {
        private TRig FRig;
        public object Parent;

        private int rigNumber = 0;
        private AnyRigConfig config;
        //private RigSettings settings;
        private DateTime onAirTime;
        //private HrdlogCredentials hrdlogCredentials;
        private bool onair;
        private HrdProtocol protocol;

        //------------------------------------------------------------------------------
        //                              ctor
        //------------------------------------------------------------------------------

        public RigCore(BaseCommPort commPort = null)
        {
            FRig = new TRig(commPort);
            FRig.Tick = () => { FRig_Tick(); };
            FRig.NotifyParams = (rigNumber, Params) => { NotifyParams(rigNumber, Params); };
            FRig.Log = (text) => { FRig_Log(text); };

            onAirTime = DateTime.Now.AddSeconds(30);
        }

        public RigCore(int rigNumber, AnyRigConfig config, BaseCommPort commPort = null) : this(commPort)
        {
            SetSettings(rigNumber, config);
        }

        //------------------------------------------------------------------------------
        //                              properties
        //------------------------------------------------------------------------------

        public Action<string> Log { get; set; }

        public Action<string> InternalLog { get; set; }

        public Action<int, RigParam[]> NotifyChanges { get; set; }

        public RigSettings Settings { get => config.Rigs[rigNumber]; }

        public string RigType => Get_RigType();

        public long ReadableParams => Get_ReadableParams();

        public long WriteableParams => Get_WriteableParams();

        public RigStatus Status => Get_Status();

        public string StatusStr => Get_StatusStr();

        public long Freq { get => Get_Freq(); set { Set_Freq(value); } }
        public long FreqA { get => Get_FreqA(); set { Set_FreqA(value); } }
        public long FreqB { get => Get_FreqB(); set { Set_FreqB(value); } }
        public int RitOffset { get => Get_RitOffset(); set { Set_RitOffset(value); } }
        public int Pitch { get => Get_Pitch(); set { Set_Pitch(value); } }
        public RigParam Vfo { get => Get_Vfo(); set { Set_Vfo(value); } }
        public bool? Split { get => Get_Split(); set { Set_Split(value.GetValueOrDefault()); } }
        public bool? Rit { get => Get_Rit(); set { Set_Rit(value.GetValueOrDefault()); } }
        public bool? Xit { get => Get_Xit(); set { Set_Xit(value.GetValueOrDefault()); } }
        public bool? Tx { get => Get_Tx(); set { Set_Tx(value.GetValueOrDefault()); } }
        public RigParam Mode { get => Get_Mode(); set { Set_Mode(value); } }
        public bool OnAir { get => onair; }

        //------------------------------------------------------------------------------
        //                              system
        //------------------------------------------------------------------------------

        public void SetSettings(int rigNumber, AnyRigConfig config)
        {

            this.rigNumber = rigNumber;
            this.config = config;

            //config.Rigs[rigNumber].RunningRig = this;

            FRig.Stop();
            try
            {
                FRig.RigType = Settings.RigType;
                FRig.ComPort.CommName = Settings.CommName;
                /*
                FRig.ComPort.PortName = Settings.Port;
                FRig.ComPort.BaudRate = Settings.BaudRate;
                FRig.ComPort.DataBits = Settings.DataBits;
                FRig.ComPort.Parity = CommPort.ToParity(Settings.Parity);
                FRig.ComPort.StopBits = CommPort.ToStopBits(Settings.StopBits);
                FRig.ComPort.DtrMode = Settings.DtrMode;
                FRig.ComPort.RtsMode = Settings.RtsMode == "H";
                */
                FRig.PollMs = Settings.PollMs;
                FRig.TimeoutMs = Settings.TimeoutMs;
                FRig.RigNumber = rigNumber;

                protocol = null;
                if (Settings.SendOnAir && !string.IsNullOrEmpty(config.HrdUser) && (config.UploadCode.Length == 10))
                {
                    onair = true;
                    protocol = new HrdProtocol(config.HrdUser, config.UploadCode, "AnyRigLibrary");
                }
                    
            }
            finally
            {
                //FRig.Enabled = true;
            }

        }

        public void DisableOnAir()
        {
            onair = false;
        }

        /*
        public void SetHrdlogCredentials(HrdlogCredentials credentials)
        {
            this.hrdlogCredentials = credentials;
            if (!string.IsNullOrEmpty(credentials.Callsign) && (credentials.UploadCode.Length == 10))
                protocol = new HrdProtocol(credentials.Callsign, credentials.UploadCode, "AnyRigLibrary");
            else
                protocol = null;
        }
        */

        public string Get_RigType()
        {
            if (FRig?.RigCommands == null)
                return "NONE";

            return FRig.RigCommands.RigType;
        }

        public long Get_ReadableParams()
        {
            if (FRig?.RigCommands == null)
                return 0;
            return FRig.RigCommands.ParamsToInt(FRig.RigCommands.ReadableParams);
        }

        public long Get_WriteableParams()
        {
            if (FRig?.RigCommands == null)
                return 0;
            return FRig.RigCommands.ParamsToInt(FRig.RigCommands.WriteableParams);
        }

        public bool IsParamReadable(RigParam Param)
        {
            return (Get_ReadableParams() & (long)Param) != 0;
        }

        public bool IsParamWriteable(RigParam Param)
        {
            return (Get_WriteableParams() & (long)Param) != 0;
        }

        public RigStatus Get_Status()
        {
            return (RigStatus)FRig?.Status;
        }

        public string Get_StatusStr()
        {
            return FRig.GetStatusStr();
        }


        //------------------------------------------------------------------------------
        //                                 get
        //------------------------------------------------------------------------------
        public long Get_Freq()
        {
            return FRig.Freq;
        }

        public long Get_FreqA()
        {
            return FRig.FreqA;
        }

        public long Get_FreqB()
        {
            return FRig.FreqB;
        }

        public int Get_RitOffset()
        {
            return FRig.RitOffset;
        }

        public int Get_Pitch()
        {
            return FRig.Pitch;
        }

        public RigParam Get_Vfo()
        {
            return (RigParam)FRig.RigCommands.ParamToInt(FRig.Vfo);
        }


        public bool? Get_Split()
        {
            return FRig.RigCommands.ParamToBool(FRig.Split, TRigParam.pmSplitOn, TRigParam.pmSplitOff);
        }


        public bool? Get_Rit()
        {
            return FRig.RigCommands.ParamToBool(FRig.Rit, TRigParam.pmRitOn, TRigParam.pmRitOff);
        }

        public bool? Get_Xit()
        {
            return FRig.RigCommands.ParamToBool(FRig.Xit, TRigParam.pmXitOn, TRigParam.pmXitOff);
        }

        public bool? Get_Tx()
        {
            return FRig.RigCommands.ParamToBool(FRig.Tx, TRigParam.pmTx, TRigParam.pmRx);
        }

        public RigParam Get_Mode()
        {
            return (RigParam)FRig.RigCommands.ParamToInt(FRig.Mode);
        }

        //------------------------------------------------------------------------------
        //                                 set
        //------------------------------------------------------------------------------
        public void Set_Freq(long Value)
        {
            FRig.Freq = Value;
        }

        public void Set_FreqA(long Value)
        {
            FRig.FreqA = Value;
        }

        public void Set_FreqB(long Value)
        {
            FRig.FreqB = Value;
        }

        public void Set_RitOffset(int Value)
        {
            FRig.RitOffset = Value;
        }

        public void Set_Pitch(int Value)
        {
            FRig.Pitch = Value;
        }

        public void Set_Vfo(RigParam Value)
        {
            FRig.Vfo = FRig.RigCommands.IntToParam((long)Value);
        }

        public void Set_Split(bool Value)
        {
            Log?.Invoke($"RIG{FRig.RigNumber} Entering SetSplit");
            FRig.Split = Value ? TRigParam.pmSplitOn : TRigParam.pmSplitOff;
            Log?.Invoke($"RIG{FRig.RigNumber} Leaving SetSplit");
        }

        public void Set_Rit(bool Value)
        {
            FRig.Rit = Value ? TRigParam.pmRitOn : TRigParam.pmRitOff;
        }

        public void Set_Xit(bool Value)
        {
            FRig.Xit = Value ? TRigParam.pmXitOn : TRigParam.pmXitOff;
        }

        public void Set_Tx(bool Value)
        {
            FRig.Tx = Value ? TRigParam.pmTx : TRigParam.pmRx;
        }

        public void Set_Mode(RigParam Value)
        {

            List<TRigParam> WrParams;
            TRigParam NewMode;

            Log?.Invoke($"RIG{FRig.RigNumber} Entering SetMode");

            NewMode = FRig.RigCommands.IntToParam((long)Value);

            if (NewMode != FRig.LastWrittenMode)
            {

                FRig.LastWrittenMode = NewMode;
                WrParams = FRig.RigCommands.WriteableParams;

                //the best way to set mode for both VFO's
                if (WrParams.Contains(TRigParam.pmVfoSwap))
                {
                    FRig.ForceVfo(TRigParam.pmVfoSwap);
                    FRig.Mode = NewMode;
                    FRig.ForceVfo(TRigParam.pmVfoSwap);
                    FRig.Mode = NewMode;
                }

                //changes VFO selection as a side effect
                else if (WrParams.Contains(TRigParam.pmVfoB))
                {
                    FRig.ForceVfo(TRigParam.pmVfoB);
                    FRig.Mode = NewMode;
                    FRig.ForceVfo(TRigParam.pmVfoA);
                    FRig.Mode = NewMode;
                }

                //changes VFO selection as a side effect
                else if (WrParams.Contains(TRigParam.pmVfoBB))
                {
                    FRig.ForceVfo(TRigParam.pmVfoBB);
                    FRig.Mode = NewMode;
                    FRig.ForceVfo(TRigParam.pmVfoAA);
                    FRig.Mode = NewMode;
                }

                //changes the frequency of the other VFO as a side effect
                else if (WrParams.Contains(TRigParam.pmVfoEqual))
                {
                    FRig.Mode = NewMode;
                    FRig.ForceVfo(TRigParam.pmVfoEqual);
                }
            }

            //TODO siamo sicuri vada qui?
            //for the radios without VFO selection
            else
                FRig.Mode = NewMode;

            Log?.Invoke($"RIG{FRig.RigNumber} Leaving SetMode");
        }

        internal void ClearRigStatus()
        {
            FRig.ClearRigStatus();
        }


        //------------------------------------------------------------------------------
        //                                 methods
        //------------------------------------------------------------------------------
        public void ClearRit()
        {
            FRig.AddWriteCommand(TRigParam.pmRit0);
        }

        /*
        public List<RigBaseData> GetRigsList()
        {
            List<RigBaseData> result = new List<RigBaseData>();

            for (int i = 0; i < config.Rigs.Length; i++)
            {
                result.Add(new RigBaseData
                {
                    RigIndex = i,
                    RigType = config.Rigs[i].RunningRig.RigType,
                    IsOnLine = config.Rigs[i].RunningRig.Status == RigStatus.ST_ONLINE
                });
            }

            return result;

        }
        */

        public void SetSimplexMode(long Freq)
        {

            if (FRig.RigCommands == null)
                return;

            Log?.Invoke($"RIG{FRig.RigNumber} Entering SetSimplexMode");

            TRigParam[] WrParams = FRig.RigCommands.WriteableParams.ToArray();

            if (new TRigParam[] { TRigParam.pmFreqA, TRigParam.pmVfoAA }.Except(WrParams).Count() == 0)
            {

                FRig.ForceVfo(TRigParam.pmVfoAA);
                FRig.FreqA = Freq;
            }
            else if (new TRigParam[] { TRigParam.pmFreqA, TRigParam.pmVfoA, TRigParam.pmSplitOff }.Except(WrParams).Count() == 0)
            {
                FRig.ForceVfo(TRigParam.pmVfoA);
                FRig.FreqA = Freq;
            }
            else if (new TRigParam[] { TRigParam.pmFreq, TRigParam.pmVfoA, TRigParam.pmVfoB }.Except(WrParams).Count() == 0)
            {
                FRig.ForceVfo(TRigParam.pmVfoB);
                FRig.Freq = Freq;
                FRig.ForceVfo(TRigParam.pmVfoA);
                FRig.Freq = Freq;
            }
            if (new TRigParam[] { TRigParam.pmFreq, TRigParam.pmVfoEqual }.Except(WrParams).Count() == 0)
            {
                FRig.Freq = Freq;
                FRig.ForceVfo(TRigParam.pmVfoEqual);
            }
            if (new TRigParam[] { TRigParam.pmFreq, TRigParam.pmVfoSwap }.Except(WrParams).Count() == 0)
            {
                FRig.ForceVfo(TRigParam.pmVfoSwap);
                FRig.Freq = Freq;
                FRig.ForceVfo(TRigParam.pmVfoSwap);
                FRig.Freq = Freq;
            }

            //TODO RA6UAZ
            // Added by RA6UAZ for Icom Marine Radio NMEA Command
            /*
            else if ([pmFreq, pmFreqA, pmFreqB] - WrParams) = [pmFreqA]
            then
            begin
            FRig.Freq := Freq;
            FRig.FreqB := Freq;
            end
            */
            else if (new TRigParam[] { TRigParam.pmFreq }.Except(WrParams).Count() == 0)
            {
                FRig.Freq = Freq;
            }


            if (WrParams.Contains(TRigParam.pmSplitOff))
                FRig.Split = TRigParam.pmSplitOff;

            FRig.Rit = TRigParam.pmRitOff;
            FRig.Xit = TRigParam.pmXitOff;
            Log?.Invoke($"RIG{FRig.RigNumber} Leaving SetSimplexMode");
        }

        public void SetSplitMode(long RxFreq, long TxFreq)
        {
            if (FRig.RigCommands == null)
                return;

            Log?.Invoke($"RIG{FRig.RigNumber} Leaving SetSimplexMode");

            TRigParam[] WrParams = FRig.RigCommands.WriteableParams.ToArray();


            //set rx and tx frequencies and split
            if (new TRigParam[] { TRigParam.pmFreqA, TRigParam.pmFreqB, TRigParam.pmVfoAB }.Except(WrParams).Count() == 0)
            {
                //TS-570
                FRig.ForceVfo(TRigParam.pmVfoAB);
                FRig.FreqA = RxFreq;
                FRig.FreqB = TxFreq;
            }
            else if (new TRigParam[] { TRigParam.pmFreq, TRigParam.pmVfoEqual }.Except(WrParams).Count() == 0)
            {
                //IC-746
                FRig.Freq = TxFreq;
                FRig.ForceVfo(TRigParam.pmVfoEqual);
                FRig.Freq = RxFreq;
                FRig.Split = TRigParam.pmSplitOn;
            }
            else if (new TRigParam[] { TRigParam.pmVfoB, TRigParam.pmFreq, TRigParam.pmVfoA }.Except(WrParams).Count() == 0)
            {
                //FT-100D
                FRig.ForceVfo(TRigParam.pmVfoB);
                FRig.Freq = TxFreq;
                FRig.ForceVfo(TRigParam.pmVfoA);
                FRig.Freq = RxFreq;
                FRig.Split = TRigParam.pmSplitOn;
            }
            else if (new TRigParam[] { TRigParam.pmFreq, TRigParam.pmVfoSwap }.Except(WrParams).Count() == 0)
            {
                //Ft-817 ?
                FRig.ForceVfo(TRigParam.pmVfoSwap);
                FRig.Freq = TxFreq;
                FRig.ForceVfo(TRigParam.pmVfoSwap);
                FRig.Freq = RxFreq;
                FRig.Split = TRigParam.pmSplitOn;
            }
            else if (new TRigParam[] { TRigParam.pmFreqA, TRigParam.pmFreqB, TRigParam.pmVfoA }.Except(WrParams).Count() == 0)
            {
                //FT-1000 MP
                FRig.ForceVfo(TRigParam.pmVfoA);
                FRig.FreqA = RxFreq;
                FRig.FreqB = TxFreq;
            }
            // Added by RA6UAZ for Icom Marine Radio NMEA Command
            /*
            else if ([pmFreq, pmFreqA, pmFreqB] - WrParams) = [pmFreqA]
            then
            begin
            FRig.Freq := RxFreq;
            FRig.FreqB := TxFreq;
            end;
            */

            if (WrParams.Contains(TRigParam.pmSplitOn))
                FRig.Split = TRigParam.pmSplitOn;

            FRig.Rit = TRigParam.pmRitOff;
            FRig.Xit = TRigParam.pmXitOff;

            Log?.Invoke($"RIG{FRig.RigNumber} Leaving SetSplitMode");
        }

        public void SendCustomCommand(object Command, int ReplyLength, object ReplyEnd)
        {

            byte[] Cmd;
            string Trm = "";

            if (Command is byte[])
            {
                Cmd = Command as byte[];
            }
            else if (Command is string)
            {
                Cmd = ByteArray.StrToBytes(Command.ToString());
            }
            else
                return;

            if (ReplyEnd is byte[])
            {
                Trm = ByteArray.BytesToStr(ReplyEnd as byte[]);
            }
            else if (ReplyEnd is string)
            {
                Trm = ReplyEnd.ToString();
            }

            FRig.AddCustomCommand(Parent, Cmd, ReplyLength, Trm);

        }

        public void SendCustomCommand(string Command)
        {
            throw new NotImplementedException();
        }

        public long FrequencyOfTone(long Tone)
        {
            long Result;

            if (!FRig.Lock())
                return 0;

            try
            {
                Result = Tone;
                if ((FRig.Mode == TRigParam.pmCW_U) || (FRig.Mode == TRigParam.pmCW_L))
                    Result -= FRig.Pitch;

                if ((FRig.Mode == TRigParam.pmCW_L) || (FRig.Mode == TRigParam.pmSSB_L))
                    Result = -Result;
                Result += FRig.Freq;
            }
            finally
            {
                FRig.UnLock();
            }

            return Result;

        }

        public long GetRxFrequency()
        {
            long Result;

            if (FRig.RigCommands == null)
                return 0;

            Log?.Invoke($"RIG{FRig.RigNumber} Entering GetRxFrequency");

            List<TRigParam> RdParams = FRig.RigCommands.ReadableParams;

            FRig.Lock();
            try
            {
                if (RdParams.Contains(TRigParam.pmFreqA) && ((FRig.Vfo == TRigParam.pmVfoA) || (FRig.Vfo == TRigParam.pmVfoAA) || (FRig.Vfo == TRigParam.pmVfoAB)))
                    Result = FRig.FreqA;

                else if (RdParams.Contains(TRigParam.pmFreqB) && ((FRig.Vfo == TRigParam.pmVfoB) || (FRig.Vfo == TRigParam.pmVfoBA) || (FRig.Vfo == TRigParam.pmVfoBB)))
                    Result = FRig.FreqB;

                else if ((FRig.Tx != TRigParam.pmTx) || (FRig.Split != TRigParam.pmSplitOn))
                    Result = FRig.Freq;

                else
                    Result = 0;

                //include RIT
                if (FRig.Rit == TRigParam.pmRitOn)
                    Result += FRig.RitOffset;

            }
            finally
            {
                FRig.UnLock();
            }

            Log?.Invoke($"RIG{FRig.RigNumber} Leaving GetRxFrequency");

            return Result;

        }


        public long GetTxFrequency()
        {
            long Result;

            if (FRig.RigCommands == null)
                return 0;

            Log?.Invoke($"RIG{FRig.RigNumber} Entering GetTxFrequency");

            List<TRigParam> RdParams = FRig.RigCommands.ReadableParams;

            if (!FRig.Lock())
                return 0;

            try
            {
                if (RdParams.Contains(TRigParam.pmFreqA) && ((FRig.Vfo == TRigParam.pmVfoAA) || (FRig.Vfo == TRigParam.pmVfoBA)))
                    Result = FRig.FreqA;
                else if (RdParams.Contains(TRigParam.pmFreqB) && ((FRig.Vfo == TRigParam.pmVfoAB) || (FRig.Vfo == TRigParam.pmVfoBB)))
                    Result = FRig.FreqB;
                else if (FRig.Tx == TRigParam.pmTx)
                    Result = FRig.Freq;
                else
                    Result = 0;

                //include XIT
                if (FRig.Xit == TRigParam.pmXitOn)
                    Result += FRig.RitOffset;
            }
            finally
            {
                FRig.UnLock();
            }

            // MainForm.Log('RIG%d Leaving GetTxFrequency', [FRig.RigNumber]);

            return Result;
        }

        //------------------------------------------------------------------------------
        //                                 events
        //------------------------------------------------------------------------------

        private void NotifyParams(int rigNumber, long Params)
        {            
            var p = new List<RigParam>();
            foreach(RigParam e in Enum.GetValues(typeof(RigParam)))
                if ((Params & (long)e) > 0)
                    p.Add(e);

            NotifyChanges?.Invoke(rigNumber, p.ToArray());
        }


        //------------------------------------------------------------------------------
        //                                 timer tick
        //------------------------------------------------------------------------------

        private void FRig_Tick()
        {
            if (onair && (DateTime.Now > onAirTime) && (Freq > 0) && (protocol != null))
            {
                onAirTime = DateTime.Now.AddSeconds(75);    // expire after 90 seconds
                Task.Run(async () =>
                {
                    bool success = await protocol.SendOnAirAsync(Freq, Mode.ToString(), RigType);
                    if (!success)
                        Log?.Invoke("HRDLOG.net: SendonAir Error");
                });
            }
        }

        private void FRig_Log(string text)
        {
            InternalLog?.Invoke(text);
        }

        //------------------------------------------------------------------------------
        //                                 start / stop
        //------------------------------------------------------------------------------

        public void Start()
        {
            FRig.Start();
        }

        public void Stop()
        {
            FRig.Stop();
        }

        //TODO IPortBits
        /*
        IPortBits Get_PortBits()
        {
            return FPortBits;
        }
        */

    }

}
