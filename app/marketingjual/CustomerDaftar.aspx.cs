using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class CustomerDaftar : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["project"] == null)
                {
                    Act.ProjectList(project);
                }
                else
                {
                    string v = Request.QueryString["project"].ToString();
                    string nproject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project='" + v + "'");
                    //Response.Write("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project='" + v + "'");
                    string t = v + " - " + nproject;
                    project.Items.Add(new ListItem(t, v));
                }

                //kalkulator
                luaslama.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                luaslama.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                luaslama.Attributes["onblur"] = "CalcBlur(this);";

                if (Request.QueryString["NoStock"] != null)
                {
                    if (Request.QueryString["reserve"] != null)
                    {
                        dariReservasi.Checked = true;
                        cancel.Attributes["onclick"] = "location.href='ReservasiDaftar2.aspx"
                            + "?NoStock=" + Request.QueryString["NoStock"] + "'";
                    }
                    else if (Request.QueryString["closing"] != null)
                    {

                        dariClosing.Checked = true;
                        cancel.Attributes["onclick"] = "location.href='ClosingLangsungDaftar2.aspx"
                            + "?NoStock=" + Request.QueryString["NoStock"] + "'";

                    }
                    else if (Request.QueryString["NoUrut"] != null)
                    {
                        dariClosing.Checked = true;
                        cancel.Visible = false;
                        FillReservasi();
                    }
                }
                else
                    cancel.Visible = false;

                Js.Focus(this, nama);
                Fill();
            }
            FeedBack();
        }
        private void FillReservasi()
        {
            nourut.Text = Request.QueryString["NoUrut"].ToString();
            if (Request.QueryString["NUP"].ToString() == "0")// Sumber eNup Non NUP
            {
                string strSql = "SELECT A.*,B.* FROM [NUP03]..CustomerReservasi A"
                + " INNER JOIN [NUP03]..CustomerData B ON A.NoCustomer = B.NoCustomer"
                + " INNER JOIN [NUP03]..AgentData C ON B.NoAgent = C.NoAgent"
                + " WHERE NoReservasi=" + Request.QueryString["NoUrut"] + ""
                + " ORDER BY A.NoReservasi";
                DataTable rs = Db.Rs(strSql);
                if (rs.Rows.Count != 0)
                {
                    nama.Text = rs.Rows[0]["Nama"].ToString();
                    noktp.Text = rs.Rows[0]["NoKTP"].ToString();
                    email.Text = rs.Rows[0]["Email"].ToString();
                    nohp.Text = rs.Rows[0]["NoHP"].ToString();

                    if (rs.Rows[0]["NoHP"].ToString() != "")
                    {
                        string[] nohpp = rs.Rows[0]["NoHP"].ToString().Split(',');
                        nohp.Text = nohpp.Length > 0 ? nohpp[0].Trim() : "";
                        nohp2.Text = nohpp.Length > 1 ? nohpp[1].Trim() : "";
                    }
                    if (rs.Rows[0]["NoTelp"].ToString() != "")
                    {
                        string[] notelpp = rs.Rows[0]["NoTelp"].ToString().Split(',');
                        notelp.Text = notelpp.Length > 0 ? notelpp[0].Trim() : "";
                    }
                }


            }
            else
            {
                string strSql = "SELECT B.*,A.NoNup,D.NoStock,D.NoUnit,A.Status,D.TglInput,D.NoSkema,B.Nama AS [NamaCs],C.Nama AS [NamaSales] FROM [NUP03]..CustomerNUP A"
                           + " INNER JOIN [NUP03]..CustomerNUPDetail D ON A.NoNUP = D.NoNUP"
                           + " INNER JOIN [NUP03]..CustomerData B ON A.NoCustomer = B.NoCustomer"
                           + " INNER JOIN [NUP03]..AgentData C ON B.NoAgent = C.NoAgent"
                           + " WHERE A.NoNUP='" + Request.QueryString["NoUrut"] + "'"
                           + " ORDER BY A.NoNUP";
                DataTable rs = Db.Rs(strSql);
                if (rs.Rows.Count != 0)
                {
                    nama.Text = rs.Rows[0]["NamaCs"].ToString();
                    noktp.Text = rs.Rows[0]["NoKTP"].ToString();
                    email.Text = rs.Rows[0]["Email"].ToString();
                    nohp.Text = rs.Rows[0]["NoHP"].ToString();

                    if (rs.Rows[0]["NoHP"].ToString() != "")
                    {
                        string[] nohpp = rs.Rows[0]["NoHP"].ToString().Split(',');
                        nohp.Text = nohpp.Length > 0 ? nohpp[0].Trim() : "";
                        nohp2.Text = nohpp.Length > 1 ? nohpp[1].Trim() : "";
                    }
                    if (rs.Rows[0]["NoTelp"].ToString() != "")
                    {
                        string[] notelpp = rs.Rows[0]["NoTelp"].ToString().Split(',');
                        notelp.Text = notelpp.Length > 0 ? notelpp[0].Trim() : "";
                    }
                }


            }
        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditCustomer('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
                project.SelectedValue = Request.QueryString["project2"];
                baru.Items.Clear();
                Fill();

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

        private bool valid()
        {
            string s = "";
            bool x = true;

            x = Cf.ValidMandatory(this, "Customer", project.SelectedValue) ? x : false;

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

                if (Cf.isEmpty(nohp))
                {
                    x = false;
                    if (s == "") s = nohp.ID;
                    nohpc.Text = "Kosong";

                }
                else
                    nohpc.Text = "";

                if (Cf.isEmpty(bentuk))
                {
                    x = false;
                    if (s == "") s = bentuk.ID;
                    bentukc.Text = "Kosong";
                }
                else
                    bentukc.Text = "";
            }

            if (Cf.Valid(this, "Customer", project.SelectedValue, tglktp.ID) == true)
            {
                if (sedup.Checked)
                {
                    x = true;
                }
            }

            if (Cf.Valid(this, "Customer", project.SelectedValue, noktp.ID) == true)
            {
                int Length = 16;

                if (wni.Checked)
                {
                    if (noktp.Text.Length != Length)
                    {
                        x = false;
                        if (s == "") s = noktp.ID;

                        noktpc.Text = "Harus " + Length + " Digit";
                    }
                    else
                        noktpc.Text = "";
                }
            }

            if (Cf.Valid(this, "Customer", project.SelectedValue, npwp.ID) == true)
            {
                if (npwp.Text.Length < 15)
                {
                    x = false;
                    if (s == "") s = npwp.ID;
                    npwpc.Text = "Harus 15 Digit";
                }
                else
                    npwpc.Text = "";
            }

            if (Cf.Valid(this, "Customer", project.SelectedValue, tglktp.ID) == true)
            {
                if (!sedup.Checked)
                {
                    x = false;
                    if (s == "") s = tglktp.ID;
                    tglktpc.Text = "Tanggal";
                }
                else
                    tglktpc.Text = "";
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

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                string HP = nohp.Text;
                if (nohp.Text != "")
                {
                    string hp = nohp.Text.Substring(0, 1);
                    if (hp == "0")
                    {
                        HP = nohp.Text.Substring(1);
                    }
                }
                string Nama = Cf.Str(nama.Text);
                string NamaNPWP = Cf.Str(namanpwp.Text);
                string Salutation = Cf.Str(salutation.Text);
                string NamaBisnis = Cf.Str(namabisnis.Text);
                string NoTelp = Cf.Str(notelp.Text);
                string NoHp = Cf.Str(kodehp.Text + HP);
                string NoHp2 = Cf.Str(nohp2.Text);
                string NoKantor = Cf.Str(tlpkantor.Text);
                string NoFax = Cf.Str(nofax.Text);
                string Email = Cf.Str(email.Text);
                string NoKTP = Cf.Str(noktp.Text);
                string KTP1 = Cf.Str(ktp1.Text);
                string KTP2 = Cf.Str(ktp2.Text);
                string KTP3 = Cf.Str(ktp3.Text);
                string KTP4 = Cf.Str(ktp4.Text);
                string KTP5 = Cf.Str(ktp5.Text);
                string Kodepos = Cf.Str(kodepos1.Text);
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
                string NPWP = Cf.Str(npwp.Text);
                string NPWP1 = Cf.Str(npwp1.Text);
                string NPWP2 = Cf.Str(npwp2.Text);
                string NPWP3 = Cf.Str(npwp3.Text);
                string NPWP4 = Cf.Str(npwp4.Text);
                string NPWP5 = Cf.Str(npwp5.Text);
                string Marital = Cf.Str(marital.SelectedValue);
                string Pekerjaan = Cf.Str(pekerjaan.Text);
                string Project = Cf.Str(project.SelectedValue);

                string wn = "";
                if (wni.Checked) wn = "WNI";
                if (wna.Checked) wn = "WNA";
                if (kori.Checked) wn = "KORPORASI INDONESIA";
                if (kora.Checked) wn = "KORPORASI ASING";

                string k1 = "", k2 = "", k3 = "", k4 = "";
                if (kori.Checked || kora.Checked)
                {
                    k1 = Cf.Str(penanggungjawab.Text);
                    k2 = Cf.Str(jabatan.Text);
                    k3 = Cf.Str(nosk.Text);
                    k4 = Cf.Str(bentuk.Text);
                }

                string TipeCs = "";
                if (perorangan.Checked) TipeCs = "PERORANGAN";
                if (badanhukum.Checked) TipeCs = "BADAN HUKUM";

                string SumberData = Cf.Str(sumberdata.SelectedValue);
                string TempatLahir = Cf.Str(tempatlahir.Text);
                string NamaOrgHub = Cf.Str(namahub.Text);
                string Hubungan = Cf.Str(hubungan.SelectedValue);
                string HPHub = Cf.Str(hphub.Text);
                string EmailHub = Cf.Str(emailhub.Text);

                Db.Execute("EXEC spCustomerDaftar"
                    + " '" + Nama + "'"
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
                    + ",'" + Kodepos + "'"
                    + ",'" + Alamat1 + "'"
                    + ",'" + Alamat2 + "'"
                    + ",'" + Alamat3 + "'"
                    + ",'" + Kantor1 + "'"
                    + ",'" + Kantor2 + "'"
                    + ",'" + Kantor3 + "'"
                    + ",'" + Agama + "'"
                    //+ ",'" + TglLahir + "'"
                    + ",'" + JenisBisnis + "'"
                    + ",'" + MerekBisnis + "'"
                    + ",'" + UnitLama + "'"
                    + ", " + LuasLama + ""
                    + ",'" + TokoLama + "'"
                    + ",'" + ZoningLama + "'"
                    + ",'" + GedungLama + "'"
                    + ",'" + TeleponLama + "'"
                    + ",'" + AkteLama + "'"
                    + ",'" + TipeCs + "'"
                    + ",'" + Salutation + "'"
                    + ",'" + NPWP + "'"
                    + ",'" + NPWP1 + "'"
                    + ",'" + wn + "'"
                    + ",'A'"
                    + ",'" + Pekerjaan + "'"
                    );

                //get nomor customer terbaru
                nocustomer.Text = Db.SingleInteger(
                    "SELECT TOP 1 NoCustomer FROM MS_CUSTOMER ORDER BY NoCustomer DESC")
                    .ToString().PadLeft(5, '0');

                string Sedup = (sedup.Checked) ? "1" : "0";
                Db.Execute("UPDATE MS_CUSTOMER SET "
                    + " AgentInput = '" + Act.UserID + "'"
                    + ",SumberData = '" + SumberData + "'"
                    + ",TempatLahir = '" + TempatLahir + "'"
                    + ",Marital = '" + Marital + "'"
                    + ",Kewarganegaraan = '" + wn + "'"
                    + ",Pekerjaan = '" + Pekerjaan + "'"
                    + ",Alamat4 = '" + Alamat4 + "'"
                    + ",Alamat5 = '" + Alamat5 + "'"
                    + ",KTP5 = '" + KTP5 + "'"
                    + ",Kantor4 = '" + Kantor4 + "'"
                    + ",Kantor5 = '" + Kantor5 + "'"
                    + ",NoHP2 = '" + NoHp2 + "'"
                    + ",NamaNPWP = '" + NamaNPWP + "'"
                    + ",NPWPAlamat1 = '" + NPWP1 + "'"
                    + ",NPWPAlamat2 = '" + NPWP2 + "'"
                    + ",NPWPAlamat3 = '" + NPWP3 + "'"
                    + ",NPWPAlamat4 = '" + NPWP4 + "'"
                    + ",NPWPAlamat5 = '" + NPWP5 + "'"
                    + ",NamaKerabat = '" + NamaOrgHub + "'"
                    + ",Hubungan = '" + Hubungan + "'"
                    + ",NoHPKerabat = '" + HPHub + "'"
                    + ",EmailKerabat = '" + EmailHub + "'"
                    + ",Nama2 = '" + Cf.Str(nama2.Text) + "'"
                    + ",PenanggungjawabKorp = '" + k1 + "'"
                    + ",JabatanKorp = '" + k2 + "'"
                    + ",NoSKKorp = '" + k3 + "'"
                    + ",BentukKorp = '" + k4 + "'"
                    + ",Kodepos = '" + Kodepos + "'"
                    + ",Project = '" + Project + "'"
                    + ",KTPSeumurHidup = " + Sedup
                    + " WHERE NoCustomer = " + nocustomer.Text);

                if (tgllahir.Text != "")
                    Db.Execute("UPDATE MS_CUSTOMER SET TglLahir = '" + Convert.ToDateTime(tgllahir.Text) + "' WHERE NoCustomer = " + nocustomer.Text);
                if (tglktp.Text != "")
                    Db.Execute("UPDATE MS_CUSTOMER SET TglKTP = '" + Convert.ToDateTime(tglktp.Text) + "' WHERE NoCustomer = " + NoCustomer);
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
                    + ",NoHP2 AS [No. HP2]"
                    + ",NoKantor AS [No. Telepon Kantor]"
                    + ",NoFax AS [No. Fax]"
                    + ",Email AS [Alamat Email]"
                    + ",Alamat1 AS [Alamat Surat Menyurat]"
                    + ",Alamat2 AS [Alamat Surat Menyurat RT/RW]"
                    + ",Alamat3 AS [Alamat Surat Menyurat Kelurahan]"
                    + ",Alamat4 AS [Alamat Surat Menyurat Kecamatan]"
                    + ",Alamat5 AS [Alamat Surat Menyurat Kotamadya]"
                    + ",Kantor1 AS [Alamat Kantor 1]"
                    + ",Kantor2 AS [Alamat Kantor 2]"
                    + ",Kantor3 AS [Alamat Kantor 3]"
                    + ",Kantor4 AS [Alamat Kantor 4]"
                    + ",Kantor5 AS [Alamat Kantor 5]"
                    + ",NoKTP AS [No. KTP]"
                    + ",KTP1 AS [KTP Alamat]"
                    + ",KTP2 AS [KTP RT/RW]"
                    + ",KTP5 AS [KTP Kelurahan]"
                    + ",KTP3 AS [KTP Kecamatan]"
                    + ",KTP4 AS [KTP Kotamadya]"
                    + ",Kodepos AS [Kodepos]"
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
                    + ",NamaNPWP AS [Nama NPWP]"
                    + ",NPWP"
                    + ",NPWPAlamat1 AS [Alamat NPWP ]"
                    + ",NPWPAlamat2 AS [Alamat NPWP RT/RW]"
                    + ",NPWPAlamat3 AS [Alamat NPWP Kelurahan]"
                    + ",NPWPAlamat4 AS [Alamat NPWP Kecamatan]"
                    + ",NPWPAlamat5 AS [Alamat NPWP Kotamadya]"
                    + ",Marital AS [Status Marital]"
                    + ",Kewarganegaraan AS [Kewarganegaraan]"
                    + ",Pekerjaan AS [Pekerjaan]"
                    + ",NamaKerabat AS [Nama Yang Dapat dihubungi]"
                    + ",Hubungan AS [Hubungan]"
                    + ",NoHPKerabat AS [No. HP]"
                    + ",EmailKerabat AS [Email]"
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
                    + ",'" + NoCustomer.PadLeft(5, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_CUSTOMER_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_CUSTOMER_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

                ////// Save Data Agent dari eNUP - Willy 2019 Mar 13
               // if (Request.QueryString["NoAgent"].ToString() != "")
              //      SaveAgent(Request.QueryString["NoAgent"].ToString());
                ///////End Save Data Agent


                if (dariReservasi.Checked)
                {
                    Response.Redirect("ReservasiDaftar2.aspx"
                        + "?NoStock=" + Request.QueryString["NoStock"]
                        + "&NoCustomer=" + NoCustomer);
                }
                else if (dariClosing.Checked)
                {
                    Response.Redirect("ClosingLangsungDaftar2.aspx"
                         + "?NoStock=" + Request.QueryString["NoStock"]
                         + "&NoCustomer=" + NoCustomer);


                    //if (Request.QueryString["NoAgent"].ToString() == "")
                    //    Response.Redirect("ClosingLangsungDaftar2.aspx"
                    //        + "?NoStock=" + Request.QueryString["NoStock"]
                    //        + "&NoCustomer=" + NoCustomer);
                    //else
                    //{
                    //    //int NoAgent = 0;
                    //    //string NoUrut = Request.QueryString["NoUrut"].ToString();
                    //    //NoAgent = Db.SingleInteger("SELECT NoAgent FROM MS_AGENT WHERE NoAgentNUP='" + Request.QueryString["NoAgent"].ToString() + "'");
                    //    //Response.Redirect("ClosingLangsungDaftar2.aspx"
                    //      + "?NoStock=" + Request.QueryString["NoStock"]
                    //      + "&NUP=" + Request.QueryString["NUP"] + "&NoUrut=" + NoUrut + "&NoAgent=" + NoAgent + "&NoSkema=" + Request.QueryString["NoSkema"].ToString() + "&NoCustomer=" + NoCustomer);
                    //}

                }
                else
                    Response.Redirect("CustomerDaftar.aspx?done=" + NoCustomer + "&project2=" + project.SelectedValue);
            }
            //}
        }
        ////Save Data Agent dari eNUP - Willy 2019 Mar 13
        //SaveAgent();

        private void SaveAgent(string NoAgentNUP)
        {
            int TipeAgent = Db.SingleInteger("SELECT TipeAgent FROM [NUP03]..AgentData WHERE NoAgent='" + NoAgentNUP + "'");
            if (TipeAgent == 1)
            {
                //Inhouse // Hardcode
                //Level 1
                string strSql1 = "SELECT * FROM [NUP03]..AgentData "
                + " WHERE NoAgent=(SELECT ParentID FROM [NUP03]..AgentData WHERE NoAgent=(SELECT ParentID FROM [NUP03]..AgentData WHERE NoAgent='" + NoAgentNUP + "'))";
                DataTable lvl1 = Db.Rs(strSql1);
                string NoAgentNEW1 = "";
                string NoAgentNEW2 = "";
                string NoAgentNEW3 = "";
                if (lvl1.Rows.Count != 0)
                {
                    if (CekAgent(lvl1.Rows[0]["NoAgent"].ToString()))
                    {
                        Response.Write(lvl1.Rows[0]["NoAgent"].ToString());
                        Db.Execute("EXEC spAgentDaftar"
                                   + " '" + lvl1.Rows[0]["Nama"].ToString() + "'"
                                   + ",''"
                                   + ",''"
                                   + ",'" + lvl1.Rows[0]["Alamat"].ToString() + "'"
                                   + ",'" + lvl1.Rows[0]["NoTelp"].ToString() + "'"
                                   + ",'" + lvl1.Rows[0]["NoNPWP"].ToString() + "'"
                                   + ",'" + TipeAgent + "'"
                                   + ",'1'"
                                   + ",''"
                                   + ",'0'"
                                   + ",''"
                                   + ",'" + lvl1.Rows[0]["RekNo"].ToString() + "'"
                                   + ",''"
                                   + ",'" + lvl1.Rows[0]["RekBank"].ToString() + "'"
                                   + ",'" + lvl1.Rows[0]["RekAN"].ToString() + "'"
                                   + ",''"
                                   + ",'1'"
                                   );
                        NoAgentNEW1 = Db.SingleInteger(
                                    "SELECT TOP 1 NoAgent FROM MS_AGENT ORDER BY NoAgent DESC")
                                    .ToString().PadLeft(5, '0');
                        Db.Execute("UPDATE MS_AGENT SET NoAgentNUP='" + lvl1.Rows[0]["NoAgent"].ToString() + "',Handphone = '" + lvl1.Rows[0]["NoHP"].ToString() + "',Email = '" + lvl1.Rows[0]["Email"].ToString() + "',Project='MARC' WHERE NoAgent='" + NoAgentNEW1 + "'");

                    }
                    else
                    {
                        NoAgentNEW1 = Db.SingleInteger(
                                   "SELECT NoAgent FROM MS_AGENT WHERE NoAgentNUP='" + lvl1.Rows[0]["NoAgent"].ToString() + "' ORDER BY NoAgent DESC")
                                   .ToString().PadLeft(5, '0');
                    }
                }
                //Level 2
                string strSql2 = "SELECT * FROM [NUP03]..AgentData WHERE NoAgent=(SELECT ParentID FROM [NUP03]..AgentData WHERE NoAgent='" + NoAgentNUP + "')";
                DataTable lvl2 = Db.Rs(strSql2);
                if (lvl2.Rows.Count != 0)
                {
                    //LvlAtasan=1 Jabatan=2 SalesLevel=2 Atasan = NoAgent di level 1 MS_AGENT
                    if (CekAgent(lvl2.Rows[0]["NoAgent"].ToString()))
                    {
                        Response.Write(lvl2.Rows[0]["NoAgent"].ToString());
                        Db.Execute("EXEC spAgentDaftar"
                                  + " '" + lvl2.Rows[0]["Nama"].ToString() + "'"
                                  + ",''"
                                  + ",''"
                                  + ",'" + lvl2.Rows[0]["Alamat"].ToString() + "'"
                                  + ",'" + lvl2.Rows[0]["NoTelp"].ToString() + "'"
                                  + ",'" + lvl2.Rows[0]["NoNPWP"].ToString() + "'"
                                  + ",'" + TipeAgent + "'"
                                  + ",'2'"//Sales level
                                  + ",''"
                                  + ",'" + NoAgentNEW1 + "'"
                                  + ",''"
                                  + ",'" + lvl2.Rows[0]["RekNo"].ToString() + "'"
                                  + ",''"
                                  + ",'" + lvl2.Rows[0]["RekBank"].ToString() + "'"
                                  + ",'" + lvl2.Rows[0]["RekAN"].ToString() + "'"
                                  + ",''"
                                  + ",'2'" //jabatan
                                  );
                        NoAgentNEW2 = Db.SingleInteger(
                                   "SELECT TOP 1 NoAgent FROM MS_AGENT ORDER BY NoAgent DESC")
                                   .ToString().PadLeft(5, '0');
                        Db.Execute("UPDATE MS_AGENT SET LvlAtasan='1',NoAgentNUP='" + lvl2.Rows[0]["NoAgent"].ToString() + "',Handphone = '" + lvl2.Rows[0]["NoHP"].ToString() + "',Email = '" + lvl2.Rows[0]["Email"].ToString() + "',Project='MARC' WHERE NoAgent='" + NoAgentNEW2 + "'");

                    }
                    else
                    {
                        NoAgentNEW2 = Db.SingleInteger(
                                   "SELECT NoAgent FROM MS_AGENT WHERE NoAgentNUP='" + lvl2.Rows[0]["NoAgent"].ToString() + "' ORDER BY NoAgent DESC")
                                   .ToString().PadLeft(5, '0');
                    }
                }
                //Level 3
                string strSql3 = "SELECT * FROM [NUP03]..AgentData WHERE NoAgent='" + NoAgentNUP + "'";
                DataTable lvl3 = Db.Rs(strSql3);
                if (lvl3.Rows.Count != 0)
                {
                    if (CekAgent(lvl3.Rows[0]["NoAgent"].ToString()))
                    {
                        Response.Write(lvl3.Rows[0]["NoAgent"].ToString());
                        Db.Execute("EXEC spAgentDaftar"
                                 + " '" + lvl3.Rows[0]["Nama"].ToString() + "'"
                                 + ",''"
                                 + ",''"
                                 + ",'" + lvl3.Rows[0]["Alamat"].ToString() + "'"
                                 + ",'" + lvl3.Rows[0]["NoTelp"].ToString() + "'"
                                 + ",'" + lvl3.Rows[0]["NoNPWP"].ToString() + "'"
                                 + ",'" + TipeAgent + "'"
                                 + ",'3'"//Sales level
                                 + ",''"
                                 + ",'" + NoAgentNEW2 + "'"
                                 + ",''"
                                 + ",'" + lvl3.Rows[0]["RekNo"].ToString() + "'"
                                 + ",''"
                                 + ",'" + lvl3.Rows[0]["RekBank"].ToString() + "'"
                                 + ",'" + lvl3.Rows[0]["RekAN"].ToString() + "'"
                                 + ",''"
                                 + ",'3'" //jabatan
                                 );
                        NoAgentNEW3 = Db.SingleInteger(
                                   "SELECT TOP 1 NoAgent FROM MS_AGENT ORDER BY NoAgent DESC")
                                   .ToString().PadLeft(5, '0');
                        Db.Execute("UPDATE MS_AGENT SET LvlAtasan='2',NoAgentNUP='" + lvl3.Rows[0]["NoAgent"].ToString() + "',Handphone = '" + lvl3.Rows[0]["NoHP"].ToString() + "',Email = '" + lvl3.Rows[0]["Email"].ToString() + "',Project='MARC' WHERE NoAgent='" + NoAgentNEW3 + "'");
                    }
                }
                else
                {
                    NoAgentNEW3 = Db.SingleInteger(
                               "SELECT NoAgent FROM MS_AGENT WHERE NoAgentNUP='" + lvl3.Rows[0]["NoAgent"].ToString() + "' ORDER BY NoAgent DESC")
                               .ToString().PadLeft(5, '0');
                }
            }
            else
            {
                //Agent // Hardcode
                //Level 1
                string strSql1 = "SELECT * FROM [NUP03]..AgentData "
                + " WHERE NoAgent=(SELECT ParentID FROM [NUP03]..AgentData WHERE NoAgent=(SELECT ParentID FROM [NUP03]..AgentData WHERE NoAgent='" + NoAgentNUP + "'))";
                DataTable lvl1 = Db.Rs(strSql1);
                string NoAgentNEW1 = "";
                string NoAgentNEW2 = "";
                string NoAgentNEW3 = "";
                if (lvl1.Rows.Count != 0)
                {
                    if (CekAgent(lvl1.Rows[0]["NoAgent"].ToString()))
                    {
                        Response.Write(lvl1.Rows[0]["NoAgent"].ToString());
                        Db.Execute("EXEC spAgentDaftar"
                                   + " '" + lvl1.Rows[0]["Nama"].ToString() + "'"
                                   + ",''"
                                   + ",''"
                                   + ",'" + lvl1.Rows[0]["Alamat"].ToString() + "'"
                                   + ",'" + lvl1.Rows[0]["NoTelp"].ToString() + "'"
                                   + ",'" + lvl1.Rows[0]["NoNPWP"].ToString() + "'"
                                   + ",'" + TipeAgent + "'"
                                   + ",'4'"
                                   + ",''"
                                   + ",'0'"
                                   + ",''"
                                   + ",'" + lvl1.Rows[0]["RekNo"].ToString() + "'"
                                   + ",''"
                                   + ",'" + lvl1.Rows[0]["RekBank"].ToString() + "'"
                                   + ",'" + lvl1.Rows[0]["RekAN"].ToString() + "'"
                                   + ",''"
                                   + ",'1'"
                                   );
                        NoAgentNEW1 = Db.SingleInteger(
                                    "SELECT TOP 1 NoAgent FROM MS_AGENT ORDER BY NoAgent DESC")
                                    .ToString().PadLeft(5, '0');
                        Db.Execute("UPDATE MS_AGENT SET NoAgentNUP='" + lvl1.Rows[0]["NoAgent"].ToString() + "',Handphone = '" + lvl1.Rows[0]["NoHP"].ToString() + "',Email = '" + lvl1.Rows[0]["Email"].ToString() + "',Project='MARC' WHERE NoAgent='" + NoAgentNEW1 + "'");

                    }
                    else
                    {
                        NoAgentNEW1 = Db.SingleInteger(
                                   "SELECT NoAgent FROM MS_AGENT WHERE NoAgentNUP='" + lvl1.Rows[0]["NoAgent"].ToString() + "' ORDER BY NoAgent DESC")
                                   .ToString().PadLeft(5, '0');
                    }
                }
                //Level 2
                string strSql2 = "SELECT * FROM [NUP03]..AgentData WHERE NoAgent=(SELECT ParentID FROM [NUP03]..AgentData WHERE NoAgent='" + NoAgentNUP + "')";
                DataTable lvl2 = Db.Rs(strSql2);
                if (lvl2.Rows.Count != 0)
                {
                    //LvlAtasan=1 Jabatan=2 SalesLevel=2 Atasan = NoAgent di level 1 MS_AGENT
                    if (CekAgent(lvl2.Rows[0]["NoAgent"].ToString()))
                    {
                        Response.Write(lvl2.Rows[0]["NoAgent"].ToString());
                        Db.Execute("EXEC spAgentDaftar"
                                  + " '" + lvl2.Rows[0]["Nama"].ToString() + "'"
                                  + ",''"
                                  + ",''"
                                  + ",'" + lvl2.Rows[0]["Alamat"].ToString() + "'"
                                  + ",'" + lvl2.Rows[0]["NoTelp"].ToString() + "'"
                                  + ",'" + lvl2.Rows[0]["NoNPWP"].ToString() + "'"
                                  + ",'" + TipeAgent + "'"
                                  + ",'5'"//Sales level
                                  + ",''"
                                  + ",'" + NoAgentNEW1 + "'"
                                  + ",''"
                                  + ",'" + lvl2.Rows[0]["RekNo"].ToString() + "'"
                                  + ",''"
                                  + ",'" + lvl2.Rows[0]["RekBank"].ToString() + "'"
                                  + ",'" + lvl2.Rows[0]["RekAN"].ToString() + "'"
                                  + ",''"
                                  + ",'2'" //jabatan
                                  );
                        NoAgentNEW2 = Db.SingleInteger(
                                   "SELECT TOP 1 NoAgent FROM MS_AGENT ORDER BY NoAgent DESC")
                                   .ToString().PadLeft(5, '0');
                        Db.Execute("UPDATE MS_AGENT SET LvlAtasan='1',NoAgentNUP='" + lvl2.Rows[0]["NoAgent"].ToString() + "',Handphone = '" + lvl2.Rows[0]["NoHP"].ToString() + "',Email = '" + lvl2.Rows[0]["Email"].ToString() + "',Project='MARC' WHERE NoAgent='" + NoAgentNEW2 + "'");

                    }
                    else
                    {
                        NoAgentNEW2 = Db.SingleInteger(
                                   "SELECT NoAgent FROM MS_AGENT WHERE NoAgentNUP='" + lvl2.Rows[0]["NoAgent"].ToString() + "' ORDER BY NoAgent DESC")
                                   .ToString().PadLeft(5, '0');
                    }
                }
                //Level 3
                string strSql3 = "SELECT * FROM [NUP03]..AgentData WHERE NoAgent='" + NoAgentNUP + "'";
                DataTable lvl3 = Db.Rs(strSql3);
                if (lvl3.Rows.Count != 0)
                {
                    if (CekAgent(lvl3.Rows[0]["NoAgent"].ToString()))
                    {
                        Response.Write(lvl3.Rows[0]["NoAgent"].ToString());
                        Db.Execute("EXEC spAgentDaftar"
                                 + " '" + lvl3.Rows[0]["Nama"].ToString() + "'"
                                 + ",''"
                                 + ",''"
                                 + ",'" + lvl3.Rows[0]["Alamat"].ToString() + "'"
                                 + ",'" + lvl3.Rows[0]["NoTelp"].ToString() + "'"
                                 + ",'" + lvl3.Rows[0]["NoNPWP"].ToString() + "'"
                                 + ",'" + TipeAgent + "'"
                                 + ",'6'"//Sales level
                                 + ",''"
                                 + ",'" + NoAgentNEW2 + "'"
                                 + ",''"
                                 + ",'" + lvl3.Rows[0]["RekNo"].ToString() + "'"
                                 + ",''"
                                 + ",'" + lvl3.Rows[0]["RekBank"].ToString() + "'"
                                 + ",'" + lvl3.Rows[0]["RekAN"].ToString() + "'"
                                 + ",''"
                                 + ",'3'" //jabatan
                                 );
                        NoAgentNEW3 = Db.SingleInteger(
                                   "SELECT TOP 1 NoAgent FROM MS_AGENT ORDER BY NoAgent DESC")
                                   .ToString().PadLeft(5, '0');
                        Db.Execute("UPDATE MS_AGENT SET LvlAtasan='2',NoAgentNUP='" + lvl3.Rows[0]["NoAgent"].ToString() + "',Handphone = '" + lvl3.Rows[0]["NoHP"].ToString() + "',Email = '" + lvl3.Rows[0]["Email"].ToString() + "',Project='MARC' WHERE NoAgent='" + NoAgentNEW3 + "'");
                    }
                }
                else
                {
                    NoAgentNEW3 = Db.SingleInteger(
                               "SELECT NoAgent FROM MS_AGENT WHERE NoAgentNUP='" + lvl3.Rows[0]["NoAgent"].ToString() + "' ORDER BY NoAgent DESC")
                               .ToString().PadLeft(5, '0');
                }
            }

        }
        protected bool CekAgent(string NoAgentNUP)
        {
            bool x = true;
            int Count = 0;
            Count = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_AGENT WHERE NoAgentNUP='" + NoAgentNUP + "'");
            if (Count != 0)
                x = false;

            return x;
        }
        ////End Save Data Agent
        private void Fill()
        {
            string strSql = "SELECT TOP 25 NoCustomer, Nama "
                + " FROM MS_CUSTOMER WHERE Project = '" + project.SelectedValue + "'"
                + " ORDER BY TglInput DESC, NoCustomer DESC";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoCustomer"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v + ")";

                baru.Items.Add(new ListItem(t, v));
            }

            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditCustomer("
                    + "this.options[this.selectedIndex].value)";
            }

            kori.Visible = false;
            kora.Visible = false;
            wni.Visible = true;
            wni.Checked = true;
            wna.Visible = true;
            korp1.Visible = korp2.Visible = korp3.Visible = korp4.Visible = false;
        }

        private string NoCustomer
        {
            get
            {
                return Cf.Pk(nocustomer.Text);
            }
        }
        
        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            baru.Items.Clear();
            Fill();
        }
    }
}
