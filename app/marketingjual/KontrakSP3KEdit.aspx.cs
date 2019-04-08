using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakSP3KEdit : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
			{
				nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempx=this.value";
				nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
				nilai.Attributes["onblur"] = "CalcBlur(this);";

				tgljtum.Text = Cf.Day(DateTime.Today.AddDays(1));

				Fill();
			}

			Js.Confirm(this, "Lanjutkan dengan proses edit SP3K?");
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

				tbTgl.Text = Cf.Day(rsHeader.Rows[0]["TglHasilSP3K"]);
				tbTarget.Text = Cf.Day(rsHeader.Rows[0]["TargetSP3K"]);
				tbPengajuan.Text = Cf.Day(rsHeader.Rows[0]["TglPengajuanSP3K"]);
				tbNoSP3K.Text = rsHeader.Rows[0]["NoSP3K"].ToString();
				tbKet.Text = rsHeader.Rows[0]["KetSP3K"].ToString();

				nilai.Text = Cf.Num(rsHeader.Rows[0]["ApprovalKPR"]);

				if(rsHeader.Rows[0]["HasilSP3K"].ToString() == "TOLAK")
					rblHasil.SelectedIndex = 0;
				else if(rsHeader.Rows[0]["HasilSP3K"].ToString() == "SETUJU")
					rblHasil.SelectedIndex = 1;
				else if(rsHeader.Rows[0]["HasilSP3K"].ToString() == "SETUJU SEBAGIAN")
					rblHasil.SelectedIndex = 2;

				if(rsHeader.Rows[0]["StatusSP3K"].ToString() == "")
				{
					rblStatus.SelectedIndex = 0;
					dijadwalkan.Visible = false;
					diajukan.Visible = false;
					selesai.Visible = false;
				}
				else if(rsHeader.Rows[0]["StatusSP3K"].ToString() == "TIDAK PERLU")
				{
					rblStatus.SelectedIndex = 1;
					dijadwalkan.Visible = false;
					diajukan.Visible = false;
					selesai.Visible = false;
				}
				else if(rsHeader.Rows[0]["StatusSP3K"].ToString() == "DIJADWALKAN")
				{
					rblStatus.SelectedIndex = 2;
					dijadwalkan.Visible = true;
					diajukan.Visible = false;
					selesai.Visible = false;
				}
				else if(rsHeader.Rows[0]["StatusSP3K"].ToString() == "DIAJUKAN")
				{
					rblStatus.SelectedIndex = 3;
					dijadwalkan.Visible = true;
					diajukan.Visible = true;
					selesai.Visible = false;
				}
				else if(rsHeader.Rows[0]["StatusSP3K"].ToString() == "SELESAI")
				{
					rblStatus.SelectedIndex = 4;
					dijadwalkan.Visible = true;
					diajukan.Visible = true;
					selesai.Visible = true;
				}

				decimal TambahanUM = Convert.ToDecimal(rsHeader.Rows[0]["NilaiPengajuan"]) - Convert.ToDecimal(rsHeader.Rows[0]["ApprovalKPR"]);
				tambahum.Text = Cf.Num(TambahanUM);
			}
		}

		private bool Valid()
		{
			bool x = true;
			string s = "";

			if(dijadwalkan.Visible)
			{
				if(!Cf.isTgl(tbTarget))
				{
					x = false;

					if(s == "")
						s = tbTarget.ID;

					lblTarget.Text = "Tanggal";
				}
				else
					lblTarget.Text = "";
			}

			if(diajukan.Visible)
			{
				if(!Cf.isTgl(tbPengajuan))
				{
					x = false;

					if(s == "")
						s = tbPengajuan.ID;

					lblPengajuan.Text = "Tanggal";
				}
				else
					lblPengajuan.Text = "";
			}

			if(selesai.Visible)
			{
				if(!Cf.isTgl(tbTgl))
				{
					x = false;

					if(s == "")
						s = tbTgl.ID;

					lblTgl.Text = "Tanggal";
				}else
					lblTgl.Text = "";

				if(!Cf.isMoney(nilai))
				{
					x = false;

					if(s == "")
						s = nilai.ID;

					nilaic.Text = "Angka";
				}
				else
					nilaic.Text = "";

				if(!Cf.isTgl(tgljtum))
				{
					x = false;

					if(s == "")
						s = tgljtum.ID;

					tgljtumc.Text = "Tanggal";
				}
				else
					tgljtumc.Text = "";
			}

			if(!x)
			{
				this.RegisterStartupScript(
					"focusScript"
					, "<script language='javascript' type='text/javascript'>"
					+ "document.getElementById('" + s + "').focus();"
					+ "</script>"
					);
			}

			return x;
		}

		private void Save()
		{
			string Status = "";
			if(rblStatus.SelectedIndex != 0)
				Status = rblStatus.SelectedItem.Text;

			string Hasil = "";
			if(Status == "TIDAK PERLU")
				Hasil = Status;

			DataTable rsBef = Db.Rs("SELECT "
				+ "StatusSP3K AS [Status SP3K]"
				+ ", TargetSP3K AS [Target SP3K]"
				+ ", TglPengajuanSP3K AS [Tgl. Pengajuan SP3K]"
				+ ", TglHasilSP3K AS [Tgl. Hasil SP3K]"
				+ ", NoSP3K AS [No. SP3K]"
				+ ", HasilSP3K AS [Hasil SP3K]"
				+ ", KetSP3K AS [Keterangan SP3K]"
				+ ", ApprovalKPR AS [Nilai KPR Disetujui]"
				+ " FROM MS_KONTRAK"
				+ " WHERE NoKontrak = '" + NoKontrak + "'" 
				);

			Db.Execute("UPDATE MS_KONTRAK"
				+ " SET StatusSP3K = '" + Status + "'"
				+ ", TargetSP3K = NULL"
				+ ", TglPengajuanSP3K = NULL"
				+ ", TglHasilSP3K = NULL"
				+ ", NoSP3K = ''"
				+ ", HasilSP3K = '" + Hasil + "'"
				+ ", KetSP3K = ''"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				);

			if(dijadwalkan.Visible)
			{
				DateTime Target = Convert.ToDateTime(tbTarget.Text);
				Hasil = "MENUNGGU";

				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET TargetSP3K = '" + Target + "'"
					+ ", TglPengajuanSP3K = NULL"
					+ ", TglHasilSP3K = NULL"
					+ ", NoSP3K = ''"
					+ ", HasilSP3K = '" + Hasil + "'"
					+ ", KetSP3K = ''"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);
			}

			if(diajukan.Visible)
			{
				DateTime TglPengajuan = Convert.ToDateTime(tbPengajuan.Text);
				Hasil = "MENUNGGU";

				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET TglPengajuanSP3K = '" + TglPengajuan + "'"
					+ ", TglHasilSP3K = NULL"
					+ ", NoSP3K = ''"
					+ ", HasilSP3K = '" + Hasil + "'"
					+ ", KetSP3K = ''"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);
			}

			if(selesai.Visible)
			{
				DateTime Tgl = Convert.ToDateTime(tbTgl.Text);
				string NoSP3K = Cf.Str(tbNoSP3K.Text);
				Hasil = rblHasil.SelectedItem.Text;
				string Ket = Cf.Str(tbKet.Text);
				decimal Nilai = Convert.ToDecimal(nilai.Text);

				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET TglHasilSP3K = '" + Tgl + "'"
					+ ", NoSP3K = '" + NoSP3K + "'"
					+ ", HasilSP3K = '" + Hasil + "'"
					+ ", KetSP3K = '" + Ket + "'"
					+ ", ApprovalKPR = " + Nilai
                    + ", RealisasiAkad = " + Nilai
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);

				decimal TambahUM = Db.SingleDecimal("SELECT NilaiPengajuan FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'") - Nilai;
				DataTable rs = Db.Rs("SELECT TOP 1 * FROM MS_TAGIHAN WHERE Tipe = 'DP' AND NamaTagihan LIKE '%TAMBAHAN UANG MUKA%' AND NoKontrak = '" + NoKontrak + "'");
				if(rs.Rows.Count == 0) //Insert tagihan
				{
					if(TambahUM > 0)
					{
						Db.Execute("EXEC spTagihanDaftar "
							+ " '" + NoKontrak + "'"
							+ ", 'TAMBAHAN UANG MUKA'"
							+ ", '" + Convert.ToDateTime(tgljtum.Text) + "'"
							+ ", " + TambahUM
							+ ", 'DP'"
							);
					}
					else if(TambahUM < 0)
						{
							Db.Execute("EXEC spTagihanDaftar "
								+ " '" + NoKontrak + "'"
								+ ", 'KELEBIHAN KPR'"
								+ ", '" + Convert.ToDateTime(tgljtum.Text) + "'"
								+ ", " + TambahUM
								+ ", 'ANG'"
								);	

						}
				}
				else //Update tagihan
				{
					Db.Execute("EXEC spTagihanEdit "
						+ " '" + NoKontrak + "'"
						+ ", " + rs.Rows[0]["NoUrut"]
						+ ",'" + rs.Rows[0]["NamaTagihan"] + "'"
						+ ",'" + Convert.ToDateTime(tgljtum.Text) + "'"
						+ ", " + TambahUM
						+ ",'" + rs.Rows[0]["Tipe"] + "'"
//						+ ",'" + Cf.BoolToSql(Convert.ToBoolean(rs.Rows[0]["Ban"])) + "'"
//						+ ",'" + Cf.BoolToSql(Convert.ToBoolean(rs.Rows[0]["Bunga"])) + "'"
						);
				}

				//Update tagihan KPR
				rs = Db.Rs("SELECT TOP 1 * FROM MS_TAGIHAN"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					+ " AND KPR = 1"
					);
				if(rs.Rows.Count > 0)
				{
					Db.Execute("EXEC spTagihanEdit "
						+ " '" + NoKontrak + "'"
						+ ", " + rs.Rows[0]["NoUrut"]
						+ ",'" + rs.Rows[0]["NamaTagihan"] + "'"
						+ ",'" + Convert.ToDateTime(rs.Rows[0]["TglJT"]) + "'"
						+ ", " + Nilai
						+ ",'" + rs.Rows[0]["Tipe"] + "'"
//						+ ",'" + Cf.BoolToSql(Convert.ToBoolean(rs.Rows[0]["Ban"])) + "'"
//						+ ",'" + Cf.BoolToSql(Convert.ToBoolean(rs.Rows[0]["Bunga"])) + "'"
						);

					Db.Execute("UPDATE MS_TAGIHAN"
						+ " SET Akad = 1"
						+ " WHERE NoKontrak = '" + NoKontrak + "'"
						+ " AND NoUrut = " + rs.Rows[0]["NoUrut"]
						);
					
					//Insert pelunasan
					string kpr = "KP";
					Db.Execute("DELETE FROM MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + rs.Rows[0]["NoUrut"] + " AND NoTTS = '0'");
					if(Db.SingleInteger("SELECT COUNT(*) FROM MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + rs.Rows[0]["NoUrut"] + " AND NoTTS = '0'") == 0)
					{
						Db.Execute("EXEC spPelunasan '" + NoKontrak + "', " + rs.Rows[0]["NoUrut"] + ", " + Nilai + ", '0'");
					    Db.Execute("UPDATE MS_PELUNASAN SET CaraBayar ='" + kpr + "', SudahCair ='1' WHERE NoKontrak = '"+ NoKontrak +"' AND NoTagihan = "+ rs.Rows[0]["NoUrut"] +" AND NoTTS = '0' ");
					}
				}

				//Insert piutang bank
//				if(Db.SingleInteger("SELECT COUNT(*) FROM MS_PIUTANGBANK WHERE NoKontrak = '" + NoKontrak + "'") == 0)
//				{
//					Db.Execute("EXEC spPiutangDaftar "
//						+ " '" + NoKontrak + "'"
//						+ ",'PENCAIRAN KPR'"
//						+ ", NULL"
//						+ ", NULL"
//						+ ", NULL"
//						+ ", " + Nilai
//						);
//				}
			}

			DataTable rsAft = Db.Rs("SELECT "
				+ "StatusSP3K AS [Status SP3K]"
				+ ", TargetSP3K AS [Target SP3K]"
				+ ", TglPengajuanSP3K AS [Tgl. Pengajuan SP3K]"
				+ ", TglHasilSP3K AS [Tgl. Hasil SP3K]"
				+ ", NoSP3K AS [No. SP3K]"
				+ ", HasilSP3K AS [Hasil SP3K]"
				+ ", KetSP3K AS [Keterangan SP3K]"
				+ ", ApprovalKPR AS [Nilai KPR Disetujui]"
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

			Response.Redirect("KontrakProses.aspx?NoKontrak=" + NoKontrak + "&done=3");
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Valid())
				Save();
		}

		protected void rblStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(rblStatus.SelectedItem.Text == "DIJADWALKAN")
			{
				dijadwalkan.Visible = true;
				diajukan.Visible = false;
				selesai.Visible = false;
			}
			else if(rblStatus.SelectedItem.Text == "DIAJUKAN")
			{
				dijadwalkan.Visible = true;
				diajukan.Visible = true;
				selesai.Visible = false;
			}
			else if(rblStatus.SelectedItem.Text == "SELESAI")
			{
				dijadwalkan.Visible = true;
				diajukan.Visible = true;
				selesai.Visible = true;
			}
			else
			{
				dijadwalkan.Visible = false;
				diajukan.Visible = false;
				selesai.Visible = false;
			}
		}

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
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
