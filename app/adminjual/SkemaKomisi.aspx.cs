using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class SkemaKomisi : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

            FillTable();

		}


        //==========================================
        //  tipe Skema internal = I / External E
        //==========================================
		private void FillTable()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			
			//Isi skema aktif
			DataTable rs = Db.Rs("SELECT * FROM REF_SKOM WHERE Status = 'A' and Tipe='I'");
			Rpt.NoData(sb, rs, "<font style='font:8pt'>Tidak terdapat skema komisi dengan status aktif.</font>");
			
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				sb.Append("<li>"
					+ "<a href=\"javascript:popSkom('" + rs.Rows[i]["Nomor"].ToString() + "')\">"
					+ rs.Rows[i]["Nama"] + " ("+rs.Rows[i]["Nomor"].ToString().PadLeft(3,'0')+")"
					+ "</a>"
					+ "</li>"
					);
			}

			aktifIntern.InnerHtml = sb.ToString();
			
			//Isi skema inaktif
			sb = new System.Text.StringBuilder();
			rs = Db.Rs("SELECT * FROM REF_SKOM WHERE Status = 'I' and Tipe='I'");
			Rpt.NoData(sb, rs, "<font style='font:8pt'>Tidak terdapat skema komisi dengan status inaktif.</font>");
			
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				sb.Append("<li>"
					+ "<a href=\"javascript:popSkom('" + rs.Rows[i]["Nomor"].ToString() + "')\">"
					+ rs.Rows[i]["Nama"].ToString() + " (" + rs.Rows[i]["Nomor"].ToString().PadLeft(3,'0') + ")"
					+ "</a>"
					+ "</li>"
					);
			}

            inaktifIntern.InnerHtml = sb.ToString();


            //Isi skema Aktif External
            sb = new System.Text.StringBuilder();
            rs = Db.Rs("SELECT * FROM REF_SKOM WHERE Status = 'A' and Tipe='E'");
            Rpt.NoData(sb, rs, "<font style='font:8pt'>Tidak terdapat skema komisi dengan status aktif.</font>");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                sb.Append("<li>"
                    + "<a href=\"javascript:popSkom2('" + rs.Rows[i]["Nomor"].ToString() + "')\">"
                    + rs.Rows[i]["Nama"] + " (" + rs.Rows[i]["Nomor"].ToString().PadLeft(3, '0') + ")"
                    + "</a>"
                    + "</li>"
                    );
            }

            aktifExtern.InnerHtml = sb.ToString();

            //Isi skema inaktif External
            sb = new System.Text.StringBuilder();
            rs = Db.Rs("SELECT * FROM REF_SKOM WHERE Status = 'I' and Tipe='E'");
            Rpt.NoData(sb, rs, "<font style='font:8pt'>Tidak terdapat skema komisi dengan status inaktif.</font>");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                sb.Append("<li>"
                    + "<a href=\"javascript:popSkom2('" + rs.Rows[i]["Nomor"].ToString() + "')\">"
                    + rs.Rows[i]["Nama"].ToString() + " (" + rs.Rows[i]["Nomor"].ToString().PadLeft(3, '0') + ")"
                    + "</a>"
                    + "</li>"
                    );
            }

            inaktifExtern.InnerHtml = sb.ToString();
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
        protected void RegSkemaIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Skom.aspx");
        }
        protected void RegSkemaEx_Click(object sender, EventArgs e)
        {
            Response.Redirect("Skom2.aspx");
        }
}
}
