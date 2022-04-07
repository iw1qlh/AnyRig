using AnyRigLibrary;
using AnyRigLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AnyRigNetWrapper
{
    public abstract class BaseRigWrapper : IRigCore
    {
        int rigIndex;
        long freq, freqA, freqB;
        int pitch, ritOffset;
        RigParam mode, vfo;
        bool? rit, split, tx, xit;
        string rigType;

        private BaseAnyRigCommandWrapper wrapper { get; }

        public int RigIndex { get => rigIndex; set { SetRigIndex(value); } }
        public long Freq { get => freq; set { SetFeq(value); } }
        public long FreqA { get => freqA; set { SetFreqA(value); } }
        public long FreqB { get => freqB; set { SetFreqB(value); } }
        public RigParam Mode { get => mode; set { SetMode(value); } }
        public int Pitch { get => pitch; set { SetPitch(value); } }
        public string RigType => rigType;
        public bool? Rit { get => rit; set { SetRit(value.GetValueOrDefault()); } }
        public int RitOffset { get => ritOffset; set { SetRitOffest(value); } }
        public bool? Split { get => split; set { SetSplit(value.GetValueOrDefault()); } }
        public bool? Tx { get => tx; set { SetTx(value.GetValueOrDefault()); } }
        public RigParam Vfo { get => vfo; set { SetVfo(value); } }
        public bool? Xit { get => xit; set { SetXit(value.GetValueOrDefault()); } }

        public Action<int, RigParam[]> NotifyChanges { get; set; }

        public abstract BaseAnyRigCommandWrapper InitCommandWrapper();


        //------------------------------------------------------------------------------
        //                                 ctor
        //------------------------------------------------------------------------------

        public BaseRigWrapper(int nRig)
        {
            wrapper = InitCommandWrapper();
            wrapper.Start();
            wrapper.OnChange = (json) => changeData(json);
            RigIndex = nRig;
        }

        public BaseRigWrapper() : this(0)
        {
        }

        //------------------------------------------------------------------------------
        //                                 private
        //------------------------------------------------------------------------------


        private string sendCommand(string cmd, object value = null, object subdata = null)
        {
            return wrapper.SendCommand($"{cmd}({rigIndex}) {value} {subdata}".Trim());
        }

        private void changeData(string json)
        {
            if (string.IsNullOrEmpty(json))
                return;
            int pos = json.IndexOf("{");
            if (pos < 0)
                return;

            List<RigParam> changedData = new List<RigParam>();

            try
            {
                
                RigData d = JsonSerializer.Deserialize<RigData>(json.Substring(pos));

                if ((d.RigIndex == rigIndex) && d.IsOnLine)
                {

                    rigType = d.RigType;

                    if (d.Freq.HasValue)
                    {
                        freq = d.Freq.Value;
                        changedData.Add(RigParam.FREQ);
                    }
                    if (d.FreqA.HasValue)
                    {
                        freqA = d.FreqA.Value;
                        changedData.Add(RigParam.FREQA);
                    }
                    if (d.FreqB.HasValue)
                    {
                        freqB = d.FreqB.Value;
                        changedData.Add(RigParam.FREQB);
                    }
                    if (d.Pitch.HasValue)
                    {
                        pitch = d.Pitch.Value;
                        changedData.Add(RigParam.PITCH);
                    }
                    if (d.RitOffset.HasValue)
                    {
                        ritOffset = d.RitOffset.Value;
                        changedData.Add(RigParam.RITOFFSET);
                    }
                    if (!string.IsNullOrEmpty(d.Mode))
                    {
                        mode = (RigParam)Enum.Parse(typeof(RigParam), d.Mode);
                        changedData.Add(mode);
                    }
                    if (d.Rit.HasValue)
                    {
                        rit = d.Rit.Value;
                        changedData.Add(d.Rit.Value ? RigParam.RITON : RigParam.RITOFF);
                    }
                    if (d.Split.HasValue)
                    {
                        split = d.Split.Value;
                        changedData.Add(d.Split.Value ? RigParam.SPLITON : RigParam.SPLITOFF);
                    }
                    if (d.Tx.HasValue)
                    {
                        tx = d.Tx.Value;
                        changedData.Add(d.Tx.Value ? RigParam.TX : RigParam.RX);
                    }
                    if (!string.IsNullOrEmpty(d.Vfo))
                    {
                        vfo = (RigParam)Enum.Parse(typeof(RigParam), d.Vfo);
                        changedData.Add(vfo);
                    }
                    if (d.Xit.HasValue)
                    {
                        xit = d.Xit.Value;
                        changedData.Add(d.Xit.Value ? RigParam.XITON : RigParam.XITOFF);
                    }

                }
            }
            catch { }

            if (changedData.Count > 0)
                NotifyChanges?.Invoke(rigIndex, changedData.ToArray());

        }

        //------------------------------------------------------------------------------
        //                                 set
        //------------------------------------------------------------------------------

        private void SetRigIndex(int value)
        {
            rigIndex = value;
            changeData(sendCommand(RigCoreCommands.CMD_FORCE_CHANGE));
        }

        private void SetFeq(long value)
        {
            sendCommand(RigCoreCommands.CMD_SET_FREQ, value);
        }

        private void SetFreqA(long value)
        {
            sendCommand(RigCoreCommands.CMD_SET_FREQA, value);
        }

        private void SetFreqB(long value)
        {
            sendCommand(RigCoreCommands.CMD_SET_FREQB, value);
        }

        private void SetMode(RigParam value)
        {
            sendCommand(RigCoreCommands.CMD_SET_MODE, value);
        }

        private void SetPitch(int value)
        {
            sendCommand(RigCoreCommands.CMD_SET_LEVEL, RigCoreCommands.CMD_LEVEL_PITCH, value);
        }

        private void SetRit(bool value)
        {
            sendCommand(RigCoreCommands.CMD_SET_FUNC, RigCoreCommands.CMD_FUNC_RIT,  value.ToOnOff());
        }

        private void SetRitOffest(int value)
        {
            sendCommand(RigCoreCommands.CMD_SET_RIT_OFFSET, value);
        }

        private void SetSplit(bool value)
        {
            sendCommand(RigCoreCommands.CMD_SET_SPLIT_MODE, value.ToOnOff());
        }

        private void SetTx(bool value)
        {
            sendCommand(RigCoreCommands.CMD_SET_PTT, value.ToOnOff());
        }

        private void SetVfo(RigParam value)
        {
            sendCommand(RigCoreCommands.CMD_SET_VFO, value);
        }

        private void SetXit(bool value)
        {
            sendCommand(RigCoreCommands.CMD_SET_FUNC, RigCoreCommands.CMD_FUNC_XIT, value.ToOnOff());
        }

        //------------------------------------------------------------------------------
        //                                 custom commands
        //------------------------------------------------------------------------------

        public void ClearRit()
        {
            sendCommand(RigCoreCommands.CMD_CLEAR_RIT);
        }

        public void DisableOnAir()
        {
            sendCommand(RigCoreCommands.CMD_DISABLE_ONAIR);
        }

        /*
        public List<RigBaseData> GetRigsList()
        {
            List<RigBaseData> result = null;

            try
            {
                string json = sendCommand(RigCoreCommands.CMD_GET_RIG_LIST);
                int pos = json.IndexOf("=");
                if (pos >= 0)
                    result = JsonSerializer.Deserialize<List<RigBaseData>>(json.Substring(pos + 1));
            }
            catch { }

            return result;

        }
        */

        public void SendCustomCommand(string Command)
        {
            wrapper.SendCommand(Command);
        }

        public void Start()
        {
        }

        public void Stop()
        {
            wrapper.Stop();
        }
        
    }
}
