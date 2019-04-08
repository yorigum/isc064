using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class ShowPetaTVMalta : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Act.Pass();
            //Act.NoCache();

            if (!Page.IsPostBack)
            {
                Func.GenerateFP(floor1);
                Display();
            }
        }

        private void Display()
        {
            dasar1.ImageUrl = "PetaBesar.aspx?floor=FP/" + floor1 + ".jpg";

            trans1.ImageUrl = "FP/" + floor1 + "_no.png";

            AssignCoord1();
        }

        private void AssignCoord1()
        {
            //Image preparations
            koordinat1.ImageUrl = "/Media/blank_separator.gif";

            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(
                Request.PhysicalApplicationPath + "FP\\" + floor1 + ".jpg");

            koordinat1.Width = bm.Width;
            koordinat1.Height = bm.Height;
            koordinat1.Attributes["usemap"] = "#coordinate";

            //Mapping
            System.Text.StringBuilder x = new System.Text.StringBuilder();
            x.Append("<map name='coordinate'>");

            string strSql = "SELECT NoUnit,Koordinat,Status FROM MS_UNIT WHERE Peta = '" + floor1 + "'";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                //x.Append(href(rs.Rows[i]["NoUnit"].ToString(), rs.Rows[i]["Koordinat"].ToString()));
            }

            x.Append("</map>");
            coord1.Text = x.ToString();

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

        private string floor1
        {
            get
            {
                return "CLUSTER MALTA";
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
