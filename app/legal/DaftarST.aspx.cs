using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{ 

public partial class DaftarST : System.Web.UI.Page
{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Js.ConfirmKeyword(this, keyword);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["status"] == "a")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "b")
                    metode.SelectedIndex = 2;

                if (metode.SelectedIndex != 0) metode.Enabled = false;
            }
        }

        protected void search_Click(object sender, System.EventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            string addq = "";
            if (metode.SelectedIndex == 1)
                addq = "SELECT * FROM MS_BAST A INNER JOIN MS_KONTRAK B ON A.NoKontrak = B.NoKontrak "
                + "WHERE B.Status = 'A'";
            else if (metode.SelectedIndex == 2)
                addq = "SELECT * FROM MS_BAST A INNER JOIN MS_KONTRAK B ON A.NoKontrak = B.NoKontrak "
                + "WHERE B.Status = 'B'";

            string strSql = "SELECT "
                + " A.NoKontrak"
                + ",B.TglKontrak"
                + ",B.Status"
                + ",B.NoUnit"
                + ",C.Nama AS Cs"
                + ",D.Nama + ' ' + D.Principal AS Ag"
                + ",(SELECT COUNT(NoUrut) FROM MS_TAGIHAN WHERE A.NoKontrak = A.NoKontrak) AS CountTagihan"
                + ",B.FlagKomisi"
                + ",A.ST"
                + ",B.NoPPJB"
                + ",B.NoAJB"
                + " FROM MS_BAST A INNER JOIN MS_KONTRAK B ON A.NoKontrak = B.NoKontrak"
                + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer "
                + " INNER JOIN MS_AGENT D ON B.NoAgent = D.NoAgent"
                + " WHERE A.NoKontrak + B.NoUnit + C.Nama + D.Nama + D.Principal "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + addq
                + " ORDER BY A.NoKontrak";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ditemukan data kontrak dengan keyword diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                string StatusKontrak = rs.Rows[i]["Status"].ToString();
                string sStrike = "";

                if (StatusKontrak == "B")
                {
                    r.ForeColor = Color.Silver;
                    sStrike = "style='text-decoration:line-through'";
                }

                string st = "";
                if ((string)rs.Rows[i]["ST"] == "D")
                    //sudah keluar
                    st = "black";
                else
                    st = "silver";

                string ppjb = "";
                if ((string)rs.Rows[i]["NoPPJB"] != "")
                    //sudah keluar
                    ppjb = "black";
                else
                    ppjb = "silver";

                string ajb = "";
                if ((string)rs.Rows[i]["NoAJB"] != "")
                    //sudah keluar
                    ajb = "black";
                else
                    ajb = "silver";

                c = new TableCell();

                if (Request.QueryString["status"] == "dari" || Request.QueryString["status"] == "sampai")
                {
                    c.Text = "<a href=\"javascript:callSource('" + rs.Rows[i]["NoKontrak"] + "', '" + Request.QueryString["status"] + "')\">"
                        + rs.Rows[i]["NoKontrak"].ToString() + "</a><br>"
                        + "<font style='font:8pt;color:" + ppjb + "'>PPJB</font>&nbsp;&nbsp;"
                        + "<font style='font:8pt;color:" + ajb + "'>AJB</font>&nbsp;&nbsp;"
                        + "<font style='font:8pt;color:" + st + "'>BAST</font>"
                        ;
                }
                else
                {
                    c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoKontrak"] + "')\" " + sStrike + ">"
                        + rs.Rows[i]["NoKontrak"].ToString() + "</a><br>"
                        + "<font style='font:8pt;color:" + ppjb + "'>PPJB</font>&nbsp;&nbsp;"
                        + "<font style='font:8pt;color:" + ajb + "'>AJB</font>&nbsp;&nbsp;"
                        + "<font style='font:8pt;color:" + st + "'>BAST</font>"
                        ;
                }
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString() + "<br>"
                    + rs.Rows[i]["Ag"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
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