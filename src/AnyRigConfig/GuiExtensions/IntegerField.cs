using Terminal.Gui;

namespace AnyRigConfig.GuiExtensions
{
    internal class IntegerField : TextField
    {

        public int Value
        {
            get => int.Parse(Text.ToString());
            set
            {
                Text = value.ToString();
            }
        }

        public override bool ProcessKey(KeyEvent kb)
        {
            bool result = false;

            switch (kb.Key)
            {
				case Key.DeleteChar:
				case Key.D | Key.CtrlMask:
				case Key.Delete:
				case Key.Backspace:
				case Key.Home:
				case Key.A | Key.CtrlMask:
				case Key.CursorLeft:
				case Key.B | Key.CtrlMask:
				case Key.End:
				case Key.E | Key.CtrlMask: // End
				case Key.CursorRight:
				case Key.F | Key.CtrlMask:
                    result = base.ProcessKey(kb);
                    if (Text.IsEmpty)
                        Text = "0";
                    break;
			}

            if ((kb.KeyValue >= '0') && (kb.KeyValue <= '9'))
            {
                result = base.ProcessKey(kb);
            }

            return result;
        }
    }
}
