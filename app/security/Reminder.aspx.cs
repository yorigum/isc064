using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class Reminder : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			countKosong.Text = Db.SingleInteger(
				"SELECT COUNT(*) FROM PAGE WHERE (SELECT COUNT(*) FROM PAGESEC WHERE Halaman = PAGE.Halaman) = 0"
				).ToString();

			countPass.Text = Db.SingleInteger(
				"SELECT COUNT(*) FROM USERNAME WHERE GantiPass = 1 AND Status = 'A'"
				).ToString();

			countIdle.Text = Db.SingleInteger(
				"SELECT COUNT(*) FROM USERNAME WHERE CONVERT(varchar,DATEADD(yy,1,TglLogin),112) <= CONVERT(varchar,getdate(),112) AND Status = 'A'"
				).ToString();
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
	}
}
