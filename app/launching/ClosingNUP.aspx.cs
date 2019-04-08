using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class ClosingNUP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Js.Focus(this, nonup);

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Bind();
            }
        }
        protected void Bind()
        {
            jenis.Items.Clear();
            jenis.Items.Add(new ListItem("Pilih : "));
            DataTable rs = Db.Rs("SELECT * FROM REF_JENISPROPERTI WHERE Project = '" + project.SelectedValue + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string a = rs.Rows[i]["SN"].ToString();
                string b = rs.Rows[i]["Nama"].ToString();
                jenis.Items.Add(new ListItem(b,b));
            }
            jenis.SelectedValue = "RUMAH";
        }
        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_NUP WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + jenis.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");

            if (jenis.SelectedIndex == 0)
            {
                x = false;
            }
            else
            {
                if (nonup.Text != "")
                {
                    if (c != 0)
                    {
                        int Status = Db.SingleInteger(
                            "SELECT Status FROM MS_NUP WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + jenis.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");
                        if (Status == 0 || Status == 5)
                        {
                            x = false;
                        }


                    }
                }
            }

            if (c == 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "NUP Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. NUP tersebut tidak terdaftar.\\n"
                    + "2. NUP tersebut sudah memilih unit.\\n"
                    + "3. NUP belum di Aktivasi.\\n"
                    + "4. Pilih Tipe Properti.\\n"
                    + "5. Pilih NoNup.\\n"
                    + "6. Nup Sudah di Refund.\\n"
                    + "5. Nup Belum melakukan pelunasan.\\n"
                    , "document.getElementById('nonup').focus();"
                    + "document.getElementById('nonup').select();"
                    );

            return x;
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Response.Redirect("ClosingNUP2.aspx?No=" + NoNUP + "&Tipe=" + Tipe + "&project=" + project.SelectedValue);

            }
        }

        private string NoNUP
        {
            get
            {
                return Cf.Pk(nonup.Text);
            }
        }
        private string Tipe
        {
            get
            {
                return Cf.Pk(jenis.SelectedValue);
            }
        }


        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            jenis.Items.Clear();
            Bind();
        }
    }
}