using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class ShowPeta4C2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Act.Pass();
            //Act.NoCache();

            if (!Page.IsPostBack)
            {
                Func.GenerateFP("CLUSTER HAWAII");
                Func.GenerateFP("CLUSTER PHUKET");
                Display();
            }
        }

        private void Display()
        {
            dasar1.ImageUrl = "PetaBesar.aspx?floor=FP/CLUSTER HAWAII.jpg";
            dasar2.ImageUrl = "PetaBesar.aspx?floor=FP/CLUSTER PHUKET.jpg";
            dasar3.ImageUrl = "PetaBesar.aspx?floor=FP/CLUSTER MALDIVES.jpg";
            dasar4.ImageUrl = "PetaBesar.aspx?floor=FP/CLUSTER MALLORCA.jpg";

            trans1.ImageUrl = "FP/CLUSTER HAWAII_no.png";
            trans2.ImageUrl = "FP/CLUSTER PHUKET_no.png";
            trans3.ImageUrl = "FP/CLUSTER MALDIVES_no.png";
            trans4.ImageUrl = "FP/CLUSTER MALLORCA_no.png";

            AssignCoord1();
            AssignCoord2();
            AssignCoord3();
            AssignCoord4();
        }

        private void AssignCoord1()
        {
            //Image preparations
            koordinat1.ImageUrl = "/Media/blank_separator.gif";

            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(
                Request.PhysicalApplicationPath + "FP\\CLUSTER HAWAII.jpg");

            koordinat1.Width = bm.Width;
            koordinat1.Height = bm.Height;
            koordinat1.Attributes["usemap"] = "#coordinate";

            //Mapping
            System.Text.StringBuilder x = new System.Text.StringBuilder();
            x.Append("<map name='coordinate'>");

            string strSql = "SELECT NoUnit,Koordinat,Status FROM MS_UNIT WHERE Peta = 'CLUSTER HAWAII'";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                //x.Append(href(rs.Rows[i]["NoUnit"].ToString(), rs.Rows[i]["Koordinat"].ToString()));
            }

            x.Append("</map>");
            coord1.Text = x.ToString();

            bm.Dispose();
        }

        private void AssignCoord2()
        {
            //Image preparations
            koordinat2.ImageUrl = "/Media/blank_separator.gif";

            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(
                Request.PhysicalApplicationPath + "FP\\CLUSTER PHUKET.jpg");

            koordinat2.Width = bm.Width;
            koordinat2.Height = bm.Height;
            koordinat2.Attributes["usemap"] = "#coordinate";

            //Mapping
            System.Text.StringBuilder x = new System.Text.StringBuilder();
            x.Append("<map name='coordinate'>");

            string strSql = "SELECT NoUnit,Koordinat,Status FROM MS_UNIT WHERE Peta = 'CLUSTER PHUKET'";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                //x.Append(href(rs.Rows[i]["NoUnit"].ToString(), rs.Rows[i]["Koordinat"].ToString()));
            }

            x.Append("</map>");
            coord2.Text = x.ToString();

            bm.Dispose();
        }

        private void AssignCoord3()
        {
            //Image preparations
            koordinat3.ImageUrl = "/Media/blank_separator.gif";

            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(
                Request.PhysicalApplicationPath + "FP\\CLUSTER MALDIVES.jpg");

            koordinat3.Width = bm.Width;
            koordinat3.Height = bm.Height;
            koordinat3.Attributes["usemap"] = "#coordinate";

            //Mapping
            System.Text.StringBuilder x = new System.Text.StringBuilder();
            x.Append("<map name='coordinate'>");

            string strSql = "SELECT NoUnit,Koordinat,Status FROM MS_UNIT WHERE Peta = 'CLUSTER MALDIVES'";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                //x.Append(href(rs.Rows[i]["NoUnit"].ToString(), rs.Rows[i]["Koordinat"].ToString()));
            }

            x.Append("</map>");
            coord3.Text = x.ToString();

            bm.Dispose();
        }

        private void AssignCoord4()
        {
            //Image preparations
            koordinat4.ImageUrl = "/Media/blank_separator.gif";

            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(
                Request.PhysicalApplicationPath + "FP\\CLUSTER MALLORCA.jpg");

            koordinat4.Width = bm.Width;
            koordinat4.Height = bm.Height;
            koordinat4.Attributes["usemap"] = "#coordinate";

            //Mapping
            System.Text.StringBuilder x = new System.Text.StringBuilder();
            x.Append("<map name='coordinate'>");

            string strSql = "SELECT NoUnit,Koordinat,Status FROM MS_UNIT WHERE Peta = 'CLUSTER MALLORCA'";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                //x.Append(href(rs.Rows[i]["NoUnit"].ToString(), rs.Rows[i]["Koordinat"].ToString()));
            }

            x.Append("</map>");
            coord4.Text = x.ToString();

            bm.Dispose();
        }

        private string href(string unit, string koor)
        {
            string x = "";

            string strSql = "SELECT Status, NoStock FROM MS_UNIT WHERE NoUnit = '" + unit + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count != 0)
            {
                if (rs.Rows[0]["Status"].ToString() == "A")
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_RESERVASI"
                        + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                    if (c != 0)
                        x = "<area shape='poly' coords='" + koor + "' style='cursor:auto'"
                        + " href='KontrakDaftar2.aspx?NoStock=" + rs.Rows[0]["NoStock"] + "' alt='" + unit + "'"
                        + ">";
                    else
                    {
                        x = "<area shape='poly' coords='" + koor + "' style='cursor:auto'"
                        + " href='UnitPilih1.aspx?NoStock=" + rs.Rows[0]["NoStock"] + "' alt='" + unit + "'"
                        + ">";
                    }
                }
            }

            return x;
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
