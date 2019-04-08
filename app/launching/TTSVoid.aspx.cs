using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
	public partial class TTSVoid : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoTTS");

			DataTable rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

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
                    + ",NoFPS"
                    + " FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

				string StatusLama = rs.Rows[0]["Status"].ToString();
				decimal NilaiKembali = Convert.ToDecimal(rs.Rows[0]["Total"]);

				#region logfile
                string Tipe = Db.SingleString("SELECT Tipe FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);
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

                Db.Execute("EXEC ISC064_FINANCEAR..spTTSVoid " + NoTTS);
                Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET Total2 = 0 WHERE NoTTS = " + NoTTS);
				string StatusBaru = Db.SingleString(
                    "SELECT Status FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

				if(StatusLama!="VOID" && StatusBaru=="VOID")
				{
					string logr = "";
					if(Request.QueryString["r"]!=null)
					{
						logr = "REIMBURSE<br>";
                        Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET NilaiKembali = " + NilaiKembali + " WHERE NoTTS = " + NoTTS);
					}

                    if (Request.QueryString["fp"] != null)
                    {
                        string nofp = Db.SingleString("SELECT NoFPS FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

                        logr = "BATAL FP<br>";
                        Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET NoFPS = '', PrintFPS = 0 WHERE NoTTS = " + NoTTS);
                        Db.Execute("UPDATE ISC064_FINANCEAR..REF_FP SET Status = 0 WHERE NoFPS = '" + nofp + "'");
                    }

					/*Update status Akunting*/
                    int Akunting = Db.SingleInteger("SELECT Akunting FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

					if(Akunting == 1)
					{
                        string NoVoucher = Db.SingleString("SELECT NoVoucher FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "'");

						Akun.InsertAnomali("TTS", NoTTS, "", "", "VOID TTS", "", NoVoucher);
					}
					/************************/

					//Log
					string KetLog = logr
						+ Cf.LogCapture(rsHeader)
						+ Cf.LogList(rsDetil,"ALOKASI PELUNASAN")
						;

                    Db.Execute("EXEC ISC064_FINANCEAR..spLogTTS"
						+ " 'VOID'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + KetLog + "'"
						+ ",'" + NoTTS.ToString().PadLeft(7,'0') + "'"
						);

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    Response.Redirect("TTSEdit.aspx?NoTTS="+NoTTS+"&done=1");
				}
				else
				{
					//Tidak bisa dihapus
					nodel.Visible = true;
				}
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
