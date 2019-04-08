using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class CancelReception : System.Web.UI.Page
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
            string Query = "SELECT DISTINCT NoNUP FROM MS_NUP WHERE NoCustomer !='' AND Status= 1";
            DataTable rsNama = Db.Rs(Query);
            Rpt.NoData(rpt, rsNama, "Tidak terdapat NUP dengan kriteria seperti tersebut diatas.");

            for (int i = 0; i < rsNama.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                bool Tampil = true;
                Tampil = CekPelunasanNUP(rsNama.Rows[i]["NoNUP"].ToString());
                if (Tampil)
                {

                    string Query2 = "SELECT * FROM MS_NUP a "
                                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                                + " WHERE a.NoNUP = '" + rsNama.Rows[i]["NoNUP"] + "' AND (b.Nama LIKE '%" + keyword.Text + "%' ) ORDER BY a.NoNUP";
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
                        if (Convert.ToInt32(rsNUP.Rows[a]["Status"]) == 1) //Sudah diaktivasi
                            c.BackColor = Color.Yellow;

                        c.Attributes["style"] = "border-bottom:1px dashed silver";
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = rsNUP.Rows[a]["Nama"].ToString();
                        if (Convert.ToInt32(rsNUP.Rows[a]["Status"]) == 1) //Sudah diaktivasi
                            c.BackColor = Color.Yellow;
                        c.Attributes["style"] = "border-bottom:1px dashed silver";
                        r.Cells.Add(c);



                        c = new TableCell();
                        c.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rsNUP.Rows[a]["NoAgent"].ToString() + "'");

                        if (Convert.ToInt32(rsNUP.Rows[a]["FlagStatus"]) == 1) //Belum diaktivasi
                            c.BackColor = Color.Yellow;
                        c.Attributes["style"] = "border-bottom:1px dashed silver";
                        r.Cells.Add(c);

                        c = new TableCell();
                        if (Convert.ToInt32(rsNUP.Rows[a]["Status"]) == 1) //Sudah diaktivasi
                            c.Text = "<a href=\"CancelReception2.aspx?NoNUP=" + rsNUP.Rows[a]["NoNUP"] + "\">"
                                    + "Cancel Aktivasi...</a>";
                        r.Cells.Add(c);


                        r.Cells.Add(c);
                        rpt.Rows.Add(r);
                    }
                }
            }

        }

        private bool CekPelunasanNUP(string NoNUPHeader)
        {
            bool x = true;
            int countJumlahNUP = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP WHERE NoNUPHeader='" + NoNUPHeader + "'");
            DataTable rs = Db.Rs("SELECT * FROM MS_NUP WHERE NoNUPHeader='"+NoNUPHeader+"'");
            int countPelunasan = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                //cek di pelunasan NUP
                int cek  = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP_PELUNASAN WHERE NoNUP='" + rs.Rows[i]["NoNUP"].ToString() + "'");
                if (cek != 0)
                {
                    countPelunasan += 1;
                }
            }
            if (countJumlahNUP != countPelunasan)
            {
                x = false;
            }

            return x;
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
