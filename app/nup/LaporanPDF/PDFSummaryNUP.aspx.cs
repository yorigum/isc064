using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace ISC064.NUP.Laporan
{
    /// <summary>
    /// Summary description for LaporanSalesPerformance.
    /// </summary>
    public partial class SummaryNUP : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Tipe { get { return (Request.QueryString["tipe"]); } }
        private string Admin { get { return (Request.QueryString["admin"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        protected void Report()
        {
            lblHeader.Text = Mi.Pt
                    + "<br />"
                    + "Laporan NUP"
                    ;

            System.Text.StringBuilder x = new System.Text.StringBuilder();
            DateTime Tanggal1 = Db.SingleTime("SELECT FilterDari FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'");
            DateTime Tanggal2 = Db.SingleTime("SELECT FilterSampai FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'");
            string addStr = "Semua Admin";

            if (Admin != "SEMUA")
                addStr = Db.SingleString("SELECT Nama FROM ISC064_SECURITY..USERNAME WHERE UserID='" + Admin + "'");


            x.Append("<br />Untuk tanggal : " + Cf.Day(Tanggal1) + " s/d " + Cf.Day(Tanggal2));
            x.Append("<br />Untuk Project : " + Cf.Str(Project));
            x.Append("<br />Untuk Admin : " + Cf.Str(addStr));
            x.Append("<br />Untuk Tipe : " + Tipe);

            x.Append("<br /><span style='font-weight: normal;'>Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
                + ", " + Cf.Date(DateTime.Now)
                + " dari workstation : " + Act.IP
                + " dan username : " + Act.UserID
                + "</span>"
                );

            lblSubHeader.Text = x.ToString();

            Fill();
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }


        private void Fill()
        {
            DateTime Tanggal1 = Db.SingleTime("SELECT FilterDari FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'");
            DateTime Tanggal2 = Db.SingleTime("SELECT FilterSampai FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'");

            string addSql = "";

            if (Admin != "SEMUA")
                addSql += " AND UserInputID = '" + Admin + "'";

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND Project IN ('" + Project.Replace(",", "','") + "')";

            string nTipe = Tipe == "SEMUA" ? "" : " AND Tipe = '" + Tipe + "'";

            string strSql = "SELECT * FROM MS_NUP WHERE 1=1"
                    + " AND CONVERT(DATETIME,TglDaftar,112) BETWEEN '" + Cf.Tgl112(Tanggal1) + "' AND '" + Cf.Tgl112(Tanggal2) + "'"
                    + nTipe
                    + nProject
                    + addSql
                    + " ORDER BY Tipe DESC, NoNUP ASC";

            DataTable dtNUP = Db.Rs(strSql);
            decimal Rm1 = 0, Rm2 = 0, Rm3 = 0, Rk = 0, Kv = 0, Kv2 = 0;

            int no = 0;
            for (int i = 0; i < dtNUP.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = (no + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                //Tgl NUP
                c = new TableCell();
                c.Text = Cf.Day(dtNUP.Rows[i]["TglDaftar"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //NoNUP
                c = new TableCell();
                string cetakn = dtNUP.Rows[i]["NoNUP"].ToString();

                if (Convert.ToInt32(dtNUP.Rows[i]["Revisi"].ToString()) > 0)
                    cetakn = cetakn + "R";
                c.Text = Cf.Str(cetakn);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string namaCS = "";
                string noKTP = "";
                string alamatCS = "";
                string telpCS = "";
                string bank = "", nrek = "";
                DataTable dtCs = Db.Rs("SELECT * FROM MS_CUSTOMER WHERE NoCustomer=" + Convert.ToInt32(dtNUP.Rows[i]["NoCustomer"]));

                if (dtCs.Rows.Count > 0)
                {
                    namaCS = dtCs.Rows[0]["Nama"].ToString();
                    noKTP = dtCs.Rows[0]["NoKTP"].ToString();
                    telpCS = dtCs.Rows[0]["NoTelp"].ToString();
                    bank = dtCs.Rows[0]["RekBank"].ToString();
                    nrek = dtCs.Rows[0]["RekNo"].ToString();
                    alamatCS = dtCs.Rows[0]["KTP1"].ToString() + " " + dtCs.Rows[0]["KTP1"].ToString() + " " + dtCs.Rows[0]["KTP2"].ToString() + " " + dtCs.Rows[0]["KTP3"].ToString() + " " + dtCs.Rows[0]["KTP4"].ToString();
                }

                //Nama Customer
                c = new TableCell();
                c.Text = Cf.Str(namaCS);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Alamat Customer
                c = new TableCell();
                c.Text = Cf.Str(alamatCS);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Telpon Customer
                c = new TableCell();
                c.Text = Cf.Str(telpCS);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string namaAG = "";
                string telpAG = "";
                DataTable dtAG = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent=" + Convert.ToInt32(dtNUP.Rows[i]["NoAgent"]));

                if (dtAG.Rows.Count > 0)
                {
                    namaAG = dtAG.Rows[0]["Nama"].ToString();
                    telpAG = dtAG.Rows[0]["Kontak"].ToString();
                }

                //Agent
                c = new TableCell();
                c.Text = Cf.Str(namaAG);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //telp Agent
                c = new TableCell();
                c.Text = Cf.Str(telpAG);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Unit
                c = new TableCell();
                string noUnit = Db.SingleString("SELECT ISNULL(NoStock,' ') FROM MS_NUP_PRIORITY WHERE NoNUP='" + dtNUP.Rows[i]["NoNUP"].ToString() + "' AND Tipe = '" + dtNUP.Rows[i]["Tipe"].ToString() + "'");
                c.Text = noUnit;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Tipe
                c = new TableCell();
                c.Text = Cf.Str(dtNUP.Rows[i]["Tipe"].ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //bank
                c = new TableCell();
                c.Text = bank + " " + nrek;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //penginput
                c = new TableCell();
                c.Text = dtNUP.Rows[i]["UserInputNama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Nilai Pembayaran
                c = new TableCell();
                decimal nBayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNUP='" + dtNUP.Rows[i]["NoNUP"].ToString() + "' AND Tipe = '" + dtNUP.Rows[i]["Tipe"].ToString() + "'");
                c.Text = Cf.Num(nBayar);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                no++;
                DataTable Jenis = Db.Rs("SELECT Nama FROM REF_JENISPROPERTI WHERE Project = '" + Project + "'");
                for (int j = 0; j < Jenis.Rows.Count; j++)
                {

                }
                //if (dtNUP.Rows[i]["Tipe"].ToString() == Jenis.Rows[]["Nama"].ToString())
                //    Rm1 += 1;
                //else if (dtNUP.Rows[i]["Tipe"].ToString() == "RUMAH 36/72")
                //    Rm2 += 1;
                //else if (dtNUP.Rows[i]["Tipe"].ToString() == "RUMAH 45/105")
                //    Rm3 += 1;
                //else if (dtNUP.Rows[i]["Tipe"].ToString() == "RUKO")
                //    Rk += 1;
                //else if (dtNUP.Rows[i]["Tipe"].ToString() == "KAVLING")
                //    Kv += 1;
                //else if (dtNUP.Rows[i]["Tipe"].ToString() == "KAVLING KOMERSIAL")
                //    Kv2 += 1;
            }

            Detail(Rm1, Rm2, Rm3, Rk, Kv, Kv2);
        }

        private void Detail(decimal Rm1, decimal Rm2, decimal Rm3, decimal Rk, decimal Kv, decimal Kv2)
        {
            DateTime Tanggal1 = Db.SingleTime("SELECT FilterDari FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'");
            DateTime Tanggal2 = Db.SingleTime("SELECT FilterSampai FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'");

            string addSql = "";

            if (Admin != "SEMUA")
                addSql += " AND UserInputID = '" + Admin + "'";

            TableRow r;
            TableCell c;

            r = new TableRow();

            c = new TableCell();
            DataTable Jenis = Db.Rs("SELECT * FROM REF_JENISPROPERTI WHERE Project = '" + Project + "'");
            c.ColumnSpan = 15;
            string JenisPro = "";
            for (int i = 0; i < Jenis.Rows.Count; i++)
            {
                int Jumlah = Db.SingleInteger("SELECT COUNT(Tipe) FROM MS_NUP WHERE 1=1"
                    + " AND CONVERT(DATETIME,TglDaftar,112) BETWEEN '" + Cf.Tgl112(Tanggal1) + "' AND '" + Cf.Tgl112(Tanggal2) + "'"
                    + " AND Project = '" + Project + "' AND Tipe = '" + Jenis.Rows[i]["Nama"] + "'"
                    + addSql);

                JenisPro += "<strong>" + Jenis.Rows[i]["JenisProperti"].ToString() + " : " + Cf.Num(Jumlah) + "<strong><br>";
            }
            c.Text = "<br /><br /><br />" + JenisPro;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
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
