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

namespace ISC064.KPA.Laporan
{
    public partial class MasterPiutangBank : System.Web.UI.Page
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
                //Js.Focus(this,scr);
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
            string Project = (project.SelectedValue != "SEMUA") ? "= '" + project.SelectedValue + "'" : "IN (" + Act.ProjectListSql + ")";
            DataTable rs = Db.Rs("SELECT DISTINCT(Kode) FROM REF_RETENSI WHERE Project " + Project);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                tipe.Items.Add(new ListItem(rs.Rows[i]["Kode"].ToString()));
            }
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

            if (!Cf.isPilih(tipe))
            {
                x = false;
                tipec.Text = " Pilih Minimum Satu";
            }
            else
                tipec.Text = "";

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
                Report();
            }
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
                //Rpt.ToExcel(this,rpt);
                Rpt.ToExcel(this, headReport, rpt);
            }
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Master Piutang Bank";
            string Link = "";
            DateTime TglGenerate = DateTime.Now;
            string FileName = "";
            string FileType = "application/pdf";
            string UserID = Act.UserID;
            string IP = Act.IP;

            Db.Execute("EXEC ISC064_MARKETINGJUAL..spLapPDFDaftar"

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
                "SELECT TOP 1 AttachmentID FROM  ISC064_MARKETINGJUAL..LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "LapMasterPiutangBank" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LapMasterPiutangBank" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //parameter
            string Tipe = tipe.SelectedValue;

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

            string JenisTgl = "";
            if (tglkontrak.Checked == true)
                JenisTgl = "TglKontrak";
            else if (tgljt.Checked == true)
                JenisTgl = "TglJt";

            //string StatusKPA = "";
            //if (includekpa.Checked)
            //    StatusKPA = "includekpa";
            //else if (excludekpa.Checked)
            //    StatusKPA = "excludekpa";

            //string nmtipe = string.Empty;
            //try
            //{
            //    foreach (ListItem item in tipe.Items)
            //    {
            //        if (item.Selected == true)
            //        {
            //            nmtipe += item.Value.Replace(" ", "%") + "-";
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //}
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
            string link = Mi.PathAlamatWeb + "kpa/LaporanPDF/PDFMasterPiutangBank.aspx?id=" + rs.Rows[0]["AttachmentID"]
                //+ "&tipe=" + nmtipe
                + "&status_a=" + nStatusA
                + "&status_b=" + nStatusB
                + "&status_s=" + nStatusS
                + "&jenistgl=" + JenisTgl
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
                //+ "&statuskpa=" + StatusKPA
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
            if (tgljt.Checked) tgl = tgljt.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(x
                , "Tipe : " + Rpt.inSql(tipe).Replace("'", "")
                );

            Rpt.SubJudul(
                x, "Project : " + project.SelectedValue
                );
            Rpt.SubJudul(
                x, "Perusahaan : " + pers.SelectedItem.Text
                );

            //Rpt.Header(rpt, x);

            string legend = "<br />Status: A = Aktif / B = Batal.<br />"
                          + "Tipe : BF = Booking Fee / DP = Downpayment / ANG = Angsuran / ADM = Biaya Administrasi.<br />"
                          + "Cara Bayar: TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer Bank / BG = Cek Giro / DN = Diskon.<br />"
                          + "** = Jatuh Tempo.";

            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            string Project = " AND b.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND b.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND b.Pers = '" + pers.SelectedValue + "'";

            string Status = "";
            if (statusA.Checked) Status = " AND b.Status = 'A'";
            if (statusB.Checked) Status = " AND b.Status = 'B'";

            string StatusKPA = "";
            if (includekpa.Checked) StatusKPA = "";
            if (excludekpa.Checked) StatusKPA = " AND ISC064_MARKETINGJUAL..MS_TAGIHAN_KPA.KPR = '0' ";

            string tgl = "";
            string order = "";
            if (tglkontrak.Checked)
            {
                tgl = "b.TglKontrak";
                order = "b.NoKontrak, a.NoUrut";
            }
            if (tgljt.Checked)
            {
                tgl = "a.TglJT";
                order = "a.TglJT, b.NoKontrak";
            }

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.NoAgent = " + UserAgent();

            decimal total = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            string strSql = "SELECT "
                + " a.NoKontrak"
                + ",a.NoUrut"
                + ",a.Tipe"
                + ",b.TglKontrak"
                + ",c.NoUnit"
                + ",d.Nama AS Cs"
                + ",d.NoTelp AS NoTelp"
                + ",d.NoHp AS NoHp"
                + ",a.NamaTagihan"
                + ",a.TglJT"
                + ",a.NilaiTagihan"
                + ",b.Status"
                + ",DATEDIFF(day,GETDATE(),TglJT) AS Diff"
                //+ ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak AND NoTagihan = ISC064_MARKETINGJUAL..MS_TAGIHAN_KPA.NoUrut) AS TotalPelunasan"
                + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN_KPA a INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER d ON b.NoCustomer = d.NoCustomer"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_UNIT c ON b.NoStock = c.NoStock"
                //+ " INNER JOIN ISC064_FINANCEAR..MS_PENGAJUAN_KPA d ON d.NoPengajuan = b.NoPengajuan"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Tipe IN (" + Rpt.inSql(tipe) + ") "
                + Project
                + Perusahaan
                + Status
                + aa
                + " ORDER BY NoKontrak";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableRow r2 = new TableRow();
                TableCell c;
                TableCell c2;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popJadwalTagihan('" + rs.Rows[i]["NoKontrak"] + "')";

                string jt = "";
                if (Convert.ToInt32(rs.Rows[i]["Diff"]) <= 0)
                    jt = " **";

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"] + "." + rs.Rows[i]["NoUrut"]
                    + jt
                    ;
                c.Font.Size = 10;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["Status"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["Cs"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //c = new TableCell();
                //c.Font.Size = 10;
                //c.Text = rs.Rows[i]["NoTelp"].ToString();
                //c.HorizontalAlign = HorizontalAlign.Left;
                //c.Wrap = false;
                //r.Cells.Add(c);

                //c = new TableCell();
                //c.Font.Size = 10;
                //c.Text = rs.Rows[i]["NoHp"].ToString();
                //c.HorizontalAlign = HorizontalAlign.Left;
                //c.Wrap = false;
                //r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["Tipe"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["NamaTagihan"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = Cf.Day(rs.Rows[i]["TglJT"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                decimal Pengajuan = Db.SingleDecimal("SELECT ISNULL(Total, 0) FROM ISC064_FINANCEAR..MS_PENGAJUAN_KPA a join ISC064_FINANCEAR..MS_PENGAJUAN_KPA_DETIL b ON a.NoPengajuan = b.NoPengajuan WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND b.Notagihan = '" + rs.Rows[i]["NoUrut"] + "'");
                c.Text = Cf.Num(Pengajuan);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                decimal Pel = Db.SingleDecimal("SELECT ISNULL (SUM(NilaiPelunasan),0) FROM MS_PELUNASAN_KPA WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoTagihan = " + rs.Rows[i]["NoUrut"]);
                c.Text = Cf.Num(Pel);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = Cf.Num((decimal)rs.Rows[i]["NilaiTagihan"] - Pel);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                //BrisBru(r, c, rs.Rows[i]["NoKontrak"].ToString());

                rpt.Rows.Add(r);
                total = total + (decimal)rs.Rows[i]["NilaiTagihan"];
                t2 = t2 + Pengajuan;
                t3 = t3 + Pel;

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", total, t2, t3);
            }
        }

        protected void BrisBru(TableRow r, TableCell c, string NoKontrak)
        {
            DataTable rsa = Db.Rs("SELECT * FROM MS_TAGIHAN_KPA WHERE NoKontrak = '" + NoKontrak + "'");
            for (int s = 0; s < rsa.Rows.Count; s++)
            {
                if (s > 0)
                {
                    r = new TableRow();
                    c = new TableCell();
                    c.Text = "";
                    c.ColumnSpan = 9;
                    r.Cells.Add(c);
                }


                c = new TableCell();
                c.Text = Cf.Num(rsa.Rows[s]["NilaiTagihan"]);
                r.Cells.Add(c);

                decimal Pel = Db.SingleDecimal("SELECT ISNULL (SUM(NilaiPelunasan),0) FROM MS_PELUNASAN_KPA WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + rsa.Rows[s]["NoUrut"]);
                c = new TableCell();
                c.Text = Cf.Num(Pel);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rsa.Rows[s]["NilaiTagihan"]) - Pel);
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }
        }

        private string DetilLunas(string NoKontrak, int NoTagihan)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT CaraBayar, TglPelunasan, Ket FROM ISC064_MARKETINGJUAL..MS_PELUNASAN"
                + " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + NoTagihan
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br>");
                x.Append(rs.Rows[i]["CaraBayar"] + ", " + Cf.Day(rs.Rows[i]["TglPelunasan"]) + " "
                    + rs.Rows[i]["Ket"]);
            }

            return x.ToString();
        }

        private void SubTotal(string txt, decimal total, decimal t2, decimal t3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 8;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(total);
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
            c.Text = Cf.Num(total - t3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        protected void tipeCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < tipe.Items.Count; i++)
            {
                tipe.Items[i].Selected = tipeCheck.Checked;
            }

            Js.Focus(this, tipeCheck);
            tipec.Text = "";
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
            tipe.Items.Clear();
            init();
        }

        protected void pers_SelectedIndexChanged(object sender, EventArgs e)
        {
            project.Items.Clear();
            tipe.Items.Clear();
            if (pers.SelectedIndex == 0)
            {
                project.Items.Add(new ListItem("SEMUA"));
                Act.ProjectList(project);
            }
            else
                Act.ProjectList(project, pers.SelectedValue);
            
            init();
        }
    }
}
