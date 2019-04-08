using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakDaftar3 : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Fill();
			}

			FeedBack();
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Proses Berhasil...";
			}
		}

		private void Fill()
		{
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            asp.HRef = "javascript:openPopUp('PrintSP.aspx?NoKontrak=" + NoKontrak + "&project=" + Project + "','920','650')";
            aDiskon.HRef = "KontrakDiskon.aspx?NoKontrak="+NoKontrak;
			aRt.HRef = "TagihanReset.aspx?NoKontrak="+NoKontrak;
			aTagihan.HRef = "TagihanCustom.aspx?NoKontrak="+NoKontrak;
			//aKom.HRef = "AgentKomisiGen.aspx?NoKontrak="+NoKontrak;

			Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);
		}

		private string NoKontrak
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
