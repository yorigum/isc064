using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR.Laporan
{
    public partial class LaporanKasir : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.CheckBoxList tipe;
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string TanggalInput { get { return (Request.QueryString["tglinput"]); } }
        private string Kasir { get { return (Request.QueryString["kasir"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");

            return a;
        }


        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            Header();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            string tgl = "";
            if (TanggalInput != "") tgl = TanggalInput;

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            Rpt.SubJudul(x
                , tgl + "Tanggal Input : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            Rpt.SubJudul(x
                , "Kasir : " + Kasir
                );
            Rpt.SubJudul(x
                , "Project : " + Project
                );
            string pers = (Perusahaan == "SEMUA") ? "SEMUA" : Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Perusahaan + "'");
            Rpt.SubJudul(x
                , "Perusahaan : " + pers
                );

            string legend = "Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer Bank / BG = Cek Giro / UJ = Uang Jaminan / DN = Diskon.";

            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            //string UserID = " AND UserID = '" + Act.UserID + "'";

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            string UserID = "";
            if (Kasir != "SEMUA")
                UserID = " AND UserID = '" + Kasir + "'";

            string tgl = "";
            if (TanggalInput != "") tgl = "TglBKM";

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = MS_TTS.Ref) = " + UserAgent();

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND b.Project IN ('" + Project.Replace(",", "','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND b.Pers = '" + Perusahaan + "'";

            string strSql = "SELECT * "
                + " FROM MS_TTS a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + nProject
                + nPerusahaan
                + UserID
                + agent
                + " ORDER BY NoTTS";

            DataTable rs = Db.Rs(strSql);

            DataTable rsGiro = Db.Rs(
                "SELECT DISTINCT NoBG"
                + " FROM MS_TTS a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + nProject
                + nPerusahaan
                + UserID
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
                c.Text = rs.Rows[i]["NoTTS2"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglTTS"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                //c.Text = rs.Rows[i]["ManualTTS"].ToString();
                c.Text = rs.Rows[i]["NoBKM2"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglBKM"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);


                c = new TableCell();
                c.Text = rs.Rows[i]["Ref"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Unit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Customer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["CaraBayar"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Ket"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoBG"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglBG"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Acc"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string Bank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE ACC = '" + rs.Rows[i]["Acc"] + "' ");
                c = new TableCell();
                c.Text = Bank;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Total"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LebihBayar"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Total2"]);
                c.HorizontalAlign = HorizontalAlign.Right;
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
                    SubTotal("GRAND TOTAL", t1, t2, t3);
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
            c.ColumnSpan = 16;
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
            c.ColumnSpan = 16;
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
            c.ColumnSpan = 14;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Kasir:<br /><br /><br /><br /><br />(" + Act.UserID + ")";
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 13;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t3);
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
