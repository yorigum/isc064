using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class KalkulatorSkema2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                noskema.Visible = false;
                hasil.Visible = false;

                InitForm(); //javascript
                Bind(); //ddl cara bayar
                Fill(); //querystring

                if (pilih.Visible)
                {
                    SetHarga(); //perhitungan price list
                    Js.Focus(this, hitung);
                }
            }
        }

        private void InitForm()
        {
            carabayar.Attributes["onchange"] = "load()";

            gross.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempx=this.value;";
            gross.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            gross.Attributes["onblur"] = "CalcBlur(this);"
                + "if(this.value!=tempx){load()}";

            nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempx=this.value";
            nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);"
                + "if(this.value!=tempx){document.getElementById('disc').value=''}";
            nilai.Attributes["onblur"] = "CalcBlur(this);";


            disc.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempdisc=this.value;"
                + "nohitung();";
            disc.Attributes["onblur"] = "if(this.value!=tempdisc){"
                + "recaldisc( "
                + "		document.getElementById('gross')"
                + "		,document.getElementById('disc')"
                + "		,document.getElementById('nilai')"
                + ");"
                + "}"
                + "okhitung();"
                ;

            nostock.Text = NoStock;
            nounit.Text = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock ='" + NoStock + "'");
        }

        private void Bind()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_UNIT where Nostock = '" + NoStock + "'");
            bool SifatPPN = Convert.ToBoolean(rs.Rows[0]["SifatPPN"]);
            string Jenis = Cf.Str(rs.Rows[0]["JenisProperti"]);
            string JenisProperti = rs.Rows[0]["JenisProperti"].ToString();
            DataTable rsCB = Db.Rs("SELECT * FROM REF_SKEMA a WHERE a.Status='A' AND Project IN (" + Act.ProjectListSql + ")");
                for (int aa = 0; aa < rsCB.Rows.Count; aa++)
                {
                    carabayar.Items.Add(new ListItem(rsCB.Rows[aa]["Nama"].ToString(), rsCB.Rows[aa]["Nomor"].ToString()));
                }
            if (rs.Rows.Count == 0)
            {
                pilih.Visible = false;
                noskema.Visible = true;
            }
        }

        protected void carabayar_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal pl = Db.SingleDecimal("SELECT ISNULL(PriceList, 0) FROM MS_PRICELIST"
               + " WHERE NoStock = '" + NoStock + "' AND NoSkema = " + carabayar.SelectedValue);

            gross.Text = Cf.Num(Math.Round(pl));

            string RumusDiskon = "";
            RumusDiskon = Db.SingleString(
                     "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + carabayar.SelectedValue);

            string[] x = RumusDiskon.Split('+');

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != "")
                {
                    decimal y = Convert.ToDecimal(x[i]) * (decimal)-1;
                    if (i < (x.Length - 1))
                        sb.Append(y.ToString() + "+");
                    else
                        sb.Append(y.ToString());
                }
            }

            disc.Text = sb.ToString();

            decimal diskon = Func.NominalDiskon(RumusDiskon, pl);
            if (diskon == 0)
            {
                nilaiDiskon.Text = "0";
            }
            else
            {
                nilaiDiskon.Text = Cf.Num(Math.Round(diskon, 0).ToString());
            }

            string RumusBunga = Db.SingleString(
                "SELECT Bunga FROM REF_SKEMA WHERE Nomor = " + carabayar.SelectedValue);


            string[] x2 = RumusBunga.Split('+');

            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();

            for (int i = 0; i < x2.Length; i++)
            {
                if (x2[i] != "")
                {
                    decimal y = Convert.ToDecimal(x2[i]) * (decimal)-1;
                    if (i < (x2.Length - 1))
                        sb2.Append(y.ToString() + "+");
                    else
                        sb2.Append(y.ToString());
                }
            }
            persenbunga.Text = sb2.ToString();
            decimal bunga = Func.NominalBunga2(RumusBunga, pl);
            if (bunga == 0)
            {
                nilaiBunga.Text = "0";
            }
            else
            {
                nilaiBunga.Text = Cf.Num(Math.Round(bunga, 0).ToString());
            }

            decimal ndpp = (pl + Convert.ToDecimal(nilaiDiskon.Text) - Convert.ToDecimal(nilaiBunga.Text)) / (decimal)1.1;
            decimal nppn = ndpp * (decimal)0.1;

            dpp.Text = Cf.Num(ndpp);
            ppn.Text = Cf.Num(nppn);
            nilai.Text = Cf.Num(ndpp + nppn);
        }


        private void Fill()
        {
            try
            {
                DateTime tgl = DateTime.Today;
                tglkontrak.Text = Cf.Day(tgl);
            }
            catch
            {
                tglkontrak.Text = Cf.Day(DateTime.Today);
            }
            decimal pl = 0;
            if (Request.QueryString["CB"] != null)
            {
                pl = Db.SingleDecimal("SELECT ISNULL(PriceList,0) FROM MS_PRICELIST WHERE NoStock = '" + NoStock + "'");
            }
            gross.Text = Cf.Num(Math.Round(pl));

        }

        private void SetHarga()
        {
            decimal Gross = Convert.ToDecimal(gross.Text);

            int NomorSkema = 0;

            string NoUnit = "";
            if (Request.QueryString["nounit"] != null)
            {
                NoUnit = Request.QueryString["nounit"].ToString();

            }

            //DataTable rs = Db.Rs("SELECT * FROM MS_UNIT where Nostock = '" + NoStock + "'");
            ////string tipeU = Db.SingleString("SELECT Jenis FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            ////rusun ppn  2
            ////    rusun tanpa ppn 1 dan 2
            ////    apartemen 3 4 5 6
            //string Jenis = Cf.Str(rs.Rows[0]["JenisProperti"]);
            //bool SifatPPN = Convert.ToBoolean(rs.Rows[0]["SifatPPN"]);
            //string JenisProperti = rs.Rows[0]["JenisProperti"].ToString();
            //if (Jenis == "RUSUNAMI" && SifatPPN == true)
            //{
            //    DataTable rsCB = Db.Rs("SELECT * FROM REF_SKEMA a WHERE a.Status='A' AND (Nomor=2)");
            //    // DataTable rsPL = Db.Rs("SELECT * FROM MS_PRICELIST WHERE NoStock='" + NoStock + "'  AND (NoSkema=2)");

            //    carabayar.Items.Add(new ListItem("Pilih Cara Bayar", ""));
            //    for (int aa = 0; aa < rsCB.Rows.Count; aa++)
            //    {
            //        carabayar.Items.Add(new ListItem(rsCB.Rows[aa]["Nama"].ToString(), rsCB.Rows[aa]["Nomor"].ToString()));
            //    }
            //}
            //else if (Jenis == "RUSUNAMI" && SifatPPN == false)
            //{
            //    DataTable rsCB = Db.Rs("SELECT * FROM REF_SKEMA a WHERE a.Status='A' AND (Nomor=2 OR Nomor=1)");
            //    //DataTable rsPL = Db.Rs("SELECT * FROM MS_PRICELIST WHERE NoStock='" + NoStock + "'  AND (NoSkema=2 OR NoSkema=1)");

            //    carabayar.Items.Add(new ListItem("Pilih Cara Bayar", ""));
            //    for (int aa = 0; aa < rsCB.Rows.Count; aa++)
            //    {
            //        carabayar.Items.Add(new ListItem(rsCB.Rows[aa]["Nama"].ToString(), rsCB.Rows[aa]["Nomor"].ToString()));
            //    }
            //}
            //else if (Jenis == "APARTEMEN")
            //{
            //    //DataTable rsPL = Db.Rs("SELECT * FROM MS_PRICELIST WHERE NoStock='" + NoStock + "' AND (NoSkema=3 OR NoSkema=4 OR NoSkema=5 OR NoSkema=6)");
            //    DataTable rsCB = Db.Rs("SELECT * FROM REF_SKEMA a WHERE a.Status='A' AND (Nomor=3 OR Nomor=4 OR Nomor=5 OR Nomor=6)");
            //    carabayar.Items.Add(new ListItem("Pilih Cara Bayar", ""));
            //    for (int aa = 0; aa < rsCB.Rows.Count; aa++)
            //    {
            //        carabayar.Items.Add(new ListItem(rsCB.Rows[aa]["Nama"].ToString(), rsCB.Rows[aa]["Nomor"].ToString()));
            //    }
            //}

            carabayar.SelectedValue = CaraBayar;
            string RumusDiskon = "";
            RumusDiskon = Db.SingleString(
                     "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + CaraBayar);
            string[] x = RumusDiskon.Split('+');

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != "")
                {
                    decimal y = Convert.ToDecimal(x[i]) * (decimal)-1;
                    if (i < (x.Length - 1))
                        sb.Append(y.ToString() + "+");
                    else
                        sb.Append(y.ToString());
                }
            }

            disc.Text = sb.ToString();

            decimal netto = Func.SetelahDiskon2(RumusDiskon, Gross);

            decimal diskon = Func.NominalDiskon(RumusDiskon, Gross);
            if (diskon == 0)
            {
                nilaiDiskon.Text = "0";
            }
            else
            {
                nilaiDiskon.Text = Cf.Num(Math.Round(diskon, 0).ToString());
            }

            decimal ndpp = (Gross + Convert.ToDecimal(nilaiDiskon.Text)) / (decimal)1.1;
            decimal nppn = ndpp * (decimal)0.1;

            dpp.Text = Cf.Num(ndpp);
            ppn.Text = Cf.Num(nppn);
            nilai.Text = Cf.Num(ndpp + nppn);
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tglkontrak))
            {
                x = false;
                if (s == "") s = tglkontrak.ID;
                tglkontrakc.Text = "Tanggal";
            }
            else
                tglkontrakc.Text = "";

            if (!Cf.isMoney(nilai))
            {
                x = false;
                if (s == "") s = nilai.ID;
                nilaic.Text = "Angka";
            }
            else
                nilaic.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        protected void hitung_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                pilih.Visible = false;
                hasil.Visible = true;

                Jadwal();
            }
        }

        private void Jadwal()
        {
            DateTime tgl = Convert.ToDateTime(tglkontrak.Text);
            decimal netto = Convert.ToDecimal(nilai.Text);

            string[,] x = Func.Breakdown(
                Convert.ToInt32(carabayar.SelectedValue), netto, tgl);

            decimal t = 0;
            for (int i = 0; i <= x.GetUpperBound(0); i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = x[i, 0] + ".";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = x[i, 2];
                r.Cells.Add(c);

                //jadwal
                c = new TableCell();
                c.Text = x[i, 3];
                r.Cells.Add(c);

                //nominal
                c = new TableCell();
                c.Text = Cf.Num(x[i, 4]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                t = t + Convert.ToDecimal(x[i, 4]);

                Rpt.Border(r);
                rpt.Rows.Add(r);

                if (i == x.GetUpperBound(0))
                {
                    r = new TableRow();

                    c = new TableCell();
                    c.Text = "Grand Total";
                    c.ColumnSpan = 2;
                    c.Font.Bold = true;
                    c.Font.Size = 10;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(t);
                    c.ColumnSpan = 2;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Font.Bold = true;
                    c.Font.Size = 10;
                    r.Cells.Add(c);

                    rpt.Rows.Add(r);
                }
            }
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }
        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }
        private string CaraBayar
        {
            get
            {
                return Cf.Pk(Request.QueryString["CB"]);
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            //Response.Redirect("UnitPilih1.aspx?NoNUP=" + NoNUP + "&NoStock="+NoStock+"&Tipe="+Tipe+"&CB=" + CaraBayar+ "");
            Js.Close(this);
        }
    }
}
