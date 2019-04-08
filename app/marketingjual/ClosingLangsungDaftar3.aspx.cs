using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class ClosingLangsungDaftar3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                InitForm();
                Fill();

                divPersenBertingkat.Visible = false;
                divLumpSum.Visible = true;

                nostock.Text = Db.SingleString(
                            "SELECT NoStock FROM MS_UNIT WHERE NoUnit = '" + Cf.Pk(unit.Text) + "'");
                tglKontrak.Text = Cf.Day(DateTime.Today);

                Surcharge.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                Surcharge.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                Surcharge.Attributes["onblur"] = "CalcBlur(this);";

                Pricelist.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                Pricelist.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                Pricelist.Attributes["onblur"] = "CalcBlur(this);";

                diskonLumpSum.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                diskonLumpSum.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                diskonLumpSum.Attributes["onblur"] = "CalcBlur(this);";

            }
        }

        private void InitForm()
        {
            //Sales
            DataTable rs = Db.Rs("SELECT Nama, Principal, NoAgent, Tipe FROM MS_AGENT WHERE Status = 'A'"
                + " ORDER BY Nama,NoAgent");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                if (rs.Rows[i]["Principal"].ToString() != "")
                {
                    t = t + " (" + rs.Rows[i]["Principal"] + ")";
                }
                t = t + " (" + rs.Rows[i]["Tipe"] + ")";
                agent.Items.Add(new ListItem(t, v));
            }

            //DataTable rs = Db.Rs("SELECT Nama,Principal,NoAgent FROM MS_AGENT WHERE Status = 'A'"
            //    + " ORDER BY Nama,NoAgent");
            //for (int i = 0; i < rs.Rows.Count; i++)
            //{
            //    string v = rs.Rows[i]["NoAgent"].ToString();
            //    string t = rs.Rows[i]["Nama"].ToString();
            //    if (rs.Rows[i]["Principal"].ToString() != "")
            //        t = t + " (" + rs.Rows[i]["Principal"] + ")";
            //    agent.Items.Add(new ListItem(t, v));
            //}

            //Cara bayar
            DataTable rs2 = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' ORDER BY Nama");
            skema.Items.Add(new ListItem("*** CUSTOMIZE / PENDING", "0")); //cara bayar customize

            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                string v = rs2.Rows[i]["Nomor"].ToString();
                string t = rs2.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                skema.Items.Add(new ListItem(t, v));


            }

        }

        private void Fill()
        {
            DataTable rs = Db.Rs("SELECT Luas,PriceList,NoUnit,Jenis,LuasSG FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                string unitpesanan = unit.Text = rs.Rows[0]["NoUnit"].ToString();
                decimal SG = Convert.ToDecimal(rs.Rows[0]["luassg"]);
                decimal PL = Convert.ToDecimal(rs.Rows[0]["PriceList"]);
                Pricelist.Text = Cf.Num(PL);
                nounit.Text = rs.Rows[0]["NoUnit"].ToString();
                nohold.Text = NoHold;
                customer.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer='" + NoCustomer + "'");
                int NoAgent = Db.SingleInteger("SELECT NoAgent FROM MS_HOLD WHERE NoHold='" + NoHold + "'");
                agent.SelectedValue = NoAgent.ToString();
                tipeunit.Text = Db.SingleString("SELECT Nama FROM REF_JENIS WHERE Jenis='" + rs.Rows[0]["Jenis"].ToString() + "'");

                //Fill Diskon dan Bunga
                FillDiskonDanBunga();
            }

            //Mengisi Lokasi
            rs = Db.Rs("SELECT * FROM REF_LOKASI_KONTRAK ORDER BY SN ASC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["SN"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                lokasikontrak.Items.Add(new ListItem(t, v));
            }

        }

        private void FillDiskonDanBunga()
        {
            string RumusDiskon = Db.SingleString(
                   "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
            string Ket = Db.SingleString(
                "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
            string CaraBayar = Db.SingleString(
                "SELECT Jenis FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
            string RumusBunga = Db.SingleString(
                "SELECT Bunga FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
            string Ket2 = Db.SingleString(
                "SELECT Bungaket FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);

            diskon2.Text = RumusDiskon;
            diskonket.Text = Ket;
            bunga2.Text = RumusBunga;
            bungaket.Text = Ket2;

            SetDiskon();
            SetBunga();
        }

        private bool csvalid()
        {
            bool x = true;

            try { int z = Convert.ToInt32(NoCustomer); }
            catch { x = false; }

            if (x)
            {
                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer
                    + " AND Status = 'A'");
                if (c == 0) x = false;
            }

            if (!x)
                Js.Alert(
                    this
                    , "Customer Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Customer tersebut tidak terdaftar.\\n"
                    + "2. Status customer tersebut adalah INAKTIF.\\n"
                    , "document.getElementById('nocustomer').focus();"
                    + "document.getElementById('nocustomer').select();"
                    );

            return x;
        }

        private void LoadCustomer()
        {
            if (csvalid())
            {
                frm.Visible = true;

                FillForm();


            }
        }

        private void FillForm()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_CUSTOMER WHERE NoCustomer ='" + NoCustomer + "'");

            string namacustomer = Db.SingleString(
                "SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer);
            customer.Text = "<a href=\"javascript:popEditCustomer('" + NoCustomer + "')\">" + namacustomer
                + " (" + Convert.ToInt32(NoCustomer).ToString().PadLeft(5, '0') + ")</a>";
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            if (Pricelist.Text == "0")
            {
                x = false;
                if (s == "") s = Pricelist.ID;
                pricec.Text = "Pricelist Tidak Boleh 0";
            }
            else
                pricec.Text = "";

            if (!Cf.isTgl(tglKontrak))
            {
                x = false;
                if (s == "") s = tglKontrak.ID;
                tglkontrakc.Text = "Tanggal";
            }
            else
                tglkontrakc.Text = "";

            if (!Cf.isMoney(Surcharge))
            {
                x = false;
                if (s == "") s = Surcharge.ID;
                surchargec.Text = "Angka";
            }
            else
                surchargec.Text = "";

            if (diskon2.Text != "")
            {
                if (!Cf.isMoney(diskon2))
                {
                    x = false;
                    if (s == "") s = diskon2.ID;
                    diskon2c.Text = "Angka";
                }
                else
                    diskon2c.Text = "";
            }
            if (skema.SelectedIndex == 0)
            {

                x = false;
                if (s == "") s = skema.ID;
                skemac.Text = "Pilih Skema Cara Bayar";

            }
            else
                skemac.Text = "";
            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Nilai Price List tidak boleh 0/Kosong.\\n"
                    + "3. Unit yang dipesan harus available dan tidak boleh kosong.\\n"
                    + "4. Jenis Tanggungan PPN harus dipilih.\\n"
                    + "5. Cara bayar harus dipilih.\\n"
                    + "6. Pilih Skema Cara Bayar.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }



        private void AutoID()
        {
            DateTime TglKontrak = Convert.ToDateTime(tglKontrak.Text);
            string[] j = (Cf.DaySlash1(tglKontrak.Text)).ToString().Split('/');
            string Tahunkontrak = j[2];
            string Tower = Db.SingleString("select Lokasi from ms_unit where nostock = '" + NoStock + "'");

            int c = Db.SingleInteger("SELECT COUNT(NoKontrak) FROM MS_KONTRAK");

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                nokontrak.Text = "SPU/" + Tower + "/" + Tahunkontrak + "/" + Cf.Roman(Convert.ToDateTime(tglKontrak.Text).Month) + "/" + c.ToString().PadLeft(5, '0');

                if (isUnique()) hasfound = true;
            }
        }
        protected void sifatppn_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            trppn.Visible = sifatppn.SelectedIndex == 1;
        }

        private bool isUnique()
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(NoKontrak) FROM MS_KONTRAK WHERE NoKontrak = '" + nokontrak.Text + "'");

            if (c == 0)
                return true;
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            decimal NilaiDiskon = Convert.ToDecimal(diskonLumpSum.Text);
            string SkemaCarabayar = Db.SingleString("SELECT Nama from REF_SKEMA WHERE nomor = " + skema.SelectedValue);
            if (valid())
            {
                Save();
            }

            //if (NilaiDiskon > 0)
            //{
            //    if (SkemaCarabayar.Contains("HARD CASH"))
            //    {
            //        if (NilaiDiskon > 0 && NilaiDiskon <= 5000000)
            //        {
            //            oto.Visible = true;
            //            lblotorisasi.Text = "Masukkan username pemberi otorisasi";
            //            ok.Enabled = false;
            //        }
            //        else
            //        {
            //            oto.Visible = true;
            //            lblotorisasi.Text = "Masukkan username pemberi otorisasi";
            //            ok.Enabled = false;
            //        }
            //    }
            //    else if (SkemaCarabayar.Contains("INSTALLMENT 12X"))
            //    {
            //        if (NilaiDiskon > 0 && NilaiDiskon <= 8000000)
            //        {
            //            oto.Visible = true;
            //            lblotorisasi.Text = "Masukkan username pemberi otorisasi";
            //            ok.Enabled = false;
            //        }
            //        else
            //        {
            //            oto.Visible = true;
            //            lblotorisasi.Text = "Masukkan username pemberi otorisasi";
            //            ok.Enabled = false;
            //        }
            //    }
            //    else
            //    {
            //        if (NilaiDiskon > 0 && NilaiDiskon <= 10000000)
            //        {
            //            oto.Visible = true;
            //            lblotorisasi.Text = "Masukkan username pemberi otorisasi";
            //            ok.Enabled = false;
            //        }
            //        else
            //        {
            //            oto.Visible = true;
            //            lblotorisasi.Text = "Masukkan username pemberi otorisasi";
            //            ok.Enabled = false;
            //        }
            //    }
            //}
            //else
            //{
            //    if (valid())
            //    {
            //        Save();
            //    }
            //}
        }

        private void SaveTagihan()
        {
            int CaraBayar = Convert.ToInt32(skema.SelectedValue);
            decimal PPN = 0, Netto = 0;
            if (CaraBayar != 0)
            {
                string RumusDiskon = diskon2.Text;
                string RumusDiskon2 = Db.SingleString(
                    "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + CaraBayar);
                string RumusBunga = bunga2.Text;
                string RumusBunga2 = "";

                decimal Gross = Db.SingleDecimal(
                    "SELECT isnull(sum(Gross),0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                decimal surcharge = Convert.ToDecimal(Surcharge.Text);
                decimal disawal = Db.SingleDecimal("SELECT isnull(sum(DiskonRupiah),0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                decimal bung = Db.SingleDecimal("SELECT isnull(sum(BungaNominal),0) FROM MS_KONTRAK WHERE  NoKontrak = '" + NoKontrak + "'");
                decimal Gross2 = Gross;

                decimal GrossAfterDiskon = Func.SetelahDiskon(RumusDiskon, Gross2);
                decimal GrossAfterBunga = Func.SetelahBunga(RumusBunga, Gross2);
                decimal HargaSetelahBunga = GrossAfterDiskon + bung;
                decimal hrgsetelahdisc = 0;
                Netto = (CaraBayar != 0) ? Func.SetelahDiskon(RumusDiskon, Gross) : Gross;

                /* DISKON TAMBAHAN SAAT CLOSING */
                decimal DiskonTambahan = 0;
                if (jenisDiskon.SelectedIndex == 0)
                {	//Diskon lum sum
                    DiskonTambahan = Convert.ToDecimal(diskonLumpSum.Text);
                }
                else if (jenisDiskon.SelectedIndex == 1)
                {	//Diskon % bertingkat
                    decimal coba = 0, totaldisc = 0;
                    string[] DiscTambahPersen = diskontambahPersen.Text.Split('+');
                    decimal dpp = Netto;

                    if (diskontambahPersen.Text != "")
                    {
                        for (int a = 0; a <= DiscTambahPersen.GetUpperBound(0); a++)
                        {
                            coba = Math.Round(Convert.ToDecimal(DiscTambahPersen[a]) * dpp / (decimal)100);
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

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET DiskonTambahan = " + DiskonTambahan
                    + " WHERE NoKontrak = '" + NoKontrak + "'");

                Netto -= DiskonTambahan;
                /********************************/

                decimal distambah = Db.SingleDecimal("SELECT isnull(sum(DiskonTambahan),0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                decimal afterDiscTambahan = HargaSetelahBunga - distambah;

                decimal NilaiKontrak = afterDiscTambahan;
                decimal DPP = Math.Round(NilaiKontrak / (decimal)1.1);
                decimal NilaiPPN = 0;
                decimal PPNDitanggungPemerintah = 0;

                if (JenisPPN.SelectedValue == "KONSUMEN")
                {
                    PPNDitanggungPemerintah = 0;
                    NilaiPPN = NilaiKontrak - DPP;
                }
                else
                {
                    PPNDitanggungPemerintah = DPP * (decimal)0.1;
                    NilaiPPN = 0;
                }

                Db.Execute("EXEC spKontrakDiskon"
                   + " '" + NoKontrak + "'"
                   + ", " + Gross
                   + ", " + NilaiKontrak
                   + ",'" + RumusDiskon + "'"
                   + ",'" + Cf.Str(RumusDiskon2) + "'"
                   );

                Db.Execute("UPDATE MS_KONTRAK SET DiskonRupiah = '" + nilaiDiskon.Text + "' "
                           + " WHERE NoKontrak = '" + NoKontrak + "'");

                Db.Execute("EXEC spKontrakBunga"
                   + " '" + NoKontrak + "'"
                   + ", " + Gross2
                   + ", " + NilaiKontrak
                   + ",'" + RumusBunga + "'"
                   + ",'" + Cf.Str(RumusBunga2) + "'"
                   );

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET "
                    + " NilaiDPP = " + DPP
                    + " ,NilaiPPN = " + NilaiPPN
                    + " WHERE NoKontrak = '" + NoKontrak + "'");

                decimal nilaitg = Db.SingleDecimal("select isnull(sum(nilaikontrak),0) from ms_kontrak WHERE NoKontrak = '" + NoKontrak + "'");
                foreach (var r in Func.ListTagihan(CaraBayar, nilaitg, Convert.ToDateTime(tglKontrak.Text)))
                {
                    if (!Response.IsClientConnected) break;

                    Db.Execute("EXEC spTagihanDaftar"
                        + " '" + NoKontrak + "'"
                        + ",'" + r.NamaTagihan + "'"
                        + ",'" + r.TglJt + "'"
                        + ",'" + r.NilaiTagihan + "'"
                        + ",'" + r.TipeTagihan + "'"
                        );

                    int NoUrut = Db.SingleInteger("SELECT TOP 1 NoUrut FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut DESC");
                    Db.Execute("UPDATE MS_TAGIHAN"
                        + " SET KPR = " + Cf.BoolToSql(r.isKPR)
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        + " AND NoUrut = " + NoUrut
                        );
                }

                //string[,] x = Func.Breakdown(CaraBayar, nilaitg, Convert.ToDateTime(tglKontrak.Text));
                //for (int i = 0; i <= x.GetUpperBound(0); i++)
                //{
                //    if (!Response.IsClientConnected) break;

                //    Db.Execute("EXEC spTagihanDaftar"
                //        + " '" + NoKontrak + "'"
                //        + ",'" + x[i, 2] + "'"
                //        + ",'" + Convert.ToDateTime(x[i, 3]) + "'"
                //        + ", " + Convert.ToDecimal(x[i, 4])
                //        + ",'" + x[i, 1] + "'"
                //        );

                //    int NoUrut = Db.SingleInteger("SELECT TOP 1 NoUrut FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut DESC");
                //    Db.Execute("UPDATE MS_TAGIHAN"
                //        + " SET KPR = " + x[i, 5]
                //        + " WHERE NoKontrak = '" + NoKontrak + "'"
                //        + " AND NoUrut = " + NoUrut
                //        );
                //}
            }
            else
            {
                string RumusBunga = bunga2.Text;
                string RumusBunga2 = "";

                decimal Gross = Db.SingleDecimal(
                    "SELECT isnull(sum(Gross),0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                decimal surcharge = Convert.ToDecimal(Surcharge.Text);
                decimal disawal = Db.SingleDecimal("SELECT isnull(sum(DiskonRupiah),0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                decimal bung = Db.SingleDecimal("SELECT isnull(sum(BungaNominal),0) FROM MS_KONTRAK WHERE  NoKontrak = '" + NoKontrak + "'");
                decimal Gross2 = Gross;

                decimal GrossAfterDiskon = Gross2;
                decimal GrossAfterBunga = Func.SetelahBunga(RumusBunga, Gross2);
                decimal HargaSetelahBunga = GrossAfterDiskon + bung;
                decimal NilaiPPN = 0;
                decimal NilaiKontrak = 0;
                decimal hrgsetelahdisc = 0;
                decimal PPNDitanggungPemerintah = 0;
                Netto = Gross;

                /* DISKON TAMBAHAN SAAT CLOSING */
                decimal DiskonTambahan = 0;
                if (jenisDiskon.SelectedIndex == 0)
                {	//Diskon lum sum
                    DiskonTambahan = Convert.ToDecimal(diskonLumpSum.Text);
                }
                else if (jenisDiskon.SelectedIndex == 1)
                {	//Diskon % bertingkat
                    decimal coba = 0, totaldisc = 0;
                    string[] DiscTambahPersen = diskontambahPersen.Text.Split('+');
                    decimal dpp = Netto;

                    if (diskontambahPersen.Text != "")
                    {
                        for (int a = 0; a <= DiscTambahPersen.GetUpperBound(0); a++)
                        {
                            coba = Math.Round(Convert.ToDecimal(DiscTambahPersen[a]) * dpp / (decimal)100);
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

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET DiskonTambahan = " + DiskonTambahan
                    + " WHERE NoKontrak = '" + NoKontrak + "'");

                Netto -= DiskonTambahan;
                /********************************/

                decimal distambah = Db.SingleDecimal("SELECT isnull(sum(DiskonTambahan),0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                decimal afterDiscTambahan = HargaSetelahBunga - distambah;
                decimal DPP = Math.Round(afterDiscTambahan / (decimal)1.1);

                if (JenisPPN.SelectedValue == "KONSUMEN")
                {
                    PPNDitanggungPemerintah = 0;
                    NilaiPPN = afterDiscTambahan - DPP;
                }
                else
                {
                    PPNDitanggungPemerintah = DPP * (decimal)0.1;
                    NilaiPPN = 0;
                }

                Db.Execute("EXEC spKontrakDiskon"
                   + " '" + NoKontrak + "'"
                   + ", " + Gross
                   + ", " + afterDiscTambahan
                   + ",''"
                   + ",''"
                   );

                Db.Execute("UPDATE MS_KONTRAK SET DiskonRupiah = '" + nilaiDiskon.Text + "' "
                           + " WHERE NoKontrak = '" + NoKontrak + "'");

                Db.Execute("EXEC spKontrakBunga"
                   + " '" + NoKontrak + "'"
                   + ", " + Gross2
                   + ", " + NilaiKontrak
                   + ",'" + RumusBunga + "'"
                   + ",'" + Cf.Str(RumusBunga2) + "'"
                   );

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET "
                    + " NilaiDPP = " + DPP
                    + " ,NilaiPPN = " + NilaiPPN
                    + " WHERE NoKontrak = '" + NoKontrak + "'");
            }
        }



        private void LogCs()
        {
            int NoCustomer = Db.SingleInteger(
                "SELECT TOP 1 NoCustomer FROM MS_CUSTOMER ORDER BY NoCustomer DESC");

            DataTable rs = Db.Rs("SELECT "
                + " NoCustomer AS [No. Customer]"
                + ",TipeCs AS [Tipe]"
                + ",Nama AS [Nama Lengkap]"
                + ",NamaBisnis AS [Nama Bisnis]"
                + ",JenisBisnis AS [Jenis Bisnis]"
                + ",MerekBisnis AS [Merek Bisnis]"
                + ",Agama AS [Agama]"
                + ",CONVERT(varchar, TglLahir, 106) AS [Tanggal Lahir]"
                + ",NoTelp AS [No. Telepon]"
                + ",NoHp AS [No. HP]"
                + ",NoKantor AS [No. Telepon Kantor]"
                + ",NoFax AS [No. Fax]"
                + ",Email AS [Alamat Email]"
                + ",Alamat1 AS [Alamat Surat Menyurat 1]"
                + ",Alamat2 AS [Alamat Surat Menyurat 2]"
                + ",Alamat3 AS [Alamat Surat Menyurat 3]"
                + ",Kantor1 AS [Alamat Kantor 1]"
                + ",Kantor2 AS [Alamat Kantor 2]"
                + ",Kantor3 AS [Alamat Kantor 3]"
                + ",NoKTP AS [No. KTP]"
                + ",KTP1 AS [KTP Alamat]"
                + ",KTP2 AS [KTP RT/RW]"
                + ",KTP3 AS [KTP Kecamatan]"
                + ",KTP4 AS [KTP Kotamadya]"
                + ",UnitLama AS [Unit Lama]"
                + ",LuasLama AS [Luas Unit Lama]"
                + ",TokoLama AS [Nama Toko Lama]"
                + ",ZoningLama AS [Zoning Lama]"
                + ",GedungLama AS [Gedung Lama]"
                + ",TeleponLama AS [Telepon Lama]"
                // + ",NamaOrangHub AS [Nama Orang yang Dihubungi]"
                // + ",Hubungan AS [Hubungan]"
                // + ",Telp AS [Telp]"
                // + ",HP AS [HP]"
                // + ",EmailHub AS [Email orang yang dihubungi]"
                + ",AkteLama AS [Akte Lama]"
                + ",Salutation"
                + " FROM MS_CUSTOMER"
                + " WHERE NoCustomer = " + NoCustomer
                );

            Db.Execute("EXEC spLogCustomer"
                + " 'DAFTAR'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + NoCustomer.ToString().PadLeft(5, '0') + "'"
                ); ;
        }

        protected void diskon2_TextChanged(object sender, EventArgs e)
        {
            if (skema.SelectedIndex > 0)
            {
                SetDiskon2();
            }
            diskon2.Focus();
        }

        protected void jenisDiskon_SelectedIndexChanged(object sender, System.EventArgs e)
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

        protected void bunga2_TextChanged(object sender, EventArgs e)
        {
            if (carabayar2.SelectedIndex > 0)
            {
                SetBunga2();
            }
        }

        protected void Surcharge_TextChanged(object sender, EventArgs e)
        {
            if (skema.SelectedIndex > 0)
            {
                SetDiskon2();
                SetBunga2();
            }
        }

        protected void skema_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skema.SelectedIndex > 0)
            {
                string RumusDiskon = Db.SingleString(
                    "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                string Ket = Db.SingleString(
                    "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                string CaraBayar = Db.SingleString(
                    "SELECT Jenis FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                string RumusBunga = Db.SingleString(
                    "SELECT Bunga FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                string Ket2 = Db.SingleString(
                    "SELECT Bungaket FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);

                carabayar2.SelectedValue = CaraBayar;
                carabayar2.Enabled = false;
                diskon2.Text = RumusDiskon;
                diskonket.Text = Ket;
                bunga2.Text = RumusBunga;
                bungaket.Text = Ket2;

                SetDiskon();
                SetBunga();
            }
        }

        private void CaraBayar()
        {
            string x = skema.SelectedValue;

            if (x.Contains("HARD") == true)
            {
                carabayar2.SelectedIndex = 0;
            }
            else if (skema.SelectedValue.Contains("SOFT") == true)
            {
                carabayar2.SelectedIndex = 1;
            }
            else if (skema.SelectedValue.Contains("INSTALLMENT") == true)
            {
                carabayar2.SelectedIndex = 2;
            }
            else if (skema.SelectedValue.Contains("KPR") == true)
            {
                carabayar2.SelectedIndex = 3;
            }
        }

        //DISKON
        private void SetDiskon()
        {
            decimal Gross = Convert.ToDecimal(Pricelist.Text);
            decimal surcharge = Convert.ToDecimal(Surcharge.Text);
            decimal Gross2 = Gross + surcharge;

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

        private void SetDiskon2()
        {
            decimal Gross = Convert.ToDecimal(Pricelist.Text);
            decimal surcharge = Convert.ToDecimal(Surcharge.Text);
            decimal Gross2 = Gross + surcharge;

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

        // BUNGA
        private void SetBunga()
        {
            decimal Gross = Convert.ToDecimal(Pricelist.Text);
            decimal surcharge = Convert.ToDecimal(Surcharge.Text);
            decimal Gross2 = Gross + surcharge;

            string RumusBunga = bunga2.Text;
            string[] x = RumusBunga.Split('+');

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
            bunga2.Text = sb.ToString();

            decimal bunga = Func.NominalBunga(RumusBunga, Gross2);
            if (bunga == 0)
            {
                nilaiBunga.Text = "";
            }
            else
            {
                nilaiBunga.Text = Cf.Num(Math.Round(bunga, 0));
            }

        }
        private static decimal RoundUp(decimal input)
        {
            string x = input.ToString();
            string[] arr = x.Split(new char[] { '.' });

            if (arr.Length > 1)
            {
                if (decimal.Parse(arr[1]) > 0)
                {
                    decimal dc = decimal.Parse(arr[0]) + 1;
                    return dc;
                }
                else
                {
                    return decimal.Parse(arr[0]);
                }
            }
            else
            {
                return input;
            }
        }
        private static decimal RoundThousand(decimal input)
        {
            if (input < 1000)
            {
                return 1000;
            }
            else
            {
                input = RoundUp(input);
                if ((input % 1000) > 0)
                {
                    input = (input - (input % 1000)) + 1000;
                }
                return input;
            }
        }
        private void SetBunga2()
        {
            decimal Gross = Convert.ToDecimal(Pricelist.Text);
            decimal surcharge = Convert.ToDecimal(Surcharge.Text);
            decimal Gross2 = Gross + surcharge;

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

            decimal bunga = Func.NominalBunga2(RumusBunga, Gross2);
            if (bunga == 0)
            {
                nilaiBunga.Text = "";
            }
            else
            {
                nilaiBunga.Text = Cf.Num(Math.Round(RoundThousand(bunga), 0));
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

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
            }
        }

        private string NoCustomer
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoCustomer"]);
            }
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }

        private string NoHold
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoHold"]);
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


        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClosingLangsungDaftar4.aspx?NoHold=" + NoHold);
        }

        private void Save()
        {
            if (valid())
            {
                AutoID();

                if (nilaiBunga.Text == "") nilaiBunga.Text = "0";

                string LokKontrak = lokasikontrak.SelectedValue;

                DateTime TglKontrak = Convert.ToDateTime(tglKontrak.Text);
                string Skema = Cf.Str(skema.SelectedItem.Text);
                int NoAgent = Convert.ToInt32(agent.SelectedValue);
                DateTime TargetST = TglKontrak.AddMonths(36);
                decimal surcharge = Convert.ToDecimal(Surcharge.Text);
                decimal pl = Convert.ToDecimal(Pricelist.Text);

                string Tujuan = ddlTujuan.SelectedValue;
                string SumberDana = ddlSumberDana.SelectedValue;
                string SumberDanaLainnya = "";
                if (lainnya.Text != "")
                    SumberDanaLainnya = lainnya.Text;
                string NoHold = Cf.Str(nohold.Text);

                string ReffAgent1 = "", ReffAgent2 = "";

                Db.Execute("EXEC spKontrakDaftar4"
                    + " '" + NoKontrak + "'"
                    + ",'" + NoStock + "'"
                    + ",'" + TglKontrak + "'"
                    + ",'" + Skema + "'"
                    + ",'" + TargetST + "'"
                    + ",'" + NoCustomer + "'"
                    + ",'" + NoAgent + "'"
                    + ", " + pl
                    );


                int KPR;
                if (carabayar2.SelectedValue == "KPR")
                {
                    KPR = 1;
                }
                else
                {
                    KPR = 0;
                }

                string sSQL = "UPDATE MS_KONTRAK"
                    + " SET JenisKPR = '" + KPR + "'"
                    + ", CaraBayar = '" + carabayar2.SelectedValue + "'"
                    + ", RefSkema = " + skema.SelectedValue
                    + ", jenisPPN = '" + JenisPPN.SelectedValue + "'"
                    + ", LokasiKontrak = " + LokKontrak
                    + ", BungaNominal = " + Math.Round(Convert.ToDecimal(nilaiBunga.Text))
                    + ", BungaPersen = '" + Convert.ToDecimal(bunga2.Text) + "'"
                    + ", SumberDana='" + SumberDana + "'"
                    + ", SumberDanaLainnya='" + Cf.Str(SumberDanaLainnya) + "'"
                    + ", TujuanKontrak = '" + Tujuan + "'"
                    + ", NoRefferatorAgent = '" + ReffAgent1 + "'"
                    + ", NoRefferatorCustomer = '" + ReffAgent2 + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    ;

                Db.Execute(sSQL);

                LogCs();

                SaveTagihan();

                DataTable rs = Db.Rs("SELECT "
                    + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                    + ",MS_KONTRAK.NoUnit AS [Unit]"
                    + ",MS_CUSTOMER.Nama AS [Customer]"
                    + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS [Agent]"
                    + ",CONVERT(varchar,MS_KONTRAK.TglKontrak,106) AS [Tanggal Kontrak]"
                    + ",MS_KONTRAK.NoStock AS [No. Stock]"
                    + ",MS_KONTRAK.Luas AS [Luas]"
                    + ",MS_KONTRAK.Gross AS [Nilai Gross]"
                    + ",MS_KONTRAK.DiskonRupiah AS [Diskon dalam Rupiah]"
                    + ",MS_KONTRAK.DiskonPersen AS [Diskon dalam Persen]"
                    + ",MS_KONTRAK.DiskonKet AS [Keterangan Diskon]"
                    + ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                    + ",MS_KONTRAK.BungaPersen AS [Bunga dalam Persen]"
                    + ",MS_KONTRAK.BungaNominal AS [Bunga dalam Rupiah]"
                    + ",MS_KONTRAK.Skema"
                    + ",MS_KONTRAK.DiskonTambahan"
                    + ",CONVERT(varchar,MS_KONTRAK.TargetST,106) AS [Jadwal Serah Terima]"
                    + ", MS_KONTRAK.JenisPPN AS [PPN Ditanggung]"
                    + ", CASE MS_KONTRAK.JenisKPR"
                    + "		WHEN 0 THEN 'KPR'"
                    + "		WHEN 1 THEN 'NON-KPR'"
                    + "	END AS [Jenis KPR]"
                    + ",MS_KONTRAK.SumberDana AS [Sumber Dana]"
                    + ",MS_KONTRAK.SumberDanaLainnya AS [Sumber Dana Lainnya]"
                    + ",MS_KONTRAK.TujuanKontrak AS [Tujuan Transaksi]"
                    + ",MS_KONTRAK.NUP"
                    + ",MS_KONTRAK.NoRefferatorAgent"
                    + ",MS_KONTRAK.NoRefferatorCustomer"
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

                Db.Execute("UPDATE MS_HOLD SET Status='C' , NoKontrak='" + NoKontrak + "' WHERE NoHold='" + NoHold + "'");
                DataTable rsHold = Db.Rs("SELECT A.NoHold,A.TglHold,A.TglHoldExpired,B.Nama,C.Nama,D.NoUnit FROM MS_HOLD A"
                        + " INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer"
                        + " INNER JOIN MS_AGENT C ON A.NoAgent = C.NoAgent"
                        + " INNER JOIN MS_UNIT D ON A.NoStock = D.NoStock WHERE A.NoHold='" + NoHold + "'"
                    );

                string Ket2 = Cf.LogCapture(rsHold)
                    + Environment.NewLine
                    + " Closing Unit : "
                    + Ket
                    ;

                Db.Execute("EXEC spLogHold"
                    + " 'CLOSE'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket2 + "'"
                    + ",'" + NoHold + "'"
                    );

                Db.Execute("EXEC spLogKontrak"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoKontrak + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("TabelStok4.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + 0);

            }
        }
    }
}
