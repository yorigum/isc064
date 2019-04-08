using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class LaporanMasterStock : System.Web.UI.Page
    {

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
        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            Rpt.SubJudul(x
                , "Project : " + project.SelectedItem.Text
                );
            Rpt.SubJudul(x
                , "Perusahaan : " + pers.SelectedItem.Text
                );

            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, "", x);
        }

        private void init()
        {
            DataTable rs = Db.Rs("SELECT Nama FROM REF_JENISPROPERTI WHERE Project IN (" + Act.ProjectListSql + ")");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                tipe.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString()));
            }
            Act.ProjectList(project);
        }

        protected void Fill()
        {
            param.Visible = false;
            rpt.Visible = true;

            //project.InnerHtml = Mi.Pt;
            string Tipe = "";
            if (tipe.SelectedIndex > 0)
            {
                Tipe = " AND JenisProperti='" + tipe.SelectedItem + "'";
                //filter.Text += "Tipe Property : " + tipe.SelectedItem + "<br/>";
            }
            else
            {
                //filter.Text += "Tipe Property : SEMUA<br/>";
            }

            string TitipJual = "";
            if (titipjual.SelectedIndex != 0)
            {
                TitipJual = " AND TitipJual=" + titipjual.SelectedValue.ToString();
                //filter.Text += "Status Titip Jual : " + titipjual.SelectedItem + "<br/>";
            }
            else
            {
                //filter.Text += "Status Titip Jual : SEMUA<br/>";
            }

            string Papen = "";
            if (papen.SelectedIndex != 0)
            {
                Papen = " AND PaketInvestasi=" + papen.SelectedValue.ToString();
                //filter.Text += "Status Paket Investasi : " + papen.SelectedItem + "<br/>";
            }
            else
            {
                //filter.Text += "Status Paket Investasi : SEMUA<br/>";
            }

            string Project = " AND Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND Pers = '" + pers.SelectedValue + "'";

            DataTable rs = Db.Rs("SELECT DISTINCT(JenisProperti) as Jenis,Project from MS_UNIT"
                               + " WHERE 1=1"
                               + Tipe
                               + Project
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
                decimal UnitSales = Db.SingleDecimal("SELECT ISNULL(COUNT(NoUnit),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + Project + "");
                tp1 += UnitSales;
                c.Text = UnitSales.ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal LuasNett = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + Project + "");

                tp2 += LuasNett;
                c.Text = Cf.Num(Math.Round(LuasNett, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal LuasSG = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + Project + "");

                tp3 += LuasSG;
                c.Text = Cf.Num(Math.Round(LuasSG, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal NilaiKontrak = Db.SingleDecimal("SELECT ISNULL(SUM(PriceListMin),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + Project + "");
                tp4 += NilaiKontrak;
                c.Text = Cf.Num(NilaiKontrak);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                //Sold
                c = new TableCell();
                decimal UnitSalesNett = Db.SingleDecimal("SELECT ISNULL(COUNT(NoUnit),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'"
                                                + Project
                                                + " AND NoUnit in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                                + TitipJual
                                                + Papen
                                                + Project
                                                + Perusahaan
                                                + " )");
                tp5 += UnitSalesNett;
                c.Text = UnitSalesNett.ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal LuasNettNett = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + Project + " AND NoUnit in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                                + TitipJual
                                                + Papen
                                                + Project
                                                + Perusahaan
                                                + " )");
                tp6 += LuasNettNett;
                c.Text = Cf.Num(Math.Round(LuasNettNett, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal LuasSGNett = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + Project + " AND NoUnit in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                                + TitipJual
                                                + Papen
                                                + Project
                                                + Perusahaan
                                                + " )");
                tp7 += LuasSGNett;
                c.Text = Cf.Num(Math.Round(LuasSGNett, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal NilaiKontrakNett = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiDPP),0) FROM MS_KONTRAK WHERE Status='A'"
                                                + TitipJual
                                                + Papen
                                                + Project
                                                + Perusahaan
                                                + " AND NoUnit IN(SELECT NoUnit FROM MS_UNIT WHERE JenisProperti='" + rs.Rows[i]["Jenis"].ToString() + "')");
                tp8 += NilaiKontrakNett;
                c.Text = Cf.Num(Math.Round(NilaiKontrakNett, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal PricelistMinSold = Db.SingleDecimal("SELECT ISNULL(SUM(PriceListMin),0) FROM MS_UNIT WHERE NoUnit IN (SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'"
                                                + TitipJual
                                                + Papen
                                                + Project
                                                + Perusahaan
                                                + " )"
                                                + " AND JenisProperti= '" + rs.Rows[i]["Jenis"].ToString() + "'");
                decimal Deviasi = NilaiKontrakNett - PricelistMinSold;
                tp14 += Deviasi;
                c.Text = Cf.Num(Math.Round(Deviasi, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                //Available
                c = new TableCell();
                decimal UnitSalesAvl = Db.SingleDecimal("SELECT ISNULL(COUNT(NoUnit),0) FROM MS_UNIT  WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + Project + " AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A' " + Project + Perusahaan + ")");
                tp9 += UnitSalesAvl;
                c.Text = UnitSalesAvl.ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal LuasNettAvl = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + Project + " AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'" + Project + Perusahaan + ")");
                tp10 += LuasNettAvl;
                c.Text = Cf.Num(Math.Round(LuasNettAvl, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal LuasSGAvl = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + Project + " AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'" + Project + Perusahaan + ")");
                tp11 += LuasSGAvl;
                c.Text = Cf.Num(Math.Round(LuasSGAvl, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal NilaiKontrakAvl = Db.SingleDecimal("SELECT ISNULL(SUM(PriceListMin),0) FROM MS_UNIT WHERE JenisProperti = '" + rs.Rows[i]["Jenis"].ToString() + "'" + Project + " AND NoUnit not in(SELECT NoUnit FROM MS_KONTRAK WHERE Status='A'" + Project + Perusahaan + ")");
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
                Header();
                Fill();
            }
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Header();
                Fill();
                Rpt.ToExcel(this, headReport, rpt);
            }
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Master Stock";
            string Link = "";
            DateTime TglGenerate = DateTime.Now;
            string FileName = "";
            string FileType = "application/pdf";
            string UserID = Act.UserID;
            string IP = Act.IP;

            Db.Execute("EXEC spLapPDFDaftar"

                    + " '" + Nama + "'"
                    + ",'" + Link + "'"
                    + ",'" + TglGenerate + "'"
                    + ",'" + IP + "'"
                    + ",'" + UserID + "'"
                    + ",'" + FileName + "'"
                    + ",'" + FileType + "'"
                    + ",'" + null + "'"
                    + ",'" + null + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "MasterStock" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "MasterStock" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter

            string Tipe = tipe.SelectedValue;
            Tipe = Tipe.Replace(" ", "%");

            string Project = "";
            if (project.SelectedIndex == 0)
            {
                Project = Act.ProjectListSql.Replace("'", "");
            }
            else
            {
                Project = project.SelectedValue;
            }

            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFLaporanMasterStock.aspx?id=" + rs.Rows[0]["AttachmentID"]

                + "&tipe=" + Tipe
                + "&titipjual=" + titipjual.SelectedValue
                + "&papen=" + papen.SelectedValue
                + "&pers=" + pers.SelectedValue
                + "&project=" + Project
                + "&userid=" + UserID
                ;

            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 8.5in --page-height 11in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

            //panggil aplikasi untuk mengconvert pdf
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();

            //60000 -> waktu jeda lama convert pdf
            p.WaitForExit(30000);

            string Src = Mi.PathFilePDFReport + nfilename;
            Mi.DownloadPDF(this, Src, (rs.Rows[0]["FileName"]).ToString(), rs.Rows[0]["FileType"].ToString());
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


        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            pers.Items.Clear();
            pers.Items.Add("SEMUA");
            if (project.SelectedValue != "SEMUA")
            {
                string strSql = "SELECT * FROM ISC064_SECURITY..PTSec A "
                + "INNER JOIN ISC064_SECURITY..REF_PERS B ON A.Pers = B.Pers "
                + "INNER JOIN ISC064_SECURITY..REF_PROJECT C ON A.Pers = C.Pers "
                + " WHERE A.UserID='" + Act.UserID + "' AND C.Project ='" + project.SelectedValue + "'  AND A.Granted = 1";

                DataTable rs = Db.Rs(strSql);
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string t = rs.Rows[i]["Nama"].ToString();
                    string v = rs.Rows[i]["Pers"].ToString();
                    pers.Items.Add(new ListItem(t, v));
                }
            }
        }
    }
}
