using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakGantiNama : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Js.Focus(this, nokontrak);
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a');";
                frm.Visible = false;
                tglgn.Text = Cf.Day(DateTime.Today);
            }
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            btnpop2.Attributes.Add("modal-url", "DaftarCustomer.aspx?status=a&project=" + Project);
            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Jalankan prosedur Pengalihan Hak?\\nProses ini akan merubah data kepemilikan unit properti.");

            if (Request.QueryString["NoKontrak"] != null)
            {
                pilih.Visible = false;
                frm.Visible = true;
                nokontrak.Text = Request.QueryString["NoKontrak"];
                nocustomer.Text = Request.QueryString["NoCustomer"];
                namacustomer.Text = Request.QueryString["Nama"];
                Func.KontrakHeader(Request.QueryString["NoKontrak"], nokontrakl, unit, customer, agent);
                Fill();
            }
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                        + "Pengalihan Hak Berhasil..."
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

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                int count = Db.SingleInteger("SELECT COUNT(*) FROM MS_APPROVAL WHERE SumberID = '" + NoKontrak + "' AND Sumber = '" + Str.Approval("1") + "' AND Status <> 'DONE'");
                if (count == 0)
                {
                    pilih.Visible = false;
                    frm.Visible = true;

                    Js.Focus(this, nocustomer);
                    nocustomer.Attributes["ondblclick"] = "popDaftarCustomer('a');";

                    Fill();
                    Js.Confirm(this, "Jalankan prosedur Pengalihan Hak?\\nProses ini akan merubah data kepemilikan unit properti.");
                }
                else
                {
                    pilih.Visible = true;
                    feed1.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Proses Pengalihan Hak untuk Kontrak tersebut belum selesai.";
                    feed1.Attributes["style"] = "background-color:white;color:red;";
                }
            }
        }

        private void Fill()
        {
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            btnbaru.Attributes["onclick"] = "location.href = 'CustomerDaftar.aspx?gn=1&NoKontrak=" + NoKontrak + "&project=" + Project + "'";

            nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

            nilaipph.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaipph.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaipph.Attributes["onblur"] = "CalcBlur(this);";

            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT"
                + " MS_CUSTOMER.Nama"
                + ",MS_CUSTOMER.NoCustomer"
                + ",TglKontrak"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                noskr.Text = rs.Rows[0]["NoCustomer"].ToString().PadLeft(5, '0');
                namaskr.Text = rs.Rows[0]["Nama"].ToString();

                TimeSpan ts = DateTime.Today.Subtract(Convert.ToDateTime(rs.Rows[0]["TglKontrak"]));
                if (ts.Days > 30)
                {
                    //lblValid.Text = "SURAT PESANAN SUDAH BERUMUR LEBIH DARI 30 HARI";
                    //save.Enabled = false;
                }
                else
                    lblValid.Text = "";
            }
        }

        private bool datavalid()
        {
            bool x = true;
            string s = "";
            nocustomer.Text = NoCustomer;

            try
            {
                int lama = Convert.ToInt32(noskr.Text);
                int z = Convert.ToInt32(NoCustomer);

                int c = Db.SingleInteger("SELECT COUNT(*) "
                    + " FROM MS_CUSTOMER"
                    + " WHERE NoCustomer = " + NoCustomer
                    + " AND NoCustomer <> " + lama
                    + " AND Status = 'A'"
                    );

                if (c == 0)
                {
                    if (s == "") s = nocustomer.ID;
                    x = false;
                }
            }
            catch { x = false; if (s == "") s = nocustomer.ID; }

            if (!Cf.isMoney(nilaibiaya))
            {
                x = false;
                if (s == "") s = nilaibiaya.ID;
                nilaibiayac.Text = "Angka";
            }
            else
                nilaibiayac.Text = "";

            if (!Cf.isMoney(nilaipph))
            {
                x = false;
                if (s == "") s = nilaipph.ID;
                nilaipphc.Text = "Angka";
            }
            else
                nilaipphc.Text = "";

            if (!Cf.isTgl(tglgn))
            {
                x = false;
                if (s == "") s = tglgn.ID;
                tglgnc.Text = "Tanggal";
            }
            else
                tglgnc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Customer tersebut tidak terdaftar.\\n"
                    + "2. Status customer tersebut adalah inaktif.\\n"
                    + "3. Customer lama dan customer baru harus berbeda.\\n"
                    + "4. Biaya Administrasi harus berupa angka."
                    + "5. Biaya PPH Pengalihan Hak harus berupa angka."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected bool filevalid()
        {
            bool x = true;
            string s = "";

            if (file.PostedFile.FileName.Length != 0
                && !file.PostedFile.FileName.EndsWith(".jpg"))
            {
                x = false;

                if (s == "")
                    s = file.ID;
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "Proses Upload Gagal.\\n"
                    + "File yang boleh di-upload adalah file dengan extension .jpg saja."
                    , "document.getElementById('" + s + "').focus();"
                    );
            }

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                string c = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'ApprovGantiNama" + Project + "'");
                string Keterangan = baru.Text;                
                decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);

                int nomor = Db.SingleInteger("SELECT COUNT(*) FROM MS_APPROVAL");
                nomor++;
                string NoApproval = nomor.ToString().PadLeft(7, '0');
                if (c == "True")
                {
                    if (filevalid())
                    {
                        DataTable rsBef = Db.Rs("SELECT "
                            + " MS_CUSTOMER.NoCustomer AS [No. Customer]"
                            + ",MS_CUSTOMER.Nama AS [Nama Customer]"
                            + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER "
                            + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                            + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                        //INSERT KE MS_APPROVAL
                        Db.Execute("EXEC spApproval"
                            + "'" + NoApproval + "'"
                            + ",'" + Str.Approval("1") + "'"//untuk ganti unit
                            + ",'" + NoKontrak + "'"
                            + ",'" + Convert.ToDateTime(tglgn.Text) + "'"
                            + ",'" + Project + "'"
                            );

                        //insert siapa aja yang berhak approve ke ms_approval_detil 
                        DataTable rs2 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 1 AND Project = '" + Project + "'");
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

                        //insert ke ms_approval ganti nama
                        Db.Execute("EXEC spKontrakGantiNamaTemp"
                            + "'" + NoApproval + "'"
                            + ",'" + NoKontrak + "'"
                            + ",'" + NoCustomer + "'"
                            + ",'" + NilaiBiaya + "'"                            
                            + ",'" + Convert.ToDateTime(tglgn.Text) + "'"
                            + ",'" + Keterangan + "'"
                            );

                        DataTable rsAft = Db.Rs("SELECT "
                            + " MS_CUSTOMER.NoCustomer AS [No. Customer]"
                            + ",MS_CUSTOMER.Nama AS [Nama Customer]"
                            + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER "
                            + " ON MS_KONTRAK.TempGN = MS_CUSTOMER.NoCustomer"
                            + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");


                        #region Default

                        //Db.Execute("EXEC spKontrakGantiNama "
                        //    + " '" + NoKontrak + "'"
                        //    + ", '" + NoCustomer + "'"
                        //    + ", '" + Convert.ToDateTime(tglgn.Text) + "'"
                        //    );

                        //decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
                        //if(NilaiBiaya!=0)
                        //{
                        //    Db.Execute("EXEC spTagihanDaftar "
                        //        + " '" + NoKontrak + "'"
                        //        + ",'BIAYA ADM. Pengalihan Hak'"
                        //        + ",'" + Cf.Day(DateTime.Today) + "'"
                        //        + ", " + NilaiBiaya
                        //        + ",'ADM'"
                        //        );
                        //}

                        ///*Pengalihan Hak customer di MS_TTS*/
                        //string strNamaCs = Cf.Str(Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer));
                        //string strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                        //    + " SET Customer = '" + strNamaCs + "'"
                        //    + " WHERE Ref = '" + NoKontrak + "'"
                        //    + " AND Tipe = 'JUAL'"
                        //    ;
                        //Db.Execute(strSql);
                        ///*******************************/

                        ///*Pengalihan Hak customer di MS_PJT*/
                        //strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PJT"
                        //    + " SET Customer = '" + strNamaCs + "'"
                        //    + " WHERE Ref = '" + NoKontrak + "'"
                        //    + " AND Tipe = 'JUAL'"
                        //    ;
                        //Db.Execute(strSql);
                        ///*******************************/

                        ///*Pengalihan Hak customer di MS_TUNGGAKAN*/
                        //strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN"
                        //    + " SET Customer = '" + strNamaCs + "'"
                        //    + " WHERE Ref = '" + NoKontrak + "'"
                        //    + " AND Tipe = 'JUAL'"
                        //    ;
                        //Db.Execute(strSql);
                        ///*******************************/
                        #endregion

                        /*Insert jurnal kontrak*/
                        string strKetJurnal = "KONTRAK Pengalihan Hak<br />" + Cf.Str(baru.Text) + "<br>No. Approval :" + NoApproval;

                        Db.Execute("EXEC spJurnalKontrak "
                            + " '" + Act.UserID + "'"
                            + ",'" + NoKontrak + "'"
                            + ",'" + strKetJurnal + "'"
                            );

                        if (file.PostedFile.FileName.Length != 0)
                        {
                            long JurnalID = Db.SingleLong("SELECT TOP 1 JurnalID FROM MS_KONTRAK_JURNAL ORDER BY JurnalID DESC");
                            string path = Request.PhysicalApplicationPath
                                + "JurnalKontrak\\" + JurnalID + ".jpg";
                            Dfc.UploadFile(".jpg", path, file);
                        }
                        /***********************/

                        string Ket = Cf.LogCompare(rsBef, rsAft)
                            + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                            + "<br>Tgl Pengalihan Hak : " + Cf.Day(Convert.ToDateTime(tglgn.Text))
                            ;

                        Db.Execute("EXEC spLogKontrak "
                            + " 'GN'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + Ket + "'"
                            + ",'" + NoKontrak + "'"
                            );

                        decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                        Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                        Func.CekKomisi(NoKontrak);

                        //Push notif ke Approval selanjutnya
                        string DeptID = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                        DataTable rsNextApp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 1 "
                            + " AND Lvl = 1 AND Project = '" + DeptID + "'");

                        for (int i = 0; i < rsNextApp.Rows.Count; i++)
                        {
                            string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                            LibApi.PushNotif("APR-GN", "Permohonan Approval Pengalihan Hak " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                        }

                        Response.Redirect("KontrakGantiNama.aspx?done=" + NoKontrak);
                    }
                }
                else
                {
                    //buat yang ga pake fitur approv
                    if (filevalid())
                    {
                        DataTable rsBef = Db.Rs("SELECT "
                            + " MS_CUSTOMER.NoCustomer AS [No. Customer]"
                            + ",MS_CUSTOMER.Nama AS [Nama Customer]"
                            + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER"
                            + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                            + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                        //INSERT KE MS_APPROVAL tapi langsung done
                        Db.Execute("INSERT INTO MS_APPROVAL VALUES ("
                            + "'" + NoApproval + "'"
                            + ",'" + Str.Approval("1") + "'"//untuk ganti unit
                            + ",'" + NoKontrak + "'"
                            + ",'" + Convert.ToDateTime(tglgn.Text) + "'"
                            + ",'DONE'"
                            + ",'" + Convert.ToDateTime(tglgn.Text) + "'"
                            + ",'" + Project + "')"
                            );

                        //insert ke ms_approval ganti nama
                        Db.Execute("EXEC spKontrakGantiNamaTemp"
                            + "'" + NoApproval + "'"
                            + ",'" + NoKontrak + "'"
                            + ",'" + NoCustomer + "'"
                            + ",'" + NilaiBiaya + "'"
                            + ",'" + Convert.ToDateTime(tglgn.Text) + "'"
                            + ",'" + Keterangan + "'"
                            );

                        Db.Execute("EXEC ISC064_MARKETINGJUAL..spKontrakGantiNama "
                                + " '" + NoKontrak + "'"
                                + ", '" + NoCustomer + "'"
                                );

                        /*Update Flag ApprovalGN*/
                        Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_KONTRAK "
                            + " SET ApprovalGN = 0"
                            + " ,Revisi = Revisi + 1"
                            + " WHERE NoKontrak='" + NoKontrak + "'"
                            );

                        DataTable rsAft = Db.Rs("SELECT "
                                + " MS_CUSTOMER.NoCustomer AS [No. Customer]"
                                + ",MS_CUSTOMER.Nama AS [Nama Customer]"
                                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER "
                                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                        if (NilaiBiaya != 0)
                        {
                            Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spTagihanDaftar "
                                + " '" + NoKontrak + "'"
                                + ",'BIAYA ADM. Pengalihan Hak'"
                                + ",'" + Cf.Day(DateTime.Today) + "'"
                                + ", " + NilaiBiaya
                                + ",'ADM'"
                                );

                            int NoUrut = Db.SingleInteger("SELECT TOP 1 NoUrut FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut DESC");
                            Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN SET Jenis = 'Pengalihan Hak' WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoUrut);
                        }

                        /* Ambil Nilai PPH Pengalihan Hak */
                        decimal NilaiPPH = Convert.ToDecimal(nilaipph.Text);

                        if (NilaiPPH != 0)
                        {
                            Db.Execute("EXEC ISC064_MARKETINGJUAL..spTagihanDaftar "
                                + " '" + NoKontrak + "'"
                                + ",'BIAYA PPH PENGALIHAN HAK'"
                                + ",'" + Cf.Day(DateTime.Today) + "'"
                                + ", " + NilaiPPH
                                + ",'ADM'"
                                );
                        }

                        /*Pengalihan Hak customer di MS_TTS*/
                        string strNamaCs = Cf.Str(Db.SingleString("SELECT Nama FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + NoCustomer));
                        string strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                            + " SET Customer = '" + strNamaCs + "'"
                            + " WHERE Ref = '" + NoKontrak + "'"
                            + " AND Tipe = 'JUAL'"
                            ;
                        Db.Execute(strSql);
                        /*******************************/

                        /*Pengalihan Hak customer di MS_MEMO*/
                        strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_MEMO"
                            + " SET Customer = '" + strNamaCs + "'"
                            + " WHERE Ref = '" + NoKontrak + "'"
                            + " AND Tipe = 'JUAL'"
                            ;
                        Db.Execute(strSql);
                        /*******************************/

                        /*Pengalihan Hak customer di MS_PJT*/
                        strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PJT"
                            + " SET Customer = '" + strNamaCs + "'"
                            + " WHERE Ref = '" + NoKontrak + "'"
                            + " AND Tipe = 'JUAL'"
                            ;
                        Db.Execute(strSql);
                        /*******************************/

                        /*Pengalihan Hak customer di MS_TUNGGAKAN*/
                        strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN"
                            + " SET Customer = '" + strNamaCs + "'"
                            + " WHERE Ref = '" + NoKontrak + "'"
                            + " AND Tipe = 'JUAL'"
                            ;
                        Db.Execute(strSql);
                        /*******************************/

                        string Ket = Cf.LogCompare(rsBef, rsAft)
                            + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                            + "<br>Tgl Pengalihan Hak : " + Cf.Day(Convert.ToDateTime(tglgn.Text))
                            ;

                        Db.Execute("EXEC spLogKontrak "
                            + " 'GN'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + Ket + "'"
                            + ",'" + NoKontrak + "'"
                            );

                        decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                        Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                        Func.CekKomisi(NoKontrak);

                        //Push notif ke Approval selanjutnya
                        DataTable rsNextApp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 1 "
                            + " AND Lvl = 1");

                        for (int i = 0; i < rsNextApp.Rows.Count; i++)
                        {
                            string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                            LibApi.PushNotif("GN", "Permohonan Pengalihan Hak " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                        }
                        Response.Redirect("KontrakGantiNama.aspx?done=" + NoKontrak);
                    }
                }
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
            }
        }

        private string NoCustomer
        {
            get
            {
                return Cf.Pk(nocustomer.Text);
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
