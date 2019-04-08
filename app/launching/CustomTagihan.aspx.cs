using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace ISC064.LAUNCHING
{
    public partial class CustomTagihan : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                hasil.Visible = false;

                InitForm();

                dariDaftar.Checked = true;
                frm.Visible = true;
                Fill();

                cancel.Attributes["onclick"] = "location.href='ClosingNUP2.aspx?No=" + NoKontrak + "&Tipe=" + Jenis + "'";
            }

            if (hasil.Visible)
            {
                Js.Confirm(this, "Lanjutkan proses custom tagihan?");
                Isi(false);
            }
        }

        private void InitForm()
        {
            //kalkulator
            bfjumlah.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            bfjumlah.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            bfjumlah.Attributes["onblur"] = "CalcBlur(this);";

            dpjumlah.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            dpjumlah.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            dpjumlah.Attributes["onblur"] = "CalcBlur(this);";

            angjumlah.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            angjumlah.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            angjumlah.Attributes["onblur"] = "CalcBlur(this);";

            diskon.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            diskon.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            diskon.Attributes["onblur"] = "CalcBlur(this);";

            netto.Attributes["style"] = "border:0px;font:bold";
            BindSkema();
        }

        private void BindSkema()
        {
            //Skema
            DataTable rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' ORDER BY Nama");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Nomor"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                skema.Items.Add(new ListItem(t, v));
            }
        }

        private void Fill()
        {
            DataTable rs = Db.Rs(
                "SELECT * FROM MS_NUP WHERE NoNUP = '" + NoKontrak + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                //Skema
                int refSkema = Db.SingleInteger("SELECT NomorSkema FROM MS_NUP_Priority WHERE NoNUP='" + NoKontrak + "' ");
                if (refSkema > 0)
                {
                    skema.SelectedValue = refSkema.ToString();
                }
                else
                {
                    skema.Items.Add(new ListItem(rs.Rows[0]["Skema"].ToString(), "0")); //cara bayar customize
                    skema.SelectedValue = refSkema.ToString();
                }

                string CaraBayar = Db.SingleString("SELECT Jenis FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                carabayar2.SelectedValue = CaraBayar;

                decimal persenDP = Db.SingleDecimal("SELECT ISNULL(SUM(CAST(ISNULL(NULLIF(NominalString, ''), 0) AS FLOAT)),0) FROM REF_SKEMA_DETAIL WHERE Nomor='" + refSkema + "' AND Tipe='DP'");
                decimal persenANG = Db.SingleDecimal("SELECT ISNULL(SUM(CAST(ISNULL(NULLIF(NominalString, ''), 0) AS FLOAT)),0) FROM REF_SKEMA_DETAIL WHERE Nomor='" + refSkema + "' AND Tipe='ANG'");

                int BF = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP_TAGIHAN WHERE NoNUP='" + NoKontrak + "' AND Tipe='BF'");
                int DP = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP_TAGIHAN WHERE NoNUP='" + NoKontrak + "' AND Tipe='DP'");
                int ANG = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP_TAGIHAN WHERE NoNUP='" + NoKontrak + "' AND Tipe='ANG'");
                dpjumlah.Text = Cf.Num(persenDP);
                angjumlah.Text = Cf.Num(persenANG);
                bfkali.Text = (BF).ToString();
                dpkali.Text = (DP).ToString();
                angkali.Text = (ANG).ToString();
                decimal NilaiKontrak = Db.SingleDecimal("SELECT Pricelist FROM MS_NUP_Priority WHERE NoNUP='" + NoKontrak + "' ");

                netto.Text = Cf.Num(NilaiKontrak);
                tgl.Text = Cf.Day(DateTime.Today);

                nokontrak2.Text = NoKontrak;
                unit.Text = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                nama.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"].ToString() + "'");
                agent.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[0]["NoAgent"].ToString() + "'");

            }
        }

        private bool Check()
        {
            string s = "";
            bool x = true;

            //bf nominal
            if (!Cf.isInt(bfkali))
            {
                x = false;
                if (s == "") s = bfkali.ID;
                bfc.Text = "Angka";
            }
            else if (Convert.ToInt32(bfkali.Text) < 0)
            {
                x = false;
                if (s == "") s = bfkali.ID;
                bfc.Text = "Positif";
            }
            else if (!Cf.isMoney(bfjumlah))
            {
                x = false;
                if (s == "") s = bfjumlah.ID;
                bfc.Text = "Angka";
            }
            else
                bfc.Text = "";

            //dp nominal
            if (!Cf.isInt(dpkali))
            {
                x = false;
                if (s == "") s = dpkali.ID;
                dpc.Text = "Angka";
            }
            else if (Convert.ToInt32(dpkali.Text) < 0)
            {
                x = false;
                if (s == "") s = dpkali.ID;
                dpc.Text = "Positif";
            }
            else if (!Cf.isMoney(dpjumlah))
            {
                x = false;
                if (s == "") s = dpjumlah.ID;
                dpc.Text = "Angka";
            }
            else if ((dppersen.Checked) && (Convert.ToDecimal(dpjumlah.Text) > 100))
            {
                x = false;
                if (s == "") s = dpjumlah.ID;
                dpc.Text = "Tidak Boleh Lebih Dari 100%";
            }
            else
                dpc.Text = "";

            //ang nominal
            if (!Cf.isInt(angkali))
            {
                x = false;
                if (s == "") s = angkali.ID;
                angc.Text = "Angka";
            }
            else if (Convert.ToInt32(angkali.Text) < 0)
            {
                x = false;
                if (s == "") s = angkali.ID;
                angc.Text = "Positif";
            }
            else if (!Cf.isMoney(angjumlah))
            {
                x = false;
                if (s == "") s = angjumlah.ID;
                angc.Text = "Angka";
            }
            else if ((angpersen.Checked) && (Convert.ToDecimal(angjumlah.Text) > 100))
            {
                x = false;
                if (s == "") s = angjumlah.ID;
                angc.Text = "Tidak Boleh Lebih Dari 100%";
            }
            else
                angc.Text = "";


            //gabungan persen antara dp dan ang tidak boleh > 100
            if ((angpersen.Checked) && (dppersen.Checked) && (Convert.ToDecimal(angjumlah.Text) + (Convert.ToDecimal(dpjumlah.Text)) != 100))
            {
                x = false;
                if (s == "") s = angjumlah.ID;
                angc.Text = "Nilai DP dan Ang Tidak Boleh Lebih atau Kurang Dari 100%";
            }
            else
                angc.Text = "";

            //tanggal
            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Format tanggal";
            }
            else
                tglc.Text = "";

            //bf jadwal
            if (!Cf.isInt(bflama1))
            {
                x = false;
                if (s == "") s = bflama1.ID;
                bf2c.Text = "Angka";
            }
            else if (Convert.ToInt32(bflama1.Text) < 0)
            {
                x = false;
                if (s == "") s = bflama1.ID;
                bf2c.Text = "Positif";
            }
            else if (!Cf.isInt(bflama2))
            {
                x = false;
                if (s == "") s = bflama2.ID;
                bf2c.Text = "Angka";
            }
            else if (Convert.ToInt32(bflama2.Text) < 0)
            {
                x = false;
                if (s == "") s = bflama2.ID;
                bf2c.Text = "Positif";
            }
            else
                bf2c.Text = "";

            //dp jadwal
            if (!Cf.isInt(dplama1))
            {
                x = false;
                if (s == "") s = dplama1.ID;
                dp2c.Text = "Angka";
            }
            else if (Convert.ToInt32(dplama1.Text) < 0)
            {
                x = false;
                if (s == "") s = dplama1.ID;
                dp2c.Text = "Positif";
            }
            else if (!Cf.isInt(dplama2))
            {
                x = false;
                if (s == "") s = dplama2.ID;
                dp2c.Text = "Angka";
            }
            else if (Convert.ToInt32(dplama2.Text) < 0)
            {
                x = false;
                if (s == "") s = dplama2.ID;
                dp2c.Text = "Positif";
            }
            else
                dp2c.Text = "";

            //ang jadwal
            if (!Cf.isInt(anglama1))
            {
                x = false;
                if (s == "") s = anglama1.ID;
                ang2c.Text = "Angka";
            }
            else if (Convert.ToInt32(anglama1.Text) < 0)
            {
                x = false;
                if (s == "") s = anglama1.ID;
                ang2c.Text = "Positif";
            }
            else if (!Cf.isInt(anglama2))
            {
                x = false;
                if (s == "") s = anglama2.ID;
                ang2c.Text = "Angka";
            }
            else if (Convert.ToInt32(anglama2.Text) < 0)
            {
                x = false;
                if (s == "") s = anglama2.ID;
                ang2c.Text = "Positif";
            }
            else
                ang2c.Text = "";

            //tanggal bf
            if (bftgl.Text != "")
            {
                if (!Cf.isTgl(bftgl))
                {
                    x = false;
                    if (s == "") s = bftgl.ID;
                    bftglc.Text = "Format tanggal";
                }
                else
                    bftglc.Text = "";
            }

            //tanggal dp
            if (dptgl.Text != "")
            {
                if (!Cf.isTgl(dptgl))
                {
                    x = false;
                    if (s == "") s = dptgl.ID;
                    dptglc.Text = "Format tanggal";
                }
                else
                    dptglc.Text = "";
            }

            //tanggal ang
            if (angtgl.Text != "")
            {
                if (!Cf.isTgl(angtgl))
                {
                    x = false;
                    if (s == "") s = angtgl.ID;
                    angtglc.Text = "Format tanggal";
                }
                else
                    angtglc.Text = "";
            }

            if (x)
            {
                if ((dp1potong.Checked || dpspotong.Checked) && (Convert.ToInt32(dpkali.Text) == 0))
                {
                    x = false;
                    if (s == "") s = dpkali.ID;
                    cc.Text = "DP 0 kali";
                }
                else if ((ang1potong.Checked || angspotong.Checked) && (Convert.ToInt32(angkali.Text) == 0))
                {
                    x = false;
                    if (s == "") s = angkali.ID;
                    cc.Text = "Angsuran 0 kali";
                }
                else
                    cc.Text = "";
            }

            if (diskon.Text != "")
            {
                decimal Netto = Convert.ToDecimal(netto.Text);
                decimal disc = Convert.ToDecimal(diskon.Text);

                if (disc > Netto)
                {
                    x = false;
                    if (s == "") s = diskon.ID;
                    diskonr.Text = "Diskon melebihi Nilai Tagihan";
                }
            }
            else
            {
                x = false;
                if (s == "") s = diskon.ID;
                diskonr.Text = "Kosong";
            }


            if (!x)
            {
                this.RegisterStartupScript(
                    "focusScript"
                    , "<script language='javascript'>"
                    + " document.getElementById('" + s + "').focus();"
                    + " document.getElementById('" + s + "').select();"
                    + "</script>"
                    );
            }

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Check())
            {
                frm.Visible = false;
                hasil.Visible = true;

                Js.Confirm(this, "Lanjutkan proses custom tagihan?");

                if (tidakpotong.Checked)
                    Isi(false);
                else
                    Isi(true);
            }
        }

        private void SetBaris(TextBox Kali, string Tipe, string Nama
            , TextBox interval1, TextBox interval2
            , CheckBox hari1, CheckBox hari2
            , TextBox nominal, CheckBox persen
            )
        {
            int count = Convert.ToInt32(Kali.Text);
            int index = rpt.Rows.Count - 1;
            decimal Total = 0;
            DateTime Tgl = Convert.ToDateTime(tgl.Text); //tanggal kontrak
            try
            {
                if (Tipe == "BF" && bftgl.Text != "")
                {
                    Tgl = Convert.ToDateTime(bftgl.Text);
                }
                else if (Tipe == "DP" && dptgl.Text != "")
                {
                    Tgl = Convert.ToDateTime(dptgl.Text);
                }
                else if (Tipe == "ANG" && angtgl.Text != "")
                {
                    Tgl = Convert.ToDateTime(angtgl.Text);
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

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = index.ToString() + ".";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Tipe;
                r.Cells.Add(c);

                //request forest
                if ((Tipe == "BF") && (i == count - 1))
                {
                    c = new TableCell();
                    c.Text = Nama;  //Nama + " " + (i + 1);
                    r.Cells.Add(c);
                }
                else if ((Nama == "KPR") && (i == count - 1))
                {
                    c = new TableCell();
                    c.Text = Nama;  //Nama + " " + (i + 1);
                    r.Cells.Add(c);
                }
                else
                {
                    c = new TableCell();
                    if (count != 1)
                        c.Text = Nama + " " + (i + 1);
                    else
                        c.Text = Nama;
                    r.Cells.Add(c);
                }

                if (i == 0)
                {
                    int interval = Convert.ToInt32(interval2.Text);
                    if (Tipe == "BF" && bftgl.Text != "")
                    {
                        interval = 0;
                    }
                    else if (Tipe == "DP" && dptgl.Text != "")
                    {
                        interval = 0;
                    }
                    else if (Tipe == "ANG" && angtgl.Text != "")
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
                        if (Tipe == "BF" && bftgl.Text != "")
                        {
                            h = Convert.ToDateTime(bftgl.Text).Day;
                        }
                        else if (Tipe == "DP" && dptgl.Text != "")
                        {
                            h = Convert.ToDateTime(dptgl.Text).Day;
                        }
                        else if (Tipe == "ANG" && angtgl.Text != "")
                        {
                            h = Convert.ToDateTime(angtgl.Text).Day;
                        }
                        Tgl = Tgl.AddMonths(interval);
                        if (h != Tgl.Day && h <= DateTime.DaysInMonth(Tgl.Year, Tgl.Month))
                            Tgl = new DateTime(Tgl.Year, Tgl.Month, h);
                    }
                }

                c = new TableCell();
                c.Text = Cf.Day(Tgl);
                r.Cells.Add(c);

                decimal Nominal = Convert.ToDecimal(nominal.Text);
                //decimal Netto = Convert.ToDecimal(netto.Text);
                decimal Netto = Convert.ToDecimal(netto.Text) - Convert.ToDecimal(diskon.Text);

                ////// Diskon dari Pricelist Perskema (hanya jika sudah pilih cara bayar / bukan customize)
                //decimal diskonpricelist = Db.SingleDecimal("SELECT ISNULL(Diskon, 0) FROM MS_PRICELIST WHERE NoStock = '" + NoStock + "' AND NoSkema = " + skema.SelectedValue);

                //Netto -= diskonpricelist;

                // Booking Fee Memotong Nilai Kontrak (bf tidak dipotong di skema cara bayar)
                decimal bookingFeePotongNilaiKontrak = 0;
                DataTable rs = Db.Rs("SELECT * "
                                + " FROM REF_SKEMA_DETAIL"
                                + " WHERE Nomor = " + skema.SelectedValue
                                + " ORDER BY Baris"
                                );

                for (int ii = 0; ii < rs.Rows.Count; ii++)
                {
                    //nominal
                    string tipenominal = rs.Rows[ii]["TipeNominal"].ToString();
                    decimal nominals = Convert.ToDecimal(rs.Rows[ii]["Nominal"]);
                    decimal nilai = 0;

                    if (rs.Rows[ii]["Tipe"].ToString() == "BF")
                    {
                        decimal n = nominals;
                        if (tipenominal == "%") n = Netto * (nominals / 100);
                        nilai += n;
                    }
                    else
                        continue;

                    bookingFeePotongNilaiKontrak += nilai;
                }

                Netto -= bookingFeePotongNilaiKontrak;
                decimal NilaiTotal = 0;
                if (persen.Checked)
                {
                    NilaiTotal = Netto * Nominal / 100;
                    Nominal = Netto * Nominal / 100;
                }

                decimal NilaiTagihan = 0;

                if (rounding.Checked)
                {
                    if (i != (count - 1))
                    {

                        NilaiTagihan = RoundThousand(Nominal / count);
                        Total += NilaiTagihan;
                    }
                    else
                    {
                        if (Tipe != "BF")
                            NilaiTagihan = NilaiTotal - Total;
                        else
                            NilaiTagihan = Nominal;
                    }

                }
                else
                {
                    NilaiTagihan = Math.Round(Nominal / count);
                }

                c = new TableCell();
                c.Text = Cf.Num(NilaiTagihan);
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
                int countkpr = Db.SingleInteger("select count(*) from REF_SKEMA_DETAIL where KPR = 1 AND Nomor = " + skema.SelectedValue);
                if (countkpr != 0)
                {
                    c.Text = Nama;
                }
                else
                {
                    c.Text = Nama + " " + (i + 1);
                }
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
            nokontrakdetail.Text = NoKontrak;
            unitdetail.Text = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            namadetail.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = (SELECT NoCustomer FROM MS_NUP WHERE NoNUP = '" + NoKontrak + "')");


            SetBaris(bfkali, "BF", "BOOKING FEE", bflama1, bflama2, bfhari1, bfhari2, bfjumlah, bfpersen);
            SetBaris(dpkali, "DP", "DP", dplama1, dplama2, dphari1, dphari2, dpjumlah, dppersen);

            int kpr = Db.SingleInteger("select count(*) from REF_SKEMA_DETAIL where KPR = 1 AND Nomor = " + skema.SelectedValue);
            if (kpr != 0)
            {
                SetBaris(angkali, "ANG", "KPA", anglama1, anglama2, anghari1, anghari2, angjumlah, angpersen);
            }
            else
            {
                SetBaris(angkali, "ANG", "ANGSURAN", anglama1, anglama2, anghari1, anghari2, angjumlah, angpersen);
            }

            //potong booking fee harus dikontrol pada saat POSTBACK
            if (potongbf)
                PotongBF();
            else
                TidakPotongBF();

        }
        private void TidakPotongBF()
        {
            decimal Netto = Convert.ToDecimal(netto.Text) - Convert.ToDecimal(diskon.Text);
            bool RThousand = rounding.Checked;
            decimal t = 0;
            for (int i = 1; i < rpt.Rows.Count; i++)
            {
                if (i != rpt.Rows.Count - 1)
                {
                    decimal native = Convert.ToDecimal(rpt.Rows[i].Cells[4].Text);
                    decimal rounded = 0;
                    //if (RThousand)
                    //    rounded = RoundThousand(native);
                    //else
                    rounded = RoundSatuan(native);
                    t = t + rounded;

                    rpt.Rows[i].Cells[4].Text = Cf.Num(rounded);
                }
                else
                {
                    decimal sisa = Netto - t;

                    rpt.Rows[i].Cells[4].Text = Cf.Num(sisa);
                }
            }
        }
        private void PotongBF()
        {
            decimal totalbf = 0;
            int minbf = 0;

            for (int i = 1; i < rpt.Rows.Count; i++)
            {
                string Tipe = rpt.Rows[i].Cells[1].Text;
                string Min = rpt.Rows[i].Cells[5].Text;

                if (Tipe == "BF")
                    totalbf = totalbf + Convert.ToDecimal(rpt.Rows[i].Cells[4].Text);

                if (Min == "MIN")
                    minbf++;
            }

            decimal bfsatuan = totalbf / minbf;

            for (int i = 1; i < rpt.Rows.Count; i++)
            {
                string Min = rpt.Rows[i].Cells[5].Text;
                if (Min == "MIN")
                {
                    rpt.Rows[i].Cells[4].Text = Cf.Num(
                        Convert.ToDecimal(rpt.Rows[i].Cells[4].Text) - bfsatuan);
                }
            }

            //decimal Netto = Convert.ToDecimal(netto.Text);
            decimal Netto = Convert.ToDecimal(netto.Text) - Convert.ToDecimal(diskon.Text);
            bool RThousand = rounding.Checked;
            decimal t = 0;
            for (int i = 1; i < rpt.Rows.Count; i++)
            {
                if (i != rpt.Rows.Count - 1)
                {
                    decimal native = Convert.ToDecimal(rpt.Rows[i].Cells[4].Text);
                    decimal rounded = 0;
                    if (RThousand)
                        rounded = RoundThousand(native);
                    else
                        rounded = RoundSatuan(native);
                    t = t + rounded;

                    rpt.Rows[i].Cells[4].Text = Cf.Num(rounded);
                }
                else
                {
                    decimal sisa = Netto - t;

                    rpt.Rows[i].Cells[4].Text = Cf.Num(sisa);
                }
            }


        }

        private void Bulat(bool RThousand)
        {
            decimal t = 0;
            for (int i = 1; i < rpt.Rows.Count; i++)
            {
                TableCell c = rpt.Rows[i].Cells[4];

                if (i != rpt.Rows.Count - 1)
                {
                    decimal native = Convert.ToDecimal(c.Text);
                    decimal rounded = 0;
                    if (RThousand)
                        rounded = RoundThousand(native);
                    else
                        rounded = RoundSatuan(native);

                    t = t + rounded;

                    c.Text = Cf.Num(rounded);
                }
                else
                {
                    //decimal sisa = Convert.ToDecimal(netto.Text) - t;
                    decimal sisa = (Convert.ToDecimal(netto.Text) - Convert.ToDecimal(diskon.Text)) - t;

                    c.Text = Cf.Num(sisa);
                }
            }
        }

        protected void insert_Click(object sender, System.EventArgs e)
        {
            Db.Execute("UPDATE MS_NUP_PRIORITY SET Diskon = " + Convert.ToDecimal(diskon.Text) + " WHERE NoNUP = '" + NoKontrak + "'");
            Db.Execute("DELETE FROM MS_NUP_TAGIHAN WHERE NoNUP = '" + NoKontrak + "'");
            for (int i = 1; i < rpt.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string Tipe = rpt.Rows[i].Cells[1].Text;
                string NamaTagihan = Cf.Str(rpt.Rows[i].Cells[2].Text);
                DateTime TglJT = Convert.ToDateTime(rpt.Rows[i].Cells[3].Text);
                decimal NilaiTagihan = Convert.ToDecimal(rpt.Rows[i].Cells[4].Text);

                Db.Execute("EXEC spTagihanDaftarNUP"
                    + " '" + NoKontrak + "'"
                    + ",'" + NamaTagihan + "'"
                    + ",'" + TglJT + "'"
                    + ", " + NilaiTagihan
                    + ",'" + Tipe + "'"
                    );
            }

            Response.Redirect("ClosingNUP2.aspx?No=" + NoKontrak + "&Tipe=" + Jenis);
        }

        protected void skema_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CaraBayar = Db.SingleString(
                    "SELECT Jenis FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);

            carabayar2.SelectedValue = CaraBayar;

        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }

        private string Jenis
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }
        private int CaraBayar
        {
            get
            {
                return Convert.ToInt16(Request.QueryString["CB"]);
            }
        }

        #region private static decimal RoundThousand(decimal input)
        private static decimal RoundThousand(decimal input)
        {
            if (input < 1000)
            {
                return 1000;
            }
            else
            {
                input = RoundUp(input);
                if ((input % 1000) > 0)
                {
                    input = (input - (input % 1000)) + 1000;
                }
                return input;
            }
        }
        private static decimal RoundSatuan(decimal input)
        {
            return Math.Round(input);
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
        #endregion
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
