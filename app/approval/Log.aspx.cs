using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.APPROVAL
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
        private void bindAkt()
        {
            DataTable rs;

            rs = Db.Rs("SELECT distinct aktivitas FROM " + tb.SelectedValue);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["aktivitas"].ToString();
                //string t = rs.Rows[i]["aktivitas"] + " (" + v + ")";
                aktivitas.Items.Add(new ListItem(v, v));
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
                UserID = " AND ApprovedBy = '" + user.SelectedValue + "'";

            string Project = " IN (" + Act.ProjectListSql + ")";
            if (project.SelectedIndex != 0)
                Project = " = '" + project.SelectedValue + "'";

            string KeyWord = "";
            if (keyword.Text != "")
                KeyWord = " AND ( (ApprovedBy LIKE '%" + keyword.Text.Replace("'", "''") + "%')"
                    + " OR (NoKontrak LIKE '%" + keyword.Text.Replace("'", "''") + "%') )"
                    ;
            string Akt = "";
            if (aktivitas.SelectedIndex != 0)
                Akt = " AND Aktivitas = '" + aktivitas.SelectedValue + "'";

            string nav = ",'<a href=\"javascript:popLogDetil('''+ CONVERT(VARCHAR(50),LogID) +''' , ''" + tb.SelectedValue + "'' , ''" + tb.SelectedItem.Text + "'')\">' + CASE WHEN Approve = 1 THEN 'Approved' WHEN Approve = 2 THEN 'Rejected' END + '</a>'";
            string Ref = ",'<a href=\"javascript:popEditKontrak2('''+ CONVERT(VARCHAR(50),NoKontrak) +''')\">' + NoKontrak + '</a>' AS Ref";

            string strSql = "SELECT "
                + "CONVERT(VARCHAR,TglApprove,106) AS Tgl"
                + ",FORMAT(TglApprove,'HH:mm:ss') AS Jam"
                + nav
                + " AS Aktivitas"
                + ",ApprovedBy AS UserID"
                //+ ",NoKontrak AS Ref"            
                + Ref
                + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = " + tb.SelectedValue + ".ApprovedBy) AS Nama"
                + " FROM " + tb.SelectedValue
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,TglApprove,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,TglApprove,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND NoKontrak IN (SELECT NoKontrak FROM MS_KONTRAK WHERE Project " + Project + ")"
                + UserID
                + KeyWord
                + Akt
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
                UserID = " AND ApprovedBy = '" + user.SelectedValue + "'";

            string KeyWord = "";
            if (keyword.Text != "")
                KeyWord = " AND ( (ApprovedBy LIKE '%" + keyword.Text.Replace("'", "''") + "%')"
                    + " OR (NoKontrak LIKE '%" + keyword.Text.Replace("'", "''") + "%') )"
                    ;

            string Project = " IN (" + Act.ProjectListSql + ")";
            if (project.SelectedIndex != 0)
                Project = " = '" + project.SelectedValue + "'";

            //string Akt= "";
            //if (aktivitas.SelectedIndex != 0)
            //    Akt = " AND A = '" + aktivitas.SelectedValue + "'";

            string strSql = "SELECT "
                + " LogID"
                + ",TglApprove"
                + ",ApprovedBy"
                + ",CASE WHEN Approve = 1 THEN 'Approved' WHEN Approve = 2 THEN 'Rejected' END AS Approve"
                + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = " + tb.SelectedValue + ".ApprovedBy) AS Nama"
                + ",NoKontrak"
                + ",Lvl"
                + ",CASE WHEN Tipe = 1 THEN 'Pengalihan Hak' WHEN Tipe = 2 THEN 'Pindah Unit' WHEN Tipe = 3 THEN 'Batal Kontrak' WHEN Tipe = 4 THEN 'Diskon' END AS Tipe"
                + ",CASE WHEN Finish = 1 THEN 'Selesai' ELSE 'Belum Selesai' END AS Finish"
                + ",Komentar"
                + " FROM " + tb.SelectedValue
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,TglApprove,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,TglApprove,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND NoKontrak IN (SELECT NoKontrak FROM MS_KONTRAK WHERE Project " + Project + ")"
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
                c.Text = Cf.Day(rs.Rows[i]["TglApprove"]);
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Time(rs.Rows[i]["TglApprove"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["ApprovedBy"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a show-modal='#ModalPopUp' modal-url='LogDetil.aspx?LogID=" + rs.Rows[i]["LogID"] + "&tb=" + tb.SelectedValue + "&sumber=" + tb.SelectedItem.Text + "'>"
                    + rs.Rows[i]["Approve"].ToString()
                    + "</a>";
                r.Cells.Add(c);

                c = new TableCell();
                if (href.SelectedItem.Text != "NA")
                {
                    string popup = href.SelectedItem.Text.Replace("%pk%", rs.Rows[i]["NoKontrak"].ToString());
                    c.Text = "<a show-modal='#ModalPopUp' modal-url=" + popup + " modal-title='Detail' >" + rs.Rows[i]["NoKontrak"].ToString() + "</a>";
                }
                else
                    c.Text = rs.Rows[i]["NoKontrak"].ToString();
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
        protected void tb_SelectedIndexChanged(object sender, EventArgs e)
        {
            aktivitas.Items.Clear();
            bindAkt();
        }
        protected void xls_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                Fill();
                Rpt.ToExcel(this, rpt);
            }
        }

        protected void tb1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb1.PageIndex = e.NewPageIndex;
            Fill2();
        }
    }
}
