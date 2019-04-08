using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace ISC064.LEGAL.Laporan
{
    public partial class LaporanPPJB : System.Web.UI.Page
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
                Rpt.ToExcel(this, rpt);
            }
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

            string nfilename = "LapPPJB" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LapPPJB" + rs.Rows[0]["AttachmentID"] + ".pdf";

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
            string link = Mi.PathAlamatWeb + "legal/LaporanPDF/PDFLaporanPPJB.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&project=" + Project + "&pers=" + pers.SelectedValue + "";

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
        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            Header();
            FillColumn();
            Fill();
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
            //    + "<h1 class='title'>LAPORAN REKAP PPJB</h1>"
            //    + "Dari " + Cf.Day(dari.Text) + " sampai " + Cf.Day(sampai.Text)
            //    + "<br />"
            //    + "<br />"
            //    + x
            //    ;
        }

        protected void FillColumn()
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            Label l;
            l = new Label();
            string Project = (project.SelectedIndex == 0) ? " IN(" + Act.ProjectListSql + ") " : " = '" + project.SelectedValue + "'";
            DataTable rs3 = Db.Rs("SELECT DISTINCT(Nama) FROM REF_BERKAS_PPJB WHERE Project " + Project);
            for (int i = 0; i < rs3.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                l.ID = "nama_" + i;
                s.Append("<th vertical-align=\"middle\" rowspan=\"2\" style=\"background-color:#5c9bd1;color:white\">" + rs3.Rows[i]["Nama"] + "</th>");
                l.Text = s.ToString();
            }
            col1.Controls.Add(l);
        }

        private void Fill()
        {            
            string nProject = (project.SelectedIndex == 0) ? " AND A.Project IN(" + Act.ProjectListSql + ") " : " AND A.Project = '" + project.SelectedValue + "'";            
            string nPerusahaan = "";
            if (pers.SelectedIndex != 0) nPerusahaan = " AND A.Pers = '" + pers.SelectedValue + "'";

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            string strSql = "SELECT A.*,B.*,C.*,D.*, D.NoPPJB AS NomorPPJB "
                          + ", D.NoPPJBm AS NomorPPJBm"
                          + " FROM MS_KONTRAK A "
                          + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER B  ON B.NoCustomer = A.NoCustomer"
                          + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT C ON C.NoUnit = A.NoUnit"
                          + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PPJB D ON A.NoKontrak = D.NoKontrak"
                          + " WHERE D.PPJB != 'B'"
                          + " AND CONVERT(varchar,D.TglPPJB,112) >= '" + Cf.Tgl112(Dari) + "'"
                          + " AND CONVERT(varchar,D.TglPPJB,112) <= '" + Cf.Tgl112(Sampai) + "'"
                          + nProject
                          + nPerusahaan
                          + " ORDER BY D.NoPPJB";

            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                Label l2;
                Label rb;
                TextBox t;
                TextBox tgl;
                HtmlInputButton btn;

                l = new Label();
                l2 = new Label();
                string berkas = "";
                string kurang = "";

                string PPJBu;
                if (rs.Rows[i]["PPJBu"].ToString() == "1")
                {
                    PPJBu = "Manual";
                }
                else
                {
                    PPJBu = "System";
                }

                l.Text = "<tr>"
                    + "<td>" + (i + 1).ToString() + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["Nama"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["NoUnit"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["Nomor"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["Lantai"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["LuasSG"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["PPJB"]) + "</td>"
                    + "<td align='right'>" + (Cf.Day(rs.Rows[i]["TglPPJB"])) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["TglCetakPPJB"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["TglTTDPPJB"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["NomorPPJB"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["NomorPPJBm"]) + "</td>"
                    + "<td align='right'>" + PPJBu + "</td>";
                //+ "<td>";
                list.Controls.Add(l);

                string tgllengkap = "";
                string Project = (project.SelectedIndex == 0) ? " IN(" + Act.ProjectListSql + ") " : " = '" + project.SelectedValue + "'";
                DataTable rs3 = Db.Rs("SELECT DISTINCT(Nama) FROM REF_BERKAS_PPJB WHERE Project " + Project);
                DataTable rs2 = Db.Rs("SELECT * FROM MS_BERKAS_PPJB WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "'");
                for (int j = 0; j < rs3.Rows.Count; j++)
                {
                    if (!Response.IsClientConnected) break;

                    l = new Label();
                    l.Text = "<td>";
                    list.Controls.Add(l);

                    string Status = "";
                    rb = new Label();
                    int ada = Db.SingleInteger("SELECT COUNT(*) FROM MS_BERKAS_PPJB WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' AND Nama = '" + rs3.Rows[j]["Nama"] + "'");
                    if (rs2.Rows.Count > 0)
                    {
                        rb.Width = 100;
                        //rb.Text = Db.SingleBool("Select ISNULL((SELECT CAST(ISNULL (Value,0) AS bit) From MS_BERKAS_PPJB WHERE NoKontrak =  '" + rs.Rows[i]["NoKontrak"] + "' AND NoBerkas='" + (i + 1) + "'),0)") ? 1 : 0;
                        int pl = Db.SingleBool("Select ISNULL((SELECT CAST(ISNULL (Value,0) AS bit) From MS_BERKAS_PPJB WHERE NoKontrak =  '" + rs.Rows[i]["NoKontrak"] + "' AND Nama='" + rs3.Rows[j]["Nama"] + "'),0)") ? 1 : 0;

                        if (pl == 0)
                            Status = "Tidak Ada";
                        else
                            Status = "Ada";

                        tgllengkap = Cf.Day(rs2.Rows[i]["TglLengkap"]);
                    }
                    else
                    {
                        Status = "Tidak Ada";
                    }
                    rb.Text = Status;
                    list.Controls.Add(rb);
                }

                for (int j = 0; j < rs2.Rows.Count; j++)
                {
                    int z = Db.SingleBool("Select ISNULL((SELECT CAST(ISNULL (Value,0) AS bit) From MS_BERKAS_PPJB WHERE NoKontrak =  '" + rs.Rows[i]["NoKontrak"] + "' AND NoBerkas='" + rs2.Rows[j]["NoBerkas"] + "'),0)") ? 0 : 1;
                    if (z == 0)
                    {
                        berkas = "Tidak Lengkap";
                    }
                    else
                    {
                        berkas = "Lengkap";
                    }

                    int a = Db.SingleBool("Select ISNULL((SELECT CAST(ISNULL (Value,0) AS bit) From MS_BERKAS_PPJB WHERE NoKontrak =  '" + rs.Rows[i]["NoKontrak"] + "' AND NoBerkas='" + rs2.Rows[j]["NoBerkas"] + "' AND Nama = '" + rs2.Rows[j]["Nama"] + "'),0)") ? 1 : 0;
                    if (a == 0)
                    {
                        kurang += (rs2.Rows[j]["Nama"]).ToString() + ",";
                    }
                }

                if (rs2.Rows.Count == 0)
                {
                    berkas = "Tidak Lengkap";
                    DataTable rs4 = Db.Rs("SELECT Nama From REF_BERKAS_PPJB WHERE Project = (SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "')");
                    for(int m = 0; m < rs4.Rows.Count; m++)
                    {
                        kurang += (rs4.Rows[m]["Nama"]).ToString() + ",";
                    }
                    tgllengkap = "-";
                }

                l = new Label();
                l.Text = "</td>";
                l2.Text = "<td align='right'>" + (rs.Rows[i]["NPWP"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["NoKontrak"]) + "</td>"
                    + "<td align='right'>" + berkas + "</td>"
                    + "<td align='right'>" + kurang + "</td>"
                    + "<td align='right'>" + tgllengkap + "</td>";
                list.Controls.Add(l2);

                l.Text = "</tr>";
                list.Controls.Add(l);
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
