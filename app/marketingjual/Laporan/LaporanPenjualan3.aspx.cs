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
    public partial class LaporanPenjualan3 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                headJudul.Visible = false;
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
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Penjualan Tahunan";
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

            string nfilename = "LaporanPenjualanTahunan" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LaporanPenjualanTahunan" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
            string Lokasi = lokasi.SelectedValue.Replace(" ", "%");

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
            try
            {
                foreach (ListItem item in cblcarabayar.Items)
                {
                    if (item.Selected == true)
                    {
                        nm2 += item.Value.Replace(" ", "+") + "-";
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

            string Agent = "";
            if (agent.SelectedIndex > 0)
                Agent = agent.SelectedValue.ToString();
            else
                Agent = "SEMUA";

            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFLaporanPenjualan3.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&tipe=" + nm + "&lokasi=" + Lokasi + "&userid=" + UserID + "&thn1=" + thn1.SelectedValue + "&thn2=" + thn2.SelectedValue + "&carabayar=" + nm2 + "&agent=" + Agent + "&project=" + Project + "&pers=" + pers.SelectedValue + "";

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
        private void init()
        {
            Cf.BindTahun(thn1);
            Cf.BindTahun(thn2);

            thn1.SelectedValue = thn2.SelectedValue = DateTime.Now.Year.ToString();

            DataTable rs;
            string Project = project.SelectedIndex == 0 ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            rs = Db.Rs("SELECT * FROM REF_JENIS WHERE " + Project + "  ORDER BY SN");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Jenis"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                jenis.Items.Add(new ListItem(t, v));
                jenis.Items[i].Selected = true;
            }

            cblcarabayar.Items.Add(new ListItem("Cash Keras", "CASH KERAS"));
            cblcarabayar.Items.Add(new ListItem("Cash Bertahap", "CASH BERTAHAP"));
            cblcarabayar.Items.Add(new ListItem("KPR", "KPR"));
            for (int i = 0; i < 3; i++)
            {
                cblcarabayar.Items[i].Selected = true;
            }
            rs = Db.Rs("SELECT * FROM REF_LOKASI WHERE " + Project + " ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            rs = Db.Rs("SELECT DISTINCT Principal FROM MS_AGENT WHERE Status = 'A' ORDER BY Principal");
            for (int i = 0; i < rs.Rows.Count; i++)
                agent.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            lokasi.SelectedIndex = 0;
            agent.SelectedIndex = 0;
            cblcarabayar.SelectedIndex = 0;
            jenis.SelectedIndex = 0;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

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
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
            }


            if (!Cf.isPilih(cblcarabayar))
            {
                x = false;
                if (s == "")
                    s = cblcarabayar.ID;

                errcarabayar.Text = "Pilih minimum satu";
            }
            else
                errcarabayar.Text = "";

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
                rp.Controls.Add(headJudul);
                rp.Controls.Add(rpt);
                Rpt.ToExcel(this, rp);
            }
        }

        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;
            headJudul.Visible = true;

            newHeader();
            //Header();
            Fill();
        }

        private void newHeader()
        {
            string header = "<h2>" + Mi.Pt + "</h2>";
            header += "<h1 class='title'>LAPORAN PENJUALAN TAHUNAN</h1>";
            header += "Periode : " + thn1.SelectedValue + " s/d " + thn2.SelectedValue;
            header += "<br/>Jenis : " + Rpt.inSql(jenis).Replace("'", "");
            header += "<br/>Lokasi : " + lokasi.SelectedItem.Text;
            header += "<br/>Principal : " + agent.SelectedItem.Text;
            header += "<br/>Perusahaan : " + pers.SelectedValue;
            header += "<br/>Project : " + project.SelectedValue;
            header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br /><br />";
            headJudul.Text = header;
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            Rpt.SubJudul(x
                , "Periode : " + thn1.SelectedValue + " s/d " + thn2.SelectedValue
                );

            Rpt.SubJudul(x
                , "Jenis : " + Rpt.inSql(jenis).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "Lokasi : " + lokasi.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Principal : " + agent.SelectedItem.Text
                );

            Rpt.Header(rpt, x);
        }

        private void Fill()
        {
            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string Agent = "";
            if (agent.SelectedIndex != 0)
            {
                Agent = " AND Principal = '" + Cf.Str(agent.SelectedValue) + "'";
            }

            string Project = " AND MS_KONTRAK.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND MS_KONTRAK.Pers = '" + pers.SelectedValue + "'";

            string carabayar = "";
            System.Text.StringBuilder z = new System.Text.StringBuilder();
            bool isFirst = true;
            for (int i = 0; i < cblcarabayar.Items.Count; i++)
            {
                if (cblcarabayar.Items[i].Selected)
                {
                    if (isFirst)
                    {
                        z.Append("'" + Cf.Str(cblcarabayar.Items[i].Text) + "'");
                        isFirst = false;
                    }
                    else
                        z.Append(",'" + Cf.Str(cblcarabayar.Items[i].Text) + "'");
                }
            }
            if (z.ToString() != "")
                carabayar = " AND MS_KONTRAK.CaraBayar IN (" + z.ToString() + ")";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND NoAgent = " + UserAgent();

            int Tahun1 = Convert.ToInt32(thn1.SelectedValue);
            int Tahun2 = Convert.ToInt32(thn2.SelectedValue);

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0, t10 = 0, t11 = 0, t12 = 0;

            for (int i = Tahun1; i <= Tahun2; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = i.ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                int Unit1 = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE Status = 'A' AND YEAR(TglKontrak) = " + i + Lokasi + carabayar + Project + Perusahaan + aa + " AND MS_KONTRAK.Jenis IN (" + Rpt.inSql(jenis) + ")");
                decimal Net1 = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE NoStock IN (SELECT NoStock FROM MS_KONTRAK WHERE Status = 'A' AND YEAR(TglKontrak) = " + i + Lokasi + carabayar + Project + Perusahaan + aa + " AND MS_KONTRAK.Jenis IN (" + Rpt.inSql(jenis) + "))");
                decimal SGA1 = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE NoStock IN (SELECT NoStock FROM MS_KONTRAK WHERE Status = 'A' AND YEAR(TglKontrak) = " + i + Lokasi + carabayar + Project + Perusahaan + aa + " AND MS_KONTRAK.Jenis IN (" + Rpt.inSql(jenis) + "))");
                decimal Nilai1 = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiKontrak),0) FROM MS_KONTRAK WHERE Status = 'A' AND YEAR(TglKontrak) = " + i + carabayar + Lokasi + Project + Perusahaan + aa + " AND MS_KONTRAK.Jenis IN (" + Rpt.inSql(jenis) + ")");

                c = new TableCell();
                c.Text = Unit1.ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Net1);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(SGA1);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Nilai1);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "&nbsp;";
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                int Unit2 = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE Status = 'B' AND YEAR(TglKontrak) = " + i + Lokasi + carabayar + Project + Perusahaan + aa + " AND MS_KONTRAK.Jenis IN (" + Rpt.inSql(jenis) + ")");
                decimal Net2 = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE NoStock IN (SELECT NoStock FROM MS_KONTRAK WHERE Status = 'B' AND YEAR(TglKontrak) = " + i + Lokasi + carabayar + Project + Perusahaan + aa + " AND MS_KONTRAK.Jenis IN (" + Rpt.inSql(jenis) + "))");
                decimal SGA2 = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE NoStock IN (SELECT NoStock FROM MS_KONTRAK WHERE Status = 'B' AND YEAR(TglKontrak) = " + i + Lokasi + carabayar + Project + Perusahaan + aa + " AND MS_KONTRAK.Jenis IN (" + Rpt.inSql(jenis) + "))");
                decimal Nilai2 = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiKontrak),0) FROM MS_KONTRAK WHERE Status = 'B' AND YEAR(TglKontrak) = " + i + Lokasi + carabayar + Project + Perusahaan + aa + " AND MS_KONTRAK.Jenis IN (" + Rpt.inSql(jenis) + ")");

                c = new TableCell();
                c.Text = Unit2.ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Net2);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(SGA2);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Nilai2);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "&nbsp;";
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                int Unit3 = Unit1 + Unit2;
                decimal Net3 = Net1 + Net2;
                decimal SGA3 = SGA1 + SGA2;
                decimal Nilai3 = Nilai1 + Nilai2;

                c = new TableCell();
                c.Text = Unit3.ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Net3);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(SGA3);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Nilai3);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += Unit1;
                t2 += Net1;
                t3 += SGA1;
                t4 += Nilai1;
                t5 += Unit2;
                t6 += Net2;
                t7 += SGA2;
                t8 += Nilai2;
                t9 += Unit3;
                t10 += Net3;
                t11 += SGA3;
                t12 += Nilai3;

                if (i == Tahun2)
                    SubTotal("TOTAL", t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, decimal t8, decimal t9, decimal t10, decimal t11, decimal t12)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.HorizontalAlign = HorizontalAlign.Center;
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

            rpt.Rows.Add(r);
        }

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
            cblcarabayar.Items.Clear();
            jenis.Items.Clear();
            lokasi.Items.Clear();
            agent.Items.Clear();
            init();
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

        protected void cbcarabayar_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                cblcarabayar.Items[i].Selected = cbcarabayar.Checked;
            }
        }
    }
}
