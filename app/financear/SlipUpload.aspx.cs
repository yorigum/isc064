using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class SlipUpload : System.Web.UI.Page
	{
		protected string from = "";
		protected string to = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Js.Confirm(this,"Lanjutkan proses upload slip setoran?");
			feed.Text = "";
		}

		protected void upload_Click(object sender, System.EventArgs e)
		{
			if(!file.PostedFile.FileName.EndsWith(".xls"))
			{
				Js.Alert(
					this
					, "Proses Upload Gagal.\\n"
					+ "File yang boleh di-upload adalah file dengan extension .xls saja."
					, ""
					);
			}
			else
			{
				string path = Request.PhysicalApplicationPath
					+ "Template\\Slip_" + Session.SessionID + ".xls";
				
				Dfc.UploadFile(".xls", path, file);

				Cek(path);
				
				//Hapus file sementara tersebut dari hard-disk server
				Dfc.DeleteFile(path);
			}
		}

		private void Cek(string path)
		{
			string strSql = "SELECT * FROM [SLIP$]";
			DataTable rs = new DataTable();

			try
			{
				rs = Db.xls(strSql, path);
			}
			catch {}

			if(Rpt.ValidateXls(rs, rule, gagal))
				Save(path);
		}

		private void Save(string path)
		{
			int total = 0;

			string strSql = "SELECT * FROM [SLIP$]";
			DataTable rs = Db.xls(strSql, path);

			int NoSlip = Db.SingleInteger("SELECT ISNULL(MAX(NoSlip), 0) FROM MS_TTS");
			NoSlip++;

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected) break;

				if(Save(rs, i, NoSlip))
					total++;
			}

			feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
				+ "<a href='SlipLaporan.aspx?NoSlip=" + NoSlip + "'>"
				+ "Upload Berhasil  : " + total + " baris data"
				+ "</a>"
				;
		}

		private bool Save(DataTable rs, int i, int NoSlip)
		{
			string NoBG = Cf.Pk(rs.Rows[i][0]);
			DateTime TglSlipSetoran = Convert.ToDateTime(rs.Rows[i][1]);

			string strSql = "SELECT *"
				+ " FROM MS_TTS"
				+ " WHERE (NoSlip = 0"
				+ " OR TglSetoran IS NULL)"
				+ " AND NoBG = '" + NoBG + "'"
				;
			DataTable rsSlip = Db.Rs(strSql);

			for(int j = 0; j < rsSlip.Rows.Count; j++)
			{
				if(!Response.IsClientConnected)
					break;

				string NoTTS = Cf.Pk(rsSlip.Rows[j]["NoTTS"]);

				DataTable rsHead = Db.Rs("SELECT "
					+ " NoTTS AS [No. TTS]"
					+ ",Tipe"
					+ ",Ref AS [Ref.]"
					+ ",CaraBayar AS [Cara Bayar]"
					+ ",Total AS [Nilai TTS]"
					+ " FROM MS_TTS"
					+ " WHERE NoTTS = " + NoTTS
					);

				DataTable rsBef = Db.Rs("SELECT "
					+ " CONVERT(varchar, TglTTS, 106) AS [Tanggal TTS]"
					+ ",CONVERT(varchar, TglBKM, 106) AS [Tanggal BKM]"
					+ ",Ket AS [Keterangan]"
					+ ",NoBG AS [No. BG]"
					+ ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
					+ ",Titip AS [Pengelola BG]"
					+ ",Unit"
					+ ",Customer"
					+ ",Pph AS [PPH]"
					+ ",ManualTTS AS [Manual TTS]"
					+ ",ManualBKM AS [Manual BKM]"
					+ ", Acc AS [Rekening Bank]"
					+ ", NoSlip AS [No. Slip]"
					+ ", TglSetoran AS [Tgl. Slip Setoran]"
					+ " FROM MS_TTS"
					+ " WHERE NoTTS = " + NoTTS
					);

				Db.Execute("UPDATE MS_TTS"
					+ " SET NoSlip = " + NoSlip
					+ ", TglSetoran = '" + TglSlipSetoran + "'"
					+ " WHERE NoTTS = " + NoTTS
					);

				DataTable rsAft = Db.Rs("SELECT "
					+ " CONVERT(varchar, TglTTS, 106) AS [Tanggal TTS]"
					+ ",CONVERT(varchar, TglBKM, 106) AS [Tanggal BKM]"
					+ ",Ket AS [Keterangan]"
					+ ",NoBG AS [No. BG]"
					+ ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
					+ ",Titip AS [Pengelola BG]"
					+ ",Unit"
					+ ",Customer"
					+ ",Pph AS [PPH]"
					+ ",ManualTTS AS [Manual TTS]"
					+ ",ManualBKM AS [Manual BKM]"
					+ ", Acc AS [Rekening Bank]"
					+ ", NoSlip AS [No. Slip]"
					+ ", TglSetoran AS [Tgl. Slip Setoran]"
					+ " FROM MS_TTS"
					+ " WHERE NoTTS = " + NoTTS
					);

				//Logfile
				string ketlog = Cf.LogCapture(rsHead)
					+ Cf.LogCompare(rsBef, rsAft);

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

            }

            return true;
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
