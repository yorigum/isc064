using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

namespace ISC064.ADMINJUAL
{
    public partial class UnitUpload : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
            }
            Bind();
            Js.Confirm(this, "Lanjutkan proses upload data unit?");
            feed.Text = "";
        }

        private void Bind()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT Jenis FROM REF_JENIS WHERE Project = '" + project.SelectedValue + "' ORDER BY SN");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("/");
                x.Append(rs.Rows[i][0].ToString());
            }

            TableCell c = rule.Rows[1].Cells[4];
            c.Text = x.ToString();

            System.Text.StringBuilder x2 = new System.Text.StringBuilder();
            DataTable rs2 = Db.Rs("SELECT Lokasi FROM REF_LOKASI WHERE Project = '" + project.SelectedValue + "' ORDER BY SN");
            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                if (x2.Length != 0) x2.Append("/");
                x2.Append(rs2.Rows[i][0].ToString());
            }

            c = rule.Rows[2].Cells[4];
            c.Text = x2.ToString();

            //Ini codingan yang tipe propertinya di setup di modul settings
            System.Text.StringBuilder x4 = new System.Text.StringBuilder();
            DataTable rs4 = Db.Rs("SELECT JenisProperti FROM REF_JENISPROPERTI WHERE Project = '" + project.SelectedValue + "'");
            for (int i = 0; i < rs4.Rows.Count; i++)
            {
                if (x4.Length != 0) x4.Append("/");
                x4.Append(rs4.Rows[i][0].ToString());
            }
            c = rule.Rows[23].Cells[4];
            c.Text = x4.ToString();
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
                    + "Template\\Unit_" + Session.SessionID + ".xls";

                Dfc.UploadFile(".xls", path, file);

                Cek(path);

                //Hapus file sementara tersebut dari hard-disk server
                Dfc.DeleteFile(path);

            }
        }

        private void Cek(string path)
        {
            string strSql = "SELECT * FROM [UNIT$]";
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

            string strSql = "SELECT * FROM [Unit$]";
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

        private string AutoID()
        {
            int c = Db.SingleInteger("SELECT COUNT(NoStock) FROM MS_UNIT");
            string x = "";

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                x = c.ToString().PadLeft(7, '0');

                if (isUnique(x)) hasfound = true;
            }

            return x;
        }

        private bool isUnique(string NoStock)
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

            if (c != 0)
                x = false;

            return x;
        }

        private bool Save(DataTable rs, int i)
        {
            string NoStock = "";
            string Jenis = Cf.Str(rs.Rows[i][0]);
            string Lokasi = Cf.Str(rs.Rows[i][1]);
            string Lantai = Cf.Str(rs.Rows[i][2]);
            string Nomor = Cf.Str(rs.Rows[i][3]);
            decimal Luas = Convert.ToDecimal(rs.Rows[i][4]);
            //string Project = Cf.Str(rs.Rows[i][23]);
            string Kategori = Cf.Str(rs.Rows[i][23]);
            string ParamID = "FormatLantai" + project.SelectedValue;
            string ParamID2 = "FormatUnit" + project.SelectedValue;

            string strSql = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");
            string strSql2 = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID2 + "'");

            string NoUnit = Lokasi + strSql + Lantai + strSql2 + Nomor;

            bool x = true;
            string Aksi = "";

            if (Db.SingleInteger("SELECT COUNT(*) FROM MS_UNIT WHERE NoUnit = '" + NoUnit + "'") == 0)
            {
                Aksi = "DAFTAR";
                NoStock = AutoID();

                Db.Execute("EXEC spUnitDaftar "
                    + " '" + NoStock + "'"
                    + ",'" + Jenis + "'"
                    + ",'" + Lokasi + "'"
                    + ",'" + NoUnit + "'"
                    + ", " + Luas
                    );
            }
            else if (overwrite.Checked)
            {
                Aksi = "EDIT";
                NoStock = Db.SingleString("SELECT NoStock FROM MS_UNIT WHERE NoUnit = '" + NoUnit + "'");

                Db.Execute("EXEC spUnitEdit"
                    + " '" + NoStock + "'"
                    + ",'" + Jenis + "'"
                    + ",'" + Lokasi + "'"
                    + ",'" + NoUnit + "'"
                    + ", " + Luas
                    + ", '" + Lantai + "'"
                    + ", '" + Nomor + "'"
                    );
            }
            else x = false;

            if (x)
            {
                decimal PriceListMin = Convert.ToDecimal(rs.Rows[i][5]);
                decimal PriceListRmh = Convert.ToDecimal(rs.Rows[i][6]);
                decimal PriceListKav = Convert.ToDecimal(rs.Rows[i][7]);
                decimal BiayaBPHTB = Convert.ToDecimal(rs.Rows[i][8]);
                decimal BiayaSurat = Convert.ToDecimal(rs.Rows[i][9]);
                decimal BiayaProses = Convert.ToDecimal(rs.Rows[i][10]);
                decimal BiayaLain = Convert.ToDecimal(rs.Rows[i][11]);
                string NamaJalan = Cf.Str(rs.Rows[i][24]); if (NamaJalan == "NA") NamaJalan = "";
                decimal BiayaTanah = Convert.ToDecimal(rs.Rows[i][25]);

                Db.Execute("EXEC spUnitPriceList"
                    + " '" + NoStock + "'"
                    + ", " + PriceListMin
                    + ", " + PriceListRmh
                    );

                Db.Execute("UPDATE MS_UNIT SET"
                        + " BiayaBPHTB = '" + BiayaBPHTB + "'"
                        + ",BiayaSurat = '" + BiayaSurat + "'"
                        + ",BiayaProses = '" + BiayaProses + "'"
                        + ",BiayaLainLain = '" + BiayaLain + "'"
                        + ",HargaTanah = '" + BiayaTanah + "'"
                        + ",PricelistKavling = '" + PriceListKav + "'"
                        + ",NamaJalan = '" + NamaJalan + "'"
                        + " WHERE NoStock = '" + NoStock + "'"
                    );

                Db.Execute("EXEC spPriceListHistory"
                    + " '" + NoStock + "'"
                    + ", " + PriceListMin
                    + ", " + PriceListRmh
                    + ", " + PriceListKav
                    + ",'" + DateTime.Today + "'"
                    );

                string Zoning = Cf.Str(rs.Rows[i][12]); if (Zoning == "NA") Zoning = "";
                decimal Panjang = Convert.ToDecimal(rs.Rows[i][13]);
                decimal Lebar = Convert.ToDecimal(rs.Rows[i][14]);
                decimal Tinggi = Convert.ToDecimal(rs.Rows[i][15]);
                decimal LuasSG = Convert.ToDecimal(rs.Rows[i][16]);
                decimal LuasNett = Convert.ToDecimal(rs.Rows[i][18]);
                decimal LuasLebih = Convert.ToDecimal(rs.Rows[i][17]);

                bool HadapAtrium = false;
                bool HadapEntrance = false;
                bool HadapEskalator = false;
                bool HadapLift = false;
                bool HadapParkir = false;
                bool HadapAxis = false;
                bool Hook = false;

                decimal LebarJalan = Convert.ToDecimal(rs.Rows[i][19]);

                bool Outdoor = false;

                string ArahHadap = Cf.Str(rs.Rows[i][20]); if (ArahHadap == "NA") ArahHadap = "";
                string Panorama = Cf.Str(rs.Rows[i][21]); if (Panorama == "NA") Panorama = "";
                string JenisProperti = Cf.Str(rs.Rows[i][22]); if (JenisProperti == "Apartment") JenisProperti = "";

                Db.Execute("EXEC spUnitEditSpek"
                    + " '" + NoStock + "'"
                    + ",'" + Zoning + "'"
                    + ", " + Panjang
                    + ", " + Lebar
                    + ", " + Tinggi
                    + ", " + LuasSG
                    + ", " + LuasNett
                    + ", " + Cf.BoolToSql(HadapAtrium)
                    + ", " + Cf.BoolToSql(HadapEntrance)
                    + ", " + Cf.BoolToSql(HadapEskalator)
                    + ", " + Cf.BoolToSql(HadapLift)
                    + ", " + Cf.BoolToSql(HadapParkir)
                    + ", " + Cf.BoolToSql(HadapAxis)
                    + ", " + Cf.BoolToSql(Hook)
                    + ", " + LebarJalan
                    + ", " + Cf.BoolToSql(Outdoor)
                    + ",'" + ArahHadap + "'"
                    + ",'" + Panorama + "'"
                    + ",'" + JenisProperti + "'"
                    + ",'" + Kategori + "'"
                    );

                Db.Execute("UPDATE MS_UNIT SET"
                        + " LuasLebih = '" + LuasLebih + "'"
                        + " WHERE NoStock = '" + NoStock + "'"
                    );

                //Response.Write(Kategori);
                //int SifatPPN = (Kategori.ToUpper() == "REAL ESTATE") ? 1 : 0;
                int SifatPPN;
                if(Kategori == "REAL ESTATE")
                {
                    SifatPPN = 1;
                }
                else if(Kategori == "KOMERSIL")
                {
                    SifatPPN = 1;
                }
                else
                {
                    SifatPPN = 0;
                }
                string No = Db.SingleString("SELECT TOP 1 NoStock FROM MS_UNIT ORDER BY NoStock DESC");
                Db.Execute("UPDATE MS_UNIT SET Project='" + project.SelectedValue + "',Lantai = '" + Lantai + "',SifatPPN = " + SifatPPN + ",Nomor='" + Nomor + "' WHERE NoStock = '" + No + "'");

                DataTable log = Db.Rs("SELECT "
                    + " NoStock AS [No. Stock]"
                    + ",Jenis AS [Jenis]"
                    + ",Lokasi AS [Lokasi]"
                    + ",NoUnit AS [Unit]"
                    + ",Kategori AS [Kategori Unit]"
                    + ",Lantai AS [Blok]"
                    + ",Nomor"
                    + ",Luas AS [Luas]"
                    + ",PriceListMin AS [Price List Minimum]"
                    + ",PriceList AS [Price List Rumah]"
                    + ",PricelistKavling AS [Price List Kavling]"
                    + ",BiayaBPHTB AS [Biaya BPHTB]"
                    + ",BiayaSurat AS [Biaya Surat]"
                    + ",BiayaProses AS [Biaya Proses]"
                    + ",BiayaLainLain AS [Biaya Lain-Lain]"
                    + ",HargaTanah AS [Harga Tanah]"
                    + ",Zoning"
                    + ",Panjang"
                    + ",Lebar"
                    + ",Tinggi"
                    + ",LuasSG AS [Luas Tanah]"
                    + ",LuasLebih AS [Luas Lebih Tanah]"
                    + ",LuasNett AS [Luas Bangunan]"
                    + ",HadapAtrium AS [Hadap Atrium/Void]"
                    + ",HadapEntrance AS [Hadap Entrance]"
                    + ",HadapEskalator AS [Hadap Eskalator]"
                    + ",HadapLift AS [Hadap Lift]"
                    + ",HadapParkir AS [Hadap Parkir]"
                    + ",HadapAxis AS [Hadap Axis]"
                    + ",Hook AS [Hook]"
                    + ",LebarJalan AS [Lebar Jalan]"
                    + ",Outdoor AS [Outdoor]"
                    + ",NamaJalan AS [Nama Jalan]"
                    + ",ArahHadap AS [Arah Hadap]"
                    + ",Panorama"
                    + ",JenisProperti"
                    + " FROM MS_UNIT"
                    + " WHERE NoStock = '" + NoStock + "'"
                    );

                Db.Execute("EXEC spLogUnit"
                    + " '" + Aksi + "'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(log) + "'"
                    + ",'" + NoStock + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_UNIT_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_UNIT_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

            }

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
