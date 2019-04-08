using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using MessagingToolkit.QRCode.Codec;
//using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace ISC064.LAUNCHING
{
    public partial class NUPDaftar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //FeedBack();
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack)
            {

                //bindtipe();
                InitForm();
            }


        }

        //private void FeedBack()
        //{
        //    feed.Text = "";
        //    if (!Page.IsPostBack)
        //    {
        //        if (Request.QueryString["done"] != null)
        //            feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
        //                + "<a href=\"javascript:popEditCustomer('" + Request.QueryString["done"] + "')\">"
        //                + "Registrasi Berhasil..."
        //                + "</a>";
        //    }
        //}



        private bool valid()
        {
            string s = "";
            bool x = true;
            if (!Cf.isTgl(tglNUP))
            {
                x = false;
                if (s == "") s = tglNUP.ID;
                tglNUPc.Text = "Tanggal";
            }
            else
                tglNUPc.Text = "*";


            //nama
            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "*";

            ////kontak
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
                ctelpc.Text = "*";

            //noktp
            if (Cf.isEmpty(noktp))
            {
                x = false;
                if (s == "") s = noktp.ID;
                noktpc.Text = "Kosong";
            }
            else
                noktpc.Text = "*";

            //nonpwp
            if (Cf.isEmpty(nonpwp))
            {
                x = false;
                if (s == "") s = nonpwp.ID;
                nonpwpc.Text = "Kosong";
            }
            else
                nonpwpc.Text = "*";

            //email
            if (Cf.isEmpty(email))
            {
                x = false;
                if (s == "") s = email.ID;
                emailc.Text = "Kosong";
            }
            else
                emailc.Text = "*";

            ////bank
            if (Cf.isEmpty(bank))
            {
                x = false;
                if (s == "") s = bank.ID;
                bankc.Text = "Kosong";
            }
            else
                bankc.Text = "*";

            //cabang
            if (Cf.isEmpty(cabang))
            {
                x = false;
                if (s == "") s = cabang.ID;
                cabangc.Text = "Kosong";
            }
            else
                cabangc.Text = "*";

            //no rek
            if (Cf.isEmpty(rek))
            {
                x = false;
                if (s == "") s = rek.ID;
                rekc.Text = "Kosong";
            }
            else
                rekc.Text = "*";

            //nama rek
            if (Cf.isEmpty(reknama))
            {
                x = false;
                if (s == "") s = reknama.ID;
                reknamac.Text = "Kosong";
            }
            else
                reknamac.Text = "*";

            //agent
            if (agent.SelectedValue == "0")
            {
                x = false;
                if (s == "") s = agent.ID;
                agentc.Text = "Pilih Agent";
            }
            else
                agentc.Text = "*";

            //if (tipe.SelectedValue == "0")
            //{
            //    x = false;
            //    if (s == "") s = tipe.ID;
            //    tipec.Text = "Pilih Tipe";
            //}
            //else
            //    tipec.Text = "*";

            if (sumberdata.SelectedValue == "0")
            {
                x = false;
                if (s == "") s = sumberdata.ID;
                sumberdatac.Text = "Pilih Sumber Data";
            }
            else
                sumberdatac.Text = "*";

            if (!Cf.isMoney(nilaibayar))
            {
                x = false;
                if (s == "") s = nilaibayar.ID;
                nilaibayarc.Text = "Angka";
            }
            else
                nilaibayarc.Text = "*";


            if (carabayar.SelectedIndex == -1)
            {
                x = false;

                if (s == "")
                    s = ddlAcc.ID;

                carabayarc.Text = "Harus dipilih";
            }
            else
                carabayarc.Text = "*";

            if (ddlAcc.SelectedIndex == 0)
            {
                x = false;

                if (s == "")
                    s = ddlAcc.ID;

                ddlAccErr.Text = "Harus dipilih";
            }
            else
                ddlAccErr.Text = "*";

            if (Cf.isEmpty(ktp1))
            {
                x = false;
                if (s == "") s = ktp1.ID;
                ktpc.Text = "Kosong";
            }
            else
                ktpc.Text = "*";

            if (Cf.isEmpty(Korespon1))
            {
                x = false;
                if (s == "") s = Korespon1.ID;
                Koresponc.Text = "Kosong";
            }
            else
                Koresponc.Text = "*";

            //if (carabayar.SelectedValue == "KD" || carabayar.SelectedValue == "KK")
            //{
            //    if (Cf.isEmpty(txtNamaBank))
            //    {
            //        x = false;
            //        if (s == "") s = txtNamaBank.ID;
            //        txtNamaBankc.Text = "Kosong";
            //    }
            //    else
            //        txtNamaBankc.Text = "*";

            //    if (Cf.isEmpty(nokk1) || Cf.isEmpty(nokk2) || Cf.isEmpty(nokk3) || Cf.isEmpty(nokk4))
            //    {
            //        x = false;
            //        if (s == "") s = nokkc.ID;
            //        nokkc.Text = "Kosong";
            //    }
            //    else
            //        nokkc.Text = "*";
            //}
            //else if (carabayar.SelectedValue == "TR")
            //{
            //    if (Cf.isEmpty(txtNamaBankTransfer))
            //    {
            //        x = false;
            //        if (s == "") s = txtNamaBankTransferc.ID;
            //        txtNamaBankTransferc.Text = "Kosong";
            //    }
            //    else
            //        txtNamaBankTransferc.Text = "*";

            //    if (!Cf.isTgl(tglTransfer))
            //    {
            //        x = false;
            //        if (s == "") s = tglTransfer.ID;
            //        tglTransferc.Text = "Tanggal";
            //    }
            //    else
            //        tglTransferc.Text = "*";
            //}

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

        private void InitForm()
        {
            tglNUP.Text = Cf.Day(DateTime.Today);
            nilaibayar.Text = Cf.Num(5000000);
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

            //DataTable rs2 = Db.Rs("SELECT Tipe FROM ISC064_MARKETINGJUAL..REF_TIPE ORDER BY Tipe DESC");
            //for (int i = 0; i < rs2.Rows.Count; i++)
            //{
            //    string v = rs2.Rows[i]["Tipe"].ToString();
            //    string t = rs2.Rows[i]["Tipe"].ToString();

            //    tipe.Items.Add(new ListItem(t, v));
            //}

            string strSql = "SELECT * FROM REF_EVENT WHERE Status='L' ORDER BY SN";
            DataTable rs3 = Db.Rs(strSql);
            for (int i = 0; i < rs3.Rows.Count; i++)
            {
                string v = rs3.Rows[i]["SN"].ToString();
                string d = rs3.Rows[i]["Event"].ToString();
                string t = d + " - " + rs3.Rows[i]["Nama"].ToString();
                sumberdata.Items.Add(new ListItem(t, v));
            }
            sumberdata.SelectedIndex = 0;

            DataTable rs4 = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_ACC");
            for (int i = 0; i < rs4.Rows.Count; i++)
            {
                string v = rs4.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs4.Rows[i]["Bank"] + " " + rs4.Rows[i]["Rekening"];
                ddlAcc.Items.Add(new ListItem(t, v));
            }

            ddlAcc.SelectedValue = "102-401"; //BNI
            nilaibayar.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibayar.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibayar.Attributes["onblur"] = "CalcBlur(this);";
        }


        private void AutoID()
        {

            int c = Db.SingleInteger("SELECT COUNT(NoNUP) FROM MS_NUP");

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c += 1;
                nomorNUP.Text = "8" + c.ToString().PadLeft(3, '0');


                if (isUnique()) hasfound = true;
            }
        }
        private bool isUnique()
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(NoNUP) FROM MS_NUP WHERE NoNUP = '" + NoNUP + "'");

            if (c == 0)
                return true;
            else
                return false;
        }
        protected void save_Click(object sender, System.EventArgs e)
        {


            if (valid())
            {
                AutoID();
                DateTime TglNUP = Convert.ToDateTime(tglNUP.Text);
                string noBaruNUP = NoNUP;
                string SumberData = Cf.Str(sumberdata.SelectedValue);
                string Nama = Cf.Str(nama.Text);
                string NoHP = Cf.Str(chp.Text);
                string NoTelp = Cf.Str(ctelp.Text);
                string NoKTP = Cf.Str(noktp.Text);
                string NoNPWP = Cf.Str(nonpwp.Text);
                string Email = Cf.Str(email.Text);

                string KTP1 = Cf.Str(ktp1.Text);
                string KTP2 = Cf.Str(ktp2.Text);
                string KTP3 = Cf.Str(ktp3.Text);
                string KTP4 = Cf.Str(ktp4.Text);

                string kor1 = Cf.Str(Korespon1.Text);
                string kor2 = Cf.Str(Korespon2.Text);
                string kor3 = Cf.Str(Korespon3.Text);
                string kor4 = Cf.Str(Korespon4.Text);


                string RekBank = Cf.Str(bank.Text);
                string RekCabang = Cf.Str(cabang.Text);
                string RekNomor = Cf.Str(rek.Text);
                string RekAN = Cf.Str(reknama.Text);
                int NoAgent = Convert.ToInt32(agent.SelectedValue);
                string NamaAgent = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + NoAgent + "'");
                string Tipe = Cf.Str(tipe.SelectedValue);

                Db.Execute("EXEC spNUPDaftar"
                    + " '" + noBaruNUP + "'"
                    + ",'" + TglNUP + "'"
                    + ",'" + SumberData + "'"
                    + ",'" + Nama + "'"
                    + ",'" + NoKTP + "'"
                    + ",'" + NoNPWP + "'"
                    + ",'" + Email + "'"
                    + ",'" + NoTelp + "'"
                    + ",'" + NoHP + "'"
                    + ",'" + KTP1 + "'"
                    + ",'" + KTP2 + "'"
                    + ",'" + KTP3 + "'"
                    + ",'" + KTP4 + "'"
                    + ",'" + kor1 + "'"
                    + ",'" + kor2 + "'"
                    + ",'" + kor3 + "'"
                    + ",'" + kor4 + "'"
                    + "," + NoAgent
                    + ",'" + NamaAgent + "'"
                    + ",'" + Tipe + "'"
                    + ",'" + RekBank + "'"
                    + ",'" + RekNomor + "'"
                    + ",'" + RekAN + "'"
                    + ",'" + RekCabang + "'"
                    + ",'" + Act.UserID + "'"
                    );

                SavePembayaran(Nama);

                //Log File
                DataTable rs = Db.Rs(" SELECT "
                    + " NoNUP AS [No NUP]"
                    + ",CONVERT(varchar,MS_NUP.TglNUP,106) AS [Tanggal NUP]"
                    + ",SumberData + ' (' +(SELECT Nama FROM REF_EVENT WHERE Event = SumberData)+')' AS [Sumber Data]"
                    + ",NamaCustomer AS [Nama Customer]"
                    + ",NoKTP  AS [No KTP]"
                    + ",NoNPWP AS [No NPWP]"
                    + ",Email AS [Email]"
                    + ",NoTelp AS [No Telp]"
                    + ",NoHP AS [No HP]"
                    + ",KTP1 AS [Alamat KTP]"
                    + ",KTP2 AS [Alamat KTP RT/RW]"
                    + ",KTP3 AS [Alamat KTP Kecamatan]"
                    + ",KTP4 AS [Alamat KTP Kotamadya]"
                    + ",Alamat1 AS [Alamat Korespodensi]"
                    + ",Alamat2 AS [Alamat Korespodensi RT/RW]"
                    + ",Alamat3 AS [Alamat Korespodensi Kecamatan]"
                    + ",Alamat4 AS [Alamat Korespodensi Kotamadya]"
                    + ",NoAgent AS [No Agent]"
                    + ",NamaAgent AS [Nama Agent]"
                    + ",RekBank AS [Rekening Bank Refund]"
                    + ",RekNomor AS [No Rekening Refund]"
                    + ",RekAN AS [A/N Rekening Refund]"
                    + ",RekCabang AS [Cabang Rekening Refund]"
                    + ",UserID AS [User ID]"
                    + ",(SELECT Nama FROM ISC064_SECURITY..USERNAME WHERE UserID = MS_NUP.UserID) AS [Nama User]"
                    + ",TglInput"
                    + " FROM MS_NUP "
                    + " WHERE NoNUP =" + noBaruNUP
                );

                Db.Execute("EXEC spLogNUP"
                + " 'REGIS'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + noBaruNUP + "'"
                + ",'" + Tipe + "'"
                );
                //GenerateQR();
                Response.Redirect("NUPDaftar2.aspx?No=" + noBaruNUP);

            }
        }
        private void GenerateQR()
        {
            //QRCodeEncoder encoder = new QRCodeEncoder();
            //Bitmap img = encoder.Encode(NoNUP);
            ////img.Save("D:\\AM\\ISC064\\app\\launching\\QR\\" + NoNUP + ".jpg", ImageFormat.Jpeg);
            
        }
        private void SavePembayaran(string Nama)
        {
            DateTime TglTTNUP = Convert.ToDateTime(tglNUP.Text);
            string NoTTNUP = AutoIDTTNUP;
            string CaraBayar = carabayar.SelectedValue;
            string Ket = Cf.Str(ket.Text);
            decimal Total = Convert.ToDecimal(nilaibayar.Text);
            string Acc = ddlAcc.SelectedValue;

            Db.Execute("EXEC spTTNUPPelunasan"
                        + " '" + NoTTNUP + "'"
                        + ",'" + NoNUP + "'"
                        + ",'" + TglTTNUP + "'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Nama + "'"
                        + ",'" + CaraBayar + "'"
                        + ",'" + Ket + "'"
                        + "," + Total
                        + ",'" + Acc + "'"
                        );

            if (carabayar.SelectedValue == "KD" || carabayar.SelectedValue == "KK")
            {
                string NoKartu = nokk1.Text + "-" + nokk2.Text + "-" + nokk3.Text + "-" + nokk4.Text;
                Db.Execute("UPDATE MS_NUP_PELUNASAN SET NoKartu='" + NoKartu + "',NamaBank='" + txtNamaBank.Text + "' WHERE NoTTNUP='" + NoTTNUP + "'");
            }
            DataTable rs = Db.Rs(" SELECT"
                 + " NoTTNUP AS [No TTNUP]"
                 + ",NoNUP AS [No NUP]"
                 + ",CONVERT(varchar,TgLTTNUP,106) AS [Tanggal NUP]"
                 + ",Customer"
                 + ",CaraBayar AS [Cara Bayar]"
                 + ",Ket AS [Keterangan]"
                 + ",Total"
                 + ",NoKartu AS [No Kartu Debit / Kredit]"
                 + ",NamaBank AS [Bank Penerbit]"
                 + ",CONVERT(varchar,TglTransfer,106) AS [Tanggal Transfer]"
                 + "FROM MS_NUP_PELUNASAN WHERE NoTTNUP = '" + NoTTNUP + "'"
                 );

            Db.Execute("EXEC spLogNUPPelunasan"
               + " 'REGIS'"
               + ",'" + Act.UserID + "'"
               + ",'" + Act.IP + "'"
               + ",'" + Cf.LogCapture(rs) + "'"
               + ",'" + NoTTNUP + "'"
               );

        }
        private string AutoIDTTNUP
        {
            get
            {
                int c = Db.SingleInteger("SELECT COUNT(NoTTNUP) FROM MS_NUP_PELUNASAN WHERE NoTTNUP != '' AND YEAR(TglTTNUP) = " + TglTTNUP.Year);

                string nobkm = "";
                bool hasfound = false;
                while (!hasfound)
                {
                    if (!Response.IsClientConnected) break;

                    c += 1;
                    //nopjt = c.ToString() + "/" + u + "/" + Convert.ToDateTime(tgl.Text).Year;
                    nobkm = "GWM-TTNUP/" + TglTTNUP.Year + "-" + c.ToString().PadLeft(4, '0');

                    if (isUnique(nobkm)) hasfound = true;
                }

                return nobkm;
            }
        }

        private bool isUnique(string nobkm)
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(NoTTNUP) FROM MS_NUP_PELUNASAN WHERE NoTTNUP = '" + nobkm + "'");

            if (c == 0)
                return true;
            else
                return false;
        }
        private DateTime TglTTNUP
        {
            get
            {
                return Convert.ToDateTime(tglNUP.Text);
            }
        }
        private string NoTTS
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoTTS"]);
            }
        }
        private string NoNUP
        {
            get
            {
                return Cf.Pk(nomorNUP.Text);
            }
        }
        //protected void carabayar_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (carabayar.SelectedIndex == 1 || carabayar.SelectedIndex == 2)
        //    {
        //        ketkartu.Visible = true;
        //        CaraBG.Visible = false;
        //        tranfer.Visible = false;
        //    }
        //    else if (carabayar.SelectedIndex == 4)
        //    {
        //        CaraBG.Visible = true;
        //        ketkartu.Visible = false;
        //        tranfer.Visible = false;
        //    }
        //    else if (carabayar.SelectedIndex == 3)
        //    {
        //        tranfer.Visible = true;
        //        CaraBG.Visible = false;
        //        ketkartu.Visible = false;
        //    }
        //    else
        //    {
        //        ketkartu.Visible = false;
        //        CaraBG.Visible = false;
        //        tranfer.Visible = false;
        //    }
        //}
    }
}
