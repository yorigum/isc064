using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class PanggilNUPInputLED : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
        }
        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isPilih(ddlstatus))
            {
                x = false;
                if (s == "") s = ddlstatus.ID;
                ddlstatusc.Text = "*Pilih Satu";
            }
            else
                ddlstatusc.Text = "";
            return x;
        }
        protected void display_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Response.Redirect("PanggilNUPTampilLED.aspx?j=" + input.Text + "&s=" + ddlstatus.SelectedValue);
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}
