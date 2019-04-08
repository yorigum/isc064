using System;
using System.Drawing;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.APPROVAL
{
    public partial class KontrakApprovGN2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
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
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_GN f ON e.SumberID = f.NoKontrak"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a ON f.NoKontrak = a.NoKontrak"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT b ON a.NoStock = b.NoStock"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT d ON a.NoAgent = d.NoAgent"
                            + " WHERE e.SumberID = '" + NoKontrak + "'"
                            ;

            DataTable rs = Db.Rs(strSql);
            nokontrak.Text = rs.Rows[0]["SumberID"].ToString();
            unit.Text = rs.Rows[0]["NoUnit"].ToString();
            customer.Text = rs.Rows[0]["Nama"].ToString();
            agent.Text = rs.Rows[0]["Agent"].ToString();
            custlama.Text = rs.Rows[0]["Nama"].ToString();
            custbaru.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["CustomerBaru"].ToString() + "'");
            keterangan.Text = rs.Rows[0]["Keterangan"].ToString();
            tglpengajuan.Text = Cf.Day(rs.Rows[0]["TglPengajuan"].ToString());

            string File = Convert.ToString(Db.SingleDecimal("SELECT JurnalID FROM MS_KONTRAK_JURNAL WHERE NoKontrak = '" + NoKontrak + "' AND Ket LIKE '%" + NoApproval + "%'"));
            if (System.IO.File.Exists("D:\\ISC\\ISC064\\app\\marketingjual\\JurnalKontrak\\" + File + ".jpg"))
                file.HRef = "javascript:popGambar('../marketingjual/JurnalKontrak/" + File + ".jpg')";
            else
                file.InnerText = "";
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
                Response.Redirect("KontrakApprovGN.aspx?done=1");
            }
        }

        private void SaveApproval(String NoKontrak, string lvl, string Proj, int Approve)
        {
            int Lvl = Convert.ToInt16(lvl);
            int MaxApp = Db.SingleByte("SELECT TOP 1 Lvl FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 1 AND Project='" + Proj + "' ORDER BY Lvl DESC");
            string Note = note.Text;

            string Ket = "";

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
                    DataTable rsNextApp = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 1 "
                            + " AND Project='" + Proj + "' AND Lvl = " + (Lvl + 1));

                    for (int i = 0; i < rsNextApp.Rows.Count; i++)
                    {
                        string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                        LibApi.PushNotif("APR-GU", "Permohonan Approval Pindah Unit " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                    }
                }
                else
                {
                    DataTable rsBef = Db.Rs("SELECT "
                            + " MS_CUSTOMER.NoCustomer AS [No. Customer]"
                            + ",MS_CUSTOMER.Nama AS [Nama Customer]"
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER"
                            + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                            + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                    //ambil nocustomerbaru & biaya
                    string CustomerBaru = Db.SingleString(
                        "SELECT CustomerBaru FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_GN WHERE NoApproval = '" + NoApproval + "'");
                    decimal biaya = Db.SingleDecimal("SELECT BiayaAdmin FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_GN WHERE NoApproval = '" + NoApproval + "'");

                    //ganti nama
                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spKontrakGantiNama "
                            + " '" + NoKontrak + "'"
                            + ", '" + CustomerBaru + "'"
                            );

                    /*Update Flag ApprovalGN*/
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK "
                            + " SET ApprovalGN = 0"
                            + " ,Revisi = Revisi + 1"
                            + " WHERE NoKontrak='" + NoKontrak + "'"
                            );

                    DataTable rsAft = Db.Rs("SELECT "
                            + " MS_CUSTOMER.NoCustomer AS [No. Customer]"
                            + ",MS_CUSTOMER.Nama AS [Nama Customer]"                            
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER "
                            + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                            + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                    //update detail approval dari user yang approve
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_DETAIL SET Approve = 1,Note = '" + Note + "',TglApproval = '" + DateTime.Today + "' WHERE NoApproval = '" + NoApproval + "' AND UserID = '" + Act.UserID + "'");

                    //Insert tagihan
                    if (biaya != 0)
                    {
                        Db.Execute("EXEC ISC064_MARKETINGJUAL..spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA ADM. Pengalihan Hak'"
                            + ",'" + Cf.Day(DateTime.Today) + "'"
                            + ", " + biaya
                            + ",'ADM'"
                            );

                        int NoUrut = Db.SingleInteger("SELECT TOP 1 NoUrut FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut DESC");
                        Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN SET Jenis = 'Pengalihan Hak' WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoUrut);

                    }

                    /*Pengalihan Hak customer di MS_TTS*/
                    string strNamaCs = Cf.Str(Db.SingleString("SELECT Nama FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + CustomerBaru));
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

                    Ket = Cf.LogCompare(rsBef, rsAft)
                        + "<br>Biaya Administrasi : " + Cf.Num(biaya)
                        + "<br>Tgl Approval GN : " + DateTime.Today
                        ;
                }
            }
            else
            {
                //update detail approval dari user yang approve
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_DETAIL Set Approve = 2 "
                        + ", Note = '" + Note + "' "
                        + ", TglApproval = '" + DateTime.Today + "'"
                        + " WHERE NoApproval = '" + NoApproval + "' AND UserID = '" + Act.UserID + "'"
                        );
            }

            Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogKontrakApp "
            + " '" + NoKontrak + "'"
            + ",'" + Act.UserID + "'"
            + "," + Approve //Kode Approve
            + ",'" + DateTime.Today + "'"
            + "," + Lvl
            + "," + 1 //Tipe
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
                + " 'APR-GN'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + NoKontrak + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);
        }

        protected void reject_Click(object sender, EventArgs e)
        {
            if (datavalid())
            {
                SaveApproval(NoKontrak, Level, Project, 2);//1 = Approve, 2 = Reject
                Response.Redirect("KontrakApprovGN.aspx?done=2");
            }
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
    }
}
