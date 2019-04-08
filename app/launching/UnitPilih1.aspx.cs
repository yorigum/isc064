using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.AspNet.SignalR;

namespace ISC064.LAUNCHING
{
    public partial class UnitPilih1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Fill();
                Bind();
                nostock2.Text = Request.QueryString["NoStock"];
            }
            FeedBack();
        }

        protected void Bind()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_UNIT where Nostock = '" + NoStock + "'");
            bool SifatPPN = Convert.ToBoolean(rs.Rows[0]["SifatPPN"]);
            string JenisProperti = rs.Rows[0]["JenisProperti"].ToString();
            DataTable rsCB = Db.Rs("SELECT * FROM REF_SKEMA WHERE Project ='" + Project + "'");
            crbyt.Items.Add(new ListItem("Pilih Cara Bayar", ""));
            for (int aa = 0; aa < rsCB.Rows.Count; aa++)
            {
                crbyt.Items.Add(new ListItem(rsCB.Rows[aa]["Nama"].ToString(), rsCB.Rows[aa]["Nomor"].ToString()));
            }
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
            string tipeU = Db.SingleString("SELECT Jenis FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

            string strSql = "SELECT * FROM MS_UNIT WHERE NoStock = '" + NoStock + "'";
            DataTable rs = Db.Rs(strSql);

            string strSql2 = "SELECT * FROM MS_NUP a INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer WHERE a.NoNUP='" + NoNUP + "' AND a.Tipe = '" + Jenis + "'";
            DataTable rs2 = Db.Rs(strSql2);

            int Count = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_NUP WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Jenis + "'");

            if (Count != 0)
                priority.Text = NoNUP;
            if (rs.Rows.Count > 0)
                nomorunit.Text = rs.Rows[0]["NoUnit"].ToString();
            if (rs2.Rows.Count > 0)
                namacust.Text = rs2.Rows[0]["Nama"].ToString();

            if (crbyt.SelectedValue == "")
                btncek.Text = "Silahkan Pilih Cara Bayar";
            else
                btncek.Text = "<a href=\"javascript:call('" + NoStock + "','" + NoNUP + "','" + Jenis + "','" + crbyt.SelectedValue + "','" + Project + "')\">Cek Harga...</a>";
            
            string st = "NOT AVAILABLE";
            if (rs.Rows[0]["Status"].ToString() == "A" || rs.Rows[0]["Status"].ToString() == "H")
            {
                st = "AVAILABLE";
            }
            else
                tdsave.Attributes["style"] = "display:none";

            statusunit.Text = st;
            typeunit.Text = rs.Rows[0]["Jenis"].ToString();
        }

        private bool valid()
        {
            string s = "";

            bool x = true;
            string status = Db.SingleString("SELECT Status FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            if (status == "A" || status == "H")
            {
                nomorunitc.Text = "";
            }
            else
            {
                x = false;
                if (s == "") s = nomorunit.ID;
                nomorunitc.Text = "Unit sudah dipilih.";
            }


            if (crbyt.SelectedIndex == 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Unit Properti tidak boleh kosong.\\n"
                    + "2. Unit sudah dipilih.\\n"
                    + "3. Pilih Cara Bayar.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                Db.Execute("UPDATE MS_UNIT SET Status = 'B' WHERE NoStock = '" + NoStock + "'");
                Db.Execute("UPDATE MS_NUP SET Status=3 WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Jenis + "'");
                //Db.Execute("INSERT INTO MS_NUP_PRIORITY(NoNUP, NoStock, NoNUPHeader,NomorSkema,Tipe,Project) VALUES('" + NoNUP + "', '" + NoStock + "', '" + NoNUP + "','" + crbyt.SelectedValue + "','" + Jenis + "','" + Project + "')");

                Db.Execute("UPDATE MS_NUP_PRIORITY SET NomorSkema = '" + crbyt.SelectedValue + "',Harga = " + Convert.ToDecimal(nml.Text) + " WHERE NoNUP = '" + NoNUP + "'");
                return true;

                var context = GlobalHost.ConnectionManager.GetHubContext<ClosingUnit>();
                context.Clients.All.invokeStatus(NoStock);
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
            if (Save())
            {
                Response.Redirect("UnitPilih2.aspx?No=" + NoNUP + "&Tipe=" + Jenis);
            }
        }
        protected void cancel_Click(object sender, EventArgs e)
        {
            Db.Execute("UPDATE MS_UNIT SET STATUS = 'A' WHERE NoStock = '" + NoStock + "'");
            Db.Execute("DELETE FROM MS_NUP_PRIORITY WHERE NoNUP='" + NoNUP + "' AND NoStock='" + NoStock + "'");
            Response.Redirect("ClosingNUP2.aspx?No=" + NoNUP + "&Tipe=" + Jenis + "&project=" + Project);
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }
        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
        private string Jenis
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }
        private int CaraBayar
        {
            get
            {
                return Convert.ToInt16(Request.QueryString["CB"]);
            }
        }
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }

        protected void crbyt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (crbyt.SelectedIndex == 0)
            {

                btncek.Text = "Silahkan Pilih Cara Bayar";

                nml.Text = Cf.Num(0);
            }
            else
            {
                btncek.Text = "<a href=\"javascript:call('" + NoStock + "','" + NoNUP  + "','" + Jenis + "','" + crbyt.SelectedValue + "','" + Project + "')\">Cek Harga...</a>";
                Label l = new Label();
                decimal pl = Db.SingleDecimal("SELECT ISNULL(PriceList, 0) FROM MS_UNIT"
                   + " WHERE NoStock = '" + NoStock + "'");

                string RumusDiskon = "";
                RumusDiskon = Db.SingleString(
                         "SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + crbyt.SelectedValue);

                string[] x = RumusDiskon.Split('+');

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != "")
                    {
                        decimal y = Convert.ToDecimal(x[i]) * (decimal)-1;
                        if (i < (x.Length - 1))
                            sb.Append(y.ToString() + "+");
                        else
                            sb.Append(y.ToString());
                    }
                }
                string RumusBunga = Db.SingleString(
                    "SELECT Bunga FROM REF_SKEMA WHERE Nomor = " + crbyt.SelectedValue);


                string[] x2 = RumusBunga.Split('+');

                System.Text.StringBuilder sb2 = new System.Text.StringBuilder();

                for (int i = 0; i < x2.Length; i++)
                {
                    if (x2[i] != "")
                    {
                        decimal y = Convert.ToDecimal(x2[i]) * (decimal)-1;
                        if (i < (x2.Length - 1))
                            sb2.Append(y.ToString() + "+");
                        else
                            sb2.Append(y.ToString());
                    }
                }

                //persenbunga.Text = sb2.ToString();
                //decimal bunga = Func.NominalDiskon(RumusBunga, pl);
                //if (bunga == 0)
                //{
                //    nilaiBunga.Text = "0";
                //}
                //else
                //{
                //    nilaiBunga.Text = Cf.Num(Math.Round(bunga, 0).ToString());
                //}

                //persenbunga.Text = sb2.ToString();
                decimal bunga = Func.NominalDiskon(RumusBunga, pl);

                //disc.Text = sb.ToString();
                decimal diskon = Func.NominalDiskon(RumusDiskon, pl + bunga);

                decimal ndpp = 0, nppn = 0;
                string ParamID = "PLIncludePPN" + Project;

                bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";
                if (includeppn)
                    ndpp = (pl - diskon + (bunga * (decimal)-1)) / (decimal)1.1;
                else
                    ndpp = (pl - diskon + (bunga * (decimal)-1));

                if (includeppn)
                {
                    nppn = (pl - diskon + (bunga * (decimal)-1)) - ndpp;
                }
                else
                {
                    nppn = (ndpp * (decimal)0.1);
                }
                
                nml.Text = Cf.Num(Math.Round(ndpp + nppn));

                btncek.Text = "<a href=\"javascript:call('" + NoNUP + "','" + NoStock + "','" + Jenis + "','" + crbyt.SelectedValue + "','" + Project + "')\">Cek Harga...</a>";
            }
        }
    }
}