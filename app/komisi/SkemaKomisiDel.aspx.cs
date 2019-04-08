using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
{
    public partial class SkemaKomisiDel : System.Web.UI.Page
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
                "SELECT * FROM REF_SKOM WHERE NoSkema = " + Nomor);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                DataTable rsHeader = Db.Rs("SELECT "
                    + " NoSkema"
                    + ",SalesTipe"
                    + ",Nama"
                    + ",CONVERT(varchar,Dari,106) AS [Periode Dari]"
                    + ",CONVERT(varchar,Sampai,106) AS [Periode Sampai]"
                    + ",Rumus AS [Rumus Komisi]"
                    + ",DasarHitung AS [Dasar Perhitungan]"
                    + ",Inaktif AS [Status Inaktif]"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM "
                    + " WHERE NoSkema = " + Nomor);

                DataTable rsDetail = Db.Rs("SELECT [SN] "
                                  + " ,(SELECT Nama FROM REF_AGENT_LEVEL WHERE LevelID = SalesLevel) AS [Level] "
                                  + " ,[Nilai] "
                                  + " ,CASE WHEN [TipeTarif]='%' THEN 'Persen' ELSE 'Nominal' END AS [Tipe Tarif] "
                                  + "  FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM_DETAIL] WHERE NoSkema = " + Nomor);

                DataTable rsDetail2 = Db.Rs("SELECT [SN] "
                                  + " ,(SELECT Nama FROM REF_AGENT_LEVEL WHERE LevelID = SalesLevel) AS [Level] "
                                  + " ,[TipeTarget] "
                                  + " ,[TargetBawah] "
                                  + " ,[TargetAtas] "
                                  + " ,[Nilai] "
                                  + " ,CASE WHEN [TipeTarif]='%' THEN 'Persen' ELSE 'Nominal' END AS [Tipe Tarif] "
                                  + "  FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM_DETAIL2] WHERE NoSkema = " + Nomor);

                Db.Execute("EXEC spSkomDel "
                    + Nomor
                    );

                string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                    + "<br><br>***Data Sebelum Delete :<br>"
                    + Cf.LogCapture(rsHeader)
                    + " ------RUMUS UNIT-------" + Cf.LogCapture(rsDetail)
                    + " -------RUMUS KUMULATIF/PROGRESIF-------" + Cf.LogCapture(rsDetail2);

                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM REF_SKOM WHERE NoSkema = " + Nomor);

                if (c == 0)
                {
                    Db.Execute("EXEC spLogSkom"
                        + " 'DELETE'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + Nomor.PadLeft(5, '0') + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_SKOM_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM REF_SKOM WHERE NoSkema = " + Nomor);
                    Db.Execute("UPDATE REF_SKOM_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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
