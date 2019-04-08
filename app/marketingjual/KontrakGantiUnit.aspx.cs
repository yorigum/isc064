using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakGantiUnit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Js.Focus(this, nokontrak);
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a');";
                frm.Visible = false;
                tglgu.Text = Cf.Day(DateTime.Today);
            }
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            btnpop2.Attributes.Add("modal-url", "DaftarUnit.aspx?gu=1&status=a&project=" + Project);
            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Jalankan prosedur Pindah Unit?\\nProses ini akan merubah data kepemilikan unit properti.");

            if (Request.QueryString["NoKontrak"] != null)
            {
                pilih.Visible = false;
                frm.Visible = true;
                nokontrak.Text = Request.QueryString["NoKontrak"];
                nostock.Text = Request.QueryString["NoStock"];
                Func.KontrakHeader(Request.QueryString["NoKontrak"], nokontrakl, unit, customer, agent);
                Fill();
            }

        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                        + "Pindah Unit Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");

            if (c == 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );

            return x;
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                int count = Db.SingleInteger("SELECT COUNT(*) FROM MS_APPROVAL WHERE SumberID = '" + NoKontrak + "' AND Sumber = '" + Str.Approval("2") + "' AND Status <> 'DONE'");
                if (count == 0)
                {
                    pilih.Visible = false;
                    frm.Visible = true;

                    Js.Focus(this, nostock);
                    nostock.Attributes["ondblclick"] = "popDaftarUnit('a');";

                    Fill();
                    Js.Confirm(this, "Jalankan prosedur Pindah Unit?\\nProses ini akan merubah data kepemilikan unit properti.");
                }
                else
                {
                    pilih.Visible = true;
                    feed1.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Proses Ganti Unit untuk Kontrak tersebut belum selesai.";
                    feed1.Attributes["style"] = "background-color:white;color:red;";
                }
            }
        }

        private void Fill()
        {
            string project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            //tbstok.Attributes["onclick"] = "location.href='TabelStok.aspx?gu=1&NoKontrak=" + NoKontrak + "&project=" + project + "'";
            nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT "
                + " NoStock"
                + ", TglKontrak"
                + " FROM MS_KONTRAK"
                + " WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nostockl.Text = rs.Rows[0]["NoStock"].ToString();

                TimeSpan ts = DateTime.Today.Subtract(Convert.ToDateTime(rs.Rows[0]["TglKontrak"]));
                if (ts.Days > 30)
                {
                    //lblValid.Text = "SURAT PESANAN SUDAH BERUMUR LEBIH DARI 30 HARI";
                    save.Enabled = true; //false;
                }
                else
                    lblValid.Text = "";
            }
        }

        private bool datavalid()
        {
            bool x = true;
            string s = "";
            nostock.Text = NoStock;

            try
            {
                string lama = Cf.Pk(nostockl.Text);

                int c = Db.SingleInteger("SELECT COUNT(*) "
                    + " FROM MS_UNIT"
                    + " WHERE NoStock = '" + NoStock + "'"
                    + " AND NoStock <> '" + lama + "'"
                    + " AND Status = 'A'"
                    );

                if (c == 0)
                {
                    if (s == "") s = nostock.ID;
                    x = false;
                }
            }
            catch { x = false; if (s == "") s = nostock.ID; }

            if (!Cf.isMoney(nilaibiaya))
            {
                x = false;
                if (s == "") s = nilaibiaya.ID;
                nilaibiayac.Text = "Angka";
            }
            else
            {
                int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A' AND PPJB = 'D'");
                if (c > 0 && Convert.ToDecimal(nilaibiaya.Text) == 0)
                {
                    x = false;
                    if (s == "") s = nilaibiaya.ID;
                    nilaibiayac.Text = "Angka > 0";
                }
                else
                    nilaibiayac.Text = "";
            }

            if (!Cf.isTgl(tglgu))
            {
                x = false;
                if (s == "") s = tglgu.ID;
                tglguc.Text = "Tanggal";
            }
            else
                tglguc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Unit tersebut tidak terdaftar.\\n"
                    + "2. Unit dalam kondisi diblokir.\\n"
                    + "3. Unit lama dan unit baru harus berbeda.\\n"
                    + "4. Biaya Administrasi harus berupa angka."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected bool filevalid()
        {
            bool x = true;
            string s = "";

            if (file.PostedFile.FileName.Length != 0
                && !file.PostedFile.FileName.EndsWith(".jpg"))
            {
                x = false;

                if (s == "")
                    s = file.ID;
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "Proses Upload Gagal.\\n"
                    + "File yang boleh di-upload adalah file dengan extension .jpg saja."
                    , "document.getElementById('" + s + "').focus();"
                    );
            }

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                string c = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'ApprovGantiUnit" + Project + "'");
                decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);

                int nomor = Db.SingleInteger("SELECT COUNT(*) FROM MS_APPROVAL");
                nomor++;
                string NoApproval = nomor.ToString().PadLeft(7, '0');
                if (c == "True")
                {
                    if (filevalid())
                    {
                        DataTable rsBef = Db.Rs("SELECT "
                            + " NoStock AS [No. Stock]"
                            + ",NoUnit AS [Unit]"
                            + ",Luas AS [Luas]"
                            + ",Gross AS [Nilai Gross]"
                            + " FROM MS_KONTRAK"
                            + " WHERE NoKontrak = '" + NoKontrak + "'");

                        string NoStockOld = Db.SingleString(
                            "SELECT NoStock FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                        //Update TempGU, TempBiayaGU                        
                        int count = Db.SingleInteger("SELECT COUNT (*) FROM MS_APPROVAL_GU WHERE UnitBaru ='" + NoStock + "' AND NoApproval IN (SELECT NoApproval FROM MS_APPROVAL WHERE Sumber = '" + Str.Approval("2") + "' AND Status <> 'DONE')");
                        if (count > 0)
                        {
                            nostockc.Text = "Unit Tidak Valid";

                            Js.Alert(
                                this
                                , "Unit Tidak Valid.\\n\\n"
                                + "Kemungkinan Sebab :\\n"
                                + "1. Unit sudah dijual kepada customer lain.\\n"
                                , "document.getElementById('nostock').focus();"
                                + "document.getElementById('nostock').select();"
                                );
                        }
                        else
                        {
                            //INSERT KE MS_APPROVAL
                            Db.Execute("EXEC spApproval"
                                + "'" + NoApproval + "'"
                                + ",'" + Str.Approval("2") + "'"//untuk ganti unit
                                + ",'" + NoKontrak + "'"
                                + ",'" + Convert.ToDateTime(tglgu.Text) + "'"
                                + ",'" + Project + "'"
                                );

                            //insert siapa aja yang berhak approve ke ms_approval_detil 
                            DataTable rs2 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 2 AND Project = '" + Project + "'");
                            for (int i = 0; i < rs2.Rows.Count; i++)
                            {
                                Db.Execute("EXEC spApprovalDetil"
                                    + "'" + NoApproval + "'"
                                    + ",'" + (i + 1) + "'"
                                    + ",'" + rs2.Rows[i]["UserID"].ToString() + "'"//dari Textbox
                                    + "," + rs2.Rows[i]["Lvl"]
                                    + ",'" + Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + rs2.Rows[i]["UserID"].ToString() + "'") + "'"
                                    );
                            }

                            //insert perubahan unit nya ke ms_approval_gu
                            Db.Execute("EXEC spKontrakGantiUnitTemp"
                                + "'" + NoApproval + "'"
                                + ",'" + NoKontrak + "'"
                                + ",'" + NoStock + "'"//dari Textbox
                                + ",'" + NilaiBiaya + "'"
                                + ",'" + Convert.ToDateTime(tglgu.Text) + "'"
                                + ",'" + Cf.Str(baru.Text) + "'"
                                );

                            string NoStockNew = Db.SingleString(
                                "SELECT UnitBaru FROM MS_APPROVAL_GU WHERE NoApproval = '" + NoApproval + "'");

                            if (NoStockNew == NoStockOld)
                            {
                                nostockc.Text = "Unit Tidak Valid";

                                Js.Alert(
                                    this
                                    , "Unit Tidak Valid.\\n\\n"
                                    + "Kemungkinan Sebab :\\n"
                                    + "1. Unit sudah dijual kepada customer lain.\\n"
                                    , "document.getElementById('nostock').focus();"
                                    + "document.getElementById('nostock').select();"
                                    );
                            }
                            else
                            {
                                //Data Unit Baru dari MS_UNIT
                                DataTable rsAft = Db.Rs("SELECT "
                                    + " NoStock AS [No. Stock]"
                                    + ",NoUnit AS [Unit]"
                                    + ",Luas AS [Luas]"
                                    + ",PriceList AS [Nilai Gross]"
                                    + " FROM MS_UNIT"
                                    + " WHERE NoStock = '" + NoStockNew + "'");

                                /*Insert jurnal kontrak*/
                                string strKetJurnal = "KONTRAK Pindah Unit<br />" + Cf.Str(baru.Text);

                                Db.Execute("EXEC spJurnalKontrak "
                                    + " '" + Act.UserID + "'"
                                    + ",'" + NoKontrak + "'"
                                    + ",'" + strKetJurnal + "'"
                                    );

                                if (file.PostedFile.FileName.Length != 0)
                                {
                                    long JurnalID = Db.SingleLong("SELECT TOP 1 JurnalID FROM MS_KONTRAK_JURNAL ORDER BY JurnalID DESC");
                                    string path = Request.PhysicalApplicationPath
                                        + "JurnalKontrak\\" + JurnalID + ".jpg";
                                    Dfc.UploadFile(".jpg", path, file);
                                }
                                /***********************/

                                string Ket = Cf.LogCompare(rsBef, rsAft)
                                    + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                                    + "<br>Tgl Pindah Unit : " + Cf.Day(tglgu.Text)
                                    ;

                                Db.Execute("EXEC spLogKontrak "
                                    + " 'GU'"
                                    + ",'" + Act.UserID + "'"
                                    + ",'" + Act.IP + "'"
                                    + ",'" + Ket + "'"
                                    + ",'" + NoKontrak + "'"
                                    );

                                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                                Func.CekKomisi(NoKontrak);

                                //Push notif ke Approval selanjutnya
                                string DeptID = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                                DataTable rsNextApp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 2 "
                                    + " AND Lvl = 1 AND Project = '" + DeptID + "'");

                                for (int i = 0; i < rsNextApp.Rows.Count; i++)
                                {
                                    string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                                    LibApi.PushNotif("APR-GU", "Permohonan Approval Pindah Unit " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                                }

                                Response.Redirect("KontrakGantiUnit.aspx?done=" + NoKontrak);
                            }
                        }
                    }
                }
                else
                {
                    if (filevalid())
                    {
                        DataTable rsBef = Db.Rs("SELECT "
                            + " NoStock AS [No. Stock]"
                            + ",NoUnit AS [Unit]"
                            + ",Luas AS [Luas]"
                            + ",Gross AS [Nilai Gross]"
                            + ",NilaiKontrak AS [Nilai Kontrak]"
                            + ",DiskonRupiah AS [Diskon dalam Rupiah]"
                            + ",DiskonPersen AS [Diskon dalam Persen]"
                            + ",NilaiPPN AS [Nilai PPN]"
                            + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK"
                            + " WHERE NoKontrak = '" + NoKontrak + "'");

                        string NoStockOld = Db.SingleString(
                            "SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                        decimal biaya = Convert.ToDecimal(nilaibiaya.Text);

                        //INSERT KE MS_APPROVAL
                        Db.Execute("INSERT INTO MS_APPROVAL VALUES ("
                            + "'" + NoApproval + "'"
                            + ",'" + Str.Approval("2") + "'"//untuk ganti unit
                            + ",'" + NoKontrak + "'"
                            + ",'" + Convert.ToDateTime(tglgu.Text) + "'"
                            + ",'DONE'"
                            + ",'" + Convert.ToDateTime(tglgu.Text) + "'"
                            + ",'" + Project + "')"
                            );

                        //insert perubahan unit nya ke ms_approval_gu
                        Db.Execute("EXEC spKontrakGantiUnitTemp"
                            + "'" + NoApproval + "'"
                            + ",'" + NoKontrak + "'"
                            + ",'" + NoStock + "'"//dari Textbox
                            + ",'" + NilaiBiaya + "'"
                            + ",'" + Convert.ToDateTime(tglgu.Text) + "'"
                            + ",'" + Cf.Str(baru.Text) + "'"
                            );

                        //GANTI UNIT NYA
                        Db.Execute("EXEC ISC064_MARKETINGJUAL..spKontrakGantiUnit "
                            + " '" + NoKontrak + "'"
                            + ",'" + NoStock + "'"
                            // + ",'" + Tgl + "'"
                            );

                        string NoUnitBaru = Db.SingleString("SELECT NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoStock = '" + NoStock + "'");

                        //update nounit di kontrak
                        Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK SET NoUnit = '" + NoUnitBaru + "' WHERE NoKontrak = '" + NoKontrak + "'");

                        //Insert tagihan
                        if (biaya != 0)
                        {
                            Db.Execute("EXEC ISC064_MARKETINGJUAL..spTagihanDaftar "
                                + " '" + NoKontrak + "'"
                                + ",'BIAYA ADM. Pindah Unit'"
                                + ",'" + Cf.Day(DateTime.Today) + "'"
                                + ", " + biaya
                                + ",'ADM'"
                                );

                            int NoUrut = Db.SingleInteger("SELECT TOP 1 NoUrut FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut DESC");
                            Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN SET Jenis = 'Pindah Unit' WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoUrut);

                        }

                        //UPDATE Nilai PPN , Nilai Kontrak Terbaru, PPNPemerintah, ApprovalGU
                        decimal GrossBaru = Db.SingleDecimal("SELECT Pricelist FROM MS_UNIT WHERE NoUnit = '" + NoUnitBaru + "'");
                        decimal DiskonRupiah = Db.SingleDecimal("SELECT DiskonRupiah FROM MS_KONTRAK WHERE NoKOntrak = '" + NoKontrak + "'");
                        decimal DiskonTambahan = Db.SingleDecimal("SELECT DiskonTambahan FROM MS_KONTRAK WHERE NoKOntrak = '" + NoKontrak + "'");
                        decimal BungaRupiah = Db.SingleDecimal("SELECT BungaNominal FROM MS_KONTRAK WHERE NoKOntrak = '" + NoKontrak + "'");
                        string ParamID = "PLIncludePPN" + Project;
                        decimal DPP = 0, NilaiPPN = 0, NilaiKontrak = 0;
                        bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";
                        bool jenisppn = Db.SingleBool("SELECT PPN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");
                        decimal Netto = GrossBaru + BungaRupiah - DiskonRupiah - DiskonTambahan;
                        if (jenisppn)
                        {
                            if (includeppn)
                            {
                                DPP = Math.Round(Netto / (decimal)1.1);
                                NilaiPPN = Netto - DPP;
                            }
                            else
                            {
                                DPP = Netto;
                                NilaiPPN = (DPP * (decimal)0.1);
                            }
                        }
                        else
                        {
                            DPP = Netto;
                        }

                        Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK "
                                    + " SET NilaiPPN='" + NilaiPPN + "'"
                                    + ", NilaiKontrak='" + NilaiKontrak + "'"
                                    + ", NilaiDPP='" + DPP + "'"
                                    + ", Gross='" + GrossBaru + "'"
                                    + ", ApprovalGU = '" + Convert.ToBoolean(0) + "'"
                                    + ", Revisi = Revisi + 1"
                                    + " WHERE NoKontrak='" + NoKontrak + "'"
                                    );


                        decimal pl = Db.SingleDecimal("SELECT ISNULL(PriceList, 0) FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                        
                        //string RumusDiskon = Db.SingleString("SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + crbyt.SelectedValue);

                        DataTable rsAft = Db.Rs("SELECT "
                                    + " NoStock AS [No. Stock]"
                                    + ",NoUnit AS [Unit]"
                                    + ",Luas AS [Luas]"
                                    + ",Gross AS [Nilai Gross]"
                                    + ",NilaiKontrak AS [Nilai Kontrak]"
                                    + ",DiskonRupiah AS [Diskon dalam Rupiah]"
                                    + ",DiskonPersen AS [Diskon dalam Persen]"
                                    + ",NilaiPPN AS [Nilai PPN]"
                                    + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK"
                                    + " WHERE NoKontrak = '" + NoKontrak + "'");


                        /*Ganti nomor unit di MS_TTS*/
                        string strNoUnit = Cf.Str(Db.SingleString("SELECT NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoStock = '" + NoStock + "'"));
                        string strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                            + " SET Unit = '" + strNoUnit + "'"
                            + " WHERE Ref = '" + NoKontrak + "'"
                            + " AND Tipe = 'JUAL'"
                            ;
                        Db.Execute(strSql);
                        /*******************************/

                        /*Ganti nomor unit di MS_MEMO*/
                        strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_MEMO"
                            + " SET Unit = '" + strNoUnit + "'"
                            + " WHERE Ref = '" + NoKontrak + "'"
                            + " AND Tipe = 'JUAL'"
                            ;
                        Db.Execute(strSql);
                        /*******************************/

                        /*Ganti nomor unit di MS_PJT*/
                        strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PJT"
                            + " SET Unit = '" + strNoUnit + "'"
                            + " WHERE Ref = '" + NoKontrak + "'"
                            + " AND Tipe = 'JUAL'"
                            ;
                        Db.Execute(strSql);
                        /*******************************/

                        /*Ganti nomor unit di MS_TUNGGAKAN*/
                        strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN"
                            + " SET Unit = '" + strNoUnit + "'"
                            + " WHERE Ref = '" + NoKontrak + "'"
                            + " AND Tipe = 'JUAL'"
                            ;
                        Db.Execute(strSql);
                        /*******************************/

                        string Ket = Cf.LogCompare(rsBef, rsAft)
                                    + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                                    + "<br>Tgl Pindah Unit : " + Cf.Day(tglgu.Text)
                                    ;

                        /*Insert jurnal kontrak*/
                        string strKetJurnal = "KONTRAK Pindah Unit<br />" + Cf.Str(baru.Text);

                        Db.Execute("EXEC spJurnalKontrak "
                            + " '" + Act.UserID + "'"
                            + ",'" + NoKontrak + "'"
                            + ",'" + strKetJurnal + "'"
                            );

                        if (file.PostedFile.FileName.Length != 0)
                        {
                            long JurnalID = Db.SingleLong("SELECT TOP 1 JurnalID FROM MS_KONTRAK_JURNAL ORDER BY JurnalID DESC");
                            string path = Request.PhysicalApplicationPath
                                + "JurnalKontrak\\" + JurnalID + ".jpg";
                            Dfc.UploadFile(".jpg", path, file);
                        }
                        /***********************/

                        Db.Execute("EXEC spLogKontrak "
                            + " 'GU'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + Ket + "'"
                            + ",'" + NoKontrak + "'"
                            );

                        decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                        Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                        Func.CekKomisi(NoKontrak);

                        //Push notif ke Approval selanjutnya
                        DataTable rsNextApp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 2 "
                            + " AND Lvl = 1");

                        for (int i = 0; i < rsNextApp.Rows.Count; i++)
                        {
                            string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                            LibApi.PushNotif("APR-GU", "Permohonan Approval Pindah Unit " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                        }

                        Response.Redirect("KontrakGantiUnit.aspx?done=" + NoKontrak);
                    }
                }
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
            }
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(nostock.Text);
            }
        }

        private string Project
        {
            get
            {
                return Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
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
