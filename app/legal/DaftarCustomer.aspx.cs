using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class DaftarCustomer : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Js.ConfirmKeyword(this,keyword);

			if(!Page.IsPostBack)
			{
				if(Request.QueryString["status"]==null)
					metode.SelectedIndex = 0;
				else if(Request.QueryString["status"]=="a")
					metode.SelectedIndex = 1;
				else if(Request.QueryString["status"]=="i")
					metode.SelectedIndex = 2;

				if(metode.SelectedIndex!=0) metode.Enabled = false;
			}
		}

		protected void search_Click(object sender, System.EventArgs e)
		{
			Fill();
		}

		private void Fill()
		{
			string addq = "";
			if(metode.SelectedIndex==1)
				addq = " AND Status = 'A'";
			else if(metode.SelectedIndex==2)
				addq = " AND Status = 'I'";

			string customsec = "";
			if(Act.SecLevel=="AG")
				customsec = " AND AgentInput = '"+Cf.Str(Act.UserID)+"'";

			string strSql = "SELECT * "
				+ " FROM MS_CUSTOMER "
				+ " WHERE Nama + NamaBisnis + JenisBisnis + MerekBisnis "
				+ " + Alamat1 + Alamat2 + Alamat3 "
				+ " + Kantor1 + Kantor2 + Kantor3 "
				+ " + NoTelp + NoHP + NoKantor + NoFax + Email "
				+ " + NoKTP + KTP1 + KTP2 + KTP3 + KTP4"
				+ " + UnitLama +  TokoLama + AkteLama + TeleponLama"
				+ " LIKE '%" + Cf.Str(keyword.Text) +"%'"
				+ addq
				+ customsec
				+ " ORDER BY Nama, NoCustomer";

			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Tidak ditemukan data customer dengan keyword diatas.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				string lama = "";
				if(rs.Rows[i]["UnitLama"].ToString()!=""
					|| rs.Rows[i]["TokoLama"].ToString()!=""
					|| rs.Rows[i]["ZoningLama"].ToString()!=""
					|| rs.Rows[i]["GedungLama"].ToString()!=""
					|| rs.Rows[i]["TeleponLama"].ToString()!=""
					|| rs.Rows[i]["AkteLama"].ToString()!=""
					|| (decimal)rs.Rows[i]["LuasLama"]!=0)
				{
					lama = "<div><b>Customer Lama</b></div>"
						+ "<div>Gedung : " + rs.Rows[i]["GedungLama"] + "</div>"
                        + "<div>Unit : " + rs.Rows[i]["UnitLama"]
						+ " ("+Cf.Num(rs.Rows[i]["LuasLama"])+ " M2)</div>"
                        + "<div>Toko : " + rs.Rows[i]["TokoLama"] + "</div>"
                        + "<div>Zoning : " + rs.Rows[i]["ZoningLama"] + "</div>"
                        + "<div>Telepon : " + rs.Rows[i]["TeleponLama"] + "</div>"
                        + "<div>Akte : " + rs.Rows[i]["AkteLama"] + "</div>"
                        ;
				}

				c = new TableCell();
				c.Text = "<a href=\"javascript:call('"+rs.Rows[i]["NoCustomer"]+"')\"><b>"
					+ rs.Rows[i]["Nama"].ToString()
					+ " (" + rs.Rows[i]["NoCustomer"].ToString().PadLeft(5,'0') + ")"
					+ "</b></a><br /><br />"
					+ "<div>Tipe : " + rs.Rows[i]["TipeCs"] + "</div>"
					+ "<div>Nama Bisnis : " + rs.Rows[i]["NamaBisnis"] + "</div>"
					+ "<div>Jenis Bisnis : " + rs.Rows[i]["JenisBisnis"] + "</div>"
					+ "<div>Merek Bisnis : " + rs.Rows[i]["MerekBisnis"] + "</div>"
					+ "<br /><br />"
					+ "<div>Telp : " + rs.Rows[i]["NoTelp"] + "</div>"
                    + "<div>HP : " + rs.Rows[i]["NoHP"] + "</div>"
                    + "<div>Kantor : " + rs.Rows[i]["NoKantor"] + "</div>"
                    + "<div>Fax : " + rs.Rows[i]["NoFax"] + "</div>"
                    + "<div>Email : " + rs.Rows[i]["Email"] + "</div>"
                    + lama
					;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "Surat Menyurat :"
					+ "<div style=padding-left:20>"
					+ rs.Rows[i]["Alamat1"]
					+ "<br />" + rs.Rows[i]["Alamat2"]
					+ "<br />" + rs.Rows[i]["Alamat3"]
					+ "</div>"
					
					+ "Kantor :"
					+ "<div style=padding-left:20>"
					+ rs.Rows[i]["Kantor1"]
					+ "<br>" + rs.Rows[i]["Kantor2"]
					+ "<br>" + rs.Rows[i]["Kantor3"]
					+ "</div>"
					
					+ "KTP :"
					+ "<div style=padding-left:20>"
					+ rs.Rows[i]["NoKTP"]
					+ "<br>" + rs.Rows[i]["KTP1"]
					+ "<br>" + rs.Rows[i]["KTP2"]
					+ "<br>" + rs.Rows[i]["KTP3"]
					+ "<br>" + rs.Rows[i]["KTP4"]
					+ "</div>"
					;
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
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
