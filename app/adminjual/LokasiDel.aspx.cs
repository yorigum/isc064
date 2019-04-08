using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class LokasiDel : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Js.Focus(this, ket);
                string Lokasi = Db.SingleString("SELECT Lokasi FROM REF_Lokasi WHERE SN='" + NoLokasi + "'");
                Js.Confirm(this,
                    "Apakah anda ingin menghapus lokasi unit : " + Lokasi + " ?\\n"
                    + "Perhatian bahwa data akan dihapus secara PERMANEN."
                    );
            }
        }

        protected void delbtn_Click(object sender, System.EventArgs e)
        {
            DataTable rs = Db.Rs(
                "SELECT * FROM REF_LOKASI WHERE SN = '" + NoLokasi + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                    + "<br><br>***Data Sebelum Delete :<br>"
                    + Cf.LogCapture(rs);

                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM MS_UNIT WHERE Lokasi = '" + rs.Rows[0]["Lokasi"] + "'");

                if (c == 0)
                {
                    Db.Execute("EXEC spLogLokasi"
                  + " 'DELETE'"
                  + ",'" + Act.UserID + "'"
                  + ",'" + Act.IP + "'"
                  + ",'" + Ket + "'"
                  + ",'" + NoLokasi + "'"
                  );
                    Db.Execute("DELETE FROM REF_LOKASI WHERE SN = '" + NoLokasi + "'");
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

        private string NoLokasi
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoLokasi"]);
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
