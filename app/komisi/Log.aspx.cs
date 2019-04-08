using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
{
    public partial class Log : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb1);
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                init();
            }

            Js.Focus(this, keyword);
        }

        private void init()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);

            tb.Attributes["ondblclick"] = "document.getElementById('" + display.ID + "').click()";
            tb.SelectedIndex = 0;

            bindUser();
        }

        private void bindUser()
        {
            DataTable rs;

            rs = Db.Rs("SELECT UserID,Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME ORDER BY Nama, UserID");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["UserID"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v + ")";
                user.Items.Add(new ListItem(t, v));
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(dari))
            {
                daric.Text = "Tanggal";
                if (s == "") s = dari.ID;
                x = false;
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                sampaic.Text = "Tanggal";
                if (s == "") s = sampai.ID;
                x = false;
            }
            else
                sampaic.Text = "";

            if (!x)
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");

            return x;
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Fill2();
            }
        }

        private void Fill()
        {
            href.SelectedValue = tb.SelectedValue;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string UserID = "";
            if (user.SelectedIndex != 0)
                UserID = " AND UserID = '" + user.SelectedValue + "'";

            string KeyWord = "";
            if (keyword.Text != "")
                KeyWord = " AND ( (Ket LIKE '%" + keyword.Text.Replace("'", "''") + "%')"
                    + " OR (Pk LIKE '%" + keyword.Text.Replace("'", "''") + "%') )"
                    ;

            string strSql = "SELECT "
                + " LogID"
                + ",Tgl"
                + ",Aktivitas"
                + ",UserID"
                + ",IP"
                + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = " + tb.SelectedValue + ".UserID) AS Nama"
                + ",Pk"
                + ",Approve"
                + " FROM " + tb.SelectedValue
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + UserID
                + KeyWord
                + " ORDER BY LogID";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ada logfile untuk periode tanggal tersebut.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Time(rs.Rows[i]["Tgl"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["UserID"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a show-modal='#ModalPopUp' modal-url='LogDetil.aspx?LogID=" + rs.Rows[i]["LogID"] + "&tb=" + tb.SelectedValue + "&sumber=" + tb.SelectedItem.Text + "'>"
                    + rs.Rows[i]["Aktivitas"].ToString()
                    + "</a>";
                r.Cells.Add(c);

                c = new TableCell();
                if (href.SelectedItem.Text != "NA")
                {
                    string popup = href.SelectedItem.Text.Replace("%pk%", rs.Rows[i]["Pk"].ToString());
                    c.Text = "<a show-modal='#ModalPopUp' modal-url=" + popup + " modal-title='Detail' >" + rs.Rows[i]["Pk"].ToString() + "</a>";
                }
                else
                    c.Text = rs.Rows[i]["Pk"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Approve"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }
        private void Fill2()
        {
            href.SelectedValue = tb.SelectedValue;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string UserID = "";
            if (user.SelectedIndex != 0)
                UserID = " AND UserID = '" + user.SelectedValue + "'";

            string KeyWord = "";
            if (keyword.Text != "")
                KeyWord = " AND ( (Ket LIKE '%" + keyword.Text.Replace("'", "''") + "%')"
                    + " OR (Pk LIKE '%" + keyword.Text.Replace("'", "''") + "%') )"
                    ;

            string Project = " AND Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedIndex != 0)
                Project = " AND Project = '" + project.SelectedValue + "'";

            string nav = ",'<a href=\"javascript:popLogDetil('''+ CONVERT(VARCHAR(50),LogID) +''' , ''" + tb.SelectedValue + "'' , ''" + tb.SelectedItem.Text + "'')\">' + Aktivitas + '</a>'";

            string popup = "";
            if (href.SelectedItem.Value == "REF_SKOM_LOG")
            {
                popup = ",'<a href=\"javascript:popSkom('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "REF_SKOM_TERM_LOG")
            {
                popup = ",'<a href=\"javascript:popSkomTerm('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "REF_SKOM_CF_LOG")
            {
                popup = ",'<a href=\"javascript:popSkomCF('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "REF_SKOM_REWARD_LOG")
            {
                popup = ",'<a href=\"javascript:popSkomReward('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "MS_KOMISI_LOG")
            {
                popup = ",'<a href=\"javascript:popEditKomisi('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "MS_KOMISI_P_LOG")
            {
                popup = ",'<a href=\"javascript:popEditKomisiP('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "MS_KOMISI_R_LOG")
            {
                popup = ",'<a href=\"javascript:popEditKomisiR('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "MS_KOMISI_CF_LOG")
            {
                popup = ",'<a href=\"javascript:popEditCF('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "MS_KOMISI_CFP_LOG")
            {
                popup = ",'<a href=\"javascript:popEditCFP('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "MS_KOMISI_CFR_LOG")
            {
                popup = ",'<a href=\"javascript:popEditCFR('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "MS_KOMISI_REWARD_LOG")
            {
                popup = ",'<a href=\"javascript:popEditReward('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "MS_KOMISI_REWARD_P_LOG")
            {
                popup = ",'<a href=\"javascript:popEditRewardP('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "MS_KOMISI_REWARD_R_LOG")
            {
                popup = ",'<a href=\"javascript:popEditRewardR('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else
                popup = ",'+Pk+'";

            string strSql = "SELECT "
            + "CONVERT(VARCHAR,Tgl,106) AS Tgl"
            + ",FORMAT(Tgl,'HH:mm:ss') AS Jam"
            + nav
            + " AS Aktivitas"
            + ",UserID"
            + popup
            + " AS Ref"
            + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = " + tb.SelectedValue + ".UserID) AS Nama"
            + ",Approve"
            + " FROM " + tb.SelectedValue
            + " WHERE 1=1 "
            + " AND CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
            + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
            + UserID
            + Project
            + KeyWord
            + " ORDER BY LogID";

            DataTable rs = Db.Rs(strSql);
            tb1.DataSource = rs;
            tb1.DataBind();
        }
        protected void xls_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                Fill();
                Rpt.ToExcel(this, rpt);
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

        protected void tb1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb1.PageIndex = e.NewPageIndex;
            Fill2();
        }
    }
}
