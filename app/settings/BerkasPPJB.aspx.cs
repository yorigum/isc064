using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace ISC064.SETTINGS
{
    public partial class BerkasPPJB : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton cancel;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Js.Focus(this, namaberkas);
                Act.ProjectList(project);                
            }

            FeedBack();
            Fill();
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
                            + "<a href=\"javascript:popEditBerkasPPJB('" + Request.QueryString["done"] + "')\">"
                            + "Pendaftaran Berhasil..."
                            + "</a>";
                    }
                    project.SelectedValue = Request.QueryString["project"];
                }
            }
        }

        private void AutoID()
        {
            int c = Db.SingleInteger("SELECT COUNT(SN) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB");

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                noberkas.Text = c.ToString();

                if (isUnique()) hasfound = true;
            }
        }

        private bool isUnique()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB WHERE SN = '" + NoBerkas + "'");

            if (c != 0)
                x = false;

            return x;
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
                    + "2. Nama Berkas tidak boleh kosong.\\n"
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
                //string Lokasi = Cf.Pk(lokasi.Text);
                string SN = Cf.Pk(noberkas.Text);
                string NamaBerkas = namaberkas.Text;

                int c = Db.SingleInteger("SELECT COUNT(Nama) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB WHERE Nama = '" + NamaBerkas + "' AND Project = '" + project.SelectedValue + "'");
                if (c == 1)
                {
                    noberkas.Text = "#AUTO#";
                    namaberkasc.Text = "Duplikat";

                    Js.Alert(
                        this
                        , "Unit Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Nama Berkas sudah ada.\\n"
                        , "document.getElementById('nounit').focus();"
                        + "document.getElementById('nounit').select();"
                        );
                }
                else
                {
                    Db.Execute("INSERT INTO " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB (Nama,SN,Project) VALUES('" + NamaBerkas + "','" + SN + "','" + project.SelectedValue + "')");
                    DataTable rs = Db.Rs("SELECT "
                                 + " Nama"
                                 + ",SN"
                                 + ",Project"
                                 + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB "
                                 + " WHERE SN = '" + SN + "'");

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogBerkasPPJB"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    + ",'" + SN + "'"
                    );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                    Response.Redirect("BerkasPPJB.aspx?done=" + NoBerkas + "&project=" + project.SelectedValue);
                }
            }
        }

        private void Fill()
        {
            string strSql = "SELECT TOP 25 SN, Nama "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BERKAS_PPJB WHERE Project = '" + project.SelectedValue + "'"
                + " ORDER BY SN DESC";

            baru.Items.Clear();
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["SN"].ToString();
                //string x = rs.Rows[i]["Lokasi"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();

                baru.Items.Add(new ListItem(t, v));
            }

            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditBerkasPPJB("
                    + "this.options[this.selectedIndex].value)";
            }
        }

        private string NoBerkas
        {
            get
            {
                return Cf.Pk(noberkas.Text);
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