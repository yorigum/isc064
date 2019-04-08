using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
	public partial class PrintJadwalTagihan : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			SetTemplate();
			Js.AutoPrint(this);
		}

		private void SetTemplate()
		{
			PrintJadwalTagihanTemplate uc = (PrintJadwalTagihanTemplate) Page.LoadControl("PrintJadwalTagihanTemplate.ascx"); 
			uc.NoKontrak = NoKontrak;
			list.Controls.Add(uc);
		}

		protected string NoKontrak
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
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
