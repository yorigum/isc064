using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class TTSRegistrasiMarketing : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Table rpt;
        protected DataTable rsTagihan;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                nobg.Attributes["ondblclick"] = "popDaftarBG();";
                InitForm();

                Js.Focus(this, ket);

                detildiv.Visible = false;
                nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                nilai.Attributes["onblur"] = "CalcBlur(this);";

                admBank.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                admBank.Attributes["onkeyup"] = "CalcType(this,tempnum); hitungtotal();";
                admBank.Attributes["onblur"] = "CalcBlur(this); CalcBlur(hitungtotal);";

                //kurang bayar
                lebihBayar.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                lebihBayar.Attributes["onkeyup"] = "CalcType(this,tempnum); hitungtotal();";
                lebihBayar.Attributes["onblur"] = "CalcBlur(this); CalcBlur(hitungtotal);";

                //lebih bayar
                lb.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                lb.Attributes["onkeyup"] = "CalcType(this,tempnum); hitungtotal();";
                lb.Attributes["onblur"] = "CalcBlur(this); CalcBlur(hitungtotal);";

                fillanonim();
                fillAcc();
            }

            if (detildiv.Visible)
                Js.Confirm(this, "Lanjutkan proses registrasi tanda terima sementara?");

            FillTb();
        }

        private void fillanonim()
        {
            anonim.Items.Clear();
            DataTable rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..MS_ANONIM WHERE Status <> 'S'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAnonim"].ToString();
                string t = Cf.Day(rs.Rows[i]["Tgl"])
                    + " (" + rs.Rows[i]["Bank"] + ") "
                    + Cf.Num(rs.Rows[i]["Nilai"]);
                //+ " Keterangan : "
                //+ rs.Rows[i]["Unit"] + " "
                //+ rs.Rows[i]["Customer"] + " "
                //+ rs.Rows[i]["Ket"];
                anonim.Items.Add(new ListItem(t, v));
            }
        }

        private void fillAcc()
        {
            DataTable rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_ACC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                ddlAcc.Items.Add(new ListItem(t, v));
            }
        }

        private void InitForm()
        {
            if (Act.Sec("DiskonTTS"))
            {
                //	carabayar.Items.Add(new ListItem("DN = Diskon","DN"));
            }

            gt.Attributes["style"] = "border:0px;font:bold;";
            tgl.Text = Cf.Day(DateTime.Today);

            tipe.Text = Tipe;
            referensi.Text = Ref;

            unit.Text = Db.SingleString("SELECT NoUnit "
                + " FROM " + Tb + "..MS_KONTRAK "
                + " WHERE NoKontrak = '" + Ref + "'");

            customer.Text = Db.SingleString("SELECT Nama "
                + " FROM " + Tb + "..MS_KONTRAK AS MS_KONTRAK "
                + " INNER JOIN " + Tb + "..MS_CUSTOMER AS MS_CUSTOMER "
                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE NoKontrak = '" + Ref + "'");
        }

        private void FillTb()
        {
            string strSql = "SELECT * "
                + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') ) AS SisaTagihan"
                + " FROM " + Tb + "..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + Ref + "'"
                + " AND KPR != '1' "
                + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') ) > 0"
                + " ORDER BY NoUrut, TglJT";
            rsTagihan = Db.Rs(strSql);

            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                TextBox t;

                l = new Label();
                l.Text = "<tr valign=top>"
                    + "<td>" + rsTagihan.Rows[i]["NoKontrak"] + "." + rsTagihan.Rows[i]["NoUrut"] + "</td>"
                    + "<td>" + rsTagihan.Rows[i]["NamaTagihan"] + "</td>"
                    + "<td>" + rsTagihan.Rows[i]["Tipe"] + "</td>"
                    + "<td style='white-space:nowrap'>" + Cf.Day(rsTagihan.Rows[i]["TglJT"]) + "</td>"
                    + "<td align=right>" + Cf.Num(rsTagihan.Rows[i]["SisaTagihan"]) + "</td>"
                    + "<td>"
                    ;
                list.Controls.Add(l);

                t = new TextBox();
                t.ID = "lunas_" + i;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);hitunggt();";
                list.Controls.Add(t);

                l = new Label();
                l.Text = "</td>"
                    + "<td><input type='checkbox' onclick=\"tagihan('" + i + "','" + Cf.Num(rsTagihan.Rows[i]["SisaTagihan"]) + "',this)\"></td>"
                    + "</tr>";
                list.Controls.Add(l);
            }
        }

        private bool datavalid()
        {

            if (carabayar.SelectedIndex == -1)
            {
                Js.Alert(
                    this
                    , "Cara Bayar Tidak Valid.\\n"
                    + "Silakan pilih salah satu cara bayar yang tersedia."
                    , ""
                    );

                return false;
            }
            else
            {
                string s = "";
                bool x = true;

                if (!Cf.isTgl(tgl))
                {
                    x = false;
                    if (s == "") s = tgl.ID;
                    tglc.Text = "Tanggal";
                }
                else
                    tglc.Text = "";

                if (ddlAcc.SelectedIndex == 0)
                {
                    x = false;

                    if (s == "")
                        s = ddlAcc.ID;

                    ddlAccErr.Text = "Harus dipilih";
                }
                else
                    ddlAccErr.Text = "";

                if (carabayar.SelectedValue == "BG")
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

                bool adasatu = false;
                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    TextBox lunas = (TextBox)list.FindControl("lunas_" + i);
                    if (lunas.Text != "")
                    {
                        adasatu = true;
                        try
                        {
                            decimal z = Convert.ToDecimal(lunas.Text);
                        }
                        catch
                        {
                            x = false;
                            if (s == "") s = lunas.ID;
                        }
                    }
                }

                if (!adasatu)
                {
                    x = false;
                    if (s == "") s = gt.ID;
                    gtc.Attributes["style"] = "color:red";
                }
                else
                    gtc.Attributes["style"] = "color:black";

                if (!Cf.isMoney(admBank))
                {
                    x = false;
                    if (s == "") s = admBank.ID;
                    admBankc.Text = "Angka";
                }
                else
                    admBankc.Text = "";


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

                if (!x)
                    Js.Alert(
                        this
                        , "Input Tidak Valid.\\n\\n"
                        + "Aturan Proses :\\n"
                        + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                        + "2. Pembayaran harus berupa angka dan minimal untuk satu tagihan.\\n"
                        + "3. Khusus Cek Giro : No. BG tidak boleh kosong.\\n"
                        + "4. Rekening Bank harus dipilih.\\n"
                        + "5. Kolom Admin Bank, Pembulatan dan Lebih Bayar harus angka.\\n"
                        , "document.getElementById('" + s + "').focus();"
                        );

                return x;
            }
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                DateTime TglTTS = Convert.ToDateTime(tgl.Text);
                string Unit = Cf.Str(unit.Text);
                string Customer = Cf.Str(customer.Text);
                string CaraBayar = carabayar.SelectedValue;
                string Ket = Cf.Str(ket.Text);
                decimal AdminBank = Convert.ToDecimal(admBank.Text);
                decimal LebihBayar = Convert.ToDecimal(lebihBayar.Text); //kurang bayar
                decimal LB = Convert.ToDecimal(lb.Text); //lebih bayar
                decimal NilaiMemo = LebihBayar;

                Db.Execute("EXEC ISC064_FINANCEAR..spTTSRegistrasi"
                    + " '" + TglTTS + "'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Tipe + "'"
                    + ",'" + Ref + "'"
                    + ",'" + Unit + "'"
                    + ",'" + Customer + "'"
                    + ",'" + CaraBayar + "'"
                    + ",'" + Ket + "'"
                    );


                int noTTS = Db.SingleInteger("SELECT TOP 1 NoTTS FROM ISC064_FINANCEAR..MS_TTS ORDER BY NoTTS DESC");
                decimal total2 = (Convert.ToDecimal(gt.Text) + LebihBayar) - AdminBank;
                Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS"
                    + " SET Acc = '" + ddlAcc.SelectedValue + "'"
                    + ", AdminBank='" + AdminBank + "' "
                    //      + ", Total2 = '" + total2 + "'"
                    + ", LebihBayar = '" + LebihBayar + "'" //kurang bayar
                    + ", LB = '" + LB + "'" //lebih bayar
                    + ", SumberBayar = " + sumberdana.SelectedValue
                    + " WHERE NoTTS = " + noTTS);

                if (anonim.SelectedValue != "")
                {
                    Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET NoAnonim = '" + anonim.SelectedValue + "' WHERE NoTTS = '" + noTTS + "'  ");
                }

                // add by viana 23/03/09
                //				string sSQL = "UPDATE MS_TTS SET a.TipePosting = 1 FROM MS_TTS a, MS_KONTRAK b"
                //						+ " WHERE a.Ref = b.NoKontrak AND b.Akunting2 = 1 AND NoTTS = " + NoTTS + "";
                //				Db.Execute(sSQL);

                //khusus cek giro
                if (carabayar.SelectedValue == "BG")
                {
                    string NoBG = Cf.Pk(nobg.Text);
                    DateTime TglBG = Convert.ToDateTime(tglbg.Text);

                    Db.Execute("EXEC ISC064_FINANCEAR..spTTSRegistrasiBG"
                        + " '" + noTTS + "'"
                        + ",'" + NoBG + "'"
                        + ",'" + TglBG + "'"
                        );
                }

                if (anonim.SelectedIndex > 0)
                {
                    Db.Execute("UPDATE ISC064_FINANCEAR..MS_ANONIM SET Status = 'S' WHERE NoAnonim = "
                        + anonim.SelectedValue);
                }

                //khusus kartu kredit
                if (carabayar.SelectedValue == "KK")
                {
                    string NoKK = Cf.Pk(nokk.Text);
                    string BankKK = Cf.Pk(bankkk.Text);

                    Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET "
                        + " NoKK = '" + NoKK + "'"
                        + ",BankKK = '" + BankKK + "'"
                        + " WHERE NoTTS = '" + noTTS + "'"
                        );
                }

                if (LebihBayar > 0)
                {
                    Db.Execute("EXEC ISC064_FINANCEAR..spMEMORegistrasi"
                        + " '" + TglTTS + "'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Tipe + "'"
                        + ",'" + Ref + "'"
                        + ",'" + Unit + "'"
                        + ",'" + Customer + "'"
                        + ",'PP'"
                        + ",''"
                        + "," + noTTS
                        );

                }
                int NoMEMO = 0;
                if (Db.SingleInteger("SELECT COUNT(*) FROM ISC064_FINANCEAR..MS_MEMO") > 0)
                    NoMEMO = Db.SingleInteger("SELECT TOP 1 NoMEMO FROM ISC064_FINANCEAR..MS_MEMO ORDER BY NoMEMO DESC");

                System.Text.StringBuilder alokasiM = new System.Text.StringBuilder();

                System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    TextBox lunas = (TextBox)list.FindControl("lunas_" + i);
                    if (lunas.Text != "")
                    {
                        int NoTagihan = (int)rsTagihan.Rows[i]["NoUrut"];
                        string NamaTagihan = Cf.Str(rsTagihan.Rows[i]["NamaTagihan"])
                            + " (" + rsTagihan.Rows[i]["Tipe"] + ")";
                        decimal Nilai = Convert.ToDecimal(lunas.Text);

                        Db.Execute("EXEC ISC064_FINANCEAR..spTTSAlokasi "
                            + "  " + noTTS
                            + ", " + NoTagihan
                            + ", " + Nilai
                            );

                        alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");

                        if (LebihBayar > 0)
                        {
                            decimal NilaiTagihan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM " + Tb + "..MS_TAGIHAN WHERE NoUrut = " + NoTagihan + " AND NoKontrak = '" + Ref + "'");
                            decimal Pelunasan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = " + NoTagihan + " AND NoKontrak = '" + Ref + "'");
                            decimal SisaTag = NilaiTagihan - Pelunasan;
                            decimal n = 0;

                            if (SisaTag > 0)
                            {
                                n = SisaTag < LebihBayar ? SisaTag : LebihBayar;

                                Db.Execute("EXEC ISC064_FINANCEAR..spMEMOAlokasi "
                                    + "  " + NoMEMO
                                    + ", " + NoTagihan
                                    + ", " + n
                                    );

                                Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_PELUNASAN"
                                     + " SET"
                                     + " TglPelunasan ='" + TglTTS + "'"
                                     + ", SudahCair='1'"
                                     + " WHERE NoKontrak='" + referensi.Text + "' AND NoMemo='" + NoMEMO + "' AND NoTagihan='" + NoTagihan + "'"
                                    );
                                Db.Execute("UPDATE ISC064_FINANCEAR..MS_MEMO SET Status='POST' WHERE NoMemo='" + NoMEMO + "'");

                                alokasiM.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");

                                LebihBayar -= n;
                            }
                        }
                    }
                }

                decimal TotalSatu = Db.SingleDecimal("SELECT Total FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = '" + noTTS + "' ");
                decimal TotalDua = (TotalSatu + LebihBayar + LB) - AdminBank;
                Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET Total2 = '" + TotalDua + "' WHERE NoTTS = '" + noTTS + "' ");

                DataTable rs = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglTTS, 106) AS [Tanggal]"
                    + ",Tipe"
                    + ",Ref AS [Ref.]"
                    + ",Unit"
                    + ",Customer"
                    + ",CaraBayar AS [Cara Bayar]"
                    + ",Ket AS [Keterangan]"
                    + ",Total"
                    + ",NoBG AS [No. BG]"
                    + ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
                    + ", Acc AS [Rekening Bank]"
                    + " FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + noTTS);

                string KetLog = Cf.LogCapture(rs)
                    + "<br>***ALOKASI PEMBAYARAN:<br>"
                    + alokasi.ToString();

                Db.Execute("EXEC ISC064_FINANCEAR..spLogTTS"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + noTTS.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + noTTS + "')");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                if (NilaiMemo > 0)
                {
                    DataTable rsM = Db.Rs("SELECT "
                        + " CONVERT(varchar, TglMEMO, 106) AS [Tanggal]"
                        + ",Tipe"
                        + ",Ref AS [Ref.]"
                        + ",Unit"
                        + ",Customer"
                        + ",CaraBayar AS [Cara Bayar]"
                        + ",Ket AS [Keterangan]"
                        + ",Total"
                        + ",NoBG AS [No. BG]"
                        + ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
                        + ", Acc AS [Rekening Bank]"
                        + " FROM ISC064_FINANCEAR..MS_MEMO WHERE NoMEMO = " + NoMEMO);

                    string KetLogM = Cf.LogCapture(rsM)
                        + "<br>***ALOKASI PEMBAYARAN:<br>"
                        + alokasiM.ToString();

                    Db.Execute("EXEC ISC064_FINANCEAR..spLogMEMO"
                        + " 'REGIS'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLogM + "'"
                        + ",'" + NoMEMO.ToString().PadLeft(7, '0') + "'"
                        );

                    decimal LogID2 = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_MEMO_LOG ORDER BY LogID DESC");
                    string Project2 = Db.SingleString("SELECT Project FROM MS_MEMO WHERE NoMEMO = '" + NoMEMO + "'");
                    Db.Execute("UPDATE MS_MEMO_LOG SET Project = '" + Project2 + "' WHERE LogID  = " + LogID2);

                    Db.Execute("EXEC ISC064_MARKETINGJUAL..spProsentasePelunasan '" + referensi.Text + "'");
                    Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_KONTRAK SET FlagMemo=1 WHERE NoKontrak='" + referensi.Text + "'");


                }
                string NoTTS2 = AutoID;
                Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET NoTTS2='" + NoTTS2 + "' WHERE NoTTS=" + noTTS);
                Response.Redirect("TTSRegistrasi.aspx?done=" + noTTS);
            }
        }
        private string AutoID
        {
            get
            {
                DateTime TglTTS = Convert.ToDateTime(tgl.Text);
                int c = Db.SingleInteger("SELECT COUNT(NoTTS2) FROM ISC064_FINANCEAR..MS_TTS WHERE MONTH(TglTTS)=" + TglTTS.Month + " AND YEAR(TglTTS) = " + TglTTS.Year);

                string nobkm = "";
                bool hasfound = false;
                while (!hasfound)
                {
                    if (!Response.IsClientConnected) break;

                    c += 1;
                    //nopjt = c.ToString() + "/" + u + "/" + Convert.ToDateTime(tgl.Text).Year;
                    nobkm = c.ToString().PadLeft(4, '0') + "/TTS-GEM/AKRS/" + Cf.Roman(TglTTS.Month) + "/" + TglTTS.Year;//"GEM-TTS/" + TglTTS.Year + "/" + c.ToString().PadLeft(4, '0');

                    if (isUnique(nobkm)) hasfound = true;
                }

                return nobkm;
            }
        }

        private bool isUnique(string nobkm)
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(NoTTS2) FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS2 = '" + nobkm + "'");

            if (c == 0)
                return true;
            else
                return false;
        }
        protected void next_Click(object sender, System.EventArgs e)
        {
            if (Cf.isMoney(nilai))
            {
                detildiv.Visible = true;
                nilaitr.Visible = false;

                Alokasi(Convert.ToDecimal(nilai.Text));

                Js.Confirm(this, "Lanjutkan proses registrasi tanda terima sementara?");
            }
            else
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Nilai harus berupa angka.\\n"
                    , "document.getElementById('" + nilai.ID + "').focus();"
                    + "document.getElementById('" + nilai.ID + "').select();"
                    );
        }
        private void Alokasi(decimal total)
        {
            decimal x = total;

            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                TextBox lunas = (TextBox)list.FindControl("lunas_" + i);
                decimal SisaTagihan = (decimal)rsTagihan.Rows[i]["SisaTagihan"];

                if (i == rsTagihan.Rows.Count - 1)
                {
                    //last row
                    lunas.Text = Cf.Num(x);
                }
                else
                {
                    if (SisaTagihan >= x)
                    {
                        //break, soalnya total udah abis
                        lunas.Text = Cf.Num(x);
                        break;
                    }
                    else
                    {
                        lunas.Text = Cf.Num(SisaTagihan);
                    }
                }
                x = x - SisaTagihan;
            }

            gt.Text = Cf.Num(total);
        }

        protected void rekening_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (carabayar.SelectedValue == "TR")
            {
                anonim.Items.Clear();
                DataTable rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..MS_ANONIM WHERE Status <> 'S' AND Bank='" + ddlAcc.SelectedValue + "'");
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string v = rs.Rows[i]["NoAnonim"].ToString();
                    string t = Cf.Day(rs.Rows[i]["Tgl"])
                        + " (" + rs.Rows[i]["Bank"] + ") "
                        + Cf.Num(rs.Rows[i]["Nilai"]);
                    //+ " Keterangan : "
                    //+ rs.Rows[i]["Unit"] + " "
                    //+ rs.Rows[i]["Customer"] + " "
                    //+ rs.Rows[i]["Ket"];
                    anonim.Items.Add(new ListItem(t, v));
                }
            }
            else
            {
                anonim.Items.Clear();
            }
        }

        private string Tb
        {
            get
            {
                return Sc.MktTb(Tipe);
            }
        }

        private string Ref
        {
            get
            {
                return Cf.Pk(Request.QueryString["Ref"]);
            }
        }

        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
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
