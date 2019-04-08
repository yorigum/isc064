using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064
{
	public partial class Calendar : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Cf.BindTahun(tahun);
				
				if(Request.QueryString["date"]==null)
				{
					HariIni();
					cal.SelectedDate = DateTime.Today;
				}
				else
				{
					try
					{
						DateTime d = Convert.ToDateTime(Request.QueryString["date"]);
						
						cal.VisibleDate = d;
						cal.SelectedDate = d;

						tahun.Items.Add(new ListItem(d.Year.ToString()));
						tahun.SelectedValue = d.Year.ToString();
						bulan.SelectedValue = d.Month.ToString();
					}
					catch{}
				}
			}
		}

		private void HariIni()
		{
			cal.VisibleDate = DateTime.Today;
			tahun.SelectedValue = DateTime.Today.Year.ToString();
			bulan.SelectedValue = DateTime.Today.Month.ToString();
		}

		protected void hariini_Click(object sender, System.EventArgs e)
		{
			HariIni();
		}
		protected void tahun_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Reload();
			Js.Focus(this,tahun);
		}
		protected void bulan_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Reload();
			Js.Focus(this,bulan);
		}

		private void Reload()
		{
			cal.VisibleDate = Convert.ToDateTime(
				bulan.SelectedValue + "/1/" + tahun.SelectedValue
				);
		}

		protected void cal_SelectionChanged(object sender, System.EventArgs e)
		{
			if(Request.QueryString["ctrl"]!=null)
			{
				string ctrl = Request.QueryString["ctrl"];
				
				RegisterStartupScript(
					"selectCal"
					, "<script language='javascript'>"
                    + " window.opener.getCalendar('" + ctrl + "','" + Cf.Day(cal.SelectedDate) + "');"
					+ " window.close();"
					+ "</script>"
					);
			}
			else
			{
				RegisterStartupScript(
					"selectCal"
					, "<script language='javascript'>"
					+ " window.close();"
					+ "</script>"
					);
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
