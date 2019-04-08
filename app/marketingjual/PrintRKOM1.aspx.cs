using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class PrintRKOM1 : System.Web.UI.Page
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
			PrintRKOMTemplate uc = (PrintRKOMTemplate) Page.LoadControl("PrintRKOMTemplate.ascx"); 
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

		protected void btn_Click(object sender, System.EventArgs e)
		{
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
