using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{

    public partial class BerkasPPJBEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                //Bind(); //tanggal dan bulan
                Act.ProjectList(project);
                Fill();                
            }

            FeedBack();
            Js.Confirm(this, "Lanjutkan proses edit data berkas PPJB?\\n"
                + "");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Berhasil...";
            }
        }

        private void Fill()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?tb=ISC064_MARKETINGJUAL..REF_BERKAS_PPJB_LOG&Pk=" + NoBerkas + "'";
            btndel.Attributes["onclick"] = "location.href='BerkasPPJBDel.aspx?NoBerkas=" + NoBerkas + "'";

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB WHERE SN = '" + NoBerkas + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                noberkas.Text = rs.Rows[0]["SN"].ToString();
                namaberkas.Text = rs.Rows[0]["Nama"].ToString();
                project.SelectedValue = rs.Rows[0]["Project"].ToString();
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(namaberkas))
            {
                x = false;
                if (s == "") s = namaberkas.ID;
                namaberkasc.Text = "Kosong";
            }
            else
                namaberkasc.Text = "";


            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Lokasi Unit Properti tidak boleh kosong.\\n"
                    + "2. Nama Lokasi Unit Properti tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }


        protected void ok_Click(object sender, System.EventArgs e)
        {
            //if (Save()) Js.Close(this);
            if (valid())
            {
                int ada = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB WHERE Nama='" + namaberkas.Text + "' AND SN != " + NoBerkas);
                if (ada > 0)
                {
                    string s = "";

                    Js.Alert(
                       this
                       , "Input Tidak Valid.\\n\\n"
                       + "Aturan Proses :\\n"
                       + "1. Lokasi Unit Properti tidak boleh kosong.\\n"
                       + "2. Nama Lokasi Unit tidak boleh kosong.\\n"
                       + "3. Lokasi Unit Duplikat.\\n"
                       , "document.getElementById('" + s + "').focus();"
                       + "document.getElementById('" + s + "').select();"
                       );
                }
                else
                {

                    string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB WHERE SN = '" + NoBerkas + "'";
                    DataTable rs = Db.Rs(strSql);
                    DataTable rsBef = Db.Rs("SELECT "
                                + " Nama"
                                + ",SN"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB "
                                + " WHERE SN = '" + NoBerkas + "'");

                    //relasi
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_PPJB SET Lokasi='" + lokasi.Text + "' WHERE Lokasi='" + rs.Rows[0]["Lokasi"] + "'");
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK SET Lokasi='" + lokasi.Text + "' WHERE Lokasi='" + rs.Rows[0]["Lokasi"] + "'");
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI SET Lokasi='" + lokasi.Text + "' WHERE Lokasi='" + rs.Rows[0]["Lokasi"] + "'");
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI_OBS SET Lokasi='" + lokasi.Text + "' WHERE Lokasi='" + rs.Rows[0]["Lokasi"] + "'");

                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB SET Nama='" + namaberkas.Text + "' WHERE SN = '" + NoBerkas + "'");

                    DataTable rsAft = Db.Rs("SELECT "
                                + " Nama"
                                + ",SN"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB "
                                + " WHERE SN = '" + NoBerkas + "'");

                    //Logfile
                    string Ket = "Nama Berkas: " + namaberkas.Text + "<br>"
                        + Cf.LogCompare(rsBef, rsAft);

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogBerkasPPJB"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoBerkas + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                    Js.CloseAndReload(this);
                }
            }

        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            // if (Save()) Response.Redirect("JenisEdit.aspx?done=1&NoJenis=" + NoJenis);

            if (valid())
            {
                int ada = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB WHERE Nama='" + namaberkas.Text + "' AND SN != " + NoBerkas);
                if (ada > 0)
                {
                    string s = "";

                    Js.Alert(
                       this
                       , "Input Tidak Valid.\\n\\n"
                       + "Aturan Proses :\\n"
                       + "1. Lokasi Unit Properti tidak boleh kosong.\\n"
                       + "2. Nama Lokasi Unit tidak boleh kosong.\\n"
                       + "3. Lokasi Unit Duplikat.\\n"
                       , "document.getElementById('" + s + "').focus();"
                       + "document.getElementById('" + s + "').select();"
                       );
                }
                else
                {
                    string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB WHERE SN = '" + NoBerkas + "'";
                    DataTable rs = Db.Rs(strSql);
                    DataTable rsBef = Db.Rs("SELECT "
                                + " Nama"
                                + ",SN"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB "
                                + " WHERE SN = '" + NoBerkas + "'");

                    //relasi
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT SET Lokasi='" + lokasi.Text + "' WHERE Lokasi='" + rs.Rows[0]["Lokasi"] + "'");
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK SET Lokasi='" + lokasi.Text + "' WHERE Lokasi='" + rs.Rows[0]["Lokasi"] + "'");
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI SET Lokasi='" + lokasi.Text + "' WHERE Lokasi='" + rs.Rows[0]["Lokasi"] + "'");
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI_OBS SET Lokasi='" + lokasi.Text + "' WHERE Lokasi='" + rs.Rows[0]["Lokasi"] + "'");

                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB SET Nama='" + namaberkas.Text + "' WHERE SN = '" + NoBerkas + "'");

                    DataTable rsAft = Db.Rs("SELECT "
                                + " Nama"
                                + ",SN"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB "
                                + " WHERE SN = '" + NoBerkas + "'");

                    //Logfile
                    string Ket = "Nama Berkas: " + namaberkas.Text + "<br>"
                        + Cf.LogCompare(rsBef, rsAft);

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogBerkasPPJB"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoBerkas + "'"
                        );
                    Response.Redirect("BerkasPPJBEdit.aspx?done=1&NoBerkas=" + NoBerkas);
                }
            }
        }

        private string NoBerkas
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoBerkas"]);
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