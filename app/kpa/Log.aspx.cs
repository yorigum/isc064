using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA
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
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");

            return x;
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Cf.SetGrid(tb1);
                Fill2();
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

            string Project = " AND Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedIndex != 0)
                Project = " AND Project = '" + project.SelectedValue + "'";

            string KeyWord = "";
            if (keyword.Text != "")
                KeyWord = " AND ( (Ket LIKE '%" + keyword.Text.Replace("'", "''") + "%')"
                    + " OR (Pk LIKE '%" + keyword.Text.Replace("'", "''") + "%') )"
                    ;

            string nav = ",'<a href=\"javascript:popLogDetil('''+ CONVERT(varchar(50),LogID) +''' , ''" + tb.SelectedValue + "'' , ''" + tb.SelectedItem.Text + "'')\">' + Aktivitas + '</a>'";

            string aktivitas = "";

            string popup = "";
            if (href.SelectedItem.Value == "MS_CUSTOMER_LOG")
            {
                popup = ",'<a href=\"javascript:popEditCustomer('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "MS_RESERVASI_LOG")
            {
                popup = ",'<a href=\"javascript:popEditReservasi('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else if (href.SelectedItem.Value == "MS_KONTRAK_LOG")
            {
                popup = ",'<a href=\"javascript:popEditKontrak('''+Pk+''')\">' + Pk +  '</a>'";
                aktivitas = " AND Aktivitas IN ('KPACLB','KPAWAN','KPAOTS','KPALPA','KPASP3','KPAAKD')";
            }
            else if (href.SelectedItem.Value == "MS_TTR_LOG")
            {
                popup = ",'<a href=\"javascript:popEditTTR('''+Pk+''')\">' + Pk +  '</a>'";
            }
            else
                popup = ",''+Pk+''";

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
            + aktivitas
            + KeyWord
            + " ORDER BY LogID";

            DataTable rs = Db.Rs(strSql);
            tb1.DataSource = rs;
            tb1.DataBind();
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

            string Project = " AND Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedIndex != 0)
                Project = " AND Project = '" + project.SelectedValue + "'";

            string KeyWord = "";
            if (keyword.Text != "")
                KeyWord = " AND ( (Ket LIKE '%" + keyword.Text.Replace("'", "''") + "%')"
                    + " OR (Pk LIKE '%" + keyword.Text.Replace("'", "''") + "%') )"
                    ;
            string KPA = "";
            if (tb.SelectedIndex == 0)
            {
                KPA = " AND Aktivitas LIKE '%KPA%' or Like '%KPR%'";
            }

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
                + KPA
                + UserID
                + Project
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
                c.Text = "<a href=\"javascript:popLog('" + rs.Rows[i]["LogID"] + "','" + tb.SelectedValue + "','" + tb.SelectedItem.Text + "')\">"
                    + rs.Rows[i]["Aktivitas"].ToString()
                    + "</a>";
                r.Cells.Add(c);

                c = new TableCell();
                if (href.SelectedItem.Text != "NA")
                {
                    string popup = href.SelectedItem.Text.Replace("%pk%", rs.Rows[i]["Pk"].ToString());
                    c.Text = "<a href=\"javascript:" + popup + "\">" + rs.Rows[i]["Pk"].ToString() + "</a>";
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
