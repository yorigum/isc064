namespace ISC064.COLLECTION
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;

    public partial class PrintSTTemplate : System.Web.UI.UserControl
    {
        protected System.Web.UI.WebControls.Label answer;

        //Passing parameter
        public string nomor;
        public string proj;
        public string NoTunggakan
        {
            set { nomor = value; }
        }
        public string Project
        {
            set { proj = value; }
        }
        private string Halaman { get { return "SuratPeringatan"; } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //content.InnerHtml = He.Template(Halaman, nomor, proj);
            Fill();
        }

        private void Fill()
        {
            decimal NilaiTunggakan = 0;
            decimal TotalNilaiTunggakan = 0;
            decimal NilaiPelunasan = 0;
            decimal TotalNilaiPelunasan = 0;
            decimal TotalDendaHari = 0;
            decimal TotalDenda = 0;
            decimal TotalAll = 0;

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN WHERE NoTunggakan = " + nomor;
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count > 0)
            {
                lbltgl.Text = Cf.Day(DateTime.Now.AddDays(0));

                lblnama.Text = Cf.Str(rs.Rows[0]["Customer"]);
                lblalamat.Text = Cf.Str(rs.Rows[0]["Alamat1"]) + "<br /> "
                    + Cf.Str(rs.Rows[0]["Alamat2"]) + "<br /> "
                    + Cf.Str(rs.Rows[0]["Alamat3"]);

                DataTable rsUnit = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoStock = (SELECT NoStock FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[0]["Ref"].ToString() + "')");
                if (rsUnit.Rows.Count > 0)
                {
                    lblnounitalmt.Text = Cf.Str(rsUnit.Rows[0]["Lokasi"])
                    + ", " + Cf.Str(rsUnit.Rows[0]["NamaJalan"]) 
                    + ", " + Cf.Str(rsUnit.Rows[0]["Jenis"])
                    + " " + Cf.Num(rsUnit.Rows[0]["LuasNett"])
                    + "/" + Cf.Num(rsUnit.Rows[0]["LuasSG"]);
                }

                string LvlTg = rs.Rows[0]["LevelTunggakan"].ToString();
                if (LvlTg == "1")
                    lblperingatan.Text = "Peringatan I";
                else if (LvlTg == "2")
                    lblperingatan.Text = "Peringatan II";
                else if (LvlTg == "3")
                    lblperingatan.Text = "Peringatan III";
                else
                    lblperingatan.Text = "Somasi";

                if (LvlTg == "1" || LvlTg == "2" || LvlTg == "3")
                {
                    TableBot1.Visible = true;
                    TableBot2.Visible = false;
                }
                else
                {
                    TableBot1.Visible = false;
                    TableBot2.Visible = true;
                }

                //string strSql2 = "SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN_DETIL WHERE NoTunggakan = " + nomor;

                string strSql2 = "SELECT a.*, b.Ref FROM ISC064_FINANCEAR..MS_TUNGGAKAN_DETIL a "
                        + " INNER JOIN ISC064_FINANCEAR..MS_TUNGGAKAN b on a.NoTunggakan = b.NoTunggakan "
                        + " WHERE a.NoTunggakan = " + nomor;

                DataTable rsTagihan = Db.Rs(strSql2);

                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    string Garis = "border-top: 1px solid black; "
                        + "border-right: 1px solid black; "
                        + "border-bottom: 1px solid black; "
                        + "border-left: 1px solid black; ";

                    TableRow r = new TableRow();
                    TableCell c;

                    c = new TableCell();
                    c.Text = Cf.Str(rsTagihan.Rows[i]["NamaTagihan"]);
                    c.Wrap = false;
                    c.Attributes.Add("style", Garis);
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rsTagihan.Rows[i]["Nilai"]);
                    NilaiTunggakan = Convert.ToDecimal(c.Text);
                    TotalNilaiTunggakan += Convert.ToDecimal(c.Text);
                    c.Wrap = false;
                    c.Attributes.Add("style", Garis);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Day(rsTagihan.Rows[i]["TglJt"]);
                    c.Wrap = false;
                    c.Attributes.Add("style", Garis);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Db.SingleDecimal("SELECT IsNull(NilaiPelunasan, 0) FROM "
                        + " ISC064_MARKETINGJUAL..MS_PELUNASAN "
                        + " WHERE NoTagihan = " + rsTagihan.Rows[i]["NoTagihan"]
                        + " AND NoKontrak = '" + rsTagihan.Rows[i]["Ref"].ToString() + "'").ToString();
                    NilaiPelunasan = Convert.ToDecimal(c.Text);
                    TotalNilaiPelunasan += Convert.ToDecimal(c.Text);
                    c.Wrap = false;
                    c.Attributes.Add("style", Garis);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    int ada = Db.SingleInteger("SELECT Count(*) FROM "
                        + " ISC064_MARKETINGJUAL..MS_PELUNASAN "
                        + " WHERE NoTagihan = " + rsTagihan.Rows[i]["NoTagihan"]
                        + " AND NoKontrak = '" + rsTagihan.Rows[i]["Ref"].ToString() + "'");
                    if (ada > 0)
                    {
                        c.Text = Cf.Day(Db.SingleTime("SELECT IsNull(TglPelunasan, '-') FROM "
                        + " ISC064_MARKETINGJUAL..MS_PELUNASAN "
                        + " WHERE NoTagihan = " + rsTagihan.Rows[i]["NoTagihan"]
                        + " AND NoKontrak = '" + rsTagihan.Rows[i]["Ref"].ToString() + "'"));
                    }
                    c.Wrap = false;
                    c.Attributes.Add("style", Garis);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rsTagihan.Rows[i]["Telat"]);
                    TotalDendaHari += Convert.ToDecimal(c.Text);
                    c.Wrap = false;
                    c.Attributes.Add("style", Garis);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rsTagihan.Rows[i]["Denda"]);
                    TotalDenda += Convert.ToDecimal(c.Text);
                    c.Wrap = false;
                    c.Attributes.Add("style", Garis);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(NilaiTunggakan - NilaiPelunasan + Convert.ToDecimal(rsTagihan.Rows[i]["Denda"]));
                    TotalAll += Convert.ToDecimal(c.Text);
                    c.Wrap = false;
                    c.Attributes.Add("style", Garis);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    TableTag.Rows.Add(r);
                }

                SubTotal(TotalNilaiTunggakan, TotalNilaiPelunasan, TotalDendaHari, TotalDenda, TotalAll);
            }
        }

        private void SubTotal(decimal TotalNilaiTunggakan, decimal TotalNilaiPelunasan, decimal TotalDendaHari, decimal TotalDenda, decimal TotalAll)
        {
            string Garis = "border-top: 1px solid black; "
                        + "border-right: 1px solid black; "
                        + "border-bottom: 1px solid black; "
                        + "border-left: 1px solid black; ";

            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = "Jumlah Keseluruhan";
            c.Wrap = false;
            c.Attributes.Add("style", Garis);
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(TotalNilaiTunggakan);
            c.Wrap = false;
            c.Attributes.Add("style", Garis);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "";
            c.Wrap = false;
            c.Attributes.Add("style", Garis);
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(TotalNilaiPelunasan);
            c.Wrap = false;
            c.Attributes.Add("style", Garis);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "";
            c.Wrap = false;
            c.Attributes.Add("style", Garis);
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(TotalDendaHari);
            c.Wrap = false;
            c.Attributes.Add("style", Garis);
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(TotalDenda);
            c.Wrap = false;
            c.Attributes.Add("style", Garis);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(TotalAll);
            c.Wrap = false;
            c.Attributes.Add("style", Garis);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            TableTag.Rows.Add(r);
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
