using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class ClosingNUP2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {

            }
            Fill();
        }

        private void Fill()
        {
            string strSql = "SELECT * FROM MS_NUP a INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer WHERE a.NoNUP = '" + NoNUP + "' AND a.Tipe = '" + Jenis + "' AND a.Project = '" + Project + "'";
            DataTable rsNUP = Db.Rs(strSql);
            int countPilihUnit = Db.SingleInteger("SELECT ISNULL(COUNT(*), 0) FROM MS_NUP_PRIORITY WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Jenis + "' AND Project = '" + Project + "'");

            if (rsNUP.Rows[0]["Status"].ToString() == "3")
            {
                int Countbayar2 = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_NUP_PELUNASAN WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Jenis + "' AND Project = '" + Project + "'");
                if (Countbayar2 != 2)
                {
                    save.Enabled = false;
                    error.Text = "Harap melunasi Booking Fee";
                }

            }

            if (rsNUP.Rows.Count > 0)
            {
                nama.Text = "<font color='green'>" + Cf.Str(rsNUP.Rows[0]["Nama"]) + "</font>";
                string a = "";
                DataTable bind = Db.Rs("SELECT a.* FROM MS_NUP a WHERE a.NoNUP='" + NoNUP + "' AND a.Tipe = '" + Jenis + "' AND a.Project = '" + Project + "'");

                int countkontrak = 0;
                if (bind.Rows.Count > 0)
                    countkontrak = Db.SingleInteger("SELECT ISNULL(COUNT(NoKontrak), 0) FROM MS_NUP_PRIORITY WHERE NoNUP = '" + bind.Rows[0]["NoNUP"].ToString() + "' AND NoKontrak != '' AND Tipe = '" + Jenis + "'  AND Project = '" + Project + "'");

                //No
                Label l;
                TextBox bx;
                TextBox bx2;
                HtmlInputButton btn;
                DropDownList ddl;
                LinkButton btnl;

                l = new Label();
                l.Text = "<tr>"
                    + "<td>" + 1 + ".</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "nup_";
                bx.Text = bind.Rows[0]["NoNUP"].ToString();
                bx.ReadOnly = true;
                if (countkontrak > 0)
                    bx.Enabled = false;
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "bayarnup_";
                bx.Text = Cf.Num(Convert.ToDecimal(Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Jenis + "' AND Project = '" + Project + "'")));//Convert.ToDecimal(bind.Rows[0]["Bayar"].ToString()));
                bx.ReadOnly = true;
                if (countkontrak > 0)
                    bx.Enabled = false;
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td style='display:none'>";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "unit_";
                bx.Attributes["style"] = "display:none;";
                string NoStock = "";
                DataTable dtPrior = Db.Rs("SELECT * FROM MS_NUP_PRIORITY WHERE NoNUP = '" + bind.Rows[0]["NoNUP"].ToString() + "' AND Tipe= '" + Jenis + "' AND Project = '" + Project + "'");
                if (dtPrior.Rows.Count > 0)
                    NoStock = dtPrior.Rows[0]["NoStock"].ToString();
                bx.Text = NoStock;
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                bx2 = new TextBox();
                string NoUnit = "";
                if (NoStock != "")
                {
                    NoUnit = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                }
                bx2.ID = "nounit_";
                bx2.Text = NoUnit;
                if (countkontrak > 0)
                    bx2.Enabled = false;
                bx2.ReadOnly = true;
                list.Controls.Add(bx2);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);


                string Tipe = "";
                if (NoStock != "")
                    Tipe = Db.SingleString("SELECT Jenis FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                bx2 = new TextBox();
                bx2.ID = "tipe_";
                bx2.Text = Tipe;
                if (countkontrak > 0)
                    bx2.Enabled = false;
                bx2.ReadOnly = true;
                list.Controls.Add(bx2);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                if (NoStock == "")
                {
                    btnl = new LinkButton();
                    btnl.ID = "BtnLink_" + NoNUP + "_" + Jenis + "_" + Project;
                    btnl.Text = "Pilih Site Plan";
                    btnl.Width = 120;
                    btnl.Click += new EventHandler(PilihCB);
                    list.Controls.Add(btnl);
                }

                l = new Label();
                if (countkontrak == 0)
                {
                    if (NoStock != "")
                    {

                        l.Text = "&nbsp;<a href=\"UnitCancel.aspx?NoNUP=" + NoNUP + "&NoPil=" + bind.Rows[0]["NoNUP"].ToString() + "&NoStock=" + NoStock + "&Tipe=" + Jenis + "&project=" + Project + "\">Cancel Unit..</a>";
                    }
                    else
                    {
                        l.Text = "";
                    }
                }
                else
                {
                    l.Text = dtPrior.Rows[0]["NoKontrak"].ToString();
                }
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                int countPr = Db.SingleInteger("SELECT Count(*) FROM MS_NUP_PRIORITY WHERE NoNUP = '" + NoNUP + "' AND NomorSkema != 0");

                if (countPr > 0)
                {
                    decimal Harga = Db.SingleDecimal("SELECT Harga FROM MS_NUP_PRIORITY WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Jenis + "'");
                    btnl = new LinkButton();
                    btnl.ID = "BtnLink2_" + NoNUP + "_" + Jenis + "_" + Harga;
                    btnl.Text = "Print NUP";
                    btnl.Width = 120;
                    btnl.Click += new EventHandler(PilihCB2);
                    list.Controls.Add(btnl);

                    l = new Label();
                    l.Text = "<td>";
                    list.Controls.Add(l);
                }

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);
            }
        }
        protected void PilihCB(object sender, System.EventArgs e)
        {
            LinkButton btnl = (LinkButton)sender;
            DropDownList ddl = (DropDownList)list.FindControl("carabayar_");
            string[] Data = btnl.ID.Split('_');
            string Tower = Data[1];
            string NoNup = Data[2];
            Response.Redirect("UnitPetaDetil.aspx?id=1010&NoNUP=" + NoNUP + "&Tipe=" + Jenis);
        }
        protected void PilihCB2(object sender, System.EventArgs e)
        {
            LinkButton btnl = (LinkButton)sender;
            string[] Data = btnl.ID.Split('_');
            string NoNup = Data[1];
            string Harga = Data[3];
            Response.Redirect("UnitPilih2.aspx?No=" + NoNUP + "&Tipe=" + Jenis);
        }

        private bool valid()
        {
            bool x = true;
            string NoKTP = "";

            TextBox unit = (TextBox)list.FindControl("nounit_");
            DropDownList carabayar = (DropDownList)list.FindControl("carabayar_");
            if (unit.Text != "")
            {
                x = true;
            }


            if (!x)
                Js.Alert(
                    this
                    , "NUP Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Unit belum di pilih.\\n"
                    + "2. Cara Bayar belum di pilih.\\n"
                    , "document.getElementById('nonup').focus();"
                    + "document.getElementById('nonup').select();"
                    );

            return x;
        }
        private bool valid2()
        {
            bool x = true;

            TextBox unit = (TextBox)list.FindControl("nounit_");
            DropDownList carabayar = (DropDownList)list.FindControl("carabayar_");
            if (unit.Text == "")
            {
                x = true;
                if (carabayar.SelectedIndex == 0)
                    x = false;
            }


            if (!x)
                Js.Alert(
                    this
                    , "NUP Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Unit belum di pilih.\\n"
                    + "2. Cara Bayar belum di pilih.\\n"
                    , "document.getElementById('nonup').focus();"
                    + "document.getElementById('nonup').select();"
                    );

            return x;
        }

        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["No"]);
            }
        }
        private string Jenis
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            save.Enabled = false;
            if (valid())
            {
                TextBox unit = (TextBox)list.FindControl("unit_");
                //DropDownList carabayar = (DropDownList)list.FindControl("carabayar_");
                TextBox nup = (TextBox)list.FindControl("nup_");
                //Db.Execute("UPDATE MS_NUP_PRIORITY SET NomorSkema ='" + carabayar.SelectedValue + "' WHERE NoNUP = '" + nup.Text + "' AND Tipe = '" + Jenis + "'");

                Db.Execute("UPDATE MS_NUP SET FlagStatus=4 WHERE NoNUP ='" + nup.Text + "' AND Tipe = '" + Jenis + "'");

                Response.Redirect("ClosingNUP3.aspx?NoNUP=" + NoNUP + "&Tipe=" + Jenis + "&project=" + Project);
            }
        }
        protected void submitubah_Click1(object sender, EventArgs e)
        {
            TextBox nup = (TextBox)list.FindControl("nup_");
            TextBox Tipe = (TextBox)list.FindControl("tipe_");

            DataTable rsBef = Db.Rs("SELECT "
                + " NoNUP AS [No. NUP]"
                + ", Status AS [Jenis NUP]"
                + " FROM MS_NUP"
                + " WHERE NoNUP = '" + nup.Text + "' AND Tipe = '" + Jenis + "'"
                );

            //ubah status dan jenis
            Db.Execute("UPDATE MS_NUP SET Status = 'P', FlagStatus = 1 WHERE NoNUP ='" + nup.Text + "' AND Tipe = '" + Jenis + "'");

            DataTable rsAft = Db.Rs("SELECT "
                + " NoNUP AS [No. NUP]"
                + ", Status AS [Jenis NUP]"
                + " FROM MS_NUP"
                + " WHERE NoNUP = '" + nup.Text + "' AND Tipe = '" + Jenis + "'"
                );

            //Logfile
            string Ket = "No NUP : " + nup.Text + "<br>"
                + "Keterangan rubah : " + ketubah.Text + "<br>"
                + Cf.LogCompare(rsBef, rsAft);

            Db.Execute("EXEC spLogNUP"
                + " 'EDIT'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + nup.Text + "'"
                + ",'" + Jenis + "'"
                );

            Response.Redirect("ClosingNUP.aspx");
        }
    }
}