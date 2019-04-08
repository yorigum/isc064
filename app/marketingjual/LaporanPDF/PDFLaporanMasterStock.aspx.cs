using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class LaporanMasterStock : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Tipe { get { return (Request.QueryString["tipe"]); } }
        private string TitipJual { get { return (Request.QueryString["titipjual"]); } }
        private string Papen { get { return (Request.QueryString["papen"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
         Header();
         Fill();
        }
        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);
            Rpt.SubJudul(x
                , "Project : " + Project
                );
            Rpt.SubJudul(x
                , "Perusahaan : " + Perusahaan
                );

            Rpt.HeaderReport(headReport, "", x);
        }
        protected void Fill()
        {
            param.Visible = false;
            rpt.Visible = true;

            project.InnerHtml = Mi.Pt;
            string nTipe = "";
            if (Tipe !="SEMUA")
            {
                nTipe = " AND JenisProperti='" + Tipe.Replace("%"," ") + "'";
                filter.Text += "Tipe Property : " + Tipe.Replace("%", " ") + "<br/>";
            }
            else
            {
                filter.Text += "Tipe Property : SEMUA<br/>";
            }

            string nTitipJual = "";
            if (TitipJual != "SEMUA")
            {
                nTitipJual = " AND TitipJual=" + TitipJual;
                filter.Text += "Status Titip Jual : " + TitipJual + "<br/>";
            }
            else
            {
                filter.Text += "Status Titip Jual : SEMUA<br/>";
            }

            string nPapen = "";
            if (Papen != "SEMUA")
            {
                nPapen = " AND PaketInvestasi=" + Papen;
                filter.Text += "Status Paket Investasi : " + Papen + "<br/>";
            }
            else
            {
                filter.Text += "Status Paket Investasi : SEMUA<br/>";
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND Pers = '" + Perusahaan + "'";

            //Response.Write(nTitipJual);
            //Response.Write(nTipe);

            DataTable rs = Db.Rs("SELECT DISTINCT(JenisProperti) as Jenis,Project from MS_UNIT"
                               + " WHERE 1=1"
                               + nProject
                               + nTipe
                                );

            decimal tp1 = 0;
            decimal tp2 = 0;
            decimal tp3 = 0;
            decimal tp4 = 0;
            decimal tp5 = 0;
            decimal tp6 = 0;
            decimal tp7 = 0;
            decimal tp8 = 0;
            decimal tp9 = 0;
            decimal tp10 = 0;
            decimal tp11 = 0;
            decimal tp12 = 0;
            decimal tp13 = 0;
            decimal tp14 = 0;


            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TableRow r = new TableRow();
                TableCell c;

                rpt.Rows.Add(r);

                c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Stock

                c = new TableCell();
                decimal UnitSales = Db.SingleDecimal("SELECT ISNULL(COUNT(NoUnit),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + nProject + "");
                tp1 += UnitSales;
                c.Text = UnitSales.ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal LuasNett = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + nProject + "");

                tp2 += LuasNett;
                c.Text = Cf.Num(Math.Round(LuasNett, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal LuasSG = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + nProject + "");

                tp3 += LuasSG;
                c.Text = Cf.Num(Math.Round(LuasSG, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal NilaiKontrak = Db.SingleDecimal("SELECT ISNULL(SUM(PriceListMin),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + nProject + "");
                tp4 += NilaiKontrak;
                c.Text = Cf.Num(NilaiKontrak);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                //Sold
                c = new TableCell();
                decimal UnitSalesNett = Db.SingleDecimal("SELECT ISNULL(COUNT(NoUnit),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'"
                                                + nProject
                                                + " AND NoUnit in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                                + nTitipJual
                                                + nPapen
                                                + nProject
                                                + nPerusahaan
                                                + " )");
                tp5 += UnitSalesNett;
                c.Text = UnitSalesNett.ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal LuasNettNett = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + nProject + " AND NoUnit in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                                + nTitipJual
                                                + nPapen
                                                + nProject
                                                + nPerusahaan
                                                + " )");
                tp6 += LuasNettNett;
                c.Text = Cf.Num(Math.Round(LuasNettNett, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal LuasSGNett = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + nProject + " AND NoUnit in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                                + nTitipJual
                                                + nPapen
                                                + nProject
                                                + nPerusahaan
                                                + " )");
                tp7 += LuasSGNett;
                c.Text = Cf.Num(Math.Round(LuasSGNett, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal NilaiKontrakNett = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiDPP),0) FROM MS_KONTRAK WHERE Status='A'"
                                                + nTitipJual
                                                + nPapen
                                                + nProject
                                                + nPerusahaan
                                                + " AND NoUnit IN(SELECT NoUnit FROM MS_UNIT WHERE JenisProperti='" + rs.Rows[i]["Jenis"].ToString() + "')");
                tp8 += NilaiKontrakNett;
                c.Text = Cf.Num(Math.Round(NilaiKontrakNett, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal PricelistMinSold = Db.SingleDecimal("SELECT ISNULL(SUM(PriceListMin),0) FROM MS_UNIT WHERE NoUnit IN (SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                                + nTitipJual
                                                + nPapen
                                                + nProject
                                                + nPerusahaan
                                                + " )"
                                                + " AND JenisProperti= '" + rs.Rows[i]["Jenis"].ToString() + "'");
                decimal Deviasi = NilaiKontrakNett - PricelistMinSold;
                tp14 += Deviasi;
                c.Text = Cf.Num(Math.Round(Deviasi, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                //Available
                c = new TableCell();
                decimal UnitSalesAvl = Db.SingleDecimal("SELECT ISNULL(COUNT(NoUnit),0) FROM MS_UNIT  WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + nProject + " AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A' " + nProject + nPerusahaan + ")");
                tp9 += UnitSalesAvl;
                c.Text = UnitSalesAvl.ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal LuasNettAvl = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + nProject + " AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'" + nProject + nPerusahaan + ")");
                tp10 += LuasNettAvl;
                c.Text = Cf.Num(Math.Round(LuasNettAvl, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal LuasSGAvl = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + nProject + " AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'" + nProject + nPerusahaan + ")");
                tp11 += LuasSGAvl;
                c.Text = Cf.Num(Math.Round(LuasSGAvl, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal NilaiKontrakAvl = Db.SingleDecimal("SELECT ISNULL(SUM(PriceListMin),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + nProject + " AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'" + nProject + nPerusahaan + ")");
                tp12 += NilaiKontrakAvl;
                c.Text = Cf.Num(Math.Round(NilaiKontrakAvl, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);
            }

            Total(tp1, tp2, tp3, tp4, tp5, tp6, tp7, tp8, tp9, tp10, tp11, tp12, tp13, tp14, "Grand Total :");
        }
        protected void Total(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, decimal t8, decimal t9, decimal t10, decimal t11, decimal t12, decimal t13, decimal t14, string y)
        {
            TableRow r = new TableRow();
            TableCell c;


            c = new TableCell();
            c.Text = "<b>" + y + "</b>";
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = t1.ToString();
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t2, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t3, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t4, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = t5.ToString();
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t6, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t7, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t8, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t14, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = t9.ToString();
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t10, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t11, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);
            rpt.Rows.Add(r);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t12, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);
        }
        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }
        protected void scr_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Fill();
            }
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Fill();
                Rpt.ToExcel(this, rpt);
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
