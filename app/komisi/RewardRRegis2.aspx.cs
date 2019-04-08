using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class RewardRRegis2 : System.Web.UI.Page
    {
        protected string Project { get { return Request.QueryString["Project"]; } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Fill();
        }
        protected void Fill()
        {
            kode.Text = Request.QueryString["id"];
            tgl.Text = Cf.Day(DateTime.Today);

            list.Controls.Clear();

            string strSql = "SELECT a.Reward, a.NoReward, b.*"
                + " FROM MS_KOMISI_REWARD_P_DETAIL a"
                + " INNER JOIN MS_KOMISI_REWARD b ON a.NoReward = b.NoReward"
                + " WHERE 1=1 "
                + " AND (SELECT COUNT(*) FROM MS_KOMISI_REWARD_R_DETAIL WHERE NoReward = a.NoReward) = 0"
                + " AND Project = '" + Project + "'"
                + " AND NoRP = '" + Request.QueryString["id"] + "'";

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
                c.InnerHtml = rs.Rows[i]["NamaAgent"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoSkema"] + " (" + rs.Rows[i]["NamaSkema"] + ")";
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml= Cf.Day(Convert.ToDateTime(rs.Rows[i]["PeriodeDari"])) + " s/d " + Cf.Day(Convert.ToDateTime(rs.Rows[i]["PeriodeSampai"]));
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

            //Tanggal Realisasi
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
        protected void save_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                DateTime Tgl = Convert.ToDateTime(tgl.Text);
                string RRID = LibKom.RRID(Tgl.Month, Tgl.Year);

                Db.Execute("EXEC spKomisiRewardRDaftar"
                    + " '" + RRID + "'"
                    + ",'" + Tgl + "'"
                    + ",'" + Request.QueryString["id"] + "'"
                    + ",'" + Cf.Str(ket.Text) + "'"
                    );

                Db.Execute("UPDATE MS_KOMISI_REWARD_R SET "
                    + " Project = '" + Project + "'"
                    + " WHERE NoRR = '" + RRID + "'");

                int index = 0;
                foreach (Control tr in list.Controls)
                {
                    CheckBox cb = (CheckBox)list.FindControl("cb_" + index);

                    if (cb.Checked)
                    {
                        DataTable dd = Db.Rs("SELECT * FROM MS_KOMISI_REWARD_P_DETAIL WHERE NoReward = '" + cb.Attributes["title"] + "'");
                        if (dd != null)
                        {
                            Db.Execute("EXEC spKomisiRewardRDetil"
                                + " '" + RRID + "'"
                                + ",'" + dd.Rows[0]["NoReward"].ToString() + "'"
                                + ",'" + dd.Rows[0]["Reward"].ToString() + "'"
                                );
                        }
                    }

                    index++;
                }

                DataTable rsHeader = Db.Rs("SELECT "
                    + " NoRR"
                    + ",CONVERT(varchar,Tgl,106) AS [Tgl. Realisasi]"
                    + ",Ket AS [Keterangan]"
                    + ",NoRP AS [Kode Pengajuan]"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_REWARD_R "
                    + " WHERE NoRR = '" + RRID + "'");

                DataTable rsDetail = Db.Rs("SELECT "
                    + " CONVERT(VARCHAR, SN) "
                    + " + '.  ' + NoReward"
                    + " + '  ' + Reward "
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_REWARD_R_DETAIL a WHERE NoRR = '" + RRID + "'");

                string Ket = Cf.LogCapture(rsHeader)
                    + Cf.LogList(rsDetail, "DETAIL");

                Db.Execute("EXEC spLogKomisiRewardR"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + RRID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_REWARD_R_LOG ORDER BY LogID DESC");                
                Db.Execute("UPDATE MS_KOMISI_REWARD_R_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("RewardRRegis1.aspx?id=" + RRID);
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
