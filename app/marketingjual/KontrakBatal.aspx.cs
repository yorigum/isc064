using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakBatal : System.Web.UI.Page
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

                nodel.Visible = false;
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses pembatalan kontrak?\\nStatus kontrak tersebut akan menjadi BATAL.");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                        + "Pembatalan Berhasil..."
                        + "</a>";

                //if(Request.QueryString["klaim"] != null)
                //{
                //    this.RegisterStartupScript(
                //        "klaimScript"
                //        , "<script language='javascript' type='text/javascript'>"
                //        + "openPopUp('/marketingjual/PrintFBatal.aspx?NoKontrak=" + Cf.Pk(Request.QueryString["klaim"]) + "', '920', '650')"
                //        + "</script>"
                //        );
                //}

                totalPengembalian.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                totalPengembalian.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                totalPengembalian.Attributes["onblur"] = "CalcBlur(this);";
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
                pilih.Visible = false;
                frm.Visible = true;

                Fill();

                Js.Confirm(this, "Lanjutkan proses pembatalan kontrak?\\nStatus kontrak tersebut akan menjadi BATAL.");
            }
        }

        private void Fill()
        {
            nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

            nilaiklaim.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaiklaim.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaiklaim.Attributes["onblur"] = "CalcBlur(this);";

            totalPelunasan.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            totalPelunasan.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            totalPelunasan.Attributes["onblur"] = "CalcBlur(this);";

            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);


            if (Func.CekAkunting(NoKontrak))
                warning.Text = "Transaksi sudah pernah diposting ke Akunting";
            else
                warning.Text = "";

            decimal TotalLunas = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan), 0)"
                       + " FROM MS_PELUNASAN"
                       + " WHERE NoKontrak = '" + NoKontrak + "'"
                       + " AND SudahCair = 1"
                       );

            int BF = Db.SingleInteger("SELECT ISNULL(SUM(NoUrut), 0)"
                       + " FROM MS_PELUNASAN"
                       + " WHERE NoKontrak = '" + NoKontrak + "'"
                       + " AND SudahCair = 1"
                       );


            totalPelunasan.Text = Cf.Num(TotalLunas);
            if (BF == 1) { nilaiklaim.Text = Cf.Num(TotalLunas); }
            else { nilaiklaim.Text = Cf.Num(0); }
        }

        private bool datavalid()
        {
            bool x = true;
            string s = "";

            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

            if (!Cf.isTgl(tglkembali) && nilaiklaim.Text != "0")
            {
                x = false;
                if (s == "") s = tglkembali.ID;
                tglkembalic.Text = "Tanggal";
            }
            else
                tglkembalic.Text = "";

            //if(!Cf.isMoney(nilaibiaya))
            //{
            //    x = false;
            //    if(s=="") s = nilaibiaya.ID;
            //    nilaibiayac.Text = "Angka";
            //}
            //else
            //    nilaibiayac.Text = "";
            if (Convert.ToDecimal(totalPengembalian.Text) != Convert.ToDecimal(totalPelunasan.Text))
            {
                if (Convert.ToDecimal(nilaiklaim.Text) == Convert.ToDecimal(0))
                    x = false;
            }


            if (!Cf.isMoney(nilaiklaim))
            {
                x = false;
                if (s == "") s = nilaiklaim.ID;
                nilaiklaimc.Text = "Angka";
            }
            else
                nilaiklaimc.Text = "";

            if (!Cf.isMoney(totalPengembalian))
            {
                x = false;
                if (s == "") s = totalPengembalian.ID;
                totalpengembalianc.Text = "Angka";
            }
            else
                totalpengembalianc.Text = "";

          

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Biaya Administrasi harus berupa angka.\\n"
                    + "2. Nilai Klaim harus berupa angka.\\n"
                    //+ "3. Rekening Pembatalan harus dipilih."
                    + "3. Tanggal Pengembalian salah format / kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    );

            return x;
        }

        protected void totalklaim(object sender, EventArgs e)
        {
            try
            {
                decimal Klaim = Convert.ToDecimal(totalPelunasan.Text) - Convert.ToDecimal(totalPengembalian.Text);
                nilaiklaim.Text = Cf.Num(Klaim);
                totalPengembalian.Text = Cf.Num(Convert.ToDecimal(totalPengembalian.Text));
            }
            catch
            {

            }
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                string c = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'ApprovBatal" + Project + "'");
                if (c == "True")
                {
                    int nomor = Db.SingleInteger("SELECT COUNT(*) FROM MS_APPROVAL");
                    nomor++;
                    string NoApproval = nomor.ToString().PadLeft(7, '0');

                    string AlasanBatal = Cf.Str(alasan.SelectedItem.Text);

                    // Update Manual 1
                    decimal NilaiKlaim = Convert.ToDecimal(nilaiklaim.Text);
                    decimal NilaiPengembalian = Convert.ToDecimal(totalPengembalian.Text);
                    decimal Biaya = Convert.ToDecimal(nilaibiaya.Text);

                    decimal TotalLunas = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan), 0)"
                        + " FROM MS_PELUNASAN"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        + " AND SudahCair = 1"
                        );

                    string ket;
                    if (ketalasan.Text != "")
                        ket = ketalasan.Text;
                    else
                        ket = "";

                    DateTime TglKembali = Convert.ToDateTime(tglkembali.Text);


                    int count = Db.SingleInteger("SELECT COUNT (*) FROM MS_APPROVAL_BATAL WHERE NoKontrak ='" + NoKontrak + "' AND NoApproval IN (SELECT NoApproval FROM MS_APPROVAL WHERE Sumber = '" + Str.Approval("3") + "' AND Status <> 'DONE')");
                    if (count > 0)
                    {
                        //nostockc.Text = "Unit Tidak Valid";

                        Js.Alert(
                            this
                            , "NoKontrak Tidak Valid.\\n\\n"
                            + "Kemungkinan Sebab :\\n"
                            + "1. Kontrak sudah dijual kepada customer lain.\\n"
                            , "document.getElementById('nokontrakl').focus();"
                            + "document.getElementById('nokontrakl').select();"
                            );
                    }
                    else
                    {
                        //INSERT KE MS_APPROVAL
                        Db.Execute("EXEC spApproval"
                            + "'" + NoApproval + "'"
                            + ",'" + Str.Approval("3") + "'"//untuk batal kontrak
                            + ",'" + NoKontrak + "'"
                            + ",'" + Convert.ToDateTime(tgl.Text) + "'"
                            + ",'" + Project + "'"
                            );

                        //insert siapa aja yang berhak approve ke ms_approval_detil 
                        DataTable rs2 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 3 AND Project = '" + Project + "'");
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

                        //insert perubahan batal kontrak ke ms_approval_batal
                        Db.Execute("EXEC spKontrakBatalTemp"
                            + "'" + NoApproval + "'"
                            + ",'" + NoKontrak + "'"
                            + ",'" + Convert.ToDateTime(tgl.Text) + "'"//dari Textbox
                            + ",'" + Convert.ToDateTime(tglkembali.Text) + "'"
                            + ",'" + AlasanBatal + "'"
                            + ",'" + TotalLunas + "'"
                            + ",'" + NilaiPengembalian + "'"
                            + ",'" + NilaiKlaim + "'"
                            + ",'" + ket + "'"
                            );

                        DataTable rs = Db.Rs("SELECT"
                            + " NoKontrak AS [NoKontrak]"
                            + ",NoUnit AS [No. Unit]"
                            + ",(SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = MS_KONTRAK.NoCustomer) AS [Customer]"
                            + " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'"
                            );
                        
                        string Ket = Cf.LogCapture(rs)
                                    + "<br>Tgl Pengembalian : " + Cf.Day(tglkembali.Text)
                                    + "<br>Alasan Batal : " + AlasanBatal
                                    + "<br>Total Pelunasan : " + TotalLunas
                                    + "<br>Total Pengembalian : " + NilaiPengembalian
                                    + "<br>Nilai Klaim : " + NilaiKlaim
                                    ;

                        Db.Execute("EXEC spLogKontrak "
                            + " 'BATAL'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + Ket + "'"
                            + ",'" + NoKontrak + "'"
                            );

                        decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                        Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                        //Push notif ke Approval selanjutnya
                        string DeptID = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                        DataTable rsNextApp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 3 "
                            + " AND Lvl = 1 AND Project = '" + DeptID + "'");

                        for (int i = 0; i < rsNextApp.Rows.Count; i++)
                        {
                            string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                            LibApi.PushNotif("BATAL", "Permohonan Approval Pembatalan Kontrak " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                        }

                        Response.Redirect("KontrakBatal.aspx?done=" + NoKontrak + "&klaim=" + NoKontrak);
                        //}
                    }
                }
                else
                {
                    Db.Execute("EXEC ISC064_MARKETINGJUAL..spKontrakBatal "
                    + " '" + NoKontrak + "'"
                    );

                    if (Db.SingleString("SELECT Status FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'") == "B")
                    {
                        DataTable rs = Db.Rs("SELECT"
                            + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                            + ",MS_KONTRAK.NoUnit AS [Unit]"
                            + ",MS_CUSTOMER.Nama AS [Customer]"
                            + ",MS_AGENT.Nama AS [Agent]"
                            + ",AlasanBatal AS [Alasan Pembatalan]"
                            + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER"
                            + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                            + " INNER JOIN ISC064_MARKETINGJUAL..MS_AGENT AS MS_AGENT"
                            + " ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                            + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                        decimal NilaiBiaya = Db.SingleDecimal("SELECT BiayaBatal FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");

                        if (NilaiBiaya != 0)
                        {
                            Db.Execute("EXEC ISC064_MARKETINGJUAL..spTagihanDaftar "
                                + " '" + NoKontrak + "'"
                                + ",'BIAYA ADM. PEMBATALAN'"
                                + ",'" + Cf.Day(DateTime.Today) + "'"
                                + ", " + NilaiBiaya
                                + ",'ADM'"
                                );
                        }
                        decimal NilaiMasuk = Db.SingleDecimal(
                            "SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "'");
                        Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_KONTRAK SET BatalMasuk = "
                            + NilaiMasuk + " WHERE NoKontrak = '" + NoKontrak + "'");
                        decimal NilaiKlaim = Db.SingleDecimal("SELECT NilaiKlaim FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                        decimal NilaiPengembalian = Db.SingleDecimal("SELECT NilaiPulang FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                        decimal TotalLunas = Db.SingleDecimal("SELECT TotalLunasBatal FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                        string acc = Db.SingleString("SELECT AccBatal FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");

                        string Ket = Cf.LogCapture(rs)
                            + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                            + "<br>Uang Masuk : " + Cf.Num(NilaiMasuk)
                            + "<br>Nilai Klaim : " + Cf.Num(NilaiKlaim)
                            + "<br>Tgl. Batal : " + Cf.Day(DateTime.Today)
                            + "<br>Total Pelunasan : " + Cf.Num(TotalLunas)
                            + "<br>Nilai Kembali : " + Cf.Num(NilaiPengembalian)
                            + "<br>Rekening Pembatalan : " + acc
                            ;

                        Func.CekKomisi(NoKontrak);

                        //floor plan
                        string Peta = Db.SingleString("SELECT Peta "
                            + " FROM ISC064_MARKETINGJUAL..MS_UNIT AS MS_UNIT INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK ON MS_UNIT.NoStock = MS_KONTRAK.NoStock "
                            + " WHERE NoKontrak = '" + NoKontrak + "'");
                        Func.GenerateFP(Peta);

                        //SA01
                        string CnnEsales = "Data Source=.;Initial Catalog=SA01;Persist Security Info=True;User ID=batavianet;Password=iNDigo100";
                        string ClosingID = Db.SingleString("SELECT ISNULL(ClosingID,'') FROM ISC064_MARKETINGJUAL..MS_Kontrak WHERE NoKontrak='" + NoKontrak + "'");
                        string AlasanBatal = Db.SingleString("SELECT ISNULL(AlasanBatal,'') FROM ISC064_MARKETINGJUAL..MS_Kontrak WHERE NoKontrak='" + NoKontrak + "'");

                        if (ClosingID != "")
                        {
                            Execute("UPDATE SalesClosing SET"
                                    + " Status=1"
                                    + ", AlasanCancel='" + AlasanBatal + "'"
                                    + ", TglCancel='" + Cf.Day(DateTime.Today) + "'"
                                    + " WHERE ClosingID='" + ClosingID + "'"
                                    , CnnEsales);


                            StringBuilder x = new StringBuilder();
                            x.Append("Closing dengan kode " + ClosingID);
                            x.Append("<br/>");
                            x.Append("<i>Dibatalkan setelah kontrak terdaftar</i>");

                            string Closinger = "";
                            Closinger = SingleString("SELECT ISNULL(UserID,'') FROM SalesClosing WHERE ClosingID='" + ClosingID + "'", CnnEsales);
                            if (Closinger != "")
                            {
                                string Role = "";
                                Role = SingleString("SELECT ISNULL(RoleID,'') FROM SecUser WHERE UserID='" + Closinger + "'", CnnEsales);
                                if (Role == "SA.OPR")
                                {
                                    Execute("EXEC InsertSecNotification "
                                       + "'Pembatalan Kontrak'"
                                       + ",'" + x.ToString() + "'"
                                       + ",'" + Closinger + "'"
                                       + ",'../Sales/ClosingFile.aspx?id=" + ClosingID + "'"
                                       , CnnEsales);
                                }
                                else if (Role == "SA.MGR")
                                {
                                    Execute("EXEC InsertSecNotification "
                                    + "'Pembatalan Kontrak'"
                                    + ",'" + x.ToString() + "'"
                                    + ",'" + Closinger + "'"
                                    + ",'../SM/ClosingFile.aspx?id=" + ClosingID + "'"
                                    , CnnEsales);
                                }
                            }
                        }
                        Db.Execute("EXEC spLogKontrak "
                            + " 'BATAL'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",''"
                            + ",'" + NoKontrak + "'"
                            );

                        decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                        Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                        //Push notif ke Approval selanjutnya
                        DataTable rsNextApp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 3 "
                            + " AND Lvl = 1");

                        for (int i = 0; i < rsNextApp.Rows.Count; i++)
                        {
                            string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                            LibApi.PushNotif("BATAL", "Permohonan Approval Pembatalan Kontrak " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                        }

                        Response.Redirect("KontrakBatal.aspx?done=" + NoKontrak + "&klaim=" + NoKontrak);
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

        //Common Driver
        protected void Execute(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCnn.Close();
        }
        protected DataTable Rs(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql, sqlCnn);
            DataSet objDS = new DataSet();
            sqlAdapter.Fill(objDS, "data");
            sqlCnn.Close();

            DataTable rs = new DataTable();
            rs = objDS.Tables["data"];

            return rs;
        }
        protected string SingleString(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            string x = "";
            x = (string)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected int SingleInteger(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            int x = (int)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected long SingleLong(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            long x = Convert.ToInt64(sqlCmd.ExecuteScalar());
            sqlCnn.Close();

            return x;
        }
        protected decimal SingleDecimal(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            decimal x = Convert.ToDecimal(sqlCmd.ExecuteScalar());
            sqlCnn.Close();

            return x;
        }
        protected bool SingleBool(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            bool x = (bool)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected byte SingleByte(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            byte x = (byte)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected DateTime SingleTime(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            DateTime x = (DateTime)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
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
        protected void alasan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (alasan.SelectedItem.ToString() == "LAINNYA")
            {
                lain.Visible = true;
            }
            else
            {
                lain.Visible = false;
            }
        }
    }
}
