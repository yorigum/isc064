using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{

    public partial class TipeSalesDaftar : System.Web.UI.Page
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
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditTipeSales('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
                    project.SelectedValue = Request.QueryString["project"];
                    baru.Items.Clear();
                    Fill();
                }
            }
        }

        private void InitForm()
        {
            Act.ProjectList(project);

            Js.Focus(this, tipe);
        }

        private void AutoID()
        {
            int c = Db.SingleInteger("SELECT COUNT(ID) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE");

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                notipe.Text = c.ToString();

                if (isUnique()) hasfound = true;
            }
        }

        private bool isUnique()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE WHERE ID = '" + NoTipe + "'");

            if (c != 0)
                x = false;

            return x;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(tipe))
            {
                x = false;
                if (s == "") s = tipe.ID;
                tipec.Text = "Kosong";
            }
            else
                tipec.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Tipe Sales tidak boleh kosong.\\n"
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
                ////AutoID();
                string Tipe = Cf.Pk(tipe.Text);
                //string ID = Cf.Pk(notipe.Text);
                string Project = Cf.Pk(project.SelectedValue);

                int c = Db.SingleInteger("SELECT COUNT(Tipe) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE WHERE Tipe = '" + Tipe + "' AND Project = '" + Project + "'");
                if (c == 1)
                {
                    notipe.Text = "#AUTO#";
                    tipec.Text = "Duplikat";

                    Js.Alert(
                        this
                        , "Tipe Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Tipe Sales sudah ada.\\n"
                        , "document.getElementById('nounit').focus();"
                        + "document.getElementById('nounit').select();"
                        );
                }
                else
                {
                    Db.Execute("INSERT INTO " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE (Tipe, Project) Values('" + Tipe + "', '" + Project + "')");

                    int ID = Db.SingleInteger("SELECT TOP 1 ID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE ORDER BY ID DESC");

                    DataTable rs = Db.Rs("SELECT "
                                  + " ID"
                                  + ",Project"
                                  + ",Tipe"
                                  + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE "
                                  + " WHERE ID = '" + ID + "'");

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogTipeSales"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    + ",'" + ID + "'"
                    );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                    Response.Redirect("TipeSalesDaftar.aspx?done=" + ID + "&project=" + project.SelectedValue);
                }
            }
        }

        private void Fill()
        {
            string strSql = "SELECT TOP 25 ID,Tipe "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE "
                + " WHERE Project = '" + project.SelectedValue + "'"
                + " ORDER BY ID DESC";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["ID"].ToString();
                string x = v + " (" + rs.Rows[i]["Tipe"] + ")";

                baru.Items.Add(new ListItem(x, v));
            }

            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditTipeSales("
                    + "this.options[this.selectedIndex].value)";
            }
        }

        private string NoTipe
        {
            get
            {
                return Cf.Pk(notipe.Text);
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