using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class DaftarUnit : System.Web.UI.Page
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
                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["status"] == "a")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "b")
                    metode.SelectedIndex = 2;
                
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
                addq = " AND Status = 'B'";

            string nav = "'<a href=\"javascript:call(''' + NoStock + ''')\">'"
                    + " + NoStock + "
                    + "'</a>'"
                    ;

            string strSql = "SELECT "
                + nav
                + " AS NoStock"
                + ",NoUnit"
                + ",Luas"
                + ",FORMAT(LuasSG, '#,##0') AS LuasSG"
                + ",FORMAT(LuasNett, '#,##0') AS LuasNett"
                + ",CASE WHEN DefaultPL = 2 THEN FORMAT(PricelistKavling, '#,##0') ELSE FORMAT(PriceList,'#,###') END AS PL"
                + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = MS_UNIT.Project) AS Project"
                + ",Jenis AS Keterangan"
                + " FROM MS_UNIT "
                + " WHERE NoStock + NoUnit "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND Project = '" + project.SelectedValue + "'"
                + addq
                + " ORDER BY NoStock";

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
