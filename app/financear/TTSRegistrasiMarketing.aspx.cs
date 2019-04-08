using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class TTSRegistrasiMarketing : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Table rpt;
        protected DataTable rsTagihan;
        protected DataTable rsTagihan2;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                nobg.Attributes["ondblclick"] = "popDaftarBG();";
                InitForm();

                Js.Focus(this, ket);

                nilaitr.Visible = false;
                Js.NumberFormat(nilai);
                Js.NumberFormat(admBank);
                Js.NumberFormat(lebihBayar);
                Js.NumberFormat(lb);
                Js.NumberFormat(biayaadmin);
                biayaadmin.Attributes["onblur"] += "javascript:hitungbiayaadmin();";
                lb.Attributes["onblur"] += "javascript:hitungbiayaadmin();";
                lebihBayar.Attributes["onblur"] += "javascript:hitungbiayaadmin();";
                admBank.Attributes["onblur"] += "javascript:hitungbiayaadmin();";

                gt.Attributes.Add("readonly", "readonly");
                grandtotal.Attributes.Add("readonly", "readonly");

                fillAcc();
            }

            ClientScript.RegisterOnSubmitStatement(
                GetType(),
                "hitungulang",
                "hitunggt();"
                );
            ClientScript.RegisterStartupScript(
                GetType(),
                "hitungulang2",
                "hitunggt();",
                true
                );

            if (detildiv.Visible)
                Js.Confirm(this, "Lanjutkan proses registrasi tanda terima sementara?");

            FillTb();
        }

        private void fillanonim()
        {
            anonim.Items.Clear();
            if (ddlAcc.SelectedIndex > 0)
            {
                string NoKontrak = referensi.Text;
                string Project = Db.SingleString("SELECT Project FROM ISC064_MARKETINGJUAL..MS_KONTRAK where NoKontrak ='" + NoKontrak + "'");

                anonim.Items.Add(new ListItem(""));
                DataTable rs = Db.Rs("SELECT * FROM MS_ANONIM WHERE Status <> 'S' AND Project = '" + Project + "' AND NoKontrak='" + NoKontrak + "'");

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string v = rs.Rows[i]["NoAnonim"].ToString();
                    string t = Cf.Day(rs.Rows[i]["Tgl"])
                        + " (" + rs.Rows[i]["Bank"] + ") "
                        + Cf.Num(rs.Rows[i]["Nilai"]);
                    anonim.Items.Add(new ListItem(t, v));
                }
            }
        }

        private void fillAcc()
        {
            string NoUnit = unit.Text;

            string Project = Db.SingleString("SELECT Project FROM ISC064_MARKETINGJUAL..MS_UNIT where NoUnit ='" + NoUnit + "'");

            DataTable rs = Db.Rs("SELECT * FROM REF_ACC where Project = '" + Project + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v2 = rs.Rows[i]["Acc"].ToString() + ";" + rs.Rows[i]["SubID"].ToString();
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];

                //string p2 = rs.Rows[i]["Acc"].ToString() + ";" + rs.Rows[i]["SubID"].ToString();
                //string p = rs.Rows[i]["Acc"].ToString();
                //string s = p + " : " + rs.Rows[i]["AtasNama"];

                ddlAcc.Items.Add(new ListItem(t, v2));
                //ddlpt.Items.Add(new ListItem(s,p2));
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
                + " AND KPR != '1' AND Tipe != 'ADM'"
                + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') ) > 0"
                + " ORDER BY TglJT, NoUrut";
            rsTagihan = Db.Rs(strSql);

            decimal Ano = Db.SingleDecimal("SELECT ISNULL(Nilai, 0) FROM MS_ANONIM WHERE NoAnonim = '" + anonim.SelectedValue + "'");
            decimal sisatotal = 0;
            decimal sisatotal2 = 0;

            int b = 0;
            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                b++;
                Label l;
                TextBox t;
                CheckBox cb;

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
                Js.NumberFormat(t);
                t.Attributes["onblur"] += "hitunggt();";
                list.Controls.Add(t);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                cb = new CheckBox();
                cb.Attributes.Add("onclick", "tagihan('" + i + "','" + Cf.Num(rsTagihan.Rows[i]["SisaTagihan"]) + "',this)");
                list.Controls.Add(cb);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                sisatotal += Convert.ToDecimal(rsTagihan.Rows[i]["SisaTagihan"]);
                //Response.Write(sisatotal + "<Br>");
            }

            string strSql2 = "SELECT * "
                + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') ) AS SisaTagihan"
                + " FROM " + Tb + "..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + Ref + "'"
                + " AND KPR != '1' AND Tipe = 'ADM'"
                + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') ) > 0"
                + " ORDER BY TglJT, NoUrut";
            rsTagihan2 = Db.Rs(strSql2);

            for (int a = 0; a < rsTagihan2.Rows.Count; a++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                TextBox t;
                CheckBox cb;

                l = new Label();
                l.Text = "<tr valign=top>"
                    + "<td>" + rsTagihan2.Rows[a]["NoKontrak"] + "." + rsTagihan2.Rows[a]["NoUrut"] + "</td>"
                    + "<td>" + rsTagihan2.Rows[a]["NamaTagihan"] + "</td>"
                    + "<td>" + rsTagihan2.Rows[a]["Tipe"] + "</td>"
                    + "<td style='white-space:nowrap'>" + Cf.Day(rsTagihan2.Rows[a]["TglJT"]) + "</td>"
                    + "<td align=right>" + Cf.Num(rsTagihan2.Rows[a]["SisaTagihan"]) + "</td>"
                    + "<td>"
                    ;
                list.Controls.Add(l);

                t = new TextBox();
                t.ID = "lunas_" + (a + b);
                t.Width = 100;
                t.CssClass = "txt_num";
                Js.NumberFormat(t);
                t.Attributes["onblur"] += "hitunggt();";
                list.Controls.Add(t);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                cb = new CheckBox();
                cb.Attributes.Add("onclick", "tagihan('" + (a + b) + "','" + Cf.Num(rsTagihan2.Rows[a]["SisaTagihan"]) + "',this)");
                list.Controls.Add(cb);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                sisatotal2 += Convert.ToDecimal(rsTagihan2.Rows[a]["SisaTagihan"]);
            }

            if (Ano > (sisatotal + sisatotal2))
            {
                gt.Text = Cf.Num(Ano);
                lb.Text = Cf.Num(Ano - (sisatotal + sisatotal2));
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

                if (carabayar.SelectedValue == "KK")
                {
                    if (Cf.isEmpty(nokk))
                    {
                        x = false;
                        if (s == "") s = nokk.ID;
                        nokkc.Text = "Kosong";
                    }
                    else
                    {
                        nokkc.Text = "";
                    }

                    if (Cf.isEmpty(bankkk))
                    {
                        x = false;
                        if (s == "") s = bankkk.ID;
                        bankkkc.Text = "Kosong";
                    }
                    else
                    {
                        bankkkc.Text = "";
                    }
                }

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

                    if (!Cf.isTgl(tgljtbg))
                    {
                        x = false;
                        if (s == "") s = tgljtbg.ID;
                        tgljtbgc.Text = "Tanggal";
                    }
                    else
                        tgljtbgc.Text = "";
                }

                if (carabayar.SelectedValue == "KK")
                {
                    if (!Cf.isMoney(biayaadmin))
                    {
                        x = false;
                        if (s == "") s = biayaadmin.ID;
                        Cf.MarkError(biayaadmin);
                    }
                    else
                        Cf.ClrError(biayaadmin);
                }

                bool adasatu = false;
                int b = 0;
                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    b++;
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

                for (int a = 0; a < rsTagihan2.Rows.Count; a++)
                {
                    TextBox lunas = (TextBox)list.FindControl("lunas_" + (a + b));
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

                if (anonim.SelectedIndex > 0)
                {
                    decimal Ano = Db.SingleDecimal("SELECT ISNULL(Nilai, 0) FROM MS_ANONIM WHERE NoAnonim = " + anonim.SelectedValue);

                    if (Convert.ToDecimal(grandtotal.Text) != Ano)
                    {
                        x = false;
                    }
                }

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
                        + "6. Apabila menggunakan anonim. Maka nilai total pembayaran harus sesuai dengan nilai anonim.\\n"
                        , "document.getElementById('" + s + "').focus();"
                        );

                //Js.Alert(this, "", gt.Text);

                return x;
            }
        }

        protected bool ValidNilai()
        {
            bool x = true;

            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                TextBox lunas = (TextBox)list.FindControl("lunas_" + i);

                if (lunas.Text != "" && anonim.SelectedIndex == 0)
                {
                    decimal NilaiTagihan = Convert.ToDecimal(rsTagihan.Rows[i]["SisaTagihan"]);
                    decimal NilaiBayar = Convert.ToDecimal(lunas.Text);

                    if (NilaiBayar > NilaiTagihan)
                    {
                        x = false;
                        lunas.ForeColor = Color.Red;
                    }
                    else
                    {
                        lunas.ForeColor = Color.Black;
                    }
                }
            }

            for (int j = 0; j < rsTagihan2.Rows.Count; j++)
            {
                TextBox lunas2 = (TextBox)list.FindControl("lunas_" + (j + rsTagihan.Rows.Count));
                if (lunas2.Text != "" && anonim.SelectedIndex == 0)
                {
                    decimal NilaiTagihan = Convert.ToDecimal(rsTagihan2.Rows[j]["SisaTagihan"]);
                    decimal NilaiBayar = Convert.ToDecimal(lunas2.Text);

                    if (NilaiBayar > NilaiTagihan)
                    {
                        x = false;
                        lunas2.ForeColor = Color.Red;
                    }
                    else
                    {
                        lunas2.ForeColor = Color.Black;
                    }
                }
            }

            if (!x)
            {
                Js.Alert(this, "Nilai Pembayaran Melebihi Tagihan!", "");
            }

            return x;
        }
        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid() && ValidNilai())
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

                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Ref + "'");
                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                
                //Numerator
                string NoTTS2 = Numerator.TTS(TglTTS.Month, TglTTS.Year, Project);

                Db.Execute("EXEC spTTSRegistrasi"
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
                string[] x = Cf.SplitByString(ddlAcc.SelectedValue, ";");
                // string[] y = Cf.SplitByString(ddlpt.SelectedValue, ";");

                int noTTS = Db.SingleInteger("SELECT TOP 1 NoTTS FROM MS_TTS ORDER BY NoTTS DESC");
                Db.Execute("UPDATE MS_TTS SET NoTTS2 = '" + NoTTS2 + "',Project = '" + Project + "',NamaProject = '" + NamaProject + "'  WHERE NoTTS ='" + noTTS + "'");
                Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_PELUNASAN SET NoTTS2='" + NoTTS2 + "' WHERE NoTTS ='" + noTTS + "'");
                decimal total2 = (Convert.ToDecimal(gt.Text) + LebihBayar) - AdminBank;
                Db.Execute("UPDATE MS_TTS"
                    + " SET Acc = '" + x[0] + "'"
                    + ", SubID='" + x[1] + "'"
                    + ", AdminBank='" + AdminBank + "' "
                    //      + ", Total2 = '" + total2 + "'"
                    + ", LebihBayar = '" + LebihBayar + "'" //kurang bayar
                    + ", LB = '" + LB + "'" //lebih bayar
                    + ", SumberBayar = " + sumberdana.SelectedValue
                    //+ ", NoPT='" + y[0] + "'"
                    + " WHERE NoTTS = " + noTTS);

                if (anonim.SelectedValue != "")
                {
                    Db.Execute("UPDATE MS_TTS SET NoAnonim = '" + anonim.SelectedValue + "' WHERE NoTTS = '" + noTTS + "'  ");
                }

                //khusus cek giro
                if (carabayar.SelectedValue == "BG")
                {
                    string NoBG = Cf.Pk(nobg.Text);
                    DateTime TglBG = Convert.ToDateTime(tglbg.Text);

                    Db.Execute("EXEC spTTSRegistrasiBG"
                        + " '" + noTTS + "'"
                        + ",'" + NoBG + "'"
                        + ",'" + TglBG + "'"
                        );

                    Db.Execute("UPDATE MS_TTS SET "
                        + " BankBG= '" + Cf.Str(bankbg.Text) + "'"
                        + ",TglJTBG = '" + Convert.ToDateTime(tgljtbg.Text) + "'"
                        + " WHERE NoTTS = '" + noTTS + "'"
                        );
                }

                if (anonim.SelectedIndex > 0)
                {
                    Db.Execute("UPDATE MS_ANONIM SET Status = 'S' WHERE NoAnonim = "
                        + anonim.SelectedValue);
                }

                //khusus kartu kredit
                if (carabayar.SelectedValue == "KK")
                {
                    string NoKK = Cf.Pk(nokk.Text);
                    string BankKK = Cf.Pk(bankkk.Text);
                    decimal BiayaAdmin = Convert.ToDecimal(biayaadmin.Text);
                    int BebanBiayaAdmin = Convert.ToInt16(bebanbiayaadmin.SelectedValue);

                    Db.Execute("UPDATE MS_TTS SET "
                        + " NoKK = '" + NoKK + "'"
                        + ",BankKK = '" + BankKK + "'"
                        + ",BebanBiayaAdmin = " + BebanBiayaAdmin
                        + " WHERE NoTTS = '" + noTTS + "'"
                        );
                }

                if (LebihBayar > 0)
                {
                    Db.Execute("EXEC spMEMORegistrasi"
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
                if (Db.SingleInteger("SELECT COUNT(*) FROM MS_MEMO") > 0)
                    NoMEMO = Db.SingleInteger("SELECT TOP 1 NoMEMO FROM MS_MEMO ORDER BY NoMEMO DESC");

                Db.Execute("UPDATE MS_MEMO SET Project = '" + Project + "',NamaProject = '" + NamaProject + "' WHERE NoMemo = " + NoMEMO);
                System.Text.StringBuilder alokasiM = new System.Text.StringBuilder();

                System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

                int b = 0;
                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    b++;
                    TextBox lunas = (TextBox)list.FindControl("lunas_" + i);
                    if (lunas.Text != "")
                    {
                        int NoTagihan = (int)rsTagihan.Rows[i]["NoUrut"];
                        string NamaTagihan = Cf.Str(rsTagihan.Rows[i]["NamaTagihan"])
                            + " (" + rsTagihan.Rows[i]["Tipe"] + ")";
                        decimal Nilai = Convert.ToDecimal(lunas.Text);

                        Db.Execute("EXEC spTTSAlokasi "
                            + "  " + noTTS
                            + ", " + NoTagihan
                            + ", " + Nilai
                            );

                        alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");
                        //Benefit
                        decimal benefit = Convert.ToDecimal(rsTagihan.Rows[i]["Benefit"]);
                        DateTime TglJT = Convert.ToDateTime(rsTagihan.Rows[i]["TglJT"]);
                        int beda = TglJT.Subtract(TglTTS).Days;
                        if (beda > 0)
                        {
                            benefit += (decimal)0.001 * Nilai * beda;

                            Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN SET Benefit = " + benefit
                                + " WHERE NoKontrak = '" + rsTagihan.Rows[i]["NoKontrak"] + "'"
                                + " AND NoUrut = " + NoTagihan
                                );
                        }

                        if (LebihBayar > 0)
                        {
                            decimal NilaiTagihan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM " + Tb + "..MS_TAGIHAN WHERE NoUrut = " + NoTagihan + " AND NoKontrak = '" + Ref + "'");
                            decimal Pelunasan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = " + NoTagihan + " AND NoKontrak = '" + Ref + "'");
                            decimal SisaTag = NilaiTagihan - Pelunasan;
                            decimal n = 0;

                            if (SisaTag > 0)
                            {
                                n = SisaTag < LebihBayar ? SisaTag : LebihBayar;

                                Db.Execute("EXEC spMEMOAlokasi "
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
                                Db.Execute("UPDATE MS_MEMO SET Status='POST' WHERE NoMemo='" + NoMEMO + "'");

                                alokasiM.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");

                                LebihBayar -= n;
                            }
                        }
                    }
                }

                for (int a = 0; a < rsTagihan2.Rows.Count; a++)
                {
                    TextBox lunas = (TextBox)list.FindControl("lunas_" + (a + b));
                    if (lunas.Text != "")
                    {
                        int NoTagihan = (int)rsTagihan2.Rows[a]["NoUrut"];
                        string NamaTagihan = Cf.Str(rsTagihan2.Rows[a]["NamaTagihan"])
                            + " (" + rsTagihan2.Rows[a]["Tipe"] + ")";
                        decimal Nilai = Convert.ToDecimal(lunas.Text);

                        Db.Execute("EXEC spTTSAlokasi "
                            + "  " + noTTS
                            + ", " + NoTagihan
                            + ", " + Nilai
                            );

                        alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");
                        //Benefit
                        decimal benefit = Convert.ToDecimal(rsTagihan2.Rows[a]["Benefit"]);
                        DateTime TglJT = Convert.ToDateTime(rsTagihan2.Rows[a]["TglJT"]);
                        int beda = TglJT.Subtract(TglTTS).Days;
                        if (beda > 0)
                        {
                            benefit += (decimal)0.001 * Nilai * beda;

                            Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN SET Benefit = " + benefit
                                + " WHERE NoKontrak = '" + rsTagihan2.Rows[a]["NoKontrak"] + "'"
                                + " AND NoUrut = " + NoTagihan
                                );
                        }

                        if (LebihBayar > 0)
                        {
                            decimal NilaiTagihan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM " + Tb + "..MS_TAGIHAN WHERE NoUrut = " + NoTagihan + " AND NoKontrak = '" + Ref + "'");
                            decimal Pelunasan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = " + NoTagihan + " AND NoKontrak = '" + Ref + "'");
                            decimal SisaTag = NilaiTagihan - Pelunasan;
                            decimal n = 0;

                            if (SisaTag > 0)
                            {
                                n = SisaTag < LebihBayar ? SisaTag : LebihBayar;

                                Db.Execute("EXEC spMEMOAlokasi "
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
                                Db.Execute("UPDATE MS_MEMO SET Status='POST' WHERE NoMemo='" + NoMEMO + "'");

                                alokasiM.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");

                                LebihBayar -= n;
                            }
                        }
                    }
                }
                decimal TotalSatu = Db.SingleDecimal("SELECT Total FROM MS_TTS WHERE NoTTS = '" + noTTS + "' ");
                decimal TotalDua = (TotalSatu + LebihBayar + LB) - AdminBank;
                Db.Execute("UPDATE MS_TTS SET Total2 = '" + TotalDua + "' WHERE NoTTS = '" + noTTS + "' ");

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
                    + ",CONVERT(varchar, TglJTBG, 106) AS [Tanggal Jatuh Tempo BG]"
                    + ", Acc AS [Rekening Bank]"
                    + " FROM MS_TTS WHERE NoTTS = " + noTTS);

                string KetLog = Cf.LogCapture(rs)
                    + "<br>***ALOKASI PEMBAYARAN:<br>"
                    + alokasi.ToString();

                Db.Execute("EXEC spLogTTS"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + noTTS.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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
                        + " FROM MS_MEMO WHERE NoMEMO = " + NoMEMO);

                    string KetLogM = Cf.LogCapture(rsM)
                        + "<br>***ALOKASI PEMBAYARAN:<br>"
                        + alokasiM.ToString();

                    Db.Execute("EXEC spLogMEMO"
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

                Response.Redirect("TTSRegistrasi.aspx?done=" + noTTS);
            }
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
                    if (rsTagihan2.Rows.Count == 0)
                    {
                        //last row
                        lunas.Text = Cf.Num(SisaTagihan);

                    }
                    else
                    {
                        lunas.Text = Cf.Num(SisaTagihan);
                    }
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
            for (int j = 0; j < rsTagihan2.Rows.Count; j++)
            {
                TextBox lunas2 = (TextBox)list.FindControl("lunas_" + (j + rsTagihan.Rows.Count));
                decimal SisaTagihan2 = (decimal)rsTagihan2.Rows[j]["SisaTagihan"];

                if (j == rsTagihan2.Rows.Count - 1)
                {
                    lunas2.Text = Cf.Num(SisaTagihan2);
                }
                else
                {
                    if (SisaTagihan2 >= x)
                    {
                        //break, soalnya total udah abis
                        lunas2.Text = Cf.Num(x);
                        break;
                    }
                    else
                    {
                        lunas2.Text = Cf.Num(SisaTagihan2);
                    }
                    x = x - SisaTagihan2;
                }
            }
            //Response.Write(Ano);
            //gt.Text = Cf.Num(Ano);            
        }

        private bool isUnique(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_TTS WHERE NoTTS2 = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
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

        protected void carabayar_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool visiblekredit = false, visiblegiro = false;

            if (carabayar.SelectedValue == "KK")
                visiblekredit = true;
            else if (carabayar.SelectedValue == "BG")
                visiblegiro = true;

            trkredit1.Visible = trkredit2.Visible = trkredit3.Visible = trkredit4.Visible = trkredit5.Visible = visiblekredit;
            trgiro1.Visible = trgiro2.Visible = trgiro3.Visible = trgiro4.Visible = trgiro5.Visible = visiblegiro;
        }

        protected void ddlAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillanonim();
        }

        protected void anonim_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal Ano = 0;

            if (anonim.SelectedIndex > 0)
            {
                Ano = Db.SingleDecimal("SELECT ISNULL(Nilai, 0) FROM MS_ANONIM WHERE NoAnonim = " + anonim.SelectedValue);

                Alokasi(Ano);
            }
            else
            {
                Alokasi(Ano);
            }
        }
    }
}
