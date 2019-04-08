using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class KasKeluarEdit : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoVoucher");

			if(!Act.Sec("ED:"+Request.PhysicalPath))
			{
				ok.Enabled = false;
				save.Enabled = false;
			}

			if(!Page.IsPostBack)
			{
				fillacc();
				Fill();

				nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
				nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
				nilai.Attributes["onblur"] = "CalcBlur(this);";
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
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Edit Berhasil...";
			}
		}

		private void Fill()
		{
			btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_KASKELUAR_LOG&Pk="+NoVoucher.ToString().PadLeft(5,'0')+"'";
			
			printKK.HRef = "PrintKK.aspx?NoVoucher="+NoVoucher;

			string strSql = "SELECT * "
				+ " FROM MS_KASKELUAR WHERE NoVoucher = " + NoVoucher;
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				no.InnerHtml = NoVoucher.ToString().PadLeft(5,'0');
				tglinput.Text = Cf.Date(rs.Rows[0]["TglInput"]);
				tgl.Text = Cf.Day(rs.Rows[0]["Tgl"]);
				
				acc.Items.Add(new ListItem("Tidak berubah: " + rs.Rows[0]["Acc"].ToString(), rs.Rows[0]["Acc"].ToString()));
				acc.SelectedValue = rs.Rows[0]["Acc"].ToString();

				try 
				{
					carabayar.SelectedValue = rs.Rows[0]["CaraBayar"].ToString();
				}
				catch 
				{
					carabayar.SelectedIndex = 0;
				}
				
				alatbayar.Text = rs.Rows[0]["AlatBayar"].ToString();
				dibayarkepada.Text = rs.Rows[0]["DibayarKepada"].ToString();
				keterangan.Text = rs.Rows[0]["Keterangan"].ToString();

				nilai.Text = Cf.Num(rs.Rows[0]["Nilai"]);
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
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					+ "2. Nilai harus berupa angka.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool Save()
		{
			if(valid())
			{
				DateTime Tgl = Convert.ToDateTime(tgl.Text);
				string Acc = Cf.Str(acc.SelectedValue);
				string CaraBayar = Cf.Str(carabayar.SelectedValue);
				string AlatBayar = Cf.Str(alatbayar.Text);
				string DibayarKepada = Cf.Str(dibayarkepada.Text);
				string Keterangan = Cf.Str(keterangan.Text);
				decimal Nilai = Convert.ToDecimal(nilai.Text);

				DataTable rs = Db.Rs("SELECT "
					+ " NoVoucher AS [No.]"
					+ " FROM MS_KASKELUAR"
					+ " WHERE NoVoucher = " + NoVoucher
					);

				DataTable rsBef = Db.Rs("SELECT "
					+ " NoVoucher AS [No. Voucher]"
					+ ",CONVERT(varchar,Tgl,106) AS Tgl"
					+ ",Acc AS [No. Account]"
					+ ",CaraBayar AS [Cara Bayar]"
					+ ",AlatBayar AS [Alat Bayar]"
					+ ",DibayarKepada AS [Dibayar Kepada]"
					+ ",Keterangan"
					+ ",Nilai"
					+ " FROM MS_KASKELUAR"
					+ " WHERE NoVoucher = " + NoVoucher
					);

				Db.Execute("EXEC spKasKeluarEdit"
					+ "  " + NoVoucher
					+ ",'" + Tgl + "'"
					+ ",'" + Acc + "'"
					+ ",'" + CaraBayar + "'"
					+ ",'" + AlatBayar + "'"
					+ ",'" + DibayarKepada + "'"
					+ ",'" + Keterangan + "'"
					+ ", " + Nilai
					);

				DataTable rsAft = Db.Rs("SELECT "
					+ " NoVoucher AS [No. Voucher]"
					+ ",CONVERT(varchar,Tgl,106) AS Tgl"
					+ ",Acc AS [No. Account]"
					+ ",CaraBayar AS [Cara Bayar]"
					+ ",AlatBayar AS [Alat Bayar]"
					+ ",DibayarKepada AS [Dibayar Kepada]"
					+ ",Keterangan"
					+ ",Nilai"
					+ " FROM MS_KASKELUAR"
					+ " WHERE NoVoucher = " + NoVoucher
					);

				//Logfile
				string ketlog = Cf.LogCapture(rs)
					+ Cf.LogCompare(rsBef,rsAft);

				Db.Execute("EXEC spLogKasKeluar"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + ketlog + "'"
					+ ",'" + NoVoucher.ToString().PadLeft(5,'0') + "'"
					);

				return true;
			}
			else
				return false;
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Save()) Js.Close(this);
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(Save()) Response.Redirect("KasKeluarEdit.aspx?done=1&NoVoucher="+NoVoucher);
		}

		private string NoVoucher
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoVoucher"]);
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
