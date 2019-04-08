using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class DaftarVA : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            //Js.ConfirmKeyword(this, keyword);

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
            }
        }

        protected void search_Click(object sender, System.EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
        }

        private void Fill()
        {
            string nav = "'<a href=\"javascript:call('''+ a.NoVA+''')\">' +a.NoVA+ '</a>'";

            string strSql = "SELECT"
                + nav
                + " AS VA"
                + ",a.Bank AS Bank"
                + ",b.NoKontrak AS Kontrak"
                + ",CASE WHEN b.NoCustomer <> '' THEN + (SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = b.NoCustomer) + '<br>' + ''+ a.NoUnit +'' ELSE ''+a.NoUnit+''  END AS Customer"
                + " FROM REF_VA a"
                + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.NoVA = b.NoVA"
                + " WHERE a.NoVA + Bank"
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND a.NoUnit = (SELECT NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoUnit = a.NoUnit AND Project = '" + project.SelectedValue + "')"
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
    }
}
