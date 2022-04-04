using NStack;
using System.Collections;
using Terminal.Gui;

namespace AnyRigConfig.GuiExtensions
{
    internal class DropDownList : View
    {

        public Action SelectedItemChanged { get; set; }
        public override ustring Text 
        { 
            get => textLabel?.Text; 
            set { SetText(value); }
        }

        private IList source;
        public IList Source 
        { 
            get => source; 
            set
            {
                source = value;
                if ((source != null) && (source.Count > 0))
                {
                    SetText(source[0].ToString());
                }
            }
        }

        private ustring title;
        private Label textLabel;
        private Label downLabel;


        public DropDownList(int x, int y, int w, ustring Title) : base(x, y, "")
        {
            Width = w;
            Height = 1;            
            title = Title;
            Initialize();
        }

        private void Initialize()
        {
            textLabel = new Label()
            {
                X = 1,
                Y = 0,
                Width = this.Width - 1,
                Height = 1,
                Text = title
            };
            textLabel.Clicked += DropDownList_Clicked;
            this.Add(textLabel);

            downLabel = new Label()
            {
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
                Text = Driver.DownArrow.ToString()
            };
            downLabel.Clicked += DropDownList_Clicked;
            this.Add(downLabel);
        }

        private void SetText(ustring value)
        {
            if ((source == null) || (source.Count == 0))
            {
                if (textLabel != null)
                    textLabel.Text = "";
                return;
            }

            string selected = source[0].ToString();
            for (int i = 0; i < source.Count; i++)
            {
                string s = source[i].ToString();
                if (s == value)
                    selected = s;
            }

            if (textLabel.Text != selected)
            {
                textLabel.Text = selected;
                SelectedItemChanged?.Invoke();
            }

        }


        private void DropDownList_Clicked()
        {
            if (source == null)
                return;

            DropDownDialog dialog = new DropDownDialog(this, this.X, this.Y, title, source);
            Application.Run(dialog);
        }


        private class DropDownDialog : Dialog
        {
            private readonly DropDownList parent;
            RadioGroup listView;
            private IList source;

            public DropDownDialog(DropDownList Parent, Pos X, Pos Y, ustring title, IList Source)
            {
                if (Source == null)
                    return;

                int scrollHeight = Math.Min(Source.Count, 18);

                this.X = Pos.Center();
                this.Y = Pos.Center();
                Height = scrollHeight + 4;
                Title = title;

                parent = Parent;

                int selected = -1;
                int max = Math.Max(9, title.Length);
                ustring[] labels = new ustring[Source.Count];
                for (int i = 0; i < Source.Count; i++)
                {
                    string value = Source[i].ToString();
                    labels[i] = value;
                    int len = value.Length;
                    if (len > max)
                        max = len;
                    if (Parent.Text == value)
                        selected = i;
                }
                Width = max + 6;

                var scrollView = new ScrollView(new Rect(0, 0, max + 4, scrollHeight))
                {
                    //ColorScheme = Colors.TopLevel,
                    ContentSize = new Size(max, Source.Count),
                    //ContentOffset = new Point (0, 0),
                    ShowVerticalScrollIndicator = true,
                    //ShowHorizontalScrollIndicator = true,
                };

                source = Source;                
                //listView = new ListView(Source)
                listView = new RadioGroup(labels)
                {
                    //AllowsMarking = true,
                    //AllowsMultipleSelection = false,
                    X = 1,
                    Width = this.Width - 2,
                    Height = Source.Count
                };
                if (selected != -1)
                    listView.SelectedItem = selected;

                scrollView.Add(listView);
                this.Add(scrollView);

                Button btnClose = new Button("Close")
                {
                    X = Pos.Center(),
                    Y = Pos.AnchorEnd(1)
                };
                btnClose.Clicked += () =>
                {
                    string value = Source[listView.SelectedItem].ToString();
                    if (value != parent.Text)
                    {
                        parent.Text = value;
                        parent.SelectedItemChanged?.Invoke();
                    }
                    Application.RequestStop();
                };
                this.Add(btnClose);

            }

        }


    }

}
