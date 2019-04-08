using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
    public partial class FollowUp : System.Web.UI.Page
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
                        + "<a href=\"javascript:popEditFollowUp('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
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

        protected void save_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                string Nama = Cf.Pk(lokasi.Text);
                string No = Cf.Pk(nolokasi.Text);
                string Project = Cf.Pk(project.SelectedValue);

                int c = Db.SingleInteger("SELECT COUNT(NamaGrouping) FROM ISC064_MARKETINGJUAL..REF_FOLLOWUP WHERE NamaGrouping = '" + Nama + "' AND Project = '" + Project + "'");
                if (c > 1)
                {
                    nolokasi.Text = "#AUTO#";
                    lokasic.Text = "Duplikat";

                    Js.Alert(
                        this
                        , "Unit Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Nama Follow Up sudah ada.\\n"
                        , "document.getElementById('nounit').focus();"
                        + "document.getElementById('nounit').select();"
                        );
                }
                else
                {
                    Db.Execute("INSERT INTO ISC064_MARKETINGJUAL..REF_FOLLOWUP(NamaGrouping, Project) VALUES('" + Nama + "', '" + Project + "')");
                    int NoGr = Db.SingleInteger("SELECT Top 1 No FROM "+Mi.DbPrefix+"MARKETINGJUAL..REF_FOLLOWUP ORDER BY No DESC");
                    DataTable rs = Db.Rs("SELECT "                                
                                + "NamaGrouping"
                                + ",Project"                                
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_followup "
                                + " WHERE No= '" + NoGr + "'");

                    //Logfile
                    string Ket = Cf.LogCapture(rs);

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogGrouping"
                        + " 'DAFTAR'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoGr + "'"
                        );
                    Response.Redirect("FollowUp.aspx?done=" + NoGr);
                }
            }
        }

        private void Fill()
        {
            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..REF_FOLLOWUP WHERE Project IN (" + Act.ProjectListSql + ") ORDER BY No DESC";

            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["No"].ToString();
                string x = rs.Rows[i]["NamaGrouping"].ToString();
                string t = "(" + v + ") " + x;

                baru.Items.Add(new ListItem(t, v));
            }

            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditFollowUp("
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
    }
}