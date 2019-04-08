using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class PrintMEMO : System.Web.UI.Page
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
			PrintMEMOTemplate uc = (PrintMEMOTemplate) Page.LoadControl("PrintMEMOTemplate.ascx");
            uc.NoMEMO = NoMEMO;
            uc.Project = Project;
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

		private string NoMEMO
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoMEMO"]);
			}
		}
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }
    }
}
