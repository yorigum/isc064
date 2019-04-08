using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class RewardDel : System.Web.UI.Page
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

            strSql = "SELECT * FROM REF_SKOM_REWARD WHERE Inaktif = 0 ORDER BY Dari";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoSkema"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                skema.Items.Add(new ListItem(t, v));
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

            string w = "";
            if (skema.SelectedIndex != 0)
                w = " AND NoSkema = '" + skema.SelectedValue + "'";

            string strSql = "SELECT *"
                + " FROM MS_KOMISI_REWARD"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + w
                + " AND Project = '" + project.SelectedValue + "'"
                + " ORDER BY NoReward";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Tidak terdapat data dengan kriteria seperti tersebut diatas.");
            del.Enabled = false;

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = rs.Rows[i]["NoReward"].ToString();
                c.ID = "pk_" + index;
                c.Attributes["title"] = rs.Rows[i]["NoReward"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaAgent"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaSkema"].ToString() + " (" + rs.Rows[i]["NoSkema"].ToString() + ")";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Tgl(Convert.ToDateTime(rs.Rows[i]["PeriodeDari"])) + " s/d " + Cf.Tgl(Convert.ToDateTime(rs.Rows[i]["PeriodeSampai"]));
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
            Fill();
        }
        protected void del_Click(object sender, EventArgs e)
        {
            int index = 0;
            foreach (Control tr in list.Controls)
            {
                TableCell pk = (TableCell)list.FindControl("pk_" + index);

                int cfp = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_REWARD_P_DETAIL WHERE NoReward = '" + pk.Attributes["title"] + "'");
                if (cfp == 0)
                {
                    DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_REWARD WHERE NoReward = '" + pk.Attributes["title"] + "'");
                    if (rs.Rows.Count == 0)
                        Response.Redirect("/CustomError/Deleted.html");
                    else
                    {
                        string Ket = "***Alasan Delete :<br>" + Cf.Str(alasan.Text)
                            + "<br><br>***Data Sebelum Delete :<br>"
                            + Cf.LogCapture(rs);

                        Db.Execute("EXEC spKomisiRewardDel '" + rs.Rows[0]["NoReward"].ToString() + "'");

                        int c = Db.SingleInteger(
                            "SELECT COUNT(*) FROM MS_KOMISI_REWARD WHERE NoReward = '" + rs.Rows[0]["NoReward"].ToString() + "'");

                        if (c == 0)
                        {
                            //Log
                            Db.Execute("EXEC spLogKomisiReward "
                                + " 'DELETE'"
                                + ",'" + Act.UserID + "'"
                                + ",'" + Act.IP + "'"
                                + ",'" + Ket + "'"
                                + ",'" + rs.Rows[0]["NoReward"].ToString() + "'"
                                );

                            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_REWARD_LOG ORDER BY LogID DESC");                            
                            Db.Execute("UPDATE MS_KOMISI_REWARD_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                        }
                    }
                }

                index++;
            }

            Response.Redirect("RewardDel.aspx");
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
            Fill();
        }
    }
}
