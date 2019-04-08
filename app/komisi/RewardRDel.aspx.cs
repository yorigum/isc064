using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{

    public partial class RewardRDel : System.Web.UI.Page
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
            fill();
        }

        private void Bind()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);
        }

        protected void fill()
        {
            list.Controls.Clear();

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string strSql = "SELECT a.NoRR,a.NoReward, a.SN, a.Reward, b.NoRP, c.NamaAgent, c.NoSkema, c.NamaSkema, c.PeriodeDari, c.PeriodeSampai"
                + " FROM MS_KOMISI_REWARD_R_DETAIL a"
                + " INNER JOIN MS_KOMISI_REWARD_R b ON a.NoRR = b.NoRR"
                + " INNER JOIN MS_KOMISI_REWARD c ON a.NoReward = c.NoReward"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,b.Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,b.Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND b.Project ='" + project.SelectedValue + "'"
                //+ " AND (SELECT COUNT(*) FROM MS_KOMISI_REWARD_P_DETAIL WHERE NoReward = c.NoReward AND SN = a.SN) = 0"
                + " ORDER BY b.NoRR";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Tidak terdapat pengajuan dengan kriteria seperti tersebut diatas.");
            del.Enabled = false;

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;
                CheckBox cb;

                r.VerticalAlign = VerticalAlign.Top;

                cb = new CheckBox();
                cb.ID = "cb_" + index;
                cb.Attributes["title"] = rs.Rows[i]["NoRR"].ToString();

                c = new TableCell();
                c.Controls.Add(cb);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoRP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaAgent"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoSkema"] + " (" + rs.Rows[i]["NamaSkema"] + ")";
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

                list.Controls.Add(r);

                index++;
                del.Enabled = true;
            }
        }

        protected void display_Click(object sender, System.EventArgs e)
        {

        }

        protected void delbtn_Click(object sender, EventArgs e)
        {
            int index = 0;
            foreach (Control tr in list.Controls)
            {
                CheckBox cb = (CheckBox)list.FindControl("cb_" + index);

                if (cb.Checked)
                {
                    int cfp = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_REWARD_R_DETAIL WHERE NoRR = '" + cb.Attributes["title"] + "'");
                    if (cfp > 0)
                    {
                        DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_REWARD_R WHERE NoRR = '" + cb.Attributes["title"] + "'");
                        if (rs.Rows.Count == 0)
                            Response.Redirect("/CustomError/Deleted.html");
                        else
                        {
                            string Ket = "***Alasan Delete :<br>" + Cf.Str(alasan.Text)
                                + "<br><br>***Data Sebelum Delete :<br>"
                                + Cf.LogCapture(rs);

                            //Db.Execute("UPDATE MS_KOMISI_CFP SET Realisasi = 0 WHERE NoCFP = '" + rs.Rows[0]["NoCFP"].ToString() + "' AND Realisasi = 1");
                            Db.Execute("EXEC spKomisiRewardRDel '" + rs.Rows[0]["NoRR"].ToString() + "'");

                            int c = Db.SingleInteger(
                                "SELECT COUNT(*) FROM MS_KOMISI_REWARD_R WHERE NoRR = '" + rs.Rows[0]["NoRR"].ToString() + "'");

                            if (c == 0)
                            {
                                //Log
                                Db.Execute("EXEC spLogKomisiRewardR "
                                    + " 'DELETE'"
                                    + ",'" + Act.UserID + "'"
                                    + ",'" + Act.IP + "'"
                                    + ",'" + Ket + "'"
                                    + ",'" + rs.Rows[0]["NoRR"].ToString() + "'"
                                    );

                                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_REWARD_R_LOG ORDER BY LogID DESC");                                
                                Db.Execute("UPDATE MS_KOMISI_REWARD_R_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                            }
                        }
                    }
                }
                index++;
            }
            Response.Redirect("RewardRDel.aspx");
        }

        private string NoRR
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoRR"]);
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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
