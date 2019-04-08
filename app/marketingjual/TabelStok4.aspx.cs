using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.AspNet.SignalR;

namespace ISC064.MARKETINGJUAL
{
	public partial class TabelStok4 : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Fill();
			}
		}

		private void Fill()
		{
			Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);
            lanjutan.Visible = false;            
            DataTable rs = Db.Rs("SELECT Project,RefSkema FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

			asp.HRef = "javascript:openPopUp('PrintSP.aspx?NoKontrak=" + NoKontrak+"&project="+rs.Rows[0]["Project"]+"','920','650')";

            if (NoTTS == "0")
                atts.Visible = false;
            else
                atts.HRef = "javascript:openPopUp('PrintTTS.aspx?NoTTS=" + NoTTS + "&project=" + rs.Rows[0]["Project"] + "','920','650')";
            
            aDiskon.HRef = "KontrakDiskon.aspx?NoKontrak=" + NoKontrak;
            if(rs.Rows[0]["RefSkema"].ToString() == "0")
            {
                lanjutan.Visible = true;
            }
            aReset.HRef = "TagihanReset.aspx?NoKontrak=" + NoKontrak;
            aCustom.HRef = "TagihanCustom.aspx?NoKontrak=" + NoKontrak;
            //aTagihan.HRef = "TagihanCustom.aspx?NoKontrak=" + NoKontrak;
        }

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
			}
		}

        private string NoTTS
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoTTS"]);
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
