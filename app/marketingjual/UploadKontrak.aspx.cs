using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class UploadKontrak : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Act.ProjectList(project);
            //Bind();
            Js.Confirm(this, "Lanjutkan proses upload data kontrak?");
            feed.Text = "";
        }

        //private void Bind()
        //{
        //    System.Text.StringBuilder x = new System.Text.StringBuilder();

        //    DataTable rs = Db.Rs("SELECT Jenis FROM REF_JENIS ORDER BY SN");
        //    for (int i = 0; i < rs.Rows.Count; i++)
        //    {
        //        if (x.Length != 0) x.Append("/");
        //        x.Append(rs.Rows[i][0].ToString());
        //    }

        //    TableCell c = rule.Rows[24].Cells[4];
        //    c.Text = x.ToString();

        //    System.Text.StringBuilder x2 = new System.Text.StringBuilder();
        //    DataTable rs2 = Db.Rs("SELECT Lokasi FROM REF_LOKASI ORDER BY SN");
        //    for (int i = 0; i < rs2.Rows.Count; i++)
        //    {
        //        if (x2.Length != 0) x2.Append("/");
        //        x2.Append(rs2.Rows[i][0].ToString());
        //    }

        //    c = rule.Rows[25].Cells[4];
        //    c.Text = x2.ToString();
        //}

        protected void upload_Click(object sender, System.EventArgs e)
        {
            if (!file.PostedFile.FileName.EndsWith(".xls"))
            {
                Js.Alert(
                    this
                    , "Proses Upload Gagal.\\n"
                    + "File yang boleh di-upload adalah file dengan extension .xls saja."
                    , ""
                    );
            }
            else
            {
                string path = Request.PhysicalApplicationPath
                    + "Template\\MigrateKontrak_" + Session.SessionID + ".xls";

                Dfc.UploadFile(".xls", path, file);

                Cek(path);

                //Hapus file sementara tersebut dari hard-disk server
                Dfc.DeleteFile(path);

            }
        }

        private void Cek(string path)
        {
            string strSql = "SELECT * FROM [Kontrak$]";
            DataTable rs = new DataTable();

            try
            {
                rs = Db.xls(strSql, path);
            }
            catch { }

            if (Rpt.ValidateXls(rs, rule, gagal))
                Save(path);
        }

        private void Save(string path)
        {
            int total = 0;

            string strSql = "SELECT * FROM [Kontrak$]";
            DataTable rs = Db.xls(strSql, path);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                if (Save(rs, i))
                    total++;
            }

            feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Berhasil  : " + total + " baris data";
        }

        //private string AutoID()
        //{
        //    int c = Db.SingleInteger("SELECT COUNT(NoKontrak) FROM MS_KONTRAK");
        //    string x = "";

        //    bool hasfound = false;
        //    while (!hasfound)
        //    {
        //        if (!Response.IsClientConnected) break;

        //        c++;
        //        x = c.ToString().PadLeft(7, '0');

        //        if (isUnique(x)) hasfound = true;
        //    }

        //    return x;
        //}

        //private bool isUnique(string NoKontrak)
        //{
        //    bool x = true;

        //    int c = Db.SingleInteger(
        //        "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

        //    if (c != 0)
        //        x = false;

        //    return x;
        //}

        private bool Save(DataTable rs, int i)
        {

            string NoKontrak = Cf.Str(rs.Rows[i][0]);
            string Status = Cf.Str(rs.Rows[i][1]);
            string NamaCust = Cf.Str(rs.Rows[i][2]);
            string NoKTP = Cf.Str(rs.Rows[i][3]); //if (NoKTP == "-") NoKTP = "";
            string KTPAlamat = Cf.Str(rs.Rows[i][4]); //if (KTPAlamat == "-") KTPAlamat = "";
            string TLahir = Cf.Str(rs.Rows[i][5]); //if (TLahir == "-") TLahir = "";
            DateTime TglLahir = Convert.ToDateTime(rs.Rows[i][6]);
            string StatusM = Cf.Str(rs.Rows[i][7]);
            string Agama = Cf.Str(rs.Rows[i][8]);
            string NoTelp = Cf.Str(rs.Rows[i][9]); //if (NoTelp == "-") NoTelp = "";
            string NoFax = Cf.Str(rs.Rows[i][10]); //sif (NoFax == "-") NoFax = "";
            string NPWP = Cf.Str(rs.Rows[i][11]); //if (NPWP == "-") NPWP = "";
            string AlamatNPWP1 = Cf.Str(rs.Rows[i][12]); //if (AlamatNPWP1 == "-") AlamatNPWP1 = "";
            string AlamatSurat1 = Cf.Str(rs.Rows[i][13]); //if (AlamatSurat1 == "-") AlamatSurat1 = "";
            string Agent = Cf.Str(rs.Rows[i][14]);

            DateTime TglKontrak = Convert.ToDateTime(rs.Rows[i][15]);
            string NoUnit = Cf.Str(rs.Rows[i][16]);
            decimal Gross = Convert.ToDecimal(rs.Rows[i][17]);
            decimal DiskonRupiah = Convert.ToDecimal(rs.Rows[i][18]);
            decimal NilaiKontrak = Convert.ToDecimal(rs.Rows[i][19]);
            string SkemaBayar = Cf.Str(rs.Rows[i][20]);
            string CaraBayar = Cf.Str(rs.Rows[i][21]);
            string JenisPPN = Cf.Str(rs.Rows[i][22]);
            decimal NilaiPPN = Convert.ToDecimal(rs.Rows[i][23]);
            decimal NilaiDPP = Convert.ToDecimal(rs.Rows[i][24]);
            //string NoVA = Cf.Str(rs.Rows[i][25]); if (NoVA == "-") NoVA = "";
            //DateTime TglBAST = Convert.ToDateTime(rs.Rows[i][26]);
            //string NoBAST = Cf.Str(rs.Rows[i][27]); //if (NoBAST == "-") NoBAST = "";
            //DateTime TargetBAST = Convert.ToDateTime(rs.Rows[i][28]);
            //DateTime TglPPJB = Convert.ToDateTime(rs.Rows[i][29]);
            //string NoPPJB = Cf.Str(rs.Rows[i][30]); //if (NoPPJB == "-") NoPPJB = "";
            //DateTime TglAJB = Convert.ToDateTime(rs.Rows[i][31]);
            //string NoAJB = Cf.Str(rs.Rows[i][32]); //if (NoAJB == "-") NoAJB = "";
            //DateTime TglBatal = Convert.ToDateTime(rs.Rows[i][25]);
            string AlasanBatal = Cf.Str(rs.Rows[i][26]); if (AlasanBatal == "-") AlasanBatal = ""; //if (AlasanBatal == "NULL")  AlasanBatal = ""; 
            //decimal BatalMasuk = Convert.ToDecimal(rs.Rows[i][35]);
            //decimal NilaiKlaim = Convert.ToDecimal(rs.Rows[i][36]);
            //decimal NilaiPulang = Convert.ToDecimal(rs.Rows[i][37]);
            //string AccBatal = Cf.Str(rs.Rows[i][38]); //if (AccBatal == "-") AccBatal = "";
            int NoAgent = 0;

            NoAgent = Db.SingleInteger("SELECT ISNULL(NoAgent,0) FROM MS_AGENT WHERE Nama = '" + Agent + "'");
            bool x = true;
            string Aksi = "";

            if (NoAgent != 0 && Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'") == 0)
            {

                Db.Execute("EXEC spCustomerDaftar"
                + " '" + NamaCust + "'"
                + ",''"
                + ",'" + NoTelp + "'"
                + ",''"
                + ",''"
                + ",'" + NoFax + "'"
                + ",''"
                + ",'" + NoKTP + "'"
                + ",'" + KTPAlamat + "'"
                + ",''"// + KTP2 + "'"
                + ",''"// + KTP3 + "'"
                + ",''"// + KTP4 + "'"
                + ",'" + AlamatSurat1 + "'"
                + ",''"// + Alamat2 + "'"
                + ",''"// + Alamat3 + "'"
                + ",''"
                + ",''"
                + ",''"
                + ",'" + Agama + "'"
                + ",'" + Convert.ToDateTime(TglLahir) + "'"
                + ",''"
                + ",''"
                + ",''"
                + ",0"
                + ",''"
                + ",''"
                + ",''"
                + ",''"
                + ",''"
                + ",''"
                + ",''"
                + ",'" + NPWP + "'"
                );

                string NoCust = Db.SingleInteger(
                "SELECT TOP 1 NoCustomer FROM MS_CUSTOMER ORDER BY NoCustomer DESC")
                .ToString().PadLeft(5, '0');

                Db.Execute("UPDATE MS_CUSTOMER SET "
                + " AgentInput = '" + Act.UserID + "'"
                + ",TempatLahir = '" + TLahir + "'"
                + ",TglLahir = '" + TglLahir + "'"
                + ",Marital = '" + StatusM + "'"
                + ",NPWPAlamat1 = '" + AlamatNPWP1 + "'"
                + ",Project = '" + project.SelectedValue + "'"
                + " WHERE NoCustomer = '" + NoCust + "'");

                DataTable rs1 = Db.Rs("SELECT "
                + " NoCustomer AS [No. Customer]"
                + ",TipeCs AS [Tipe]"
                + ",Nama AS [Nama Lengkap]"
                + ",NamaBisnis AS [Nama Bisnis]"
                + ",JenisBisnis AS [Jenis Bisnis]"
                + ",MerekBisnis AS [Merek Bisnis]"
                + ",Agama AS [Agama]"
                + ",CONVERT(varchar, TglLahir, 106) AS [Tanggal Lahir]"
                + ",NoTelp AS [No. Telepon]"
                + ",NoHp AS [No. HP]"
                + ",NoKantor AS [No. Telepon Kantor]"
                + ",NoFax AS [No. Fax]"
                + ",Email AS [Alamat Email]"
                + ",Alamat1 AS [Alamat Surat Menyurat 1]"
                + ",Alamat2 AS [Alamat Surat Menyurat 2]"
                + ",Alamat3 AS [Alamat Surat Menyurat 3]"
                + ",Kantor1 AS [Alamat Kantor 1]"
                + ",Kantor2 AS [Alamat Kantor 2]"
                + ",Kantor3 AS [Alamat Kantor 3]"
                + ",NoKTP AS [No. KTP]"
                + ",KTP1 AS [KTP Alamat]"
                + ",KTP2 AS [KTP RT/RW]"
                + ",KTP3 AS [KTP Kecamatan]"
                + ",KTP4 AS [KTP Kotamadya]"
                + ",UnitLama AS [Unit Lama]"
                + ",LuasLama AS [Luas Unit Lama]"
                + ",TokoLama AS [Nama Toko Lama]"
                + ",ZoningLama AS [Zoning Lama]"
                + ",GedungLama AS [Gedung Lama]"
                + ",TeleponLama AS [Telepon Lama]"
                + ",AkteLama AS [Akte Lama]"
                + ",Salutation"
                + ",AgentInput AS [Sales Account]"
                + ",SumberData AS [Sumber Data]"
                + ",TempatLahir AS [Tempat Lahir]"
                + ",NPWP"
                //+ ",Perusahaan"
                //+ ",SIUP AS [No. SIUP/Akte]"
                //+ ",JenisUsaha AS [Jenis Usaha]"
                //+ ",FaxKantor AS [Fax Kantor]"
                //+ ",JenisKelamin AS [Jenis Kelamin]"
                //+ ",SumberPendapatan AS [Sumber Pendapatan]"
                + ",Marital AS [Status Marital]"
                + ",Project"
                + " FROM MS_CUSTOMER"
                + " WHERE NoCustomer = '" + NoCust + "'"
                );

                Db.Execute("EXEC spLogCustomer"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs1) + "'"
                    + ",'" + NoCust + "'"
                    );

                string NoStock = Db.SingleString("SELECT NoStock FROM MS_UNIT WHERE NoUnit = '" + NoUnit + "'");



                Db.Execute("EXEC spKontrakDaftar3"
                    + " '" + NoKontrak + "'"
                    + ",'" + NoStock + "'"
                    + ",'" + TglKontrak + "'"
                    + ",'" + SkemaBayar + "'"
                    + ",'" + CaraBayar + "'"
                    + ",'" + Gross + "'"
                    + ",'" + NoCust + "'"
                    + ",'" + NoAgent + "'"
                    + ",'" + Status + "'"
                    + ",'" + DiskonRupiah + "'"
                    + ",'" + NilaiKontrak + "'"
                    + ",'" + JenisPPN + "'"
                    + ",'" + NilaiPPN + "'"
                    + ",'" + NilaiDPP + "'"
                    );

                string strTipeSkema = CaraBayar;
                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + project.SelectedValue + "'");
                string Pers = Db.SingleString("SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = (SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + project.SelectedValue + "')");
                string NamaPers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Pers + "'");

                Db.Execute("UPDATE MS_KONTRAK SET Project = '" + project.SelectedValue + "'"
                        + ", NamaProject = '"+NamaProject+"'"
                        + ", Pers = '" + Pers + "'"
                        + ", NamaPers = '" + NamaPers + "'"
                        );
                //int KPR = 0;
                //if (strTipeSkema == "KPA")
                //{
                //    KPR = 1;
                //}
                //else
                //{
                //    KPR = 0;
                //}

                ////BAST
                //if (NoBAST != "")
                //{
                //    Db.Execute("UPDATE MS_KONTRAK SET"
                //        + " TglST = ''"// + TglBAST + "'"
                //        + ",NoST = ''"// + NoBAST + "'"
                //        + ",ST = 'D'"
                //        + " WHERE NoKontrak = '" + NoKontrak + "'"
                //        );
                //}

                ////PPJB
                //if (NoPPJB != "")
                //{
                //    Db.Execute("UPDATE MS_KONTRAK SET"
                //        + " TglPPJB = ''"// + TglPPJB + "'"
                //        + ",NoPPJB = ''"// + NoPPJB + "'"
                //        + ",PPJB = 'D'"
                //        + " WHERE NoKontrak = '" + NoKontrak + "'"
                //        );
                //}

                ////AJB
                //if (NoAJB != "")
                //{
                //    Db.Execute("UPDATE MS_KONTRAK SET"
                //        + " TglAJB = ''"// + TglAJB + "'"
                //        + ",NoAJB = ''"// + NoAJB+ "'"
                //        + ",AJB = 'D'"
                //        + " WHERE NoKontrak = '" + NoKontrak + "'"
                //        );
                //}

                if (rs.Rows[i][25].ToString() != "NULL")
                {
                    //JIKA Batal
                    if (Status == "B")
                    {
                        DateTime TglBat = Convert.ToDateTime(rs.Rows[i][25]);
                        Db.Execute("UPDATE MS_KONTRAK SET"
                            + " TglBatal = '" + TglBat + "'"
                            + ",AlasanBatal = '" + AlasanBatal + "'"
                            // + ",TotalLunasBatal = " + Convert.ToDecimal(BatalMasuk)
                            //+ ",NilaiKlaim = " + Convert.ToDecimal(NilaiKlaim)
                            //+ ",NilaiPulang = " + Convert.ToDecimal(NilaiPulang)
                            //+ ",AccBatal = '" + AccBatal + "'"
                            + " WHERE NoKontrak = '" + NoKontrak + "'"
                            );
                    }
                }
                if (rs.Rows[i][25].ToString() == "NULL")
                {
                    Db.Execute("UPDATE MS_KONTRAK SET TglBatal = NULL");
                }

                //Manual update
                //string sSQL = "UPDATE MS_KONTRAK"
                //    + " SET"
                //    + "  JenisKPR = " + KPR
                //    + " ,UserID = '" + Act.UserID + "'"
                //    + " WHERE NoKontrak = '" + NoKontrak + "'"
                //    ;
                //Db.Execute(sSQL);

                //Db.Execute("UPDATE MIGRATE_KONTRAK SET Approved = 1 WHERE NoKontrak = '" + mnokontrak.Text + "'");

                DataTable rs2 = Db.Rs("SELECT "
                        + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                        + ",MS_KONTRAK.Status AS [Status]"
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
                        + ",MS_KONTRAK.NilaiPPN AS [Nilai PPN]"
                        + ",MS_KONTRAK.Skema"
                        + ",MS_KONTRAK.CaraBayar"
                        + ",CONVERT(varchar,MS_KONTRAK.TargetST,106) AS [Jadwal Serah Terima]"
                        + ", MS_KONTRAK.JenisPPN AS [PPN Ditanggung]"
                        + ", CASE MS_KONTRAK.JenisKPR"
                        + "		WHEN 0 THEN 'KPR'"
                        + "		WHEN 1 THEN 'NON-KPR'"
                        + "	END AS [Jenis KPR]"
                        + ",MS_KONTRAK.NoVA AS [No. VA]"
                        + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                        + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );

                string Ket = Cf.LogCapture(rs2);

                Db.Execute("EXEC spLogKontrak"
                        + " 'DAFTAR'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoKontrak + "'"
                        );
                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            }
            else x = false;

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

    }
}
