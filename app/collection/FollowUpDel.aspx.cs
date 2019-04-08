using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class FollowUpDel : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("NoFU");
            Act.CekInt("Ref");

            if (!Page.IsPostBack)
            {
                Js.Focus(this, ket);
                Js.Confirm(this,
                    "Apakah anda ingin menghapus follow up : " + Ref + "." + NoFU + " ?\\n"
                    + "Perhatian bahwa data akan dihapus secara PERMANEN."
                    );
            }
        }

        protected void delbtn_Click(object sender, System.EventArgs e)
        {
            DataTable rs = Db.Rs(
                "select * from ISC064_marketingjual..ms_followup where NoFU = '" + NoFU + "' and NoKontrak ='" + Ref + "' ");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                    + "<br><br>***Data Sebelum Delete :<br>"
                    + Cf.LogCapture(rs);

                Db.Execute("EXEC ISC064_marketingjual..spFUDel "
                    + "'" + NoFU + "'"
                    + ",'" + Ref + "'"
                    );

                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoFU = " + NoFU + " and NoKontrak = '" + Ref + "'");

                if (c == 0)
                {
                    //Log
                    Db.Execute("EXEC ISC064_MARKETINGJUAL..spLogFU "
                        + " 'DELETE'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoFU.PadLeft(5, '0') + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_FOLLOWUP_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Ref + "'");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_FOLLOWUP_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    Js.Close(this);
                }
                else
                {
                    //Tidak bisa dihapus
                    frm.Visible = false;
                    nodel.Visible = true;
                }
            }
        }

        private string NoFU
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoFU"]);
            }
        }
        private string Ref
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
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
