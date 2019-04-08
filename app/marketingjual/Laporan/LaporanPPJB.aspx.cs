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
    public partial class LaporanPPJB : System.Web.UI.Page
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

        private void init()
        {
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());
            project.Items.Clear();
            project.Items.Add("SEMUA");
            Act.ProjectList(project);
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan PPJB";
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

            string nfilename = "LaporanPPJB" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LaporanPPJB" + rs.Rows[0]["AttachmentID"] + ".pdf";

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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFLaporanPPJB.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&userid=" + UserID
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
                ;

            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 35cm --page-height 55cm --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

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

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , "Tanggal PPJB : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );
            Rpt.SubJudul(x
                , "Project : " + project.SelectedItem.Text
                );
            Rpt.HeaderReport(headReport, "", x);
            //x.Append("Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
            //  + ", " + Cf.Date(DateTime.Now)
            //  + " dari workstation : " + Act.IP
            //  + " dan username : " + Act.UserID);

            //lblHeader.Text = "<h3>" + Mi.Pt + "</h3>"
            //    + "<h1 class='title'>LAPORAN REKAP PPJB</h1>"
            //    + "Dari " + Cf.Day(dari.Text) + " sampai " + Cf.Day(sampai.Text)
            //    + "<br />"
            //    + "<br />"
            //    + x
            //    ;
        }
        private void Fill()
        {
            rpt.Style["border-collapse"] = "collapse";

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }
            string Project = " AND MS_KONTRAK.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND MS_KONTRAK.Pers = '" + pers.SelectedValue + "'";

            string strSql = "SELECT * FROM MS_KONTRAK"
                          + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER  ON MS_CUSTOMER.NoCustomer = MS_KONTRAK.NoCustomer"
                          + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT ON MS_UNIT.NoUnit = MS_KONTRAK.NoUnit"
                          + " WHERE PPJB != 'B'"
                          + " AND CONVERT(varchar,TglPPJB,112) >= '" + Cf.Tgl112(Dari) + "'"
                          + " AND CONVERT(varchar,TglPPJB,112) <= '" + Cf.Tgl112(Sampai) + "'"
                          + Project + Perusahaan
                          + " ORDER BY MS_KONTRAK.NoPPJB";

            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;
                bool a = true;
                bool b = true;
                bool d = true;
                bool e = true;
                bool f = true;
                bool g = true;
                bool h = true;
                bool j = true;
                bool k = true;

                r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string[] x = Cf.SplitByString(rs.Rows[i]["NoUnit"].ToString(), "/");

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["Nomor"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["Lokasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["Lantai"].ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = Cf.Num(rs.Rows[i]["LuasSG"]) + "m<sup>2</sup>";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //    B = BELUM
                //    D = SUDAH REGIS
                //    T = PROSES TTD
                //    S = SELESAI

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["PPJB"].ToString() == "D")
                {
                    c.Text = "Teregister";
                }
                else if (rs.Rows[i]["PPJB"].ToString() == "T")
                {
                    c.Text = "Proses Tanda Tangan";
                }
                else if (rs.Rows[i]["PPJB"].ToString() == "S")
                {
                    c.Text = "Selesai";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);


                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglPPJB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglPPJB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);


                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglCetakPPJB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglCetakPPJB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglTTDPPJB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglTTDPPJB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoPPJB"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoPPJBm"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["PPJBu"].ToString() == "1")
                {
                    c.Text = "Manual";
                }
                else
                {
                    c.Text = "System";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);


                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoKTP"].ToString() + " ";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["KTPMilik"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                }
                else
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["KTPIstri"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                else
                {
                    c.Text = "Tidak Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);



                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["KK"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    a = false;
                }
                else if (rs.Rows[i]["KK"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["SNKH"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    b = false;
                }
                else if (rs.Rows[i]["SNKH"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["SKK"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    k = false;
                }
                else if (rs.Rows[i]["SKK"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["RK"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    d = false;
                }
                else if (rs.Rows[i]["RK"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["BT"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    e = false;
                }
                else if (rs.Rows[i]["BT"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["KW"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    f = false;
                }
                else if (rs.Rows[i]["KW"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);


                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["DU"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    g = false;
                }
                else if (rs.Rows[i]["DU"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["DL"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    h = false;
                }
                else if (rs.Rows[i]["DL"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["SM"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    j = false;
                }
                else if (rs.Rows[i]["SM"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                bool z = a & b & d & e & f & g & h & j & k;
                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (z == false)
                {
                    c.Text = "Tidak Lengkap";
                }
                else if (z == true)
                {
                    c.Text = "Lengkap";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string ker = "";
                if (a == false)
                {
                    ker += "Kartu Keluarga,";
                }
                if (b == false)
                {
                    ker += "Surat Nikah,";
                }
                if (k == false)
                {
                    ker += "SKK,";
                }
                if (d == false)
                {
                    ker += "RK,";
                }
                if (e == false)
                {
                    ker += "BT,";
                }
                if (f == false)
                {
                    ker += "KW,";
                }
                if (g == false)
                {
                    ker += "Denah Unit,";
                }
                if (h == false)
                {
                    ker += "Denah Lantai,";
                }
                if (j == false)
                {
                    ker += "Spesifikasi Material";
                }


                c = new TableCell();
                c.Text = ker;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglLengkapPPJB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglLengkapPPJB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
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

            return x;
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
        }
    }
}
