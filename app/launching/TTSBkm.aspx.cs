using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class TTSBkm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("NoTTS");

            DataTable rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS + " AND Status = 'BARU' AND Acc <> '' AND Acc <> '0'");

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
                    + " FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

                string StatusLama = rs.Rows[0]["Status"].ToString();

                #region logfile
                string Tipe = Db.SingleString("SELECT Tipe FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);
                string Tb = Sc.MktTb(Tipe);

                string strSql = "";
                if (Tipe != "TENANT")
                {
                    strSql = "SELECT "
                        + " CASE NoTagihan"
                        + "		WHEN 0 THEN 'UNALLOCATED    ' + CONVERT(varchar,NilaiPelunasan,1)"
                        + "		ELSE (SELECT NamaTagihan FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
                        + "          + '    ' + CONVERT(varchar,NilaiPelunasan,1)"
                        + " END AS NamaTagihan"
                        + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN AS l "
                        + " WHERE NoTTS = " + NoTTS;
                }
                else
                {
                    strSql = "SELECT "
                        + " NamaTagihan + '    ' + CONVERT(varchar,NilaiTagihan,1) "
                        + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN AS l "
                        + " WHERE NoTTS = " + NoTTS;
                }
                #endregion
                DataTable rsDetil = Db.Rs(strSql);

                string NoBKM = AutoID;

                Db.Execute("EXEC ISC064_FINANCEAR..spPostingTTS " + NoTTS + ",'" + NoBKM + "','" + TglBKM + "'");
                Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET ManualBKM = ManualTTS WHERE NoTTS = " + NoTTS);

                //Ambil Stok No. FP
                DataTable fp = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_FP WHERE Status = 0");
                if (fp.Rows.Count > 0)
                {
                    Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET"
                        + " NoFPS = '" + fp.Rows[0]["NoFPS"].ToString() + "'"
                        + " WHERE NoTTS = " + NoTTS);

                    Db.Execute("UPDATE ISC064_FINANCEAR..REF_FP SET"
                        + " Status = 1"
                        + " WHERE NoFPS = '" + fp.Rows[0]["NoFPS"].ToString() + "'");
                }

                //Log
                string KetLog = Cf.LogCapture(rsHeader)
                    + Cf.LogList(rsDetil, "ALOKASI PELUNASAN")
                    ;

                Db.Execute("EXEC ISC064_FINANCEAR..spLogTTS"
                    + " 'POST'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'***BUKA KWITANSI***<br>" + KetLog + "'"
                    + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("TTSEdit.aspx?NoTTS=" + NoTTS + "&done=1");
            }
        }

        private string AutoID
        {
            get
            {
                int c = Db.SingleInteger("SELECT COUNT(NoBKM) FROM ISC064_FINANCEAR..MS_TTS WHERE NoBKM != '' AND YEAR(TglBKM) = " + TglBKM.Year);

                string nobkm = "";
                bool hasfound = false;
                while (!hasfound)
                {
                    if (!Response.IsClientConnected) break;

                    c += 1;
                    //nopjt = c.ToString() + "/" + u + "/" + Convert.ToDateTime(tgl.Text).Year;
                    nobkm = c.ToString().PadLeft(4, '0') + "/TTS-GEM/AKRS/" + Cf.Roman(TglBKM.Month) + "/" + TglBKM.Year;//"GEM-TTS/" + TglTTS.Year + "/" + c.ToString().PadLeft(4, '0');

                    if (isUnique(nobkm)) hasfound = true;
                }

                return nobkm;
            }
        }

        private bool isUnique(string nobkm)
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(NoBKM) FROM ISC064_FINANCEAR..MS_TTS WHERE NoBKM = '" + nobkm + "'");

            if (c == 0)
                return true;
            else
                return false;
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
