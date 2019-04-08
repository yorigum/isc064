using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.APPROVAL
{
    public partial class ApprovalTagihanReschedule2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {

            }

            Fill();
            FillOld();
            FillNew();

        }

        private void Fill()
        {

            string strSql = "SELECT MS_TAGIHAN_TEMP.*, MS_KONTRAK.NilaiKontrak FROM MS_TAGIHAN_TEMP INNER JOIN MS_KONTRAK ON MS_TAGIHAN_TEMP.NoKontrak = MS_KONTRAK.NoKontrak WHERE MS_TAGIHAN_TEMP.NoKontrak = '" + NoKontrak + "'";

            DataTable rs = Db.Rs(strSql);
            //Response.Write(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {

                nokontrak.Text = rs.Rows[0]["NoKontrak"].ToString();
                decimal NT = Db.SingleDecimal("Select ISNULL(SUM(NilaiTagihan), 0) From Ms_TAGIHAN Where NoKontrak = '" + NoKontrak + "' AND Tipe IN ('BF','DP','ANG')");
                netto.Text = Cf.Num(NT);
                //netto.Text = Cf.Num(rs.Rows[0]["NilaiTagihan"]).ToString();
                //tglot.Text = Cf.Day(Convert.ToDateTime(DateTime.Today));

            }
        }

        private void FillOld()
        {
            string strSql = "SELECT * FROM MS_TAGIHAN where NoKontrak = '" + NoKontrak + "'"
                //+ " AND Nokontrak = '" + NoKontrak + "' AND Tipe <> 'ADM'"
                + " ORDER BY NoUrut";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rptTagihanNew, rs, "No Reservation.");
            decimal TotalOld = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUrut"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Tipe"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaTagihan"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglJT"]));
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]));
                r.Cells.Add(c);

                TotalOld += Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]);
                Rpt.Border(r);
                rptTagihanOld.Rows.Add(r);
            }

            grandTotalold.Text = Cf.Num(TotalOld);

        }

        private void FillNew()
        {
            string strSql = "SELECT * FROM MS_TAGIHAN_TEMP WHERE NoKontrak = '" + NoKontrak + "'"
                + " ORDER BY NoUrut";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rptTagihanNew, rs, "No Reservartion.");
            decimal TotalNew = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUrut"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Tipe"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaTagihan"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglJT"]));
                r.Cells.Add(c);

                //decimal NiTa = Db.SingleDecimal("select isnull(sum(NilaiTagihan),0) From MS_TAGIHAN Where NoKontrak = '" + NoKontrak + "'");
                //decimal NiPe = Db.SingleDecimal("select isnull(sum(NilaiPelunasan),0) From MS_PELUNASAN Where NoKontrak = '" + NoKontrak + "'");
                //decimal NiTe = Db.SingleDecimal("select isnull(sum(NilaiTagihan),0) From MS_TAGIHAN_TEMP Where NoKontrak = '" + NoKontrak + "'");
                //decimal NiMa = Db.SingleDecimal("select max(NilaiTagihan) from ms_tagihan_temp where NoKontrak = '" + NoKontrak + "'");
                //int NiMax = Db.SingleInteger("select max(NoUrut) from ms_tagihan_temp where NoKontrak = '" + NoKontrak + "'");
                //int NoUrut = Db.SingleInteger("select NoUrut from ms_tagihan_temp where NoKontrak = '" + NoKontrak + "'");
                //decimal SiTa = NiTa - NiPe;
                //decimal SeTa = SiTa - NiTe;
                //decimal NiMaBa = NiMa - SeTa;
                //decimal NiMaBa2 = NiMa + SeTa;

                //decimal A = Db.SingleDecimal("Select NilaiKontrak From Ms_Kontrak Where NoKontrak = '" + NoKontrak + "'");
                //decimal B = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan), 0)"
                //    + " FROM MS_PELUNASAN a"
                //    + " INNER JOIN MS_TAGIHAN b ON a.NoTagihan = b.NoUrut AND a.NoKontrak = b.NoKontrak"
                //    + " WHERE a.NoKontrak = '" + NoKontrak + "'"
                //    + " AND b.Tipe <> 'ADM'"
                //    );
                //decimal C = A - B;
                //decimal D = Db.SingleDecimal("Select Isnull(Sum(NiLaiTagihan),0) From Ms_Tagihan Where NoKontrak = '" + NoKontrak + "'");
                //decimal E = C - D;

                c = new TableCell();
                //if (Convert.ToInt32(rs.Rows[i]["NoUrut"].ToString()) == NiMax)
                //{
                //    if (NiTe < SiTa)
                //    {
                //        c.Text = Cf.Num(NiMaBa2);
                //    }
                //    else if (NiTe > SiTa)
                //    {
                //        c.Text = Cf.Num(NiMaBa);
                //    }
                //}
                //else
                //{
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]));
                //}
                r.Cells.Add(c);

                TotalNew += Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]);

                Rpt.Border(r);
                rptTagihanNew.Rows.Add(r);
            }

            grandTotalnew.Text = Cf.Num(TotalNew);

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
                    , "<script type='text/javascript'>"
                    + "document.getElementById('" + s + "').focus();"
                    + "</script>"
                    );
            }

            return x;
        }

        private void SaveApproval(String NoKontrak, string lvl, string Proj, int Approve)
        {
            int Lvl = Convert.ToInt16(lvl);
            int MaxApp = Db.SingleByte("SELECT TOP 1 Lvl FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 6 AND Project='" + Proj + "' ORDER BY Lvl DESC");
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

                    Ket = "Tgl. Reschedule : " + Cf.Day(DateTime.Today);
                    //Push notif ke Approval selanjutnya
                    //string Pemohon = Db.SingleString("SELECT PemohonRE FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    byte nextLvl = Convert.ToByte(Lvl + 1);
                    DataTable rsNextApp = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 6 "
                        + " AND Lvl = " + (Lvl + 1));

                    for (int i = 0; i < rsNextApp.Rows.Count; i++)
                    {
                        string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                        LibApi.PushNotif("APR-RE", "Permohonan Approval Reschedule Tagihan" + NoKontrak, UserIDNextApp, NoKontrak, 1);
                    }
                }
                else
                {
                    //FillNew();
                    DataTable rsBef = Db.Rs("SELECT "
                            + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                            + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                    DataTable caraBef = Db.Rs("SELECT CaraBayar AS [Cara Bayar] FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");

                    Db.Execute("DELETE FROM MS_TAGIHAN where nourut not in (select notagihan from ms_pelunasan where NoKontrak = '" + NoKontrak + "' AND NilaiPelunasan > 0) AND Nokontrak = '" + NoKontrak + "' AND Tipe != 'ADM'");
                    //Db.Execute("DELETE FROM MS_TAGIHAN where nourut not in (select notagihan from ms_pelunasan where NoKontrak = '" + NoKontrak + "') AND Nokontrak = '" + NoKontrak + "' AND Tipe <> 'ADM'");

                    //update detail approval dari user yang approve
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_DETAIL SET Approve = 1,Note = '" + Note + "',TglApproval = '" + DateTime.Today + "' WHERE NoApproval = '" + NoApproval + "' AND UserID = '" + Act.UserID + "'");

                    decimal TotalTagihanBef = 0;
                    for (int i = 1; i < rptTagihanNew.Rows.Count; i++)
                    {
                        if (!Response.IsClientConnected) break;

                        string Tipe = rptTagihanNew.Rows[i].Cells[1].Text;
                        string NamaTagihan = Cf.Str(rptTagihanNew.Rows[i].Cells[2].Text);
                        DateTime TglJT = Convert.ToDateTime(rptTagihanNew.Rows[i].Cells[3].Text);
                        decimal NilaiTagihan = Convert.ToDecimal(rptTagihanNew.Rows[i].Cells[4].Text);

                        Db.Execute("EXEC spTagihanDaftar"
                            + " '" + NoKontrak + "'"
                            + ",'" + NamaTagihan + "'"
                            + ",'" + TglJT + "'"
                            + ", " + NilaiTagihan
                            + ",'" + Tipe + "'"
                            );
                        if (i < rptTagihanNew.Rows.Count - 1)
                        {
                            TotalTagihanBef += NilaiTagihan;
                        }
                    }

                    DateTime Tgl = DateTime.Today;

                    string CB = Db.SingleString("Select CaraBayar From MS_TAGIHAN_HEADER WHERE NoKontrak ='" + NoKontrak + "'");
                    string Skema = Db.SingleString("Select Skema From MS_TAGIHAN_HEADER WHERE NoKontrak ='" + NoKontrak + "'");
                    int RefSkema = Db.SingleInteger("Select RefSkema From MS_TAGIHAN_HEADER WHERE NoKontrak ='" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK SET FlagReschedule = 0, ApprovelReschedule = 0 WHERE NoKontrak ='" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK SET CaraBayar = '" + CB + "', Skema = '" + Skema + "',RefSkema='" + RefSkema + "' WHERE NoKontrak = '" + NoKontrak + "'");

                    int NoUrutMax = Db.SingleInteger("SELECT MAX(NoUrut) FROM MS_TAGIHAN_TEMP WHERE NoKontrak = '" + NoKontrak + "' ");

                    if (CB == "KPA")
                    {
                        Db.Execute("Update Ms_Tagihan Set KPR = " + 1 + " Where NoKontrak = '" + NoKontrak + "' And NamaTagihan = 'PENCAIRAN KPA'"); ;//Nourut = '" + NoUrutMax + "'");
                    }

                    Db.Execute("Delete From MS_TAGIHAN_TEMP WHERE NoKontrak ='" + NoKontrak + "'");
                    DataTable rsDetail = Db.Rs("SELECT"
                        + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                        + ",MS_KONTRAK.NoUnit AS [Unit]"
                        + ",MS_CUSTOMER.Nama AS [Customer]"
                        + ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                        + ",CONVERT(VARCHAR,MS_KONTRAK_APP_LOG.TglApprove,106) AS [TglApprove]"
                        + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                        + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                        + " INNER JOIN MS_KONTRAK_APP_LOG"
                        + " ON MS_KONTRAK.NoKontrak = MS_KONTRAK_APP_LOG.NoKontrak"
                        + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "' ");

                    DataTable rsAft = Db.Rs("SELECT "
                        + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                        + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                    DataTable caraAft = Db.Rs("SELECT CaraBayar AS [Cara Bayar] FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");

                    Ket = Cf.LogCapture(rsDetail)
                        + Cf.LogList(rsBef, rsAft, "JADWAL TAGIHAN");

                    string Ket2 = Cf.LogCompare(caraBef, caraAft);
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
            + "," + 6 //Tipe
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
                + " 'APR-RE'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + NoKontrak + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if (datavalid())
            {
                SaveApproval(NoKontrak, Level, Project, 1);//1 = Approve, 2 = Reject
                Response.Redirect("ApprovalTagihanReschedule.aspx?done=1");
            }
        }
        protected void Batal_Click(object sender, EventArgs e)
        {
            SaveApproval(NoKontrak, Level, Project, 2);//1 = Approve, 2 = Reject
            Db.Execute("Delete From MS_TAGIHAN_HEADER WHERE NoKontrak ='" + NoKontrak + "'");
            Db.Execute("Delete From MS_TAGIHAN_TEMP WHERE NoKontrak ='" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK SET FlagReschedule = 0, ApprovelReschedule = 0 WHERE NoKontrak ='" + NoKontrak + "'");

            Response.Redirect("ApprovalTagihanReschedule.aspx?done=2");
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
