using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class CBRegistrasi1 : System.Web.UI.Page
    {
        DataTable rs;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                rBankKeluar.Visible = false;
                rRek.Visible = false;
                rTglKembali.Visible = false;
                backbtn.Visible = false;
                btnAct.Visible = false;
                
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&ppjb=1');";
                fillAcc();

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

                Js.NumberFormat(bk);
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
                {
                    if (Request.QueryString["memo"] != null)
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                                + "<a href=\"javascript:popEditMEMO('" + Request.QueryString["done"] + "')\">"
                                + "Registrasi Berhasil.."
                                + "</a>"
                                ;
                    else
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                                + "<a href=\"javascript:popEditCB('" + Request.QueryString["done"] + "')\">"
                                + "Registrasi Berhasil.."
                                + "</a>"
                                ;
                }
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

        private void fillAcc()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_ACC where Project IN (" + Act.ProjectListSql + ")");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                ddlAcc.Items.Add(new ListItem(t, v));
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

                decimal sumAlokasi = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak='" + NoKontrak + "' AND CaraBayar='AL'");
                decimal MemoCB = 0;
                lebihbayar = Db.SingleDecimal("SELECT ISNULL(SUM(LB),0) FROM MS_TTS WHERE Ref = '" + NoKontrak + "' AND Status <> 'VOID'");
                sisatagihan = nilaitagihan - nilaipelunasan1;
                bankkeluar = nilaitagihan - nilaipelunasan2;
                //                lebihbayar = sisatagihan - bankkeluar - sumAlokasi;

                decimal BK = Db.SingleDecimal("SELECT ISNULL(SUM(BankKeluar),0) FROM MS_CASHBACK WHERE NoKontrak = '" + NoKontrak + "'");

                sisa.Text = Cf.Num(sisatagihan);
                lb.Text = Cf.Num(lebihbayar - BK - sumAlokasi - MemoCB);
                bk.Text = Cf.Num(Convert.ToDecimal(lb.Text));


                if (lebihbayar - BK <= 0)
                {
                    btnClaim.Enabled = btnAlokasi.Enabled = btnPendapatan.Enabled = false;
                }
            }
        }
        private bool datavalid(int tipe)
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

            if (ddlAcc.SelectedIndex == 0 && tipe == 0)
            {
                x = false;

                if (s == "")
                    s = ddlAcc.ID;

                ddlAccErr.Text = "Harus dipilih";
            }
            else
                ddlAccErr.Text = "";

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

            if (!Cf.isMoney(bk))
            {
                x = false;
                if (s == "") s = bk.ID;
                bkc.Text = "Angka";
            }
            else
                bkc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Sisa Tagihan harus berupa angka.\\n"
                    + "3. Lebih Bayar harus berupa angka.\\n"
                    + "4. Bank harus berupa angka."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }
        protected void save_Click(object sender, System.EventArgs e)
        {
            Save(0); // refund
        }
        protected void save2_Click(object sender, EventArgs e)
        {
            Save(1); // memo pendapatan
        }

        protected void Save(int Tipe)
        {

            if (datavalid(Tipe))
            {
                if (Convert.ToDecimal(bk.Text) == 0)
                {
                    string Unit = Cf.Str(unit.Text);
                    string Customer = Cf.Str(customer.Text);

                    DateTime Tgl = Convert.ToDateTime(tgl.Text);
                    decimal Sisa = Convert.ToDecimal(sisa.Text);
                    decimal BK = Convert.ToDecimal(bk.Text);
                    decimal LB = Convert.ToDecimal(lb.Text);
                    decimal CB = BK;
                    string Bank = Cf.Str(ddlAcc.SelectedValue);
                    int Nocb = Db.SingleInteger("SELECT TOP 1 Nocb FROM MS_CASHBACK ORDER BY Nocb DESC");
                    int NoUrut = Db.SingleInteger("select isnull((SELECT ISNULL(MAX(NoUrut),0)+1 FROM MS_CASHBACK WHERE NoKontrak = '" + NoKontrak + "'),1)");

                    Db.Execute("EXEC spCBRegistrasi "
                        + " '" + NoKontrak + "'"
                        + ", " + NoUrut
                        + ",'" + Unit + "'"
                        + ",'" + Customer + "'"
                        + ",'" + Tgl + "'"
                        + ",'" + Sisa + "'"
                        + ",'" + LB + "'"
                        + ",'" + CB + "'"
                        + ",'" + BK + "'"
                        + ",'" + Bank + "'"
                        + "," + Tipe
                        );

                    DataTable rs = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglPengembalian, 106) AS [Tanggal Pengembalian]"
                    + ",NoKontrak AS [No. Kontrak]"
                    + ",NoUrut AS [No. Urut]"
                    + ",Unit"
                    + ",Customer"
                    + ",SisaTagihan AS [Sisa Tagihan]"
                    + ",LebihBayar AS [Lebih Bayar]"
                    + ",Cashback"
                    + ", BankKeluar AS [Bank Keluar]"
                    + ", Bank AS [No. Rekening]"
                    + " FROM MS_CASHBACK"
                    + " WHERE Nocb = " + Nocb
                    );

                    string ketlog = Cf.LogCapture(rs);

                    //Project & NamaProject
                    string Project = Db.SingleString("SELECT Project FROM ISC064_MARKETINGJUAL..MS_KONTRAK where NoKontrak ='" + NoKontrak + "'");
                    string NamaProject = Db.SingleString("SELECT Nama FROM ISC064_SECURITY..REF_PROJECT where Project ='" + Project + "'");
                    //update project & namaproject
                    Db.Execute("UPDATE MS_CASHBACK SET Project = '" + Project + "', NamaProject='" + NamaProject + "' WHERE Nocb ='" + Nocb + "'");

                    Db.Execute("EXEC spLogMemoCashback"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ketlog + "'"
                    + ",'" + Nocb.ToString().PadLeft(7, '0') + "'"
                    );
                    //    );

                    Response.Redirect("CbRegistrasi1.aspx?done=" + Nocb);
                }
                else if (Convert.ToDecimal(bk.Text) != 0)
                {
                    string Unit = Cf.Str(unit.Text);
                    string Customer = Cf.Str(customer.Text);

                    DateTime Tgl = Convert.ToDateTime(tgl.Text);
                    decimal Sisa = Convert.ToDecimal(sisa.Text);
                    decimal BK = Convert.ToDecimal(bk.Text);
                    decimal LB = Convert.ToDecimal(lb.Text);
                    decimal CB = LB - BK;
                    string Bank = Cf.Str(ddlAcc.SelectedValue);
                    int NoUrut = Db.SingleInteger("SELECT ISNULL(MAX(NoUrut),0)+1 FROM MS_CASHBACK WHERE NoKontrak = '" + NoKontrak + "'");                    

                    Db.Execute("EXEC spCBRegistrasi "
                        + " '" + NoKontrak + "'"
                        + ", " + NoUrut
                        + ",'" + Unit + "'"
                        + ",'" + Customer + "'"
                        + ",'" + Tgl + "'"
                        + ",'" + Sisa + "'"
                        + ",'" + LB + "'"
                        + ",'" + CB + "'"
                        + ",'" + BK + "'"
                        + ",'" + Bank + "'"
                        + "," + Tipe
                        );
                    
                    int Nocb = Db.SingleInteger("SELECT TOP 1 Nocb  FROM MS_CASHBACK ORDER BY Nocb DESC");                    
                    DataTable rs = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglPengembalian, 106) AS [Tanggal Pengembalian]"
                    + ",NoKontrak AS [No. Kontrak]"
                    + ",NoUrut AS [No. Urut]"
                    + ",Unit"
                    + ",Customer"
                    + ",SisaTagihan AS [Sisa Tagihan]"
                    + ",LebihBayar AS [Lebih Bayar]"
                    + ",Cashback"
                    + ", BankKeluar AS [Bank Keluar]"
                    + ", Bank AS [No. Rekening]"
                    + " FROM MS_CASHBACK"
                    + " WHERE Nocb = " + Nocb
                    );

                    string ketlog = Cf.LogCapture(rs);

                    Db.Execute("EXEC spLogCashback"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ketlog + "'"
                    + ",'" + Nocb.ToString().PadLeft(7, '0') + "'"
                    );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_CASHBACK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_CASHBACK WHERE Nocb = '" + Nocb + "'");
                    Db.Execute("UPDATE MS_CASHBACK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    Response.Redirect("CbRegistrasi1.aspx?done=" + Nocb);

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

        protected void btnClaim_Click(object sender, EventArgs e)
        {
            rTglKembali.Visible = true;
            rRek.Visible = true;
            rBankKeluar.Visible = true;
            btnAct.Visible = true;
            save2.Visible = false;
            trtombol.Visible = trsisa.Visible = false;
        }

        protected void btnPendapatan_Click(object sender, EventArgs e)
        {
            rTglKembali.Visible = true;
            rRek.Visible = true;
            rBankKeluar.Visible = true;
            btnAct.Visible = true;
            trtombol.Visible = trsisa.Visible = false;
            save.Visible = false;
            labelbankkeluar.InnerHtml = "Pengakuan Pendapatan";
            labeltglkembali.InnerHtml = "Tanggal";
            rRek.Visible = false;
        }
        protected void btnAlokasi_Click(object sender, EventArgs e)
        {
            Response.Redirect("CBRegistrasiAlokasi.aspx?NoKontrak=" + NoKontrak);
        }

        //bool validBayar()
        //{
        //    bool x = true;
        //    try
        //    {
        //        int i = 0;
        //        decimal total = 0;
        //        foreach (TableRow a in tbList.Rows)
        //        {
        //            TableCell NoTagihan = (TableCell)tbList.FindControl("NoTagihan_" + i);
        //            TextBox NilaiBayar = (TextBox)tbList.FindControl("_nilaiBayar" + i);


        //            if (NoTagihan != null)
        //            {

        //                if (NilaiBayar.Text != ""
        //                || NilaiBayar.Text != "0"
        //                || !Cf.isMoney(NilaiBayar)
        //                )
        //                {
        //                    x = false;

        //                }
        //                else
        //                {
        //                    if (NilaiBayar.Text != "" && Cf.isMoney(NilaiBayar))
        //                    {
        //                        total += Convert.ToDecimal(NilaiBayar.Text);
        //                    }
        //                }

        //                if (total > Convert.ToDecimal(lb.Text))
        //                {
        //                    x = false;

        //                }
        //            }
        //            else
        //            {
        //                x = false;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('" + ex.Message + "');</script>");
        //        x = false;
        //    }

        //    return true;
        //}

    }
}
