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


namespace ISC064.ADMINJUAL.Laporan
{
    public partial class MasterKomisi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                init();
            }
        }

        private void init()
        {
            DataTable rs;

            rs = Db.Rs("SELECT DISTINCT YEAR(TglKontrak), MONTH(TglKontrak) FROM MS_KOMISI INNER JOIN MS_KONTRAK ON MS_KOMISI.NoKontrak=MS_KONTRAK.NoKontrak"
                + " ORDER BY YEAR(TglKontrak), MONTH(TglKontrak)");
            for (int i = 0; i < rs.Rows.Count; i++)
                periodekomisi.Items.Add(new ListItem(
                    Cf.Monthname((int)rs.Rows[i][1]) + " " + rs.Rows[i][0].ToString()
                    , rs.Rows[i][0] + "," + rs.Rows[i][1]
                    ));
            periodekomisi.SelectedIndex = 0;
        }

        protected void display_Click(object sender, EventArgs e)
        {
            rpt.Visible = true;
            string kunci = "";
            if (key.Text != "")
            {
                kunci = " AND ISC064_MARKETINGJUAL..MS_KOMISI.NoAgent LIKE '%" + key.Text + "%'or ISC064_MARKETINGJUAL..MS_AGENT.Nama LIKE '%" + key.Text + "%' ";
            }

            string PeriodeKomisi = "";
            if (periodekomisi.SelectedIndex != 0)
            {
                string[] z = periodekomisi.SelectedValue.Split(',');
                PeriodeKomisi = " AND YEAR(TglKontrak) = " + z[0]
                    + " AND MONTH(TglKontrak) = " + z[1];
            }
            string strSql = "SELECT"
                + " a.NoAgent"
                + ",a.NamaKomisi AS Ag"
                + ",a.NoAgent"
                + " FROM MS_KOMISI a"
                + " INNER JOIN MS_KONTRAK c ON a.NoKontrak = c.NoKontrak"
                + " INNER JOIN MS_AGENT b ON c.NoAgent = b.NoAgent"
                + " WHERE 1=1"
                + kunci
                + PeriodeKomisi
                + " ORDER BY a.NamaKomisi";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak terdapat agent dengan kriteria seperti tersebut diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoAgent"].ToString() + "')\">"
                    + rs.Rows[i]["Ag"].ToString() + "</a><br>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a href=\"LaporanMasterKomisi.aspx?NoAgent="+rs.Rows[i]["NoAgent"].ToString()+"&PeriodeKomisi="+periodekomisi.SelectedItem+"\">"
                    + "Screen Preview" + "</a><br>"
                    + "<a href=\"LaporanMasterKomisi.aspx?NoAgent=" + rs.Rows[i]["NoAgent"].ToString()+"&PeriodeKomisi="+periodekomisi.SelectedItem+"&Excel=1\">Export to Excel</a>";
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
