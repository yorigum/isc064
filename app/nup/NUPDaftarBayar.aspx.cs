using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class NUPDaftarBayar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                if (Request.QueryString["project"] != null)
                {
                    
                    project.SelectedValue = Request.QueryString["project"];
                }
            }
            Fill();

        }

        private void Fill()
        {

            string nav = ",'<a href=\"NUPDaftarBayar2.aspx?No='''+A.NoNUP+''' &Tipe='''+A.Tipe+''' &Project='''+A.Project+'''\">Next..</a>'";
            string strSql = "SELECT "
                + "CASE WHEN A.Revisi > 0 THEN + A.NoNUP + 'R' ELSE + A.NoNUP END AS NUP"
                + ",B.Nama AS Customer"
                + ",C.Nama AS Agent"
                + nav
                + " AS Act"
                + " FROM MS_NUP A "
                + " INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer"
                + " INNER JOIN MS_AGENT C ON A.NoAgent = C.NoAgent"
                //+ " WHERE B.NoKTP != '' AND B.NoHP != '' AND B.RekBank != ''"
                + " WHERE A.NilaiBayar = 0"
                + " AND A.NoNUP + B.Nama + C.Nama LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND A.Project = '" + project.SelectedValue + "'"
                + " ORDER BY A.NoNUP";

            string tampilNUP = "";            
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
        protected void display_Click(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Cf.SetGrid(tb);
                Fill();
            }
        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}
