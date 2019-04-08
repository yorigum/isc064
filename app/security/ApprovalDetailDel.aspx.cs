using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.SECURITY
{
    public partial class ApprovalDetailDel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            DataTable rs = Db.Rs(
                "SELECT * FROM REF_APPROVAL WHERE UserID='" + UserID + "' AND Tipe=" + Tipe + " AND Lvl=" + Lvl);

            string w = "";

            if (Tipe == 1)
                w += " AND ApprovalGN = 1";
            else if (Tipe == 2)
                w += " AND ApprovalGU = 1";
            else if (Tipe == 3)
                w += " AND ApprovalBatal = 1";

            DataTable kontrak = Db.Rs(
                "SELECT * FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE Status <> 'B' " + w);

            int japp = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM REF_APPROVAL WHERE Tipe = " + Tipe + " AND Lvl = " + Lvl + " AND UserID != '" + UserID + "'");

            bool lvlaft = false;
            int laft = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM REF_APPROVAL WHERE Tipe = " + Tipe + " AND Lvl = " + (Lvl + 1));
            if (laft > 0)
            {
                lvlaft = true;
            }

            if (rs.Rows.Count == 0) 
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Approval Tidak Dapat Dihapus.\\n\\n"
                    + "Kemungkinan Terjadi Karena :\\n"
                    + "1. Data tidak ditemukan.\\n');window.location ='ApprovalDetailEdit.aspx?lvl=" + Lvl + "&tipe=" + Tipe + "';",
                    true);
            }
            else if (kontrak.Rows.Count > 0 && japp == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Approval Tidak Dapat Dihapus.\\n\\n"
                    + "Kemungkinan Terjadi Karena :\\n"
                    + "1. Masih terdapat kontrak yang harus di approve.\\n');window.location ='ApprovalDetailEdit.aspx?lvl=" + Lvl + "&tipe=" + Tipe + "';",
                    true);
            }
            else if (lvlaft && japp == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Approval Tidak Dapat Dihapus.\\n\\n"
                    + "Kemungkinan Terjadi Karena :\\n"
                    + "1. Level dibawahnya harus dihapus terlebih dahulu.\\n');window.location ='ApprovalDetailEdit.aspx?lvl=" + Lvl + "&tipe=" + Tipe + "';",
                    true);
            }
            else
            {
                Db.Execute("DELETE FROM REF_APPROVAL WHERE UserID='" + UserID + "' AND Tipe=" + Tipe + " AND Lvl=" + Lvl);

                Response.Redirect("ApprovalDetail.aspx?tipe=" + Tipe);
            }
        }

        private string UserID
        {
            get
            {
                return Request.QueryString["userid"];
            }
        }

        private int Lvl
        {
            get
            {
                return Convert.ToUInt16(Request.QueryString["lvl"]);
            }
        }

        private int Tipe
        {
            get
            {
                return Convert.ToUInt16(Request.QueryString["tipe"]);
            }
        }
    }
}