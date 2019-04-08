using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
{
    public partial class DaftarCFP : System.Web.UI.Page
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
                + " FROM MS_KOMISI_CFP "
                + " WHERE NoCFP + NamaAgent + NamaSkema "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND Project IN (" + Act.ProjectListSql + ")"
                + " ORDER BY NoCFP";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ditemukan data pengajuan closing fee dengan keyword diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoCFP"] + "')\">"
                        + rs.Rows[i]["NoCFP"].ToString() + "</a>";
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.Wrap = false;
                r.Cells.Add(c);

                string Sales = Db.SingleString("SELECT TOP 1 STUFF((SELECT distinct ', ' + NamaAgent FROM MS_KOMISI_CFP_DETAIL AS T1"
                    + " where NoCFP = '" + rs.Rows[i]["NoCFP"].ToString() + "'"
                    + " FOR XML PATH('')), 1, 1, '') As Nama "
                    + " FROM MS_KOMISI_CFP_DETAIL AS T2 where NoCFP = '" + rs.Rows[i]["NoCFP"].ToString() + "'"
                );

                c = new TableCell();
                c.Text = Sales;
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
