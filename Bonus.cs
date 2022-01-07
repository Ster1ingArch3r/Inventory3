using System;
using System.Windows.Forms;

namespace TCIS_Inventory3
{
    public partial class Bonus : Form
    {
        public Bonus()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var embed = "<html><head>" +
            "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
            "</head><body>" +
            "<iframe width=\"530\" height=\"276\" src=\"{0}\"" +
            "frameborder = \"0\" allow = \"autoplay; encrypted-media\" allowfullscreen></iframe>" +
            "</body></html>";
            var url = "https://www.youtube.com/embed/q7xsh7DcFcc";
            this.webBrowser1.DocumentText = string.Format(embed, url);
        }

        private void Bonus_FormClosed(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}
