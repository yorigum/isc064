using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.AspNet.SignalR;

namespace ISC064.MARKETINGJUAL
{
    public partial class ClosingLangsungDaftar2 : System.Web.UI.Page
    {
        //public decimal HargaTanah = 0;
        //public decimal HargaBangunan = 0;
        //public decimal HargaBangunan = 0;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Func.UnitSedangClosing(NoStock);

            if (!Page.IsPostBack)
            {
                frm.Visible = false;
                InitForm();

                Fill();
                CaraBayar();
                frm.Visible = divPersenBertingkat.Visible = false;

                divPersenBertingkat.Visible = false;
                divLumpSum.Visible = true;

                if (Request.QueryString["NoCustomer"] != null)
                {
                    nocustomer.Text = Request.QueryString["NoCustomer"];
                    nostock2.Text = Request.QueryString["NoStock"];
                    LoadCustomer();
                }
                nostock.Text = nostock2.Text = Db.SingleString(
                            "SELECT NoStock FROM MS_UNIT WHERE NoUnit = '" + Cf.Pk(unit.Text) + "'");
                tglKontrak.Text = Cf.Day(DateTime.Today);

                Js.NumberFormat(nilai);
                Js.NumberFormat(Pricelist);
                Js.NumberFormat(diskonLumpSum);
            }
            btnpop.Attributes.Add("modal-url", "DaftarCustomer.aspx?status=a&project=" + Project);
        }

        protected void GantiTipeSales(object sender, System.EventArgs e)
        {
            string Tipe = Db.SingleString("SELECT Tipe FROM MS_AGENT WHERE NoAgent = '" + agent.SelectedValue + "'");
            reff.Visible = Tipe == "INHOUSE" ? true : false;
        }

        private void InitForm()
        {
            //Sales
            DataTable rs = Db.Rs("SELECT Nama,Principal,NoAgent FROM MS_AGENT WHERE Status = 'A' AND Jabatan in (3,6) AND Project = '" + Project + "'"
                + " ORDER BY Nama,NoAgent");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                if (rs.Rows[i]["Principal"].ToString() != "")
                    t = t + " (" + rs.Rows[i]["Principal"] + ")";
                agent.Items.Add(new ListItem(t, v));
            }

