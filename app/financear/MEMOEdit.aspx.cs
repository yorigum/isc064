using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class MEMOEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("NoMEMO");

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                Fill();
            }

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Berhasil...";
            }
        }

        private void Fill()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_MEMO_LOG&Pk=" + NoMEMO.PadLeft(7, '0') + "'";
            btnvoid.Attributes["onclick"] = "if(confirm('"
                + "Apakah anda ingin membatalkan MEMO nomor : " + NoMEMO + " ?\\n"
                + "Perhatian bahwa proses ini TIDAK bisa dibalik."
                + "'))"
                + "{location.href='MEMOVoid.aspx?NoMEMO=" + NoMEMO + "'}";
            string Project = Db.SingleString("SELECT Project FROM MS_MEMO WHERE NoMemo = '" + NoMEMO + "'");
            printMEMO.HRef = "PrintMEMO.aspx?NoMEMO="+NoMEMO+"&project="+Project;

            string strSql = "SELECT * "
                + ",CASE CaraBayar"
                + "		WHEN 'TN' THEN 'TUNAI'"
                + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                + "		WHEN 'BG' THEN 'CEK GIRO'"  //jangan diganti!!!!!!! bisa merubah flow program dibawah
                + "		WHEN 'UJ' THEN 'UANG JAMINAN'"
                + "		WHEN 'DN' THEN 'DISKON'"
                + "		WHEN 'MB' THEN 'MERCHANT BANKING'"
                + "     WHEN 'PP' THEN 'PENGHAPUSAN PIUTANG'"
                + "     WHEN 'TG' THEN 'TUKAR GULING'"
                + "     WHEN 'AL' THEN 'ALOKASI LEBIH BAYAR'"
                + " END AS CaraBayar2"
                + " FROM MS_MEMO WHERE NoMEMO = " + NoMEMO;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
                string JenisPPN = Db.SingleString("SELECT JenisPPN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Cf.Pk(rs.Rows[0]["Ref"]) + "'");
                string HeaderPajak = "";
                if (JenisPPN == "PEMERINTAH")
                    HeaderPajak = (string)s.GetValue("NoFPSPemerintah", typeof(string));
                else if (JenisPPN == "KONSUMEN")
                    HeaderPajak = (string)s.GetValue("NoFPSKonsumen", typeof(string));

                tglmemo.Text = Cf.Day(rs.Rows[0]["TglMEMO"]);
                ket.Text = rs.Rows[0]["Ket"].ToString();
                tipememo.Text = rs.Rows[0]["CaraBayar2"].ToString();

                unit.Text = rs.Rows[0]["Unit"].ToString();
                customer.Text = rs.Rows[0]["Customer"].ToString();

                printMEMO.InnerHtml = printMEMO.InnerHtml + " (" + rs.Rows[0]["PrintMEMO"] + ")";

                kasir.Text = rs.Rows[0]["UserID"].ToString();
                ip.Text = rs.Rows[0]["IP"].ToString();
                tglInput.Text = Cf.Date(rs.Rows[0]["TglInput"]);
                nilai.Text = Cf.Num(rs.Rows[0]["Total"]);
                pph.Checked = (bool)rs.Rows[0]["Pph"];
                memoinfo.Text = rs.Rows[0]["NoMEMO2"].ToString();
                string stat = rs.Rows[0]["Status"].ToString();
                status.Text = stat;

                if (stat == "VOID")
                {
                    status.ForeColor = Color.Red;
                    btnvoid.Disabled = true;
                }
                else if (stat == "POST")
                {
                    status.ForeColor = Color.Blue;
                }

                FillTb(rs.Rows[0]["Tipe"].ToString());

                if (rs.Rows[0]["Tipe"].ToString() != "TENANT")
                    alokasi.InnerHtml = "<a href='CustomerLunas.aspx?Tipe=" + rs.Rows[0]["Tipe"] + "&Ref=" + rs.Rows[0]["Ref"] + "'>"
                        + "<b>Alokasi Pelunasan</b></a>";

            }
        }

        private void FillTb(string Tipe)
        {
            string Tb = Sc.MktTb(Tipe);
            string strSql = "";

            if (Tipe != "TENANT")
            {
                strSql = "SELECT "
                    + " NilaiPelunasan AS Nilai"
                    + ",NoKontrak + '.' + CONVERT(VARCHAR,NoTagihan) AS RefTagihan"
                    + ",CASE NoTagihan"
                    + "		WHEN 0 THEN 'UNALLOCATED'"
                    + "		ELSE (SELECT NamaTagihan FROM " + Tb + "..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
                    + " END AS NamaTagihan"
                    + " FROM " + Tb + "..MS_PELUNASAN AS l "
                    + " WHERE NoMEMO = " + NoMEMO + " ";
            }
            else
            {
                strSql = "SELECT "
                    + " NilaiTagihan+LebihBayar AS Nilai"
                    + ",Tipe + '.' + CONVERT(VARCHAR,NoUrut) AS RefTagihan"
                    + ",NamaTagihan"
                    + " FROM " + Tb + "..MS_TAGIHAN AS l "
                    + " WHERE NoMEMO = " + NoMEMO + " ";
            }

            System.Text.StringBuilder x = new System.Text.StringBuilder();
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                x.Append("<li style='font:8pt; text-align:left;'>"
                    + "<strong>" + rs.Rows[i]["NamaTagihan"] + "</strong>"
                    + "<br><span style='width:120px;'>No. : " + rs.Rows[i]["RefTagihan"] + "</span><br />"
                    + "Nilai : " + Cf.Num(rs.Rows[i]["Nilai"]) + "</li>");
            }

            detil.InnerHtml = x.ToString();
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tglmemo))
            {
                x = false;
                if (s == "") s = tglmemo.ID;
                tglmemoc.Text = "Tanggal";
            }
            else
                tglmemoc.Text = "";


            if (Cf.isEmpty(unit))
            {
                x = false;
                if (s == "") s = unit.ID;
                unitc.Text = "Kosong";
            }
            else
                unitc.Text = "";

            if (Cf.isEmpty(customer))
            {
                x = false;
                if (s == "") s = customer.ID;
                customerc.Text = "Kosong";
            }
            else
                customerc.Text = "";

            if (!x)
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
            if (valid())
            {
                DateTime TglMEMO = Convert.ToDateTime(tglmemo.Text);
                string Ket = Cf.Str(ket.Text);
                string Unit = Cf.Str(unit.Text);
                string Customer = Cf.Str(customer.Text);

                DataTable rs = Db.Rs("SELECT "
                    + " NoMEMO AS [No. MEMO]"
                    + ",Tipe"
                    + ",Ref AS [Ref.]"
                    + ",CaraBayar AS [Cara Bayar]"
                    + ",Total AS [Nilai MEMO]"
                    + " FROM MS_MEMO"
                    + " WHERE NoMEMO = " + NoMEMO
                    );

                DataTable rsBef = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglMEMO, 106) AS [Tanggal MEMO]"
                    + ",CONVERT(varchar, TglBKM, 106) AS [Tanggal BKM]"
                    + ",Ket AS [Keterangan]"
                    + ",NoBG AS [No. BG]"
                    + ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
                    + ",Titip AS [Pengelola BG]"
                    + ",Unit"
                    + ",Customer"
                    + ",Pph AS [PPH]"
                    + ",ManualMEMO AS [Manual MEMO]"
                    + ",ManualBKM AS [Manual BKM]"
                    + ", Acc AS [Rekening Bank]"
                    + ", NoFPS AS [No. Faktur Pajak]"
                    + " FROM MS_MEMO"
                    + " WHERE NoMEMO = " + NoMEMO
                    );

                Db.Execute("EXEC spMEMOEdit"
                    + " '" + NoMEMO + "'"
                    + ",'" + TglMEMO + "'"
                    + ",'" + Unit + "'"
                    + ",'" + Customer + "'"
                    + ",'" + Ket + "'"
                    );

                Db.Execute("EXEC spSinkronisasiMEMO " + NoMEMO);

                DataTable rsAft = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglMEMO, 106) AS [Tanggal MEMO]"
                    + ",CONVERT(varchar, TglBKM, 106) AS [Tanggal BKM]"
                    + ",Ket AS [Keterangan]"
                    + ",NoBG AS [No. BG]"
                    + ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
                    + ",Titip AS [Pengelola BG]"
                    + ",Unit"
                    + ",Customer"
                    + ",Pph AS [PPH]"
                    + ",ManualMEMO AS [Manual MEMO]"
                    + ",ManualBKM AS [Manual BKM]"
                    + ", Acc AS [Rekening Bank]"
                    + ", NoFPS AS [No. Faktur Pajak]"
                    + " FROM MS_MEMO"
                    + " WHERE NoMEMO = " + NoMEMO
                    );

                /*Update status Akunting*/
                int Akunting = Db.SingleInteger("SELECT Akunting FROM MS_MEMO WHERE NoMEMO = " + NoMEMO);

                if (Akunting == 1)
                {
                    string NoVoucher = Db.SingleString("SELECT NoVoucher FROM MS_MEMO WHERE NoMEMO = " + NoMEMO);

                    Akun.InsertAnomali("MEMO", NoMEMO, Cf.LogCapture(rsBef), Cf.LogCapture(rsAft), "EDIT MEMO", "", NoVoucher);
                }
                /************************/

                //Logfile
                string ketlog = Cf.LogCapture(rs)
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogMEMO"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ketlog + "'"
                    + ",'" + NoMEMO.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_MEMO_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_MEMO WHERE NoMEMO = '" + NoMEMO + "'");
                Db.Execute("UPDATE MS_MEMO_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                return true;
            }
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("MEMOEdit.aspx?done=1&NoMEMO=" + NoMEMO);
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
