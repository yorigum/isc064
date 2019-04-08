using System;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class DaftarGimmick : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cf.SetGrid(tb);
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);

                Fill();
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

            string NoGimmick = "";


            NoGimmick = "'<a href=\"javascript:call(''' + CONVERT(VARCHAR(10), Nama) + ''')\">"
                + "' + Nama + '"
                + " (' + CONVERT(VARCHAR(10), ID) + ')"
                + "</a>' AS NoGimmick"
                ;


            string strSql = "SELECT "
                + NoGimmick
                + " FROM REF_TIPE_GIMMICK "
                + " WHERE Project='" + project.SelectedValue + "' AND Nama"
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + addq
                + " ORDER BY Nama, ID";

            DataTable rs = new DataTable();
            Db.Fill(rs, strSql);

            tb.DataSource = rs;
            tb.DataBind();
        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}