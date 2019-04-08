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

namespace ISC064.MARKETINGJUAL
{
	public partial class Accounting : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox periode;
		System.Text.StringBuilder x = new System.Text.StringBuilder();

		public string xls = "";
		public string JenisPenjualan = "";
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
			
//			Fill();
//
//			FeedBack();
//			Js.Confirm(this, "Lanjutkan dengan proses Posting Accounting?");
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
//			string strSql = "SELECT *"
//				+ " FROM MS_KONTRAK"
//				+ " WHERE PersenLunas = 20 AND Akunting = 0 AND Status = 'A'"
//				;

			if(penjualan.SelectedIndex==0)
				JenisPenjualan = " AND JenisPenjualan = 0";	
			else if(penjualan.SelectedIndex==1)		
				JenisPenjualan = " AND JenisPenjualan = 1";

			string strSql = "SELECT *, "
				+ " (SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS "
				+ " WHERE Ref = MS_KONTRAK.NoKontrak AND TipePosting = 1) AS TotalPelunasan "
				+ " FROM MS_KONTRAK WHERE PersenLunas >= 60";

			DataTable rs = Db.Rs(strSql);
			Response.Write(penjualan.SelectedIndex);

			Rpt.NoData(list, rs, "Tidak ditemukan data yang siap untuk di-posting.");

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href=\"javascript:popEditKontrak('" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "');\">"
					+ rs.Rows[i]["NoKontrak"].ToString()
					+ "</a>"
					;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"].ToString());
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["TotalPelunasan"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

//				c = new TableCell();
//				c.Text = Cf.Num(rs.Rows[i]["PersenLunas"]);
//				c.HorizontalAlign = HorizontalAlign.Right;
//				r.Cells.Add(c);

				list.Controls.Add(r);
				Rpt.Border(r);
			}
		}

		private void GenerateAcc()
		{
			if(penjualan.SelectedIndex==0)
			{
				xls = "MappingCOA1.xls";
				JenisPenjualan = " AND JenisPenjualan = 0";
			}
			else if(penjualan.SelectedIndex==1)
			{
				xls = "MappingCOA2.xls";
				JenisPenjualan = " AND JenisPenjualan = 1";
			}
			
			Response.Write(penjualan.SelectedIndex + "<br>");
			Response.Write(xls);
			Response.End();
			string strSql = "SELECT *, "
				+ " (SELECT ISNULL(SUM(Total),0) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS "
				+ " WHERE Ref = MS_KONTRAK.NoKontrak AND TipePosting = 1) AS TotalPelunasan "
				+ " FROM MS_KONTRAK WHERE PersenLunas >= 60 "
				+ JenisPenjualan ;

			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				string JournalID = Akun.NewJournalID();
				string JournalMemo = rs.Rows[i]["NoUnit"].ToString() 
					+ " "
					+ Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + Cf.Pk(rs.Rows[i]["NoCustomer"]))
					;
				string Source = "SALES";
				string SourceID = Cf.Pk(rs.Rows[i]["NoKontrak"]);
				DateTime TglKontrak = Convert.ToDateTime(rs.Rows[i]["TglKontrak"]);
				
				//Insert Journal
				Akun.Journal(
					JournalID
					, "IDR"
					, 1
					, TglKontrak
					, JournalMemo
					, Source
					, SourceID
					);

				InsertJournalDetail("BF", JournalID, SourceID, TglKontrak);
				InsertJournalDetail("DP", JournalID, SourceID, TglKontrak);
				InsertJournalDetail("ANG", JournalID, SourceID, TglKontrak);

				//Update Status Akunting + NoVoucher di MS_KONTRAK; NoKontrak di " + Mi.DbPrefix + "ACC..Journal
				if(x.ToString() == "")
				{
					Db.Execute("UPDATE MS_KONTRAK SET Akunting = 1, NoVoucher = '" + JournalID + "' WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'");
				
					//Logfile
					DataTable rsDetail = Db.Rs("SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'");
					string Ket = Cf.LogCapture(rsDetail);

					Db.Execute("EXEC spLogKontrak"
						+ " 'ACC'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + Ket + "'"
						+ ",'" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
						);
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

		private void InsertJournalDetail(string TipeTagihan, string JournalID, string NoKontrak, DateTime TglKontrak)
		{
			decimal Total = Hitung(TipeTagihan, NoKontrak);
			string TipeMapping = "", COA1 = "", COA2 = "";
			bool isExistCOA1 = true, isExistCOA2 = true;

			if(Source == "JUAL")
				TipeMapping = " AND [Tipe Mapping] = '" + TipeTagihan + "'";
			else if(Source == "SEWA")
				TipeMapping = " AND [Tipe Mapping] = '" + TipeTagihan + "2'";
			else if(Source == "SB")
				TipeMapping = " AND [Tipe Mapping] = '" + TipeTagihan + "3'";
			
			DataTable rsCOA = Db.xls("SELECT [Jual Belum Diakui], [Jual Sudah Diakui]"
				+ " FROM [MappingCOA$]"
				+ " WHERE 1 = 1"
				+ TipeMapping
				, Request.PhysicalApplicationPath.Replace("\\marketingjual\\","\\root\\") + xls
				);
			COA1 = rsCOA.Rows[0][0].ToString();
			COA2 = rsCOA.Rows[0][1].ToString();

			if(Akun.SingleInteger("SELECT COUNT(AccountID) FROM Account WHERE AccountID = '" + COA1 + "' AND Postable = 1") == 0)
				isExistCOA1 = false;
			if(Akun.SingleInteger("SELECT COUNT(AccountID) FROM Account WHERE AccountID = '" + COA2 + "' AND Postable = 1") == 0)
				isExistCOA2 = false;

			if(isExistCOA1 && isExistCOA2)
			{
				int SN = Akun.SingleInteger("SELECT ISNULL(MAX(SN), 0)"
					+ " FROM JournalDetail"
					+ " WHERE JournalID = '" + JournalID + "'"
					);
				SN++;

				//Debet
				Akun.JournalDetail(
					JournalID
					, SN
					, COA1
					, Total
					, 0
					, Total
					, 0
					, TipeTagihan + "_1"
					, TglKontrak
					);

				//Kredit
				SN++;
				Akun.JournalDetail(
					JournalID
					, SN
					, COA2
					, 0
					, Total
					, 0
					, Total
					, TipeTagihan + "_2"
					, TglKontrak
					);
			}
			else
			{
				x.Append("No. Kontrak: " + NoKontrak + ", MAPPING COA TAGIHAN " + TipeTagihan + " tidak terdaftar.<br />");
				Akun.Execute("DELETE FROM Journal WHERE JournalID = '" + JournalID + "'");
			}
		}

		private decimal Hitung(string TipeTagihan, string NoKontrak)
		{
//			string strSql = "SELECT ISNULL(SUM(NilaiPelunasan), 0)"
//				+ " FROM " + Mi.DbPrefix + "MARKETING" + Source + "..MS_PELUNASAN a"
//				+ " INNER JOIN " + Mi.DbPrefix + "MARKETING" + Source + "..MS_TAGIHAN b ON a.NoTagihan = b.NoUrut AND a.NoKontrak = b.NoKontrak"
//				+ " INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_TTS c ON a.NoTTS = c.NoTTS"
//				+ " WHERE b.Tipe = '" + TipeTagihan + "'"
//				+ " AND a.NoKontrak = '" + NoKontrak + "'"
//				+ " AND c.Akunting = 1 AND c.AccDP = 1"
//				;

			string strSql = "SELECT ISNULL(SUM(NilaiPelunasan), 0)"
				+ " FROM " + Mi.DbPrefix + "MARKETING" + Source + "..MS_PELUNASAN a"
				+ " INNER JOIN " + Mi.DbPrefix + "MARKETING" + Source + "..MS_TAGIHAN b ON a.NoTagihan = b.NoUrut AND a.NoKontrak = b.NoKontrak"
				+ " INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_TTS c ON a.NoTTS = c.NoTTS"
				+ " WHERE b.Tipe = '" + TipeTagihan + "'"
				+ " AND a.NoKontrak = '" + NoKontrak + "'"
				+ " AND c.Akunting = 1 AND c.TipePosting = 0 AND c.AccDP = 1"
				;
			decimal x = Db.SingleDecimal(strSql);

			return x;
		}

		protected string Source
		{
			get
			{
				return "JUAL";
			}
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

		protected void search_Click(object sender, System.EventArgs e)
		{
			isi.Attributes["style"] = "display:;";
			Fill();

			FeedBack();
			Js.Confirm(this, "Lanjutkan dengan proses Posting Accounting?");		
		}

	}
}
