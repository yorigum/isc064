using System;
using System.Drawing;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.APPROVAL
{
    public partial class KontrakApprovADJ2 : System.Web.UI.Page
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
            string strSql = " SELECT e.*,f.*,b.NoUnit,b.PriceListMin, c.Nama, d.Nama AS Agent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL e"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_ADJUSMENT f ON e.SumberID = f.NoKontrak"
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
            skema.Text = rs.Rows[0]["SkemaAft"].ToString();
            tglpengajuan.Text = Cf.Day(rs.Rows[0]["TglPengajuan"].ToString());
            pricemin.Text = Cf.Num(rs.Rows[0]["PriceListMin"]);
            nilai.Text = Cf.Num(rs.Rows[0]["NilaiKontrakBfr"]);
            lblDPP.Text = Cf.Num(rs.Rows[0]["DPPBfr"]);
            lblPPN.Text = Cf.Num(rs.Rows[0]["NilaiPPNBfr"]);
            gross.Text = Cf.Num(rs.Rows[0]["GrossAft"]);
            bunga.Text = Cf.Num(rs.Rows[0]["BungaAft"]);
            diskonrupiah.Text = Cf.Num(rs.Rows[0]["DiskonRupiahAft"]);
            diskontambahan.Text = Cf.Num(rs.Rows[0]["DiskonTambahanAft"]);
            dpp.Text = Cf.Num(rs.Rows[0]["DPPAft"]);
            ppn.Text = Cf.Num(rs.Rows[0]["NilaiPPNAft"]);
            nilaikontrak.Text = Cf.Num(rs.Rows[0]["NilaiKontrakAft"]);
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
                Response.Redirect("KontrakApprovADJ.aspx?done=1");
            }
        }

        private void SaveApproval(String NoKontrak, string lvl, string Proj, int Approve)
        {
            int Lvl = Convert.ToInt16(lvl);
            int MaxApp = Db.SingleByte("SELECT TOP 1 Lvl FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 5 AND Project='" + Proj + "' ORDER BY Lvl DESC");
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
                    DataTable rsNextApp = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 5 "
                            + " AND Project='" + Proj + "' AND Lvl = " + (Lvl + 1));

                    for (int i = 0; i < rsNextApp.Rows.Count; i++)
                    {
                        string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                        LibApi.PushNotif("APR-GU", "Permohonan Approval Adjustment Kontrak " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                    }
                }
                else
                {
                    DataTable rsBef = Db.Rs("SELECT "
                           + " NilaiKontrak AS [No. Kontrak]"
                           + ", Gross AS [Gross] "
                           + ", DiskonRupiah AS [Diskon Rupiah]"
                           + ", DiskonTambahan AS [Diskon Tambahan]"
                           + ", BungaNominal AS [Bunga Nominal]"
                           + ", NilaiDPP AS [Nilai DPP]"
                           + ", NilaiPPN AS [Nilai PPN]"
                           + ", PPN AS [Status PPN]"
                           + ", Skema AS [Skema]"
                           + " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                    string strSql = "SELECT * FROM MS_APPROVAL_ADJUSMENT WHERE NoApproval = '" + NoApproval + "'";
                    DataTable rs = Db.Rs(strSql);

                    if (rs.Rows.Count != 0)
                    {
                        Db.Execute("UPDATE MS_KONTRAK "
                                    + " SET"
                                    + " NilaiKontrak = '" + rs.Rows[0]["NilaiKontrakAft"] + "'"
                                    + ", Gross = '" + rs.Rows[0]["GrossAft"] + "'"
                                    + ", DiskonRupiah = '" + rs.Rows[0]["DiskonRupiahAft"] + "'"
                                    + ", DiskonTambahan = '" + rs.Rows[0]["DiskonTambahanAft"] + "'"
                                    + ", BungaNominal = '" + rs.Rows[0]["BungaAft"] + "'"
                                    + ", NilaiDPP = '" + rs.Rows[0]["DPPAft"] + "'"
                                    + ", NilaiPPN = '" + rs.Rows[0]["NilaiPPNAft"] + "'"
                                    + ", PPN = '" + rs.Rows[0]["PPNAft"] + "'"
                                    + ", Skema = '" + rs.Rows[0]["SkemaAft"] + "'"
                                    + " WHERE NoKontrak = '" + NoKontrak + "'");

                        Db.Execute("EXEC spKontrakDiskon"
                            + " '" + NoKontrak + "'"
                            + ",'" + rs.Rows[0]["GrossAft"] + "'"
                            + ",'" + rs.Rows[0]["NilaiKontrakAft"] + "'"
                            + ",'" + rs.Rows[0]["DiskonRupiahAft"] + "'"
                            + ",''"
                            + ",''"
                        );
                    }

                    //update detail approval dari user yang approve
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_DETAIL SET Approve = 1,Note = '" + Note + "',TglApproval = '" + DateTime.Today + "' WHERE NoApproval = '" + NoApproval + "' AND UserID = '" + Act.UserID + "'");

                    DataTable rsAft = Db.Rs("SELECT "
                           + " NilaiKontrak AS [No. Kontrak]"
                           + ", Gross AS [Gross] "
                           + ", DiskonRupiah AS [Diskon Rupiah]"
                           + ", DiskonTambahan AS [Diskon Tambahan]"
                           + ", BungaNominal AS [Bunga Nominal]"
                           + ", NilaiDPP AS [Nilai DPP]"
                           + ", NilaiPPN AS [Nilai PPN]"
                           + ", PPN AS [Status PPN]"
                           + ", Skema AS [Skema]"
                           + " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                    Ket = Cf.LogCompare(rsBef, rsAft)
                        + "<br>Tgl Approval : " + DateTime.Today
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
            + "," + 5 //Tipe
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
                + " 'APR-ADJ'"
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
                Response.Redirect("KontrakApprovADJ.aspx?done=2");
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
