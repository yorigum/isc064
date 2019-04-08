using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakSertifikatEdit : System.Web.UI.Page
	{
	
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
				Fill();

			
			jangkawaktu.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			jangkawaktu.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			jangkawaktu.Attributes["onblur"] = "CalcBlur(this);";

			Js.Confirm(this, "Lanjutkan dengan proses edit Sertifikat?");
			Js.Focus(this, ok);
		}

		protected void Fill()
		{
			
			cancel.Attributes["onclick"] = "location.href='KontrakProses.aspx?NoKontrak=" + NoKontrak + "'";

			
			string strSql = "SELECT "
				+ " MS_KONTRAK.*"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";

			DataTable rsHeader = Db.Rs(strSql);

			if(rsHeader.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				nokontrak.Text = rsHeader.Rows[0]["NoKontrak"].ToString();
				unit.Text = rsHeader.Rows[0]["NoUnit"].ToString();
				customer.Text = rsHeader.Rows[0]["Cs"].ToString();
				namaperusahaan.Text = rsHeader.Rows[0]["NamaPerusahaan"].ToString();
				tbTgl.Text = Cf.Day(rsHeader.Rows[0]["TglSertifikat"]);
				tbNoSertifikat.Text = rsHeader.Rows[0]["NoSertifikat"].ToString();
				tglakhir.Text = Cf.Day(rsHeader.Rows[0]["TglAkhir"]);
				jangkawaktu.Text = Cf.Num(rsHeader.Rows[0]["JangkaWaktu"]).ToString();
				statussertifikat.SelectedIndex = Convert.ToInt32(rsHeader.Rows[0]["StatusHak"]);
				tglakhir.Text = Cf.Day(rsHeader.Rows[0]["TglAkhir"]);
				
				if(Convert.ToInt32(rsHeader.Rows[0]["StatusSertifikat"]) == 0)
				{
					rblStatus.SelectedIndex = 0;
					selesai.Visible = false;
					sedangproses.Visible = false;
				}
				else if(Convert.ToInt32(rsHeader.Rows[0]["StatusSertifikat"]) == 1)
				{
					rblStatus.SelectedIndex = 3;
					selesai.Visible = true;
					sedangproses.Visible = false;
				}

				else if(Convert.ToInt32(rsHeader.Rows[0]["StatusSertifikat"]) == 2)
				{
					rblStatus.SelectedIndex = 1;
					selesai.Visible = false;
					sedangproses.Visible = true;
				}

				else if(Convert.ToInt32(rsHeader.Rows[0]["StatusSertifikat"]) == 3)
				{
					rblStatus.SelectedIndex = 2;
					selesai.Visible = true;
					sedangproses.Visible = false;
				}

				if(statussertifikat.SelectedIndex == 0)
				{
					sertifikat1.Visible = true;
					sertifikat2.Visible = true;
				}
				else
				{
					sertifikat1.Visible = false;
					sertifikat2.Visible = false;
				}
			}
		
		}

		private bool valid()
		{

			string s = "";
			bool x = true;

			if(!Cf.isMoney(jangkawaktu))
			{
				x = false;
				if(s == "") s = jangkawaktu.ID;
				jangkawaktuc.Text = "Angka";
			}

			if(!Cf.isTgl(tglakhir))
			{
				x = false;
				if(s == "") s = tglakhir.ID;
				tglakhirc.Text = "Tanggal";
			}
			else
				tglakhirc.Text = " ";


			if(sedangproses.Visible)
			{
				if(!Cf.isTgl(tbTgl1))
				{
					x = false;

					if(s == "")
						s = tbTgl1.ID;

					lblTgl1.Text = "Tanggal";
				}
				else
					lblTgl1.Text = "";

				if(!Cf.isTgl(tbTgl2))
				{
					x = false;

					if(s == "")
						s = tbTgl2.ID;

					lblTgl2.Text = "Tanggal";
				}
				else
					lblTgl2.Text = "";


				if(selesai.Visible)
				{
					if(!Cf.isTgl(tbTgl))
					{
						x = false;

						if(s == "")
							s = tbTgl.ID;

						lblTgl.Text = "Tanggal";
					}
					else
						lblTgl.Text = "";

					
					if(sertifikat2.Visible)
					{
						if(!Cf.isMoney(jangkawaktu))
						{
							x = false;
							if(s == "") s = jangkawaktu.ID;
							jangkawaktuc.Text = "Angka";
						}
						else
							jangkawaktuc.Text = " ";

						
					}
				}
			}

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					+ "2. Nama tidak boleh kosong.\\n"
					+ "3. Luas Lama harus berupa angka.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}
		
		private void Save()
		{
			DataTable rsBef = Db.Rs("SELECT "
				+ "StatusSertifikat AS [Status Sertifikat]"
				+ ", TglSertifikat AS [Tgl. Sertifikat]"
				+ ", NoSertifikat AS [No. Sertifikat]"
				+ ", StatusHak AS [Status Hak]"
				+ ", JangkaWaktu AS [Jangka Waktu]"
				+ ", TglAkhir AS [Tgl. Berakhir Sertifikat]"
				+ " FROM MS_KONTRAK"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				);

			Db.Execute("UPDATE MS_KONTRAK"
				+ " SET StatusSertifikat = " + rblStatus.SelectedValue
				+ ", TglSertifikat = NULL"
				+ ", NoSertifikat = ''"
				+ ", StatusHak = 0"
				+ ", JangkaWaktu = 0"
				+ ", TglAkhir = NULL"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				);

			if(selesai.Visible)
			{
				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET TglSertifikat = '" + Convert.ToDateTime(tbTgl.Text) + "'"
					+ ", NoSertifikat = '" + Cf.Str(tbNoSertifikat.Text) + "'"
					+ ", StatusHak = " + statussertifikat.SelectedValue
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);

				if(sertifikat2.Visible)
				{
					Db.Execute("UPDATE MS_KONTRAK"
							+ " SET JangkaWaktu = " + Convert.ToInt32(jangkawaktu.Text)
							+ ", TglAkhir = '" + Convert.ToDateTime(tglakhir.Text) + "'"
							+ " WHERE NoKontrak = '" + NoKontrak + "'"
							);
				}

			if(atasnama.Visible)
				{
					Db.Execute("UPDATE MS_KONTRAK"
						+ " SET NamaPerusahaan = '" + Cf.Str(namaperusahaan.Text)+ "'"
						+ " WHERE NoKontrak = '" + NoKontrak + "'"
						);
				}
				
			}
			
			if(sedangproses.Visible)
			{
				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET TglPengukuranBidang = '" + Convert.ToDateTime(tbTgl1.Text) + "'"
					+ ", NoPengukuranBidang = '" + Cf.Str(nomorukur.Text) + "'"
					+ ", NoSuratUkur = '" + Cf.Str(nomorsuratukur.Text) + "'"
					+ ", TglSuratUkur = '" + Convert.ToDateTime(tbTgl2.Text) + "'"
					+ ", JumlahBidang = '" + Cf.Str(jumlahbidang.Text) + "'"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);
			}

			DataTable rsAft = Db.Rs("SELECT "
				+ "StatusSertifikat AS [Status Sertifikat]"
				+ ", TglSertifikat AS [Tgl. Sertifikat]"
				+ ", NoSertifikat AS [No. Sertifikat]"
				+ ", StatusHak AS [Status Hak]"
				+ ", JangkaWaktu AS [Jangka Waktu]"
				+ ", TglAkhir AS [Tgl. Berakhir Sertifikat]"
				+ " FROM MS_KONTRAK"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				);

			//Logfile
			string Log = Cf.LogCompare(rsBef, rsAft);

			Db.Execute("EXEC spLogKontrak"
				+ " 'EDIT'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Log + "'"
				+ ",'" + NoKontrak + "'"
				);

			Response.Redirect("KontrakProses.aspx?NoKontrak=" + NoKontrak + "&done=10");
		}
		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(valid())
				Save();
		}

		protected void rblStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(rblStatus.SelectedIndex == 1)
			{
				selesai.Visible = false;
				sedangproses.Visible = true; 
				atasnama.Visible = false;
				tbTgl1.Text = Cf.Day(DateTime.Today);
			}

			else if(rblStatus.SelectedIndex == 3)
			{
				selesai.Visible = true;
				atasnama.Visible = false;
				sedangproses.Visible = false; 
				tbTgl.Text = Cf.Day(DateTime.Today);
			}

			else if(rblStatus.SelectedIndex == 2)
			{
				selesai.Visible = true;
				sedangproses.Visible = false; 
				atasnama.Visible = true;
				tbTgl.Text = Cf.Day(DateTime.Today);
			}

			else
			{
				selesai.Visible = false;
				sedangproses.Visible = false; 
				atasnama.Visible = false;
			}
		}

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
			}
		}

		protected void statussertifikat_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(statussertifikat.SelectedIndex == 0)
			{
				sertifikat1.Visible = true;
				sertifikat2.Visible = true;
			}
			else
			{
				sertifikat1.Visible = false;
				sertifikat2.Visible = false;
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
