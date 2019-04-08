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
	public partial class PrintTTSMulti : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
			{
				a.Visible = true;
				b.Visible = false;
				Fill();
			}
		}

		private void Fill()
		{
			if(Request.QueryString["from"] != null && Request.QueryString["to"] != null)
			{
				tbDari.Text = Cf.Pk(Request.QueryString["from"]);
				tbSampai.Text = Cf.Pk(Request.QueryString["to"]);
			}
		}

		private void SetTemplate(string NoTTS)
		{
			PrintTTSTemplate uc = (PrintTTSTemplate) Page.LoadControl("PrintTTSTemplate.ascx"); 
			uc.NoTTS = NoTTS;
			list.Controls.Add(uc);
		}

		private bool Valid()
		{
			bool x = true;

			if(Cf.isEmpty(tbDari))
			{
				x = false;
				
				lblDariErr.Text = "Kosong";
			}else
				lblDariErr.Text = "";

			if(Cf.isEmpty(tbSampai))
			{
				x = false;

				lblSampaiErr.Text = "Kosong";
			}else
				lblSampaiErr.Text = "";

			return x;
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Valid())
			{
				a.Visible = false;
				b.Visible = true;

				int intFrom = Convert.ToInt32(tbDari.Text);
				int intTo = Convert.ToInt32(tbSampai.Text);

				if(intFrom > intTo)
				{
					int x = intTo;
					intTo = intFrom;
					intFrom = x;
				}
			
				for(int i = intFrom; i <= intTo; i++)
				{
					SetTemplate(i.ToString());

					if(i <= (intTo - 1))
					{
						Label l = new Label();
						l.Text = "<div style='page-break-after: always;'></div>";
						list.Controls.Add(l);
					}
				}

				Js.AutoPrint(this);
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
