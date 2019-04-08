using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
    public partial class FollowUpEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
            Js.Confirm(this, "Lanjutkan proses edit data follow up?\\n"
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
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?tb=ISC064_MARKETINGJUAL..REF_FOLLOWUP_LOG&Pk=" + No + "'";
            btndel.Attributes["onclick"] = "location.href='FollowUpDel.aspx?No=" + No + "'";

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_FOLLOWUP WHERE No = '" + No + "'";

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nolokasi.Text = rs.Rows[0]["No"].ToString();
                lokasi.Text = rs.Rows[0]["NamaGrouping"].ToString();
                Cf.SelectedValue(project, rs.Rows[0]["Project"].ToString());
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(lokasi))
            {
                x = false;
                if (s == "") s = lokasi.ID;
                lokasic.Text = "Kosong";
            }
            else
                lokasic.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Nama tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private void Save()
        {
            if (valid())
            {
                DataTable rsBef = Db.Rs("SELECT "
                            + "NamaGrouping"
                            + ",Project"
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_followup "
                            + " WHERE No= '" + No + "'");

                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_FOLLOWUP SET NamaGrouping='" + Cf.Str(lokasi.Text) + "', Project = '" + project.SelectedValue + "' WHERE No='" + No + "'");

                DataTable rsAft = Db.Rs("SELECT "
                            + "NamaGrouping"
                            + ",Project"
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_followup "
                            + " WHERE No= '" + No + "'");

                //Logfile
                string Ket = Cf.LogCompare(rsBef,rsAft);

                Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogGrouping"
                    + " 'Edit'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + No + "'"
                    );
            }
        }

        protected void ok_Click(object sender, EventArgs e)
        {
            Save();
            Js.Close(this);
        }

        protected void save_Click(object sender, EventArgs e)
        {
            Save();
            Response.Redirect("FollowUpEdit.aspx?done=1&NoJenis=" + No);
        }

        private string No
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoJenis"]);
            }
        }
    }
}