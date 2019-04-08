using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
{
    public partial class RewardRRegis1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Bind();
                Act.ProjectList(project);
            }
        }

        private void Bind()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string strSql = "SELECT * FROM MS_KOMISI_REWARD_P"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND Realisasi = 0"
                + " AND Project = '"+project.SelectedValue+"'"
                + " ORDER BY NoRP";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak terdapat pengajuan dengan kriteria seperti tersebut diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                DateTime Tgl = Convert.ToDateTime(rs.Rows[i]["Tgl"]);

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = "<a href=\"RewardRRegis2.aspx?id=" + rs.Rows[i]["NoRP"].ToString() + "&Project=" + project.SelectedValue + "\">Next</a>";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoRP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(Tgl);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Ket"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
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
