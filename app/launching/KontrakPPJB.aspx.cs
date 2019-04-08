using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class KontrakPPJB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                backbtn.Visible = false;
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&ppjb=1');";

                if (Request.QueryString["NoKontrak"] != null)
                {
                    dariReminder.Checked = true;
                    nokontrak.Text = Request.QueryString["NoKontrak"];
                    LoadKontrak();

                    cancel.Attributes["onclick"] = "location.href='ReminderPPJB.aspx'";
                }
                else
                {
                    Js.Focus(this, nokontrak);
                    frm.Visible = false;
                }
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas PPJB?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                        + "PPJB Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;
            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A' AND PPJB <> 'D'");

            if (c == 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Prosedur PPJB sudah dijalankan.\\n"
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );

            return x;
        }

        private void LoadKontrak()
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                Fill();

                Js.Focus(this, save);
                Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas PPJB?");
            }
            else
            {
                backbtn.Visible = true;
                Js.Focus(this, nokontrak);
                frm.Visible = false;
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                Fill();

                Js.Focus(this, save);
                Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas PPJB?");
            }
        }

        private void Fill()
        {
            nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

            nilaikpa.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaikpa.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaikpa.Attributes["onblur"] = "CalcBlur(this);";

            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                tglppjb.Text = Cf.Day(DateTime.Today);

                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);
                jenis.Text = rs.Rows[0]["Jenis"].ToString();
                //if((decimal)rs.Rows[0]["PersenLunas"] < 20)
                //    lunasinfo.Text = "PELUNASAN BELUM MENCAPAI 20%";
            }
        }

        private bool datavalid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tglppjb))
            {
                x = false;
                if (s == "") s = tglppjb.ID;
                tglppjbc.Text = "Tanggal";
            }
            else
                tglppjbc.Text = "";


            //if (seb.SelectedValue == "DIREKTUR")
            //{
            //    if (Cf.isEmpty(tglskuasa))
            //    {
            //        x = false;
            //        if (s == "") s = tglskuasa.ID;
            //        tglskuasac.Text = "Kosong";
            //    }
            //    else if (!Cf.isEmpty(tglskuasa) && !Cf.isTgl(tglskuasa))
            //    {
            //        x = false;
            //        if (s == "") s = tglskuasa.ID;
            //        tglskuasac.Text = "Format salah";
            //    }
            //    else
            //        tglskuasac.Text = "";

            //    if (Cf.isEmpty(akta))
            //    {
            //        x = false;
            //        if (s == "") s = akta.ID;
            //        aktac.Text = "Kosong";
            //    }
            //    else
            //        aktac.Text = "";

            //    if (Cf.isEmpty(nosk))
            //    {
            //        x = false;
            //        if (s == "") s = nosk.ID;
            //        noskc.Text = "Kosong";
            //    }
            //    else
            //        noskc.Text = "";

            //    if (Cf.isEmpty(nama))
            //    {
            //        x = false;
            //        if (s == "") s = nama.ID;
            //        namac.Text = "Kosong";
            //    }
            //    else
            //        namac.Text = "";

            //    if (Cf.isEmpty(kedudukan))
            //    {
            //        x = false;
            //        if (s == "") s = kedudukan.ID;
            //        kedudukanc.Text = "Kosong";
            //    }
            //    else
            //        kedudukanc.Text = "";

            //    if (Cf.isEmpty(alamat))
            //    {
            //        x = false;
            //        if (s == "") s = alamat.ID;
            //        alamatc.Text = "Kosong";
            //    }
            //    else
            //        alamatc.Text = "";

            //    if (Cf.isEmpty(rt) || Cf.isEmpty(rw))
            //    {
            //        x = false;
            //        if (Cf.isEmpty(rt) && s == "") s = rt.ID;
            //        if (Cf.isEmpty(rw) && s == "") s = rw.ID;

            //        rtrwc.Text = "Kosong";
            //    }
            //    else
            //        rtrwc.Text = "";

            //    if (Cf.isEmpty(kel))
            //    {
            //        x = false;
            //        if (s == "") s = kel.ID;
            //        kelc.Text = "Kosong";
            //    }
            //    else
            //        kelc.Text = "";

            //    if (Cf.isEmpty(kec))
            //    {
            //        x = false;
            //        if (s == "") s = kec.ID;
            //        kecc.Text = "Kosong";
            //    }
            //    else
            //        kecc.Text = "";

            //    if (Cf.isEmpty(komad))
            //    {
            //        x = false;
            //        if (s == "") s = komad.ID;
            //        komadc.Text = "Kosong";
            //    }
            //    else
            //        komadc.Text = "";

            //    if (Cf.isEmpty(prov))
            //    {
            //        x = false;
            //        if (s == "") s = prov.ID;
            //        provc.Text = "Kosong";
            //    }
            //    else
            //        provc.Text = "";

            //    trSrtKuasa.Attributes.Add("style", "display:block");
            //    trSrtPersetujuan.Attributes.Add("style", "display:none");
            //    trNoAkta.Attributes.Add("style", "display:block");
            //    trNoSK.Attributes.Add("style", "display:block");
            //    trNama.Attributes.Add("style", "display:block");
            //    trKedudukan.Attributes.Add("style", "display:block");
            //    trAlamat.Attributes.Add("style", "display:block");
            //    trRTRW.Attributes.Add("style", "display:block");
            //    trKel.Attributes.Add("style", "display:block");
            //    trKec.Attributes.Add("style", "display:block");
            //    trKomad.Attributes.Add("style", "display:block");
            //    trProv.Attributes.Add("style", "display:block");
            //}
            //else if (seb.SelectedValue == "KUASA")
            //{
            //    if (Cf.isEmpty(tglskuasa))
            //    {
            //        x = false;
            //        if (s == "") s = tglskuasa.ID;
            //        tglskuasac.Text = "Kosong";
            //    }
            //    else if (!Cf.isEmpty(tglskuasa) && !Cf.isTgl(tglskuasa))
            //    {
            //        x = false;
            //        if (s == "") s = tglskuasa.ID;
            //        tglskuasac.Text = "Format salah";
            //    }
            //    else
            //        tglskuasac.Text = "";

            //    if (Cf.isEmpty(tglspersetujuan))
            //    {
            //        x = false;
            //        if (s == "") s = tglspersetujuan.ID;
            //        tglspersetujuanc.Text = "Kosong";
            //    }
            //    else if (!Cf.isEmpty(tglspersetujuan) && !Cf.isTgl(tglspersetujuan))
            //    {
            //        x = false;
            //        if (s == "") s = tglspersetujuan.ID;
            //        tglspersetujuanc.Text = "Format salah";
            //    }
            //    else
            //        tglspersetujuanc.Text = "";

            //    if (Cf.isEmpty(nama))
            //    {
            //        x = false;
            //        if (s == "") s = nama.ID;
            //        namac.Text = "Kosong";
            //    }
            //    else
            //        namac.Text = "";

            //    if (Cf.isEmpty(kedudukan))
            //    {
            //        x = false;
            //        if (s == "") s = kedudukan.ID;
            //        kedudukanc.Text = "Kosong";
            //    }
            //    else
            //        kedudukanc.Text = "";

            //    if (Cf.isEmpty(alamat))
            //    {
            //        x = false;
            //        if (s == "") s = alamat.ID;
            //        alamatc.Text = "Kosong";
            //    }
            //    else
            //        alamatc.Text = "";

            //    if (Cf.isEmpty(rt) || Cf.isEmpty(rw))
            //    {
            //        x = false;
            //        if (Cf.isEmpty(rt) && s == "") s = rt.ID;
            //        if (Cf.isEmpty(rw) && s == "") s = rw.ID;

            //        rtrwc.Text = "Kosong";
            //    }
            //    else
            //        rtrwc.Text = "";

            //    if (Cf.isEmpty(kel))
            //    {
            //        x = false;
            //        if (s == "") s = kel.ID;
            //        kelc.Text = "Kosong";
            //    }
            //    else
            //        kelc.Text = "";

            //    if (Cf.isEmpty(kec))
            //    {
            //        x = false;
            //        if (s == "") s = kec.ID;
            //        kecc.Text = "Kosong";
            //    }
            //    else
            //        kecc.Text = "";

            //    if (Cf.isEmpty(komad))
            //    {
            //        x = false;
            //        if (s == "") s = komad.ID;
            //        komadc.Text = "Kosong";
            //    }
            //    else
            //        komadc.Text = "";

            //    if (Cf.isEmpty(prov))
            //    {
            //        x = false;
            //        if (s == "") s = prov.ID;
            //        provc.Text = "Kosong";
            //    }
            //    else
            //        provc.Text = "";

            //    trSrtKuasa.Attributes.Add("style", "display:block");
            //    trSrtPersetujuan.Attributes.Add("style", "display:block");
            //    trNoAkta.Attributes.Add("style", "display:none");
            //    trNoSK.Attributes.Add("style", "display:none");
            //    trNama.Attributes.Add("style", "display:block");
            //    trKedudukan.Attributes.Add("style", "display:block");
            //    trAlamat.Attributes.Add("style", "display:block");
            //    trRTRW.Attributes.Add("style", "display:block");
            //    trKel.Attributes.Add("style", "display:block");
            //    trKec.Attributes.Add("style", "display:block");
            //    trKomad.Attributes.Add("style", "display:block");
            //    trProv.Attributes.Add("style", "display:block");
            //}

            if (!Cf.isMoney(nilaibiaya))
            {
                x = false;
                if (s == "") s = nilaibiaya.ID;
                nilaibiayac.Text = "Angka";
            }
            else
                nilaibiayac.Text = "";

            if (!Cf.isMoney(nilaikpa))
            {
                x = false;
                if (s == "") s = nilaikpa.ID;
                nilaikpac.Text = "Angka";
            }
            else
                nilaikpac.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Isian form belum sesuai / lengkap.\\n"
                    + "2. Biaya Administrasi harus berupa angka."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool BFLunas()
        {
            string s = "";
            bool x = true;

            //Dibuat dengan Db.Rs karena harus ngecek, ada kemungkinan Table Tagihan kosong atau Table Pelunasannya kosong. -- Himawan
            DataTable dtTagihanBF = Db.Rs("SELECT ISNULL(NilaiTagihan,0) AS NilaiTagihan FROM MS_TAGIHAN WHERE NoKontrak='" + NoKontrak + "' AND NoUrut=1");
            DataTable dtPelunasanBF = Db.Rs("SELECT ISNULL(SUM(NilaiPelunasan),0) AS TotalPelunasan FROM MS_PELUNASAN WHERE NoKontrak='" + NoKontrak + "' AND NoTagihan = 1");

            decimal TagihanBF = 100002; //Angka tembak, boleh diganti
            decimal PelunasanBF = 100001; //Angka tembak, boleh diganti

            if(dtTagihanBF.Rows.Count > 0)
                TagihanBF = Convert.ToDecimal(dtTagihanBF.Rows[0]["NilaiTagihan"]);
            if (dtPelunasanBF.Rows.Count > 0)
                PelunasanBF = Convert.ToDecimal(dtPelunasanBF.Rows[0]["TotalPelunasan"]);

            decimal SisaBF = TagihanBF - PelunasanBF;

            if (SisaBF > 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "Harap melakukan pelunasan Booking Fee.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool isUnique(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoPPJB = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
        }

        private string AutoID()
        {
            string x = "";
            int c = Db.SingleInteger("SELECT COUNT(NoPPJB) FROM MS_KONTRAK "
                + " WHERE PPJB = 'D'"
                );

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                x = c.ToString().PadLeft(5, '0');

                if (isUnique(x))
                {
                    x = x + "/SP/PPJB/" + Cf.Roman(DateTime.Now.Month) + "/" + DateTime.Now.Year;
                    hasfound = true;
                }
            }

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
		{
            if (datavalid() && BFLunas())
			{
				DateTime TglPPJB = new DateTime();
                DateTime TglSuratKuasa = new DateTime();
                DateTime TglSuratPersetujuan = new DateTime();

                if(tglppjb.Text != "")
                    TglPPJB = Convert.ToDateTime(tglppjb.Text);

                if(tglskuasa.Text != "")
                    TglSuratKuasa = Convert.ToDateTime(tglskuasa.Text);

                if(tglspersetujuan.Text != "")
                    TglSuratPersetujuan = Convert.ToDateTime(tglspersetujuan.Text);

                string Sebagai = Cf.Str(seb.SelectedValue);
                string kAkta = Cf.Str(Cf.FileSafe(akta.Text));
                string kSK = Cf.Str(Cf.FileSafe(nosk.Text));
                string kNama = Cf.Str(Cf.FileSafe(nama.Text));
                string kKedudukan = Cf.Str(Cf.FileSafe(kedudukan.Text));
                string kAlamat = Cf.Str(Cf.FileSafe(alamat.Text));
                string kRT = Cf.Str(Cf.FileSafe(rt.Text));
                string kRW = Cf.Str(Cf.FileSafe(rw.Text));
                string kKel = Cf.Str(Cf.FileSafe(kel.Text));
                string kKec = Cf.Str(Cf.FileSafe(kec.Text));
                string kKomad = Cf.Str(Cf.FileSafe(komad.Text));
                string kProv = Cf.Str(Cf.FileSafe(prov.Text));

				string NoPPJB = Db.SingleString("SELECT NoPPJB FROM MS_KONTRAK WHERE NoKontrak = '"+ NoKontrak +"'");
				if(NoPPJB == "")
					NoPPJB = AutoID();

				Db.Execute("EXEC spKontrakPPJB "
					+ " '" + NoKontrak + "'"
					+ ",'" + NoPPJB + "'"
					+ ",'" + TglPPJB + "'"
					);

                //Notes : Data-data PPJB ini dibuat by request, tapi pada akhirnya tidak digunakan. (Per 10 Juni 2015 - Himawan)
				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET NilaiRealisasiKPR = " + Convert.ToDecimal(nilaikpa.Text)
                    + ", RekeningCairKPR = '" + rekcair.SelectedValue + "'"
                    + ", PPJBSuratKuasaTgl = '" + TglSuratKuasa + "'"
                    + ", PPJBSuratPersetujuanTgl = '" + TglSuratPersetujuan + "'"
                    + ", PPJBSebagai = '" + Sebagai + "'"
                    + ", PPJBSebagaiNoAkta = '" + kAkta + "'"
                    + ", PPJBSebagaiNoSK = '" + kSK + "'"
                    + ", PPJBSebagaiNama = '" + kNama + "'"
                    + ", PPJBSebagaiKedudukan = '" + kKedudukan + "'"
                    + ", PPJBSebagaiAlamat = '" + kAlamat + "'"
                    + ", PPJBSebagaiRT = '" + kRT + "'"
                    + ", PPJBSebagaiRW = '" + kRW + "'"
                    + ", PPJBSebagaiKel = '" + kKel + "'"
                    + ", PPJBSebagaiKec = '" + kKec + "'"
                    + ", PPJBSebagaiKomad = '" + kKomad + "'"
                    + ", PPJBSebagaiProv = '" + kProv + "'"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);
				
				DataTable rs = Db.Rs("SELECT"
					+ " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
					+ ",MS_KONTRAK.NoUnit AS [Unit]"
					+ ",MS_CUSTOMER.Nama AS [Customer]"
					+ ",NoPPJB AS [No. PPJB]"
					+ ",CONVERT(varchar, TglPPJB, 106) AS [Tanggal PPJB]"
					+ ",PersenLunas AS [Prosentase Pelunasan]"
					+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
					+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
					+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

				decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
				if(NilaiBiaya!=0)
				{
					Db.Execute("EXEC spTagihanDaftar "
						+ " '" + NoKontrak + "'"
						+ ",'BIAYA ADM. PPJB'"
						+ ",'" + TglPPJB + "'"
						+ ", " + NilaiBiaya
						+ ",'ADM'"
						);
				}

				string ket = Cf.LogCapture(rs)
					+ "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
					;

				Db.Execute("EXEC spLogKontrak "
					+ " 'PPJB'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + ket + "'"
					+ ",'" + NoKontrak + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                if (dariReminder.Checked)
					Response.Redirect("ReminderPPJB.aspx?done="+NoKontrak);
				else
					Response.Redirect("KontrakPPJB.aspx?done="+NoKontrak);
			}
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
}
}
