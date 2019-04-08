using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	 public partial class PrintST1 : System.Web.UI.Page
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
            PrintSTTemplate uc = (PrintSTTemplate)Page.LoadControl("PrintSTTemplate.ascx");
            uc.NoTunggakan = NoTunggakan;
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

        private string NoTunggakan
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoTunggakan"]);
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
