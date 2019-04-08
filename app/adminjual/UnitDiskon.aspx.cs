using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class UnitDiskon : System.Web.UI.Page
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

		private void Fill()
		{
            int StatusDiskon = Db.SingleInteger("SELECT DiscountAuthorized FROM MS_UNIT WHERE NoStock = '"+NoStock+"' ");

            if (StatusDiskon == 1)
            {
                statusdiskon.Text = "Authorized";
            }
            else
            {
                statusdiskon.Text = "Unauthorized";
            }
		}

        protected void autorisasi_click(object sender, System.EventArgs e)
        {
            Db.Execute("UPDATE MS_UNIT SET DiscountAuthorized = '1' WHERE NoStock = '"+NoStock+"' ");

            Response.Redirect("UnitDiskon.aspx?done=1&NoStock=" + NoStock);
        }

        protected void deautorisasi_click(object sender, System.EventArgs e)
        {
            Db.Execute("UPDATE MS_UNIT SET DiscountAuthorized = '0' WHERE NoStock = '" + NoStock + "' ");

            Response.Redirect("UnitDiskon.aspx?done=1&NoStock=" + NoStock);
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Autorisasi Diskon Berhasil...";
            }
        }

		private string NoStock
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoStock"]);
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
