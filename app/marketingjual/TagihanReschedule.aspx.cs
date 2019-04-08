using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace ISC064.MARKETINGJUAL
{
    public partial class TagihanReschedule : System.Web.UI.Page
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
                Js.Confirm(this, "Lanjutkan proses reschedule tagihan?");
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
                        + "Reschedule Tagihan Berhasil..."
                        + "</a>";
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

            netto.Attributes["style"] = "border:0px;font:bold";
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");

            int d = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");

            int e = Db.SingleInteger(
               "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND StatusSP3K='SELESAI' AND StatusAkad='SELESAI'");

            if (c == 0 || d == 0 || e != 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Kontrak tersebut sudah diperpanjang.\\n"
                    + "4. Jadwal tagihan belum pernah dikeluarkan.\\n"
                    + "5. Kontrak tersebut sudah SP3K dan AKAD."
                    , "document.getElementById('nokontrak1').focus();"
                    + "document.getElementById('nokontrak1').select();"
                    );

            return x;
        }

        private void LoadKontrak()
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                Fill();
            }
            else
            {
                backbtn.Visible = true;
                Js.Focus(this, nokontrak1);
                frm.Visible = false;
            }
        }

        private void BindSkema()
        {
            //Skema
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            DataTable rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' AND Project ='" + Project + "' ORDER BY Nama");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Nomor"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                skema.Items.Add(new ListItem(t, v));
            }

        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                int RESC = Db.SingleInteger("Select FlagReschedule From MS_KONTRAK Where NoKontrak = '" + NoKontrak + "'");
                if (RESC == 0)
                {
                    pilih.Visible = false;
                    frm.Visible = true;
                    Fill();
                }
                else
                {
                    pilih.Visible = true;
                    resc.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Tagihan Ini Belum di Approval...";
                    resc.Attributes["style"] = "background-color:white;color:red;";
                }
            }
        }

        private void FillRumus()
        {
            int BF = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'BF'");
            int DP = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'DP'");
            int ANG = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'ANG'");
            decimal BFJumlah = Db.SingleDecimal("SELECT Nominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'BF'");
            decimal DPJumlah = Db.SingleDecimal("SELECT Nominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'DP'");
            decimal ANGJumlah = Db.SingleDecimal("SELECT Nominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'ANG'");
            string BFTipe = Db.SingleString("SELECT TipeNominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'BF'");
            string DPTipe = Db.SingleString("SELECT TipeNominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'DP'");
            string ANGTipe = Db.SingleString("SELECT TipeNominal FROM REF_SKEMA_DETAIL WHERE Nomor = '" + skema.SelectedValue + "' AND Tipe = 'ANG'");

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
        private void Fill()
        {
            Func.KontrakHeader(NoKontrak, nokontrak2, unit, nama, agent);

            DataTable rs = Db.Rs(
                "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                decimal NilaiKontrak = Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]);
                decimal TotalLunas = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan), 0)"
                    + " FROM MS_PELUNASAN a"
                    + " INNER JOIN MS_TAGIHAN b ON a.NoTagihan = b.NoUrut AND a.NoKontrak = b.NoKontrak"
                    + " WHERE a.NoKontrak = '" + NoKontrak + "'"
                    + " AND b.Tipe <> 'ADM'"
                    );
                decimal SisaTagihan = NilaiKontrak - TotalLunas;
                //Skema
                BindSkema();
                int refSkema = Db.SingleInteger("SELECT RefSkema FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "' ");
                if (refSkema > 0)
                {
                    skema.SelectedValue = refSkema.ToString();
                }
                else
                {
                    skema.Items.Add(new ListItem(rs.Rows[0]["Skema"].ToString(), "0")); //cara bayar customize
                    skema.SelectedValue = refSkema.ToString();
                }

                carabayar2.SelectedValue = rs.Rows[0]["CaraBayar"].ToString();

                decimal persenDP = Db.SingleDecimal("SELECT ISNULL(SUM(Nominal),0) FROM REF_SKEMA_DETAIL WHERE Nomor='" + refSkema + "' AND Tipe='DP'");
                decimal persenANG = Db.SingleDecimal("SELECT ISNULL(SUM(Nominal),0) FROM REF_SKEMA_DETAIL WHERE Nomor='" + refSkema + "' AND Tipe='ANG'");

                FillRumus();

                netto.Text = Cf.Num(SisaTagihan);
                tgl.Text = Cf.Day(rs.Rows[0]["TglKontrak"]);
            }
        }

        private bool Check()
        {
            string s = "";
            bool x = true;


            if (dppersen.Checked && angpersen.Checked)
            {
                decimal akum = Convert.ToDecimal(dpjumlah.Text) + Convert.ToDecimal(angjumlah.Text);
                if (akum > 100 || akum < 100)
                {
                    x = false;
                    Js.Alert(this, "Akumulasi DP dan Angsuran harus 100%", "");
                }
            }
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

            if (dp1potong.Checked)
            {
                if (dpkali.Text != "0")
                {
                    decimal DP = Convert.ToDecimal(dpjumlah.Text);
                    decimal BF = Convert.ToDecimal(bfjumlah.Text);
                    if (bfpersen.Checked) BF = Convert.ToDecimal(bfjumlah.Text) * Convert.ToDecimal(netto.Text) / 100;
                    if (dppersen.Checked) DP = Convert.ToDecimal(dpjumlah.Text) * Convert.ToDecimal(netto.Text) / 100 / Convert.ToDecimal(dpkali.Text);

                    if (DP < BF)
                    {
                        x = false;
                        Js.Alert(this, "DP tidak boleh kurang dari Booking Fee", "");
                    }
                }
            }

            if (ang1potong.Checked)
            {
                decimal ANG = Convert.ToDecimal(angjumlah.Text);
                decimal BF = Convert.ToDecimal(bfjumlah.Text);
                if (bfpersen.Checked) BF = Convert.ToDecimal(bfjumlah.Text) * Convert.ToDecimal(netto.Text) / 100;
                if (angpersen.Checked) ANG = Convert.ToDecimal(angjumlah.Text) * Convert.ToDecimal(netto.Text) / 100 / Convert.ToDecimal(angkali.Text);

                if (ANG < BF)
                {
                    x = false;
                    Js.Alert(this, "Angsuran tidak boleh kurang dari Booking Fee", "");
                }
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
                else if (dpspotong.Checked && Convert.ToInt32(dpkali.Text) == 1)
                {
                    x = false;
                    if (s == "") s = dpkali.ID;
                    cc.Text = "DP 1 kali";
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
                frm.Visible = false;
                hasil.Visible = true;

                Js.Confirm(this, "Lanjutkan proses reschedule tagihan?");

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
            decimal JumNom = 0;
            decimal Nominal2 = 0;
            decimal Nominal3 = 0;

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
                
                decimal Nominal4 = 0;
                decimal Nominal5 = 0;
                

                if (persen.Checked) Nominal = Netto * (Nominal / 100);


                //if (carabayar2.SelectedValue == "KPR")
                //{
                //    if (Tipe == "ANG")
                //    {
                //        if (i == count - 1)
                //        {
                //            Nominal = Math.Round(Nominal / count);
                //        }
                //        else
                //        {
                //            Nominal = RoundThousand(Nominal / count);
                //        }
                //    }
                //    else
                //    {
                //        Nominal = RoundThousand(Nominal / count);
                //    }
                //}
                //else
                //{
                //    Nominal = RoundThousand(Nominal / count);
                //}


                //if (rounding.Checked)
                //{
                //    //Nominal = RoundThousand(Nominal / count);
                //    Nominal2 = RoundThousand(Nominal / count);
                //    Nominal3 = Math.Round(Nominal / count);
                //    Nominal4 = Nominal2 - Nominal3;
                //    JumNom += Nominal4;

                //    if(Nama == "KPR")
                //    {
                //        Nominal = Nominal2 - JumNom;
                //    }
                //    else
                //    {
                //        Nominal = Nominal2 - JumNom;
                //    }
                //}
                //else
                //{
                //    Nominal = Math.Round(Nominal / count);
                //}

                if (rounding.Checked)
                {
                    Nominal2 = RoundThousand(Nominal / count);
                    Nominal3 = Math.Round(Nominal / count);

                    Nominal = Nominal3 - Nominal2;
                }
                else
                {
                    Nominal = Math.Round(Nominal / count);
                }

                c = new TableCell();
                c.Text = Cf.Num(Nominal3);
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

                //JumNom += Nominal;
                //Response.Write(JumNom);
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
            Func.KontrakHeader(NoKontrak, nokontrakdetail, unitdetail, namadetail, agent);
            
            SetBaris(bfkali, "BF", "BOOKING FEE", bflama1, bflama2, bfhari1, bfhari2, bfjumlah, bfpersen);
            SetBaris(dpkali, "DP", "DP", dplama1, dplama2, dphari1, dphari2, dpjumlah, dppersen);
            //SetBaris(angkali, "ANG", "ANGSURAN", anglama1, anglama2, anghari1, anghari2, angjumlah, angpersen);

            int kpr = Db.SingleInteger("select count(*) from REF_SKEMA_DETAIL where KPR = 1 AND Nomor = " + skema.SelectedValue);
            if (kpr != 0)
            {
                SetBaris(angkali, "ANG", "KPR", anglama1, anglama2, anghari1, anghari2, angjumlah, angpersen);
            }
            else
            {
                SetBaris(angkali, "ANG", "ANGSURAN", anglama1, anglama2, anghari1, anghari2, angjumlah, angpersen);
            }

            Response.Write(dppersen.Text);

            //potong booking fee harus dikontrol pada saat POSTBACK
            if (potongbf)
                PotongBF();

        }

        private void PotongBF()
        {
            decimal totalbf = 0;
            int minbf = 0;
            if (tidakpotong.Checked == false)
            {

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
                        rpt.Rows[i].Cells[4].Text = Cf.Num(Convert.ToDecimal(rpt.Rows[i].Cells[4].Text) - bfsatuan);
                    }
                }
            }

            //decimal Sum = 0;
            //decimal NilaiTagihanTerakhir = 0;
            //decimal Netto = Convert.ToDecimal(netto.Text);

            //for (int n = 1; n < rpt.Rows.Count - 1; n++)
            //{
            //    Sum = Sum + Convert.ToDecimal(rpt.Rows[n].Cells[4].Text);
            //    NilaiTagihanTerakhir = Netto - Sum;
            //}

            //int a = rpt.Rows.Count - 1;
            //rpt.Rows[a].Cells[4].Text = Cf.Num(NilaiTagihanTerakhir.ToString());
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
            string k = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'ApprovReschedule" + Project + "'");
            if (k == "True")
            {
                int nomor = Db.SingleInteger("SELECT COUNT(*) FROM MS_APPROVAL");
                nomor++;
                string NoApproval = nomor.ToString().PadLeft(7, '0');
                //nyimpen tagihan sebelumnya dulu - untuk laporan custom tagihan
                int TagihanSebelumnya = Db.SingleInteger("select ISNULL(MAX(TagihanKe),0) + 1 from ms_tagihan_laporan where NoKontrak = '" + NoKontrak + "'");

                //decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
                int count = Db.SingleInteger("SELECT COUNT (*) FROM MS_APPROVAL_RESCHEDULE WHERE NoKontrak ='" + NoKontrak + "' AND NoApproval IN (SELECT NoApproval FROM MS_APPROVAL WHERE Sumber = '" + Str.Approval("6") + "' AND Status <> 'DONE')");
                if (count > 0)
                {
                    //nostockc.Text = "Unit Tidak Valid";

                    Js.Alert(
                        this
                        , "NoKontrak Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. NoKontrak tidak ada.\\n"
                        , "document.getElementById('nostock').focus();"
                        + "document.getElementById('nostock').select();"
                        );
                }
                else
                {

                    //INSERT KE MS_APPROVAL
                    Db.Execute("EXEC spApproval"
                        + "'" + NoApproval + "'"
                        + ",'" + Str.Approval("6") + "'"//untuk ganti unit
                        + ",'" + NoKontrak + "'"
                        + ",'" + Convert.ToDateTime(tgl.Text) + "'"
                        + ",'" + Project + "'"
                        );

                    //insert siapa aja yang berhak approve ke ms_approval_detil 
                    DataTable rs1 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 6 AND Project = '" + Project + "'");
                    for (int i = 0; i < rs1.Rows.Count; i++)
                    {
                        Db.Execute("EXEC spApprovalDetil"
                            + "'" + NoApproval + "'"
                            + ",'" + (i + 1) + "'"
                            + ",'" + rs1.Rows[i]["UserID"].ToString() + "'"//dari Textbox
                            + "," + rs1.Rows[i]["Lvl"]
                            + ",'" + Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + rs1.Rows[i]["UserID"].ToString() + "'") + "'"
                            );
                    }

                    string strSql = "SELECT * FROM MS_TAGIHAN where NoKontrak = '" + NoKontrak + "'";
                    DataTable rs2 = Db.Rs(strSql);
                    for (int i = 0; i < rs2.Rows.Count; i++)
                    {
                        int RefSkema = Db.SingleInteger("select RefSkema from ms_kontrak where NoKontrak = '" + NoKontrak + "'");
                        string SkemaPrev = Db.SingleString("select Nama from Ref_skema where Nomor = '" + RefSkema + "'");
                        decimal NilaiKontrakBef = Db.SingleDecimal("select NilaiKontrak from ms_kontrak where NoKontrak = '" + NoKontrak + "'");
                        Db.Execute("EXEC spTagihanDaftar_Laporan"
                            + "'" + NoApproval + "'"
                            + ",'" + rs2.Rows[i]["NoKontrak"].ToString() + "'"
                            + ",'" + rs2.Rows[i]["NamaTagihan"].ToString() + "'"
                            + ",'" + Cf.Day(rs2.Rows[i]["TglJT"]) + "'"
                            + ",'" + Cf.Num(rs2.Rows[i]["NilaiTagihan"]) + "'"
                            + ",'" + rs2.Rows[i]["Tipe"].ToString() + "'"
                            + ",'" + TagihanSebelumnya + "'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + DateTime.Now + "'"
                            + ",'" + SkemaPrev + "'"
                            + ",'" + NilaiKontrakBef + "'"
                            );
                    }

                    //sampai disini nyimpennya.
                    string TipeSkema = "";
                    if (skema.SelectedIndex > 0)
                        TipeSkema = Db.SingleString("SELECT Jenis FROM REF_SKEMA WHERE Nomor='" + skema.SelectedValue + "'");

                    //Log Before
                    DataTable rsBef = Db.Rs("SELECT "
                            + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                            + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                    DataTable caraBef = Db.Rs("SELECT CaraBayar AS [Cara Bayar] FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");
                    string CaraBayarLama = Db.SingleString("SELECT CaraBayar FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");

                    //Db.Execute("DELETE FROM MS_TAGIHAN where nourut not in (select notagihan from ms_pelunasan where NoKontrak = '" + NoKontrak + "' AND NilaiPelunasan > 0) AND Nokontrak = '" + NoKontrak + "' AND Tipe != 'ADM'");

                    int RefSkema2 = Db.SingleInteger("select RefSkema from ms_kontrak where NoKontrak = '" + NoKontrak + "'");
                    string SkemaPrev2 = Db.SingleString("select Nama from Ref_skema where Nomor = '" + RefSkema2 + "'");

                    //insert perubahan jadwal tagihan ke ms_approval_reschedule
                    Db.Execute("EXEC spTagihanDaftarTempRE"
                        + " '" + NoApproval + "'"
                        + ",'" + NoKontrak + "'"
                        + ",'" + Convert.ToDateTime(tgl.Text) + "'"
                        + ",'" + SkemaPrev2 + "'"
                        + ",'" + TipeSkema + "'"
                        + ",'" + CaraBayarLama + "'"
                        + ",'" + carabayar2.SelectedValue + "'"
                        );

                    int TagihanKe = Db.SingleInteger("select ISNULL(MAX(TagihanKe),0) + 1 from MS_TAGIHAN_LAPORAN where NoKontrak = '" + NoKontrak + "'");
                    decimal TotalTagihanBef = 0;
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

                        //tagihan laporan
                        decimal NilaiKontrakAft = Db.SingleDecimal("select NilaiKontrak from ms_kontrak where NoKontrak = '" + NoKontrak + "'");
                        string SkemaFull = Db.SingleString("select Nama from Ref_skema where Nomor = '" + skema.SelectedValue + "'");
                        Db.Execute("EXEC spTagihanDaftar_Laporan"
                            + " '" + NoApproval + "'"
                            + ",'" + NoKontrak + "'"
                            + ",'" + NamaTagihan + "'"
                            + ",'" + TglJT + "'"
                            + ", " + NilaiTagihan
                            + ",'" + Tipe + "'"
                            + ",'" + TagihanKe + "'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + DateTime.Now + "'"
                            + ",'" + SkemaFull + "'"
                            + ",'" + NilaiKontrakAft + "'"
                            );
                        if (i < rpt.Rows.Count - 1)
                        {
                            TotalTagihanBef += NilaiTagihan;
                        }
                    }

                    Db.Execute("UPDATE MS_KONTRAK SET FlagReschedule = 1, ApprovelReschedule = 1 WHERE NoKontrak ='" + NoKontrak + "'");

                    int NoUrutMax = Db.SingleInteger("SELECT MAX(NoUrut) FROM MS_TAGIHAN_TEMP WHERE NoKontrak = '" + NoKontrak + "' ");

                    //    //decimal A = Db.SingleDecimal("select Isnull(sum(Nilaitagihan),0) from ms_tagihan where NoKontrak = '" + NoKontrak + "' and NoUrut IN (select NoTagihan from ms_pelunasan where NoKontrak = '" + NoKontrak + "')");
                    //    //decimal B = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM MS_PELUNASAN a INNER JOIN MS_TAGIHAN b ON a.NoTagihan = b.NoUrut AND a.NoKontrak = b.NoKontrak WHERE a.NoKontrak = '" + NoKontrak + "' AND b.Tipe <> 'ADM'");
                    //    //decimal C = A - B;

                    //    //decimal NilaiKontrak = Db.SingleDecimal("SELECT NilaiKontrak FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' ");
                    //    //decimal NilaiLunas = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' ");
                    //    //decimal NilaiLunasVoid = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN where nourut in (select notagihan from ms_pelunasan where NoKontrak = '" + NoKontrak + "' AND NilaiPelunasan = 0) AND Nokontrak = '" + NoKontrak + "'");
                    //    //decimal NilaiTagihanTerakhir = NilaiKontrak - NilaiLunas - TotalTagihanBef - NilaiLunasVoid;
                    //    //decimal NilaiterakhirBaru = NilaiTagihanTerakhir - C;

                    Db.Execute("UPDATE MS_TAGIHAN_TEMP SET KPR = " + 1 + " WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" + (NoUrutMax) + "'");

                    string Skema = Cf.Str(skema.SelectedItem.Text);
                    Db.Execute("UPDATE MS_KONTRAK SET Revisi = Revisi + 1 WHERE NoKontrak = '" + NoKontrak + "'");
                    int NC = Db.SingleInteger("SELECT NoCustomer FROM MS_Kontrak WHERE NoKontrak = '" + NoKontrak + "' ");
                    int NA = Db.SingleInteger("SELECT NoAgent FROM MS_Kontrak WHERE NoKontrak = '" + NoKontrak + "' ");
                    string NU = Db.SingleString("SELECT NoUnit FROM MS_Kontrak WHERE NoKontrak = '" + NoKontrak + "' ");

                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_TAGIHAN_HEADER WHERE NoKontrak = '" + NoKontrak + "'");
                    if (c == 0)
                    {

                        Db.Execute("EXEC spTagihanHeaderTemp"
                           + " '" + NoKontrak + "'"
                           + ",'" + Skema + "'"
                           + ",'" + NC + "'"
                           + ",'" + NU + "'"
                           + ",'" + NA + "'"
                           + ",'" + carabayar2.SelectedValue + "'"
                           //+ ",'  + Revisi + 1 + '"
                           + ", " + skema.SelectedValue
                           );
                    }
                    else
                    {
                        Db.Execute("UPDATE MS_TAGIHAN_HEADER"
                                + " SET Skema = '" + Skema + "'"
                                + " ,NoCustomer = '" + NC + "'"
                                + " ,NoUnit = '" + NU + "'"
                                + " ,NoAgent = '" + NA + "'"
                                + " ,CaraBayar = '" + carabayar2.SelectedValue + "'"
                                + " ,RefSkema = '" + skema.SelectedValue + "'"
                                + " WHERE NoKontrak = '" + NoKontrak + "'"
                                );
                    }
                    DataTable rsDetail = Db.Rs("SELECT"
                        + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                        + ",MS_KONTRAK.NoUnit AS [Unit]"
                        + ",MS_CUSTOMER.Nama AS [Customer]"
                        + ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                        + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                        + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                        + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "' ");

                    DataTable rsAft = Db.Rs("SELECT "
                        + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                        + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                    DataTable caraAft = Db.Rs("SELECT CaraBayar AS [Cara Bayar] FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");
                    string CaraBayarBaru = Db.SingleString("SELECT CaraBayar FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");

                    string Ket = Cf.LogCapture(rsDetail)
                        + Cf.LogList(rsBef, rsAft, "JADWAL TAGIHAN");

                    string Ket2 = Cf.LogCompare(caraBef, caraAft);

                    Db.Execute("EXEC spLogKontrak"
                        + " 'APR-RE'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    if (CaraBayarLama != CaraBayarBaru)
                    {
                        Db.Execute("EXEC spLogKontrak"
                             + " 'PCB'"
                             + ",'" + Act.UserID + "'"
                             + ",'" + Act.IP + "'"
                             + ",'" + Ket2 + "'"
                             + ",'" + NoKontrak + "'"
                             );

                        decimal LogID2 = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                        Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID2);

                    }


                    Db.Execute("UPDATE MS_KONTRAK SET OutBalance = '0' WHERE NoKontrak = '" + NoKontrak + "' ");

                    if (alokasi.Checked)
                    {
                        if (dariDaftar.Checked)
                            Response.Redirect("Alokasi.aspx?dd=1&NoKontrak=" + NoKontrak);
                        else
                            Response.Redirect("Alokasi.aspx?NoKontrak=" + NoKontrak);
                    }
                    else
                    {
                        if (dariDaftar.Checked)
                            Response.Redirect("KontrakDaftar3.aspx?NoKontrak=" + NoKontrak + "&done=1");
                        else
                            Response.Redirect("TagihanReschedule.aspx?done=" + NoKontrak);
                    }
                }
            }
            else
            {
                int TagihanSebelumnya = Db.SingleInteger("select ISNULL(MAX(TagihanKe),0) + 1 from ms_tagihan_laporan where NoKontrak = '" + NoKontrak + "'");
                int nomor = Db.SingleInteger("SELECT COUNT(*) FROM MS_APPROVAL");
                nomor++;
                string NoApproval = nomor.ToString().PadLeft(7, '0');

                //INSERT KE MS_APPROVAL
                Db.Execute("EXEC spApproval"
                    + "'" + NoApproval + "'"
                    + ",'" + Str.Approval("6") + "'"//untuk ganti unit
                    + ",'" + NoKontrak + "'"
                    + ",'" + DateTime.Today + "'"
                    + ",'" + Project + "'"
                    );

                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL SET Status = 'DONE'"
                    //+ ",TglApproval = '" + DateTime.Today + "'"
                    + " WHERE NoApproval = '" + NoApproval + "'"
                    );

                int RefSkema2 = Db.SingleInteger("select RefSkema from ms_kontrak where NoKontrak = '" + NoKontrak + "'");
                string SkemaPrev2 = Db.SingleString("select Nama from Ref_skema where Nomor = '" + RefSkema2 + "'");
                string CaraBayarLama2 = Db.SingleString("SELECT CaraBayar FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");

                //insert perubahan jadwal tagihan ke ms_approval_reschedule
                Db.Execute("EXEC spTagihanDaftarTempRE"
                    + "'" + NoApproval + "'"
                    + ",'" + NoKontrak + "'"
                    + ",'" + Convert.ToDateTime(tgl.Text) + "'"
                    + ",'" + SkemaPrev2 + "'"
                    + ",'" + skema.SelectedValue + "'"
                    + ",'" + CaraBayarLama2 + "'"
                    + ",'" + carabayar2.SelectedValue + "'"
                    );

                string strSql1 = "SELECT * FROM MS_TAGIHAN where NoKontrak = '" + NoKontrak + "'";
                DataTable rs2 = Db.Rs(strSql1);
                for (int i = 0; i < rs2.Rows.Count; i++)
                {
                    int RefSkema = Db.SingleInteger("select RefSkema from ms_kontrak where NoKontrak = '" + NoKontrak + "'");
                    string SkemaPrev = Db.SingleString("select Nama from Ref_skema where Nomor = '" + RefSkema + "'");
                    decimal NilaiKontrakBef = Db.SingleDecimal("select NilaiKontrak from ms_kontrak where NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("EXEC spTagihanDaftar_Laporan"
                        + " '" + NoApproval + "'"
                        + ",'" + rs2.Rows[i]["NoKontrak"].ToString() + "'"
                        + ",'" + rs2.Rows[i]["NamaTagihan"].ToString() + "'"
                        + ",'" + Cf.Day(rs2.Rows[i]["TglJT"]) + "'"
                        + ",'" + Cf.Num(rs2.Rows[i]["NilaiTagihan"]) + "'"
                        + ",'" + rs2.Rows[i]["Tipe"].ToString() + "'"
                        + ",'" + TagihanSebelumnya + "'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + DateTime.Now + "'"
                        + ",'" + SkemaPrev + "'"
                        + ",'" + NilaiKontrakBef + "'"
                        );
                }

                DataTable rsBef = Db.Rs("SELECT "
                        + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                        + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                DataTable caraBef = Db.Rs("SELECT CaraBayar AS [Cara Bayar] FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");

                Db.Execute("DELETE FROM MS_TAGIHAN where nourut not in (select notagihan from ms_pelunasan where NoKontrak = '" + NoKontrak + "' AND NilaiPelunasan > 0) AND Nokontrak = '" + NoKontrak + "' AND Tipe != 'ADM'");
                //Db.Execute("DELETE FROM MS_TAGIHAN where nourut not in (select notagihan from ms_pelunasan where NoKontrak = '" + NoKontrak + "') AND Nokontrak = '" + NoKontrak + "' AND Tipe <> 'ADM'");

                //string strSql = "SELECT * FROM MS_TAGIHAN_TEMP WHERE NoKontrak = '" + NoKontrak + "'"
                //+ " ORDER BY NoUrut";

                //DataTable rs = Db.Rs(strSql);
                //Rpt.NoData(rptTagihanNew, rs, "No Reservartion.");

                decimal TotalTagihanBef = 0;
                for (int i = 1; i < rpt.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    string Tipe = rpt.Rows[i].Cells[1].Text;
                    string NamaTagihan = rpt.Rows[i].Cells[2].Text;
                    DateTime TglJT = Convert.ToDateTime(rpt.Rows[i].Cells[3].Text);
                    decimal NilaiTagihan = Convert.ToDecimal(rpt.Rows[i].Cells[4].Text);

                    Db.Execute("EXEC spTagihanDaftar"
                        + " '" + NoKontrak + "'"
                        + ",'" + NamaTagihan + "'"
                        + ",'" + TglJT + "'"
                        + ", " + NilaiTagihan
                        + ",'" + Tipe + "'"
                        );

                    if (i < rpt.Rows.Count - 1)
                    {
                        TotalTagihanBef += NilaiTagihan;
                    }
                }

                //DateTime Tgl = Convert.ToDateTime(tglot.Text);
                string Skema = Cf.Str(skema.SelectedItem.Text);
                Db.Execute("UPDATE MS_KONTRAK SET Revisi = Revisi + 1 WHERE NoKontrak = '" + NoKontrak + "'");
                int NC = Db.SingleInteger("SELECT NoCustomer FROM MS_Kontrak WHERE NoKontrak = '" + NoKontrak + "' ");
                int NA = Db.SingleInteger("SELECT NoAgent FROM MS_Kontrak WHERE NoKontrak = '" + NoKontrak + "' ");
                string NU = Db.SingleString("SELECT NoUnit FROM MS_Kontrak WHERE NoKontrak = '" + NoKontrak + "' ");

                int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_TAGIHAN_HEADER WHERE NoKontrak = '" + NoKontrak + "'");
                if (c == 0)
                {

                    Db.Execute("EXEC spTagihanHeaderTemp"
                       + " '" + NoKontrak + "'"
                       + ",'" + Skema + "'"
                       + ",'" + NC + "'"
                       + ",'" + NU + "'"
                       + ",'" + NA + "'"
                       + ",'" + carabayar2.SelectedValue + "'"
                       //+ ",'  + Revisi + 1 + '"
                       + ", " + skema.SelectedValue
                       );
                }
                else
                {
                    Db.Execute("UPDATE MS_TAGIHAN_HEADER"
                            + " SET Skema = '" + Skema + "'"
                            + " ,NoCustomer = '" + NC + "'"
                            + " ,NoUnit = '" + NU + "'"
                            + " ,NoAgent = '" + NA + "'"
                            + " ,CaraBayar = '" + carabayar2.SelectedValue + "'"
                            + " ,RefSkema = '" + skema.SelectedValue + "'"
                            + " WHERE NoKontrak = '" + NoKontrak + "'"
                            );
                }

                string CB = Db.SingleString("Select CaraBayar From MS_TAGIHAN_HEADER WHERE NoKontrak ='" + NoKontrak + "'");
                //string Skema = Db.SingleString("Select Skema From MS_TAGIHAN_HEADER WHERE NoKontrak ='" + NoKontrak + "'");
                Db.Execute("UPDATE MS_TAGIHAN_HEADER SET ApprovelReschedule = 1 WHERE NoKontrak ='" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK SET FlagReschedule = 0, ApprovelReschedule = 0 WHERE NoKontrak ='" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK SET CaraBayar = '" + CB + "', Skema = '" + Skema + "' WHERE NoKontrak = '" + NoKontrak + "'");

                int NoUrutMax = Db.SingleInteger("SELECT ISNULL(MAX(NoUrut),0) FROM MS_TAGIHAN_TEMP WHERE NoKontrak = '" + NoKontrak + "' ");

                if (CB == "KPR")
                {
                    Db.Execute("Update Ms_Tagihan Set KPR = " + 1 + " Where NoKontrak = '" + NoKontrak + "' And NamaTagihan = 'PENCAIRAN KPR'"); ;//Nourut = '" + NoUrutMax + "'");
                }

                //decimal NilaiKontrak = Db.SingleDecimal("SELECT NilaiKontrak FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' ");
                //decimal NilaiLunas = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' ");
                //decimal NilaiLunasVoid = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN_TEMP where nourut in (select notagihan from ms_pelunasan where NoKontrak = '" + NoKontrak + "' AND NilaiPelunasan = 0) AND Nokontrak = '" + NoKontrak + "'");
                //decimal NilaiTagihanTerakhir = NilaiKontrak - NilaiLunas - TotalTagihanBef - NilaiLunasVoid;
                //if (NilaiTagihanTerakhir < 0)
                //{
                //    Db.Execute("UPDATE MS_TAGIHAN SET NilaiTagihan = NilaiTagihan + " + NilaiTagihanTerakhir + " WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" + (NoUrutMax - (int)1) + "'  ");
                //    Db.Execute("DELETE MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" + NoUrutMax + "'  ");
                //}
                //else
                //{
                //    Db.Execute("UPDATE MS_TAGIHAN SET NilaiTagihan = '" + NilaiTagihanTerakhir + "' WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" + NoUrutMax + "'  ");
                //}

                DataTable rsDetail = Db.Rs("SELECT"
                    + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                    + ",MS_KONTRAK.NoUnit AS [Unit]"
                    + ",MS_CUSTOMER.Nama AS [Customer]"
                    + ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                    + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                    + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                    + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "' ");

                DataTable rsAft = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                    + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                DataTable caraAft = Db.Rs("SELECT CaraBayar AS [Cara Bayar] FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");

                string Ket = Cf.LogCapture(rsDetail)
                    + Cf.LogList(rsBef, rsAft, "JADWAL TAGIHAN");

                string Ket2 = Cf.LogCompare(caraBef, caraAft);

                Db.Execute("EXEC spLogKontrak"
                    + " 'RESCHE'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoKontrak + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                if (alokasi.Checked)
                {
                    if (dariDaftar.Checked)
                        Response.Redirect("Alokasi.aspx?dd=1&NoKontrak=" + NoKontrak);
                    else
                        Response.Redirect("Alokasi.aspx?NoKontrak=" + NoKontrak);
                }
                else
                {
                    if (dariDaftar.Checked)
                        Response.Redirect("KontrakDaftar3.aspx?NoKontrak=" + NoKontrak + "&done=1");
                    else
                        Response.Redirect("TagihanReschedule.aspx?done=" + NoKontrak);
                }
            }
        }

        protected void skema_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CaraBayar = Db.SingleString(
                    "SELECT Jenis FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);

            carabayar2.Text = CaraBayar;
            FillRumus();
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
