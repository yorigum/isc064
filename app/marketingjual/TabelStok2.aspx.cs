//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
//// in file 'App_Code\Migrated\Stub_TabelStok2_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'TabelStok2.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ISC064.MARKETINGJUAL;

namespace ISC064.MARKETINGJUAL
{
    public partial class TabelStok2 : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton Button1;
        private string NoStock
        {
            get
            {
                return Db.SingleString("SELECT NoStock FROM MS_UNIT WHERE Nostock = '" + Request.QueryString["NoStock"] + "'");
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Func.UnitSedangClosing(NoStock);

            if (!Page.IsPostBack)
            {
                init();
                if (Request.QueryString["NoStock"] != null)
                {
                    nostock.Text = Request.QueryString["NoStock"];
                    Pricelist.Text = Cf.Num(Convert.ToString(Db.SingleDecimal("SELECT PriceList FROM ISC064_MARKETINGJUAL..MS_UNIT"
                            + " WHERE NoStock = '" + Cf.Pk(unit.Text) + "'")));

                    decimal HargaGimmick = Db.SingleDecimal("SELECT TambahanHargaGimmick FROM MS_UNIT WHERE NoStock = '" + Cf.Pk(unit.Text) + "' ");
                    decimal HargaLainLain = Db.SingleDecimal("SELECT TambahanHargaLainLain FROM MS_UNIT WHERE NoStock = '" + Cf.Pk(unit.Text) + "' ");

                    gimmick.Text = Cf.Num(HargaGimmick);
                    hargatambahan.Text = Cf.Num(HargaLainLain);
                    ilus.Attributes["onclick"] = "openPopUp('Ilustrasi.aspx?NoStock=" + nostock.Text + "')";
                    reserv.Attributes["onclick"] = "location.href='ReservasiDaftar2.aspx?NoStock=" + nostock.Text + "';";
                    closing.Attributes["onclick"] = "location.href='ClosingLangsungDaftar2.aspx?NoStock=" + nostock.Text + "';";
                }

                persentingkat2.Visible = false;
                lumsum2.Visible = true;

                dclosing.Visible = false;
                trppn.Visible = true;

                tglKontrak.Text = Cf.Day(DateTime.Today);
            }

            ilus.Visible = false;

        }



        private void init()
        {
            bindag();
            bindskema();

            //lsbunga.Visible = false;
            persenBunga.Visible = false;

            nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilai.Attributes["onblur"] = "CalcBlur(this);";

            netto.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            netto.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            netto.Attributes["onblur"] = "CalcBlur(this);";

            diskon.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            diskon.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            diskon.Attributes["onblur"] = "CalcBlur(this);";

            diskontambahan.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            diskontambahan.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            diskontambahan.Attributes["onblur"] = "CalcBlur(this);";

            diskonlamsam.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            diskonlamsam.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            diskonlamsam.Attributes["onblur"] = "CalcBlur(this);";

            gimmick.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            gimmick.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            gimmick.Attributes["onblur"] = "CalcBlur(this);";
            gimmick.Attributes["onblur"] = "kaliLuas(this, hargatambahan, Pricelist)";

            hargatambahan.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            hargatambahan.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            hargatambahan.Attributes["onblur"] = "CalcBlur(this);";
            hargatambahan.Attributes["onblur"] = "kaliLuas(gimmick, this, Pricelist)";

            lsbunga.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            lsbunga.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            lsbunga.Attributes["onblur"] = "CalcBlur(this);";

            fo.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            fo.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            fo.Attributes["onblur"] = "CalcBlur(this);";

            focounter.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            focounter.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            focounter.Attributes["onblur"] = "CalcBlur(this);";

            totalharga.Text = Cf.Num(Db.SingleDecimal("SELECT (Pricelist + TambahanHargaGimmick + TambahanHargaLainLain) FROM MS_UNIT WHERE Nostock = '" + Request.QueryString["NoStock"] + "'"));
        }

        private void bindag()
        {
            //Populate data agent
            DataTable rs = Db.Rs("SELECT Nama,Principal,NoAgent FROM MS_AGENT WHERE Status = 'A'"
                + " ORDER BY Nama,NoAgent");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                if (rs.Rows[i]["Principal"].ToString() != "")
                    t = t + " (" + rs.Rows[i]["Principal"] + ")";
                agent.Items.Add(new ListItem(t, v));
            }
        }

