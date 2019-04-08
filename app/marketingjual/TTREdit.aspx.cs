using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class TTREdit : System.Web.UI.Page
	{
		
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Act.Sec("ED:" + Request.PhysicalPath))
			{
				ok.Enabled = false;
				save.Enabled = false;
			}

			if(!Page.IsPostBack)
				Fill();

			FeedBack();
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
			btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_TTR_LOG&Pk=" + NoTTR + "'";
			btnvoid.Attributes["onclick"] = "if(confirm('"
				+ "Apakah anda ingin membatalkan TTR nomor : " + NoTTR + " ?\\n"
				+ "Perhatian bahwa proses ini TIDAK bisa dibalik."
				+ "'))"
				+ "{location.href='TTRVoid.aspx?NoTTR=" + NoTTR + "'}";
			btnvoid2.Attributes["onclick"] = "if(confirm('"
				+ "Apakah anda ingin membatalkan dan mengembalikan uang TTR nomor : " + NoTTR + " ?\\n"
				+ "Perhatian bahwa proses ini TIDAK bisa dibalik."
				+ "'))"
				+ "{location.href='TTRVoid.aspx?r=1&NoTTR=" + NoTTR + "'}";
			printTTR.HRef = "PrintTTR.aspx?NoTTR=" + NoTTR;

			string strSql = "SELECT * "
				+ ",CASE CaraBayar"
				+ "		WHEN 'TN' THEN 'TUNAI'"
				+ "		WHEN 'KK' THEN 'KARTU KREDIT'"
				+ "		WHEN 'KD' THEN 'KARTU DEBIT'"
				+ "		WHEN 'TR' THEN 'TRANSFER BANK'"
				+ "		WHEN 'BG' THEN 'CEK GIRO'"
				+ " END AS CaraBayar2"
				+ " FROM MS_TTR WHERE NoTTR = '" + NoTTR + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				tglttr.Text = Cf.Day(rs.Rows[0]["TglTTR"]);
				ket.Text = rs.Rows[0]["Ket"].ToString();
				
				unit.Text = rs.Rows[0]["Unit"].ToString();
				customer.Text = rs.Rows[0]["Customer"].ToString();

				printTTR.InnerHtml = printTTR.InnerHtml + " (" + rs.Rows[0]["PrintTTR"] + ")";

				kasir.Text = rs.Rows[0]["UserID"].ToString();
				ip.Text = rs.Rows[0]["IP"].ToString();
				tglInput.Text = Cf.Date(rs.Rows[0]["TglInput"]);
				
				carabayar.Text = rs.Rows[0]["CaraBayar2"].ToString();
				if(rs.Rows[0]["CaraBayar"].ToString()=="BG")
				{
					nobg.Text = rs.Rows[0]["NoBG"].ToString();
					tglbg.Text = Cf.Day(rs.Rows[0]["TglBG"]);
				}

				nilai.Text = Cf.Num(rs.Rows[0]["Total"]);

				string stat = rs.Rows[0]["Status"].ToString();
				status.Text = stat;
				if((decimal)rs.Rows[0]["NilaiKembali"]!=0)
					status.Text = status.Text
						+ "<br><font style='font-size:9pt'>Reimburse : "
						+ Cf.Num(rs.Rows[0]["NilaiKembali"]) + "</font>";
				
				if(stat=="VOID")
				{
					status.ForeColor = Color.Red;

					btnvoid.Disabled = true;
					btnvoid2.Disabled = true;
				}
				else if(stat=="POST")
				{
					status.ForeColor = Color.Blue;

					btnvoid.Disabled = true;
					btnvoid2.Disabled = true;
				}

				manualttr.Text = rs.Rows[0]["ManualTTR"].ToString();
			}
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(tglttr))
			{
				x = false;
				if(s=="") s = tglttr.ID;
				tglttrc.Text = "Tanggal";
			}
			else
				tglttrc.Text = "";

			if(Cf.isEmpty(unit))
			{
				x = false;
				if(s=="") s = unit.ID;
				unitc.Text = "Kosong";
			}
			else
				unitc.Text = "";

			if(Cf.isEmpty(customer))
			{
				x = false;
				if(s=="") s = customer.ID;
				customerc.Text = "Kosong";
			}
			else
				customerc.Text = "";

			if(carabayar.Text=="CEK GIRO")
			{
				nobg.Text = Cf.Pk(nobg.Text);
				if(Cf.isEmpty(nobg))
				{
					x = false;
					if(s=="") s = nobg.ID;
					nobgc.Text = "Kosong";
				}
				else
					nobgc.Text = "";

				if(!Cf.isTgl(tglbg))
				{
					x = false;
					if(s=="") s = tglbg.ID;
					tglbgc.Text = "Tanggal";
				}
				else
					tglbgc.Text = "";
			}

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					+ "2. Unit Properti tidak boleh kosong.\\n"
					+ "3. Customer tidak boleh kosong.\\n"
					+ "4. Khusus Cek Giro : No. BG tidak boleh kosong.\\n"
					, "document.getElementById('" + s + "').focus();"
					+ "document.getElementById('" + s + "').select();"
					);

			return x;
		}

		private bool Save()
		{
			if(valid())
			{
				DateTime TglTTR = Convert.ToDateTime(tglttr.Text);
				string Ket = Cf.Str(ket.Text);
				string Unit = Cf.Str(unit.Text);
				string Customer = Cf.Str(customer.Text);
				string ManualTTR = Cf.Str(manualttr.Text);
				
				string NoBG = "";
				DateTime TglBG = DateTime.Today;

				if(carabayar.Text == "CEK GIRO")
				{
					NoBG = Cf.Pk(nobg.Text);
					TglBG = Convert.ToDateTime(tglbg.Text);
				}

				DataTable rs = Db.Rs("SELECT "
					+ " NoTTR AS [No. TTR]"
					+ ",NoReservasi AS [No. Reservasi]"
					+ ",CaraBayar AS [Cara Bayar]"
					+ ",Total AS [Nilai TTR]"
					+ " FROM MS_TTR"
					+ " WHERE NoTTR = '" + NoTTR + "'"
					);

				DataTable rsBef = Db.Rs("SELECT "
					+ " CONVERT(varchar, TglTTR, 106) AS [Tanggal TTR]"
					+ ",Ket AS [Keterangan]"
					+ ",NoBG AS [No. BG]"
					+ ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
					+ ",Unit"
					+ ",Customer"
					+ ",ManualTTR AS [Manual TTR]"
					+ " FROM MS_TTR"
					+ " WHERE NoTTR = '" + NoTTR + "'"
					);

				Db.Execute("EXEC spTTREdit"
					+ " '" + NoTTR + "'"
					+ ", '" + TglTTR + "'"
					+ ", '" + Unit + "'"
					+ ", '" + Customer + "'"
					+ ", '" + Ket + "'"
					+ ", '" + NoBG + "'"
					+ ", '" + TglBG + "'"
					+ ", '" + ManualTTR + "'"
					);

				DataTable rsAft = Db.Rs("SELECT "
					+ " CONVERT(varchar, TglTTR, 106) AS [Tanggal TTR]"
					+ ",Ket AS [Keterangan]"
					+ ",NoBG AS [No. BG]"
					+ ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
					+ ",Unit"
					+ ",Customer"
					+ ",ManualTTR AS [Manual TTR]"
					+ " FROM MS_TTR"
					+ " WHERE NoTTR = '" + NoTTR + "'"
					);
				
				//Logfile
				string ketlog = Cf.LogCapture(rs)
					+ Cf.LogCompare(rsBef, rsAft);

				Db.Execute("EXEC spLogTTR"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + ketlog + "'"
					+ ",'" + NoTTR + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTR_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoUnit = (SELECT NoUnit FROM MS_TTR WHERE NoTTR = '" + NoTTR + "')");
                Db.Execute("UPDATE MS_TTR_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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
			if(Save()) Response.Redirect("TTREdit.aspx?done=1&NoTTR=" + NoTTR);
		}

		private string NoTTR
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoTTR"]);
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
