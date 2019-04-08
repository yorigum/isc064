using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{
	public partial class PrintPPJB1 : System.Web.UI.Page
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
			PrintPPJBTemplate uc = (PrintPPJBTemplate) Page.LoadControl("PrintPPJBTemplate.ascx"); 
			uc.NoKontrak = NoKontrak;
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

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
			}
		}
	}
}
