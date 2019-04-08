using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class NUPEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                bindAgent();
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

        private void bindAgent()
        {
            //Sales
            DataTable rs = Db.Rs("SELECT Nama, Principal, NoAgent FROM MS_AGENT WHERE Status = 'A'"
                + " AND Project = '" + Request.QueryString["Project"] + "'"
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

        private void Fill()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_NUP_LOG&Pk=" + NoNUP + "&Tipe=" + Tipe + "&Project=" + Project + "'";
            printNUP.HRef = "PrintNUP.aspx?NoNUP=" + NoNUP + "&Tipe=" + Tipe + "&Project=" + Project;
            printRefund.HRef = "PrintRefund.aspx?NoNUP=" + NoNUP + "&Tipe=" + Tipe + "&Project=" + Project;

            DataTable dtNUP = Db.Rs("SELECT * FROM MS_NUP WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");

            if (dtNUP.Rows.Count > 0)
            {
                string tampilNUP = dtNUP.Rows[0]["NoNUP"].ToString();
                int intrevisi = Convert.ToInt32(dtNUP.Rows[0]["Revisi"]);
                if (intrevisi > 0)
                    tampilNUP = tampilNUP + "R" + dtNUP.Rows[0]["Revisi"].ToString();

                nomorNUP.Text = tampilNUP;
                tipe.Text = dtNUP.Rows[0]["Tipe"].ToString();
                revisi.Text = dtNUP.Rows[0]["Revisi"].ToString();
                printNUP.InnerHtml = printNUP.InnerHtml + " (" + dtNUP.Rows[0]["PrintNUP"] + ")";

                string NoTTS = "", NoTTS2 = "";
                NoTTS = Db.SingleString("SELECT NoTTS FROM MS_NUP_PELUNASAN WHERE NoNUP = '" + NoNUP + "' AND PelunasanKe=1 AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");
                NoTTS2 = Db.SingleString("SELECT NoTTS FROM MS_NUP_PELUNASAN WHERE NoNUP = '" + NoNUP + "' AND PelunasanKe=2 AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");

                if (NoTTS != "")
                {
                    int HitPrintTTS = 0;

                    DataTable dtT = Db.Rs("SELECT * FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS ='" + NoTTS + "'");

                    if (dtT.Rows.Count > 0)
                    {
                        HitPrintTTS = Db.SingleInteger("SELECT PrintTTS,0 FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS ='" + NoTTS + "'");
                        printTTS.HRef = "PrintTTS.aspx?NoTTS=" + NoTTS + "&project=" + Project;
                        printTTS.InnerHtml = printTTS.InnerHtml + " (" + HitPrintTTS.ToString() + ")";
                    }
                    else
                        printTTS.Visible = false;
                }

                if (NoTTS2 != "")
                {
                    int HitPrintTTS = 0;

                    DataTable dtT = Db.Rs("SELECT * FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS ='" + NoTTS2 + "'");

                    if (dtT.Rows.Count > 0)
                    {
                        HitPrintTTS = Db.SingleInteger("SELECT PrintTTS,0 FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS ='" + NoTTS2 + "'");
                        printTTS2.HRef = "PrintTTS.aspx?NoTTS=" + NoTTS2 + "&project=" + Project;
                        printTTS2.InnerHtml = printTTS2.InnerHtml + " (" + HitPrintTTS.ToString() + ")";
                    }
                    else
                        printTTS2.Visible = false;
                }

                decimal nLunas = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");
                lunasrupiah.Text = "Rp " + Cf.Num(nLunas);

                decimal pLunas = (nLunas / (decimal)5000000) * 100;
                lunaspersen.Text = "(" + Math.Round(pLunas, 0).ToString() + "%)";

                DataTable dtCS = Db.Rs("SELECT * FROM MS_CUSTOMER WHERE NoCustomer = " + Convert.ToInt32(dtNUP.Rows[0]["NoCustomer"]));
                DataTable dtAG = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent = " + Convert.ToInt32(dtNUP.Rows[0]["NoAgent"]));

                nama.Text = dtCS.Rows[0]["Nama"].ToString();
                ctelp.Text = dtCS.Rows[0]["NoTelp"].ToString();
                chp.Text = dtCS.Rows[0]["NoHP"].ToString();
                email.Text = dtCS.Rows[0]["Email"].ToString();
                string tahun = Cf.Day(dtCS.Rows[0]["TglLahir"]);
                if (tahun == "01 Jan 1900")
                {
                    tgllahir.Text = "";
                }
                else
                {
                    tgllahir.Text = Cf.Day(dtCS.Rows[0]["TglLahir"]);
                }
                noktp.Text = dtCS.Rows[0]["NoKTP"].ToString();
                npwp.Text = dtCS.Rows[0]["NPWP"].ToString();
                agent.SelectedValue = dtNUP.Rows[0]["NoAgent"].ToString();

                //Alamat KTP
                ktp1.Text = dtCS.Rows[0]["KTP1"].ToString();
                ktp2.Text = dtCS.Rows[0]["KTP2"].ToString();
                ktp3.Text = dtCS.Rows[0]["KTP3"].ToString();
                ktp4.Text = dtCS.Rows[0]["KTP4"].ToString();

                //Alamat Korespondensi
                Korespon1.Text = dtCS.Rows[0]["Alamat1"].ToString();
                Korespon2.Text = dtCS.Rows[0]["Alamat2"].ToString();
                Korespon3.Text = dtCS.Rows[0]["Alamat3"].ToString();
                Korespon4.Text = dtCS.Rows[0]["Alamat4"].ToString();

                if (ktp1.Text == Korespon1.Text && ktp2.Text == ktp2.Text)
                {
                    sama.Checked = true;
                }
                else
                {
                    beda.Checked = true;
                }

                //Rekening Refund
                bank.Text = dtCS.Rows[0]["RekBank"].ToString();
                cabang.Text = dtCS.Rows[0]["RekCabang"].ToString();
                rek.Text = dtCS.Rows[0]["RekNo"].ToString();
                reknama.Text = dtCS.Rows[0]["RekNama"].ToString();
            }

            DataTable dtNUPPel = Db.Rs("SELECT * FROM MS_NUP_PELUNASAN WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            //nama
            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "";

            //kontak
            if (Cf.isEmpty(ctelp) || Cf.isEmpty(chp))
            {
                x = false;
                if (s == "")
                {
                    if (Cf.isEmpty(ctelp))
                        s = ctelp.ID;
                    else if (Cf.isEmpty(chp))
                        s = chp.ID;
                }
                ctelpc.Text = "Kosong";
            }
            else
                ctelpc.Text = "";

            ////npwp
            if (Cf.isEmpty(noktp))
            {
                x = false;
                if (s == "") s = noktp.ID;
                npwpc.Text = "Kosong";
            }
            else
                npwpc.Text = "";

            //noktp
            if (Cf.isEmpty(noktp))
            {
                x = false;
                if (s == "") s = noktp.ID;
                noktpc.Text = "Kosong";
            }
            else
                noktpc.Text = "";

            //bank
            if (Cf.isEmpty(bank))
            {
                x = false;
                if (s == "") s = bank.ID;
                bankc.Text = "Kosong";
            }
            else
                bankc.Text = "";

            //cabang
            if (Cf.isEmpty(cabang))
            {
                x = false;
                if (s == "") s = cabang.ID;
                cabangc.Text = "Kosong";
            }
            else
                cabangc.Text = "";

            //no rek
            if (Cf.isEmpty(rek))
            {
                x = false;
                if (s == "") s = rek.ID;
                rekc.Text = "Kosong";
            }
            else
                rekc.Text = "";

            //nama rek
            if (Cf.isEmpty(reknama))
            {
                x = false;
                if (s == "") s = reknama.ID;
                reknamac.Text = "Kosong";
            }
            else
                reknamac.Text = "";

            //Agent
            if (agent.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = agent.ID;
                agentc.Text = "Pilih satu";
            }
            else
                agentc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Masukkan data yang diperlukan.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                saveEdit();
                Response.Redirect("NUPEdit.aspx?done=1&NoNUP=" + NoNUP + "&Jenis=" + Tipe + "&Project=" + Project);
            }
        }

        private void saveEdit()
        {
            string Nama = Cf.Str(nama.Text);
            string NoHP = Cf.Str(chp.Text);
            string NoTelp = Cf.Str(ctelp.Text);
            string Mail = Cf.Str(email.Text);
            string TglLahir = "";
            if (tgllahir.Text == "")
            {
                TglLahir = Cf.Str(tgllahir.Text);
            }
            string NoKTP = Cf.Str(noktp.Text);
            string NPWP = Cf.Str(npwp.Text);
            string KTP1 = Cf.Str(ktp1.Text);
            string KTP2 = Cf.Str(ktp2.Text);
            string KTP3 = Cf.Str(ktp3.Text);
            string KTP4 = Cf.Str(ktp4.Text);
            string Kor1 = Cf.Str(Korespon1.Text);
            string Kor2 = Cf.Str(Korespon2.Text);
            string Kor3 = Cf.Str(Korespon3.Text);
            string Kor4 = Cf.Str(Korespon4.Text);
            string Tipe = Cf.Str(tipe.Text);
            string rekN = Cf.Str(rek.Text);
            string rekNam = Cf.Str(reknama.Text);
            string rekB = Cf.Str(bank.Text);
            string rekC = Cf.Str(cabang.Text);
            int NoAgent = Convert.ToInt32(agent.SelectedValue);

            DataTable dtNUP = Db.Rs("SELECT * FROM MS_NUP WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");

            decimal noCS = Convert.ToInt32(dtNUP.Rows[0]["NoCustomer"].ToString());

            //Log NUP //Ditaro disini karena akan ngebaca data customer yang berubah.
            DataTable rsBefNUP = Db.Rs("SELECT "
                + " MS_NUP.NoNUP AS [NUP]"
                + ",MS_AGENT.Nama AS [Nama Sales]"
                + ",MS_CUSTOMER.Nama AS [Nama Customer]"
                + ",MS_CUSTOMER.NoTelp AS [No. Telepon]"
                + ",MS_CUSTOMER.NoHp AS [No. HP]"
                + ",MS_CUSTOMER.Email AS [Email]"
                + ",MS_CUSTOMER.TglLahir AS [Tgl Lahir]"
                + ",MS_CUSTOMER.NoKTP AS [No. KTP]"
                + ",MS_CUSTOMER.NPWP AS [No. NPWP]"
                + ",MS_CUSTOMER.KTP1 AS [KTP Alamat]"
                + ",MS_CUSTOMER.KTP2 AS [KTP RT/RW]"
                + ",MS_CUSTOMER.KTP3 AS [KTP Kecamatan]"
                + ",MS_CUSTOMER.KTP4 AS [KTP Kotamadya]"
                + ",MS_CUSTOMER.Alamat1 AS [Korespoden Alamat]"
                + ",MS_CUSTOMER.Alamat2 AS [Korespoden RT/RW]"
                + ",MS_CUSTOMER.Alamat3 AS [Korespoden Kecamatan]"
                + ",MS_CUSTOMER.Alamat4 AS [Korespoden Kotamadya]"
                + ",MS_CUSTOMER.RekBank AS [Bank Refund]"
                + ",MS_CUSTOMER.RekCabang AS [Cabang Bank Refund]"
                + ",MS_CUSTOMER.RekNo AS [No. Rek Refund]"
                + ",MS_CUSTOMER.RekNama AS [Atas Nama Rek Refund]"
                //+ ",MS_NUP.UserInputNama AS [Daftar oleh]"
                + " FROM MS_NUP INNER JOIN MS_AGENT ON MS_NUP.NoAgent = MS_AGENT.NoAgent"
                + " INNER JOIN MS_CUSTOMER ON MS_NUP.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_NUP.NoNUP='" + NoNUP + "' AND MS_NUP.Tipe = '" + Tipe + "' AND MS_NUP.Project = '" + Project + "'"
                );

            //Log CS
            DataTable rsBefCS = Db.Rs("SELECT "
                + " NoCustomer AS [No. Customer]"
                + ",Nama AS [Nama Lengkap]"
                + ",NoTelp AS [No. Telepon]"
                + ",NoHp AS [No. HP]"
                + ",Email AS [Email]"
                + ",TglLahir AS [Tgl Lahir]"
                + ",NoKTP AS [No. KTP]"
                + ",NPWP AS [No. NPWP]"
                + ",KTP1 AS [KTP Alamat]"
                + ",KTP2 AS [KTP RT/RW]"
                + ",KTP3 AS [KTP Kecamatan]"
                + ",KTP4 AS [KTP Kotamadya]"
                + ",Alamat1 AS [Korespoden Alamat]"
                + ",Alamat2 AS [Korespoden RT/RW]"
                + ",Alamat3 AS [Korespoden Kecamatan]"
                + ",Alamat4 AS [Korespoden Kotamadya]"
                + ",RekBank AS [Bank Refund]"
                + ",RekCabang AS [Cabang Bank Refund]"
                + ",RekNo AS [No. Rek Refund]"
                + ",RekNama AS [Atas Nama Rek Refund]"
                + " FROM MS_CUSTOMER"
                + " WHERE NoCustomer = " + noCS
                );

            Db.Execute("UPDATE MS_Customer SET "
                + "Nama = '" + Nama + "'"
                + ",NoTelp = '" + NoTelp + "'"
                + ",NoHp = '" + NoHP + "'"
                + ",Email = '" + Mail + "'"
                + ",NoKTP = '" + NoKTP + "'"
                + ",NPWP = '" + NPWP + "'"
                + ",KTP1 = '" + KTP1 + "'"
                + ",KTP2 = '" + KTP2 + "'"
                + ",KTP3 = '" + KTP3 + "'"
                + ",KTP4 = '" + KTP4 + "'"
                + ",Alamat1 = '" + Kor1 + "'"
                + ",Alamat2 = '" + Kor2 + "'"
                + ",Alamat3 = '" + Kor3 + "'"
                + ",Alamat4 = '" + Kor4 + "'"
                + ",RekBank = '" + rekB + "'"
                + ",RekCabang = '" + rekC + "'"
                + ",RekNo = '" + rekN + "'"
                + ",RekNama = '" + rekNam + "'"
                + " WHERE NoCustomer = " + noCS
                );

            if (tgllahir.Text != "")
            {
                Db.Execute("UPDATE MS_Customer SET TglLahir = '" + Convert.ToDateTime(TglLahir) + "' where NoCustomer = " + noCS
                );
            }

            //Log CS
            DataTable rsAftCS = Db.Rs("SELECT "
                + " NoCustomer AS [No. Customer]"
                + ",Nama AS [Nama Lengkap]"
                + ",NoTelp AS [No. Telepon]"
                + ",NoHp AS [No. HP]"
                + ",Email AS [Email]"
                + ",TglLahir AS [Tgl Lahir]"
                + ",NoKTP AS [No. KTP]"
                + ",NPWP AS [No. NPWP]"
                + ",KTP1 AS [KTP Alamat]"
                + ",KTP2 AS [KTP RT/RW]"
                + ",KTP3 AS [KTP Kecamatan]"
                + ",KTP4 AS [KTP Kotamadya]"
                + ",Alamat1 AS [Korespoden Alamat]"
                + ",Alamat2 AS [Korespoden RT/RW]"
                + ",Alamat3 AS [Korespoden Kecamatan]"
                + ",Alamat4 AS [Korespoden Kotamadya]"
                + ",RekBank AS [Bank Refund]"
                + ",RekCabang AS [Cabang Bank Refund]"
                + ",RekNo AS [No. Rek Refund]"
                + ",RekNama AS [Atas Nama Rek Refund]"
                + " FROM MS_CUSTOMER"
                + " WHERE NoCustomer = " + noCS
                );

            //Logfile Customer
            string KetCS = "Nama Lengkap : " + Nama + "<br>"
                + Cf.LogCompare(rsBefCS, rsAftCS);

            Db.Execute("EXEC spLogCustomer"
                + " 'EDIT'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetCS + "'"
                + ",'" + noCS.ToString().PadLeft(5, '0') + "'"
                );

            //UPDATE MS_NUP
            Db.Execute("UPDATE MS_NUP"
                + " SET NoAgent = " + agent.SelectedValue
                + ", UserInputID = '" + Act.UserID + "'"
                + ", UserInputNama = '" + Db.SingleString("SELECT Nama FROM ISC064_SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'") + "'"
                + " WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'"
                );

            DataTable rsAftNUP = Db.Rs("SELECT "
                + " MS_NUP.NoNUP AS [NUP]"
                + ",MS_AGENT.Nama AS [Nama Sales]"
                + ",MS_NUP.Tipe AS [Tipe yang diminati]"
                + ",MS_CUSTOMER.Nama AS [Nama Customer]"
                + ",MS_CUSTOMER.NoTelp AS [No. Telepon]"
                + ",MS_CUSTOMER.NoHp AS [No. HP]"
                + ",MS_CUSTOMER.Email AS [Email]"
                + ",MS_CUSTOMER.TglLahir AS [Tgl Lahir]"
                + ",MS_CUSTOMER.NoKTP AS [No. KTP]"
                + ",MS_CUSTOMER.NPWP AS [No. NPWP]"
                + ",MS_CUSTOMER.KTP1 AS [KTP Alamat]"
                + ",MS_CUSTOMER.KTP2 AS [KTP RT/RW]"
                + ",MS_CUSTOMER.KTP3 AS [KTP Kecamatan]"
                + ",MS_CUSTOMER.KTP4 AS [KTP Kotamadya]"
                + ",MS_CUSTOMER.Alamat1 AS [Korespoden Alamat]"
                + ",MS_CUSTOMER.Alamat2 AS [Korespoden RT/RW]"
                + ",MS_CUSTOMER.Alamat3 AS [Korespoden Kecamatan]"
                + ",MS_CUSTOMER.Alamat4 AS [Korespoden Kotamadya]"
                + ",MS_CUSTOMER.RekBank AS [Bank Refund]"
                + ",MS_CUSTOMER.RekCabang AS [Cabang Bank Refund]"
                + ",MS_CUSTOMER.RekNo AS [No. Rek Refund]"
                + ",MS_CUSTOMER.RekNama AS [Atas Nama Rek Refund]"
                //+ ",MS_NUP.UserInputNama AS [Daftar oleh]"
                + " FROM MS_NUP INNER JOIN MS_AGENT ON MS_NUP.NoAgent = MS_AGENT.NoAgent"
                + " INNER JOIN MS_CUSTOMER ON MS_NUP.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_NUP.NoNUP='" + NoNUP + "' AND MS_NUP.Tipe = '" + Tipe + "' AND MS_NUP.Project = '" + Project + "'"
                );

            //Logfile NUP
            string KetNUP = "Edit data NUP : " + NoNUP + "<br>"
                + Cf.LogCompare(rsBefNUP, rsAftNUP);

            Db.Execute("EXEC spLogNUP"
                + " 'EDIT'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetNUP + "'"
                + ",'" + NoNUP + "'"
                + ",'" + Project + "'"
                + ",'" + Tipe + "'"
                );
        }

        protected void sama_CheckedChanged(object sender, EventArgs e)
        {
            Korespon1.Text = ktp1.Text;
            Korespon2.Text = ktp2.Text;
            Korespon3.Text = ktp3.Text;
            Korespon4.Text = ktp4.Text;
        }

        protected void beda_CheckedChanged(object sender, EventArgs e)
        {
            Korespon1.Text = "";
            Korespon2.Text = "";
            Korespon3.Text = "";
            Korespon4.Text = "";
        }

        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Jenis"]);
            }
        }
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["Project"]);
            }
        }

        protected void otorisasi_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                if (username.Text == "")
                {
                    usernamec.Text = "Kosong";
                    lblotorisasi.Text = "";
                }
                else
                    usernamec.Text = "";

                if (password.Text == "")
                {
                    passc.Text = "Kosong";
                    lblotorisasi.Text = "";
                }
                else
                    passc.Text = "";

                if (username.Text != "" && password.Text != "")
                {
                    string Username = username.Text;
                    string Password = Act.Hash(password.Text);

                    int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM " + Mi.DbPrefix + "SECURITY..USERNAME "
                    + " WHERE UserID = '" + Username + "'"
                    + " AND Pass = '" + Password + "'"
                    + " AND (SecLevel='SUP' OR SecLevel='DIR')"
                    + " AND Status = 'A'"
                    );

                    if (c != 0)
                    {
                        //Logfile
                        string KetNUP = "Edit Nama : " + NoNUP + "<br>"
                            + "<br>Otorisasi Oleh : " + Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID='" + Username + "'")
                            + "<br>Username : " + Cf.Str(Username);

                        Db.Execute("EXEC spLogNUP"
                            + " 'OTRS'"
                            + ",'" + Cf.Str(Username) + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + KetNUP + "'"
                            + ",'" + NoNUP + "'"
                            + ",'" + Project + "'"
                            + ",'" + Tipe + "'"
                            );

                        saveEdit();

                        Response.Redirect("NUPEdit.aspx?done=1&NoNUP=" + NoNUP + "&Jenis=" + Tipe);
                    }
                    else
                        lblotorisasi.Text = "Otorisasi Gagal. Username tidak valid.";
                }
                else
                    lblotorisasi.Text = "Otorisasi Gagal";
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
