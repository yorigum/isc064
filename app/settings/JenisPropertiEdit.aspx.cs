using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
    public partial class JenisPropertiEdit : System.Web.UI.Page
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
                Fill();
            }

            FeedBack();
            Js.Confirm(this, "Lanjutkan proses edit data Tipe unit properti?\\n"
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
            Act.ProjectList(project);

            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?tb=ISC064_MARKETINGJUAL..REF_JENISPROPERTI_LOG&Pk=" + NoJenis + "'";
            btndel.Attributes["onclick"] = "location.href='JenisPropertiDel.aspx?NoJenis=" + NoJenis + "'";

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENISPROPERTI WHERE SN = '" + NoJenis + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else if (!Act.AksesProject(rs.Rows[0]["Project"].ToString()))
                Response.Redirect("/CustomError/SecLevel.html");
            else
            {
                nojenis.Text = rs.Rows[0]["SN"].ToString();
                jenis.Text = rs.Rows[0]["JenisProperti"].ToString();
                namajenis.Text = rs.Rows[0]["Nama"].ToString();
                Cf.SelectedValue(project, rs.Rows[0]["Project"].ToString());
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(jenis))
            {
                x = false;
                if (s == "") s = jenis.ID;
                jenisc.Text = "Kosong";
            }
            else
                jenisc.Text = "";

            if (Cf.isEmpty(namajenis))
            {
                x = false;
                if (s == "") s = namajenis.ID;
                namajenisc.Text = "Kosong";
            }
            else
                namajenisc.Text = "";


            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Tipe Properti Unit tidak boleh kosong.\\n"
                    + "2. Nama Tipe Unit Properti tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void Save(bool close)
        {
            if (valid())
            {
                int ada = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENISPROPERTI WHERE JenisProperti='" + jenis.Text + "' AND Project = '" + project.SelectedValue + "' AND SN != " + NoJenis);
                if (ada > 0)
                {
                    string s = "";

                    Js.Alert(
                       this
                       , "Input Tidak Valid.\\n\\n"
                       + "Aturan Proses :\\n"
                       + "1. Tipe Unit Properti tidak boleh kosong.\\n"
                       + "2. Nama Tipe Unit tidak boleh kosong.\\n"
                       + "3. Tipe Unit Duplikat.\\n"
                       , "document.getElementById('" + s + "').focus();"
                       + "document.getElementById('" + s + "').select();"
                       );
                }
                else
                {
                    string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENISPROPERTI WHERE SN = '" + NoJenis + "'";
                    DataTable rs = Db.Rs(strSql);
                    DataTable rsBef = Db.Rs("SELECT "
                                + " JenisProperti"
                                + ",Nama"
                                + ",Project"
                                + ",SN"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENISPROPERTI "
                                + " WHERE SN = '" + NoJenis + "'");

                    //relasi
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT SET JenisProperti='" + jenis.Text + "' WHERE JenisProperti='" + rs.Rows[0]["JenisProperti"] + "'");
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK SET JenisProperti='" + jenis.Text + "' WHERE JenisProperti='" + rs.Rows[0]["JenisProperti"] + "'");
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI SET JenisProperti='" + jenis.Text + "' WHERE JenisProperti='" + rs.Rows[0]["JenisProperti"] + "'");
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI_OBS SET JenisProperti='" + jenis.Text + "' WHERE JenisProperti='" + rs.Rows[0]["JenisProperti"] + "'");

                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENISPROPERTI SET JenisProperti='" + jenis.Text + "',Nama='" + namajenis.Text + "',Project='" + project.SelectedValue + "' WHERE SN = '" + NoJenis + "'");

                    DataTable rsAft = Db.Rs("SELECT "
                                + " JenisProperti"
                                + ",Nama"
                                + ",Project"
                                + ",SN"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENISPROPERTI "
                                + " WHERE SN = '" + NoJenis + "'");

                    //Logfile
                    string Ket = "Jenis Properti: " + jenis.Text + "<br>"
                        + Cf.LogCompare(rsBef, rsAft);

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogJenisProperti"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoJenis + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENISPROPERTI_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENISPROPERTI_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                    if (close)
                        Js.CloseAndReload(this);
                    else
                        Response.Redirect("JenisPropertiEdit.aspx?done=1&NoJenis=" + NoJenis);
                }
            }
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            Save(true);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            Save(false);
        }

        private string NoJenis
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoJenis"]);
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
