using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakDaftar4 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                nolanjut.Visible = false;

                InitForm();
                Fill();
                FillWL();

                frm.Visible = false;

                Surcharge.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                Surcharge.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                Surcharge.Attributes["onblur"] = "CalcBlur(this);";

                //diskon2.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                //diskon2.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                //diskon2.Attributes["onblur"] = "CalcBlur(this);";
            }

            //if(frm.Visible) Js.Confirm(this, "Lanjutkan proses pendaftaran surat pesanan?");
        }

        private void InitForm()
        {
            tglkontrak.Text = Cf.Day(DateTime.Today);

            DataTable rs;

            //Cara bayar
            rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' ORDER BY Nama");
            carabayar.Items.Add(new ListItem("*** CUSTOMIZE / PENDING", "0")); //cara bayar customize

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Nomor"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                carabayar.Items.Add(new ListItem(t, v));
            }
            carabayar.SelectedIndex = 0;
            carabayar.Attributes["ondblclick"] = "kalk(this)";

            //persentingkat.Visible = false;
            persenBunga.Visible = false;
            noreservasi.Visible = false;

            //diskon.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            //diskon.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            //diskon.Attributes["onblur"] = "CalcBlur(this);";

            lsbunga.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            lsbunga.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            lsbunga.Attributes["onblur"] = "CalcBlur(this);";

            fo.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            fo.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            fo.Attributes["onblur"] = "CalcBlur(this);";

            focounter.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            focounter.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            focounter.Attributes["onblur"] = "CalcBlur(this);";
        }

        private void Fill()
        {
            cancel.Attributes["onclick"] = "location.href='KontrakDaftar2.aspx?NoStock=" + NoStock + "'";

            string strSql = "SELECT NoUnit, PriceListStandard, Luas "
                + " FROM MS_UNIT WHERE NoStock = '" + NoStock + "'"
                + " AND Status = 'A' AND FlagSPL = 1"; //cek kondisi
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
            {
                nolanjut.Visible = true;
                lanjut.Visible = false;
            }
            else
            {
                unit.Text = "<a href=\"javascript:popUnit('" + NoStock + "')\">"
                    + rs.Rows[0]["NoUnit"] + "</a>";

                decimal PriceListStandard = Convert.ToDecimal(rs.Rows[0]["PriceListStandard"]);
                decimal Luas = Convert.ToDecimal(rs.Rows[0]["Luas"]);
                string lantai = rs.Rows[0]["NoUnit"].ToString().Substring(3, 2);
                if (lantai == "08" || lantai == "09")
                {
                    trsurcharge.Visible = true;
                    Surcharge.Text = "8000000";
                }
                else
                {
                    trsurcharge.Visible = false;
                    Surcharge.Text = "0";
                }

                pl.Text = Cf.Num(PriceListStandard);
                luas.Text = Cf.Num(Luas);
            }
        }

        private void FillWL()
        {
            string strSql = "SELECT "
                + " NoUrut"
                + ",Tgl"
                + ",TglExpire"
                + ",NoQueue"
                + ",MS_CUSTOMER.Nama AS Cs"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
                + ",MS_RESERVASI.Status"
                + ",MS_RESERVASI.NoReservasi"
                + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
                + " WHERE NoStock = '" + NoStock + "' AND MS_RESERVASI.STATUS ='A'"
                + " ORDER BY NoUrut";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Unit tidak memiliki waiting list.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUrut"].ToString();
                c.Font.Size = 15;
                c.Font.Bold = true;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a href=\"javascript:popEditReservasi('" + rs.Rows[i]["NoReservasi"] + "')\">"
                    + rs.Rows[i]["NoReservasi"].ToString().PadLeft(5, '0') + "</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Date(rs.Rows[i]["TglExpire"])
                    + "<br><i>NUP : " + rs.Rows[i]["NoQueue"] + "</i>";
                if ((string)rs.Rows[i]["Status"] == "E")
                {
                    if ((int)rs.Rows[i]["NoUrut"] == 1)
                    {
                        expireinfo.Text = "Reservasi urutan pertama dalam kondisi expire. Silakan menghubungi supevisor.";
                        expireinfo.ForeColor = Color.Red;
                        next.Enabled = false;
                    }
                    c.Text = c.Text + "<br><b class=err>Expire</b>";
                }
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString()
                    + "<br>" + rs.Rows[i]["Ag"].ToString();
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }

            //belum ada waiting list tidak boleh lanjut.
            if (rs.Rows.Count == 0)
                next.Enabled = false;
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            pilih.Visible = false;
            frm.Visible = true;

            FillDataReservasi();

            Js.Focus(this, tglkontrak);
            Js.Confirm(this, "Lanjutkan proses pendaftaran surat pesanan?");
        }

        private void FillDataReservasi()
        {
            string strSql = "SELECT "
                + " MS_RESERVASI.NoReservasi"
                + ", MS_CUSTOMER.NoCustomer"
                + ",MS_CUSTOMER.Nama AS CS"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
                + ",MS_RESERVASI.Skema"
                + ",MS_RESERVASI.Netto"
                + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
                + " WHERE NoStock = '" + NoStock + "' AND NoUrut = 1";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                noreservasi.Text = rs.Rows[0]["NoReservasi"].ToString();
                customer.Text = "<a href=\"javascript:popEditCustomer('" + rs.Rows[0]["NoCustomer"] + "')\">"
                    + rs.Rows[0]["Cs"] + "</a>";

                agent.Text = rs.Rows[0]["Ag"].ToString();
                skema.Text = rs.Rows[0]["Skema"].ToString();
                nettorsv.Text = Cf.Num(rs.Rows[0]["Netto"]);
            }
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

            if (!Cf.isTgl(targetst))
            {
                x = false;
                if (s == "") s = targetst.ID;
                targetstc.Text = "Tanggal";
            }
            else
                targetstc.Text = "";

            if (JenisPPN.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = JenisPPN.ID;
                JenisPPNc.Text = "Pilih";
            }
            else
                JenisPPNc.Text = "";

            if (carabayar2.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = carabayar2.ID;
                carabayarc.Text = "Pilih salah satu jenis.";
            }
            else
                carabayarc.Text = "";

            if (!Cf.isMoney(fo))
            {
                x = false;
                if (s == "") s = fo.ID;
                foc.Text = "Angka";
            }
            else
                foc.Text = "";

            if (!Cf.isMoney(Surcharge))
            {
                x = false;
                if (s == "") s = Surcharge.ID;
                surchargec.Text = "Angka";
            }
            else
                surchargec.Text = "";

            if (diskon2.Text != "")
            {
                if (!Cf.isMoney(diskon2))
                {
                    x = false;
                    if (s == "") s = diskon2.ID;
                    diskon2c.Text = "Angka";
                }
                else
                    diskon2c.Text = "";
            }

            if (pl.Text == "0")
            {
                x = false;
                if (s == "") s = pl.ID;
                pricec.Text = "PriceListStandard tidak boleh 0";
            }

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Jenis Tanggungan PPN harus dipilih.\\n"
                    + "3. Price List tidak boleh bernilai 0.\\n"
                    + "4. Cara bayar harus dipilih."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                DateTime TglKontrak = Convert.ToDateTime(tglkontrak.Text);
                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                //Numerator
                nokontrak.Text = Numerator.SuratPesanan(TglKontrak.Month, TglKontrak.Year, Project);

                decimal nilaitagihan = Db.SingleDecimal("SELECT Netto FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "'");

                DateTime TargetST = Convert.ToDateTime(targetst.Text);
                string Skema = Cf.Str(carabayar.SelectedItem.Text);
                decimal surcharge = Convert.ToDecimal(Surcharge.Text);

                Db.Execute("EXEC spKontrakDaftar"
                    + " '" + NoKontrak + "'"
                    + ",'" + NoStock + "'"
                    + ",'" + TglKontrak + "'"
                    + ",'" + Skema + "'"
                    + ",'" + TargetST + "'"
                    );

                int KPR = 0;
                if (carabayar2.SelectedValue == "KPR")
                {
                    KPR = 1;
                }
                else
                {
                    KPR = 0;
                }


                //Manual update
                string sSQL = "UPDATE MS_KONTRAK"
                    + " SET JenisPPN = '" + JenisPPN.SelectedItem.Text + "'"
                    + ", JenisKPR = " + KPR
                    + ", CaraBayar = '" + carabayar2.SelectedValue + "'"
                    + ", RefSkema = '" + carabayar.SelectedValue + "'"
                    + ", Surcharge='" + surcharge + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    ;
                Db.Execute(sSQL);

                //Update Status Reservasi
                Db.Execute("UPDATE MS_RESERVASI SET Status='C' WHERE NoReservasi='" + NoReservasi + "'");

                int c = Db.SingleInteger("SELECT COUNT(NoKontrak) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                if (c == 0)
                {
                    nokontrak.Text = "#AUTO#";

                    Js.Alert(
                        this
                        , "Unit Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Unit sudah dijual kepada customer lain.\\n"
                        , "document.getElementById('tglkontrak').focus();"
                        + "document.getElementById('tglkontrak').select();"
                        );
                }
                else
                {
                    SaveTagihan();

                    int Count = Db.SingleInteger("SELECT COUNT(*) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");
                    if (Count == 0)
                    {

                    }
                    else
                    {
                        SaveFO();
                    }

                    DataTable rs = Db.Rs("SELECT "
                        + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                        + ",MS_KONTRAK.NoUnit AS [Unit]"
                        + ",MS_CUSTOMER.Nama AS [Customer]"
                        + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS [Agent]"
                        + ",CONVERT(varchar,MS_KONTRAK.TglKontrak,106) AS [Tanggal Kontrak]"
                        + ",MS_KONTRAK.NoStock AS [No. Stock]"
                        + ",MS_KONTRAK.Luas AS [Luas]"
                        + ",MS_KONTRAK.Gross AS [Nilai Gross]"
                        + ",MS_KONTRAK.DiskonRupiah AS [Diskon dalam Rupiah]"
                        + ",MS_KONTRAK.DiskonPersen AS [Diskon dalam Persen]"
                        + ",MS_KONTRAK.DiskonKet AS [Keterangan Diskon]"
                        + ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                        + ",MS_KONTRAK.Skema"
                        + ",CONVERT(varchar,MS_KONTRAK.TargetST,106) AS [Jadwal Serah Terima]"
                        + ", MS_KONTRAK.JenisPPN AS [PPN Ditanggung]"
                        + ", CASE MS_KONTRAK.JenisKPR"
                        + "		WHEN 0 THEN 'KPR'"
                        + "		WHEN 1 THEN 'NON-KPR'"
                        + "	END AS [Jenis KPR]"
                        + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                        + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );

                    DataTable rsTagihan = Db.Rs("SELECT "
                        + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                        + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                    //Logfile
                    string Ket = Cf.LogCapture(rs)
                        + Cf.LogList(rsTagihan, "JADWAL TAGIHAN");

                    Db.Execute("EXEC spLogKontrak"
                        + " 'DAFTAR'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");                    
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    //floor plan
                    string Peta = Db.SingleString("SELECT Peta "
                        + " FROM MS_UNIT INNER JOIN MS_KONTRAK ON MS_UNIT.NoStock = MS_KONTRAK.NoStock "
                        + " WHERE NoKontrak = '" + NoKontrak + "'");
                    Func.GenerateFP(Peta);

                    Response.Redirect("KontrakDaftar3.aspx?NoKontrak=" + NoKontrak);
                }
            }
        }

        private void SaveTagihan()
        {
            int CaraBayar = Convert.ToInt32(carabayar.SelectedValue);

            //cara bayar 0 = customize
            if (CaraBayar != 0)
            {
                string RumusDiskon = diskon2.Text;
                string RumusDiskon2 = Db.SingleString(
                    "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + CaraBayar);


                decimal Gross = Db.SingleDecimal(
                    "SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                decimal surcharge = Convert.ToDecimal(Surcharge.Text);
                decimal Gross2 = Gross + surcharge;//Gross + surcharge

                decimal GrossAfterDiskon = Func.SetelahDiskon(RumusDiskon, Gross2);
                decimal DPP = GrossAfterDiskon / (decimal)1.1;
                decimal NilaiPPN = 0;
                decimal NilaiKontrak = 0;
                decimal PPNDitanggungPemerintah = 0;

                if (JenisPPN.SelectedValue == "KONSUMEN")
                {
                    PPNDitanggungPemerintah = 0;
                    NilaiPPN = (DPP * (decimal)0.1) - PPNDitanggungPemerintah;
                    NilaiKontrak = DPP + NilaiPPN;

                }
                else
                {
                    PPNDitanggungPemerintah = DPP * (decimal)0.1;
                    NilaiPPN = ((DPP * (decimal)0.1)) - PPNDitanggungPemerintah;
                    NilaiKontrak = DPP + NilaiPPN;
                }

                Db.Execute("EXEC spKontrakDiskon"
                    + " '" + NoKontrak + "'"
                    + ", " + Gross2
                    + ", " + NilaiKontrak
                    + ", " + (Gross2 - GrossAfterDiskon)
                    + ",'" + RumusDiskon + "'"
                    + ",'" + Cf.Str(RumusDiskon2) + "'"
                    );

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET "
                    + " NilaiPPN = " + NilaiPPN
                    + " , PPNPemerintah = " + PPNDitanggungPemerintah
                    + " WHERE NoKontrak = '" + NoKontrak + "'");

                string[,] x = Func.Breakdown(CaraBayar, NilaiKontrak, Convert.ToDateTime(tglkontrak.Text));
                for (int i = 0; i <= x.GetUpperBound(0); i++)
                {
                    if (!Response.IsClientConnected) break;

                    Db.Execute("EXEC spTagihanDaftar"
                        + " '" + NoKontrak + "'"
                        + ",'" + x[i, 2] + "'"
                        + ",'" + Convert.ToDateTime(x[i, 3]) + "'"
                        + ", " + Convert.ToDecimal(x[i, 4])
                        + ",'" + x[i, 1] + "'"
                        );

                    //
                    int NoUrut = Db.SingleInteger("SELECT TOP 1 NoUrut FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut DESC");
                    Db.Execute("UPDATE MS_TAGIHAN"
                        + " SET KPR = " + x[i, 5]
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        + " AND NoUrut = " + NoUrut
                        );

                }

            }
            else
            {
                decimal Gross = Db.SingleDecimal(
                    "SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                decimal surcharge = Convert.ToDecimal(Surcharge.Text);
                decimal Gross2 = Gross + surcharge;

                decimal DPP = Gross2 / (decimal)1.1;
                decimal NilaiPPN = 0;
                decimal NilaiKontrak = 0;
                decimal PPNDitanggungPemerintah = 0;

                if (JenisPPN.SelectedValue == "KONSUMEN")
                {
                    PPNDitanggungPemerintah = 0;
                    NilaiPPN = (DPP * (decimal)0.1) - PPNDitanggungPemerintah;
                    NilaiKontrak = DPP + NilaiPPN;

                }
                else
                {
                    PPNDitanggungPemerintah = DPP * (decimal)0.1;
                    NilaiPPN = ((DPP * (decimal)0.1)) - PPNDitanggungPemerintah;
                    NilaiKontrak = DPP + NilaiPPN;
                }

                Db.Execute("EXEC spKontrakDiskon"
                     + " '" + NoKontrak + "'"
                     + ", " + Gross2
                     + ", " + NilaiKontrak
                     + ",0"
                     + ",''"
                     + ",''"
                     );

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET "
                    + " NilaiPPN = " + NilaiPPN
                    + " , PPNPemerintah = " + PPNDitanggungPemerintah
                    + " WHERE NoKontrak = '" + NoKontrak + "'");
            }

        }

        private void SaveFO()
        {
            int CaraBayar = Convert.ToInt32(carabayar.SelectedValue);

            decimal FONominal = Convert.ToDecimal(fo.Text);

            int FOCounter = Convert.ToInt32(focounter.Text);

            int count = 1;

            int NoUrut = Db.SingleInteger("SELECT ISNULL(MAX(NoUrut),0) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");
            DateTime TglJT = Db.SingleTime("SELECT TglJT FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" + NoUrut + "' ");

            if (fo.Text != "0")
            {
                string[,] x = Func.BreakFO(NoKontrak, FOCounter, FONominal);
                for (int i = 0; i <= x.GetUpperBound(0); i++)
                {
                    if (!Response.IsClientConnected) break;
                    string a = "FITTING OUT ";
                    string b = "ADM";
                    Db.Execute("EXEC spTagihanDaftar"
                        + " '" + NoKontrak + "'"
                        + ",'" + a + " " + x[i, 2] + "'"
                        + ",'" + TglJT.AddMonths(count) + "'"
                        + ", " + Convert.ToDecimal(x[i, 1])
                        + ",'" + b + "'"
                        );
                    count++;
                }
            }
        }

        protected void rdBunga_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            if (rdBunga.SelectedIndex == 1)
            {
                persenBunga.Visible = true;
                lsbunga.Visible = false;
            }
            else
            {
                persenBunga.Visible = false;
                lsbunga.Visible = true;
            }
        }

        protected void carabayar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carabayar.SelectedIndex > 0)
            {
                string RumusDiskon = Db.SingleString(
                    "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + carabayar.SelectedValue);
                string Ket = Db.SingleString(
                    "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + carabayar.SelectedValue);
                diskon2.Text = RumusDiskon;
                diskonket.Text = Ket;
                SetDiskon();
            }
        }
        protected void diskon2_TextChanged(object sender, EventArgs e)
        {
            if (carabayar.SelectedIndex > 0)
            {
                SetDiskon2();
            }
        }

        protected void Surcharge_TextChanged(object sender, EventArgs e)
        {
            if (carabayar.SelectedIndex > 0)
            {
                SetDiskon2();
            }
        }

        private void SetDiskon()
        {
            decimal Gross = Db.SingleDecimal("SELECT PriceListStandard FROM ISC064_MARKETINGJUAL..MS_UNIT"
                            + " WHERE NoStock = '" + NoStock + "'");
            decimal surcharge = Convert.ToDecimal(Surcharge.Text);
            decimal Gross2 = Gross + surcharge;

            string RumusDiskon = diskon2.Text;
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
            diskon2.Text = sb.ToString();

            decimal diskon = Func.NominalDiskon(RumusDiskon, Gross2);
            if (diskon == 0)
            {
                nilaiDiskon.Text = "";
            }
            else
            {
                nilaiDiskon.Text = Cf.Num(Math.Round(diskon, 0).ToString());
            }

        }

        private void SetDiskon2()
        {
            decimal Gross = Db.SingleDecimal("SELECT PriceListStandard FROM ISC064_MARKETINGJUAL..MS_UNIT"
                            + " WHERE NoStock = '" + NoStock + "'");
            decimal surcharge = Convert.ToDecimal(Surcharge.Text);
            decimal Gross2 = Gross + surcharge;

            string RumusDiskon = diskon2.Text;
            string[] x = RumusDiskon.Split('+');

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != "")
                {
                    decimal y = Convert.ToDecimal(x[i]);
                    if (i < (x.Length - 1))
                        sb.Append(y.ToString() + "+");
                    else
                        sb.Append(y.ToString());
                }
            }
            diskon2.Text = sb.ToString();

            decimal diskon = Func.NominalDiskon2(RumusDiskon, Gross2);
            if (diskon == 0)
            {
                nilaiDiskon.Text = "tttt";
            }
            else
            {
                nilaiDiskon.Text = Cf.Num(Math.Round(diskon, 0).ToString());
            }
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
            }
        }

        protected string NoReservasi
        {
            get
            {
                return Cf.Pk(noreservasi.Text);
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
