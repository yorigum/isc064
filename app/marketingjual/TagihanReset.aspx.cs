using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class TagihanReset : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                backbtn.Visible = false;
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a');";

                if (Request.QueryString["NoKontrak"] != null)
                {
                    dariDaftar.Checked = true;
                    nokontrak.Text = Request.QueryString["NoKontrak"];
                    LoadKontrak();

                    cancel.Attributes["onclick"] = "location.href='KontrakDaftar3.aspx?NoKontrak=" + NoKontrak + "'";
                }
                else
                {
                    Js.Focus(this, nokontrak);
                    frm.Visible = false;
                }
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses reset jadwal tagihan?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popJadwalTagihan('" + Request.QueryString["done"] + "')\">"
                        + "Reset Tagihan Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");

            if (c == 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
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

                InitForm();
                Fill();

                Js.Confirm(this, "Lanjutkan proses reset jadwal tagihan?");
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

                InitForm();
                Fill();

                Js.Confirm(this, "Lanjutkan proses reset jadwal tagihan?");
            }
        }

        private void InitForm()
        {
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            //Cara bayar
            DataTable rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' AND Project = '" + Project + "' ORDER BY Nama");
            carabayar.Items.Add(new ListItem("*** CUSTOMIZE / PENDING", "0")); //cara bayar customize

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Nomor"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                carabayar.Items.Add(new ListItem(t, v));
            }
            carabayar.SelectedIndex = 0;
            carabayar.Attributes["ondblclick"] = "kalk(this)";

            tglkontrak.Attributes["style"] = "border:0;font:bold 9pt;font-family: Trebuchet MS;";

            nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

            if (Func.CekAkunting(NoKontrak))
                lblAkunting.Text = "Transaksi sudah pernah diposting ke Akunting";
            else
                lblAkunting.Text = "";
        }

        private void Fill()
        {
            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT * "
                + " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                pl.Text = Cf.Num(rs.Rows[0]["Gross"]);

                tglkontrak.Text = Cf.Day(rs.Rows[0]["TglKontrak"]);
                netto.Text = Cf.Num(rs.Rows[0]["NilaiKontrak"]);
                gross.Text = Cf.Num(rs.Rows[0]["Gross"]);

                skema.Text = rs.Rows[0]["Skema"].ToString();
            }
        }

        private bool datavalid()
        {
            bool x = true;
            string s = "";

            if (!Cf.isMoney(nilaibiaya))
            {
                x = false;
                if (s == "") s = nilaibiaya.ID;
                nilaibiayac.Text = "Angka";
            }
            else
                nilaibiayac.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Biaya Administrasi harus berupa angka."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                DataTable rsHeaderBef = Db.Rs("SELECT "
                    + " Skema"
                    + ",NilaiKontrak AS [Nilai Kontrak]"
                    + ",DiskonRupiah AS [Diskon dalam Rupiah]"
                    + ",DiskonPersen AS [Diskon dalam Persen]"
                    + ",DiskonKet AS [Keterangan Diskon]"
                    + " FROM MS_KONTRAK"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );

                DataTable rsBef = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                    + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                SaveTagihan();

                DataTable rsHeaderAft = Db.Rs("SELECT "
                    + " Skema"
                    + ",NilaiKontrak AS [Nilai Kontrak]"
                    + ",DiskonRupiah AS [Diskon dalam Rupiah]"
                    + ",DiskonPersen AS [Diskon dalam Persen]"
                    + ",DiskonKet AS [Keterangan Diskon]"
                    + " FROM MS_KONTRAK"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );

                DataTable rsAft = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                    + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                DataTable rsDetail = Db.Rs("SELECT"
                    + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                    + ",MS_KONTRAK.NoUnit AS [Unit]"
                    + ",MS_CUSTOMER.Nama AS [Customer]"
                    + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                    + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                    + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
                if (NilaiBiaya != 0)
                {
                    Db.Execute("EXEC spTagihanDaftar "
                        + " '" + NoKontrak + "'"
                        + ",'BIAYA ADM. GANTI CARA BAYAR'"
                        + ",'" + Cf.Day(DateTime.Today) + "'"
                        + ", " + NilaiBiaya
                        + ",'ADM'"
                        );
                }

                string ket2 = "";
                if (pakaigross.Checked)
                    ket2 = "---DISKON DIHITUNG ULANG---";
                else
                    ket2 = "---DISKON TIDAK BERUBAH---";

                string Ket = Cf.LogCapture(rsDetail)
                    + "<br>" + ket2 + "<br><br>"
                    + Cf.LogCompare(rsHeaderBef, rsHeaderAft)
                    + "<br>-----------------------------<br>"
                    + Cf.LogList(rsBef, rsAft, "JADWAL TAGIHAN")
                    + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                    ;

                Db.Execute("EXEC spLogKontrak"
                    + " 'RT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoKontrak + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Func.CekKomisi(NoKontrak);

                if (alokasi.Checked)
                {
                    if (dariDaftar.Checked)
                        Response.Redirect("Alokasi.aspx?reset=1&dd=1&NoKontrak=" + NoKontrak);
                    else
                        Response.Redirect("Alokasi.aspx?reset=1&NoKontrak=" + NoKontrak);
                }
                else
                {
                    if (dariDaftar.Checked)
                        Response.Redirect("KontrakDaftar3.aspx?done=1&NoKontrak=" + NoKontrak);
                    else
                        Response.Redirect("TagihanReset.aspx?done=" + NoKontrak);
                }
            }
        }

        private void SaveTagihan()
        {
            //reset tagihan
            Db.Execute("EXEC spTagihanReset '" + NoKontrak + "'");

            //skema
            string Skema = Cf.Str(carabayar.SelectedItem.Text);
            Db.Execute("UPDATE MS_KONTRAK SET Skema = '" + Skema + "' WHERE NoKontrak = '" + NoKontrak + "'");

            int CaraBayar = Convert.ToInt32(carabayar.SelectedValue);

            //cara bayar 0 = customize
            if (CaraBayar != 0)
            {
                decimal Netto = 0;

                if (pakaigross.Checked)
                {
                    //hitung ulang diskon
                    string RumusDiskon = Db.SingleString(
                        "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + CaraBayar);

                    string RumusDiskon2 = Db.SingleString(
                        "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + CaraBayar);

                    decimal Gross = Db.SingleDecimal(
                        "SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                    Netto = Func.SetelahDiskon(RumusDiskon, Gross);

                    Db.Execute("EXEC spKontrakDiskon"
                        + " '" + NoKontrak + "'"
                        + ", " + Gross
                        + ", " + Netto
                        + ", " + (Gross - Netto)
                        + ",'" + RumusDiskon + "'"
                        + ",'" + Cf.Str(RumusDiskon2) + "'"
                        );
                }
                else
                {
                    //pakai diskon yang berlaku sekarang
                    Netto = Db.SingleDecimal(
                        "SELECT NilaiKontrak FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                }

                string[,] x = Func.Breakdown(CaraBayar, Netto, Convert.ToDateTime(tglkontrak.Text));
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
