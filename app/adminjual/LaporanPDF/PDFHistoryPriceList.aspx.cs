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
            TableHeaderCell c = new TableHeaderCell();

            DataTable rs = Db.Rs("SELECT DISTINCT(Periode) FROM MS_PRICELIST_HISTORY");
            int j = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                c = new TableHeaderCell();
                rpt.Rows[0].Cells.Add(c);
                rpt.Rows[0].Cells[3 + j].Text = "Periode";

                c = new TableHeaderCell();
                rpt.Rows[0].Cells.Add(c);
                rpt.Rows[0].Cells[4 + j].Text = "PriceList";

                j += 2;
            }
            //rpt.Rows[1].Cells[14].Text = "";

            c = new TableHeaderCell();
            rpt.Rows[0].Cells.Add(c);
            rpt.Rows[0].Cells[j + 3].Text = "No Kontrak";

            c = new TableHeaderCell();
            rpt.Rows[0].Cells.Add(c);
            rpt.Rows[0].Cells[j + 4].Text = "Tgl Kontrak";

            c = new TableHeaderCell();
            rpt.Rows[0].Cells.Add(c);
            rpt.Rows[0].Cells[j + 5].Text = "Customer";

            c = new TableHeaderCell();
            rpt.Rows[0].Cells.Add(c);
            rpt.Rows[0].Cells[j + 6].Text = "Nilai PriceList";


            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            string[] str = Input.Split('-');

            //Rpt.SubJudul(x
            //    , "Periode : " +Cf.NamaBln(str[1]) + " " + str[0] 
            //    );


            string legend = "Status: A = Aktif / B = Blokir."
                            + "<br />"
                            + "Luas dalam meter persegi.Harga price list adalah dalam rupiah.";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {

            string nStatus = "";
            if (StatusA != "") nStatus = " AND Status = 'A'";
            if (StatusB != "") nStatus = " AND Status = 'B'";

            string nProject = "";
            if (Project != "SEMUA")
            {
                nProject = " AND b.Project IN ('" + Project.Replace(",","','") + "')";
            }

            string nInput = "";
            if (Input != "SEMUA")
            {
                string[] z = Input.Split('-');
                nInput = " AND YEAR(a.Periode) = '" + z[0] + "'"
                    + " AND MONTH(a.Periode) = '" + z[1] + "'";
            }

            //string nLokasi = "";
            //if(Lokasi != "SEMUA")
            //{
            //    nLokasi = " AND MS_UNIT.Lokasi = '" + Cf.Str(Lokasi) + "'";
            //}


            //change parameter jenis
            string akt = String.Empty;
            akt = Jenis.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace(",", "','");
            akt = akt.Replace("%", " ");

            akt = "'" + akt + "'";

            decimal t1 = 0, t2 = 0;

            Response.Write(nInput);

            string strSql = "SELECT a.NoStock"
                + " FROM MS_PRICELIST_HISTORY a"
                + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                + " WHERE b.Jenis IN (" + akt + ")"
                + nProject
                + nStatus
                + nInput
                + " GROUP BY a.NoStock"
                + " ORDER BY a.NoStock";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string strSql2 = "SELECT a.*, b.NoUnit"
                    + " FROM MS_PRICELIST_HISTORY a"
                    + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                    + " WHERE a.NoStock = '" + rs.Rows[i]["NoStock"] + "'"
                    + " ORDER BY a.NoStock";
                DataTable rs2 = Db.Rs(strSql2);
                if (rs2.Rows.Count > 0)
                {
                    TableRow r = new TableRow();
                    TableCell c;

                    r.VerticalAlign = VerticalAlign.Top;
                    r.Attributes["ondblclick"] = "popEditUnit('" + rs2.Rows[0]["NoStock"] + "')";

                    DateTime p = Convert.ToDateTime(rs2.Rows[0]["Periode"]);

                    c = new TableCell();
                    c.Text = (i + 1).ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs2.Rows[0]["NoStock"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs2.Rows[0]["NoUnit"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    DataTable aa = Db.Rs("SELECT DISTINCT(Periode) FROM MS_PRICELIST_HISTORY ORDER BY Periode");
                    for (int j = 0; j < aa.Rows.Count; j++)
                    {
                        string nilai = "";
                        DataTable bb = Db.Rs("SELECT TOP 1 PriceList FROM MS_PRICELIST_HISTORY WHERE NoStock = '" + rs2.Rows[0]["NoStock"] + "' AND Periode = '" + aa.Rows[j]["Periode"] + "' ORDER BY No DESC");
                        if (bb.Rows.Count > 0)
                            nilai = Cf.Num(bb.Rows[0]["PriceList"]);

                        c = new TableCell();
                        c.Text = nilai != "" ? Cf.Day(aa.Rows[j]["Periode"]) : "";
                        c.HorizontalAlign = HorizontalAlign.Right;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = nilai;
                        c.HorizontalAlign = HorizontalAlign.Right;
                        r.Cells.Add(c);
                    }

                    string strSql3 = "SELECT b.*, c.Nama"
                    + " FROM MS_KONTRAK b"
                    + " INNER JOIN MS_CUSTOMER c ON b.NoCustomer = c.NoCustomer"
                    + " WHERE b.NoStock = '" + rs.Rows[i]["NoStock"] + "'"
                    + " ORDER BY b.NoStock";
                    DataTable rs3 = Db.Rs(strSql3);
                    if (rs3.Rows.Count > 0)
                    //for (int k = 0; k < rs3.Rows.Count; k++)
                    {
                        c = new TableCell();
                        c.Text = rs3.Rows[0]["NoKontrak"].ToString();
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = Cf.Day(rs3.Rows[0]["TglKontrak"]);
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = rs3.Rows[0]["Nama"].ToString();
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = Cf.Num(rs3.Rows[0]["Gross"]);
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                    }
                    else
                    {
                        c = new TableCell();
                        c.Text = "";
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = "";
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = "";
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = "";
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);
                    }

                    rpt.Rows.Add(r);

                    t1 += (decimal)rs2.Rows[0]["PriceList"];
                }

                //if(i==rs.Rows.Count-1)
                //    SubTotal("GRAND TOTAL", t1);
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
