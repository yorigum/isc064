using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.LAUNCHING
{
    public partial class UnitPilih : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_UNIT"
                        + " WHERE Status = 'A' AND NoStock = '" + NoStock + "'");
            if (c != 0)
            {
                Db.Execute("UPDATE MS_UNIT SET STATUS = 'H' WHERE NoStock = '" + NoStock + "'");
                Db.Execute("INSERT INTO MS_NUP_PRIORITY(NoNUP, NoStock, NoNUPHeader,NomorSkema,Tipe,Project) VALUES('" + NoNUP + "', '" + NoStock + "', '" + NoNUP + "','','RUMAH','" + Project + "')");

                Response.Redirect("UnitPilih1.aspx?NoNUP=" + NoNUP + "&NoStock=" + NoStock + "&Tipe=" + Tipe + "&project=" + Project);
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
        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
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