namespace AnyRigLibrary.Models
{
    public class RigData
    {
        public int RigIndex { get; set; }
        public string RigType { get; set; }
        public bool IsOnLine { get; set; }
        public long? Freq { get; set; }
        public long? FreqA { get; set; }
        public long? FreqB { get; set; }
        public int? Pitch { get; set; }
        public int? RitOffset { get; set; }
        public string Mode { get; set; }
        public bool? Rit { get; set; }
        public bool? Split { get; set; }
        public bool? Tx { get; set; }
        public string Vfo { get; set; }
        public bool? Xit { get; set; }
    }
}
