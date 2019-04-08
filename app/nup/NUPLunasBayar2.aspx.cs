using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class NUPLunasBayar2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FeedBack();
            if (!Page.IsPostBack)
            {
                fill();
                fillNilaiBayar();
                fillAcc();
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
                        + "Pelunasan NUP Berhasil..."
                        + "</a>";
            }
        }
        private bool valid()
        {
            string s = "";
            bool x = true;

            //carabayar
            if (carabayar.SelectedValue == "")
            {
                x = false;
                if (s == "") s = carabayar.ID;
                carabayarc.Text = "Pilih cara bayar";
            }
            else
                carabayarc.Text = "";


            //Jika Transfer tp ga pilih rek bank
            if (ddlAcc.SelectedIndex == 0) // && carabayar.SelectedValue == "TR")
            {
                x = false;
                if (s == "") s = ddlAccErr.ID;
                ddlAccErr.Text = "Pilih rekening bank";
            }
            else
                ddlAccErr.Text = "";

            string NoStock = Db.SingleString("SELECT NoStock FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_NUP_PRIORITY WHERE NoNUP = '" + NoNUP + "'");
            int unit = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP_PRIORITY WHERE NoStock = '" + NoStock + "' AND Tipe = '" + Tipe + "' AND NoNUP != '" + NoNUP + "'");
            if (unit > 0)
            {
                x = false;
            }

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP_PELUNASAN WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Tipe + "' AND PelunasanKe=2");
            if (c != 0)
            {
                x = false;

                if (!x)
                    Js.Alert(
                        this
                        , "Input Tidak Valid.\\n\\n"
                        + "Aturan Proses :\\n"
                        + "1. NUP sudah melakukan pelunasan.\\n"
                        + "2. Unit sudah dipilih oleh customer lain.\\n"
                        , "document.getElementById('" + s + "').focus();"
                        + "document.getElementById('" + s + "').select();"
                        );
            }


            return x;
        }

        private void fillAcc()
        {
            DataTable rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_ACC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                ddlAcc.Items.Add(new ListItem(t, v));
            }
        }

        private void fillNilaiBayar()
        {
            string nounitNUP = Db.SingleString("select NoStock from MS_NUP_PRIORITY where NoNUP = '" + NoNUP + "' AND TIpe = '" + Tipe + "' AND Project = '" + Project + "'");
            decimal luasunit = Db.SingleDecimal("select LuasSG from MS_unit where NoStock = '" + nounitNUP + "'");
            string JenisProperti = "";

            JenisProperti = Db.SingleString("SELECT Tipe FROM MS_NUP WHERE NoNUP='" + NoNUP + "' AND TIpe = '" + Tipe + "' AND Project = '" + Project + "'");

            //ambil nilai pelunasan ke 2
            decimal pelunasan1 = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar), 0) FROM MS_NUP_PELUNASAN WHERE NoNUP = '" + NoNUP + "' AND Project ='" + Project + "' AND Tipe = '" + Tipe + "' and PelunasanKe = 2");

            decimal pelunasan = Db.SingleDecimal("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'LunasBayarNUP" + Project + Tipe + "'");

            decimal sisa = pelunasan - pelunasan1;
            string t1 = Cf.Num(sisa.ToString());            
                //if (Tipe == "RUSUNAMI")
                //{
                //    v1 = "1000000";
                //    t1 = "1,000,000";
                //}
                //else
                //{
                //    v1 = "5000000";
                //    t1 = "5,000,000";
                //}
                valueNUP.Items.Add(new ListItem(t1, sisa.ToString()));
        }

        private void fill()
        {
            DataTable rsNUP = Db.Rs("SELECT * FROM MS_NUP WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");
            if (rsNUP.Rows.Count != 0)
            {
                DataTable rsCust = Db.Rs("SELECT * FROM MS_CUSTOMER WHERE NoCustomer=" + Convert.ToInt32(rsNUP.Rows[0]["NoCustomer"]));
                if (rsCust.Rows.Count != 0)
                {
                    string kontak = rsCust.Rows[0]["NoTelp"].ToString();
                    if (kontak != "" && rsCust.Rows[0]["NoHP"].ToString() != "")
                        kontak = kontak + " / " + rsCust.Rows[0]["NoHP"].ToString();
                    else
                        kontak = rsCust.Rows[0]["NoHP"].ToString();

                    string rekening = rsCust.Rows[0]["RekNo"].ToString() + "<br/>";
                    if (rsCust.Rows[0]["RekBank"].ToString() != "") rekening += rsCust.Rows[0]["RekBank"].ToString() + "<br />";
                    if (rsCust.Rows[0]["RekCabang"].ToString() != "") rekening += rsCust.Rows[0]["RekCabang"].ToString() + "<br />";
                    if (rsCust.Rows[0]["RekNama"].ToString() != "") rekening += rsCust.Rows[0]["RekNama"].ToString();

                    namacs.Text = rsCust.Rows[0]["Nama"].ToString();
                    ktpcs.Text = rsCust.Rows[0]["NoKTP"].ToString();
                    nokontak.Text = kontak;
                    refund.Text = rekening;
                }

                nonup.Text = rsNUP.Rows[0]["NoNUP"].ToString();


                string agent = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent=" + Convert.ToInt32(rsNUP.Rows[0]["NoAgent"]));
                namaag.Text = agent;

                decimal bayaran = Db.SingleDecimal("SELECT ISNULL(NilaiBayar,0) FROM MS_NUP_PELUNASAN WHERE NoNUP='" + rsNUP.Rows[0]["NoNUP"].ToString() + "' AND Tipe = '" + Tipe + "'AND PelunasanKe=1  AND Project = '" + Project + "'");
                bayar1.Text = "Rp. " + Cf.Num(bayaran);


                int PilihUnit = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_NUP_PRIORITY WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Tipe + "' AND NoStock!=''  AND Project = '" + Project + "'");
                if (PilihUnit == 0)
                {
                    save.Enabled = false;
                    note.Text = "Silahkan Pilih Unit Terlebih Dahulu";
                }
            }

            tbTglTerima.Text = Cf.Day(DateTime.Now);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                string cabar = Cf.Str(carabayar.SelectedValue);
                string rekpers = Cf.Str(ddlAcc.SelectedValue);
                string ketbayar = Cf.Str(ket.Text);

                decimal valuenup = 0;

                valuenup = Convert.ToDecimal(valueNUP.SelectedValue);

                string NoTTS = "";
                NoTTS = SaveTTS(NoNUP, Db.SingleInteger("SELECT NoCustomer FROM MS_NUP WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Tipe + "'  AND Project = '" + Project + "'"));
                
                //int roman = Convert.ToInt32(Cf.Bulan(DateTime.Now));                
                string NoTTS2 = Db.SingleString("SELECT NoTTSNUP FROM MS_NUP_PELUNASAN WHERE NoNUP=" + NoTTS);

                Db.Execute("EXEC spInsertNUPPelunasan"
                + " '" + NoNUP + "'"
                + ",'" + Cf.Tgl112(DateTime.Now) + "'"
                + "," + valuenup
                + ",'" + cabar + "'"
                + ",'" + ketbayar + "'"
                + ",'" + NoTTS.ToString() + "'"
                + "," + 2
                + ",'" + ddlAcc.SelectedValue + "'"
                + ",'" + Tipe + "'"
                + ",'" + Project + "'"
                ); //Yang belakang diset 2, karena ini adalah pembayaran kedua NUP

                Db.Execute("UPDATE MS_NUP_PELUNASAN SET FlagUntukBayar=1 WHERE NoNUP='" + NoNUP + "'  AND Tipe ='" + Tipe + "' AND PelunasanKe=1  AND Project = '" + Project + "'");
                Db.Execute("UPDATE MS_NUP_PELUNASAN SET FlagUntukBayar=1, NoTTSNUP='" + NoTTS2 + "' WHERE NoNUP='" + NoNUP + "' AND Tipe ='" + Tipe + "' AND PelunasanKe=2 AND Project = '" + Project + "'");
                Db.Execute("UPDATE MS_NUP SET Status = '4' WHERE NoNUP='" + NoNUP + "' AND Tipe ='" + Tipe + "' AND Project = '" + Project + "'");

                Db.Execute("UPDATE MS_NUP_PELUNASAN SET "
                    + " UserInputID = '" + Act.UserID + "'"
                    + ", UserInputNama = '" + Db.SingleString("SELECT Nama FROM ISC064_SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'") + "'"
                    + " WHERE NoNUP='" + NoNUP + "' AND Tipe ='" + Tipe + "' AND Project = '" + Project + "'");

                //total di ms_nup
                //decimal lunasawal = Db.SingleDecimal("select ISNULL(SUM(NilaiBayar),0) from MS_NUP_PELUNASAN where NoNUP = '" + NoNUP + "' AND Tipe ='" + Tipe + "'");
                decimal pembayaran1 = valuenup;//Db.SingleDecimal("select ISNULL(SUM(NilaiBayar),0) from MS_NUP_PELUNASAN where NoNUP = '" + NoNUP + "' AND Tipe ='" + Tipe + "'");
                Db.Execute("UPDATE MS_NUP SET NilaiBayar = '" + pembayaran1 + "', Keterangan = 'LUNAS' WHERE NoNUP='" + NoNUP + "' AND Tipe ='" + Tipe + "' AND Project = '" + Project + "'");

                DataTable rsLog = new DataTable();
                //Logfile
                rsLog = Db.Rs("SELECT "
                    + " A.NoNUP AS [NUP]"
                    + ",C.Nama AS [Customer]"
                    + ",B.CaraBayar AS [Cara Bayar]"
                    + ",E.Bank+'/'+E.Rekening+'/'+E.AtasNama AS [Transfer ke]"
                    + ",B.NilaiBayar AS [Nilai Bayar]"
                    + ",B.Keterangan AS [Keterangan]"
                    + ",B.UserInputNama AS [Diinput Oleh]"
                    + " FROM MS_NUP A "
                    + " INNER JOIN MS_NUP_PELUNASAN B ON A.NoNUP = B.NoNUP"
                    + " INNER JOIN MS_CUSTOMER C ON A.NoCustomer = C.NoCustomer"
                    + " INNER JOIN " + Mi.DbPrefix + "FINANCEAR..REF_ACC E ON B.RekBank = E.Acc"
                    + " WHERE A.NoNUP = '" + NoNUP + "' AND A.Tipe ='" + Tipe + "' AND A.Project = '" + Project + "'");

                string KetLog = Cf.LogCapture(rsLog)
                + "<br>***PEMBAYARAN NUP KEDUA:<br>";

                Db.Execute("EXEC spLogNUP"
                + " 'PL-NUP'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetLog + "'"
                + ",'" + NoNUP + "'"
                );

                //balikin ke halaman daftar
                Response.Redirect("NUPLunasBayarFin.aspx?No=" + NoNUP + "&Tipe=" + Tipe + "&project=" + Project);

            }
        }

        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["No"]);
            }
        }
        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }

        private string SaveTTS(string NoNUP, int NoCustomer)
        {
            DateTime TglTTS = Convert.ToDateTime(tbTglTerima.Text);
            string Customer = Cf.Str(Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer));
            string CaraBayar = carabayar.SelectedValue;

            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSRegistrasi"
                + " '" + TglTTS + "'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'JUAL'"
                + ",''" //Ref / NoKontrak
                + ",''" //Unit
                + ",'" + Customer + "'"
                + ",'" + CaraBayar + "'"
                + ",'" + Cf.Str(ket.Text) + "'"
                );
            
            int NoTTS = Db.SingleInteger("SELECT TOP 1 NoTTS FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS ORDER BY NoTTS DESC");
            
            string NoTTSBaru = Numerator.TTS(TglTTS.Month, TglTTS.Year, Project);

            string noKK = "";

            if (nokk1.Text != "" || nokk2.Text != "" || nokk3.Text != "" || nokk4.Text != "")
                noKK = nokk1.Text + '-' + nokk2.Text + '-' + nokk3.Text + '-' + nokk4.Text;

            string NoStock = Db.SingleString("SELECT NoStock FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_NUP_PRIORITY WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");
            string NoUnit = Db.SingleString("SELECT NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoStock = '" + NoStock + "'");
            //update nilai bayar di TTSnya
            decimal nBayar = Convert.ToDecimal(valueNUP.Text);
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET Total=" + nBayar
                + ", Acc = '" + ddlAcc.SelectedValue + "'"
                + ", NoNUP = '" + NoNUP + "'"
                + ", NoTTS2 = '" + NoTTSBaru + "'"
                + ", Jenis ='" + Tipe + "'"
                + ", Unit ='" + NoUnit + "'"
                + ", Project ='" + Project + "'"
                + ", Catatan = '" + Cf.Str(ket.Text) + "'"
                + " WHERE NoTTS='" + NoTTS + "'");

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
                + "<br>***PEMBAYARAN NUP KEDUA:<br>";

            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogTTS"
                + " 'REGIS'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetLog + "'"
                + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            return NoTTS.ToString();
        }

    }
}
