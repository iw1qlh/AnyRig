
/* Unmerged change from project 'AnyRigBase (net6.0)'
Before:
using System;
After:
using AnyRigBase.Helpers;
using System;
*/
using AnyRigBase.Helpers;

/* Unmerged change from project 'AnyRigBase (net6.0)'
Before:
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using AnyRigBase.Helpers;
After:
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
*/
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace AnyRigBase.Services
{
    public class CommPort
    {
        public static readonly int[] SupportedBaudRates = new int[]
        {
            110, 300, 600, 1200,
            2400, 4800, 9600, 14400, 19200, 38400, 56000,
            57600, 115200, 128000, 256000
        };

        public static readonly int[] SupportedDataBits = new int[]
        {
            5, 6, 7, 8
        };

        public static readonly string[] SupportedParities = new string[]
        {
            "Even", "Mark", "None", "Odd", "Space"
        };

        public static readonly string[] SupportedStopBits = new string[]
        {
            "0", "1", "1.5", "2"
        };

        public static readonly string[] SupportedRtsMode = new string[]
        {
            "High", "Low", "RTS/CTS Handshake"
        };

        public static readonly string[] SupportedDtrMode = new string[]
        {
            "High", "Low"
        };

        public enum TRxBlockMode { rbChar, rbBlockSize, rbTerminator };

        const int BUF_SIZE = 1024;

        private SerialPort com;

        //public int Port;
        
        public long TimeStamp;

        public TRxBlockMode RxBlockMode;
        public int RxBlockSize;
        public string RxBlockTerminator;

        public string PortName { get => com.PortName; set { com.PortName = value; } }
        public int BaudRate { get => com.BaudRate; set { com.BaudRate = value; } }
        public int DataBits { get => com.DataBits; set { com.DataBits = value; } }
        public StopBits StopBits { get => com.StopBits; set { com.StopBits = value; } }
        public Parity Parity { get => com.Parity; set { com.Parity = value; } }
        public bool DtrMode { get => com.DtrEnable; set { SetDtrMode(value); } }
        public bool RtsMode { get => com.RtsEnable; set { SetRtsMode(value); } }
        public bool RlsdBit { get => com.CDHolding; }

        public bool Open { get => com.IsOpen; set { SetOpen(value); } }

        public int TxQueue { get => com.BytesToWrite; }

        public byte[] RxBuffer
        {
            get
            {
                
                int size = rxBuffPtr;
                if (size == 0)
                    return new byte[0];
                byte[] buff = new byte[size];
                Array.Copy(rxBuffer, buff, size);
                return buff;
            }
        }

        public bool CtsBit { get => com.CtsHolding; }
        public bool DsrBit { get => com.DsrHolding; }

        public Action<object, SerialError> OnError { get; set; }
        public Action<object> OnSent { get; set; }
        public Action<object> OnReceived { get; set; }
        public Action<object, SerialPinChange> OnCtsDsr { get; set; }

        private byte[] rxBuffer;
        private int rxBuffPtr;

        public CommPort()
        {
            com = new SerialPort();
            com.DataReceived += Com_DataReceived;
            com.ErrorReceived += Com_ErrorReceived;
            com.PinChanged += Com_PinChanged;

            //fill in DCB
            com.Handshake = Handshake.None;

            //set default comm paraams
            PortName = "COM1";
            BaudRate = 19200;
            DataBits = 8;
            StopBits = StopBits.One;
            Parity = Parity.None;

            DtrMode = false;
            RtsMode = false;

            ClearRxBuffer();

        }

        ~CommPort()
        {
            com.Close();
        }

        //------------------------------------------------------------------------------
        //                             open/close
        //------------------------------------------------------------------------------
        private void SetOpen(bool value)
        {
            if (value == Open)
                return;
            if (value)
                OpenPort();
            else
                ClosePort();
        }

        public void OpenPort()  //TODO private?
        {
            com.Open();
        }

        private void ClosePort()
        {
            com.PinChanged -= Com_PinChanged;            
            com.ErrorReceived -= Com_ErrorReceived;
            com.DataReceived -= Com_DataReceived;

            PurgeRx();
            PurgeTx();            
            com.Close();
        }

        // COM3,115200,N,8,1
        public void ConfigurePort(string config)
        {
            int n;

            string[] p = config.Split(',');
            for (int i = 0; i < p.Length; i++)
            {
                if (i == 0)
                    com.PortName = p[i];
                if ((i == 1) && int.TryParse(p[i], out n))
                    com.BaudRate = n;
                if (i == 2)
                {
                    com.Parity = ToParity(p[i]);
                }
                if ((i == 3) && int.TryParse(p[i], out n))
                    com.DataBits = n;
                if (i == 4)
                {
                    com.StopBits = ToStopBits(p[i]);
                }
            }

        }

        public static Parity ToParity(string parity)
        {
            switch (parity)
            {
                case "N": return Parity.None;
                case "E": return Parity.Even;
                case "O": return Parity.Odd;
                case "M": return Parity.Mark;
                case "S": return Parity.Space;
            }
            return Parity.None;
        }

        public static string FromParity(Parity parity)
        {
            switch (parity)
            {
                case Parity.None: return "N";
                case Parity.Even: return "E";
                case Parity.Odd: return "O";
                case Parity.Mark: return "M";
                case Parity.Space: return "S";
            }
            return "N";
        }

        public static StopBits ToStopBits(string stop)
        {
            switch (stop)
            {
                case "0": return StopBits.None;
                case "1": return StopBits.One;
                case "15": case "1.5": return StopBits.OnePointFive;
                case "2": return StopBits.Two;
            }
            return StopBits.None;

        }

        public static string FromStopBits(StopBits stop)
        {
            switch (stop)
            {
                case StopBits.None: return "0";
                case StopBits.One: return "1";
                case StopBits.OnePointFive: return "15";
                case StopBits.Two: return "2";
            }
            return "0";

        }

        private void ClearRxBuffer()
        {
            rxBuffer = new byte[BUF_SIZE];
            rxBuffPtr = 0;
        }


        //------------------------------------------------------------------------------
        //                             read/write
        //------------------------------------------------------------------------------
        public void PurgeRx()
        {
            com.DiscardInBuffer();
            ClearRxBuffer();
        }

        public void PurgeTx()
        {
            com.DiscardOutBuffer();
        }

        public void Send(byte[] buff)
        {
            if ((buff == null) || (buff.Length == 0))
                return;

            try
            {
                com.Write(buff, 0, buff.Length);
                Task.Run(async () =>
                {
                    while (com.BytesToWrite > 0)
                    {
                        await Task.Delay(10);
                    }
                    OnSent?.Invoke(this);
                });                               
            }
            catch
            {
                OnError?.Invoke(this, 0);
            }

        }

        private void SetDtrMode(bool value)
        {
            com.DtrEnable = value;
        }

        private void SetRtsMode(bool value)
        {
            com.RtsEnable = value;
        }


        //------------------------------------------------------------------------------
        //                             events
        //------------------------------------------------------------------------------
        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            bool Fire = true;

            Monitor.Enter(rxBuffer);
            try
            {
                //read rx count
                int available = com.BytesToRead;
                if (available == 0)
                    return;

                //read bytes
                available = com.Read(rxBuffer, rxBuffPtr, available);
                rxBuffPtr += available;
                
                switch (RxBlockMode)
                {
                    case TRxBlockMode.rbBlockSize:
                        Fire = (rxBuffPtr >= RxBlockSize);
                        break;
                    case TRxBlockMode.rbTerminator:
                        byte[] buff = RxBuffer;
                        string text = ByteArray.BytesToStr(buff);
                        Fire = text.Contains(RxBlockTerminator);
                        break;
                }
            }
            finally
            {
                Monitor.Exit(rxBuffer);
            }

            //purge buffer
            if (Fire)
            {
                OnReceived?.Invoke(this);
                ClearRxBuffer();
            }

        }

        private void Com_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            OnCtsDsr?.Invoke(this, e.EventType);
        }

        private void Com_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            OnError?.Invoke(this, e.EventType);
        }

    }
}
