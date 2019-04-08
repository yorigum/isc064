using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	/// <summary>
	/// Summary description for PrintTKOM.
	/// </summary>
	public partial class PrintTKOM : System.Web.UI.Page
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
			PrintTKOMTemplate uc = (PrintTKOMTemplate) Page.LoadControl("PrintTKOMTemplate.ascx"); 
			uc.NoKontrak = NoKontrak;
			uc.NoUrut = NoUrut;
            uc.Baris = Baris;
            Db.Execute("UPDATE MS_KOMISI_DETAIL SET PrintTKOM = PrintTKOM + 1 WHERE NoKontrak = '" + NoKontrak + "' AND Baris='" + NoUrut + "' AND BarisTermin='" + Baris + "'");
			list.Controls.Add(uc);
		}

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
			}
		}

		protected string NoUrut
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoUrut"]);
			}
		}

        protected string Baris
        {
            get
            {
                return Cf.Pk(Request.QueryString["Baris"]);
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
