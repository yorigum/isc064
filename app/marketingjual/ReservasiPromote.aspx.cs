using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class ReservasiPromote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("NoReservasi");

            DataTable rs = Db.Rs(
                "SELECT * FROM MS_RESERVASI WHERE NoReservasi = " + NoReservasi);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                DataTable rsDetail = Db.Rs("SELECT "
                    + " MS_RESERVASI.NoUnit AS [Unit]"
                    + ",MS_RESERVASI.NoStock AS [No. Stock]"
                    + ",MS_RESERVASI.NoUrut AS [No. Urut]"
                    + ",MS_CUSTOMER.Nama AS [Customer]"
                    + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS [Agent]"
                    + ",CONVERT(varchar,MS_RESERVASI.Tgl,106) AS [Tanggal]"
                    + ",CONVERT(varchar,MS_RESERVASI.TglExpire,100) AS [Batas Waktu]"
                    + ",MS_RESERVASI.Netto AS [Nilai Pengikatan]"
                    + ",MS_RESERVASI.Skema AS [Skema]"
                    + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                    + " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
                    + " WHERE MS_RESERVASI.NoReservasi = " + NoReservasi
                    );

                Db.Execute("EXEC spReservasiPromote " + NoReservasi);

                //Log
                Db.Execute("EXEC spLogReservasi "
                    + " 'PWL'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rsDetail) + "'"
                    + ",'" + NoReservasi.ToString().PadLeft(5, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_RESERVASI_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = (SELECT NoStock FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "')");
                Db.Execute("UPDATE MS_RESERVASI_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("UnitWL.aspx?NoStock=" + rs.Rows[0]["NoStock"]);
            }
        }

        private string NoReservasi
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoReservasi"]);
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
