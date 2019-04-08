using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class Closing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {


                InitForm();
                Fill();

                frm.Visible = divPersenBertingkat.Visible = false;

                divPersenBertingkat.Visible = false;
                divLumpSum.Visible = true;
                pilih.Visible = false;
                frm.Visible = true;

                FillForm();
                //if (Request.QueryString["NoNUP"] != null)
                //{
                //    //nocustomer.Text = Request.QueryString["NoCustomer"];
                //    LoadCustomer();

                //}
                nostock.Text = Db.SingleString(
                            "SELECT NoStock FROM MS_UNIT WHERE NoUnit = '" + Cf.Pk(unit.Text) + "'");
                tglKontrak.Text = Cf.Day(DateTime.Today);

                Surcharge.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                Surcharge.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                Surcharge.Attributes["onblur"] = "CalcBlur(this);";

                nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                nilai.Attributes["onblur"] = "CalcBlur(this);";

                bktambah.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                bktambah.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                bktambah.Attributes["onblur"] = "CalcBlur(this);";

                //btng.Attributes["onclick"] = "popDaftarCustomer('a');";
                //grouping.Attributes.Add("readonly", "readonly");

            }
        }

        protected void GantiTipeSales(object sender, System.EventArgs e)
        {
            string Tipe = Db.SingleString("SELECT Tipe FROM MS_AGENT WHERE NoAgent = '" + agent.SelectedValue + "'");
            reff.Visible = Tipe == "INHOUSE" ? true : false;
        }

        private void InitForm()
        {

            Pricelist.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            Pricelist.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            Pricelist.Attributes["onblur"] = "CalcBlur(this);";

            sisa.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            sisa.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            sisa.Attributes["onblur"] = "CalcBlur(this);";

            nilaiDiskon.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaiDiskon.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaiDiskon.Attributes["onblur"] = "CalcBlur(this);";

            diskonLumpSum.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            diskonLumpSum.Attributes["onkeyup"] = "CalcType(this,tempnum); hitungbphtb(diskonLumpSum,sisa,bphtb); CalcBlur(bphtb);";
            diskonLumpSum.Attributes["onblur"] = "CalcBlur(this);";

            bphtb.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            bphtb.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            bphtb.Attributes["onblur"] = "CalcBlur(this);";

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

            rs = Db.Rs("SELECT Nama,NoAgent FROM MS_AGENT WHERE Status = 'A' AND Tipe = 'REFFERAL'"
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

            //persentingkat.Visible = false;
            //persenBunga.Visible = false;

            string strSql;

            //BankKPR
            strSql = "SELECT * FROM REF_BANKKPA ORDER BY KodeBank";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["KodeBank"].ToString();
                string t = rs.Rows[i]["Bank"].ToString();
                bankkpr.Items.Add(new ListItem(t));
            }
            bankkpr.SelectedIndex = 0;

            //Cara bayar
            DataTable rs2 = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' ORDER BY Nama");
            //skema.Items.Add(new ListItem("*** CUSTOMIZE / PENDING", "0")); //cara bayar customize

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

            //Sumber Data
            strSql = "SELECT * FROM REF_EVENT ORDER BY SN";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["SN"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                sumberdata.Items.Add(new ListItem(t, v));
            }
            sumberdata.SelectedIndex = 0;

            fo.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            fo.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            fo.Attributes["onblur"] = "CalcBlur(this);";

            focounter.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            focounter.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            focounter.Attributes["onblur"] = "CalcBlur(this);";
        }

        private void Fill()
        {
            cancel.Attributes["onclick"] = "location.href = 'ClosingLangsungDaftar2.aspx?NoStock=" + NoStock + "'";
            btnbaru.Attributes["onclick"] = "location.href = 'ClosingLangsungDaftar.aspx?NoStock=" + NoStock + "'";
            //Data Unit
            DataTable rs = Db.Rs("SELECT Luas,PriceList,NoUnit,LuasSG,LuasNett FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                string unitpesanan = unit.Text = rs.Rows[0]["NoUnit"].ToString();
                luastanah.Text = Cf.Num(rs.Rows[0]["LuasSG"]);
                luasbangunan.Text = Cf.Num(rs.Rows[0]["LuasNett"]);
                Pricelist.Text = Cf.Num(rs.Rows[0]["PriceList"]);
                string lantai = unitpesanan.Substring(3, 2);
                nounit.Text = "<a href=\"javascript:popUnit('" + NoStock + "')\">"
                    + rs.Rows[0]["NoUnit"] + "</a>";

                if (lantai == "08" || lantai == "09")
                {
                    trsurcharge.Visible = true;
                    Surcharge.Text = "0";
                }
                else
                {
                    trsurcharge.Visible = false;
                    Surcharge.Text = "0";
                }
                decimal BPHTB = Math.Round(((Convert.ToDecimal(rs.Rows[0]["PriceList"]) / (decimal)1.1) - 60000000) * (decimal)0.05);
                bphtb.Text = Cf.Num(BPHTB);

                string CaraBayar = Db.SingleString(
                    "SELECT Jenis FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                carabayar2.SelectedValue = CaraBayar;

                string RumusDiskon = Db.SingleString("SELECT Diskon FROM REF_SKEMA WHERE Nomor = '" + skema.SelectedValue + "'");
                diskon2.Text = RumusDiskon;
                string Ket = Db.SingleString("SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = '" + skema.SelectedValue + "'");
                diskonket.Text = Ket;

                string RumusBunga = Db.SingleString("SELECT Bunga FROM REF_SKEMA WHERE Nomor = '" + skema.SelectedValue + "'");
                persenBunga.Text = RumusBunga;
                string Ket2 = Db.SingleString("SELECT Bungaket FROM REF_SKEMA WHERE Nomor = '" + skema.SelectedValue + "'");
                bungaket.Text = Ket2;
            }

        }

        private bool unitvalid()
        {
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
            //if (csvalid())
            //{
            pilih.Visible = true;
            frm.Visible = true;

            FillForm();

            //Js.Focus(this, nilai);
            //}
            //else
            //{
            //    Js.Focus(this, nocustomer);
            //    frm.Visible = false;
            //}
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
            DataTable rs = Db.Rs("SELECT * FROM MS_NUP WHERE NoNUP ='" + NoNUP + "'");

            string namacustomer = rs.Rows[0]["NamaCustomer"].ToString();
            customer.Text = namacustomer;
            noqueue.Text = rs.Rows[0]["NoNUP"].ToString();
            string KodeMarketing = rs.Rows[0]["KodeMarketing"].ToString();
            int NoAgent = 0;
            NoAgent = Db.SingleInteger("SELECT NoAgent FROM MS_AGENT WHERE KodeMarketing ='" + KodeMarketing + "'");

            agent.Text = NoAgent.ToString();
            agent.Enabled = false;
            // sumberdata.Enabled = false;


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

            DateTime TglKontrak = DateTime.Now.AddDays(-30);

            //if (Convert.ToDateTime(tglKontrak.Text) < TglKontrak)
            //{
            //    x = false;
            //    if (s == "") s = tglKontrak.ID;
            //    tglkontrakc2.Text = "Tanggal melebihi 12 hari";
            //}
            //else
            //    tglkontrakc2.Text = "";

            if (!unitvalid())
            {
                x = false;
                if (s == "") s = unit.ID;
                unitc.Text = "Tidak Available";
            }
            else
                unitc.Text = "";

            if (carabayar2.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = carabayar2.ID;
                carabayarc.Text = "Pilih salah satu jenis";
            }
            else
                carabayarc.Text = "";

            if (!Cf.isMoney(Surcharge))
            {
                x = false;
                if (s == "") s = Surcharge.ID;
                surchargec.Text = "Angka";
            }
            else
                surchargec.Text = "";

            int nkm = 0;
            if (nokontrakmanual.Text != "")
            {
                nkm = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrakManual='" + nokontrakmanual.Text + "'");
                if (nkm > 0)
                {
                    x = false;
                    nokontrakmanualc.Text = "duplikat";
                }
                else
                    nokontrakmanualc.Text = "";
            }
            //if (diskon2.Text != "")
            //{
            //    if (!Cf.isMoney(diskon2))
            //    {
            //        x = false;
            //        if (s == "") s = diskon2.ID;
            //        diskon2c.Text = "Angka";
            //    }
            //    else
            //        diskon2c.Text = "";
            //}

            //if (Cf.isEmpty(noqueue))
            //{
            //    x = false;
            //    if (s == "") s = noqueue.ID;
            //    noqueuec.Text = "Kosong";
            //}
            //else
            //    noqueuec.Text = "";

            //if (ddlAcc.SelectedIndex == 0)
            //{
            //    x = false;

            //    if (s == "")
            //        s = ddlAcc.ID;

            //    ddlAccErr.Text = "Harus dipilih";
            //}
            //else
            //    ddlAccErr.Text = "";

            //string tglk = tglKontrak.Text;
            //string[] tglko = tglk.Split(' ');
            //if (tglko[1] != "Jan" && tglko[1] != "Feb" && tglko[1] != "Mar" && tglko[1] != "Apr" && tglko[1] != "May" && tglko[1] != "Jun" && tglko[1] != "Jul" && tglko[1] != "Aug" && tglko[1] != "Sep" && tglko[1] != "Oct" && tglko[1] != "Nov" && tglko[1] != "Dec")
            //{
            //    x = false;
            //    if (s == "") s = tglKontrak.ID;
            //    tglkontrakc2.Text = "In English";
            //}
            //else
            //{
            //    tglkontrakc2.Text = "";
            //}

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
                    //+ "7. NUP tidak boleh Kosong."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private void AutoID()
        {
            string u = Db.SingleString("SELECT Lokasi FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            int c = Db.SingleInteger("SELECT COUNT(NoKontrak) FROM MS_KONTRAK WHERE Lokasi = '" + u + "'");
            int c2 = Db.SingleInteger("SELECT SN FROM REF_LOKASI WHERE Lokasi = '" + u + "'") - 1;

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c += 1;
                nokontrak.Text = c.ToString() + "/SPU-" + u + "/" + Cf.Roman(Convert.ToDateTime(tglKontrak.Text).Month) + "/" + Convert.ToDateTime(tglKontrak.Text).Year;// c.ToString().PadLeft(7, '0') +"/ADM-MKT/CL-" + Cf.Roman(Convert.ToDateTime(tglKontrak.Text).Month) + "/" + Convert.ToDateTime(tglKontrak.Text).Year;
                //nokontrakmanual.Text = c2.ToString() + c.ToString().PadLeft(6, '0');

                if (isUnique()) hasfound = true;
            }
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
            if (valid())
            {
                AutoID();

                //if (nilaiBunga.Text == "") nilaiBunga.Text = "0";

                DateTime TglKontrak = Convert.ToDateTime(tglKontrak.Text);
                string Skema = Cf.Str(skema.SelectedItem.Text);
                int NoAgent = Convert.ToInt32(agent.SelectedValue);
                string Supervisor = "";
                //Supervisor = Db.SingleString("SELECT Spv FROM MS_AGENT WHERE NoAgent = '" + NoAgent + "'");
                DateTime TargetST = DateTime.Today.AddYears(3);
                decimal surcharge = Convert.ToDecimal(Surcharge.Text);
                decimal lt = Convert.ToDecimal(luastanah.Text);
                decimal lb = Convert.ToDecimal(luasbangunan.Text);
                decimal pl = Convert.ToDecimal(Pricelist.Text);

                string Tujuan = ddlTujuan.SelectedValue;
                string SumberDana = ddlSumberDana.SelectedValue;
                string SumberDanaLainnya = "";
                if (lainnya.Text != "")
                    SumberDanaLainnya = lainnya.Text;
                //string SumberData = sumberdata.SelectedValue;
                string NUP = Cf.Str(noqueue.Text);

                decimal bkt = Convert.ToDecimal(bktambah.Text);
                decimal BPHTB = Convert.ToDecimal(bphtb.Text);

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
                string DiskonKet = Cf.Str(diskonket.Text);

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
                string BankKPR = "";
                if (carabayar2.SelectedValue == "KPR")
                {
                    KPR = 1;
                    BankKPR = bankkpr.SelectedValue;
                }
                else
                {
                    KPR = 0;
                }

                if (grouping.Text != "")
                {
                    int NG = Convert.ToInt32(nomorgroup.Value);
                    Db.Execute("UPDATE MS_KONTRAK SET NoGrouping = " + NG);
                }

                if (nokontrakmanual.Text != "")
                    Db.Execute("UPDATE MS_KONTRAK SET DataLama = 1 where NoKontrak='" + NoKontrak + "'");

                string sSQL = "UPDATE MS_KONTRAK"
                    + " SET JenisKPR = '" + KPR + "'"
                    + ", CaraBayar = '" + carabayar2.SelectedValue + "'"
                    + ", RefSkema = " + skema.SelectedValue
                    // + ", Surcharge ='" + surcharge + "'"
                    + ", jenisPPN = '" + JenisPPN.SelectedValue + "'"
                    // + ", DiscTambahPersen = '" + diskontambahPersen.Text + "'"
                    + ", SumberDana='" + SumberDana + "'"
                    + ", SumberDanaLainnya='" + Cf.Str(SumberDanaLainnya) + "'"
                    + ", TujuanKontrak = '" + Tujuan + "'"
                    + ", NUP = '" + NUP + "'"
                    + ", NoKontrakManual = '" + NoKontrakManual + "'"
                    + ", NoRefferatorAgent = '" + ReffAgent1 + "'"
                    + ", NoRefferatorCustomer = '" + ReffAgent2 + "'"
                    //+ ", BiayaKerjaTambah = " + bkt
                    //+ ", BiayaBPHTB = " + BPHTB
                    + ", KetInternal = '" + Cf.Str(ketin.Text) + "'"
                    + ", KetExternal = '" + Cf.Str(ketex.Text) + "'"
                    + ", BankKPR = '" + BankKPR + "'"
                    + ", SumberData = '" + sumberdata.SelectedValue + "'"
                    + ", LuasTanah = '" + lt + "'"
                    + ", LuasBangunan = '" + lb + "'"
                    + ", Spv = '" + Supervisor + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    ;

                Db.Execute(sSQL);
                //Response.Write(NoKontrak);
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
                          + ",MS_KONTRAK.LuasTanah AS [Luas Tanah]"
                          + ",MS_KONTRAK.LuasBangunan AS [Luas Bangunan]"
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
                          + "	END AS [Jenis KPR]"
                          + ",MS_KONTRAK.SumberDana AS [Sumber Dana]"
                          + ",MS_KONTRAK.SumberDanaLainnya AS [Sumber Dana Lainnya]"
                          + ",MS_KONTRAK.SumberData AS [Sumber Data]"
                          + ",MS_KONTRAK.TujuanKontrak AS [Tujuan Transaksi]"
                          + ",MS_KONTRAK.NUP"
                          + ",MS_KONTRAK.NoRefferatorAgent"
                          + ",MS_KONTRAK.NoRefferatorCustomer"
                    //+ ",MS_KONTRAK.BiayaKerjaTambah AS [Biaya Kerja Tambah]"
                    //+ ",MS_KONTRAK.BiayaBPHTB AS [Biaya BPHTB]"
                          + ",MS_KONTRAK.KetInternal AS [Keterangan Internal]"
                          + ",MS_KONTRAK.KetExternal AS [Keterangan External]"
                          + ",MS_KONTRAK.BankKPR AS [Bank KPR]"
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

                Response.Redirect("TabelStok4.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS);

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
                + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "')");
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            return NoTTS;
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

        private void SaveTagihan()
        {

            int CaraBayar = Convert.ToInt32(skema.SelectedValue);
            decimal PPN = 0, Netto = 0;
            //cara bayar 0 = customize
            //if (CaraBayar != 0)
            //{
            string RumusDiskon = diskon2.Text;
            string RumusDiskon2 = Db.SingleString(
                "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + CaraBayar);
            string DiskonKet = Cf.Str(diskonket.Text);
            string BungaPersen = persenBunga.Text;
            // string RumusBunga2 = "";//Db.SingleString("SELECT Bungaket FROM REF_SKEMA WHERE Nomor = " + CaraBayar);

            decimal Gross = Db.SingleDecimal(
                "SELECT isnull(sum(Gross),0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            // decimal surcharge = 0;// Convert.ToDecimal(Surcharge.Text);
            // decimal disawal = Db.SingleDecimal("SELECT isnull(sum(DiskonRupiah),0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            //decimal bung = Db.SingleDecimal("SELECT isnull(sum(BungaNominal),0) FROM MS_KONTRAK WHERE  NoKontrak = '" + NoKontrak + "'");
            decimal Gross2 = Gross;

            Decimal TotalBunga = 0;
            //if (rdBunga.SelectedIndex == 0)
            //{	//Bunga lum sum
            //    TotalBunga = Convert.ToDecimal(lsbunga.Text);
            //}
            if (persenBunga.Text != "")
            {	//Bunga % bertingkat
                decimal coba = 0, totalbunga = 0;
                string[] BungaTambahPersen = persenBunga.Text.Split('+');
                decimal dpp = Gross2;

                if (persenBunga.Text != "")
                {
                    for (int a = 0; a <= BungaTambahPersen.GetUpperBound(0); a++)
                    {
                        coba = Math.Round(Convert.ToDecimal(BungaTambahPersen[a]) * dpp / (decimal)100);
                        dpp += coba;
                        totalbunga += coba;
                    }
                }
                else
                {
                    totalbunga = 0;
                }

                TotalBunga = totalbunga;
            }

            Gross2 += TotalBunga;

            decimal GrossAfterDiskon = Math.Round(Func.SetelahDiskon(RumusDiskon, Gross2));
            //decimal GrossAfterBunga = Func.SetelahBunga(RumusBunga, Gross2);
            decimal HargaSetelahBunga = GrossAfterDiskon;
            decimal NilaiDiskon = Math.Round(Gross2 - GrossAfterDiskon);
            Netto = HargaSetelahBunga;//(CaraBayar != 0) ? Func.SetelahDiskon(RumusDiskon, Gross) : Gross; 

            // //decimal diskontambah = Convert.ToDecimal(diskontambahan.Text);

            // /* DISKON TAMBAHAN SAAT CLOSING */
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
                + ", DiscTambahPersen = '" + diskontambahPersen.Text + "'"
                + ", DiscTambahKet = '" + Cf.Str(diskontambahKet.Text) + "'"
                + " WHERE NoKontrak = '" + NoKontrak + "'");

            Netto -= DiskonTambahan;
            /********************************/

            decimal distambah = Db.SingleDecimal("SELECT isnull(sum(DiskonTambahan),0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            decimal afterDiscTambahan = Netto;// -distambah;
            //decimal DPP = Math.Round(afterDiscTambahan / (decimal)1.1);

            decimal DPP = afterDiscTambahan;
            decimal NPPN = Math.Floor(DPP * (decimal)0.1);
            decimal NilaiKontrak = DPP + NPPN;
            decimal NilaiPPN = 0;
            decimal PPNDitanggungPemerintah = 0;

            if (JenisPPN.SelectedValue == "KONSUMEN")
            {
                PPNDitanggungPemerintah = 0;
                //NilaiPPN = (DPP * (decimal)0.1)-PPNDitanggungPemerintah;
                NilaiPPN = NPPN;
                //NilaiKontrak = DPP + NilaiPPN;

            }
            else
            {
                PPNDitanggungPemerintah = Math.Floor(DPP * (decimal)0.1);
                //NilaiPPN = ((DPP * (decimal) 0.1))-PPNDitanggungPemerintah;
                NilaiPPN = 0;
                //NilaiKontrak = DPP + NilaiPPN;
            }

            Db.Execute("EXEC spKontrakDiskon"
               + " '" + NoKontrak + "'"
               + ", " + Gross
               + ", " + NilaiKontrak
               + ", " + NilaiDiskon//Convert.ToDecimal(nilaiDiskon.Text)
               + ",'" + RumusDiskon + "'"
               + ",'" + Cf.Str(DiskonKet) + "'"
               );

            Db.Execute("UPDATE MS_KONTRAK"
                + " SET "
                + " NilaiDPP = " + DPP
                + " ,NilaiPPN = " + NilaiPPN
                + ", BungaNominal = " + TotalBunga
                + ", BungaPersen = '" + BungaPersen + "'"
                + ", BungaKet = '" + Cf.Str(bungaket.Text) + "'"
                // + " , PPNPemerintah = " + PPNDitanggungPemerintah
                + " WHERE NoKontrak = '" + NoKontrak + "'");

            //Biaya BPHTB
            decimal BPHTB = 0;
            if (cekbphtb.Checked == true)
            {
                BPHTB = Math.Round((DPP - (decimal)60000000) * (decimal)0.05);

                Db.Execute("UPDATE MS_KONTRAK SET"
                    + " BiayaBPHTB = '" + BPHTB + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
            }

            decimal nilaitg = Db.SingleDecimal("select isnull(sum(nilaikontrak),0) from ms_kontrak WHERE NoKontrak = '" + NoKontrak + "'");

            string[,] x = Func.Breakdown(CaraBayar, nilaitg, Convert.ToDateTime(tglKontrak.Text));
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

            //Save Tagihan BPHTB
            if (BPHTB != 0)
            {
                Db.Execute("EXEC spTagihanDaftar "
                    + " '" + NoKontrak + "'"
                    + ",'BIAYA ADM. BPHTB'"
                    + ",'" + Convert.ToDateTime(tglKontrak.Text).AddMonths(1) + "'"
                    + ", " + BPHTB
                    + ",'ADM'"
                    );
            }

            ////Biaya Kerja Tambah
            //decimal BKT = Convert.ToDecimal(bktambah.Text);
            //decimal PPNBKT = 0;
            //if (sifatppn2.SelectedIndex == 1)
            //{
            //    if (includeppn2.Checked)
            //    {
            //        if (roundppn2.Checked)
            //        {
            //            PPNBKT = BKT - Math.Round(BKT / (decimal)1.1);
            //        }
            //        else
            //        {
            //            PPNBKT = BKT - (BKT / (decimal)1.1);
            //        }
            //    }
            //    else
            //    {
            //        if (roundppn2.Checked)
            //            PPNBKT = Math.Round((decimal)0.1 * (BKT / (decimal)1.1));
            //        else
            //            PPNBKT = (decimal)0.1 * (BKT / (decimal)1.1);
            //    }
            //}

            //decimal NBKT = BKT - PPNBKT;
            //Db.Execute("UPDATE MS_KONTRAK SET PPNBiayaKerjaTambah='" + PPNBKT + "'"
            //    + ", BiayaKerjaTambah = '" + NBKT + "'"
            //    + " WHERE NoKontrak = '" + NoKontrak + "'"
            //    );

            //Jadwal Tagihan Biaya Kerja Tambah
            //SaveTagihanBKT();
            //}
            //else
            //{
            //    string RumusBunga = bunga2.Text;
            //    string RumusBunga2 = "";//Db.SingleString("SELECT Bungaket FROM REF_SKEMA WHERE Nomor = " + CaraBayar);

            //    decimal Gross = Db.SingleDecimal(
            //        "SELECT isnull(sum(Gross),0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            //    decimal surcharge = Convert.ToDecimal(Surcharge.Text);
            //    decimal disawal = Db.SingleDecimal("SELECT isnull(sum(DiskonRupiah),0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            //    decimal bung = Db.SingleDecimal("SELECT isnull(sum(BungaNominal),0) FROM MS_KONTRAK WHERE  NoKontrak = '" + NoKontrak + "'");
            //    decimal Gross2 = Gross;

            //    decimal GrossAfterDiskon = Gross2;
            //    decimal GrossAfterBunga = Func.SetelahBunga(RumusBunga, Gross2);
            //    decimal HargaSetelahBunga = GrossAfterDiskon + bung;
            //    decimal NilaiPPN = 0;
            //    decimal NilaiKontrak = 0;
            //    decimal hrgsetelahdisc = 0;
            //    decimal PPNDitanggungPemerintah = 0;
            //    Netto = Gross;

            //    //decimal diskontambah = Convert.ToDecimal(diskontambahan.Text);

            //    /* DISKON TAMBAHAN SAAT CLOSING */



            //    decimal DiskonTambahan = 0;
            //    if (jenisDiskon.SelectedIndex == 0)
            //    {	//Diskon lum sum
            //        DiskonTambahan = Convert.ToDecimal(diskonLumpSum.Text);
            //    }
            //    else if (jenisDiskon.SelectedIndex == 1)
            //    {	//Diskon % bertingkat
            //        decimal coba = 0, totaldisc = 0;
            //        string[] DiscTambahPersen = diskontambahPersen.Text.Split('+');
            //        decimal dpp = Netto;

            //        if (diskontambahPersen.Text != "")
            //        {
            //            for (int a = 0; a <= DiscTambahPersen.GetUpperBound(0); a++)
            //            {
            //                coba = Math.Round(Convert.ToDecimal(DiscTambahPersen[a]) * dpp / (decimal)100);
            //                dpp -= coba;
            //                totaldisc += coba;


            //            }
            //        }
            //        else
            //        {
            //            totaldisc = 0;
            //        }

            //        DiskonTambahan = totaldisc;
            //    }


            //    Db.Execute("UPDATE MS_KONTRAK"
            //        + " SET DiskonTambahan = " + DiskonTambahan
            //        //+ ", DiscTambahPersen = '" + DiscTambahPersen + "'"
            //        + " WHERE NoKontrak = '" + NoKontrak + "'");

            //    Netto -= DiskonTambahan;
            //    /********************************/

            //    ////Biaya BPHTB
            //    //decimal BPHTB = Convert.ToDecimal(bphtb.Text);

            //    //Db.Execute("UPDATE MS_KONTRAK SET"
            //    //    + " BiayaBPHTB = '" + BPHTB + "'"
            //    //    + " WHERE NoKontrak = '" + NoKontrak + "'"
            //    //    );

            //    ////Netto += BPHTB;

            //    decimal distambah = Db.SingleDecimal("SELECT isnull(sum(DiskonTambahan),0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            //    decimal afterDiscTambahan = HargaSetelahBunga - distambah;
            //    decimal DPP = Math.Round(afterDiscTambahan / (decimal)1.1);

            //    if (JenisPPN.SelectedValue == "KONSUMEN")
            //    {
            //        PPNDitanggungPemerintah = 0;
            //        //NilaiPPN = (DPP * (decimal)0.1)-PPNDitanggungPemerintah;
            //        NilaiPPN = afterDiscTambahan - DPP;
            //        //NilaiKontrak = DPP + NilaiPPN;

            //    }
            //    else
            //    {
            //        PPNDitanggungPemerintah = DPP * (decimal)0.1;
            //        //NilaiPPN = ((DPP * (decimal) 0.1))-PPNDitanggungPemerintah;
            //        NilaiPPN = 0;
            //        //NilaiKontrak = DPP + NilaiPPN;
            //    }

            //    Db.Execute("EXEC spKontrakDiskon"
            //       + " '" + NoKontrak + "'"
            //       + ", " + Gross
            //       + ", " + afterDiscTambahan
            //        // + ", " + DPP
            //       + ",''"
            //       + ",''"
            //       );


            //    // Db.Execute("EXEC spKontrakBunga"
            //    //+ " '" + NoKontrak + "'"
            //    //+ ", " + Gross2
            //    //+ ", " + NilaiKontrak
            //    ////+ ", " + NilaiPPN  
            //    //+ ", " + DPP
            //    //+ ",'" + RumusBunga + "'"
            //    //+ ",'" + Cf.Str(RumusBunga2) + "'"
            //    //);

            //    Db.Execute("UPDATE MS_KONTRAK"
            //        + " SET "
            //        + " NilaiDPP = " + DPP
            //        + " ,NilaiPPN = " + NilaiPPN
            //        // + " , PPNPemerintah = " + PPNDitanggungPemerintah
            //        + " WHERE NoKontrak = '" + NoKontrak + "'");

            //    //Biaya Kerja Tambah
            //    decimal BKT = Convert.ToDecimal(bktambah.Text);
            //    decimal PPNBKT = 0;
            //    if (sifatppn2.SelectedIndex == 1)
            //    {
            //        if (includeppn2.Checked)
            //        {
            //            if (roundppn2.Checked)
            //            {
            //                PPNBKT = BKT - Math.Round(BKT / (decimal)1.1);
            //            }
            //            else
            //            {
            //                PPNBKT = BKT - (BKT / (decimal)1.1);
            //            }
            //        }
            //        else
            //        {
            //            if (roundppn2.Checked)
            //                PPNBKT = Math.Round((decimal)0.1 * (BKT / (decimal)1.1));
            //            else
            //                PPNBKT = (decimal)0.1 * (BKT / (decimal)1.1);
            //        }
            //    }

            //    decimal NBKT = BKT - PPNBKT;
            //    Db.Execute("UPDATE MS_KONTRAK SET PPNBiayaKerjaTambah='" + PPNBKT + "'"
            //        + ", BiayaKerjaTambah = '" + NBKT + "'"
            //        + " WHERE NoKontrak = '" + NoKontrak + "'"
            //        );
            //}
        }

        private void SaveTagihanBKT()
        {
            decimal BKT = Convert.ToDecimal(bktambah.Text);
            if (BKT > 0)
            {
                int counttagihan = Db.SingleInteger("SELECT COUNT(*) FROM MS_TAGIHAN WHERE NoKontrak='" + NoKontrak + "' AND Tipe IN('DP','ANG','PEL')");
                decimal nilaitagihan = BKT / (decimal)counttagihan;
                DataTable rs = Db.Rs("SELECT * FROM MS_TAGIHAN WHERE NoKontrak='" + NoKontrak + "' AND Tipe IN('DP','ANG','PEL')");

                decimal t = 0;
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    if (i != rs.Rows.Count - 1)
                    {
                        decimal native = nilaitagihan;
                        decimal rounded = 0;
                        rounded = RoundThousand(native);
                        t = t + rounded;

                        Db.Execute("EXEC spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA KERJA TAMBAH " + (i + 1) + "'"
                            + ",'" + rs.Rows[i]["TglJT"] + "'"
                            + ", " + rounded
                            + ",'ADM'"
                            );
                    }
                    else
                    {
                        decimal sisa = BKT - t;

                        Db.Execute("EXEC spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA KERJA TAMBAH " + (i + 1) + "'"
                            + ",'" + rs.Rows[i]["TglJT"] + "'"
                            + ", " + sisa
                            + ",'ADM'"
                            );
                    }

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
            //if (skema.SelectedIndex > 0)
            //{
            SetDiskon2();
            //}
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
            //if (skema.SelectedIndex > 0)
            //{
            SetDiskon2();
            SetBunga2();
            //}
        }

        protected void skema_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (skema.SelectedIndex > 0)
            //{
            string RumusDiskon = Db.SingleString(
                "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
            string Ket = Db.SingleString(
                "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
            //string NamaCashKeras = Db.SingleString(
            //    "SELECT Nama FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
            string CaraBayar = Db.SingleString(
                "SELECT Jenis FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
            string RumusBunga = Db.SingleString(
            "SELECT Bunga FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
            string Ket2 = Db.SingleString(
            "SELECT Bungaket FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);


            carabayar2.SelectedValue = CaraBayar;
            carabayar2.Enabled = false;
            if (CaraBayar == "KPR")
            {
                kprbank.Visible = true;
            }
            else
            {
                kprbank.Visible = false;
            }
            diskon2.Text = RumusDiskon;
            //nilaiDiskon.Text = Cf.Num((Convert.ToDecimal(RumusDiskon) * Convert.ToDecimal(Pricelist.Text) /100));
            diskonket.Text = Ket;
            persenBunga.Text = RumusBunga;
            bungaket.Text = Ket2;

            //SetDiskon();
            //SetBunga();
            //}
            //else { carabayar2.Enabled = true; }

            //Js.Focus(this, skema);
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
            else if (skema.SelectedValue.Contains("KPR") == true)
            {
                carabayar2.SelectedIndex = 2;
            }
        }


        // DISKON ---------------
        private void SetDiskon()
        {
            decimal Gross = Convert.ToDecimal(Pricelist.Text);//Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
            //+ " WHERE NoUnit = '" + Cf.Pk(unit.Text) + "'");
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
            decimal Gross = Convert.ToDecimal(Pricelist.Text); //Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
            //                + " WHERE NoUnit = '" + Cf.Pk(unit.Text) + "'");

            decimal surcharge = Convert.ToDecimal(Surcharge.Text);
            decimal Gross2 = Gross + surcharge;

            string RumusDiskon = diskon2.Text;
            string[] x = RumusDiskon.Split('+');

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < x.Length; i++)
            {
                if (RumusDiskon != "")
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
                decimal n = Gross2 - diskon;
                decimal nbphtb = (((n / (decimal)1.1) - 60000000) * (decimal)0.05);
                bphtb.Text = Cf.Num(Math.Round(nbphtb, 0).ToString());
                sisa.Text = Cf.Num(Convert.ToDecimal(Pricelist.Text) - Convert.ToDecimal(nilaiDiskon.Text));
            }
            else
            {
                nilaiDiskon.Text = Cf.Num(Math.Round(diskon, 0).ToString());
                decimal n = Gross2 - diskon;
                decimal nbphtb = (((n / (decimal)1.1) - 60000000) * (decimal)0.05);
                bphtb.Text = Cf.Num(Math.Round(nbphtb, 0).ToString());
                sisa.Text = Cf.Num(Convert.ToDecimal(Pricelist.Text) - Convert.ToDecimal(nilaiDiskon.Text));
            }
        }


        // BUNGA -------

        private void SetBunga()
        {
            decimal Gross = Convert.ToDecimal(Pricelist.Text); //Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
            //+ " WHERE NoStock = '" + NoStock + "'");
            decimal surcharge = Convert.ToDecimal(Surcharge.Text);
            decimal Gross2 = Gross + surcharge;

            string RumusBunga = persenBunga.Text;
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
            persenBunga.Text = sb.ToString();

            //decimal bunga = Func.NominalDiskon(RumusBunga, Gross2);
            //if (bunga == 0)
            //{
            //    nilaiBunga.Text = "";
            //}
            //else
            //{
            //    nilaiBunga.Text = Cf.Num(Math.Round(bunga, 0).ToString());
            //}

        }

        private void SetBunga2()
        {
            decimal Gross = Convert.ToDecimal(Pricelist.Text); //Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
            //+ " WHERE NoStock = '" + NoStock + "'");
            decimal surcharge = Convert.ToDecimal(Surcharge.Text);
            decimal Gross2 = Gross + surcharge;

            string RumusBunga = persenBunga.Text;
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
            persenBunga.Text = sb.ToString();

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

        protected void sifatppn_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            trppn.Visible = sifatppn.SelectedIndex == 1;

        }

        protected void sifatppn2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            trppn2.Visible = sifatppn2.SelectedIndex == 1;

        }

        protected void sifatppn3_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            trppn3.Visible = sifatppn3.SelectedIndex == 1;

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
        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
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
            Response.Redirect("UnitPilih2.aspx?done=1&NoNUP=" + NoNUP + "&NoStock=" + NoStock);
        }
    }
}
