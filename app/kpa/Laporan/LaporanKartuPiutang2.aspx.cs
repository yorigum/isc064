using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA.Laporan
{
    /// <summary>
    /// Summary description for LaporanKartuPiutang2.
    /// </summary>
    public partial class LaporanKartuPiutang2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack) Act.ProjectList(project);
            // Put user code to initialize the page here
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            rpt.Visible = true;
            string kunci = "";
            if (key.Text != "")
            {
                kunci = " AND (ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak LIKE '%" + key.Text + "%' or ISC064_MARKETINGJUAL..MS_KONTRAK.NoUnit LIKE '%" + key.Text + "%' or ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama LIKE '%" + key.Text + "%') ";
            }

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.NoAgent = " + UserAgent();

            string strSql = "SELECT"
                + " ISC064_MARKETINGJUAL..MS_KONTRAK.*"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama AS Cs"
                + ",ISC064_MARKETINGJUAL..MS_AGENT.Nama + ' ' + ISC064_MARKETINGJUAL..MS_AGENT.Principal AS Ag"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoCustomer = ISC064_MARKETINGJUAL..MS_CUSTOMER.NoCustomer "
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_AGENT ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoAgent = ISC064_MARKETINGJUAL..MS_AGENT.NoAgent"
                + " WHERE 1=1"
                + " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Project = '" + project.SelectedValue + "'"
				+ " AND ISC064_MARKETINGJUAL..MS_KONTRAK.CaraBayar = 'KPR' "
                + kunci
                + aa
                + " ORDER BY NoKontrak";
            
            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak terdapat kontrak dengan kriteria seperti tersebut diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;
                Button b;

                c = new TableCell();
                c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoKontrak"].ToString() + "')\">"
                    + rs.Rows[i]["NoKontrak"].ToString() + "</a><br>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString() + "<br>"
                    + rs.Rows[i]["Ag"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a href=\"KartuPiutangKPA.aspx?NoKontrak=" + rs.Rows[i]["NoKontrak"].ToString() + "\">"
                    + "Screen Preview" + "</a><br>"
                    + "<a href=\"KartuPiutangKPA.aspx?NoKontrak=" + rs.Rows[i]["NoKontrak"].ToString() + "&userid=" + Act.UserID + "&pdf=1\">"
                    + "Export PDF" + "</a><br>"
                    + "<a href=\"KartuPiutangKPA.aspx?NoKontrak=" + rs.Rows[i]["NoKontrak"].ToString() + "&Excel=1\">Export to Excel</a>";
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
    }
}
