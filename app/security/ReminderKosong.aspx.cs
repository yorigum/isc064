using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class ReminderKosong : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            Cf.SetGrid(tb);
			Fill();
		}

		private void Fill()
		{
            string namaFile = "+Halaman+";
            int index = namaFile.LastIndexOf("\\");

            string nav = "'<a style=font:8pt href=\"javascript:popHalaman('''+REPLACE(Halaman,'\\','\\\\')+''')\">' +(select right(Halaman, charindex('\\', reverse(Halaman) + '\\') - 1)) + '</a>'";
			string strSql = "SELECT " 
                + nav
                + " AS Halaman"
                + ",CONVERT(VARCHAR,TglInput,106) AS Tanggal"
                + ",Modul"
                + ",Nama AS Keterangan"
                + " FROM PAGE"
				+ " WHERE (SELECT COUNT(*) FROM PAGESEC WHERE Halaman = PAGE.Halaman) = 0"
				+ " ORDER BY Modul,Nama";
			
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
