using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class LaporanMasterStockDetil : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }
        protected void SubTotal(string Jenis, string TitipJual, string Papen)
        {

            TableRow r = new TableRow();
            TableCell c;

            rpt.Rows.Add(r);

            c = new TableCell();
            c.Text = "<b>Sub Total : </b>";
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            //Stock

            c = new TableCell();
            decimal UnitSales = Db.SingleDecimal("SELECT ISNULL(COUNT(NoUnit),0) FROM MS_UNIT WHERE JenisProperti = '" + Jenis + "'");
            c.Text = UnitSales.ToString();
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal LuasNett = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE JenisProperti = '" + Jenis + "'");
            c.Text = Cf.Num(Math.Round(LuasNett, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal LuasSG = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE JenisProperti = '" + Jenis + "'");
            c.Text = Cf.Num(Math.Round(LuasSG, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal NilaiKontrak = Db.SingleDecimal("SELECT ISNULL(SUM(PriceListMin),0) FROM MS_UNIT WHERE JenisProperti = '" + Jenis + "'");
            c.Text = Cf.Num(NilaiKontrak);
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            //Sold
            c = new TableCell();
            decimal UnitSalesNett = Db.SingleDecimal("SELECT ISNULL(COUNT(NoUnit),0) FROM MS_UNIT WHERE JenisProperti = '" + Jenis + "'"
                                            + " AND NoUnit in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                            + TitipJual
                                            + Papen
                                            + " )");
            c.Text = UnitSalesNett.ToString();
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal LuasNettNett = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE JenisProperti = '" + Jenis + "' AND NoUnit in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                            + TitipJual
                                            + Papen
                                            + " )");
            c.Text = Cf.Num(Math.Round(LuasNettNett, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal LuasSGNett = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE JenisProperti = '" + Jenis + "' AND NoUnit in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                            + TitipJual
                                            + Papen
                                            + " )");
            c.Text = Cf.Num(Math.Round(LuasSGNett, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal NilaiKontrakNett = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiDPP),0) FROM MS_KONTRAK WHERE Status='A'"
                                            + TitipJual
                                            + Papen
                                            + " AND NoUnit IN(SELECT NoUnit FROM MS_UNIT WHERE JenisProperti='" + Jenis + "')");
            c.Text = Cf.Num(Math.Round(NilaiKontrakNett, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal PricelistMinSold = Db.SingleDecimal("SELECT ISNULL(SUM(PriceListMin),0) FROM MS_UNIT WHERE NoUnit IN (SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                            + TitipJual
                                            + Papen
                                            + " )"
                                            + " AND JenisProperti= '" + Jenis + "'");
            decimal Deviasi = NilaiKontrakNett - PricelistMinSold;
            c.Text = Cf.Num(Math.Round(Deviasi, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            //Available
            c = new TableCell();
            decimal UnitSalesAvl = Db.SingleDecimal("SELECT ISNULL(COUNT(NoUnit),0) FROM MS_UNIT  WHERE JenisProperti = '" + Jenis + "' AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A')");
            c.Text = UnitSalesAvl.ToString();
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal LuasNettAvl = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE JenisProperti = '" + Jenis + "' AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A')");
            c.Text = Cf.Num(Math.Round(LuasNettAvl, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal LuasSGAvl = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE JenisProperti = '" + Jenis + "' AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A')");
            c.Text = Cf.Num(Math.Round(LuasSGAvl, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal NilaiKontrakAvl = Db.SingleDecimal("SELECT ISNULL(SUM(PriceListMin),0) FROM MS_UNIT WHERE JenisProperti = '" + Jenis + "' AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A')");
            c.Text = Cf.Num(Math.Round(NilaiKontrakAvl, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

        }
        protected void GrandTotal(string TitipJual, string Papen)
        {

            TableRow r = new TableRow();
            TableCell c;

            rpt.Rows.Add(r);

            c = new TableCell();
            c.Text = "<b>Grand Total : </b>";
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            //Stock

            c = new TableCell();
            decimal UnitSales = Db.SingleDecimal("SELECT ISNULL(COUNT(NoUnit),0) FROM MS_UNIT");
            c.Text = UnitSales.ToString();
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal LuasNett = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT ");
            c.Text = Cf.Num(Math.Round(LuasNett, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal LuasSG = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT ");
            c.Text = Cf.Num(Math.Round(LuasSG, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal NilaiKontrak = Db.SingleDecimal("SELECT ISNULL(SUM(PriceListMin),0) FROM MS_UNIT ");
            c.Text = Cf.Num(NilaiKontrak);
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            //Sold
            c = new TableCell();
            decimal UnitSalesNett = Db.SingleDecimal("SELECT ISNULL(COUNT(NoUnit),0) FROM MS_UNIT WHERE "
                                            + " NoUnit in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                            + TitipJual
                                            + Papen
                                            + " )");
            c.Text = UnitSalesNett.ToString();
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal LuasNettNett = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE NoUnit in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                            + TitipJual
                                            + Papen
                                            + " )");
            c.Text = Cf.Num(Math.Round(LuasNettNett, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal LuasSGNett = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE NoUnit in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                            + TitipJual
                                            + Papen
                                            + " )");
            c.Text = Cf.Num(Math.Round(LuasSGNett, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal NilaiKontrakNett = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiDPP),0) FROM MS_KONTRAK WHERE Status='A'"
                                            + TitipJual
                                            + Papen
                                            );
            c.Text = Cf.Num(Math.Round(NilaiKontrakNett, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal PricelistMinSold = Db.SingleDecimal("SELECT ISNULL(SUM(PriceListMin),0) FROM MS_UNIT WHERE NoUnit IN (SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                            + TitipJual
                                            + Papen
                                            + " )"
                                            );
            decimal Deviasi = NilaiKontrakNett - PricelistMinSold;
            c.Text = Cf.Num(Math.Round(Deviasi, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            //Available
            c = new TableCell();
            decimal UnitSalesAvl = Db.SingleDecimal("SELECT ISNULL(COUNT(NoUnit),0) FROM MS_UNIT  WHERE NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A')");
            c.Text = UnitSalesAvl.ToString();
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal LuasNettAvl = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A')");
            c.Text = Cf.Num(Math.Round(LuasNettAvl, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal LuasSGAvl = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A')");
            c.Text = Cf.Num(Math.Round(LuasSGAvl, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

            c = new TableCell();
            decimal NilaiKontrakAvl = Db.SingleDecimal("SELECT ISNULL(SUM(PriceListMin),0) FROM MS_UNIT WHERE NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A')");
            c.Text = Cf.Num(Math.Round(NilaiKontrakAvl, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.BorderColor = Color.Black;
            c.BorderWidth = 1;
            r.Cells.Add(c);

        }
        protected void FillStock()
        {
            param.Visible = false;
            rpt.Visible = true;


            string Tipe = "";
            if (tipe.SelectedIndex > 0)
            {
                Tipe = " AND JenisProperti='" + tipe.SelectedItem + "'";
                filter.Text += "Tipe Property : " + tipe.SelectedItem + "<br/>";
            }
            else
            {
                filter.Text += "Tipe Property : SEMUA<br/>";
            }

            string TitipJual = "";
            if (titipjual.SelectedIndex != 0)
            {
                TitipJual = " AND TitipJual=" + titipjual.SelectedValue.ToString();
                filter.Text += "Status Titip Jual : " + titipjual.SelectedItem + "<br/>";
            }
            else
            {
                filter.Text += "Status Titip Jual : SEMUA<br/>";
            }

            string Papen = "";
            if (papen.SelectedIndex != 0)
            {
                Papen = " AND PaketInvestasi=" + papen.SelectedValue.ToString();
                filter.Text += "Status Paket Investasi : " + papen.SelectedItem + "<br/>";
            }
            else
            {
                filter.Text += "Status Paket Investasi : SEMUA<br/>";
            }
            DataTable rsa = Db.Rs("SELECT DISTINCT(JenisProperti) as Jenis from MS_UNIT"
                               + " WHERE 1=1"
                               + Tipe
                                );
            decimal Jumlah = 0; 
            for (int a = 0; a < rsa.Rows.Count; a++)
            {
                DataTable rs = Db.Rs("SELECT * FROM MS_UNIT WHERE JenisProperti = '" + rsa.Rows[a]["Jenis"].ToString() + "'");

                DataTable sold = Db.Rs("SELECT * FROM MS_KONTRAK a INNER JOIN MS_UNIT b ON a.NoUnit=b.NoUnit "
                                        + " WHERE a.Status='A'"
                                        + TitipJual
                                        + Papen
                                        + " AND b.JenisProperti='" + rsa.Rows[a]["Jenis"].ToString() + "'");

                DataTable ava = Db.Rs("SELECT * FROM MS_UNIT  WHERE JenisProperti = '" + rsa.Rows[a]["Jenis"].ToString() + "' AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A')");

                Jumlah += rs.Rows.Count;
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    TableRow r = new TableRow();
                    TableCell c;

                    rpt.Rows.Add(r);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["JenisProperti"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoUnit"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["LuasNett"].ToString());
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["LuasSG"].ToString());
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["PriceListMin"].ToString());
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);


                    c = new TableCell();
                    if (i < sold.Rows.Count)
                    {
                        c.Text = sold.Rows[i]["NoUnit"].ToString();
                    }
                    else
                    {
                        c.Text = "";
                    }
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                    c = new TableCell();
                    if (i < sold.Rows.Count)
                    {
                        c.Text = Cf.Num(sold.Rows[i]["LuasNett"].ToString());
                    }
                    else
                    {
                        c.Text = "";
                    }
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                    c = new TableCell();
                    if (i < sold.Rows.Count)
                    {
                        c.Text = Cf.Num(sold.Rows[i]["LuasSG"].ToString());
                    }
                    else
                    {
                        c.Text = "";
                    }
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                    c = new TableCell();
                    if (i < sold.Rows.Count)
                    {
                        c.Text = Cf.Num(sold.Rows[i]["NilaiDPP"].ToString());
                    }
                    else
                    {
                        c.Text = "";
                    }
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                    c = new TableCell();
                    if (i < sold.Rows.Count)
                    {
                        c.Text = Cf.Num(Convert.ToDecimal(sold.Rows[i]["NilaiDPP"]) - Convert.ToDecimal(sold.Rows[i]["PriceListMin"]));
                    }
                    else
                    {
                        c.Text = "";
                    }
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                    c = new TableCell();
                    if (i < ava.Rows.Count)
                    {
                        c.Text = ava.Rows[i]["NoUnit"].ToString();
                    }
                    else
                    {
                        c.Text = "";
                    }
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                    c = new TableCell();
                    if (i < ava.Rows.Count)
                    {
                        c.Text = Cf.Num(ava.Rows[i]["LuasNett"].ToString());
                    }
                    else
                    {
                        c.Text = "";
                    }
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                    c = new TableCell();
                    if (i < ava.Rows.Count)
                    {
                        c.Text = Cf.Num(ava.Rows[i]["LuasSG"].ToString());
                    }
                    else
                    {
                        c.Text = "";
                    }
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                    c = new TableCell();
                    if (i < ava.Rows.Count)
                    {
                        c.Text = Cf.Num(ava.Rows[i]["PriceListMin"].ToString());
                    }
                    else
                    {
                        c.Text = "";
                    }
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    c.BorderColor = Color.Black;
                    c.BorderWidth = 1;
                    r.Cells.Add(c);

                }
                SubTotal(rsa.Rows[a]["Jenis"].ToString(),TitipJual, Papen);
            }
            if (Jumlah > 0 && Tipe == "")
            {
                GrandTotal(TitipJual, Papen);
            }
        }
        private bool valid()
        {
            string s = "";
            bool x = true;

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
            {
                FillStock();
            }
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                FillStock();
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
