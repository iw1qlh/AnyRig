using AnyRigLibrary;
using AnyRigLibrary.Models;
using AnyRigNetWrapper;
using System.Diagnostics;
using TestCat.Helpers;

namespace TestCat
{
    public partial class Form1 : Form
    {
        IRigCore rig;
        //RigCore rigCore;

        public Form1()
        {
            InitializeComponent();

            BtnUsb.Tag = RigParam.USB;
            BtnLsb.Tag = RigParam.LSB;
            BtnCw.Tag = RigParam.CW;
            BtnFm.Tag = RigParam.FM;
            BtnAm.Tag = RigParam.AM;


            rbLibrary.Checked = true;
            rbMethod_Click(null, null);

            //OnChanges(0, new RigParam[0]);
          
        }

        private void OnChanges(int rx, RigParam[] changed)
        {
            if (rig == null)
                return;          

            LblChanges.InvokeIfRequired(lbl => { lbl.Text = $"{rx} {String.Join(",", changed.Select(s => Enum.GetName(typeof(RigParam), s)))}"; });

            LblFreq.InvokeIfRequired(lbl => lbl.Text = rig.Freq.ToString("0,000"));
            lblFreqA.InvokeIfRequired(lbl => lbl.Text = rig.FreqA.ToString("0,000"));
            lblFreqB.InvokeIfRequired(lbl => lbl.Text = rig.FreqB.ToString("0,000"));

            LblMode.InvokeIfRequired(lbl => lbl.Text = rig.Mode.ToString());

            nudRit.InvokeIfRequired(nud => nudRit.Value = rig.RitOffset);

            var regularfont = new Font(btnRit.Font.Name, btnRit.Font.Size, FontStyle.Regular);
            var boldFont = new Font(btnRit.Font.Name, btnRit.Font.Size, FontStyle.Bold);

            btnVfoA.InvokeIfRequired(btn => btn.Font = rig.Vfo == RigParam.VFOAA ? boldFont : regularfont);
            btnVfoB.InvokeIfRequired(btn => btn.Font = rig.Vfo == RigParam.VFOBB ? boldFont : regularfont);

            btnRit.InvokeIfRequired(btn => btn.Font = rig.Rit.GetValueOrDefault() ? boldFont : regularfont);
            btnXit.InvokeIfRequired(btn => btn.Font = rig.Xit.GetValueOrDefault() ? boldFont : regularfont);
            btnSplit.InvokeIfRequired(btn => btn.Font = rig.Split.GetValueOrDefault() ? boldFont : regularfont);
            btnTx.InvokeIfRequired(btn => btn.Font = rig.Tx.GetValueOrDefault() ? boldFont : regularfont);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //OnChanges(0, 0);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            rig?.Stop();
        }

        private void BtnMode_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;            
            if (btn == null)
                return;

            if (!(btn.Tag is RigParam))
                return;
            RigParam param = (RigParam)btn.Tag;

            rig.Mode = param;
        }

        private void rbMethod_Click(object sender, EventArgs e)
        {
            LblFreq.Text = "0";
            lblFreqA.Text = "0";
            lblFreqB.Text = "0";
            LblMode.Text = "---";
            LblChanges.Text = "";
            lblRigs.Text = "?";

            rig?.Stop();

            if (rbLibrary.Checked)
            {
                AnyRigConfig config = ConfigManager.Load();
                RigCore[] rigs = ConfigManager.LoadRigs(config);

                if (rigs.Length > 0)
                {
                    RigCore rigCore = rigs[0];

                    rigCore.Log += (text) => Debug.WriteLine(text);
                    rigCore.InternalLog += (text) => Debug.WriteLine(text);

                    rigCore.Start();

                    rig = rigCore;

                }

            }
            else if (rbSocket.Checked)
            {
                rig = new SocketRigWrapper();
            }
            else if (rbNetpipe.Checked)
            {
                rig = new NetpipeRigWrapper();
            }

            rig.NotifyChanges = (rx, changed) => OnChanges(rx, changed);

            var rigList = rig.GetRigsList();
            if (rigList != null)
                lblRigs.Text = string.Join(", ", rigList.Select(s => $"{s.RigType} {(s.IsOnLine ? "(on-line)" : "")}"));

            //OnChanges(0, new RigParam[0]);

        }

        private void btnRit_Click(object sender, EventArgs e)
        {
            rig.Rit = !rig.Rit;
        }

        private void btnXit_Click(object sender, EventArgs e)
        {
            rig.Xit = !rig.Xit;
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            rig.Split = !rig.Split;
        }

        private void btnTx_Click(object sender, EventArgs e)
        {
            rig.Tx = !rig.Tx;
        }

        private void btnVfoA_Click(object sender, EventArgs e)
        {
            rig.Vfo = RigParam.VFOA;
        }

        private void btnVfoB_Click(object sender, EventArgs e)
        {
            rig.Vfo = RigParam.VFOB;
        }

        private void btnClearRit_Click(object sender, EventArgs e)
        {
            rig.ClearRit();
        }

        private void nudRit_ValueChanged(object sender, EventArgs e)
        {
            rig.RitOffset = (int)nudRit.Value;

        }
    }
}
