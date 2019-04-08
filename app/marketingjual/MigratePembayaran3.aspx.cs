using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class MigratePembayaran3 : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();

                Js.Confirm(this, "Lanjutkan proses migrate pembayaran?");
            }

            FillTable();
        }

        private void Fill()
        {
            cancel.Attributes["onclick"] = "location.href='MigratePembayaran2.aspx?No=" + NoKontrak + "'";

            string strSql = "SELECT * "
                + " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rsHeader = Db.Rs(strSql);

            if (rsHeader.Rows.Count > 0)
            {
                nokontrak.Text = rsHeader.Rows[0]["NoKontrak"].ToString();
                agent.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rsHeader.Rows[0]["NoAgent"] + "'");
                nounit.Text = rsHeader.Rows[0]["NoUnit"].ToString();
                cust.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = '" + rsHeader.Rows[0]["NoCustomer"] + "'");
                nilaikontrak.Text = Cf.Num(rsHeader.Rows[0]["NilaiKontrak"]);
                skema.Text = rsHeader.Rows[0]["Skema"].ToString();
                notts.Text = NoTTS;
            }
            else
            {
                cek.Text = "Kontrak No Exists";
                save.Enabled = false;
            }
        }

        private void FillTable()
        {
            list.Controls.Clear();

            rs = Db.Rs("SELECT * FROM MIGRATE_PEMBAYARAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTTS = '" + NoTTS + "' AND Approved = 0");
            Rpt.NoData(list, rs, "Tidak ada pembayaran untuk kontrak tersebut.");

          
          for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                //No
                Label l;
                TextBox bx;
                RadioButtonList rbl;
                HtmlInputButton btn;
                DropDownList ddl;

                l = new Label();
                l.Text = "<tr>" + "<td>" + (i + 1).ToString() + ".</td>";
                
                 
                list.Controls.Add(l);
                             
              if (i == 0)
                {
                    //Tgl. TTS
                    l = new Label();
                    l.Text = "<td>";
                    list.Controls.Add(l);

                    bx = new TextBox();
                    bx.ID = "tgltts_" + Convert.ToString(i);
                    bx.Width = 75;
                    bx.CssClass = "txt_center";
                    bx.Text = Cf.Day(rs.Rows[i]["TglTTS"]);
                    bx.Attributes["style"] = "font:8pt";
                    list.Controls.Add(bx);

                    l = new Label();
                    l.Text = "&nbsp;";
                    list.Controls.Add(l);

                    btn = new HtmlInputButton();
                    btn.Value = "...";
//                    btn.Attributes["onclick"] = "openCalendar('tgltts_" + i.ToString() + "')";
                    btn.Attributes["class"] = "btn";
                    list.Controls.Add(btn);

                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);

                    //No. Kwitansi
                    l = new Label();
                    l.Text = "<td>";
                    list.Controls.Add(l);

                    bx = new TextBox();
                    bx.ID = "nobkm_" + Convert.ToString(i);
                    bx.Width = 140;
                    bx.CssClass = "txt";
                    bx.Text = rs.Rows[i]["NoBKM"].ToString();
                    bx.MaxLength = 50;
                    bx.Attributes["style"] = "font:8pt";
                    list.Controls.Add(bx);

                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);

                    //Tgl. Kwitansi
                    l = new Label();
                    l.Text = "<td>";
                    list.Controls.Add(l);

                    bx = new TextBox();
                    bx.ID = "tglbkm_" + Convert.ToString(i);
                    bx.Width = 75;
                    bx.CssClass = "txt_center";
                    bx.Text = Cf.Day(rs.Rows[i]["TglBKM"]);
                    bx.Attributes["style"] = "font:8pt";
                    list.Controls.Add(bx);

                    l = new Label();
                    l.Text = "&nbsp;";
                    list.Controls.Add(l);

                    btn = new HtmlInputButton();
                    btn.Value = "...";
