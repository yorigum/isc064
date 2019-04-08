namespace ISC064.LAUNCHING
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;

    public partial class PrintJadwalTagihanTemplate : System.Web.UI.UserControl
    {
        public string nomor;
        public string NoKontrak
        {
            set { nomor = value; }
        }

        public string proj;
        public string Project
        {
            set { proj = value; }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Fill();
        }

        protected void Fill()
        {
            string strSql = "SELECT a.*, b.Nama AS Cs"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " WHERE a.NoKontrak = '" + nomor + "'"
                ;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count > 0)
            {
                nokontrak.Text = rs.Rows[0]["NoKontrak"].ToString();
                namacs.Text = namacs2.Text = rs.Rows[0]["Cs"].ToString();
                carabayar.Text = Db.SingleString("select ISNULL(Nama, '') from REF_SKEMA where Nomor = '" + rs.Rows[0]["Refskema"] + "'");
                dpp.Text = "Rp. " + Cf.Num(rs.Rows[0]["NilaiKontrak"]) + ",-";

                if (Convert.ToDecimal(rs.Rows[0]["DiskonRupiah"]) != 0)
                {
                    trdiskon.Visible = true;
                }
                else
                {
                    trdiskon.Visible = false;
                }

                diskon.Text = Cf.Num(rs.Rows[0]["DiskonRupiah"]);
                total.Text = Cf.Num((decimal)rs.Rows[0]["NilaiKontrak"] - (decimal)rs.Rows[0]["DiskonRupiah"]);

                bank.Text = "Bank Sinarmas";
                atasnama.Text = "PT Panahome Deltamas Indonesia";
                nova.Text = rs.Rows[0]["Nova"].ToString();

                tglkontrak.Text = Cf.DayIndo(rs.Rows[0]["TglKontrak"]);

                //fill data unit
                int CountUnit = Db.SingleInteger("select count(*) from MS_UNIT where NoStock = '" + rs.Rows[0]["NoStock"] + "'");
                if (CountUnit != 0)
                {
                    string strSqlUnit = "";
                    strSqlUnit = "SELECT * "
                        + " FROM MS_UNIT WHERE NoStock = '" + rs.Rows[0]["NoStock"] + "'";
                    DataTable rsUnit = Db.Rs(strSqlUnit);
                    for (int k = 0; k < rsUnit.Rows.Count; k++)
                    {
                        namajalan.Text = rsUnit.Rows[k]["NamaJalan"].ToString();
                        nounit.Text = rsUnit.Rows[k]["Nomor"].ToString().PadLeft(2, '0');
                    }
                }

                FillTb();
            }
        }

        protected void FillTb()
        {
            string strSql = "SELECT "
                + " NamaTagihan"
                + ",TglJT"
                + ",NilaiTagihan"
                + ",NoUrut"
                + ",Tipe"
                + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + nomor + "') ) AS SisaTagihan"
                + " FROM MS_TAGIHAN"
                + " WHERE NoKontrak = '" + nomor + "' and Tipe != 'ADM'"
                + " ORDER BY NoUrut";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Daftar tagihan untuk kontrak tersebut masih kosong.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUrut"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaTagihan"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = ":";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "Rp. ";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "&nbsp;";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.DayIndoShort(rs.Rows[i]["TglJT"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "&nbsp;";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                Rpt.Border(r);
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
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}