            rs = Db.Rs("SELECT Nama,NoAgent FROM MS_AGENT WHERE Status = 'A' AND Tipe = 'REFFERATOR'"
                + " ORDER BY Nama,NoAgent");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"] + ";EMPLOYEE";
                string t = rs.Rows[i]["Nama"].ToString() + " (EMPLOYEE)";
                agentreff.Items.Add(new ListItem(t, v));
            }

            rs = Db.Rs("SELECT Nama,NoCustomer FROM MS_CUSTOMER WHERE Status = 'A' AND Refferator = 1"
                + " ORDER BY Nama,NoCustomer");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoCustomer"] + ";BUYER";
                string t = rs.Rows[i]["Nama"].ToString() + " (BUYER)";
                agentreff.Items.Add(new ListItem(t, v));
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

            //Cara bayar
            string Lokasi = Db.SingleString("SELECT Lokasi FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            DataTable rs2 = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' AND Project = '" + Project + "'AND TipeUnit='"+ Lokasi +"' ORDER BY Nama");
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
            rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE Project = '" + Project + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                ddlAcc.Items.Add(new ListItem(t, v));
            }

            rs = Db.Rs("SELECT * FROM REF_LOKASI_KONTRAK WHERE Project = '" + Project + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["SN"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                lokpen.Items.Add(new ListItem(t, v));
            }

            Js.NumberFormat(fo);
            Js.NumberFormat(focounter);
        }

        private void Fill()
        {

            cancel.Attributes["onclick"] = "location.href = 'ClosingLangsungDaftar2.aspx?NoStock=" + NoStock + "'";
            btnbaru.Attributes["onclick"] = "location.href = 'CustomerDaftar.aspx?NoStock=" + NoStock + "&closing=1&project=" + Project + "'";
            //Data Unit
            DataTable rs = Db.Rs("SELECT Luas,PriceList,NoUnit,PriceListKavling FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                string unitpesanan = unit.Text = rs.Rows[0]["NoUnit"].ToString();
                
                if (Request.QueryString["NoAgent"] != null && Request.QueryString["NoSkema"].ToString() != null)
                {
                    agent.SelectedValue = Request.QueryString["NoAgent"].ToString();
                    agent.Enabled = false;
                    skema.SelectedValue = Request.QueryString["NoSkema"].ToString();
                    CaraBayar();

                }

                decimal pl = Db.SingleDecimal("SELECT ISNULL(PriceList, 0) FROM ISC064_MARKETINGJUAL..MS_PRICELIST WHERE NoSkema='" + skema.SelectedValue + "' AND NoStock='" + NoStock + "'");
                Pricelist.Text = Cf.Num(pl);


                nounit.Text = "<a href=\"javascript:popUnit('" + NoStock + "')\">"
                + rs.Rows[0]["NoUnit"] + "</a>";
            }

            string Tipe = Db.SingleString("SELECT Kategori FROM MS_UNIT WHERE NoUnit = '" + unit.Text + "'");
            if (Tipe == "FLPP")
            {
                sifatppn.SelectedIndex = 0;
            }
            else if (Tipe == "REAL ESTATE")
            {
                sifatppn.SelectedIndex = 1;
            }
            else if (Tipe == "KOMERSIL")
            {
                sifatppn.SelectedIndex = 1;
            }
        }

        private bool unitvalid()
        {
            bool x = true;
            if (NoStock == "")
                x = false;
            else
            {
                string Status = Db.SingleString("SELECT Status FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                if (Status == "B")
                {
                    int Kontrak = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK WHERE NoStock='" + NoStock + "' AND Status='A'");
                    if (Kontrak != 0)
                    {
                        x = false;
                    }
                }
                else
                    x = true;
            }
            return x;
        }

        private bool csvalid()
        {
            bool x = true;

            try { int z = Convert.ToInt32(NoCustomer); }
            catch { x = false; }

            if (x)
            {
                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer
                    + " AND Status = 'A' AND Project = '" + Project + "'");
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
                pilih.Visible = false;
                frm.Visible = true;

                FillForm();

                Js.Focus(this, nilai);
            }
            else
            {
                Js.Focus(this, nocustomer);
                frm.Visible = false;
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (csvalid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                FillForm();

                Js.Focus(this, noqueue);
            }
        }

        private void FillForm()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_CUSTOMER WHERE NoCustomer ='" + NoCustomer + "'");
            string namacustomer = Db.SingleString(
                "SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer);
            customer.Text = "<a href=\"javascript:popEditCustomer('" + NoCustomer + "')\">" + namacustomer
                + " (" + Convert.ToInt32(NoCustomer).ToString().PadLeft(5, '0') + ")</a>";

            string CnnEsales = "Data Source=.;Initial Catalog=SA01;Persist Security Info=True;User ID=batavianet;Password=iNDigo100";
            string Project = "";
            //Project = SingleString("SELECT ISNULL(Dept,'') FROM RefMkt WHERE Db='117'", CnnEsales);
            string ProspectID = "";
            ProspectID = Db.SingleString("SELECT ISNULL(ProspectID,'') FROM MS_CUSTOMER WHERE NoCustomer=" + NoCustomer);

            if (Project != "" && ProspectID != "")
            {
                DataTable ES = Rs("SELECT A.ClosingID, B.Nama FROM SalesClosing A INNER JOIN SecUser B ON A.UserID = B.UserID WHERE A.Dept='" + Project + "' AND A.ProspectID='" + ProspectID + "' AND Status=0 AND NoKontrak=''", CnnEsales);

                for (int i = 0; i < ES.Rows.Count; i++)
                {
                    string v = ES.Rows[i]["ClosingID"].ToString();
                    string t = ES.Rows[i]["ClosingID"].ToString() + " - " + ES.Rows[i]["Nama"].ToString();
                    esales.Items.Add(new ListItem(t, v));
                }
            }
            else
            {
                esales.SelectedIndex = 0;
                esales.Enabled = false;
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

            if (!Cf.isTgl(tglKontrak))
            {
                x = false;
                if (s == "") s = tglKontrak.ID;
                tglkontrakc.Text = "Tanggal";
            }
            else
                tglkontrakc.Text = "";

            if (agent.Items.Count == 0)
            {
                x = false;
                Js.Alert(this, "Belum memiliki Sales", "");
            }

            if (!unitvalid())
            {
                x = false;
                if (s == "") s = unit.ID;
                unitc.Text = "Tidak Available";
            }
            else
                unitc.Text = "";

            if (skema.SelectedIndex != 0)
            {
                if (carabayar2.SelectedIndex == -1)
                {
                    x = false;
                    if (s == "") s = carabayar2.ID;
                    carabayarc.Text = "Pilih salah satu jenis";
                }
                else
                    carabayarc.Text = "";
            }

            if (carabayar2.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = carabayar2.ID;
                carabayarc.Text = "Pilih salah satu jenis";
            }
            else
                carabayarc.Text = "";

            string Lokasi = Db.SingleString("SELECT Lokasi FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            DataTable TargetST = Db.Rs("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'TargetBAST" + Project + Lokasi + "'");

            if (TargetST.Rows.Count == 0)
            {
                x = false;
                Js.Alert(this, "Target BAST untuk lokasi " + Lokasi + " belum di setting", "");
            }


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
                    + "2. Nilai Price List tidak boleh 0/Kosong.\\n"
                    + "3. Unit yang dipesan harus available dan tidak boleh kosong.\\n"
                    + "4. Jenis Tanggungan PPN harus dipilih.\\n"
                    + "5. Cara bayar harus dipilih.\\n"
                    + "6. Surcharge harus berupa angka.\\n"
                    + "7. Sumber Dana harus dipilih.\\n"
                    + "8. Tujuan Pembelian harus dipilih.\\n"
                    //+ "7. NUP tidak boleh Kosong."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                decimal NilaiDiskon = Convert.ToDecimal(diskonLumpSum.Text);
                decimal NilaiDiskonPersen = Func.NominalDiskon(diskontambahPersen.Text, Convert.ToDecimal(Pricelist.Text));

                if (NilaiDiskon > 0 || NilaiDiskonPersen != 0)
                {
                    string cek = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'ApprovDiskon" + Project + "'");
                    if (cek == "True")
                    {
                        SaveApprove();
                    }
                    else
                    {
                        Save();
                    }
                }
                else
                {
                    Save();
                }
            }
        }

        private void Save()
        {
            DateTime TglKontrak = Convert.ToDateTime(tglKontrak.Text);
            string Lokasi = Db.SingleString("SELECT Lokasi FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            //Numerator
            nokontrak.Text = Numerator.SuratPesanan(TglKontrak.Month, TglKontrak.Year, Project);

            if (nilaiBunga.Text == "") nilaiBunga.Text = "0";

            string Skema = Cf.Str(skema.SelectedItem.Text);
            int NoAgent = Convert.ToInt32(agent.SelectedValue);
            decimal pl = Convert.ToDecimal(Pricelist.Text);
            DataTable TargetST = Db.Rs("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'TargetBAST" + Project + Lokasi + "'");
            string Tujuan = ddlTujuan.SelectedValue;
            string TujuanLainnya = "";
            if (tujuanlain.Text != "")
                TujuanLainnya = tujuanlain.Text;
            string SumberDana = ddlSumberDana.SelectedValue;
            string SumberDanaLainnya = "";
            if (lainnya.Text != "")
                SumberDanaLainnya = lainnya.Text;
            string NUP = Cf.Str(noqueue.Text);

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

            Db.Execute("EXEC spKontrakDaftar4"
                + " '" + NoKontrak + "'"
                + ",'" + NoStock + "'"
                + ",'" + TglKontrak + "'"
                + ",'" + Skema + "'"
                + ",'" + TargetST.Rows[0]["Value"] + "'"
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

            int CaraBayar = Convert.ToInt32(skema.SelectedValue);
            decimal PPN = 0, Netto = 0;

            string RumusDiskon = diskon2.Text;
            string RumusDiskon2 = Db.SingleString(
                "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + CaraBayar);
            string RumusBunga = bunga2.Text;
            string RumusBunga2 = Db.SingleString("SELECT Bungaket FROM REF_SKEMA WHERE Nomor = " + CaraBayar);

            decimal Gross = Db.SingleDecimal(
                "SELECT isnull(sum(Gross),0) FROM MS_KONTRAK_APPROVAL WHERE NoKontrak = '" + NoKontrak + "'");

            decimal disawal = Db.SingleDecimal("SELECT isnull(sum(DiskonRupiah),0) FROM MS_KONTRAK_APPROVAL WHERE NoKontrak = '" + NoKontrak + "'");

            decimal bung = Db.SingleDecimal("SELECT isnull(sum(BungaNominal),0) FROM MS_KONTRAK_APPROVAL WHERE  NoKontrak = '" + NoKontrak + "'");
            decimal Gross2 = Gross;

            decimal GrossAfterDiskon = Func.SetelahDiskon(RumusDiskon, Gross2);
            decimal GrossAfterBunga = Func.SetelahBunga(RumusBunga, Gross2);
            decimal ND = Gross2 - GrossAfterDiskon;
            decimal NB = Gross2 - GrossAfterBunga;
            decimal HargaSetelahBunga = GrossAfterBunga - ND;

            //Tambahan Richard Harga Tanah dan Bangunan 6 Des 2018
            //decimal HargaTanahAfterBunga = Func.SetelahBunga(RumusBunga, HargaTanah);
            //decimal HargaBangunanAfterBunga = Func.SetelahBunga(RumusBunga, HargaBangunan);
            //End of Tambahan

            Netto = (CaraBayar != 0) ? Func.SetelahDiskon(RumusDiskon, Gross2) : Gross2;

            /* DISKON TAMBAHAN SAAT CLOSING */

            decimal DiskonTambahan = 0;
            if (jenisDiskon.SelectedIndex == 0)
            {   //Diskon lum sum
                DiskonTambahan = Convert.ToDecimal(diskonLumpSum.Text);
            }
            else if (jenisDiskon.SelectedIndex == 1)
            {   //Diskon % bertingkat
                decimal coba = 0, totaldisc = 0;
                string[] DiscTambahPersen = diskontambahPersen.Text.Split('+');
                decimal dpp = HargaSetelahBunga;

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

            Db.Execute("UPDATE MS_KONTRAK_APPROVAL"
                + " SET DiskonTambahan = " + DiskonTambahan
                + " WHERE NoKontrak = '" + NoKontrak + "'");

            Netto -= DiskonTambahan;
            /********************************/

            decimal distambah = Db.SingleDecimal("SELECT isnull(sum(DiskonTambahan),0) FROM MS_KONTRAK_APPROVAL WHERE NoKontrak = '" + NoKontrak + "'");
            decimal afterDiscTambahan = Math.Round(HargaSetelahBunga - distambah); //sebelumnya tanpa Math.round

            decimal NilaiKontrak = afterDiscTambahan;
            //Batam tidak ada PPN jadi di coment willy
            decimal DPP = NilaiKontrak;/// (decimal)1.1; //sebelumnya dengan Math.round

            Db.Execute("UPDATE MS_KONTRAK_APPROVAL SET NilaiKontrak = " + Convert.ToDecimal(NilaiKontrak)
                + ", DiskonPersen = '" + RumusDiskon + "'"
                + ", DiskonKet = '" + RumusDiskon2 + "'"
                + ", DiskonRupiah = " + Convert.ToDecimal(nilaiDiskon.Text)
                + ", BungaPersen = '" + RumusBunga + "'"
                + ", BungaNominal = " + Convert.ToDecimal(nilaiBunga.Text)
                + ", BungaKet = '" + RumusBunga2 + "'"
                + ", PPN = '" + Cf.BoolToSql(sifatppn.SelectedIndex == 1) + "'"
                + ", PPNBulat = '" + Cf.BoolToSql(roundppn.Checked) + "'"
                + ", DiskonTambahan = " + DiskonTambahan
                //+ ", HargaTanah = " + HargaTanahAfterBunga
                //+ ", HargaBangunan = " + HargaBangunanAfterBunga
                //+ ", SN = " + SN
                + " WHERE NoKontrak = '" + NoKontrak + "'");

            string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
            string Pers = Db.SingleString("SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
            string NamaPers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Pers + "'");

            //VA
            string NoVA = "";
            string LokasiVA = Db.SingleString("SELECT Lokasi FROM MS_KONTRAK WHERE NoStock = '" + NoStock + "'");

            //string VA = "06583"; NO VA Lama
            string VA = "08652";

            //di kurang 1 karena sudah save kontrak di ms kontrak
            //int CountUnit = Db.SingleInteger("SELECT Count(*) FROM MS_KONTRAK WHERE NoStock = '" + NoStock + "'") - 1;
            int CountUnit = Db.SingleInteger("SELECT Count(*) FROM MS_KONTRAK WHERE NoStock = '" + NoStock + "'");
            string Nomor = Db.SingleString("SELECT Nomor FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            string Lantai = Db.SingleString("SELECT Lantai FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

            //Tambahan Richard Harga Tanah dan Bangunan 6 Des 2018

            decimal HargaTanah = Db.SingleDecimal("Select HargaTanah From MS_UNIT Where NoStock = '" + NoStock + "'");

            decimal HargaTanahAfterBunga = Func.SetelahBunga(RumusBunga, HargaTanah) - Math.Round((Func.SetelahBunga(RumusBunga, HargaTanah) / (decimal)1.1));
            //End of Tambahan

            int Lokasii = Db.SingleInteger("SELECT SNVA FROM REF_LOKASI WHERE Lokasi = '" + LokasiVA + "'");
            string Tipe = "02";
            if (LokasiVA != "S")
                Tipe = "01";
            NoVA = VA + TglKontrak.Year.ToString().Substring(2, 2) + TglKontrak.Month.ToString().PadLeft(2, '0') + Tipe + Lokasii.ToString() + Lantai.PadLeft(2, '0') + Nomor.PadLeft(2, '0');

            string sSQL = "UPDATE MS_KONTRAK"
                + " SET JenisKPR = '" + KPR + "'"
                + ", CaraBayar = '" + carabayar2.SelectedValue + "'"
                + ", RefSkema = " + skema.SelectedValue
                + ", LokasiPenjualan = " + lokpen.SelectedValue
                // + ", Surcharge ='" + surcharge + "'"
                + ", jenisPPN = '" + JenisPPN.SelectedValue + "'"
                + ", BungaNominal = " + Convert.ToDecimal(nilaiBunga.Text)
                + ", BungaPersen = '" + bunga2.Text + "'"
                // + ", DiscTambahPersen = '" + diskontambahPersen.Text + "'"
                + ", SumberDana='" + SumberDana + "'"
                + ", SumberDanaLainnya='" + Cf.Str(SumberDanaLainnya) + "'"
                + ", TujuanKontrak = '" + Tujuan + "'"
                + ", TujuanLainnya='" + Cf.Str(TujuanLainnya) + "'"
                + ", NUP = '" + NUP + "'"
                + ", NoKontrakManual = '" + NoKontrakManual + "'"
                + ", reffcust = '" + reffcust.Text + "'"
                + ", anreff = '" + anreff.Text + "'"
                + ", bankreff = '" + bankreff.Text + "'"
                + ", norekreff = '" + norekreff.Text + "'"
                + ", npwpreff = '" + npwpreff.Text + "'"
                + ", NoRefferatorAgent = '" + rep.Text + "'"
                + ", NoRefferatorCustomer = '" + ReffAgent2 + "'"
                + ", ClosingID = '" + esales.SelectedValue.ToString() + "'"
                + ", note = '" + note.Text + "'"
                + ", TitipJual=" + Convert.ToByte(titipjual.SelectedValue.ToString())
                + ", NoVA = '" + NoVA + "'"
                + ", HargaTanah = " + HargaTanahAfterBunga
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                ;
            Db.Execute(sSQL);

            Db.Execute("UPDATE MS_KONTRAK SET Project = '" + Project + "', NamaProject='" + NamaProject + "',Pers='" + Pers + "',NamaPers = '" + NamaPers + "' WHERE NoKontrak = '" + NoKontrak + "'");
            LogCs();

            SaveKontrakAgent(NoAgent, 1);
            SaveTagihan();

            if (gimmick.Text != "")
                SaveGimmick(NoKontrak);

            //string CnnEsales = "Data Source=.;Initial Catalog=SA01;Persist Security Info=True;User ID=batavianet;Password=iNDigo100";
            //if (esales.SelectedValue.ToString() != "")
            //{
            //    Execute("UPDATE SalesClosing SET"
            //            + " NoKontrak='" + NoKontrak + "'"
            //            + " WHERE ClosingID='" + esales.SelectedValue.ToString() + "'"
            //            , CnnEsales);
            //}


            //int Count = Db.SingleInteger("SELECT COUNT(*) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");
            //if (Count != 0)
            //{
            //    //SaveFO();
            //}

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
                // + ",MS_KONTRAK.Bungaket AS [KeteranganBunga]"
                // + ",MS_KONTRAK.DiskonTambahan AS [Diskon Tambahan]"
                // + ",MS_KONTRAK.DiscTambahPersen AS [Diskon Tambahan dalam persen]"
                + ",MS_KONTRAK.Skema"
                + ",MS_KONTRAK.DiskonTambahan"
                // + ",MS_KONTRAK.DiscTambahPersen"
                + ",CONVERT(varchar,MS_KONTRAK.TargetST,106) AS [Jadwal Serah Terima]"
                + ", MS_KONTRAK.JenisPPN AS [PPN Ditanggung]"
                + ", CASE MS_KONTRAK.JenisKPR"
                + "		WHEN 0 THEN 'KPR'"
                + "		WHEN 1 THEN 'NON-KPR'"
                + "	END AS [Jenis KPA]"
                + ",MS_KONTRAK.SumberDana AS [Sumber Dana]"
                + ",MS_KONTRAK.SumberDanaLainnya AS [Sumber Dana Lainnya]"
                + ",MS_KONTRAK.TujuanKontrak AS [Tujuan Transaksi]"
                + ",MS_KONTRAK.TujuanLainnya AS [Tujuan Transaksi Lainnya]"
                + ",MS_KONTRAK.NUP"
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

            //TTS
            int NoTTS = 0;
            if (Convert.ToDecimal(nilai.Text) != 0)
            {
                NoTTS = SaveTTS(NoKontrak
                    , Db.SingleInteger("SELECT NoCustomer FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'"));
            }

            var context = GlobalHost.ConnectionManager.GetHubContext<ClosingUnit>();
            context.Clients.All.invokeStatus(NoStock);
            if (Request.QueryString["NoUrut"] != null && Request.QueryString["NUP"] != null)
            {
                if (Request.QueryString["NUP"].ToString() == "0")
                {
                    //UPDATE KE CUSTOMERRESERVASI
                    Db.Execute("UPDATE [NUP03]..CustomerReservasi SET Status='C' WHERE NoReservasi='" + Request.QueryString["NoUrut"].ToString() + "'");
                }
                else
                {
                    Db.Execute("UPDATE [NUP03]..CustomerNUP SET Status='C' WHERE NoNUP='" + Request.QueryString["NoUrut"].ToString() + "'");
                    //UPDATE Ke CUSTOMERNUPDETAIL
                    NoTTS = SaveTTS2(Request.QueryString["NoUrut"].ToString()
                    , Db.SingleInteger("SELECT NoCustomer FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'"));
                    //Insert to MS_TTS
                }
            }
            string SumberClosing = "LANGSUNG";
            //Response.Redirect("ClosingLangsungGimmick.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS + "&Sumber=" + SumberClosing);
            Response.Redirect("TabelStok4.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS + "&Sumber=" + SumberClosing);
        }

        private void SaveGimmick(string NoKontrak)
        {
            //willy 7 nov 2018
            //save gimmick ambil dari 
            //SELECT * FROM MS_GIMMICK WHERE Tipe = (SELECT ID FROM REF_TIPE_GIMMICK WHERE Nama='TYPE 1 A' AND Project='SVS')
            DataTable rs = Db.Rs("SELECT * FROM MS_GIMMICK WHERE Tipe = (SELECT ID FROM REF_TIPE_GIMMICK WHERE Nama='" + gimmick.Text + "' AND Project='SVS')");
            if (rs.Rows.Count != 0)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    //Response.Write("EXEC spKontrakGimmick"
                    //       + " '" + NoKontrak + "'"
                    //       + ",'" + (i+1) + "'"
                    //       + ",'" + rs.Rows[i]["ItemID"] + "'"
                    //       + ",'" + rs.Rows[i]["Nama"] + "'"
                    //       + ",'" + rs.Rows[i]["Tipe"] + "'"
                    //       + ",'" + rs.Rows[i]["Satuan"] + "'"
                    //       + ",'" + rs.Rows[i]["Stock"] + "'"
                    //       + ",'" + Convert.ToDecimal(rs.Rows[i]["HargaSatuan"]) + "'"
                    //       + ",'" + Convert.ToDecimal(rs.Rows[i]["HargaTotal"]) + "'");

                    Db.Execute("EXEC spKontrakGimmick"
                           + " '" + NoKontrak + "'"
                           + ",'" + (i + 1) + "'"
                           + ",'" + rs.Rows[i]["ItemID"] + "'"
                           + ",'" + rs.Rows[i]["Nama"] + "'"
                           + ",'" + rs.Rows[i]["Tipe"] + "'"
                           + ",'" + rs.Rows[i]["Satuan"] + "'"
                           + ",'" + rs.Rows[i]["Stock"] + "'"
                           + ",'" + Convert.ToDecimal(rs.Rows[i]["HargaSatuan"]) + "'"
                           + ",'" + Convert.ToDecimal(rs.Rows[i]["HargaTotal"]) + "'");
                }
            }
        }

        private void SaveApprove()
        {
            DateTime TglKontrak = Convert.ToDateTime(tglKontrak.Text);
            string Lokasi = Db.SingleString("SELECT Lokasi FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

            nokontrak.Text = Numerator.Approval(TglKontrak.Month, TglKontrak.Year, Project);

            if (nilaiBunga.Text == "") nilaiBunga.Text = "0";

            string Skema = Cf.Str(skema.SelectedItem.Text);
            int NoAgent = Convert.ToInt32(agent.SelectedValue);
            DataTable TargetST = Db.Rs("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'TargetBAST" + Project + Lokasi + "'");
            decimal pl = Convert.ToDecimal(Pricelist.Text);

            string Tujuan = ddlTujuan.SelectedValue;
            string TujuanLainnya = "";
            if (tujuanlain.Text != "")
                TujuanLainnya = tujuanlain.Text;
            string SumberDana = ddlSumberDana.SelectedValue;
            string SumberDanaLainnya = "";
            if (lainnya.Text != "")
                SumberDanaLainnya = lainnya.Text;
            string NUP = Cf.Str(noqueue.Text);

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
            Db.Execute("EXEC spKontrakApprov"
                + " '" + NoKontrak + "'"
                + ",'" + NoStock + "'"
                + ",'" + TglKontrak + "'"
                + ",'" + Skema + "'"
                + ",'" + TargetST.Rows[0]["Value"] + "'"
                + ",'" + NoCustomer + "'"
                + ",'" + NoAgent + "'"
                + ", " + pl
                );
            Db.Execute("UPDATE MS_UNIT SET STATUS = 'H' WHERE NoStock = '" + NoStock + "'");

            int KPR;
            if (carabayar2.SelectedValue == "KPR")
            {
                KPR = 1;
            }
            else
            {
                KPR = 0;
            }

            int CaraBayar = Convert.ToInt32(skema.SelectedValue);
            decimal PPN = 0, Netto = 0;

            string RumusDiskon = diskon2.Text;
            string RumusDiskon2 = Db.SingleString(
                "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + CaraBayar);
            string RumusBunga = bunga2.Text;
            string RumusBunga2 = Db.SingleString("SELECT Bungaket FROM REF_SKEMA WHERE Nomor = " + CaraBayar);

            decimal Gross = Db.SingleDecimal(
                "SELECT isnull(sum(Gross),0) FROM MS_KONTRAK_APPROVAL WHERE NoKontrak = '" + NoKontrak + "'");

            decimal disawal = Db.SingleDecimal("SELECT isnull(sum(DiskonRupiah),0) FROM MS_KONTRAK_APPROVAL WHERE NoKontrak = '" + NoKontrak + "'");

            decimal bung = Db.SingleDecimal("SELECT isnull(sum(BungaNominal),0) FROM MS_KONTRAK_APPROVAL WHERE  NoKontrak = '" + NoKontrak + "'");
            decimal Gross2 = Gross;

            decimal GrossAfterDiskon = Func.SetelahDiskon(RumusDiskon, Gross2);
            decimal GrossAfterBunga = Func.SetelahBunga(RumusBunga, Gross2);
            decimal ND = Gross2 - GrossAfterDiskon;
            decimal NB = Gross2 - GrossAfterBunga;
            decimal HargaSetelahBunga = GrossAfterBunga - ND;

            //Tambahan Richard Harga Tanah dan Bangunan 6 Des 2018
            //decimal HargaTanahAfterBunga = Func.SetelahBunga(RumusBunga, HargaTanah);
            //decimal HargaBangunanAfterBunga = Func.SetelahBunga(RumusBunga, HargaBangunan);
            //End of Tambahan

            Netto = (CaraBayar != 0) ? Func.SetelahDiskon(RumusDiskon, Gross2) : Gross2;

            /* DISKON TAMBAHAN SAAT CLOSING */
            decimal DiskonTambahan = 0;
            if (jenisDiskon.SelectedIndex == 0)
            {   //Diskon lum sum
                DiskonTambahan = Convert.ToDecimal(diskonLumpSum.Text);
            }
            else if (jenisDiskon.SelectedIndex == 1)
            {   //Diskon % bertingkat
                decimal coba = 0, totaldisc = 0;
                string[] DiscTambahPersen = diskontambahPersen.Text.Split('+');
                decimal dpp = HargaSetelahBunga;

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

            Db.Execute("UPDATE MS_KONTRAK_APPROVAL"
                + " SET DiskonTambahan = " + DiskonTambahan
                + " WHERE NoKontrak = '" + NoKontrak + "'");

            Netto -= DiskonTambahan;
            /********************************/

            decimal distambah = Db.SingleDecimal("SELECT isnull(sum(DiskonTambahan),0) FROM MS_KONTRAK_APPROVAL WHERE NoKontrak = '" + NoKontrak + "'");
            decimal afterDiscTambahan = Math.Round(HargaSetelahBunga - distambah); //sebelumnya tanpa Math.round

            decimal NilaiKontrak = afterDiscTambahan;
            //Batam tidak ada PPN jadi di coment Willy
            decimal DPP = NilaiKontrak;// / (decimal)1.1; //sebelumnya dengan Math.round

            Db.Execute("UPDATE MS_KONTRAK_APPROVAL SET NilaiKontrak = " + Convert.ToDecimal(NilaiKontrak)
                + ", DiskonPersen = '" + RumusDiskon + "'"
                + ", DiskonKet = '" + RumusDiskon2 + "'"
                + ", DiskonRupiah = " + Convert.ToDecimal(nilaiDiskon.Text)
                + ", BungaPersen = '" + RumusBunga + "'"
                + ", BungaNominal = " + Convert.ToDecimal(nilaiBunga.Text)
                + ", BungaKet = '" + RumusBunga2 + "'"
                + ", PPN = '" + Cf.BoolToSql(sifatppn.SelectedIndex == 1) + "'"
                + ", PPNBulat = '" + Cf.BoolToSql(roundppn.Checked) + "'"
                + ", DiskonTambahan = " + DiskonTambahan
                + " WHERE NoKontrak = '" + NoKontrak + "'");

            string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
            string Pers = Db.SingleString("SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
            string NamaPers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Pers + "'");
            decimal LuasNett = Db.SingleDecimal("SELECT LuasNett FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            decimal LuasSG = Db.SingleDecimal("SELECT LuasSG FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

            string sSQL = "UPDATE MS_KONTRAK_Approval"
                + " SET JenisKPR = '" + KPR + "'"
                + ", CaraBayar = '" + carabayar2.SelectedValue + "'"
                + ", RefSkema = " + skema.SelectedValue
                + ", LokasiPenjualan = " + lokpen.SelectedValue
                + ", jenisPPN = '" + JenisPPN.SelectedValue + "'"
                + ", SumberDana ='" + SumberDana + "'"
                + ", SumberDanaLainnya ='" + Cf.Str(SumberDanaLainnya) + "'"
                + ", TujuanKontrak = '" + Tujuan + "'"
                + ", LuasNett = '" + LuasNett + "'"
                + ", LuasSG = '" + LuasSG + "'"
                + ", Project = '" + Project + "'"
                + ", NamaProject = '" + NamaProject + "'"
                + ", Pers = '" + Pers + "'"
                + ", NamaPers = '" + NamaPers + "'"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                ;
            Db.Execute(sSQL);

            Db.Execute("EXEC spTagihanBalance"
              + "'" + NoKontrak + "'"
              );

            DataTable rs2 = Db.Rs("SELECT "
                + " MS_KONTRAK_Approval.NoKontrak AS [No. Kontrak]"
                + ",MS_KONTRAK_Approval.NoUnit AS [Unit]"
                + ",MS_CUSTOMER.Nama AS [Customer]"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS [Agent]"
                + ",CONVERT(varchar,MS_KONTRAK_Approval.TglKontrak,106) AS [Tanggal Kontrak]"
                + ",MS_KONTRAK_Approval.NoStock AS [No. Stock]"
                + ",MS_KONTRAK_Approval.LuasNett AS [Luas]"
                + ",MS_KONTRAK_Approval.Gross AS [Nilai Gross]"
                + ",MS_KONTRAK_Approval.DiskonRupiah AS [Diskon dalam Rupiah]"
                + ",MS_KONTRAK_Approval.NilaiKontrak AS [Nilai Kontrak]"
                + ",MS_KONTRAK_Approval.BungaPersen AS [Bunga dalam Persen]"
                + ",MS_KONTRAK_Approval.BungaNominal AS [Bunga dalam Rupiah]"
                + ",MS_KONTRAK_Approval.Skema"
                + ",CONVERT(varchar,MS_KONTRAK_Approval.TargetST,106) AS [Jadwal Serah Terima]"
                + ", MS_KONTRAK_Approval.JenisPPN AS [PPN Ditanggung]"
                + ", CASE MS_KONTRAK_Approval.JenisKPR"
                + "		WHEN 0 THEN 'KPR'"
                + "		WHEN 1 THEN 'NON-KPR'"
                + "	END AS [Jenis KPR]"
                + ",MS_KONTRAK_Approval.SumberDana AS [Sumber Dana]"
                + ",MS_KONTRAK_Approval.SumberDanaLainnya AS [Sumber Dana Lainnya]"
                + ",MS_KONTRAK_Approval.TujuanKontrak AS [Tujuan Transaksi]"
                + " FROM MS_KONTRAK_Approval INNER JOIN MS_CUSTOMER ON MS_KONTRAK_Approval.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_KONTRAK_Approval.NoAgent = MS_AGENT.NoAgent"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            DataTable rsTagihan = Db.Rs("SELECT "
                + "CONVERT(VARCHAR,SN) + '.   ' + Nama + ' ('+TitleJabatan+') ' "
                + "FROM ms_kontrak_approval_detail WHERE NoKontrak = '" + NoKontrak + "' ORDER BY SN");

            string Ket = Cf.LogCapture(rs2)
                + Cf.LogList(rsTagihan, "Kontrak Approval");

            Db.Execute("EXEC spLogKontrakApprov"
                + " 'DAFTAR'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + NoKontrak + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_APP_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE MS_KONTRAK_APP_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            //TTS
            int NoTTS = 0;
            if (Convert.ToDecimal(nilai.Text) != 0)
            {
                NoTTS = SaveTTS(NoKontrak
                    , Db.SingleInteger("SELECT NoCustomer FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'"));
            }

            string SumberClosing = "APPROVE";
            Response.Redirect("ClosingLangsungGimmick.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS + "&Sumber=" + SumberClosing);
            //Response.Redirect("ClosingLangsungApprov.aspx?NoKontrak=" + NoKontrak + "&No");
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
                string RumusBunga = Cf.Str(bunga2.Text);

                decimal Gross = Db.SingleDecimal(
                     "SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
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
                decimal DiskonRupiah = Convert.ToDecimal(nilaiDiskon.Text);

                Db.Execute("UPDATE MS_KONTRAK"
                 + " SET DiskonKet='" + RumusDiskon2 + "'"
                 + ", DiskonPersen='" + DiskonPersen + "'"
                 + ", DiskonRupiah = " + DiskonRupiah
                 + " WHERE NoKontrak = '" + NoKontrak + "'");

                /***********/

                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                string Pers = Db.SingleString("SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                string NamaPers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Pers + "'");

                string ParamID = "PLIncludePPN" + Project;
                bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";

                decimal NilaiPPN = 0;
                decimal NilaiKontrak = 0;
                decimal DPP = 0;
                //Di coment karena di batam tidak ad PPN willy
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

                //Tambahan Richard Harga Tanah dan Bangunan 6 Des 2018
                //decimal HargaTanahAfterBunga = Func.SetelahBunga(RumusBunga, HargaTanah);
                //decimal HargaBangunanAfterBunga = Func.SetelahBunga(RumusBunga, HargaBangunan);
                //End of Tambahan

                //update bunga manual,gapake sp
                Db.Execute("UPDATE MS_KONTRAK SET"
                    + " NilaiKontrak = '" + NilaiKontrak + "'"
                    + ",BungaPersen = '" + RumusBunga + "'"
                    + ",BungaNominal = '" + nilaiBunga.Text + "'"
                    + ",FlagGross = 0"
                    + ",TglEdit = '" + DateTime.Today + "'"
                    //+ ", HargaTanah = " + HargaTanahAfterBunga
                    //+ ", HargaBangunan = " + HargaBangunanAfterBunga
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );

                decimal NilaiTagihan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND Tipe IN('BF', 'DP', 'ANG')");
                decimal OutBalance = NilaiKontrak - NilaiTagihan;

                Db.Execute("UPDATE MS_KONTRAK SET OutBalance = '" + OutBalance + "' WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_CUSTOMER SET TglTransaksi = '" + DateTime.Today + "' WHERE NoCustomer = (SELECT NoCustomer FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "')");
                //Db.Execute("EXEC spKontrakBunga"
                //   + " '" + NoKontrak + "'"
                //   + ", " + Gross2
                //   + ", " + NilaiKontrak
                //   + ",'" + RumusBunga + "'"
                //   );

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
                    decimal PersenPokok = Math.Round(Convert.ToDecimal(rs2.Rows[0]["PriceList"]) / NilaiInclude * 100, 2);
                    decimal PersenPPN = Math.Round(PPN / NilaiInclude * 100, 2, MidpointRounding.AwayFromZero);
                    decimal PersenBPHTB = Math.Round(BiayaBPHTB / NilaiInclude * 100, 2, MidpointRounding.AwayFromZero);
                    decimal PersenSurat = Math.Round(BiayaSurat / NilaiInclude * 100, 2, MidpointRounding.AwayFromZero);
                    decimal PersenProses = Math.Round(BiayaProses / NilaiInclude * 100, 2, MidpointRounding.AwayFromZero);
                    decimal PersenLain = Math.Round(BiayaLainLain / NilaiInclude * 100, 2, MidpointRounding.AwayFromZero);

                    decimal SisaPersen = 100 - (PersenPokok + PersenPPN + PersenBPHTB + PersenSurat + PersenProses + PersenLain);
                    if (SisaPersen > 0)
                    {
                        PersenPokok = PersenPokok + SisaPersen;
                    }

                    Db.Execute("UPDATE MS_KONTRAK SET PersenPokok = " + PersenPokok
                            + ", PersenPPN = " + PersenPPN
                            + ", PersenBPHTB = " + PersenBPHTB
                            + ", PersenSurat = " + PersenSurat
                            + ", PersenProses = " + PersenProses
                            + ", PersenLain = " + PersenLain
                            + " WHERE NoKontrak = '" + NoKontrak + "'"
                            );
                }
                else
                {
                    //tambah tagihan bphtb
                    decimal bphtb = Db.SingleDecimal("SELECT BiayaBPHTB FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

                    DateTime TglJT = DateTime.Today;
                    int tipe = Db.SingleInteger("SELECT COUNT(Tipe) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND Tipe = 'DP'");
                    if (tipe > 0)
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


            Db.Execute("UPDATE MS_KONTRAK"
                + " SET "
                + " ClosingID = '" + esales.SelectedValue.ToString() + "'"
                + " WHERE NoKontrak = '" + NoKontrak + "'");
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

            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET Acc = '" + ddlAcc.SelectedValue + "',Project = '" + Project + "' WHERE NoTTS ='" + NoTTS + "'");

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
                + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);
            return NoTTS;
        }
        //NUP
        private int SaveTTS2(string NoNUP, int NoCustomer)
        {
            DataTable rsPayment = Db.Rs("SELECT B.* FROM [NUP03]..CustomerNUP A"
                + " INNER JOIN [NUP03]..CustomerPayment B ON A.NoPayment = B.NoPayment"
                + " WHERE NoNUP = '" + NoNUP + "'");
            DateTime TglTTS = Convert.ToDateTime(tglKontrak.Text);
            string Unit = Cf.Str(unit.Text);
            string Customer = Cf.Str(Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer));
            string CaraBayar = "TR";
            decimal Nilai = Convert.ToDecimal(rsPayment.Rows[0]["NilaiTransaksi"].ToString());
            string Ket = Cf.Str(rsPayment.Rows[0]["Keterangan"].ToString());
            ddlAcc.SelectedValue = "110201001";
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
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");

            //Numerator
            string NoTTS2 = Numerator.TTS(TglTTS.Month, TglTTS.Year, Project);
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET Acc = '" + ddlAcc.SelectedValue + "',NoTTS2='" + NoTTS2 + "',Project = '" + Project + "' WHERE NoTTS ='" + NoTTS + "'");

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
                + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);
            return NoTTS;
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

        private void SaveFO()
        {
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
            //nilaiBunga.Text = bunga2.Text = "0";
        }

        protected void bunga2_TextChanged(object sender, EventArgs e)
        {
            if (carabayar.SelectedIndex > 0)
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
            CaraBayar();
            decimal pl = Db.SingleDecimal("SELECT ISNULL(PriceList, 0) FROM ISC064_MARKETINGJUAL..MS_PRICELIST WHERE NoSkema='" + skema.SelectedValue + "' AND NoStock='" + NoStock + "'");
            Pricelist.Text = Cf.Num(pl);
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


                if (CaraBayar2.Contains("CASH KERAS") == true)
                {
                    carabayar2.SelectedIndex = 0;
                    carabayar2.Enabled = false;
                }
                else if (CaraBayar2.Contains("CASH BERTAHAP") == true)
                {
                    carabayar2.SelectedIndex = 1;
                    carabayar2.Enabled = false;
                }
                else if (CaraBayar2.Contains("KPR") == true)
                {
                    carabayar2.SelectedIndex = 2;
                    carabayar2.Enabled = false;
                }
                else
                {
                    carabayar2.ClearSelection();
                    carabayar2.Enabled = false;
                }

                carabayar2.Enabled = false;
                diskon2.Text = RumusDiskon;
                //nilaiDiskon.Text = Cf.Num((Convert.ToDecimal(RumusDiskon) * Convert.ToDecimal(Pricelist.Text) /100));
                diskonket.Text = Ket;
                bunga2.Text = RumusBunga;
                bungaket.Text = Ket2;

                SetBunga();
                SetDiskon();
                //SetTanah();
                //SetBangunan();
            }
        }

        private void CaraBayar()
        {
            string CaraBayar2 = Db.SingleString(
                    "SELECT Jenis FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);

            if (skema.SelectedIndex > 0)
            {
                if (CaraBayar2.Contains("CASH KERAS") == true)
                {
                    carabayar2.SelectedIndex = 0;
                    carabayar2.Enabled = false;
                }
                else if (CaraBayar2.Contains("CASH BERTAHAP") == true)
                {
                    carabayar2.SelectedIndex = 1;
                    carabayar2.Enabled = false;
                }
                else if (CaraBayar2.Contains("KPR") == true)
                {
                    carabayar2.SelectedIndex = 2;
                    carabayar2.Enabled = false;
                }
                else
                {
                    carabayar2.ClearSelection();
                    carabayar2.Enabled = false;
                }
            }

            carabayar2.Enabled = false;
        }


        // DISKON ---------------
        private void SetDiskon()
        {
            decimal Gross = Convert.ToDecimal(Pricelist.Text);
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

        private void SetDiskon2()
        {
            decimal Gross = Convert.ToDecimal(Pricelist.Text);
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


        // BUNGA -------

        private void SetBunga()
        {
            decimal Gross = Convert.ToDecimal(Pricelist.Text); //Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
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

            decimal bunga = Func.NominalBunga(RumusBunga, Gross2);
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
            //+ " WHERE NoStock = '" + NoStock + "'");; + surcharge;

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

        private string NoCustomer
        {
            get
            {
                return Cf.Pk(nocustomer.Text);
            }
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }

        private string Project
        {
            get
            {
                return Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
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

        //Common Driver
        protected void Execute(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCnn.Close();
        }
        protected DataTable Rs(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql, sqlCnn);
            DataSet objDS = new DataSet();
            sqlAdapter.Fill(objDS, "data");
            sqlCnn.Close();

            DataTable rs = new DataTable();
            rs = objDS.Tables["data"];

            return rs;
        }
        protected string SingleString(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            string x = "";
            x = (string)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected int SingleInteger(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            int x = (int)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected long SingleLong(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            long x = Convert.ToInt64(sqlCmd.ExecuteScalar());
            sqlCnn.Close();

            return x;
        }
        protected decimal SingleDecimal(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            decimal x = Convert.ToDecimal(sqlCmd.ExecuteScalar());
            sqlCnn.Close();

            return x;
        }
        protected bool SingleBool(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            bool x = (bool)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected byte SingleByte(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            byte x = (byte)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected DateTime SingleTime(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            DateTime x = (DateTime)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
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



        protected void pldef_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill();
        }
    }
}
