using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
{
    public partial class TerminKomisiDel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("Nomor");

            if (!Page.IsPostBack)
            {
                Js.Focus(this, ket);
                Js.Confirm(this,
                    "Apakah anda ingin menghapus termin komisi : " + Nomor + " ?\\n"
                    + "Perhatian bahwa data akan dihapus secara PERMANEN."
                    );
            }
        }

        protected void delbtn_Click(object sender, System.EventArgs e)
        {
            DataTable rs = Db.Rs(
                "SELECT * FROM REF_SKOM_TERM WHERE NoTermin = " + Nomor);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                DataTable rsHeader = Db.Rs("SELECT "
                    + " NoTermin"
                    + ",Nama"
                    + ",CaraBayar AS [Cara Bayar]"
                    + ",Inaktif AS [Status Inaktif]"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_TERM "
                    + " WHERE NoTermin = " + Nomor);

                DataTable rsDetail = Db.Rs("SELECT [SN] "
                                  + " ,Nama "
                                  + " ,CONVERT(VARCHAR, PersenCair, 1) AS [Persen Cair] "
                                  + " ,CONVERT(VARCHAR, PersenBF, 1) AS [Persen BF] "
                                  + " ,CONVERT(VARCHAR, PersenDP, 1) AS [Persen DP] "
                                  + " ,CONVERT(VARCHAR, PersenANG, 1) AS [Persen ANG] "
                                  + " ,CASE WHEN [TipeCair]='0' THEN 'Semua' ELSE 'Salah Satu' END AS [Tipe Cair] "
                                  + " ,CASE WHEN [PPJB]='0' THEN 'Tidak' ELSE 'Ya' END AS [PPJB] "
                                  + " ,CASE WHEN [AJB]='0' THEN 'Tidak' ELSE 'Ya' END AS [AJB] "
                                  + " ,CASE WHEN [AKAD]='0' THEN 'Tidak' ELSE 'Ya' END AS [AKAD] "
                                  + "  FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM_TERM_DETAIL] WHERE NoTermin = " + Nomor);

                Db.Execute("EXEC spSkomTermDel "
                    + Nomor
                    );

                string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                    + "<br><br>***Data Sebelum Delete :<br>"
                    + Cf.LogCapture(rsHeader)
                    + " ------RUMUS TERMIN-------" + Cf.LogCapture(rsDetail);

                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM REF_SKOM_TERM WHERE NoTermin = " + Nomor);

                if (c == 0)
                {
                    Db.Execute("EXEC spLogSkomTerm"
                        + " 'DELETE'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + Nomor.PadLeft(5, '0') + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_SKOM_TERM_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM REF_SKOM_TERM WHERE NoTermin = " + Nomor);
                    Db.Execute("UPDATE REF_SKOM_TERM_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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

        private string Nomor
        {
            get
            {
                return Cf.Pk(Request.QueryString["Nomor"]);
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
