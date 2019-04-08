using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class ClosingNUP : System.Web.UI.Page
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
            string strSql = "SELECT D.NoSkema,A.NoAgent,A.NoNup,D.NoStock,D.NoUnit,A.Status,D.TglInput,D.NoSkema,B.Nama AS [NamaCs],C.Nama AS [NamaSales] FROM [NUP02]..CustomerNUP A"
                            + " INNER JOIN [NUP02]..CustomerNUPDetail D ON A.NoNUP = D.NoNUP"
                            + " INNER JOIN [NUP02]..CustomerData B ON A.NoCustomer = B.NoCustomer"
                            + " INNER JOIN [NUP02]..AgentData C ON B.NoAgent = C.NoAgent"
                            + " ORDER BY A.NoNUP";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ada data.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                if (rs.Rows[i]["Status"].ToString() == "A" && rs.Rows[i]["NoSkema"].ToString() != "0")
                    c.Text = "<a href='CustomerDaftar.aspx?NoSkema=" + rs.Rows[i]["NoSkema"].ToString() + "&NoAgent=" + rs.Rows[i]["NoAgent"].ToString() + "&NUP=1&NoStock=" + rs.Rows[i]["NoStock"].ToString() + "&NoUrut=" + rs.Rows[i]["NoNUP"].ToString() + "' style='text-decoration:none;color:blue;'>Next</a>";
                else if (rs.Rows[i]["Status"].ToString() == "A" && rs.Rows[i]["NoSkema"].ToString() == "0")
                    c.Text = "CARA BAYAR BELUM DI PILIH";
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
