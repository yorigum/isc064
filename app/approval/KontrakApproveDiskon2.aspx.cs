using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.APPROVAL
{
    public partial class KontrakApproveDiskon2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {

            }
            Fill();
        }

        private void Fill()
        {
            Func.DiskonHeader(NoKontrak, noapprov1, unit, customer, agent, tglpengajuan);

            btnlog.Attributes["ondbclick"] = "<a href=\"javascript:call('" + NoKontrak + "')\"></a>";

            decimal NilaiDiskon = Db.SingleDecimal("SELECT DiskonTambahan FROM MS_KONTRAK_APPROVAL WHERE NoKontrak='" + NoKontrak + "'");
            decimal NilaiKontrakAft = Db.SingleDecimal("SELECT NilaiKontrak FROM MS_KONTRAK_APPROVAL WHERE NoKontrak='" + NoKontrak + "'");
            decimal PricelistMin = Db.SingleDecimal("SELECT PricelistMin FROM MS_UNIT WHERE NoStock = (SELECT NoStock FROM MS_KONTRAK_APPROVAL WHERE NoKontrak = '" + NoKontrak + "')");
            decimal NilaiKontrakBef = NilaiKontrakAft + NilaiDiskon;
            decimal PersenDiskon = NilaiDiskon / NilaiKontrakBef * 100;
            nkontrakaft.Text = Cf.Num(Math.Round(NilaiKontrakAft));
            nkontrakaft.ForeColor = (NilaiKontrakAft < PricelistMin) ? Color.Red : Color.Black;
            nkontrakbef.Text = Cf.Num(Math.Round(NilaiKontrakBef));
            nilaidiskon.Text = Cf.Num(NilaiDiskon) + " ( " + Cf.Num(PersenDiskon) + " %)";

            byte lvl = Db.SingleByte("SELECT Lvl FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 4 AND Project='" + Project + "' AND UserID = '" + Act.UserID + "'");
            string komen = Db.SingleString("SELECT Komentar FROM MS_KONTRAK_APP_LOG WHERE NoKontrak = '" + NoKontrak + "' AND Tipe = 4 AND Lvl = '" + (lvl - 1) + "'");

            //if (NilaiKontrakAft < PricelistMin && stat2.SelectedValue == "1")
            //{
            //    Js.Confirm(save, "Nilai Kontrak dibawah Pricelist Minimum. Lanjutkan?");
            //}

        }

        private bool valid()
        {
            bool x = true;

            if (Cf.isEmpty(note))
            {
                x = false;
                Cf.MarkError(note);
            }

            return x;
        }

        private void SaveTagihan(string NoKontrakAft)
        {
            DataTable rsapp = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrakAft + "'");
            int CaraBayar = Convert.ToInt32(rsapp.Rows[0]["RefSkema"]);
            decimal Netto = 0;
            //cara bayar 0 = customize
            if (CaraBayar != 0)
            {
                string RumusDiskon = rsapp.Rows[0]["DiskonPersen"].ToString();
                string RumusDiskon2 = Db.SingleString(
                    "SELECT DiskonKet FROM REF_SKEMA WHERE Nomor = " + CaraBayar);
                string RumusBunga = rsapp.Rows[0]["BungaPersen"].ToString();

                decimal Gross = Db.SingleDecimal(
                     "SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrakAft + "'");
                decimal Gross2 = Gross;

                Netto = Func.SetelahBunga(RumusBunga, Gross2);
                decimal GrossAfterDiskon = Func.SetelahDiskon(RumusDiskon, Netto);
                Netto = (CaraBayar != 0) ? Func.SetelahDiskon(RumusDiskon, Netto) : Netto;
                //decimal NilaiDiskon = Math.Round(Gross2 - GrossAfterDiskon);


                /* DISKON TAMBAHAN SAAT CLOSING */
                decimal DiskonTambahan = 0;
                if (rsapp.Rows[0]["DiskonTambahan"].ToString() != "")
                {   //Diskon lum sum
                    DiskonTambahan = Convert.ToDecimal(rsapp.Rows[0]["DiskonTambahan"].ToString());
                }
                Netto -= DiskonTambahan;

                Db.Execute("UPDATE MS_KONTRAK"
                 + " SET DiskonTambahan = " + DiskonTambahan
                 + " WHERE NoKontrak = '" + NoKontrakAft + "'");


                /********************************/

                string DiskonPersen = rsapp.Rows[0]["DiskonPersen"].ToString();
                decimal DiskonRupiah = Convert.ToDecimal(rsapp.Rows[0]["DiskonRupiah"].ToString());

                Db.Execute("UPDATE MS_KONTRAK"
                 + " SET DiskonKet='" + RumusDiskon2 + "'"
                 + ", DiskonPersen='" + DiskonPersen + "'"
                 + ", DiskonRupiah = " + DiskonRupiah
                 + " WHERE NoKontrak = '" + NoKontrakAft + "'");

                /***********/

                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + rsapp.Rows[0]["NoStock"].ToString() + "'");
                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                string Pers = Db.SingleString("SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                string NamaPers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Pers + "'");

                string ParamID = "PLIncludePPN" + Project;
                bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";

                decimal NilaiPPN = 0;
                decimal NilaiKontrak = 0;
                decimal DPP = 0;

                if (includeppn)
                    DPP = Math.Round(Netto / (decimal)1.1);
                else
                    DPP = Netto;


                if (rsapp.Rows[0]["PPN"].ToString() == "1")
                {
                    if (includeppn)
                    {
                        if (rsapp.Rows[0]["PPNBulat"].ToString() == "1")
                            NilaiPPN = Math.Round(Netto - DPP);
                        else
                            NilaiPPN = Netto - DPP;
                    }
                    else
                    {
                        NilaiPPN = (DPP * (decimal)0.1);

                        if (rsapp.Rows[0]["PPNBulat"].ToString() == "1")
                            NilaiPPN = Math.Round(NilaiPPN);
                    }
                }

                NilaiKontrak = Netto + NilaiPPN;
                decimal PPN = Math.Round(NilaiKontrak - DPP);


                Db.Execute("EXEC spKontrakDiskon"
                    + " '" + NoKontrakAft + "'"
                    + ", " + Gross2
                    + ", " + NilaiKontrak
                    + ", " + DiskonRupiah
                    + ",'" + RumusDiskon + "'"
                    + ",'" + Cf.Str(RumusDiskon2) + "'"
                    );

                Db.Execute("EXEC spKontrakBunga"
                   + " '" + NoKontrakAft + "'"
                   + ", " + Gross
                   + ", " + Math.Round(NilaiKontrak)
                   + ",'" + RumusBunga + "'"
                   );

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET "
                    + " NilaiPPN = " + PPN
                    + ", NilaiDPP = " + DPP
                    + " , NilaiKontrak = " + NilaiKontrak
                    + " WHERE NoKontrak = '" + NoKontrakAft + "'");

                string[,] x = Func.Breakdown(CaraBayar, NilaiKontrak, Convert.ToDateTime(rsapp.Rows[0]["TglKontrak"].ToString()));
                for (int i = 0; i <= x.GetUpperBound(0); i++)
                {

                    Db.Execute("EXEC spTagihanDaftar"
                        + " '" + NoKontrakAft + "'"
                        + ",'" + x[i, 2] + "'"
                        + ",'" + Convert.ToDateTime(x[i, 3]) + "'"
                        + ", " + Convert.ToDecimal(x[i, 4])
                        + ",'" + x[i, 1] + "'"
                        );

                    int NoUrut = Db.SingleInteger("SELECT TOP 1 NoUrut FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrakAft + "' ORDER BY NoUrut DESC");
                    Db.Execute("UPDATE MS_TAGIHAN"
                        + " SET KPR = " + x[i, 5]
                        + " WHERE NoKontrak = '" + NoKontrakAft + "'"
                        + " AND NoUrut = " + NoUrut
                        );

                }

            }
            else
            {
                string RumusBunga = rsapp.Rows[0]["BungaPersen"].ToString();

                decimal Gross = Db.SingleDecimal(
                     "SELECT Gross FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrakAft + "'");
                decimal Gross2 = Gross;

                decimal GrossAfterDiskon = Gross2;
                Netto = Gross2;


                /* DISKON TAMBAHAN SAAT CLOSING */
                decimal DiskonTambahan = 0;
                if (rsapp.Rows[0]["DiskonTambahan"].ToString() != "")
                {   //Diskon lum sum
                    DiskonTambahan = Convert.ToDecimal(rsapp.Rows[0]["DiskonTambahan"].ToString());
                }
                Netto -= DiskonTambahan;

                Db.Execute("UPDATE MS_KONTRAK"
                 + " SET DiskonTambahan = " + DiskonTambahan
                 + " WHERE NoKontrak = '" + NoKontrakAft + "'");


                /********************************/
                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = '" + rsapp.Rows[0]["NoStock"].ToString() + "'");
                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                string Pers = Db.SingleString("SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                string NamaPers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Pers + "'");

                string ParamID = "PLIncludePPN" + Project;
                bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'PLIncludePPN'") == "True";

                decimal NilaiPPN = 0;
                decimal NilaiKontrak = 0;
                decimal DPP = 0;

                if (includeppn)
                    DPP = Math.Round(Netto / (decimal)1.1);
                else
                    DPP = Netto;


                if (rsapp.Rows[0]["PPN"].ToString() == "1")
                {
                    if (includeppn)
                    {
                        if (rsapp.Rows[0]["PPNBulat"].ToString() == "1")
                            NilaiPPN = Math.Round(Netto - DPP);
                        else
                            NilaiPPN = Netto - DPP;
                    }
                    else
                    {
                        NilaiPPN = (DPP * (decimal)0.1);

                        if (rsapp.Rows[0]["PPNBulat"].ToString() == "1")
                            NilaiPPN = Math.Round(NilaiPPN);
                    }
                }

                NilaiKontrak = Netto + NilaiPPN;

                decimal PPN = Math.Round(NilaiKontrak - DPP);

                Db.Execute("EXEC spKontrakDiskon"
                    + " '" + NoKontrakAft + "'"
                    + ", " + Gross2
                    + ", " + NilaiKontrak
                    + ",0"
                    + ",''"
                    + ",''"
                    );

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET "
                    + " NilaiPPN = " + PPN
                    + ", NilaiDPP = " + DPP
                    + " , NilaiKontrak = " + NilaiKontrak
                    + " WHERE NoKontrak = '" + NoKontrakAft + "'");
            }
        }

        private string SN
        {
            get
            {
                return Cf.Pk(Request.QueryString["SN"]);
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

        private static decimal RoundUp(decimal input)
        {
            string x = input.ToString();
            string[] arr = x.Split(new char[] { '.' });

            if (arr.Length > 1)
            {
                if (decimal.Parse(arr[1]) > 0)
                {
                    decimal dc = decimal.Parse(arr[0]) + 1;
                    return dc;
                }
                else
                {
                    return decimal.Parse(arr[0]);
                }
            }
            else
            {
                return input;
            }
        }
        private static decimal RoundThousand(decimal input)
        {
            if (input < 1000)
            {
                return 0;
            }
            else
            {
                input = RoundUp(input);
                if ((input % 1000) > 0)
                {
                    input = (input - (input % 1000)) + 0;
                }
                return input;
            }
        }
        private static DataTable HakApp()
        {
            DataTable hakapp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 4");

            return hakapp;
        }

        protected void save_Click1(object sender, EventArgs e)
        {
            if (valid())
            {
                DateTime Tgl = Convert.ToDateTime(DateTime.Today);

                SaveApproval(NoKontrak, Tgl, Level);//1 = Approve, 2 = Reject
                Response.Redirect("KontrakApproveDiskon.aspx?done=1");
            }
        }

        protected void reject_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                byte lvl = Db.SingleByte("SELECT Lvl FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 4 AND Project='" + Project + "' AND UserID = '" + Act.UserID + "'");
                Db.Execute("EXEC ISC064_MARKETINGJUAL..spLogKontrakApp "
                    + " '" + NoKontrak + "'"
                    + ",'" + Act.UserID + "'"
                    + "," + 2 //Reject
                    + ",'" + Convert.ToDateTime(Cf.Day(DateTime.Today)) + "'"
                    + "," + lvl
                    + "," + 4 //Tipe
                    + ",''"
                    );

                //kembalikan status unit jadi available
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT SET Status = 'A' WHERE NoStock = (SELECT NoStock FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK_APPROVAL WHERE NoKontrak = '" + NoApproval + "')");

                //ganti status approval jadi done
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL SET Status = 'DONE'"
                    + ",TglApproval = '" + DateTime.Today + "'"
                    + " WHERE NoApproval = '" + NoApproval + "'"
                    );

                //update detail approval dari user yang approve
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_DETAIL Set Approve = 2 "
                        + ", Note = '" + note.Text + "' "
                        + ", TglApproval = '" + DateTime.Today + "'"
                        + " WHERE NoApproval = '" + NoApproval + "' AND UserID = '" + Act.UserID + "'"
                        );

                Response.Redirect("KontrakApproveDiskon.aspx?done=2");
            }
        }

        private void SaveApproval(string NoKontrak, DateTime Tgl, string lvl)
        {
            int Lvl = Convert.ToInt16(lvl);
            int MaxApp = Db.SingleByte("SELECT TOP 1 Lvl FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 4 AND Project='" + Project + "' ORDER BY Lvl DESC");
            string Note = note.Text;
            string Ket = "";

            if (Lvl < MaxApp)
            {
                //update status approval jadi proses
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL SET Status = 'PROCESS',TglApproval = '" + DateTime.Today + "' WHERE NoApproval = '" + NoApproval + "'");
                //update detail approval dari user yang approve
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_DETAIL SET Approve = 1,Note = '" + Note + "',TglApproval = '" + DateTime.Today + "' WHERE NoApproval = '" + NoApproval + "' AND UserID = '" + Act.UserID + "' AND Lvl = '" + Lvl + "'");

                Ket = "Tgl Approval : " + Cf.Day(Convert.ToDateTime(Tgl));
            }
            else
            {
                DataTable rsapp = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK_APPROVAL WHERE NoKontrak = '" + NoKontrak + "'");
                for (int i = 0; i < rsapp.Rows.Count; i++)
                {
                    DateTime TglKontrak = Convert.ToDateTime(rsapp.Rows[i]["TglKontrak"].ToString());
                    DateTime TglApproveDiskon = Convert.ToDateTime(DateTime.Today);
                    string Skema = rsapp.Rows[i]["Skema"].ToString();
                    string RefSkema = rsapp.Rows[i]["RefSkema"].ToString();
                    string JenisPPN = rsapp.Rows[i]["JenisPPN"].ToString();
                    string CaraBayar = rsapp.Rows[i]["CaraBayar"].ToString();
                    string KPR = rsapp.Rows[i]["JenisKPR"].ToString();
                    string NoStock = rsapp.Rows[i]["NoStock"].ToString();
                    string NoCustomer = rsapp.Rows[i]["NoCustomer"].ToString();
                    int NoAgent = Convert.ToInt32(rsapp.Rows[i]["NoAgent"].ToString());
                    DateTime TargetST = Convert.ToDateTime(rsapp.Rows[i]["TargetST"].ToString());
                    decimal gross = Convert.ToDecimal(rsapp.Rows[i]["Gross"].ToString());
                    decimal pl = Convert.ToDecimal(rsapp.Rows[i]["NilaiKontrak"].ToString());
                    String BungaPersen = rsapp.Rows[i]["BungaPersen"].ToString();
                    string BungaNominal = rsapp.Rows[i]["BungaNominal"].ToString();
                    decimal DiskonRp = Convert.ToDecimal(rsapp.Rows[i]["DiskonRupiah"].ToString());
                    string DiskonPersen = rsapp.Rows[i]["DiskonPersen"].ToString();
                    string DiskonTambahan = rsapp.Rows[i]["DiskonTambahan"].ToString();
                    string SumberDana = rsapp.Rows[i]["SumberDana"].ToString();
                    string SumberDanaLainnya = rsapp.Rows[i]["SumberDanaLainnya"].ToString();
                    string TujuanKontrak = rsapp.Rows[i]["TujuanKontrak"].ToString();
                    string NamaProject = rsapp.Rows[i]["NamaProject"].ToString();
                    string Pers = rsapp.Rows[i]["Pers"].ToString();
                    string PPN = rsapp.Rows[i]["PPN"].ToString();
                    string NamaPers = rsapp.Rows[i]["NamaPers"].ToString();
                    string NoKontrakAft = Numerator.SuratPesanan(TglKontrak.Month, TglKontrak.Year, Project);

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spKontrakDaftar4"
                                + " '" + NoKontrakAft + "'"
                                + ",'" + NoStock + "'"
                                + ",'" + TglKontrak + "'"
                                + ",'" + Skema + "'"
                                + ",'" + TargetST + "'"
                                + ",'" + NoCustomer + "'"
                                + ",'" + NoAgent + "'"
                                + ", " + pl
                                );

                    string sSQL = "UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK"
                                    + " SET JenisKPR = '" + KPR + "'"
                                    + ", CaraBayar = '" + CaraBayar + "'"
                                    + ", RefSkema = " + RefSkema + ""
                                    + ", Gross = " + gross + ""
                                    + ", PPN = '" + PPN + "'"
                                    + ", jenisPPN = '" + JenisPPN + "'"
                                    + ", SumberDana ='" + SumberDana + "'"
                                    + ", SumberDanaLainnya ='" + Cf.Str(SumberDanaLainnya) + "'"
                                    + ", TujuanKontrak = '" + TujuanKontrak + "'"
                                    + ", TglApproveDiskon = '" + TglApproveDiskon + "'"
                                    + ", DiskonRupiah = '" + DiskonRp + "'"
                                    + ", DiskonPersen = '" + DiskonPersen + "'"
                                    + ", DiskonTambahan = '" + DiskonTambahan + "'"
                                    + ", BungaPersen = '" + BungaPersen + "'"
                                    + ", BungaNominal = '" + BungaNominal + "'"
                                    + ", Project = '" + Project + "'"
                                    + ", NamaProject = '" + NamaProject + "'"
                                    + ", Pers = '" + Pers + "'"
                                    + ", NamaPers = '" + NamaPers + "'"
                                    + " WHERE NoKontrak = '" + NoKontrakAft + "'"
                                    ;
                    Db.Execute(sSQL);
                    SaveKontrakAgent(NoKontrakAft, NoAgent, 1);
                    SaveTagihan(NoKontrakAft);

                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT SET Status = 'B' WHERE NoStock = '" + NoStock + "'");

                    DataTable rs = Db.Rs("SELECT "
                        + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                        + ",MS_KONTRAK.NoUnit AS [Unit]"
                        + ",MS_CUSTOMER.Nama AS [Customer]"
                        + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS [Agent]"
                        + ",CONVERT(varchar,MS_KONTRAK.TglKontrak,106) AS [Tanggal Kontrak]"
                        + ",MS_KONTRAK.NoStock AS [No. Stock]"
                        + ",MS_KONTRAK.Luas AS [Luas]"
                        + ",MS_KONTRAK.Gross AS [Nilai Gross]"
                        + ",MS_KONTRAK.DiskonRupiah AS [Diskon dalam Rupiah]"
                        + ",MS_KONTRAK.DiskonPersen AS [Diskon dalam Persen]"
                        + ",MS_KONTRAK.DiskonKet AS [Keterangan Diskon]"
                        + ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                        + ",MS_KONTRAK.BungaPersen AS [Bunga dalam Persen]"
                        + ",MS_KONTRAK.BungaNominal AS [Bunga dalam Rupiah]"
                        + ",MS_KONTRAK.Skema"
                        + ",MS_KONTRAK.DiskonTambahan"
                        + ",CONVERT(varchar,MS_KONTRAK.TargetST,106) AS [Jadwal Serah Terima]"
                        + ", MS_KONTRAK.JenisPPN AS [PPN Ditanggung]"
                        + ", CASE MS_KONTRAK.JenisKPR"
                        + "		WHEN 0 THEN 'KPA'"
                        + "		WHEN 1 THEN 'NON-KPA'"
                        + "	END AS [Jenis KPA]"
                        + ",MS_KONTRAK.SumberDana AS [Sumber Dana]"
                        + ",MS_KONTRAK.SumberDanaLainnya AS [Sumber Dana Lainnya]"
                        + ",MS_KONTRAK.TujuanKontrak AS [Tujuan Transaksi]"
                        + ",MS_KONTRAK.TujuanLainnya AS [Tujuan Transaksi Lainnya]"
                        + ",MS_KONTRAK.NUP"
                        + ",MS_KONTRAK.NoRefferatorAgent"
                        + ",MS_KONTRAK.NoRefferatorCustomer"
                        + ", CASE MS_KONTRAK.TitipJual"
                        + "		WHEN 0 THEN 'Non Titip Jual'"
                        + "		WHEN 1 THEN 'Titip Jual'"
                        + "	END AS [Status Titip Jual]"
                        + ", CASE MS_KONTRAK.PaketInvestasi"
                        + "		WHEN 0 THEN 'TIDAK'"
                        + "		WHEN 1 THEN 'YA'"
                        + "	END AS [Status Paket Investasi]"
                        + ", TglPaketInvestasi AS [Tanggal Berakhir Paket Investasi]"
                        + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                        + " INNER JOIN ISC064_MARKETINGJUAL..MS_AGENT AS MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                        + " WHERE NoKontrak = '" + NoKontrakAft + "'"
                        );



                    DataTable rsTagihan = Db.Rs("SELECT "
                        + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                        + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrakAft + "' ORDER BY NoUrut");

                    //Logfile
                    string KetLog = Cf.LogCapture(rs)
                        + Cf.LogList(rsTagihan, "JADWAL TAGIHAN");

                    Db.Execute("EXEC ISC064_MARKETINGJUAL..spLogKontrak "
                        + " 'APR-DISKON'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + NoKontrakAft + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    //update nokontrak after
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_DISKON SET NoKontrakAfter = '" + NoKontrakAft + "' WHERE NoApproval = '" + NoApproval + "'");


                    //floor plan
                    string Peta = Db.SingleString("SELECT Peta "
                        + " FROM ISC064_MARKETINGJUAL..MS_UNIT AS MS_UNIT INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK ON MS_UNIT.NoStock = MS_KONTRAK.NoStock "
                        + " WHERE NoKontrak = '" + NoKontrakAft + "'");
                    Func.GenerateFP(Peta);
                }

                //update detail approval dari user yang approve
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL_DETAIL SET Approve = 1,Note = '" + Note + "',TglApproval = '" + DateTime.Today + "' WHERE NoApproval = '" + NoApproval + "' AND UserID = '" + Act.UserID + "'");

                //ganti status done ke approval tsb
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_APPROVAL SET Status = 'DONE'"
                    + ",TglApproval = '" + DateTime.Today + "'"
                    + " WHERE NoApproval = '" + NoApproval + "'"
                    );
            }

            if (HakApp().Rows.Count > 0)
            {
                Db.Execute("EXEC ISC064_MARKETINGJUAL..spLogKontrakApp "
                + " '" + NoKontrak + "'"
                + ",'" + Act.UserID + "'"
                + "," + 1
                + ",'" + Convert.ToDateTime(Cf.Date(Tgl)) + "'"
                + "," + Lvl
                + "," + 4
                + ",'" + Note + "'"
                );
            }

        }
        private void SaveKontrakAgent(string NoKontrakAft, object _NoAgent, int NoUrut)
        {
            var rs = Db.Rs("Select * from ms_agent where NoAgent='" + _NoAgent + "'");
            if (rs.Rows.Count != 0)
            {
                var r = rs.Rows[0];
                Db.Execute("INSERT INTO " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK_AGENT (NoKontrak, NoUrut, SalesTipe, SalesLevel, NoAgent) VALUES ('" + NoKontrakAft + "','" + NoUrut + "','" + r["SalesTipe"] + "','" + r["SalesLevel"] + "','" + _NoAgent + "')");

                SaveKontrakAgent(NoKontrakAft, r["Atasan"], ++NoUrut);
            }
        }
    }
}