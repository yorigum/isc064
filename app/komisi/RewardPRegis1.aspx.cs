using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class RewardPRegis1 : System.Web.UI.Page
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

            string strSql = "SELECT *"
                + " FROM MS_KOMISI_REWARD a"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,a.Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,a.Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND (SELECT COUNT(*) FROM MS_KOMISI_REWARD_P_DETAIL WHERE NoReward = a.NoReward) = 0"
                + " AND a.Project = '" + project.SelectedValue + "'"
                + " ORDER BY a.NoReward";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Tidak terdapat data dengan kriteria seperti tersebut diatas.");

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow r = new HtmlTableRow();
                HtmlTableCell c;
                CheckBox cb;

                cb = new CheckBox();
                cb.ID = "cb_" + index;
                cb.Attributes["title"] = rs.Rows[i]["NoReward"].ToString();

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
            }
        }
        private bool valid()
        {

            string s = "";
            bool x = true;

            //Tanggal Pengajuan
            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

            if (!x)
            {
                this.RegisterStartupScript(
                    "focusScript"
                    , "<script language='javascript'>"
                    + " document.getElementById('" + s + "').focus();"
                    + " document.getElementById('" + s + "').select();"
                    + "</script>"
                    );
            }
            else
            {
            }

            return x;
        }
        protected void display_Click(object sender, System.EventArgs e)
        {
            Fill();
        }
        protected void save_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                DateTime Tgl = Convert.ToDateTime(tgl.Text);
                string RPID = LibKom.RPID(Tgl.Month, Tgl.Year);

                Db.Execute("EXEC spKomisiRewardPDaftar"
                    + " '" + RPID + "'"
                    + ",'" + Tgl + "'"
                    + ",'" + Cf.Str(ket.Text) + "'"
                    );

                Db.Execute("UPDATE MS_KOMISI_REWARD_P SET "
                    + " Project = '" + project.SelectedValue + "'"
                    + " WHERE NoRP = '" + RPID + "'");

                int index = 0;
                foreach (Control tr in list.Controls)
                {
                    CheckBox cb = (CheckBox)list.FindControl("cb_" + index);

                    if (cb.Checked)
                    {
                        DataTable dd = Db.Rs("SELECT * FROM MS_KOMISI_REWARD WHERE NoReward = '" + cb.Attributes["title"] + "'");
                        if (dd != null)
                        {
                            Db.Execute("EXEC spKomisiRewardPDetil"
                                + " '" + RPID + "'"
                                + ",'" + dd.Rows[0]["NoReward"].ToString() + "'"
                                + ",'" + dd.Rows[0]["Reward"].ToString() + "'"
                                );
                        }
                    }

                    index++;
                }

                DataTable rsHeader = Db.Rs("SELECT "
                    + " NoRP"
                    + ",CONVERT(varchar,Tgl,106) AS [Tgl. Pengajuan]"
                    + ",Ket AS [Keterangan]"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_REWARD_P "
                    + " WHERE NoRP = '" + RPID + "'");

                DataTable rsDetail = Db.Rs("SELECT "
                    + " CONVERT(VARCHAR, SN) "
                    + " + '  ' + NoReward "
                    + " + '  ' + Reward "
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_REWARD_P_DETAIL a WHERE NoRP = '" + RPID + "'");

                string Ket = Cf.LogCapture(rsHeader)
                    + Cf.LogList(rsDetail, "DETAIL");

                Db.Execute("EXEC spLogKomisiRewardP"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + RPID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_REWARD_P_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_KOMISI_REWARD_P_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                Response.Redirect("RewardPRegis1.aspx?id=" + RPID);
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
