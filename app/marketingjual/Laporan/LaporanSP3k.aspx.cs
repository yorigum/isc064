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
    public partial class Laporan_LaporanSP3k : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
            string Project = project.SelectedIndex == 0 ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs("SELECT DISTINCT BankKPR FROM MS_KONTRAK WHERE " + Project + " ");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                rekening.Items.Add(new ListItem(rs.Rows[i]["BankKPR"].ToString()));
            }

            //rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_KONTRAK WHERE " + Project + "  ORDER BY Lokasi");
            rs = Db.Rs("SELECT * FROM REF_LOKASI WHERE " + Project + " ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));
            lokasi.SelectedIndex = 0;
            rekening.SelectedIndex = 0;
            
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

            //Rpt.SubJudul(x
            //    , "Lokasi : " + lokasi.SelectedItem.Text
            //    );

            string tgl = "";
            if (tbTarget.Checked) tgl = tbTarget.Text;
            if (tbPengajuan.Checked) tgl = tbPengajuan.Text;
            if (tbTgl.Checked) tgl = tbTgl.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(
                x, "Status SP3K : " + ddlStatus.SelectedItem.Text
                );

            Rpt.SubJudul(
                x, "Hasil SP3K : " + ddlHasil.SelectedItem.Text
                );

            Rpt.SubJudul(
                x, "Project : " + project.SelectedItem.Text
                );

            Rpt.SubJudul(
                x, "Perusahaan : " + pers.SelectedItem.Text
                );

            if (rekening.SelectedIndex == 0)
                Rpt.SubJudul(x, "Rekening Bank: SEMUA");
            else
                Rpt.SubJudul(x, "Rekening Bank: " + rekening.SelectedValue);

            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, "", x);
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan SP3K";
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

            string nfilename = "LaporanSP3K" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LaporanSP3K" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
            string Lokasi = lokasi.SelectedValue;
            string tanggalspk = "";
            if (tbTarget.Checked)
                tanggalspk = "TargetSP3K";
            if (tbPengajuan.Checked)
                tanggalspk = "TglPengajuanSP3K";
            if (tbTgl.Checked)
                tanggalspk = "TglHasilSP3K";
            string rek = rekening.SelectedValue;
            string statusspk = ddlStatus.SelectedValue;
            string hasilspk = ddlHasil.SelectedValue;

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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFLaporanSP3K.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&rek=" + rek
                + "&statusspk=" + statusspk.Replace(" ", "-")
                + "&hasilspk=" + hasilspk.Replace(" ", "-")
                + "&tanggal=" + tanggalspk
                + "&lokasi=" + Lokasi
                + "&userid=" + UserID
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
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

        private void Fill()
        {
            //string w = "";
            string Lokasi = "";
            if (lokasi.SelectedValue != "SEMUA")
                Lokasi += " AND a.Lokasi = '" + lokasi.SelectedValue + "'";

            string tgl = "";
            if (tbTarget.Checked)
                tgl = "TargetSP3K";

            if (tbPengajuan.Checked)
                tgl = "TglPengajuanSP3K";

            if (tbTgl.Checked)
                tgl = "TglHasilSP3K";

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string Status = "";
            if (ddlStatus.SelectedIndex != 0)
            {
                if (ddlStatus.SelectedItem.Text == "BELUM DITENTUKAN")
                    Status = " AND StatusSP3K = ''";
                else
                    Status = " AND StatusSP3K = '" + ddlStatus.SelectedItem.Text + "'";
            }

            string Hasil = "";
            if (ddlHasil.SelectedIndex != 0)
            {
                Hasil = " AND HasilSP3K = '" + ddlHasil.SelectedItem.Text + "'";
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

            string strSql = "SELECT a.*, b.Nama AS NamaCustomer, d.LuasSG"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                  + " INNER JOIN MS_UNIT d ON a.NoUnit = d.NoUnit "
                + " WHERE a.Status = 'A'"
                + Project
                + Perusahaan
                + Lokasi
                + Tanggal
                + Status
                + Hasil
                + BankKPR
                + aa
                ;
            DataTable rs = Db.Rs(strSql);

            decimal t = 0, PotensiKPR = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                r.Attributes["ondblclick"] = "popEditProsesKPR('" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "');";
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaCustomer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LuasSG"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["StatusSP3K"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TargetSP3K"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglPengajuanSP3K"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglHasilSP3K"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoSP3K"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["HasilSP3K"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);
                c = new TableCell();
                PotensiKPR = Convert.ToDecimal(rs.Rows[i]["ApprovalKPR"]);
                c.Text = Cf.Num(PotensiKPR);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);


                //c = new TableCell();
                //PotensiKPR = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0) FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND Tipe = 'ANG' AND (NamaTagihan LIKE '%KPR%' OR NamaTagihan LIKE '%AKAD%')");
                //c.Text = Cf.Num(PotensiKPR);
                //c.HorizontalAlign = HorizontalAlign.Right;
                //r.Cells.Add(c);

                rpt.Rows.Add(r);

                t += PotensiKPR;

                if (i == (rs.Rows.Count - 1))
                    SubTotal(t);
            }
        }
        protected void SubTotal(decimal t)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "<strong>GRAND TOTAL</strong>";
            c.ColumnSpan = 13;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "<strong>" + Cf.Num(t) + "</strong>";
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
                Rpt.ToExcel(this, headReport, rpt);
            }
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
            rekening.Items.Clear();
            lokasi.Items.Clear();
            lokasi.Items.Add("SEMUA");
            rekening.Items.Add("SEMUA");
            init();
        }
    }
}