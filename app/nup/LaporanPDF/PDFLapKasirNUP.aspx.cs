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
    public partial class LapKasirNUP : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Kasir { get { return (Request.QueryString["kasir"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string TanggalDari { get { return Cf.Day(Db.SingleTime("SELECT FilterDari FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'")); } }
        private string TanggalSampai { get { return Cf.Day(Db.SingleTime("SELECT FilterSampai FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'")); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        protected void Report()
        {
            lblHeader.Text = Mi.Pt
                    + "<br />"
                    + "Laporan Kasir NUP"
                    ;

            System.Text.StringBuilder x = new System.Text.StringBuilder();
            DateTime Tanggal1 = Db.SingleTime("SELECT FilterDari FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'");
            DateTime Tanggal2 = Db.SingleTime("SELECT FilterSampai FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'");

            x.Append("<br />Untuk tanggal : " + Cf.Day(Tanggal1) + " s/d " + Cf.Day(Tanggal2));
            x.Append("<br />Untuk Project : " + Cf.Str(Project));
            x.Append("<br />Untuk Kasir : " + Cf.Str(Kasir));

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
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");

            return a;
        }

        private void Fill()
        {
            DateTime Tanggal1 = Db.SingleTime("SELECT FilterDari FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'");
            DateTime Tanggal2 = Db.SingleTime("SELECT FilterSampai FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'");

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            string addSql = "";

            if (Kasir != "SEMUA")
                addSql += " AND UserID = '" + Kasir + "'";

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = MS_TTS.Ref) = " + UserAgent();

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND Project IN ('" + Project.Replace(",", "','") + "')";

            string Jenis = " AND(SELECT COUNT(Project) FROM MS_NUP WHERE NoNUP = " + Mi.DbPrefix + "FINANCEAR..MS_TTS.NoNUP " + nProject + ") > 0";

            string strSql = "SELECT * "
                + ",CASE CaraBayar"
                + "		WHEN 'TN' THEN 'TUNAI'"
                + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                + "		WHEN 'BG' THEN 'CEK GIRO'"
                + "		WHEN 'UJ' THEN 'UANG JAMINAN'"
                + "		WHEN 'DN' THEN 'DISKON'"
                + " END AS CaraBayar2"
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,TglInput,112) >= '" + Cf.Tgl112(Tanggal1) + "'"
                + " AND CONVERT(varchar,TglInput,112) <= '" + Cf.Tgl112(Tanggal2) + "'"
                + " AND NoNUP != ''"
                + Jenis
                + addSql
                + agent
                + " ORDER BY NoTTS";

            DataTable rs = Db.Rs(strSql);
            DataTable rsGiro = Db.Rs(
                "SELECT DISTINCT NoBG"
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,TglInput,112) >= '" + Cf.Tgl112(Tanggal1) + "'"
                + " AND CONVERT(varchar,TglInput,112) <= '" + Cf.Tgl112(Tanggal2) + "'"
                + " AND NoNUP != ''"
                + addSql
                + agent
                + " AND NoBG != ''"
                );
            int LembarGiro = rsGiro.Rows.Count;

            decimal TN = 0, KD = 0, KK = 0, TR = 0, BG = 0, UJ = 0, DN = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditTTS('" + rs.Rows[i]["NoTTS"] + "')";

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglTTS"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoNUP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                //c.Text = rs.Rows[i]["ManualTTS"].ToString();
                c.Text = rs.Rows[i]["NoTTS"].ToString().PadLeft(7, '0');
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                //Unit Customer
                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["Unit"].ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Customer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Total"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["CaraBayar2"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                string CaraBayarr = rs.Rows[i]["CaraBayar"].ToString();
                if (CaraBayarr == "TN")
                {
                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["Total"]);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = "&nbsp;";
                    c.ColumnSpan = 3;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);
                }
                else if (CaraBayarr == "KD")
                {
                    c = new TableCell();
                    c.Text = "&nbsp;";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["Total"]);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = "&nbsp;";
                    c.ColumnSpan = 2;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);
                }
                else if (CaraBayarr == "TR")
                {
                    c = new TableCell();
                    c.Text = "&nbsp;";
                    c.ColumnSpan = 2;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["Total"]);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = "&nbsp;";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);
                }
                else if (CaraBayarr == "KK")
                {
                    c = new TableCell();
                    c.Text = "&nbsp;";
                    c.ColumnSpan = 3;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["Total"]);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);
                }

                string Bank = Db.SingleString("SELECT Bank FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE ACC = '" + rs.Rows[i]["Acc"] + "' ");
                c = new TableCell();
                c.Text = Bank;
                c.Width = 1000;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + rs.Rows[i]["UserID"] + "'");
                c.Width = 1000;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Catatan"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 = t1 + (decimal)rs.Rows[i]["Total"];
                t2 = t2 + (decimal)rs.Rows[i]["LebihBayar"];
                t3 = t3 + (decimal)rs.Rows[i]["Total2"];

                if (rs.Rows[i]["CaraBayar"].ToString() == "TN")
                    TN += Convert.ToDecimal(rs.Rows[i]["Total"]);
                else if (rs.Rows[i]["CaraBayar"].ToString() == "KD")
                    KD += Convert.ToDecimal(rs.Rows[i]["Total"]);
                else if (rs.Rows[i]["CaraBayar"].ToString() == "KK")
                    KK += Convert.ToDecimal(rs.Rows[i]["Total"]);
                else if (rs.Rows[i]["CaraBayar"].ToString() == "TR")
                    TR += Convert.ToDecimal(rs.Rows[i]["Total"]);
                else if (rs.Rows[i]["CaraBayar"].ToString() == "BG")
                    BG += Convert.ToDecimal(rs.Rows[i]["Total"]);
                else if (rs.Rows[i]["CaraBayar"].ToString() == "UJ")
                    UJ += Convert.ToDecimal(rs.Rows[i]["Total"]);
                else if (rs.Rows[i]["CaraBayar"].ToString() == "DN")
                    DN += Convert.ToDecimal(rs.Rows[i]["Total"]);

                if (i == rs.Rows.Count - 1)
                {
                    SubTotal("TOTAL", t1, t2, t3, TN, KD, KK, TR, BG, UJ, DN);
                    Giro(LembarGiro);
                    Detail(TN, KD, KK, TR, BG, UJ, DN);
                }
            }
        }

        private void Giro(int LembarGiro)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.ColumnSpan = 15;
            c.Text = "<strong>Lembar Giro: </strong>" + LembarGiro.ToString();
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void Detail(decimal TN, decimal KD, decimal KK, decimal TR, decimal BG, decimal UJ, decimal DN)
        {
            TableRow r;
            TableCell c;

            r = new TableRow();

            c = new TableCell();
            c.ColumnSpan = 15;
            c.Text = "<strong>Jumlah Tunai (TN): </strong>" + Cf.Num(TN)
                + "<br />"
                + "<strong>Jumlah Kartu Debit (KD): </strong>" + Cf.Num(KD)
                + "<br />"
                + "<strong>Jumlah Kartu Kredit (KK): </strong>" + Cf.Num(KK)
                + "<br />"
                + "<strong>Jumlah Transfer Bank (TR): </strong>" + Cf.Num(TR)
                + "<br />"
                + "<strong>Jumlah Cek Giro (BG): </strong>" + Cf.Num(BG)
                + "<br />"
                + "<strong>Jumlah Uang Jaminan (UJ): </strong>" + Cf.Num(UJ)
                + "<br />"
                + "<strong>Jumlah Diskon (DN): </strong>" + Cf.Num(DN)
                ;
            r.Cells.Add(c);

            rpt.Rows.Add(r);

            //Ttd
            r = new TableRow();

            c = new TableCell();
            c.ColumnSpan = 9;
            r.Cells.Add(c);

            string Nama = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..Username WHERE UserID='" + Act.UserID + "'");
            c = new TableCell();
            c.Text = "Dibuat <br />Oleh:<br /><br /><br /><br /><br />(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)";

            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Diperiksa <br />Oleh:<br /><br /><br /><br /><br />(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)";

            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Disetujui <br />Oleh:<br /><br /><br /><br /><br />(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)";

            c = new TableCell();
            c.Text = "";
            c.ColumnSpan = 4;

            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3, decimal TN, decimal KD, decimal KK, decimal TR, decimal BG, decimal UJ, decimal DN)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 8;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TN);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(KD);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TR);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(KK);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.ColumnSpan = 4;
            c.HorizontalAlign = HorizontalAlign.Right;
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
