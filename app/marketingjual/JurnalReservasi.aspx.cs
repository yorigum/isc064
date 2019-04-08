using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class JurnalReservasi : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoReservasi");

			Fill();
			FeedBack();

			Js.Confirm(this, "Simpan aktivitas baru ke dalam jurnal?");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Simpan Jurnal Berhasil...";
			}
		}

		private void Fill()
		{
			string strSql = "SELECT *"
				+ ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = MS_RESERVASI_JURNAL.UserID) AS Nama"
				+ " FROM MS_RESERVASI_JURNAL"
				+ " WHERE NoReservasi = " + NoReservasi
				+ " ORDER BY JurnalID";
			
			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Jurnal untuk reservasi tersebut masih kosong.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Time(rs.Rows[i]["Tgl"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["UserID"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Nama"].ToString();
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Ket"].ToString();
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			string Ket = Cf.Str(akt.SelectedValue) + "<br>" + Cf.Str(baru.Text);
			
			Db.Execute("EXEC spJurnalReservasi "
				+ " '" + Act.UserID + "'"
				+ ", " + NoReservasi
				+ ",'" + Ket + "'"
				);

			Response.Redirect("JurnalReservasi.aspx?done=1&NoReservasi="+NoReservasi);
		}

		private string NoReservasi
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoReservasi"]);
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
