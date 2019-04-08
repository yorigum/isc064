using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class NUPDaftarBayar2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FeedBack();
            if (!Page.IsPostBack)
            {
                //Nilai Bayar NUP
                decimal nilai = Db.SingleDecimal("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'FormatDaftarBayarNUP" + Project + "'");
                nil.Items.Add(new ListItem(Cf.Num(nilai), Convert.ToString(nilai)));
                //nil.SelectedValue = "1000000"; //1jt

                fill();
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

            //tgl TTS
            if (tbTglTerima.Text == "")
            {
                x = false;
                if (s == "") s = tbTglTerima.ID;
                tbTglTerimac.Text = "Kosong";
            }
            else
                tbTglTerimac.Text = "";

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
            if (ddlAcc.SelectedIndex == 0) //&& carabayar.SelectedValue == "TR")
            {
                x = false;
                if (s == "") s = ddlAccErr.ID;
                ddlAccErr.Text = "Pilih";
            }
            else
                ddlAccErr.Text = "";

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP_PELUNASAN WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "' AND PelunasanKe=1");
            if (c != 0)
            {
                x = false;

                if (!x)
                    Js.Alert(
                        this
                        , "Input Tidak Valid.\\n\\n"
                        + "Aturan Proses :\\n"
                        + "1. NUP sudah melakukan pembayaran.\\n"
                        , "document.getElementById('" + s + "').focus();"
                        + "document.getElementById('" + s + "').select();"
                        );
            }

            return x;
        }

        private void fillAcc()
        {
            DataTable rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_ACC WHERE Project = '" + Project + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                ddlAcc.Items.Add(new ListItem(t, v));
            }
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
                tipe.Text = rsNUP.Rows[0]["Tipe"].ToString();

                string agent = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent=" + Convert.ToInt32(rsNUP.Rows[0]["NoAgent"]));
                namaag.Text = agent;

                if (rsNUP.Rows[0]["CaraBayar"].ToString() != "")
                    save.Enabled = false;

                tbTglTerima.Text = Cf.Day(DateTime.Now);
            }
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                string cabar = Cf.Str(carabayar.SelectedValue);
                string rekpers = Cf.Str(ddlAcc.SelectedValue);
                string ketbayar = Cf.Str(ket.Text);

                decimal totalnup = (decimal)10000000; //10jt

                decimal valuenup = 0;

                valuenup = Convert.ToDecimal(nil.SelectedValue);

                string NoTTS = "";
                DataTable rsLog = new DataTable();

                //kode no tts

                //int roman = Convert.ToInt32(Cf.Bulan(DateTime.Now));
                DateTime TglTTS = DateTime.Now;
                string TTS = Numerator.TTS(TglTTS.Month, TglTTS.Year, Project);
                //string TTS = NoTTS.ToString() + "/BTV/TTS/" + Cf.Roman(roman) + "/" + Cf.Year(DateTime.Now);
                NoTTS = SaveTTS(NoNUP, Db.SingleInteger("SELECT NoCustomer FROM MS_NUP WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'"), TTS);

                Db.Execute("EXEC spInsertNUPPelunasan"
                + " '" + NoNUP + "'"
                + ",'" + Cf.Tgl112(DateTime.Now) + "'"
                + "," + valuenup
                + ",'" + cabar + "'"
                + ",'" + ketbayar + "'"
                + ",'" + NoTTS.ToString() + "'"
                + "," + 1
                + ",'" + ddlAcc.SelectedValue + "'"
                + ",'" + Tipe + "'"
                + ",'" + Project + "'"
                ); //Yang belakang diset 1, karena ini adalah pembayaran pertama NUP

                Db.Execute("UPDATE MS_NUP_PELUNASAN SET "
                    + " UserInputID = '" + Act.UserID + "'"
                    + ", UserInputNama = '" + Db.SingleString("SELECT Nama FROM ISC064_SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'") + "'"
                    + ", NoTTSNUP = '" + TTS + "'"
                    + " WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");
                
                Db.Execute("UPDATE MS_NUP SET NilaiBayar = '" + valuenup + "' WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");

                //Logfile
                rsLog = Db.Rs("SELECT "
                    + " A.NoNUP AS [NUP]"
                    + ",B.Tipe AS [Tipe]"
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
                    + " WHERE A.NoNUP = '" + NoNUP + "' AND A.Tipe = '" + Tipe + "' AND A.Project = '" + Project + "'");

                string KetLog = Cf.LogCapture(rsLog)
                    + "<br>***PEMBAYARAN NUP PERTAMA:<br>";

                Db.Execute("EXEC spLogNUP"
                + " 'PY-NUP'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetLog + "'"
                + ",'" + NoNUP + "'"
                + ",'" + Project + "'"
                + ",'" + Tipe + "'"
                );

                //balikin ke halaman daftar
                Response.Redirect("NUPDaftarBayarFin.aspx?No=" + NoNUP + "&Tipe=" + Tipe + "&Project=" + Project);

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
                return Cf.Pk(Request.QueryString["Project"]);
            }
        }

        private string SaveTTS(string NoNUP, int NoCustomer, string NoTTS2)
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

            //update nilai bayar di TTSnya
            decimal nBayar = Convert.ToDecimal(nil.Text);
            Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET Total=" + nBayar
                + ", Acc = '" + ddlAcc.SelectedValue + "'"
                + ", NoNUP = '" + NoNUP + "'"
                + ", Jenis = '" + Tipe + "'"
                + ", NoTTS2 = '" + NoTTSBaru + "'"
                + ", Project = '" + Project + "'"
                //+ ", NoKK = '" + Cf.Str(noKK) + "'"
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

            string KetLog =
                "***PEMBAYARAN NUP PERTAMA: " + NoNUP + " Tipe = " + Tipe + "<br/><br/>"
                + Cf.LogCapture(rs);

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