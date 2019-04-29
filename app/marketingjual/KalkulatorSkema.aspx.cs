using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
namespace ISC064.MARKETINGJUAL
{
    public partial class KalkulatorSkema : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                LoadTipeUnit();

                noskema.Visible = false;
                hasil.Visible = false;
                tb2.Visible = false;
                InitForm(); //javascript
                //Bind(); //ddl cara bayar
                Fill(); //querystring                
                if (pilih.Visible)
                {
                    SetHarga(); //perhitungan price list
                    Js.Focus(this, hitung);
                }
            }
            btnpop.Attributes.Add("modal-url", "/marketingjual/DaftarUnit.aspx?calc=1&project=" + project.SelectedValue + "&lokasi=" + lokasi.SelectedValue);
        }
        private void LoadTipeUnit()
        {
            string strSql = "SELECT * FROM REF_LOKASI";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Lokasi"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                lokasi.Items.Add(new ListItem(t, v));
            }
            lokasi.SelectedIndex = 0;
        }

        private void InitForm()
        {
            string ParamID = "PLIncludePPN" + project.SelectedValue;
            inclppn.Text = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'");

            carabayar.Attributes["onchange"] = "load()";

            bfjumlah.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            bfjumlah.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            bfjumlah.Attributes["onblur"] = "CalcBlur(this);";

            dpjumlah.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            dpjumlah.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            dpjumlah.Attributes["onblur"] = "CalcBlur(this);";

            angjumlah.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            angjumlah.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            angjumlah.Attributes["onblur"] = "CalcBlur(this);";

            gross.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempx=this.value;";
            gross.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            gross.Attributes["onblur"] = "CalcBlur(this);"
                + "if(this.value!=tempx){load()}";

            nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempx=this.value";
            nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);"
                + "if(this.value!=tempx){document.getElementById('disc').value='';document.getElementById('persenbunga').value=''}";
            nilai.Attributes["onblur"] = "CalcBlur(this);";

            disc.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempdisc=this.value;"
                + "nohitung();";
            disc.Attributes["onblur"] = "if(this.value!=tempdisc){"
                + "recaldisc( "
                //+ "		document.getElementById('inclppn')"
                //+ "		document.getElementById('gross')"
                //+ "		,document.getElementById('disc')"
                //+ "		,document.getElementById('nilai')"
                //+ "		,document.getElementById('nilaiDiskon')"
                //+ "		,document.getElementById('nilaiBunga')"
                + ");"
                + "}"
                + "okhitung();"
                ;

            persenbunga.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempbunga=this.value;"
                + "nohitung();";
            persenbunga.Attributes["onblur"] = "if(this.value!=tempbunga){"
                + "recalbunga( "
                + "		document.getElementById('gross')"
                + "		,document.getElementById('persenbunga')"
                + "		,document.getElementById('nilai')"
                + "		,document.getElementById('nilaiBunga')"
                //+ "		,document.getElementById('inclppn')"
                + ");"
                + "}"
                + "okhitung();"
                ;

            nostock.Text = NoStock;
            nounit.Text = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock ='" + NoStock + "'");
        }

        private void Bind()
        {
            string strAdd = " AND TipeUnit = '" + lokasi.SelectedValue + "'";
            //if (TipeUnit != "")
            //  strAdd += " AND TipeUnit = '" + TipeUnit + "'";
            string Lokasi = Db.SingleString("SELECT Lokasi FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            DataTable rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' AND Project = '" + project.SelectedValue + "'"
                //+ " AND TipeUnit='"+ Lokasi +"'"
                + strAdd);
            carabayar.Items.Add(new ListItem("Pilih Skema", "0"));
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Nomor"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                carabayar.Items.Add(new ListItem(t, v));
            }

            if (rs.Rows.Count == 0)
            {
                pilih.Visible = false;
                noskema.Visible = true;
            }
        }

        protected void carabayar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (valid())
            {
                //decimal pl = Db.SingleDecimal("SELECT ISNULL(PriceList, 0) FROM MS_UNIT"
                //   + " WHERE NoStock = '" + NoStock + "'");

                decimal pl = Db.SingleDecimal("SELECT ISNULL(PriceList, 0) FROM MS_PRICELIST"
                + " WHERE NoStock = '" + NoStock + "' AND NoSkema='" + carabayar.SelectedValue + "'");
                gross.Text = Cf.Num(pl);

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

                //disc.Text = sb.ToString();
                //decimal diskon = Func.NominalDiskon(RumusDiskon, pl);
                //if (diskon == 0)
                //{
                //    nilaiDiskon.Text = "0";
                //}
                //else
                //{
                //    nilaiDiskon.Text = Cf.Num(Math.Round(diskon, 0).ToString());
                //}

                string RumusBunga = Db.SingleString(
                    "SELECT Bunga FROM REF_SKEMA WHERE Nomor = " + carabayar.SelectedValue);


                string[] x2 = RumusBunga.Split('+');

                System.Text.StringBuilder sb2 = new System.Text.StringBuilder();

                for (int i = 0; i < x2.Length; i++)
                {
                    if (x2[i] != "")
                    {
                        decimal y = Convert.ToDecimal(x2[i]) * (decimal)1;
                        if (i < (x2.Length - 1))
                            sb2.Append(y.ToString() + "+");
                        else
                            sb2.Append(y.ToString());
                    }
                }

                //persenbunga.Text = sb2.ToString();
                //decimal bunga = Func.NominalDiskon(RumusBunga, pl);
                //if (bunga == 0)
                //{
                //    nilaiBunga.Text = "0";
                //}
                //else
                //{
                //    nilaiBunga.Text = Cf.Num(Math.Round(bunga, 0).ToString());
                //}

                persenbunga.Text = sb2.ToString();
                decimal bunga = Func.NominalDiskon(RumusBunga, pl);
                if (bunga == 0)
                {
                    nilaiBunga.Text = "0";
                }
                else
                {
                    nilaiBunga.Text = Cf.Num(Math.Round(bunga, 0).ToString());
                }

                disc.Text = sb.ToString();
                decimal diskon = Func.NominalDiskon(RumusDiskon, pl + Convert.ToDecimal(nilaiBunga.Text));
                if (diskon == 0)
                {
                    nilaiDiskon.Text = "0";
                }
                else
                {
                    nilaiDiskon.Text = Cf.Num(Math.Round(diskon, 0).ToString());
                }

                decimal ndpp = 0, nppn = 0;
                string ParamID = "PLIncludePPN" + project.SelectedValue;

                bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";
                if (includeppn)
                    ndpp = (pl - Convert.ToDecimal(nilaiDiskon.Text) + Convert.ToDecimal(nilaiBunga.Text)) / (decimal)1.1;
                else
                    ndpp = (pl - Convert.ToDecimal(nilaiDiskon.Text) + Convert.ToDecimal(nilaiBunga.Text));

                if (includeppn)
                {
                    nppn = (pl - Convert.ToDecimal(nilaiDiskon.Text) + Convert.ToDecimal(nilaiBunga.Text)) - ndpp;
                }
                else
                {
                    nppn = (ndpp * (decimal)0.1);
                }

                dpp.Text = Cf.Num(Math.Round(ndpp));
                ppn.Text = Cf.Num(Math.Round(nppn));
                nilai.Text = Cf.Num(RoundThousand(ndpp + nppn));
            }

            if (metode.SelectedValue == "1")
            {

                int BF = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKEMA_DETAIL WHERE Nomor = '" + carabayar.SelectedValue + "' AND Tipe = 'BF'");
                int DP = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKEMA_DETAIL WHERE Nomor = '" + carabayar.SelectedValue + "' AND Tipe = 'DP'");
                int ANG = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKEMA_DETAIL WHERE Nomor = '" + carabayar.SelectedValue + "' AND Tipe = 'ANG'");
                decimal BFJumlah = Db.SingleDecimal("SELECT Nominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + carabayar.SelectedValue + "' AND Tipe = 'BF'");
                decimal DPJumlah = Db.SingleDecimal("SELECT Nominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + carabayar.SelectedValue + "' AND Tipe = 'DP'");
                decimal ANGJumlah = Db.SingleDecimal("SELECT Nominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + carabayar.SelectedValue + "' AND Tipe = 'ANG'");
                string BFTipe = Db.SingleString("SELECT TipeNominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + carabayar.SelectedValue + "' AND Tipe = 'BF'");
                string DPTipe = Db.SingleString("SELECT TipeNominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + carabayar.SelectedValue + "' AND Tipe = 'DP'");
                string ANGTipe = Db.SingleString("SELECT TipeNominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + carabayar.SelectedValue + "' AND Tipe = 'ANG'");

                bfkali.Text = BF.ToString();
                dpkali.Text = DP.ToString();
                angkali.Text = ANG.ToString();
                bfjumlah.Text = Cf.Num(Math.Round(BFJumlah * BF));
                dpjumlah.Text = Cf.Num(Math.Round(DPJumlah * DP));
                angjumlah.Text = Cf.Num(Math.Round(ANGJumlah * ANG));
                bool x = true;
                if (BFTipe == "%")
                {
                    bfpersen.Checked = true;
                }
                else
                {
                    bfpersen.Checked = false;
                    bfrupiah.Checked = true;
                }
                if (DPTipe == "%")
                {
                    dppersen.Checked = true;
                }
                else
                {
                    dppersen.Checked = false;
                    dprupiah.Checked = true;
                }
                if (ANGTipe == "%")
                {
                    angpersen.Checked = true;
                }
                else
                {
                    angpersen.Checked = false;
                    angrupiah.Checked = true;
                }

            }

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

            decimal pl = Db.SingleDecimal("SELECT ISNULL(PriceList,0) FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            gross.Text = Cf.Num(pl);

            //carabayar.Items.Clear();
            //DataTable rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' AND Project = (SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "')");
            //carabayar.Items.Add(new ListItem("Pilih Skema", "0"));
            //for (int i = 0; i < rs.Rows.Count; i++)
            //{
            //    string v = rs.Rows[i]["Nomor"].ToString();
            //    string t = rs.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
            //    carabayar.Items.Add(new ListItem(t, v));
            //}

            string ParamID = "PLIncludePPN" + project.SelectedValue;

            inclppn.Text = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'");


            SetHarga();
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

            string strAdd = " AND TipeUnit = '" + lokasi.SelectedValue + "'";

            NomorSkema = Db.SingleInteger("SELECT TOP 1 Nomor FROM REF_SKEMA WHERE Status='A' AND Project = '" + project.SelectedValue + "' " + strAdd);

            carabayar.Items.Clear();
            DataTable rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' AND Project = '" + project.SelectedValue + "'" + strAdd + " ORDER BY Nama");
            carabayar.Items.Add(new ListItem("Pilih Skema", ""));
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Nomor"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                carabayar.Items.Add(new ListItem(t, v));
            }

            carabayar.SelectedValue = "";
            string RumusDiskon = "";
            RumusDiskon = Db.SingleString(
                     "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + NomorSkema);
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

            string RumusBunga = Db.SingleString(
                    "SELECT Bunga FROM REF_SKEMA WHERE Nomor = " + NomorSkema);
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

            persenbunga.Text = sb2.ToString();
            decimal bunga = Func.NominalDiskon(RumusBunga, Gross);
            if (bunga == 0)
            {
                nilaiBunga.Text = "0";
            }
            else
            {
                nilaiBunga.Text = Cf.Num(Math.Round(bunga, 0).ToString());
            }

            decimal ndpp = 0, nppn = 0;
            bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'PLIncludePPN" + project.SelectedValue + "'") == "True";
            if (includeppn)
                ndpp = (Gross + Convert.ToDecimal(nilaiBunga.Text) - Convert.ToDecimal(nilaiDiskon.Text)) / (decimal)1.1;
            else
                ndpp = (Gross + Convert.ToDecimal(nilaiBunga.Text) - Convert.ToDecimal(nilaiDiskon.Text));

            if (includeppn)
            {
                nppn = netto - ndpp;
            }
            else
            {
                nppn = (ndpp * (decimal)0.1);
            }

            dpp.Text = Cf.Num(Math.Round(ndpp));
            ppn.Text = Cf.Num(Math.Round(nppn));
            nilai.Text = Cf.Num(RoundThousand(ndpp + nppn));
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
            if (carabayar.SelectedIndex == 0 && (metode.SelectedValue == "0" || metode.SelectedValue == "1"))
            {
                x = false;
                if (s == "") s = carabayar.ID;
                carabayarc.Text = "Pilih Skema";
            }
            else
                carabayarc.Text = "";

            if (dpkali.Text == "1" && dpspotong.Checked)
            {
                x = false;
                if (s == "") s = dpkali.ID;
                cc.Text = "Tidak Bisa DP disebar";
            }
            else
            {
                cc.Text = "";
            }

            if (dppersen.Checked && angpersen.Checked)
            {
                if ((Convert.ToDecimal(dpjumlah.Text)) + (Convert.ToDecimal(angjumlah.Text)) != 100)
                {
                    x = false;
                    Js.Alert(this, "Harus 100%", "");
                }
            }

            if (!x && s != "")
            {
                ClientScript.RegisterStartupScript(GetType(), "err"
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        protected void hitung_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                if (metode.SelectedIndex == 0)
                {
                    pilih.Visible = false;
                    hasil.Visible = true;

                    tb1.Visible = true;
                    tb2.Visible = false;
                    Jadwal();
                }

                else if (metode.SelectedIndex == 1)
                {
                    bool x = true;
                    if ((dpkali.Text == "0" && (dp1potong.Checked || dpspotong.Checked)) || (angkali.Text == "0" && (ang1potong.Checked || angspotong.Checked)))
                    {
                        x = false;
                        Js.Alert(this, "Rumus Nilai Tidak Boleh Kosong", "");
                    }
                    else
                    {
                        decimal DP = Convert.ToDecimal(dpjumlah.Text);
                        decimal BF = Convert.ToDecimal(bfjumlah.Text);
                        decimal ANG = Convert.ToDecimal(angjumlah.Text);
                        if (bfpersen.Checked) BF = Convert.ToDecimal(bfjumlah.Text) * Convert.ToDecimal(nilai.Text) / 100;
                        if (dppersen.Checked) DP = Convert.ToDecimal(dpjumlah.Text) * Convert.ToDecimal(nilai.Text) / 100 / Convert.ToDecimal(dpkali.Text);
                        if (angpersen.Checked) ANG = Convert.ToDecimal(angjumlah.Text) * Convert.ToDecimal(nilai.Text) / 100 / Convert.ToDecimal(angkali.Text);

                        if ((DP < BF && dp1potong.Checked) || (ANG < BF && ang1potong.Checked))
                        {
                            x = false;
                            Js.Alert(this, "Penyebaran BF tidak bisa minus.", "");
                        }
                        else
                        {
                            pilih.Visible = false;
                            hasil.Visible = true;

                            tb1.Visible = true;
                            tb2.Visible = true;
                            Isi(true);
                        }
                    }
                }
            }
        }

        private void Jadwal()
        {
            DateTime tgl = Convert.ToDateTime(tglkontrak.Text);
            decimal netto = Convert.ToDecimal(nilai.Text);

            //string[,] x = Func.Breakdown(
            //    Convert.ToInt32(carabayar.SelectedValue), netto, tgl);

            decimal t = 0;
            //for (int i = 0; i <= x.GetUpperBound(0); i++)
            //{
            //    if (!Response.IsClientConnected) break;
            int index = 0;
            var d = Func.ListTagihan(carabayar.SelectedValue, netto, tgl);
            foreach (var rb in d)
            {

                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (index + 1) + ".";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rb.NamaTagihan;
                r.Cells.Add(c);

                //jadwal
                c = new TableCell();
                c.Text = Cf.Day(rb.TglJt);
                r.Cells.Add(c);

                //nominal
                c = new TableCell();
                c.Text = Cf.Num(rb.NilaiTagihan);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                if (rb.PotongBF)
                    c.Text = "MIN";

                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rb.TipeTagihan;
                c.Visible = false;
                r.Cells.Add(c);

                t = t + Convert.ToDecimal(rb.NilaiTagihan);
                index++;
                Rpt.Border(r);
                rpt.Rows.Add(r);
                int CountJum = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM REF_SKEMA_DETAIL WHERE Nomor='" + carabayar.SelectedValue + "'");
                if (index == CountJum)
                {
                    r = new TableRow();

                    c = new TableCell();
                    c.Text = "Grand Total";
                    c.ColumnSpan = 2;
                    c.Font.Bold = true;
                    c.Font.Size = 10;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(d.Sum(p => p.NilaiTagihan));
                    c.ColumnSpan = 2;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Font.Bold = true;
                    c.Font.Size = 10;
                    r.Cells.Add(c);

                    rpt.Rows.Add(r);
                }
            }
        }


        private static decimal RoundUp(decimal input)
        {
            string x = input.ToString();
            string[] arr = x.Split(new char[] { '.' });

            if (arr.Length > 1)
            {
                if (decimal.Parse(arr[1]) > 0)
                {
                    decimal dc = decimal.Parse(arr[0]) + 1;
                    return dc;
                }
                else
                {
                    return decimal.Parse(arr[0]);
                }
            }
            else
            {
                return input;
            }
        }

        private static decimal RoundThousand(decimal input)
        {
            if (input < 1000)
            {
                return 0;
            }
            else
            {
                input = RoundUp(input);
                if ((input % 1000) > 0)
                {
                    input = (input - (input % 1000)) + 0;
                }
                return input;
            }
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(nostock.Text);
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

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("KalkulatorSkema.aspx");
        }

        protected void metode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SetHarga();

            if (metode.SelectedIndex == 0)
            {
                tb1.Visible = true;
                tb2.Visible = false;
                disc.Enabled = nilaiDiskon.Enabled = false;
                persenbunga.Enabled = nilaiBunga.Enabled = false;
            }
            else if (metode.SelectedIndex == 1)
            {
                tb1.Visible = true;
                tb2.Visible = true;
                disc.Enabled = nilaiDiskon.Enabled = true;
                persenbunga.Enabled = nilaiBunga.Enabled = true;

                decimal pl = Db.SingleDecimal("SELECT ISNULL(PriceList,0) FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                gross.Text = Cf.Num(pl);
            }
        }

        private void SetBaris(TextBox Kali, string Tipe, string Nama
            , TextBox interval1, TextBox interval2
            , CheckBox hari1, CheckBox hari2
            , TextBox nominal, CheckBox persen
            )
        {
            decimal Temp = 0;

            int count = Convert.ToInt32(Kali.Text);
            int index = rpt.Rows.Count - 1;

            DateTime Tgl = Convert.ToDateTime(tglkontrak.Text); //tanggal kontrak
            try
            {
                if (Tipe == "BF")
                {
                    Tgl = Convert.ToDateTime(tglkontrak.Text); /*= Convert.ToDateTime(bftgl.Text);*/
                }
                else if (Tipe == "DP")
                {
                    Tgl = Convert.ToDateTime(tglkontrak.Text);
                }
                else if (Tipe == "ANG")
                {
                    Tgl = Convert.ToDateTime(tglkontrak.Text);
                }
                else
                {
                    Tgl = Convert.ToDateTime(rpt.Rows[rpt.Rows.Count - 1].Cells[3].Text);
                }
            }
            catch { }

            for (int i = 0; i < count; i++)
            {
                if (!Response.IsClientConnected) break;

                index++;
                //Response.Write(index);                
                //Response.Write(i);
                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = index.ToString() + ".";
                r.Cells.Add(c);

                //c = new TableCell();
                //c.Text = Tipe;
                //r.Cells.Add(c);

                c = new TableCell();
                //decimal cc = Db.SingleDecimal("SELECT COUNT (*)  FROM REF_SKEMA_DETAIL WHERE Tipe = '" + Tipe + "'"); /*Db.SingleDecimal("SELECT COUNT(*) FROM MS_TAGIHAN WHERE tipe='" + Tipe + "' AND nourut in (select notagihan from ms_pelunasan where NoKontrak = '" + NoStock + "' AND NilaiPelunasan > 0) AND Nokontrak = '" + NoKontrak + "'");*/
                c.Text = Nama + " " + (i + 1);
                r.Cells.Add(c);

                if (i == 0)
                {
                    int interval = Convert.ToInt32(interval2.Text);
                    if (Tipe == "BF")
                    {
                        interval = 0;
                    }
                    else if (Tipe == "DP")
                    {
                        interval = 0;
                    }
                    else if (Tipe == "ANG")
                    {
                        interval = 0;
                    }
                    else
                    {
                        //pertama
                        if (hari2.Checked)
                            Tgl = Tgl.AddDays(interval);
                        else
                            Tgl = Tgl.AddMonths(interval);
                    }
                }
                else
                {
                    int interval = Convert.ToInt32(interval1.Text);
                    //pertama
                    if (hari1.Checked)
                        Tgl = Tgl.AddDays(interval);
                    else
                    {
                        int h = Tgl.Day;
                        //if (Tipe == "BF" && bftgl.Text != "")
                        //{
                        //    h = Convert.ToDateTime(bftgl.Text).Day;
                        //}
                        //else if (Tipe == "DP" && dptgl.Text != "")
                        //{
                        //    h = Convert.ToDateTime(dptgl.Text).Day;
                        //}
                        //else if (Tipe == "ANG" && angtgl.Text != "")
                        //{
                        //    h = Convert.ToDateTime(angtgl.Text).Day;
                        //}
                        Tgl = Tgl.AddMonths(interval);
                        if (h != Tgl.Day && h <= DateTime.DaysInMonth(Tgl.Year, Tgl.Month))
                            Tgl = new DateTime(Tgl.Year, Tgl.Month, h);
                    }
                }

                c = new TableCell();
                c.Text = Cf.Day(Tgl);
                r.Cells.Add(c);

                decimal Nominal = Convert.ToDecimal(nominal.Text);

                bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'PLIncludePPN" + project.SelectedValue + "'") == "True";
                decimal dpp = 0;
                decimal ppn = 0;
                if (includeppn)
                {
                    dpp = Math.Round((Convert.ToDecimal(gross.Text) + Convert.ToDecimal(nilaiBunga.Text) - Convert.ToDecimal(nilaiDiskon.Text)) / Convert.ToDecimal(1.1));
                    ppn = Convert.ToDecimal(gross.Text) + Convert.ToDecimal(nilaiBunga.Text) - Convert.ToDecimal(nilaiDiskon.Text) - dpp;
                }
                else
                {
                    dpp = Math.Round(Convert.ToDecimal(gross.Text) + Convert.ToDecimal(nilaiBunga.Text) - Convert.ToDecimal(nilaiDiskon.Text));
                    ppn = Math.Round(dpp * Convert.ToDecimal(0.1));
                }
                decimal Netto = dpp + ppn;

                if (persen.Checked) Nominal = Netto * (Nominal / 100);

                //if (rounding.Checked)
                //{
                decimal rounded = 0;
                Nominal = RoundThousand(Nominal / count);
                //}
                //else
                //{
                //    Nominal = Math.Round(Nominal / count);
                //}                

                c = new TableCell();
                c.Text = Cf.Num(Nominal);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                if (dp1potong.Checked && i == 0 && Tipe == "DP")
                    c.Text = "MIN";
                if (ang1potong.Checked && i == 0 && Tipe == "ANG")
                    c.Text = "MIN";
                if (dpspotong.Checked && Tipe == "DP")
                    c.Text = "MIN";
                if (angspotong.Checked && Tipe == "ANG")
                    c.Text = "MIN";

                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Tipe;
                c.Visible = false;
                r.Cells.Add(c);

                //Temp += Convert.ToDecimal(rpt.Rows[index].Cells[];

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }

        private void SetBarisPM(TextBox Kali, string Tipe, string Nama, TextBox nominal)
        {
            int count = Convert.ToInt32(Kali.Text);
            int index = rpt.Rows.Count - 1;

            for (int i = 0; i < count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                index++;

                c = new TableCell();
                c.Text = index.ToString() + ".";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Tipe;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Nama + " " + (i + 1);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(DateTime.Today);
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }

        private void Isi(bool potongbf)
        {
            //Func.KontrakHeader(NoKontrak, nokontrakdetail, unitdetail, namadetail, agent);


            SetBaris(bfkali, "BF", "BOOKING FEE", bflama1, bflama2, bfhari1, bfhari2, bfjumlah, bfpersen);
            SetBaris(dpkali, "DP", "DP", dplama1, dplama2, dphari1, dphari2, dpjumlah, dppersen);
            SetBaris(angkali, "ANG", "ANGSURAN", anglama1, anglama2, anghari1, anghari2, angjumlah, angpersen);

            //potong booking fee harus dikontrol pada saat POSTBACK
            if (potongbf)
                PotongBF();

            decimal Sum = 0;
            for (int i = 1; i < rpt.Rows.Count; i++)
            {
                Sum = Sum + Convert.ToDecimal(rpt.Rows[i].Cells[3].Text);
            }

            TableRow r = new TableRow();
            TableCell c;

            r = new TableRow();

            c = new TableCell();
            c.Text = "Grand Total";
            c.ColumnSpan = 2;
            c.Font.Bold = true;
            c.Font.Size = 10;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Sum);
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Font.Bold = true;
            c.Font.Size = 10;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void PotongBF()
        {
            decimal totalbf = 0;
            int minbf = 0;

            if (bftdkdisebar.Checked)
            {
                for (int i = 1; i < rpt.Rows.Count; i++)
                {
                    string Tipe = rpt.Rows[i].Cells[5].Text;
                    string Min = rpt.Rows[i].Cells[4].Text;

                    if (Tipe == "BF")
                        totalbf = totalbf + Convert.ToDecimal(rpt.Rows[i].Cells[3].Text);
                }

                decimal bfsatuan = totalbf / rpt.Rows.Count - 2;
                for (int i = 1; i < rpt.Rows.Count; i++)
                {
                    string Tipe = rpt.Rows[i].Cells[1].Text;
                    if (Tipe != "BF")
                    {
                        rpt.Rows[i].Cells[3].Text = Cf.Num(Math.Round(
                               Convert.ToDecimal(rpt.Rows[i].Cells[3].Text) - bfsatuan));
                    }
                }
            }
            else
            {
                for (int i = 1; i < rpt.Rows.Count; i++)
                {
                    string Tipe = rpt.Rows[i].Cells[5].Text;
                    string Min = rpt.Rows[i].Cells[4].Text;

                    if (Tipe == "BF")
                        totalbf = totalbf + Convert.ToDecimal(rpt.Rows[i].Cells[3].Text);

                    if (Min == "MIN")
                        minbf++;
                }
                decimal bfsatuan = Math.Round(totalbf / minbf);

                for (int i = 1; i < rpt.Rows.Count; i++)
                {
                    string Min = rpt.Rows[i].Cells[4].Text;
                    if (Min == "MIN")
                    {
                        rpt.Rows[i].Cells[3].Text = Cf.Num(
                            Convert.ToDecimal(rpt.Rows[i].Cells[3].Text) - bfsatuan);
                    }
                }
            }
            decimal Sum = 0;
            decimal NilaiTagihanTerakhir = 0;
            bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'PLIncludePPN" + project.SelectedValue + "'") == "True";
            decimal dpp = 0;
            decimal ppn = 0;
            if (includeppn)
            {
                dpp = Math.Round((Convert.ToDecimal(gross.Text) + Convert.ToDecimal(nilaiBunga.Text) - Convert.ToDecimal(nilaiDiskon.Text)) / Convert.ToDecimal(1.1));
                ppn = Convert.ToDecimal(gross.Text) + Convert.ToDecimal(nilaiBunga.Text) - Convert.ToDecimal(nilaiDiskon.Text) - dpp;
            }
            else
            {
                dpp = Math.Round(Convert.ToDecimal(gross.Text) + Convert.ToDecimal(nilaiBunga.Text) - Convert.ToDecimal(nilaiDiskon.Text));
                ppn = Math.Round(dpp * Convert.ToDecimal(0.1));
            }
            decimal Netto = dpp + ppn;

            for (int n = 1; n < rpt.Rows.Count - 1; n++)
            {
                Sum = Sum + Convert.ToDecimal(rpt.Rows[n].Cells[3].Text);
                NilaiTagihanTerakhir = Netto - Sum;
            }
            int a = rpt.Rows.Count - 1;
            rpt.Rows[a].Cells[3].Text = Cf.Num(NilaiTagihanTerakhir.ToString());

        }

        protected void pdf_Click(object sender, EventArgs e)
        {
            Process p = new System.Diagnostics.Process();

            string Nama = "Skema Cara Bayar";
            string Link = "";
            DateTime TglGenerate = DateTime.Now;
            string FileName = "";
            string FileType = "application/pdf";
            string UserID = Act.UserID;
            string IP = Act.IP;

            Db.Execute("EXEC ISC064_MARKETINGJUAL..spLapPDFDaftar"

                    + " '" + Nama + "'"
                    + ",'" + Link + "'"
                    + ",'" + TglGenerate + "'"
                    + ",'" + IP + "'"
                    + ",'" + UserID + "'"
                    + ",'" + FileName + "'"
                    + ",'" + FileType + "'"
                    + ",'" + Cf.Date(tglkontrak.Text) + "'"
                    + ",'" + null + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "SkemaCaraBayar" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);

            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "SkemaCaraBayar" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //parameter
            string CaraBayar = carabayar.SelectedValue;
            CaraBayar = CaraBayar.Replace(" ", "%");
            string NoStok = nostock.Text;
            int Metode = metode.SelectedIndex;
            string potong = "";
            if (dp1potong.Checked)
            {
                potong = "DP1";
            }
            else if (dpspotong.Checked)
            {
                potong = "DPS";
            }
            else if (ang1potong.Checked)
            {
                potong = "ANG1";
            }
            else
            {
                potong = "ANGS";
            }

            string persen = "";
            if (bfpersen.Checked)
                persen = "%";
            string persen2 = "";
            if (dppersen.Checked)
                persen2 = "%";
            string persen3 = "";
            if (angpersen.Checked)
                persen3 = "%";
            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFSkema.aspx?id=" + rs.Rows[0]["AttachmentID"]
                        + "&nilai=" + Cf.Num(nilai.Text)
                        + "&carabayar=" + carabayar.SelectedValue
                        + "&metode=" + Metode
                        + "&diskon=" + nilaiDiskon.Text
                        + "&presenvalue=" + nilaiBunga.Text
                        + "&dpp=" + dpp.Text
                        + "&ppn=" + ppn.Text
                        + "&bfkali=" + bfkali.Text
                        + "&bflama1=" + bflama1.Text
                        + "&bflama2=" + bflama2.Text
                        + "&bfhari1=" + bfhari1.Text
                        + "&bfhari2=" + bfhari2.Text
                        + "&bfjumlah=" + bfjumlah.Text
                        + "&bfpersen=" + persen
                        + "&dpkali=" + dpkali.Text
                        + "&dplama1=" + dplama1.Text
                        + "&dplama2=" + dplama2.Text
                        + "&dphari1=" + dphari1.Text
                        + "&dphari2=" + dphari2.Text
                        + "&dpjumlah=" + dpjumlah.Text
                        + "&dppersen=" + persen2
                        + "&angkali=" + angkali.Text
                        + "&anglama1=" + anglama1.Text
                        + "&anglama2=" + anglama2.Text
                        + "&anghari1=" + anghari1.Text
                        + "&anghari2=" + anghari2.Text
                        + "&angjumlah=" + angjumlah.Text
                        + "&angpersen=" + persen3
                        + "&potong=" + potong
                        + "&nostok=" + nostock.Text;
            ;

            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 8.5in --page-height 11in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

            //panggil aplikasi untuk mengconvert pdf
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();

            //60000 -> waktu jeda lama convert pdf
            p.WaitForExit(30000);

            string Src = Mi.PathFilePDFReport + nfilename;
            Mi.DownloadPDF(this, Src, (rs.Rows[0]["FileName"]).ToString(), rs.Rows[0]["FileType"].ToString());

        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            carabayar.Items.Clear();
            Bind();
            InitForm();
        }
        protected void lokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            carabayar.Items.Clear();
            Bind();
            InitForm();

            if (nostock.Text != "")
            {
                Response.Redirect("KalkulatorSkema.aspx");
            }
        }
    }
}
