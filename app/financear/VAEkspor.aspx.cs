using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class VAEkspor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            //Cf.SetGrid(tb);
            if (!Page.IsPostBack)
            {
                init();

                //Act.ProjectList(project);
                //if (Request.QueryString["project"] != null)
                //{

                //    project.SelectedValue = Request.QueryString["project"];
                //}
            }
            //Fill();

        }

        private void init()
        {
            dari.Text = Cf.Day(Cf.AwalBulan(DateTime.Now.Month, DateTime.Now.Year));
            sampai.Text = Cf.Day(Cf.AkhirBulan(DateTime.Now.Month, DateTime.Now.Year));
        }

        private void Fill()
        {

            string nav = ",'<a href=\"VAEksporSinarmas.aspx?dari=" + dari.Text + "&sampai=" + sampai.Text + "\">Next..</a>'";
            string strSql = "SELECT "
                + " Acc"
                + ",Bank"
                + nav
                + " AS Act"
                + " FROM REF_ACC "
                + " where Acc = '11-1201'"
                ;

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
                
            }

            Cf.SetGrid(tb);
            Fill();
        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}
