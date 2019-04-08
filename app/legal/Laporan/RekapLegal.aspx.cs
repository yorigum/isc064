using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace ISC064.LEGAL.Laporan
{

    public partial class RekapLegal: System.Web.UI.Page
{
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                init();
                rpt.Visible = false;
                Js.Focus(this, scr);
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
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
                Rpt.ToExcel(this,headReport, rpt);
            }
        }
        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            Header();
            Fill();
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Rekap Legal";
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

            string nfilename = "LapRekapLegal" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LapRekapLegal" + rs.Rows[0]["AttachmentID"] + ".pdf";
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
            string link = Mi.PathAlamatWeb + "legal/LaporanPDF/PDFRekapLegal.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&project=" + Project + "&pers=" + pers.SelectedValue + "";

            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 15in --page-height 20in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

            //panggil aplikasi untuk mengconvert pdf
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();

            //60000 -> waktu jeda lama convert pdf
            p.WaitForExit(30000);

            string Src = Mi.PathFilePDFReport + nfilename;
            Mi.DownloadPDF(this, Src, (rs.Rows[0]["FileName"]).ToString(), rs.Rows[0]["FileType"].ToString());
        }
        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , "Tanggal : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );
            Rpt.SubJudul(x
                , "Project : " + project.SelectedValue
                );
            Rpt.SubJudul(x
                , "Perusahaan : " + pers.SelectedItem.Text
                );
            Rpt.HeaderReport(headReport, "", x);
            //x.Append("Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
            //  + ", " + Cf.Date(DateTime.Now)
            //  + " dari workstation : " + Act.IP
            //  + " dan username : " + Act.UserID);

            //lblHeader.Text = "<h3>" + Mi.Pt + "</h3>"
            //    + "<h1 class='title'>REKAP LEGAL</h1>"
            //    + "Dari " + Cf.Day(dari.Text) + " sampai " + Cf.Day(sampai.Text)
            //    + "<br />"
            //    + "<br />"
            //    + x
            //    ;
        }
        private void Fill()
        {
            //rpt.Style["border-collapse"] = "collapse";

            string Project = " AND A.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND A.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND A.Pers = '" + pers.SelectedValue + "'";

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }
            string strSql = "SELECT A.NoKontrak"
                          + ",B.Nama, C.NoUnit"
                          + ",D.TglTargetPPJB,D.PPJB,D.NoPPJB,D.TglTTDPPJB,D.TglLengkapPPJB,D.TglPPJB "
                          + ",E.TglTargetAJB,E.AJB,E.NoAJB,E.TglTTDAJB,E.TglAJB "
                          + ",F.TargetST,F.ST,F.NoST,F.TglTTDST,F.TglST "
                          + ",G.TglTargetIMB,G.StatusIMB,G.NoIMB,G.TglProsesIMB,G.TglIMB "
                          + ",H.TglTargetSertifikat,H.StatusSertifikat,H.NoSertifikat,H.TglProsesSertifikat,H.TglSertifikat "
                          //+ ",E.*,F.*,G.*,H.* "
                          + "FROM MS_KONTRAK A"
                          + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER B  ON B.NoCustomer = A.NoCustomer"
                          + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT C ON C.NoUnit = A.NoUnit"
                          + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PPJB D ON A.NoKontrak = D.NoKontrak"
                          + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AJB E ON A.NoKontrak = E.NoKontrak"
                          + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_BAST F ON A.NoKontrak = F.NoKontrak"
                          + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_IMB G ON A.NoKontrak = G.NoKontrak"
                          + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_SERTIFIKAT H ON A.NoKontrak = H.NoKontrak"
                        //+" WHERE D.PPJB != 'B' AND E.AJB != 'B' AND F.ST != 'B' AND G.StatusIMB != '0' AND H.StatusSertifikat != '0'"
                        + " WHERE CONVERT(varchar,A.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "'"
                        + " AND CONVERT(varchar,A.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'"
                        + Project
                        + Perusahaan
                        + " ORDER BY A.NoKontrak";
            //Response.Write(strSql);

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
                r.Cells.Add(c); //nomor

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false; //nomor kontrak
                r.Cells.Add(c);

                //string[] x = Cf.SplitByString(rs.Rows[i]["NoUnit"].ToString(), "/");

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false; //Custsomer
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false; //unit
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

                //    B = BELUM
                //    D = SUDAH REGIS
                //    T = PROSES TTD
                //    S = SELESAI

                //bool z = a & b & d & e & f & g & h & j & k;
                //c = new TableCell();
                //c.VerticalAlign = VerticalAlign.Top;
                //if (z == false)
                //{
                //    c.Text = "Tidak Lengkap";
                //}
                //else if (z == true)
                //{
                //    c.Text = "Lengkap";
                //}
                //c.HorizontalAlign = HorizontalAlign.Left;
                //c.Wrap = false;
                //r.Cells.Add(c); //kelengkapan berkas

                //string ker = "";
                //if (a == false)
                //{
                //    ker += "Kartu Keluarga,";
                //}
                //if (b == false)
                //{
                //    ker += "Surat Nikah,";
                //}
                //if (k == false)
                //{
                //    ker += "SKK,";
                //}
                //if (d == false)
                //{
                //    ker += "RK,";
                //}
                //if (e == false)
                //{
                //    ker += "BT,";
                //}
                //if (f == false)
                //{
                //    ker += "KW,";
                //}
                //if (g == false)
                //{
                //    ker += "Denah Unit,";
                //}
                //if (h == false)
                //{
                //    ker += "Denah Lantai,";
                //}
                //if (j == false)
                //{
                //    ker += "Spesifikasi Material";
                //}

                //c = new TableCell();
                //c.Text = ker;
                //c.HorizontalAlign = HorizontalAlign.Left;
                //c.Wrap = false;
                //r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglTargetPPJB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglTargetPPJB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //c = new TableCell();
                //c.VerticalAlign = VerticalAlign.Top;
                //if (rs.Rows[i]["PPJB"].ToString() == "B")
                //{
                //    c.Text = "Belum PPJB";
                //}
                //else if (rs.Rows[i]["PPJB"].ToString() == "S")
                //{
                //    c.Text = "Target PPJB";
                //}
                //else if (rs.Rows[i]["PPJB"].ToString() == "D")
                //{
                //    c.Text = "Selesai PPJB";
                //}
                //else if (rs.Rows[i]["PPJB"].ToString() == "T")
                //{
                //    c.Text = "Tanda Tangan PPJB";
                //}
                //c.HorizontalAlign = HorizontalAlign.Left;
                //c.Wrap = false;
                //r.Cells.Add(c);

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
                c.Text = rs.Rows[i]["NoPPJB"].ToString();
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
                if (rs.Rows[i]["TglTargetAJB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglTargetAJB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //c = new TableCell();
                //c.VerticalAlign = VerticalAlign.Top;
                //if (rs.Rows[i]["AJB"].ToString() == "B")
                //{
                //    c.Text = "Belum AJB";
                //}
                //else if (rs.Rows[i]["AJB"].ToString() == "S")
                //{
                //    c.Text = "Target AJB";
                //}
                //else if (rs.Rows[i]["AJB"].ToString() == "D")
                //{
                //    c.Text = "Selesai AJB";
                //}
                //else if (rs.Rows[i]["AJB"].ToString() == "T")
                //{
                //    c.Text = "Tanda Tangan AJB";
                //}
                //c.HorizontalAlign = HorizontalAlign.Left;
                //c.Wrap = false;
                //r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglAJB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglAJB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoAJB"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglTTDAJB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglTTDAJB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TargetST"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TargetST"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //c = new TableCell();
                //c.VerticalAlign = VerticalAlign.Top;
                //if (rs.Rows[i]["ST"].ToString() == "B")
                //{
                //    c.Text = "Belum Serah Terima";
                //}
                //else if (rs.Rows[i]["ST"].ToString() == "S")
                //{
                //    c.Text = "Target Serah Terima";
                //}
                //else if (rs.Rows[i]["ST"].ToString() == "D")
                //{
                //    c.Text = "Selesai Serah Terima";
                //}
                //else if (rs.Rows[i]["ST"].ToString() == "T")
                //{
                //    c.Text = "Tanda Tangan Serah Terima";
                //}
                //c.HorizontalAlign = HorizontalAlign.Left;
                //c.Wrap = false;
                //r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglST"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglST"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoST"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglTTDST"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglTTDST"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglTargetIMB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglTargetIMB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglProsesIMB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglProsesIMB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //c = new TableCell();
                //c.VerticalAlign = VerticalAlign.Top;
                //if (rs.Rows[i]["StatusIMB"].ToString() == "0")
                //{
                //    c.Text = "Belum";
                //}
                //else if (rs.Rows[i]["StatusIMB"].ToString() == "1")
                //{
                //    c.Text = "Target IMB";
                //}
                //else if (rs.Rows[i]["StatusIMB"].ToString() == "2")
                //{
                //    c.Text = "Selesai";
                //}
                //else if (rs.Rows[i]["StatusIMB"].ToString() == "3")
                //{
                //    c.Text = "Tanda Tangan";
                //}
                //c.HorizontalAlign = HorizontalAlign.Left;
                //c.Wrap = false;
                //r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglIMB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglIMB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoIMB"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglTargetSertifikat"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglTargetSertifikat"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglProsesSertifikat"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglProsesSertifikat"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //c = new TableCell();
                //c.VerticalAlign = VerticalAlign.Top;
                //if (rs.Rows[i]["StatusSertifikat"].ToString() == "0")
                //{
                //    c.Text = "Belum";
                //}
                //else if (rs.Rows[i]["StatusSertifikat"].ToString() == "1")
                //{
                //    c.Text = "Target Sertifikat";
                //}
                //else if (rs.Rows[i]["StatusSertifikat"].ToString() == "2")
                //{
                //    c.Text = "Selesai";
                //}
                //else if (rs.Rows[i]["StatusSertifikat"].ToString() == "3")
                //{
                //    c.Text = "Tanda Tangan";
                //}
                //c.HorizontalAlign = HorizontalAlign.Left;
                //c.Wrap = false;
                //r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglSertifikat"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglSertifikat"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoSertifikat"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
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
