using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

namespace ISC064.FINANCEAR
{
	public partial class Accounting : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox periode;
		public string JenisPenjualan = "";
		public string xls = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["status"]=="a")
					penjualan.SelectedIndex = 1;
				else if(Request.QueryString["status"]=="i")
					penjualan.SelectedIndex = 2;

				if(penjualan.SelectedIndex!=0) penjualan.Enabled = false;
				isi.Attributes["style"] = "display:none;";				
			}			
		}

		protected void search_Click(object sender, System.EventArgs e)
		{
		
			isi.Attributes["style"] = "display:;";
			Fill();

			FeedBack();
			Js.Confirm(this, "Lanjutkan dengan proses Posting Accounting?");		
		}


		private void FeedBack()
		{
			feed.Text = "";

			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"] != null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Posting Accounting Berhasil.."
						;
			}
		}

		protected void Fill()
		{
			if(penjualan.SelectedIndex==0)
				JenisPenjualan = " AND a.JenisPenjualan = 0";	
			else if(penjualan.SelectedIndex==1)		
				JenisPenjualan = " AND a.JenisPenjualan = 1";

			string strSql = "SELECT *"
				+ " FROM MS_TTS INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a"
				+ " ON a.NoKontrak = MS_TTS.Ref"
				+ " WHERE MS_TTS.Status = 'POST' AND MS_TTS.Akunting = 0"
				+ JenisPenjualan
				;
			DataTable rs = Db.Rs(strSql);

			Rpt.NoData(list, rs, "Tidak ditemukan data yang siap untuk di-posting.");
			

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href=\"javascript:popEditTTS('" + Cf.Pk(rs.Rows[i]["NoTTS"]) + "');\">"
					+ rs.Rows[i]["NoTTS"].ToString().PadLeft(7, '0')
					+ "</a>"
					;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoBKM"].ToString().PadLeft(7, '0');
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Acc"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Customer"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Unit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["Total"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(Db.SingleDecimal("SELECT PersenLunas FROM " + Mi.DbPrefix + "MARKETING" + rs.Rows[i]["Tipe"] + "..MS_KONTRAK WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Ref"]) + "'"));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				list.Controls.Add(r);
				Rpt.Border(r);
			}
		}

		private void GenerateAcc()
		{
			
			if(penjualan.SelectedIndex==0)
			{
				xls = "MappingCOA1.xls";
				JenisPenjualan = " AND a.JenisPenjualan = 0";
			}
			else if(penjualan.SelectedIndex==1)
			{
				xls = "MappingCOA2.xls";
				JenisPenjualan = " AND a.JenisPenjualan = 1";
			}
			
			string strSql = "SELECT *"
				+ " FROM MS_TTS INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a"
				+ " ON a.NoKontrak = MS_TTS.Ref"
				+ " WHERE MS_TTS.Status = 'POST' AND MS_TTS.Akunting = 0"
				+ JenisPenjualan
				;
			
			DataTable rs = Db.Rs(strSql);

			System.Text.StringBuilder x = new System.Text.StringBuilder();
           
			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				string CBID = Akun.NewCBID(rs.Rows[i]["Acc"].ToString(), 1);
				string JournalMemo = rs.Rows[i]["Unit"].ToString() + " " + rs.Rows[i]["Ref"].ToString();
				string OtherParty = rs.Rows[i]["Customer"].ToString();
				string AccountID = rs.Rows[i]["Acc"].ToString();
				decimal Deposit = Convert.ToDecimal(rs.Rows[i]["Total"]);
				decimal ForexDeposit = Deposit;
				DateTime TglBKM = Convert.ToDateTime(rs.Rows[i]["TglBKM"]);
				string Source = rs.Rows[i]["Tipe"].ToString();
				int TipePosting = Convert.ToInt32(rs.Rows[i]["TipePosting"]);

				int AccCount = Akun.SingleInteger("SELECT COUNT(AccountID) FROM Account WHERE AccountID = '" + rs.Rows[i]["Acc"].ToString() + "' AND Postable = 1");
				
				if(AccCount == 0)
				{
					x.Append("No. TTS: " + rs.Rows[i]["NoTTS"].ToString().PadLeft(7, '0') + ", REKENING tidak terdaftar.<br />");
				}
				else
				{
					//Insert CB
					/*Akun.CB(
						CBID
						, "IDR"
						, 1
						, TglBKM
						, JournalMemo
						, OtherParty
						, AccountID
						, Deposit
						, 0
						, ForexDeposit
						, 0
						, true
						, Cf.Pk(rs.Rows[i]["NoTTS"])
						, ""
						, ""
						, false
						, "TTS"
						);*/
					// modify by viana 171208 TTS -> BKM
					Akun.CB(
						CBID
						, "IDR"
						, 1
						, TglBKM
						, JournalMemo
						, OtherParty
						, AccountID
						, Deposit
						, 0
						, ForexDeposit
						, 0
						, true
						, Cf.Pk(rs.Rows[i]["NoBKM"])
						, ""
						, ""
						, false
						, "BKM"
						);

					//Insert CB Detail (Debit)
					decimal Debit = Convert.ToDecimal(rs.Rows[i]["Total"]);
					decimal ForexDebit = Debit;
					string Notes = "TOTAL"
						+ " "
						+ rs.Rows[i]["Ket"].ToString() 
						;

					if(Notes.Length > 255)
						Notes = Notes.Substring(0, 254);

					Akun.CBDetail(
						CBID
						, 1
						, AccountID
						, Debit
						, 0
						, ForexDebit
						, 0
						, Notes
						);

					//Insert CB Detail (Kredit)
					strSql = "SELECT *"
						+ " FROM " + Mi.DbPrefix + "MARKETING" + Source + "..MS_PELUNASAN a"
						+ " INNER JOIN " + Mi.DbPrefix + "MARKETING" + Source + "..MS_TAGIHAN b ON a.NoTagihan = b.NoUrut AND a.NoKontrak = b.NoKontrak"
						+ " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Ref"]) + "'"
						+ " AND a.NoTTS = " + Cf.Pk(rs.Rows[i]["NoTTS"])
						;
					DataTable rsTagihan = Db.Rs(strSql);

					DataTable rsCOA;
					bool isValid = true;

					int SN = 1;
					bool AccDP = false;

					for(int j = 0; j < rsTagihan.Rows.Count; j++)
					{
						if(!Response.IsClientConnected)
							break;
						
						SN++;

						string COA = "", col = "", add = "";
						string Tipe = rsTagihan.Rows[j]["Tipe"].ToString();
						decimal Kredit = Convert.ToDecimal(rsTagihan.Rows[j]["NilaiPelunasan"]);
						decimal ForexKredit = Kredit;
						Notes = rsTagihan.Rows[j]["NamaTagihan"].ToString()
							+ " "
							+ rsTagihan.Rows[j]["Ket"].ToString()
							;
						if(Notes.Length > 255)
							Notes = Notes.Substring(0, 254);
						decimal PersenLunas = Db.SingleDecimal("SELECT PersenLunas FROM " + Mi.DbPrefix + "MARKETING" + Source + "..MS_KONTRAK WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Ref"]) + "'");
						bool StatusAkunting2 = Db.SingleBool("SELECT Akunting2 FROM " + Mi.DbPrefix + "MARKETING" + Source + "..MS_KONTRAK WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Ref"]) + "'");
						if (StatusAkunting2 == true)
							TipePosting = 1;
						else
							TipePosting = 0;

						if(TipePosting == 1)
						{
							col = "[Amortisasi1]";
						}
						else
							AccDP = true;
							col = "[Jual Belum Diakui]";

						if(Tipe == "ADM")
						{
							if(rsTagihan.Rows[j]["NamaTagihan"].ToString() == "BIAYA ADM. PPJB")
								add = " AND [Tipe Mapping] = 'ADM PPJB'";
							if(rsTagihan.Rows[j]["NamaTagihan"].ToString() == "BIAYA ADM. AJB")
								add = " AND [Tipe Mapping] = 'ADM AJB'";
							if(rsTagihan.Rows[j]["NamaTagihan"].ToString() == "BIAYA ADM. PEMBATALAN")
								add = " AND [Tipe Mapping] = 'ADM BATAL'";
							if(rsTagihan.Rows[j]["NamaTagihan"].ToString() == "BIAYA ADM. SERAH TERIMA")
								add = " AND [Tipe Mapping] = 'ADM ST'";
							if(rsTagihan.Rows[j]["NamaTagihan"].ToString() == "BIAYA ADM. Pengalihan Hak")
								add = " AND [Tipe Mapping] = 'ADM GN'";
							if(rsTagihan.Rows[j]["NamaTagihan"].ToString() == "BIAYA ADM. Pindah Unit")
								add = " AND [Tipe Mapping] = 'ADM GU'";

							col = "[Normal]";
						}
						else
						{
							if(Source == "JUAL")
								add = " AND [Tipe Mapping] = '" + Tipe + "'";
							else if(Source == "SEWA")
								add = " AND [Tipe Mapping] = '" + Tipe + "2'";
							else if(Source == "SB")
								add = " AND [Tipe Mapping] = '" + Tipe + "3'";
						}

						rsCOA = Db.xls("SELECT " + col + " FROM [MappingCOA$] WHERE 1 = 1" + add, 
							Request.PhysicalApplicationPath.Replace("\\financear\\","\\root\\") + xls);

						COA = rsCOA.Rows[0][0].ToString();

						if(Akun.SingleInteger("SELECT COUNT(AccountID) FROM Account WHERE AccountID = '" + COA + "' AND Postable = 1") == 0)
						{
							isValid = false;
							x.Append("No. TTS: " + rs.Rows[i]["NoTTS"].ToString().PadLeft(7, '0') + ", MAPPING COA TAGIHAN tidak terdaftar.<br />");
							break;
						}

						Akun.CBDetail(
							CBID
							, SN
							, COA
							, 0
							, Kredit
							, 0
							, ForexKredit
							, Notes
							);
					}

					if(!isValid)
						Akun.Execute("DELETE FROM CB WHERE CBID = '" + CBID + "'");
					else
					{
						//Update Status Akunting + NoVoucher TTS
						Db.Execute("UPDATE MS_TTS SET Akunting = 1, TipePosting = " + TipePosting + ",AccDP = " + Cf.BoolToSql(AccDP) + ", NoVoucher = '" + CBID + "' WHERE NoTTS = " + Cf.Pk(rs.Rows[i]["NoTTS"]));

						//Logfile
						DataTable rsDetail = Db.Rs("SELECT * FROM MS_TTS WHERE NoTTS = " + Cf.Pk(rs.Rows[i]["NoTTS"]));
						string KetLog = Cf.LogCapture(rsDetail);

						Db.Execute("EXEC spLogTTS"
							+ " 'ACC'"
							+ ",'" + Act.UserID + "'"
							+ ",'" + Act.IP + "'"
							+ ",'" + KetLog + "'"
							+ ",'" + Cf.Pk(rs.Rows[i]["NoTTS"]).PadLeft(7,'0') + "'"
							);
					}
				}	
			}
			
			if(x.ToString() != "")
			{
				err.Text = "<h2 style='border-top:1px dashed gray; padding-top:10'>Gagal posting:</h2>"
					+ "<div style='margin-left: 40px; padding-top: 10px;'>"
					+ x.ToString()
					+ "</div>"
					;
			}
			else
				Response.Redirect("Accounting.aspx?done=1");
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			GenerateAcc();
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
