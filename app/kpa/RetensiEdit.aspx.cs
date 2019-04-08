using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA
{
    public partial class RetensiEdit : System.Web.UI.Page
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
                Act.ProjectList(project);
                Fill();
            }

            FeedBack();
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
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=REF_RETENSI_LOG&Pk=" + Kode + "&project=" + Project + "'";
            btndel.Attributes["onclick"] = "location.href='RetensiDel.aspx?Kode=" + Kode + "&project=" + Project + "'";

            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_RETENSI WHERE Kode = '" + Kode + "' AND Project = '" + Project + "'");
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                kode.Text = rs.Rows[0]["Kode"].ToString();
                nama.Text = rs.Rows[0]["NamaKategori"].ToString();
                project.SelectedValue = rs.Rows[0]["Project"].ToString();
            }
        }

        private bool unik()
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_RETENSI WHERE"
                + " Kode <> '" + Kode + "'"
                + " AND Kode = '" + Cf.Pk(kode.Text) + "'"
                );

            if (c != 0)
                x = false;

            return x;
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            //acc
            if (Cf.isEmpty(kode))
            {
                x = false;
                if (s == "") s = kode.ID;
                kodec.Text = "Kosong";
            }
            else
            {
                if (!unik())
                {
                    x = false;
                    if (s == "") s = kodec.ID;
                    kodec.Text = "Duplikat";
                }
                else
                    kodec.Text = "";
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Kode Bank tidak boleh kosong dan tidak boleh duplikat.\\n"
                    + "2. Nama Bank tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );
            }

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                string KodeBaru = Cf.Pk(kode.Text);
                string Nama = Cf.Str(nama.Text);

                DataTable rsBef = Db.Rs("SELECT "
                    + " Kode AS [Kode Retensi]"
                    + ",NamaKategori"
                    + ",Project"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_RETENSI "
                    + " WHERE Kode = '" + Kode + "' AND Project = '" + Project + "'");

                Db.Execute("EXEC spRetensiKPAEdit"
                    + " '" + Kode + "'"
                    + ",'" + KodeBaru + "'"
                    + ",'" + Nama + "'"
                    + ",'" + Project + "'"
                    );

                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_RETENSI SET Project = '" + project.SelectedValue + "' WHERE Kode = '" + KodeBaru + "' AND Project = '" + Project + "'");

                DataTable rsAft = Db.Rs("SELECT "
                    + " Kode AS [Kode Retensi]"
                    + ",NamaKategori"
                    + ",Project"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_RETENSI "
                    + " WHERE Kode = '" + KodeBaru + "' AND Project = '" + project.SelectedValue + "'");

                /*UPDATE Referensi Bank KPA di MS_KONTRAK*/
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK SET RetensiKPA='" + KodeBaru + "' WHERE RetensiKPA='" + Kode + "'");

                string KetLog = Cf.LogCompare(rsBef, rsAft);

                if (Kode != KodeBaru)
                {
                    Db.Execute("EXEC spLogRetensiKPA"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + KodeBaru + "'"
                        );
                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_RETENSI_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE REF_RETENSI_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                }
                else
                {
                    Db.Execute("EXEC spLogRetensiKPA"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + Kode + "'"
                        );
                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_RETENSI_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE REF_RETENSI_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                }

                return true;
            }
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            string KodeBaru = Cf.Pk(kode.Text);
            if (Save()) Response.Redirect("RetensiEdit.aspx?Kode=" + KodeBaru + "&done=1&project=" + project.SelectedValue);
        }

        private string Kode
        {
            get
            {
                return Cf.Pk(Request.QueryString["Kode"]);
            }
        }
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
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
