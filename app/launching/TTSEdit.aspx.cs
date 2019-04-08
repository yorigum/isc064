using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace ISC064.LAUNCHING
{
    public partial class TTSEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("NoTTS");

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                fillacc();
                Fill();

                admBank.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                admBank.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                admBank.Attributes["onblur"] = "CalcBlur(this);";

                //kurang bayar
                lebihBayar.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                lebihBayar.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                lebihBayar.Attributes["onblur"] = "CalcBlur(this);";

                //lebih bayar
                lb.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                lb.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                lb.Attributes["onblur"] = "CalcBlur(this);";
            }

            FeedBack();

            Js.Confirm(btnkw,
                "Apakah anda ingin membuka kwitansi untuk TTS nomor : " + NoTTS + " ?"
                );
        }
        protected void btnkw_Click(object sender, EventArgs e)
        {
            
            DendaIlang(NoTTS);
            //
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

        private void fillacc()
        {
            DataTable rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_ACC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                ddlAcc.Items.Add(new ListItem(t, v));
            }
        }

        private void Fill()
        {
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
                + " END AS CaraBayar2"
                + " FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS;
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

                // lblNoFaktur.Text = HeaderPajak;
                tbNoFaktur.Text = rs.Rows[0]["NoFPS"].ToString();
                delfp.Enabled = rs.Rows[0]["NoFPS"].ToString() != "" ? true : false;

                //bkmtr.Visible = false;
                tglbkm.Text = Cf.Day(rs.Rows[0]["TglBKM"]);
                if (rs.Rows[0]["TglBKM"] is DBNull)
                {
                    tglbkm.Text = Cf.Day(rs.Rows[0]["TglTTS"]);
                }

                tgltts.Text = Cf.Day(rs.Rows[0]["TglTTS"]);
                ket.Text = rs.Rows[0]["Ket"].ToString();
                admBank.Text = Cf.Num(Convert.ToDecimal(rs.Rows[0]["AdminBank"]));
                lebihBayar.Text = Cf.Num(Convert.ToDecimal(rs.Rows[0]["LebihBayar"])); //kurang bayar
                lb.Text = Cf.Num(Convert.ToDecimal(rs.Rows[0]["LB"])); //lebih bayar

                btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_TTS_LOG&Pk=" + NoTTS.PadLeft(7, '0') + "'";
                btnslip.Attributes["onclick"] = "location.href='TTSSlip.aspx?NoTTS=" + NoTTS + "'";
                btnvoid.Attributes["onclick"] = "if(confirm('"
                    + "Apakah anda ingin membatalkan TTS nomor : " + NoTTS + " ?\\n"
                    + "Perhatian bahwa proses ini TIDAK bisa dibalik."
                    + "'))"
                    + "{location.href='TTSVoid.aspx?NoTTS=" + NoTTS + "'}";
                btnvoidfp.Attributes["onclick"] = "if(confirm('"
                    + "Apakah anda ingin membatalkan TTS nomor : " + NoTTS + " dan membatalkan faktur pajaknya ?\\n"
                    + "Perhatian bahwa proses ini TIDAK bisa dibalik."
                    + "'))"
                    + "{location.href='TTSVoid.aspx?fp=1&NoTTS=" + NoTTS + "'}";
                btnvoid2.Attributes["onclick"] = "if(confirm('"
                    + "Apakah anda ingin membatalkan dan mengembalikan uang TTS nomor : " + NoTTS + " ?\\n"
                    + "Perhatian bahwa proses ini TIDAK bisa dibalik."
                    + "'))"
                    + "{location.href='TTSVoid.aspx?r=1&NoTTS=" + NoTTS + "'}";
                //btnkw.Attributes["onclick"] = "if(confirm('"
                //    + "Apakah anda ingin membuka kwitansi untuk TTS nomor : "+NoTTS+" ?"
                //    + "'))"
                //    + "{location.href='TTSBkm.aspx?NoTTS="+NoTTS+"&TglBKM="+tglbkm.Text+"'}";
                btnbatalkw.Attributes["onclick"] = "if(confirm('"
                    + "Apakah anda ingin membatalkan kwitansi untuk TTS nomor : " + NoTTS + " ?\\n"
                    + "Perhatian bahwa proses ini TIDAK bisa dibalik."
                    + "'))"
                    + "{location.href='TTSBkmVoid.aspx?NoTTS=" + NoTTS + "'}";
                printTTS.HRef = "PrintTTS.aspx?NoTTS=" + NoTTS;
                printBKM.HRef = "PrintBKM.aspx?NoTTS=" + NoTTS;
				printFPS.HRef = "PrintFPS.aspx?NoTTS=" + NoTTS;

                unit.Text = rs.Rows[0]["Unit"].ToString();
                customer.Text = rs.Rows[0]["Customer"].ToString();

                printTTS.InnerHtml = printTTS.InnerHtml + " (" + rs.Rows[0]["PrintTTS"] + ")";
                printBKM.InnerHtml = printBKM.InnerHtml + " (" + rs.Rows[0]["PrintBKM"] + ")";
                printFPS.InnerHtml = printFPS.InnerHtml + " ("+rs.Rows[0]["PrintFPS"]+")";

                kasir.Text = rs.Rows[0]["UserID"].ToString();
                ip.Text = rs.Rows[0]["IP"].ToString();
                tglInput.Text = Cf.Date(rs.Rows[0]["TglInput"]);

                carabayar.Text = rs.Rows[0]["CaraBayar2"].ToString();
                if (rs.Rows[0]["CaraBayar"].ToString() == "BG")
                {
                    nobg.Text = rs.Rows[0]["NoBG"].ToString();
                    tglbg.Text = Cf.Day(rs.Rows[0]["TglBG"]);
                    titip.Text = rs.Rows[0]["Titip"].ToString();
                    tolak.Text = rs.Rows[0]["Tolak"].ToString();
                    if (tolak.Text != "") tolak.Text = "Tolakan : " + tolak.Text;
                }

                if (rs.Rows[0]["CaraBayar"].ToString() == "KK")
                {
                    nokk.Text = rs.Rows[0]["NoKK"].ToString();
                    bankkk.Text = rs.Rows[0]["BankKK"].ToString();
                }

                nilai.Text = Cf.Num(rs.Rows[0]["Total"]);
                pph.Checked = (bool)rs.Rows[0]["Pph"];

                string stat = rs.Rows[0]["Status"].ToString();
                status.Text = stat;
                if ((decimal)rs.Rows[0]["NilaiKembali"] != 0)
                    status.Text = status.Text
                        + "<br><font style='font-size:9pt'>Reimburse : "
                        + Cf.Num(rs.Rows[0]["NilaiKembali"]) + "</font>";

                if (stat == "VOID")
                {
                    status.ForeColor = Color.Red;

                    //tidak boleh void 2x
                    btnvoid.Disabled = true;
                    btnvoidfp.Disabled = true;
                    btnvoid2.Disabled = true;
                    //tidak bisa akses program-program bkm
                    printBKM.Visible = false;
                    printFPS.Visible = false;
                    btnkw.Enabled = false;
                    btnbatalkw.Disabled = true;
                    manualbkm.Enabled = false;
                    trAdm.Visible = false;
                    trLb.Visible = false;
                    tbNoFaktur.Enabled = false;
                }
                else if (stat == "POST")
                {
                    status.ForeColor = Color.Blue;

                    bkminfo.Text = rs.Rows[0]["NoBKM"].ToString().PadLeft(7, '0')
                        + " (" + Cf.Day(rs.Rows[0]["TglBKM"]) + ")";

                    //gak bisa void sebelum kw-nya dibatalkan
                    btnvoid.Disabled = true;
                    btnvoidfp.Disabled = true;
                    btnvoid2.Disabled = true;
                    btnslip.Disabled = true; //gak bisa edit slip
                    btnkw.Enabled = false; //gak boleh posting kw 2x


                    //set bkm
                    bkmtr.Visible = true;
                    tglbkm.Text = Cf.Day(rs.Rows[0]["TglBKM"]);
                }
                else
                {
                    //belum ada kwitansi
                    printBKM.Visible = false;
                    printFPS.Visible = false;
                    btnbatalkw.Disabled = true;
                    manualbkm.Enabled = false;
                    tbNoFaktur.Enabled = false;

                    if(rs.Rows[0]["NoFPS"].ToString() == "")
                        btnvoidfp.Disabled = true;
                }

                FillTb(rs.Rows[0]["Tipe"].ToString());

                if (rs.Rows[0]["Tipe"].ToString() != "TENANT")
                    alokasi.InnerHtml = "<a href='CustomerLunas.aspx?Tipe=" + rs.Rows[0]["Tipe"] + "&Ref=" + rs.Rows[0]["Ref"] + "'>"
                        + "<b>Alokasi Pelunasan</b></a>";

                manualtts.Text = rs.Rows[0]["ManualTTS"].ToString();
                manualbkm.Text = rs.Rows[0]["ManualBKM"].ToString();

                ddlAcc.Items.Add(new ListItem("Tidak berubah: " + rs.Rows[0]["Acc"].ToString(), rs.Rows[0]["Acc"].ToString()));
                ddlAcc.SelectedIndex = ddlAcc.Items.Count - 1;

                if (Func.CekAkunting(NoTTS))
                    lblAkunting.Text = "Transaksi sudah pernah diposting ke Akunting";
                else
                    lblAkunting.Text = "";

                if (Convert.ToInt16(rs.Rows[0]["SumberBayar"]) == 0)
                    sumberbayar.Text = "Dari Customer";
                else
                    sumberbayar.Text = "Dari Bank";

                // LeBron James, Kobe Bryant, Kevin Durant, Chris Paul, Dwight Howard.

                if (rs.Rows[0]["Acc"].ToString() == "" || rs.Rows[0]["Acc"].ToString() == "0")
                    btnkw.Enabled = false;

                if (Convert.ToBoolean(rs.Rows[0]["FOBO"]))
                {
                    ok.Enabled = save.Enabled = false;
                    btnvoid.Attributes["disabled"] = "true;";
                    btnvoidfp.Attributes["disabled"] = "true;";
                    btnvoid2.Attributes["disabled"] = "true;";
                }

                if (Convert.ToInt32(rs.Rows[0]["PrintBKM"]) > 0)
                {
                    //ok.Enabled = false;
                    //save.Enabled = false;
                    alasan.Text = "Kuitansi Sudah Beredar";
                }

                //Warning untuk kuitansi yang fobonya sudah ditarik
                string fobo = Convert.ToInt32(rs.Rows[0]["FOBO"]).ToString();
                string statTTS = rs.Rows[0]["Status"].ToString();

                if ((fobo == "1") && (statTTS == "POST"))
                {
                    statusFOBO.Text = "Data sudah ditarik ke Accounting";
                    tglbkm.Enabled = tgltts.Enabled = false;
                    btnbatalkw.Disabled = true;
                }

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
                    + " WHERE NoTTS = " + NoTTS + " ";
            }
            else
            {
                strSql = "SELECT "
                    + " NilaiTagihan+LebihBayar AS Nilai"
                    + ",Tipe + '.' + CONVERT(VARCHAR,NoUrut) AS RefTagihan"
                    + ",NamaTagihan"
                    + " FROM " + Tb + "..MS_TAGIHAN AS l "
                    + " WHERE NoTTS = " + NoTTS + " ";
            }

            System.Text.StringBuilder x = new System.Text.StringBuilder();
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                x.Append("<li style='font:8pt'>"
                    + rs.Rows[i]["NamaTagihan"]
                    + "<br><span style='width:120;'>No. : " + rs.Rows[i]["RefTagihan"] + "</span>"
                    + "Nilai : " + Cf.Num(rs.Rows[i]["Nilai"]) + "</li>");
            }

            detil.InnerHtml = x.ToString();
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tgltts))
            {
                x = false;
                if (s == "") s = tgltts.ID;
                tglttsc.Text = "Tanggal";
            }
            else
                tglttsc.Text = "";

            if (bkmtr.Visible)
            {
                if (!Cf.isTgl(tglbkm))
                {
                    x = false;
                    if (s == "") s = tglbkm.ID;
                    tglbkmc.Text = "Tanggal";
                }
                else
                    tglbkmc.Text = "";
            }

            if (!Cf.isMoney(admBank))
            {
                x = false;
                if (s == "") s = admBank.ID;
                nilaic.Text = "Angka";
            }
            else
                nilaic.Text = "";

            if (!Cf.isMoney(lebihBayar))
            {
                x = false;
                if (s == "") s = lebihBayar.ID;
                lebihBayarc.Text = "Angka";
            }
            else
                lebihBayarc.Text = "";

            if (!Cf.isMoney(lb))
            {
                x = false;
                if (s == "") s = lb.ID;
                lbc.Text = "Angka";
            }
            else
                lbc.Text = "";

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

            if (carabayar.Text == "CEK GIRO")
            {
                nobg.Text = Cf.Pk(nobg.Text);
                if (Cf.isEmpty(nobg))
                {
                    x = false;
                    if (s == "") s = nobg.ID;
                    nobgc.Text = "Kosong";
                }
                else
                    nobgc.Text = "";

                if (!Cf.isTgl(tglbg))
                {
                    x = false;
                    if (s == "") s = tglbg.ID;
                    tglbgc.Text = "Tanggal";
                }
                else
                    tglbgc.Text = "";
            }

            if (manualbkm.Text != "")
            {
                string ManualBKM = Cf.Str(manualbkm.Text).PadLeft(6, '0');

                if (!Cf.isInt(manualbkm))
                {
                    x = false;
                    if (s == "") s = manualbkm.ID;
                    manualbkmc.Text = "Angka Bulat";
                }
                else
                {
                    int bkm = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_FINANCEAR..MS_TTS WHERE ManualBKM = '" + ManualBKM + "' AND NoTTS != " + NoTTS);
                    if (bkm > 0)
                    {
                        x = false;
                        if (s == "") s = manualbkm.ID;
                        manualbkmc.Text = "Duplikat";
                    }
                    else
                        manualbkmc.Text = "";
                }
            }
            else
                manualbkmc.Text = "";

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
                DateTime TglTTS = Convert.ToDateTime(tgltts.Text);
                string Ket = Cf.Str(ket.Text);
                string Unit = Cf.Str(unit.Text);
                string Customer = Cf.Str(customer.Text);
                decimal admbank = Convert.ToDecimal(admBank.Text);
                decimal lebihbayar = Convert.ToDecimal(lebihBayar.Text); //kurang bayar
                decimal lbayar = Convert.ToDecimal(lb.Text); //lebih bayar

                string ManualTTS = manualtts.Text;
                string ManualBKM = manualbkm.Text != "" ? Cf.Str(manualbkm.Text).PadLeft(6, '0') : "";

                string NoBG = "";
                DateTime TglBG = DateTime.Today;
                string Titip = "";
                if (carabayar.Text == "CEK GIRO")
                {
                    NoBG = Cf.Pk(nobg.Text);
                    TglBG = Convert.ToDateTime(tglbg.Text);
                    Titip = Cf.Str(titip.Text);
                }

                //khusus kartu kredit
                string NoKK = "", BankKK = "";
                if (carabayar.Text == "KARTU KREDIT")
                {
                    NoKK = Cf.Pk(nokk.Text);
                    BankKK = Cf.Pk(bankkk.Text);
                }

                DataTable rs = Db.Rs("SELECT "
                    + " NoTTS AS [No. TTS]"
                    + ",Tipe"
                    + ",Ref AS [Ref.]"
                    + ",CaraBayar AS [Cara Bayar]"
                    + ",Total AS [Nilai TTS]"
                    + " FROM ISC064_FINANCEAR..MS_TTS"
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
                    + ", NoFPS AS [No. Faktur Pajak]"
                    + " FROM ISC064_FINANCEAR..MS_TTS"
                    + " WHERE NoTTS = " + NoTTS
                    );

                Db.Execute("EXEC spTTSEdit"
                    + " '" + NoTTS + "'"
                    + ",'" + TglTTS + "'"
                    + ",'" + Unit + "'"
                    + ",'" + Customer + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoBG + "'"
                    + ",'" + TglBG + "'"
                    + ",'" + Titip + "'"
                    );

                //manual update
                Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET "
                    + " Pph = " + Cf.BoolToSql(pph.Checked)
                    + ",ManualTTS = '" + ManualTTS + "'"
                    + ",ManualBKM = '" + ManualBKM + "'"
                    + ", NoFPS = '" + Cf.Str(tbNoFaktur.Text) + "'"
                    + ",NoKK = '" + NoKK + "'"
                    + ",BankKK = '" + BankKK + "'"
                    + " WHERE NoTTS = " + NoTTS);

                if (delfp.Checked)
                {
                    Db.Execute("UPDATE ISC064_FINANCEAR..REF_FP SET Status = 0 WHERE NoFPS = '" + tbNoFaktur.Text + "'");
                    Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET NoFPS = '' WHERE NoTTS = " + NoTTS);
                }

                bool FOBO = Db.SingleBool("SELECT FOBO FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS='" + NoTTS + "'");
                if (FOBO != true)
                {
                    decimal Total = Db.SingleDecimal("SELECT Total FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS='" + NoTTS + "'");
                    decimal total2 = Total + lebihbayar + lbayar - admbank;
                    Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET "
                    + " AdminBank = '" + admbank + "'"
                    + " , Total2 ='" + total2 + "'"
                    + " , LebihBayar ='" + lebihbayar + "'"
                    + " , LB ='" + lbayar + "'"
                    + " WHERE NoTTS = " + NoTTS);
                }


                //if (manualbkm.Text != "")
                //{
                //    Db.Execute("UPDATE MS_TTS SET "
                //        + " NoBKM = " + manualbkm.Text
                //        + " WHERE NoTTS = " + NoTTS);
                //}
                //if(bkmtr.Visible)
                //{
                Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET "
                    + " TglBKM = '" + Convert.ToDateTime(tglbkm.Text) + "'"
                    + " WHERE NoTTS = " + NoTTS);
                //}

                Db.Execute("EXEC spSinkronisasi " + NoTTS);

                Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET Acc = '" + ddlAcc.SelectedValue + "' WHERE NoTTS = " + NoTTS);

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
                    + ", NoFPS AS [No. Faktur Pajak]"
                    + " FROM ISC064_FINANCEAR..MS_TTS"
                    + " WHERE NoTTS = " + NoTTS
                    );

                /*Update status Akunting*/
                int Akunting = Db.SingleInteger("SELECT Akunting FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

                if (Akunting == 1)
                {
                    string NoVoucher = Db.SingleString("SELECT NoVoucher FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

                    Akun.InsertAnomali("TTS", NoTTS, Cf.LogCapture(rsBef), Cf.LogCapture(rsAft), "EDIT TTS", "", NoVoucher);
                }
                /************************/

                //Logfile
                string ketlog = Cf.LogCapture(rs)
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC "+Mi.DbPrefix+"FINANCEAR..spLogTTS"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ketlog + "'"
                    + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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
            if (Save()) Response.Redirect("TTSEdit.aspx?done=1&NoTTS=" + NoTTS);
        }

        private string NoTTS
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoTTS"]);
            }
        }
        private void DendaIlang(string NoTTS)
        {
            string strsql = "SELECT NoTagihan,NoKontrak,TglPelunasan FROM  ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NOTTS='" + NoTTS + "' ";
            DataTable rs = Db.Rs(strsql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {


                DateTime tgljt = Db.SingleTime("SELECT TOP 1 tgljt FROM  ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NOURUT='" + rs.Rows[i]["NoTagihan"].ToString() + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "'");
               
                
                DateTime wew1 = Convert.ToDateTime(rs.Rows[i]["TglPelunasan"]);
                //double aaa = (wew1 - wew).TotalDays;
                //DateTime d1 = DateTime.Now;
                //DateTime d2 = DateTime.Now.AddDays(-1);

                TimeSpan t = wew1 - tgljt;
                double NrOfDays = t.TotalDays;
                if (NrOfDays <= 0)
                {
                    Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_TAGIHAN SET Denda = 0 WHERE NOURUT='" + rs.Rows[i]["NoTagihan"].ToString() + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "'");
                    //Response.Write("<script>alert('Hello" + NrOfDays + "');</script>");
                    Response.Redirect("TTSBkm.aspx?NoTTS=" + NoTTS + "&TglBKM=" + tglbkm.Text); 
                }
                else
                {
                    //Response.Write("<script>alert('Helloddd" + tgljt + "');</script>");
                    Response.Redirect("TTSBkm.aspx?NoTTS=" + NoTTS + "&TglBKM=" + tglbkm.Text); 

                }

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
