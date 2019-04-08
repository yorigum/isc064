using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class PJTDownload : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.CheckBoxList tipe;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                //comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                init();
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }

        private void init()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(dari))
            {
                x = false;
                if (s == "") s = dari.ID;
                daric.Text = "Tanggal";
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                x = false;
                if (s == "") s = sampai.ID;
                sampaic.Text = "Tanggal";
            }
            else
                sampaic.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        protected void scr_Click(object sender, System.EventArgs e)
        {
            if (valid())
                Report();
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
                Rpt.ToExcel(this, rpt);
            }
        }

        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            Header();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            //Rpt.Judul(x, comp, judul);

            //string tgl = "";
            //if (tglinput.Checked) tgl = tglinput.Text;

            //DateTime Dari = Convert.ToDateTime(dari.Text);
            //DateTime Sampai = Convert.ToDateTime(sampai.Text);
            //Rpt.SubJudul(x
            //    , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
            //    );

            //Rpt.SubJudul(x
            //    , "Kasir : " + kasir.SelectedItem.Text
            //    );

            //Rpt.Header(rpt, x);
        }

        private void Fill()
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string strSql = "SELECT a.*, c.NoHP, b.NoUnit, b.NoCustomer, b.NoVA "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER c ON b.NoCustomer = c.NoCustomer"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,a.TglJT,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,a.TglJT,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND b.Status = 'A'"
                + " AND b.Project IN (" + Act.ProjectListSql + ")"
                + " ORDER BY b.NoUnit";

            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                decimal Tag = 0, Bayar = 0, Sisa = 0;
                DataTable aa = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN"
                    + " WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND CONVERT(VARCHAR, TglJT, 112) < '" + Cf.Tgl112(Dari) + "'"
                    );
                for (int j = 0; j < aa.Rows.Count; j++)
                {
                    Tag += Convert.ToDecimal(aa.Rows[j]["NilaiTagihan"]);
                    Bayar += Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoTagihan = '" + aa.Rows[j]["NoUrut"] + "'");
                }
                Sisa = Tag - Bayar;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = rs.Rows[i]["NoHP"].ToString() != "" ? "&nbsp;" + rs.Rows[i]["NoHP"].ToString() : "-";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                string Pesan = "Yth: Bpk/Ibu pemilik unit {F}, Tagihan Angsuran ke {H}, = Rp. {I}, Jatuh tempo {G}, Outstanding = Rp. {J} (bila ada) Info Lebih Lanjut Hub : 2952 7200. Mohon abaikan bila tagihan sudah lunas, Terima Kasih.";

                //string Pesan2 = "Yth: Bpk/Ibu pemilik unit " + rs.Rows[i]["NoUnit"] 
                //    + ", Tagihan " + rs.Rows[i]["NamaTagihan"] + ", = Rp. " + Cf.Num(rs.Rows[i]["NilaiTagihan"]) 
                //    + ", Jatuh tempo " + Cf.Day(rs.Rows[i]["TglJT"]) 
                //    + ", Outstanding = Rp. " + Cf.Num(Sisa) + " (bila ada) Info Lebih Lanjut Hub : 2952 7200."
                //    + " Mohon abaikan bila tagihan sudah lunas, Terima Kasih.";

                c = new TableCell();
                c.Text = Pesan;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT NAMA FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NOCUSTOMER='" + rs.Rows[i]["NoCustomer"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoVA"].ToString() + "&nbsp";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglJT"].ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaTagihan"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"].ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Sisa);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Sisa + Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]));
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
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
