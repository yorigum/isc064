using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
	public partial class TTSBkmVoid : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoTTS");

            DataTable rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS + " AND Status = 'POST'");

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
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
                    + " FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

				string StatusLama = rs.Rows[0]["Status"].ToString();

				#region logfile
				string Tipe = Db.SingleString("SELECT Tipe FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = "+NoTTS);
				string Tb = Sc.MktTb(Tipe);
				
				string strSql = "";
				if(Tipe!="TENANT")
				{
					strSql = "SELECT "
						+ " CASE NoTagihan"
						+ "		WHEN 0 THEN 'UNALLOCATED    ' + CONVERT(varchar,NilaiPelunasan,1)"
						+ "		ELSE (SELECT NamaTagihan FROM "+Tb+"..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
						+ "          + '    ' + CONVERT(varchar,NilaiPelunasan,1)"
						+ " END AS NamaTagihan"
						+ " FROM "+Tb+"..MS_PELUNASAN AS l "
						+ " WHERE NoTTS = " + NoTTS;
				}
				else
				{
					strSql = "SELECT "
						+ " NamaTagihan + '    ' + CONVERT(varchar,NilaiTagihan,1) "
						+ " FROM "+Tb+"..MS_TAGIHAN AS l "
						+ " WHERE NoTTS = " + NoTTS;
				}
				#endregion
				DataTable rsDetil = Db.Rs(strSql);

                Db.Execute("EXEC ISC064_FINANCEAR..spPostingTTSVoid " + NoTTS);
                Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET ManualBKM = '' WHERE NoTTS = " + NoTTS);
				//Log
				string KetLog = Cf.LogCapture(rsHeader)
					+ Cf.LogList(rsDetil,"ALOKASI PELUNASAN")
					;

                Db.Execute("EXEC ISC064_FINANCEAR..spLogTTS"
					+ " 'POST'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'***BATAL KWITANSI***<br>" + KetLog + "'"
					+ ",'" + NoTTS.ToString().PadLeft(7,'0') + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("TTSEdit.aspx?NoTTS="+NoTTS+"&done=1");
			}
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
