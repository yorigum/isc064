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

namespace ISC064.MARKETINGJUAL
{
    public partial class ClosingLangsungDaftar : System.Web.UI.Page
    {
        string JenisProperti;

        protected void Page_Load(object sender, System.EventArgs e)
        {

            Act.Pass();
            Act.NoCache();
            kora.Visible = false;
            kori.Visible = false;

            if (!Page.IsPostBack)
            {
                //frm.Visible = false;
                //frm.Visible = divPersenBertingkat.Visible = false;

                divPersenBertingkat.Visible = false;
                divLumpSum.Visible = true;
                CaraBayar();

                InitForm();
                if (Request.QueryString["NoUnit"] != null)
                {
                    unit.Text = Request.QueryString["NoUnit"];
                    nostock.Text = NoStock;


                    Pricelist.Text = Cf.Num(Convert.ToString(Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
                            + " WHERE NoUnit = '" + Cf.Pk(unit.Text) + "'")));

                    ilus.Attributes["onclick"] = "openPopUp('Ilustrasi.aspx?NoStock=" + nostock.Text + "')";
                    reserv.Attributes["onclick"] = "location.href='ReservasiDaftar2.aspx?NoStock=" + nostock.Text + "';";
                    closing.Attributes["onclick"] = "location.href='ClosingLangsungDaftar2.aspx?NoStock=" + nostock.Text + "';";
                }
                unit.Text = Db.SingleString(
                            "SELECT NoUnit FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                //dclosing.Visible = false;
                Pricelist.Text = Cf.Num(Convert.ToString(Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
                            + " WHERE NoStock = '" + NoStock + "'")));
                trppn.Visible = true;
                tglKontrak.Text = Cf.Day(DateTime.Today);
            }

            ilus.Visible = false;
            persentingkat.Visible = true;
            // JenisProperti = Db.SingleString("SELECT JenisProperti FROM MS_UNIT WHERE NoStock='" + NoStock + "'");
            //persentingkat.Visible = jenisdiskon.SelectedIndex == 1;
            //lumsum.Visible = jenisdiskon.SelectedIndex == 0;           
            //includeppn.Checked = false;
            //Js.Confirm(this, "Lanjutkan dengan proses reservasi?");
        }

        protected void GantiTipeSales(object sender, System.EventArgs e)
        {
            string Tipe = Db.SingleString("SELECT Tipe FROM MS_AGENT WHERE NoAgent = '" + agent.SelectedValue + "'");
            reff.Visible = Tipe == "Referral" ? true : false;
        }

        private void InitForm()
        {
            //Sales
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

            rs = Db.Rs("SELECT Nama,NoAgent FROM MS_AGENT WHERE Status = 'A' AND Tipe = 'Referral'"
                + " ORDER BY Nama,NoAgent");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"] + ";EMPLOYEE";
                string t = rs.Rows[i]["Nama"].ToString() + " (EMPLOYEE)";
                agentreff.Items.Add(new ListItem(t, v));
            }

