using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class UploadPembayaran : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            //Bind();
            Js.Confirm(this, "Lanjutkan proses upload data pembayaran?");
            feed.Text = "";
        }

        private void Bind()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT Acc FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("/");
                x.Append(rs.Rows[i][0].ToString());
            }

            TableCell c = rule.Rows[9].Cells[4];
            c.Text = x.ToString();
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
                    + "Template\\MigratePembayaran_" + Session.SessionID + ".xls";

                Dfc.UploadFile(".xls", path, file);
                               
                Cek(path);

                //Hapus file sementara tersebut dari hard-disk server
                //Dfc.DeleteFile(path);
                Response.Redirect("UploadPembayaran2.aspx?path=" + path);

            }
        }

        private void Cek(string path)
        {
            string strSql = "SELECT * FROM [Pembayaran$]";
            DataTable rs = new DataTable();

            try
            {
                rs = Db.xls(strSql, path);
            }
            catch {}

            if (Rpt.ValidateXls(rs, rule, gagal))
                Save(path);
        }

        private void Save(string path)
        {
            int total = 0;
                        
            string strSql = "SELECT * FROM [Pembayaran$]";
            DataTable rs = Db.xls(strSql, path);           
                        
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;
                                
                decimal Alokasi = 0;
                string NoKontrak = Cf.Str(rs.Rows[i][0]);                
                string NamaTagihan = Cf.Str(rs.Rows[i][8]);

                
                if (Save(rs, i, Alokasi))
                        total++;
                                                                             
            }            

            feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Berhasil  : " + total + " baris data";
        }        

        private bool Save(DataTable rs, int i, decimal Alokasi)
        {            
            string NoKontrak = Cf.Str(rs.Rows[i][0]);
            string NoTTS = Cf.Str(rs.Rows[i][1]);
            string NoTTSManual = Cf.Str(rs.Rows[i][2]);
            DateTime TglTTS = Convert.ToDateTime(rs.Rows[i][3]);
            string NoBKM = Cf.Str(rs.Rows[i][4]);
            DateTime TglBKM = Convert.ToDateTime(rs.Rows[i][5]);
            string CB = Cf.Str(rs.Rows[i][6]);
            decimal NilaiTagihan = Convert.ToDecimal(rs.Rows[i][7]);            
            string NamaTagihan = Cf.Str(rs.Rows[i][8]);
            string Rekening = Cf.Str(rs.Rows[i][9]);            

            bool x = true;     
                           
            decimal Tagihan = Db.SingleDecimal("SELECT ISNULL(NilaiTagihan,0) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NamaTagihan = '" + NamaTagihan + "'");
            decimal sisa = NilaiTagihan - Tagihan;

            System.Text.StringBuilder alokasi = new System.Text.StringBuilder();
            
            
                Db.Execute("EXEC spMigratePembayaran "
                    + " '" + NoKontrak + "'"
                    + ",'" + NoTTS + "'"
                    + ",'" + NoTTSManual + "'"
                    + ",'" + TglTTS + "'"
                    + ",'" + NoBKM + "'"
                    + ",'" + TglBKM + "'"
                    + ",'" + CB + "'"
                    + ", " + NilaiTagihan
                    + ",'" + NamaTagihan + "'"
                    + ",'" + Rekening + "'"
                    );

                
                      
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
