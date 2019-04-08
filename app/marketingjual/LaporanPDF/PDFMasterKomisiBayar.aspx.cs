using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class MasterKomisiBayar : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
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

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND A.Project IN ('" + Project.Replace(",", "','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND A.Pers = '" + Perusahaan + "'";

            int index = 1;

            int no = 1;

            string sql = "SELECT DISTINCT (NoAgent) from MS_KONTRAK A"
                + " where (select ISNULL(count(*),0) from MS_KOMISI where NoKontrak = A.NoKontrak) > 0" + nProject + nPerusahaan;
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
                    + nProject
                    + nPerusahaan
                    + " AND CONVERT(varchar,A.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar,A.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " AND NoKontrak IN (SELECT NoKontrak FROM MS_KOMISI WHERE SudahBayar=1 AND Realisasi=0)"
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
