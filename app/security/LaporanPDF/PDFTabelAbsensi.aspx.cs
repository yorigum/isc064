using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY.Laporan
{
	public partial class TabelAbsensi : System.Web.UI.Page
	{
        private string NoCustomer { get { return (Request.QueryString["NoCustomer"]); } }
        private string Aktivitas { get { return (Request.QueryString["aktivitas"]); } }
        private string User { get { return (Request.QueryString["user"]); } }
        private string AttachmentID { get { return Request.QueryString["id"]; } }
        private string SecLevel { get { return (Request.QueryString["seclevel"]); } }
        private string IP { get { return (Request.QueryString["ip"]); } }
		protected void Page_Load(object sender, System.EventArgs e)
		{
            Report();
		}



		private void Report()
		{
			param.Visible = false;
			rpt.Visible = true;

			Header();
			Fill();
		}

		private void Header()
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


			Rpt.Judul(x, comp, judul);

			Rpt.SubJudul(x
				, "Tanggal Masuk : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
				);

			Rpt.SubJudul(x
				, "User : " + User
				);

			Rpt.SubJudul(x
				, "Security Level : " + SecLevel
				);

			Rpt.SubJudul(x
				, "IP Address : " + IP
				);

            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

		private void Fill()
		{
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

			if(Dari>Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = x;
			}

            string UserID = "";
            if (User != "SEMUA")
                UserID = " AND UserID = '" + User + "'";

            string nSecLevel = "";
            if (SecLevel != "SEMUA")
                nSecLevel = " AND SecLevel = '" + SecLevel + "'";

            string nIP = "";
            if (IP != "0" && IP != "SEMUA")
                nIP = " AND IP = '" + IP + "'";


			string strSql = "SELECT "
				+ " LogID"
				+ ",TglLogin"
				+ ",TglLogout"
				+ ",UserID"
				+ ",Nama"
				+ ",SecLevel"
				+ ",IP"
				+ " FROM LOGIN"
				+ " WHERE 1=1 "
				+ " AND CONVERT(varchar,TglLogin,112) >= '" + Cf.Tgl112(Dari) + "'"
				+ " AND CONVERT(varchar,TglLogin,112) <= '" + Cf.Tgl112(Sampai) + "'"
				+ UserID
				+ nSecLevel
				+ nIP
				+ " ORDER BY LogID";
			
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;

				c = new TableCell();
				c.Text = rs.Rows[i]["LogID"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Date(rs.Rows[i]["TglLogin"]);
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Date(rs.Rows[i]["TglLogout"]);
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["UserID"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Nama"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["SecLevel"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["IP"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

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
