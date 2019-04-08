using System;
using System.Drawing;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class FP : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Bind();
                update.Visible = false;
            }

            Fill();
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            //Fill();
        }

        private void Bind()
        {
            int countFP = Db.SingleInteger("select count(*) from REF_FP");

            if (countFP != 0)
            {
                DataTable rs;

                rs = Db.Rs("SELECT DISTINCT TglTerimaFP FROM REF_FP ORDER BY TglTerimaFP");
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string v = Cf.Day(rs.Rows[i]["TglTerimaFP"].ToString());
                    ddltgl.Items.Add(new ListItem(v, v));
                }

                DataTable FpAkhir = Db.Rs("SELECT TOP 1 * FROM REF_FP ORDER BY TglTerimaFP DESC");
                ddltgl.SelectedValue = Cf.Day(Convert.ToDateTime(FpAkhir.Rows[0]["TglTerimaFP"]));
            }
        }

        private void Fill()
        {
            string Status = "";
            if (statusA.Checked) Status = " AND a.Status = '2' ";
            if (statusB.Checked) Status = " AND a.Status = '0' ";
            if (statusC.Checked) Status = " AND a.Status = '1' ";
            if (statusD.Checked) Status = "";

            string Project = "";
            //if (project.SelectedIndex != 0)
            //{
                Project = "AND a.Project = '" + project.SelectedValue + "'";
            //}

            string Tgl = "";
            if (ddltgl.SelectedIndex != 0)
                Tgl = " AND TglTerimaFP = '" + Cf.Day(ddltgl.SelectedValue) + "'";

            string strSql = "";
            if (statusA.Checked)
            {
                strSql = "SELECT a.* FROM REF_FP a"
                + " WHERE 1 = 1"
                + Project
                + Tgl
                + Status
                + " ORDER BY a.NoFPS";
            }
            else if (statusB.Checked)
            {
                strSql = "SELECT a.* FROM REF_FP a"
                + " WHERE 1 = 1"
                + Project
                + Tgl
                + Status
                + " ORDER BY a.NoFPS";
            }
            else if (statusC.Checked || statusD.Checked)
            {
                strSql = "SELECT a.*, b.NoTTS, b.NoTTS2, b.NoBKM, b.NoBKM2, b.ManualBKM FROM REF_FP a"
                + " LEFT JOIN MS_TTS b ON a.NoFPS = b.NoFPS"
                + " WHERE 1 = 1"
                + Project
                + Tgl
                + Status
                + " ORDER BY a.NoFPS";
            }
                                
            DataTable rs = Db.Rs(strSql);
            //Rpt.NoData(rpt, rs, "Belum ada Faktur Pajak yang didaftarkan.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow r = new HtmlTableRow();
                HtmlTableCell c;
                CheckBox cb;

                cb = new CheckBox();
                cb.ID = "nofp_" + i;
                if (statusA.Checked)
                    cb.Visible = false;
                else if (statusB.Checked)
                    cb.Visible = true;
                else if (statusC.Checked)
                    cb.Visible = false;
                else if (statusD.Checked)
                    cb.Visible = false;

                //cb.Visible = rs.Rows[i]["NoTTS"] is DBNull && statusB.Checked ? true : false;

                c = new HtmlTableCell();
                c.ID = "pk_" + i;
                c.Attributes["title"] = rs.Rows[i]["NoFPS"].ToString();
                c.Controls.Add(cb);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoFPS"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                if (statusC.Checked || statusD.Checked)
                    c.InnerHtml = rs.Rows[i]["NoTTS"] is DBNull ? "" : "<a href=\"javascript:call('" + rs.Rows[i]["NoTTS"] + "')\">"
                    + rs.Rows[i]["NoTTS2"].ToString() + "</a>";
                r.Cells.Add(c);

                c = new HtmlTableCell();
                if (statusC.Checked || statusD.Checked)
                    c.InnerHtml = rs.Rows[i]["NoBKM"] is DBNull ? "" : ""
                    + rs.Rows[i]["NoBKM2"].ToString() + "";
                r.Cells.Add(c);

                //Rpt.Border(r);
                rpt.Controls.Add(r);
            }

            if (statusB.Checked)
                update.Visible = true;
            else
                update.Visible = false;
        }

        protected void del_Click(object sender, System.EventArgs e)
        {
            int index = 0;
            foreach (Control tr in rpt.Controls)
            {
                HtmlTableCell c = (HtmlTableCell)rpt.FindControl("pk_" + index);
                CheckBox cb = (CheckBox)rpt.FindControl("nofp_" + index);
                
                if (c != null)
                {
                    Save(c.Attributes["title"], cb);
                }

                index++;
            }
            Response.Redirect("FP.aspx?done=yes");
        }

        protected void update_Click(object sender, EventArgs e)
        {
            int index = 0;
            foreach (Control tr in rpt.Controls)
            {
                HtmlTableCell c = (HtmlTableCell)rpt.FindControl("pk_" + index);
                CheckBox cb = (CheckBox)rpt.FindControl("nofp_" + index);

                if (c != null)
                {
                    if (cb.Checked)
                    {
                        Db.Execute("UPDATE REF_FP SET Status = 2 WHERE NoFPS = '" + c.Attributes["title"] + "' AND Project = '" + project.SelectedValue + "'");
                    }
                }

                index++;
            }
            Response.Redirect("FP.aspx?done=yes");
        }

        private void Save(string NoFP, CheckBox cb)
        {
            if (cb.Checked)
            {
                DataTable rs = Db.Rs("SELECT NoFPS FROM REF_FP WHERE NoFPS = '" + NoFP + "'");
                if (rs.Rows.Count > 0)
                {
                    Db.Execute("DELETE FROM REF_FP WHERE NoFPS = '" + NoFP + "'");
                }
            }
        }
        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            rpt.Controls.Clear();
            Fill();
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
