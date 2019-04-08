using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{

    public partial class LevelSalesDaftar : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton cancel;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                BindTipe();
                BindParent();
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
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditLevelSales('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
            }
        }

        private void InitForm()
        {
            Act.ProjectList(project);
            Js.Focus(this, nama);
        }

        private void AutoID()
        {
            int c = Db.SingleInteger("SELECT COUNT(LevelID) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL");

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                nolevel.Text = c.ToString();

                if (isUnique()) hasfound = true;
            }
        }

        private bool isUnique()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL WHERE LevelID = '" + NoLevel + "'");

            if (c != 0)
                x = false;

            return x;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "";

            if (tipe.SelectedIndex < 0)
            {
                x = false;
                tipec.Text = "harus pilih";
            }

            int cek = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL WHERE ParentID = '" + parent.SelectedValue + "'");
            if (cek > 0)
            {
                x = false;
                parentc.Text = "Parent ID Sudah digunakan";
            }
            else
                parentc.Text = "harus pilih";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Nama tidak boleh kosong.\\n"
                    + "2. ParentID Sudah Digunakan.\\n"
                    + "3. ParentID harus dipilih."
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
                string Nama = Cf.Pk(nama.Text);
                string ID = Cf.Pk(nolevel.Text);
                string Tipe = tipe.SelectedValue;
                string Parent = parent.SelectedValue;
                string Project = project.SelectedValue;

                int c = Db.SingleInteger("SELECT COUNT(Nama) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL WHERE Nama = '" + Nama + "' AND Tipe='" + Tipe + "' AND Project = '" + Project + "'");
                if (c == 1)
                {
                    nolevel.Text = "#AUTO#";
                    namac.Text = "Duplikat";
                    parentc.Text = "Duplikat";

                    Js.Alert(
                        this
                        , "Tipe Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Nama sudah ada.\\n"
                        , "document.getElementById('nounit').focus();"
                        + "document.getElementById('nounit').select();"
                        );
                    // Response.Write(Jenis + NamaJenis + SN);
                }
                else
                {
                    Db.Execute("INSERT INTO " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL (Nama,Tipe,ParentID,Project) Values('" + Nama + "','" + Tipe + "','" + Parent + "','" + Project + "')");

                    DataTable rs = Db.Rs("SELECT "
                                  + " LevelID"
                                  + ",Nama"
                                  + ",Project"
                                  + ",Tipe"
                                  + ",ParentID"
                                  + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL "
                                  + " WHERE LevelID = '" + ID + "'");

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogLevelSales"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    + ",'" + ID + "'"
                    );

                    Response.Redirect("LevelSalesDaftar.aspx?done=" + NoLevel);
                }
            }
        }

        private void Fill()
        {
            string strSql = "SELECT TOP 25 LevelID,Nama "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL "
                + " WHERE Project IN (" + Act.ProjectListSql + ")"
                + " ORDER BY LevelID DESC";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["LevelID"].ToString();
                string x = v + " (" + rs.Rows[i]["Nama"] + ")";

                baru.Items.Add(new ListItem(x, v));
            }

            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditLevelSales("
                    + "this.options[this.selectedIndex].value)";
            }
        }

        private void BindTipe()
        {            
            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE WHERE Project = '"+project.SelectedValue+"' ";
            DataTable rs = Db.Rs(strSql);
            tipe.Items.Add(new ListItem { Text = "Tipe :", Value = "0" });

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["ID"].ToString();
                string t = rs.Rows[i]["Tipe"].ToString();
                tipe.Items.Add(new ListItem(t, v));
            }
        }

        private void BindParent()
        {
            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL WHERE Project = '" + project.SelectedValue + "' ";
            DataTable rs = Db.Rs(strSql);
            parent.Items.Add(new ListItem { Text = "Parent :", Value = "0" });

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["LevelID"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                parent.Items.Add(new ListItem(t, v));
            }
        }

        private string NoLevel
        {
            get
            {
                return Cf.Pk(nolevel.Text);
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
            tipe.Items.Clear();
            parent.Items.Clear();
            BindTipe();
            BindParent();
        }
    }
}