using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	public partial class DaftarPJT : System.Web.UI.Page
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
            string nav = "'<a href=\"javascript:call('''+ CONVERT(varchar(50),A.NoPJT) +''')\">' + A.NoPJT + '</a>'";
            string strSql = "SELECT"
                + nav
                + " AS PJT"
                + ",CONVERT(VARCHAR, TglJT, 106) AS Tgl"
                + ",A.Customer + '<br/>Telp :' + A.NoTelp  AS Customer"
                + ",A.Tipe + ' No : ' + A.Ref + '<br/>Unit :' + A.Unit AS Keterangan "
                + ",FORMAT(A.Total,'#,###') AS Nilai"
                + ",B.NamaProject AS Project"
                + " FROM MS_PJT A INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK B ON A.Ref = B.NoKontrak"
                + " WHERE CONVERT(varchar,NoPJT) + Ref + Unit + Customer + NoTelp + Alamat1 + Alamat2 + Alamat3 "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND B.Project = '" + project.SelectedValue + "'"
                + " ORDER BY NoPJT";

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
