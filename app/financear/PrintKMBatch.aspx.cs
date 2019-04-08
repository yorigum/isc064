using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class PrintKMBatch : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				InitForm();
				Js.Focus(this, print);
			}
		}

		private void InitForm()
		{
			dari.Text = Cf.Day(DateTime.Today);
			sampai.Text = Cf.Day(DateTime.Today);
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(dari))
			{
				x = false;
				if(s=="") s = dari.ID;
				daric.Text = "Tanggal";
			}
			else
				daric.Text = "";

			if(!Cf.isTgl(sampai))
			{
				x = false;
				if(s=="") s = sampai.ID;
				sampaic.Text = "Tanggal";
			}
			else
				sampaic.Text = "";

			if(!x && s!="")
			{
				RegisterStartupScript("err"
					,"<script language='javascript'>document.getElementById('"+s+"').select()</script>");
			}

			return x;
		}

		protected void print_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				reprint.Visible = false;
				Js.AutoPrint(this);

				Fill();
			}
		}

		private void Fill()
		{
			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			if(Dari>Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = x;
			}

			DataTable rs = Db.Rs("SELECT "
				+ " NoVoucher"
				+ " FROM MS_KASMASUK"
				+ " WHERE CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
				+ " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				Print((int)rs.Rows[i]["NoVoucher"]);

				if(i!=rs.Rows.Count-1)
				{
					Label pb = new Label();
					pb.Text = "<div style='page-break-after:always'></div>";
					list.Controls.Add(pb);
				}
			}
		}

		private void Print(int NoVoucher)
		{
			//increment
			Db.Execute("UPDATE MS_KASMASUK SET PrintKM = PrintKM + 1 WHERE NoVoucher = " + NoVoucher);

			//Logfile
			DataTable rs = Db.Rs("SELECT "
				+ " NoVoucher AS [No. Voucher]"
				+ ",CONVERT(varchar,Tgl,106) AS Tgl"
				+ ",Acc AS [No. Account]"
				+ ",CaraBayar AS [Cara Bayar]"
				+ ",AlatBayar AS [Alat Bayar]"
				+ ",DiterimaDari AS [Diterima Dari]"
				+ ",Keterangan"
				+ ",Nilai"
				+ " FROM MS_KASMASUK WHERE NoVoucher = " + NoVoucher);

			Db.Execute("EXEC spLogKasMasuk"
				+ " 'P-KM'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Cf.LogCapture(rs) + "'"
				+ ",'" + NoVoucher.ToString().PadLeft(5,'0') + "'"
				);

			//Template
			PrintKMTemplate uc = (PrintKMTemplate) Page.LoadControl("PrintKMTemplate.ascx"); 
			uc.NoVoucher = NoVoucher.ToString();
			list.Controls.Add(uc);
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
