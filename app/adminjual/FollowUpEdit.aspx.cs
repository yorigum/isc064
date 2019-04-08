using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
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
            //btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=REF_LOKASI_LOG&Pk=" + NoLokasi + "'";
            //btndel.Attributes["onclick"] = "location.href='FollowUp.aspx?NoLokasi=" + NoLokasi + "'";

            string strSql = "SELECT * FROM MS_FOLLOWUP WHERE No = '" + No + "'";

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nolokasi.Text = rs.Rows[0]["No"].ToString();
                lokasi.Text = rs.Rows[0]["Nama"].ToString();
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
                Db.Execute("UPDATE MS_FOLLOWUP SET Nama='" + Cf.Str(lokasi.Text) + "' WHERE No='" + No + "'");
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
            Response.Redirect("FollowUpEdit.aspx?done=1&No=" + No);
        }

        private string No
        {
            get
            {
                return Cf.Pk(Request.QueryString["No"]);
            }
        }
    }
}