using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class PanggilNUPTampil2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            fillBerjalan();
            fillPending();

        }

        protected void fillBerjalan()
        {
            string Query = "SELECT DISTINCT TOP " + Jumlah + " NoNUP FROM MS_NUP WHERE NoCustomer !='' AND Status=1";
            DataTable rsKTP = Db.Rs(Query);

            for (int i = 0; i < rsKTP.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string Query2 = "SELECT * FROM MS_NUP WHERE NoNUP = '" + rsKTP.Rows[i]["NoNUP"] + "' ORDER BY NoNUP";
                DataTable rsNUP = Db.Rs(Query2);
                for (int a = 0; a < rsNUP.Rows.Count; a++)
                {
                    if (!Response.IsClientConnected) break;

                    TableRow r = new TableRow();
                    TableCell c;

                    c = new TableCell();

                    c.Text = rsNUP.Rows[a]["NoNUP"].ToString();
                    c.Attributes["style"] = "font-size:40pt;";
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.VerticalAlign = VerticalAlign.Top;
                    r.Cells.Add(c);

                    nupberjalan.Rows.Add(r);
                }
            }
        }

        protected void fillPending()
        {
            string strPending = "SELECT * FROM MS_NUP WHERE Status = 2 ORDER BY NoNUP";
            DataTable rsPending = Db.Rs(strPending);

            decimal penghitung = 1;
            int hit = 0;
            if (rsPending.Rows.Count > 10)
                penghitung = Math.Ceiling(Convert.ToDecimal(rsPending.Rows.Count) / 10);

            for (int x = 0; x < 10; x++)
            {
                TableRow r = new TableRow();
                for (int y = 0; y < penghitung; y++)
                {
                    if (hit != rsPending.Rows.Count)
                    {
                        TableCell c;
                        c = new TableCell();

                        c.Text = rsPending.Rows[hit]["NoNUP"].ToString();
                        c.Attributes["style"] = "font-size:40pt;";
                        c.HorizontalAlign = HorizontalAlign.Center;
                        c.VerticalAlign = VerticalAlign.Top;
                        r.Cells.Add(c);
                        hit += 1;
                    }
                }
                nuppending.Rows.Add(r);
            }
        }

        private string Jumlah
        {
            get
            {
                return Cf.Pk(Request.QueryString["j"]);
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
        protected void save_Click(object sender, EventArgs e)
        {

            string Query = "SELECT DISTINCT TOP " + Jumlah + " NoNUP FROM MS_NUP WHERE NoCustomer !='' AND Status=1";
            DataTable rsKTP = Db.Rs(Query);

            for (int i = 0; i < rsKTP.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string Query2 = "SELECT * FROM MS_NUP WHERE NoNUP = '" + rsKTP.Rows[i]["NoNUP"] + "' ORDER BY NoNUP";
                DataTable rsNUP = Db.Rs(Query2);
                for (int a = 0; a < rsNUP.Rows.Count; a++)
                {
                    if (!Response.IsClientConnected) break;

                    TableRow r = new TableRow();
                    TableCell c;

                    c = new TableCell();

                    c.Text = rsNUP.Rows[a]["Status"].ToString() + "-" + rsNUP.Rows[a]["NoNUP"].ToString();
                    c.Attributes["style"] = "font-size:40pt;";
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.VerticalAlign = VerticalAlign.Top;
                    r.Cells.Add(c);

                    nupberjalan.Rows.Add(r);
                }
            }
            fillPending();
            Response.Redirect("PanggilNUPInput2.aspx?a=done");
        }
    }
}
