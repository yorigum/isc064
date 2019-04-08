using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class MEMOVoid : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("NoMEMO");

            DataTable rs = Db.Rs("SELECT * FROM MS_MEMO WHERE NoMEMO = " + NoMEMO);


            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                DataTable rsHeader = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglMEMO, 106) AS [Tanggal]"
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
                    + " FROM MS_MEMO WHERE NoMEMO = " + NoMEMO);

                string StatusLama = rs.Rows[0]["Status"].ToString();
                decimal NilaiKembali = Convert.ToDecimal(rs.Rows[0]["Total"]);

                #region logfile
                string Tipe = Db.SingleString("SELECT Tipe FROM MS_MEMO WHERE NoMEMO = " + NoMEMO);
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
                        + " WHERE NoMEMO = " + NoMEMO;
                }
                else
                {
                    strSql = "SELECT "
                        + " NamaTagihan + '    ' + CONVERT(varchar,NilaiTagihan,1) "
                        + " FROM " + Tb + "..MS_TAGIHAN AS l "
                        + " WHERE NoMEMO = " + NoMEMO;
                }
                #endregion
                DataTable rsDetil = Db.Rs(strSql);

                Db.Execute("EXEC spMemoVOID " + NoMEMO);
                Db.Execute("EXEC ISC064_MARKETINGJUAL..spPelunasanVoidMEMO " + NoMEMO);

                Db.Execute("UPDATE MS_TTS SET LebihBayar = 0 WHERE NoTTS = " + rs.Rows[0]["NoTTS"]);

                string StatusBaru = Db.SingleString(
                    "SELECT Status FROM MS_MEMO WHERE NoMEMO = " + NoMEMO);

                if (StatusLama != "VOID")// && StatusBaru=="VOID")
                {
                    string logr = "";
                    if (Request.QueryString["r"] != null)
                    {
                        logr = "REIMBURSE<br>";
                        //Db.Execute("UPDATE MS_MEMO SET NilaiKembali = " + NilaiKembali + " WHERE NoMEMO = " + NoMEMO);

                    }

                    /*Update status Akunting*/
                    int Akunting = Db.SingleInteger("SELECT Akunting FROM MS_MEMO WHERE NoMEMO = " + NoMEMO);

                    if (Akunting == 1)
                    {
                        string NoVoucher = Db.SingleString("SELECT NoVoucher FROM MS_MEMO WHERE NoMEMO = '" + NoMEMO + "'");

                        Akun.InsertAnomali("MEMO", NoMEMO, "", "", "VOID MEMO", "", NoVoucher);
                    }
                    /************************/

                    //Log
                    string KetLog = logr
                        + Cf.LogCapture(rsHeader)
                        + Cf.LogList(rsDetil, "ALOKASI PELUNASAN")
                        ;

                    Db.Execute("EXEC spLogMEMO"
                        + " 'VOID'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + NoMEMO.ToString().PadLeft(7, '0') + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_MEMO_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_MEMO WHERE NoMEMO = '" + NoMEMO + "'");
                    Db.Execute("UPDATE MS_MEMO_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    Response.Redirect("MEMOEdit.aspx?NoMEMO=" + NoMEMO + "&done=1");
                }
                else
                {
                    //Tidak bisa dihapus
                    nodel.Visible = true;
                }
            }
        }

        private string NoMEMO
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoMEMO"]);
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
