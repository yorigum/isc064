using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class UploadJadwal : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            //Bind();
            Js.Confirm(this, "Lanjutkan proses upload data jadwal tagihan?");
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

        //    TableCell c = rule.Rows[25].Cells[4];
        //    c.Text = x.ToString();

        //    System.Text.StringBuilder x2 = new System.Text.StringBuilder();
        //    DataTable rs2 = Db.Rs("SELECT Lokasi FROM REF_LOKASI ORDER BY SN");
        //    for (int i = 0; i < rs2.Rows.Count; i++)
        //    {
        //        if (x2.Length != 0) x2.Append("/");
        //        x2.Append(rs2.Rows[i][0].ToString());
        //    }

        //    c = rule.Rows[26].Cells[4];
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
                    + "Template\\MigrateJadwal_" + Session.SessionID + ".xls";

                Dfc.UploadFile(".xls", path, file);

                Cek(path);

                //Hapus file sementara tersebut dari hard-disk server
                Dfc.DeleteFile(path);

            }
        }

        private void Cek(string path)
        {
            string strSql = "SELECT * FROM [JadwalTagihan$]";
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

            string strSql = "SELECT * FROM [JadwalTagihan$]";
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
        //    int c = Db.SingleInteger("SELECT COUNT(NoStock) FROM MS_UNIT");
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

        //private bool isUnique(string NoStock)
        //{
        //    bool x = true;

        //    int c = Db.SingleInteger(
        //        "SELECT COUNT(*) FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

        //    if (c != 0)
        //        x = false;

        //    return x;
        //}

        private bool Save(DataTable rs, int i)
        {
            string NoKontrak = Cf.Str(rs.Rows[i][0]);
            string NamaTagihan = Cf.Str(rs.Rows[i][1]);
            string TipeTagihan = Cf.Str(rs.Rows[i][2]);
            DateTime TglJT = Convert.ToDateTime(rs.Rows[i][3]);
            decimal NilaiTagihan = Convert.ToDecimal(rs.Rows[i][4]);
            decimal Denda = Convert.ToDecimal(rs.Rows[i][5]);
            string Kpr = Cf.Str(rs.Rows[i][6]);
            int KPR = 0;
            if (Kpr == "YA")
                KPR = 1;

            bool x = true;
            string Aksi = "";

            if (Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'") != 0)
            {


                //Db.Execute("EXEC spMigrateJadwal "
                //    + " '" + NoKontrak + "'"
                //    + ",'" + NamaTagihan + "'"
                //    + ",'" + TipeTagihan + "'"
                //    + ",'" + TglJT + "'"
                //    + ", " + NilaiTagihan
                //    + ", " + Denda
                //    );

                Db.Execute("EXEC spTagihanDaftarM "
                            + " '" + NoKontrak + "'"
                            + ",'" + NamaTagihan + "'"
                            + ",'" + TglJT + "'"
                            + ", " + NilaiTagihan
                            + ", " + Denda
                            + ",'" + TipeTagihan + "'"
                            + ", " + KPR
                            );


            }
            else
                x = false; 
                
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
