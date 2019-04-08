using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI.Laporan
{

    public partial class MasterReward : System.Web.UI.Page
{
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");

            return a;
        }

        private void Report()
        {
            rpt.Visible = true;

            Header();
            MenuAtas();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            //Rpt.Judul(x, comp, judul);

            Rpt.SubJudul(x
                , "Project : " + Project
                );

            string pers = (Perusahaan == "SEMUA") ? "SEMUA" : Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
            Rpt.SubJudul(x
                , "Perusahaan : " + pers
                );

            string legend = "Status: A = Aktif / B = Batal.";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void MenuAtas()
        {
            TableRow r = new TableRow();
            TableCell c = new TableCell();

            c = new TableCell();
            c.Text = "Kode Reward";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Agent";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Periode";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Reward";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Status";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void Fill()
        {
            string nProject = "";
            if (Project != "SEMUA") nProject = " AND A.Project = '" + Project + "'";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND C.Pers = '" + Perusahaan + "'";

            int index = 1;

            int no = 1;
            decimal t1 = 0;

            string strSql = "SELECT "
                + "A.NoReward"
                + ",A.NoAgent"
                + ",A.NamaAgent"
                //+ ",A.Status"
                + ",A.PeriodeDari"
                + ",A.PeriodeSampai"
                + ",A.Reward"
                + ",B.SN"
                + ",C.Pers"
                + " FROM MS_KOMISI_REWARD A INNER JOIN MS_KOMISI_REWARD_DETAIL B ON A.NoReward = B.NoReward"
                + " INNER JOIN MS_KONTRAK C ON B.NoKontrak = C.NoKontrak"
                + nProject
                + nPerusahaan
                + " ORDER BY A.NoReward";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                //r.Attributes["ondblclick"] = "popJadwalKomisi('" + rs.Rows[i]["NoKontrak"] + "')";

                c = new TableCell();
                c.Text = rs.Rows[i]["NoReward"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaAgent"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["PeriodeDari"])) + " s/d " + Cf.Day(Convert.ToDateTime(rs.Rows[i]["PeriodeSampai"]));
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Reward"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                string Status = "<label style='color:red;'>Belum Pengajuan</label>", NoRef = "";
                DataTable cfp = Db.Rs("SELECT * FROM MS_KOMISI_REWARD_P_DETAIL WHERE NoReward = '" + rs.Rows[i]["NoReward"].ToString() + "'");
                if (cfp.Rows.Count > 0)
                {
                    NoRef = cfp.Rows[0]["NoRP"].ToString();
                    Status = "<label style='color:green;'>Pengajuan Pencairan</label>";

                    DataTable cfr = Db.Rs("SELECT * FROM MS_KOMISI_REWARD_R_DETAIL WHERE NoReward = '" + rs.Rows[i]["NoReward"].ToString() + "'");
                    if (cfr.Rows.Count > 0)
                    {
                        NoRef = cfr.Rows[0]["NoRR"].ToString();
                        Status = "<label style='color:blue;'>Realisasi Pencairan</label>";
                    }
                }

                c = new TableCell();
                c.Text = Status;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
                no++;
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