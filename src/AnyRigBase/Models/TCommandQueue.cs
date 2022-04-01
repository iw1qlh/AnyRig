using System.Collections.Generic;
using System.Linq;

namespace AnyRigBase.Models
{
    public enum TCommandKind { ckInit, ckWrite, ckStatus, ckCustom };
    public enum TExchangePhase { phSending, phReceiving, phIdle };

    public class TQueueItem
    {
        public byte[] Code;
        public TCommandKind Kind;
        
        public TRigParam Param;      // param of Set comand
        public int Number;           // ordinal number of Init or Status command
        public object CustSender;    // COM object that sent custom command
        public  int ReplyLength;
        public string ReplyEnd;

        public bool NeedsReply()
        {
            return ((ReplyLength > 0) || !string.IsNullOrEmpty(ReplyEnd));
        }

    }

    public class TCommandQueue : List<TQueueItem>
    {
        public TExchangePhase Phase;

        public bool HasStatusCommands
        {
            get => this.Any(w => w.Kind == TCommandKind.ckStatus);
        }

        public TQueueItem CurrentCmd
        {
            get => this.FirstOrDefault();
        }

        internal void Delete(int i)
        {
            this.RemoveAt(i);
        }
    }
}
