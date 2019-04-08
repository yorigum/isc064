using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class TTSSlip : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				cancel.Attributes["onclick"] = "location.href='TTSEdit.aspx?NoTTS="+NoTTS+"'";
				tglsetoran.Text = Cf.Day(DateTime.Today);
				
				Fill();
				Bind();
			}
		}

		private void Fill()
		{
			string strSql = "SELECT * FROM MS_TTS WHERE Status <> 'POST' AND NoTTS = " + NoTTS;
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				noslipl.Text = rs.Rows[0]["NoSlip"].ToString();
				tglsetoranl.Text = Cf.Day(rs.Rows[0]["TglSetoran"]);
				bankl.Text = rs.Rows[0]["Bank"].ToString();
			}
		}

		private void Bind()
		{
			DataTable rs = Db.Rs("SELECT TOP 20 * FROM "
				+ "(SELECT NoSlip, TglSetoran, Bank, COUNT(NoTTS) AS Jumlah, SUM(Total) AS Nilai "
				+ " FROM MS_TTS WHERE NoSlip <> 0 GROUP BY NoSlip,TglSetoran,Bank) AS MS_SLIP "
				+ "ORDER BY NoSlip DESC");
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["NoSlip"].ToString();
				string t = "<span style='width:50px;'><b>"+v+"</b></span> "
					+ "<span style='width:100px;'>" + Cf.Day(rs.Rows[i]["TglSetoran"]) + "</span>"
					+ "<span style='width:150px;'>" + rs.Rows[i]["Bank"] + "</span>"
					+ "<span style='width:80px;text-align:right'>" + rs.Rows[i]["Jumlah"].ToString() + " TTS</span>"
					+ "<span style='width:110px;text-align:right'>" + Cf.Num(rs.Rows[i]["Nilai"]) + "</span>"
					;
				sliplama.Items.Add(new ListItem(t,v));
			}

			if(sliplama.Items.Count!=0)
				sliplama.Items[0].Selected = true;
			else
				btnlama.Disabled = true;
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isInt(noslipl))
			{
				x = false;
				if(s=="") s = noslipl.ID;
				nosliplc.Text = "Angka Bulat";
			}
			else
				nosliplc.Text = "";

			if(!Cf.isTgl(tglsetoranl))
			{
				x = false;
				if(s=="") s = tglsetoranl.ID;
				tglsetoranlc.Text = "Tanggal";
			}
			else
				tglsetoranlc.Text = "";

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					+ "2. Nomor Slip harus berupa angka bulat.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool validbaru()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(tglsetoran))
			{
				x = false;
				if(s=="") s = tglsetoran.ID;
				tglsetoranc.Text = "Tanggal";
			}
			else
				tglsetoranc.Text = "";

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}
		
		protected void btnedit_ServerClick(object sender, System.EventArgs e)
		{
			if(valid())
			{
				DataTable rs = Db.Rs("SELECT "
					+ " NoTTS AS [No. TTS]"
					+ ",Tipe"
					+ ",Ref AS [Ref.]"
					+ ",CaraBayar AS [Cara Bayar]"
					+ ",Total AS [Nilai TTS]"
					+ " FROM MS_TTS"
					+ " WHERE NoTTS = " + NoTTS
					);

				DataTable rsBef = Db.Rs("SELECT "
					+ " NoSlip AS [No. Slip]"
					+ ",CONVERT(varchar, TglSetoran, 106) AS [Tanggal Setoran]"
					+ ",Bank AS [Rekening]"
					+ " FROM MS_TTS"
					+ " WHERE NoTTS = " + NoTTS
					);

				int NoSlip = Convert.ToInt32(noslipl.Text);
				DateTime TglSetoran = Convert.ToDateTime(tglsetoranl.Text);
				string Bank = Cf.Str(bankl.Text);

				if(NoSlip!=0)
				{
					Db.Execute("UPDATE MS_TTS SET "
						+ " NoSlip = " + NoSlip
						+ ",TglSetoran = CONVERT(datetime, '"+TglSetoran+"', 101)"
						+ ",Bank = '" + Bank + "'"
						+ " WHERE NoTTS = " + NoTTS);
				}
				else
				{
					Db.Execute("UPDATE MS_TTS SET "
						+ " NoSlip = 0"
						+ ",TglSetoran = NULL"
						+ ",Bank = ''"
						+ " WHERE NoTTS = " + NoTTS);
				}

				DataTable rsAft = Db.Rs("SELECT "
					+ " NoSlip AS [No. Slip]"
					+ ",CONVERT(varchar, TglSetoran, 106) AS [Tanggal Setoran]"
					+ ",Bank AS [Rekening]"
					+ " FROM MS_TTS"
					+ " WHERE NoTTS = " + NoTTS
					);

				//Logfile
				string ketlog = Cf.LogCapture(rs)
					+ Cf.LogCompare(rsBef,rsAft);

				Db.Execute("EXEC spLogTTS"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + ketlog + "'"
					+ ",'" + NoTTS.ToString().PadLeft(7,'0') + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("TTSEdit.aspx?done=1&NoTTS="+NoTTS);
			}
		}

		protected void btnbaru_ServerClick(object sender, System.EventArgs e)
		{
			if(validbaru())
			{
				DataTable rs = Db.Rs("SELECT "
					+ " NoTTS AS [No. TTS]"
					+ ",Tipe"
					+ ",Ref AS [Ref.]"
					+ ",CaraBayar AS [Cara Bayar]"
					+ ",Total AS [Nilai TTS]"
					+ " FROM MS_TTS"
					+ " WHERE NoTTS = " + NoTTS
					);

				DataTable rsBef = Db.Rs("SELECT "
					+ " NoSlip AS [No. Slip]"
					+ ",CONVERT(varchar, TglSetoran, 106) AS [Tanggal Setoran]"
					+ ",Bank AS [Rekening]"
					+ " FROM MS_TTS"
					+ " WHERE NoTTS = " + NoTTS
					);

				int NoSlip = Db.SingleInteger("SELECT ISNULL(MAX(NoSlip),0)+1 FROM MS_TTS");
				DateTime TglSetoran = Convert.ToDateTime(tglsetoran.Text);
				string Bank = Cf.Str(bank.Text);

				Db.Execute("UPDATE MS_TTS SET "
					+ " NoSlip = " + NoSlip
					+ ",TglSetoran = CONVERT(datetime, '"+TglSetoran+"', 101)"
					+ ",Bank = '" + Bank + "'"
					+ " WHERE NoTTS = " + NoTTS);

				DataTable rsAft = Db.Rs("SELECT "
					+ " NoSlip AS [No. Slip]"
					+ ",CONVERT(varchar, TglSetoran, 106) AS [Tanggal Setoran]"
					+ ",Bank AS [Rekening]"
					+ " FROM MS_TTS"
					+ " WHERE NoTTS = " + NoTTS
					);

				//Logfile
				string ketlog = Cf.LogCapture(rs)
					+ Cf.LogCompare(rsBef,rsAft);

				Db.Execute("EXEC spLogTTS"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + ketlog + "'"
					+ ",'" + NoTTS.ToString().PadLeft(7,'0') + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("TTSEdit.aspx?done=1&NoTTS="+NoTTS);
			}
		}

		protected void btnlama_ServerClick(object sender, System.EventArgs e)
		{
			DataTable rs = Db.Rs("SELECT "
				+ " NoTTS AS [No. TTS]"
				+ ",Tipe"
				+ ",Ref AS [Ref.]"
				+ ",CaraBayar AS [Cara Bayar]"
				+ ",Total AS [Nilai TTS]"
				+ " FROM MS_TTS"
				+ " WHERE NoTTS = " + NoTTS
				);

			DataTable rsBef = Db.Rs("SELECT "
				+ " NoSlip AS [No. Slip]"
				+ ",CONVERT(varchar, TglSetoran, 106) AS [Tanggal Setoran]"
				+ ",Bank AS [Rekening]"
				+ " FROM MS_TTS"
				+ " WHERE NoTTS = " + NoTTS
				);

			int NoSlip = Convert.ToInt32(sliplama.SelectedValue);
			DateTime TglSetoran = Db.SingleTime("SELECT TOP 1 TglSetoran FROM MS_TTS WHERE NoSlip = " + NoSlip);
			string Bank = Cf.Str(Db.SingleString("SELECT TOP 1 Bank FROM MS_TTS WHERE NoSlip = " + NoSlip));

			Db.Execute("UPDATE MS_TTS SET "
				+ " NoSlip = " + NoSlip
				+ ",TglSetoran = CONVERT(datetime, '"+TglSetoran+"', 101)"
				+ ",Bank = '" + Bank + "'"
				+ " WHERE NoTTS = " + NoTTS);

			DataTable rsAft = Db.Rs("SELECT "
				+ " NoSlip AS [No. Slip]"
				+ ",CONVERT(varchar, TglSetoran, 106) AS [Tanggal Setoran]"
				+ ",Bank AS [Rekening]"
				+ " FROM MS_TTS"
				+ " WHERE NoTTS = " + NoTTS
				);

			//Logfile
			string ketlog = Cf.LogCapture(rs)
				+ Cf.LogCompare(rsBef,rsAft);

			Db.Execute("EXEC spLogTTS"
				+ " 'EDIT'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + ketlog + "'"
				+ ",'" + NoTTS.ToString().PadLeft(7,'0') + "'"
				);

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TTS WHERE NoTTS = '" + NoTTS + "')");
            Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Response.Redirect("TTSEdit.aspx?done=1&NoTTS="+NoTTS);
		}

		private string NoTTS
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoTTS"]);
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
