using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.AspNet.SignalR;

namespace ISC064.MARKETINGJUAL
{
    public partial class ReservasiDaftar2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Func.UnitSedangClosing(NoStock);

            if (!Page.IsPostBack)
            {
                nocustomer.Attributes["ondblclick"] = "popDaftarCustomer('a')";

                InitForm();
                Fill();

                divPersenBertingkat.Visible = false;
                divLumpSum.Visible = true;

                if (Request.QueryString["NoCustomer"] != null)
                {
                    nocustomer.Text = Request.QueryString["NoCustomer"];
                    LoadCustomer();
                }
                else
                {
                    Js.Focus(this, nocustomer);
                    frm.Visible = false;
                }
                nostock.Text = NoStock;

            }
            string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            btnpop.Attributes.Add("modal-url", "DaftarCustomer.aspx?status=a&project=" + Project);
        }

        protected void GantiTipeSales(object sender, System.EventArgs e)
        {
            supervisor.Text = Db.SingleString("SELECT Principal FROM MS_AGENT WHERE NoAgent = '" + agent.SelectedValue + "'");
            manager.Text = Db.SingleString("SELECT SalesManager FROM MS_AGENT WHERE NoAgent = '" + agent.SelectedValue + "'");
            //string Tipe = Db.SingleString("SELECT Tipe FROM MS_AGENT WHERE NoAgent = '" + agent.SelectedValue + "'");
            //reff.Visible = Tipe == "INHOUSE" ? true : false;
        }

        private void InitForm()
        {
            string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

            pl.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            pl.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            pl.Attributes["onblur"] = "CalcBlur(this);";

            nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilai.Attributes["onblur"] = "CalcBlur(this);";

            DataTable rs;

            //Skema bayar
            rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' AND Project = '" + Project + "' ORDER BY Nama");
            skema.Items.Add(new ListItem("*** CUSTOMIZE / PENDING", "0")); //cara bayar customize

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Nomor"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                skema.Items.Add(new ListItem(t, v));
            }
            skema.SelectedIndex = 0;
            skema.Attributes["ondblclick"] = "kalk(this)";

            //Populate data agent
            rs = Db.Rs("SELECT Nama,Principal,NoAgent FROM MS_AGENT WHERE Status = 'A' AND Project = '" + Project + "'"
                + " ORDER BY Nama,NoAgent");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                agent.Items.Add(new ListItem(t, v));
            }

            supervisor.Text = Db.SingleString("SELECT Principal FROM MS_AGENT WHERE NoAgent = '" + agent.SelectedValue + "'");
            manager.Text = Db.SingleString("SELECT SalesManager FROM MS_AGENT WHERE NoAgent = '" + agent.SelectedValue + "'");

            //Principal
            //rs = Db.Rs("SELECT Principal FROM MS_AGENT WHERE Status = 'A'"
            //    + " ORDER BY Principal");
            //for (int i = 0; i < rs.Rows.Count; i++)
            //{
            //    string v = rs.Rows[i]["Principal"].ToString();
            //    //string t = rs.Rows[i]["Nama"].ToString();
            //    if (rs.Rows[i]["Principal"].ToString() != "")
            //        //v = v + " (" + rs.Rows[i]["Principal"] + ")";
            //    principal.Items.Add(new ListItem(v));
            //}

            //Refferator
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

            //Fill Acc
            rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_ACC WHERE Project = '" + Project + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                ddlAcc.Items.Add(new ListItem(t, v));
            }

            batas.Attributes.Add("readonly", "readonly");
        }

        private void Fill()
        {
            string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

            cancel.Attributes["onclick"] = "location.href = 'ReservasiDaftar2.aspx?NoStock=" + NoStock + "'";
            btnbaru.Attributes["onclick"] = "location.href = 'CustomerDaftar.aspx?NoStock=" + NoStock + "&reserve=1&project=" + Project + "'";

            DataTable rs = Db.Rs("SELECT Luas, LuasSG, LuasNett, PriceList, NoUnit, Lokasi FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nounit.Text = "<a href=\"javascript:popUnit('" + NoStock + "')\">"
                    + rs.Rows[0]["NoUnit"].ToString() + "</a>";

                luasbangunan.Text = Cf.Num(rs.Rows[0]["LuasNett"]);
                luastanah.Text = Cf.Num(rs.Rows[0]["LuasSG"]);
                pl.Text = Cf.Num(rs.Rows[0]["PriceList"]);

                tgl.Text = Cf.Day(DateTime.Today);

                string ParamID = "BatasReservasi" + Project;
                string ParamReserve = Db.SingleString("SELECT VALUE FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'");
                
                if (!String.IsNullOrEmpty(ParamReserve))
                {
                    string[] value = ParamReserve.Split(';');

                    int jarak = Convert.ToInt16(value[0]);

                    switch (value[1])
                    {
                        case "1": batas.Text = Cf.Date(DateTime.Now.AddMinutes(+jarak)); break;
                        case "2": batas.Text = Cf.Date(DateTime.Now.AddHours(+jarak)); break;
                        case "3": batas.Text = Cf.Date(DateTime.Now.AddDays(+jarak)); break;
                        default: batas.Text = Cf.Date(DateTime.Now.AddMinutes(+jarak)); break;
                    }
                }
                else
                {
                    ok.Enabled = false;
                    batasc.Text = "Parameter batas reservasi belum ada. Silahkan hubungi admin terkait.";
                }

            }
            lokasi.Items.Add(new ListItem("-"));
            DataTable rs2 = Db.Rs("SELECT * FROM REF_LOKASI_KONTRAK WHERE Project = '" + Project + "' ORDER BY SN");
            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                lokasi.Items.Add(new ListItem(rs2.Rows[i]["Nama"].ToString(), rs2.Rows[i]["SN"].ToString()));
            }
            //string Lokasi = Db.SingleString("SELECT Nama FROM REF_LOKASI WHERE Lokasi = '" + rs.Rows[0]["Lokasi"].ToString() + "' AND Project = '" + Project + "'");
            //lokasi.SelectedValue = Lokasi;
        }

        private bool csvalid()
        {
            bool x = true;

            try { int z = Convert.ToInt32(NoCustomer); }
            catch { x = false; }

            if (x)
            {
                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer
                    + " AND Status = 'A' AND Project = '" + Project + "'");
                if (c == 0) x = false;
            }

            if (!x)
                Js.Alert(
                    this
                    , "Customer Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Customer tersebut tidak terdaftar.\\n"
                    + "2. Status customer tersebut adalah INAKTIF.\\n"
                    , "document.getElementById('nocustomer').focus();"
                    + "document.getElementById('nocustomer').select();"
                    );

            return x;
        }

        private void LoadCustomer()
        {
            if (csvalid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                FillForm();

                Js.Focus(this, nilai);
            }
            else
            {
                Js.Focus(this, nocustomer);
                frm.Visible = false;
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (csvalid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                FillForm();

                Js.Focus(this, noqueue);
            }
        }

        private void FillForm()
        {
            //nama customer
            string nama = Db.SingleString(
                "SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer);
            customer.Text = "<a href=\"javascript:popEditCustomer('" + NoCustomer + "')\">" + nama
                + " (" + Convert.ToInt32(NoCustomer).ToString().PadLeft(5, '0') + ")</a>";
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            if (rbCaraBayar.SelectedValue == "")
            {
                x = false;
                if (s == "") s = rbCaraBayar.ID;
                cb.Text = "Harus Di Pilih";
            }
            else
            {
                cb.Text = "";
            }

            if (rbCaraBayar.SelectedValue != "TN")
            {
                if (ddlAcc.SelectedIndex == 0)
                {
                    x = false;
                    if (s == "") s = ddlAcc.ID;
                    ddlAccErr.Text = "Harus Di Pilih";
                }
                else
                {
                    ddlAccErr.Text = "";
                }
            }

            //skema harus dipilih buat keperluan fobo nanti
            if (skema.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = ddlAcc.ID;
                skemac.Text = "Harus Di Pilih";
            }
            else
            {
                skemac.Text = "";
            }

            if (agent.Items.Count == 0)
            {
                x = false;
                Js.Alert(this, "Belum memiliki Sales", "");
            }

            if (rbCaraBayar.SelectedValue == "BG" && (Cf.isEmpty(nobg) || Cf.isEmpty(tglbg)))
            {
                x = false;
                if (s == "") s = bgc.ID;
                bgc.Text = "Harus Diisi";
            }
            else
            {
                bgc.Text = "";
            }

            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

            if (!Cf.isTgl(batas))
            {
                x = false;
                if (s == "") s = batas.ID;
                batasc.Text = "Tanggal";
            }
            else
                batasc.Text = "";

            if (!Cf.isInt(noqueue))
            {
                x = false;
                if (s == "") s = noqueue.ID;
                noqueuec.Text = "Angka Bulat";
            }
            else
                noqueuec.Text = "";

            if (!Cf.isMoney(nilai))
            {
                x = false;
                if (s == "") s = nilai.ID;
                nilaic.Text = "Angka";
            }
            else
            {
                decimal z = Convert.ToDecimal(nilai.Text);
                if (z < 0)
                {
                    x = false;
                    if (s == "") s = nilai.ID;
                    nilaic.Text = "Harus Positif";
                }
                else
                    nilaic.Text = "";
            }

            if (!Cf.isMoney(pl))
            {
                x = false;
                if (s == "") s = pl.ID;
                plc.Text = "Angka";
            }
            else
            {
                decimal z = Convert.ToDecimal(pl.Text);
                if (z == 0)
                {
                    x = false;
                    if (s == "") s = pl.ID;
                    plc.Text = "Tidak boleh nol";
                }
                else
                    nilaic.Text = "";
            }
            //if (Cf.isEmpty(supervisor))
            //{
            //    x = false;
            //    if (s == "") s = supervisor.ID;
            //    spv.Text = "Kosong";
            //}
            //else
            //    spv.Text = "";

            //if (Cf.isEmpty(manager))
            //{
            //    x = false;
            //    if (s == "") s = manager.ID;
            //    mgr.Text = "Kosong";
            //}
            //else
            //    mgr.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Nilai Pengikatan harus berupa angka dan positif.\\n"
                    //+ "3. NUP harus berupa angka bulat.\\n"
                    + "3. Skema Pembayaran Harus DiPilih.\\n"
                    + "4. Nama Bank dan Cara Pembayaran Nilai Pengikatan Harus DiPilih.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                int NoAgent = Convert.ToInt32(agent.SelectedValue);
                DateTime Tgl = Convert.ToDateTime(tgl.Text);
                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                string ParamID = "BatasReservasi" + Project;
                string ParamReserve = Db.SingleString("SELECT VALUE FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'");
                noreservasifull.Text = Numerator.SuratReservasi(Tgl.Month, Tgl.Year, Project);

                if (!String.IsNullOrEmpty(ParamReserve))
                {
                    string[] value = ParamReserve.Split(';');

                    int jarak = Convert.ToInt16(value[0]);

                    switch (value[1])
                    {
                        case "1": batas.Text = Cf.Date(DateTime.Now.AddMinutes(+jarak)); break;
                        case "2": batas.Text = Cf.Date(DateTime.Now.AddHours(+jarak)); break;
                        case "3": batas.Text = Cf.Date(DateTime.Now.AddDays(+jarak)); break;
                        default: batas.Text = Cf.Date(DateTime.Now.AddMinutes(+jarak)); break;
                    }
                }

                DateTime TglExpire = Convert.ToDateTime(batas.Text);
                int NoQueue = Convert.ToInt32(noqueue.Text);
                decimal Netto = Convert.ToDecimal(nilai.Text);
                decimal Harga = Convert.ToDecimal(pl.Text);
                string Skema = skema.SelectedItem.Text;
                string Supervisor = Cf.Str(supervisor.Text);
                string Manager = Cf.Str(manager.Text);
                string Alasan = Cf.Str(alasan1.Text);
                string Lokasi = lokasi.SelectedValue;

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

                Db.Execute("EXEC spReservasiDaftar"
                    + " '" + NoStock + "'"
                    + ", " + NoCustomer
                    + ", " + NoAgent
                    + ",'" + Tgl + "'"
                    + ",'" + TglExpire + "'"
                    + ", " + Netto
                    + ",'" + Skema + "'"
                    + ", " + NoQueue
                    );

                int NoReservasi = Db.SingleInteger("SELECT TOP 1 NoReservasi FROM MS_RESERVASI ORDER BY NoReservasi DESC");

                string QueryCarba = "", QueryAcc = "";
                if(carabayar2.SelectedIndex != 0)
                {
                    QueryCarba = ", CaraBayar = '" + carabayar2.SelectedValue + "'";
                }

                if (ddlAcc.SelectedIndex != 0)
                {
                    QueryAcc = ", Acc = '" + ddlAcc.SelectedValue + "'";
                }

                Db.Execute("UPDATE MS_RESERVASI SET "
                    + " NoRefferatorAgent = '" + ReffAgent1 + "'"
                    + ", NoRefferatorCustomer = '" + ReffAgent2 + "'"
                    + ", Gross ='" + Harga + "'"
                    + ", Supervisor ='" + Supervisor + "'"
                    + ", Manager ='" + Manager + "'"
                    + ", LokasiPenjualan = '" + Lokasi + "'"
                    + ", RefSkema ='" + skema.SelectedValue + "'"
                    + ", reffcust = '" + reffcust.Text + "'"
                    + ", anreff = '" + anreff.Text + "'"
                    + ", bankreff = '" + bankreff.Text + "'"
                    + ", norekreff = '" + norekreff.Text + "'"
                    + ", npwpreff = '" + npwpreff.Text + "'"
                    + ", Alasan ='" + Alasan + "'"
                    + ", NoReservasi2 ='" + Cf.Str(noreservasifull.Text) + "'"
                    + ", Project ='" + Project + "'"
                    + ", UserID ='" + Act.UserID + "'"
                    + QueryCarba
                    + QueryAcc
                    + " WHERE NoReservasi = '" + NoReservasi + "'"
                    );

                SaveTagihan(NoReservasi);

                DataTable rs = Db.Rs("SELECT "
                    + " NoReservasi AS [No. Reservasi Sistem]"
                    + ",NoReservasi2 AS [No. Reservasi]"
                    + ",NoUnit AS [Unit]"
                    + ",NoUrut AS [No. Urut]"
                    + ",NoStock AS [No. Stock]"
                    + ",MS_CUSTOMER.Nama AS [Customer 1]"
                    + ",MS_CUSTOMER.Nama2 AS [Customer 2]"
                    + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS [Agent]"
                    + ",CONVERT(varchar,Tgl,106) AS [Tanggal]"
                    + ",CONVERT(varchar,TglExpire,100) AS [Batas Waktu]"
                    + ",Netto AS [Nilai Pengikatan]"
                    + ",MS_RESERVASI.Skema AS [Skema]"
                    + ",NoQueue AS [NUP]"
                    + ",MS_RESERVASI.NoRefferatorAgent"
                    + ",MS_RESERVASI.NoRefferatorCustomer"
                    + ",Gross AS [Harga]"
                    + ",Supervisor AS [Supervisor]"
                    + ",MS_AGENT.SalesManager AS [Manager]"
                    + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                    + " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
                    + " WHERE NoReservasi = " + NoReservasi
                    );

                Db.Execute("EXEC spLogReservasi"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    + ",'" + NoReservasi.ToString().PadLeft(5, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_RESERVASI_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_RESERVASI_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                //floor plan
                string Peta = Db.SingleString("SELECT Peta "
                    + " FROM MS_UNIT INNER JOIN MS_RESERVASI ON MS_UNIT.NoStock = MS_RESERVASI.NoStock "
                    + " WHERE NoReservasi = '" + NoReservasi + "'");
                Func.GenerateFP(Peta);

                //Save TTR
                var context = GlobalHost.ConnectionManager.GetHubContext<ClosingUnit>();
                context.Clients.All.invokeStatus(NoStock);

                string NoTTR = "";
                string NoTTS = "";
                if (Convert.ToDecimal(nilai.Text) > 0)
                {
                    NoTTR = SaveTTR(NoReservasi, NoStock, NoCustomer);
                    NoTTS = SaveTTS(NoReservasi, NoStock, Convert.ToInt32(NoCustomer));
                    Response.Redirect("ReservasiDaftar3.aspx?NoReservasi=" + NoReservasi + "&NoTTR=" + NoTTR + "&NoTTS=" + NoTTS);
                }
                else
                    Response.Redirect("ReservasiDaftar3.aspx?NoReservasi=" + NoReservasi);
            }
        }

        private void SaveTagihan(int NoReservasi)
        {
            int CaraBayar = Convert.ToInt32(skema.SelectedValue);

            if (CaraBayar != 0)
            {
                string RumusDiskon = Db.SingleString("SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + CaraBayar);
                string RumusDiskon2 = Db.SingleString("SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + CaraBayar);
                string RumusBunga = Db.SingleString("SELECT Bunga FROM REF_SKEMA WHERE Nomor = " + CaraBayar);
                decimal Gross = Db.SingleDecimal("SELECT Gross FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "'");
                decimal Netto = Gross + Func.NominalBunga2(RumusBunga, Gross) - Func.NominalDiskon2(RumusDiskon, Gross);

                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

                string ParamID = "PLIncludePPN" + Project;
                bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";

                decimal NilaiPPN = 0;
                decimal NilaiReservasi = 0;
                decimal DPP = 0;

                if (includeppn)
                    DPP = Math.Round(Netto / (decimal)1.1);
                else
                    DPP = Netto;


                if (sifatppn.SelectedIndex == 1)
                {
                    if (includeppn)
                    {
                        NilaiPPN = Math.Round(Netto - DPP);
                    }
                    else
                    {
                        NilaiPPN = Math.Round(DPP * (decimal)0.1);
                    }
                }

                NilaiReservasi = DPP + NilaiPPN;
                decimal PPN = Math.Round(NilaiReservasi - DPP);

                Db.Execute("UPDATE MS_RESERVASI"
                    + " SET IncludePPN = " + ((includeppn) ? "1" : "0")
                    + " ,PPN = " + ((sifatppn.SelectedIndex == 1) ? "1" : "0")
                    + " ,NilaiPPN = " + NilaiPPN
                    + " ,NilaiDPP = " + DPP
                    + " ,NilaiReservasi = " + NilaiReservasi
                    + " ,DiskonRupiah = " + Func.NominalDiskon2(RumusDiskon, Gross)
                    + " ,DiskonPersen = '" + RumusDiskon + "'"
                    + " ,BungaNominal = " + Func.NominalBunga2(RumusBunga, Gross)
                    + " ,BungaPersen = '" + RumusBunga + "'"
                    + " WHERE NoReservasi = '" + NoReservasi + "'");

                /* DISKON TAMBAHAN SAAT CLOSING */
                decimal GrossAfterDiskon = Func.SetelahDiskon(RumusDiskon, Gross);
                decimal GrossAfterBunga = Func.SetelahBunga(RumusBunga, Gross);
                decimal ND = Gross - GrossAfterDiskon;
                decimal NB = Gross - GrossAfterBunga;
                decimal HargaSetelahBunga = GrossAfterBunga - ND;

                decimal DiskonTambahan = 0;
                if (jenisDiskon.SelectedIndex == 0)
                {   //Diskon lum sum
                    DiskonTambahan = Convert.ToDecimal(diskonLumpSum.Text);
                }
                else if (jenisDiskon.SelectedIndex == 1)
                {   //Diskon % bertingkat
                    decimal coba = 0, totaldisc = 0;
                    string[] DiscTambahPersen = diskontambahPersen.Text.Split('+');
                    decimal dpp = HargaSetelahBunga;

                    if (diskontambahPersen.Text != "")
                    {
                        for (int a = 0; a <= DiscTambahPersen.GetUpperBound(0); a++)
                        {
                            coba = Math.Round(Convert.ToDecimal(DiscTambahPersen[a]) * dpp / (decimal)100);
                            dpp -= coba;
                            totaldisc += coba;
                        }
                    }
                    else
                    {
                        totaldisc = 0;
                    }

                    DiskonTambahan = totaldisc;
                }

                Db.Execute("UPDATE MS_RESERVASI"
                    + " SET DiskonTambahan = " + DiskonTambahan
                    + " WHERE NoReservasi = '" + NoReservasi + "'");

                //tagihan reservasi
                string[,] x = Func.Breakdown(CaraBayar, Netto, Convert.ToDateTime(tgl.Text));
                for (int i = 0; i <= x.GetUpperBound(0); i++)
                {
                    if (!Response.IsClientConnected) break;

                    Db.Execute("EXEC spReservasiTagihanDaftar"
                        + " '" + NoReservasi + "'"
                        + ",'" + x[i, 2] + "'"
                        + ",'" + Convert.ToDateTime(x[i, 3]) + "'"
                        + ", " + Convert.ToDecimal(x[i, 4])
                        + ",'" + x[i, 1] + "'"
                        );
                }
            }
        }

        private string SaveTTS(int NoReservasi, string NoStock, int NoCustomer)
        {
            DateTime TglTTS = Convert.ToDateTime(tgl.Text);
            string Customer = Cf.Str(Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer));
            string CaraBayar = rbCaraBayar.SelectedValue;
            string NoUnit = Db.SingleString("select ISNULL(NoUnit, '') from " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT where NoStock = '" + NoStock + "'");
            string Project = Db.SingleString("SELECT ISNULL(Project, '') FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT Where NoStock = '" + NoStock + "'");
            string NoTTS2 = Numerator.TTS(TglTTS.Month, TglTTS.Year, Project);

            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSRegistrasi"
                + " '" + TglTTS + "'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'JUAL'"
                + ",''" //Ref / NoKontrak
                + ",'" + NoUnit + "'"
                + ",'" + Customer + "'"
                + ",'" + CaraBayar + "'"
                + ",'PEMBAYARAN RESERVASI'"
                );

            int NoTTS = Db.SingleInteger("SELECT TOP 1 NoTTS FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS ORDER BY NoTTS DESC");
            
            //update nilai bayar di TTSnya
            decimal nBayar = Convert.ToDecimal(nilai.Text);
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET "
                + "  Total=" + nBayar
                + ", Acc = '" + ddlAcc.SelectedValue + "'"
                + ", NoTTS2 = '" + NoTTS2 + "'"
                + ", Project = '" + Project + "'"
                + ", NamaProject = '" + Db.SingleString("select ISNULL(Nama, '') from " + Mi.DbPrefix + "SECURITY..REF_PROJECT where Project = '" + Project + "'") + "'"
                + ", NoReservasi = '" + NoReservasi + "'"
                //+ ", NoKK = '" + Cf.Str(noKK) + "'"
                //+ ", Catatan = '" + Cf.Str(ket.Text) + "'"
                + " WHERE NoTTS='" + NoTTS + "'");

            //update ke ms_reservasi_tagihan.. buat keperluan FOBO nantinya
            int NoUrutTagihanReservasi = Db.SingleInteger("Select TOP 1 ISNULL(NoUrut, 0) from MS_RESERVASI_TAGIHAN where NoReservasi = '" + NoReservasi + "' and Tipe = 'BF' order by NoUrut");
            Db.Execute("Update MS_RESERVASI_TAGIHAN set NoTTS = '" + NoTTS + "' where NoReservasi = '" + NoReservasi + "' and NoUrut = '" + NoUrutTagihanReservasi + "'");
            
            DataTable rs = Db.Rs("SELECT "
                + " CONVERT(varchar, TglTTS, 106) AS [Tanggal]"
                + ",Tipe"
                + ",Ref AS [Ref.]"
                + ",Unit"
                + ",Customer"
                + ",CaraBayar AS [Cara Bayar]"
                + ",Ket AS [Keterangan]"
                + ",Total"
                + ",NoBG AS [No. BG]"
                + ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

            string KetLog =
                "***PEMBAYARAN RESERVASI : " + NoReservasi + "<br/><br/>"
                + Cf.LogCapture(rs);

            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogTTS"
                + " 'REGIS'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetLog + "'"
                + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            return NoTTS.ToString();
        }

        protected string SaveTTR(int NoReservasi, string NoStock, string NoCustomer)
        {
            string Lokasi = Db.SingleString("SELECT Lokasi FROM MS_UNIT WHERE NoStock = " + NoStock);
            string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = " + NoStock);
            int x = Db.SingleInteger("SELECT COUNT(NoTTR) FROM MS_TTR") + 1;
            string NoTTR = x.ToString();

            while (Db.SingleInteger("SELECT COUNT(NoTTR) FROM MS_TTR WHERE NoTTR = '" + NoTTR + "'") > 0)
            {
                x++;
                NoTTR = x.ToString();
            }

            string NoUnit = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            string Customer = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer);
            string CaraBayar = rbCaraBayar.SelectedValue;
            string KetTTR = Cf.Str(ketttr.Text);
            decimal Total = Convert.ToDecimal(nilai.Text);

            string NoBG = "", TglBG = "NULL";
            if (rbCaraBayar.SelectedValue == "BG")
            {
                NoBG = Cf.Str(nobg.Text);
                TglBG = "'" + Convert.ToDateTime(tglbg.Text) + "'";
            }
            
            Db.Execute("EXEC spTTRRegistrasi"
                + " '" + NoTTR + "'"
                + ", '" + NoReservasi + "'"
                + ", '" + Act.UserID + "'"
                + ", '" + Act.IP + "'"
                + ", '" + NoUnit + "'"
                + ", '" + Customer + "'"
                + ", '" + CaraBayar + "'"
                + ", '" + KetTTR + "'"
                + ", " + Total
                + ", '" + NoBG + "'"
                + ", " + TglBG
                );

            Db.Execute("UPDATE MS_TTR SET Acc = '" + ddlAcc.SelectedValue + "' WHERE NoTTR = '" + NoTTR + "'");

            DataTable rs = Db.Rs("SELECT "
                + " CONVERT(varchar, TglTTR, 106) AS [Tanggal]"
                + ", NoReservasi AS [No. Reservasi]"
                + ",Unit"
                + ",Customer"
                + ",CaraBayar AS [Cara Bayar]"
                + ",Ket AS [Keterangan]"
                + ",Total"
                + ",NoBG AS [No. BG]"
                + ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
                + " FROM MS_TTR WHERE NoTTR = '" + NoTTR + "'");

            string KetLog = Cf.LogCapture(rs);

            Db.Execute("EXEC spLogTTR"
                + " 'REGIS'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetLog + "'"
                + ",'" + NoTTR + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTR_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE MS_TTR_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            return NoTTR;
        }

        private string NoCustomer
        {
            get
            {
                return Cf.Pk(nocustomer.Text);
            }
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }

        protected void skema_SelectedIndexChanged(object sender, EventArgs e)
        {
            CaraBayar();
            if (skema.SelectedIndex > 0)
            {
                string RumusDiskon = Db.SingleString(
                    "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                string Ket = Db.SingleString(
                    "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                //string NamaCashKeras = Db.SingleString(
                //    "SELECT Nama FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                string CaraBayar2 = Db.SingleString(
                    "SELECT Jenis FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                string RumusBunga = Db.SingleString(
                "SELECT Bunga FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);
                string Ket2 = Db.SingleString(
                "SELECT Bungaket FROM REF_SKEMA WHERE Nomor = " + skema.SelectedValue);

                decimal PL = Db.SingleDecimal("SELECT ISNULL(PriceList,0) FROM MS_PRICELIST WHERE NoStock = '" + NoStock + "' AND NoSkema = " + skema.SelectedValue);

                pl.Text = Cf.Num(PL);

                if (CaraBayar2.Contains("CASH KERAS") == true)
                {
                    carabayar2.SelectedIndex = 0;
                    carabayar2.Enabled = false;
                }
                else if (CaraBayar2.Contains("CASH BERTAHAP") == true)
                {
                    carabayar2.SelectedIndex = 1;
                    carabayar2.Enabled = false;
                }
                else if (CaraBayar2.Contains("KPR") == true)
                {
                    carabayar2.SelectedIndex = 2;
                    carabayar2.Enabled = false;
                }
                else
                {
                    carabayar2.ClearSelection();
                    carabayar2.Enabled = false;
                }

                carabayar2.Enabled = false;
                diskon2.Text = RumusDiskon;
                //nilaiDiskon.Text = Cf.Num((Convert.ToDecimal(RumusDiskon) * Convert.ToDecimal(Pricelist.Text) /100));
                diskonket.Text = Ket;
                bunga2.Text = RumusBunga;
                bungaket.Text = Ket2;

                SetBunga();
                SetDiskon();
            }
            else
            {
                diskon2.Text = "0";
                bunga2.Text = "0";
                nilaiBunga.Text = "0";
            }
        }

        private void CaraBayar()
        {
            string x = skema.SelectedValue;

            if (x.Contains("KERAS") == true)
            {
                carabayar2.SelectedIndex = 0;
            }
            else if (skema.SelectedValue.Contains("BERTAHAP") == true)
            {
                carabayar2.SelectedIndex = 1;
            }
            else if (skema.SelectedValue.Contains("KPR") == true)
            {
                carabayar2.SelectedIndex = 2;
            }
            else
            {
                carabayar2.ClearSelection();
                carabayar2.Enabled = true;
            }
        }

        protected void jenisDiskon_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (jenisDiskon.SelectedIndex == 0)
            {
                divLumpSum.Visible = true;
                divPersenBertingkat.Visible = false;
            }
            else if (jenisDiskon.SelectedIndex == 1)
            {
                divLumpSum.Visible = false;
                divPersenBertingkat.Visible = true;
            }
            //nilaiBunga.Text = bunga2.Text = "0";
        }

        protected void diskon2_TextChanged(object sender, EventArgs e)
        {
            if (skema.SelectedIndex > 0)
            {
                SetDiskon2();
            }
            diskon2.Focus();
        }

        protected void bunga2_TextChanged(object sender, EventArgs e)
        {
            if (rbCaraBayar.SelectedIndex > 0)
            {
                SetBunga2();
            }
        }

        // DISKON ---------------
        protected decimal SetDiskon()
        {
            decimal Gross = Convert.ToDecimal(pl.Text);
            decimal Bunga = SetBunga();
            decimal Gross2 = Gross + Bunga;

            int CaraBayar = Convert.ToInt32(skema.SelectedValue);
            string RumusDiskon = Db.SingleString("SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + CaraBayar);

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

            decimal diskon = Math.Round(Func.NominalDiskon(RumusDiskon, Gross2), 0);

            if (diskon == 0)
            {
                nilaiDiskon.Text = "0";
            }
            else
            {
                nilaiDiskon.Text = Cf.Num(Math.Round(diskon, 0).ToString());
            }

            return diskon;
        }

        private void SetDiskon2()
        {
            decimal Gross = Convert.ToDecimal(pl.Text);
            decimal Bunga = nilaiBunga.Text != "" ? Convert.ToDecimal(nilaiBunga.Text) : 0;
            decimal Gross2 = Gross + Bunga;

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
                nilaiDiskon.Text = "0";
            }
            else
            {
                nilaiDiskon.Text = Cf.Num(Math.Round(diskon, 0).ToString());
            }
        }

        // BUNGA -------

        protected decimal SetBunga()
        {
            int CaraBayar = Convert.ToInt32(skema.SelectedValue);
            string RumusBunga = Db.SingleString("SELECT Bunga FROM REF_SKEMA WHERE Nomor = " + CaraBayar);

            decimal Gross = Convert.ToDecimal(pl.Text);
            decimal Gross2 = Gross;

            string[] x = RumusBunga.Split('+');

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != "")
                {
                    decimal y = Convert.ToDecimal(x[i]) * (decimal)1;
                    if (i < (x.Length - 1))
                        sb.Append(y.ToString() + "+");
                    else
                        sb.Append(y.ToString());
                }
            }

            decimal bunga = Math.Round(Func.NominalDiskon(RumusBunga, Gross2), 0);

            if (bunga == 0)
            {
                nilaiBunga.Text = "0";
            }
            else
            {
                nilaiBunga.Text = Cf.Num(Math.Round(bunga, 0).ToString());
            }

            return bunga;
        }

        private void SetBunga2()
        {
            decimal Gross = Convert.ToDecimal(pl.Text); //Db.SingleDecimal("SELECT PriceList FROM MS_UNIT"
            //+ " WHERE NoStock = '" + NoStock + "'");; + surcharge;

            string RumusBunga = bunga2.Text;
            string[] x = RumusBunga.Split('+');

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
            bunga2.Text = sb.ToString();

            decimal bunga = Func.NominalDiskon2(RumusBunga, Gross);
            if (bunga == 0)
            {
                nilaiBunga.Text = "0";
            }
            else
            {
                nilaiBunga.Text = Cf.Num(Math.Round(bunga, 0).ToString());
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
