using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ISC064.KOMISI
{
    public partial class RewardEdit : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                FillHeader();
            }

            if (!Page.IsPostBack)
            {
                FillTable();
            }
        }
        private void FillHeader()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_KOMISI_REWARD_LOG&Pk=" + Nomor.PadLeft(5, '0') + "'";

            DataTable rsHeader = Db.Rs("SELECT * FROM MS_KOMISI_REWARD WHERE NoReward = '" + Nomor + "'");
            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                skema.Text = rsHeader.Rows[0]["NamaSkema"] + "(" + rsHeader.Rows[0]["NoSkema"].ToString().PadLeft(3, '0') + ")";
                sales.Text = rsHeader.Rows[0]["NamaAgent"].ToString();
                periode.Text = Cf.Day(rsHeader.Rows[0]["PeriodeDari"]) + " s/d " + Cf.Day(rsHeader.Rows[0]["PeriodeSampai"]);
                reward.Text = rsHeader.Rows[0]["Reward"].ToString();
                tgl.Text = Cf.Day(rsHeader.Rows[0]["Tgl"]);
                project.Text = rsHeader.Rows[0]["Project"].ToString();
            }
        }

        private void FillTable()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_REWARD_DETAIL WHERE NoReward = '" + Nomor + "'");
            if (rs.Rows.Count > 0)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    TableCell c;
                    TableRow tr;

                    tr = new TableRow();

                    c = new TableCell();
                    c.Text = (i + 1).ToString() + ".";
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoKontrak"].ToString();
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoUnit"].ToString();
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NamaCust"].ToString();
                    tr.Cells.Add(c);

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
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = NoRef;
                    tr.Cells.Add(c);

                    tb.Rows.Add(tr);
                }
            }
        }
        protected void ok_Click(object sender, System.EventArgs e)
        {
            Js.Close(this);
        }
        private string Nomor
        {
            get
            {
                return Cf.Pk(Request.QueryString["Nomor"]);
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
