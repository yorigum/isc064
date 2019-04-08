using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class NUPLunasBayar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack) Act.ProjectList(project);
            //Fill();
        }

        private void Fill()
        {
            decimal nilai = Db.SingleDecimal("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'FormatDaftarBayarNUP" + project.SelectedValue + "'");
            string nav = ",'<a href=\"NUPLunasBayar2.aspx?No='''+a.NoNUP+'''+&Tipe='''+a.Tipe+'''&project='''+a.Project+'''\">Next..</a>' AS Act";

            string tipe = "'" + Db.SingleString("SELECT Nama + ';' FROM REF_JENISPROPERTI WHERE Project = '" + project.SelectedValue + "' for xml path('')").TrimEnd(';').Replace(";", "','") + "'";

            string strSql2 = "SELECT a.NoNUP AS NUP,(SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = (SELECT NoCustomer FROM MS_NUP WHERE NoNUP = a.NoNUP AND Tipe = a.Tipe AND Project = a.Project)) AS Customer"
                     +" ,(SELECT NoUnit FROM MS_UNIT WHERE NoStock = a.NoStock) as Unit"
                     + nav
                     + " FROM MS_NUP_PRIORITY a"
                     + " WHERE a.NoKontrak = '' "
                     + " AND a.NoNUP Like '%" + keyword.Text + "%'"
                     + " AND a.Project = '" + project.SelectedValue + "'"
                     + " AND a.Tipe IN (" + tipe + ")"
                     + " AND (SELECT COUNT(*) FROM MS_NUP_PELUNASAN WHERE NoNUP = a.NoNUP) = 1"
                     ;
            DataTable rs = Db.Rs(strSql2);
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
            //if (!Page.IsPostBack)
            //{
            Cf.SetGrid(tb);
            Fill();
            //}
        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}
