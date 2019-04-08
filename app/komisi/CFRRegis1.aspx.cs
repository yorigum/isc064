using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
{
    public partial class CFRRegis1 : System.Web.UI.Page
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

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (Request.QueryString["id"] != null)
            {
                feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                    + "<a href=\"javascript:popEditCFR('" + Request.QueryString["id"] + "')\">"
                    + "Realisasi Closing Fee Telah Berhasil..."
                    + "</a>";
            }
        }

        private void Bind()
        {
            dari.Text = Cf.Day(Cf.AwalBulan(DateTime.Now.Month, DateTime.Now.Year));
            sampai.Text = Cf.Day(Cf.AkhirBulan(DateTime.Now.Month, DateTime.Now.Year));

            //bind tipe marketing 
            tipesales.Items.Add(new ListItem { Text = "Tipe Marketing :", Value = "0" });
            LibMkt.ListTipeSales(tipesales, project.SelectedValue);
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(dari))
            {
                x = false;
                if (s == "") s = dari.ID;
                daric.Text = "Tanggal";
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                x = false;
                if (s == "") s = sampai.ID;
                sampaic.Text = "Tanggal";
            }
            else
                sampaic.Text = "";

            if (project.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = project.ID;
                projectc.Text = " &nbsp; Project Belum Dipilih";
            }
            else
            {
                projectc.Text = "";
            }

            return x;
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Fill();
            }
        }

        protected void Fill()
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string w = "";
            if(tipesales.SelectedIndex != 0)
            {
                w = " AND SalesTipe = '" + tipesales.SelectedValue + "'";
            }

            string strSql = "SELECT * FROM MS_KOMISI_CFP"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND Realisasi = 0"
                + w
                + " AND Project = '" + project.SelectedValue + "'"
                + " ORDER BY NoCFP";

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
                c.Text = "<a href=\"CFRRegis2.aspx?id=" + rs.Rows[i]["NoCFP"].ToString() + "&Project=" + project.SelectedValue + "\">Next</a>";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoCFP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(Tgl);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("select Tipe from REF_AGENT_TIPE where ID = '" + rs.Rows[i]["SalesTipe"] + "'");
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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipesales.Items.Clear();
            Bind();
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
