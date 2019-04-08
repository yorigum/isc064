using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class MigrateKontrak2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                InitForm();
                Fill();
                Js.Confirm(this, "Lanjutkan proses migrate kontrak?");
            }
        }
        private void InitForm()
        {
            DataTable rs;

            //Populate data agent
            rs = Db.Rs("SELECT Nama,Principal,NoAgent FROM MS_AGENT WHERE Status = 'A'"
                //+ " AND Proyek = '" + Proyek + "'"
                + " ORDER BY Nama,NoAgent");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                if (rs.Rows[i]["Principal"].ToString() != "")
                    t = t + " (" + rs.Rows[i]["Principal"] + ")";
                agent.Items.Add(new ListItem(t, v));
            }

            rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_ACC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                acc.Items.Add(new ListItem(t, v));
            }

            pl.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            pl.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            pl.Attributes["onblur"] = "CalcBlur(this);";

            diskonrupiah.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            diskonrupiah.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            diskonrupiah.Attributes["onblur"] = "CalcBlur(this);";

            nilaikontrak.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaikontrak.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaikontrak.Attributes["onblur"] = "CalcBlur(this);";

            nilaippn.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaippn.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaippn.Attributes["onblur"] = "CalcBlur(this);";

            batalmasuk.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            batalmasuk.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            batalmasuk.Attributes["onblur"] = "CalcBlur(this);";

            nilaiklaim.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaiklaim.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaiklaim.Attributes["onblur"] = "CalcBlur(this);";

            nilaipulang.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaipulang.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaipulang.Attributes["onblur"] = "CalcBlur(this);";
        }

        private void Fill()
        {
            cancel.Attributes["onclick"] = "location.href='MigrateKontrak.aspx?NoKontrak=" + NoKontrak + "'";

            string strSql = "SELECT * "
                + " FROM MIGRATE_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'"
                //+ " AND Status = 'A'"
                + " AND Approved = 0"; //cek kondisi
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count > 0)
            {
                nokontrak.Text = mnokontrak.Text = rs.Rows[0]["NoKontrak"].ToString();
                tglkontrak.Text = Cf.Day(rs.Rows[0]["TglKontrak"]);
                statuskontrak.SelectedValue = mstatuskontrak.Text = rs.Rows[0]["Status"].ToString();
                agent.SelectedValue = magent.Text = rs.Rows[0]["Agent"].ToString();
                nounit.Text = rs.Rows[0]["NoUnit"].ToString();
                pl.Text = Cf.Num(rs.Rows[0]["Gross"]);
                diskonrupiah.Text = Cf.Num(rs.Rows[0]["DiskonRupiah"]);
                nilaikontrak.Text = Cf.Num(rs.Rows[0]["NilaiKontrak"]);
                skema.Text = rs.Rows[0]["Skema"].ToString();
                tipeSkema.SelectedValue = mtipeSkema.Text = rs.Rows[0]["CaraBayar"].ToString();
                sifatppn.SelectedValue = rs.Rows[0]["JenisPPN"].ToString() == "PEMERINTAH" ? "0" : "1";
                msifatppn.Text = rs.Rows[0]["JenisPPN"].ToString();
                nilaippn.Text = Cf.Num(rs.Rows[0]["NilaiPPN"]);
                nova.Text = rs.Rows[0]["NoVA"].ToString();
                tglst.Text = rs.Rows[0]["NoST"].ToString() != "" ? Cf.Day(rs.Rows[0]["TglST"]) : "";
                nost.Text = rs.Rows[0]["NoST"].ToString();
                targetst.Text = Cf.Day(rs.Rows[0]["TargetST"]);
                tglppjb.Text = rs.Rows[0]["NoPPJB"].ToString() != "" ? Cf.Day(rs.Rows[0]["TglPPJB"]) : "";
                noppjb.Text = rs.Rows[0]["NoPPJB"].ToString();
                tglajb.Text = rs.Rows[0]["NoAJB"].ToString() != "" ? Cf.Day(rs.Rows[0]["TglAJB"]) : "";
                noajb.Text = rs.Rows[0]["NoAJB"].ToString();
                tglbatal.Text = rs.Rows[0]["Status"].ToString() == "B" ? Cf.Day(rs.Rows[0]["TglBatal"]) : "";
                alasanbatal.Text = rs.Rows[0]["AlasanBatal"].ToString();
                batalmasuk.Text = Cf.Num(rs.Rows[0]["BatalMasuk"]);
                nilaiklaim.Text = Cf.Num(rs.Rows[0]["NilaiKlaim"]);
                nilaipulang.Text = Cf.Num(rs.Rows[0]["NilaiPulang"]);
                acc.SelectedValue = rs.Rows[0]["Status"].ToString() == "B" ? rs.Rows[0]["AccBatal"].ToString() : "";
                macc.Text = rs.Rows[0]["AccBatal"].ToString();

                nama.Text = rs.Rows[0]["Customer"].ToString();
                ktp.Text = rs.Rows[0]["NoKTP"].ToString();
                ktp1.Text = rs.Rows[0]["KTPAlamat"].ToString();
                tempatlahir.Text = rs.Rows[0]["TempatLahir"].ToString();
                tgllahir.Text = Cf.Day(rs.Rows[0]["TglLahir"]);
                marital.SelectedValue = mmarital.Text = rs.Rows[0]["Marital"].ToString();
                agama.SelectedValue = magama.Text = rs.Rows[0]["Agama"].ToString();
                telp.Text = rs.Rows[0]["NoTelp"].ToString();
                fax.Text = rs.Rows[0]["NoFax"].ToString();
                npwp.Text = rs.Rows[0]["NPWP"].ToString();
                alamat1.Text = rs.Rows[0]["AlamatSurat"].ToString();
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            int kontrak = Db.SingleInteger("SELECT COUNT(NoKontrak) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            if (kontrak > 0)
            {
                x = false;
                cek.Text = "No. Kontrak Exists ";
            }
            else
                cek.Text = "";

            int unit = Db.SingleInteger("SELECT COUNT(NoUnit) FROM MS_UNIT WHERE NoUnit = '" + nounit.Text + "' AND Status = 'A'");
            if (unit == 0)
            {
                x = false;
                cek.Text = "No. Unit Not Available";
            }
            else
                cek.Text = "";

            if (Cf.isEmpty(nokontrak))
            {
                x = false;
                if (s == "") s = nokontrak.ID;
                nokontrakc.Text = "Kosong";
            }
            else
                nokontrakc.Text = "";

            if (!Cf.isTgl(tglkontrak))
            {
                x = false;
                if (s == "") s = tglkontrak.ID;
                tglkontrakc.Text = "Tanggal";
            }
            else
                tglkontrakc.Text = "";

            if (statuskontrak.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = statuskontrak.ID;
                statuskontrakc.Text = "Pilih";
            }
            else
                statuskontrakc.Text = "";

            if (agent.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = agent.ID;
                agentc.Text = "Pilih";
            }
            else
                agentc.Text = "";

            if (Cf.isEmpty(nounit))
            {
                x = false;
                if (s == "") s = nounit.ID;
                nounitc.Text = "Kosong";
            }
            else
                nounitc.Text = "";

            if (!Cf.isTgl(targetst))
            {
                x = false;
                if (s == "") s = targetst.ID;
                targetstc.Text = "Tanggal";
            }
            else
                targetstc.Text = "";

            if (sifatppn.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = sifatppn.ID;
                sifatppnc.Text = "Pilih";
            }
            else
                sifatppnc.Text = "";

            if (tipeSkema.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = tipeSkema.ID;
                tipeSkemac.Text = "Pilih salah satu jenis.";
            }
            else
                tipeSkemac.Text = "";

            if (pl.Text == "0")
            {
                x = false;
                if (s == "") s = pl.ID;
                pricec.Text = "Pricelist tidak boleh 0";
            }

            if (!Cf.isMoney(diskonrupiah))
            {
                x = false;
                if (s == "") s = diskonrupiah.ID;
                diskonrupiahc.Text = "Angka";
            }
            else
                diskonrupiahc.Text = "";

            if (!Cf.isMoney(nilaikontrak))
            {
                x = false;
                if (s == "") s = nilaikontrak.ID;
                nilaikontrakc.Text = "Angka";
            }
            else
                nilaikontrakc.Text = "";

            if (Cf.isEmpty(skema))
            {
                x = false;
                if (s == "") s = skemac.ID;
                skemac.Text = "Kosong";
            }
            else
                skemac.Text = "";

            if (!Cf.isMoney(nilaippn))
            {
                x = false;
                if (s == "") s = nilaippn.ID;
                nilaippnc.Text = "Angka";
            }
            else
                nilaippnc.Text = "";

            //BAST
            if (!Cf.isEmpty(nost))
            {
                if (!Cf.isTgl(tglst))
                {
                    x = false;
                    if (s == "") s = tglst.ID;
                    tglstc.Text = "Tanggal";
                }
                else
                    tglstc.Text = "";
            }

            //PPJB
            if (!Cf.isEmpty(noppjb))
            {
                if (!Cf.isTgl(tglppjb))
                {
                    x = false;
                    if (s == "") s = tglppjb.ID;
                    tglppjbc.Text = "Tanggal";
                }
                else
                    tglppjbc.Text = "";
            }

            //AJB
            if (!Cf.isEmpty(noajb))
            {
                if (!Cf.isTgl(tglajb))
                {
                    x = false;
                    if (s == "") s = tglajb.ID;
                    tglajbc.Text = "Tanggal";
                }
                else
                    tglajbc.Text = "";
            }

            //Batal
            if (statuskontrak.SelectedValue == "B")
            {
                if (!Cf.isTgl(tglbatal))
                {
                    x = false;
                    if (s == "") s = tglbatal.ID;
                    tglbatalc.Text = "Tanggal";
                }
                else
                    tglbatalc.Text = "";

                if (!Cf.isMoney(batalmasuk))
                {
                    x = false;
                    if (s == "") s = batalmasuk.ID;
                    batalmasukc.Text = "Angka";
                }
                else
                    batalmasukc.Text = "";

                if (!Cf.isMoney(nilaiklaim))
                {
                    x = false;
                    if (s == "") s = nilaiklaim.ID;
                    nilaiklaimc.Text = "Angka";
                }
                else
                    nilaiklaimc.Text = "";

                if (!Cf.isMoney(nilaipulang))
                {
                    x = false;
                    if (s == "") s = nilaipulang.ID;
                    nilaipulangc.Text = "Angka";
                }
                else
                    nilaipulangc.Text = "";

                if (acc.SelectedIndex == 0)
                {
                    x = false;
                    if (s == "") s = acc.ID;
                    accerr.Text = "Pilih";
                }
                else
                    accerr.Text = "";
            }

            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "";

            if (!Cf.isTgl(tgllahir))
            {
                x = false;
                if (s == "") s = tgllahir.ID;
                tgllahirc.Text = "Tanggal";
            }
            else
                tgllahirc.Text = "";

            if (Cf.isEmpty(ktp))
            {
                x = false;
                if (s == "") s = ktp.ID;
                ktpc.Text = "Kosong";
            }
            else
                ktpc.Text = "";

            if (Cf.isEmpty(ktp1))
            {
                x = false;
                if (s == "") s = ktp1.ID;
                ktp1c.Text = "Kosong";
            }
            else
                ktp1c.Text = "";

            if (Cf.isEmpty(alamat1))
            {
                x = false;
                if (s == "") s = alamat1.ID;
                suratc.Text = "Kosong";
            }
            else
                suratc.Text = "";

            if (Cf.isEmpty(telp))
            {
                x = false;
                if (s == "") s = telp.ID;
                telpc.Text = "Kosong";
            }
            else
                telpc.Text = "";

            if (marital.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = marital.ID;
                maritalc.Text = "Pilih";
            }
            else
                maritalc.Text = "";

            if (agama.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = agama.ID;
                agamac.Text = "Pilih";
            }
            else
                agamac.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void SaveCustomer()
        {
            string Nama = Cf.Str(nama.Text);
            string NoTelp = Cf.Str(telp.Text);
            string NoFax = Cf.Str(fax.Text);
            string NPWP = Cf.Str(npwp.Text);
            string NoKTP = Cf.Str(ktp.Text);
            string KTP1 = Cf.Str(ktp1.Text);
            string KTP2 = Cf.Str(ktp2.Text);
            string KTP3 = Cf.Str(ktp3.Text);
            string KTP4 = Cf.Str(ktp4.Text);
            string Alamat1 = Cf.Str(alamat1.Text);
            string Alamat2 = Cf.Str(alamat2.Text);
            string Alamat3 = Cf.Str(alamat3.Text);
            string Agama = Cf.Str(agama.SelectedValue);
            string Marital = Cf.Str(marital.SelectedValue);
            string TempatLahir = Cf.Str(tempatlahir.Text);
            DateTime TglLahir = Convert.ToDateTime(tgllahir.Text);

            Db.Execute("EXEC spCustomerDaftar"
                + " '" + Nama + "'"
                + ",''"
                + ",'" + NoTelp + "'"
                + ",''"
                + ",''"
                + ",'" + NoFax + "'"
                + ",''"
                + ",'" + NoKTP + "'"
                + ",'" + KTP1 + "'"
                + ",'" + KTP2 + "'"
                + ",'" + KTP3 + "'"
                + ",'" + KTP4 + "'"
                + ",'" + Alamat1 + "'"
                + ",'" + Alamat2 + "'"
                + ",'" + Alamat3 + "'"
                + ",''"
                + ",''"
                + ",''"
                + ",'" + Agama + "'"
                + ",'" + TglLahir + "'"
                + ",''"
                + ",''"
                + ",''"
                + ",0"
                + ",''"
                + ",''"
                + ",''"
                + ",''"
                + ",''"
                + ",''"
                + ",''"
                + ",'" + NPWP + "'"
                );

            //get nomor customer terbaru
            nocustomer.Text = Db.SingleInteger(
                "SELECT TOP 1 NoCustomer FROM MS_CUSTOMER ORDER BY NoCustomer DESC")
                .ToString().PadLeft(5, '0');

            Db.Execute("UPDATE MS_CUSTOMER SET "
                + " AgentInput = '" + Act.UserID + "'"
                + ",TempatLahir = '" + TempatLahir + "'"
                + ",Marital = '" + Marital + "'"
                + " WHERE NoCustomer = '" + nocustomer.Text + "'");

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
                + ",AgentInput AS [Sales Account]"
                + ",SumberData AS [Sumber Data]"
                + ",TempatLahir AS [Tempat Lahir]"
                + ",NPWP"
                //+ ",Perusahaan"
                //+ ",SIUP AS [No. SIUP/Akte]"
                //+ ",JenisUsaha AS [Jenis Usaha]"
                //+ ",FaxKantor AS [Fax Kantor]"
                //+ ",JenisKelamin AS [Jenis Kelamin]"
                //+ ",SumberPendapatan AS [Sumber Pendapatan]"
                + ",Marital AS [Status Marital]"
                + " FROM MS_CUSTOMER"
                + " WHERE NoCustomer = '" + nocustomer.Text + "'"
                );

            Db.Execute("EXEC spLogCustomer"
                + " 'DAFTAR'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + nocustomer.Text + "'"
                );
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                SaveCustomer();

                Db.Execute("EXEC spKontrakDaftar3"
                    + " '" + NoKontrak + "'"
                    + ",'" + NoStock + "'"
                    + ",'" + Convert.ToDateTime(tglkontrak.Text) + "'"
                    + ",'" + skema.Text.ToUpper() + "'"
                    + ",'" + tipeSkema.Text + "'"
                    + ",'" + Convert.ToDateTime(targetst.Text) + "'"
                    + ", " + Convert.ToDecimal(pl.Text)
                    + ",'" + nocustomer.Text + "'"                    
                    + ",'" + agent.SelectedValue + "'"
                    + ",'" + statuskontrak.SelectedValue + "'"
                    + ", " + Convert.ToDecimal(diskonrupiah.Text)
                    + ", " + Convert.ToDecimal(nilaikontrak.Text)
                    + ",'" + sifatppn.SelectedValue + "'"                    
                    + ", " + Convert.ToDecimal(pl.Text)
                    + ", " + Convert.ToDecimal(nilaippn.Text)
                    + ",'" + Cf.Str(nova.Text) + "'"
                    );

                string strTipeSkema = tipeSkema.Text.ToUpper();

                int KPR = 0;
                if (strTipeSkema == "KPR")
                {
                    KPR = 1;
                }
                else
                {
                    KPR = 0;
                }

                //BAST
                if (!Cf.isEmpty(nost))
                {
                    Db.Execute("UPDATE MS_KONTRAK SET"
                        + " TglST = '" + Convert.ToDateTime(tglst.Text) + "'"
                        + ",NoST = '" + Cf.Str(nost.Text) + "'"
                        + ",ST = 'D'"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );
                }

                //PPJB
                if (!Cf.isEmpty(noppjb))
                {
                    Db.Execute("UPDATE MS_KONTRAK SET"
                        + " TglPPJB = '" + Convert.ToDateTime(tglppjb.Text) + "'"
                        + ",NoPPJB = '" + Cf.Str(noppjb.Text) + "'"
                        + ",PPJB = 'D'"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );
                }

                //AJB
                if (!Cf.isEmpty(noajb))
                {
                    Db.Execute("UPDATE MS_KONTRAK SET"
                        + " TglAJB = '" + Convert.ToDateTime(tglajb.Text) + "'"
                        + ",NoAJB = '" + Cf.Str(noajb.Text) + "'"
                        + ",AJB = 'D'"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );
                }

                //Batal
                if (statuskontrak.SelectedValue == "B")
                {
                    Db.Execute("UPDATE MS_KONTRAK SET"
                        + " TglBatal = '" + Convert.ToDateTime(tglbatal.Text) + "'"
                        + ",AlasanBatal = '" + Cf.Str(alasanbatal.Text) + "'"
                        + ",TotalLunasBatal = " + Convert.ToDecimal(batalmasuk.Text)
                        + ",NilaiKlaim = " + Convert.ToDecimal(nilaiklaim.Text)
                        + ",NilaiPulang = " + Convert.ToDecimal(nilaipulang.Text)
                        + ",AccBatal = '" + acc.SelectedValue + "'"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );
                }

                //Manual update
                string sSQL = "UPDATE MS_KONTRAK"
                    + " SET"
                    + "  JenisKPR = " + KPR
                    + " ,UserID = '" + Act.UserID + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    ;
                Db.Execute(sSQL);

                Db.Execute("UPDATE MIGRATE_KONTRAK SET Approved = 1 WHERE NoKontrak = '" + mnokontrak.Text + "'");

                DataTable rs = Db.Rs("SELECT "
                        + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                        + ",MS_KONTRAK.Status AS [Status]"
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
                        + ",MS_KONTRAK.NilaiPPN AS [Nilai PPN]"
                        + ",MS_KONTRAK.Skema"
                        + ",MS_KONTRAK.CaraBayar"
                        + ",CONVERT(varchar,MS_KONTRAK.TargetST,106) AS [Jadwal Serah Terima]"
                        + ", MS_KONTRAK.JenisPPN AS [PPN Ditanggung]"
                        + ", CASE MS_KONTRAK.JenisKPR"
                        + "		WHEN 0 THEN 'KPR'"
                        + "		WHEN 1 THEN 'NON-KPR'"
                        + "	END AS [Jenis KPR]"
                        + ",MS_KONTRAK.NoVA AS [No. VA]"
                        + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                        + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );

                string Ket = Cf.LogCapture(rs);

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

                Response.Redirect("MigrateKontrak.aspx?done=" + NoKontrak);
            }
        }

        private string NoStock
        {
            get
            {
                string stok = Db.SingleString("SELECT NoStock FROM MS_UNIT WHERE NoUnit = '" + nounit.Text + "'");

                return Cf.Pk(stok);
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
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

        protected bool DenganPPN { get { return sifatppn.SelectedValue != "0"; } }


    }
}
