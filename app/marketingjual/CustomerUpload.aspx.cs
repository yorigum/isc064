using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class CustomerUpload : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Bind();
            }
            Js.Confirm(this, "Lanjutkan proses upload data customer?");
            feed.Text = "";
        }
        private void Bind()
        {
            //System.Text.StringBuilder x = new System.Text.StringBuilder();

            //DataTable rs = Db.Rs("SELECT Tipe FROM REF_AGENT_TIPE WHERE Project ='" + project.SelectedValue + "' ORDER BY ID");
            //for (int i = 0; i < rs.Rows.Count; i++)
            //{
            //    if (x.Length != 0) x.Append("/");
            //    x.Append(rs.Rows[i][0].ToString());
            //}

            //TableCell c = rule.Rows[3].Cells[4];
            //c.Text = x.ToString();
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

        private void Save(string path)
        {
            int total = 0;

            string strSql = "SELECT * FROM [Customer$]";
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

        private bool Save(DataTable rs, int i)
        {
            string TipeCs = Cf.Str(rs.Rows[i][0]);
            string Nama = Cf.Str(rs.Rows[i][1]);
            string SumberData = Cf.Str(rs.Rows[i][2]);
            string Agama = Cf.Str(rs.Rows[i][3]);
            string TempatLahir = Cf.Str(rs.Rows[i][4]);
            DateTime TglLahir = Convert.ToDateTime(rs.Rows[i][5]);
            string status = Cf.Str(rs.Rows[i][6]);
            string NoTelp = Cf.Str(rs.Rows[i][7]);
            string NoHp = Cf.Str(rs.Rows[i][8]);
            string NoHp2 = Cf.Str(rs.Rows[i][9]);
            string NoFax = Cf.Str(rs.Rows[i][10]);
            string Email = Cf.Str(rs.Rows[i][11]);
            string NoKTP = Cf.Str(rs.Rows[i][12]);
            string KTP1 = Cf.Str(rs.Rows[i][13]);
            string KTP2 = Cf.Str(rs.Rows[i][14]);
            string KTP3 = Cf.Str(rs.Rows[i][15]);
            string KTP4 = Cf.Str(rs.Rows[i][16]);
            string KTP5 = Cf.Str(rs.Rows[i][17]);
            string KodePos = Cf.Str(rs.Rows[i][18]);
            DateTime TglKTP = Convert.ToDateTime(rs.Rows[i][19]);
            string Alamat1 = Cf.Str(rs.Rows[i][20]);
            string Alamat2 = Cf.Str(rs.Rows[i][21]);
            string Alamat3 = Cf.Str(rs.Rows[i][22]);
            string Alamat4 = Cf.Str(rs.Rows[i][23]);
            string Alamat5 = Cf.Str(rs.Rows[i][24]);
            string pekerjaan = Cf.Str(rs.Rows[i][25]);
            string NoKantor = Cf.Str(rs.Rows[i][26]);
            string Kantor1 = Cf.Str(rs.Rows[i][27]);
            string Kantor2 = Cf.Str(rs.Rows[i][28]);
            string Kantor3 = Cf.Str(rs.Rows[i][29]);
            string Kantor4 = Cf.Str(rs.Rows[i][30]);
            string Kantor5 = Cf.Str(rs.Rows[i][31]);
            string NamaNPWP = Cf.Str(rs.Rows[i][32]);
            string NPWP = Cf.Str(rs.Rows[i][33]);
            string NPWPAlamat = Cf.Str(rs.Rows[i][34]);
            string NPWPAlamat2 = Cf.Str(rs.Rows[i][35]);
            string NPWPAlamat3 = Cf.Str(rs.Rows[i][36]);
            string NPWPAlamat4 = Cf.Str(rs.Rows[i][37]);
            string NPWPAlamat5 = Cf.Str(rs.Rows[i][38]);
            string wn = Cf.Str(rs.Rows[i][39]);
            string NamaKorporasi = Cf.Str(rs.Rows[i][40]);
            string JabatanKorporasi = Cf.Str(rs.Rows[i][41]);
            string SKKorporasi = Cf.Str(rs.Rows[i][42]);
            string BentukKorporasi = Cf.Str(rs.Rows[i][43]);
            string NamaRelasi = Cf.Str(rs.Rows[i][44]);
            string HubunganRelasi = Cf.Str(rs.Rows[i][45]);
            string HPRelasi = Cf.Str(rs.Rows[i][46]);
            string EmailRelasi = Cf.Str(rs.Rows[i][47]);

            //Jika data NA maka diisi dengan string kosong saja di dalam database
            if (TempatLahir == "NA") TempatLahir = "";
            if (NoTelp == "NA") NoTelp = "";
            if (NoHp == "NA") NoHp = "";
            if (NoHp2 == "NA") NoHp2 = "";
            if (NoFax == "NA") NoFax = "";
            if (Email == "NA") Email = "";
            if (NoKTP == "NA") NoKTP = "";
            if (KTP1 == "NA") KTP1 = "";
            if (KTP2 == "NA") KTP2 = "";
            if (KTP3 == "NA") KTP3 = "";
            if (KTP4 == "NA") KTP4 = "";
            if (KTP5 == "NA") KTP5 = "";
            if (KodePos == "NA") KodePos = "";
            if (Alamat1 == "NA") Alamat1 = "";
            if (Alamat2 == "NA") Alamat2 = "";
            if (Alamat3 == "NA") Alamat3 = "";
            if (Alamat4 == "NA") Alamat4 = "";
            if (Alamat5 == "NA") Alamat5 = "";
            if (pekerjaan == "NA") pekerjaan = "";
            if (NoKantor == "NA") NoKantor = "";
            if (Kantor1 == "NA") Kantor1 = "";
            if (Kantor2 == "NA") Kantor2 = "";
            if (Kantor3 == "NA") Kantor3 = "";
            if (Kantor4 == "NA") Kantor4 = "";
            if (Kantor5 == "NA") Kantor5 = "";
            if (NamaNPWP == "NA") NamaNPWP = "";
            if (NPWP == "NA") NPWP = "";
            if (NPWPAlamat == "NA") NPWPAlamat = "";
            if (NPWPAlamat2 == "NA") NPWPAlamat2 = "";
            if (NPWPAlamat3 == "NA") NPWPAlamat3 = "";
            if (NPWPAlamat4 == "NA") NPWPAlamat4 = "";
            if (NPWPAlamat5 == "NA") NPWPAlamat5 = "";
            if (NamaKorporasi == "NA") NamaKorporasi = "";
            if (JabatanKorporasi == "NA") JabatanKorporasi = "";
            if (SKKorporasi == "NA") SKKorporasi = "";
            if (BentukKorporasi == "NA") BentukKorporasi = "";
            if (NamaRelasi == "NA") NamaRelasi = "";
            if (HPRelasi == "NA") HPRelasi = "";
            if (EmailRelasi == "NA") EmailRelasi = "";

            Db.Execute("EXEC spCustomerDaftar "
                + " '" + Nama + "'"
                + ",''"
                + ",'" + NoTelp + "'"
                + ",'" + NoHp + "'"
                + ",'" + NoKantor + "'"
                + ",'" + NoFax + "'"
                + ",'" + Email + "'"
                + ",'" + NoKTP + "'"
                + ",'" + KTP1 + "'"
                + ",'" + KTP2 + "'"
                + ",'" + KTP3 + "'"
                + ",'" + KTP4 + "'"
                + ",'" + KodePos + "'"
                + ",'" + Alamat1 + "'"
                + ",'" + Alamat2 + "'"
                + ",'" + Alamat3 + "'"
                + ",'" + Kantor1 + "'"
                + ",'" + Kantor2 + "'"
                + ",'" + Kantor3 + "'"
                + ",'" + Agama + "'"
                //+ ",'" + TglLahir + "'"
                + ",''"
                + ",''"
                + ",''"
                + ",0"
                + ",''"
                + ",''"
                + ",''"
                + ",''"
                + ",''"
                + ",'" + TipeCs + "'"
                + ",''"
                + ",'" + NPWP + "'"
                + ",'" + NPWPAlamat + "'"
                + ",'" + wn + "'"
                + ",'" + status + "'"
                + ",'" + pekerjaan + "'"
                );

            //get nomor customer terbaru
            int NoCustomer = Db.SingleInteger(
                "SELECT TOP 1 NoCustomer FROM MS_CUSTOMER ORDER BY NoCustomer DESC");

            string Sedup = (TglKTP != new DateTime(1900, 01, 01)) ? "1" : "0";

            //manual update
            Db.Execute("UPDATE MS_CUSTOMER SET "
                + " AgentInput = '" + Act.UserID + "'"
                    + ",SumberData = '" + SumberData + "'"
                    + ",TglLahir = '" + TglLahir + "'"
                    + ",TempatLahir = '" + TempatLahir + "'"
                    + ",Marital = '" + status + "'"
                    + ",Kewarganegaraan = '" + wn + "'"
                    + ",Pekerjaan = '" + pekerjaan + "'"
                    + ",Alamat4 = '" + Alamat4 + "'"
                    + ",Alamat5 = '" + Alamat5 + "'"
                    + ",KTP5 = '" + KTP5 + "'"
                    + ",Kantor4 = '" + Kantor4 + "'"
                    + ",Kantor5 = '" + Kantor5 + "'"
                    + ",NoHP2 = '" + NoHp2 + "'"
                    + ",NamaNPWP = '" + NamaNPWP + "'"
                    + ",NPWPAlamat1 = '" + NPWPAlamat + "'"
                    + ",NPWPAlamat2 = '" + NPWPAlamat2 + "'"
                    + ",NPWPAlamat3 = '" + NPWPAlamat3 + "'"
                    + ",NPWPAlamat4 = '" + NPWPAlamat4 + "'"
                    + ",NPWPAlamat5 = '" + NPWPAlamat5 + "'"
                    + ",NamaKerabat = '" + NamaRelasi + "'"
                    + ",Hubungan = '" + HubunganRelasi + "'"
                    + ",NoHPKerabat = '" + HPRelasi + "'"
                    + ",EmailKerabat = '" + EmailRelasi + "'"
                    + ",Kodepos = '" + KodePos + "'"
                    + ",KTPSeumurHidup = " + Sedup
                    + ",Project = '" + project.SelectedValue + "'"
                + " WHERE NoCustomer = " + NoCustomer);

            if (TipeCs == "BADAN HUKUM")
            {
                Db.Execute("UPDATE MS_CUSTOMER SET "
                        + " PenanggungjawabKorp = '" + NamaKorporasi + "'"
                        + ",JabatanKorp = '" + JabatanKorporasi + "'"
                        + ",NoSKKorp = '" + SKKorporasi + "'"
                        + ",BentukKorp = '" + BentukKorporasi + "'"
                    + " WHERE NoCustomer = " + NoCustomer);
            }

            if (TglKTP != new DateTime(1900, 01, 01))
                Db.Execute("UPDATE MS_CUSTOMER SET TglKTP = '" + Convert.ToDateTime(rs.Rows[i][19]) + "' WHERE NoCustomer = " + NoCustomer);

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
                    + ",NoHP2 AS [No. HP2]"
                    + ",NoKantor AS [No. Telepon Kantor]"
                    + ",NoFax AS [No. Fax]"
                    + ",Email AS [Alamat Email]"
                    + ",Alamat1 AS [Alamat Surat Menyurat]"
                    + ",Alamat2 AS [Alamat Surat Menyurat RT/RW]"
                    + ",Alamat3 AS [Alamat Surat Menyurat Kelurahan]"
                    + ",Alamat4 AS [Alamat Surat Menyurat Kecamatan]"
                    + ",Alamat5 AS [Alamat Surat Menyurat Kotamadya]"
                    + ",Kantor1 AS [Alamat Kantor 1]"
                    + ",Kantor2 AS [Alamat Kantor 2]"
                    + ",Kantor3 AS [Alamat Kantor 3]"
                    + ",Kantor4 AS [Alamat Kantor 4]"
                    + ",Kantor5 AS [Alamat Kantor 5]"
                    + ",NoKTP AS [No. KTP]"
                    + ",KTP1 AS [KTP Alamat]"
                    + ",KTP2 AS [KTP RT/RW]"
                    + ",KTP5 AS [KTP Kelurahan]"
                    + ",KTP3 AS [KTP Kecamatan]"
                    + ",KTP4 AS [KTP Kotamadya]"
                    + ",Kodepos AS [Kodepos]"
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
                    + ",NamaNPWP AS [Nama NPWP]"
                    + ",NPWP"
                    + ",NPWPAlamat1 AS [Alamat NPWP ]"
                    + ",NPWPAlamat2 AS [Alamat NPWP RT/RW]"
                    + ",NPWPAlamat3 AS [Alamat NPWP Kelurahan]"
                    + ",NPWPAlamat4 AS [Alamat NPWP Kecamatan]"
                    + ",NPWPAlamat5 AS [Alamat NPWP Kotamadya]"
                    + ",Marital AS [Status Marital]"
                    + ",Kewarganegaraan AS [Kewarganegaraan]"
                    + ",Pekerjaan AS [Pekerjaan]"
                    + ",NamaKerabat AS [Nama Yang Dapat dihubungi]"
                    + ",Hubungan AS [Hubungan]"
                    + ",NoHPKerabat AS [No. HP]"
                    + ",EmailKerabat AS [Email]"
                    + ",PenanggungjawabKorp AS [Penanggungjawab Korporasi]"
                    + ",JabatanKorp AS [Jabatan Korporasi]"
                    + ",NoSKKorp AS [No. SK Korporasi]"
                    + ",BentukKorp AS [Bentuk Korporasi]"
                    + ",Project AS [Project]"
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

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_CUSTOMER_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE MS_CUSTOMER_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

            return true;
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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind();
        }
    }
}
