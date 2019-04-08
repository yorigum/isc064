using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class TTSBkm : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

            Act.CekInt("NoTTS");

            DataTable rs = Db.Rs("SELECT * FROM MS_TTS WHERE NoTTS = " + NoTTS + " AND Status = 'BARU' AND Acc <> '' AND Acc <> '0'");

            if (rs.Rows.Count == 0)
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
                    + ",NoFPS AS [No. Faktur Pajak]"
                    + " FROM MS_TTS WHERE NoTTS = " + NoTTS);

                string StatusLama = rs.Rows[0]["Status"].ToString();

                #region logfile
                string Tipe = Db.SingleString("SELECT Tipe FROM MS_TTS WHERE NoTTS = " + NoTTS);
                string Tb = Sc.MktTb(Tipe);

                string strSql = "";
                if (Tipe != "TENANT")
                {
                    strSql = "SELECT "
                        + " CASE NoTagihan"
                        + "		WHEN 0 THEN 'UNALLOCATED    ' + CONVERT(varchar,NilaiPelunasan,1)"
                        + "		ELSE (SELECT NamaTagihan FROM " + Tb + "..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
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

                string Project = Db.SingleString("SELECT Project FROM MS_TTS WHERE NoTTS = '" + NoTTS + "'");
                string NoBKM2 = Numerator.BKM(TglBKM.Month, TglBKM.Year,Project);

                DataTable rsDetil = Db.Rs(strSql);

                Db.Execute("EXEC spPostingTTS " + NoTTS + ",'" + TglBKM + "'");
                Db.Execute("UPDATE MS_TTS SET ManualBKM = ManualTTS, NoBKM2='" + NoBKM2 + "', TglFP = '" + TglBKM + "' WHERE NoTTS = " + NoTTS);
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN SET NoBKM2='" + NoBKM2 + "' WHERE NoTTS = " + NoTTS);
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN_KPA SET SudahCair=1 WHERE NoTTS=" + NoTTS);

                //Ambil Stok No. FP
                string TipeTagihan = Db.SingleString("SELECT Tipe FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = (SELECT TOP 1 NoTagihan FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTTS = '" + NoTTS + "')");
                DataTable rsHeader2 = new DataTable();
                if (TipeTagihan == "BF" || TipeTagihan == "DP" || TipeTagihan == "ANG")
                {
                    DataTable fp = Db.Rs("SELECT * FROM REF_FP WHERE Status = 0 AND CONVERT(varchar,TglTerimaFP,112) < = '" + Cf.Tgl112(TglBKM) + "' AND Project = '" + Project + "'"); //disini
                    if (fp.Rows.Count > 0)
                    {                        
                        Db.Execute("UPDATE MS_TTS SET"
                            + " NoFPS = '" + fp.Rows[0]["NoFPS"].ToString() + "'"
                            + " WHERE NoTTS = " + NoTTS);

                        Db.Execute("UPDATE REF_FP SET"
                            + " Status = 1"
                            + " WHERE NoFPS = '" + fp.Rows[0]["NoFPS"].ToString() + "'");

                        rsHeader2 = Db.Rs("SELECT "
                                + "NoFPS AS [No. Faktur Pajak]"
                                + " FROM MS_TTS WHERE NoTTS = " + NoTTS);

                    }
                }

                DataTable StockFPS = Db.Rs("SELECT * FROM REF_FP WHERE CONVERT(varchar,TglTerimaFP,112) <= '" + Cf.Tgl112(Convert.ToDateTime(rs.Rows[0]["TglTTS"])) + "' AND Status = 0 AND Project = '" + Project + "'");
                string kett = "";

                if (StockFPS.Rows.Count >= 0)
                {
                    if (StockFPS.Rows.Count <= 100)
                    {
                        kett = "Sisa No. Faktur Pajak yang tersedia : " + StockFPS.Rows.Count + ". Segera hub pihak Pajak.";
                    }
                    else
                    {
                        kett = "No. Faktur Pajak tersedia.";
                    }
                }

                string noSSP = AutoNoSSP();
                Db.Execute("UPDATE MS_TTS SET NoSSP = '" + noSSP + "' WHERE NoTTS = '" + NoTTS + "' AND Project = '" + Project + "'");

                //                Log
                string KetLog = Cf.LogCapture(rsHeader)
                    + Cf.LogCapture(rsHeader2)
                    + Cf.LogList(rsDetil, "ALOKASI PELUNASAN")
                    + Environment.NewLine
                    + "Warning : " + kett
                    ;

                Db.Execute("EXEC spLogTTS"
                    + " 'POST'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'***BUKA KWITANSI***<br>" + KetLog + "'"
                    + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");                
                Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("TTSEdit.aspx?NoTTS=" + NoTTS + "&done=1");
            }
        }

        private string AutoNoSSP()
        {
            string NoSSP = "RV00000";
            int NoAkhir = 0;

            int adaSSP = Db.SingleInteger("SELECT COUNT(*) FROM MS_TTS WHERE MONTH(TglBKM) = " + TglBKM.Month + " AND YEAR(TglBKM) = " + TglBKM.Year + " AND NoSSP != ''");
            string LastNoSSP = Db.SingleString("SELECT TOP 1 ISNULL(NoSSP,'RV00000') FROM MS_TTS WHERE MONTH(TglBKM) = " + TglBKM.Month + " AND YEAR(TglBKM) = " + TglBKM.Year + " AND NoSSP != '' AND Status = 'POST' ORDER BY NoSSP DESC");

            if (adaSSP > 0)
            {
                string aa = LastNoSSP.Substring(2);
                NoAkhir = Convert.ToInt32(aa);
            }

            NoSSP = "RV" + (NoAkhir + 1).ToString().PadLeft(5, '0');

            return NoSSP;
        }

        private bool isUnique(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_TTS WHERE NoBKM2 = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
        }
		private string NoTTS
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoTTS"]);
			}
		}

        //Tambahan
        private DateTime TglBKM
        {
            get
            {
                return Convert.ToDateTime(Request.QueryString["TglBKM"]);
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
