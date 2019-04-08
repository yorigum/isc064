using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP.Laporan
{
    public partial class LaporanKasirNUP : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.CheckBoxList tipe;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                init();
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }

        private void init()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);

            DataTable rs;

            rs = Db.Rs("SELECT DISTINCT UserID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS ORDER BY UserID");
            for (int i = 0; i < rs.Rows.Count; i++)
                kasir.Items.Add(new ListItem(
                    rs.Rows[i][0].ToString()));

            kasir.SelectedIndex = 0;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(dari))
            {
                x = false;
                if (s == "") s = dari.ID;
                daric.Text = "Tanggal";
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                x = false;
                if (s == "") s = sampai.ID;
                sampaic.Text = "Tanggal";
            }
            else
                sampaic.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        protected void scr_Click(object sender, System.EventArgs e)
        {
            if (valid())
                Report();
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
                Rpt.ToExcel(this, rpt);
            }
        }

        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            Report2();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            string tgl = "";
            if (tglinput.Checked) tgl = tglinput.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(x
                , "Kasir : " + kasir.SelectedItem.Text
                );

            Rpt.Header(rpt, x);
        }
        private void Report2()
        {
            param.Visible = false;
            rpt.Visible = true;

            string tgl = "";
            if (tglinput.Checked) tgl = tglinput.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            lblHeader.Text = Mi.Pt
                + "<br />"
                + "Laporan Kasir NUP"
                + "<br />"
                + tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                ;

            System.Text.StringBuilder x = new System.Text.StringBuilder();
            x.Append("<br /><span style='font-weight: normal;'>Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
                + ", " + Cf.Date(DateTime.Now)
                + " dari workstation : " + Act.IP
                + " dan username : " + Act.UserID
                + "</span>"
                );

            lblSubHeader.Text = x.ToString();
            Fill();
        }
        private void Fill()
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            string UserID = "";
            if (kasir.SelectedIndex != 0)
                UserID = " AND UserID = '" + kasir.SelectedValue + "'";

            string tgl = "";
            if (tglinput.Checked) tgl = "TglInput";

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = MS_TTS.Ref) = " + UserAgent();

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
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND NoNUP != ''"
                + UserID
                + agent
                + " ORDER BY NoTTS";

            DataTable rs = Db.Rs(strSql);

            DataTable rsGiro = Db.Rs(
                "SELECT DISTINCT NoBG"
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND NoNUP != ''"
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
