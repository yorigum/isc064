using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class CustomerEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("NoCustomer");
            Func.CustomerPassword(NoCustomer); //Custom SECURITY

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                //kalkulator
                Js.NumberFormat(luaslama);
                Act.ProjectList(project);

                Fill();
            }

            FeedBack();
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

        protected void gantikorporasi(object sender, EventArgs e)
        {
            if (kori.Checked || kora.Checked)
            {
                korp1.Visible = korp2.Visible = korp3.Visible = korp4.Visible = true;
            }
            else
            {
                korp1.Visible = korp2.Visible = korp3.Visible = korp4.Visible = false;
            }
        }

        private void Fill()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_CUSTOMER_LOG&Pk=" + NoCustomer.PadLeft(5, '0') + "'";
            btndel.Attributes["onclick"] = "location.href='CustomerDel.aspx?NoCustomer=" + NoCustomer + "'";

            string strSql = "SELECT * FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nocustomer.Text = rs.Rows[0]["NoCustomer"].ToString().PadLeft(5, '0');
                if ((string)rs.Rows[0]["TipeCs"] == "PERORANGAN") perorangan.Checked = true;
                if ((string)rs.Rows[0]["TipeCs"] == "BADAN HUKUM") badanhukum.Checked = true;

                if (Convert.ToSByte(rs.Rows[0]["Refferator"]) == 1)
                {
                    reff.Checked = true;
                    DataTable a = Db.Rs("SELECT * FROM MS_KONTRAK WHERE NoRefferatorAgent='" + rs.Rows[0]["NoCustomer"].ToString() + "' or NoRefferatorCustomer='" + rs.Rows[0]["NoCustomer"].ToString() + "' and Status='A'");
                    if (a.Rows.Count == 0)
                    {
                        reff.Enabled = true;
                    }
                    else
                    {
                        reff.Enabled = false;
                    }
                }
                else
                {
                    reff.Checked = false;
                }
                nama.Text = rs.Rows[0]["Nama"].ToString();
                nama2.Text = rs.Rows[0]["Nama2"].ToString();
                salutation.Text = rs.Rows[0]["Salutation"].ToString();
                namabisnis.Text = rs.Rows[0]["NamaBisnis"].ToString();
                jenisbisnis.Text = rs.Rows[0]["JenisBisnis"].ToString();
                merekbisnis.Text = rs.Rows[0]["MerekBisnis"].ToString();
                namanpwp.Text = rs.Rows[0]["NamaNPWP"].ToString();
                npwp.Text = rs.Rows[0]["NPWP"].ToString();
                pekerjaan.Text = rs.Rows[0]["Pekerjaan"].ToString();
                npwp1.Text = rs.Rows[0]["NPWPAlamat1"].ToString();
                npwp2.Text = rs.Rows[0]["NPWPAlamat2"].ToString();
                npwp3.Text = rs.Rows[0]["NPWPAlamat3"].ToString();
                npwp4.Text = rs.Rows[0]["NPWPAlamat4"].ToString();
                npwp5.Text = rs.Rows[0]["NPWPAlamat5"].ToString();

                if ((string)rs.Rows[0]["Status"] == "A")
                    aktif.Checked = true;
                else
                    inaktif.Checked = true;

                notelp.Text = rs.Rows[0]["NoTelp"].ToString();
                nohp.Text = rs.Rows[0]["NoHp"].ToString().Length > 2 ? rs.Rows[0]["NoHp"].ToString().Substring(0, 3) != "+62" ? rs.Rows[0]["NoHp"].ToString().Substring(1) : rs.Rows[0]["NoHp"].ToString().Substring(3) : "";
                nohp2.Text = rs.Rows[0]["NoHP2"].ToString();
                nokantor.Text = rs.Rows[0]["NoKantor"].ToString();
                nofax.Text = rs.Rows[0]["NoFax"].ToString();
                email.Text = rs.Rows[0]["Email"].ToString();
                noktp.Text = rs.Rows[0]["NoKTP"].ToString();

                ktp1.Text = rs.Rows[0]["KTP1"].ToString();
                ktp2.Text = rs.Rows[0]["KTP2"].ToString();
                ktp3.Text = rs.Rows[0]["KTP3"].ToString();
                ktp4.Text = rs.Rows[0]["KTP4"].ToString();
                ktp5.Text = rs.Rows[0]["KTP5"].ToString();
                kodepos1.Text = rs.Rows[0]["Kodepos"].ToString();

                alamat1.Text = rs.Rows[0]["Alamat1"].ToString();
                alamat2.Text = rs.Rows[0]["Alamat2"].ToString();
                alamat3.Text = rs.Rows[0]["Alamat3"].ToString();
                alamat4.Text = rs.Rows[0]["Alamat4"].ToString();
                alamat5.Text = rs.Rows[0]["Alamat5"].ToString();

                kantor1.Text = rs.Rows[0]["Kantor1"].ToString();
                kantor2.Text = rs.Rows[0]["Kantor2"].ToString();
                kantor3.Text = rs.Rows[0]["Kantor3"].ToString();
                kantor4.Text = rs.Rows[0]["Kantor4"].ToString();
                kantor5.Text = rs.Rows[0]["Kantor5"].ToString();

                nmorghub.Text = rs.Rows[0]["NamaKerabat"].ToString();
                hubungan.SelectedValue = rs.Rows[0]["Hubungan"].ToString();
                hphub.Text = rs.Rows[0]["NoHPKerabat"].ToString();
                emailhub.Text = rs.Rows[0]["EmailKerabat"].ToString();

                try
                {
                    agama.SelectedValue = rs.Rows[0]["Agama"].ToString();
                }
                catch
                {
                    agama.SelectedIndex = agama.Items.Count - 1;
                }

                tgllahir.Text = Cf.Day(rs.Rows[0]["TglLahir"]);
                tglktp.Text = Cf.Day(rs.Rows[0]["TglKTP"]);
                if (Convert.ToBoolean(rs.Rows[0]["KTPSeumurHidup"]) == true)
                {
                    sedup.Checked = true;
                    tglktp.Text = "";
                }

                unitlama.Text = rs.Rows[0]["UnitLama"].ToString();
                luaslama.Text = Cf.Num(rs.Rows[0]["LuasLama"]);
                tokolama.Text = rs.Rows[0]["TokoLama"].ToString();
                zoninglama.Text = rs.Rows[0]["ZoningLama"].ToString();
                gedunglama.Text = rs.Rows[0]["GedungLama"].ToString();
                teleponlama.Text = rs.Rows[0]["TeleponLama"].ToString();
                aktelama.Text = rs.Rows[0]["AkteLama"].ToString();

                tgltransaksi.Text = Cf.Day(rs.Rows[0]["TglTransaksi"]);
                tglInput.Text = Cf.Date(rs.Rows[0]["TglInput"]);
                tglEdit.Text = Cf.Date(rs.Rows[0]["TglEdit"]);

                sumberdata.Items.Add(new ListItem(rs.Rows[0]["SumberData"].ToString()));
                sumberdata.SelectedValue = rs.Rows[0]["SumberData"].ToString();

                Cf.SelectedValue(project, rs.Rows[0]["Project"].ToString());

                int c1 = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoCustomer = " + NoCustomer);
                if (c1 > 0)
                    sifat.Text = "Sudah Beli";
                else
                    sifat.Text = "Belum Beli";

                tempatlahir.Text = rs.Rows[0]["TempatLahir"].ToString();

                marital.SelectedValue = rs.Rows[0]["Marital"].ToString();

                switch (rs.Rows[0]["Kewarganegaraan"].ToString())
                {
                    case "WNI":
                        wni.Checked = true;
                        break;
                    case "WNA":
                        wna.Checked = true;
                        break;
                    case "KORPORASI INDONESIA":
                        kori.Checked = true;
                        break;
                    case "KORPORASI ASING":
                        kora.Checked = true;
                        break;
                }

                penanggungjawab.Text = rs.Rows[0]["PenanggungjawabKorp"].ToString();
                jabatan.Text = rs.Rows[0]["JabatanKorp"].ToString();
                nosk.Text = rs.Rows[0]["NoSKKorp"].ToString();
                bentuk.Text = rs.Rows[0]["BentukKorp"].ToString();
                if (kori.Checked || kora.Checked)
                {
                    korp1.Visible = korp2.Visible = korp3.Visible = korp4.Visible = true;
                }
                else
                {
                    korp1.Visible = korp2.Visible = korp3.Visible = korp4.Visible = false;
                }

                kori.Visible = false;
                kora.Visible = false;
                wni.Visible = true;
                wni.Checked = true;
                wna.Visible = true;
                korp1.Visible = korp2.Visible = korp3.Visible = korp4.Visible = false;
            }
        }

        private bool validMandatory
        {
            get
            {
                return Cf.ValidMandatory(this, "Customer", project.SelectedValue);
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (npwp.Text.Length < 15)
            {
                x = false;
                if (s == "") s = npwp.ID;
                npwpc.Text = "Minimal 15 Digit";
            }
            else
                npwpc.Text = "";

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
            if (!Cf.isMoney(luaslama))
            {
                x = false;
                if (s == "") s = luaslama.ID;
                luaslamac.Text = "Angka";
            }
            else
                luaslamac.Text = "";

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

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Nama tidak boleh kosong.\\n"
                    + "3. NPWP minimal 15 angka.\\n"
                    + "4. Data korporasi tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool Save()
        {
            if (validMandatory && valid())
            {
string hp = "";
string HP = "";
if(nohp.Text != ""){
                hp = nohp.Text.Substring(0, 1);
                HP = nohp.Text;
                if (hp == "0")
                {
                    HP = nohp.Text.Substring(1);
                }
}
                string Nama = Cf.Str(nama.Text);
                string Salutation = Cf.Str(salutation.Text);
                string NamaBisnis = Cf.Str(namabisnis.Text);
                string NoTelp = Cf.Str(notelp.Text);
                string NoHp = Cf.Str(kodehp.Text + HP);
                string NoHp2 = Cf.Str(nohp2.Text);
                string NoKantor = Cf.Str(nokantor.Text);
                string NoFax = Cf.Str(nofax.Text);
                string Email = Cf.Str(email.Text);
                string NoKTP = Cf.Str(noktp.Text);
                string KTP1 = Cf.Str(ktp1.Text);
                string KTP2 = Cf.Str(ktp2.Text);
                string KTP3 = Cf.Str(ktp3.Text);
                string KTP4 = Cf.Str(ktp4.Text);
                string KTP5 = Cf.Str(ktp5.Text);
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
                string Agama = Cf.Str(agama.SelectedValue);
                //DateTime TglLahir = Convert.ToDateTime(tgllahir.Text);
                string JenisBisnis = Cf.Str(jenisbisnis.Text);
                string MerekBisnis = Cf.Str(merekbisnis.Text);
                string UnitLama = Cf.Str(unitlama.Text);
                decimal LuasLama = Convert.ToDecimal(luaslama.Text);
                string TokoLama = Cf.Str(tokolama.Text);
                string ZoningLama = Cf.Str(zoninglama.Text);
                string GedungLama = Cf.Str(gedunglama.Text);
                string TeleponLama = Cf.Str(teleponlama.Text);
                string AkteLama = Cf.Str(aktelama.Text);
                string NamaNPWP = Cf.Str(namanpwp.Text);
                string NPWP = Cf.Str(npwp.Text);
                string NPWP1 = Cf.Str(npwp1.Text);
                string NPWP2 = Cf.Str(npwp2.Text);
                string NPWP3 = Cf.Str(npwp3.Text);
                string NPWP4 = Cf.Str(npwp4.Text);
                string NPWP5 = Cf.Str(npwp5.Text);
                string Pekerjaan = Cf.Str(pekerjaan.Text);
                string Kodepos = Cf.Str(kodepos1.Text);
                string NamaKerabat = Cf.Str(nmorghub.Text);
                string Hubungan = Cf.Str(hubungan.SelectedValue);
                string HpKerabat = Cf.Str(hphub.Text);
                string EmailKerabat = Cf.Str(emailhub.Text);

                string TipeCs = "";
                if (perorangan.Checked) TipeCs = "PERORANGAN";
                if (badanhukum.Checked) TipeCs = "BADAN HUKUM";

                string Marital = Cf.Str(marital.SelectedValue);

                string wn = "";
                if (wni.Checked) wn = "WNI";
                if (wna.Checked) wn = "WNA";
                if (kori.Checked) wn = "KORPORASI INDONESIA";
                if (kora.Checked) wn = "KORPORASI ASING";

                string Status = "";
                if (aktif.Checked) Status = "A";
                if (inaktif.Checked) Status = "I";

                int Reff = Convert.ToInt16(reff.Checked);

                string SumberData = Cf.Str(sumberdata.SelectedValue);
                string TempatLahir = Cf.Str(tempatlahir.Text);

                string k1 = "", k2 = "", k3 = "", k4 = "";
                if (kori.Checked || kora.Checked)
                {
                    k1 = Cf.Str(penanggungjawab.Text);
                    k2 = Cf.Str(jabatan.Text);
                    k3 = Cf.Str(nosk.Text);
                    k4 = Cf.Str(bentuk.Text);
                }

                DataTable rsBef = Db.Rs("SELECT "
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
                    + ",NoHP2 AS [No. HP2]"
                    + ",NoFax AS [No. Fax]"
                    + ",Email AS [Alamat Email]"
                    + ",Alamat1 AS [Alamat Surat Menyurat]"
                    + ",Alamat2 AS [Alamat Surat Menyurat RT/RW]"
                    + ",Alamat3 AS [Alamat Surat Menyurat Kelurahan]"
                    + ",Alamat4 AS [Alamat Surat Menyurat Kecamatan]"
                    + ",Alamat5 AS [Alamat Surat Menyurat Kotamadya]"
                    + ",Pekerjaan AS [Pekerjaan]"
                    + ",NoKantor AS [No. Telepon Kantor]"
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
                    + ",TglKTP"
                    + ",UnitLama AS [Unit Lama]"
                    + ",LuasLama AS [Luas Unit Lama]"
                    + ",TokoLama AS [Nama Toko Lama]"
                    + ",ZoningLama AS [Zoning Lama]"
                    + ",GedungLama AS [Gedung Lama]"
                    + ",TeleponLama AS [Telepon Lama]"
                    + ",AkteLama AS [Akte Lama]"
                    + ",Salutation"
                    + ",Status"
                    + ",AgentInput AS [Sales Account]"
                    + ",SumberData AS [Sumber Data]"
                    + ",TempatLahir AS [Tempat Lahir]"
                    + ",NamaNPWP AS [Nama NPWP]"
                    + ",NPWP"
                    + ",NPWPAlamat1 AS [Alamat NPWP]"
                    + ",NPWPAlamat2 AS [Alamat NPWP RT/RW]"
                    + ",NPWPAlamat3 AS [Alamat NPWP Kelurahan]"
                    + ",NPWPAlamat4 AS [Alamat NPWP Kecamatan]"
                    + ",NPWPAlamat5 AS [Alamat NPWP Kotamadya]"
                    + ",Marital AS [Status Marital]"
                    + ",NamaKerabat [Nama yang dapat dihubungi]"
                    + ",Hubungan AS [Hubungan]"
                    + ",NoHPKerabat AS [No. HP]"
                    + ",EmailKerabat AS [Email Kerabat]"
                    + ",Kewarganegaraan AS [Kewarganegaraan]"
                    + ",Pekerjaan AS [Pekerjaan]"
                    + ",Refferator"
                    + ",PenanggungjawabKorp AS [Penanggungjawab Korporasi]"
                    + ",JabatanKorp AS [Jabatan Korporasi]"
                    + ",NoSKKorp AS [No. SK Korporasi]"
                    + ",BentukKorp AS [Bentuk Korporasi]"
                    + ",Kodepos AS [Kodepos]"
                    + ",Project"
                    + " FROM MS_CUSTOMER"
                    + " WHERE NoCustomer = " + NoCustomer
                    );

                Db.Execute("EXEC spCustomerEdit"
                    + "  " + NoCustomer
                    + ",'" + Nama + "'"
                    + ",'" + NamaBisnis + "'"
                    + ",'" + NoTelp + "'"
                    + ",'" + NoHp + "'"
                    + ",'" + NoKantor + "'"
                    + ",'" + NoFax + "'"
                    + ",'" + Email + "'"
                    + ",'" + NoKTP + "'"
                    + ",'" + KTP1 + "'"
                    + ",'" + KTP2 + "'"
                    + ",'" + KTP3 + "'"
                    + ",'" + KTP4 + "'"
                    + ",'" + Alamat1 + "'"
                    + ",'" + Alamat2 + "'"
                    + ",'" + Alamat3 + "'"
                    + ",'" + Kantor1 + "'"
                    + ",'" + Kantor2 + "'"
                    + ",'" + Kantor3 + "'"
                    + ",'" + Agama + "'"
                    //+ ",'" + TglLahir + "'"
                    + ",'" + Status + "'"
                    + ",'" + JenisBisnis + "'"
                    + ",'" + MerekBisnis + "'"
                    + ",'" + UnitLama + "'"
                    + ", " + LuasLama
                    + ",'" + TokoLama + "'"
                    + ",'" + ZoningLama + "'"
                    + ",'" + GedungLama + "'"
                    + ",'" + TeleponLama + "'"
                    + ",'" + AkteLama + "'"
                    + ",'" + TipeCs + "'"
                    + ",'" + Salutation + "'"
                    + ",'" + NPWP + "'"
                    + ",'" + Reff + "'"
                    + ",'" + Kodepos + "'"
                    );

                string Sedup = (sedup.Checked) ? "1" : "0";
                Db.Execute("UPDATE MS_CUSTOMER SET "
                    + " SumberData = '" + SumberData + "'"
                    + ",KTP5 = '" + KTP5 + "'"
                    + ",TempatLahir = '" + TempatLahir + "'"
                    + ",Marital = '" + Marital + "'"
                    + ",Kewarganegaraan = '" + wn + "'"
                    + ",Pekerjaan = '" + Pekerjaan + "'"
                    + ",NPWPAlamat1 = '" + NPWP1 + "'"
                    + ",NPWPAlamat2 = '" + NPWP2 + "'"
                    + ",NPWPAlamat3 = '" + NPWP3 + "'"
                    + ",NPWPAlamat4 = '" + NPWP4 + "'"
                    + ",NPWPAlamat5 = '" + NPWP5 + "'"
                    + ",NoHP2 = '" + NoHp2 + "'"
                    + ",Alamat4 = '" + Alamat4 + "'"
                    + ",Alamat5 = '" + Alamat5 + "'"
                    + ",Kantor4 = '" + Kantor4 + "'"
                    + ",Kantor5 = '" + Kantor5 + "'"
                    + ",NamaKerabat = '" + NamaKerabat + "'"
                    + ",Hubungan = '" + Hubungan + "'"
                    + ",NoHPKerabat = '" + HpKerabat + "'"
                    + ",EmailKerabat = '" + EmailKerabat + "'"
                    + ",Refferator = '" + Reff + "'"
                    + ",Nama2 = '" + Cf.Str(nama2.Text) + "'"
                    + ",PenanggungjawabKorp = '" + k1 + "'"
                    + ",JabatanKorp = '" + k2 + "'"
                    + ",NoSKKorp = '" + k3 + "'"
                    + ",BentukKorp = '" + k4 + "'"
                    + ",KTPSeumurHidup = " + Sedup
                    + ",Project = '" + project.SelectedValue + "'"
                    + " WHERE NoCustomer = " + NoCustomer);

                string strSql = "SELECT NoKontrak FROM MS_KONTRAK WHERE NoCustomer = " + NoCustomer;
                DataTable rs = Db.Rs(strSql);
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PJT"
                           + " SET ALAMAT1 ='" + Alamat1 + "'"
                           + ", ALAMAT2 ='" + Alamat2 + "'"
                           + ", ALAMAT3 ='" + Alamat3 + "'"
                           + ", ALAMAT4 ='" + Alamat4 + "'"
                           + ", ALAMAT5 ='" + Alamat5 + "'"
                           + ", NoTelp ='" + NoTelp + "'"
                           + " WHERE REF='" + rs.Rows[i]["NoKontrak"] + "'"
                           );
                    Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN"
                            + " SET ALAMAT1 ='" + Alamat1 + "'"
                            + ", ALAMAT2 ='" + Alamat2 + "'"
                            + ", ALAMAT3 ='" + Alamat3 + "'"
                            + ", ALAMAT4 ='" + Alamat4 + "'"
                            + ", ALAMAT5 ='" + Alamat5 + "'"
                            + ", NoTelp ='" + NoTelp + "'"
                            + " WHERE REF='" + rs.Rows[i]["NoKontrak"] + "'"
                            );
                }




                if (tgllahir.Text != "")
                    Db.Execute("UPDATE MS_CUSTOMER SET TglLahir = '" + Convert.ToDateTime(tgllahir.Text) + "' WHERE NoCustomer = " + NoCustomer);
                if (tglktp.Text != "")
                    Db.Execute("UPDATE MS_CUSTOMER SET TglKTP = '" + Convert.ToDateTime(tglktp.Text) + "' WHERE NoCustomer = " + NoCustomer);

                DataTable rsAft = Db.Rs("SELECT "
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
                    + ",NoHP2 AS [No. HP2]"
                    + ",NoFax AS [No. Fax]"
                    + ",Email AS [Alamat Email]"
                    + ",Alamat1 AS [Alamat Surat Menyurat]"
                    + ",Alamat2 AS [Alamat Surat Menyurat RT/RW]"
                    + ",Alamat3 AS [Alamat Surat Menyurat Kelurahan]"
                    + ",Alamat4 AS [Alamat Surat Menyurat Kecamatan]"
                    + ",Alamat5 AS [Alamat Surat Menyurat Kotamadya]"
                    + ",Pekerjaan AS [Pekerjaan]"
                    + ",NoKantor AS [No. Telepon Kantor]"
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
                    + ",TglKTP"
                    + ",UnitLama AS [Unit Lama]"
                    + ",LuasLama AS [Luas Unit Lama]"
                    + ",TokoLama AS [Nama Toko Lama]"
                    + ",ZoningLama AS [Zoning Lama]"
                    + ",GedungLama AS [Gedung Lama]"
                    + ",TeleponLama AS [Telepon Lama]"
                    + ",AkteLama AS [Akte Lama]"
                    + ",Salutation"
                    + ",Status"
                    + ",AgentInput AS [Sales Account]"
                    + ",SumberData AS [Sumber Data]"
                    + ",TempatLahir AS [Tempat Lahir]"
                    + ",NamaNPWP AS [Nama NPWP]"
                    + ",NPWP"
                    + ",NPWPAlamat1 AS [Alamat NPWP]"
                    + ",NPWPAlamat2 AS [Alamat NPWP RT/RW]"
                    + ",NPWPAlamat3 AS [Alamat NPWP Kelurahan]"
                    + ",NPWPAlamat4 AS [Alamat NPWP Kecamatan]"
                    + ",NPWPAlamat5 AS [Alamat NPWP Kotamadya]"
                    + ",Marital AS [Status Marital]"
                    + ",NamaKerabat [Nama yang dapat dihubungi]"
                    + ",Hubungan AS [Hubungan]"
                    + ",NoHPKerabat AS [No. HP]"
                    + ",EmailKerabat AS [Email Kerabat]"
                    + ",Kewarganegaraan AS [Kewarganegaraan]"
                    + ",Pekerjaan AS [Pekerjaan]"
                    + ",Refferator"
                    + ",PenanggungjawabKorp AS [Penanggungjawab Korporasi]"
                    + ",JabatanKorp AS [Jabatan Korporasi]"
                    + ",NoSKKorp AS [No. SK Korporasi]"
                    + ",BentukKorp AS [Bentuk Korporasi]"
                    + ",Kodepos AS [Kodepos]"
                    + ",Project"
                    + " FROM MS_CUSTOMER"
                    + " WHERE NoCustomer = " + NoCustomer
                    );

                //Logfile
                string Ket = "Nama Lengkap : " + Nama + "<br>"
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogCustomer"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoCustomer.PadLeft(5, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_CUSTOMER_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_CUSTOMER_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

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
            if (Save()) Response.Redirect("CustomerEdit.aspx?done=1&NoCustomer=" + NoCustomer);
        }

        private string NoCustomer
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoCustomer"]);
            }
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
                korp1.Visible = korp2.Visible = korp3.Visible = korp4.Visible = false;
            }
            else
            {
                kori.Visible = true;
                kori.Checked = true;
                kora.Visible = true;
                wni.Visible = false;
                wna.Visible = false;
                korp1.Visible = korp2.Visible = korp3.Visible = korp4.Visible = true;
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
