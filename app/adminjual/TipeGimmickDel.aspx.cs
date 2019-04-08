using System;
using System.Data;

namespace ISC064.ADMINJUAL
{
    public partial class TipeGimmickDel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                if (Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK WHERE Tipe = '" + ID + "'") > 0)
                {
                    warning.Text = "Tipe Telah Terpakai";
                    delbtn.Enabled = false;
                }
                else
                    warning.Text = "";

                Js.Focus(this, ket);
                Js.Confirm(this,
                    "Apakah anda ingin menghapus kontrak : " + ID + " ?\\n"
                    + "Perhatian bahwa data akan dihapus secara PERMANEN."
                    );
            }
        }

        protected void delbtn_Click(object sender, System.EventArgs e)
        {
            DataTable rs = Db.Rs(
               "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE ID = '" + ID + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                    + "<br><br>***Data Sebelum Delete :<br>";

                Db.Execute("DELETE FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE ID = '" + ID + "'");

                //Log
                Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogTipeGimmick "
                    + " 'DELETE'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + ID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE ID = '" + ID + "'");
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Js.Close(this);
            }
        }

        private string ID
        {
            get
            {
                return Cf.Pk(Request.QueryString["Nomor"]);
            }
        }
    }
}