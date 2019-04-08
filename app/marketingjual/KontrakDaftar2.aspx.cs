using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakDaftar2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                nolanjut.Visible = false;

                InitForm();
                Fill();
                FillWL();

                frm.Visible = false;
                divPersenBertingkat.Visible = false;
                divLumpSum.Visible = true;
                persentingkat.Visible = true; trppn.Visible = true;
            }

        }

        private void InitForm()
        {
            string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

            tglkontrak.Text = Cf.Day(DateTime.Today);

            DataTable rs;

            //Cara bayar
            rs = Db.Rs("SELECT Nomor, Nama FROM REF_SKEMA WHERE Status = 'A' AND Project = '" + Project + "' ORDER BY Nama");
            carabayar.Items.Add(new ListItem("*** CUSTOMIZE / PENDING", "0")); //cara bayar customize

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Nomor"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                carabayar.Items.Add(new ListItem(t, v));
            }
            carabayar.SelectedIndex = 0;
            carabayar.Attributes["ondblclick"] = "kalk(this)";

            persentingkat.Visible = false;
            noreservasi.Visible = false;

            diskon.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            diskon.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            diskon.Attributes["onblur"] = "CalcBlur(this);";

            DataTable rs2 = Db.Rs("SELECT * FROM REF_LOKASI WHERE Project = '" + Project + "' ORDER BY SN");
            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                lokasi.Items.Add(new ListItem(rs2.Rows[i]["Nama"].ToString(), rs2.Rows[i]["Lokasi"].ToString()));
            }

            int defPL = Db.SingleInteger("SELECT DefaultPL FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            if (defPL > 0)
            {
                pldef.SelectedValue = defPL.ToString();
                pldefault.Visible = false;
            }
            else
            {
                pldef.SelectedValue = "0";
            }

            fo.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            fo.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            fo.Attributes["onblur"] = "CalcBlur(this);";

            focounter.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            focounter.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            focounter.Attributes["onblur"] = "CalcBlur(this);";

            pl.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            pl.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            pl.Attributes["onblur"] = "CalcBlur(this);";
            
            diskonLumpSum.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            diskonLumpSum.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            diskonLumpSum.Attributes["onblur"] = "CalcBlur(this);";
        }

        private void Fill()
        {
            cancel.Attributes["onclick"] = "location.href='KontrakDaftar2.aspx?NoStock=" + NoStock + "'";

            string strSql = "SELECT NoUnit, PriceList, Luas, LuasNett, LuasSG, PricelistKavling,DefaultPL,Project,Lokasi "
                + " FROM MS_UNIT WHERE NoStock = '" + NoStock + "'"
                + " AND Status = 'A' AND FlagSPL != 0"; //cek kondisi
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
            {
                nolanjut.Visible = true;
                lanjut.Visible = false;
            }
            else
            {
                unit.Text = "<a href=\"javascript:popUnit('" + NoStock + "')\">"
                    + rs.Rows[0]["NoUnit"] + "</a>";

                string Lokasi = Db.SingleString("SELECT Lokasi FROM MS_RESERVASI WHERE NoStock = '" + NoStock + "'");
                lokasi.Items.Add(new ListItem(Lokasi));
                lokasi.SelectedValue = Lokasi;
                
                if(Convert.ToDecimal(rs.Rows[0]["PriceList"]) != 0)
                {
                    if (Convert.ToDecimal(rs.Rows[0]["PricelistKavling"]) != 0)
                    {
                        pldef.SelectedIndex = 0;
                    }
                    else
                    {
                        pldef.SelectedIndex = 1;
                        pl.Text = Cf.Num(rs.Rows[0]["PriceList"]);
                    }
                }
                else
                {
                    if (Convert.ToDecimal(rs.Rows[0]["PricelistKavling"]) != 0)
                    {
                        pldef.SelectedIndex = 2;
                        pl.Text = Cf.Num(rs.Rows[0]["PricelistKavling"]);
                    }
                    else
                    {
                        pldef.SelectedIndex = 0;
                        defaultc.Text = "Unit ini tidak memiliki pricelist";
                    }
                }

                luasbangunan.Text = Cf.Num(rs.Rows[0]["LuasNett"]);
                luastanah.Text = Cf.Num(rs.Rows[0]["LuasSG"]);

                string Tipe = Db.SingleString("SELECT Kategori FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                if (Tipe == "FLPP")
                {
                    sifatppn.SelectedIndex = 0;
                }
                else if (Tipe == "REAL ESTATE")
                {
                    sifatppn.SelectedIndex = 1;
                }

            }
        }

        private void FillWL()
        {
            string strSql = "SELECT "
                + " NoUrut"
                + ",Tgl"
                + ",TglExpire"
                + ",NoQueue"
                + ",Lokasi"
                + ",MS_CUSTOMER.Nama AS Cs"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
                + ",MS_RESERVASI.Status"
                + ",MS_RESERVASI.NoReservasi"
                + ",MS_RESERVASI.NoReservasi2"
                + ",MS_RESERVASI.Tgl AS TglReservasi"
                + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
                + " WHERE NoStock = '" + NoStock + "'"
                + " ORDER BY NoUrut";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Unit tidak memiliki waiting list.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUrut"].ToString();
                c.Font.Size = 15;
                c.Font.Bold = true;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a href=\"javascript:popEditReservasi('" + rs.Rows[i]["NoReservasi"] + "')\">"
                    + rs.Rows[i]["NoReservasi2"].ToString() + "</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglReservasi"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString()
                    + "<br>" + rs.Rows[i]["Ag"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Date(rs.Rows[i]["TglExpire"]);
                if ((string)rs.Rows[i]["Status"] == "E")
                {
                    if ((int)rs.Rows[i]["NoUrut"] == 1)
                    {
                        expireinfo.Text = "Reservasi urutan pertama dalam kondisi expire. Silakan menghubungi supevisor.";
                        expireinfo.ForeColor = Color.Red;
                        next.Enabled = false;
                    }
                    c.Text = c.Text + "<br><b class=err>Expire</b>";
                }
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }

            //belum ada waiting list tidak boleh lanjut.
            if (rs.Rows.Count == 0)
                next.Enabled = false;
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            pilih.Visible = false;
            frm.Visible = true;

            FillDataReservasi();

            Js.Focus(this, tglkontrak);
            Js.Confirm(this, "Lanjutkan proses pendaftaran surat pesanan?");
        }

        private void FillDataReservasi()
        {
            string strSql = "SELECT "
                + " MS_RESERVASI.NoReservasi"
                + ", MS_CUSTOMER.NoCustomer"
                + ",MS_CUSTOMER.Nama AS CS"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
                + ",MS_RESERVASI.Gross"
                + ",MS_RESERVASI.Skema"
                + ",MS_RESERVASI.RefSkema"
                + ",MS_RESERVASI.Netto"
                + ",MS_RESERVASI.BungaPersen"
                + ",MS_RESERVASI.BungaNominal"
                + ",MS_RESERVASI.DiskonRupiah"
                + ",MS_RESERVASI.DiskonPersen"
                + ",MS_RESERVASI.DiskonTambahan"
                + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
                + " WHERE NoStock = '" + NoStock + "' AND NoUrut = 1";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                noreservasi.Text = rs.Rows[0]["NoReservasi"].ToString();
                customer.Text = "<a href=\"javascript:popEditCustomer('" + rs.Rows[0]["NoCustomer"] + "')\">"
                    + rs.Rows[0]["Cs"] + "</a>";

                agent.Text = rs.Rows[0]["Ag"].ToString();
                skema.Text = rs.Rows[0]["Skema"].ToString();

                carabayar.SelectedValue = rs.Rows[0]["RefSkema"].ToString();

                string Jenis = Db.SingleString("SELECT Jenis FROM REF_SKEMA WHERE Nomor = '" + rs.Rows[0]["RefSkema"].ToString() + "'");

                //nilai bunga rupiah
                string RumusBunga = Db.SingleString("SELECT ISNULL(Bunga, '') FROM REF_SKEMA WHERE Nomor = '" + rs.Rows[0]["RefSkema"].ToString() + "'");
                bunga2.Text = RumusBunga;
                if (sifatppn.SelectedIndex != 0)
                {
                    nilaiBunga.Text = Cf.Num(Func.NominalDiskon2(rs.Rows[0]["BungaPersen"].ToString(), Math.Round(Convert.ToDecimal(rs.Rows[0]["Gross"]) / (decimal)1.1)));
                }
                else
                {
                    nilaiBunga.Text = Cf.Num(Func.NominalDiskon2(rs.Rows[0]["BungaPersen"].ToString(), Math.Round(Convert.ToDecimal(rs.Rows[0]["Gross"]))));
                }

                //nilai diskon persen
                string RumusDiskon = Db.SingleString("SELECT ISNULL(Diskon, '') FROM REF_SKEMA WHERE Nomor = '" + rs.Rows[0]["RefSkema"].ToString() + "'");
                if (rs.Rows[0]["DiskonPersen"].ToString() == "")
                {
                    diskon2.Text = "0";
                } 
                else
                {
                    diskon2.Text = Cf.Num(RumusDiskon);
                }
                
                //nilai diskon rupiah
                if(sifatppn.SelectedIndex != 0)
                {
                    nilaiDiskon.Text = Cf.Num(Func.NominalDiskon2(RumusDiskon, Math.Round(Convert.ToDecimal(rs.Rows[0]["Gross"]) / (decimal)1.1)));
                }
                else
                {
                    nilaiDiskon.Text = Cf.Num(Func.NominalDiskon2(RumusDiskon, Math.Round(Convert.ToDecimal(rs.Rows[0]["Gross"]))));
                }

                diskonLumpSum.Text = Cf.Num(rs.Rows[0]["DiskonTambahan"]);

                carabayar2.SelectedValue = Jenis;
                carabayar2.Enabled = false;

                nettorsv.Text = Cf.Num(rs.Rows[0]["Netto"]);

                DataTable rsTTR = Db.Rs("SELECT NoTTR, Total FROM MS_TTR WHERE NoReservasi = " + NoReservasi);

                trNoTTR.Visible = false;
                trNilaiTTR.Visible = false;

                if (rsTTR.Rows.Count > 0)
                {
                    trNoTTR.Visible = true;
                    trNilaiTTR.Visible = true;

                    infoNoTTR.Text = "<a href=\"javascript:callEditTTR('"
                            + rsTTR.Rows[0]["NoTTR"].ToString() + "')\">"
                            + rsTTR.Rows[0]["NoTTR"].ToString() + "</a>";
                    infoNilaiTTR.Text = Cf.Num(rsTTR.Rows[0]["Total"]);
                }
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tglkontrak))
            {
                x = false;
                if (s == "") s = tglkontrak.ID;
                tglkontrakc.Text = "Tanggal";
            }
            else
                tglkontrakc.Text = "";

            if (!Cf.isTgl(targetst))
            {
                x = false;
                if (s == "") s = targetst.ID;
                targetstc.Text = "Tanggal";
            }
            else
                targetstc.Text = "";

            if (JenisPPN.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = JenisPPN.ID;
                JenisPPNc.Text = "Pilih";
            }
            else
                JenisPPNc.Text = "";

            if (carabayar2.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = carabayar2.ID;
                carabayarc.Text = "Pilih salah satu jenis.";
            }
            else
                carabayarc.Text = "";

            if (carabayar.SelectedIndex == 0 && carabayar2.SelectedIndex == -1)
            {
                x = true;
            }

            if (!Cf.isMoney(fo))
            {
                x = false;
                if (s == "") s = fo.ID;
                foc.Text = "Angka";
            }
            else
                foc.Text = "";

            if (ddlSumberDana.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = ddlSumberDana.ID;
                ddlSumberDanac.Text = "Harus Dipilih";
            }
            else
                ddlSumberDanac.Text = "";

            if (ddlTujuan.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = ddlTujuan.ID;
                ddlTujuanc.Text = "Harus Dipilih";
            }
            else
                ddlTujuanc.Text = "";

            if (pl.Text == "0")
            {
                x = false;
                if (s == "") s = pl.ID;
                pricec.Text = "Pricelist tidak boleh 0";
            }

            if (paketinvest.Checked)
            {
                if (!Cf.isTgl(tglinv))
                {
                    x = false;
                    tglinvc.Text = "Tanggal berakhir paket investasi harap diisi";
                }
            }
            else
            {
                tglinvc.Text = "";
            }

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Jenis Tanggungan PPN harus dipilih.\\n"
                    + "3. Price List tidak boleh bernilai 0.\\n"
                    + "4. Cara bayar harus dipilih."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                DateTime TglKontrak = Convert.ToDateTime(tglkontrak.Text);
                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                //Numerator
                nokontrak.Text = Numerator.SuratPesanan(TglKontrak.Month, TglKontrak.Year, Project);

                decimal nilaitagihan = Db.SingleDecimal("SELECT Netto FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "'");

                DateTime TargetST = Convert.ToDateTime(targetst.Text);
                string Skema = Cf.Str(carabayar.SelectedItem.Text);
                decimal pricelist = Convert.ToDecimal(pl.Text);
                string Lokasi = Db.SingleString("SELECT Lokasi FROM REF_LOKASI WHERE Nama='" + lokasi.SelectedValue + "'");
                int LokasiPenjualan = Db.SingleInteger("SELECT LokasiPenjualan FROM MS_RESERVASI WHERE NoReservasi ='" + NoReservasi + "'");
                string Tujuan = ddlTujuan.SelectedValue;
                string SumberDana = ddlSumberDana.SelectedValue;
                string SumberDanaLainnya = "";
                if (lainnya.Text != "")
                    SumberDanaLainnya = lainnya.Text;

                string RefEm = Db.SingleString("SELECT NoRefferatorAgent FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "'");
                string RefCust = Db.SingleString("SELECT NoRefferatorCustomer FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "'");

                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                string Pers = Db.SingleString("SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                string NamaPers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Pers + "'");

                Db.Execute("EXEC spKontrakDaftar"
                    + " '" + NoKontrak + "'"
                    + ",'" + NoStock + "'"
                    + ",'" + TglKontrak + "'"
                    + ",'" + Skema + "'"
                    + ",'" + TargetST + "'"
                    + ", " + pricelist
                    );

                int kpr = 0;
                if (carabayar2.SelectedValue == "KPR")
                {
                    kpr = 0;
                }
                else
                {
                    kpr = 1;
                }

                //VA
                string NoVA = "";
                string LokasiVA = Db.SingleString("SELECT Lokasi FROM MS_KONTRAK WHERE NoStock = '" + NoStock + "'");

                string VA = "8151";

                //di kurang 1 karena sudah save kontrak di ms kontrak
                //int CountUnit = Db.SingleInteger("SELECT Count(*) FROM MS_KONTRAK WHERE NoStock = '" + NoStock + "'") - 1;
                int CountUnit = Db.SingleInteger("SELECT Count(*) FROM MS_KONTRAK WHERE NoStock = '" + NoStock + "'");
                string Nomor = Db.SingleString("SELECT Nomor FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                string Lantai = Db.SingleString("SELECT Lantai FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                string NoLantai = "";
                if (Lantai == "BLV")
                {
                    NoLantai = "99";
                }
                else
                {
                    NoLantai = Lantai.PadLeft(2, '0');
                }

                //Tambahan Harga Tanah dan Bangunan 6 Des 2018
                string RumusBunga2 = bunga2.Text;
                string RumusDiskon2 = diskon2.Text;
                decimal HargaTanah = Db.SingleDecimal("Select ISNULL(HargaTanah, 0) From MS_UNIT Where NoStock = '" + NoStock + "'");
                decimal HargaTanahDPP = (HargaTanah / (decimal)1.1);
                decimal HargaTanahBunga = Func.NominalBunga2(RumusBunga2, HargaTanahDPP);
                decimal HargaTanahDiskon = Func.NominalDiskon2(RumusDiskon2, HargaTanahDPP);
                decimal HargaTanahPermeter = HargaTanahDPP + HargaTanahBunga - HargaTanahDiskon;
                //End of Tambahan

                int Lokasii = Db.SingleInteger("SELECT SNVA FROM REF_LOKASI WHERE Lokasi = '" + LokasiVA + "'");
                NoVA = VA + CountUnit.ToString().PadLeft(2, '0') + Lokasii.ToString().PadLeft(2, '0') + NoLantai + Nomor.PadLeft(2, '0');

                //Manual update
                string sSQL = "UPDATE MS_KONTRAK"
                    + " SET JenisPPN = '" + JenisPPN.SelectedItem.Text + "'"
                    + ", JenisKPR = " + kpr
                    + ", CaraBayar = '" + carabayar2.SelectedValue + "'"
                    + ", Lokasi = '" + Lokasi + "'"
                    + ", LokasiPenjualan = '" + LokasiPenjualan + "'"
                    + ", SumberDana='" + SumberDana + "'"
                    + ", SumberDanaLainnya='" + Cf.Str(SumberDanaLainnya) + "'"
                    + ", TujuanKontrak = '" + Tujuan + "'"
                    + ", RefSkema = " + carabayar.SelectedValue
                    + ", NoKontrakManual = '" + NoKontrakManual + "'"
                    + ", NoRefferatorAgent = '" + RefEm + "'"
                    + ", NoRefferatorCustomer = '" + RefCust + "'"
                    + ", TitipJual=" + Convert.ToByte(titipjual.SelectedValue.ToString())
                    + ", NoVA = '" + NoVA + "'"
                    + ", HargaTanah = " + HargaTanahPermeter
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    ;

                Db.Execute(sSQL);
                Db.Execute("UPDATE MS_KONTRAK SET Note = '" + note.Text + "' WHERE NoKontrak = '" + NoKontrak + "' ");
                Db.Execute("UPDATE MS_KONTRAK SET Project = '" + Project + "', NamaProject='" + NamaProject + "',Pers='" + Pers + "',NamaPers = '" + NamaPers + "' WHERE NoKontrak = '" + NoKontrak + "'");

                byte paketinv = 0;
                if (paketinvest.Checked)
                {
                    paketinv = 1;
                }

                if (paketinv == (byte)1)
                {
                    Db.Execute("UPDATE MS_KONTRAK SET TglPaketInvestasi='" + Convert.ToDateTime(tglinv.Text) + "', PaketInvestasi = " + paketinv + " WHERE NoKontrak = '" + NoKontrak + "'");
                }

                int c = Db.SingleInteger("SELECT COUNT(NoKontrak) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                if (c == 0)
                {
                    nokontrak.Text = "#AUTO#";

                    Js.Alert(
                        this
                        , "Unit Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Unit sudah dijual kepada customer lain.\\n"
                        , "document.getElementById('tglkontrak').focus();"
                        + "document.getElementById('tglkontrak').select();"
                        );
                }
                else
                {
                    int NoAgent = Db.SingleInteger("SELECT NoAgent FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "'");
                    SaveKontrakAgent(NoAgent, 1);
                    SaveTagihan();

                    int Count = Db.SingleInteger("SELECT COUNT(*) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");
                    if (Count != 0)
                        SaveFO();

                    DataTable rs = Db.Rs("SELECT "
                        + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                        + ",MS_KONTRAK.NoUnit AS [Unit]"
                        + ",MS_CUSTOMER.Nama AS [Customer]"
                        + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS [Agent]"
                        + ",CONVERT(varchar,MS_KONTRAK.TglKontrak,106) AS [Tanggal Kontrak]"
                        + ",MS_KONTRAK.NoStock AS [No. Stock]"
                        + ",MS_KONTRAK.Luas AS [Luas]"
                        + ",MS_KONTRAK.Lokasi AS [Lokasi]"
                        + ",MS_KONTRAK.Gross AS [Nilai Gross]"
                        + ",MS_KONTRAK.DiskonRupiah AS [Diskon dalam Rupiah]"
                        + ",MS_KONTRAK.DiskonPersen AS [Diskon dalam Persen]"
                        + ",MS_KONTRAK.DiskonKet AS [Keterangan Diskon]"
                        + ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                        + ",MS_KONTRAK.Skema"
                        + ",CONVERT(varchar,MS_KONTRAK.TargetST,106) AS [Jadwal Serah Terima]"
                        + ", MS_KONTRAK.JenisPPN AS [PPN Ditanggung]"
                        + ", CASE MS_KONTRAK.JenisKPR"
                        + "		WHEN 0 THEN 'KPR'"
                        + "		WHEN 1 THEN 'NON-KPR'"
                        + "	END AS [Jenis KPR]"
                        + ",MS_KONTRAK.SumberDana AS [Sumber Dana]"
                        + ",MS_KONTRAK.SumberDanaLainnya AS [Sumber Dana Lainnya]"
                        + ",MS_KONTRAK.TujuanKontrak AS [Tujuan Transaksi]"
                        + ",MS_KONTRAK.NoRefferatorAgent"
                        + ",MS_KONTRAK.NoRefferatorCustomer"
                        + ", CASE MS_KONTRAK.TitipJual"
                        + "		WHEN 0 THEN 'Non Titip Jual'"
                        + "		WHEN 1 THEN 'Titip Jual'"
                        + "	END AS [Status Titip Jual]"
                        + ", CASE MS_KONTRAK.PaketInvestasi"
                        + "		WHEN 0 THEN 'TIDAK'"
                        + "		WHEN 1 THEN 'YA'"
                        + "	END AS [Status Paket Investasi]"
                        + ", TglPaketInvestasi AS [Tanggal Berakhir Paket Investasi]"
                        + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                        + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );

                    DataTable rsTagihan = Db.Rs("SELECT "
                        + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                        + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                    //Logfile
                    string Ket = Cf.LogCapture(rs)
                        + Cf.LogList(rsTagihan, "JADWAL TAGIHAN");

                    Db.Execute("EXEC spLogKontrak"
                        + " 'DAFTAR'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    //floor plan
                    string Peta = Db.SingleString("SELECT Peta "
                        + " FROM MS_UNIT INNER JOIN MS_KONTRAK ON MS_UNIT.NoStock = MS_KONTRAK.NoStock "
                        + " WHERE NoKontrak = '" + NoKontrak + "'");
                    Func.GenerateFP(Peta);

                    //PostingTTR(NoKontrak);
                    PostingTTS(NoKontrak);

                    Response.Redirect("KontrakDaftar3.aspx?NoKontrak=" + NoKontrak);
                }
            }
        }

        private void SaveKontrakAgent(object _NoAgent, int NoUrut)
        {
            var rs = Db.Rs("Select * from ms_agent where NoAgent='" + _NoAgent + "'");
            if (rs.Rows.Count != 0)
            {
                var r = rs.Rows[0];
                Db.Execute("INSERT INTO MS_KONTRAK_AGENT (NoKontrak, NoUrut, SalesTipe, SalesLevel, NoAgent) VALUES ('" + NoKontrak + "','" + NoUrut + "','" + r["SalesTipe"] + "','" + r["SalesLevel"] + "','" + _NoAgent + "')");

                SaveKontrakAgent(r["Atasan"], ++NoUrut);
            }
        }

        protected void PostingTTR(string Kontrak)
        {
            DataTable rs = Db.Rs("SELECT TOP 1 * FROM MS_TTR WHERE NoReservasi = '" + NoReservasi + "'");
            if (rs.Rows.Count > 0)
            {
                string NoTTR = rs.Rows[0]["NoTTR"].ToString();
                DateTime TglMEMO = DateTime.Today;
                string Unit = rs.Rows[0]["Unit"].ToString();
                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoUnit = '" + rs.Rows[0]["Unit"].ToString() + "'");
                string Customer = rs.Rows[0]["Customer"].ToString();
                string CaraBayar = rs.Rows[0]["CaraBayar"].ToString();
                decimal Nilai = Convert.ToDecimal(rs.Rows[0]["Total"]);
                string Ket = rs.Rows[0]["Ket"].ToString();


                #region Memo yg pake format
                //NoTTS
                string[] FM = Cf.DaySlash(TglMEMO).ToString().Split('/');
                string formatMonth = FM[1];
                string formatTahun = TglMEMO.Year.ToString().Substring(2, 2);
                string NoMEMO2 = "";
                bool hasfound = false;
                while (!hasfound)
                {
                    if (!Response.IsClientConnected) break;

                    int num = Db.SingleInteger("SELECT COUNT(NoMemo2) FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO");
                    if (num == 0)
                    {
                        //TTS Pertama
                        int increment = num + 1;
                        string no = increment.ToString().PadLeft(6, '0');
                        NoMEMO2 = "M-" + formatTahun + formatMonth + no;

                    }
                    else
                    {
                        //TTS Berikutnya
                        string terakhir = Db.SingleString("SELECT TOP 1 NoMEMO2 FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO WHERE NoMEMO2 <> '0' ORDER BY NoMEMO DESC");
                        string nourut = terakhir.Substring(6, 6);
                        int temp = Convert.ToInt32(nourut) + 1;
                        string no = temp.ToString().PadLeft(6, '0');
                        NoMEMO2 = "M-" + formatTahun + formatMonth + no;

                    }

                    if (isUnique(NoMEMO2)) hasfound = true;
                }
                #endregion
                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spMEMORegistrasi"
                   + " '" + TglMEMO + "'"
                   + ",'" + Act.UserID + "'"
                   + ",'" + Act.IP + "'"
                   + ",'JUAL'" //dari ttr
                   + ",'" + NoKontrak + "'"
                   + ",'" + Unit + "'"
                   + ",'" + Customer + "'"
                   + ",'" + CaraBayar + "'"
                   + ",'" + Ket + "'"
                   + ",0"
                   );

                int NoMEMO = Db.SingleInteger("SELECT TOP 1 NoMEMO FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO ORDER BY NoMEMO DESC");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_MEMO SET NoMEMO2 = '" + NoMEMO2 + "' WHERE NoMEMO ='" + NoMEMO + "'");

                // Get Acc From TTR for MEMO
                string Acc = Db.SingleString("SELECT Acc FROM MS_TTR WHERE NoTTR = '" + NoTTR + "'");

                // Manual update Acc memo
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_MEMO"
                    + " SET Acc = '" + Acc + "'"
                    //+ " ,NoTTR = '" + NoTTR + "'"
                    //+ " WHERE Ref = '" + NoKontrak + "'"
                    + " WHERE NOMEMO = '" + NoMEMO + "'"
                    );

                int NoTagihan = 0;
                string Tipe = "";
                System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

                DataTable rsTagihan = Db.Rs("SELECT TOP 1 * FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = 1");
                if (rsTagihan.Rows.Count > 0)
                {
                    NoTagihan = Convert.ToInt32(rsTagihan.Rows[0]["NoUrut"]);
                    string NamaTagihan = Cf.Str(rsTagihan.Rows[0]["NamaTagihan"])
                        + " (" + rsTagihan.Rows[0]["Tipe"] + ")";

                    Tipe = rsTagihan.Rows[0]["Tipe"].ToString();

                    alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");
                }

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spMEMOAlokasi "
                    + " '" + NoMEMO + "'"
                    + ", " + NoTagihan
                    + ", " + Nilai
                    );

                Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_PELUNASAN"
                     + " SET"
                     + " TglPelunasan ='" + TglMEMO + "'"
                     + " ,SudahCair = 1"
                     + " WHERE NoKontrak='" + NoKontrak + "' AND NoMemo='" + NoMEMO + "' AND NoTagihan='" + NoTagihan + "'"
                    );
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_MEMO SET Status='POST', TglBKM=TglMemo WHERE NoMemo='" + NoMEMO + "'");

                DataTable rsLog = Db.Rs("SELECT "
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
                    + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO WHERE NoMEMO = " + NoMEMO);

                string KetLog = Cf.LogCapture(rsLog)
                    + "<br>***ALOKASI PEMBAYARAN:<br>"
                    + alokasi.ToString();

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogMEMO"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + NoMEMO.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID2 = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO_LOG ORDER BY LogID DESC");
                string Project2 = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO WHERE NoMEMO = '" + NoMEMO + "'");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_MEMO_LOG SET Project = '" + Project2 + "' WHERE LogID  = " + LogID2);

                Db.Execute("EXEC ISC064_MARKETINGJUAL..spProsentasePelunasan '" + NoKontrak + "'");

                //Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_KONTRAK SET FlagMemo=1 WHERE NoKontrak='" + NoKontrak + "'");
                Db.Execute("UPDATE MS_TTR SET NoReservasi = '' WHERE Unit = '" + Unit + "'");
                //Db.Execute("UPDATE MS_KONTRAK SET NoTTR = '" + NoTTR + "' WHERE NoKontrak = '" + Kontrak + "'");
            }
        }

        protected void PostingTTS(string Kontrak)
        {
            DataTable rs = Db.Rs("SELECT TOP 1 * FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoReservasi = '" + NoReservasi + "'");
            if (rs.Rows.Count > 0)
            {
                int NoTTS = Convert.ToInt32(rs.Rows[0]["NoTTS"]);
                DateTime TglTTS = Convert.ToDateTime(rs.Rows[0]["TglTTS"]);
                string Unit = rs.Rows[0]["Unit"].ToString();
                string Project = rs.Rows[0]["Project"].ToString();
                string Customer = rs.Rows[0]["Customer"].ToString();
                string CaraBayar = rs.Rows[0]["CaraBayar"].ToString();
                decimal Nilai = Convert.ToDecimal(rs.Rows[0]["Total"]);
                string Ket = rs.Rows[0]["Ket"].ToString();
                
                int NoTagihan = 0;
                string Tipe = "";
                System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

                DataTable rsTagihan = Db.Rs("SELECT TOP 1 * FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = 1");
                if (rsTagihan.Rows.Count > 0)
                {
                    NoTagihan = Convert.ToInt32(rsTagihan.Rows[0]["NoUrut"]);
                    string NamaTagihan = Cf.Str(rsTagihan.Rows[0]["NamaTagihan"])
                        + " (" + rsTagihan.Rows[0]["Tipe"] + ")";

                    Tipe = rsTagihan.Rows[0]["Tipe"].ToString();

                    alokasi.Append(NamaTagihan + " " + Cf.Num(Nilai) + "<br>");
                }

                //ini belum jadi kwitansi yah.
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET Ref = '" + NoKontrak + "' WHERE NoTTS = '" + NoTTS + "'");

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSAlokasiReservasi "
                            + "  " + NoTTS
                            + ", " + NoTagihan
                            + ", " + Nilai
                            );

                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
                     + " SET"
                     + " TglPelunasan ='" + TglTTS + "'"
                     + " ,SudahCair = 0"
                     + " WHERE NoKontrak='" + NoKontrak + "' AND NoTTS = '" + NoTTS  + "' AND NoTagihan='" + NoTagihan + "'"
                    );

                Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spProsentasePelunasan '" + NoKontrak + "'");
            }
        }

        private void SaveTagihan()
        {
            int CaraBayar = Convert.ToInt32(carabayar.SelectedValue);
            decimal Netto = 0;
            //cara bayar 0 = customize
            if (CaraBayar != 0)
            {
                string RumusDiskon = diskon2.Text;
                string RumusDiskon2 = Db.SingleString("SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + CaraBayar);
                string RumusBunga = bunga2.Text;
                decimal Gross = Db.SingleDecimal("SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                decimal Gross2 = Gross;

                Netto = Func.SetelahBunga(RumusBunga, Gross2);
                decimal GrossAfterDiskon = Func.SetelahDiskon(RumusDiskon, Netto);
                Netto = (CaraBayar != 0) ? Func.SetelahDiskon(RumusDiskon, Netto) : Netto;
                //decimal NilaiDiskon = Math.Round(Gross2 - GrossAfterDiskon);
                
                /* DISKON TAMBAHAN SAAT CLOSING */
                decimal DiskonTambahan = 0;
                if (jenisDiskon.SelectedIndex == 0)
                {	//Diskon lum sum
                    DiskonTambahan = Convert.ToDecimal(diskonLumpSum.Text);
                }
                else if (jenisDiskon.SelectedIndex == 1)
                {	//Diskon % bertingkat
                    decimal coba = 0, totaldisc = 0;
                    string[] diskonpersen = diskontambahPersen.Text.Split('+');
                    decimal dpp = Netto;

                    if (diskontambahPersen.Text != "")
                    {
                        for (int a = 0; a <= diskonpersen.GetUpperBound(0); a++)
                        {
                            coba = Math.Round(Convert.ToDecimal(diskonpersen[a]) * dpp / (decimal)100);
                            dpp -= coba;
                            totaldisc += coba;
                        }
                    }
                    else
                    {
                        totaldisc = 0;
                    }

                    DiskonTambahan = totaldisc;
                }
                Netto -= DiskonTambahan;

                Db.Execute("UPDATE MS_KONTRAK"
                 + " SET DiskonTambahan = " + DiskonTambahan
                 + " WHERE NoKontrak = '" + NoKontrak + "'");
                
                /********************************/
                string DiskonPersen = diskon2.Text;
                decimal DiskonRupiah = 0;//Convert.ToDecimal(nilaiDiskon.Text);
                if (sifatppn.SelectedIndex != 0)
                {
                    DiskonRupiah = Func.NominalDiskon2(RumusDiskon, Math.Round(Gross / (decimal)1.1));
                }
                else
                {
                    DiskonRupiah = Func.NominalDiskon2(RumusDiskon, Math.Round(Gross));
                }

                Db.Execute("UPDATE MS_KONTRAK"
                 + " SET DiskonKet='" + RumusDiskon2 + "'"
                 + ", DiskonPersen='" + DiskonPersen + "'"
                 + ", DiskonRupiah = " + DiskonRupiah
                 + " WHERE NoKontrak = '" + NoKontrak + "'");


                /***********/

                decimal NilaiPPN = 0;
                decimal NilaiKontrak = 0;

                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                string Pers = Db.SingleString("SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                string NamaPers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Pers + "'");

                string ParamID = "PLIncludePPN" + Project;
                bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";
                decimal DPP = 0;

                //if (includeppn)
                //    DPP = Math.Round(Netto / (decimal)1.1);
                //else
                //    DPP = Netto;


                if (sifatppn.SelectedIndex == 1)
                {
                    if (includeppn)
                    {
                        DPP = Math.Round(Netto / (decimal)1.1);

                        if (roundppn.Checked)
                            NilaiPPN = Math.Round(Netto - DPP);
                        else
                            NilaiPPN = Netto - DPP;
                    }
                    else
                    {
                        DPP = Netto;
                        NilaiPPN = (DPP * (decimal)0.1);

                        if (roundppn.Checked)
                            NilaiPPN = Math.Round(NilaiPPN);
                    }
                }
                else
                {
                    DPP = Netto;
                }

                NilaiKontrak = DPP + NilaiPPN;
                decimal PPN = Math.Round(NilaiKontrak - DPP);


                Db.Execute("EXEC spKontrakDiskon"
                    + " '" + NoKontrak + "'"
                    + ", " + Gross2
                    + ", " + NilaiKontrak
                    + ", " + DiskonRupiah
                    + ",'" + RumusDiskon + "'"
                    + ",'" + Cf.Str(RumusDiskon2) + "'"
                    );

                if (sifatppn.SelectedIndex != 0)
                {
                    Gross = Math.Round(Gross / (decimal)1.1);
                }

                Db.Execute("EXEC spKontrakBunga"
                   + " '" + NoKontrak + "'"
                   + ", " + Gross
                   + ", " + Math.Round(NilaiKontrak)
                   + ",'" + RumusBunga + "'"
                   );

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET "
                    + " NilaiPPN = " + NilaiPPN
                    + ", NilaiDPP = " + DPP
                    + " , NilaiKontrak = " + NilaiKontrak
                    + " WHERE NoKontrak = '" + NoKontrak + "'");

                string[,] x = Func.Breakdown(CaraBayar, NilaiKontrak, Convert.ToDateTime(tglkontrak.Text));
                for (int i = 0; i <= x.GetUpperBound(0); i++)
                {

                    Db.Execute("EXEC spTagihanDaftar"
                        + " '" + NoKontrak + "'"
                        + ",'" + x[i, 2] + "'"
                        + ",'" + Convert.ToDateTime(x[i, 3]) + "'"
                        + ", " + Convert.ToDecimal(x[i, 4])
                        + ",'" + x[i, 1] + "'"
                        );

                    int NoUrut = Db.SingleInteger("SELECT TOP 1 NoUrut FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut DESC");
                    Db.Execute("UPDATE MS_TAGIHAN"
                        + " SET KPR = " + x[i, 5]
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        + " AND NoUrut = " + NoUrut
                        );

                }

                string ParamID2 = "KontrakIncludeBiaya" + Project;
                bool includebiaya = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID2 + "'") == "True";

                decimal NilaiInclude = 0;

                if (includebiaya)
                {
                    DataTable rs2 = Db.Rs("SELECT * FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                    decimal BiayaBPHTB = Convert.ToDecimal(rs2.Rows[0]["BiayaBPHTB"]);
                    decimal BiayaSurat = Convert.ToDecimal(rs2.Rows[0]["BiayaSurat"]);
                    decimal BiayaProses = Convert.ToDecimal(rs2.Rows[0]["BiayaProses"]);
                    decimal BiayaLainLain = Convert.ToDecimal(rs2.Rows[0]["BiayaLainLain"]);
                    NilaiInclude = NilaiKontrak + BiayaBPHTB + BiayaProses + BiayaSurat + BiayaLainLain;
                    decimal PersenPokok = Convert.ToDecimal(rs2.Rows[0]["PriceList"]) / NilaiInclude * 100;
                    decimal PersenPPN = PPN / NilaiInclude * 100;
                    decimal PersenBPHTB = BiayaBPHTB / NilaiInclude * 100;
                    decimal PersenSurat = BiayaSurat / NilaiInclude * 100;
                    decimal PersenProses = BiayaProses / NilaiInclude * 100;
                    decimal PersenLain = BiayaLainLain / NilaiInclude * 100;

                    Db.Execute("UPDATE MS_KONTRAK SET PersenPokok = " + Math.Round(PersenPokok, 2)
                            + ", PersenPPN = " + Math.Round(PersenPPN, 2)
                            + ", PersenBPHTB = " + Math.Round(PersenBPHTB, 2)
                            + ", PersenSurat = " + Math.Round(PersenSurat, 2)
                            + ", PersenProses = " + Math.Round(PersenProses, 2)
                            + ", PersenLain = " + Math.Round(PersenLain, 2)
                            + " WHERE NoKontrak = '" + NoKontrak + "'"
                            );
                }
                else
                {
                    //tambah tagihan bphtb
                    decimal bphtb = Db.SingleDecimal("SELECT BiayaBPHTB FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                    DateTime TglJT = DateTime.Today;
                    int tipe = Db.SingleInteger("SELECT COUNT(Tipe) FROM MS_TAGIHAN WHERE NoKontrak = '"+NoKontrak+"' AND Tipe = 'DP'");
                    if(tipe > 0)
                    {
                        TglJT = Db.SingleTime("SELECT TOP 1 TglJT FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND Tipe='DP' ORDER BY TglJT DESC");
                    }
                    else
                    {
                        TglJT = Db.SingleTime("SELECT TOP 1 TglJT FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND Tipe='ANG' ORDER BY TglJT DESC");
                    }

                    TglJT = TglJT.AddMonths(1);
                    if (bphtb > 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar"
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA BPHTB'"
                            + ",'" + TglJT + "'"
                            + ",'" + bphtb + "'"
                            + ",'ADM'"
                            );
                    }

                    //tambah tagihan biaya surat
                    decimal bsurat = Db.SingleDecimal("SELECT BiayaSurat FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                    if (bsurat > 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar"
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA SURAT'"
                            + ",'" + TglJT + "'"
                            + ",'" + bsurat + "'"
                            + ",'ADM'"
                            );
                    }

                    //tambah tagihan biaya proses
                    decimal bproses = Db.SingleDecimal("SELECT BiayaProses FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                    if (bproses > 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar"
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA PROSES'"
                            + ",'" + TglJT + "'"
                            + ",'" + bproses + "'"
                            + ",'ADM'"
                            );
                    }

                    //tambah tagihan biaya lain-lain
                    decimal blain = Db.SingleDecimal("SELECT BiayaLainLain FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                    if (blain > 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar"
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA LAIN-LAIN'"
                            + ",'" + TglJT + "'"
                            + ",'" + blain + "'"
                            + ",'ADM'"
                            );
                    }
                }


            }
            else
            {
                string RumusBunga = bunga2.Text;

                decimal Gross = Db.SingleDecimal(
                     "SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                decimal Gross2 = Gross;

                decimal GrossAfterDiskon = Gross2;
                Netto = Gross2;


                /* DISKON TAMBAHAN SAAT CLOSING */
                decimal DiskonTambahan = 0;
                if (jenisDiskon.SelectedIndex == 0)
                {	//Diskon lum sum
                    DiskonTambahan = Convert.ToDecimal(diskonLumpSum.Text);
                }
                else if (jenisDiskon.SelectedIndex == 1)
                {	//Diskon % bertingkat
                    decimal coba = 0, totaldisc = 0;
                    string[] diskonpersen = diskontambahPersen.Text.Split('+');
                    decimal dpp = Netto;

                    if (diskontambahPersen.Text != "")
                    {
                        for (int a = 0; a <= diskonpersen.GetUpperBound(0); a++)
                        {
                            coba = Math.Round(Convert.ToDecimal(diskonpersen[a]) * dpp / (decimal)100);
                            dpp -= coba;
                            totaldisc += coba;
                        }
                    }
                    else
                    {
                        totaldisc = 0;
                    }

                    DiskonTambahan = totaldisc;
                }
                Netto -= DiskonTambahan;

                Db.Execute("UPDATE MS_KONTRAK"
                 + " SET DiskonTambahan = " + DiskonTambahan
                 + " WHERE NoKontrak = '" + NoKontrak + "'");


                /********************************/
                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                string Pers = Db.SingleString("SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                string NamaPers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Pers + "'");

                string ParamID = "PLIncludePPN" + Project;
                bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";

                decimal NilaiPPN = 0;
                decimal NilaiKontrak = 0;
                decimal DPP = 0;

                if (includeppn)
                    DPP = Math.Round(Netto / (decimal)1.1);
                else
                    DPP = Netto;


                if (sifatppn.SelectedIndex == 1)
                {
                    if (includeppn)
                    {
                        if (roundppn.Checked)
                            NilaiPPN = Math.Round(Netto - DPP);
                        else
                            NilaiPPN = Netto - DPP;
                    }
                    else
                    {
                        NilaiPPN = (DPP * (decimal)0.1);

                        if (roundppn.Checked)
                            NilaiPPN = Math.Round(NilaiPPN);
                    }
                }

                NilaiKontrak = DPP + NilaiPPN;
                decimal PPN = Math.Round(NilaiKontrak - DPP);


                Db.Execute("EXEC spKontrakDiskon"
                    + " '" + NoKontrak + "'"
                    + ", " + Gross2
                    + ", " + NilaiKontrak
                    + ",0"
                    + ",''"
                    + ",''"
                    );

                Db.Execute("EXEC spKontrakBunga"
                   + " '" + NoKontrak + "'"
                   + ", " + Gross
                   + ", " + Math.Round(NilaiKontrak)
                   + ",'" + RumusBunga + "'"
                   );

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET "
                    + " NilaiPPN = " + NilaiPPN
                    + ", NilaiDPP = " + DPP
                    + " , NilaiKontrak = " + NilaiKontrak
                    + " WHERE NoKontrak = '" + NoKontrak + "'");
            }

        }

        private void SaveFO()
        {
            int CaraBayar = Convert.ToInt32(carabayar.SelectedValue);

            decimal FONominal = Convert.ToDecimal(fo.Text);

            int FOCounter = Convert.ToInt32(focounter.Text);

            int count = 1;

            int NoUrut = Db.SingleInteger("SELECT ISNULL(MAX(NoUrut),0) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");
            DateTime TglJT = Db.SingleTime("SELECT TglJT FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" + NoUrut + "' ");

            if (fo.Text != "0")
            {
                string[,] x = Func.BreakFO(NoKontrak, FOCounter, FONominal);
                for (int i = 0; i <= x.GetUpperBound(0); i++)
                {
                    if (!Response.IsClientConnected) break;
                    string a = "FITTING OUT ";
                    string b = "ADM";
                    Db.Execute("EXEC spTagihanDaftar"
                        + " '" + NoKontrak + "'"
                        + ",'" + a + " " + x[i, 2] + "'"
                        + ",'" + TglJT.AddMonths(count) + "'"
                        + ", " + Convert.ToDecimal(x[i, 1])
                        + ",'" + b + "'"
                        );
                    count++;
                }
            }
        }

        private bool isUnique(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO WHERE NoMEMO2 = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
            }
        }

        private string NoKontrakManual
        {
            get
            {
                return Cf.Pk(nokontrakmanual.Text);
            }
        }

        protected string NoReservasi
        {
            get
            {
                return Cf.Pk(noreservasi.Text);
            }
        }



        protected void diskon2_TextChanged(object sender, EventArgs e)
        {
            if (carabayar.SelectedIndex > 0)
            {
                SetDiskon2();
            }
            diskon2.Focus();
        }

        private void SetDiskon2()
        {
            decimal Gross = Convert.ToDecimal(pl.Text);
            decimal Bunga = nilaiBunga.Text != "" ? Convert.ToDecimal(nilaiBunga.Text) : 0;
            decimal Gross2 = Gross + Bunga;

            string RumusDiskon = diskon2.Text;
            string[] x = RumusDiskon.Split('+');

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != "")
                {
                    decimal y = Convert.ToDecimal(x[i]);
                    if (i < (x.Length - 1))
                        sb.Append(y.ToString() + "+");
                    else
                        sb.Append(y.ToString());
                }
            }
            diskon2.Text = sb.ToString();

            decimal diskon = Func.NominalDiskon2(RumusDiskon, Gross2);
            if (diskon == 0)
            {
                nilaiDiskon.Text = "0";
            }
            else
            {
                nilaiDiskon.Text = Cf.Num(Math.Round(diskon, 0).ToString());
            }
        }

        protected void bunga2_TextChanged(object sender, EventArgs e)
        {
            if (carabayar.SelectedIndex > 0)
            {
                SetBunga2();
            }
        }

        private void SetBunga()
        {
            decimal Gross = Convert.ToDecimal(pl.Text); //Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
            //+ " WHERE NoStock = '" + NoStock + "'");
            decimal Gross2 = Gross;

            string RumusBunga = bunga2.Text;
            string[] x = RumusBunga.Split('+');

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != "")
                {
                    decimal y = Convert.ToDecimal(x[i]) * (decimal)1;
                    if (i < (x.Length - 1))
                        sb.Append(y.ToString() + "+");
                    else
                        sb.Append(y.ToString());
                }
            }
            bunga2.Text = sb.ToString();

            decimal bunga = Func.NominalDiskon(RumusBunga, Gross2);
            if (bunga == 0)
            {
                nilaiBunga.Text = "";
            }
            else
            {
                nilaiBunga.Text = Cf.Num(Math.Round(bunga, 0).ToString());
            }

        }

        private void SetBunga2()
        {
            decimal Gross = Convert.ToDecimal(pl.Text);

            string RumusBunga = bunga2.Text;
            string[] x = RumusBunga.Split('+');

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != "")
                {
                    decimal y = Convert.ToDecimal(x[i]);
                    if (i < (x.Length - 1))
                        sb.Append(y.ToString() + "+");
                    else
                        sb.Append(y.ToString());
                }
            }
            bunga2.Text = sb.ToString();

            decimal bunga = Func.NominalDiskon2(RumusBunga, Gross);
            if (bunga == 0)
            {
                nilaiBunga.Text = "";
            }
            else
            {
                nilaiBunga.Text = Cf.Num(Math.Round(bunga, 0).ToString());
            }
        }

        protected void sifatppn_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            trppn.Visible = sifatppn.SelectedIndex == 1;
        }

        protected void Pricelist_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string strSql = "SELECT NoUnit, PriceList, Luas,PricelistKavling,DefaultPL,Project,Lokasi "
                + " FROM MS_UNIT WHERE NoStock = '" + NoStock + "'"
                + " AND Status = 'A' AND FlagSPL != 0"; //cek kondisi
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
            {
                defaultc.Text = "Unit ini belum memiliki Pricelist";
            }
            else
            {
                if (pldef.SelectedIndex == 1)
                {
                    pl.Text = Cf.Num(rs.Rows[0]["PriceList"]);
                }
                else if (pldef.SelectedIndex == 2)
                {
                    pl.Text = Cf.Num(rs.Rows[0]["PricelistKavling"]);
                }
                else
                {
                    pl.Text = "0";
                }
            }
        }

        protected void ddlSumberDana_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSumberDana.SelectedIndex == 4)
            {
                trLainnya.Visible = true;
            }
            else
            {
                trLainnya.Visible = false;
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

        protected void skema_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (carabayar.SelectedIndex == 0)
            {
                carabayar2.Enabled = true; ;
            }
            else
            {
                carabayar2.Enabled = false;
                string JenisSkema = Db.SingleString("SELECT Jenis FROM REF_SKEMA WHERE Nomor = '" + carabayar.SelectedValue + "' ");

                carabayar2.SelectedValue = JenisSkema;

                string RumusDiskon = Db.SingleString(
                    "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + carabayar.SelectedValue);
                string Ket = Db.SingleString(
                    "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + carabayar.SelectedValue);
                string CaraBayar = Db.SingleString(
                    "SELECT Jenis FROM REF_SKEMA WHERE Nomor = " + carabayar.SelectedValue);
                string RumusBunga = Db.SingleString(
                "SELECT Bunga FROM REF_SKEMA WHERE Nomor = " + carabayar.SelectedValue);
                string Ket2 = Db.SingleString(
                "SELECT Bungaket FROM REF_SKEMA WHERE Nomor = " + carabayar.SelectedValue);


                carabayar2.SelectedValue = CaraBayar;
                carabayar2.Enabled = false;

                //nilai diskon rupiah
                diskon2.Text = RumusDiskon;
                diskonket.Text = Ket;
                if (sifatppn.SelectedIndex != 0)
                {
                    nilaiDiskon.Text = Cf.Num(Func.NominalDiskon2(RumusDiskon, Math.Round(Convert.ToDecimal(pl.Text) / (decimal)1.1)));
                }
                else
                {
                    nilaiDiskon.Text = Cf.Num(Func.NominalDiskon2(RumusDiskon, Math.Round(Convert.ToDecimal(pl.Text))));
                }
                
                //nilai diskon rupiah
                bunga2.Text = RumusBunga;
                bungaket.Text = Ket2;
                if (sifatppn.SelectedIndex != 0)
                {
                    nilaiBunga.Text = Cf.Num(Func.NominalBunga2(RumusBunga, Math.Round(Convert.ToDecimal(pl.Text) / (decimal)1.1)));
                }
                else
                {
                    nilaiBunga.Text = Cf.Num(Func.NominalBunga2(RumusBunga, Math.Round(Convert.ToDecimal(pl.Text))));
                }
                
                //SetBunga();
                //SetDiskon();
            }
        }

        private void SetDiskon()
        {
            decimal Gross = Convert.ToDecimal(pl.Text);
            decimal Bunga = nilaiBunga.Text != "" ? Convert.ToDecimal(nilaiBunga.Text) : 0;
            decimal Gross2 = Gross + Bunga;

            string RumusDiskon = diskon2.Text;
            string[] x = RumusDiskon.Split('+');

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != "")
                {
                    decimal y = Convert.ToDecimal(x[i]) * (decimal)-1;
                    if (i < (x.Length - 1))
                        sb.Append(y.ToString() + "+");
                    else
                        sb.Append(y.ToString());
                }
            }
            diskon2.Text = sb.ToString();

            decimal diskon = Func.NominalDiskon(RumusDiskon, Gross2);
            if (diskon == 0)
            {
                nilaiDiskon.Text = "0";
            }
            else
            {
                nilaiDiskon.Text = Cf.Num(Math.Round(diskon, 0).ToString());
            }
        }

        protected void jenisDiskon_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (jenisDiskon.SelectedIndex == 0)
            {
                divLumpSum.Visible = true;
                divPersenBertingkat.Visible = false;
            }
            else if (jenisDiskon.SelectedIndex == 1)
            {
                divLumpSum.Visible = false;
                divPersenBertingkat.Visible = true;
            }
        }
    }
}
