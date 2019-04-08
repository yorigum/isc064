using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	public partial class DaftarTunggakan : System.Web.UI.Page
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
            string nav = "'<a href=\"javascript:call('''+ CONVERT(varchar(50),a.NoTunggakan) +''')\">' + a.ManualTunggakan + '</a>'";
            string strSql = "SELECT"
                + nav
                + " AS ST"
                + ",CONVERT(VARCHAR,a.TglTunggakan,106) AS Tgl"
                + ",'<font style=font:bold 15pt>' + CONVERT(varchar(50),a.LevelTunggakan) + '</font><br><i>' + "
                + " CASE a.Status "
                + "		WHEN 'A' THEN 'AKTIF' "
                + "		WHEN 'S' THEN 'SETTLED' "
                + "		WHEN 'U' THEN 'UPGRADED' "
                + " END "
                + " AS Status"
                + ",a.Customer + '<br>Telp. ' + a.NoTelp AS Customer"
                + ",a.Tipe + ' No. : ' + a.Ref + '<br>Unit : ' + a.Unit AS Keterangan"
                + ",FORMAT(a.Total,'#,###') AS Nilai"
                + ",b.NamaProject AS Project"
                + " FROM MS_TUNGGAKAN a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b"
                + " ON a.Ref = b.NoKontrak"
                + " WHERE CONVERT(varchar,NoTunggakan) + Ref + Unit + Customer + NoTelp + Alamat1 + Alamat2 + Alamat3 "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND b.Project = '" + project.SelectedValue + "'"
                + " ORDER BY NoTunggakan";

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
