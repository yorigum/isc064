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
	public partial class AccountingPiutang : System.Web.UI.Page
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
				JenisPenjualan = " AND JenisPenjualan = 0";	
			else if(penjualan.SelectedIndex==1)		
				JenisPenjualan = " AND JenisPenjualan = 1";

			
			string strSql = "SELECT *"
				+ " FROM MS_KONTRAK"
				+ " WHERE PersenLunas >= 20 AND Akunting = 0 AND Status = 'A'"
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

//				c = new TableCell();
//				c.Text = Cf.Num(rs.Rows[i]["TotalPelunasan"]);
//				c.HorizontalAlign = HorizontalAlign.Right;
//				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["PersenLunas"]);
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
				JenisPenjualan = " AND JenisPenjualan = 0";
			}
			else if(penjualan.SelectedIndex==1)
			{
				xls = "MappingCOA2.xls";
				JenisPenjualan = " AND JenisPenjualan = 1";
			}
			
			string strSql = "SELECT *"
				+ " FROM MS_KONTRAK"
				+ " WHERE PersenLunas >=20 AND Akunting = 0 AND Status = 'A'"
				+ JenisPenjualan
				;
			
			
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
				string SourceID = Cf.Pk(rs.Rows[i]["NoKontrak"]);
				DateTime TglKontrak = Convert.ToDateTime(rs.Rows[i]["TglKontrak"]);
				decimal NilaiKontrak = Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
				
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
				InsertJournalDetail(NilaiKontrak, JournalID, SourceID, TglKontrak);

				//Update Status Akunting + NoVoucher di MS_KONTRAK; NoKontrak di ISC064_ACC..Journal
				if(x.ToString() == "")
				{
					Db.Execute("UPDATE MS_KONTRAK SET Akunting2 = 1, NoVoucher = '" + JournalID + "' WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'");
				
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
				Response.Redirect("AccountingPiutang.aspx?done=1");
		}

		private void InsertJournalDetail(decimal NilaiKontrak, string JournalID, string NoKontrak, DateTime TglKontrak)
		{
			decimal Total = NilaiKontrak;
			string TipeMapping = "", COA1 = "", COA2 = "";
			bool isExistCOA1 = true, isExistCOA2 = true;

			TipeMapping = " AND [Tipe Mapping] = '" + Source + "'";
			
			
			DataTable rsCOA = Db.xls("SELECT [Amortisasi1], [Amortisasi2]"
				+ " FROM [MappingCOA$]"
				+ " WHERE 1 = 1"
				+ TipeMapping
				, Request.PhysicalApplicationPath.Replace("\\marketingjual\\","\\root\\") + xls
				);

			
			COA1 = rsCOA.Rows[0][0].ToString();
			COA2 = rsCOA.Rows[0][1].ToString();
//			Response.Write(COA1 + "<br>");
//			Response.Write(COA2 + "<br>");
//			Response.End();

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
					, Source + "_1"
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
					, Source + "_2"
					, TglKontrak
					);
			}
			else
			{
				x.Append("No. Kontrak: " + NoKontrak + ", MAPPING COA TAGIHAN " + Source + " tidak terdaftar.<br />");
				Akun.Execute("DELETE FROM Journal WHERE JournalID = '" + JournalID + "'");
			}
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

		protected void search_Click(object sender, System.EventArgs e)
		{
			isi.Attributes["style"] = "display:;";
			Fill();

			FeedBack();
			Js.Confirm(this, "Lanjutkan dengan proses Posting Accounting?");		
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
