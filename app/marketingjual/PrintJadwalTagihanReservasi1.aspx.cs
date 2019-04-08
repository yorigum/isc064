using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class PrintJadwalTagihanReservasi : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetTemplate();
			
			if(!Page.IsPostBack)
			{
				Fill();
			}
		}

		private void SetTemplate()
		{
            PrintJadwalTagihanReservasiTemplate uc = (PrintJadwalTagihanReservasiTemplate)Page.LoadControl("PrintJadwalTagihanReservasiTemplate.ascx");
            uc.NoReservasi = NoReservasi;
			list.Controls.Add(uc);
		}

		private void Fill()
		{
            Tampil();
		}

		private void Tampil()
		{
			list.Visible = true;
		}

		private string NoReservasi
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoReservasi"]);
			}
		}
	}
}
