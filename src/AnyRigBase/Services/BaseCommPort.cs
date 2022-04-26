using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using AnyRigBase.Helpers;
//using static AnyRigBase.Services.MultiCommPort;

namespace AnyRigBase.Services
{
    public abstract class BaseCommPort
    {
        public enum TRxBlockMode { rbChar, rbBlockSize, rbTerminator };

        public Action<object> OnSent { get; set; }
        public Action<object> OnReceived { get; set; }
        public Action<object, object> OnError { get; set; }
        public Action<object, object> OnChanges { get; set; }

        string commName;

        public string CommName
        {
            get { return commName; }
            set 
            { 
                SetName(value);
                commName = value;
            }
        }

        public bool Open { get => IsOpen(); set { SetOpen(value); } }
        public int TxQueue { get => GetTxQueue(); }
        public byte[] RxBuffer { get => GetRxBuffer(); }

        public void SetOpen(bool value)
        {
            if (value == Open)
                return;
            if (value)
                OpenPort();
            else
                ClosePort();
        }

        abstract public void SetName(string value);
        abstract public void OpenPort();
        abstract public void ClosePort();
        abstract public bool IsOpen();
        abstract public int GetTxQueue();
        abstract public void PurgeRx();
        abstract public void PurgeTx();
        abstract public void Send(byte[] buff);

        public TRxBlockMode RxBlockMode;
        public int RxBlockSize;
        public string RxBlockTerminator;

        const int BUF_SIZE = 1024;
        private byte[] rxBuffer;
        private int rxBuffPtr;

        protected void ClearRxBuffer()
        {
            rxBuffer = new byte[BUF_SIZE];
            rxBuffPtr = 0;
        }

        public byte[] GetRxBuffer()
        {
            int size = rxBuffPtr;
            if (size == 0)
                return new byte[0];
            byte[] buff = new byte[size];
            Array.Copy(rxBuffer, buff, size);
            return buff;
        }

        protected void DataReceived(int dataLen, byte[] data)
        {
            if (dataLen > data.Length)
                return;

            bool Fire = true;

            Monitor.Enter(rxBuffer);
            try
            {
                Array.Copy(data, 0, rxBuffer, rxBuffPtr, dataLen);
                rxBuffPtr += dataLen;
                
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

    }


}

