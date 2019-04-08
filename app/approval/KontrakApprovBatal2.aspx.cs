using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using Microsoft.AspNet.SignalR;

namespace ISC064.APPROVAL
{
    public partial class KontrakApprovBatal2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Fill();
        }

        private class id
        {
            public int index { get; set; }
        }

        private void Fill()
        {
            string strSql = " SELECT e.*,f.*,b.NoUnit, c.Nama, d.Nama AS Agent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL e"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_BATAL f ON e.SumberID = f.NoKontrak"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a ON f.NoKontrak = a.NoKontrak"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT b ON a.NoStock = b.NoStock"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT d ON a.NoAgent = d.NoAgent"
                            + " WHERE e.SumberID = '" + NoKontrak + "'"
                            ;

            DataTable rs = Db.Rs(strSql);
            decimal nilaipengembalian = Convert.ToDecimal(rs.Rows[0]["NilaiPengembalian"]);

            nokontrak.Text = rs.Rows[0]["SumberID"].ToString();
            unit.Text = rs.Rows[0]["NoUnit"].ToString();
            customer.Text = rs.Rows[0]["Nama"].ToString();
            agent.Text = rs.Rows[0]["Agent"].ToString();
            alasan.Text = rs.Rows[0]["AlasanBatal"].ToString();
            ket.Text = rs.Rows[0]["Keterangan"].ToString();
            totallunas.Text = Cf.Num(rs.Rows[0]["TotalPelunasan"]);
            tglkembali.Text = Cf.Day(rs.Rows[0]["TglPengembalian"].ToString());
            totalkembali.Text = Cf.Num(rs.Rows[0]["NilaiPengembalian"]);
            nilaiklaim.Text = Cf.Num(rs.Rows[0]["NilaiKlaim"]);
        }

        private static DataTable HakApp(string ProjectID)
        {
            DataTable hakapp = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 3 AND Project='" + ProjectID + "'");

            return hakapp;
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] == "1")
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Approval Batal Selesai..."
                        ;
                else if (Request.QueryString["done"] == "2")
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Reject Batal Selesai..."
                        ;
            }
        }

        private bool datavalid()
        {
            bool x = true;
            string s = "";

            if (Cf.isEmpty(note))
            {
                x = false;
                Cf.MarkError(note);
            }

            if (!x)
            {
                this.RegisterStartupScript(
                    "focusScript"
                    , "<script language='javascript' type='text/javascript'>"
                    + "document.getElementById('" + s + "').focus();"
                    + "</script>"
                    );
            }

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                SaveApproval(NoKontrak, Level, Project, 1);//1 = Approve, 2 = Reject
                Response.Redirect("KontrakApprovBatal.aspx?done=1");
                //Response.Redirect("KontrakApprovBatal.aspx?done=1&project=" + project.SelectedValue + "&nostock=" + NoStock);
            }
        }

        private string SaveApproval(string NoKontrak, string lvl, string Proj, int Approve)
        {
            int Lvl = Convert.ToInt16(lvl);
            int MaxApp = 0;
            MaxApp = Db.SingleByte("SELECT TOP 1 Lvl FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 3 AND Project='" + Proj + "' ORDER BY Lvl DESC");
            string Note = note.Text;
            string Ket = "";
            string NoStock = "";

            if (Approve == 1)
            {
                if (Lvl < MaxApp)
                {

                    //update status approval jadi proses
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL SET Status = 'PROCESS',TglApproval = '" + DateTime.Today + "' WHERE NoApproval = '" + NoApproval + "'");
                    //update detail approval dari user yang approve
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_DETAIL SET Approve = 1,Note = '" + Note + "',TglApproval = '" + DateTime.Today + "' WHERE NoApproval = '" + NoApproval + "' AND UserID = '" + Act.UserID + "' AND Lvl = '" + Lvl + "'");
                    Ket = "Tgl Approval : " + Cf.Day(DateTime.Today);

                    //Push notif ke Approval selanjutnya
                    string Pemohon = Db.SingleString("SELECT PemohonBatal FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    byte nextLvl = Convert.ToByte(Lvl + 1);

                    DataTable rsNextApp = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 3 "
                        + " AND Lvl = " + (Lvl + 1));

                    for (int i = 0; i < rsNextApp.Rows.Count; i++)
                    {
                        string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                        LibApi.PushNotif("BATAL", "Permohonan Approval Pembatalan Kontrak " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                    }
                }
                else
                {
                    NoStock = Db.SingleString("SELECT NoStock FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spKontrakBatal "
                    + " '" + NoKontrak + "'"
                    );

                    if (Db.SingleString("SELECT Status FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'") == "B")
                    {
                        DataTable rs = Db.Rs("SELECT"
                            + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                            + ",MS_KONTRAK.NoUnit AS [Unit]"
                            + ",MS_CUSTOMER.Nama AS [Customer]"
                            + ",MS_AGENT.Nama AS [Agent]"
                            + ",AlasanBatal AS [Alasan Pembatalan]"
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER"
                            + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT AS MS_AGENT"
                            + " ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                            + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                        decimal NilaiBiaya = Db.SingleDecimal("SELECT BiayaBatal FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");

                        if (NilaiBiaya != 0)
                        {
                            Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spTagihanDaftar "
                                + " '" + NoKontrak + "'"
                                + ",'BIAYA ADM. PEMBATALAN'"
                                + ",'" + Cf.Day(DateTime.Today) + "'"
                                + ", " + NilaiBiaya
                                + ",'ADM'"
                                );
                        }

                        decimal NilaiMasuk = Db.SingleDecimal(
                            "SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "'");
                        Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK SET BatalMasuk = "
                            + NilaiMasuk + " WHERE NoKontrak = '" + NoKontrak + "'");
                        decimal NilaiKlaim = Db.SingleDecimal("SELECT NilaiKlaim FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                        decimal NilaiPengembalian = Db.SingleDecimal("SELECT NilaiPulang FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                        decimal TotalLunas = Db.SingleDecimal("SELECT TotalLunasBatal FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                        string acc = Db.SingleString("SELECT AccBatal FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");

                        Ket = Cf.LogCapture(rs)
                            + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                            + "<br>Uang Masuk : " + Cf.Num(NilaiMasuk)
                            + "<br>Nilai Klaim : " + Cf.Num(NilaiKlaim)
                            + "<br>Tgl. Batal : " + Cf.Day(DateTime.Today)
                            + "<br>Total Pelunasan : " + Cf.Num(TotalLunas)
                            + "<br>Nilai Kembali : " + Cf.Num(NilaiPengembalian)
                            + "<br>Rekening Pembatalan : " + acc
                            ;

                        Func.CekKomisi(NoKontrak);

                        //floor planF
                        string Peta = Db.SingleString("SELECT Peta "
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT AS MS_UNIT INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK ON MS_UNIT.NoStock = MS_KONTRAK.NoStock "
                            + " WHERE NoKontrak = '" + NoKontrak + "'");
                        Func.GenerateFP(Peta);

                        //update siteplan
                        var context = GlobalHost.ConnectionManager.GetHubContext<ClosingUnit>();
                        context.Clients.All.invokeStatus(NoStock);

                        //SA01
                        string CnnEsales = "Data Source=.;Initial Catalog=SA01;Persist Security Info=True;User ID=batavianet;Password=iNDigo100";
                        string ClosingID = Db.SingleString("SELECT ISNULL(ClosingID,'') FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_Kontrak WHERE NoKontrak='" + NoKontrak + "'");
                        string AlasanBatal = Db.SingleString("SELECT ISNULL(AlasanBatal,'') FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_Kontrak WHERE NoKontrak='" + NoKontrak + "'");

                        if (ClosingID != "")
                        {
                            Execute("UPDATE SalesClosing SET"
                                    + " Status=1"
                                    + ", AlasanCancel='" + AlasanBatal + "'"
                                    + ", TglCancel='" + DateTime.Today + "'"
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
                    }
                }
            }
            else
            {
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_DETAIL Set Approve = 2 "
                    + ", Note = '" + Note + "' "
                    + ", TglApproval = '" + DateTime.Today + "'"
                    + " WHERE NoApproval = '" + NoApproval + "' AND UserID = '" + Act.UserID + "'"
                    );

                DataTable rs = Db.Rs("SELECT"
                    + " NoKontrak AS [NoKontrak]"
                    + ",NoUnit AS [No. Unit]"
                    + ",(SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = MS_KONTRAK.NoCustomer) AS [Customer]"
                    + " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'"
                    );
                                
                decimal NilaiKlaim = Db.SingleDecimal("SELECT NilaiKlaim FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_BATAL WHERE NoApproval ='" + NoApproval + "'");
                decimal NilaiPengembalian = Db.SingleDecimal("SELECT NilaiPengembalian FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoApproval ='" + NoApproval + "'");
                decimal TotalLunas = Db.SingleDecimal("SELECT TotalPelunasan FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoApproval ='" + NoApproval + "'");                
                string Alasan = Db.SingleString("SELECT AlasanBatal FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoApproval ='" + NoApproval + "'");

                Ket = Cf.LogCapture(rs)
                    + "<br>Nilai Klaim : " + Cf.Num(NilaiKlaim)
                    + "<br>Tgl. Batal : " + Cf.Day(DateTime.Today)
                    + "<br>Total Pelunasan : " + Cf.Num(TotalLunas)
                    + "<br>Nilai Kembali : " + Cf.Num(NilaiPengembalian)
                    + "<br>Alasan Batal : " + Alasan
                    + "<br>Alasan Reject : " + Note
                    ;

            }

            Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogKontrakApp "
            + " '" + NoKontrak + "'"
            + ",'" + Act.UserID + "'"
            + "," + Approve //Approve
            + ",'" + DateTime.Today + "'"
            + "," + Lvl
            + "," + 3 //Tipe
            + ",''"
            );

            if (Lvl == MaxApp || Approve == 2)
            {
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL SET Status = 'DONE'"
                    + ",TglApproval = '" + DateTime.Today + "'"
                    + " WHERE NoApproval = '" + NoApproval + "'"
                    );
            }

            Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogKontrak "
                + " 'APR-BA'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + NoKontrak + "'"
                );
            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            return NoStock;
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

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }
        private string NoApproval
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoApproval"]);
            }
        }
        private string Level
        {
            get
            {
                return Cf.Pk(Request.QueryString["Level"]);
            }
        }
        private string Project
        {
            get
            {
                return Db.SingleString("SELECT Project FROM MS_APPROVAL WHERE NoApproval = '" + NoApproval + "'");
            }
        }

        protected void reject_Click(object sender, EventArgs e)
        {
            if (datavalid())
            {
                SaveApproval(NoKontrak, Level, Project, 2);//1 = Approve, 2 = Reject
                Response.Redirect("KontrakApprovGU.aspx?done=2");
                //Response.Redirect("KontrakApprovBatal.aspx?done=2&project=" + project.SelectedValue);
            }
        }
    }
}
