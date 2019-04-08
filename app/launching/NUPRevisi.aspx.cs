using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class NUPRevisi : System.Web.UI.Page
    {
        //NOTES : Approval hanya boleh bu Kartika (SecLevel : DIR) sebenarnya
        //Dibuat cadangan, SUP diperbolehkan.

        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            FeedBack();
            if (!Page.IsPostBack)
            {
                bindTipe();
                InitForm();
                Fill();
            }
        }

        private void Fill()
        {
            nomorNUP.Text = NoNUP;
            nomorNUP.ReadOnly = true;

            DataTable dtNUP = Db.Rs("SELECT * FROM MS_NUP WHERE NoNUP = '" + NoNUP + "'");

            if (dtNUP.Rows.Count > 0)
            {
                DataTable dtCS = Db.Rs("SELECT * FROM MS_CUSTOMER WHERE NoCustomer = " + Convert.ToInt32(dtNUP.Rows[0]["NoCustomer"]));
                DataTable dtAG = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent = " + Convert.ToInt32(dtNUP.Rows[0]["NoAgent"]));

                //nama.Text = dtCS.Rows[0]["Nama"].ToString();
                //ctelp.Text = dtCS.Rows[0]["NoTelp"].ToString();
                //chp.Text = dtCS.Rows[0]["NoHP"].ToString();
                //noktp.Text = dtCS.Rows[0]["NoKTP"].ToString();
                //agent.SelectedValue = dtNUP.Rows[0]["NoAgent"].ToString();
                //tipe.SelectedValue = dtNUP.Rows[0]["Tipe"].ToString();

                ////Alamat KTP
                //ktp1.Text = dtCS.Rows[0]["KTP1"].ToString();
                //ktp2.Text = dtCS.Rows[0]["KTP2"].ToString();
                //ktp3.Text = dtCS.Rows[0]["KTP3"].ToString();
                //ktp4.Text = dtCS.Rows[0]["KTP4"].ToString();

                ////Alamat Korespondensi
                //Korespon1.Text = dtCS.Rows[0]["AlamatKorespon1"].ToString();
                //Korespon2.Text = dtCS.Rows[0]["AlamatKorespon2"].ToString();
                //Korespon3.Text = dtCS.Rows[0]["AlamatKorespon3"].ToString();
                //Korespon4.Text = dtCS.Rows[0]["AlamatKorespon4"].ToString();

                ////Rekening Refund
                //bank.Text = dtCS.Rows[0]["RekBank"].ToString();
                //cabang.Text = dtCS.Rows[0]["RekCabang"].ToString();
                //rek.Text = dtCS.Rows[0]["RekNo"].ToString();
                //reknama.Text = dtCS.Rows[0]["RekNama"].ToString();

                tipe.SelectedValue = dtNUP.Rows[0]["Tipe"].ToString();

                if (dtCS.Rows[0]["NoKTP"].ToString() == ""
                    || dtCS.Rows[0]["RekBank"].ToString() == ""
                    || dtCS.Rows[0]["RekCabang"].ToString() == ""
                    || dtCS.Rows[0]["RekNo"].ToString() == ""
                    || dtCS.Rows[0]["RekNama"].ToString() == ""
                    || dtNUP.Rows[0]["NoAgent"].ToString() == "0"
                    )
                {
                    otorisasi.Enabled = false;
                    trWarn.Visible = true;
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
                        + "Registrasi Berhasil..."
                        + "</a>";
            }
        }

        private void bindTipe()
        {
            string Tipe1 = Db.SingleString("SELECT Tipe FROM MS_NUP WHERE NoNUP='" + NoNUP + "'");
            //Populate data agent
            DataTable rs = Db.Rs("SELECT Tipe FROM AM132_MARKETINGJUAL..REF_TIPE ORDER BY Tipe DESC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Tipe"].ToString();
                string t = rs.Rows[i]["Tipe"].ToString();

                tipe.Items.Add(new ListItem(t, v));
            }
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

            //agent
            if (agent.SelectedValue == "0")
            {
                x = false;
                if (s == "") s = agent.ID;
                agentc.Text = "Pilih Agent";
            }
            else
                agentc.Text = "";

            return x;
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
        }

        private string AutoID()
        {
            string strLastNo = Db.SingleString("SELECT TOP 1 NoNUP FROM MS_NUP ORDER BY NoNUP DESC");

            int intLastNo = Convert.ToInt32(strLastNo);

            int noBaru = intLastNo + 1;
            string NUP = noBaru.ToString().PadLeft(4, '0');

            return NUP;
        }

        protected void saveRevisi()
        {
            if (valid())
            {
                string Nama = Cf.Str(nama.Text);
                string NoHP = Cf.Str(chp.Text);
                string NoTelp = Cf.Str(ctelp.Text);
                string NoKTP = Cf.Str(noktp.Text);
                string KTP1 = Cf.Str(ktp1.Text);
                string KTP2 = Cf.Str(ktp2.Text);
                string KTP3 = Cf.Str(ktp3.Text);
                string KTP4 = Cf.Str(ktp4.Text);
                string Kor1 = Cf.Str(Korespon1.Text);
                string Kor2 = Cf.Str(Korespon2.Text);
                string Kor3 = Cf.Str(Korespon3.Text);
                string Kor4 = Cf.Str(Korespon4.Text);
                string Tipe = Cf.Str(tipe.SelectedValue);
                string rekN = Cf.Str(rek.Text);
                string rekNam = Cf.Str(reknama.Text);
                string rekB = Cf.Str(bank.Text);
                string rekC = Cf.Str(cabang.Text);
                int NoAgent = Convert.ToInt16(agent.SelectedValue);

                //Data Customer dari NUP
                DataTable dtNUP = Db.Rs("SELECT * FROM MS_NUP WHERE NoNUP = " + NoNUP);
                string nupCNama = "";
                string nupCTelp = "";
                string nupCHP = "";
                string nupCKTP = "";
                string nupKTP1 = "";
                string nupKTP2 = "";
                string nupKTP3 = "";
                string nupKTP4 = "";
                string nupKorespon1 = "";
                string nupKorespon2 = "";
                string nupKorespon3 = "";
                string nupKorespon4 = "";
                string nupTipe = "";
                string nupBank = "";
                string nupCabang = "";
                string nupRekNo = "";
                string nupRekNama = "";
                int nupAgent = 0;

                if (dtNUP.Rows.Count > 0)
                {
                    DataTable dtCS = Db.Rs("SELECT * FROM MS_CUSTOMER WHERE NoCustomer = " + Convert.ToInt32(dtNUP.Rows[0]["NoCustomer"]));
                    DataTable dtAG = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent = " + Convert.ToInt32(dtNUP.Rows[0]["NoAgent"]));

                    nupCNama = dtCS.Rows[0]["Nama"].ToString();
                    nupCTelp = dtCS.Rows[0]["NoTelp"].ToString();
                    nupCHP = dtCS.Rows[0]["NoHP"].ToString();
                    nupCKTP = dtCS.Rows[0]["NoKTP"].ToString();
                    nupCKTP = dtCS.Rows[0]["NoKTP"].ToString();
                    nupTipe = dtNUP.Rows[0]["Tipe"].ToString();
                    nupAgent = Convert.ToInt32(dtNUP.Rows[0]["NoAgent"].ToString());

                    //Alamat KTP
                    nupKTP1 = dtCS.Rows[0]["KTP1"].ToString();
                    nupKTP2 = dtCS.Rows[0]["KTP2"].ToString();
                    nupKTP3 = dtCS.Rows[0]["KTP3"].ToString();
                    nupKTP4 = dtCS.Rows[0]["KTP4"].ToString();

                    //Alamat Korespondensi
                    nupKorespon1 = dtCS.Rows[0]["AlamatKorespon1"].ToString();
                    nupKorespon2 = dtCS.Rows[0]["AlamatKorespon2"].ToString();
                    nupKorespon3 = dtCS.Rows[0]["AlamatKorespon3"].ToString();
                    nupKorespon4 = dtCS.Rows[0]["AlamatKorespon4"].ToString();

                    //Rekening Refund
                    nupBank = dtCS.Rows[0]["RekBank"].ToString();
                    nupCabang = dtCS.Rows[0]["RekCabang"].ToString();
                    nupRekNo = dtCS.Rows[0]["RekNo"].ToString();
                    nupRekNama = dtCS.Rows[0]["RekNama"].ToString();
                }

                //Jika data inputan ada yang berubah dari NUP (Data yang divalidasi saja), akan membuat Customer baru dan update MS_NUPnya
                int noCust = 0;
                //if (Nama != nupCNama
                //    || NoHP != nupCHP
                //    || NoTelp != nupCTelp
                //    || NoKTP != nupCKTP
                //    || KTP1 != nupKTP1
                //    || KTP2 != nupKTP2
                //    || KTP3 != nupKTP3
                //    || KTP4 != nupKTP4
                //    || Kor1 != nupKorespon1
                //    || Kor2 != nupKorespon2
                //    || Kor3 != nupKorespon3
                //    || Kor4 != nupKorespon4
                //    || rekN != nupRekNo
                //    || rekNam != nupRekNama
                //    || rekB != nupBank
                //    || rekC != nupCabang
                //    || NoAgent != nupAgent
                //    || Tipe != nupTipe
                //    )
                //{
                //Untuk Log
                DataTable rsBef = Db.Rs("SELECT "
                + " B.NoCustomer AS [No. Customer]"
                + ",B.Nama AS [Nama Lengkap]"
                + ",B.NoHP AS [No. HP]"
                + ",B.NoTelp AS [No. Telp]"
                + ",B.NoKTP AS [No. KTP]"
                + ",B.KTP1+' '+B.KTP2+' '+B.KTP3+' '+B.KTP4 AS [Alamat KTP]"
                + ",B.AlamatKorespon1+' '+B.AlamatKorespon2+' '+B.AlamatKorespon3+' '+B.AlamatKorespon4 AS [Alamat Korespondensi]"
                + ",B.RekBank+' '+B.RekCabang AS [Bank]"
                + ",B.RekNo AS [No. Rek]"
                + ",B.RekNama AS [Atas Nama]"
                + ",A.Tipe AS [Tipe yang diminati]"
                + ",A.Revisi AS [Revisi NUP]"
                + " FROM MS_NUP A"
                + " INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer"
                + " WHERE A.NoCustomer = " + Convert.ToInt32(dtNUP.Rows[0]["NoCustomer"])
                );

                //Exec new data customer
                Db.Execute("EXEC spCustomerDaftarBSA"
                + " '" + Nama + "'"
                + ",'" + NoHP + "'"
                + ",'" + NoTelp + "'"
                + ",'" + NoKTP + "'"
                + ",'" + KTP1 + "'"
                + ",'" + KTP2 + "'"
                + ",'" + KTP3 + "'"
                + ",'" + KTP4 + "'"
                + ",'" + Kor1 + "'"
                + ",'" + Kor2 + "'"
                + ",'" + Kor3 + "'"
                + ",'" + Kor4 + "'"
                + ",'" + rekNam + "'"
                + ",'" + rekB + "'"
                + ",'" + rekC + "'"
                + ",'" + rekN + "'"
                );

                //get nomor customer terbaru
                noCust = Db.SingleInteger("SELECT TOP 1 NoCustomer FROM MS_CUSTOMER ORDER BY NoCustomer DESC");

                //UPDATE MS_NUP
                Db.Execute("UPDATE MS_NUP"
                    + " SET NoAgent = " + agent.SelectedValue
                    + ", NoCustomer = " + noCust
                    + ", Tipe = '" + tipe.SelectedValue + "'"
                    + ", Revisi = Revisi + 1"
                    + ", UserInputNama = '" + Db.SingleString("SELECT Nama FROM AM132_SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'") + "'"
                    + " WHERE NoNUP='" + NoNUP + "'"
                    );

                //Untuk Log
                DataTable rsAft = Db.Rs("SELECT "
                + " B.NoCustomer AS [No. Customer]"
                + ",B.Nama AS [Nama Lengkap]"
                + ",B.NoHP AS [No. HP]"
                + ",B.NoTelp AS [No. Telp]"
                + ",B.NoKTP AS [No. KTP]"
                + ",B.KTP1+' '+B.KTP2+' '+B.KTP3+' '+B.KTP4 AS [Alamat KTP]"
                + ",B.AlamatKorespon1+' '+B.AlamatKorespon2+' '+B.AlamatKorespon3+' '+B.AlamatKorespon4 AS [Alamat Korespondensi]"
                + ",B.RekBank+' '+B.RekCabang AS [Bank]"
                + ",B.RekNo AS [No. Rek]"
                + ",B.RekNama AS [Atas Nama]"
                + ",A.Tipe AS [Tipe yang diminati]"
                + ",A.Revisi AS [Revisi NUP]"
                + ",A.UserInputNama AS [Direvisi Oleh]"
                + " FROM MS_NUP A"
                + " INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer"
                + " WHERE A.NoCustomer = " + noCust
                );

                //Logfile
                string Ket = "No. NUP : " + NoNUP + "<br>"
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogNUP"
                + " 'REVISI'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + NoNUP + "'"
                );

                Response.Redirect("NUPRevisiFin.aspx?No=" + NoNUP);
                //}
            }
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                int Count = Db.SingleInteger(" SELECT ISNULL(Revisi,0) FROM MS_NUP WHERE NoNUP = '" + NoNUP + "'");
                if (Count >= 1)
                {
                    trotorisasi.Visible = true;
                    trsave.Visible = false;
                    lblotorisasi.Text = "Masukkan username pemberi otorisasi";
                }
                else
                {
                    saveRevisi();
                }
            }
        }
        private string NoCustomer
        {
            get
            {
                return Cf.Pk(nocustomer.Text);
            }
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
                return Cf.Pk(Request.QueryString["No"]);
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
                        string KetNUP = "REVISI NUP : NoNUP<br>"
                            + "<br>Otorisasi Oleh : " + Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID='" + Username + "'")
                            + "<br>Username : " + Cf.Str(Username);

                        Db.Execute("EXEC spLogNUP"
                            + " 'OTORISASI'"
                            + ",'" + Cf.Str(Username) + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + KetNUP + "'"
                            + ",'" + NoNUP + "'"
                            );

                        saveRevisi();
                    }
                    else
                        lblotorisasi.Text = "Otorisasi Gagal. Username tidak valid.";

                }
                else
                    lblotorisasi.Text = "Masukkan Username dan Password.";
            }
        }
    }
}
