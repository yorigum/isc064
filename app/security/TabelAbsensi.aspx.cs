using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class TabelAbsensi : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Js.Focus(this,date);

			if(!Page.IsPostBack)
			{
				date.Text = Cf.Day(DateTime.Today);
			}
		}

		private bool valid()
		{
			bool x = true;

			if(!Cf.isTgl(date))
			{
				x = false;
				datec.Text = "Tanggal";
			}
			else
				datec.Text = "";

			return x;
		}

		protected void display_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				SetHeader();
				Fill();
			}
		}

		private void SetHeader()
		{
			//Header
			TableRow r = new TableRow();
			TableCell c = new TableCell();
			c.Text = Cf.Day(date.Text);
			c.Font.Size = 15;
			c.Font.Bold = true;
			c.ColumnSpan = 6;
			r.Cells.Add(c);
			rpt.Rows.Add(r);
		}

		private void Fill()
		{
			DateTime tgl = Convert.ToDateTime(date.Text);
			string strSql = "SELECT TglLogin, TglLogout, UserID, IP, Nama, SecLevel FROM LOGIN"
				+ " WHERE CONVERT(varchar,TglLogin,112) = '" + Cf.Tgl112(tgl) + "'"
				+ " ORDER BY TglLogin";
			
			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Tidak ada catatan absensi untuk tanggal tersebut.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;
				
				c = new TableCell();
				c.Text = Cf.Time(rs.Rows[i]["TglLogin"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Time(rs.Rows[i]["TglLogout"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["UserID"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Nama"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["SecLevel"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["IP"].ToString();
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
