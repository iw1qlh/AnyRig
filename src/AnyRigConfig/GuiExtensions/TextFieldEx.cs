using Terminal.Gui;

namespace AnyRigConfig.GuiExtensions
{
    internal class TextFieldEx : TextField
    {

        public Func<bool> SpacePressed { get; set; }
        public Func<bool> EnterPressed { get; set; }
        public bool ToUpperCase { get; set; }

        public TextFieldEx(int x, int y, int w) : base(x, y, w, "")
        {
        }

        public override bool ProcessKey(KeyEvent kb)
        {
            bool result = false;
            bool process = true;

            if ((kb.KeyValue == (int)Key.Space) && (SpacePressed != null))
            {
                process = SpacePressed();
            }
            if ((kb.KeyValue == (int)Key.Enter) && (EnterPressed != null))
            {
                process = EnterPressed();
            }

            if (process)
            {
                if (ToUpperCase && (kb.KeyValue >= 'a') && (kb.KeyValue <= 'z'))
                {
                    KeyModifiers km = new KeyModifiers();
                    kb = new KeyEvent((Key)kb.KeyValue - 0x20, km);
                }
                result = base.ProcessKey(kb);
            }

            return result;
        }
    }
}
