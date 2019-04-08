using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class PopUnitPetaDetil : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                try
                {
                    Func.GenerateFP(f);
                    Display();
                }
                catch { }
            }
        }

        private void Display()
        {
            dasar.ImageUrl = "PetaBesar.aspx?f=FP/" + f + ".jpg";
            trans.ImageUrl = "FP/" + f + "_no.png";

            AssignCoord();
        }

        private void AssignCoord()
        {
            //Image preparations
            koordinat.ImageUrl = "/Media/blank_separator.gif";

            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(
                Request.PhysicalApplicationPath + "FP\\" + f + ".jpg");

            koordinat.Width = bm.Width;
            koordinat.Height = bm.Height;
            koordinat.Attributes["usemap"] = "#coordinate";

            //Mapping
            System.Text.StringBuilder x = new System.Text.StringBuilder();
            x.Append("<map name='coordinate'>");

            string strSql = "SELECT NoUnit,Koordinat,Status,Tipe FROM MS_UNIT WHERE Peta = '" + f + "'";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string st = rs.Rows[i]["Status"].ToString();
                string add = "";

                if (st == "A" || st == "P")
                {
                    if (st == "P")
                    {
                        add = "";// "(" + Db.SingleString("SELECT NoNUP FROM MS_PRIORITY WHERE NoUnit='" + rs.Rows[i]["NoUnit"] + "'") + ") Tipe : " + rs.Rows[i]["Tipe"];
                    }
                    else
                    {
                        add = " Tipe : " + rs.Rows[i]["Tipe"];
                    }
                }
                else
                {
                    add = " Tipe : " + rs.Rows[i]["Tipe"];
                }
                x.Append(href(rs.Rows[i]["NoUnit"].ToString(), rs.Rows[i]["Koordinat"].ToString(), add));

            }

            x.Append("</map>");

            coord.Text = x.ToString();

            bm.Dispose();
        }

        private string href(string unit, string koor, string add)
        {
            string x = "";

            string strSql = "SELECT Status, NoStock FROM MS_UNIT WHERE NoUnit = '" + unit + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count != 0)
            {
                if (rs.Rows[0]["Status"].ToString() == "B" || rs.Rows[0]["Status"].ToString() == "H")
                {
                    x = "<area shape='poly' coords='" + koor + "' style='cursor:auto'"
                            + " alt='" + unit + add + "'"
                            + ">";
                }
                else if (rs.Rows[0]["Status"].ToString() == "P")
                {
                    int aa = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP_PRIORITY WHERE NoNUP='" + NoNUP + "' AND NoStock ='" + rs.Rows[0]["NoStock"].ToString() + "'");
                    if (aa > 0)
                    {
                        x = "<area shape='poly' coords='" + koor + "' style='cursor: auto;'"
                        + " href='UnitPilih2.aspx?NoNUP=" + NoNUP + "&NoStock=" + rs.Rows[0]["NoStock"] + "' alt='" + unit + add + "'"
                        + ">";
                    }
                }
                else if (rs.Rows[0]["Status"].ToString() == "A")
                {
                    x = "<area shape='poly' coords='" + koor + "' style='cursor:auto' href=\"javascript:pilih('" + rs.Rows[0]["NoStock"].ToString() + "','" + unit + "')\""
                                + ">";
                    //Db.Execute("UPDATE MS_UNIT SET Status='P' SET NoStock = '" + rs.Rows[0]["NoStock"].ToString() + "'");
                }
            }

            return x;
        }
        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
        private string f
        {
            get
            {
                return Cf.Pk(Request.QueryString["f"]);
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
