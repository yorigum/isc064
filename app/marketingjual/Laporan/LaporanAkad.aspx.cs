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
    public partial class Laporan_LaporanAkad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());

            DataTable rs = Db.Rs("SELECT DISTINCT BankKPR FROM MS_KONTRAK");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                rekening.Items.Add(new ListItem(rs.Rows[i]["BankKPR"].ToString()));
            }

            rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_KONTRAK WHERE Project IN (" + Act.ProjectListSql + ") ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            Act.ProjectList(project);
            Act.PersList(pers);
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
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Akad";
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
                    + ",'" + Convert.ToDateTime(dari.Text) + "'"
                    + ",'" + Convert.ToDateTime(sampai.Text) + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "LaporanAkad" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LaporanAkad" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //parameter
            string Lokasi = lokasi.SelectedValue;
            string Rek = rekening.SelectedValue;

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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFLaporanAkad.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&lokasi=" + Lokasi
                + "&rek=" + Rek
                + "&pers=" + pers.SelectedValue
                + "&project=" + Project
                + "&userid=" + UserID
                ;

            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation portrait --page-width 8.5in --page-height 11in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

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
        }
        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            //Rpt.SubJudul(x
            //    , "Lokasi : " + lokasi.SelectedItem.Text
            //    );

            string tgl = "";
            if (tbTgl.Checked) tgl = tbTgl.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );


            if (rekening.SelectedIndex == 0)
            {
                Rpt.SubJudul(x, "Rekening Bank: SEMUA");
                if (lokasi.SelectedIndex == 0)
                {
                    Rpt.SubJudul(x, "Lokasi / Tower: SEMUA");
                    //Rpt.Header(rpt, x);
                    Rpt.HeaderReport(headReport, "", x);
                    Fill();
                }
                else
                {
                    Rpt.SubJudul(x, "Lokasi / Tower: " + lokasi.SelectedValue);
                    //Rpt.Header(rpt, x);
                    Rpt.HeaderReport(headReport, "", x);
                    tower.Visible = false;
                    Fill2();
                }

            }
            else
            {
                Rpt.SubJudul(x, "Rekening Bank: " + rekening.SelectedValue);
                if (lokasi.SelectedIndex == 0)
                {
                    Rpt.SubJudul(x, "Lokasi / Tower: SEMUA");
                    //Rpt.Header(rpt, x);
                    Rpt.HeaderReport(headReport, "", x);
                    Fill();
                }
                else
                {
                    Rpt.SubJudul(x, "Lokasi / Tower: " + lokasi.SelectedValue);
                    //Rpt.Header(rpt, x);
                    Rpt.HeaderReport(headReport, "", x);
                    tower.Visible = false;
                    Fill2();
                }
            }



        }
        private void Fill()
        {
            string Lokasi = "";
            if (lokasi.SelectedIndex > 0)
                Lokasi += " AND c.Lokasi = '" + lokasi.SelectedValue + "'";

            string tgl = "";
            if (tbTgl.Checked)
                tgl = "TglAkad";

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string Tanggal = "";
            Tanggal = " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'";


            string BankKPR = "";
            if (rekening.SelectedIndex != 0)
                BankKPR = " AND BankKPR = '" + rekening.SelectedValue + "'";

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND a.NoAgent = " + UserAgent();

            string strSql = "SELECT a.*, b.*, C.Lokasi AS L, c.LuasSG"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_UNIT c ON a.NoStock = c.NoStock"
                + " WHERE a.Status = 'A'"
                + " AND StatusAkad = 'SELESAI'"
                + Project
                + Perusahaan
                + Lokasi
                + Tanggal
                + BankKPR
                + aa
                + "ORDER BY TglAkad"
                ;
            DataTable rs = Db.Rs(strSql);

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0;
            decimal PotensiKPR = 0;
            decimal RealisasiAkad = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoAkad"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglAkad"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);


                c = new TableCell();
                c.Text = rs.Rows[i]["KTP1"].ToString()
                    + "</br>" + rs.Rows[i]["KTP2"].ToString()
                    + "</br>" + rs.Rows[i]["KTP3"].ToString()
                    + "</br>" + rs.Rows[i]["KTP4"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoAgent"].ToString() + "' ");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["LuasSG"]));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["L"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Gross"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]), 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                PotensiKPR = Convert.ToDecimal(rs.Rows[i]["NilaiPengajuan"]);
                c.Text = Cf.Num(PotensiKPR);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                RealisasiAkad = Convert.ToDecimal(rs.Rows[i]["ApprovalKPR"]);
                c.Text = Cf.Num(RealisasiAkad);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += Convert.ToDecimal(rs.Rows[i]["Gross"]);
                t2 += Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]);
                t3 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                t4 += PotensiKPR;
                t5 += RealisasiAkad;
                t6 += Convert.ToDecimal(rs.Rows[i]["LuasSG"]);
            }
            SubTotal(t1, t2, t3, t4, t5, t6);
        }

        private void Fill2()
        {
            string Lokasi = "";
            if (lokasi.SelectedIndex > 0)
                Lokasi += " AND c.Lokasi = '" + lokasi.SelectedValue + "'";

            string tgl = "";
            if (tbTgl.Checked)
                tgl = "TglAkad";

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string Tanggal = "";
            Tanggal = " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'";

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            string BankKPR = "";
            if (rekening.SelectedIndex != 0)
                BankKPR = " AND BankKPR = '" + rekening.SelectedValue + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND a.NoAgent = " + UserAgent();

            string strSql = "SELECT a.*, b.*, C.Lokasi AS L"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_UNIT c ON a.NoStock = c.NoStock"
                + " WHERE a.Status = 'A'"
                + " AND StatusAkad = 'SELESAI'"
                + Project
                + Perusahaan
                + Lokasi
                + Tanggal
                + BankKPR
                + aa
                + "ORDER BY TglAkad"
                ;
            DataTable rs = Db.Rs(strSql);

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0;
            decimal PotensiKPR = 0;
            decimal RealisasiAkad = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoAkad"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglAkad"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);


                c = new TableCell();
                c.Text = rs.Rows[i]["KTP1"].ToString()
                    + "</br>" + rs.Rows[i]["KTP2"].ToString()
                    + "</br>" + rs.Rows[i]["KTP3"].ToString()
                    + "</br>" + rs.Rows[i]["KTP4"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoAgent"].ToString() + "' ");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["Luas"]), 2));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                //c = new TableCell();
                //c.Text = rs.Rows[i]["L"].ToString();
                //c.HorizontalAlign = HorizontalAlign.Left;
                //c.VerticalAlign = VerticalAlign.Top;
                //r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Gross"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]), 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                PotensiKPR = Convert.ToDecimal(rs.Rows[i]["NilaiPengajuan"]);
                c.Text = Cf.Num(PotensiKPR);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                RealisasiAkad = Convert.ToDecimal(rs.Rows[i]["ApprovalKPR"]);
                c.Text = Cf.Num(RealisasiAkad);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += Convert.ToDecimal(rs.Rows[i]["Gross"]);
                t2 += Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]);
                t3 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                t4 += PotensiKPR;
                t5 += RealisasiAkad;
                t6 += Convert.ToDecimal(rs.Rows[i]["Luas"]);
            }
            SubTotal(t1, t2, t3, t4, t5, t6);
        }

        protected void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5
            , decimal t6)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "<strong>GRAND TOTAL</strong>";
            c.ColumnSpan = 9;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t6, 2));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            if (lokasi.SelectedIndex == 0)
                c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t2, 0));
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

            rpt.Rows.Add(r);
        }

        protected void scr_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                Report();
            }
        }
        protected void xls_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                Report();
                Rpt.ToExcel(this, rpt);
            }
        }


    }
}