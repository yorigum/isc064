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


namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class MasterKomisiBayar : System.Web.UI.Page
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

            //if(!Cf.isPilih(tipe))
            //{
            //    x = false;
            //    tipec.Text = " Pilih Minimum Satu";
            //}
            //else
            //    tipec.Text = "";

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

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Master Komisi Per - Kontrak";
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

            string nfilename = "MasterKomisiDetail" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "MasterKomisiDetail" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFMasterKomisiBayar.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&status_s=" + nStatusS
                + "&status_b=" + nStatusB
                + "&status_a=" + nStatusA
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
            //if(tglbolehbayar.Checked) tgl = tglbolehbayar.Text;
            //if(tglbayar.Checked) tgl = tglbayar.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(x
                , "Sales : " + agent.SelectedItem.Text
                );

            //Rpt.SubJudul(x
            //    , "Tipe : " + Rpt.inSql(tipe).Replace("'","")
            //    );

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
            decimal t1 = 0, t2 = 0;
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
                    + " AND NoKontrak IN (SELECT NoKontrak FROM MS_KOMISI WHERE SudahBayar=1 AND Realisasi=0)"
                    //+ Agent
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
                    TableCell c2;
                    TableRow r2;
                    TableRow r2a;
                    TableHeaderCell th2;
                    Table tb;

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
                    c.Text = rs.Rows[i]["Ag"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NPWP"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Rekening"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]));
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    //komisi marketing
                    c = new TableCell();
                    c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]));
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    tb = new Table();
                    tb.CssClass = "datatb";
                    tb.Style.Add("width", "100%");
                    tb.BorderColor = Color.Black;
                    c.Controls.Add(tb);

                    r2 = new TableRow();

                    th2 = new TableHeaderCell();
                    th2.BackColor = Color.Gray;
                    th2.ForeColor = Color.White;
                    th2.Text = "Jenis";
                    r2.Cells.Add(th2);

                    th2 = new TableHeaderCell();
                    th2.Text = "Penerima";
                    th2.BackColor = Color.Gray;
                    th2.ForeColor = Color.White;
                    r2.Cells.Add(th2);

                    th2 = new TableHeaderCell();
                    th2.Text = "Nilai";
                    th2.BackColor = Color.Gray;
                    th2.ForeColor = Color.White;
                    r2.Cells.Add(th2);

                    tb.Rows.Add(r2);

                    DataTable kom = Db.Rs("SELECT * FROM MS_KOMISI WHERE NoKontrak='" + rs.Rows[i]["NoKontrak"] + "' AND Tipe !='OVN' AND SudahBayar=1 AND Realisasi=0");
                    decimal total = 0;
                    for (int a = 0; a < kom.Rows.Count; a++)
                    {
                        r2a = new TableRow();

                        c2 = new TableCell();
                        c2.Text = kom.Rows[a]["NamaKomisi"].ToString();
                        c2.HorizontalAlign = HorizontalAlign.Left;
                        r2a.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = kom.Rows[a]["NamaPenerima"].ToString();
                        c2.HorizontalAlign = HorizontalAlign.Left;
                        r2a.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Num(Convert.ToDecimal(kom.Rows[a]["NilaiKomisi"]));
                        c2.HorizontalAlign = HorizontalAlign.Right;
                        r2a.Cells.Add(c2);

                        total += Convert.ToDecimal(kom.Rows[a]["NilaiKomisi"]);

                        tb.Rows.Add(r2a);
                    }

                    r2a = new TableRow();

                    c2 = Rpt.Foot();
                    c2.Text = "Total";
                    c2.ColumnSpan = 2;
                    c2.HorizontalAlign = HorizontalAlign.Left;
                    r2a.Cells.Add(c2);

                    c2 = Rpt.Foot();
                    c2.Text = Cf.Num(total);
                    c2.HorizontalAlign = HorizontalAlign.Right;
                    r2a.Cells.Add(c2);

                    tb.Rows.Add(r2a);

                    rpt.Rows.Add(r);
                    no++;

                    t1 += total;

                }

            }
            SubTotal("GRAND TOTAL", t1);
        }


        private void SubTotal(string txt, decimal t1)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 11;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        protected string TipeReff(string NoKontrak, string RefEm, string RefCust)
        {
            string Tipe = "";
            if (RefEm != "" && RefCust == "")
            {
                Tipe = "EMPLOYEE";
            }
            else if (RefEm == "" && RefCust != "")
            {
                Tipe = "BUYER";
            }

            return Tipe;
        }

        protected string NamaReff(string RefEm, string RefCust)
        {
            string Nama = "";
            if (RefEm != "" && RefCust == "")
            {
                Nama = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + RefEm + "'");
            }
            else if (RefEm == "" && RefCust != "")
            {
                Nama = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = '" + RefCust + "'");
            }

            return Nama;
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
