using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class MasterKomisi : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
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

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND NoAgent = " + UserAgent();

            DataTable rs;
            rs = Db.Rs("SELECT DISTINCT Nama, NoAgent FROM MS_AGENT WHERE Status = 'A'" + aa + " ORDER BY Nama");
            for (int i = 0; i < rs.Rows.Count; i++)
                agent.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString(), rs.Rows[i]["NoAgent"].ToString()));

            Act.ProjectList(project);
            Act.PersList(pers);
            agent.SelectedIndex = 0;
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
                ClientScript.RegisterStartupScript(GetType(), "err"
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
                Rpt.ToExcel(this,headReport, rpt);
            }
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Master Komisi Non Sales";
            string Link = "";
            DateTime TglGenerate = DateTime.Now;
            string FileName = "";
            string FileType = "application/pdf";
            string UserID = Act.UserID;
            string IP = Act.IP;
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            Db.Execute("EXEC spLapPDFDaftar"

                    + " '" + Nama + "'"
                    + ",'" + Link + "'"
                    + ",'" + TglGenerate + "'"
                    + ",'" + IP + "'"
                    + ",'" + UserID + "'"
                    + ",'" + FileName + "'"
                    + ",'" + FileType + "'"
                    + ",'"+Dari+"'"
                    + ",'"+Sampai+"'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "MasterKomisiNonSales" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "MasterKomisiNonSales" + rs.Rows[0]["AttachmentID"] + ".pdf";

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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFMasterKomisiNonSales.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&status_s=" + nStatusS
                + "&status_a=" + nStatusA
                + "&status_b=" + nStatusB
                + "&agent=" + Sales
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

            //Rpt.Header(rpt, x);
            string legend = "Status : A = Aktif / B = Batal.<br />";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            string Status = "";
            if (statusA.Checked) Status = " AND A.Status = 'A'";
            if (statusB.Checked) Status = " AND A.Status = 'B'";

            string tgl = "";
            string order = "";
            if (tglkontrak.Checked)
            {
                tgl = "A.TglKontrak";
                order = ",A.NoKontrak";
            }

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string Agent = "", Agent2 = "";
            if (agent.SelectedIndex != 0)
            {
                Agent = " AND B.NoAgent = '" + agent.SelectedValue + "'";
                Agent2 = " AND A.NoAgent = '" + agent.SelectedValue + "'";
            }
            else
            {
                if (UserAgent() > 0)
                    Agent = " AND B.NoAgent = " + UserAgent();
            }

            string Project = " AND A.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND A.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND A.Pers = '" + pers.SelectedValue + "'";

            int index = 1;

            int no = 1;

            string sql = "SELECT DISTINCT (NoAgent) from MS_KONTRAK A"
                + " where (select ISNULL(count(*),0) from MS_KOMISI where NoKontrak = A.NoKontrak) > 0" + Agent2 + Project + Perusahaan;
            DataTable sr = Db.Rs(sql);
            decimal t1 = 0, t2 = 0, t3 = 0;
            for (int g = 0; g < sr.Rows.Count; g++)
            {
                if (!Response.IsClientConnected) break;

                string strSql = "SELECT "
                    + "A.NoKontrak"
                    + ",A.TglKontrak"
                    + ",A.NilaiDPP"
                    + ",A.NoUnit"
                    + ",A.NilaiKontrak"
                    + ",B.Nama AS Ag"
                    + ",B.Principal"
                    + ",B.NPWP"
                    + ",B.Rekening"
                    + ",A.Status"
                    + ",A.PersenLunas"
                    + ",C.Nama as Customer"
                    + ",A.NoAgent"
                    + ",A.NoStock"
                    + " FROM MS_KONTRAK A INNER JOIN MS_AGENT B ON A.NoAgent = B.NoAgent"
                    + " INNER JOIN MS_CUSTOMER C ON A.NoCustomer = C.NoCustomer"
                    + " WHERE A.NoAgent= '" + sr.Rows[g]["NoAgent"] + "'"
                    + " AND A.FlagKomisi = '1'"
                    + Status
                    + " AND CONVERT(varchar,A.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar,A.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + Agent
                    + Project
                    + Perusahaan
                    + " ORDER BY B.Nama"
                    + order;



                DataTable rs = Db.Rs(strSql);
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    TableRow r = new TableRow();
                    TableCell c;

                    r.VerticalAlign = VerticalAlign.Top;
                    r.Attributes["ondblclick"] = "popJadwalKomisi('" + rs.Rows[i]["NoKontrak"] + "')";

                    //nambah no default
                    c = new TableCell();
                    c.Text = (no).ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);


                    c = new TableCell();
                    c.Text = rs.Rows[i]["Status"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoKontrak"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Customer"].ToString();
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
                    c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]));
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);


                    decimal OVC = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiKomisi),0) FROM MS_KOMISI WHERE NoKontrak='" + rs.Rows[i]["NoKontrak"] + "' AND Tipe='OVC'");
                    decimal OVK = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiKomisi),0) FROM MS_KOMISI WHERE NoKontrak='" + rs.Rows[i]["NoKontrak"] + "' AND Tipe='OVK'");
                    decimal OVKP = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiKomisi),0) FROM MS_KOMISI WHERE NoKontrak='" + rs.Rows[i]["NoKontrak"] + "' AND Tipe='OVKP'");

                    c = new TableCell();
                    c.Text = Cf.Num(OVC);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(OVK);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);


                    c = new TableCell();
                    c.Text = Cf.Num(OVKP);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    rpt.Rows.Add(r);
                    no++;

                    t1 += OVC;
                    t2 += OVK;
                    t3 += OVKP;

                }

            }
            SubTotal("GRAND TOTAL", t1, t2, t3);
        }


        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 8;
            c.HorizontalAlign = HorizontalAlign.Right;
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
            c.Text = Cf.Num(t2);
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
    }
}
