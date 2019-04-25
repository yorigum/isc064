using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class Closing : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {

        }

        protected void Fill()
        {
            string strSql = "SELECT B.NoAgent,A.Status,A.NoStock, A.NoReservasi,A.TglInput,B.Nama AS [NamaCs],C.Nama AS [NamaSales],A.NoUnit,A.NoSkema,A.Harga FROM [NUP03]..CustomerReservasi A"
                            + " INNER JOIN [NUP03]..CustomerData B ON A.NoCustomer = B.NoCustomer"
                            + " INNER JOIN [NUP03]..AgentData C ON B.NoAgent = C.NoAgent"
                            + " ORDER BY A.NoReservasi";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ada data.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                if (rs.Rows[i]["Status"].ToString() == "A")
                    c.Text = "<a href='CustomerDaftar.aspx?NoSkema=" + rs.Rows[i]["NoSkema"].ToString() + "&NoAgent=" + rs.Rows[i]["NoAgent"].ToString() + "&NUP=0&NoStock=" + rs.Rows[i]["NoStock"].ToString() + "&NoUrut=" + rs.Rows[i]["NoReservasi"].ToString() + "' style='text-decoration:none;color:blue;'>Next</a>";
                else if (rs.Rows[i]["Status"].ToString() == "E")
                    c.Text = "EXPIRED";
                else if (rs.Rows[i]["Status"].ToString() == "C")
                    c.Text = "CLOSING";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglInput"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaCS"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaSales"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                r.Cells.Add(c);

                string NamaSkema = "";
                NamaSkema = Db.SingleString("SELECT Nama FROM ISC064_MARKETINGJUAL..REF_SKEMA WHERE Nomor='" + rs.Rows[i]["NoSkema"].ToString() + "'");
                c = new TableCell();
                c.Text = NamaSkema;
                r.Cells.Add(c);


                c = new TableCell();
                c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglInput"]).AddDays(3));
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            Fill();
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
