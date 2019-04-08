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
            Act.Pass();
            Act.NoCache();
            FeedBack();

        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Clear Komisi Berhasil...";
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            pilih.Visible = false;
            frm.Visible = true;


            Fill();

            Js.Confirm(this, "Lanjutkan proses hapus jadwal komisi?");

        }

        protected void Fill()
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            string strSql = "SELECT * FROM MS_KONTRAK WHERE  TglKontrak >= '" + Dari + "' and TglKontrak <= '" + Sampai + "' and Status='A' AND FlagKomisi=1";
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string Customer = Db.SingleString(
                     "SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
                string NoKontrak = rs.Rows[i]["NoKontrak"].ToString();
                string Unit = rs.Rows[i]["NoUnit"].ToString();
                decimal DPP = Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]);
                FillTb(NoKontrak, Customer, Unit, DPP);
            }

            if (rpt.Rows.Count == 1)
            {
                save.Enabled = false;
            }
        }
        protected void FillTb(string NoKontrak, string Customer, string Unit, decimal DPP)
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI WHERE NOKONTRAK='" + NoKontrak + "' AND SudahBayar = 0");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TableCell c;
                TableRow r;

                r = new TableRow();

                c = new TableCell();
                c.Text = "<a href=\"javascript:popEditKontrak('" + NoKontrak + "')\">" + NoKontrak + "</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Customer;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Unit;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(DPP);
                r.Cells.Add(c);

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

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            string strSql = "SELECT * FROM MS_KONTRAK WHERE  TglKontrak >= '" + Dari + "' and TglKontrak <= '" + Sampai + "' and Status='A' AND FlagKomisi=1";
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                DataTable rsBef = Db.Rs("SELECT CONVERT(VARCHAR,NoUrut) + '.  ' + NamaKomisi + ' - '+ NamaPenerima + ' ('+Tipe+')   CAIR:' + CONVERT(VARCHAR,TermCair,1) + '% ' + CONVERT(VARCHAR,NilaiKomisi,1) "
             + "FROM MS_KOMISI WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' ORDER BY NoUrut");

                Db.Execute("DELETE FROM MS_KOMISI"
               + " WHERE NilaiBayar = '0' AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
               + " AND SudahBayar='0'");
                Db.Execute("UPDATE MS_KONTRAK SET FlagKomisi=0 WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'");

                DataTable rsAft = Db.Rs("SELECT CONVERT(VARCHAR,NoUrut) + '.  ' + NamaKomisi + ' - '+ NamaPenerima + ' ('+Tipe+')   CAIR:' + CONVERT(VARCHAR,TermCair,1) + '% ' + CONVERT(VARCHAR,NilaiKomisi,1) "
             + "FROM MS_KOMISI WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' ORDER BY NoUrut");

                string Ket = Cf.LogList(rsBef, rsAft, "JADWAL KOMISI");

                Db.Execute("EXEC spLogKontrak"
                    + " 'RK'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + rs.Rows[i]["NoKontrak"] + "'"
                    );
            }

            Response.Redirect("KomisiClearancePeriode.aspx?done=1");
        }
    }
}
