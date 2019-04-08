using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class KonfirmasiNUP : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                //Fill();
            }
        }

        private void Fill()
        {
            string Query = "SELECT DISTINCT NoNUPHeader FROM MS_NUP WHERE NoCustomer !='' AND FlagStatus=2";
            DataTable rsNama = Db.Rs(Query);
            Rpt.NoData(rpt, rsNama, "Tidak terdapat NUP dengan kriteria seperti tersebut diatas.");
            for (int i = 0; i < rsNama.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string Query2 = "SELECT * FROM MS_NUP a INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer WHERE NoNUP = '" + rsNama.Rows[i]["NoNUPHeader"] + "'  AND NoNUP+Nama LIKE '%" + keyword.Text + "%' ORDER BY NoNUP";
                DataTable rsNUP = Db.Rs(Query2);
                for (int a = 0; a < rsNUP.Rows.Count; a++)
                {
                    if (!Response.IsClientConnected) break;
                    TableRow r = new TableRow();
                    TableCell c;


                    c = new TableCell();
                    c.Text = "<a href=\"javascript:call('" + rsNUP.Rows[a]["NoNUP"] + "')\">"
                           + rsNUP.Rows[a]["NoNUP"]
                           + "</a>";
                    c.Attributes["style"] = "border-bottom:1px dashed silver";
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rsNUP.Rows[a]["Nama"].ToString();
                    c.Attributes["style"] = "border-bottom:1px dashed silver";
                    r.Cells.Add(c);



                    c = new TableCell();
                    c.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rsNUP.Rows[a]["NoAgent"].ToString() + "'");
                    //if (rsNUP.Rows[a]["KodeMarketing"].ToString() == "0")
                    //{
                    //    c.Text = "";
                    //}
                    //else
                    //{
                    //    int AdaSubAgent = 0;
                    //    AdaSubAgent = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_AGENT WHERE KodeMarketing = " + rsNUP.Rows[a]["KodeMarketing"] + "");
                    //    string NamaSubAgent = "";
                    //    if (AdaSubAgent > 0)
                    //    {
                    //        NamaSubAgent = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE KodeMarketing = " + rsNUP.Rows[a]["KodeMarketing"] + "");

                    //    }
                    //    c.Text = Cf.Str(NamaSubAgent);
                    //}


                    c.Attributes["style"] = "border-bottom:1px dashed silver";
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = "<a href=\"KonfirmasiNUP2.aspx?NoNUP=" + rsNUP.Rows[a]["NoNUP"] + "\">"
                        + "Konfirmasi...</a>";
                    r.Cells.Add(c);


                    r.Cells.Add(c);
                    rpt.Rows.Add(r);
                }
            }

        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            Fill();
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
