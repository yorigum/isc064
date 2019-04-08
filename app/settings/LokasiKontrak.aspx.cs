using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class LokasiKontrak : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton cancel;

        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Js.Focus(this, lokasi);

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
                        + "<a href=\"javascript:popEditLokasiKontrak('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
                project.SelectedValue = Request.QueryString["project"];
                baru.Items.Clear();
                Fill();
            }
        }

        private void AutoID()
        {
            int c = Db.SingleInteger("SELECT COUNT(SN) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_KONTRAK");

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                nolokasi.Text = c.ToString();

                if (isUnique()) hasfound = true;
            }
        }

        private bool isUnique()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_KONTRAK WHERE SN = '" + NoLokasi + "'");

            if (c != 0)
                x = false;

            return x;
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

            if (Cf.isEmpty(namalokasi))
            {
                x = false;
                if (s == "") s = namalokasi.ID;
                namalokasic.Text = "Kosong";
            }
            else
                namalokasic.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Lokasi Penjualan tidak boleh kosong.\\n"
                    + "2. Nama Lokasi Penjualan tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                //Generate nomor unik
                AutoID();
                string Lokasi = Cf.Pk(lokasi.Text);
                string SN = Cf.Pk(nolokasi.Text);
                string NamaLokasi = namalokasi.Text;

                int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_KONTRAK WHERE Nama = '" + NamaLokasi + "' AND Project = '" + project.SelectedValue + "'");
                if (c > 0)
                {
                    nolokasi.Text = "#AUTO#";
                    namalokasic.Text = "Duplikat";

                    Js.Alert(
                        this
                        , "Unit Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Nama Lokasi Kontrak sudah ada.\\n"
                        , "document.getElementById('nounit').focus();"
                        + "document.getElementById('nounit').select();"
                        );
                }
                else
                {
                    Db.Execute("INSERT INTO " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_KONTRAK (Lokasi,Nama,SN,Project) VALUES('" + Lokasi + "','" + NamaLokasi + "','" + SN + "','" + project.SelectedValue + "')");
                    DataTable rs = Db.Rs("SELECT "
                                 + " LOKASI"
                                 + ",Nama"
                                 + ",SN"
                                 + ",Project"
                                 + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_KONTRAK "
                                 + " WHERE SN = '" + SN + "'");

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogLokasiKontrak"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    + ",'" + SN + "'"
                    );
                    Response.Redirect("LokasiKontrak.aspx?done=" + NoLokasi + "&project=" + project.SelectedValue);
                }
            }
        }

        private void Fill()
        {
            string strSql = "SELECT TOP 25 Lokasi,SN, Nama "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_KONTRAK WHERE Project = '" + project.SelectedValue + "'"
                + " ORDER BY SN DESC";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["SN"].ToString();
                string x = rs.Rows[i]["Lokasi"].ToString();
                string t = x + " (" + rs.Rows[i]["Nama"] + ")";

                baru.Items.Add(new ListItem(t, v));
            }

            //Edit lokasi diperbaharui
            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditLokasiKontrak("
                    + "this.options[this.selectedIndex].value)";
            }
        }

        private string NoLokasi
        {
            get
            {
                return Cf.Pk(nolokasi.Text);
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            baru.Items.Clear();
            Fill();
        }
    }
}