using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
{
    public partial class DaftarReward : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Act.Pass();
            //Act.NoCache();

            Js.ConfirmKeyword(this, keyword);
        }

        protected void search_Click(object sender, System.EventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            string strSql = "SELECT *"
                + " FROM MS_KOMISI_Reward"
                + " WHERE NoReward + NamaAgent + NamaSkema + Reward "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND Project IN (" + Act.ProjectListSql + ")"
                + " ORDER BY NoReward";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ditemukan data reward dengan keyword diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoReward"] + "')\">"
                        + rs.Rows[i]["NoReward"].ToString() + "</a>";
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaAgent"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaSkema"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Rumus"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["PeriodeDari"])) + " s/d " + Cf.Day(Convert.ToDateTime(rs.Rows[i]["PeriodeSampai"]));
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Reward"].ToString();
                c.Wrap = false;
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
