using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace ISC064.NUP
{
    public partial class NUP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Bind();
            }
            //Fill();
        }

        protected void Bind()
        {
            jenis.Items.Clear();
            jenis.Items.Add(new ListItem("Pilih : "));
            DataTable rs = Db.Rs("SELECT * FROM REF_JENISPROPERTI WHERE Project = '" + project.SelectedValue + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string a = rs.Rows[i]["SN"].ToString();
                string b = rs.Rows[i]["Nama"].ToString();
                jenis.Items.Add(new ListItem(b));
            }
        }

        protected void display_Click(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            Cf.SetGrid(tb);
            Fill();
            //}
        }

        private void Fill()
        {
            string a = "";
            if (jenis.SelectedIndex > 0) a = " AND Tipe = '" + jenis.SelectedValue + "'";

            string Project = (project.SelectedIndex == 0) ? " AND A.Project IN (" + Act.ProjectListSql + ")" : " AND A.Project = '" + project.SelectedValue + "'";

            string nav = "'<a href=\"javascript:call('''+ A.NoNUP +''','''+ A.Tipe +''','''+ A.Project +''')\">' + A.NoNUP + '</a></p>'";
            string strSql = "SELECT "
                + nav
                + " AS NUP"
                + ",CASE WHEN A.Status = 0 THEN 'Normal' WHEN A.Status = 1 THEN 'Aktivasi' WHEN A.Status = 2 THEN 'Pemanggilan NUP' WHEN A.Status = 3 THEN 'Sudah Pilih Unit' WHEN A.Status = 4 THEN 'Closing Unit' WHEN A.Status = 5 THEN 'Refund NUP' END AS Status"
                + ",(SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = A.NoCustomer) AS Customer"
                + ",(SELECT Nama FROM MS_AGENT WHERE NoAgent = A.NoAgent) AS Agent"
                + ",A.Tipe AS Jenis"
                + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = A.Project) AS Project "
                + ",CASE WHEN A.Status < 3 THEN '<a href=\"NUPRevisi.aspx?NoNUP='''+ A.NoNUP + '''&Tipe='''+A.Tipe+'''&project='''+A.Project+'''\">Revisi</a>' END AS Revisi"
                + ",CASE WHEN A.Status < 3 AND (SELECT COUNT(*) FROM MS_NUP_PELUNASAN WHERE NoNUP = A.NoNUP AND Tipe = A.Tipe AND Project = A.Project) > 0 THEN '<a href=\"NUPRefund.aspx?NoNUP='''+ A.NoNUP + '''&Tipe='''+A.Tipe+'''&project='''+A.Project+'''\">Refund</a>' END AS Refund"
                + " FROM MS_NUP A "
                + " INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer "
                + " WHERE A.NoNUP+B.Nama LIKE '%" + keyword.Text + "%' "
                + Project
                + a
                + " ORDER BY A.Tipe,A.NoNUP";

            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            jenis.Items.Clear();
            jenis.Items.Add(new ListItem("Jenis : "));
            Bind();
        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}