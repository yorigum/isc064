using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class NUPDaftarBayar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            //Js.ConfirmKeyword(this, keyword);

            if (!Page.IsPostBack)
            {
                Fill();
            }
        }

        private void Fill()
        {
            string strSql = "SELECT A.*, B.Nama AS NamaCS, B.NoKTP, B.NoHP, B.NoTelp, B.RekBank, B.RekNo, C.Nama AS NamaAG"
                + " FROM MS_NUP A "
                + " INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer"
                + " INNER JOIN MS_AGENT C ON A.NoAgent = C.NoAgent"
                + " WHERE B.NoKTP != '' AND B.NoHP != '' AND B.NoTelp != '' AND B.RekBank != '' AND B.RekNo != ''"
                + " AND NoNUP NOT IN(SELECT NoNUP FROM MS_NUP_PELUNASAN)"
                + " AND A.NoNUP+B.Nama LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " ORDER BY NoNUP";

            string tampilNUP = "";
            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak terdapat NUP dengan kriteria seperti tersebut diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                tampilNUP = rs.Rows[i]["NoNUP"].ToString();
                int intrevisi = Convert.ToInt32(rs.Rows[i]["Revisi"]);
                if (intrevisi > 0)
                    tampilNUP = tampilNUP + "R";

                c = new TableCell();
                c.Text = tampilNUP;
                r.Cells.Add(c);

                string namaC = "";
                if (Convert.ToInt32(rs.Rows[i]["NoCustomer"]) != 0)
                {
                    namaC = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + Convert.ToInt32(rs.Rows[i]["NoCustomer"]));
                }

                c = new TableCell();
                c.Text = namaC;
                r.Cells.Add(c);

                string namaA = "";
                if (Convert.ToInt32(rs.Rows[i]["NoAgent"]) != 0)
                {
                    namaA = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = " + Convert.ToInt32(rs.Rows[i]["NoAgent"]));
                }

                c = new TableCell();
                c.Text = namaA;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a href=\"NUPDaftarBayar2.aspx?No=" + rs.Rows[i]["NoNUP"].ToString() +"\">Next..</a>";
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
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
        protected void display_Click(object sender, EventArgs e)
        {
            Fill();
        }
}
}
