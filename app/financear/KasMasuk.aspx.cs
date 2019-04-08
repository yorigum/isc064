using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class KasMasuk : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack) 
			{
				Js.Focus(this, acc);

				nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
				nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
				nilai.Attributes["onblur"] = "CalcBlur(this);";

				tgl.Text = Cf.Day(DateTime.Today);

				fillacc();
			}

			FeedBack();
		}

		private void fillacc() 
		{
			DataTable rs = Db.Rs("SELECT * FROM REF_ACC");
			for(int i=0;i<rs.Rows.Count;i++) 
			{
				string v = rs.Rows[i]["Acc"].ToString();
				string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
				acc.Items.Add(new ListItem(t,v));
			}

			if(rs.Rows.Count==0)
			{
				ok.Enabled = false;
				noacc.Text = "Account belum di-setup dengan baik.";
			}
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "<a href=\"javascript:popEditKasMasuk('"+Request.QueryString["done"]+"')\">"
						+ "Pendaftaran Berhasil..."
						+ "</a>";
			}
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(tgl))
			{
				x = false;
				if(s=="") s = tgl.ID;
				tglc.Text = "Tanggal";
			}
			else
				tglc.Text = "";

			if(!Cf.isMoney(nilai))
			{
				x = false;
				if(s=="") s = nilai.ID;
				nilaic.Text = "Angka";
			}
			else
				nilaic.Text = "";

			if(!x)
			{
				if(!x)
					Js.Alert(
						this
						, "Input Tidak Valid.\\n\\n"
						+ "Aturan Proses :\\n"
						+ "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
						+ "2. Nilai harus berupa angka.\\n"
						, "document.getElementById('"+s+"').focus();"
						+ "document.getElementById('"+s+"').select();"
						);
			}

			return x;
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				DateTime Tgl = Convert.ToDateTime(tgl.Text);
				string Acc = Cf.Str(acc.SelectedValue);
				string CaraBayar = Cf.Str(carabayar.SelectedValue);
				string AlatBayar = Cf.Str(alatbayar.Text);
				string DiterimaDari = Cf.Str(diterimadari.Text);
				string Keterangan = Cf.Str(keterangan.Text);
				decimal Nilai = Convert.ToDecimal(nilai.Text);

				Db.Execute("EXEC spKasMasuk"
					+ " '" + Tgl + "'"
					+ ",'" + Acc + "'"
					+ ",'" + CaraBayar + "'"
					+ ",'" + AlatBayar + "'"
					+ ",'" + DiterimaDari + "'"
					+ ",'" + Keterangan + "'"
					+ ", " + Nilai
					);

				//get nomor terbaru
				int NoVoucher = Db.SingleInteger(
					"SELECT TOP 1 NoVoucher FROM MS_KASMASUK ORDER BY NoVoucher DESC");
				
				DataTable rs = Db.Rs("SELECT "
					+ " NoVoucher AS [No. Voucher]"
					+ ",CONVERT(varchar,Tgl,106) AS Tgl"
					+ ",Acc AS [No. Account]"
					+ ",CaraBayar AS [Cara Bayar]"
					+ ",AlatBayar AS [Alat Bayar]"
					+ ",DiterimaDari AS [Diterima Dari]"
					+ ",Keterangan"
					+ ",Nilai"
					+ " FROM MS_KASMASUK"
					+ " WHERE NoVoucher = " + NoVoucher
					);

				Db.Execute("EXEC spLogKasMasuk"
					+ " 'REGIS'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Cf.LogCapture(rs) + "'"
					+ ",'" + NoVoucher.ToString().PadLeft(5,'0') + "'"
					);
                
                Response.Redirect("KasMasuk.aspx?done=" + NoVoucher);
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
