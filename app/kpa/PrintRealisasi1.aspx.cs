using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Diagnostics;

namespace ISC064.KPA
{
	public partial class PrintRealisasi1 : System.Web.UI.Page
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
            PrintRealisasiTemplate uc = (PrintRealisasiTemplate) Page.LoadControl("PrintRealisasiTemplate.ascx"); 
			uc.NoRealisasi = NoRealisasi;            
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

        private string NoRealisasi
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoRealisasi"]);
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
