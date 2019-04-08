using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class MemoCashback : System.Web.UI.Page
    {
        DataTable rs;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                rTglKembali.Visible = false;
                backbtn.Visible = false;
                btnAct.Visible = false;
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&ppjb=1');";

                if (Request.QueryString["NoKontrak"] != null)
                {
                    nokontrak.Text = Request.QueryString["NoKontrak"];
                    LoadKontrak();

                    cancel.Attributes["onclick"] = "location.href='CBRegistrasi1.aspx'";
                }
                else
                {
                    Js.Focus(this, nokontrak);
                    frm.Visible = false;
                }
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses pencatatan Cashback?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                            + "<a href=\"javascript:popEditMEMO('" + Request.QueryString["done"] + "')\">"
                            + "Registrasi Berhasil.."
                            + "</a>"
                            ;
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");// AND PPJB <> 'D'");

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

                Fill();

                Js.Focus(this, save);
                Js.Confirm(this, "Lanjutkan proses pencatatan Cashback?");
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
                Js.Confirm(this, "Lanjutkan proses pencatatan CashBack?");
            }
        }
        decimal LebihBayar1;
        private void Fill()
        {
            sisa.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            sisa.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            sisa.Attributes["onblur"] = "CalcBlur(this);";

            lb.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            lb.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            lb.Attributes["onblur"] = "CalcBlur(this);";

            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            unit.Text = Db.SingleString("SELECT NoUnit "
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK "
                + " WHERE NoKontrak = '" + NoKontrak + "'");

            customer.Text = Db.SingleString("SELECT Nama "
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK "
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER "
                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE NoKontrak = '" + NoKontrak + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                tgl.Text = Cf.Day(DateTime.Today);
                decimal nilaitagihan = 0;
                decimal nilaipelunasan1 = 0;
                decimal nilaipelunasan2 = 0;
                decimal sisatagihan = 0;
                decimal lebihbayar = 0;
                decimal bankkeluar = 0;

                DataTable rs1 = Db.Rs("SELECT * "
                + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN "
                + " WHERE NoKontrak = '" + NoKontrak + "'");

                for (int i = 0; i < rs1.Rows.Count; i++)
                {
                    decimal x = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = '" + rs1.Rows[i]["Nourut"] + "' AND CaraBayar!='AL'");
                    nilaitagihan += Convert.ToDecimal(rs1.Rows[i]["NilaiTagihan"]);

                    if (x > Convert.ToDecimal(rs1.Rows[i]["NilaiTagihan"]))
                    {
                        nilaipelunasan1 += Convert.ToDecimal(rs1.Rows[i]["NilaiTagihan"]);
                    }
                    else
                    {
                        nilaipelunasan1 += x;
                    }

                    nilaipelunasan2 += x;
                }

                decimal sumAlokasi = Db.SingleDecimal("use ISC064_marketingjual;Select ISNULL(SUM(NilaiPelunasan),0) from ms_pelunasan where nokontrak='" + NoKontrak + "' and CaraBayar='AL'");
                decimal MemoCB = Db.SingleDecimal("Select ISNULL(SUM(LebihBayar),0) from MS_CASHBACK_MEMO where nokontrak='" + NoKontrak + "'");
                lebihbayar = Db.SingleDecimal("SELECT ISNULL(SUM(LB),0) FROM MS_TTS WHERE Ref = '" + NoKontrak + "' AND Status <> 'VOID'");
                sisatagihan = nilaitagihan - nilaipelunasan1;
                bankkeluar = nilaitagihan - nilaipelunasan2;
                decimal BK = Db.SingleDecimal("SELECT ISNULL(SUM(BankKeluar),0) FROM MS_CASHBACK WHERE NoKontrak = '" + NoKontrak + "'");

                sisa.Text = Cf.Num(sisatagihan);
                lb.Text = Cf.Num(lebihbayar - BK - sumAlokasi-MemoCB);
                if (lebihbayar - BK - sumAlokasi <= 0)
                {
                    btnClaim.Enabled = false;                    
                }
            }
        }

        private bool datavalid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

            if (!Cf.isMoney(sisa))
            {
                x = false;
                if (s == "") s = sisa.ID;
                sisac.Text = "Angka";
            }
            else
                sisac.Text = "";

            if (!Cf.isMoney(lb))
            {
                x = false;
                if (s == "") s = lb.ID;
                lbc.Text = "Angka";
            }
            else
                lbc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Sisa Tagihan harus berupa angka.\\n"
                    + "3. Lebih Bayar harus berupa angka.\\n"                    
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                string Unit = Cf.Str(unit.Text);
                string Customer = Cf.Str(customer.Text);

                DateTime Tgl = Convert.ToDateTime(tgl.Text);
                decimal Sisa = Convert.ToDecimal(sisa.Text);
                decimal LB = Convert.ToDecimal(lb.Text);
                int NoMemo = Db.SingleInteger("SELECT COUNT(NoMemo) FROM MS_CASHBACK_MEMO");
                if (NoMemo == 0)
                {
                    NoMemo = 1;
                }
                else
                {
                    NoMemo = Db.SingleInteger("SELECT TOP 1 NoMemo FROM MS_CASHBACK_MEMO ORDER BY NoMemo DESC");
                }
                int NoUrut = Db.SingleInteger("select isnull((SELECT ISNULL(MAX(NoUrut),0)+1 FROM MS_CASHBACK_MEMO WHERE NoKontrak = '" + NoKontrak + "'),1)");

                Db.Execute("EXEC spMEMOCBRegistrasi "
                    + " '" + NoKontrak + "'"
                    + ", " + NoUrut
                    + ",'" + Unit + "'"
                    + ",'" + Customer + "'"
                    + ",'" + Tgl + "'"
                    + ",'" + Sisa + "'"
                    + ",'" + LB + "'"
                    //+ ",''"
                    );

                DataTable rs = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglPengembalian, 106) AS [Tanggal Pengembalian]"
                    + ",NoKontrak AS [No. Kontrak]"
                    + ",NoUrut AS [No. Urut]"
                    + ",Unit"
                    + ",Customer"
                    + ",SisaTagihan AS [Sisa Tagihan]"
                    + ",LebihBayar AS [Lebih Bayar]"
                    + " FROM MS_CASHBACK_MEMO"
                    + " WHERE NoMemo = " + NoMemo
                    );
                string ketlog = Cf.LogCapture(rs);

                Db.Execute("EXEC spLogMemoCashback"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ketlog + "'"
                    + ",'" + NoMemo.ToString().PadLeft(7, '0') + "'"
                    );


                Response.Redirect("MemoCashback.aspx?done=" + NoMemo);
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

        protected void btnClaim_Click(object sender, EventArgs e)
        {
            rTglKembali.Visible = true;
            btnAct.Visible = true;
            btnClaim.Visible = false;            
        }
    }
}