            rs = Db.Rs("SELECT Nama,NoCustomer FROM MS_CUSTOMER WHERE Status = 'A'"// AND Referral = 1"
                + " ORDER BY Nama,NoCustomer");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoCustomer"] + ";BUYER";
                string t = rs.Rows[i]["Nama"].ToString() + " (BUYER)";
                agentreff.Items.Add(new ListItem(t, v));
            }

            //persentingkat.Visible = false;
            //persenBunga.Visible = false;


            //Cara bayar
            DataTable rs2 = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' ORDER BY Nama");
            skema.Items.Add(new ListItem("*** CUSTOMIZE / PENDING", "0")); //cara bayar customize

            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                string v = rs2.Rows[i]["Nomor"].ToString();
                string t = rs2.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                skema.Items.Add(new ListItem(t, v));


            }
            carabayar.SelectedIndex = 0;
            carabayar.Attributes["ondblclick"] = "kalk(this)";

            //Fill Acc
            rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                ddlAcc.Items.Add(new ListItem(t, v));
            }

            string Tipe = Db.SingleString("SELECT Kategori FROM MS_UNIT WHERE NoUnit = '" + unit.Text + "'");
            Response.Write(Tipe);
            if (Tipe == "FLPP")
            {
                sifatppn.SelectedValue = "False";
            }
            else if (Tipe == "REAL ESTATE")
            {
                sifatppn.SelectedValue = "True";
            }
            else if (Tipe == "KOMERSIL")
            {
                sifatppn.SelectedValue = "True";
            }


            fo.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            fo.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            fo.Attributes["onblur"] = "CalcBlur(this);";

            focounter.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            focounter.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            focounter.Attributes["onblur"] = "CalcBlur(this);";

            nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilai.Attributes["onblur"] = "CalcBlur(this);";

            Pricelist.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            Pricelist.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            Pricelist.Attributes["onblur"] = "CalcBlur(this);";

            diskonLumpSum.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            diskonLumpSum.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            diskonLumpSum.Attributes["onblur"] = "CalcBlur(this);";
        }

        protected void gantikorporasi(object sender, EventArgs e)
        {
            if (kori.Checked || kora.Checked)
                korp.Visible = true;
            else
                korp.Visible = false;
        }

        private bool unitvalid()
        {
            //string NoStock = Cf.Pk(nostock.Text);

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

            if (npwp.Text.Length < 15)
            {
                x = false;
                if (s == "") s = npwp.ID;
                npwpc.Text = "Minimal 15 Digit";
            }

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

            if (!sedup.Checked)
            {
                if (!Cf.isTgl(tglktp))
                {
                    x = false;
                    if (s == "") s = tglktp.ID;
                    tglktpc.Text = "Tanggal";
                }
                else
                    tglktpc.Text = "";
            }

            if (!Cf.isTgl(tglKontrak))
            {
                x = false;
                if (s == "") s = tglKontrak.ID;
                tglkontrakc.Text = "Tanggal";
            }
            else
                tglkontrakc.Text = "";

            if (!unitvalid())
            {
                x = false;
                if (s == "") s = unit.ID;
                unitc.Text = "Tidak Available";
            }
            else
                unitc.Text = "";

            if (!Cf.isMoney(nilai))
            {
                x = false;
                if (s == "") s = nilai.ID;
                nilaic.Text = "Angka";
            }
            else
                nilaic.Text = "";

            if (carabayar2.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = carabayar2.ID;
                carabayarc.Text = "Pilih salah satu jenis";
            }
            else
                carabayarc.Text = "";



            if (nilaiDiskon.Text != "")
            {
                if (!Cf.isMoney(nilaiDiskon))
                {
                    x = false;
                    if (s == "") s = nilaiDiskon.ID;
                    diskon2c.Text = "Angka";
                }
                else
                    diskon2c.Text = "";
            }



            if (JenisPPN.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = JenisPPN.ID;
                JenisPPNc.Text = "Pilih";
            }
            else
                JenisPPNc.Text = "";

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
                ddlSumberDanac.Text = "Pilih";
            }
            else
                ddlSumberDanac.Text = "";

            if (ddlTujuan.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = ddlTujuan.ID;
                ddlTujuanc.Text = "Pilih";
            }
            else
                ddlTujuanc.Text = "";

            if (kori.Checked || kora.Checked)
            {
                if (Cf.isEmpty(penanggungjawab))
                {
                    x = false;
                    if (s == "") s = penanggungjawab.ID;
                    penanggungjawabc.Text = "Kosong";
                }
                else
                    penanggungjawabc.Text = "";

                if (Cf.isEmpty(jabatan))
                {
                    x = false;
                    if (s == "") s = jabatan.ID;
                    jabatanc.Text = "Kosong";
                }
                else
                    jabatanc.Text = "";

                if (Cf.isEmpty(nosk))
                {
                    x = false;
                    if (s == "") s = nosk.ID;
                    noskc.Text = "Kosong";
                }
                else
                    noskc.Text = "";

                if (Cf.isEmpty(bentuk))
                {
                    x = false;
                    if (s == "") s = bentuk.ID;
                    bentukc.Text = "Kosong";
                }
                else
                    bentukc.Text = "";
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
                    + " 1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + " 2. Nama tidak boleh kosong.\\n"
                    + " 3. No. Telpon dan HP tidak boleh kosong.\\n"
                    + " 4. Unit yang dipesan harus available dan tidak boleh kosong.\\n"
                    + " 5. Nilai Booking Fee dan Nilai Netto harus berupa angka.\\n"
                    + " 6. Khusus Cek Giro : No. BG tidak boleh kosong.\\n"
                    + " 7. Sumber Dana harus dipilih.\\n"
                    + " 8. Tujuan Pembelian harus dipilih.\\n"
                    + " 9. Jenis Tanggungan PPN harus dipilih.\\n"
                    + "10. Nilai Price List tidak boleh 0/Kosong."
                    + "11. Data korporasi tidak boleh kosong.\\n"
                    + "12. NPWP minimal 15 angka.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private void AutoID()
        {
            string SP = "SPA-";
            DateTime TglKontrak = Convert.ToDateTime(tglKontrak.Text);
            int c = Db.SingleInteger("SELECT COUNT(NoKontrak) FROM MS_KONTRAK");
            string[] j = (Cf.DaySlash1(tglKontrak.Text)).ToString().Split('/');
            string Tahunkontrak = j[2];
            string Bulankontrak = j[1];

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                nokontrak.Text = SP + Tahunkontrak + Bulankontrak + c.ToString().PadLeft(6, '0');

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
                DateTime TanggalKontrak = Convert.ToDateTime(tglKontrak.Text);
                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                //Numerator
                nokontrak.Text = Numerator.SuratPesanan(TanggalKontrak.Month, TanggalKontrak.Year,Project);

                string Nama = Cf.Str(nama.Text);
                string KTP1 = Cf.Str(ktp1.Text);
                string KTP2 = Cf.Str(ktp2.Text);
                string KTP3 = Cf.Str(ktp3.Text);
                string KTP4 = Cf.Str(ktp4.Text);
                string KTP5 = Cf.Str(ktp5.Text);
                string NoKTP = Cf.Str(noktp.Text);
                string NoTelp = Cf.Str(telp.Text);
                string NoHp = Cf.Str(hp.Text);
                string NoHp2 = Cf.Str(hp2.Text);

                //string NoStock = Cf.Pk(nostock.Text);
                string Skema = Cf.Str(skema.SelectedItem.Text);

                int NoAgent = Convert.ToInt32(agent.SelectedValue);

                //Editan
                string SumberData = Cf.Str(sumberdata.SelectedValue);

                string TipeCs = "";
                if (perorangan.Checked) TipeCs = "PERORANGAN";
                if (badanhukum.Checked) TipeCs = "BADAN HUKUM";

                string Marital = Cf.Str(marital.SelectedValue);

                string wn = "";
                if (wni.Checked) wn = "WNI";
                if (wna.Checked) wn = "WNA";
                if (kori.Checked) wn = "KORPORASI INDONESIA";
                if (kora.Checked) wn = "KORPORASI ASING";

                string TempatLahir = Cf.Str(tempat.Text);
                //DateTime TanggalLahir = Convert.ToDateTime(tgllahir.Text);
                string Agama = Cf.Str(agama.SelectedValue);
                string JenisKelamin = Cf.Str(jenisKelamin.SelectedValue);
                string NoFax = Cf.Str(fax.Text);
                string Kodepos = Cf.Str(kodepos1.Text);
                string Email = Cf.Str(email.Text);
                string Alamat1 = Cf.Str(alamat1.Text);
                string Alamat2 = Cf.Str(alamat2.Text);
                string Alamat3 = Cf.Str(alamat3.Text);
                string Alamat4 = Cf.Str(alamat4.Text);
                string Alamat5 = Cf.Str(alamat5.Text);
                string Kantor1 = Cf.Str(kantor1.Text);
                string Kantor2 = Cf.Str(kantor2.Text);
                string Kantor3 = Cf.Str(kantor3.Text);
                string Kantor4 = Cf.Str(kantor4.Text);
                string Kantor5 = Cf.Str(kantor5.Text);
                string Perusahaan = Cf.Str(perusahaan.Text);
                string SIUP = Cf.Str(siup.Text);
                string JenisUsaha = Cf.Str(jenisusaha.Text);
                string NoKantor = Cf.Str(telpk.Text);
                //string FaxKantor = Cf.Str(faxk.Text);
                string NamaOrangHub = Cf.Str(nmorghub.Text);
                string Hubungan = Cf.Str(hubungan.SelectedValue);
                //string Telp = Cf.Str(tlphub.Text);
                string HP = Cf.Str(hphub.Text);
                string EmailHub = Cf.Str(emailhub.Text);
                string Tujuan = ddlTujuan.SelectedValue;
                string TujuanLainnya = "";
                if (tujuanlain.Text != "")
                    TujuanLainnya = tujuanlain.Text;
                string SumberDana = ddlSumberDana.SelectedValue;
                string SumberDanaLainnya = "";
                if (lainnya.Text != "")
                    SumberDanaLainnya = lainnya.Text;
                string NUP = Cf.Str(noqueue.Text);
                string Pekerjaan = Cf.Str(pekerjaan.Text);
                string NamaNPWP = Cf.Str(namanpwp.Text);
                string NPWP1 = Cf.Str(npwp1.Text);
                string NPWP2 = Cf.Str(npwp2.Text);
                string NPWP3 = Cf.Str(npwp3.Text);
                string NPWP4 = Cf.Str(npwp4.Text);
                string NPWP5 = Cf.Str(npwp5.Text);

                string k1 = "", k2 = "", k3 = "", k4 = "";
                if (kori.Checked || kora.Checked)
                {
                    k1 = Cf.Str(penanggungjawab.Text);
                    k2 = Cf.Str(jabatan.Text);
                    k3 = Cf.Str(nosk.Text);
                    k4 = Cf.Str(bentuk.Text);
                }

                decimal pl = Convert.ToDecimal(Pricelist.Text);

                string ReffAgent1 = "", ReffAgent2 = "";
                if (reff.Visible && agentreff.SelectedIndex > 0)
                {
                    string[] aa = agentreff.SelectedValue.Split(';');
                    if (aa[1] == "EMPLOYEE")
                    {
                        ReffAgent1 = aa[0];
                    }
                    else
                    {
                        ReffAgent2 = aa[0];
                    }
                }

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
                    + "," + pl
                    );
                int NoCustomer = Db.SingleInteger(
                    "SELECT TOP 1 NoCustomer FROM MS_CUSTOMER ORDER BY NoCustomer DESC");

                string Sedup = (sedup.Checked) ? "1" : "0";
                Db.Execute("UPDATE MS_CUSTOMER SET "
                    + " Alamat1 = '" + Cf.Str(alamat1.Text) + "'"
                    + ",Alamat2 = '" + Cf.Str(alamat2.Text) + "'"
                    + ",Alamat3 = '" + Cf.Str(alamat3.Text) + "'"
                    + ",Alamat4 = '" + Cf.Str(alamat4.Text) + "'"
                    + ",Alamat5 = '" + Cf.Str(alamat5.Text) + "'"
                    + ",KTP5 = '" + KTP5 + "'"
                    + ",NamaNPWP = '" + Cf.Str(namanpwp.Text) + "'"
                    + ",NPWP = '" + Cf.Str(npwp.Text) + "'"
                    + ",NPWPAlamat1 = '" + NPWP1 + "'"
                    + ",NPWPAlamat2 = '" + NPWP2 + "'"
                    + ",NPWPAlamat3 = '" + NPWP3 + "'"
                    + ",NPWPAlamat4 = '" + NPWP4 + "'"
                    + ",NPWPAlamat5 = '" + NPWP5 + "'"
                    + ",SumberData='" + SumberData + "'"
                    + ",TipeCs ='" + TipeCs + "'"
                    + ",TempatLahir='" + TempatLahir + "'"
                    + ",Agama ='" + Agama + "'"
                    + ",NoHP2 ='" + NoHp2 + "'"
                    //+ ",JenisKelamin ='" + JenisKelamin + "'"
                    + ",Marital ='" + Marital + "'"
                    + ",NoFax ='" + NoFax + "'"
                    + ",Kodepos ='" + Kodepos + "'"
                    + ",Email ='" + Email + "'"
                    + ",Kantor1 = '" + Kantor1 + "'"
                    + ",Kantor2 = '" + Kantor2 + "'"
                    + ",Kantor3 = '" + Kantor3 + "'"
                    + ",Kantor4 = '" + Kantor4 + "'"
                    + ",Kantor5 = '" + Kantor5 + "'"
                    + ",NamaKerabat = '" + NamaOrangHub + "'"
                    + ",Hubungan = '" + Hubungan + "'"
                    + ",NoHPKerabat = '" + HP + "'"
                    + ",EmailKerabat = '" + EmailHub + "'"
                    //+ ",Perusahaan ='" + Perusahaan + "'"
                    //+ ",SIUP ='" + SIUP + "'"
                    //+ ",JenisUsaha='" + JenisUsaha + "'"
                    + ",NoKantor='" + NoKantor + "'"
                    //+ ",FaxKantor='" + FaxKantor + "'"
                    + ",AgentInput='" + Act.UserID + "'"
                    + ",Kewarganegaraan = '" + wn + "'"
                    + ",Pekerjaan = '" + Pekerjaan + "'"
                    + ",PenanggungjawabKorp = '" + k1 + "'"
                    + ",JabatanKorp = '" + k2 + "'"
                    + ",NoSKKorp = '" + k3 + "'"
                    + ",BentukKorp = '" + k4 + "'"
                    + ",KTPSeumurHidup = " + Sedup
                    + " WHERE NoCustomer = " + NoCustomer);

                if (tgllahir.Text != "")
                    Db.Execute("UPDATE MS_CUSTOMER SET TglLahir = '" + Convert.ToDateTime(tgllahir.Text) + "' WHERE NoCustomer = " + NoCustomer);
                if (tglktp.Text != "")
                    Db.Execute("UPDATE MS_CUSTOMER SET TglKTP = '" + Convert.ToDateTime(tglktp.Text) + "' WHERE NoCustomer = " + NoCustomer);
                int KPR;
                if (carabayar2.SelectedIndex == 2)
                {
                    KPR = 1;
                }
                else
                {
                    KPR = 0;
                }

                //Tambahan Richard Harga Tanah dan Bangunan 6 Des 2018
                string RumusBunga = bunga2.Text;

                decimal HargaTanah = Db.SingleDecimal("Select HargaTanah From MS_UNIT Where NoStock = '" + NoStock + "'");

                decimal HargaTanahAfterBunga = Func.SetelahBunga(RumusBunga, HargaTanah) - Math.Round((Func.SetelahBunga(RumusBunga, HargaTanah) / (decimal)1.1));
                //End of Tambahan

                //Manual update
                string sSQL = "UPDATE MS_KONTRAK"
                    + " SET JenisPPN = '" + JenisPPN.SelectedItem.Text + "'"
                    + ", JenisKPR = " + KPR
                    + ", CaraBayar = '" + carabayar2.SelectedValue + "'"
                    + ", TglKontrak ='" + TanggalKontrak + "'"
                    + ", RefSkema ='" + skema.SelectedValue + "'"
                    + ", SumberDana='" + SumberDana + "'"
                    + ", SumberDanaLainnya='" + Cf.Str(SumberDanaLainnya) + "'"
                    + ", TujuanKontrak = '" + Tujuan + "'"
                    + ", TujuanLainnya='" + Cf.Str(TujuanLainnya) + "'"
                    + ", NUP = '" + NUP + "'"
                    + ", Gross = " + pl
                    + ", NilaiKontrak = " + pl
                    + ", reffcust = '" + reffcust.Text + "'"
                    + ", anreff = '" + anreff.Text + "'"
                    + ", bankreff = '" + bankreff.Text + "'"
                    + ", norekreff = '" + norekreff.Text + "'"
                    + ", npwpreff = '" + npwpreff.Text + "'"
                    + ", NoKontrakManual = '" + NoKontrakManual + "'"
                    + ", NoReferratorAgent = '" + rep.Text + "'" //Referral
                    + ", NoReferratorCustomer = '" + ReffAgent2 + "'" //Referral
                    + ", TitipJual=" + Convert.ToByte(titipjual.SelectedValue.ToString())
                    + ", HargaTanah = " + HargaTanahAfterBunga
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    ;
                Db.Execute(sSQL);

                byte paketinv = 0;
                if (paketinvest.Checked)
                {
                    paketinv = 1;
                }

                if (paketinv == (byte)1)
                {
                    Db.Execute("UPDATE MS_KONTRAK SET TglPaketInvestasi='" + Convert.ToDateTime(tglinv.Text) + "', PaketInvestasi = " + paketinv + " WHERE NoKontrak = '" + NoKontrak + "'");
                }

                LogCs();

                //Komisi
                DataTable aagent = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent=" + NoAgent);
                Boolean FlagCross = Convert.ToBoolean(aagent.Rows[0]["CrossSelling"]);
                //if (FlagCross == false)
                //{
                //    Db.Execute("EXEC spKontrakKomisiOverDaftar"
                //        + "'" + NoKontrak + "'"
                //        + ", '" + aagent.Rows[0]["GeneralManager"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["SalesManager"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["AdminSales"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["ProjectManager"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["KepalaUnitSales"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["MarketingSupport"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["BillingCollection"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["Cadangan"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["Kinerja"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["KantorPusat"].ToString() + "'"
                //        + ",'',''"
                //        );
                //}
                //else if (FlagCross == true)
                //{
                //    Db.Execute("EXEC spKontrakKomisiOverDaftar"
                //        + "'" + NoKontrak + "'"
                //        + ", '" + aagent.Rows[0]["GeneralManager"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["SalesManager"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["AdminSales"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["ProjectManager"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["KepalaUnitSales"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["MarketingSupport"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["BillingCollection"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["Cadangan"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["Kinerja"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["KantorPusat"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["CrossGM"].ToString() + "'"
                //        + ", '" + aagent.Rows[0]["CrossSM"].ToString() + "'"
                //        );
                //}

                SaveTagihan();


                int Count = Db.SingleInteger("SELECT COUNT(*) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");
                if (Count != 0)
                {
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
                    + ",MS_KONTRAK.SumberDana AS [Sumber Dana]"
                    + ",MS_KONTRAK.SumberDanaLainnya AS [Sumber Dana Lainnya]"
                    + ",MS_KONTRAK.TujuanKontrak AS [Tujuan Transaksi]"
                    + ",MS_KONTRAK.TujuanLainnya AS [Tujuan Transaksi Lainnya]"
                    + ",MS_KONTRAK.NUP"
                    + ",MS_KONTRAK.NoReferratorAgent"
                    + ",MS_KONTRAK.NoReferratorCustomer"
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

                //TTS
                int NoTTS = 0;
                if (Convert.ToDecimal(nilai.Text) != 0)
                {
                    NoTTS = SaveTTS(NoKontrak
                        , Db.SingleInteger("SELECT NoCustomer FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'"));
                }

                Response.Redirect("TabelStok5.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS);
            }
        }

        private void SaveTagihan()
        {
            int CaraBayar = Convert.ToInt32(skema.SelectedValue);
            decimal Netto = 0;
            //cara bayar 0 = customize
            if (CaraBayar != 0)
            {
                string RumusDiskon = diskon2.Text;
                string RumusDiskon2 = Db.SingleString(
                    "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + CaraBayar);
                string RumusBunga = bunga2.Text;

                decimal Gross = Db.SingleDecimal(
                     "SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                decimal Gross2 = Gross;

                decimal GrossAfterDiskon = Func.SetelahDiskon(RumusDiskon, Gross2);
                Netto = (CaraBayar != 0) ? Func.SetelahDiskon(RumusDiskon, Gross2) : Gross2;
                decimal NilaiDiskon = Math.Round(Gross2 - GrossAfterDiskon);

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
                Netto = Func.SetelahBunga(RumusBunga, Netto);

                Db.Execute("UPDATE MS_KONTRAK"
                 + " SET DiskonTambahan = " + DiskonTambahan
                 + " WHERE NoKontrak = '" + NoKontrak + "'");


                /********************************/


                string DiskonPersen = diskon2.Text;
                decimal DiskonRupiah = Convert.ToDecimal(nilaiDiskon.Text);

                Db.Execute("UPDATE MS_KONTRAK"
                 + " SET DiskonKet='" + RumusDiskon2 + "'"
                 + ", DiskonPersen='" + DiskonPersen + "'"
                 + ", DiskonRupiah = " + DiskonRupiah
                 + " WHERE NoKontrak = '" + NoKontrak + "'");
                /***********/

                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
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
                    + ", " + NilaiDiskon
                    + ",'" + RumusDiskon + "'"
                    + ",'" + Cf.Str(RumusDiskon2) + "'"
                    );

                Db.Execute("EXEC spKontrakBunga"
                   + " '" + NoKontrak + "'"
                   + ", " + Gross
                   + ", " + Math.Round(NilaiKontrak)
                   + ",'" + RumusBunga + "'"
                   );


                //decimal DPP = NilaiKontrak - NilaiPPN;

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET "
                    + " PPN = '" + Cf.BoolToSql(sifatppn.SelectedIndex == 1) + "'"
                    + ", NilaiPPN = " + PPN
                    + ", NilaiDPP = " + DPP
                    + " , NilaiKontrak = " + NilaiKontrak
                    + " WHERE NoKontrak = '" + NoKontrak + "'");

                string[,] x = Func.Breakdown(CaraBayar, NilaiKontrak, Convert.ToDateTime(tglKontrak.Text));
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
                    DateTime TglJT = Db.SingleTime("SELECT TOP 1 TglJT FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND Tipe='DP' ORDER BY TglJT DESC");
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

                decimal Gross = Db.SingleDecimal(
                     "SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                decimal Gross2 = Gross;
                string RumusBunga = bunga2.Text;
                string RumusBunga2 = Db.SingleString("SELECT Bungaket FROM REF_SKEMA WHERE Nomor = " + CaraBayar);

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
                Netto = Func.SetelahBunga(RumusBunga, Netto);

                Db.Execute("UPDATE MS_KONTRAK"
                 + " SET DiskonTambahan = " + DiskonTambahan
                 + " WHERE NoKontrak = '" + NoKontrak + "'");


                /********************************/

                decimal NilaiPPN = 0;
                decimal NilaiKontrak = 0;

                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                string ParamID = "PLIncludePPN" + Project;

                bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";

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

                NilaiKontrak = Netto + NilaiPPN;


                Db.Execute("EXEC spKontrakDiskon"
                    + " '" + NoKontrak + "'"
                    + ", " + Gross2
                    + ", " + NilaiKontrak
                    + ",0"
                    + ",''"
                    + ",''"
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
                        + ",0"
                        );
                    count++;
                }
            }
        }

        private int SaveTTS(string NoKontrak, int NoCustomer)
        {
            DateTime TglTTS = DateTime.Today;
            string Unit = Cf.Str(unit.Text);
            string Customer = Cf.Str(Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer));
            string CaraBayar = carabayar.SelectedValue;
            decimal Nilai = Convert.ToDecimal(nilai.Text);
            string Ket = Cf.Str(kettts.Text);
            string rekening = ddlAcc.SelectedValue;

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

            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET Acc = '" + ddlAcc.SelectedValue + "' WHERE NoTTS ='" + NoTTS + "'");

            //khusus cek giro
            if (carabayar.SelectedValue == "BG")
            {
                string NoBG = Cf.Pk(nobg.Text);
                DateTime TglBG = Convert.ToDateTime(tglbg.Text);
                string BankBG = Cf.Pk(bankbg.Text);

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSRegistrasiBG"
                    + " '" + NoTTS + "'"
                    + ",'" + NoBG + "'"
                    + ",'" + TglBG + "'"
                    );

                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET BankBG = '" + BankBG + "' WHERE NoTTS = '" + NoTTS + "'");
            }

            //khusus kartu kredit
            if (carabayar.SelectedValue == "KK")
            {
                string NoKK = Cf.Pk(nokk.Text);
                string BankKK = Cf.Pk(bankkk.Text);

                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET "
                    + " NoKK = '" + NoKK + "'"
                    + ",BankKK = '" + BankKK + "'"
                    + " WHERE NoTTS = '" + NoTTS + "'"
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
                + ",Acc AS [Rekening Bank]"
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

            string KetLog = Cf.LogCapture(rs)
                + "<br>***ALOKASI PEMBAYARAN:<br>"
                + alokasi.ToString();

            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogTTS"
                + " 'REGIS'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetLog + "'"
                + ",'" + NoTTS + "'"
                //+ ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
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
                + " NoCustomer  AS [No. Customer]"
                + ",TipeCs AS [Tipe]"
                + ",Nama AS [Nama Lengkap]"
                + ",NamaBisnis AS [Nama Bisnis]"
                + ",JenisBisnis AS [Jenis Bisnis]"
                + ",MerekBisnis AS [Merek Bisnis]"
                + ",Agama AS [Agama]"
                + ",CONVERT(varchar, TglLahir, 106) AS [Tanggal Lahir]"
                + ",NoTelp AS [No. Telepon]"
                + ",NoHp AS [No. HP]"
                + ",NoHP2 AS [No. HP 2]"
                + ",NoKantor AS [No. Telepon Kantor]"
                + ",NoFax AS [No. Fax]"
                + ",Kodepos AS [Kode pos]"
                + ",Email AS [Alamat Email]"
                + ",Alamat1 AS [Alamat Surat Menyurat]"
                + ",Alamat2 AS [Alamat Surat Menyurat RT/RW]"
                + ",Alamat3 AS [Alamat Surat Menyurat Kelurahan]"
                + ",Alamat4 AS [Alamat Surat Menyurat Kecamatan]"
                + ",Alamat5 AS [Alamat Surat Menyurat Kotamadya]"
                + ",Kantor1 AS [Alamat Kantor]"
                + ",Kantor2 AS [Alamat Kantor RT/RW]"
                + ",Kantor3 AS [Alamat Kantor Kelurahan]"
                + ",Kantor4 AS [Alamat Kantor Kecamatan]"
                + ",Kantor5 AS [Alamat Kantor Kotamadya]"
                + ",NoKTP AS [No. KTP]"
                + ",KTP1 AS [KTP Alamat]"
                + ",KTP2 AS [KTP RT/RW]"
                + ",KTP3 AS [KTP Kelurahan]"
                + ",KTP4 AS [KTP Kecamatan]"
                + ",KTP5 AS [KTP Kotamadya]"
                + ",UnitLama AS [Unit Lama]"
                + ",LuasLama AS [Luas Unit Lama]"
                + ",TokoLama AS [Nama Toko Lama]"
                + ",ZoningLama AS [Zoning Lama]"
                + ",GedungLama AS [Gedung Lama]"
                + ",TeleponLama AS [Telepon Lama]"
                + ",NamaKerabat AS [Nama Orang yang Dihubungi]"
                + ",Hubungan AS [Hubungan]"
                //+ ",Telp AS [Telp]"
                + ",NoHPKerabat AS [HP]"
                + ",EmailKerabat AS [Email orang yang dihubungi]"
                + ",AkteLama AS [Akte Lama]"
                + ",Salutation"
                + ",Marital AS [Status Marital]"
                + ",Kewarganegaraan AS [Kewarganegaraan]"
                + ",Pekerjaan AS [Pekerjaan]"
                + ",NamaNPWP AS [Nama NPWP]"
                + ",NPWP"
                + ",NPWPAlamat1 AS [Alamat NPWP]"
                + ",NPWPAlamat2 AS [Alamat NPWP RT/RW]"
                + ",NPWPAlamat3 AS [Alamat NPWP Kelurahan]"
                + ",NPWPAlamat4 AS [Alamat NPWP Kecamatan]"
                + ",NPWPAlamat5 AS [Alamat NPWP Kotamadya]"
                + ",PenanggungjawabKorp AS [Penanggungjawab Korporasi]"
                + ",JabatanKorp AS [Jabatan Korporasi]"
                + ",NoSKKorp AS [No. SK Korporasi]"
                + ",BentukKorp AS [Bentuk Korporasi]"
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

        protected void skema_SelectedIndexChanged(object sender, EventArgs e)
        {
            CaraBayar();
            if (skema.SelectedIndex > 0)
            {
                string RumusDiskon = Db.SingleString(
                    "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                string Ket = Db.SingleString(
                    "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                //string NamaCashKeras = Db.SingleString(
                //    "SELECT Nama FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                string CaraBayar2 = Db.SingleString(
                    "SELECT Jenis FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                string RumusBunga = Db.SingleString(
                "SELECT Bunga FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                string Ket2 = Db.SingleString(
                "SELECT Bungaket FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);


                carabayar2.SelectedValue = CaraBayar2;
                carabayar2.Enabled = false;
                diskon2.Text = RumusDiskon;
                //nilaiDiskon.Text = Cf.Num((Convert.ToDecimal(RumusDiskon) * Convert.ToDecimal(Pricelist.Text) /100));
                diskonket.Text = Ket;
                bunga2.Text = RumusBunga;
                bungaket.Text = Ket2;

                SetDiskon();
                SetBunga();
            }

        }

        protected void diskon2_TextChanged(object sender, EventArgs e)
        {
            if (skema.SelectedIndex > 0)
            {
                SetDiskon2();
            }
            diskon2.Focus();
        }

        protected void bunga2_TextChanged(object sender, EventArgs e)
        {
            if (carabayar.SelectedIndex > 0)
            {
                SetBunga2();
            }
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

        private void CaraBayar()
        {
            string x = skema.SelectedValue;

            if (x.Contains("KERAS") == true)
            {
                carabayar2.SelectedIndex = 0;
            }
            else if (skema.SelectedValue.Contains("BERTAHAP") == true)
            {
                carabayar2.SelectedIndex = 1;
            }
            else if (skema.SelectedValue.Contains("KPA") == true)
            {
                carabayar2.SelectedIndex = 2;
            }
            else
            {
                carabayar2.ClearSelection();
                carabayar2.Enabled = true;
            }
        }

        // DISKON ---------------
        private void SetDiskon()
        {
            decimal Gross = Convert.ToDecimal(Pricelist.Text); //Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
            //+ " WHERE NoUnit = '" + Cf.Pk(unit.Text) + "'");
            decimal Gross2 = Gross; // + surcharge

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
            decimal Gross = Convert.ToDecimal(Pricelist.Text); //Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
            //+ " WHERE NoUnit = '" + Cf.Pk(unit.Text) + "'");

            decimal Gross2 = Gross;// +surcharge;

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

        // BUNGA -------
        private void SetBunga()
        {
            decimal Gross = Convert.ToDecimal(Pricelist.Text); //Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
            //+ " WHERE NoStock = '" + NoStock + "'");
            //decimal surcharge = Convert.ToDecimal(Surcharge.Text);
            decimal Gross2 = Gross;// +surcharge;

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
            decimal Gross = Convert.ToDecimal(Pricelist.Text); //Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
            //+ " WHERE NoStock = '" + NoStock + "'");
            decimal Gross2 = Gross;// +surcharge;

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

            decimal bunga = Func.NominalDiskon2(RumusBunga, Gross2);
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

        protected void ddlTujuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTujuan.SelectedIndex == 4)
            {
                trTujuanLain.Visible = true;
            }
            else
            {
                trTujuanLain.Visible = false;
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

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
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


        protected void noktp_TextChanged(object sender, EventArgs e)
        {
            if (Cf.isEmpty(noktp))
                cktp.Checked = false;
            else
                cktp.Checked = true;
        }
        protected void npwp_TextChanged(object sender, EventArgs e)
        {
            if (Cf.isEmpty(npwp))
                cnpwp.Checked = false;
            else
                cnpwp.Checked = true;
        }
        protected void gantitipe(object sender, EventArgs e)
        {
            if (perorangan.Checked)
            {
                kori.Visible = false;
                kora.Visible = false;
                wni.Visible = true;
                wni.Checked = true;
                wna.Visible = true;
                korp.Visible = false;
            }
            else
            {
                kori.Visible = true;
                kori.Checked = true;
                kora.Visible = true;
                wni.Visible = false;
                wna.Visible = false;
                korp.Visible = true;
            }
        }
    }
}
