using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

namespace ISC064.MARKETINGJUAL
{
	public partial class UnitLokasi : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Table rpt;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(File.Exists(Request.PhysicalApplicationPath + "FP\\UnitLokasi\\" + Cf.FileSafe(NoUnit) + ".jpg"))
			{
				unit.ImageUrl = "PetaBesar.aspx?f=FP/UnitLokasi/"+Cf.FileSafe(NoUnit)+".jpg";
				noimg.Visible = false;
			}
			else
			{
				unit.Visible = false;
				noimg.Visible = true;
			}
		}

		private string NoUnit
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoUnit"]);
			}
		}

		private string Peta
		{
			get
			{
				return Cf.Pk(Request.QueryString["Peta"]);
			}
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
