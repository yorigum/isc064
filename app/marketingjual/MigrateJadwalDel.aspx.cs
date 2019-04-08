using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class MigrateJadwalDel : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (Request.QueryString["NoUrut"] != null)
            {
                Act.CekInt("NoUrut");

                DataTable rs = Db.Rs(
                    "SELECT * FROM MIGRATE_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'"
                    + " AND NoUrut = '" + Request.QueryString["NoUrut"] + "'");

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    Db.Execute("DELETE FROM MIGRATE_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" + Request.QueryString["NoUrut"] + "'");

                    Response.Redirect("MigrateJadwal2.aspx?NoKontrak=" + NoKontrak);
                }
            }
            else
            {
                DataTable rs = Db.Rs(
                    "SELECT * FROM MIGRATE_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    Db.Execute("DELETE FROM MIGRATE_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");

                    Response.Redirect("MigrateJadwal.aspx");
                }
            }
        }

        private string NoKontrak
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
