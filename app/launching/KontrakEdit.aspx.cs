using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class KontrakEdit : System.Web.UI.Page
    {
        string foCheck, focounterCheck;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = true;
                save.Enabled = true;
            }

            if (!Page.IsPostBack)
            {
                btnpop.Attributes["onclick"] = "popDaftarVA('" + NoStock + "')";

                nova.Attributes.Add("readonly", "readonly");
                Bind(); //tanggal dan bulan
                Fill();
            }

            if (carabayar2.SelectedValue != "")
            {
                carabayar2.Enabled = false;
            }

            FeedBack();
            Js.Confirm(this, "Lanjutkan proses edit kontrak?\\n");
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

        private void Bind()
        {
            nilaikpa.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaikpa.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaikpa.Attributes["onblur"] = "CalcBlur(this);";

            nilaiklaim.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaiklaim.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaiklaim.Attributes["onblur"] = "CalcBlur(this);";

            totallunas.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            totallunas.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            totallunas.Attributes["onblur"] = "CalcBlur(this);";

            nilaipulang.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaipulang.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaipulang.Attributes["onblur"] = "CalcBlur(this);";

            fo.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            fo.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            fo.Attributes["onblur"] = "CalcBlur(this);";

            focounter.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            focounter.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            focounter.Attributes["onblur"] = "CalcBlur(this);";

            DataTable rs = Db.Rs("SELECT Nama,Principal,NoAgent FROM MS_AGENT WHERE Status = 'A'"
                + " ORDER BY Nama,NoAgent");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                if (rs.Rows[i]["Principal"].ToString() != "")
                    t = t + " (" + rs.Rows[i]["Principal"] + ")";
                agent.Items.Add(new ListItem(t, v));
            }

            rs = Db.Rs("SELECT Nama,NoAgent FROM MS_AGENT WHERE Status = 'A' AND Tipe = 'REFFERATOR'"
                + " ORDER BY Nama,NoAgent");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"] + ";EMPLOYEE";
                string t = rs.Rows[i]["Nama"].ToString() + " (EMPLOYEE)";
                agentreff.Items.Add(new ListItem(t, v));
            }

            rs = Db.Rs("SELECT Nama,NoCustomer FROM MS_CUSTOMER WHERE Status = 'A' AND Refferator = 1"
                + " ORDER BY Nama,NoCustomer");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoCustomer"] + ";BUYER";
                string t = rs.Rows[i]["Nama"].ToString() + " (BUYER)";
                agentreff.Items.Add(new ListItem(t, v));
            }

            rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_ACC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                acc.Items.Add(new ListItem(t, v));
            }
        }

        private void Fill()
        {
            aKey.HRef = "javascript:openModal('KontrakEditKey.aspx?NoKontrak=" + NoKontrak + "','350','220')";
            aStatus.HRef = "javascript:openModal('KontrakStatus.aspx?NoKontrak=" + NoKontrak + "','500','500')";

            printSP.HRef = "PrintSP.aspx?NoKontrak=" + NoKontrak;
            printPPJB.HRef = "PrintPPJB.aspx?NoKontrak=" + NoKontrak;
            printAJB.HRef = "PrintAJB.aspx?NoKontrak=" + NoKontrak;
            printBAST.HRef = "PrintBAST.aspx?NoKontrak=" + NoKontrak;
            printRKOM.HRef = "PrintRKOM.aspx?NoKontrak=" + NoKontrak;
            printFPS.HRef = "PrintFPS.aspx?NoKontrak=" + NoKontrak;
            printFBatal.HRef = "PrintFBatal.aspx?NoKontrak=" + NoKontrak;
            printJadwalTagihan.HRef = "PrintJadwalTagihan.aspx?NoKontrak=" + NoKontrak;

            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_KONTRAK_LOG&Pk=" + NoKontrak + "'";
            btndel.Attributes["onclick"] = "location.href='KontrakDel.aspx?NoKontrak=" + NoKontrak + "'";
            refresh.Attributes["onclick"] = "if(confirm('"
                + "Apakah anda ingin mengambil ulang data unit ?\\n"
                + "Perhatian bahwa nilai GROSS dan DISKON bisa berubah."
                + "'))"
                + "{location.href='KontrakRefresh.aspx?NoKontrak=" + NoKontrak + "'}";

            string strSql = "SELECT *"
                + " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nokontrak.Text = rs.Rows[0]["NoKontrak"].ToString();

                if (Convert.ToInt16(rs.Rows[0]["FlagProsesBatal"]) == 1)
                {
                    prosesbatal.Checked = true;
                }
                else
                {
                    prosesbatal.Checked = false;
                }

                string stat = rs.Rows[0]["Status"].ToString();
                if (stat == "A")
                {
                    status.ForeColor = Color.Green;
                    status.Text = "Aktif";
                }
                else if (stat == "B")
                {
                    status.ForeColor = Color.Red;
                    status.Text = "Batal";
                }
                else if (stat == "E")
                {
                    status.ForeColor = Color.Gray;
                    status.Text = "Proses Batal";
                }

                tglkontrak.Text = Cf.Day(rs.Rows[0]["TglKontrak"]);
                targetst.Text = Cf.Day(rs.Rows[0]["TargetST"]);

                //tanggal input, edit dan follow-up
                tglInput.Text = Cf.Date(rs.Rows[0]["TglInput"]);
                tglEdit.Text = Cf.Date(rs.Rows[0]["TglEdit"]);

                string ket = "";
                switch ((int)rs.Rows[0]["FlagGross"])
                {
                    case 1:
                        ket = "Edit Kontrak";
                        break;
                    case 2:
                        ket = "Refresh Unit";
                        break;
                    case 3:
                        ket = "Pindah Unit";
                        break;
                    case 4:
                        ket = "Serah Terima";
                        break;
                }
                decimal t = 0;

                decimal LuasSG = Db.SingleDecimal("SELECT LuasSG FROM MS_UNIT Where NoStock = '" + rs.Rows[0]["NoStock"] + "'");
                decimal LuasNett = Db.SingleDecimal("SELECT LuasNett FROM MS_UNIT Where NoStock = '" + rs.Rows[0]["NoStock"] + "'");
                luas.Text = Cf.Num(LuasSG);
                luas2.Text = Cf.Num(LuasNett);

                decimal bungaNominal = Db.SingleDecimal("SELECT ISNULL(BungaNominal,0) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");//checkNull(rs.Rows[0]["BungaNominal"]);

                gross.Text = Cf.Num(rs.Rows[0]["Gross"]);
                diskon.Text = Cf.Num(Convert.ToDecimal(rs.Rows[0]["DiskonRupiah"]));
                diskontambahan.Text = Cf.Num(Convert.ToDecimal(rs.Rows[0]["DiskonTambahan"]));
                afterdisc.Text = Cf.Num(Convert.ToDecimal(rs.Rows[0]["Gross"]) - Convert.ToDecimal(rs.Rows[0]["DiskonRupiah"]) - Convert.ToDecimal(rs.Rows[0]["DiskonTambahan"]));
                hargaSD.Text = Cf.Num(Convert.ToDecimal(rs.Rows[0]["Gross"]) + Convert.ToDecimal(rs.Rows[0]["HargaLainLain"]) + Convert.ToDecimal(rs.Rows[0]["HargaGimmick"]));
                note.Text = rs.Rows[0]["Note"].ToString();

                decimal bungarupiah = Convert.ToDecimal(rs.Rows[0]["BungaNominal"]);
                lblBunga.Text = Cf.Num(Math.Round(bungarupiah));

                DPP2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["NilaiDPP"]), 0));
                nilai.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]), 0));
                PPN2.Text = ppn.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["NilaiPPN"]), 0));

                //decimal nilaiKontrak = Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]) + Convert.ToDecimal(rs.Rows[0]["NilaiPPN"]);
                //if (Convert.ToDecimal(rs.Rows[0]["NilaiPPN"]) == 0)
                //{
                //    DPP2.Text = Cf.Num(rs.Rows[0]["NilaiKontrak"]);
                //    PPN2.Text = "0";
                //}
                //else
                //{
                //    DPP2.Text = Cf.Num(Math.Round(Convert.ToDecimal(nilai.Text) / (decimal)1.1));
                //    PPN2.Text = Cf.Num(Math.Round(Convert.ToDecimal(DPP2.Text) * (decimal)0.1));
                //}

                //refresh info
                DataTable rsunit = Db.Rs("SELECT NoUnit, Luas, PriceList "
                    + " FROM MS_UNIT WHERE NoStock = '" + rs.Rows[0]["NoStock"] + "'");
                if (rsunit.Rows.Count != 0)
                {
                    if (rsunit.Rows[0]["NoUnit"].ToString() != rs.Rows[0]["NoUnit"].ToString()
                        || Convert.ToDecimal(rsunit.Rows[0]["Luas"]) != Convert.ToDecimal(rs.Rows[0]["Luas"])
                        || Convert.ToDecimal(rsunit.Rows[0]["PriceList"]) != Convert.ToDecimal(rs.Rows[0]["Gross"])
                        )
                    {
                        refreshinfo.Text = "DATA ADMINISTRASI BERBEDA";
                    }
                    else
                    {
                        refresh.Disabled = true;
                    }
                }

                //agent
                agent.Items.Add(new ListItem(
                    "Tidak Berubah : " + rs.Rows[0]["NoAgent"].ToString().PadLeft(5, '0')
                    , rs.Rows[0]["NoAgent"].ToString()));
                agent.SelectedValue = rs.Rows[0]["NoAgent"].ToString();

                string TipeAgent = Db.SingleString("SELECT Tipe FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[0]["NoAgent"] + "'");
                if (TipeAgent == "INHOUSE") reff.Visible = true;
                else reff.Visible = false;

                //refferator
                if (rs.Rows[0]["NoRefferatorAgent"].ToString() != "")
                {
                    agentreff.Items.Add(new ListItem(
                        "Tidak Berubah : " + rs.Rows[0]["NoRefferatorAgent"].ToString().PadLeft(5, '0')
                        , rs.Rows[0]["NoRefferatorAgent"].ToString()));
                    agentreff.SelectedValue = rs.Rows[0]["NoRefferatorAgent"].ToString() + ";EMPLOYEE";
                }
                else
                {
                    agentreff.Items.Add(new ListItem(
                        "Tidak Berubah : " + rs.Rows[0]["NoRefferatorCustomer"].ToString().PadLeft(5, '0')
                        , rs.Rows[0]["NoRefferatorCustomer"].ToString()));
                    agentreff.SelectedValue = rs.Rows[0]["NoRefferatorCustomer"].ToString() + ";BUYER";
                }

                skema.Text = rs.Rows[0]["Skema"].ToString();

                batal.Text = rs.Rows[0]["AlasanBatal"].ToString();
                if (batal.Text == "") bataltr.Visible = false;

                ppjb.Text = Cf.Day(rs.Rows[0]["TglPPJB"])
                    + "&nbsp;&nbsp;&nbsp;"
                    + "<u>" + rs.Rows[0]["NoPPJB"] + "</u>";

                ajb.Text = Cf.Day(rs.Rows[0]["TglAJB"])
                    + "&nbsp;&nbsp;&nbsp;"
                    + "<u>" + rs.Rows[0]["NoAJB"] + "</u>";

                //serah terima
                if ((string)rs.Rows[0]["ST"] == "T")
                    st.Text = "<b>Terlambat</b>";
                else if ((string)rs.Rows[0]["ST"] == "D")
                    st.Text = Cf.Day(rs.Rows[0]["TglST"])
                        + "&nbsp;&nbsp;&nbsp;"
                        + "<u>" + rs.Rows[0]["NoST"] + "</u>";

                printSP.InnerHtml = printSP.InnerHtml + " (" + rs.Rows[0]["PrintSP"] + ")";
                printPPJB.InnerHtml = printPPJB.InnerHtml + " (" + rs.Rows[0]["PrintPPJB"] + ")";
                printAJB.InnerHtml = printAJB.InnerHtml + " (" + rs.Rows[0]["PrintAJB"] + ")";
                printBAST.InnerHtml = printBAST.InnerHtml + " (" + rs.Rows[0]["PrintBAST"] + ")";
                printRKOM.InnerHtml = printRKOM.InnerHtml + " (" + rs.Rows[0]["PrintRKOM"] + ")";
                printFPS.InnerHtml = printFPS.InnerHtml + " (" + rs.Rows[0]["PrintFPS"] + ")";
                //   printJadwalTagihan.InnerHtml = printJadwalTagihan.InnerHtml + " (" + rs.Rows[0]["PrintJadwalTagihan"] + ")";

                JenisPPN.SelectedValue = rs.Rows[0]["JenisPPN"].ToString();

                if (Convert.ToBoolean(rs.Rows[0]["JenisKPR"]))
                    jeniskpr.Items[1].Selected = true;
                else
                    jeniskpr.Items[0].Selected = true;

                if (rs.Rows[0]["CaraBayar"].ToString() == "KPR")
                {
                    carabayar2.Items[0].Selected = true;
                }
                else if (rs.Rows[0]["CaraBayar"].ToString() == "INSTALLMENT")
                {
                    carabayar2.Items[1].Selected = true;
                }
                else if (rs.Rows[0]["CaraBayar"].ToString() == "HARD CASH")
                {
                    carabayar2.Items[2].Selected = true;
                }
                else if (rs.Rows[0]["CaraBayar"].ToString() == "SOFT CASH")
                {
                    carabayar2.Items[3].Selected = true;
                }
                else if (rs.Rows[0]["CaraBayar"].ToString() == "")
                {
                    carabayar2.ClearSelection();
                }

                nilaikpa.Text = Cf.Num(rs.Rows[0]["NilaiRealisasiKPR"]);
                rekcair.Items.Add(new ListItem("Sekarang : " + rs.Rows[0]["RekeningCairKPR"], rs.Rows[0]["RekeningCairKPR"].ToString()));
                rekcair.Items[rekcair.Items.Count - 1].Selected = true;


                if (rs.Rows[0]["PPJB"].ToString() == "D")
                {
                    tra.Visible = true;
                    trb.Visible = true;
                }
                else
                {
                    tra.Visible = false;
                    trb.Visible = false;
                }

                //Faktur Pajak Standar
                if (rs.Rows[0]["AJB"].ToString() == "D")
                {
                    printFPS.Visible = true;
                    kdPajak.Enabled = true;
                    //noFPS.ReadOnly = true;
                }
                else
                {
                    printFPS.Visible = false;
                    kdPajak.SelectedValue = "";
                    kdPajak.Enabled = false;
                    //noFPS.ReadOnly = true;
                }

                if (rs.Rows[0]["Status"].ToString() == "B")
                {
                    trc.Visible = trd.Visible = tre.Visible = trf.Visible = true;
                    printFBatal.Visible = true;
                }
                else
                {
                    trc.Visible = trd.Visible = tre.Visible = trf.Visible = false;
                    printFBatal.Visible = false;
                }

                //Fill No Faktur Pajak
                string kp = Db.SingleString("SELECT NoFPS FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                if (kp == "")
                {
                    kdPajak.SelectedValue = "";
                    noFPS.Text = "";
                }
                else
                {
                    string kp2 = kp.Substring(0, 3);
                    if (kp2 == "010")
                    {
                        kdPajak.SelectedValue = "010";
                        noFPS.Text = Cf.Str(rs.Rows[0]["NoFPS"]).ToString().Substring(3, 16);
                    }
                    else
                    {
                        kdPajak.SelectedValue = "011";
                        noFPS.Text = Cf.Str(rs.Rows[0]["NoFPS"]).ToString().Substring(3, 16);
                    }
                }

                nilaiklaim.Text = Cf.Num(rs.Rows[0]["NilaiKlaim"]);
                totallunas.Text = Cf.Num(rs.Rows[0]["TotalLunasBatal"]);
                nilaipulang.Text = Cf.Num(rs.Rows[0]["NilaiPulang"]);
                acc.Items.Add(new ListItem("Tidak berubah: " + rs.Rows[0]["AccBatal"], rs.Rows[0]["AccBatal"].ToString()));
                acc.SelectedIndex = acc.Items.Count - 1;


                if (Convert.ToBoolean(rs.Rows[0]["FOBO"]) || Convert.ToBoolean(rs.Rows[0]["FOBOBatal"]))
                    btndel.Attributes["disabled"] = "true";

                focounter.Text = (Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NamaTagihan LIKE 'FITTING OUT%'")).ToString();
                decimal fitting = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NamaTagihan LIKE 'FITTING OUT%'");
                fo.Text = Cf.Num(Math.Round(fitting));

                //nilai ini berguna untuk mengecek nilai focounter dan fo
                //apabila focounter dan fo berubah maka data berubah

                foCheck = focounter.Text;
                focounterCheck = fo.Text;

                //status PPN

                //if ((bool)rs.Rows[0]["PPN"])
                //    statusPPN.Text = "<b>(INCLUDE)</b>";
                //else if (!(bool)rs.Rows[0]["PPN"])
                //    statusPPN.Text = "<b>(EXCLUDE)</b>";

                /*Sumber Dana*/
                ddlSumberDana.SelectedValue = rs.Rows[0]["SumberDana"].ToString();
                if ((int)rs.Rows[0]["SumberDana"] == 4)
                {
                    trLainnya.Visible = true;
                }
                else
                {
                    trLainnya.Visible = false;
                }
                lainnya.Text = rs.Rows[0]["SumberDanaLainnya"].ToString();

                ddlTujuan.SelectedValue = rs.Rows[0]["TujuanKontrak"].ToString();
                noqueue.Text = rs.Rows[0]["NUP"].ToString();

                nova.Text = rs.Rows[0]["NoVA"].ToString();
                if (nova.Text != "") btnpop.Disabled = true;
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

            if (tra.Visible)
            {
                if (!Cf.isMoney(nilaikpa))
                {
                    x = false;
                    if (s == "") s = nilaikpa.ID;
                    nilaikpac.Text = "Angka";
                }
                else
                    nilaikpac.Text = "";
            }

            if (trc.Visible)
            {
                if (!Cf.isMoney(nilaiklaim))
                {
                    x = false;
                    if (s == "") s = nilaiklaim.ID;
                    nilaiklaimc.Text = "Angka";
                }
                else
                    nilaiklaimc.Text = "";
            }

            if (trd.Visible)
            {
                if (!Cf.isMoney(totallunas))
                {
                    x = false;
                    if (s == "") s = totallunas.ID;
                    totallunasc.Text = "Angka";
                }
                else
                    totallunasc.Text = "";
            }

            if (tre.Visible)
            {
                if (!Cf.isMoney(nilaipulang))
                {
                    x = false;
                    if (s == "") s = nilaipulang.ID;
                    nilaipulangc.Text = "Angka";
                }
                else
                    nilaipulangc.Text = "";
            }

            //if (Cf.isEmpty(noqueue))
            //{
            //    x = false;
            //    if (s == "") s = noqueue.ID;
            //    noqueuec.Text = "Kosong";
            //}
            //else
            //    noqueuec.Text = "";

            if (TipeAgent == "INHOUSE" && agentreff.SelectedValue == "")
            {
                x = false;
                if (s == "") s = agentreff.ID;
                agentreffc.Text = "Harus pilih salah satu";
            }
            else
                agentreffc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Nilai Realisasi KPR, Nilai Klaim, Total Pelunasan pada saat Batal, Nilai Kembali harus berupa angka.\\n"
                    + "3. NUP tidak boleh Kosong."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                string NoAgent = agent.SelectedValue;
                DateTime TglKontrak = Convert.ToDateTime(tglkontrak.Text);
                DateTime TargetST = Convert.ToDateTime(targetst.Text);

                string Tujuan = ddlTujuan.SelectedValue;
                string SumberDana = ddlSumberDana.SelectedValue;
                string SumberDanaLainnya = "";
                if (lainnya.Text != "")
                    SumberDanaLainnya = lainnya.Text;
                string NUP = Cf.Str(noqueue.Text);

                string flag1 = Db.SingleTime(
                    "SELECT TglEdit FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'").ToString();

                string ReffAgent1 = "", ReffAgent2 = "";
                if (reff.Visible && agentreff.SelectedIndex > 0)
                {
                    string[] aa = agentreff.SelectedValue.Split(';');
                    if (aa[1] == "EMPLOYEE")
                    {
                        ReffAgent1 = aa[0];
                    }
                    else
                    {
                        ReffAgent2 = aa[0];
                    }
                }

                DataTable rsBef = Db.Rs("SELECT "
                    + " NoKontrak AS [No. Kontrak]"
                    + ",CONVERT(varchar,TglKontrak,106) AS [Tanggal Kontrak]"
                    + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS [Agent]"
                    + ",CONVERT(varchar,MS_KONTRAK.TargetST,106) AS [Jadwal Serah Terima]"
                    + ", JenisPPN AS [PPN Ditanggung]"
                    + ", CASE JenisKPR"
                    + "		WHEN 0 THEN 'KPR'"
                    + "		WHEN 1 THEN 'NON-KPR'"
                    + " END AS [Jenis KPR]"
                    + ", NilaiKlaim AS [Nilai Klaim]"
                    + ", TotalLunasBatal AS [Total Pelunasan saat Batal]"
                    + ", NilaiPulang AS [Nilai Kembali]"
                    + ", AccBatal AS [Rekening Pembatalan]"
                    + ", SumberDana AS [Sumber Dana]"
                    + ", SumberDanaLainnya AS [Sumber Dana Lainnya]"
                    + ", TujuanKontrak AS [Tujuan Transaksi]"
                    + ", NUP"
                    + ", Skema"
                    + ", NoRefferatorAgent"
                    + ", NoRefferatorCustomer"
                    + ", NoVA AS [No. Virtual Account]"
                    + " FROM MS_KONTRAK INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );

                Db.Execute("EXEC spKontrakEdit"
                    + " '" + NoKontrak + "'"
                    + ",'" + TglKontrak + "'"
                    + ", " + NoAgent
                    + ",'" + TargetST + "'"
                    );

                int kpr = 0;
                if (carabayar2.SelectedValue == "KPR")
                {
                    kpr = 0;
                }
                else
                {
                    kpr = 1;
                }

                //Manual update
                DataTable rs = Db.Rs("SELECT FlagKomisi FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");
                if (Convert.ToBoolean(rs.Rows[0]["FlagKomisi"]) != false)
                    if (carabayar2.SelectedValue != "CASH KERAS")
                        Db.Execute("UPDATE MS_KOMISI SET FlagCaraBayar=1 WHERE NoKontrak='" + NoKontrak + "'");

                Db.Execute("UPDATE MS_KONTRAK SET FlagProsesBatal = '0' WHERE NoKontrak = '" + NoKontrak + "' ");
                if (prosesbatal.Checked)
                {
                    Db.Execute("UPDATE MS_KONTRAK SET FlagProsesBatal = '1' WHERE NoKontrak = '" + NoKontrak + "'  ");
                }

                Db.Execute("UPDATE MS_KONTRAK SET Skema = '" + skema.Text + "' WHERE NoKontrak = '" + NoKontrak + "' ");
                Db.Execute("UPDATE MS_KONTRAK SET Note = '" + note.Text + "' WHERE NoKontrak = '" + NoKontrak + "' ");

                Db.Execute("UPDATE MS_KONTRAK SET NoRefferatorAgent = '" + ReffAgent1 + "', NoRefferatorCustomer = '" + ReffAgent2 + "' WHERE NoKontrak = '" + NoKontrak + "' ");


                //Virtual Account
                string NoVA = Cf.Str(nova.Text);
                Db.Execute("UPDATE MS_KONTRAK SET NoVA = '" + Cf.Str(nova.Text) + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'");

                if (delva.Checked)
                {
                    Db.Execute("UPDATE MS_KONTRAK SET NoVA = ''"
                        + " WHERE NoKontrak = '" + NoKontrak + "'");
                }

                string noFakturPajak = kdPajak.SelectedValue + noFPS.Text;
                if (noFPS.Text == "")
                {
                    Db.Execute("UPDATE MS_KONTRAK"
                    + " SET JenisPPN = '" + JenisPPN.SelectedItem.Text + "'"
                    + ", JenisKPR = " + kpr
                    + ", CaraBayar = '" + carabayar2.SelectedValue + "'"
                    + ", NilaiRealisasiKPR = " + Convert.ToDecimal(nilaikpa.Text)
                    + ", RekeningCairKPR = '" + rekcair.SelectedValue + "'"
                    + ", NilaiKlaim = " + Convert.ToDecimal(nilaiklaim.Text)
                    + ", TotalLunasBatal = " + Convert.ToDecimal(totallunas.Text)
                    + ", NilaiPulang = " + Convert.ToDecimal(nilaipulang.Text)
                    + ", AccBatal = '" + acc.SelectedValue + "'"
                    + ", SumberDana='" + SumberDana + "'"
                    + ", SumberDanaLainnya='" + Cf.Str(SumberDanaLainnya) + "'"
                    + ", TujuanKontrak = '" + Tujuan + "'"
                    + ", NUP = '" + NUP + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
                }
                else
                {
                    Db.Execute("UPDATE MS_KONTRAK"
                    + " SET JenisPPN = '" + JenisPPN.SelectedItem.Text + "'"
                    + ", JenisKPR = " + kpr
                    + ", CaraBayar = '" + carabayar2.SelectedValue + "'"
                    + ", NilaiRealisasiKPR = " + Convert.ToDecimal(nilaikpa.Text)
                    + ", RekeningCairKPR = '" + rekcair.SelectedValue + "'"
                    + ", NilaiKlaim = " + Convert.ToDecimal(nilaiklaim.Text)
                    + ", TotalLunasBatal = " + Convert.ToDecimal(totallunas.Text)
                    + ", NilaiPulang = " + Convert.ToDecimal(nilaipulang.Text)
                    + ", AccBatal = '" + acc.SelectedValue + "'"
                    + ", NoFPS = '" + noFakturPajak + "'"
                    + ", SumberDana='" + SumberDana + "'"
                    + ", SumberDanaLainnya='" + Cf.Str(SumberDanaLainnya) + "'"
                    + ", TujuanKontrak = '" + Tujuan + "'"
                    + ", NUP = '" + NUP + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
                }

                DataTable rsAft = Db.Rs("SELECT "
                    + " NoKontrak AS [No. Kontrak]"
                    + ",CONVERT(varchar,TglKontrak,106) AS [Tanggal Kontrak]"
                    + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS [Agent]"
                    + ",CONVERT(varchar,MS_KONTRAK.TargetST,106) AS [Jadwal Serah Terima]"
                    + ", JenisPPN AS [PPN Ditanggung]"
                    + ", CASE JenisKPR "
                    + "		WHEN 0 THEN 'KPR' "
                    + "		WHEN 1 THEN 'NON-KPR' "
                    + " END AS [Jenis KPR] "
                    + ", NilaiKlaim AS [Nilai Klaim] "
                    + ", TotalLunasBatal AS [Total Pelunasan saat Batal] "
                    + ", NilaiPulang AS [Nilai Kembali] "
                    + ", AccBatal AS [Rekening Pembatalan] "
                    + ", SumberDana AS [Sumber Dana]"
                    + ", SumberDanaLainnya AS [Sumber Dana Lainnya]"
                    + ", TujuanKontrak AS [Tujuan Transaksi]"
                    + ", NUP"
                    + ", Skema"
                    + ", NoRefferatorAgent"
                    + ", NoRefferatorCustomer"
                    + ", NoVA AS [No. Virtual Account]"
                    + " FROM MS_KONTRAK INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );

                //Logfile
                string Ket = Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogKontrak"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoKontrak + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                if ((focounter.Text != focounterCheck) && (fo.Text != foCheck))
                {
                    Db.Execute("DELETE FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NamaTagihan LIKE 'FITTING OUT%'");

                    if (fo.Text != "0")
                    {
                        int count = 1;

                        int NoUrut = Db.SingleInteger("SELECT ISNULL(MAX(NoUrut),0) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");
                        DateTime TglJT = Db.SingleTime("SELECT TglJT FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoUrut + " ");

                        string[,] x = Func.BreakFO(NoKontrak, Convert.ToInt32(focounter.Text), Convert.ToDecimal(fo.Text));
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
                    else if (fo.Text == "0")
                    {
                        Db.Execute("DELETE FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NamaTagihan LIKE 'FITTING OUT%'");
                    }
                }
                return true;
            }
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("KontrakEdit.aspx?done=1&NoKontrak=" + NoKontrak);
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }

        private string NoStock
        {
            get
            {
                string no = Db.SingleString("SELECT NoStock FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                return no;
            }
        }

        private string TipeAgent
        {
            get
            {
                string TipeAgent = Db.SingleString("SELECT Tipe FROM MS_AGENT WHERE NoAgent = '" + agent.SelectedValue + "'");

                return TipeAgent;
            }
        }

        protected void ddlSumberDana_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSumberDana.SelectedIndex == 4)
            {
                trLainnya.Visible = true;
            }
            else
            {
                trLainnya.Visible = false;
            }
        }

        private decimal checkNull(object obj)
        {
            if (obj == null)
                return 0;
            else
                return Convert.ToDecimal(obj);
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
