using System;
using System.Windows.Forms;
using AnyRigConfigCommon;

namespace WinAnyRigConfig
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {

            InitializeComponent();

            LblName.Text = Application.ProductName;
            LblVersion.Text = $"Ver {Application.ProductVersion}";            
            LblCopyright.Text = $"Copyright 2022-{DateTime.Now.Year} by IW1QLH";

        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel ll = sender as LinkLabel;
            if (ll != null)
                BrowserHelpers.OpenBrowser(ll.Text);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PictureArs_DoubleClick(object sender, EventArgs e)
        {
            int n = 0;
            Console.WriteLine(1 / n);
        }

        private void FrmAbout_Shown(object sender, EventArgs e)
        {
            LblServiceVersion.Text = ConfigCommon.GetServiceVersion();
        }
    }
}
