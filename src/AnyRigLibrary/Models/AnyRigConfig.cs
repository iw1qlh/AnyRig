namespace AnyRigLibrary.Models
{
    public class AnyRigConfig
    {
        public int SocketPort { get; set; } = 4532;
        public bool SocketEnabled { get; set; }
        public bool NetpipeEnabled { get; set; }
        public int WebSocketPort { get; set; } = 8081;
        public bool WebSocketEnabled { get; set; }
        public string HrdUser { get; set; } = "TEST";
        public string UploadCode { get; set; } = "0000000000";
        public string ConfigExePath { get; set; }
        public RigSettings[] Rigs { get; set; }
        
    }
}