        private void bindskema()
        {
            //Cara bayar
            DataTable rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' ORDER BY Nama");
            skema.Items.Add(new ListItem("*** CUSTOMIZE / PENDING", "0")); //cara bayar customize

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Nomor"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                skema.Items.Add(new ListItem(t, v));
            }
        }

        private bool unitvalid()
        {
            string NoStock = Cf.Pk(nostock.Text);

            if (NoStock == "")
                return false;
            else
            {
                string Status = Db.SingleString("SELECT Status FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                if (Status == "B")
                    return false;
                else
                    return true;
            }
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

            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "";

            //Tambahan
            if (Cf.isEmpty(alamat1))
            {
                x = false;
                if (s == "") s = alamat1.ID;
                alamat1c.Text = "Kosong";
            }
            else
                alamat1c.Text = "";

            if (Cf.isEmpty(ktp1))
            {
                x = false;
                if (s == "") s = ktp1.ID;
                ktp1c.Text = "Kosong";
            }
            else
                ktp1c.Text = "";

            if (Cf.isEmpty(noktp))
            {
                x = false;
                if (s == "") s = noktp.ID;
                noktpc.Text = "Kosong";
            }
            else
                noktpc.Text = "";

            if (Cf.isEmpty(telp) && Cf.isEmpty(hp))
            {
                x = false;
                if (s == "") s = telp.ID;
                telpc.Text = "Kosong";
            }
            else
                telpc.Text = "";

            if (!unitvalid())
            {
                x = false;
                if (s == "") s = unit.ID;
                unitc.Text = "Tidak Available";
            }
            else
                unitc.Text = "";

            if (!Cf.isEmpty(netto))
            {
                if (!Cf.isMoney(netto))
                {
                    x = false;
                    if (s == "") s = netto.ID;
                    nettoc.Text = "Angka";
                }
                else
                {
                    nettoc.Text = "";
                }
            }
            else
            {
                nettoc.Text = "";
            }

            if (!Cf.isMoney(nilai))
            {
                x = false;
                if (s == "") s = nilai.ID;
                nilaic.Text = "Angka";
            }
            else
                nilaic.Text = "";

            if (!Cf.isMoney(gimmick))
            {
                x = false;
                if (s == "") s = gimmick.ID;
                gimmickc.Text = "Angka";
            }
            else
                gimmickc.Text = "";

            if (!Cf.isMoney(hargatambahan))
            {
                x = false;
                if (s == "") s = hargatambahan.ID;
                hargatambahanc.Text = "Angka";
            }
            else
                hargatambahanc.Text = "";

            if (!Cf.isMoney(diskontambahan))
            {
                x = false;
                if (s == "") s = diskontambahan.ID;
                diskontambahanc.Text = "Angka";
            }
            else
                diskontambahanc.Text = "";

            if (carabayar2.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = carabayar2.ID;
                carabayarc.Text = "Pilih salah satu jenis";
            }
            else
                carabayarc.Text = "";

            if (carabayar.SelectedValue == "BG")
            {
                nobg.Text = Cf.Pk(nobg.Text);
                if (Cf.isEmpty(nobg))
                {
                    x = false;
                    if (s == "") s = nobg.ID;
                    bgc.Text = "Kosong";
                }
                else
                {
                    if (!Cf.isTgl(tglbg))
                    {
                        x = false;
                        if (s == "") s = tglbg.ID;
                        bgc.Text = "Tanggal";
                    }
                    else
                        bgc.Text = "";
                }
            }

            if (JenisPPN.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = JenisPPN.ID;
                JenisPPNc.Text = "Pilih";
            }
            else
                JenisPPNc.Text = "";

            if (jenisdiskon.SelectedIndex == 0)
            {
                if (!Cf.isMoney(diskon))
                {
                    x = false;
                    if (s == "") s = diskon.ID;
                    diskonc.Text = "Angka";
                }
                else
                    diskonc.Text = "";
            }

            if (!Cf.isMoney(fo))
            {
                x = false;
                if (s == "") s = fo.ID;
                foc.Text = "Angka";
            }
            else
                foc.Text = "";

            if (!Cf.isTgl(tglKontrak))
            {
                x = false;
                if (s == "") s = tglKontrak.ID;
                tglkontrakc.Text = "Tanggal";
            }
            else
                tglkontrakc.Text = "";

            // Diskon Index 0 = Diskon LumSum
            int Autorisasi = Db.SingleInteger("SELECT DiscountAuthorized FROM MS_UNIT WHERE NoUnit = '" + unit.Text + "' ");
            if (Autorisasi == 0)
            {
                Decimal Lamsam = Convert.ToDecimal(diskonlamsam.Text);
                string DiskonPersen = Cf.Str(diskon3.Text);
                string DiskonKet = Cf.Str(diskonket2.Text);

                decimal BiayaGimmick = Convert.ToDecimal(gimmick.Text);
                decimal BiayaLainLain = Convert.ToDecimal(hargatambahan.Text);
                Decimal Gross = Db.SingleDecimal("SELECT PriceList FROM MS_UNIT WHERE NoUnit = '" + unit.Text + "' ");
                Gross += BiayaGimmick;
                Gross += BiayaLainLain;
                Decimal DiskonHitung = 0;
                Decimal DiskonTotal = 0;
                Decimal HargaSetelahDiskon = 0;
                Decimal BungaNominal = 0;
                Decimal BungaPersen = 0;
                Decimal NilaiDPP = 0;
                Decimal TotalBunga = 0;

                // Diskon Index 0 = Diskon LumSum
                if (jenisdiskon2.SelectedIndex == 0)
                {
                    DiskonTotal = Convert.ToDecimal(diskonlamsam.Text);
                }

                // Diskon Index 1 = Diskon %Bertingkat
                else if (jenisdiskon2.SelectedIndex == 1)
                {
                    string[] diskonpersen = diskon3.Text.Split('+');
                    for (int a = 0; a <= diskonpersen.GetUpperBound(0); a++)
                    {
                        // Cek jika ada pembulatan Diskon
                        if (bulat.Checked)
                        {
                            DiskonHitung = Math.Round(Convert.ToDecimal(diskonpersen[a]) * Gross) / (decimal)100;
                        }
                        else
                        {
                            DiskonHitung = Convert.ToDecimal(diskonpersen[a]) * Gross / (decimal)100;
                        }
                        DiskonTotal += DiskonHitung;
                    }
                }

                // merubah minus menjadi plus
                if (DiskonTotal < 0)
                {
                    DiskonTotal = DiskonTotal * -1;
                }

                HargaSetelahDiskon = Gross - DiskonTotal;

                if (rdBunga.SelectedIndex == 1)
                {
                    BungaPersen = Convert.ToDecimal(Cf.Num(persenBunga.Text));
                }
                else if (rdBunga.SelectedIndex == 0)
                {
                    BungaNominal = Convert.ToDecimal(lsbunga.Text);
                }

                // Bunga Index 0 = Nominal
                if (rdBunga.SelectedIndex == 0)
                {
                    TotalBunga = BungaNominal;
                }
                // Bunga Index 1 = Persen
                else if (rdBunga.SelectedIndex == 1)
                {
                    TotalBunga = (BungaPersen / (decimal)100) * Gross;
                }

                NilaiDPP = HargaSetelahDiskon + TotalBunga;

                decimal PriceListMinimum = Db.SingleDecimal("SELECT PriceListMin FROM MS_UNIT WHERE NoUnit = '" + unit.Text + "' ");
                if (NilaiDPP < PriceListMinimum)
                {
                    x = false;
                    diskonbaruc.Text = "Diskon Melebihi Pricelist minimum";
                }
                else
                    diskonbaruc.Text = "";
            }

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Nama tidak boleh kosong.\\n"
                    + "3. No. Telpon dan HP tidak boleh kosong.\\n"
                    + "4. Unit yang dipesan harus available dan tidak boleh kosong.\\n"
                    + "5. Nilai Booking Fee dan Nilai Netto harus berupa angka.\\n"
                    + "6. Khusus Cek Giro : No. BG tidak boleh kosong.\\n"
                    + "7. Jenis Tanggungan PPN harus dipilih.\\n"
                    + "8. Diskon melebihi nilai PriceList minimum.\\n"
                    + "9. Nilai Price List tidak boleh 0/Kosong."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private void AutoID()
        {
            int c = Db.SingleInteger("SELECT COUNT(NoKontrak) FROM MS_KONTRAK");

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                nokontrak.Text = c.ToString().PadLeft(7, '0');

                if (isUnique()) hasfound = true;
            }
        }

        private bool isUnique()
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(NoKontrak) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            if (c == 0)
                return true;
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                AutoID();

                string Nama = Cf.Str(nama.Text);
                string KTP1 = Cf.Str(ktp1.Text);
                string KTP2 = Cf.Str(ktp2.Text);
                string KTP3 = Cf.Str(ktp3.Text);
                string KTP4 = Cf.Str(ktp4.Text);
                string NoKTP = Cf.Str(noktp.Text);
                string NoTelp = Cf.Str(telp.Text);
                string NoHp = Cf.Str(hp.Text);
                string NPWP = Cf.Str(npwp.Text);
                DateTime TanggalKontrak = Convert.ToDateTime(tglKontrak.Text);

                string NoStock = Cf.Pk(nostock.Text);
                string Skema = Cf.Str(skema.SelectedItem.Text);

                int NoAgent = Convert.ToInt32(agent.SelectedValue);

                Db.Execute("EXEC spReservasiLaunching"
                    + " '" + Nama + "'"
                    + ",'" + KTP1 + "'"
                    + ",'" + KTP2 + "'"
                    + ",'" + KTP3 + "'"
                    + ",'" + KTP4 + "'"
                    + ",'" + NoKTP + "'"
                    + ",'" + NoTelp + "'"
                    + ",'" + NoHp + "'"
                    + ",'" + NoStock + "'"
                    + ",'" + Skema + "'"
                    + ", " + NoAgent
                    + ",'" + NoKontrak + "'"
                    );
                int NoCustomer = Db.SingleInteger(
                    "SELECT TOP 1 NoCustomer FROM MS_CUSTOMER ORDER BY NoCustomer DESC");

                Db.Execute("UPDATE MS_CUSTOMER SET "
                    + " Alamat1 = '" + Cf.Str(alamat1.Text) + "'"
                    + ",Alamat2 = '" + Cf.Str(alamat2.Text) + "'"
                    + ",Alamat3 = '" + Cf.Str(alamat3.Text) + "'"
                    + ",NPWP = '" + NPWP + "'"
                    + " WHERE NoCustomer = " + NoCustomer);

                int kpr = 0;
                if (carabayar2.SelectedValue == "KPR")
                {
                    kpr = 0;
                }
                else
                {
                    kpr = 1;
                }
                //Manual update
                string sSQL = "UPDATE MS_KONTRAK"
                    + " SET JenisPPN = '" + JenisPPN.SelectedItem.Text + "'"
                    + ", JenisKPR = " + kpr
                    + ", CaraBayar = '" + carabayar2.SelectedValue + "'"
                    + ", TglKontrak ='" + TanggalKontrak + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    ;
                Db.Execute(sSQL);

                Db.Execute("UPDATE MS_KONTRAK SET Note = '" + note.Text + "' WHERE NoKontrak = '" + NoKontrak + "' ");

                LogCs();

                SaveTagihan2();


                int Count = Db.SingleInteger("SELECT COUNT(*) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");
                if (Count != 0)
                {
                    SaveFO();
                }

                //decimal disc = Db.SingleDecimal("SELECT DiskonRupiah FROM MS_KONTRAK WHERE NoKontrak = '"+NoKontrak+"'");
                decimal disc = 0;
                if (jenisdiskon.SelectedIndex == 0)
                {
                    disc = Convert.ToDecimal(diskon.Text);
                    if (disc > 0)
                    {
                        Db.Execute("UPDATE MS_KONTRAK SET DiskonRupiah = " + disc + "WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                }
                else
                {
                    decimal gross = Db.SingleDecimal("SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    decimal gross2 = Db.SingleDecimal("SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    string[] disc2 = diskon2.Text.Split('+');
                    for (int i = 0; i < disc2.Length; i++)
                    {
                        //						gross = gross - (gross * (Convert.ToDecimal(disc2[i]) * (decimal)0.01));												
                    }
                    //					decimal potongan = gross2 - gross;
                    //					Db.Execute("UPDATE MS_KONTRAK SET DiskonRupiah = " + potongan + ", DiskonPersen = '" + diskon2.Text + "' WHERE NoKontrak = '"+NoKontrak+"'");
                }

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
                    + ",MS_KONTRAK.Skema"
                    + ",CONVERT(varchar,MS_KONTRAK.TargetST,106) AS [Jadwal Serah Terima]"
                    + ", MS_KONTRAK.JenisPPN AS [PPN Ditanggung]"
                    + ", CASE MS_KONTRAK.JenisKPR"
                    + "		WHEN 0 THEN 'KPR'"
                    + "		WHEN 1 THEN 'NON-KPR'"
                    + "	END AS [Jenis KPR]"
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
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                //floor plan
                string Peta = Db.SingleString("SELECT Peta "
                    + " FROM MS_UNIT INNER JOIN MS_KONTRAK ON MS_UNIT.NoStock = MS_KONTRAK.NoStock "
                    + " WHERE NoKontrak = '" + NoKontrak + "'");
                Func.GenerateFP(Peta);

                //TTS
                int NoTTS = 0;
                if (Convert.ToDecimal(nilai.Text) != 0)
                {
                    NoTTS = SaveTTS(NoKontrak
                        , Db.SingleInteger("SELECT NoCustomer FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'"));
                }

                //Func.GenerateKomisi(NoKontrak, special);

                Response.Redirect("TabelStok3.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS);
            }
        }

        private void SaveFO()
        {
            //int CaraBayar = Convert.ToInt32(carabayar.SelectedValue);

            decimal FONominal = Convert.ToDecimal(fo.Text);

            int FOCounter = Convert.ToInt32(focounter.Text);

            int count = 1;

            int NoUrut = Db.SingleInteger("SELECT ISNULL(MAX(NoUrut),0) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");
            DateTime TglJT = Db.SingleTime("SELECT TglJT FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoUrut + " ");

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

        private void SaveTagihan2()
        {
            int CaraBayar = Convert.ToInt32(skema.SelectedValue);

            Decimal Lamsam = Convert.ToDecimal(diskonlamsam.Text);
            string DiskonPersen = Cf.Str(diskon3.Text);
            string DiskonKet = Cf.Str(diskonket2.Text);

            decimal BiayaGimmick = Convert.ToDecimal(gimmick.Text);
            decimal BiayaLainLain = Convert.ToDecimal(hargatambahan.Text);
            Decimal Gross = Db.SingleDecimal(
                    "SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Gross += BiayaGimmick;
            Gross += BiayaLainLain;
            Decimal DiskonHitung = 0;
            Decimal DiskonTotal = 0;
            Decimal HargaSetelahDiskon = 0;
            Decimal BungaNominal = 0;
            Decimal BungaPersen = 0;
            Decimal NilaiDPP = 0;
            Decimal NilaiPPN = 0;
            Decimal NilaiKontrak = 0;
            Decimal TotalBunga = 0;

            // Diskon Index 0 = Diskon LumSum
            if (jenisdiskon2.SelectedIndex == 0)
            {
                DiskonTotal = Math.Round(Convert.ToDecimal(diskonlamsam.Text));
            }
            // Diskon Index 1 = Diskon %Bertingkat
            else if (jenisdiskon2.SelectedIndex == 1)
            {
                string[] diskonpersen = diskon3.Text.Split('+');
                for (int a = 0; a <= diskonpersen.GetUpperBound(0); a++)
                {
                    // Cek jika ada pembulatan Diskon
                    if (bulat.Checked)
                    {
                        DiskonHitung = Math.Round(Convert.ToDecimal(diskonpersen[a]) * Gross / (decimal)100);
                    }
                    else
                    {
                        DiskonHitung = Convert.ToDecimal(diskonpersen[a]) * Gross / (decimal)100;
                    }
                    DiskonTotal += DiskonHitung;
                }
            }

            // merubah minus menjadi plus
            if (DiskonTotal < 0)
            {
                DiskonTotal = DiskonTotal * -1;
            }

            HargaSetelahDiskon = Gross - DiskonTotal;

            decimal DiskonTambahan = Convert.ToDecimal(diskontambahan.Text);

            HargaSetelahDiskon = HargaSetelahDiskon - DiskonTambahan;

            if (rdBunga.SelectedIndex == 1)
            {
                BungaPersen = Convert.ToDecimal(Cf.Num(persenBunga.Text));
            }
            else if (rdBunga.SelectedIndex == 0)
            {
                BungaNominal = Math.Round(Convert.ToDecimal(lsbunga.Text));
            }

            // Bunga Index 0 = Nominal
            if (rdBunga.SelectedIndex == 0)
            {
                TotalBunga = BungaNominal;
            }
            // Bunga Index 1 = Persen
            else if (rdBunga.SelectedIndex == 1)
            {
                TotalBunga = Math.Round((BungaPersen / (decimal)100) * Gross);
            }

            //NilaiDPP = HargaSetelahDiskon + TotalBunga;
            //NilaiPPN = Math.Round((Decimal)0.1 * NilaiDPP);
            //NilaiKontrak = NilaiDPP + NilaiPPN;

            string NoStock = Db.SingleString("SELECT NoStock FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' ");


            NilaiKontrak = HargaSetelahDiskon + TotalBunga;
            NilaiDPP = Math.Round(Convert.ToDecimal(Convert.ToDouble(NilaiKontrak) / 1.1));
            NilaiPPN = Math.Round((Decimal)0.1 * NilaiDPP);

            // Update 1
            Db.Execute(" UPDATE MS_KONTRAK "
                + " SET NilaiPPN = " + NilaiPPN
                + ", NilaiDPP = " + NilaiDPP
                + ", BungaNominal = " + TotalBunga
                + ", BungaPersen = " + BungaPersen
                + ", HargaGimmick = " + BiayaGimmick
                + ", HargaLainLain = " + BiayaLainLain
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            // Update 2 (Default SP)
            Db.Execute("EXEC spKontrakDiskon"
                + " '" + NoKontrak + "'"
                + ", " + Gross
                + ", " + NilaiKontrak
                + ", " + 0
                + ",''"
                + ",''"
                );

            // Update 3
            Db.Execute("UPDATE MS_KONTRAK"
                + " SET DiskonPersen = '" + DiskonPersen + "' "
                + ", DiskonKet = '" + DiskonKet + "' "
                + ", DiskonRupiah = " + DiskonTotal
                 + ", DiskonTambahan = " + DiskonTambahan
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            // Jika Cara Bayar bukan Customize/Pending
            if (CaraBayar != 0)
            {
                // Untuk Breakdown Tagihan (Insert MS_TAGIHAN)
                string[,] x = Func.Breakdown(CaraBayar, Convert.ToDecimal(NilaiKontrak), Convert.ToDateTime(tglKontrak.Text));
                for (int i = 0; i <= x.GetUpperBound(0); i++)
                {
                    if (!Response.IsClientConnected) break;

                    Db.Execute("EXEC spTagihanDaftar "
                        + " '" + NoKontrak + "' "
                        + ",'" + x[i, 2] + "' "
                        + ",'" + Convert.ToDateTime(x[i, 3]) + "' "
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
            }
        }

        private void SaveTagihan()
        {
            int CaraBayar = Convert.ToInt32(skema.SelectedValue);

            decimal manualNK = Db.SingleDecimal("SELECT NilaiKontrak FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            decimal bunga = Convert.ToDecimal(lsbunga.Text);

            //cara bayar 0 = customize
            if (CaraBayar != 0)
            {
                string RumusDiskon = Db.SingleString(
                    "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + CaraBayar);

                string RumusDiskon2 = Db.SingleString(
                    "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + CaraBayar);

                decimal Gross = Db.SingleDecimal(
                    "SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                decimal Netto = 0, NilaiPPN = 0, DiskonRupiah = 0;
                if (!Cf.isEmpty(netto))
                {
                    Netto = Convert.ToDecimal(netto.Text);

                    if (sifatppn.SelectedIndex == 1)
                    {
                        if (includeppn.Checked)
                        {
                            if (roundppn.Checked)
                                NilaiPPN = Netto - Math.Round(Netto / (decimal)1.1);
                            else
                                NilaiPPN = Netto - Netto / (decimal)1.1;
                        }
                        else
                        {
                            if (roundppn.Checked)
                                NilaiPPN = Math.Round((decimal)0.1 * Netto);
                            else
                                NilaiPPN = (decimal)0.1 * Netto;

                            Netto += NilaiPPN;
                            DiskonRupiah = Gross + NilaiPPN - Netto;
                        }
                    }

                    //Netto = 
                    if (sifatppn.SelectedIndex == 1)
                    {
                        if (includeppn.Checked)
                            NilaiPPN = 0;
                        else
                        {
                            NilaiPPN = Netto / 10;
                            if (roundppn.Checked)
                                NilaiPPN = Math.Round(Netto / 10);
                        }
                    }
                    //									
                    //					Netto += NilaiPPN;
                    //Db.Execute("UPDATE MS_KONTRAK SET NilaiKontrak = " + Netto + " WHERE NoKontrak = '"+ NoKontrak +"'");


                    Db.Execute("EXEC spKontrakDiskon"
                        + " '" + NoKontrak + "'"
                        + ", " + Gross
                        + ", " + Netto
                        + ",0"
                        + ",''"
                        + ",''"
                        );
                }
                else
                {
                    Netto = Func.SetelahDiskon(RumusDiskon, Gross);

                    if (sifatppn.SelectedIndex == 1)
                    {
                        if (includeppn.Checked)
                        {
                            if (roundppn.Checked)
                                NilaiPPN = Netto - Math.Round(Netto / (decimal)1.1);
                            else
                                NilaiPPN = Netto - Netto / (decimal)1.1;
                        }
                        else
                        {
                            //							NilaiPPN = 0;
                            if (roundppn.Checked)
                            {
                                NilaiPPN = Math.Round((decimal)0.1 * Netto);
                            }
                            else
                            {
                                NilaiPPN = (decimal)0.1 * Netto;
                            }

                            Netto += NilaiPPN;
                            DiskonRupiah = Gross + NilaiPPN - Netto;
                        }
                    }

                    Db.Execute("EXEC spKontrakDiskon"
                        + " '" + NoKontrak + "'"
                        + ", " + Gross
                        + ", " + Netto
                        + ", " + DiskonRupiah
                        + ",'" + RumusDiskon + "'"
                        + ",'" + Cf.Str(RumusDiskon2) + "'"
                        );
                }

                //update manual nilaikontrak (overwrite)
                //decimal manualNK = Gross - Convert.ToDecimal(diskon.Text);
                if (diskon.Text != "")
                {
                    manualNK = Gross - Convert.ToDecimal(diskon.Text);
                    Netto = manualNK;
                    Db.Execute("UPDATE MS_KONTRAK SET NilaiKontrak = " + manualNK + " WHERE NoKontrak = '" + NoKontrak + "'");
                }

                //update bunga lum sum manual (overwrite)
                //decimal bunga = Convert.ToDecimal(lsbunga.Text); 
                if (lsbunga.Text != "")
                {
                    decimal manualBunga = manualNK + bunga;
                    Netto = manualNK + Convert.ToDecimal(lsbunga.Text);
                    Db.Execute("UPDATE MS_KONTRAK SET NilaiKontrak = " + manualBunga + ", BungaNominal = " + bunga + " WHERE NoKontrak = '" + NoKontrak + "'");
                }

                NilaiPPN = 0;
                if (sifatppn.SelectedIndex == 1)
                {
                    NilaiPPN = Math.Round(Netto / 10);
                    if (!includeppn.Checked)
                    {
                        Netto += NilaiPPN;
                    }
                    else
                    {
                        NilaiPPN = Math.Round(Netto / (decimal)1.1 / (decimal)10);
                    }
                }

                Db.Execute("UPDATE MS_KONTRAK SET NilaiKontrak = " + Netto + " WHERE NoKontrak = '" + NoKontrak + "'");

                //Update manual
                string addSql = (!includeppn.Checked) ? (", DiskonRupiah = " + DiskonRupiah) : "";
                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET PPN = " + (includeppn.Checked ? "1" : "0") //((sifatppn.SelectedIndex == 1) ? "1" : "0")
                    + ", NilaiPPN = " + NilaiPPN
                    + addSql
                    + " WHERE NoKontrak = '" + NoKontrak + "'");

                string[,] x = Func.Breakdown(CaraBayar, Netto, DateTime.Today);
                for (int i = 0; i <= x.GetUpperBound(0); i++)
                {
                    if (!Response.IsClientConnected) break;

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
            }
            else
            {
                decimal Gross = Db.SingleDecimal(
                    "SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                decimal Netto = 0, NilaiPPN = 0, DiskonRupiah = 0;

                Netto = Convert.ToDecimal(Pricelist.Text);

                if (sifatppn.SelectedIndex == 1)
                {
                    if (includeppn.Checked)
                    {
                        if (roundppn.Checked)
                            NilaiPPN = Netto - Math.Round(Netto / (decimal)1.1);
                        else
                            NilaiPPN = Netto - Netto / (decimal)1.1;
                    }
                    else
                    {
                        //NilaiPPN = 0;
                        if (roundppn.Checked)
                            NilaiPPN = Math.Round((decimal)0.1 * Netto);
                        else
                            NilaiPPN = (decimal)0.1 * Netto;

                        Netto += NilaiPPN;
                    }
                }

                Db.Execute("EXEC spKontrakDiskon"
                    + " '" + NoKontrak + "'"
                    + ", " + Gross
                    + ", " + Netto
                    + ", 0"
                    + ",''"
                    + ",''"
                    );

                //update manual nilaikontrak
                if (diskon.Text != "")
                {
                    manualNK = Gross - Convert.ToDecimal(diskon.Text);
                    Netto = manualNK;
                    Db.Execute("UPDATE MS_KONTRAK SET NilaiKontrak = " + manualNK + " WHERE NoKontrak = '" + NoKontrak + "'");
                }

                //update bunga lum sum manual
                if (lsbunga.Text != "")
                {
                    decimal manualBunga = manualNK + bunga;
                    Netto = manualNK + Convert.ToDecimal(lsbunga.Text);
                    Db.Execute("UPDATE MS_KONTRAK SET NilaiKontrak = " + manualBunga + ", BungaNominal = " + bunga + " WHERE NoKontrak = '" + NoKontrak + "'");
                }

                NilaiPPN = 0;
                if (sifatppn.SelectedIndex == 1)
                {
                    NilaiPPN = Math.Round(Netto / 10);
                    if (!includeppn.Checked)
                    {
                        Netto += NilaiPPN;
                    }
                    else
                    {
                        NilaiPPN = Math.Round(Netto / (decimal)1.1 / (decimal)10);
                    }
                }
                Db.Execute("UPDATE MS_KONTRAK SET NilaiKontrak = " + Netto + " WHERE NoKontrak = '" + NoKontrak + "'");

                //Update manual
                string addSql = (!includeppn.Checked) ? (", DiskonRupiah = " + DiskonRupiah) : "";
                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET PPN = " + (includeppn.Checked ? "1" : "0") //((sifatppn.SelectedIndex == 1) ? "1" : "0")
                    + ", NilaiPPN = " + NilaiPPN
                    + addSql
                    + " WHERE NoKontrak = '" + NoKontrak + "'");

            }

            #region Edit nilai booking fee otomatis terhadap nilai tagihan
            //			decimal NilaiBF = Convert.ToDecimal(nilai.Text);
            //			decimal NilaiTag = Db.SingleDecimal(
            //				"SELECT NilaiTagihan FROM MS_TAGIHAN WHERE NoKontrak = '"+NoKontrak+"' AND NoUrut = 1");
            //			decimal LebihBayar = NilaiBF - NilaiTag;
            //			
            //			if(LebihBayar!=0)
            //			{
            //				Db.Execute(
            //					"UPDATE MS_TAGIHAN SET NilaiTagihan = NilaiTagihan + " + LebihBayar
            //					+ " WHERE NoKontrak = '"+NoKontrak+"' AND NoUrut = 1");
            //				
            //				int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_TAGIHAN WHERE NoKontrak = '"+NoKontrak+"'");
            //				if(c>1)
            //				{
            //					Db.Execute(
            //						"UPDATE MS_TAGIHAN SET NilaiTagihan = NilaiTagihan - " + LebihBayar
            //						+ " WHERE NoKontrak = '"+NoKontrak+"' AND NoUrut = 2");
            //				}
            //			}
            #endregion

        }

        private int SaveTTS(string NoKontrak, int NoCustomer)
        {
            DateTime TglTTS = DateTime.Today;
            string Unit = Cf.Str(unit.Text);
            string Customer = Cf.Str(Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer));
            string CaraBayar = carabayar.SelectedValue;
            decimal Nilai = Convert.ToDecimal(nilai.Text);
            string Ket = Cf.Str(kettts.Text);

            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSRegistrasi"
                + " '" + TglTTS + "'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'JUAL'"
                + ",'" + NoKontrak + "'"
                + ",'" + Unit + "'"
                + ",'" + Customer + "'"
                + ",'" + CaraBayar + "'"
                + ",'" + Ket + "'"
                );


            int NoTTS = Db.SingleInteger("SELECT TOP 1 NoTTS FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS ORDER BY NoTTS DESC");

            //khusus cek giro
            if (carabayar.SelectedValue == "BG")
            {
                string NoBG = Cf.Pk(nobg.Text);
                DateTime TglBG = Convert.ToDateTime(tglbg.Text);

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSRegistrasiBG"
                    + " '" + NoTTS + "'"
                    + ",'" + NoBG + "'"
                    + ",'" + TglBG + "'"
                    );
            }

            DataTable rsTagihan = Db.Rs("SELECT * FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = 1");
            System.Text.StringBuilder alokasi = new System.Text.StringBuilder();
            if (rsTagihan.Rows.Count != 0)
            {
                int NoTagihan = (int)rsTagihan.Rows[0]["NoUrut"];
                string NamaTagihan = Cf.Str(rsTagihan.Rows[0]["NamaTagihan"])
                    + " (" + rsTagihan.Rows[0]["Tipe"] + ")";

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSAlokasi "
                    + "  " + NoTTS
                    + ", " + NoTagihan
                    + ", " + Nilai
                    );

                alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");
            }

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
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

            string KetLog = Cf.LogCapture(rs)
                + "<br>***ALOKASI PEMBAYARAN:<br>"
                + alokasi.ToString();

            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogTTS"
                + " 'REGIS'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetLog + "'"
                + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "')");
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            return NoTTS;
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
                );
        }

        // protected void closing_ServerClick(object sender, System.EventArgs e)
        // {
        // if(dclosing.Visible)
        // {
        // dclosing.Visible = false;
        // }
        // else
        // {
        // dclosing.Visible = true;
        // Js.Focus(this, nama);
        // }
        // }

        protected void skema_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (skema.SelectedIndex == 0)
            {
                nilai.ReadOnly = true;
                nilai.Text = "0";
                carabayar2.Enabled = true; ;
            }
            else
            {
                nilai.ReadOnly = false;
                carabayar2.Enabled = false;
                string JenisSkema = Db.SingleString("SELECT Jenis FROM REF_SKEMA WHERE Nomor = '" + skema.SelectedValue + "' ");

                carabayar2.SelectedValue = JenisSkema;
            }
            Js.Focus(this, skema);
        }

        protected void sifatppn_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            trppn.Visible = sifatppn.SelectedIndex == 1;
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
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

        protected void rdBunga_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (rdBunga.SelectedIndex == 1)
            {
                persenBunga.Visible = true;
                lsbunga.Visible = false;
            }
            else
            {
                persenBunga.Visible = false;
                lsbunga.Visible = true;
            }
        }

        protected void jenisdiskon_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (jenisdiskon2.SelectedIndex == 0)
            {
                lumsum2.Visible = true;
                persentingkat2.Visible = false;
            }
            else if (jenisdiskon2.SelectedIndex == 1)
            {
                lumsum2.Visible = false;
                persentingkat2.Visible = true;
            }
        }

        protected void includeppn_CheckedChanged(object sender, System.EventArgs e)
        {

        }

    }
}
