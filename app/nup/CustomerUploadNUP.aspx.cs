using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class CustomerUploadNUP : System.Web.UI.Page
    {
        int Berhasil = 0;
        int Gagals = 0;
        string BarisGagal = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Bind();
            Js.Confirm(this, "Lanjutkan proses upload data customer?");
            feedaa.Text = "";
        }
        private void Bind()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT Nama FROM MS_AGENT ORDER BY NoAgent");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("/");
                x.Append(rs.Rows[i][0].ToString());
            }

            TableCell c = rule.Rows[4].Cells[4];
            c.Text = x.ToString();


            System.Text.StringBuilder x2 = new System.Text.StringBuilder();

            DataTable rs2 = Db.Rs("SELECT Nama FROM MS_PROPERTI ORDER BY ID");
            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                if (x2.Length != 0) x2.Append("/");
                x2.Append(rs2.Rows[i][0].ToString());
            }
            
            TableCell c2 = rule.Rows[27].Cells[4];
            c2.Text = x2.ToString();
        }
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
                    + "Template\\Customer_" + Session.SessionID + ".xls";

                Dfc.UploadFile(".xls", path, file);

                Cek(path);

                //Hapus file sementara tersebut dari hard-disk server
                Dfc.DeleteFile(path);

            }
        }

        private void Cek(string path)
        {
            string strSql = "SELECT * FROM [Customer$]";
            DataTable rs = new DataTable();

            try
            {
                rs = Db.xls(strSql, path);
            }
            catch { }

            if (Rpt.ValidateXls(rs, rule, gagal))
                Save(path);
        }
        private int AutoID()
        {
            int NoCustomer = Db.SingleInteger("Select ISNULL(MAX(NoCustomer),0) + 1 FROM MS_CUSTOMER");

            return NoCustomer;
        }
        private void Save(string path)
        {
            int total = 0;

            string strSql = "SELECT * FROM [Customer$]";
            DataTable rs = Db.xls(strSql, path);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;
                //Response.Write(i);
                if (Save(rs, i))
                    total++;
            }

            feedaa.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Berhasil  : " + Berhasil + " Baris Data";

            feed2aa.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Gagal, No. NUP Sudah Ada  : " + Gagals + " Baris Data, " + BarisGagal;
        }

        private bool Save(DataTable rs, int i)
        {
            string NoNUP = Cf.Str(rs.Rows[i][0].ToString().PadLeft(4, '0'));
            string SumberData = Cf.Str(rs.Rows[i][1]);
            string TipeCs = Cf.Str(rs.Rows[i][2]);
            string NamaAgent = Cf.Str(rs.Rows[i][3]);
            string Nama = Cf.Str(rs.Rows[i][4]);
            string NoTelp = Cf.Str(rs.Rows[i][5]);
            string NoHp = Cf.Str(rs.Rows[i][6]);
            string Email = Cf.Str(rs.Rows[i][7]);

            string NoKTP = Cf.Str(rs.Rows[i][8]);
            string KTP1 = Cf.Str(rs.Rows[i][9]);
            string KTP2 = Cf.Str(rs.Rows[i][10]);
            string KTP3 = Cf.Str(rs.Rows[i][11]);
            string KTP4 = Cf.Str(rs.Rows[i][12]);
            string Alamat1 = Cf.Str(rs.Rows[i][13]);
            string Alamat2 = Cf.Str(rs.Rows[i][14]);
            string Alamat3 = Cf.Str(rs.Rows[i][15]);
            string Alamat4 = Cf.Str(rs.Rows[i][16]);
            string rekB = Cf.Str(rs.Rows[i][17]);
            string rekC = Cf.Str(rs.Rows[i][18]);
            string rekN = Cf.Str(rs.Rows[i][19]);
            string rekNam = Cf.Str(rs.Rows[i][20]);
            string NPWP = Cf.Str(rs.Rows[i][21]);

            DateTime TglTransferNUP = Convert.ToDateTime(rs.Rows[i][22]);
            string RekeningnUP = Cf.Str(rs.Rows[i][23]);
            string CaraBayarNUP = Cf.Str(rs.Rows[i][24]);

            decimal NilaiNUP = Convert.ToDecimal(rs.Rows[i][25]);
            string JenisProperti = Cf.Str(rs.Rows[i][26]);
            DateTime TglLahir = Convert.ToDateTime(rs.Rows[i][27]);
            //Jika data NA maka diisi dengan string kosong saja di dalam database
            //Check Jika NoNUP Ada Maka Tidak Akan di Upload Data Customernya
            int AdaNUP = Db.SingleInteger("Select Count(*) From MS_NUP WHERE NoNup = " + NoNUP + " AND TIPE = '" + JenisProperti + "'");

            if (AdaNUP == 0)
            {
                if (NoTelp == "0") NoTelp = "";
                if (NoHp == "0") NoHp = "";
                if (Email == "NA") Email = "";
                if (NoKTP == "NA") NoKTP = "";
                if (KTP1 == "NA") KTP1 = "";
                if (KTP2 == "NA") KTP2 = "";
                if (KTP3 == "NA") KTP3 = "";
                if (KTP4 == "NA") KTP4 = "";
                if (Alamat1 == "NA") Alamat1 = "";
                if (Alamat2 == "NA") Alamat2 = "";
                if (Alamat3 == "NA") Alamat3 = "";
                if (Alamat4 == "NA") Alamat4 = "";
                if (SumberData == "NA") SumberData = "LAINNYA";

                int NoAgent = Db.SingleInteger(
                    "SELECT TOP 1 NoAgent FROM MS_AGENT WHERE Nama = '" + NamaAgent + "'");
                int NoCustomer = AutoID();

                Db.Execute("EXEC spCustomerDaftarNUPUpload"
                    + " '" + NoNUP + "'"
                    + ", '" + NoCustomer + "'"
                    + "," + NoAgent
                    + ",'" + Nama + "'"
                    + ",'" + NoHp + "'"
                    + ",'" + NoTelp + "'"
                    + ",'" + Email + "'"
                    + ",'" + TglLahir + "'"
                    + ",'" + NoKTP + "'"
                    + ",'" + NPWP + "'"
                    + ",'" + KTP1 + "'"
                    + ",'" + KTP2 + "'"
                    + ",'" + KTP3 + "'"
                    + ",'" + KTP4 + "'"
                    + ",'" + Alamat1 + "'"
                    + ",'" + Alamat2 + "'"
                    + ",'" + Alamat3 + "'"
                    + ",'" + Alamat4 + "'"
                    + ",'" + rekNam + "'"
                    + ",'" + rekB + "'"
                    + ",'" + rekC + "'"
                    + ",'" + rekN + "'"
                    + ",'" + JenisProperti + "'"
                    );


                DateTime TglDaftar = DateTime.Now.Date;

                Db.Execute("UPDATE MS_NUP SET "
                    + " TglDaftar = '" + TglDaftar + "'"
                    + ", NoCustomerBfr = '" + NoCustomer + "'"
                    + ", NamaBfr = '" + Nama + "'"
                    + ", UserInputID = '" + Act.UserID + "'"
                    + ", Tipe = '" + JenisProperti + "'"
                    + ", UserInputNama = '" + Db.SingleString("SELECT Nama FROM ISC064_SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'") + "'"
                    + " WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + JenisProperti + "'");

                //Logfile
                DataTable rsLog = Db.Rs("SELECT "
                    + " MS_NUP.NoNUP AS [NUP]"
                    + ",MS_CUSTOMER.Nama AS [Customer]"
                    + ",(select top 1 Nama from ms_agent where MS_AGENT.NoAgent = MS_NUP.NoAgent) AS [Nama Agent]"
                    //+ ",MS_CUSTOMER.Nama AS [Customer]"
                    + ",MS_NUP.UserInputNama AS [Diinput Oleh]"
                    + " FROM MS_NUP INNER JOIN MS_CUSTOMER"
                    + " ON MS_NUP.NoCustomer = MS_CUSTOMER.NoCustomer"
                    + " INNER JOIN MS_AGENT ON MS_NUP.NoAgent = MS_AGENT.NoAgent"
                    + " WHERE MS_NUP.NoNUP = '" + NoNUP + "' AND MS_NUP.Tipe = '" + JenisProperti + "'");

                Db.Execute("EXEC spLogNUP"
                + " 'REGIS'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rsLog) + "'"
                + ",'" + NoNUP + "'"
                + ",'" + JenisProperti + "'"
                );

                DataTable log = Db.Rs("SELECT "
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
                 + ",Alamat4 AS [Alamat Surat Menyurat 4]"
                 + ",Kantor1 AS [Alamat Kantor 1]"
                 + ",Kantor2 AS [Alamat Kantor 2]"
                 + ",Kantor3 AS [Alamat Kantor 3]"
                 + ",Kantor4 AS [Alamat Kantor 4]"
                 + ",NoKTP AS [No. KTP]"
                 + ",KTP1 AS [KTP Alamat]"
                 + ",KTP2 AS [KTP RT/RW]"
                 + ",KTP3 AS [KTP Kecamatan]"
                 + ",KTP4 AS [KTP Kotamadya]"
                 + ",NoNUP AS [No.NUP]"
                 + ",NilaiNUP AS [Nilai NUP]"
                 + " FROM MS_CUSTOMER"
                 + " WHERE NoCustomer = " + NoCustomer
                 );

                Db.Execute("EXEC spLogCustomer"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(log) + "'"
                    + ",'" + NoCustomer.ToString().PadLeft(5, '0') + "'"
                    );

                string cabar = Cf.Str(CaraBayarNUP);
                string rekpers = Cf.Str(RekeningnUP);
                string ketbayar = Cf.Str("PEMBAYARAN NUP PERTAMA");

                decimal totalnup = (decimal)0;

                decimal valuenup = 0;

                valuenup = Convert.ToDecimal(NilaiNUP);

                string NoTTS = "";

                Db.Execute("EXEC spInsertNUPPelunasan"
                + " '" + NoNUP + "'"
                + ",'" + Cf.Tgl112(DateTime.Now) + "'"
                + "," + valuenup
                + ",'" + cabar + "'"
                + ",'" + ketbayar + "'"
                + ",'" + NoTTS.ToString() + "'"
                + "," + 1
                + ",'" + rekpers + "'"
                + ",'" + JenisProperti + "'"
                ); //Yang belakang diset 1, karena ini adalah pembayaran pertama NUP

                //kode no tts
                NoTTS = SaveTTS(NoNUP, cabar, rekpers, ketbayar, TglTransferNUP, Db.SingleInteger("SELECT NoCustomer FROM MS_NUP WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + JenisProperti + "'"), NilaiNUP, JenisProperti);
                int roman = Convert.ToInt32(Cf.Bulan(DateTime.Now));
                string TTS = NoTTS.ToString();

                Db.Execute("UPDATE MS_NUP_PELUNASAN SET "
                    + " UserInputID = '" + Act.UserID + "'"
                    + ", UserInputNama = '" + Db.SingleString("SELECT Nama FROM ISC064_SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'") + "'"
                    + ", NoTTSNUP = '" + TTS + "'"
                    + ", NoTTS = '" + TTS + "'"
                    + " WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + JenisProperti + "'");

                if (JenisProperti == "RUSUNAMI")
                    totalnup = 2000000;
                else
                    totalnup = 6000000;
                if (valuenup == totalnup)
                {
                    Db.Execute("UPDATE MS_NUP_PELUNASAN SET FlagUntukBayar=1 WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + JenisProperti + "'");
                }

                Db.Execute("UPDATE MS_NUP SET NilaiBayar = '" + valuenup + "' WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + JenisProperti + "'");

                //Logfile
                rsLog = Db.Rs("SELECT "
                    + " A.NoNUP AS [NUP]"
                    + ",C.Nama AS [Customer]"
                    + ",B.CaraBayar AS [Cara Bayar]"
                    + ",E.Bank+'/'+E.Rekening+'/'+E.AtasNama AS [Transfer ke]"
                    + ",B.NilaiBayar AS [Nilai Bayar]"
                    + ",B.Keterangan AS [Keterangan]"
                    + ",B.UserInputNama AS [Diinput Oleh]"
                    + " FROM MS_NUP A "
                    + " INNER JOIN MS_NUP_PELUNASAN B ON A.NoNUP = B.NoNUP"
                    + " INNER JOIN MS_CUSTOMER C ON A.NoCustomer = C.NoCustomer"
                    + " INNER JOIN ISC064_FINANCEAR..REF_ACC E ON B.RekBank = E.Acc"
                    + " WHERE A.NoNUP = '" + NoNUP + "' AND A.Tipe = '" + JenisProperti + "'");

                string KetLog = Cf.LogCapture(rsLog)
                    + "<br>***PEMBAYARAN NUP PERTAMA:<br>";

                Db.Execute("EXEC spLogNUP"
                + " 'PY-NUP'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetLog + "'"
                + ",'" + NoNUP + "'"
                + ",'" + JenisProperti + "'"
                );

                Berhasil++;
            }
            else
            {
                Gagals++;
                BarisGagal += "Baris Ke-" + (i + 1).ToString() + ", ";
            }


            return true;
        }
        private string SaveTTS(string NoNUP, string cabar, string rekpers, string ketbayar, DateTime TglTransfer, int NoCustomer, decimal nilai, string Jenis)
        {
            int roman = Convert.ToInt32(Cf.Bulan(DateTime.Now));

            DateTime TglTTS = TglTransfer;
            string Customer = Cf.Str(Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer));
            string CaraBayar = cabar;

            Db.Execute("EXEC ISC064_FINANCEAR..spTTSRegistrasi"
                + " '" + TglTTS + "'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'JUAL'"
                + ",''" //Ref / NoKontrak
                + ",''" //Unit
                + ",'" + Customer + "'"
                + ",'" + CaraBayar + "'"
                + ",'PEMBAYARAN PLP'"
                );

            int NoTTS = Db.SingleInteger("SELECT TOP 1 NoTTS FROM ISC064_FINANCEAR..MS_TTS ORDER BY NoTTS DESC");

            //di hard code untuk projectnya
            string NoTTS2 = Numerator.TTS(TglTTS.Month, TglTTS.Year, "SVS");

            //update nilai bayar di TTSnya
            decimal nBayar = nilai;
            Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET Total=" + nBayar
                + ", Acc = '" + rekpers + "'"
                + ", NoNUP = '" + NoNUP + "'"
                + ", Jenis = '" + Jenis + "'"
                + ", NoTTS2 = '" + NoTTS2 + "'"
                //+ ", NoKK = '" + Cf.Str(noKK) + "'"
                + ", Catatan = '" + Cf.Str(ketbayar) + "'"
                + " WHERE NoTTS='" + NoTTS + "'");

            DataTable rs = Db.Rs("SELECT "
                + " CONVERT(varchar, TglTTS, 106) AS [Tanggal]"
                + ",Tipe"
                + ",Ref AS [Ref.]"
                + ",Unit"
                + ",Customer"
                + ",CaraBayar AS [Cara Bayar]"
                + ",Ket AS [Keterangan]"
                + ",Total"
                + ",NoBG AS [No. BG]"
                + ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
                + " FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

            string KetLog =
                "***PEMBAYARAN NUP PERTAMA: " + NoNUP + " Tipe = " + Jenis + "<br/><br/>"
                + Cf.LogCapture(rs);

            Db.Execute("EXEC ISC064_FINANCEAR..spLogTTS"
                + " 'REGIS'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetLog + "'"
                + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                );

            return NoTTS.ToString();
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
