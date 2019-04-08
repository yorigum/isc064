using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA
{
	public partial class KontrakProses : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
				Fill();
		}

		private void Fill()
		{
			
			string strSql = "SELECT *"
				+ " FROM MS_KONTRAK a inner join ms_unit b"
				+ " on a.nounit = b.nounit"
				+ " WHERE NoKontrak = '" + NoKontrak + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
                //Fill Berkas
                //lblBankKPA.Text = Db.SingleString("SELECT Bank FROM REF_BANKKPA WHERE KodeBank='" + rs.Rows[0]["BankKPR"].ToString() + "'");
                if (Convert.ToBoolean(rs.Rows[0]["StatusBerkas"]) == false)
                    lblStatusBerkas.Text = "BELUM LENGKAP";
                else
                    lblStatusBerkas.Text = "SUDAH LENGKAP";
                
                lblTglSelesaiBerkas.Text = Cf.Day(rs.Rows[0]["TglSelesaiBerkas"]);
                lblChecklistDokumen.Text = rs.Rows[0]["CheckListDokumen"].ToString();

                //Fill Wawancara
                lblBankKPA.Text = Db.SingleString("SELECT Bank FROM REF_BANKKPA WHERE KodeBank='"+rs.Rows[0]["BankKPR"].ToString()+"'");
				if(rs.Rows[0]["StatusWawancara"].ToString() == "")
					lblStatusWawancara.Text = "BELUM DITENTUKAN";
				else
					lblStatusWawancara.Text = rs.Rows[0]["StatusWawancara"].ToString();
				lblTargetWawancara.Text = Cf.Day(rs.Rows[0]["TargetWawancara"]);
				lblTglWawancara.Text = Cf.Day(rs.Rows[0]["TglWawancara"]);
				lblLokasiWawancara.Text = rs.Rows[0]["LokasiWawancara"].ToString();
				lblKetWawancara.Text = rs.Rows[0]["KetWawancara"].ToString();
				
				decimal NilaiPengajuan = 0;
				if(Convert.ToDecimal(rs.Rows[0]["NilaiPengajuan"]) == 0)
				{
					NilaiPengajuan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0) FROM MS_TAGIHAN"
						+ " WHERE NoKontrak = '" + NoKontrak + "' AND Tipe = 'ANG' AND KPR = 1"
						);
				}
				else
					NilaiPengajuan = Convert.ToDecimal(rs.Rows[0]["NilaiPengajuan"]);
				nilaipengajuan.Text = Cf.Num(NilaiPengajuan);

                //Fill OTS
                if (rs.Rows[0]["StatusOTS"].ToString() == "")
					statusots.Text = "BELUM DITENTUKAN";
				else
					statusots.Text = rs.Rows[0]["StatusOTS"].ToString();
				targetots.Text = Cf.Day(rs.Rows[0]["TargetOTS"]);
				tglots.Text = Cf.Day(rs.Rows[0]["TglOTS"]);
				if(rs.Rows[0]["HasilOTS"].ToString() == "")
					hasilots.Text = "BELUM DITENTUKAN";
				else
					hasilots.Text = rs.Rows[0]["HasilOTS"].ToString();
				ketots.Text = rs.Rows[0]["KetOTS"].ToString();

				//Fill SP3K
				if(rs.Rows[0]["StatusSP3K"].ToString() == "")
					lblStatusSP3K.Text = "BELUM DITENTUKAN";
				else
					lblStatusSP3K.Text = rs.Rows[0]["StatusSP3K"].ToString();
				lblTargetSP3K.Text = Cf.Day(rs.Rows[0]["TargetSP3K"]);
				lblTglPengajuanSP3K.Text = Cf.Day(rs.Rows[0]["TglPengajuanSP3K"]);
				lblTglHasilSP3K.Text = Cf.Day(rs.Rows[0]["TglHasilSP3K"]);
				lblNoSP3K.Text = rs.Rows[0]["NoSP3K"].ToString();
				if(rs.Rows[0]["HasilSP3K"].ToString() == "")
					lblHasilSP3K.Text = "BELUM DITENTUKAN";
				else
					lblHasilSP3K.Text = rs.Rows[0]["HasilSP3K"].ToString();
				lblKetSP3K.Text = rs.Rows[0]["KetSP3K"].ToString();
				approvalkpr.Text = Cf.Num(rs.Rows[0]["ApprovalKPR"]);

				//Fill LPA
				if(rs.Rows[0]["StatusLPA"].ToString() == "")
					lblStatusLPA.Text = "BELUM DITENTUKAN";
				else
					lblStatusLPA.Text = rs.Rows[0]["StatusLPA"].ToString();
				lblTargetLPA.Text = Cf.Day(rs.Rows[0]["TargetLPA"]);
				lblTglLPA.Text = Cf.Day(rs.Rows[0]["TglLPA"]);
				lblNoLPA.Text = rs.Rows[0]["NoLPA"].ToString();
				lblKetLPA.Text = rs.Rows[0]["KetLPA"].ToString();

				//Fill Akad
				if(rs.Rows[0]["StatusAkad"].ToString() == "")
					lblStatusAkad.Text = "BELUM DITENTUKAN";
				else
					lblStatusAkad.Text = rs.Rows[0]["StatusAkad"].ToString();
				lblTargetAkad.Text = Cf.Day(rs.Rows[0]["TargetAkad"]);
				lblTglAkad.Text = Cf.Day(rs.Rows[0]["TglAkad"]);
				lblNoAkad.Text = rs.Rows[0]["NoAkad"].ToString();
				lblKetAkad.Text = rs.Rows[0]["KetAkad"].ToString();
				realisasiakad.Text = Cf.Num(rs.Rows[0]["RealisasiAkad"]);
								

				//Fill AJB
				if(rs.Rows[0]["StatusAJB"].ToString() == "")
					lblStatusAJB.Text = "BELUM DILAKUKAN";
				else
					lblStatusAJB.Text = rs.Rows[0]["StatusAJB"].ToString();
				lblTglAJB.Text = Cf.Day(rs.Rows[0]["TglAJB"]);
				lblNoAJB.Text = rs.Rows[0]["NoAJB"].ToString();
				lblNamaNotaris.Text = rs.Rows[0]["NamaNotaris"].ToString();
				lblKetAJB.Text = rs.Rows[0]["KetAJB"].ToString();

				//Fill IMB
				bool StatusImb;
					StatusImb = Convert.ToBoolean(rs.Rows[0]["StatusIMB"]);
				
				if(!StatusImb)
					statusimb.Text = "BELUM DIKELUARKAN";
				else
					statusimb.Text = "SELESAI";
				tglimb.Text = Cf.Day(rs.Rows[0]["TglImb"]);
				noimb.Text = rs.Rows[0]["NoImb"].ToString();
				keteranganimb.Text = rs.Rows[0]["KetImb"].ToString();

				//Fill Sertifikat
				if(rs.Rows[0]["StatusSertifikat"].ToString() == "0")
					lblStatusSertifikat.Text = "BELUM DIKELUARKAN";
				else if (rs.Rows[0]["StatusSertifikat"].ToString() == "1")
					lblStatusSertifikat.Text = "SELESAI";
				else if (rs.Rows[0]["StatusSertifikat"].ToString() == "2")
					lblStatusSertifikat.Text = "SEDANG PROSES";
				else if (rs.Rows[0]["StatusSertifikat"].ToString() == "3")
					lblStatusSertifikat.Text = "BELUM BALIK NAMA";
				lblNoSertifikat.Text = rs.Rows[0]["NoSertifikat"].ToString();
				lblTglSertifikat.Text = Cf.Day(rs.Rows[0]["TglSertifikat"]);
				sedangproses1.Visible = false;
				sedangproses2.Visible = false;
				sedangproses3.Visible = false;
				sedangproses4.Visible = false;
				sedangproses5.Visible = false;
				atasnama.Visible = false;
				if(rs.Rows[0]["StatusSertifikat"].ToString() == "3")
				{
				atasnama.Visible = true;
				namaperusahaan.Text = rs.Rows[0]["NamaPerusahaan"].ToString();
				}

				if(rs.Rows[0]["StatusSertifikat"].ToString() == "2")
				{
					sedangproses1.Visible = true;
					sedangproses2.Visible = true;
					sedangproses3.Visible = true;
					sedangproses4.Visible = true;
					sedangproses5.Visible = true;
					nomorukur.Text = rs.Rows[0]["NoPengukuranBidang"].ToString();
					tanggalukur.Text = Cf.Day(rs.Rows[0]["TglPengukuranBidang"]);
					tanggalsuratukur.Text = Cf.Day(rs.Rows[0]["TglSuratUkur"]);
					nosuratukur.Text = rs.Rows[0]["NoSuratUkur"].ToString();
					jumlahbidang.Text = rs.Rows[0]["JumlahBidang"].ToString();
				
				}

				bool StatusHak = Convert.ToBoolean(rs.Rows[0]["StatusHak"]);
				
				sertifikat1.Visible = sertifikat2.Visible = sertifikat3.Visible = false;
				
				statushak.Text = (StatusHak) ? "Hak Milik" : "HGB";

				if(!StatusHak && rs.Rows[0]["StatusSertifikat"].ToString() == "3")
				{
					sertifikat1.Visible = sertifikat2.Visible = sertifikat3.Visible = true;
					jangkawaktu.Text = Cf.Num(rs.Rows[0]["JangkaWaktu"]);
					tglakhir.Text = Cf.Day(rs.Rows[0]["TglAkhir"]);
				}

				else if(!StatusHak && rs.Rows[0]["StatusSertifikat"].ToString() == "1")
				{
					sertifikat1.Visible = sertifikat2.Visible = sertifikat3.Visible = true;
					jangkawaktu.Text = Cf.Num(rs.Rows[0]["JangkaWaktu"]);
					tglakhir.Text = Cf.Day(rs.Rows[0]["TglAkhir"]);
				}


				decimal TambahanUM = Convert.ToDecimal(rs.Rows[0]["NilaiPengajuan"]) - Convert.ToDecimal(rs.Rows[0]["ApprovalKPR"]);
				tambahum.Text = Cf.Num(TambahanUM);

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
 