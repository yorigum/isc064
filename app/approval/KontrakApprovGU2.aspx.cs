using System;
using System.Drawing;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.APPROVAL
{
    public partial class KontrakApprovGU2 : System.Web.UI.Page
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
            string strSql = " SELECT e.*,f.*,b.NoUnit, c.Nama, d.Nama AS Agent,a.NilaiKontrak FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL e"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_GU f ON e.SumberID = f.NoKontrak"
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
            unitlama.Text = rs.Rows[0]["NoUnit"].ToString();
            nilaikontrak.Text = Cf.Num(Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]));
            unitbaru.Text = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock = '" + rs.Rows[0]["UnitBaru"].ToString() + "'");
            nilaikontrak2.Text = Cf.Num(Db.SingleDecimal("SELECT Pricelist FROM MS_UNIT WHERE NoStock = '" + rs.Rows[0]["UnitBaru"].ToString() + "'"));

            keterangan.Text = rs.Rows[0]["Keterangan"].ToString();
            tglpengajuan.Text = Cf.Day(rs.Rows[0]["TglPengajuan"].ToString());

            string File = Convert.ToString(Db.SingleDecimal("SELECT JurnalID FROM MS_KONTRAK_JURNAL WHERE NoKOntrak = '" + NoKontrak + "'"));
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
                Response.Redirect("KontrakApprovGU.aspx?done=1");
            }
        }

        private void SaveApproval(String NoKontrak, string lvl, string Proj, int Approve)
        {
            int Lvl = Convert.ToInt16(lvl);
            int MaxApp = Db.SingleByte("SELECT TOP 1 Lvl FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 2 AND Project='" + Proj + "' ORDER BY Lvl DESC");
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
                    DataTable rsNextApp = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 2 "
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
                            + " NoStock AS [No. Stock]"
                            + ",NoUnit AS [Unit]"
                            + ",Luas AS [Luas]"
                            + ",Gross AS [Nilai Gross]"
                            + ",NilaiKontrak AS [Nilai Kontrak]"
                            + ",DiskonRupiah AS [Diskon dalam Rupiah]"
                            + ",DiskonPersen AS [Diskon dalam Persen]"
                            + ",NilaiPPN AS [Nilai PPN]"
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK"
                            + " WHERE NoKontrak = '" + NoKontrak + "'");

                    string NoStockOld = Db.SingleString(
                        "SELECT UnitLama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_GU WHERE NoApproval = '" + NoApproval + "'");


                    string NoStockTemp = Db.SingleString(
                        "SELECT UnitBaru FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_GU WHERE NoApproval = '" + NoApproval + "'");

                    decimal biaya = Db.SingleDecimal("SELECT BiayaAdmin FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_GU WHERE NoApproval = '" + NoApproval + "'");

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spKontrakGantiUnit "
                        + " '" + NoKontrak + "'"
                        + ",'" + NoStockTemp + "'"
                        );

                    string NoUnitBaru = Db.SingleString("SELECT NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoStock = '" + NoStockTemp + "'");

                    //update nounit di kontrak
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK SET NoUnit = '" + NoUnitBaru + "' WHERE NoKontrak = '" + NoKontrak + "'");

                    //update detail approval dari user yang approve
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_DETAIL SET Approve = 1,Note = '" + Note + "',TglApproval = '" + DateTime.Today + "' WHERE NoApproval = '" + NoApproval + "' AND UserID = '" + Act.UserID + "'");


                    //Insert tagihan
                    if (biaya != 0)
                    {
                        Db.Execute("EXEC ISC064_MARKETINGJUAL..spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA ADM. Pindah Unit'"
                            + ",'" + Cf.Day(DateTime.Today) + "'"
                            + ", " + biaya
                            + ",'ADM'"
                            );

                        int NoUrut = Db.SingleInteger("SELECT TOP 1 NoUrut FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut DESC");
                        Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN SET Jenis = 'Pindah Unit' WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoUrut);

                    }


                    //UPDATE Nilai PPN , Nilai Kontrak Terbaru, PPNPemerintah, ApprovalGU
                    decimal GrossBaru = Db.SingleDecimal("SELECT Pricelist FROM MS_UNIT WHERE NoUnit = '" + NoUnitBaru + "'");
                    decimal DiskonRupiah = Db.SingleDecimal("SELECT DiskonRupiah FROM MS_KONTRAK WHERE NoKOntrak = '" + NoKontrak + "'");
                    decimal DiskonTambahan = Db.SingleDecimal("SELECT DiskonTambahan FROM MS_KONTRAK WHERE NoKOntrak = '" + NoKontrak + "'");
                    decimal BungaRupiah = Db.SingleDecimal("SELECT BungaNominal FROM MS_KONTRAK WHERE NoKOntrak = '" + NoKontrak + "'");
                    string ParamID = "PLIncludePPN" + Project;
                    decimal DPP = 0, NilaiPPN = 0, NilaiKontrak = 0;
                    bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";
                    bool jenisppn = Db.SingleBool("SELECT PPN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");
                    decimal Netto = GrossBaru + BungaRupiah - DiskonRupiah - DiskonTambahan;
                    if (jenisppn)
                    {
                        if (includeppn)
                        {
                            DPP = Math.Round(Netto / (decimal)1.1);
                            NilaiPPN = Netto - DPP;
                        }
                        else
                        {
                            DPP = Netto;
                            NilaiPPN = (DPP * (decimal)0.1);
                        }
                    }
                    else
                    {
                        DPP = Netto;
                    }

                    NilaiKontrak = DPP + NilaiPPN;
                    decimal PPN = Math.Round(NilaiKontrak - DPP);
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK "
                                + " SET NilaiPPN='" + NilaiPPN + "'"
                                + ", NilaiKontrak='" + NilaiKontrak + "'"
                                + ", NilaiDPP='" + DPP + "'"
                                + ", Gross='" + GrossBaru + "'"
                                + ", ApprovalGU = '" + Convert.ToBoolean(0) + "'"
                                + ", Revisi = Revisi + 1"
                                + " WHERE NoKontrak='" + NoKontrak + "'"
                                );


                    DataTable rsAft = Db.Rs("SELECT "
                                + " NoStock AS [No. Stock]"
                                + ",NoUnit AS [Unit]"
                                + ",Luas AS [Luas]"
                                + ",Gross AS [Nilai Gross]"
                                + ",NilaiKontrak AS [Nilai Kontrak]"
                                + ",DiskonRupiah AS [Diskon dalam Rupiah]"
                                + ",DiskonPersen AS [Diskon dalam Persen]"
                                + ",NilaiPPN AS [Nilai PPN]"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK"
                                + " WHERE NoKontrak = '" + NoKontrak + "'");


                    /*Ganti nomor unit di MS_TTS*/
                    string strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                        + " SET Unit = '" + NoUnitBaru + "'"
                        + " WHERE Ref = '" + NoKontrak + "'"
                        + " AND Tipe = 'JUAL'"
                        ;
                    Db.Execute(strSql);
                    /*******************************/

                    /*Ganti nomor unit di MS_MEMO*/
                    strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_MEMO"
                        + " SET Unit = '" + NoUnitBaru + "'"
                        + " WHERE Ref = '" + NoKontrak + "'"
                        + " AND Tipe = 'JUAL'"
                        ;
                    Db.Execute(strSql);
                    /*******************************/

                    /*Ganti nomor unit di MS_PJT*/
                    strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PJT"
                        + " SET Unit = '" + NoUnitBaru + "'"
                        + " WHERE Ref = '" + NoKontrak + "'"
                        + " AND Tipe = 'JUAL'"
                        ;
                    Db.Execute(strSql);
                    /*******************************/

                    /*Ganti nomor unit di MS_TUNGGAKAN*/
                    strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN"
                        + " SET Unit = '" + NoUnitBaru + "'"
                        + " WHERE Ref = '" + NoKontrak + "'"
                        + " AND Tipe = 'JUAL'"
                        ;
                    Db.Execute(strSql);
                    /*******************************/

                    Ket = Cf.LogCompare(rsBef, rsAft)
                        + "<br>Biaya Administrasi : " + Cf.Num(biaya)
                        + "<br>Keterangan Tambahan : " + Db.SingleString("SELECT Keterangan FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_GU WHERE NoApproval = '" + NoApproval + "'")
                        + "<br>Tgl Approval : " + Cf.Day(DateTime.Today);
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
            + "," + 2 //Tipe
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
                + " 'APR-GU'"
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
                Response.Redirect("KontrakApprovGU.aspx?done=2");
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
