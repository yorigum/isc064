using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL.Laporan
{
    public partial class PriceList : System.Web.UI.Page
    {
        private string NoCustomer { get { return (Request.QueryString["NoCustomer"]); } }
        private string Principal { get { return (Request.QueryString["principal"]); } }
        private string Input { get { return Request.QueryString["input"]; } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Jenis { get { return (Request.QueryString["jenis"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);


            string nJenis = String.Empty;
            nJenis = Jenis.Replace("-", ",");
            nJenis = nJenis.Replace("%", " ").TrimEnd(',');


            x.Append(
                "<br/>Jenis : " + nJenis
                );

            x.Append(
                "<br/>Lokasi : " + Lokasi
                );

            x.Append(
                "<br/>Project : " + Project
                );

            lblHeader.Text = Mi.Pt
                + "<br />"
                + "PER " + Cf.Day(DateTime.Today)
                + x
                ;

            string legend = "";

            Rpt.HeaderReport(headReport, legend, x);

            Fill();
        }

        private void Fill()
        {
            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND Lokasi = '" + Cf.Str(Lokasi) + "'";
            }
            string nProject = "";
            if (Project != "SEMUA") nProject = " AND Project IN ('" + Project.Replace(",", "','") + "')";
            //change parameter jenis
            string akt = String.Empty;
            akt = Jenis.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace(",", "','");
            akt = akt.Replace("%", " ");

            akt = "'" + akt + "'";

            DataTable aa = Db.Rs("SELECT DISTINCT(Lokasi) FROM MS_UNIT WHERE 1=1" + nLokasi + nProject);
            for (int j = 0; j < aa.Rows.Count; j++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = aa.Rows[j]["Lokasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                string strSql = "SELECT DISTINCT(JENIS)"
                    + " FROM MS_UNIT"
                    + " WHERE Lokasi = '" + aa.Rows[j]["Lokasi"] + "'"
                    + " AND Jenis IN (" + akt + ")"
                    ;
                DataTable rs = Db.Rs(strSql);

                decimal TotalAvailable = 0, LuasAvailable = 0;
                decimal TotalSold = 0, LuasSold = 0;
                decimal TotalHold = 0, LuasHold = 0;
                decimal Total = 0, LuasTotal = 0;
                decimal TotalTitip = 0;//, LuasTitip = 0;
                decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0, t10 = 0;

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected)
                        break;

                    if (i > 0)
                    {
                        r = new TableRow();

                        c = new TableCell();
                        c.Text = "";
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);
                    }

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Jenis"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    TotalAvailable = Nilai(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "AVAILABLE", Project);
                    c.Text = Cf.Num(TotalAvailable);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    LuasAvailable = Math.Round(Persen(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "AVAILABLE", Project), 2);
                    c.Text = Cf.Num(Math.Round(LuasAvailable, 2)) + " %";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    TotalSold = Nilai(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "SOLD", Project);
                    c.Text = Cf.Num(TotalSold);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    LuasSold = Math.Round(Persen(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "SOLD", Project), 2);
                    c.Text = Cf.Num(LuasSold) + " %";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    TotalHold = Nilai(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "HOLD", Project);
                    c.Text = Cf.Num(TotalHold);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    LuasHold = Math.Round(Persen(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "HOLD", Project), 2);
                    c.Text = Cf.Num(LuasHold) + " %";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    Total = Nilai(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "TOTAL", Project);
                    c.Text = Cf.Num(Total);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    LuasTotal = Math.Round(Persen(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "TOTAL", Project), 2);
                    c.Text = Cf.Num(LuasTotal) + " %";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    t5 += Total;
                    t1 += TotalSold;
                    t2 = t1 / t5 * 100;
                    t3 += TotalAvailable;
                    t4 = t3 / t5 * 100;
                    t7 += TotalHold;
                    t8 = t7 / t5 * 100;
                    t9 += TotalTitip;
                    t10 = t9 / t5 * 100;
                    t6 = t2 + t4 + t8 + t10;

                    rpt.Rows.Add(r);

                    if (i == (rs.Rows.Count - 1))
                        SubTotal(t1, t2, t3, t4, t7, t8, t5, t6, t9, t10);
                }
            }
        }

        protected decimal Nilai(string Jenis, string Lokasi, string Tipe, string Project)
        {
            decimal t = 0;
            string nProject = "";
            if (Project != "SEMUA") nProject = " AND Project IN ('" + Project.Replace(",", "','") + "')";
            if (Tipe == "AVAILABLE")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'A'"
                    );
            }
            else if (Tipe == "SOLD")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'B'"
                    + " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoUnit = MS_UNIT.NoUnit AND Status = 'A') > 0"
                    );
            }
            else if (Tipe == "HOLD")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'B'"
                    + " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoUnit = MS_UNIT.NoUnit AND Status = 'A') = 0"
                    );
            }
            else if (Tipe == "TOTAL")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    );
            }
            return t;
        }

        protected decimal Persen(string Jenis, string Lokasi, string Tipe, string Project)
        {
            decimal t = 0;
            string nProject = "";
            if (Project != "SEMUA") nProject = " AND Project IN ('" + Project.Replace(",","','") + "')";

            decimal tot = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Lokasi = '" + Lokasi + "'"
                    + nProject
                    );

            decimal tot2 = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Lokasi = '" + Lokasi + "'"
                    + " AND Jenis = '" + Jenis + "'"
                    + nProject
                    );


            if (Tipe == "AVAILABLE")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'A'"
                    ) / tot * 100;
            }
            else if (Tipe == "SOLD")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'B'"
                    + " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoUnit = MS_UNIT.NoUnit AND Status = 'A') > 0"
                    ) / tot
                    * 100;
            }
            else if (Tipe == "HOLD")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'B'"
                    + " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoUnit = MS_UNIT.NoUnit AND Status = 'A') = 0"
                    ) / tot
                    * 100;
            }
            else if (Tipe == "TOTAL")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    ) / tot2 * 100;
            }
            return t;
        }

        private void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t7, decimal t8, decimal t5, decimal t6, decimal t9, decimal t10)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "TOTAL";
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Left;
            c.VerticalAlign = VerticalAlign.Top;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t3)
                //+ "<br />"
                //+ Cf.Num(t3 / t5 * 100) + "%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t4, 2)) + " %"
                //+ "<br />"
                //+ Cf.Num(t4 / t6 * 100) + "%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1)
                //+ "<br />"
                //+ Cf.Num(t1 / t5 * 100) + "%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t2, 2)) + " %"
                //+ "<br />"
                //+ Cf.Num(t2 / t6 * 100) + "%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t7)
                //+ "<br />"
                //+ Cf.Num(t7 / t5 * 100) + "%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t8, 2)) + " %"
                //+ "<br />"
                //+ Cf.Num(t8 / t6 * 100) + "%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t5)
                //+ "<br />100%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t6)) + " %"
                //+ "<br />100%"
                ;
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
