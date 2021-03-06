using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class UnitPeta : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Fill();
		}

		private void Fill()
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			string path = Request.PhysicalApplicationPath + "FP\\Base\\";
			string[] file = System.IO.Directory.GetFiles(path,"*.jpg");

			if(file.GetUpperBound(0)==-1)
			{
				x.Append("<br>Floor plan belum tersedia.");
			}

			for(int i=0;i<=file.GetUpperBound(0);i++)
			{
				if(!Response.IsClientConnected) break;

				string f = System.IO.Path.GetFileNameWithoutExtension(file[i]);

				x.Append("<li>"
					+ "<a href='UnitPetaDetil.aspx?f="+f+"' style='font:bold 10 pt'>"+f+"</a>"+"</li>");
			}

			list.InnerHtml = x.ToString();
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
