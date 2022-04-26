using System.Text.Json.Serialization;

namespace AnyRigLibrary.Models
{
    public class RigSettings
    {
        public string RigType { get; set; }

        public string CommName { get; set; } = "COM1,9600,N,8,1,H,H";
        /*
        public string Port { get; set; }
        public int BaudRate { get; set; } = 9600;
        public int DataBits { get; set; } = 8;
        public string Parity { get; set; } = "N";
        public string StopBits { get; set; } = "1";
        public string RtsMode { get; set; } = "H";
        public bool DtrMode { get; set; } = true;
        */
        public int PollMs { get; set; } = 500;
        public int TimeoutMs { get; set; } = 3000;
        public bool Enabled { get; set; }
        public bool SendOnAir { get; set; }

        /*
        [JsonIgnore]
        public RigCore RunningRig { get; set; }
        */

        /*
        public void SetRig(TRig rig, int rigNumber)
        {

            rig.Enabled = false;
            try
            {
                rig.RigType = RigType;
                rig.ComPort.PortName = Port;
                rig.ComPort.BaudRate = BaudRate;
                rig.ComPort.DataBits = DataBits;
                rig.ComPort.Parity = CommPort.ToParity(Parity);
                rig.ComPort.StopBits = CommPort.ToStopBits(StopBits);
                rig.ComPort.DtrMode = DtrMode;
                rig.ComPort.RtsMode = RtsMode == "H";
                rig.PollMs = PollMs;
                rig.TimeoutMs = TimeoutMs;
                rig.RigNumber = rigNumber;
            }
            finally
            {
                rig.Enabled = true;
            }


        }
        */

        /*
        public static RigSettings CreateFromRig(TRig rig)
        {
            RigSettings result = new RigSettings()
            {
                RigType = rig.RigType,
                Port = rig.ComPort.PortName,
                BaudRate = rig.ComPort.BaudRate,
                DataBits = rig.ComPort.DataBits,
                Parity = CommPort.FromParity(rig.ComPort.Parity),
                StopBits = CommPort.FromStopBits(rig.ComPort.StopBits),
                DtrMode = rig.ComPort.DtrMode,
                RtsMode = rig.ComPort.RtsMode ? "H" : "L",
                PollMs = rig.PollMs,
                TimeoutMs = rig.TimeoutMs                
            };

            return result;

        }
        */

    }
}
