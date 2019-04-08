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

            Header();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();


            string[] words = Jenis.Split('-');
            bool x1 = true;
            string nString = "";

            foreach (string akt in words)
            {
                nString = akt.ToString();
            }

            Rpt.Judul(x, comp, judul);

            if (StatusA != "")
                Rpt.SubJudul(x, "Status : " + StatusA);
            else if (StatusB != "")
                Rpt.SubJudul(x, "Status : " + StatusB);
            else
                Rpt.SubJudul(x, "Status : " + StatusS);

            string nJenis = String.Empty;
            nJenis = Jenis.Replace("-", ",");
            nJenis = nJenis.Replace("%", " ").TrimEnd(',');


            Rpt.SubJudul(x
                , "Periode : " + Input
                );

            Rpt.SubJudul(x
                , "Jenis : " + nJenis
                );

            Rpt.SubJudul(x
                , "Lokasi : " + Lokasi
                );

            Rpt.SubJudul(x
                , "Project : " + Project
                );

            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {

            string nStatus = "";
            if (StatusA != "") nStatus = " AND Status = 'A'";
            if (StatusB != "") nStatus = " AND Status = 'B'";

            string nInput = "";
            if (Input != "SEMUA")
            {
                string[] z = Input.Split('-');
                nInput = " AND YEAR(TglInput) = " + z[0]
                    + " AND MONTH(TglInput) = " + z[1];
            }

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND MS_UNIT.Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string nProject = "";
            if (Project != "SEMUA")
            {
                nProject = " AND Project  IN ('" + Project.Replace(",", "','") + "')";
            }

            //change parameter jenis
            string akt = String.Empty;
            akt = Jenis.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace(",", "','");
            akt = akt.Replace("%", " ");            

            akt = "'" + akt + "'";

            decimal t1 = 0, t2 = 0;

            string strSql = "SELECT "
                + "	NoStock"
                + ",Jenis"
                + ",Lokasi"
                + ",NoUnit"
                + ",Luas"
                + ",PriceList"
                + ",PriceListMin"
                + ",TglInput"
                + ",Status"
                + " FROM MS_UNIT"
                + " WHERE Jenis IN (" + akt + ")"
                //  + " WHERE"
                + nProject
                + nStatus
                + nLokasi
                + nInput
                + " ORDER BY NoStock";

            DataTable rs = Db.Rs(strSql);            
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditUnit('" + rs.Rows[i]["NoStock"] + "')";

                DateTime p = Convert.ToDateTime(rs.Rows[i]["TglInput"]);

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT NoKontrak FROM MS_KONTRAK WHERE NoStock = '" + rs.Rows[i]["NoStock"].ToString() + "' AND Status = 'A'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglInput"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Lokasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["PriceListMin"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["PriceList"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                string status = rs.Rows[i]["Status"].ToString();
                if (Convert.ToChar(status) == 'A')
                {
                    c.Text = "AVAILABLE";
                }
                else
                {
                    c.Text = "SOLD";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += (decimal)rs.Rows[i]["PriceList"];
                t2 += (decimal)rs.Rows[i]["PriceListMin"];

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", t1, t2);
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 6;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "";
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
