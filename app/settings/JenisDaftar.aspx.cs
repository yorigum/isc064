using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class JenisDaftar : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton cancel;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                InitForm();
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
                            + "<a href=\"javascript:popEditJenis('" + Request.QueryString["done"] + "')\">"
                            + "Pendaftaran Berhasil..."
                            + "</a>";
                        project.SelectedValue = Request.QueryString["project"];
                        baru.Items.Clear();
                        Fill();

                    }
                }
            }
        }

        private void InitForm()
        {
            Act.ProjectList(project);
            Js.Focus(this, jenis);
        }

        private void AutoID()
        {
            int c = Db.SingleInteger("SELECT COUNT(SN) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS");

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                nojenis.Text = c.ToString();

                if (isUnique()) hasfound = true;
            }
        }

        private bool isUnique()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS WHERE SN = '" + NoJenis + "'");

            if (c != 0)
                x = false;

            return x;
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

            if (file.PostedFile.FileName.Length != 0 && !file.PostedFile.FileName.EndsWith(".jpg"))
            {
                x = false;

                if (s == "")
                    s = file.ID;
            }

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Jenis Unit Properti tidak boleh kosong.\\n"
                    + "2. Nama Jenis Unit tidak boleh kosong.\\n"
                    + "3. File yang boleh di - upload adalah file dengan extension.jpg saja.\\n"
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
                string Jenis = Cf.Pk(jenis.Text);
                string SN = Cf.Pk(nojenis.Text);
                string NamaJenis = namajenis.Text;
                string Project = project.SelectedValue;
                string save = "";
                if (file.PostedFile.FileName.Length != 0)
                {
                    string path = "D:\\ISC\\ISC064\\app\\settings\\gambar\\" + Act.UserID + ".png";
                    save = "settings\\gambar\\" + Act.UserID + ".png";
                    Dfc.UploadFile(".jpg", path, file);
                }

                int c = Db.SingleInteger("SELECT COUNT(Jenis) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS WHERE Jenis = '" + Jenis + "' AND Project = '" + Project + "'");
                if (c == 1)
                {
                    nojenis.Text = "#AUTO#";
                    jenisc.Text = "Duplikat";

                    Js.Alert(
                        this
                        , "Unit Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Jenis Unit sudah ada.\\n"
                        , "document.getElementById('nounit').focus();"
                        + "document.getElementById('nounit').select();"
                        );
                    // Response.Write(Jenis + NamaJenis + SN);
                }
                else
                {
                    Db.Execute("INSERT INTO " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS (Jenis,Nama,SN,Project,Gambar) Values('" + Jenis + "','" + NamaJenis + "','" + SN + "','" + Project + "','" + save + "')");

                    DataTable rs = Db.Rs("SELECT "
                                  + " Jenis"
                                  + ",Project"
                                  + ",Nama"
                                  + ",SN"
                                  + ",Gambar"
                                  + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS "
                                  + " WHERE SN = '" + SN + "'");

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogJenis"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    + ",'" + SN + "'"
                    );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                    Response.Redirect("JenisDaftar.aspx?done=" + NoJenis + "&project=" + project.SelectedValue);
                }
            }
        }

        private void Fill()
        {
            string strSql = "SELECT TOP 25 Jenis,SN, Nama "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS "
                + " WHERE Project = '" + project.SelectedValue + "'"
                + " ORDER BY SN DESC";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["SN"].ToString();
                string x = rs.Rows[i]["Jenis"].ToString();
                string t = x + " (" + rs.Rows[i]["Nama"] + ")";

                baru.Items.Add(new ListItem(t, v));
            }

            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditJenis("
                    + "this.options[this.selectedIndex].value)";
            }
        }

        private string NoJenis
        {
            get
            {
                return Cf.Pk(nojenis.Text);
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
