using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.ADMINJUAL
{
    public partial class TipeGimmick : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                init();

                Js.Focus(this, Nama);
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
                        + "<a href=\"javascript:popEditTipeGimmick('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
            }
        }

        private void Fill()
        {
            string strSql = "SELECT TOP 25 ID, Nama "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE Project IN (" + Act.ProjectListSql + ")"
                + " ORDER BY ID DESC";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["ID"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();

                baru.Items.Add(new ListItem(t, v));
            }

            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditTipeGimmick("
                    + "this.options[this.selectedIndex].value)";
            }
        }

        protected bool valid()
        {
            bool x = true;

            if (project.SelectedIndex == 0)
            {
                x = false;
            }

            if (Cf.isEmpty(Nama))
            {
                x = false;
            }
            else
            {
                if (Db.SingleInteger("SELECT Count(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE Nama = '" + Nama.Text + "'") > 0)
                {
                    x = false;
                }
            }

            if (!x)
                Js.Alert(this,
                    "Proses Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Project harus di pilih.\\n"
                    + "2. Nama Tipe tidak boleh kosong.\\n"
                    + "3. Nama Tipe sudah ada.\\n"
                    , "");

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                int ID = Db.SingleInteger("SELECT ISNULL(MAX(ID),0) AS ID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE Project = '" + project.SelectedValue + "'");
                if (ID == 0)
                {
                    ID = 1;
                }
                else
                {
                    ID = (ID + 1);
                }
                string NamaTipe = Cf.Str(Nama.Text);

                Db.Execute("INSERT INTO " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK Values(" + ID + ",'" + NamaTipe + "','" + project.SelectedValue + "')");

                noid.Text = Db.SingleInteger("SELECT TOP 1 ID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE Project = '" + project.SelectedValue + "'").ToString();

                DataTable rs = Db.Rs("SELECT "
                    + "Nama as [Nama Tipe]"
                    + ",Project as [Project]"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK"
                    + " WHERE ID = " + ID
                    );
                    
                Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogTipeGimmick"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    + ",'" + ID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

                Response.Redirect("TipeGimmick.aspx?done=" + ID);
            }
        }

        protected void init()
        {
            Act.ProjectList(project);
        }
    }
}