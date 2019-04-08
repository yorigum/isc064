using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class UnitCancel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
            }
            Save();
        }

        private void Save()
        {
            Db.Execute("DELETE FROM MS_NUP_PRIORITY WHERE NoNUP= '" + NoPilihan + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");
            Db.Execute("UPDATE MS_NUP SET Status=1 WHERE NoNUP= '" + NoPilihan + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");
            Db.Execute("UPDATE MS_UNIT SET Status = 'A' WHERE NoStock = '" + NoStock + "' AND JenisProperti = '" + Tipe + "'");

            Response.Redirect("ClosingNUP2.aspx?No=" + NoNUP + "&Tipe=" + Tipe + "&project=" + Project);
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }

        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }
        private string NoPilihan
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoPil"]);
            }
        }
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }
    }
}