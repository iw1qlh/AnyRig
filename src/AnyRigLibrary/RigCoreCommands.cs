using AnyRigLibrary.Helpers;
using AnyRigLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AnyRigLibrary
{
    public class RigCoreCommands
    {
        private readonly RigCore[] rigs;
        readonly Regex regex = new Regex(@"^(?<cmd>[a-zA-Z_]+|\?|\.)(\((?<rig>\d+)\))?\s*(?<data>\S+)?(\s+(?<subdata>\S+))?$", RegexOptions.Compiled);

        public const string CMD_SET_FREQ = "F";
        public const string CMD_GET_FREQ = "f";
        public const string CMD_SET_FREQA = "FA";
        public const string CMD_GET_FREQA = "fa";
        public const string CMD_SET_FREQB = "FB";
        public const string CMD_GET_FREQB = "fb";

        public const string CMD_SET_VFO = "V";
        public const string CMD_GET_VFO = "v";

        public const string CMD_SET_RIT_OFFSET = "J";
        public const string CMD_GET_RIT_OFFSET = "j";

        public const string CMD_CLEAR_RIT = "clear_rit";

        /*
                public const string CMD_SET_XIT = "Z";
                public const string CMD_GET_XIT = "z";
        */

        public const string CMD_SET_MODE = "M";
        public const string CMD_GET_MODE = "m";

        public const string CMD_SET_PTT = "T";
        public const string CMD_GET_PTT = "t";

        public const string CMD_SET_SPLIT_MODE = "S";
        public const string CMD_GET_SPLIT_MODE = "s";

        public const string CMD_SET_LEVEL = "L";
        public const string CMD_GET_LEVEL = "l";
        public const string CMD_SET_FUNC = "U";
        public const string CMD_GET_FUNC = "u";

        public const string CMD_LEVEL_PITCH = "CWPITCH";

        public const string CMD_FUNC_RIT = "RIT";
        public const string CMD_FUNC_XIT = "XIT";

        public const string CMD_GET_RIG_LIST = "get_rigs";

        public const string CMD_FORCE_CHANGE = ".";

        public RigCoreCommands(RigCore[] rigs)
        {
            this.rigs = rigs;
        }

        public string Request(string command)
        {
            string result = "?";

            try
            {
                Match m = regex.Match(command);
                if (m.Success)
                {
                    int nRig = 0;
                    if (!int.TryParse(m.Groups["rig"].Value, out nRig))
                        nRig = 0;
                    if (nRig < rigs.Length)
                    {
                        string data = m.Groups["data"].Value;
                        string subdata = m.Groups["subdata"].Value;
                        RigCore rig = rigs[nRig];

                        switch (m.Groups["cmd"].Value)
                        {
                            case "?":
                                result = "\r\n"
                                    + "- VFO ---\r\n"
                                    + "f get_freq ....... Get Frequency [Hz]\r\n"
                                    + "fa get_freqa ..... Get Frequency VFO A [Hz]\r\n"
                                    + "fb get_freqb ..... Get Frequency VFO B [Hz]\r\n"
                                    + "F set_freq ....... Set Frequency [Hz]\r\n"
                                    + "FA set_freqa ..... Set Frequency VFO A [Hz]\r\n"
                                    + "FB set_freqb ..... Set Frequency VFO B [Hz]\r\n"

                                    + "j get_rit ........ Get RIT [Hz]\r\n"
                                    + "J set_rit ........ Set RIT [Hz]\r\n"
/*
                                    + "z get_xit ........ Get XIT [Hz]\r\n"
                                    + "Z set_xit ........ Set XIT [Hz]\r\n"
*/
                                    + "- MODE ---\r\n"
                                    + "m get_mode ....... Get Mode\r\n"
                                    + "M set_mode ....... Set Mode\r\n"
                                    + "- STATUS ---\r\n"
                                    + "t get_ptt ........ Get PTT status\r\n"
                                    + "T set_ptt ........ Set PTT status\r\n"
                                    + "s get_split_vfo .. Get SPLIT mode\r\n"
                                    + "S set_split_vfo .. Set SPLIT mode\r\n"
                                    + "- Multiple rigs ---\r\n"
                                    + "f ====> execute command on Rig 0\r\n"
                                    + "f(0) => execute command on Rig 0\r\n"
                                    + "f(1) => execute command on Rig 1\r\n"
                                    + "...\r\n";
                                break;

                            // FREQ VFO
                            case CMD_GET_FREQ:
                            case "get_freq":
                                result = rig.Freq.ToString();
                                break;

                            case CMD_SET_FREQ:
                            case "set_freq":
                                rig.Freq = long.Parse(data);
                                result = "OK";
                                break;

                            // FREQ VFO A
                            case CMD_GET_FREQA:
                            case "get_freqa":
                                result = rig.FreqA.ToString();
                                break;

                            case CMD_SET_FREQA:
                            case "set_freqa":
                                rig.FreqA = long.Parse(data);
                                result = "OK";
                                break;

                            // FREQ VFO B
                            case CMD_GET_FREQB:
                            case "get_freqb":
                                result = rig.FreqB.ToString();
                                break;

                            case CMD_SET_FREQB:
                            case "set_freqb":
                                rig.FreqB = long.Parse(data);
                                result = "OK";
                                break;

                            // VFO
                            case CMD_GET_VFO:
                            case "get_vfo":
                                result = rig.Vfo.ToString();
                                break;

                            case CMD_SET_VFO:
                            case "set_vfo":
                                rig.Vfo = data.ToRigParam();
                                result = "OK";
                                break;

                            // MODE
                            case CMD_GET_MODE:
                            case "get_mode":
                                result = rig.Mode.ToString();
                                break;

                            case CMD_SET_MODE:
                            case "set_mode":
                                rig.Mode = data.ToRigParam();
                                result = "OK";
                                break;

                            // RIT OFFSET
                            case CMD_GET_RIT_OFFSET:
                            case "get_rit":
                                result = rig.RitOffset.ToString();
                                break;

                            case CMD_SET_RIT_OFFSET:
                            case "set_rit":
                                rig.RitOffset = int.Parse(data);
                                result = "OK";
                                break;

                            case CMD_CLEAR_RIT:
                                rig.ClearRit();
                                break;

/*
                            // RIT
                            case CMD_GET_RIT:
                            case "get_rit":
                                result = rig.Rit.ToString();
                                break;

                            case CMD_SET_RIT:
                            case "set_rit":
                                rig.Rit = data.ToBool();
                                result = "OK";
                                break;


                            // XIT
                            case CMD_GET_XIT:
                            case "get_xit":
                                result = rig.Xit.ToString();
                                break;

                            case CMD_SET_XIT:
                            case "set_xit":
                                rig.Xit = data.ToBool();
                                result = "OK";
                                break;
*/

                            // PTT
                            case CMD_GET_PTT:
                            case "get_ptt":
                                result = rig.Tx.ToString();
                                break;

                            case CMD_SET_PTT:
                            case "set_ptt":
                                rig.Tx = data.ToBool();
                                result = "OK";
                                break;


                            // SPLIT
                            case CMD_GET_SPLIT_MODE:
                            case "get_split_vfo":
                                result = rig.Split.ToString();
                                break;

                            case CMD_SET_SPLIT_MODE:
                            case "set_split_vfo":
                                rig.Split = data.ToBool();
                                result = "OK";
                                break;


                            // OTHERS
                            case CMD_SET_LEVEL:
                            case "set_level":
                                switch (data)
                                {
                                    case CMD_LEVEL_PITCH:
                                        rig.Pitch = int.Parse(subdata);
                                        result = "OK";
                                        break;
                                }
                                break;

                            case CMD_GET_LEVEL:
                            case "get_level":
                                switch (data)
                                {
                                    case CMD_LEVEL_PITCH:
                                        result = rig.Pitch.ToString();
                                        break;
                                }
                                break;

                            case CMD_SET_FUNC:
                            case "set_func":
                                switch (data)
                                {
                                    case CMD_FUNC_RIT:
                                        rig.Rit = subdata.ToBool();
                                        result = "OK";
                                        break;
                                    case CMD_FUNC_XIT:
                                        rig.Xit = subdata.ToBool();
                                        result = "OK";
                                        break;
                                }
                                break;

                            case CMD_GET_FUNC:
                            case "get_func":
                                switch (data)
                                {
                                    case CMD_FUNC_RIT:
                                        result = rig.Rit.ToString();
                                        break;
                                    case CMD_FUNC_XIT:
                                        result = rig.Xit.ToString();
                                        break;
                                }
                                break;

                            case CMD_GET_RIG_LIST:
                                List<RigBaseData> rigList = new List<RigBaseData>();
                                for (int i = 0; i < rigs.Length; i++)
                                {
                                    rigList.Add(new RigBaseData
                                    {
                                        RigIndex = i,
                                        RigType = rigs[i].RigType,
                                        IsOnLine = rigs[i].Status == RigStatus.ST_ONLINE
                                    });
                                }
                                JsonSerializerOptions options = new JsonSerializerOptions()
                                {
                                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                                };
                                result = JsonSerializer.Serialize<List<RigBaseData>>(rigList, options);
                                break;

                            // simulate change
                            case CMD_FORCE_CHANGE:
                                rig.ClearRigStatus();
                                result = "OK";

                                /*
                                result = GetChanges(nRig, (RigParam[])Enum.GetValues(typeof(RigParam)));
                                */
                                /*
                                rig.NotifyChanges(0, new RigParam[0]);
                                RigData rigData = new RigData()
                                {
                                    RigIndex = nRig,
                                    RigType = rig.RigType,
                                    IsOnLine = rig.Status == RigStatus.ST_ONLINE,
                                    Freq = rig.Freq == default ? null : (long?)rig.Freq,
                                    FreqA = rig.FreqA == default ? null : (long?)rig.FreqA,
                                    FreqB = rig.FreqB == default ? null : (long?)rig.FreqB,
                                    Pitch = rig.Pitch == default ? null : (int?)rig.Pitch,
                                    RitOffset = rig.RitOffset == default ? null : (int?)rig.RitOffset,
                                    Mode = rig.Mode == RigParam.UNKNOWN ? null : Enum.GetName(typeof(RigParam), rig.Mode),
                                    Rit = rig.Rit,
                                    Split = rig.Split,
                                    Tx = rig.Tx,
                                    Vfo = rig.Vfo == RigParam.UNKNOWN ? null : (RigParam?)rig.Vfo,
                                    Xit = rig.Xit,
                                };
                                //Console.WriteLine("System.Text.Json version: " + typeof(JsonSerializer).Assembly.FullName);
                                JsonSerializerOptions options = new JsonSerializerOptions()
                                {
                                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                                };
                                result = JsonSerializer.Serialize<RigData>(rigData, options);
                                */
                                break;

                        }
                    }
                }
            }
            catch { }

            return result;
        }

        public string GetChanges(int nRig, RigParam[] changed)
        {
            RigCore rig = rigs[nRig];
            RigData rigData = new RigData()
            {
                RigIndex = nRig,
                RigType = rig.RigType,
                IsOnLine = rig.Status == RigStatus.ST_ONLINE
            };
            
            foreach (RigParam c in changed)
            {

                switch (c)
                {
                    case RigParam.FREQ:
                        rigData.Freq = rig.Freq;
                        break;
                    case RigParam.FREQA:
                        rigData.FreqA = rig.FreqA;
                        break;
                    case RigParam.FREQB:
                        rigData.FreqB = rig.FreqB;
                        break;
                    case RigParam.PITCH:
                        rigData.Pitch = rig.Pitch;
                        break;
                    case RigParam.RITOFFSET:
                        rigData.RitOffset = rig.RitOffset;
                        break;
                    case RigParam.SPLITON:
                    case RigParam.SPLITOFF:
                        rigData.Split = rig.Split;
                        break;
                    case RigParam.RITON:
                    case RigParam.RITOFF:
                        rigData.Rit = rig.Rit;
                        break;
                    case RigParam.XITON:
                    case RigParam.XITOFF:
                        rigData.Xit = rig.Xit;
                        break;
                    case RigParam.RX:
                    case RigParam.TX:
                        rigData.Tx = rig.Tx;
                        break;
                    case RigParam.VFOAA:
                    case RigParam.VFOAB:
                    case RigParam.VFOBA:
                    case RigParam.VFOBB:
                    case RigParam.VFOA:
                    case RigParam.VFOB:
                        rigData.Vfo = Enum.GetName(typeof(RigParam), rig.Vfo);
                        break;
                    case RigParam.CW:
                    case RigParam.CWR:
                    case RigParam.USB:
                    case RigParam.LSB:
                    case RigParam.DIGR:
                    case RigParam.DIG:
                    case RigParam.AM:
                    case RigParam.FM:
                        rigData.Mode = Enum.GetName(typeof(RigParam), rig.Mode);
                        break;
                    default:
                        Console.WriteLine(c);
                        break;
                }


/*
                    RIT0 = 0x00000040,
                    VFOEQUAL = 0x00002000,
                    VFOSWAP = 0x00004000,
*/


            }

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            return JsonSerializer.Serialize<RigData>(rigData, options);

        }

    }
}
