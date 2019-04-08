using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class SkomDel : System.Web.UI.Page
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
                    "Apakah anda ingin menghapus skema komisi : " + Nomor + " ?\\n"
                    + "Perhatian bahwa data akan dihapus secara PERMANEN."
                    );
            }
        }

        protected void delbtn_Click(object sender, System.EventArgs e)
        {
            DataTable rs = Db.Rs(
                "SELECT * FROM REF_SKOM WHERE Nomor = " + Nomor);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                DataTable rsHeader = Db.Rs("SELECT "
                    + " Nomor"
                    + ",Nama"
                    + ",Status"
                    + " FROM REF_SKOM "
                    + " WHERE Nomor = " + Nomor);

                DataTable rsDetail = Db.Rs("SELECT [Baris] "
                                  + " ,[Tipe] "
                                  + " ,[Nama] "
                                  + " ,[Nominal] "
                                  + " ,case when [TipeNominal]='%' then 'Persen' else 'Nominal' end as [Tipe Nominal] "
                                  + "  FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM_DETAIL] where Nomor='" + Nomor + "'");
                DataTable rsTermin = Db.Rs("SELECT [Baris] "
                                  + " ,[Nama] "
                                  + " ,[PersenLv] "
                                  + " ,[Lunas] "
                                  + " ,[NilaiLunas] "
                                  + " ,[BF] "
                                  + " ,[NilaiBF] "
                                  + " ,[DP] "
                                  + " ,[NilaiDP] "
                                  + " ,[ANG] "
                                  + " ,[NilaiANG] "
                                  + " ,[PPJB] "
                                  + " ,[Akad] "
                                  + " ,[Mode] "
                                  + "  FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM_TERM] where Nomor='" + Nomor + "'");

                Db.Execute("EXEC spSkomDel "
                    + Nomor
                    );

                string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                    + "<br><br>***Data Sebelum Delete :<br>"
                    + Cf.LogCapture(rsHeader)
                    + " ------DETAILS-------" + Cf.LogCapture(rsDetail)
                    + " -------TERMIN-------" + Cf.LogCapture(rsTermin);

                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM REF_SKOM WHERE NoSkema = " + Nomor);

                if (c == 0)
                {
                    Db.Execute("EXEC spLogSkom"
                        + " 'DELETE'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + Nomor.PadLeft(3, '0') + "'"
                        );
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
