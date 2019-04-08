using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class PrintBKMMulti : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
				Print();
		}

		private void SetTemplate(string NoTTS)
		{
			PrintBKMTemplate uc = (PrintBKMTemplate) Page.LoadControl("PrintBKMTemplate.ascx"); 
			uc.NoTTS = NoTTS;
			list.Controls.Add(uc);
		}

		private void Print()
		{
			int From = 0, To = 0;

			if(Request.QueryString["from"] != null && Request.QueryString["To"] != null)
			{
				From = Convert.ToInt32(Request.QueryString["from"]);
				To = Convert.ToInt32(Request.QueryString["to"]);
			}

			if(From > To)
			{
				int x = To;
				To = From;
				From = x;
			}

			string strSql = "SELECT NoTTS"
				+ " FROM MS_TTS"
				+ " WHERE NoBKM >= " + From
				+ " AND NoBKM <= " + To
				;
			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				SetTemplate(Cf.Pk(rs.Rows[i]["NoTTS"]));

				if(i <= (To - 1))
				{
					Label l = new Label();
					l.Text = "<div style='page-break-after: always;'></div>";
					list.Controls.Add(l);
				}
			}

			Js.AutoPrint(this);
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
