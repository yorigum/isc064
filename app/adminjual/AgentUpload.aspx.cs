using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class AgentUpload : System.Web.UI.Page
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

            Js.Confirm(this, "Lanjutkan proses upload data marketing?");
            feed.Text = "";
        }

        private void Bind()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT Tipe FROM REF_AGENT_TIPE WHERE Project = '" + project.SelectedValue + "' ORDER BY ID");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("/");
                x.Append(rs.Rows[i][0].ToString());
            }

            TableCell c = rule.Rows[4].Cells[4];
            c.Text = x.ToString();

            System.Text.StringBuilder x1 = new System.Text.StringBuilder();
            DataTable rs1 = Db.Rs("SELECT Nama FROM REF_AGENT_LEVEL WHERE Project = '" + project.SelectedValue + "' ORDER BY LevelID");
            for (int i = 0; i < rs1.Rows.Count; i++)
            {
                if (x1.Length != 0) x1.Append("/");
                x1.Append(rs1.Rows[i][0].ToString());
            }

            TableCell d = rule.Rows[5].Cells[4];
            d.Text = x1.ToString();

            System.Text.StringBuilder x2 = new System.Text.StringBuilder();
            //DataTable rs2 = Db.Rs("SELECT a.KodeSales FROM MS_AGENT a join REF_AGENT_LEVEL b ON a.SalesLevel = b.LevelID WHERE a.SalesLevel = b.ParentID");
            DataTable rs2 = Db.Rs("SELECT KodeSales FROM MS_AGENT WHERE SalesLevel IN (select LevelID from ref_agent_level where project = '"+project.SelectedValue+"' and LevelID IN (SELECT ParentID FROM REF_AGENT_LEVEL WHERE Project = '"+project.SelectedValue+"'))");
            x2.Append("NA");
            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                if (x2.Length != 0) x2.Append("/");
                x2.Append(rs2.Rows[i][0].ToString());

            }

            TableCell e = rule.Rows[6].Cells[4];
            e.Text = x2.ToString();

            //System.Text.StringBuilder x3 = new System.Text.StringBuilder();
            ////DataTable rs2 = Db.Rs("SELECT a.KodeSales FROM MS_AGENT a join REF_AGENT_LEVEL b ON a.SalesLevel = b.LevelID WHERE a.SalesLevel = b.ParentID");
            //DataTable rs3 = Db.Rs("SELECT Project FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project IN (" + Act.ProjectListSql + ")");
            //for (int i = 0; i < rs3.Rows.Count; i++)
            //{
            //    if (x3.Length != 0) x3.Append("/");
            //    x3.Append(rs3.Rows[i][0].ToString());
            //}

            //TableCell f = rule.Rows[15].Cells[4];
            //f.Text = x3.ToString();
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
                    + "Template\\Agent_" + Session.SessionID + ".xls";

                Dfc.UploadFile(".xls", path, file);

                Cek(path);

                //Hapus file sementara tersebut dari hard-disk server
                Dfc.DeleteFile(path);

            }
        }

        private void Cek(string path)
        {
            string strSql = "SELECT * FROM [AGENT$]";
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

            string strSql = "SELECT * FROM [AGENT$]";
            DataTable rs = Db.xls(strSql, path);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;
                //DataTable a = Db.Rs("SELECT * FROM REF_TIPE_AGENT WHERE NAMATIPE='" + rs.Rows[i][4].ToString() + "'");
                //if (a.Rows.Count == 0)
                //{
                //    Js.Alert(
                //        this
                //        , "Proses Upload Gagal.\\n"
                //        + "Tipe yang diinputkan tidak sesuai"
                //        , ""
                //        );
                //    break;
                //}
                //else 
                if (Save(rs, i))
                {

                    total++;
                }
            }

            feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Berhasil  : " + total + " baris data";
        }

        private bool Save(DataTable rs, int i)
        {
            string KodeSales = Cf.Str(rs.Rows[i][0]);
            string Nama = Cf.Str(rs.Rows[i][1]);
            string Alamat = Cf.Str(rs.Rows[i][2]);
            string Tipe = Cf.Str(rs.Rows[i][3]);
            string Level = Cf.Str(rs.Rows[i][4]);
            string Atasan = Cf.Str(rs.Rows[i][5]);
            string Email = Cf.Str(rs.Rows[i][6]);
            string Telpon = Cf.Str(rs.Rows[i][7]);
            string Handphone = Cf.Str(rs.Rows[i][8]);
            string Whatsapp = Cf.Str(rs.Rows[i][9]);
            string NPWP = Cf.Str(rs.Rows[i][10]);
            string RekBank = Cf.Str(rs.Rows[i][11]);
            string Bank = Cf.Str(rs.Rows[i][12]);
            string AtasNama = Cf.Str(rs.Rows[i][13]);

            int LevelSales = Db.SingleInteger("SELECT LevelID FROM REF_AGENT_LEVEL WHERE Nama = '" + Level + "'");
            int TipeSales = Db.SingleInteger("SELECT ID FROM REF_AGENT_TIPE WHERE Tipe = '" + Tipe + "'");
            int CekAtasan = 0;
            int KodeAtasan = 0;
            int LvlAtasan = 0;

            //jika tidak punya atasan
            if (Atasan != "NA")
            {
                int LevelAtasan = Db.SingleInteger("SELECT SalesLevel FROM MS_AGENT WHERE KodeSales = '" + Atasan + "'");
                CekAtasan = Db.SingleInteger("SELECT Count(NoAgent) FROM MS_AGENT WHERE KodeSales = '" + Atasan + "' AND SalesTipe = '" + TipeSales + "' AND SalesLevel = '" + LevelAtasan + "'");                
                if (CekAtasan != 0)
                {
                    KodeAtasan = Db.SingleInteger("SELECT NoAgent FROM MS_AGENT WHERE KodeSales = '" + Atasan + "'");
                    int b = Db.SingleInteger("SELECT ParentID FROM REF_AGENT_LEVEL WHERE LevelID = " + LevelSales + "");
                    LvlAtasan = LevelAtasan;
                }
                else
                {
                    Js.Alert(this, "Kode Atasan Tidak Ditemukan", "");
                    return false;
                }
            }

            int CekKode = Db.SingleInteger("SELECT COUNT(KodeSales) FROM MS_AGENT WHERE KodeSales = '" + KodeSales + "'");
            //if (Cf.Valid(this, "Customer", project.SelectedValue, npwp.ID) == true)
            //{
            //    if (NPWP.Length < 15)
            //    {
            //        x = false;
            //        if (s == "") s = npwp.ID;
            //        npwpc.Text = "Harus 15 Digit";
            //    }
            //    else
            //        npwpc.Text = "";
            //}

            if (CekKode == 0 && NPWP.Length == 15)
            {
                Db.Execute("EXEC spAgentDaftar "
                        + " '" + Nama + "'"
                        + ",''"
                        + ",''"
                        + ",'" + Alamat + "'"
                        + ",'" + Telpon + "'"
                        + ",'" + NPWP + "'"
                        + ",'" + TipeSales + "'"
                        + ",'" + LevelSales + "'"
                        + ",''"
                        + ",'" + KodeAtasan + "'"
                        + ",'" + Email + "'"
                        + ",'" + Bank + "'"
                        + ",''"
                        + ",'" + RekBank + "'"
                        + ",'" + AtasNama + "'"
                        + ",''"
                        + ",''"
                        );
                //get nomor agent terbaru
                int NoAgent = Db.SingleInteger(
                    "SELECT TOP 1 NoAgent FROM MS_AGENT ORDER BY NoAgent DESC");
                //Response.Write(strSql);

                Db.Execute("Update MS_AGENT SET KodeSales = '" + KodeSales.ToString().Replace(" ", "") + "',Whatsapp = '" + Whatsapp + "',Handphone = '" + Handphone + "',Email = '" + Email + "',LvlAtasan = '" + LvlAtasan + "', Project = '" + project.SelectedValue + "' Where NoAgent = '" + NoAgent + "'");

                //int Skema0 = Convert.ToInt32(Cf.Str(rs.Rows[i][4]).Replace("S", ""));

                //Db.Execute("UPDATE MS_AGENT SET Tipe = '" + Tipe + "' WHERE NoAgent = " + NoAgent);

                DataTable log = Db.Rs("SELECT "
                    + " NoAgent AS [No. Agent]"
                    + ",KodeSales AS [Kode Sales]"
                    + ",Nama AS [Nama Lengkap]"
                    //+ ",Principal AS [Nama Perusahaan]"
                    + ",Alamat"
                    + ",SalesTipe"
                    + ",SalesLevel"
                    + ",Atasan"
                    + ",LvlAtasan"
                    + ",Kontak"
                    + ",Handphone"
                    + ",Whatsapp"
                    + ",NPWP"
                    + ",Email"
                    + ",RekBank"
                    + ",Rekening"
                    + ",AtasNama"
                    + ",Project"
                    + " FROM MS_AGENT a"
                    + " WHERE NoAgent = " + NoAgent
                    );

                Db.Execute("EXEC spLogAgent"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(log) + "'"
                    + ",'" + NoAgent.ToString().PadLeft(5, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_AGENT_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_AGENT_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

                return true;
            }
            else
            {
                //Js.Alert(this, "Kode Sales Sudah Digunakan\\n", "");
                Js.Alert(
                    this
                    , "Kode Sales Sudah Digunakan.\\n"
                    + "Nomor NPWP harus 15 digit :\\n",""
                    );
                return false;
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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind();
        }
    }
}
