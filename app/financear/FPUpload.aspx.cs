using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class FPUpload : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
            }

            Js.Confirm(this, "Lanjutkan proses upload data no Faktur Pajak?");
            feed.Text = "";
        }

        protected void upload_Click(object sender, System.EventArgs e)
        {
            if (valid)
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
                        + "Template\\FP_" + Session.SessionID + ".xls";

                    Dfc.UploadFile(".xls", path, file);

                    Cek(path);

                    //Hapus file sementara tersebut dari hard-disk server
                    Dfc.DeleteFile(path);
                }
            }
        }

        private void Cek(string path)
        {
            string strSql = "SELECT * FROM [FP$]";
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

            string strSql = "SELECT * FROM [FP$]";
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
            //Validasi
            string strSqlUpdate = "";
            string NoPermintaanFP = Cf.Str(rs.Rows[i][0]);
            string NoPemberianFP = Cf.Str(rs.Rows[i][1]);
            string strTglPengajuanFP = Cf.Str(rs.Rows[i][2]);
            string LampiranSPTPPN = Cf.Str(rs.Rows[i][3]);
            string strTotalFP1 = Cf.Str(rs.Rows[i][4]);
            string strTotalFP2 = Cf.Str(rs.Rows[i][5]);
            string strTotalFP3 = Cf.Str(rs.Rows[i][6]);
            string strTotalFPMaks = Cf.Str(rs.Rows[i][7]);
            string strTotalFPDiterima = Cf.Str(rs.Rows[i][8]);
            string NoFP = Cf.Str(rs.Rows[i][9]);
            string strTglFPDiterima = Cf.Str(rs.Rows[i][10]);
            string PICNama = Cf.Str(Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..Username WHERE UserID = '" + Act.UserID + "'"));
            string PICBagian = Cf.Str(Db.SingleString("SELECT SecLevel FROM " + Mi.DbPrefix + "SECURITY..Username WHERE UserID = '" + Act.UserID + "'"));
            bool isComplete = true;

            if (NoPemberianFP != "")
            {
                try
                {
                    DateTime TglFPDiterima = Convert.ToDateTime(strTglFPDiterima);
                    DateTime TglPengajuanFP = Convert.ToDateTime(strTglPengajuanFP);
                    int TotalFP1 = 0, TotalFP2 = 0, TotalFP3 = 0, TotalFPMaks = 0, TotalFPDiterima = 0;
                    try
                    {
                        TotalFP1 = Convert.ToInt32(strTotalFP1);
                        TotalFP2 = Convert.ToInt32(strTotalFP2);
                        TotalFP3 = Convert.ToInt32(strTotalFP3);
                        TotalFPMaks = Convert.ToInt32(strTotalFPMaks);
                        TotalFPDiterima = Convert.ToInt32(strTotalFPDiterima);
                    }
                    catch
                    {
                        TotalFP1 = TotalFP2 = TotalFP3 = TotalFPMaks = TotalFPDiterima = 0;
                    }

                    strSqlUpdate = "UPDATE REF_FP SET TglTerimaFP = '" + Cf.Tgl112(TglFPDiterima) + "'"
                                    + ", NoSuratPemberianFP = '" + NoPemberianFP + "'"
                                    + ", NoSuratPermintaanFP = '" + NoPermintaanFP + "'"
                                    + ", Project = '" + project.SelectedValue + "'"
                                    + ", LampiranSPT = '" + LampiranSPTPPN + "'"
                                    + ", TotalFP1 = " + TotalFP1
                                    + ", TotalFP2 = " + TotalFP2
                                    + ", TotalFP3 = " + TotalFP3
                                    + ", TotalFPMaksimal = " + TotalFPMaks
                                    + ", TotalFPDiterima = " + TotalFPDiterima
                                    + ", TglPengajuanFP = '" + Cf.Tgl112(TglPengajuanFP) + "'"
                                    + ", PICNama = '" + PICNama + "'"
                                    + ", PICBagian = '" + PICBagian + "'"
                                    + " WHERE NoFPS = '" + NoFP + "'";
                }
                catch { isComplete = false; }
            }
            else isComplete = false;

            //NoFP harus unik
            int nofp = Db.SingleInteger("SELECT COUNT(*) FROM REF_FP WHERE NoFPS = '" + NoFP + "'");

            if (NoPermintaanFP != "" && nofp == 0 && isComplete)
            {
                Db.Execute("EXEC spFPRegis"
                    + " '" + NoFP + "'"
                    + ", '" + project.SelectedValue + "'"
                    );

                if (strSqlUpdate != "")
                    Db.Execute(strSqlUpdate);

                DataTable rs2 = Db.Rs("SELECT "
                                + " NoSuratPermintaanFP AS [Reff. Surat Permintaan Faktur Pajak]"
                                + ", NoSuratPemberianFP AS [Reff. Surat Pemberian FP]"
                                + ", TglPengajuanFP AS [Tgl. FP Diajukan]"
                                + ", TglTerimaFP AS [Tgl. FP Diterima]"
                                + ", LampiranSPT AS [Lampiran SPT PPN-Masa Pajak]"
                                + ", TotalFP1 AS [Total FP I]"
                                + ", TotalFP2 AS [Total FP II]"
                                + ", TotalFP3 AS [Total FP III]"
                                + ", TotalFPMaksimal AS [Total Maksimal]"
                                + ", TotalFPDiterima AS [Jumlah FP Diterima]"
                                + ", NoFPS AS [No. Faktur Pajak]"
                                + ", PICNama AS [PIC]"
                                + ", PICBagian AS [Bagian]"
                                + " FROM REF_FP "
                                + " WHERE NoFPS  = '" + NoFP + "'");

                string KetLog = Cf.LogCapture(rs2);

                Db.Execute("EXEC spLogFP"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + NoFP + "'"
                    );

                return true;
            }
            else
                return false;
        }

        protected bool valid
        {
            get
            {
                bool x = true;

                if (project.Items.Count == 0)
                    x = false;

                
                if (!x)
                    Js.Alert(
                        this
                        , "Format Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Pilih Project.\\n"
                        , ""
                        );
                return x;
            }
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
