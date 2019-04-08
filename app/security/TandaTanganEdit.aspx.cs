using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
    public partial class TandaTanganEdit : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, System.EventArgs e)
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

            Fill();

            FeedBack();
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
            dok.Text = Dokumen;

            list.Controls.Clear();

            rs = Db.Rs("SELECT * FROM REF_SIGN WHERE Dokumen = '" + Dokumen + "'");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                //No
                Label l;
                TextBox bx;

                l = new Label();
                l.Text = "<tr>"
                    + "<td>" + rs.Rows[i]["SN"].ToString() + ".</td>";
                list.Controls.Add(l);

                //Nama
                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "nama_" + Convert.ToString(i);
                bx.Width = 140;
                bx.CssClass = "txt";
                bx.Text = rs.Rows[i]["Nama"].ToString();
                bx.MaxLength = 100;
                bx.Attributes["style"] = "font:8pt";
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                //Jabatan
                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "jabatan_" + Convert.ToString(i);
                bx.Width = 140;
                bx.CssClass = "txt";
                bx.Text = rs.Rows[i]["Jabatan"].ToString();
                bx.MaxLength = 100;
                bx.Attributes["style"] = "font:8pt";
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                l = new Label();
                l.ID = "err_" + Convert.ToString(i);
                l.CssClass = "err";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</td></tr>";
                list.Controls.Add(l);
            }
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TextBox nama = (TextBox)list.FindControl("nama_" + i);
                TextBox jabatan = (TextBox)list.FindControl("jabatan_" + i);
                Label err = (Label)list.FindControl("err_" + i);

                if (Cf.isEmpty(nama))
                {
                    x = false;
                    if (s == "") s = nama.ID;
                    err.Text = "Kosong";
                }
                else if (Cf.isEmpty(jabatan))
                {
                    x = false;
                    if (s == "") s = jabatan.ID;
                    err.Text = "Kosong";
                }
            }

            if (!x)
            {
                this.RegisterStartupScript(
                    "focusScript"
                    , "<script type='text/javascript'>"
                    + " document.getElementById('" + s + "').focus();"
                    + " document.getElementById('" + s + "').select();"
                    + "</script>"
                    );
            }

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                DataTable rsBef = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,SN) + '.   ' + Nama + ' ('+Jabatan+') '"
                    + "FROM REF_SIGN WHERE Dokumen = '" + Dokumen + "' ORDER BY SN");

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    TextBox nama = (TextBox)list.FindControl("nama_" + i);
                    TextBox jabatan = (TextBox)list.FindControl("jabatan_" + i);

                    int NoUrut = Convert.ToInt32(rs.Rows[i]["SN"]);

                    string Nama = nama.Text;
                    string Jabatan = jabatan.Text;

                    Db.Execute("EXEC spRefSignEdit "
                        + " '" + Dokumen + "'"
                        + ", " + NoUrut
                        + ",'" + Nama + "'"
                        + ",'" + Jabatan + "'"
                        );
                }

                DataTable rsAft = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,SN) + '.   ' + Nama + ' ('+Jabatan+') '"
                    + "FROM REF_SIGN WHERE Dokumen = '" + Dokumen + "' ORDER BY SN");

                string Ket = Dokumen + "<br>"
                    + "<br>---EDIT TANDA TANGAN---<br>"
                    + Cf.LogList(rsBef, rsAft, "TANDA TANGAN");

                Db.Execute("EXEC spLogRefSign"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + Dokumen + "'"
                    );

                return true;
            }
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("TandaTanganEdit.aspx?Dokumen=" + Dokumen + "&done=1");
        }

        private string Dokumen
        {
            get
            {
                return Cf.Pk(Request.QueryString["Dokumen"]);
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
    }
}
