using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA
{
    public partial class Retensi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                FillTable();
                Js.Focus(this, kode);
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
                        + "<a href=\"javascript:popEditRetensi('" + Request.QueryString["done"] + "','" + Request.QueryString["project"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
                project.SelectedValue = Request.QueryString["project"];
                FillTable();
            }
        }

        private void FillTable()
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_RETENSI WHERE Project = '" + project.SelectedValue + "'");
            Rpt.NoData(sb, rs, "<font style='font:8pt'>Daftar Kategori Retensi belum di-setup.</font>");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                sb.Append("<li>"
                    + "<a href=\"javascript:popEditRetensi('" + rs.Rows[i]["Kode"].ToString() + "','" + rs.Rows[i]["Project"].ToString() + "')\">"
                    + rs.Rows[i]["Kode"] + " - " + rs.Rows[i]["NamaKategori"]
                    + "</a>"
                    + "</li>"
                    );
            }

            list.InnerHtml = sb.ToString();

        }

        private bool unik()
        {
            bool x = true;

            string Kode = Cf.Pk(kode.Text);
            int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_RETENSI WHERE"
                + " Kode = '" + Kode + "'"
                + " AND Project = '" + project.SelectedValue + "'"
                );

            if (c != 0)
                x = false;

            return x;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            kode.Text = Cf.Pk(kode.Text);

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
                    if (s == "") s = kode.ID;
                    kodec.Text = "Duplikat";
                }
                else
                    kodec.Text = "";
            }

            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }

            if (!x)
            {
                if (!x)
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

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                string Kode = Cf.Pk(kode.Text);
                string Nama = Cf.Str(nama.Text);

                Db.Execute("EXEC spRetensiKPABaru"
                    + " '" + Kode + "'"
                    + ",'" + Nama + "'"
                    + ",'" + project.SelectedValue + "'"
                    );

                DataTable rs = Db.Rs("SELECT "
                    + " Kode AS [Kode Retensi]"
                    + ",NamaKategori"
                    + ",Project"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_RETENSI "
                    + " WHERE Kode  = '" + Kode + "' AND Project  = '" + project.SelectedValue + "'");

                string KetLog = Cf.LogCapture(rs);

                Db.Execute("EXEC spLogRetensiKPA"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + Kode + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_RETENSI_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE REF_RETENSI_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                Response.Redirect("Retensi.aspx?done=" + Kode + "&project=" + project.SelectedValue);
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTable();
        }
    }
}
