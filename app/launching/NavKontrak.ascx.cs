namespace ISC064.LAUNCHING
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class NavKontrak : System.Web.UI.UserControl
	{
		
		public string aktif;
		public string Aktif
		{
			set{aktif = value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				edit.HRef = "KontrakEdit.aspx?NoKontrak="+NoKontrak;
				jurnal.HRef = "JurnalKontrak.aspx?NoKontrak="+NoKontrak;
				jtagih.HRef = "KontrakJadwalTagihan.aspx?NoKontrak="+NoKontrak;
				jkom.HRef = "KontrakJadwalKomisi.aspx?NoKontrak="+NoKontrak;
				//proses.HRef = "KontrakProses.aspx?NoKontrak=" + NoKontrak;
                ppjb.HRef = "KontrakProsesPPJB.aspx?NoKontrak=" + NoKontrak;
                //bkt.HRef = "KontrakKerjaTambah.aspx?NoKontrak=" + NoKontrak;

				switch(aktif)
				{
					case "1":
						div1.Attributes["class"] = "tabaktif";
						break;
					case "2":
						div2.Attributes["class"] = "tabaktif";
						div2.Attributes["style"] = "left:123";
						break;
					case "3":
						div3.Attributes["class"] = "tabaktif";
						div3.Attributes["style"] = "left:243";
						break;
                    case "4":
                        div4.Attributes["class"] = "tabaktif";
                        div4.Attributes["style"] = "left:363";
                        break;
                    case "5":
                        div5.Attributes["class"] = "tabaktif";
                        div5.Attributes["style"] = "left:483";
                        break;
                    //case "6":
                    //    div6.Attributes["class"] = "tabaktif";
                    //    div6.Attributes["style"] = "left:603";
                    //    break;
				}
			}
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
