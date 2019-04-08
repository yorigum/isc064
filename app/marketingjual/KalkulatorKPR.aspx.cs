using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KalkulatorKPR : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			months.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			months.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			months.Attributes["onblur"] = "CalcBlur(this);";

			rate.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			rate.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			rate.Attributes["onblur"] = "CalcBlur(this);";

			loan.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			loan.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			loan.Attributes["onblur"] = "CalcBlur(this);";
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
