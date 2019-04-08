using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class VA : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                init();
            }

            Js.Focus(this, search);
        }

        private void init()
        {
            DataTable rs = Db.Rs("SELECT DISTINCT Bank FROM REF_VA WHERE Project = '" + project.SelectedValue + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                bank.Items.Add(new ListItem(rs.Rows[i][0].ToString(), rs.Rows[i][0].ToString()));
            }
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
        }

        private void Fill()
        {
            string Bank = "";
            if (bank.SelectedIndex != 0)
                Bank = " AND Bank = '" + bank.SelectedValue + "'";

            string va = "'<a onclick=\"popEditVA('''+a.NoVA+''')\">' + a.NoVA + '</a>'";

            string Project = (project.SelectedIndex == 0) ? " AND Project IN (" + Act.ProjectListSql + ")" : " AND Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT"
                + va
                + " AS VA"
                + ",a.Bank AS Bank"
                + ",a.NoUnit AS Unit"
                + ",b.NoKontrak AS Kontrak"
                + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = a.Project) AS Project"
                + ",CASE WHEN b.NoCustomer <> '' THEN + (SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = b.NoCustomer) + '<br>' + ''+ a.NoUnit +'' ELSE ''+a.NoUnit+''  END AS Customer"
                + " FROM REF_VA a"
                + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.NoVA = b.NoVA"
                + " WHERE 1 = 1"
                + " AND a.NoUnit IN (SELECT NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoUnit = a.NoUnit " + Project + ")"
                + Bank
                + " ORDER BY a.NoVA";

            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
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

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            bank.Items.Clear();
            bank.Items.Add(new ListItem("Bank : "));
            init();
        }
    }
}
