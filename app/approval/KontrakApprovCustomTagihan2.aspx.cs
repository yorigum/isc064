using System;
using System.Drawing;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.APPROVAL
{
    public partial class KontrakApprovCustomTagihan2 : System.Web.UI.Page
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
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_CUSTOMIZE f ON e.SumberID = f.NoKontrak"
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
            skemabfr.Text = rs.Rows[0]["SkemaBfr"].ToString();
            skemaaft.Text = rs.Rows[0]["SkemaAft"].ToString();
            carabayarbfr.Text = rs.Rows[0]["CaraBayarBfr"].ToString();
            carabayaraft.Text = rs.Rows[0]["CaraBayarAft"].ToString();
            tglpengajuan.Text = Cf.Day(rs.Rows[0]["TglPengajuan"].ToString());

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
                Response.Redirect("KontrakApprovCustomTagihan.aspx?done=1");
            }
        }

        private void SaveApproval(String NoKontrak, string lvl, string Proj, int Approve)
        {
            int Lvl = Convert.ToInt16(lvl);
            int MaxApp = Db.SingleByte("SELECT TOP 1 Lvl FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 7 AND Project='" + Proj + "' ORDER BY Lvl DESC");
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
                    DataTable rsNextApp = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 7 "
                        + " AND Project='" + Proj + "' AND Lvl = " + (Lvl + 1));

                    for (int i = 0; i < rsNextApp.Rows.Count; i++)
                    {
                        string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                        LibApi.PushNotif("APR-CUS", "Permohonan Approval Customize Tagihan " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                    }
                }
                else
                {
                    string strSql = "SELECT * FROM MS_TAGIHAN_TEMP WHERE NoKontrak = '" + NoKontrak + "'"
                    + " ORDER BY NoUrut";
                    DataTable rs3 = Db.Rs(strSql);

                    for (int i = 0; i < rs3.Rows.Count; i++)
                    {
                        if (!Response.IsClientConnected) break;

                        string Tipe = rs3.Rows[i]["Tipe"].ToString();
                        string NamaTagihan = Cf.Str(rs3.Rows[i]["NamaTagihan"].ToString());
                        DateTime TglJT = Convert.ToDateTime(rs3.Rows[i]["TglJT"].ToString());
                        decimal NilaiTagihan = Convert.ToDecimal(rs3.Rows[i]["NilaiTagihan"].ToString());

                        Db.Execute("EXEC spTagihanDaftar"
                            + " '" + NoKontrak + "'"
                            + ",'" + NamaTagihan + "'"
                            + ",'" + TglJT + "'"
                            + ", " + NilaiTagihan
                            + ",'" + Tipe + "'"
                            );

                        if (i == (rs3.Rows.Count - 1))
                        {
                            if (Convert.ToBoolean(rs3.Rows[i]["KPR"]) == true)
                            {
                                int NoUrutMax = Db.SingleInteger("SELECT MAX(NoUrut) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ");
                                Db.Execute("UPDATE MS_TAGIHAN SET KPR = " + 1 + " WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" + (NoUrutMax) + "'");
                            }
                        }
                    }

                    //update detail approval dari user yang approve
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_DETAIL SET Approve = 1,Note = '" + Note + "',TglApproval = '" + DateTime.Today + "' WHERE NoApproval = '" + NoApproval + "' AND UserID = '" + Act.UserID + "'");

                    //update manual
                    Db.Execute("UPDATE MS_KONTRAK "
                        + " SET Skema = '" + skemaaft.Text + "'"
                        + ", CaraBayar = '" + carabayaraft.Text + "'"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );

                    DataTable rs2 = Db.Rs("SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                    DataTable rsAft = Db.Rs("SELECT "
                        + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                        + "FROM MS_TAGIHAN_TEMP WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                    Ket = Cf.LogCapture(rs2)
                        + "<br>Nilai Kontrak : " + Cf.Num(rs2.Rows[0]["NilaiKontrak"])
                        + "<br>Tgl. Batal : " + Cf.Day(DateTime.Today)
                        + "<br>Skema : " + skemaaft.Text
                        + "<br>Cara Bayar : " + carabayaraft.Text
                        + Cf.LogList(rsAft, "JADWAL TAGIHAN")
                        ;

                    //hapus data yang dipenampungan
                    Db.Execute("Delete From MS_TAGIHAN_TEMP WHERE NoKontrak ='" + NoKontrak + "'");

                    Func.CekKomisi(NoKontrak);
                }
            }
            else
            {
                //hapus data yang dipenampungan
                Db.Execute("Delete From MS_TAGIHAN_TEMP WHERE NoKontrak ='" + NoKontrak + "'");
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
            + "," + 7 //Tipe
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
                + " 'APR-CUS'"
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
                Response.Redirect("KontrakApprovCustomTagihan.aspx?done=2");
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
