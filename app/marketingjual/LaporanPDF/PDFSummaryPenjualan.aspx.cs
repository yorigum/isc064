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

namespace ISC064.MARKETINGJUAL.Laporan
{
    /// <summary>
    /// Summary description for SummaryPenjualan.
    /// </summary>
    public partial class SummaryPenjualan : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.RadioButton bfS;
        protected System.Web.UI.WebControls.RadioButton bf1;
        protected System.Web.UI.WebControls.RadioButton bf2;
        protected System.Web.UI.WebControls.CheckBox cbcarabayar;
        protected System.Web.UI.WebControls.Label errcarabayar;
        protected System.Web.UI.WebControls.CheckBoxList cblcarabayar;

        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string Jenis { get { return (Request.QueryString["jenis"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Agent { get { return (Request.QueryString["sales"]); } }

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

            if (StatusA != "")
                Rpt.SubJudul(x, "Status : " + StatusA);
            else if (StatusB != "")
                Rpt.SubJudul(x, "Status : " + StatusB);
            else
                Rpt.SubJudul(x, "Status : " + StatusS);

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            Rpt.SubJudul(x
                , "Tanggal Kontrak" + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            Rpt.SubJudul(x
                , "Jenis : " + Jenis//Rpt.inSql(jenis).Replace("'","")
                );

            Rpt.SubJudul(x
                , "Lokasi : " + Lokasi
                );

            Rpt.SubJudul(x
                , "Sales : " + Agent
                );

            //Rpt.Header(rpt, x);
            string legend = "Status: A = Aktif / B = Batal.< br />"
                       + "Luas dalam meter persegi.Gross adalah harga sebelum diskon.";
            Rpt.HeaderReport(headReport, legend, x);
        }



        private void Fill()
        {
            string Status = "";
            if (StatusA != "") Status = " AND MS_KONTRAK.Status = 'A'";
            if (StatusB != "") Status = " AND MS_KONTRAK.Status = 'B'";

            string tgl = "";
            string order = "";
            tgl = "TglKontrak";
            order = "NoKontrak";


            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string nAgent = "";
            if (Agent != "SEMUA")
            {
                nAgent = " AND MS_KONTRAK.NoAgent = " + Agent;
            }
            else
            {
                if (UserAgent() > 0)
                    nAgent = " AND MS_KONTRAK.NoAgent = " + UserAgent();
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND MS_KONTRAK.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND MS_KONTRAK.Pers = '" + Perusahaan + "'";
            
            //change parameter jenis
            string akt = String.Empty;
            akt = Jenis.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("%", " ");
            akt = akt.Replace(",", "','");
            akt = "'" + akt + "'";

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;
            decimal t6 = 0;
            decimal t7 = 0;
            decimal t8 = 0;
            decimal t9 = 0;
            decimal t10 = 0;
            decimal t11 = 0;
            decimal t12 = 0;
            decimal t13 = 0;
            decimal t14 = 0;

            string strSql = "SELECT *"
                + " FROM MS_KONTRAK"
                + " INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer "
                + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent "
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND Jenis IN (" + akt + ")"
                + nPerusahaan
                + nProject
                + nLokasi
                + Status
                + nAgent
                + " ORDER BY " + order;

            DataTable rs = Db.Rs(strSql);

            TableRow r = new TableRow();
            TableHeaderCell hc;
            //	r.Attributes["ondblclick"] = "popEditKontrak('"+rs.Rows[0]["NoKontrak"]+"')";

            hc = new TableHeaderCell();
            hc.Text = "Kontrak";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "View";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Biaya Admin";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "KPR / Pelunasan";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Total";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Kontrak";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "View";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Biaya Admin";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "KPR / Pelunasan";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Total";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(hc);

            rpt.Rows.Add(r);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";

                c = new TableCell();
                c.Text = Cf.Num(i + 1);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                decimal LuasSG = Db.SingleDecimal("SELECT LuasSG FROM MS_UNIT WHERE NoStock = '" + rs.Rows[i]["NoStock"] + "'");
                c = new TableCell();
                c.Text = Cf.Num(LuasSG);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                decimal NilaiKontrak = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0) FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND TIPE != 'ADM' ");
                c.Text = Cf.Num(NilaiKontrak);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal View = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0) FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NamaTagihan LIKE 'FITTING OUT%' AND TIPE = 'ADM' ");
                c.Text = Cf.Num(View);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal BiayaAdmin = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0) FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NamaTagihan NOT LIKE 'FITTING OUT%' AND TIPE = 'ADM' ");
                c.Text = Cf.Num(BiayaAdmin);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal Total = NilaiKontrak + View + BiayaAdmin;
                c.Text = Cf.Num(Total);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal KontrakBayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM "
                + " MS_PELUNASAN a INNER JOIN MS_TAGIHAN b ON "
                + " a.NoTagihan = b.NoUrut WHERE "
                + " a.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' "
                + " AND b.Tipe != 'ADM' "
                + " AND b.KPR = '0' ");
                c.Text = Cf.Num(KontrakBayar);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal ViewBayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM "
                    + " MS_PELUNASAN a INNER JOIN MS_TAGIHAN b ON "
                    + " a.NoTagihan = b.NoUrut WHERE "
                    + " a.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' "
                    + " AND b.Tipe = 'ADM' "
                    + " AND b.NamaTagihan LIKE 'FITTING OUT%' ");
                c.Text = Cf.Num(ViewBayar);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal AdmBayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM "
                    + " MS_PELUNASAN a INNER JOIN MS_TAGIHAN b ON "
                    + " a.NoTagihan = b.NoUrut WHERE "
                    + " a.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' "
                    + " AND b.Tipe = 'ADM' "
                    + " AND b.NamaTagihan NOT LIKE 'FITTING OUT%' ");
                c.Text = Cf.Num(AdmBayar);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal KPRBayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM "
                    + " MS_PELUNASAN a INNER JOIN MS_TAGIHAN b ON "
                    + " a.NoTagihan = b.NoUrut WHERE "
                    + " a.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' "
                    + " AND b.Tipe != 'ADM' "
                    + " AND b.KPR = '1' ");
                c.Text = Cf.Num(KPRBayar);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal TotalKolomBayar = KontrakBayar + ViewBayar + AdmBayar + KPRBayar;
                c.Text = Cf.Num(TotalKolomBayar);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal NilaiKontrak2 = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM "
                    + " MS_TAGIHAN"
                    + " WHERE "
                    + " NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' "
                    + " AND Tipe != 'ADM' "
                    + " AND KPR = '0' ");
                decimal KontrakOut = NilaiKontrak2 - KontrakBayar;
                c.Text = Cf.Num(KontrakOut);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal ViewOut = View - ViewBayar;
                c.Text = Cf.Num(ViewOut);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal AdmOut = BiayaAdmin - AdmBayar;
                c.Text = Cf.Num(AdmOut);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal TagihanKPR = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM "
                    + " MS_TAGIHAN "
                    + " WHERE "
                    + " NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' "
                    + " AND Tipe != 'ADM' "
                    + " AND KPR = '1' ");
                decimal KPROut = TagihanKPR - KPRBayar;
                c.Text = Cf.Num(KPROut);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal TotalKolomOut = KontrakOut + ViewOut + AdmOut + KPROut;
                c.Text = Cf.Num(TotalKolomOut);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += NilaiKontrak;
                t2 += View;
                t3 += BiayaAdmin;
                t4 += Total;
                t5 += KontrakBayar;
                t6 += ViewBayar;
                t7 += AdmBayar;
                t8 += KPRBayar;
                t9 += TotalKolomBayar;
                t10 += KontrakOut;
                t11 += ViewOut;
                t12 += AdmOut;
                t13 += KPROut;
                t14 += TotalKolomOut;

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, decimal t8, decimal t9, decimal t10, decimal t11, decimal t12, decimal t13, decimal t14)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 6;
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

            c = Rpt.Foot();
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t6);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t7);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t8);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t9);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t10);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t11);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t12);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t13);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t14);
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
