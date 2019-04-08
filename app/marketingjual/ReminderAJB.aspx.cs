using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class ReminderAJB : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            Cf.SetGrid(tb);
			Fill();

			if(Request.QueryString["done"]!=null)
			{
				feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
					+ "<a href=\"javascript:popEditKontrak('"+Request.QueryString["done"]+"')\">"
					+ "AJB Berhasil..."
					+ "</a>";
			}
		}

		private void Fill()
		{
            string nav = "'<a href=\"KontrakAJB.aspx?NoKontrak='''+ NoKontrak +'''\">' + NoKontrak + '</a>'";

			string strSql = "SELECT "
                + nav
                + " AS Kontrak"
                + ",NoUnit AS Unit"
                + ",MS_CUSTOMER.Nama AS Customer"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Agent"
                + ",PersenLunas + '<br>' AS Pelunasan"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_AGENT"
				+ " ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
				+ " WHERE 1=1"
				+ " AND MS_KONTRAK.AJB = 'B' AND PersenLunas >= 100 AND MS_KONTRAK.Status = 'A'"
				+ " ORDER BY MS_KONTRAK.NoKontrak";
			
			DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
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

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}
