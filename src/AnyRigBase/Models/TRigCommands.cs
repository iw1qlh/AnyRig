using IniParser;
using IniParser.Model;
using AnyRigBase.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AnyRigBase.Models
{
    public enum TValueFormat { vfNone, vfText, vfBinL, vfBinB, vfBcdLU, vfBcdLS, vfBcdBU, vfBcdBS, vfYaesu, vfDPIcom, vfTextUD };

    // see RigParam
    public enum TRigParam
    {
        pmNone,
        pmFreq, pmFreqA, pmFreqB, pmPitch, pmRitOffset, pmRit0,
        pmVfoAA, pmVfoAB, pmVfoBA, pmVfoBB, pmVfoA, pmVfoB, pmVfoEqual, pmVfoSwap,
        pmSplitOn, pmSplitOff,
        pmRitOn, pmRitOff,
        pmXitOn, pmXitOff,
        pmRx, pmTx,
        pmCW_U, pmCW_L, pmSSB_U, pmSSB_L, pmDIG_U, pmDIG_L, pmAM, pmFM
    };

    public class TRigCommands
    {

        public static readonly TRigParam[] NumericParams = { TRigParam.pmFreq, TRigParam.pmFreqA, TRigParam.pmFreqB, TRigParam.pmPitch, TRigParam.pmRitOffset };
        public static readonly TRigParam[] VfoParams = { TRigParam.pmVfoAA, TRigParam.pmVfoAB, TRigParam.pmVfoBA, TRigParam.pmVfoBB, TRigParam.pmVfoA, TRigParam.pmVfoB, TRigParam.pmVfoEqual, TRigParam.pmVfoSwap };
        public static readonly TRigParam[] SplitParams = { TRigParam.pmSplitOn, TRigParam.pmSplitOff };
        public static readonly TRigParam[] RitOnParams = { TRigParam.pmRitOn, TRigParam.pmRitOff };
        public static readonly TRigParam[] XitOnParams = { TRigParam.pmXitOn, TRigParam.pmXitOff };
        public static readonly TRigParam[] TxParams = { TRigParam.pmRx, TRigParam.pmTx };
        public static readonly TRigParam[] ModeParams = { TRigParam.pmCW_U, TRigParam.pmCW_L, TRigParam.pmSSB_U, TRigParam.pmSSB_L, TRigParam.pmDIG_U, TRigParam.pmDIG_L, TRigParam.pmAM, TRigParam.pmFM };

        public string RigType;

        public List<TRigCommand> InitCmd;
        public TRigCommand[] WriteCmd;
        public List<TRigCommand> StatusCmd;
        public List<TRigParam> ReadableParams;
        public List<TRigParam> WriteableParams;
        public List<string> FLog;

        private FileIniDataParser FIni;
        private IniData FIniData;
        private string FSection, FEntry;
        private string[] FList;
        private bool WasError;

        //------------------------------------------------------------------------------
        //                              system
        //------------------------------------------------------------------------------
        public TRigCommands()
        {
            FLog = new List<string>();
            FList = new string[0];
            WriteCmd = new TRigCommand[Enum.GetNames(typeof(TRigParam)).Length];
        }

        private void Log(string AMessage, bool ShowValue = true)
        {
            string Value;
            if (ShowValue && !string.IsNullOrEmpty(FEntry))
                Value = $"in {FIniData[FSection][FEntry]}";
            else
                Value = "";
            FLog.Add($"[{FSection}].{FEntry}: {AMessage} {Value}");
            WasError = true;
        }

        //------------------------------------------------------------------------------
        //                            clear record
        //------------------------------------------------------------------------------
        private void Clear(TRigCommand Rec)
        {
            Rec.Code = null;
            Clear(Rec.Value);
            Rec.ReplyLength = 0;
            Rec.ReplyEnd = null;
            Clear(Rec.Validation);
            Rec.Values = null;
            Rec.Flags = null;
        }

        private void Clear(TParamValue Rec)
        {
            Rec.Start = 0;
            Rec.Len = 0;
            Rec.Format = TValueFormat.vfNone;
            Rec.Mult = 0;
            Rec.Add = 0;
            Rec.Param = TRigParam.pmNone;
        }

        private void Clear(TBitMask Rec)
        {
            Rec.Mask = null;
            Rec.Flags = null;
            Rec.Param = TRigParam.pmNone;
        }

        //------------------------------------------------------------------------------
        //                                load
        //------------------------------------------------------------------------------
        public void FromIni(string AFileName)
        {

            FLog.Clear();

            RigType = Path.GetFileNameWithoutExtension(AFileName);

            //clear arrays
            InitCmd = new List<TRigCommand>();
            StatusCmd = new List<TRigCommand>();
            //TODO for p:= Low(TRigParam) to High(TRigParam) do Clear(WriteCmd[p]);

            try
            {
                //read ini
                FIni = new FileIniDataParser(); // FIni := TIniFile.Create(AFileName);
                FIniData = FIni.ReadFile(AFileName);

                foreach (SectionData section in FIniData.Sections)
                {
                    FSection = section.SectionName;
                    FList = FIniData[FSection].Select(s => s.KeyName).ToArray();

                    WasError = false;
                    if (FSection.ToUpper().StartsWith("INIT"))
                        LoadInitCmd();
                    else if (FSection.ToUpper().StartsWith("STATUS"))
                        LoadStatusCmd();
                    else
                        LoadWriteCmd();

                }

            }
            catch 
            {
                throw;
            }
            ListSupportedParams();

        }

        //load fields that are common to all commands
        private TRigCommand LoadCommon()
        {
            TRigCommand Result = new TRigCommand();

            try
            {
                FEntry = "Command";
                Result.Code = StrToBytes(FIniData[FSection][FEntry]);
            }
            catch
            {
                Log("invalid byte string");
            }
            if (Result.Code == null)
                Log("command code is missing");

            try
            {
                FEntry = "ReplyLength";
                Result.ReplyLength = int.Parse(FIniData[FSection][FEntry] ?? "0");
                if (Result.ReplyLength < 0)
                    throw new Exception();
            }
            catch
            {
                Log("invalid integer");
            }

            try
            {
                FEntry = "ReplyEnd";
                Result.ReplyEnd = StrToBytes(FIniData[FSection][FEntry]);
            }
            catch
            {
                Log("invalid byte string");
            }

            try
            {
                FEntry = "Validate";
                Result.Validation = StrToMask(FIniData[FSection][FEntry]);
            }
            catch
            {
                Log("invalid mask");
            }

            ValidateMask(Result.Validation, Result.ReplyLength, Result.ReplyEnd);

            return Result;

        }

        //Value=5|5|vfBcdL|1|0[|pmXXX]
        private TParamValue LoadValue()
        {
            TParamValue Result = new TParamValue();

            string value = FIniData[FSection][FEntry];
            if (value == null)
                return null;

            string[] sub = value.Split('|');
            switch (sub.Length)
            {
                case 0:
                    return null;
                case 5:
                    break;
                case 6:
                    Result.Param = StrToParam(sub[5]);
                    break;
                default:
                    Log("invalid syntax");
                    break;
            }

            try
            {
                Result.Start = int.Parse(sub[0]);
            }
            catch
            {
                Log("invalid Start value");
            }

            try
            {
                Result.Len = int.Parse(sub[1]);
            }
            catch
            {
                Log("invalid Length value");
            }

            Result.Format = StrToFmt(sub[2]);

            try
            {
                Result.Mult = float.Parse(sub[3], NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            catch
            {
                Log("invalid Multiplier value");
            }

            try
            {
                Result.Add = float.Parse(sub[4], NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            catch
            {
                Log("invalid Add value");
            }

            return Result;

        }


        private void LoadInitCmd()
        {
            TRigCommand Cmd;
            ValidateEntryNames(new string[] { "COMMAND", "REPLYLENGTH", "REPLYEND", "VALIDATE" });

            if (FList.Length == 0)
                return;

            Cmd = LoadCommon();

            //TODO Chi carica Value?
            /*
            FEntry = "Value";
            if (Cmd.Value.Format != TValueFormat.vfNone)
            {
                Log("value is not allowed in INIT");
                return;
            }
            */

            if (WasError)
                return;

            InitCmd.Add(Cmd);

        }

        private void LoadWriteCmd()
        {

            TRigCommand Cmd;
            TRigParam Param;

            FEntry = "";
            Param = StrToParam(FSection);
            if (WasError)
                return;

            ValidateEntryNames(new string[] { "COMMAND", "REPLYLENGTH", "REPLYEND", "VALIDATE", "VALUE" });

            if (FList.Length == 0)
                return;

            Cmd = LoadCommon();

            FEntry = "Value";
            Cmd.Value = LoadValue();
            if (Cmd.Value != null)
            {
                ValidateValue(Cmd.Value, Cmd.Code.Length);

                if (Cmd.Value.Param != TRigParam.pmNone)
                    Log("parameter name is not allowed");

                if (NumericParams.Contains(Param) && (Cmd.Value.Len == 0))
                    Log("Value is missing");

                if (!NumericParams.Contains(Param) && (Cmd.Value.Len > 0))
                    Log("parameter does not require a value", false);
            }

            if (!WasError)
                WriteCmd[(int)Param] = Cmd;

        }

        private void LoadStatusCmd()
        {
            TRigCommand Cmd;
            List<string> L;
            TBitMask Flag;
            TParamValue Value;            

            ValidateEntryNames(new string[] { "COMMAND", "REPLYLENGTH", "REPLYEND", "VALIDATE", "VALUE*", "FLAG*" });

            if (FList.Length == 0)
                return;

            //common fields
            Cmd = LoadCommon();

            FEntry = "";
            if ((Cmd.ReplyLength == 0) && (Cmd.ReplyEnd == null))
                Log("ReplyLength or ReplyEnd must be specified");

            //values and flags to extract from a reply

            Cmd.Values = new List<TParamValue>();
            Cmd.Flags = new List<TBitMask>();
            L = new List<string>();
            try
            {
                //list of entries in the section
                foreach (var entry in FIniData[FSection])
                {
                    if (entry.KeyName.ToUpper().StartsWith("VALUE"))
                    {
                        FEntry = entry.KeyName;
                        Value = LoadValue();
                        ValidateValue(Value, Math.Max(Cmd.ReplyLength, (Cmd.Validation != null ? Cmd.Validation.Mask.Length : 0)));
                        if (Value.Param == TRigParam.pmNone)
                            Log("parameter name is missing");
                        else if (!NumericParams.Contains(Value.Param))
                            Log("parameter must be of numeric type");

                        Cmd.Values.Add(Value);
                    }

                    else if (entry.KeyName.ToUpper().StartsWith("FLAG"))
                    {
                        FEntry = entry.KeyName;
                        Flag = StrToMask(FIniData[FSection][FEntry]);
                        ValidateMask(Flag, Cmd.ReplyLength, Cmd.ReplyEnd);
                        Cmd.Flags.Add(Flag);
                    }


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if ((Cmd.Values.Count == 0) && (Cmd.Flags.Count == 0))
                Log("at least one ValueNN or FlagNN must be defined");

            if (WasError)
                return;

            StatusCmd.Add(Cmd);

        }

        //------------------------------------------------------------------------------
        //                        conversion functions
        //------------------------------------------------------------------------------
        private TValueFormat StrToFmt(string S)
        {
            TValueFormat Result = TValueFormat.vfNone;

            if (!Enum.TryParse(S, out Result))
                Log("invalid format name");

            return Result;
        }

        private TRigParam StrToParam(string S, bool ShowInLog = false)
        {
            TRigParam Result = TRigParam.pmNone;

            if (!Enum.TryParse(S, out Result))
                if (ShowInLog)
                    Log("invalid parameter name");

            return Result;
        }

        public string ParamToStr(TRigParam Param)
        {
            return Param.ToString();
        }

        private byte[] StrToBytes(string S)
        {
            byte[] Result = null;

            if (string.IsNullOrEmpty(S))
                return null;

            S = S.Trim();
            if (S.Length < 2)
                return null;


            //asc
            if (S.StartsWith("("))
            {
                if (!S.EndsWith(")"))
                    return null;
                Result = ByteArray.StrToBytes(S.Substring(1, S.Length - 2));
            }

            //hex
            else if ("0123456789ABCDEF".Contains(S[1]))
            {
                S = S.Replace(".", "");
                if ((S.Length % 2) != 0)
                    throw new Exception();

                int len = S.Length / 2;
                Result = new byte[len];
                for (int i = 0; i < len; i++)
                {
                    Result[i] = byte.Parse(S.Substring(i * 2, 2), NumberStyles.HexNumber);
                }

            }

            //all other
            else throw new Exception();

            return Result;

        }

        private byte[] FlagsFromMask(ref byte[] AMask, char Char1)
        {
            byte[] Result = AMask.ToArray();

            if (Char1 == '(')
            {
                for (int i = 0; i < Result.Length; i++)
                {
                    if (AMask[i] == '.')
                    {
                        AMask[i] = 0;
                        Result[i] = 0;
                    }
                    else AMask[i] = 0xFF;
                }
            }
            else
            {
                for (int i = 0; i < AMask.Length; i++)
                {
                    if (AMask[i] != 0)
                        AMask[i] = 0xFF;
                }
            }

            return Result;
        }

        //Flag1 =".......................0.............."|pmRitOff
        //Flag1 =13.00.00.00.00.00.00.00|00.00.00.00.00.00.00.00|pmVfoAA
        //Validation=FEFEE05EFBFD
        //Validation=FFFFFFFF.FF.0000000000.FF|FEFEE05E.03.0000000000.FD

        private TBitMask StrToMask(string S)
        {
            TBitMask Result = new TBitMask();
            Result.Param = TRigParam.pmNone;

            Result.Mask = null;
            Result.Flags = null;
            if (string.IsNullOrEmpty(S))
                return null;

            //extract mask
            //TODO FList.DelimText = S;
            string[] sub = S.Split('|');
            Result.Mask = StrToBytes(sub[0]);
            if (Result.Mask == null)
                throw new Exception();

            switch (sub.Length)
            {
                //just mask, infer flags
                case 1:
                    Result.Flags = FlagsFromMask(ref Result.Mask, sub[0][0]);
                    break;
                //mask|param or mask|flags
                case 2:
                    Result.Param = StrToParam(sub[1], false);
                    if (Result.Param != TRigParam.pmNone)
                        Result.Flags = FlagsFromMask(ref Result.Mask, sub[0][0]);
                    else
                        Result.Flags = StrToBytes(sub[1]);
                    break;
                //mask|flags|param
                case 3:
                    Result.Flags = StrToBytes(sub[1]);
                    Result.Param = StrToParam(sub[2]);
                    break;
                default:
                    throw new Exception();

            }

            return Result;

        }

        //------------------------------------------------------------------------------
        //                             validation
        //------------------------------------------------------------------------------
        private void ValidateMask(TBitMask AMask, int ALen, byte[] AEnd)
        {

            byte[] Ending;

            if (AMask == null)
                return;

            //empty mask, that's fine
            if ((AMask.Mask == null) && (AMask.Flags == null) && (AMask.Param == TRigParam.pmNone))
                return;

            if ((AMask.Mask == null) || (AMask.Flags == null))
                Log("incorrect mask length");

            else if (AMask.Mask.Length != AMask.Flags.Length)
                Log("incorrect mask length");

            else if ((ALen > 0) && (AMask.Mask.Length != ALen))
                Log("mask length <> ReplyLength");
            
            else if (!ByteArray.BytesEqual(ByteArray.BytesAnd(AMask.Flags, AMask.Flags), AMask.Flags))
                Log("mask hides valid bits");            

            //syntax is different for validation masks and flag masks
            else if (FEntry.ToUpper() == "VALIDATE")
            {

                if (AMask.Param != TRigParam.pmNone)
                    Log("parameter name is not allowed");

                Ending = ByteArray.Copy(AMask.Flags, AMask.Flags.Length - AEnd.Length, AEnd.Length);
                if (!ByteArray.BytesEqual(Ending, AEnd))
                    Log("mask does not end with ReplyEnd");
            }
            else
            {
                if (AMask.Param == TRigParam.pmNone)
                    Log("parameter name is missing");
                if (AMask.Mask == null)
                    Log("mask is blank");
            }
        }

        private void ValidateValue(TParamValue AValue, int ALen)
        {
            if (AValue == null)
                return;

            if (AValue.Param == TRigParam.pmNone)
                return;

            if (ALen == 0)
                ALen = int.MaxValue;


            if ((AValue.Start < 0) || (AValue.Start >= ALen))
                Log("invalid Start value");

            if ((AValue.Len < 0) || (AValue.Start + AValue.Len > ALen))
            Log("invalid Length value");

            if (AValue.Mult <= 0)
                Log("invalid Multiplier value");

        }

        private void ValidateEntryNames(string[] Names)
        {
            bool Ok = false;
            string S1, S2;

            FList = FIniData[FSection].Select(s => s.KeyName).ToArray();

            foreach (string item in FList)
            {
                Ok = false;
                foreach (string name in Names)
                {
                    FEntry = item;
                    S1 = FEntry.ToUpper();
                    S2 = name;
                    if (S2.EndsWith("*"))
                    {
                        S2 = S2.Substring(0, S2.Length - 1);
                        S1 = S1.Substring(0, S2.Length);
                    }
                    Ok |= (S1 == S2);
                }
            }

            if (!Ok)
                Log("invalid entry name", false);
        }

        //------------------------------------------------------------------------------
        //                          supported params
        //------------------------------------------------------------------------------
        private void ListSupportedParams()
        {

            ReadableParams = new List<TRigParam>();
            WriteableParams = new List<TRigParam>();

            foreach (var sc in StatusCmd)
            {
                foreach (TParamValue scv in sc.Values)
                    ReadableParams.Add(scv.Param);
                foreach (TBitMask scf in sc.Flags)
                    ReadableParams.Add(scf.Param);
            }

            foreach (TRigParam rp in (TRigParam[])Enum.GetValues(typeof(TRigParam)))
            {
                if (WriteCmd[(int)rp] != null)
                    WriteableParams.Add(rp);
            }

        }

        public string ParamListToStr(TRigParam[] Params)
        {
            string comma = "";
            string Result = "";
            foreach (TRigParam rp in (TRigParam[])Enum.GetValues(typeof(TRigParam)))
            {
                if (Params.Contains(rp))
                {
                    Result += $"{comma}{rp}";
                    comma = ",";
                }
            }
            return Result;
        }

        public long ParamsToInt(List<TRigParam> Params)
        {
            long Result = 0;
            foreach (var p in Params)
            {
                Result = Result | ((long)1 << (int)p);
            }
            return Result;
        }

        public long ParamToInt(TRigParam Param)
        {
            return (long)1 << (int)Param;
        }

        public bool? ParamToBool(TRigParam Param, TRigParam trueValue, TRigParam falseValue)
        {
            if (Param == TRigParam.pmNone)
                return null;
            if (Param == trueValue)
                return true;
            if (Param == falseValue)
                return false;
            throw new Exception();
        }


        public TRigParam IntToParam(long Int)
        {
            TRigParam Result = TRigParam.pmNone;

            foreach (TRigParam rp in (TRigParam[])Enum.GetValues(typeof(TRigParam)))
            {
                if (ParamToInt(rp) == Int)
                {
                    Result = rp;
                    break;
                }
            }

            return Result;

        }

    }

    public class TParamValue
    {
        public int Start, Len;      // insert or extract bytes, Start is a 0based index                                
        public TValueFormat Format; // encode or decode according to this format                               
        public Single Mult, Add;    // linear transformation before encoding or after decoding                                
        public TRigParam Param;     // param to insert or to report
    }

    public class TBitMask
    {
        public byte[] Mask;        // do bitwise AND with this mask                                
        public byte[] Flags;       // compare result to these bits                                
        public TRigParam Param;    // report this param if bits match
    }

    public class TRigCommand
    {
        /// what to send
        public byte[] Code;
        public TParamValue Value;
        /// what to wait for
        public int ReplyLength;
        public byte[] ReplyEnd;
        public TBitMask Validation;
        /// what to extract
        public List<TParamValue> Values;
        public List<TBitMask> Flags;
    }
}