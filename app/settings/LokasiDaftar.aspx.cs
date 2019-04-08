using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
    public partial class LokasiDaftar : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton cancel;

        protected void Page_Load(object sender, System.EventArgs e)
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
                {
                    if (Request.QueryString["done"] == "1")
                    {
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                            + "Edit Berhasil...";
                    }
                    else
                    {
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                            + "<a href=\"javascript:popEditLokasi('" + Request.QueryString["done"] + "')\">"
                            + "Pendaftaran Berhasil..."
                            + "</a>";
                        project.SelectedValue = Request.QueryString["project"];
                        baru.Items.Clear();
                        Fill();
                    }
                }
            }
        }

        private void AutoID()
        {
            int c = Db.SingleInteger("SELECT COUNT(SN) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI");

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
                "SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI WHERE SN = '" + NoLokasi + "'");

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
                    + "1. Lokasi Unit Properti tidak boleh kosong.\\n"
                    + "2. Nama Lokasi Unit tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;

        }


        protected void save_Click(object sender, System.EventArgs e)
        {

            if (valid())
            {
                //Generate nomor unik
                AutoID();
                string Lokasi = Cf.Pk(lokasi.Text);
                string SN = Cf.Pk(nolokasi.Text);
                string NamaLokasi = namalokasi.Text;
                string Project = Cf.Pk(project.SelectedValue);


                int c = Db.SingleInteger("SELECT COUNT(Lokasi) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI WHERE Lokasi = '" + Lokasi + "' AND Project = '" + Project + "'");
                if (c == 1)
                {
                    nolokasi.Text = "#AUTO#";
                    lokasic.Text = "Duplikat";

                    Js.Alert(
                        this
                        , "Unit Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Lokasi Unit sudah ada.\\n"
                        , "document.getElementById('nounit').focus();"
                        + "document.getElementById('nounit').select();"
                        );
                }
                else
                {
                    Db.Execute("INSERT INTO " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI (Lokasi,Nama,SN,Project) VALUES('" + Lokasi + "','" + NamaLokasi + "','" + SN + "','" + Project + "')");
                    DataTable rs = Db.Rs("SELECT "
                                 + " LOKASI"
                                 + ",Nama"
                                 + ",SN"
                                 + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI "
                                 + " WHERE SN = '" + SN + "'");

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogLokasi"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    + ",'" + SN + "'"
                    );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                    Response.Redirect("LokasiDaftar.aspx?done=" + NoLokasi + "&project=" + project.SelectedValue);

                }
            }
        }

        private void Fill()
        {
            string strSql = "SELECT TOP 25 Lokasi,SN, Nama "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI"
                + " WHERE Project = '" + project.SelectedValue + "'"
                + " ORDER BY SN DESC";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["SN"].ToString();
                string x = rs.Rows[i]["Lokasi"].ToString();
                string t = x + " (" + rs.Rows[i]["Nama"] + ")";

                baru.Items.Add(new ListItem(t, v));
            }

            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditLokasi("
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
            baru.Items.Clear();
            Fill();
        }
    }
}
