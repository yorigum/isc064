using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{

    public partial class RewardPDel : System.Web.UI.Page
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
            Fill();
        }

        private void Bind()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);

            DataTable rs;
            string strSql;

            strSql = "SELECT * FROM MS_AGENT WHERE Status = 'A' AND Project = '" + project.SelectedValue + "' ORDER BY Nama";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                sales.Items.Add(new ListItem(t, v));
            }

        }

        protected void Fill()
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

            string strSql = "SELECT a.NoRP,a.NoReward, a.SN, a.Reward,c.NamaAgent, c.NoSkema, c.NamaSkema, c.PeriodeDari, c.PeriodeSampai"
                + " FROM MS_KOMISI_REWARD_P_DETAIL a"
                + " INNER JOIN MS_KOMISI_REWARD_P b ON a.NoRP = b.NoRP"
                + " INNER JOIN MS_KOMISI_REWARD c ON a.NoReward = c.NoReward"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,b.Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,b.Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND b.Project = '" + project.SelectedValue + "'"
                //+ " AND (SELECT COUNT(*) FROM MS_KOMISI_REWARD_P_DETAIL WHERE NoReward = c.NoReward AND SN = a.SN) = 0"
                + " ORDER BY b.NoRP";

            //string strSql = "SELECT a.NoReward, a.SN, b.NoSkema, b.NamaSkema, b.PeriodeDari, b.PeriodeSampai, b.Reward"
            //    + " FROM MS_KOMISI_REWARD_P_DETAIL a"
            //    + " INNER JOIN MS_KOMISI_REWARD b ON a.NoReward = b.NoReward"
            //    + " WHERE 1=1 "
            //    + " AND CONVERT(varchar,b.Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
            //    + " AND CONVERT(varchar,b.Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
            //    + " AND (SELECT COUNT(*) FROM MS_KOMISI_REWARD_P_DETAIL WHERE NoReward = a.NoReward AND SN = a.SN) = 0"
            //    + v
            //    + " ORDER BY b.NoReward";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Tidak terdapat data dengan kriteria seperti tersebut diatas.");
            del.Enabled = false;

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow r = new HtmlTableRow();
                HtmlTableCell c;
                CheckBox cb;

                cb = new CheckBox();
                cb.ID = "cb_" + index;
                cb.Attributes["title"] = rs.Rows[i]["NoRP"].ToString();

                c = new HtmlTableCell();
                c.Controls.Add(cb);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoReward"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NamaAgent"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoSkema"] + " (" + rs.Rows[i]["NamaSkema"] + ")";
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Day(Convert.ToDateTime(rs.Rows[i]["PeriodeDari"])) + " s/d " + Cf.Day(Convert.ToDateTime(rs.Rows[i]["PeriodeSampai"]));
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Reward"].ToString();
                r.Cells.Add(c);

                list.Controls.Add(r);

                index++;
                del.Enabled = true;
            }
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            Fill();
        }
        protected void delbtn_Click(object sender, EventArgs e)
        {
            int index = 0;
            foreach (Control tr in list.Controls)
            {
                CheckBox cb = (CheckBox)list.FindControl("cb_" + index);

                if (cb.Checked)
                {
                    int cfp = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_REWARD_P_DETAIL WHERE NoRP = '" + cb.Attributes["title"] + "'");
                    if (cfp > 0)
                    {
                        DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_REWARD_P WHERE NoRP = '" + cb.Attributes["title"] + "'");
                        if (rs.Rows.Count == 0)
                            Response.Redirect("/CustomError/Deleted.html");
                        else
                        {
                            string Ket = "***Alasan Delete :<br>" + Cf.Str(alasan.Text)
                                + "<br><br>***Data Sebelum Delete :<br>"
                                + Cf.LogCapture(rs);

                            Db.Execute("EXEC spKomisiRewardPDel '" + rs.Rows[0]["NoRP"].ToString() + "'");

                            int c = Db.SingleInteger(
                                "SELECT COUNT(*) FROM MS_KOMISI_REWARD_P WHERE NoRP = '" + rs.Rows[0]["NoRP"].ToString() + "'");

                            if (c > 0)
                            {
                                //Log
                                Db.Execute("EXEC spLogKomisiRewardP "
                                    + " 'DELETE'"
                                    + ",'" + Act.UserID + "'"
                                    + ",'" + Act.IP + "'"
                                    + ",'" + Ket + "'"
                                    + ",'" + rs.Rows[0]["NoRP"].ToString() + "'"
                                    );

                                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_REWARD_P_LOG ORDER BY LogID DESC");
                                string Project = Db.SingleString("SELECT Project FROM MS_KOMISI_REWARD_P WHERE NoRP = " + rs.Rows[0]["NoRP"].ToString());
                                Db.Execute("UPDATE MS_KOMISI_REWARD_P_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                            }
                        }
                    }
                }
                index++;
            }

            Response.Redirect("RewardPDel.aspx");
        }

        private string NoRP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoRP"]);
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
            sales.Items.Clear();
            sales.Items.Add(new ListItem("Sales : "));
            Bind();
        }
    }
}