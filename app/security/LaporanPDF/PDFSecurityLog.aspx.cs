using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY.Laporan
{
    public partial class SecurityLog : System.Web.UI.Page
    {

        private string NoCustomer { get { return (Request.QueryString["NoCustomer"]); } }
        private string Aktivitas { get { return (Request.QueryString["aktivitas"]); } }
        private string User { get { return (Request.QueryString["user"]); } }
        private string AttachmentID { get { return Request.QueryString["id"]; } }
        private string SecLevel { get { return (Request.QueryString["seclevel"]); } }
        private string IP { get { return (Request.QueryString["ip"]); } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }



        private void Report()
        {
            rpt.Visible = true;

            Header();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            string[] words = Aktivitas.Split('-');
            bool x1 = true;
            string nAktivitas = "";

            foreach (string akt in words)
            {
                nAktivitas = akt.ToString();
            }

            Rpt.SubJudul(x
                , "Tanggal Log : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            Rpt.SubJudul(x
                , "Aktivitas : " + Aktivitas.Replace("-", ",")//Rpt.inSql(Aktivitas).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "User : " + User//user.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Security Level : " + SecLevel//seclevel.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "IP Address : " + IP //ip.SelectedItem.Text
                );

            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            string UserID = "";
            if (User != "SEMUA")
                UserID = " AND UserID = '" + User + "'";

            string nSecLevel = "";
            if (SecLevel != "SEMUA")
                nSecLevel = " AND SecLevel = '" + SecLevel + "'";

            string nIP = "";
            if (IP != "0" && IP != "SEMUA")
                nIP = " AND IP = '" + IP + "'";

            //change parameter
            string akt = String.Empty;
            akt = Aktivitas.Replace("-",",").TrimEnd(',');
            akt = akt.Replace(",", "','");

            akt = "'" + akt + "'";


            string strSql = "SELECT "
            + " LogID"
            + ",Tgl"
            + ",CASE Aktivitas "
            + "		WHEN 'L' THEN 'Log-In Normal'"
            + "		WHEN 'S' THEN 'Sign-Out Normal'"
            + "		WHEN 'DL' THEN 'Double Login'"
            + "		WHEN 'SP' THEN 'Salah Password'"
            + "		WHEN 'B' THEN 'Blokir'"
            + "		WHEN 'A' THEN 'Aktivasi'"
            + "		WHEN 'GP' THEN 'Ganti Password'"
            + " END AS AktivitasDetil"
            + ",UserID"
            + ",Nama"
            + ",SecLevel"
            + ",IP"
            + " FROM SECURITY_LOG"
            + " WHERE 1=1 "
            + " AND CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
            + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
            + " AND Aktivitas IN (" + akt + ")"//Rpt.inSql(Aktivitas.Replace("-",",")) + ")"
            + UserID
            + nSecLevel
            + nIP
            + " ORDER BY LogID";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = rs.Rows[i]["LogID"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Time(rs.Rows[i]["Tgl"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["AktivitasDetil"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["UserID"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["SecLevel"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["IP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
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
