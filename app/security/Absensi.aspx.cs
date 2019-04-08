using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class Absensi : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            Cf.SetGrid(tb);
			if(!Page.IsPostBack)
			{
				date.Text = Cf.Day(DateTime.Today);
				Fill();
			}

			if(Request.QueryString["done"]!=null)
			{
				feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
					+ "Kick Berhasil...";
			}
		}

		private void Fill()
		{

			string strSql = "SELECT "
                + "CONVERT(VARCHAR,TglLogin,106) AS Jam"
                + ",UserID AS Kode"
                + ",Nama"
                + ",SecLevel"
                + ",IP"
                + ",'<a href=\"javascript:kick('''+ CONVERT(VARCHAR(10),LogID) +''')\">Kick...</a>' AS Kick"
                + " FROM LOGIN"
				+ " WHERE Status = 'A'"
				+ " AND CONVERT(varchar,TglLogin,112) = CONVERT(varchar,getdate(),112)"
				+ " ORDER BY TglLogin";

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
