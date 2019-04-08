using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ISC064.COLLECTION
{
    public partial class DaftarSKL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack) Act.ProjectList(project);
			//Js.ConfirmKeyword(this,keyword);
		}

		protected void search_Click(object sender, System.EventArgs e)
		{
            Cf.SetGrid(tb);
			Fill();
		}

        private void Fill()
        {
            string nav = "'<a href=\"javascript:call('''+ CONVERT(varchar(50),a.NoSKL) +''')\">' + a.NoSKL + '</a>'";
            string strSql = "SELECT"
                         + nav
                         + " AS System"
                         + ",a.NoSKLManual AS Manual"
                         + ",CONVERT(VARCHAR,TglSKL,106) AS Tgl"
                         + ",a.Ref AS Kontrak"
                         + ",(SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NOCUSTOMER IN(SELECT NOCUSTOMER FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NOKONTRAK=a.Ref)) AS Customer"
                         + ",b.NamaProject AS Project"
                         + " FROM MS_SKL a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                         + " WHERE NoSKL + Ref + NoSKLManual "
                         + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                         + " AND b.Project = '" + project.SelectedValue + "'"
                         + " ORDER BY NoSKL";
            DataTable rs = Db.Rs(strSql);
            
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
