using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
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

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
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
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());

            DataTable rs;
            string Project = project.SelectedIndex == 0 ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            rs = Db.Rs("SELECT * FROM REF_JENIS WHERE " + Project + " ORDER BY SN");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Jenis"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                jenis.Items.Add(new ListItem(t, v));
                jenis.Items[i].Selected = true;
            }

            rs = Db.Rs("SELECT * FROM REF_LOKASI WHERE " + Project + " ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND NoAgent = " + UserAgent();

            rs = Db.Rs("SELECT Nama, NoAgent FROM MS_AGENT WHERE Status = 'A'  AND " + Project + aa);
            for (int i = 0; i < rs.Rows.Count; i++)
                agent.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString(), rs.Rows[i]["NoAgent"].ToString()));

            //project.Items.Clear();
            //project.Items.Add("SEMUA");
            //Act.ProjectList(project);
            jenis.SelectedIndex = 0;
            lokasi.SelectedIndex = 0;
            agent.SelectedIndex = 0;
        }

        protected void scr_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
            }
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
                Rpt.ToExcel(this, headReport, rpt);
            }
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Rekapitulasi Kontrak dan Pembayaran";
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
                    + ",'" + Cf.Date(dari.Text) + "'"
                    + ",'" + Cf.Date(sampai.Text) + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "RekapitulasiKontrakPembayaran" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "RekapitulasiKontrakPembayaran" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
            string Lokasi = lokasi.SelectedValue.Replace(" ", "%");
            string Agent = agent.SelectedValue;
            //declare parameter
            string Sales = agent.SelectedValue;
            string nStatusS = "";
            string nStatusA = "";
            string nStatusB = "";
            if (statusS.Checked == true)
                nStatusS = statusS.Text;
            else
                nStatusS = "";
            if (statusA.Checked == true)
                nStatusA = statusA.Text;
            else
                nStatusA = "";
            if (statusB.Checked == true)
                nStatusB = statusB.Text;
            else
                nStatusB = "";

            string nm = string.Empty;
            string nm2 = string.Empty;
            try
            {
                foreach (ListItem item in jenis.Items)
                {
                    if (item.Selected == true)
                    {
                        nm += item.Value.Replace(" ", "%") + "-";
                    }
                }
            }
            catch (Exception)
            {
            }

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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFSummaryPenjualan.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&status_s=" + nStatusS
                + "&status_b=" + nStatusB
                + "&status_a=" + nStatusA
                + "&sales=" + Sales
                + "&jenis=" + nm
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
                + "&lokasi=" + Lokasi
                + "&userid=" + UserID

                ;
            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 8.5in --page-height 11.5in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0.25cm " + link + " " + save;

            //panggil aplikasi untuk mengconvert pdf
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();

            //60000 -> waktu jeda lama convert pdf
            p.WaitForExit(30000);

            string Src = Mi.PathFilePDFReport + nfilename;
            Mi.DownloadPDF(this, Src, (rs.Rows[0]["FileName"]).ToString(), rs.Rows[0]["FileType"].ToString());
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

            if (statusA.Checked)
                Rpt.SubJudul(x, "Status : " + statusA.Text);
            else if (statusB.Checked)
                Rpt.SubJudul(x, "Status : " + statusB.Text);
            else
                Rpt.SubJudul(x, "Status : " + statusS.Text);

            string tgl = "";
            if (tglkontrak.Checked) tgl = tglkontrak.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(x
                , "Jenis : " + Rpt.inSql(jenis).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "Lokasi : " + lokasi.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Sales : " + agent.SelectedItem.Text
                );

            //Rpt.Header(rpt, x);
            string legend = "Status: A = Aktif / B = Batal.< br />"
                       + "Luas dalam meter persegi.Gross adalah harga sebelum diskon.";
            Rpt.HeaderReport(headReport, legend, x);
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

            if (!Cf.isPilih(jenis))
            {
                x = false;
                jenisc.Text = " Pilih Minimum Satu";
            }
            else
                jenisc.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        private void Fill()
        {
            string Status = "";
            if (statusA.Checked) Status = " AND MS_KONTRAK.Status = 'A'";
            if (statusB.Checked) Status = " AND MS_KONTRAK.Status = 'B'";

            string tgl = "";
            string order = "";
            if (tglkontrak.Checked)
            {
                tgl = "TglKontrak";
                order = "NoKontrak";
            }

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string Agent = "";
            if (agent.SelectedIndex != 0)
            {
                Agent = " AND MS_KONTRAK.NoAgent = " + agent.SelectedValue;
            }
            else
            {
                if (UserAgent() > 0)
                    Agent = " AND MS_KONTRAK.NoAgent = " + UserAgent();
            }

            string Project = " AND MS_KONTRAK.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND MS_KONTRAK.Pers = '" + pers.SelectedValue + "'";

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
                + " AND Jenis IN (" + Rpt.inSql(jenis) + ")"
                + Perusahaan
                + Project
                + Lokasi
                + Status
                + Agent
                + " ORDER BY " + order;

            DataTable rs = Db.Rs(strSql);

            TableRow r = new TableRow();
            TableHeaderCell hc;
            //	r.Attributes["ondblclick"] = "popEditKontrak('"+rs.Rows[0]["NoKontrak"]+"')";

            hc = new TableHeaderCell();
            hc.Text = "Kontrak";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "View";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Biaya Admin";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "KPR / Pelunasan";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Total";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Kontrak";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "View";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Biaya Admin";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "KPA / Pelunasan";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Total";
            hc.HorizontalAlign = HorizontalAlign.Right;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
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

        protected void jenisCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < jenis.Items.Count; i++)
            {
                jenis.Items[i].Selected = jenisCheck.Checked;
            }

            Js.Focus(this, jenisCheck);
            jenisc.Text = "";
        }

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
            jenis.Items.Clear();
            lokasi.Items.Clear();
            agent.Items.Clear();
            agent.Items.Add("SEMUA");
            init();
        }

    }
}
