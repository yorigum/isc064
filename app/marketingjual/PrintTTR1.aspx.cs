using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class PrintTTR1 : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetTemplate();
			
			if(!Page.IsPostBack)
			{
				Fill();
			}
		}

		private void SetTemplate()
		{
			PrintTTRTemplate uc = (PrintTTRTemplate) Page.LoadControl("PrintTTRTemplate.ascx"); 
			uc.NoTTR = NoTTR;
			list.Controls.Add(uc);
		}

		private void Fill()
		{
            Tampil();
		}

		private void Tampil()
		{
			list.Visible = true;
		}

		private string NoTTR
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoTTR"]);
			}
		}
	}
}
