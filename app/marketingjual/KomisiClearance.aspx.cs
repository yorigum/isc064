using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ISC064.MARKETINGJUAL
{
    public partial class KomisiClearance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            FeedBack();

        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                        + "Clear Komisi Berhasil..."
                        + "</a>";
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            pilih.Visible = false;
            frm.Visible = true;

            
            Fill();

            Js.Confirm(this, "Lanjutkan proses hapus jadwal komisi?");
            
        }
        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
            }
        }

        protected void Fill()
        {
            string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                customer.Text = Db.SingleString(
                    "SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[0]["NoCustomer"]);
                no.Text = NoKontrak;
                unit.Text = rs.Rows[0]["NoUnit"].ToString();
                dpp.Text = Cf.Num(rs.Rows[0]["NilaiDPP"]);
                if (rs.Rows[0]["FlagKomisi"].ToString() != "0")
                {
                    FillTb();
                }
                else
                {
                    save.Enabled = false;
                }
            }
        }
        protected void FillTb()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI WHERE NOKONTRAK='"+ NoKontrak +"' AND SudahBayar = 0");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TableCell c;
                TableRow r;

                r = new TableRow();
                
                c = new TableCell();
                c.Text = rs.Rows[i]["Tipe"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaPenerima"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiKomisi"].ToString()));
                r.Cells.Add(c);

                rpt.Rows.Add(r);
                
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                Db.Execute("DELETE FROM MS_KOMISI"
               + " WHERE NilaiBayar = '0' AND NoKontrak = '" + NoKontrak + "'"
               + " AND SudahBayar='0'");
                Db.Execute("UPDATE MS_KONTRAK SET FlagKomisi=0 WHERE NoKontrak = '" + NoKontrak + "'");
            }

            Response.Redirect("KomisiClearance.aspx?done=" + NoKontrak);
        }
    }
}
