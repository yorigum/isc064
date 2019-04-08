using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class ClosingNUP3 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
            }
        }

        private void Fill()
        {
            string strSql = "SELECT * FROM MS_NUP_PRIORITY WHERE NoNUPHeader = '" + NoNUP + "' AND Tipe = '" + Tipe + "'";
            DataTable rsNUP = Db.Rs(strSql);

            if (rsNUP.Rows.Count > 0)
            {
                for (int i = 0; i < rsNUP.Rows.Count; i++)
                {
                    //No
                    Label l;
                    TextBox bx;
                    TextBox bx2;
                    HtmlInputButton btn;
                    DropDownList ddl;
                    l = new Label();
                    l.Text = "<tr>"
                        + "<td>" + (i + 1).ToString() + ".</td>";
                    list.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td>";
                    list.Controls.Add(l);

                    bx = new TextBox();
                    bx.ID = "nup_" + i;
                    bx.Text = rsNUP.Rows[i]["NoNUP"].ToString();
                    bx.ReadOnly = true;
                    list.Controls.Add(bx);
                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td>";
                    list.Controls.Add(l);

                    bx = new TextBox();
                    bx.ID = "namacustomer_" + i;
                    bx.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer IN(SELECT NoCustomer FROM MS_NUP WHERE NoNUP='" + rsNUP.Rows[i]["NoNUP"].ToString() + "' AND Tipe='" + Tipe + "')");
                    bx.ReadOnly = true;
                    list.Controls.Add(bx);

                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td>";
                    list.Controls.Add(l);

                    bx = new TextBox();
                    bx.ID = "unit_" + i;
                    bx.Text = rsNUP.Rows[i]["NoStock"].ToString();
                    bx.Attributes["style"] = "display:none;";
                    list.Controls.Add(bx);


                    bx2 = new TextBox();
                    bx2.ID = "nounit_" + i;
                    bx2.ReadOnly = true;
                    bx2.Text = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock ='" + rsNUP.Rows[i]["NoStock"].ToString() + "'");
                    list.Controls.Add(bx2);


                    btn = new HtmlInputButton();
                    btn.ID = "btnunit_" + i;
                    btn.Attributes["class"] = "search";
                    btn.Attributes["onclick"] =
                    "openSite('" + bx.ID + "', '" + bx2.ID + "')";
                    btn.Attributes["style"] = "display:none;";
                    list.Controls.Add(btn);

                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td>";
                    list.Controls.Add(l);

                    ddl = new DropDownList();
                    ddl.ID = "carabayar_" + i;
                    ddl.Width = 240;
                    DataTable rsCB = Db.Rs("SELECT * FROM REF_SKEMA WHERE Status='A'");
                    for (int aa = 0; aa < rsCB.Rows.Count; aa++)
                    {
                        ddl.Items.Add(new ListItem("Pilih Cara Bayar", ""));
                        ddl.Items.Add(new ListItem(rsCB.Rows[aa]["Nama"].ToString(), rsCB.Rows[aa]["Nomor"].ToString()));

                    }
                    ddl.Enabled = false;
                    ddl.SelectedValue = rsNUP.Rows[i]["NomorSkema"].ToString();

                    list.Controls.Add(ddl);

                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);

                    int NoCustomer = 0;
                    NoCustomer = Db.SingleInteger("SELECT NoCustomer FROM MS_NUP WHERE NoNUP='" + rsNUP.Rows[i]["NoNUP"] + "' AND Tipe = '" + Tipe + "'");

                    l = new Label();
                    if (rsNUP.Rows[i]["NoKontrak"].ToString() == "")
                        l.Text = "<td><a href='ClosingEdit.aspx?&NoNUPHeader=" + NoNUP + "&NoNUP=" + rsNUP.Rows[i]["NoNUP"] + "&NoStock=" + rsNUP.Rows[i]["NoStock"] + "&NoCustomer=" + NoCustomer + "&NoSkema=" + rsNUP.Rows[i]["NomorSkema"] + "&Tipe=" + Tipe + "&project=" + Project + "'>Closing</a>";
                    else
                        l.Text = "<td>" + rsNUP.Rows[i]["NoKontrak"].ToString();
                    list.Controls.Add(l);

                    list.Controls.Add(bx);

                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);
                }

            }
        }

        private bool valid()
        {
            bool x = true;
            string NoKTP = "";
            NoKTP = Db.SingleString("SELECT NoKTP FROM MS_NUP WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Tipe + "'");
            int Count = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_NUP WHERE NoKTP = '" + NoKTP + "'");
            for (int a = 0; a < Count; a++)
            {
                TextBox unit = (TextBox)list.FindControl("unit_" + a);
                DropDownList carabayar = (DropDownList)list.FindControl("carabayar_" + a);
                if (unit.Text == "")
                    x = false;

                if (carabayar.SelectedIndex == 0)
                    x = false;
            }


            if (!x)
                Js.Alert(
                    this
                    , "NUP Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. NUP tersebut tidak terdaftar.\\n"
                    + "2. NUP tersebut sudah memilih unit.\\n"
                    , "document.getElementById('nonup').focus();"
                    + "document.getElementById('nonup').select();"
                    );

            return x;
        }

        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClosingNUP2.aspx?No=" + NoNUP + "&Tipe=" + Tipe);
        }

        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }
    }
}