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

namespace ISC064.COLLECTION.Laporan
{
    public partial class MasterTagihan : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
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

            string Nama = "Laporan Master Tagihan";
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

            string strSql = "SELECT * FROM ISC064_FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "LaporanMasterTagihan" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LaporanMasterTagihan" + rs.Rows[0]["AttachmentID"] + ".pdf";

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

            string StatusKPA = "";
            if (includekpa.Checked)
                StatusKPA = "includekpa";
            else if (excludekpa.Checked)
                StatusKPA = "excludekpa";

            string nmtipe = string.Empty;
            try
            {
                foreach (ListItem item in tipe.Items)
                {
                    if (item.Selected == true)
                    {
                        nmtipe += item.Value.Replace(" ", "%") + "-";
                    }
                }
            }
            catch (Exception)
            {
            }

            string Project = "";
            if(project.SelectedIndex == 0)
            {
                Project = Act.ProjectListSql.Replace("'", "");
            }
            else
            {
                Project = project.SelectedValue;
            }

            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "collection/LaporanPDF/PDFMasterTagihan.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&tipe=" + nmtipe
                + "&status_a=" + nStatusA
                + "&status_b=" + nStatusB
                + "&status_s=" + nStatusS
                + "&jenistgl=" + JenisTgl
                + "&statuskpa=" + StatusKPA
                + "&userid=" + UserID
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
                ;

            //update link
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

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

            Rpt.SubJudul(x
                , "Perusahaan : " + pers.SelectedValue
                );

            Rpt.SubJudul(x
                , "Project : " + project.SelectedValue
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
            string Status = "";
            if (statusA.Checked) Status = " AND " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.Status = 'A'";
            if (statusB.Checked) Status = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Status = 'B'";

            string StatusKPA = "";
            if (includekpa.Checked) StatusKPA = "";
            if (excludekpa.Checked) StatusKPA = " AND ISC064_MARKETINGJUAL..MS_TAGIHAN.KPR = '0' ";

            string tgl = "";
            string order = "";
            if (tglkontrak.Checked)
            {
                tgl = "ISC064_MARKETINGJUAL..MS_KONTRAK.TglKontrak";
                order = "ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak, NoUrut";
            }
            if (tgljt.Checked)
            {
                tgl = "ISC064_MARKETINGJUAL..MS_TAGIHAN.TglJT";
                order = "ISC064_MARKETINGJUAL..MS_TAGIHAN.TglJT, MS_KONTRAK.NoKontrak";
            }

            string Project = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Project ='" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Pers ='" + pers.SelectedValue + "'";

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

            string strSql = "SELECT "
                + " ISC064_MARKETINGJUAL..MS_TAGIHAN.NoKontrak"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.NoUrut"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.Tipe"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.TglKontrak"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NoUnit"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama AS Cs"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.NoTelp AS NoTelp"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.NoHp AS NoHp"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.NamaTagihan"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.TglJT"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.NilaiTagihan"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.Status"
                + ",DATEDIFF(day,GETDATE(),TglJT) AS Diff"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak AND NoTagihan = ISC064_MARKETINGJUAL..MS_TAGIHAN.NoUrut) AS TotalPelunasan"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoCustomer = ISC064_MARKETINGJUAL..MS_CUSTOMER.NoCustomer"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak = ISC064_MARKETINGJUAL..MS_TAGIHAN.NoKontrak"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND ISC064_MARKETINGJUAL..MS_TAGIHAN.Tipe IN (" + Rpt.inSql(tipe) + ")"
                + Project
                + Perusahaan
                + Status
                + StatusKPA
                + aa
                + " ORDER BY " + order;


            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

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

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["NoTelp"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["NoHp"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

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
                c.Text = Cf.Num(rs.Rows[i]["TotalPelunasan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = Cf.Num((decimal)rs.Rows[i]["NilaiTagihan"]
                    - (decimal)rs.Rows[i]["TotalPelunasan"]
                    );
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                total = total + (decimal)rs.Rows[i]["NilaiTagihan"];
                t2 = t2 + (decimal)rs.Rows[i]["TotalPelunasan"];

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", total, t2);
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

        private void SubTotal(string txt, decimal total, decimal t2)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 10;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(total);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(total - t2);
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
