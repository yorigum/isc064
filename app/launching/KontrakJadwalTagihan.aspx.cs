using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class KontrakJadwalTagihan : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
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

        private void Fill()
        {
            edit.Attributes["onclick"] = "location.href='TagihanEdit.aspx?NoKontrak=" + NoKontrak + "'";

            string strSql = "SELECT "
                + " (SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('BF','DP','ANG')) AS TotalTagihan"
                + ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('ADM')) AS TotalBiaya"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan <> 0 AND SudahCair = 1) AS TotalPelunasan"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan <> 0) AS TotalPembayaran"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan = 0) AS Unallocated"
                + ",PersenLunas"
                + ",NilaiKontrak"
                + ",OutBalance"
                + ",Skema"
                + " FROM MS_KONTRAK"
                + " WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nilai.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]), 0));
                totaltagihan.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["TotalTagihan"]), 0));
                totalbiaya.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["TotalBiaya"]), 0));
                tagihanbiaya.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["TotalTagihan"]) + Convert.ToDecimal(rs.Rows[0]["TotalBiaya"]), 0));
                outofbalance.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["OutBalance"]), 0));
                if (outofbalance.Text == "0") outtr.Visible = false;

                pelunasan.Text = Cf.Num(rs.Rows[0]["TotalPelunasan"]);
                pembayaran.Text = Cf.Num(rs.Rows[0]["TotalPembayaran"]);
                unallocated.Text = Cf.Num(rs.Rows[0]["Unallocated"]);
                if (unallocated.Text == "0") unatr.Visible = false;
                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);

                skema.Text = rs.Rows[0]["Skema"].ToString();

                FillTb();
            }
        }

        private void FillTb()
        {
            string strSql = "SELECT "
                + " NamaTagihan"
                + ",TglJT"
                + ",NilaiTagihan"
                + ",NoUrut"
                + ",Tipe"
                + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + NoKontrak + "') ) AS SisaTagihan"
                + " FROM MS_TAGIHAN"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " ORDER BY NoUrut";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Daftar tagihan untuk kontrak tersebut masih kosong.");

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                t1 = t1 + (decimal)rs.Rows[i]["NilaiTagihan"];
                t2 = t2 + (decimal)rs.Rows[i]["SisaTagihan"];

                c = new TableCell();
                c.Text = NoKontrak + "." + rs.Rows[i]["NoUrut"];
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Tipe"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaTagihan"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglJT"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]),0 ));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["SisaTagihan"]), 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);

                t3 = t3 + Lunas((int)rs.Rows[i]["NoUrut"]);

                if (i == rs.Rows.Count - 1)
                    SubTotal(t1, t2, t3);
            }

            Lunas(0);
        }

        private decimal Lunas(int NoTagihan)
        {
            string strSql = "SELECT "
                + " CaraBayar"
                + ",TglPelunasan"
                + ",Ket"
                + ",NilaiPelunasan"
                + ",NoUrut"
                + ",SudahCair"
                + " FROM MS_PELUNASAN"
                + " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + NoTagihan
                + " ORDER BY NoUrut";

            decimal t = 0;

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

                string sudahcair = "";
                if (!(bool)rs.Rows[i]["SudahCair"])
                    sudahcair = " <u style='color:orange'>BELUM CAIR</u>";

                c = new TableCell();
                c.ColumnSpan = 4;
                c.Text = rs.Rows[i]["CaraBayar"]
                    + ", " + Cf.Day(rs.Rows[i]["TglPelunasan"])
                    + " " + rs.Rows[i]["Ket"]
                    + sudahcair;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiPelunasan"]), 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                Rpt.Border(r);
                r.Cells[0].Attributes["style"] = r.Cells[0].Attributes["style"] + ";padding-left:40";
                rpt.Rows.Add(r);

                t = t + Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiPelunasan"]), 0);
            }

            return t;
        }

        private void SubTotal(decimal t1, decimal t2, decimal t3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.ColumnSpan = 4;
            c.Text = "<b>GRAND TOTAL</b>";
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(Math.Round(Convert.ToDecimal(t1), 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(Math.Round(Convert.ToDecimal(t3), 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(Math.Round(Convert.ToDecimal(t2), 0 ));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
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
