using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class PanggilNUPTampil : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Init();
            fillBerjalan();
            filldatanup();
            filldataunit();
        }
        protected void Init()
        {
            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENISPROPERTI WHERE Project = '" + Project + "'");
            Label l;
            TextBox t;

            l = new Label();
            l.Text = "<table style='margin-top: 0; font-size: 24px'>";
            list.Controls.Add(l);

            l = new Label();
            l.Text = "<tr>";
            list.Controls.Add(l);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                l = new Label();
                l.Text = "<td colspan='3' style='vertical - align: top;padding-left:50px '> Stok Unit " + rs.Rows[i]["Nama"].ToString() + "</td>";
                list.Controls.Add(l);
            }

            l = new Label();
            l.Text = "</tr>";
            list.Controls.Add(l);

            l = new Label();
            l.Text = "<tr>";
            list.Controls.Add(l);


            DataTable a = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS WHERE Project = '" + Project + "'");

            for (int j = 0; j < a.Rows.Count; j++)
            {
                for (int b = 0; b < rs.Rows.Count; b++)
                {
                    int CountOne = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT WHERE  Status='A' AND Jenis LIKE '%" + a.Rows[j]["Nama"].ToString() + "%' AND JenisProperti='" + rs.Rows[b]["JenisProperti"] + "' AND Project = '" + Project + "'");
                    string c = "<td><span style='font-weight:bold;font-size:50px'>" + CountOne.ToString() + "</span>  Unit</td>";
                    if (b == (rs.Rows.Count - 1))
                    {
                        c = "<td><span style='font-weight:bold;font-size:50px'>" + CountOne.ToString() + "</span>  Unit</td></tr>";
                    }

                    l = new Label();
                    l.Text =
                         "<td width='150px' style='vertical - align: top;padding-left:50px'>" + a.Rows[j]["Nama"].ToString() + "</td>"
                        + "<td style='vertical - align: top; '>:</td>"
                           + c;
                    list.Controls.Add(l);
                }
            }
            l = new Label();
            l.Text = "</tr>";
            list.Controls.Add(l);

            l = new Label();
            l.Text = "</table>";
            list.Controls.Add(l);

        }

        protected void fillBerjalan()
        {

            string Query = "SELECT DISTINCT TOP " + Jumlah + " (NoNUP),Tipe FROM MS_NUP WHERE NoCustomer !='' AND Status=1 AND Tipe = '" + Tipe + "' AND Project = '"+Project+"'";
            DataTable rsKTP = Db.Rs(Query);
            string NoNUP = "";
            if (rsKTP.Rows.Count != 0)
                NoNUP = Db.SingleString("SELECT Top 1 NoNUP FROM MS_NUP ORDER BY TglAktivasi Desc");


            decimal penghitung = 1;
            int hit = 0;
            if (rsKTP.Rows.Count > 10)
                penghitung = Math.Ceiling(Convert.ToDecimal(rsKTP.Rows.Count) / 11);
            for (int y = 0; y <= penghitung; y++)
            {
                TableRow r = new TableRow();

                for (int x = 0; x < 10; x++)
                {
                    if (!Response.IsClientConnected) break;

                    if (hit != rsKTP.Rows.Count)
                    {
                        TableCell c;
                        string tipeunit = "";
                        if (rsKTP.Rows[x]["Tipe"].ToString() == "RUSUNAMI")
                            tipeunit = "G";
                        else
                            tipeunit = "P";

                        c = new TableCell();
                        if (NoNUP == rsKTP.Rows[hit]["NoNUP"].ToString())
                            c.Text = "<span style='font-size:12pt'>" + tipeunit + "-NPU </span><br/><span class='blink_me'>" + rsKTP.Rows[hit]["NoNUP"].ToString() + "</span>";
                        else
                            c.Text = "<span style='font-size:12pt'>" + tipeunit + "-NPU </span><br/><span>" + rsKTP.Rows[hit]["NoNUP"].ToString() + "</span>";

                        c.Attributes["style"] = "font-size:30pt;";
                        c.HorizontalAlign = HorizontalAlign.Center;
                        c.VerticalAlign = VerticalAlign.Top;
                        r.Cells.Add(c);
                        hit += 1;
                    }

                }
                nupberjalan.Rows.Add(r);
            }



        }

        protected void filldatanup()
        {

            string Query2 = "SELECT ISNULL(COUNT(*),0) FROM MS_NUP WHERE NoCustomer !='' AND Status=3 AND Project = '" + Project + "'";
            int Count3 = Db.SingleInteger(Query2);

            string Query = "SELECT ISNULL(COUNT(*),0) FROM MS_NUP WHERE NoCustomer !='' AND Status=4 AND Project = '" + Project + "'";
            int Count = Db.SingleInteger(Query);



            nuppilihunit.Text = Count3.ToString();
            nupclosing.Text = Count.ToString();

        }
        protected void filldataunit()
        {
            //int CountOne = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT WHERE  Status='A' AND Jenis LIKE '%1BR%' AND JenisProperti='RUSUNAMI'");
            //one.Text = "<span style='font-weight:bold;font-size:50px'>" + CountOne.ToString() + "</span>  Unit";

            //int CountTwo = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT WHERE  Status='A' AND Jenis LIKE '%2BR%' AND JenisProperti='RUSUNAMI'");
            //two.Text = "<span style='font-weight:bold;font-size:50px'>" + CountTwo.ToString() + "</span>  Unit";

            //int CountSTD = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT WHERE  Status='A' AND Jenis LIKE '%STUDIO%' AND JenisProperti='RUSUNAMI'");
            //std.Text = "<span style='font-weight:bold;font-size:50px'>" + CountSTD.ToString() + "</span>  Unit";


            //int CountOne2 = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT WHERE  Status='A' AND Jenis LIKE '%1BR%' AND JenisProperti='APARTEMEN'");
            //one2.Text = "<span style='font-weight:bold;font-size:50px'>" + CountOne2.ToString() + "</span>  Unit";

            //int CountTwo2 = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT WHERE  Status='A' AND Jenis LIKE '%2BR%' AND JenisProperti='APARTEMEN'");
            //two2.Text = "<span style='font-weight:bold;font-size:50px'>" + CountTwo2.ToString() + "</span>  Unit";

            //int CountSTD2 = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT WHERE  Status='A' AND Jenis LIKE '%STUDIO%' AND JenisProperti='APARTEMEN'");
            //std2.Text = "<span style='font-weight:bold;font-size:50px'>" + CountSTD2.ToString() + "</span>  Unit";
        }
        private string Jumlah
        {
            get
            {
                return Cf.Pk(Request.QueryString["j"]);
            }
        }
        private string Tipe
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
