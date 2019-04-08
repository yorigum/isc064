using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class CBEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("Nocb");


            if (!Page.IsPostBack)
            {
                Js.Focus(this, tgl);

                fillAcc();
                Fill();

                Js.NumberFormat(sisa);
                Js.NumberFormat(lb);
                Js.NumberFormat(bk);
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

        private void fillAcc()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_ACC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                bank.Items.Add(new ListItem(t, v));
            }
        }

        private void Fill()
        {
            string strSql = "SELECT * "
                        + " FROM MS_CASHBACK WHERE Nocb = " + Nocb;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                tgl.Text = Cf.Day(rs.Rows[0]["TglPengembalian"]);
                sisa.Text = Cf.Num(rs.Rows[0]["SisaTagihan"]);
                lb.Text = Cf.Num(rs.Rows[0]["LebihBayar"]);
                cb.Text = Cf.Num(rs.Rows[0]["Cashback"]);
                bk.Text = Cf.Num(rs.Rows[0]["BankKeluar"]);
                bank.Text = Cf.Str(rs.Rows[0]["Bank"]);

                if (Convert.ToByte(rs.Rows[0]["Tipe"]) == 0)
                {
                    labelbangkeluar.InnerHtml = "Bank Keluar";
                    tipe.InnerHtml = "Claim";
                }
                else
                {
                    labelbangkeluar.InnerHtml = "Pengakuan Pendapatan";
                    labeltglkembali.InnerHtml = "Tanggal";
                    trbank.Visible = false;
                    tipe.InnerHtml = "MEMO Pendapatan";
                }
            }
        }

        private bool valid()
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

            if (!Cf.isMoney(bk))
            {
                x = false;
                if (s == "") s = bk.ID;
                bkc.Text = "Angka";
            }
            else
                bkc.Text = "";

            if (bank.SelectedIndex == 0)
            {
                x = false;

                if (s == "")
                    s = bank.ID;

                bankc.Text = "Harus dipilih";
            }
            else
                bankc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. SisaTagihan harus berupa angka.\\n"
                    + "3. LebihBayar harus berupa angka.\\n"
                    + "4. Bank harus di isi.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                DateTime Tgl = Convert.ToDateTime(tgl.Text);
                decimal Sisa = Convert.ToDecimal(sisa.Text);
                decimal LB = Convert.ToDecimal(lb.Text);
                decimal BK = Convert.ToDecimal(bk.Text);
                decimal Cb = LB-BK;
                string Bank = Cf.Str(bank.SelectedValue);

                DataTable rs = Db.Rs("SELECT "
                    + " Nocb AS [No.]"
                    + " FROM MS_CASHBACK"
                    + " WHERE Nocb = " + Nocb
                    );

                DataTable rsBef = Db.Rs("SELECT "
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

                Db.Execute("UPDATE MS_CASHBACK SET "
                    + " TglPengembalian = '" + Tgl + "'"
                    + ",SisaTagihan = " + Sisa
                    + ",LebihBayar = " + LB
                    + ",Cashback = " + Cb
                    + ",BankKeluar = " + BK
                    + ",Bank = '" + Bank + "'"
                    + " WHERE Nocb = " + Nocb
                    );

                DataTable rsAft = Db.Rs("SELECT "
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

                string ketlog = Cf.LogCapture(rs)
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogCashback"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ketlog + "'"
                    + ",'" + Nocb.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_CASHBACK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_CASHBACK WHERE Nocb = '" + Nocb + "'");
                Db.Execute("UPDATE MS_CASHBACK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                return true;
            }
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("CBEdit.aspx?done=1&Nocb=" + Nocb);
        }

        private string Nocb
        {
            get
            {
                return Cf.Pk(Request.QueryString["Nocb"]);
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
