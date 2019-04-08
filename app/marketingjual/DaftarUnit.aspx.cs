using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class DaftarUnit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["project"] == null)
                {
                    Act.ProjectList(project);
                }
                else
                {
                    string v = Request.QueryString["project"].ToString();
                    string nproject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project='" + v + "'");
                    //Response.Write("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project='" + v + "'");
                    string t = v + " - " + nproject;
                    project.Items.Add(new ListItem(t, v));
                }

                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["status"] == "a")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "b")
                    metode.SelectedIndex = 2;

                if (metode.SelectedIndex != 0) metode.Enabled = false;
                //Fill();
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

            string nav = "";
            if (Request.QueryString["l"] != null)
            {
                nav = "'<a href=\"javascript:call2(''' + NoStock + ''',''' + NoUnit + ''')\">'"
                    + " + NoStock + "
                    + "'</a>'"
                    ;
            }
            else if (Request.QueryString["va"] != null)
            {
                nav = "'<a href=\"javascript:call(''' + NoUnit + ''')\">'"
                    + " + NoStock + "
                    + "'</a>'"
                    ;
            }
            else if (Request.QueryString["calc"] != null)
            {
                nav = "'<a href=\"javascript:call3(''' + NoStock + ''',''' + NoUnit + ''',''' + FORMAT(Pricelist, '#,###') + ''')\">'"
                   + " + NoStock + "
                   + "'</a>'"
                   ;
            }
            else
            {
                nav = "'<a href=\"javascript:call(''' + NoStock + ''')\">'"
                    + " + NoStock + "
                    + "'</a>'"
                    ;
            }

            string strSql = "SELECT "
                + nav
                + " AS NoStock"
                + ",NoUnit"
                + ",Luas"
                + ",FORMAT(LuasSG, '#,##0') AS LuasSG"
                + ",FORMAT(LuasNett, '#,##0') AS LuasNett"
                + ",FORMAT(PriceList, '#,##0') AS PL"
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
