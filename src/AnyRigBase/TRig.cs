using AnyRigBase.Helpers;
using AnyRigBase.Models;
using AnyRigBase.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AnyRigBase
{
    public class TRig : TCustomRig
    {
        //private long ExtMicroFreq;

        protected List<TRigParam> ChangedParams = new List<TRigParam>();

        public TRig(BaseCommPort commPort = null) : base(commPort)
        {
        }

        public Action<int, object> NotifyCustom { get; set; }
        public Action<int, long> NotifyParams { get; set; }
        public Action<int> WmTxtQueue { get; set; }
        public Action Tick { get; set; }

        //------------------------------------------------------------------------------
        //                          add command to queue
        //------------------------------------------------------------------------------
        protected override void AddCommands(List<TRigCommand> ACmds, TCommandKind AKind)
        {
            if (!Lock())
                return;

            try
            {
                int i = 0;
                foreach (var cmd in ACmds)
                {
                    FQueue.Add(new TQueueItem
                    {
                        Code = cmd.Code,
                        Number = i,
                        ReplyLength = cmd.ReplyLength,
                        ReplyEnd = ByteArray.BytesToStr(cmd.ReplyEnd) ?? "",
                        Kind = AKind
                    });
                    i++;
                }
            }
            finally
            {
                UnLock();
            }
        }

        public override void AddCustomCommand(object ASender, byte[] ACode, int ALen, string AEnd)
        {
            if (ACode == null)
                return;

            if (!Lock())
                return;

            try
            {
                FQueue.Add(new TQueueItem
                {
                    Code = ACode,
                    CustSender = ASender,
                    ReplyLength = ALen,
                    ReplyEnd = AEnd ?? "",
                    Kind = TCommandKind.ckCustom
                });
            }
            finally
            {
                UnLock();
            }

            WmTxtQueue?.Invoke(RigNumber);
        }

        public override void AddWriteCommand(TRigParam AParam, long AValue = 0)
        {
            Log?.Invoke($"RIG{RigNumber} Generating Write command for {RigCommands.ParamToStr(AParam)}");
            Log?.Invoke($"Generating Write command for {AValue}");

            //is cmd supported?
            if (RigCommands == null)
                return;
            TRigCommand Cmd = RigCommands.WriteCmd[(int)AParam];
            if (Cmd?.Code == null)
            {
                Log?.Invoke($"RIG{RigNumber} !Write command not supported for {RigCommands.ParamToStr(AParam)}");
                return;
            }

            //generate cmd
            byte[] NewCode = Cmd.Code;
            byte[] FmtValue = null;
            if ((Cmd.Value != null) && (Cmd.Value.Format != TValueFormat.vfNone))
            {
                try
                {
                    FmtValue = FormatValue(AValue, Cmd.Value);
                    if ((Cmd.Value.Start + Cmd.Value.Len) > NewCode.Length)
                        throw new Exception("!Value too long");
                    Array.Copy(FmtValue, 0, NewCode, Cmd.Value.Start, Cmd.Value.Len);
                }
                catch (Exception ex)
                {
                    Log?.Invoke($"!Generating command: {ex.Message}");
                }
            }

            //add to queue
            if (!Lock())
                return;
            try
            {
                FQueue.Add(new TQueueItem
                {
                    Code = NewCode,
                    Param = AParam,
                    Kind = TCommandKind.ckWrite,
                    ReplyLength = Cmd.ReplyLength,
                    ReplyEnd = ByteArray.BytesToStr(Cmd.ReplyEnd) ?? ""
                });
            }
            finally
            {
                UnLock();
            }

            //reminder to check queue
            WmTxtQueue?.Invoke(RigNumber);

        }

        //------------------------------------------------------------------------------
        //                           interpret reply
        //------------------------------------------------------------------------------

        private bool ValidateReply(byte[] AData, TBitMask AMask)
        {
            bool Result;

            if (AMask?.Mask == null)
                Result = true;
            else if (AData.Length != AMask.Mask.Length)
                Result = false;
            else if (AData.Length != AMask.Flags.Length)
                Result = false;
            else Result = ByteArray.BytesEqual(ByteArray.BytesAnd(AData, AMask.Mask), AMask.Flags);

            if (!Result)
                Log?.Invoke($"!RIG{RigNumber} reply validation failed");

            return Result;
        }


        protected override void ProcessCustomReply(object ASender, byte[] ACode, byte[] AData)
        {
            if (!Lock())
                return;

            try
            {
                //TODO TOmniRigX
                /*
                TOmniRigX(ASender).CustCommand = ACode;
                TOmniRigX(ASender).CustReply = AData;
                */
            }
            finally
            {
                UnLock();
            }

            NotifyCustom?.Invoke(RigNumber, ASender);
        }

        protected override void ProcessInitReply(int ANumber, byte[] AData)
        {
            ValidateReply(AData, RigCommands.InitCmd[ANumber].Validation);
        }

        protected override void ProcessStatusReply(int ANumber, byte[] AData)
        {
            TRigCommand Cmd = RigCommands.StatusCmd[ANumber];
            if (!ValidateReply(AData, Cmd.Validation))
                return;

            //extract numeric values
            foreach (var v in Cmd.Values)
            {
                try
                {
                    StoreParam(v.Param, UnformatValue(AData, v));
                }
                catch { };
            }

            //extract bit flags

            foreach (var f in Cmd.Flags)
            {
                if ((AData.Length != f.Mask.Length) ||
                    (AData.Length != f.Flags.Length))
                {

                    Log?.Invoke($"!RIG{RigNumber}: incorrect reply length");
                }
                else if (ByteArray.BytesEqual(ByteArray.BytesAnd(AData, f.Mask), f.Flags))
                    StoreParam(f.Param);
            }

            //tell clients
            if (ChangedParams.Count > 0)
                NotifyParams?.Invoke(RigNumber, FRigCommands.ParamsToInt(ChangedParams));
            
            ChangedParams.Clear();
        }

        protected override void ProcessWriteReply(TRigParam AParam, byte[] AData)
        {
            ValidateReply(AData, RigCommands.WriteCmd[(int)AParam].Validation);
        }

        //------------------------------------------------------------------------------
        //                                format
        //------------------------------------------------------------------------------
        private byte[] FormatValue(long AValue, TParamValue AInfo)
        {
            byte[] Result;

            long Value;
            /*
            int initLen;
            int finalLen;
            int diffLen;
            */
            long Value2;
            int newSize;

            Value = (long)Math.Round(AValue * AInfo.Mult + AInfo.Add);

            Log?.Invoke($"FormatValue Value is {Value}");

            //TODO ToRFfreq
            /*
            initLen = Value.size;
            Value2 = ToRFfreq(Value);
            MainForm.IF2RF_RF.Text := Value2.ToString;

            finalLen = Value2.Size;
            diffLen = finalLen - initLen;
            Log?.Invoke($"size difference is {diffLen}");
            */
            Value2 = Value;

            Result = null;
            newSize = AInfo.Len;
            Result = new byte[newSize];

            if (((AInfo.Format == TValueFormat.vfBcdLU) || (AInfo.Format == TValueFormat.vfBcdBU)) && (Value2 < 0))
            {
                Log?.Invoke($"RIG{RigNumber}: !user passed invalid value: {AValue}");
                return Result;
            }

            switch (AInfo.Format)
            {
                case TValueFormat.vfNone:
                    break;
                case TValueFormat.vfText:
                    ToText(ref Result, Value2); break;
                case TValueFormat.vfBinL:
                    ToBinL(ref Result, Value2); break;
                case TValueFormat.vfBinB:
                    ToBinB(ref Result, Value2); break;
                case TValueFormat.vfBcdLU:
                    ToBcdLU(ref Result, Value2); break;
                case TValueFormat.vfBcdLS:
                    ToBcdLS(ref Result, Value2); break;
                case TValueFormat.vfBcdBU:
                    ToBcdBU(ref Result, Value2); break;
                case TValueFormat.vfBcdBS:
                    ToBcdBS(ref Result, Value2); break;
                case TValueFormat.vfYaesu:
                    ToYaesu(ref Result, Value2); break;
                // Added by RA6UAZ for Icom Marine Radio NMEA Command
                case TValueFormat.vfDPIcom:         
                    ToDPIcom(ref Result, Value2); break;
                case TValueFormat.vfTextUD:
                    ToTextUD(ref Result, Value2); break;
            }

            return Result;

        }

        //ASCII codes of digits
        private void ToText(ref byte[] Arr, long Value)
        {
            Arr = ByteArray.StrToBytes(Value.ToString($"D{Arr.Length}"));
        }

        // Added by RA6UAZ for Icom Marine Radio NMEA Command
        private void ToDPIcom(ref byte[] Arr, long Value)
        {
            float F = Value / 1000000;
            string S = F.ToString("0.000000");
            while (S.Length < Arr.Length)
                S = "0" + S;
            Arr = ByteArray.StrToBytes(S);            
        }

        private void ToTextUD(ref byte[] Arr, long Value)
        {
            Arr = ByteArray.StrToBytes(Value.ToString($"D{Arr.Length}"));
            Arr[0] = (Value > 0) ? (byte)'U' : (byte)'D';
        }

        //integer, little endian
        private void ToBinL(ref byte[] Arr, long Value)
        {
            byte[] b = BitConverter.GetBytes(Value);
            int lastIndex = Array.FindLastIndex(b, x => x != 0);
            Array.Copy(b, Arr, lastIndex + 1);
        }

        //integer, big endian
        private void ToBinB(ref byte[] Arr, long Value)
        {
            ToBinL(ref Arr, Value);
            Array.Reverse(Arr);
        }

        //BCD big endian unsigned
        private void ToBcdBU(ref byte[] Arr, long Value)
        {
            int i = Arr.Length - 1;
            long v = Value;
            while ((v > 0) && (i >= 0))
            {
                long n = v % 100;
                Arr[i] = (byte)(((n / 10) << 4) | (n % 10));
                v = v / 100;
                i--;
            }
        }

        //BCD little endian unsigned
        private void ToBcdLU(ref byte[] Arr, long Value)
        {
            ToBcdBU(ref Arr, Value);
            Array.Reverse(Arr);
        }

        //BCD little endian signed; sign in high byte (00 or FF)
        private void ToBcdLS(ref byte[] Arr, long Value)
        {
            ToBcdLU(ref Arr, Math.Abs(Value));
            if (Value < 0)
                Arr[Arr.Length - 1] = 0xFF;
        }

        //BCD big endian signed
        private void ToBcdBS(ref byte[] Arr, long Value)
        {
            ToBcdBU(ref Arr, Math.Abs(Value));
            if (Value < 0)
                Arr[0] = 0xFF;
        }

        //16 bits. high bit of the 1-st byte is sign,
        //the rest is integer, absolute value, big endian (not complementary!)
        private void ToYaesu(ref byte[] Arr, long Value)
        {
            ToBinB(ref Arr, Math.Abs(Value));
            if (Value < 0)
                Arr[0] = (byte)(Arr[0] | 0x80);
        }


        //------------------------------------------------------------------------------
        //                                unformat
        //------------------------------------------------------------------------------

        private long UnformatValue(byte[] AData, TParamValue AInfo)
        {
            long Result = 0;

            byte[] data = new byte[AInfo.Len];
            Array.Copy(AData, AInfo.Start, data, 0, AInfo.Len);

            // TODO W3SZ AData
            /*
            SetString(AnsiStr, PAnsiChar(data[0]), Length(data));
            MainForm.Log('AData by W3SZ is: %s', [AnsiStr]);
            if (data = nil) or(Length(data) <> AInfo.Len) then
             begin MainForm.Log('RIG%d: {!}reply too short', [RigNumber]); Abort; end;
            */

            switch (AInfo.Format)
            {
                case TValueFormat.vfNone:
                    break;
                case TValueFormat.vfText:
                    Result = FromText(data); break;
                case TValueFormat.vfBinL:
                    Result = FromBinL(data); break;
                case TValueFormat.vfBinB:
                    Result = FromBinB(data); break;
                case TValueFormat.vfBcdLU:
                    Result = FromBcdLU(data); break;
                case TValueFormat.vfBcdLS:
                    Result = FromBcdLS(data); break;
                case TValueFormat.vfBcdBU:
                    Result = FromBcdBU(data); break;
                case TValueFormat.vfBcdBS:
                    Result = FromBcdBS(data); break;
                case TValueFormat.vfYaesu:
                    Result = FromYaesu(data); break;
                case TValueFormat.vfDPIcom:
                    Result = FromDPIcom(data); break;
                default:
                    Result = FromYaesu(data); break;
            }
            
            return (long)Math.Round(Result * AInfo.Mult + AInfo.Add);

        }

        private long FromText(byte[] AData)
        { 
            
            long Result;
            
            //MainForm.Log('Entered TRig.FromText');
            //MainForm.RF2IF_RF.Text := 'Entered TRig.FromText';
            try
            {
                string S = ByteArray.BytesToStr(AData);
                //TODO W3SZ ToIFfreq
                /*
                Move(Adata[0], S[1], Length(S));
                oldResult:= StrToInt64(S);
                Result:= ToIFfreq(oldResult);
                MainForm.Log('Result by W3SZ is: %s', [IntToStr(oldResult)]);
                MainForm.Log('IFFreq by W3SZ is: %s', [IntToStr(Result)]);
                */
                Result = long.Parse(S);

            }
            catch
            {
                Log?.Invoke($"RIG{RigNumber}: !invalid reply");
                throw;
            }

            return Result;
        }

        // Added by RA6UAZ for Icom Marine Radio NMEA Command
        private long FromDPIcom(byte[] AData)
        {
            long Result;

            try
            {
                string S = ByteArray.BytesToStr(AData).TakeWhile(c => "0123456789.".Contains(c)).ToString();
                Result = (long)Math.Round(1E6 * float.Parse(S, NumberStyles.Any, CultureInfo.InvariantCulture));
            }
            catch
            {
                Log?.Invoke($"RIG{RigNumber}: !invalid reply");
                throw;
            }

            return Result;
        }       

        private long FromBinL(byte[] AData)
        {
            return BitConverter.ToInt64(AData, 0);
        }

        private long FromBinB(byte[] AData)
        {
            Array.Reverse(AData);
            return FromBinL(AData);
        }

        private long FromBcdBU(byte[] AData)
        {
            long Result = 0;
            foreach (var b in AData)
            {
                Result *= 100;
                Result += (b >> 4) * 10 + (b & 0x0F);
            }
            return Result;
        }

        private long FromBcdLU(byte[] AData)
        {
            Array.Reverse(AData);  
            return FromBcdBU(AData);
        }

        private long FromBcdBS(byte[] AData)
        {
            long Result;

            Result = (AData[0] == 0) ? 1 : -1;
            AData[0] = 0;
            return Result * FromBcdBU(AData);
        }

        private long FromBcdLS(byte[] AData)
        {
            Array.Reverse(AData);  
            return FromBcdBS(AData);
        }

        //16 bits. high bit of the 1-st byte is sign,
        //the rest is Int64, absolute value, big endian (not complementary!)
        private long FromYaesu(byte[] AData)
        {
            long Result;

            Result = ((AData[0] & 0x80) == 0) ? 1 : -1;
            AData[0] = (byte)(AData[0] & 0x7F);
            return Result * FromBinB(AData);
        }

        //------------------------------------------------------------------------------
        //                         store extracted param
        //------------------------------------------------------------------------------

        private void StoreParam(TRigParam Param)
        {
            TRigParam pp;

            if (TRigCommands.VfoParams.Contains(Param))
                pp = FVfo;
            else if (TRigCommands.SplitParams.Contains(Param))
                pp = FSplit;
            else if (TRigCommands.RitOnParams.Contains(Param))
                pp = FRit;
            else if (TRigCommands.XitOnParams.Contains(Param))
                pp = FXit;
            else if (TRigCommands.TxParams.Contains(Param))
                pp = FTx;
            else if (TRigCommands.ModeParams.Contains(Param))
                pp = FMode;
            else return;

            if (Param == pp)
                return;

            if (TRigCommands.VfoParams.Contains(Param))
                FVfo = Param;
            else if (TRigCommands.SplitParams.Contains(Param))
                FSplit = Param;
            else if (TRigCommands.RitOnParams.Contains(Param))
                FRit = Param;
            else if (TRigCommands.XitOnParams.Contains(Param))
                FXit = Param;
            else if (TRigCommands.TxParams.Contains(Param))
                FTx = Param;
            else if (TRigCommands.ModeParams.Contains(Param))
                FMode = Param;
            else return;

            ChangedParams.Add(Param);

            Log?.Invoke($"RIG{RigNumber} status changed: {RigCommands.ParamToStr(Param)} enabled");
        }

        private void StoreParam(TRigParam Param, long Value)
        {
            long pp;

            switch (Param)
            {
                case TRigParam.pmFreqA:
                    pp = FFreqA; break;
                case TRigParam.pmFreqB:
                    pp = FFreqB; break;
                case TRigParam.pmFreq:
                    pp = FFreq; break;
                case TRigParam.pmPitch:
                    pp = FPitch; break;
                case TRigParam.pmRitOffset:
                    pp = FRitOffset; break;
                default:
                    return;
            }

            if (Value == pp)
                return;

            switch (Param)
            {
                case TRigParam.pmFreqA:
                    FFreqA = Value; break;
                case TRigParam.pmFreqB:
                    FFreqB = Value; break;
                case TRigParam.pmFreq:
                    FFreq = Value; break;
                case TRigParam.pmPitch:
                    FPitch = (int)Value; break;
                case TRigParam.pmRitOffset:
                    FRitOffset = (int)Value; break;
                default:
                    return;
            }

            ChangedParams.Add(Param);

            //unsolved problem:
            //there is no command to read the mode of the other VFO,
            //its change goes undetected.
            if (TRigCommands.ModeParams.Contains(Param) && (Param != LastWrittenMode))
                LastWrittenMode = TRigParam.pmNone;

            Log?.Invoke($"RIG{RigNumber} status changed: {RigCommands.ParamToStr(Param)} = {Value}");
              
        }

        //////
        ///
        public override void TimerTick()
        {
            base.TimerTick();
            Tick?.Invoke();
        }

    }
}
