using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class GiroUpload : System.Web.UI.Page
	{
		protected int from = 0;
		protected int to = 0;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Js.Confirm(this,"Lanjutkan proses upload cek giro sudah cair?");
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
					+ "Template\\Giro_" + Session.SessionID + ".xls";
				
				Dfc.UploadFile(".xls", path, file);

				Cek(path);
				
				//Hapus file sementara tersebut dari hard-disk server
				Dfc.DeleteFile(path);
			}
		}

		private void Cek(string path)
		{
			string strSql = "SELECT * FROM [GIRO$]";
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

			string strSql = "SELECT * FROM [GIRO$]";
			DataTable rs = Db.xls(strSql, path);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected) break;

				if(Save(rs, i))
					total++;
			}

			feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
				+ "<a href=\"javascript:popPrintBKMMulti('" + from + "', '" + to + "');\">"
				+ "Upload Berhasil  : " + total + " baris data"
				+ "</a>"
				;
		}

		private bool Save(DataTable rs, int i)
		{
			string NoBG = Cf.Pk(rs.Rows[i][0]);
			DateTime TglBKM = Convert.ToDateTime(rs.Rows[i][1]);

			string strSql = "SELECT *"
				+ " FROM MS_TTS"
				+ " WHERE NoBG = '" + NoBG + "'"
				+ " AND Status = 'BARU'"
				;
			DataTable rsGiro = Db.Rs(strSql);

			for(int j = 0; j < rsGiro.Rows.Count; j++)
			{
				if(!Response.IsClientConnected)
					break;

				string NoTTS = Cf.Pk(rsGiro.Rows[j]["NoTTS"]);
				string StatusLama = rsGiro.Rows[j]["Status"].ToString();

				DataTable rsHeader = Db.Rs("SELECT "
					+ " CONVERT(varchar, TglTTS, 106) AS [Tanggal]"
					+ ",Tipe"
					+ ",Ref AS [Ref.]"
					+ ",Unit"
					+ ",Customer"
					+ ",CaraBayar AS [Cara Bayar]"
					+ ",Ket AS [Keterangan]"
					+ ",NoSlip AS [Slip Setoran]"
					+ ",NoBG AS [No. BG]"
					+ ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
					+ ",Titip AS [Pengelola BG]"
					+ ",Total"
					+ " FROM MS_TTS WHERE NoTTS = " + NoTTS);

				#region logfile
				string Tipe = Db.SingleString("SELECT Tipe FROM MS_TTS WHERE NoTTS = " + NoTTS);
				string Tb = Sc.MktTb(Tipe);
				
				if(Tipe != "TENANT")
				{
					strSql = "SELECT "
						+ " CASE NoTagihan"
						+ "		WHEN 0 THEN 'UNALLOCATED    ' + CONVERT(varchar,NilaiPelunasan,1)"
						+ "		ELSE (SELECT NamaTagihan FROM "+Tb+"..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
						+ "          + '    ' + CONVERT(varchar,NilaiPelunasan,1)"
						+ " END AS NamaTagihan"
						+ " FROM " + Tb + "..MS_PELUNASAN AS l "
						+ " WHERE NoTTS = " + NoTTS;
				}
				else
				{
					strSql = "SELECT "
						+ " NamaTagihan + '    ' + CONVERT(varchar,NilaiTagihan,1) "
						+ " FROM " + Tb + "..MS_TAGIHAN AS l "
						+ " WHERE NoTTS = " + NoTTS;
				}
				#endregion
				DataTable rsDetil = Db.Rs(strSql);

				Db.Execute("EXEC spPostingTTS " + NoTTS + ",'" + TglBKM + "'");
				Db.Execute("UPDATE MS_TTS SET ManualBKM = NoBKM WHERE NoTTS = " + NoTTS);

				//Log
				string KetLog = Cf.LogCapture(rsHeader)
					+ Cf.LogList(rsDetil, "ALOKASI PELUNASAN")
					;

				Db.Execute("EXEC spLogTTS"
					+ " 'POST'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'***BUKA KWITANSI***<br>" + KetLog + "'"
					+ ",'" + NoTTS.ToString().PadLeft(7,'0') + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                if (from == 0)
					from = Db.SingleInteger("SELECT NoBKM FROM MS_TTS WHERE NoTTS = " + NoTTS);

				to = Db.SingleInteger("SELECT NoBKM FROM MS_TTS WHERE NoTTS = " + NoTTS);
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
