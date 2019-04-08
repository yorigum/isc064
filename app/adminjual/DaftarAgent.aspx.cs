using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class DaftarAgent : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);

            //Js.ConfirmKeyword(this, keyword);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["status"] == "a")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "i")
                    metode.SelectedIndex = 2;

                Act.ProjectList(project);
                if (metode.SelectedIndex != 0) metode.Enabled = false;
            }
        }

        protected void search_Click(object sender, System.EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
        }

        private void Fill()
        {
            string addq = "";
            if (metode.SelectedIndex == 1)
                addq = " AND Status = 'A'";
            else if (metode.SelectedIndex == 2)
                addq = " AND Status = 'I'";

            string nav = "'<a href=\"javascript:call(''' + CONVERT(varchar(50), NoAgent) + ''')\">'"
                    + " + Nama + (FORMAT(NoAgent, ' (00000#)')) +"
                    + "'</a>'"
                    ;
            string strSql = "SELECT "
                + nav
                + " AS Nama"                
                + ",Principal AS Principal"
                + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = MS_AGENT.Project) AS Project"
                + " FROM MS_AGENT "
                + " WHERE Nama + Principal"
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND Project = '" + project.SelectedValue + "'"
                + addq
                + " ORDER BY Nama, NoAgent";
            
            DataTable rs = new DataTable();
            Db.Fill(rs, strSql);    
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
