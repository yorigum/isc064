using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Diagnostics;

namespace ISC064.KPA.Laporan
{
    public partial class SummaryPotensiKPA : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Act.PersList(pers);
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                init();
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }

        private void init()
        {
            Cf.BindBulan(bln);
            Cf.BindTahun(thn);

            bln.SelectedValue = DateTime.Today.Month.ToString();
            thn.SelectedValue = DateTime.Today.Year.ToString();
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

            int Bln = Convert.ToInt32(bln.SelectedValue);
            int Thn = Convert.ToInt32(thn.SelectedValue);

            Rpt.SubJudul(x, "Periode: " + Cf.Monthname(Bln) + " " + Thn);

            Rpt.SubJudul(
                x, "Project : " + project.SelectedValue
                );
            Rpt.SubJudul(
                x, "Perusahaan : " + pers.SelectedItem.Text
                );
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, "", x);
        }

        private void Fill()
        {
            int Bln = Convert.ToInt32(bln.SelectedValue);
            int Thn = Convert.ToInt32(thn.SelectedValue);

            string strSql = "SELECT DISTINCT BankKPR FROM MS_KONTRAK WHERE BankKPR <> ''";
            DataTable rs = Db.Rs(strSql);

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0, t10 = 0, t11 = 0, t12 = 0, t13 = 0, t14 = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                decimal ar = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "POTENSIKPR", "RP");
                c.Text = Cf.Num(ar);
                t1 += ar;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal au = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "POTENSIKPR", "UNIT");
                c.Text = Cf.Num(au);
                t2 += au;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "SP3K", "RP"));
                t3 += Convert.ToDecimal(c.Text);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "SP3K", "UNIT"));
                t4 += Convert.ToDecimal(c.Text);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal br = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "REALISASIAKAD", "RP");
                c.Text = Cf.Num(br);
                t5 += br;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal bu = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "REALISASIAKAD", "UNIT");
                c.Text = Cf.Num(bu);
                t6 += bu;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "SP3KBELUMAKAD", "RP"));
                t7 += Convert.ToDecimal(c.Text);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "SP3KBELUMAKAD", "UNIT"));
                t8 += Convert.ToDecimal(c.Text);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal cr = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "KPRTOLAK", "RP");
                c.Text = Cf.Num(cr);
                t9 += cr;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal cu = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "KPRTOLAK", "UNIT");
                c.Text = Cf.Num(cu);
                t10 += cu;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal dr = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "BATAL", "RP");
                c.Text = Cf.Num(dr);
                t11 += dr;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal du = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "BATAL", "UNIT");
                c.Text = Cf.Num(du);
                t12 += du;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal SisaRp = ar - br - cr - dr;
                c.Text = Cf.Num(SisaRp);
                t13 += SisaRp;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal SisaUnit = au - bu - cu - du;
                c.Text = Cf.Num(SisaUnit);
                t14 += SisaUnit;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                if (i == (rs.Rows.Count - 1))
                    SubTotal(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
            }
        }

        protected decimal Hitung(string BankKPR, int Bln, int Thn, string Tipe, string x)
        {
            string addq = "";
            string strSql = "";

            string Project = " AND Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND Pers = '" + pers.SelectedValue + "'";

            if (Tipe == "SP3K")
            {
                addq += " AND StatusSP3K = 'SELESAI' AND HasilSP3K = 'SETUJU'";
            }

            if (Tipe == "REALISASIAKAD")
            {
                addq += " AND Status = 'A' AND StatusAkad = 'SELESAI'";
            }

            if (Tipe == "SP3KBELUMAKAD")
            {
                addq += " AND StatusSP3K = 'SELESAI' AND HasilSP3K = 'SETUJU' AND StatusAkad = 'DIJADWALKAN'";
            }

            if (Tipe == "KPRTOLAK")
            {
                addq += " AND Status = 'A' AND (KetWawancara LIKE '%TOLAK%' OR HasilOTS = 'TOLAK' OR HasilSP3K = 'TOLAK')";
            }

            if (Tipe == "BATAL")
            {
                addq += " AND Status = 'B'";
            }

            if (x == "RP")
            {
                if (Tipe == "POTENSIKPR" || Tipe == "KPRTOLAK" || Tipe == "BATAL")
                {
                    strSql = "SELECT ISNULL(SUM(NilaiPengajuan), 0)"
                        + " FROM MS_KONTRAK"
                        + " WHERE BankKPR = '" + BankKPR + "'"
                        + Project
                        + Perusahaan
                        + addq
                        ;
                }
                else if (Tipe == "SP3K" || Tipe == "SP3KBELUMAKAD")
                {
                    strSql = "SELECT ISNULL(SUM(ApprovalKPR), 0)"
                        + " FROM MS_KONTRAK"
                        + " WHERE BankKPR = '" + BankKPR + "'"
                        + Project
                        + Perusahaan
                        + addq
                        ;
                }
                else if (Tipe == "REALISASIAKAD")
                {
                    strSql = "SELECT ISNULL(SUM(RealisasiAkad), 0)"
                        + " FROM MS_KONTRAK"
                        + " WHERE BankKPR = '" + BankKPR + "'"
                        + Project
                        + Perusahaan
                        + addq
                        ;
                }
                else
                {
                    strSql = "SELECT ISNULL(SUM(NilaiKontrak), 0)"
                        + " FROM MS_KONTRAK"
                        + " WHERE BankKPR = '" + BankKPR + "'"
                        + Project
                        + Perusahaan
                        + addq
                        ;
                }
            }
            else
            {
                strSql = "SELECT COUNT(*)"
                    + " FROM MS_KONTRAK"
                    + " WHERE BankKPR = '" + BankKPR + "'"
                    + Project
                    + Perusahaan
                    + addq
                    ;
            }

            decimal Nilai = Db.SingleDecimal(strSql);
            return Nilai;
        }

        protected void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, decimal t8, decimal t9, decimal t10, decimal t11, decimal t12, decimal t13, decimal t14)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "<strong>TOTAL</strong>";
            c.ColumnSpan = 2;
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

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Summary Potensi KPR";
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

            string nfilename = "LapSummaryPotensiKPA" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LapSummaryPotensiKPA" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter

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
            string link = Mi.PathAlamatWeb + "kpa/LaporanPDF/PDFSummaryPotensiKPA.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&bln=" + bln.SelectedValue + "&thn=" + thn.SelectedValue + "&project=" + Project + "&pers=" + pers.SelectedValue + "";

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

        protected void scr_Click(object sender, System.EventArgs e)
        {
            Report();
        }

        protected void xls_Click(object sender, System.EventArgs e)
        {
            Report();
            Rpt.ToExcel(this,headReport, rpt);

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


        protected void pers_SelectedIndexChanged(object sender, EventArgs e)
        {
            project.Items.Clear();
            if (pers.SelectedIndex == 0)
            {
                project.Items.Add(new ListItem("SEMUA"));
                Act.ProjectList(project);
            }
            else
                Act.ProjectList(project, pers.SelectedValue);
        }
    }
}
