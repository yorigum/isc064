using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class ReminderIdle : System.Web.UI.Page
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
            string nav = "'<a onclick=\"popEditUser('''+UserID+''')\">' + Nama + '</a>'";
			string strSql = "SELECT "
                + nav
                + " AS Nama"
                + ",UserID AS Kode"
                + ",SecLevel"
                + ",CONVERT(VARCHAR,TglLogin,106) AS Login"
                + " FROM USERNAME"
				+ " WHERE CONVERT(varchar,DATEADD(yy,1,TglLogin),112) <= CONVERT(varchar,getdate(),112) AND Status = 'A'"
				+ " ORDER BY Nama,UserID";
			
			DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
            if (tb.PageCount == 0) kosong.InnerText = "Reminder untuk topik diatas masih kosong.";
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
