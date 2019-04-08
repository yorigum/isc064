using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class MasterKomisi : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Agent { get { return (Request.QueryString["agent"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");

            return a;
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

            if (StatusA != "")
                Rpt.SubJudul(x, "Status : " + StatusA);
            else if (StatusB != "")
                Rpt.SubJudul(x, "Status : " + StatusB);
            else
                Rpt.SubJudul(x, "Status : " + StatusS);

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            Rpt.SubJudul(x
                , "Tanggal Kontrak" + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            //Rpt.Header(rpt, x);
            string legend = "Status : A = Aktif / B = Batal.<br />";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            string Status = "";
            if (StatusA != "") Status = " AND A.Status = 'A'";
            if (StatusB != "") Status = " AND A.Status = 'B'";

            string tgl = "";
            string order = "";
            tgl = "A.TglKontrak";
            order = ",A.NoKontrak";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            string nAgent = "", nAgent2 = "";
            if (Agent != "SEMUA")
            {
                nAgent = " AND B.NoAgent = '" + Agent + "'";
                nAgent2 = " AND A.NoAgent = '" + Agent + "'";
            }
            else
            {
                if (UserAgent() > 0)
                    nAgent = " AND B.NoAgent = " + UserAgent();
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND A.Project IN ('" + Project.Replace(",", "','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND A.Pers = '" + Perusahaan + "'";

            int index = 1;

            int no = 1;

            string sql = "SELECT DISTINCT (NoAgent) from MS_KONTRAK A"
                + " where (select ISNULL(count(*),0) from MS_KOMISI where NoKontrak = A.NoKontrak) > 0" + nAgent2 + nProject + nPerusahaan;
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
                    + nAgent
                    + nProject
                    + nPerusahaan
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
