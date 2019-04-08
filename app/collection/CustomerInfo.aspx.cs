using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class CustomerInfo : System.Web.UI.Page
    {
        decimal TotalDenda = 0;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                FillTable();
            }
        }

        private void FillTable()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?tb=&Pk=" + Ref + "'";

            string strSql = "";
            if (Tipe != "TENANT")
            {
                strSql = "SELECT *"
                    + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN AS MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') ) AS SisaTagihan"
                    + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "' AND NoRealKPA = '0') AS NilaiPelunasan"
                    + " FROM " + Tb + "..MS_TAGIHAN AS MS_TAGIHAN"
                    + " WHERE NoKontrak = '" + Ref + "'"
                    + " ORDER BY NoUrut";
            }
            else
            {
                strSql = "SELECT *"
                    + ",CASE CaraBayar WHEN '' THEN NilaiTagihan ELSE LebihBayar*-1 END AS SisaTagihan"
                    + ",CASE CaraBayar WHEN '' THEN 0 ELSE NilaiTagihan+LebihBayar END AS Pelunasan"
                    + " FROM " + Tb + "..MS_TAGIHAN"
                    + " WHERE NoPenghuni = '" + Ref + "'"
                    + " ORDER BY Tipe,NoUrut";
            }
            DataTable rs = Db.Rs(strSql);

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;
            decimal t6 = 0;
            decimal t9 = 0;
            decimal t12 = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<br>";
                c.ColumnSpan = 11;
                r.Cells.Add(c);
                rpt.Rows.Add(r);

                r = new TableRow();
                c = new TableCell();
                c.Text = rs.Rows[i]["NoUrut"] + ".";
                if (Tipe == "TENANT")
                    c.Text = rs.Rows[i]["Tipe"] + "." + rs.Rows[i]["NoUrut"];
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<strong>" + rs.Rows[i]["NamaTagihan"].ToString() + "</strong>";
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglJT"]);
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["SisaTagihan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["Denda"]), 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["DendaReal"]), 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiPutihDenda"]), 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal SisaDenda = Math.Round(Convert.ToDecimal(rs.Rows[i]["Denda"]), 0) - Math.Round(Convert.ToDecimal(rs.Rows[i]["DendaReal"]), 0) - Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiPutihDenda"]), 0) - Math.Round(Convert.ToDecimal(rs.Rows[i]["AlokasiBenefit"]), 0);
                c = new TableCell();
                c.Text = Cf.Num(SisaDenda);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                decimal NilaiTagihan = (decimal)rs.Rows[i]["NilaiTagihan"];
                decimal NilaiPelunasan = (decimal)rs.Rows[i]["NilaiPelunasan"];
                decimal SisaTagihan = (decimal)rs.Rows[i]["SisaTagihan"];
                decimal NilaiLebih = Db.SingleDecimal("SELECT ISNULL(SUM(LB),0) FROM MS_TTS a JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS WHERE NoKontrak ='" + Ref + "' AND b.NoTagihan = '" + rs.Rows[i]["NoUrut"] + "'");
                c = new TableCell();
                c.Text = Cf.Num(NilaiLebih);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);

                t1 = t1 + (decimal)rs.Rows[i]["NilaiTagihan"];
                if (Tipe != "TENANT")
                    t2 = t2 + Lunas((int)rs.Rows[i]["NoUrut"], Convert.ToDateTime(rs.Rows[i]["TglJT"]), Convert.ToDecimal(rs.Rows[i]["SisaTagihan"]), Convert.ToDecimal(rs.Rows[i]["Denda"]), Convert.ToDecimal(rs.Rows[i]["DendaReal"]), Convert.ToDecimal(rs.Rows[i]["NilaiPutihDenda"]), Convert.ToDecimal(rs.Rows[i]["Benefit"]), Convert.ToDecimal(rs.Rows[i]["BenefitReal"]), Convert.ToDecimal(rs.Rows[i]["AlokasiBenefit"]), NilaiLebih);
                else
                    t2 = t2 + (decimal)rs.Rows[i]["Pelunasan"];
                t3 = t3 + (decimal)rs.Rows[i]["SisaTagihan"];
                t4 = t4 + (decimal)rs.Rows[i]["Denda"];
                t5 += Convert.ToDecimal(rs.Rows[i]["Dendareal"]);
                t6 += Convert.ToDecimal(rs.Rows[i]["NilaiPutihDenda"]);
                t9 += SisaDenda;
                t12 += NilaiLebih;

                if (Tipe == "TENANT")
                    Lunas(rs, i);

                if (i == rs.Rows.Count - 1)
                    SubTotal(t1, t2, t3, t4, t5, t6, t9, t12);
            }
            if (Tipe != "TENANT")
                Lunas(0, DateTime.Today, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        private void Lunas(DataTable rs, int i)
        {
            if (rs.Rows[i]["CaraBayar"].ToString() != "")
            {
                TableRow r = new TableRow();
                TableCell c;

                string dok = "";
                if (!(bool)rs.Rows[i]["SudahCair"])
                    dok = "TTS : " + rs.Rows[i]["NoTTS2"].ToString();
                else
                    dok = "BKM : " + rs.Rows[i]["NoBKM2"].ToString();

                c = new TableCell();
                c.Text = dok;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["CaraBayar"].ToString();
                if (rs.Rows[i]["KetBayar"].ToString().Trim() != "")
                    c.Text = c.Text + " / " + rs.Rows[i]["KetBayar"];
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglBayar"]);
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Pelunasan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                Rpt.Border(r);
                r.Cells[0].Attributes["style"] = r.Cells[0].Attributes["style"] + ";padding-left:15";
                r.Cells[1].Attributes["style"] = r.Cells[1].Attributes["style"] + ";padding-left:20";
                r.Cells[2].Attributes["style"] = r.Cells[2].Attributes["style"] + ";padding-left:20";
                rpt.Rows.Add(r);
            }
        }

        private decimal Lunas(int NoTagihan, DateTime TglJT, decimal SisaTagihan, decimal NilaiDenda, decimal DendaReal, decimal PutihDenda, decimal Benefit, decimal BenefitReal, decimal AlokasiBenefit, decimal Lebih)
        {
            string strSql = "SELECT *"
                + " FROM " + Tb + "..MS_PELUNASAN"
                + " WHERE NoKontrak = '" + Ref + "' AND NoTagihan = " + NoTagihan
                + " ORDER BY NoUrut";

            decimal t = 0, Denda = 0, SubTotalDenda = 0;
            decimal SubTotalPutihDenda = 0;
            decimal TotalPutihDenda = 0;
            decimal SubTotalDendaReal = 0;
            decimal TotalDendaReal = 0;
            decimal SubTotalBenefit = 0;
            decimal TotalBenefit = 0;
            decimal SubTotalBenefitReal = 0;
            decimal TotalBenefitReal = 0;
            decimal SubTotalAlokasiBenefit = 0;
            decimal TotalAlokasiBenefit = 0;
            decimal TotalLebih = 0;

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                if (NoTagihan == 0 && i == 0)
                {
                    TableRow r1 = new TableRow();
                    TableCell c1 = new TableCell();

                    c1.Text = "<b>PELUNASAN TIDAK TERALOKASI</b>";
                    c1.ForeColor = Color.Red;
                    c1.ColumnSpan = 7;
                    r1.Cells.Add(c1);
                    rpt.Rows.Add(r1);
                }

                TableRow r = new TableRow();
                TableCell c;

                if (Convert.ToInt32(rs.Rows[i]["NoTTS"]) == 0)
                {
                    string dok = "";
                    //if ((bool)rs.Rows[i]["SudahCair"])
                    dok = "MEMO : " + rs.Rows[i]["NoMEMO"];

                    c = new TableCell();
                    c.Text = dok;
                    c.Wrap = false;
                    r.Cells.Add(c);
                }
                else
                {
                    string dok = "";
                    if (!(bool)rs.Rows[i]["SudahCair"])
                        dok = "TTS : " + rs.Rows[i]["NoTTS2"].ToString();
                    else
                        dok = "BKM : " + rs.Rows[i]["NoBKM2"].ToString();

                    c = new TableCell();
                    c.Text = dok;
                    c.Wrap = false;
                    r.Cells.Add(c);
                }

                c = new TableCell();
                c.Text = rs.Rows[i]["CaraBayar"].ToString();
                if (rs.Rows[i]["Ket"].ToString().Trim() != "")
                    c.Text = c.Text + " / " + rs.Rows[i]["Ket"];
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglPelunasan"]);
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiPelunasan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = ""; //Cf.Num(Math.Round(NilaiDenda, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";//Cf.Num(Math.Round(DendaReal, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";//Cf.Num(Math.Round(PutihDenda, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);
                Rpt.Border(r);
                r.Cells[0].Attributes["style"] = r.Cells[0].Attributes["style"] + ";padding-left:15";
                r.Cells[1].Attributes["style"] = r.Cells[1].Attributes["style"] + ";padding-left:20";
                r.Cells[2].Attributes["style"] = r.Cells[2].Attributes["style"] + ";padding-left:20";
                rpt.Rows.Add(r);

                t = t + (decimal)rs.Rows[i]["NilaiPelunasan"];

                SubTotalPutihDenda = PutihDenda;
                TotalPutihDenda += PutihDenda;
                SubTotalBenefit = Benefit;
                TotalBenefit += Benefit;
                SubTotalBenefitReal = BenefitReal;
                TotalBenefitReal += BenefitReal;
                SubTotalAlokasiBenefit = AlokasiBenefit;
                TotalAlokasiBenefit += AlokasiBenefit;
                SubTotalDendaReal = DendaReal;
                TotalDendaReal += DendaReal;
                SubTotalDenda = NilaiDenda;
                TotalDenda = NilaiDenda;
                TotalLebih = Lebih;

                if (i == (rs.Rows.Count - 1))
                    SubDenda(SubTotalDenda, SubTotalDendaReal, SubTotalPutihDenda, SubTotalBenefit, SubTotalBenefitReal, SubTotalAlokasiBenefit, TotalLebih);
            }

            return t;
        }

        private void SubDenda(decimal TotalDenda, decimal TotalDendaReal, decimal TotalPutihDenda, decimal TotalBenefit, decimal TotalBenefitReal, decimal TotalAlokasiBenefit, decimal TotalLebih)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = "<strong>TOTAL</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            c.ColumnSpan = 6;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<strong>" + Cf.Num(Math.Round(TotalDenda)) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<strong>" + Cf.Num(Math.Round(TotalDendaReal)) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<strong>" + Cf.Num(Math.Round(TotalPutihDenda)) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<strong>" + Cf.Num(Math.Round(TotalDenda - TotalDendaReal - TotalPutihDenda)) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<strong>" + Cf.Num(Math.Round(TotalLebih)) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);
            rpt.Rows.Add(r);
        }

        private void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t9, decimal t12)
        {
            TableRow r = new TableRow();
            TableCell c;

            //c = new TableCell();
            //c.Text = "<br>";
            //r.Cells.Add(c);
            //rpt.Rows.Add(r);

            r = new TableRow();
            c = new TableCell();
            c.ColumnSpan = 3;
            c.Text = "<b>GRAND TOTAL</b>";
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(t3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(t6);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(t9);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(t12);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);
            rpt.Rows.Add(r);

            //claim
            r = new TableRow();
            c = new TableCell();
            c.ColumnSpan = 8;
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.ColumnSpan = 2;
            c.Text = "Claim";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            decimal Claim = Db.SingleDecimal("SELECT ISNULL(SUM(BankKeluar),0) FROM MS_CASHBACK WHERE NoKontrak = '" + Ref + "' AND Tipe = 0");
            c.Text = Cf.Num(Claim);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);

            //alokasi lebih Bayar
            r = new TableRow();
            c = new TableCell();
            c.ColumnSpan = 8;
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.ColumnSpan = 2;
            c.Text = "Alokasi";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            decimal Alokasi = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM MS_MEMO WHERE Ref = '" + Ref + "' AND CaraBayar = 'AL'");
            c.Text = Cf.Num(Alokasi);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);
            rpt.Rows.Add(r);

            //memo pendapatan(diakui pendapatan sendiri)
            r = new TableRow();
            c = new TableCell();
            c.ColumnSpan = 8;
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.ColumnSpan = 2;
            c.Text = "Memo Pendapatan";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            decimal MemoPendapatan = Db.SingleDecimal("SELECT ISNULL(SUM(BankKeluar),0) FROM MS_CASHBACK WHERE NoKontrak = '" + Ref + "' AND Tipe = 1");
            c.Text = Cf.Num(MemoPendapatan);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);
            rpt.Rows.Add(r);

            //sisa Lebih bayar
            r = new TableRow();
            c = new TableCell();
            c.ColumnSpan = 8;
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.ColumnSpan = 2;
            c.Text = "Sisa Lebih Bayar";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(t12 - Claim - Alokasi - MemoPendapatan);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);
            rpt.Rows.Add(r);

        }

        private string Tb
        {
            get
            {
                return Sc.MktTb(Tipe);
            }
        }

        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }

        private string Ref
        {
            get
            {
                return Cf.Pk(Request.QueryString["Ref"]);
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
