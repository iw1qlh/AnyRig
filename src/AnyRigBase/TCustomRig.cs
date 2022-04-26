using AnyRigBase.Helpers;
using AnyRigBase.Models;
using AnyRigBase.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AnyRigBase
{
    public enum TRigCtlStatus { stNotConfigured, stDisabled, stPortBusy, stNotResponding, stOnLine };

    public abstract class TCustomRig
    {
        // TODO WM_USER
        const int WM_USER = 0;  
        const int MAX_TIMEOUT = 6;
        //const int WM_TXQUEUE = WM_USER + 1;
        const int WM_COMSTATUS = WM_USER + 2;
        const int WM_COMPARAMS = WM_USER + 3;
        const int WM_COMCUSTOM = WM_USER + 4;

        static readonly DateTime NEVER = DateTime.MaxValue;

        private bool FEnabled;
        private bool FOnline;
        //private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        public TRigCommands FRigCommands; //TODO protected
        private DateTime FNextStatusTime, FDeadLineTime;

        //private Task TimerTask;
        private bool TaskCancelled;

        protected TCommandQueue FQueue;
        protected long FFreq;
        protected long FFreqA;
        protected long FFreqB;
        protected int FRitOffset;
        protected int FPitch;
        protected TRigParam FVfo;
        protected TRigParam FSplit;
        protected TRigParam FRit;
        protected TRigParam FXit;
        protected TRigParam FTx;
        protected TRigParam FMode;

        protected abstract void AddCommands(List<TRigCommand> ACmds, TCommandKind AKind);
        protected abstract void ProcessInitReply(int ANumber, byte[] AData);
        protected abstract void ProcessStatusReply(int ANumber, byte[] AData);
        protected abstract void ProcessWriteReply(TRigParam AParam, byte[] AData);
        protected abstract void ProcessCustomReply(object ASender, byte[] ACode, byte[] AData);

        public string RigType 
        { 
            get => FRigCommands.RigType; 
            set { Set_RigType(value); }
        }

        private void Set_RigType(string value)
        {
            if (FRigCommands == null)
            {
                FRigCommands = new TRigCommands();                
            }
            FRigCommands.FromIni(Path.Combine(PathHelpers.GetRigsFolder(), $"{value}.ini"));
        }

        public int RigNumber;
        public int PollMs;
        public int TimeoutMs;
        public BaseCommPort ComPort;
        public TRigParam LastWrittenMode;

        public abstract void AddWriteCommand(TRigParam AParam, long Value = 0);
        public abstract void AddCustomCommand(object ASender, byte[] ACode, int ALen, string AEnd);

        public Action<string> Log { get; set; }
        public Action<int> NotifyStatus { get; set; }
        public Action<int> NotifyRigType { get; set; }

        public TRigCommands RigCommands
        {
            get => FRigCommands;
            set { SetRigCommands(value); }
        }

        public bool Enabled
        {
            get => FEnabled;
            set { SetEnabled(value); }
        }

        public TRigCtlStatus Status
        {
            get => GetStatus();
        }

        //current rig parameters
        public long Freq
        {
            get => FFreq;
            set { SetFreq(value); }
        }

        public long FreqA
        {
            get => FFreqA;
            set { SetFreqA(value); }
        }

        public long FreqB
        {
            get => FFreqB;
            set { SetFreqB(value); }
        }

        public int Pitch
        {
            get => FPitch;
            set { SetPitch(value); }
        }

        public int RitOffset
        {
            get => FRitOffset;
            set { SetRitOffset(value); }
        }

        public TRigParam Vfo
        {
            get => FVfo;
            set { SetVfo(value); }
        }

        public TRigParam Split
        {
            get => GetSplit();
            set { SetSplit(value); }
        }

        public TRigParam Rit
        {
            get => FRit;
            set { SetRit(value); }
        }

        public TRigParam Xit
        {
            get => FXit;
            set { SetXit(value); }
        }

        public TRigParam Tx
        {
            get => FTx;
            set { SetTx(value); }
        }

        public TRigParam Mode
        {
            get => FMode;
            set { SetMode(value); }
        }

        public TCustomRig(BaseCommPort commPort = null)
        {
            
            FQueue = new TCommandQueue();
            
            ComPort = commPort ?? new CommPort();
            ComPort.OnReceived = (sender) => RecvEvent(sender);
            ComPort.OnSent = (sender) => SentEvent(sender);
            ComPort.OnChanges = (sender, pinChange) => CtsDsrEvent(sender, pinChange);
            ComPort.OnError = (sender, error) => Log?.Invoke($"SerialPort error: {error}");

            PollMs = 500;
            TimeoutMs = 3000;

        }

        ~TCustomRig()
        {
            TaskCancelled = true;
        }

        //------------------------------------------------------------------------------
        //                                 status
        //------------------------------------------------------------------------------

        private TRigCtlStatus GetStatus()
        {
            if (!Lock())
                return TRigCtlStatus.stNotResponding;

            try
            {
                if (RigCommands == null)
                    return TRigCtlStatus.stNotConfigured;
                else if (!FEnabled)
                    return TRigCtlStatus.stDisabled;
                else if (!ComPort.Open)
                    return TRigCtlStatus.stPortBusy;
                else if (!FOnline)
                    return TRigCtlStatus.stNotResponding;
                else return TRigCtlStatus.stOnLine;
            }
            finally
            {
                UnLock();
            }
        }

        readonly string[] statusStr = { "Rig is not configured", "Rig is disabled", "Port is not available", "Rig is not responding", "On-line" };
        public string GetStatusStr()
        {
            return statusStr[(int)(GetStatus())];
        }

        public void ClearRigStatus()
        {
            FFreq = 0;
            FFreqA = 0;
            FFreqB = 0;
            FRitOffset = 0;
            FPitch = 0;
            FVfo = TRigParam.pmNone;
            FSplit = TRigParam.pmNone;
            FRit = TRigParam.pmNone;
            FXit = TRigParam.pmNone;
            FTx = TRigParam.pmNone;
            FMode = TRigParam.pmNone;
        }

        //------------------------------------------------------------------------------
        //                                 Comm port
        //------------------------------------------------------------------------------
        private void SetEnabled(bool Value)
        {
            if (FEnabled == Value)
                return;

            //check for valid RigCommands
            if (Value && (RigCommands == null))
                return;

            if (Value)
                Start();
            else
                Stop();

            NotifyStatus?.Invoke(RigNumber);
            LastWrittenMode = TRigParam.pmNone;
        }

        public void Start()
        {

            if (RigCommands == null)
                return;

            Log?.Invoke($"Starting RIG{RigNumber}");

            if (!Lock())
                return;

            try
            {
                if (FEnabled)
                    return;

                FEnabled = true;
                FQueue.Clear();
                FQueue.Phase = TExchangePhase.phIdle;
                FDeadLineTime = NEVER;

                ClearRigStatus();

                AddCommands(RigCommands.InitCmd, TCommandKind.ckInit);

                AddCommands(RigCommands.StatusCmd, TCommandKind.ckStatus);
                try
                {
                    ComPort.Open = true;
                }
                catch { }
            }
            finally
            {
                UnLock();
            }

            CheckQueue();
            if (ComPort.Open)
                CheckQueue();
            else
                Log?.Invoke($"RIG%d {RigNumber} Unable to open port");

            TaskCancelled = false;
            _ = Task.Run(async () =>
            {
                while (!TaskCancelled)
                {
                    TimerTick();
                    await Task.Delay(200);  // in origine 100
                }
            });

        }

        public void Stop()
        {
            if (!FEnabled)
                return;

            Log?.Invoke($"Stopping RIG{RigNumber}");

            if (!Lock())
                return;

            try
            {                
                FEnabled = false;
                TaskCancelled = true;
                FOnline = false;
                FQueue.Clear();
                FQueue.Phase = TExchangePhase.phIdle;
                ComPort.Open = false;
            }
            finally
            {
                UnLock();
            }
        }

        private void SentEvent(object Sender)
        {

            Log?.Invoke($"RIG{RigNumber} data sent, {ComPort.TxQueue} bytes in TX buffer");

            if (ComPort.TxQueue > 0)
                return;

            if (!Lock())
                return;

            try
            {
                //are we here by mistake?
                if ((!ComPort.Open) || (FQueue.Phase != TExchangePhase.phSending) || (FQueue.Count == 0))
                    return;


                if (FQueue.CurrentCmd.NeedsReply())
                {
                    //prepare to receive reply                      
                    FQueue.Phase = TExchangePhase.phReceiving;
                    FDeadLineTime = DateTime.Now.AddMilliseconds(TimeoutMs);
                }
                else
                {
                    //send next cmd if queue not empty                    
                    FQueue.Delete(0);
                    FQueue.Phase = TExchangePhase.phIdle;
                    FDeadLineTime = NEVER;
                    CheckQueue();
                }
            }
            finally
            {
                UnLock();
            }
        }

        private void RecvEvent(object Sender)
        {

            byte[] Data;

            if (!Lock())
                return;

            try
            {
                //read data
                Data = null;

                if (ComPort.RxBuffer.Length != 0)
                    Data = ComPort.RxBuffer;

                ComPort.PurgeRx();

                //some COM ports do not send EV_TXEMPTY

                if (FQueue.Phase == TExchangePhase.phSending)
                {
                    FQueue.Phase = TExchangePhase.phReceiving;
                    Log?.Invoke($"RIG{RigNumber} {ComPort.TxQueue} bytes in TX buffer, accepting reply");
                }

                if (FQueue.Phase == TExchangePhase.phReceiving)
                    Log?.Invoke($"RIG{RigNumber} reply received: {ByteArray.ByteToHex(Data)}");
                else
                    Log?.Invoke($"RIG{RigNumber} !unexpected data received: {ByteArray.ByteToHex(Data)}");



                //are we in the right state?
                if ((!ComPort.Open) || (FQueue.Phase != TExchangePhase.phReceiving) || (FQueue.Count == 0))
                    return;


                //process data
                try
                {
                    switch (FQueue.CurrentCmd.Kind)
                    {
                        case TCommandKind.ckInit:
                            ProcessInitReply(FQueue.CurrentCmd.Number, Data);
                            break;
                        case TCommandKind.ckWrite:
                            ProcessWriteReply(FQueue.CurrentCmd.Param, Data);
                            break;
                        case TCommandKind.ckStatus:
                            ProcessStatusReply(FQueue.CurrentCmd.Number, Data);
                            break;
                        case TCommandKind.ckCustom:
                            ProcessCustomReply(FQueue.CurrentCmd.CustSender, FQueue.CurrentCmd.Code, Data);
                            break;

                    }
                }
                catch (Exception ex)
                {
                    Log?.Invoke($"RIG{RigNumber} !Processing reply: {ex.Message}");
                }

                //we are receiving data, therefore we are online
                if (!FOnline)
                {
                    FOnline = true;
                    NotifyStatus?.Invoke(RigNumber);
                }

                //send next command if queue not empty
                FQueue.Delete(0);
                FQueue.Phase = TExchangePhase.phIdle;
                FDeadLineTime = NEVER;
                CheckQueue();
            }
            finally
            {
                UnLock();
            }

        }

        private string BoolStr(bool value) => value ? "ON" : "OFF";
        private void CtsDsrEvent(object Sender, object change)
        {
            if (change is SerialPinChange)
            {
                SerialPinChange pinChange = (SerialPinChange)change;
                //Log?.Invoke($"RIG{RigNumber} ctrl bits: CTS={BoolStr(ComPort.CtsBit)} DSR={BoolStr(ComPort.DsrBit)} RLS={BoolStr(ComPort.RlsdBit)}");
                Log?.Invoke($"RIG{RigNumber} SerialPinChange: {change}");
            }
        }

        //------------------------------------------------------------------------------
        //                                  queue
        //------------------------------------------------------------------------------
        private object lockObject = new object();
        //private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        public bool Lock()
        {
            //TODO DEBUG
            StackTrace stackTrace = new StackTrace();
            StackFrame frame = stackTrace.GetFrame(1);
            string method = frame.GetMethod().Name;
            //Console.WriteLine($"TryEnter: {method}");

            bool success = Monitor.TryEnter(lockObject, 1000);
            //-bool success = semaphore.Wait(1000);
            if (!success)
                Log?.Invoke($"RIG{RigNumber} !Unable to lock {method}");
            /*
            else
                Console.WriteLine($"Go------: {method}"); 
            */
            return success;
        }

        public void UnLock()
        {
            //TODO DEBUG
            StackTrace stackTrace = new StackTrace();
            StackFrame frame = stackTrace.GetFrame(1);
            //Console.WriteLine($"Unlock--: {frame.GetMethod().Name}");
            Monitor.Exit(lockObject);
            //-semaphore.Release();
        }

        public void CheckQueue()
        {
            string S;

            if (ComPort.Open && (FQueue.Phase == TExchangePhase.phIdle) && (FQueue.Count > 0))
            {
                //TODO verificare posizione Lock
                if (!Lock())
                    return;

                try
                {
                    //anything in rx buffer?
                    if (ComPort.RxBuffer.Length != 0)
                    {
                        Log?.Invoke($"RIG{RigNumber} !unexpected bytes in RX buffer: {ByteArray.ByteToHex(ComPort.RxBuffer)}");
                        ComPort.PurgeRx();
                    }

                    TQueueItem q = FQueue[0];

                    //prepare port for receiving reply
                    ComPort.RxBlockSize = q.ReplyLength;
                    ComPort.RxBlockTerminator = q.ReplyEnd;
                    if (!string.IsNullOrEmpty(q.ReplyEnd))
                        ComPort.RxBlockMode = BaseCommPort.TRxBlockMode.rbTerminator;
                    else if (q.ReplyLength > 0)
                        ComPort.RxBlockMode = BaseCommPort.TRxBlockMode.rbBlockSize;
                    else
                        ComPort.RxBlockMode = BaseCommPort.TRxBlockMode.rbChar;


                    //log
                    S = "?";
                    switch (q.Kind)
                    {
                        case TCommandKind.ckInit:
                            S = "init";
                            break;
                        case TCommandKind.ckWrite:
                            S = FRigCommands.ParamToStr(q.Param);
                            break;
                        case TCommandKind.ckStatus:
                            S = "status";
                            break;
                        case TCommandKind.ckCustom:
                            S = "custom";
                            break;
                    }
                    Log?.Invoke($"RIG{RigNumber} sending {S} command: {ByteArray.ByteToHex(q.Code)}");

                    //send command
                    FQueue.Phase = TExchangePhase.phSending;
                    FDeadLineTime = DateTime.Now.AddMilliseconds(TimeoutMs);
                    ComPort.Send(q.Code);
                    //{!} debug
                    Log?.Invoke($"RIG{RigNumber} ComPort.Send called, {ComPort.TxQueue} bytes in TX buffer");
                }
                finally
                {
                    UnLock();
                }
            }
        }

        public virtual void TimerTick()
        {
            if (!Lock())
                return;

            try
            {

                if (!FEnabled)
                    return;

                //try to open port
                if (!ComPort.Open)
                {
                    try
                    {
                        ComPort.Open = true;
                    }
                    catch { }
                }

                //refresh params
                if (ComPort.Open && (DateTime.Now > FNextStatusTime))
                {

                    if (FQueue.HasStatusCommands)
                        Log?.Invoke($"RIG{RigNumber} Status commands already in queue");
                    else
                    {
                        Log?.Invoke($"RIG{RigNumber} Adding status commands to queue");
                        AddCommands(RigCommands.StatusCmd, TCommandKind.ckStatus);
                    }

                    FNextStatusTime = DateTime.Now.AddMilliseconds(PollMs);

                }

                //on-line timeout occurred
                if (DateTime.Now > FDeadLineTime)
                {
                    //switch off-line
                    if (FOnline)
                    {
                        FOnline = false;
                        NotifyStatus?.Invoke(RigNumber);
                        LastWrittenMode = TRigParam.pmNone;
                    }

                    //cancel pending operation
                    switch (FQueue.Phase)
                    {
                        case TExchangePhase.phSending:
                            Log?.Invoke($"RIG{RigNumber} !send timeout, {ComPort.TxQueue} bytes still in TX buffer");
                            //do not send the remaining part
                            ComPort.PurgeTx();
                            //send the same cmd again
                            FQueue.Phase = TExchangePhase.phIdle;
                            FDeadLineTime = NEVER;
                            break;
                        case TExchangePhase.phReceiving:
                            Log?.Invoke($"RIG{RigNumber} !recv timeout. RX Buffer: {ByteArray.ByteToHex(ComPort.RxBuffer)}");
                            //waste partial reply
                            ComPort.PurgeRx();
                            ComPort.RxBlockMode = BaseCommPort.TRxBlockMode.rbChar;
                            //consider current cmd unreplied
                            FQueue.Delete(0);
                            //allow next cmd
                            FQueue.Phase = TExchangePhase.phIdle;
                            FDeadLineTime = NEVER;
                            break;
                    }

                }
            }
            finally
            {
                UnLock();
            }

            CheckQueue();

        }

        //------------------------------------------------------------------------------
        //                               set param
        //------------------------------------------------------------------------------
        private void SetRigCommands(TRigCommands Value)
        {            
            FRigCommands = Value;
            NotifyRigType?.Invoke(RigNumber);
        }

        private void SetFreq(long Value)
        {
            if (!Enabled)
                return;

            if (RigCommands.WriteableParams.Contains(TRigParam.pmFreq))
                AddWriteCommand(TRigParam.pmFreq, Value);
            else if (RigCommands.WriteableParams.Contains(TRigParam.pmFreqA))
                AddWriteCommand(TRigParam.pmFreqA, Value);
            else if (RigCommands.WriteableParams.Contains(TRigParam.pmFreqB))
                AddWriteCommand(TRigParam.pmFreqB, Value);

        }

        private void SetFreqA(long Value)
        {
            if (Enabled && (Value != FFreqA))
                AddWriteCommand(TRigParam.pmFreqA, Value);
        }
        private void SetFreqB(long Value)
        {
            if (Enabled && (Value != FFreqB))
                AddWriteCommand(TRigParam.pmFreqB, Value);
        }

        private void SetMode(TRigParam Value)
        {
            if (Enabled && TRigCommands.ModeParams.Contains(Value))
                AddWriteCommand(Value);
        }

        private void SetPitch(int Value)
        {
            if (!Enabled)
                return;

            AddWriteCommand(TRigParam.pmPitch, Value);
            //remember the pitch that we set if we cannot read it back from the rig
            if (!RigCommands.ReadableParams.Contains(TRigParam.pmPitch))
                FPitch = Value;
        }

        private void SetRitOffset(long Value)
        {
            if (Enabled && (Value != FRitOffset))
                AddWriteCommand(TRigParam.pmRitOffset, Value);
        }

        private void SetRit(TRigParam Value)
        {         
            if (Enabled && TRigCommands.RitOnParams.Contains(Value) && (Value != FRit))
                AddWriteCommand(Value);
        }

        private void SetSplit(TRigParam Value)
        {

            if (!(Enabled && TRigCommands.SplitParams.Contains(Value)))
                return;

            if (RigCommands.WriteableParams.Contains(Value) && (Value != Split))
                AddWriteCommand(Value);

            else if (Value == TRigParam.pmSplitOn)
            {
                if (Vfo == TRigParam.pmVfoAA)
                    Vfo = TRigParam.pmVfoAB;
                else if (Vfo == TRigParam.pmVfoBB)
                    Vfo = TRigParam.pmVfoBA;
            }
            else
            {
                if (Vfo == TRigParam.pmVfoAB)
                    Vfo = TRigParam.pmVfoAA;
                else if (Vfo == TRigParam.pmVfoBA)
                    Vfo = TRigParam.pmVfoBB;
            }
        }

        private void SetTx(TRigParam Value)
        {
            if (Enabled && TRigCommands.TxParams.Contains(Value))
                AddWriteCommand(Value);
        }

        private void SetVfo(TRigParam Value)
        {
            if (Enabled && TRigCommands.VfoParams.Contains(Value) && (Value != FVfo))
                AddWriteCommand(Value);
        }

        // TODO verificare condizione
        public void ForceVfo(TRigParam Value)
        {
            if (Enabled && TRigCommands.VfoParams.Contains(Value))
                AddWriteCommand(Value);
        }

        private void SetXit(TRigParam Value)
        {
            if (Enabled && TRigCommands.XitOnParams.Contains(Value) && (Value != Xit))
                AddWriteCommand(Value);
        }

        private TRigParam GetSplit()
        {
            TRigParam Result = FSplit;
            if (Result != TRigParam.pmNone)
                return Result;

            if ((Vfo == TRigParam.pmVfoAA) || (Vfo == TRigParam.pmVfoBB))
                return TRigParam.pmSplitOff;
            else if ((Vfo == TRigParam.pmVfoAB) || (Vfo == TRigParam.pmVfoBA))
                return TRigParam.pmSplitOn;

            return Result;

        }




    }
}
