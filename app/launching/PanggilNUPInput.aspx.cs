using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class PanggilNUPInput : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Bind();
            }
        }
        protected void Bind()
        {
            pilihhhh.Items.Clear();
            pilihhhh.Items.Add(new ListItem("Pilih : ", "0"));
            DataTable rs = Db.Rs("SELECT * FROM REF_JENISPROPERTI WHERE Project = '" + project.SelectedValue + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string a = rs.Rows[i]["SN"].ToString();
                string b = rs.Rows[i]["Nama"].ToString();
                pilihhhh.Items.Add(new ListItem(b));
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(input))
            {
                x = false;
                if (s == "") s = inputc.ID;
                inputc.Text = "*Harap Diisi";
            }
            else
                inputc.Text = "";

            if (pilihhhh.SelectedValue == "0")
            {
                x = false;
                Js.Alert(this, "Silakan Pilih Tipe Terlebih Dahulu!", "");
            }

            return x;
        }
        protected void display_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Response.Redirect("PanggilNUPTampil.aspx?j=" + input.Text + "&Tipe=" + pilihhhh.SelectedValue + "&project=" + project.SelectedValue);
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
            pilihhhh.Items.Clear();            
            Bind();
        }
    }
}
