using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
    public partial class JenisDel : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Js.Focus(this, ket);
                string Jenis = Db.SingleString("SELECT JENIS FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS WHERE SN='" + NoJenis + "'");
                Js.Confirm(this,
                    "Apakah anda ingin menghapus jenis unit : " + Jenis + " ?\\n"
                    + "Perhatian bahwa data akan dihapus secara PERMANEN."
                    );
            }
        }

        protected void delbtn_Click(object sender, System.EventArgs e)
        {
            DataTable rs = Db.Rs(
                "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS WHERE SN = '" + NoJenis + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else if (!Act.AksesProject(rs.Rows[0]["Project"].ToString()))
                Response.Redirect("/CustomError/SecLevel.html");
            else
            {
                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE Jenis = '" + rs.Rows[0]["Jenis"] + "'");

                if (c == 0)
                {
                    string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                    + "<br><br>***Data Sebelum Delete :<br>"
                    + Cf.LogCapture(rs);

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogJenis"
                    + " 'DELETE'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoJenis + "'"
                    );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS WHERE SN = '" + NoJenis + "'");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    Db.Execute("DELETE FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS WHERE SN = '" + NoJenis + "'");
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

        private string NoJenis
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoJenis"]);
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
