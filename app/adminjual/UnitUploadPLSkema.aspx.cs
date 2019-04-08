using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class UnitUploadPLSkema : System.Web.UI.Page
    {
        int Berhasil = 0;
        int Gagal = 0;
        string BarisGagal = "";
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack)
                Act.ProjectList(project);

            Js.Confirm(this, "Lanjutkan proses upload data price list unit properti?");
            feed.Text = "";
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
                    + "Template\\PL_" + Session.SessionID + ".xls";

                Dfc.UploadFile(".xls", path, file);

                Cek(path);

                //Hapus file sementara tersebut dari hard-disk server
                Dfc.DeleteFile(path);

            }
        }

        private void Cek(string path)
        {
            string strSql = "SELECT * FROM [PriceList$]";
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
            System.Text.StringBuilder log = new System.Text.StringBuilder();
            int total = 0;

            string strSql = "SELECT * FROM [PriceList$]";
            DataTable rs = Db.xls(strSql, path);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                if (Save(rs, i))
                {
                    total++;
                }
            }

            if (Berhasil == 0)
            {
                feed2.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Gagal, No. Skema Bukan Milik Project  : " + Gagal + " Baris Data, " + BarisGagal;
            }
            else
            {
                feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Berhasil  : " + Berhasil + " Baris Data";

                feed2.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Gagal, No. Skema Bukan Milik Project  : " + Gagal + " Baris Data, " + BarisGagal;
            }
        }

        private bool Save(DataTable rs, int i)
        {
            string Unit = Cf.Str(rs.Rows[i][0]);
            int NoSkema2 = Convert.ToInt32(rs.Rows[i][1]);
            string Project2 = Db.SingleString("SELECT Project FROM REF_SKEMA WHERE Nomor =  '" + NoSkema2 + "'");

            if (Db.SingleInteger("SELECT COUNT(*) FROM MS_UNIT WHERE NoUnit = '" + Unit + "' AND Project = '"+Project2+"'") != 0)
            {
                string NoStock = Db.SingleString("SELECT NoStock FROM MS_UNIT WHERE NoUnit = '" + Unit + "'");
                int NoSkema = Convert.ToInt32(rs.Rows[i][1]);
                decimal plmin = Convert.ToDecimal(rs.Rows[i][2]);
                decimal PriceList = Convert.ToDecimal(rs.Rows[i][3]);

                DataTable rsBef;
                if (Db.SingleInteger("SELECT COUNT(*) FROM MS_PRICELIST WHERE NoStock = '" + NoStock + "' AND NoSkema = " + NoSkema) != 0)
                {
                    rsBef = Db.Rs("SELECT "
                        + " PriceList AS [Price List]"
                        + " FROM MS_PRICELIST "
                        + " WHERE NoStock = '" + NoStock + "'"
                        + " AND NoSkema = '" + NoSkema + "'"
                        );
                }
                else
                {
                    rsBef = Db.Rs("SELECT 0 AS [Price List]");
                }

                Db.Execute("EXEC spUnitPriceListSkema "
                    + "'" + NoStock + "'"
                    + "," + NoSkema
                    + "," + PriceList
                    );

                Db.Execute("EXEC spUnitPriceList "
                    + "'" + NoStock + "'"
                    + "," + plmin
                    + "," + 0
                    );

                DataTable rsAft = Db.Rs("SELECT "
                    + " PriceList AS [Price List]"
                    + " FROM MS_PRICELIST "
                    + " WHERE NoStock = '" + NoStock + "'"
                    + " AND NoSkema = '" + NoSkema + "'"
                    );

                string Ket = "*** UPLOAD PRICE LIST<br>Unit : " + Unit + "<br>"
                    + Cf.LogCompare(rsBef, rsAft);

                DataTable rsID = Db.Rs(" SELECT Nomor FROM MS_PRICELIST "
                    + " WHERE NoStock = '" + NoStock + "'"
                    + " AND NoSkema = '" + NoSkema + "'"
                    );

                Db.Execute("EXEC spLogUnit"
                    + " 'SPL'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + rsID.Rows[0]["Nomor"] + "'"
                    );

                Berhasil++;
            }
            else
            {
                Gagal++;
                BarisGagal += "Baris Ke-" + (i + 1).ToString() + ", ";
            }
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
    }
}
