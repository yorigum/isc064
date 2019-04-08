using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class AdjustmentKontrak : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);

                backbtn.Visible = false;
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a');";

                if (Request.QueryString["NoKontrak"] != null)
                {
                    //dari halaman pendaftaran
                    dariDaftar.Checked = true;
                    nokontrak.Text = Request.QueryString["NoKontrak"];
                    LoadKontrak();

                    //InitForm();
                    //Fill();

                    if (Request.QueryString["gross"] != null)
                        //dari halaman gross
                        cancel.Attributes["onclick"] = "location.href='ReminderGross.aspx'";
                    else
                        cancel.Attributes["onclick"] = "location.href='KontrakDaftar3.aspx?NoKontrak=" + NoKontrak + "'";
                }
                else
                {
                    Js.Focus(this, nokontrak);
                    frm.Visible = false;
                }

                //if (frm.Visible) Js.Confirm(this, "Lanjutkan proses adjustment kontrak?");

                bunga.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                bunga.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                bunga.Attributes["onblur"] = "CalcBlur(this);";

                discSkema.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                discSkema.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                discSkema.Attributes["onblur"] = "CalcBlur(this);";

                disc.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                disc.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                disc.Attributes["onblur"] = "CalcBlur(this);";

                gross.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                gross.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                gross.Attributes["onblur"] = "CalcBlur(this);";

                nilaidpp.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                nilaidpp.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                nilaidpp.Attributes["onblur"] = "CalcBlur(this);";

                nilaippn.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                nilaippn.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                nilaippn.Attributes["onblur"] = "CalcBlur(this);";
            }

            FeedBack();

        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                        + "Adjustment Kontrak Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");

            if (c == 0)
                x = false;


            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );

            return x;
        }

        private void LoadKontrak()
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                InitForm();
                Fill();

                Js.Focus(this, disc);

                //if(frm.Visible) Js.Confirm(this, "Lanjutkan proses diskon nilai kontrak?");
            }
            else
            {
                backbtn.Visible = true;
                Js.Focus(this, nokontrak);
                frm.Visible = false;
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                int count = Db.SingleInteger("SELECT COUNT(*) FROM MS_APPROVAL WHERE SumberID = '" + NoKontrak + "' AND Sumber = '" + Str.Approval("5") + "' AND Status <> 'DONE'");
                if (count == 0)
                {
                    pilih.Visible = false;
                    frm.Visible = true;

                InitForm();
                Fill();

                    Js.Focus(this, disc);
                }
                else
                {
                    pilih.Visible = true;
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Proses Adjustment untuk Kontrak tersebut belum selesai.";
                    feed.Attributes["style"] = "background-color:white;color:red;";
                }


                // if(frm.Visible) Js.Confirm(this, "Lanjutkan proses diskon nilai kontrak?");
            }
        }

        private void InitForm()
        {
            gross.Attributes["style"] = nilai.Attributes["style"] = "font:bold;";
            gross.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            //gross.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            gross.Attributes["onblur"] = "CalcBlur(this);";

            nilaidpp.Attributes["style"] = nilai.Attributes["style"] = "font:bold;";
            nilaidpp.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            //nilaidpp.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaidpp.Attributes["onblur"] = "CalcBlur(this);";

            nilaippn.Attributes["style"] = nilai.Attributes["style"] = "font:bold;";
            nilaippn.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            //nilaippn.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaippn.Attributes["onblur"] = "CalcBlur(this);";

            disc.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            //disc.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            disc.Attributes["onblur"] = "CalcBlur(this);";

            discSkema.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            //discSkema.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            discSkema.Attributes["onblur"] = "CalcBlur(this);";

            bunga.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            bunga.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            //bunga.Attributes["onblur"] = "CalcBlur(this); hitungPPN(gross, disc, this); CalcBlur(ppn,tempnum);";
            bunga.Attributes["onblur"] = "CalcBlur(this);";

            //ppn.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            //ppn.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            //ppn.Attributes["onblur"] = "CalcBlur(this);";
        }

        private void Fill()
        {
            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT "
                + " MS_UNIT.PriceListMin"
                + ",MS_KONTRAK.Gross"
                + ",MS_KONTRAK.NilaiKontrak"
                + ",MS_KONTRAK.DiskonRupiah"
                + ",MS_KONTRAK.DiskonTambahan"
                + ",MS_KONTRAK.FlagGross"
                + ",MS_KONTRAK.OutBalance"
                + ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe<>'ADM') AS TotalTagihan"
                + ",MS_KONTRAK.Skema"
                + ",MS_KONTRAK.FlagKomisi"
                + ", MS_KONTRAK.NilaiDPP"
                + ", MS_KONTRAK.NilaiPPN"
                + ", MS_KONTRAK.PPN"
                + ", MS_KONTRAK.BungaNominal"
                + ", MS_KONTRAK.JenisPPN"
                + ", MS_KONTRAK.Project"
                + " FROM MS_KONTRAK INNER JOIN MS_UNIT ON MS_KONTRAK.NoStock = MS_UNIT.NoStock"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
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
                if (ket != "") adjustinfo.Text = "Gross berubah karena " + ket + "<br><br>";

                gross.Text = Cf.Num(rs.Rows[0]["Gross"]);
                //discSkema.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["DiskonRupiah"]), 0));
                discSkema.Text = "0";
                disc.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["DiskonTambahan"]), 0));
                //decimal bng = Db.SingleDecimal("SELECT ISNULL(BungaNominal,0) FROM MS_KONTRAK WHERE NoKontrak = '"+ NoKontrak +"'");
                bunga.Text = Cf.Num(rs.Rows[0]["BungaNominal"]);
                //ppn.Text = Cf.Num(rs.Rows[0]["NilaiPPN"]);
                nilai.Text = netto.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]), 0));
                lblDPP.Text = Cf.Num(rs.Rows[0]["NilaiDPP"]);
                lblPPN.Text = Cf.Num(rs.Rows[0]["NilaiPPN"]);

                project.SelectedValue = rs.Rows[0]["Project"].ToString();
                sifatppn.SelectedValue = Convert.ToDecimal(rs.Rows[0]["NilaiPPN"]) > 0 ? "1" : "0";

                //if (Convert.ToDecimal(rs.Rows[0]["NilaiPPN"]) == 0)
                //{
                //    dpp.Text = Cf.Num( Math.Round(Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]),0));
                //}
                //else	
                //{
                //    dpp.Text = Cf.Num(Math.Round(( Convert.ToDecimal(nilai.Text) / (decimal)1.1),0));
                //}

                //Price list minimum
                decimal PriceListMin = Convert.ToDecimal(rs.Rows[0]["PriceListMin"]);
                pricemin.Text = Cf.Num(PriceListMin);
                decimal selisih = Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]) - Convert.ToDecimal(rs.Rows[0]["TotalTagihan"]);

                totaltagihan.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["TotalTagihan"]), 0));
                outofbalance.Text = "<a href=\"javascript:popJadwalTagihan('" + NoKontrak + "')\">"
                    + Cf.Num(Math.Round(selisih, 0))
                    + "</a>"
                    ;

                skema.Text = rs.Rows[0]["Skema"].ToString();

                if ((int)rs.Rows[0]["FlagKomisi"] == 0)
                    warningkomisi.Visible = false;

                setharga();
                //ppn.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["NilaiPPN"]),0));
            }
        }

        private bool datavalid()
        {
            bool x = true;

            //if(!Cf.isMoney(ppn))
            //{
            //    x = false;
            //    ppnc.Text = "Angka";
            //    //ppn.Text = "0";
            //}
            //else
            //    ppnc.Text = "";

            if (Cf.isEmpty(skema))
            {
                x = false;
                skemac.Text = "Tidak Boleh Kosong";
            }

            if (!Cf.isMoney(bunga))
            {
                x = false;
                bungac.Text = "Angka";
                //ppn.Text = "0";
            }
            else
                bungac.Text = "";

            if (!Cf.isMoney(disc))
            {
                x = false;
                discc.Text = "Angka";
                //disc.Text = "0";
            }
            else
                discc.Text = "";

            if ((sifatppn.SelectedIndex != 0) && (sifatppn.SelectedIndex != 1))
            {
                x = false;
                sifatppnc.Text = "Pilih Salah Satu";
            }
            else
                sifatppnc.Text = "";

            decimal Netto = Convert.ToDecimal(gross.Text) + Convert.ToDecimal(bunga.Text) - Convert.ToDecimal(disc.Text) - Convert.ToDecimal(discSkema.Text); 

            if (Netto < Convert.ToDecimal(pricemin.Text))
            {
                x = false;
                errorc.Text = "Nilai kontrak di bawah batas minimum";
            }
            else
                errorc.Text = "";

            if (Convert.ToDecimal(nilai.Text) < Convert.ToDecimal(pricemin.Text))

                if (!x)
                    Js.Alert(
                        this
                        , "Input Tidak Valid.\\n\\n"
                        + "Aturan Proses :\\n"
                        + "1. Nilai Kontrak harus berupa angka dan positif.\\n"
                        + "2. Nilai Kontrak harus lebih besar dari harga minimum.\\n"
                        + "3. Nilai Include dan Exclude PPN harus dipilih.\\n"
                        , "document.getElementById('" + nilai.ID + "').focus();"
                        + "document.getElementById('" + nilai.ID + "').select();"
                        );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                int nomor = Db.SingleInteger("SELECT COUNT(*) FROM MS_APPROVAL");
                nomor++;
                string NoApproval = nomor.ToString().PadLeft(7, '0');
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                DataTable rsBef = Db.Rs("SELECT "
                + " DiskonRupiah AS [Diskon dalam Rupiah]"
                + ", NilaiPPN AS [PPN]"
                + ", BungaNominal AS [BungaNominal]"
                + ",NilaiKontrak AS [Nilai Kontrak]"
                + ",Project AS [Project]"
                + ",NamaProject AS [Nama Project]"
                + ",Pers AS [Perusahaan]"
                + ",NamaPers AS [Nama Perusahaan]"
                + " FROM MS_KONTRAK"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

                decimal Gross = Convert.ToDecimal(gross.Text);
                decimal DiskonSkema = Convert.ToDecimal(discSkema.Text);
                decimal DiskonTambahan = Convert.ToDecimal(disc.Text);
                decimal bng = Convert.ToDecimal(bunga.Text);
                decimal PPN = 0;
                decimal Netto = Gross + bng;
                decimal NilaiKontrak = 0;
                string statusPPN = sifatppn.SelectedValue;
                string valueSkema = skema.Text;
                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + project.SelectedValue + "'");
                string Pers = Db.SingleString("SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + project.SelectedValue + "'");
                string NamaPers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Pers + "'");

                /* DISKON SKEMA CARA BAYAR */
                //string RumusDiskon = "";//Convert.ToString(DiskonSkema);
                //string RumusDiskon2 = "diskon";

                //Netto = Func.SetelahDiskon(RumusDiskon, Gross);
                Netto -= DiskonSkema;

                /***************************/

                /* DISKON TAMBAHAN SAAT CLOSING */
                //Db.Execute("UPDATE MS_KONTRAK SET"
                //    + " DiskonTambahan = " + DiskonTambahan
                //    + " WHERE NoKontrak = '" + NoKontrak + "'");

                Netto -= DiskonTambahan;
                /***************************/
                string ParamID = "PLIncludePPN" + project.SelectedValue;
                bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";
                decimal NilaiPPN = 0;
                decimal DPP = 0;

                if (statusPPN == "1")
                {
                    if (includeppn)
                    {
                        DPP = Math.Round(Netto / (decimal)1.1);
                        NilaiPPN = Math.Round(Netto - DPP);
                    }
                    else
                    {
                        DPP = Math.Round(Netto / (decimal)1.1);
                        NilaiPPN = Math.Round(DPP * (decimal)0.1);
                    }
                }
                else
                {
                    DPP = Netto;
                }
                NilaiKontrak = DPP + NilaiPPN;
                PPN = Math.Round(NilaiKontrak - DPP);

                string c = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'ApprovAdjustment" + Project + "'");
                if (c == "True")
                {
                    //INSERT KE MS_APPROVAL
                    Db.Execute("EXEC spApproval"
                        + "'" + NoApproval + "'"
                        + ",'" + Str.Approval("5") + "'"//untuk adjusment
                        + ",'" + NoKontrak + "'"
                        + ",'" + DateTime.Today + "'"
                        + ",'" + Project + "'"
                        );

                    //insert siapa aja yang berhak approve ke ms_approval_detil 
                    DataTable rs2 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 5 AND Project = '" + Project + "'");
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

                    //simpan before afternya ke ms_approval_adjusment
                    Db.Execute("EXEC spKontrakADJTemp"
                        + " '" + NoApproval + "'"
                        + ",'" + NoKontrak + "'"
                        + "," + Gross
                        + "," + DPP
                        + ",'" + statusPPN + "'"
                        + "," + PPN
                        + ",'" + valueSkema + "'"
                        + "," + bng
                        + "," + DiskonSkema
                        + "," + DiskonTambahan
                        + "," + NilaiKontrak
                        + ",'" + DateTime.Today + "'"
                        );

                    Func.CekKomisi(NoKontrak);

                }
                else
                {
                    //INSERT KE MS_APPROVAL
                    Db.Execute("INSERT INTO MS_APPROVAL VALUES ("
                        + "'" + NoApproval + "'"
                        + ",'" + Str.Approval("5") + "'"//untuk adjusment
                        + ",'" + NoKontrak + "'"
                        + ",'" + DateTime.Today + "'"
                        + ",'DONE'"
                        + ",'" + DateTime.Today + "'"
                        + ",'" + Project + "')"
                        );

                    //simpan before afternya ke ms_approval_adjusment
                    Db.Execute("EXEC spKontrakADJTemp"
                        + " '" + NoApproval + "'"
                        + ",'" + NoKontrak + "'"
                        + "," + Gross
                        + "," + DPP
                        + ",'" + statusPPN + "'"
                        + "," + PPN
                        + ",'" + valueSkema + "'"
                        + "," + bng
                        + "," + DiskonSkema
                        + "," + DiskonTambahan
                        + "," + NilaiKontrak
                        + ",'" + DateTime.Today + "'"
                        );

                    string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
                    DataTable rs3 = Db.Rs(strSql);

                    if (rs3.Rows.Count != 0)
                    {
                        Db.Execute("UPDATE MS_KONTRAK "
                                    + " SET"
                                    + " NilaiKontrak = '" + NilaiKontrak + "'"
                                    + ", Gross = '" + Gross + "'"
                                    + ", DiskonRupiah = '" + DiskonSkema + "'"
                                    + ", DiskonTambahan = '" + DiskonTambahan + "'"
                                    + ", BungaNominal = '" + bng + "'"
                                    + ", NilaiDPP = '" + DPP + "'"
                                    + ", NilaiPPN = '" + PPN + "'"
                                    + ", PPN = '" + statusPPN + "'"
                                    + ", Skema = '" + valueSkema + "'"
                                    + ", FlagADJ = 0 "
                                    + " WHERE NoKontrak = '" + NoKontrak + "'");

                        Db.Execute("EXEC spKontrakDiskon"
                            + " '" + NoKontrak + "'"
                            + ",'" + Gross + "'"
                            + ",'" + NilaiKontrak + "'"
                            + ",'" + DiskonSkema + "'"
                            + ",'" + rs3.Rows[0]["DiskonPersen"] + "'"
                            + ",'" + rs3.Rows[0]["DiskonKet"] + "'"
                        );
                    }
                }

                    DataTable rsAft = Db.Rs("SELECT "
                        + " DiskonRupiah AS [Diskon dalam Rupiah]"
                        + ", NilaiPPN AS [PPN]"
                        + ", BungaNominal AS [BungaNominal]"
                        + ",NilaiKontrak AS [Nilai Kontrak]"
                        + ",Project AS [Project]"
                        + ",NamaProject AS [Nama Project]"
                        + ",Pers AS [Perusahaan]"
                        + ",NamaPers AS [Nama Perusahaan]"
                        + " FROM MS_KONTRAK"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );

                    DataTable rs = Db.Rs("SELECT "
                        + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                        + ",MS_KONTRAK.NoUnit AS [Unit]"
                        + ",MS_CUSTOMER.Nama AS [Customer]"
                        + ",MS_KONTRAK.Gross AS [Nilai Gross]"
                        + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                        + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                        + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                    string Ket = Cf.LogCapture(rs)
                        + Cf.LogCompare(rsBef, rsAft)
                        ;

                Db.Execute("EXEC spLogKontrak"
                    + " 'ADJ'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoKontrak + "'"
                    );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");                    
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("AdjustmentKontrak.aspx?done=" + NoKontrak);
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
            }
        }

        protected bool DenganPPN { get { return sifatppn.SelectedValue != "0"; } }

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
        protected void gross_TextChanged(object sender, EventArgs e)
        {
            setharga();
        }
        protected void discSkema_TextChanged(object sender, EventArgs e)
        {
            setharga();
        }
        protected void disc_TextChanged(object sender, EventArgs e)
        {
            setharga();
        }
        protected void bunga_TextChanged(object sender, EventArgs e)
        {
            setharga();
        }
        protected void setharga()
        {
            decimal Gross = Convert.ToDecimal(gross.Text);
            decimal DiskonSkema = Convert.ToDecimal(discSkema.Text);
            decimal DiskonTambahan = Convert.ToDecimal(disc.Text);
            decimal Bunga = Convert.ToDecimal(bunga.Text);
            decimal PPN = 0;
            decimal Netto = Gross + Bunga;
            decimal DPP = 0;
            decimal NilaiKontrak = 0;
            string statusPPN = sifatppn.SelectedValue;
            string valueSkema = skema.Text;

            //Netto = Func.SetelahDiskon(RumusDiskon, Gross);
            Netto -= DiskonSkema;
            Netto -= DiskonTambahan;
            string ParamID = "PLIncludePPN" + project.SelectedValue;
            bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";

            decimal NilaiPPN = 0;                

            if (statusPPN == "1")
            {
                if (includeppn)
                {
                    DPP = Math.Round(Netto / (decimal)1.1);
                    NilaiPPN = Math.Round(Netto - DPP);
                }
                else
                {
                    DPP = Math.Round(Netto / (decimal)1.1);
                    NilaiPPN = Math.Round(DPP * (decimal)0.1);
                }
            }
            else
            {
                DPP = Netto;
            }

            NilaiKontrak = DPP + NilaiPPN;
            PPN = Math.Round(NilaiKontrak - DPP);

            nilaidpp.Text = Cf.Num(Math.Round(DPP));
            nilaippn.Text = Cf.Num(Math.Round(PPN));
            nilaikontrak.Text = Cf.Num(Math.Round(NilaiKontrak));
        }
        protected void nilaippn_TextChanged(object sender, EventArgs e)
        {
            nilaikontrak.Text = Cf.Num(Convert.ToDecimal(nilaidpp.Text) + Convert.ToDecimal(nilaippn.Text));
        }
        protected void nilaidpp_TextChanged(object sender, EventArgs e)
        {
            nilaippn.Text = Cf.Num((Convert.ToDecimal(nilaidpp.Text) * 10) / 100);
            nilaikontrak.Text = Cf.Num(Convert.ToDecimal(nilaidpp.Text) + ((Convert.ToDecimal(nilaidpp.Text) * 10) / 100));
        }
        protected void includeppn_CheckedChanged(object sender, EventArgs e)
        {
            setharga();
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            setharga();
        }
    }
}
