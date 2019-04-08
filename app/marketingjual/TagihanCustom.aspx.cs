using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace ISC064.MARKETINGJUAL
{
    public partial class TagihanCustom : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                backbtn.Visible = false;
                hasil.Visible = false;

                nokontrak1.Attributes["ondblclick"] = "popDaftarKontrak('a&tag=1');";
                InitForm();                
                if (Request.QueryString["NoKontrak"] != null)
                {
                    //dari halaman kontrak
                    dariDaftar.Checked = true;
                    nokontrak1.Text = Cf.Pk(Request.QueryString["NoKontrak"]);
                    cancel.Attributes["onclick"] = "location.href='KontrakDaftar3.aspx?NoKontrak="
                        + Cf.Pk(Request.QueryString["NoKontrak"]) + "'";
                    LoadKontrak();
                }
                else
                {                 
                    Js.Focus(this, nokontrak1);
                    frm.Visible = false;
                    hasil.Visible = false;
                }

            }
            FeedBack();

            if (hasil.Visible)
            {
                Js.Confirm(this, "Lanjutkan proses customize tagihan?");
                Isi(false);
            }

        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popJadwalTagihan('" + Request.QueryString["done"] + "')\">"
                        + "Customize Tagihan Berhasil..."
                        + "</a>";
            }
        }

        //private void Bind()
        //{            
        //    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
        //    //DataTable rs = Db.Rs("SELECT * FROM REF_SKEMA WHERE Project = '" + Project + "' ORDER BY Nama");
        //    //skema.Items.Add(new ListItem("*** CUSTOMIZE / PENDING", "0"));
        //    //for (int i = 0; i < rs.Rows.Count; i++)
        //    //{
        //    //    skema.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString(), rs.Rows[i]["Nomor"].ToString()));
        //    //}
        //}

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

            netto.Attributes["style"] = "border:0px;font:bold";            
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");

            int d = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");

            if (c == 0 || d != 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Jadwal tagihan sudah pernah dikeluarkan."
                    , "document.getElementById('nokontrak1').focus();"
                    + "document.getElementById('nokontrak1').select();"
                    );

            return x;
        }
        private bool validTagihan()
        {
            bool x = true;
            if (!tidakpotong.Checked)
            {
                for (int i = 1; i < rpt.Rows.Count; i++)
                {
                    bool xx = true;
                    string Min = rpt.Rows[i].Cells[5].Text;
                    if (Min == "MIN")
                    {
                        if (Convert.ToDecimal(rpt.Rows[i].Cells[4].Text) <= 0)
                        {
                            xx = x = false;
                        }
                    }
                }
                if (!x)
                {
                    Js.Alert(
                        this
                        , "Kontrak Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Nilai Tagihan yang dipotong Booking Fee Kurang.\\n"
                        , "document.getElementById('nokontrak1').focus();"
                        + "document.getElementById('nokontrak1').select();"
                        );

                    rpt.Rows.Clear();
                }
            }
            return x;
        }
        private void LoadKontrak()
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;
                //Bind();
                Fill();
            }
            else
            {
                backbtn.Visible = true;
                Js.Focus(this, nokontrak1);
                frm.Visible = false;
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                int count = Db.SingleInteger("SELECT COUNT(*) FROM MS_APPROVAL WHERE SumberID = '" + NoKontrak + "' AND Sumber = '" + Str.Approval("7") + "' AND Status <> 'DONE'");
                if (count == 0)
                {
                    pilih.Visible = false;
                    frm.Visible = true;
                    //Bind();
                    Fill();
                }
                else
                {
                    pilih.Visible = true;
                    feed1.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Proses Customize tagihan untuk Kontrak tersebut belum selesai.";
                    feed1.Attributes["style"] = "background-color:white;color:red;";
                }
            }
        }

        //private void FillRumus()
        //{
        //    int BF = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'BF'");
        //    int DP = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'DP'");
        //    int ANG = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'ANG'");
        //    decimal BFJumlah = Db.SingleDecimal("SELECT Nominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'BF'");
        //    decimal DPJumlah = Db.SingleDecimal("SELECT Nominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'DP'");
        //    decimal ANGJumlah = Db.SingleDecimal("SELECT Nominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'ANG'");
        //    string BFTipe = Db.SingleString("SELECT TipeNominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'BF'");
        //    string DPTipe = Db.SingleString("SELECT TipeNominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'DP'");
        //    string ANGTipe = Db.SingleString("SELECT TipeNominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'ANG'");

        //    bfkali.Text = BF.ToString();
        //    dpkali.Text = DP.ToString();
        //    angkali.Text = ANG.ToString();
        //    bfjumlah.Text = Cf.Num(Math.Round(BFJumlah * BF));
        //    dpjumlah.Text = Cf.Num(Math.Round(DPJumlah * DP));
        //    angjumlah.Text = Cf.Num(Math.Round(ANGJumlah * ANG));
        //    bool x = true;
        //    if (BFTipe == "%")
        //    {
        //        bfpersen.Checked = true;
        //    }
        //    else
        //    {
        //        bfpersen.Checked = false;
        //        bfrupiah.Checked = true;
        //    }
        //    if (DPTipe == "%")
        //    {
        //        dppersen.Checked = true;
        //    }
        //    else
        //    {
        //        dppersen.Checked = false;
        //        dprupiah.Checked = true;
        //    }
        //    if (ANGTipe == "%")
        //    {
        //        angpersen.Checked = true;
        //    }
        //    else
        //    {
        //        angpersen.Checked = false;
        //        angrupiah.Checked = true;
        //    }
        //}
        private void Fill()
        {
            //FillRumus();
            Func.KontrakHeader(NoKontrak, nokontrak2, unit, nama, agent);

            DataTable rs = Db.Rs(
                "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                netto.Text = Cf.Num(rs.Rows[0]["NilaiKontrak"]);
                tgl.Text = Cf.Day(rs.Rows[0]["TglKontrak"]).ToString();
                skema.Text = rs.Rows[0]["Skema"].ToString();
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
                if (tidakpotong.Checked)
                {
                    Isi(false);
                }
                else
                {
                    Isi(true);
                }
            }
            if (validTagihan())
            {
                frm.Visible = false;
                hasil.Visible = true;
                Js.Confirm(this, "Lanjutkan proses customize tagihan?");
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

                c = new TableCell();
                decimal cc = Db.SingleDecimal("SELECT COUNT(*) FROM MS_TAGIHAN WHERE tipe='" + Tipe + "' AND nourut in (select notagihan from ms_pelunasan where NoKontrak = '" + NoKontrak + "' AND NilaiPelunasan > 0) AND Nokontrak = '" + NoKontrak + "'");

                if (carabayar2.SelectedValue == "KPR")
                {
                    if (Tipe == "ANG")
                    {
                        if (i == count - 1)
                        {
                            c.Text = "PENCAIRAN KPR";
                        }
                        else
                        {
                            c.Text = Nama + " " + (cc + i + 1);
                        }
                    }
                    else
                    {
                        c.Text = Nama + " " + (cc + i + 1);
                    }
                }
                else
                {
                    c.Text = Nama + " " + (cc + i + 1);
                }
                //c.Text = Nama + " " + (cc + i + 1);
                r.Cells.Add(c);

                //c = new TableCell();
                //c.Text = Nama + " " + (i + 1);
                //r.Cells.Add(c);

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
                decimal Netto = Convert.ToDecimal(netto.Text);

                if (persen.Checked) Nominal = Netto * (Nominal / 100);

                if (Tipe == "ANG" && i == (count - 1))
                {
                    decimal aa = Convert.ToDecimal(netto.Text);
                    decimal nilaiDP = 0, nilaiBF = 0, nilaiANG = 0;

                    for (int j = 1; j < rpt.Rows.Count; j++)
                    {
                        if (!Response.IsClientConnected) break;

                        if (rpt.Rows[j].Cells[1].Text == "BF")
                            nilaiBF += Convert.ToDecimal(rpt.Rows[j].Cells[4].Text);
                        else if (rpt.Rows[j].Cells[1].Text == "DP")
                            nilaiDP += Convert.ToDecimal(rpt.Rows[j].Cells[4].Text);
                        else if (rpt.Rows[j].Cells[1].Text == "ANG")
                            nilaiANG += Convert.ToDecimal(rpt.Rows[j].Cells[4].Text);
                    }
                    Nominal = RoundThousand(aa - RoundThousand((nilaiDP + nilaiANG)));
                }
                else
                {
                    if (rounding.Checked)
                    {
                        decimal rounded = 0;
                        Nominal = Math.Round(Nominal / count);
                    }
                    else
                    {
                        Nominal = Nominal / count;
                    }
                }
                
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

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }

        private void Isi(bool potongbf)
        {
            Func.KontrakHeader(NoKontrak, nokontrakdetail, unitdetail, namadetail, agent);

            SetBaris(bfkali, "BF", "BOOKING FEE", bflama1, bflama2, bfhari1, bfhari2, bfjumlah, bfpersen);
            SetBaris(dpkali, "DP", "DP", dplama1, dplama2, dphari1, dphari2, dpjumlah, dppersen);
            SetBaris(angkali, "ANG", "ANGSURAN", anglama1, anglama2, anghari1, anghari2, angjumlah, angpersen);

            //potong booking fee harus dikontrol pada saat POSTBACK
            if(potongbf)                        
                PotongBF();

            //Hitung total tagihan
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
                        rpt.Rows[i].Cells[4].Text = Cf.Num(Math.Round(
                            Convert.ToDecimal(rpt.Rows[i].Cells[4].Text) - bfsatuan));
                    }
                }

            decimal Netto = Convert.ToDecimal(netto.Text);
            decimal Sum = 0;
            decimal NilaiTagihanTerakhir = 0;
            for (int n = 1; n < rpt.Rows.Count - 1; n++)
            {
                Sum = Sum + Convert.ToDecimal(rpt.Rows[n].Cells[4].Text);
                NilaiTagihanTerakhir = Netto - Sum;
            }
            int a = rpt.Rows.Count - 1;
            rpt.Rows[a].Cells[4].Text = Cf.Num(NilaiTagihanTerakhir.ToString());

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
                    decimal sisa = Convert.ToDecimal(netto.Text) - t;

                    c.Text = Cf.Num(sisa);
                }
            }
        }

        protected void insert_Click(object sender, System.EventArgs e)
        {
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            string c = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'ApprovCustomTagihan" + Project + "'");
            int nomor = Db.SingleInteger("SELECT COUNT(*) FROM MS_APPROVAL");
            nomor++;
            string NoApproval = nomor.ToString().PadLeft(7, '0');
            string Ket = "";
            if (c == "True")
            {
                for (int i = 1; i < rpt.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    string Tipe = rpt.Rows[i].Cells[1].Text;
                    string NamaTagihan = Cf.Str(rpt.Rows[i].Cells[2].Text);
                    DateTime TglJT = Convert.ToDateTime(rpt.Rows[i].Cells[3].Text);
                    decimal NilaiTagihan = Convert.ToDecimal(rpt.Rows[i].Cells[4].Text);

                    Db.Execute("EXEC spTagihanDaftarTEMP"
                        + " '" + NoKontrak + "'"
                        + ",'" + NamaTagihan + "'"
                        + ",'" + TglJT + "'"
                        + ", " + NilaiTagihan
                        + ",'" + Tipe + "'"
                        );
                }

                //INSERT KE MS_APPROVAL
                Db.Execute("EXEC spApproval"
                    + "'" + NoApproval + "'"
                    + ",'" + Str.Approval("7") + "'"//untuk customize
                    + ",'" + NoKontrak + "'"
                    + ",'" + Convert.ToDateTime(tgl.Text) + "'"
                    + ",'" + Project + "'"
                    );

                //insert siapa aja yang berhak approve ke ms_approval_detil 
                DataTable rs2 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 7 AND Project = '" + Project + "'");
                for (int i = 0; i < rs2.Rows.Count; i++)
                {
                    Db.Execute("EXEC spApprovalDetil"
                        + "'" + NoApproval + "'"
                        + ",'" + (i + 1) + "'"
                        + ",'" + rs2.Rows[i]["UserID"].ToString() + "'"//dari Textbox
                        + "," + rs2.Rows[i]["Lvl"]
                        + ",'" + Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + rs2.Rows[i]["UserID"].ToString() + "'") + "'"
                        );
                }
                                
                Db.Execute("EXEC spKontrakCustomTemp "
                    + " '" + NoApproval + "'"
                    + ",'" + NoKontrak + "'"
                    + ",'" + Db.SingleString("SELECT Skema FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'") + "'"
                    + ",'" + skema.Text + "'"
                    + ",'" + Db.SingleString("SELECT CaraBayar FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'") + "'"
                    + ",'" + carabayar2.SelectedValue + "'"
                    + ",'" + Convert.ToDateTime(tgl.Text) + "'"
                    );
                
                if (carabayar2.SelectedValue == "KPA")
                {
                    int NoUrutMax = Db.SingleInteger("SELECT MAX(NoUrut) FROM MS_TAGIHAN_TEMP WHERE NoKontrak = '" + NoKontrak + "' ");
                    Db.Execute("UPDATE MS_TAGIHAN_TEMP SET KPR = " + 1 + " WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" + (NoUrutMax) + "'");
                }

                DataTable rsDetail = Db.Rs("SELECT"
                    + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                    + ",MS_KONTRAK.NoUnit AS [Unit]"
                    + ",MS_CUSTOMER.Nama AS [Customer]"
                    + ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                    + ",MS_KONTRAK.Skema AS [Skema]"
                    + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                    + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                    + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                DataTable rsAft = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                    + "FROM MS_TAGIHAN_TEMP WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                Ket = Cf.LogCapture(rsDetail)
                    + Cf.LogList(rsAft, "JADWAL TAGIHAN");

            }
            else
            {
                for (int i = 1; i < rpt.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    string Tipe = rpt.Rows[i].Cells[1].Text;
                    string NamaTagihan = Cf.Str(rpt.Rows[i].Cells[2].Text);
                    DateTime TglJT = Convert.ToDateTime(rpt.Rows[i].Cells[3].Text);
                    decimal NilaiTagihan = Convert.ToDecimal(rpt.Rows[i].Cells[4].Text);

                    Db.Execute("EXEC spTagihanDaftar"
                        + " '" + NoKontrak + "'"
                        + ",'" + NamaTagihan + "'"
                        + ",'" + TglJT + "'"
                        + ", " + NilaiTagihan
                        + ",'" + Tipe + "'"
                        );
                }

                DataTable rs2 = Db.Rs("SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                if (rs2.Rows.Count > 0)
                {
                    Db.Execute("UPDATE MS_KONTRAK "
                        + " SET Skema = '" + skema.Text + "'"
                        + ", CaraBayar = '" + carabayar2.SelectedValue + "'"
                        + ", ApprovalCustomTagihan = 0"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );
                }

                if (carabayar2.SelectedValue == "KPR")
                {
                    int NoUrutMax = Db.SingleInteger("SELECT MAX(NoUrut) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ");
                    Db.Execute("UPDATE MS_TAGIHAN SET KPR = " + 1 + " WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" + (NoUrutMax) + "'");
                }

                decimal NilaiKontrak = Db.SingleDecimal("SELECT NilaiKontrak FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                //string skema = Db.SingleString("SELECT Skema FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                string carabayar = Db.SingleString("SELECT CaraBayar FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");

                DataTable rsAft = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                    + "FROM MS_TAGIHAN_TEMP WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                Ket = Cf.LogCapture(rs2)
                    + "<br>Nilai Kontrak : " + Cf.Num(NilaiKontrak)
                    + "<br>Tgl. Batal : " + Cf.Day(DateTime.Today)
                    + "<br>Skema : " + skema
                    + "<br>Cara Bayar : " + carabayar
                    + Cf.LogList(rsAft, "JADWAL TAGIHAN")
                    ;
            }
            Db.Execute("EXEC spLogKontrak"
                + " 'CUSTOM'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + NoKontrak + "'"
                );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Func.CekKomisi(NoKontrak);

                if (alokasi.Checked)
                {
                    if (dariDaftar.Checked)
                        Response.Redirect("Alokasi.aspx?custom=1&dd=1&NoKontrak=" + NoKontrak);
                    else
                        Response.Redirect("Alokasi.aspx?custom=1&NoKontrak=" + NoKontrak);
                }
                else
                {
                    if (dariDaftar.Checked)
                        Response.Redirect("KontrakDaftar3.aspx?NoKontrak=" + NoKontrak + "&done=1");
                    else
                        Response.Redirect("TagihanCustom.aspx?done=" + NoKontrak);
                }

        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak1.Text);
            }
        }

        #region private static decimal RoundThousand(decimal input)
        private static decimal RoundThousand(decimal input)
        {
            if (input < 1000)
            {
                return input;
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

        //protected void skema_SelectedIndexChanged(object sender, EventArgs e)
        //{            
        //    string Jenis = Db.SingleString("SELECT Jenis FROM REF_SKEMA WHERE Nomor = '" + skema.SelectedValue + "'");
        //    carabayar2.SelectedValue = Jenis;

        //    int bfdp = Db.SingleInteger("SELECT COUNT(BF) FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'DP' AND BF=1");
        //    int bfang = Db.SingleInteger("SELECT COUNT(BF) FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'ANG' AND BF=1");
            
        //    dp1potong.Checked = dpspotong.Checked = ang1potong.Checked = angspotong.Checked = tidakpotong.Checked = false;

        //    if (bfdp == 0 && bfang == 0)
        //        tidakpotong.Checked = true;
        //    else if (bfang == 1)
        //        ang1potong.Checked = true;
        //    else if (bfdp == 1)
        //        dp1potong.Checked = true;
        //    else if (bfdp > 1)
        //        dpspotong.Checked = true;
        //    else if (bfang > 1)
        //        angspotong.Checked = true;

        //    FillRumus();
        //}
    }
}