//                    btn.Attributes["onclick"] = "openCalendar('tglbkm_" + i.ToString() + "')";
                    btn.Attributes["class"] = "btn";
                    list.Controls.Add(btn);

                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);

                    //Cara Bayar
                    l = new Label();
                    l.Text = "<td>";
                    list.Controls.Add(l);

                    rbl = new RadioButtonList();
                    rbl.ID = "cb_" + i.ToString();
                    rbl.Items.Add(new ListItem("TN = Tunai", "TN"));
                    rbl.Items.Add(new ListItem("KK = Kartu Kredit", "KK"));
                    rbl.Items.Add(new ListItem("KD = Kartu Debit", "KD"));
                    rbl.Items.Add(new ListItem("TR = Transfer Bank", "TR"));
                    rbl.Items.Add(new ListItem("BG = Cek Giro", "BG"));
                    rbl.Items.Add(new ListItem("MB = Merchant Banking", "MB"));
                    rbl.SelectedValue = rs.Rows[i]["CaraBayar"].ToString();
                    rbl.Width = 200;
                    list.Controls.Add(rbl);

                    l = new Label();
                    l.ID = "cbr_" + i.ToString();
                    l.CssClass = "err";
                    l.Text = rs.Rows[i]["CaraBayar"].ToString();
                    list.Controls.Add(l);

                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);

                    //Rekening
                    l = new Label();
                    l.Text = "<td>";
                    list.Controls.Add(l);

                    ddl = new DropDownList();
                    ddl.ID = "rek_" + Convert.ToString(i);
                    ddl.Items.Add(new ListItem("Pilih Rekening :", ""));
                    DataTable rsrek = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_ACC");
                    for (int j = 0; j < rsrek.Rows.Count; j++)
                    {
                        string v = rsrek.Rows[j]["Acc"].ToString();
                        string t = v + " : " + rsrek.Rows[j]["Bank"] + " " + rsrek.Rows[j]["Rekening"];
                        ddl.Items.Add(new ListItem(t, v));
                    }
                    ddl.SelectedValue = rs.Rows[i]["Rekening"].ToString();
                    list.Controls.Add(ddl);

                    l = new Label();
                    l.ID = "rekr_" + i.ToString();
                    l.CssClass = "err";
                    l.Text = "<br/>" + rs.Rows[i]["Rekening"].ToString();
                    list.Controls.Add(l);

                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);
                }
                else
                {
                    //Rekening
                    l = new Label();
                    l.Text = "<td colspan='5'>&nbsp;</td>";
                    list.Controls.Add(l);
                }

                //Nilai
                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "nilai_" + Convert.ToString(i);
                bx.CssClass = "txt_num";
                bx.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["Nilai"]), 0));
                bx.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                bx.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                bx.Attributes["onblur"] = "CalcBlur(this);";
                bx.Width = 90;
                bx.Attributes["style"] = "font:8pt";
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                //Nama Tagihan
                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                ddl = new DropDownList();
                ddl.ID = "tag_" + Convert.ToString(i);
                ddl.Items.Add(new ListItem("Pilih Tagihan :", ""));
                DataTable rst = Db.Rs("SELECT * FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");
                for (int j = 0; j < rst.Rows.Count; j++)
                {
                    string v = rst.Rows[j]["NoUrut"].ToString();
                    string t = rst.Rows[j]["NamaTagihan"].ToString();
                    ddl.Items.Add(new ListItem(t, v));
                }
                ddl.SelectedValue = rs.Rows[i]["NamaTagihan"].ToString();
                list.Controls.Add(ddl);

                l = new Label();
                l.ID = "tagr_" + i.ToString();
                l.CssClass = "err";
                l.Text = "<br/>" + rs.Rows[i]["NamaTagihan"].ToString();
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                l = new Label();
                l.ID = "err_" + Convert.ToString(i);
                l.CssClass = "err";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</td></tr>";
                list.Controls.Add(l);
            }
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            int tts = Db.SingleInteger("SELECT COUNT(NoTTS) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "'");
            if (tts > 0)
            {
                x = false;
                cek.Text = "No. TTS Exists";
            }
            else
                cek.Text = "";

            rs = Db.Rs("SELECT * FROM MIGRATE_PEMBAYARAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTTS = '" + NoTTS + "' AND Approved = 0");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TextBox tgltts = (TextBox)list.FindControl("tgltts_" + i);
                TextBox nobkm = (TextBox)list.FindControl("nobkm_" + i);
                TextBox tglbkm = (TextBox)list.FindControl("tglbkm_" + i);
                RadioButtonList cb = (RadioButtonList)list.FindControl("cb_" + i);
                TextBox nilai = (TextBox)list.FindControl("nilai_" + i);
                DropDownList tag = (DropDownList)list.FindControl("tag_" + i);
                DropDownList rek = (DropDownList)list.FindControl("rek_" + i);
                Label err = (Label)list.FindControl("err_" + i);

                if (!Cf.isTgl(tgltts))
                {
                    x = false;
                    if (s == "") s = tgltts.ID;
                    err.Text = "Tanggal";
                }
                else if (Cf.isEmpty(nobkm))
                {
                    x = false;
                    if (s == "") s = nobkm.ID;
                    err.Text = "Kosong";
                }
                else if (!Cf.isTgl(tglbkm))
                {
                    x = false;
                    if (s == "") s = tglbkm.ID;
                    err.Text = "Tanggal";
                }
                else if (cb.SelectedIndex < 0)
                {
                    x = false;
                    if (s == "") s = cb.ID;
                    err.Text = "Pilih";
                }
                else if (!Cf.isMoney(nilai))
                {
                    x = false;
                    if (s == "") s = nilai.ID;
                    err.Text = "Angka";
                }
                else if (tag.SelectedIndex == 0)
                {
                    x = false;
                    if (s == "") s = tag.ID;
                    err.Text = "Pilih";
                }
                else if (rek.SelectedIndex == 0)
                {
                    x = false;
                    if (s == "") s = tag.ID;
                    err.Text = "Pilih";
                }
                else
                    err.Text = "";
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
            if (valid())
            {
                DataTable tts = Db.Rs("SELECT TOP 1 * FROM MIGRATE_PEMBAYARAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTTS = '" + NoTTS + "' AND Approved = 0");
                if (tts.Rows.Count > 0)
                {
                    DataTable kon = Db.Rs("SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    DataTable cus = Db.Rs("SELECT * FROM MS_CUSTOMER WHERE NoCustomer = '" + kon.Rows[0]["NoCustomer"] + "'");

                    TextBox tgltts = (TextBox)list.FindControl("tgltts_0");
                    TextBox nobkm = (TextBox)list.FindControl("nobkm_0");
                    TextBox tglbkm = (TextBox)list.FindControl("tglbkm_0");
                    RadioButtonList cb = (RadioButtonList)list.FindControl("cb_0");
                    DropDownList rek = (DropDownList)list.FindControl("rek_0");

                    DateTime TglTTS = Convert.ToDateTime(tgltts.Text);
                    string Unit = Cf.Str(kon.Rows[0]["NoUnit"]);
                    string Customer = Cf.Str(cus.Rows[0]["Nama"]);
                    string CaraBayar = cb.SelectedValue;
                    string Ket = "";
                    //decimal AdminBank = 0;
                    //if (Cf.isMoney(admBank)) { AdminBank = Convert.ToDecimal(admBank.Text); }
                    //decimal LebihBayar = 0;
                    //if (Cf.isMoney(lebihBayar)) { LebihBayar = Convert.ToDecimal(lebihBayar.Text); }

                    //// Logic Create Format Penomoran TTS Reset Per Year
                    //// Get Year Terakhir TTS 
                    //int rsYearTTS = 0;
                    //if (Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE Year(TglTTS) = " + TglTTS.Year) > 0)
                    //{
                    //    rsYearTTS = Db.SingleInteger("SELECT TOP 1 YEAR(TglTTS) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE Year(TglTTS) = " + TglTTS.Year + " ORDER BY TglTTS DESC");
                    //}
                    //int rsYearInput = TglTTS.Year;
                    //string idTTS = "1";
                    //int PadLeftId = 6;

                    //// Set increment Nomor jika sama tahun
                    //if (rsYearTTS == rsYearInput)
                    //{
                    //    int c = Db.SingleInteger("SELECT COUNT(NoTTS) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE Year(TglTTS) = " + TglTTS.Year);

                    //    bool hasfound = false;
                    //    while (!hasfound)
                    //    {
                    //        if (!Response.IsClientConnected) break;
                    //        c++;

                    //        if (isUniqueTTS(c, TglTTS, PadLeftId))
                    //        {
                    //            idTTS = c.ToString();
                    //            hasfound = true;
                    //        }
                    //    }
                    //}

                    //string NoTTS = idTTS.PadLeft(PadLeftId, '0')
                    //             + "/" + "TTS"
                    //             + "/" + Cf.Roman(TglTTS.Month)
                    //             + "/" + TglTTS.Year.ToString();



                    Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSRegistrasiMigrate"
                        + " '" + NoTTS + "'"
                        + ",'" + TglTTS + "'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'JUAL'"
                        + ",'" + NoKontrak + "'"
                        + ",'" + Unit + "'"
                        + ",'" + Customer + "'"
                        + ",'" + CaraBayar + "'"
                        + ",'" + Ket + "'"
                        );

                    ////khusus cek giro
                    //if (carabayar.SelectedValue == "BG")
                    //{
                    //    string NoBG = Cf.Pk(nobg.Text);
                    //    DateTime TglBG = Convert.ToDateTime(tglbg.Text);

                    //    Db.Execute("EXEC spTTSRegistrasiBG"
                    //        + " '" + NoTTS + "'"
                    //        + ",'" + NoBG + "'"
                    //        + ",'" + TglBG + "'"
                    //        );
                    //}

                    //if (anonim.SelectedIndex > 0)
                    //{
                    //    Db.Execute("UPDATE MS_TTS SET ANOID = '" + Cf.Str(anonim.SelectedValue) + "' WHERE NoTTS = '" + NoTTS + "'");
                    //    Db.Execute("UPDATE CbANO SET StatusANO = 1 WHERE ANOID='" + Cf.Str(anonim.SelectedValue) + "' ", Cnn);
                    //}

                    //DataTable rs3 = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + Ref + "'");
                    //decimal nilaiTTR = 0;
                    //if (rs3.Rows[0]["NoTTR"].ToString() != "")
                    //{
                    //    string NoStock = Db.SingleString("SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Ref + "'");
                    //    int NoReservasi = Db.SingleInteger("SELECT NoReservasi FROM ISC064_MARKETINGJUAL..MS_RESERVASI WHERE NoStock = '" + NoStock + "'");
                    //    DataTable rs2 = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_TTR WHERE NoReservasi = '" + NoReservasi + "' AND Status = 'BARU'");

                    //    string NoTTR = Db.SingleString("SELECT NoTTR FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Ref + "'");

                    //    if (rs2.Rows.Count > 0)
                    //    {
                    //        nilaiTTR = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_MARKETINGJUAL..MS_TTR WHERE NoTTR = '" + NoTTR + "'");

                    //        Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_TTR SET Status = 'POST' WHERE NoReservasi = '" + NoReservasi + "'");

                    //    }

                    //}

                    //decimal total2 = Convert.ToDecimal(gt.Text) + nilaiTTR - AdminBank;
                    //Db.Execute("UPDATE MS_TTS"
                    //    + " SET Acc = '" + tts.Rows[0]["Rekening"] + "'"
                    //    + ", SumberBayar = 0"
                    //    //+ ", AdminBank='" + AdminBank + "' "
                    //    + ", Total2 = '" + total2 + "'"
                    //    //+ ", LebihBayar = '" + LebihBayar + "'"
                    //    + " WHERE NoTTS = '" + NoTTS + "'");

                    System.Text.StringBuilder alokasi = new System.Text.StringBuilder();
                    int NoTagihan = 0;
                    decimal total = 0;
                    DataTable rsa = Db.Rs("SELECT * FROM MIGRATE_PEMBAYARAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTTS = '" + NoTTS + "' AND Approved = 0");
                    for (int i = 0; i < rsa.Rows.Count; i++)
                    {
                        TextBox lunas = (TextBox)list.FindControl("nilai_" + i);
                        DropDownList tag = (DropDownList)list.FindControl("tag_" + i);
                        string Tipe = Db.SingleString("SELECT Tipe FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" + tag.SelectedValue + "'");

                        if (lunas.Text != "")
                        {
                            NoTagihan = Convert.ToInt16(tag.SelectedValue);
                            string NamaTagihan = Cf.Str(tag.SelectedItem.Text)
                                + " (" + Tipe + ")";
                            decimal Nilai = Convert.ToDecimal(lunas.Text);// +nilaiTTR;

                            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSAlokasi "
                                + " '" + NoTTS + "'"
                                + ", " + NoTagihan
                                + ", " + Nilai
                                );
                            total += Nilai;

                            alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");
                        }
                    }

                    Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                        + " SET Acc = '" + rek.SelectedValue + "'"
                        + ", SumberBayar = 0"
                        //+ ", AdminBank='" + AdminBank + "' "
                        + ", Total2 = '" + total + "'"
                        //+ ", LebihBayar = '" + LebihBayar + "'"
                        + " WHERE NoTTS = '" + NoTTS + "'");

                    //BKM
                    if (tts.Rows[0]["NoBKM"] != "")
                    {
                        DateTime TglBKM = Convert.ToDateTime(tglbkm.Text);
                        string NoBKM = nobkm.Text;
                        Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spPostingTTS '" + NoTTS + "','" + TglBKM + "'");
                        Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET ManualBKM = ManualTTS, NoBKM = '" + NoBKM + "' WHERE NoTTS = '" + NoTTS + "'");
                        Db.Execute("UPDATE MS_PELUNASAN SET NoBKM = '" + NoBKM + "' WHERE NoTTS = '" + NoTTS + "'");
                    }

                    Db.Execute("UPDATE MIGRATE_PEMBAYARAN SET Approved = 1 WHERE NoKontrak = '" + NoKontrak + "' AND NoTTS = '" + NoTTS + "'");

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
                        + ", Acc AS [Rekening Bank]"
                        + " FROM " + Mi.DbPrefix + "FINANCEAR.. MS_TTS WHERE NoTTS = '" + NoTTS + "'");

                    string KetLog = Cf.LogCapture(rs)
                        + "<br>***ALOKASI PEMBAYARAN:<br>"
                        + alokasi.ToString();

                    Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogTTS"
                        + " 'REGIS'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + NoTTS.ToString() + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }

                Response.Redirect("MigratePembayaran2.aspx?No=" + NoKontrak);
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["No"]);
            }
        }

        private string NoTTS
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoTTS"]);
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
