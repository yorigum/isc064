using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KomisiEditDataOver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
            }

            FeedBack();

            Js.Confirm(this, "Lanjutkan proses edit penerima komisi overriding?");
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Jadwal Komisi Berhasil...";
            }
        }
        private void Fill()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'");

            if (rs.Rows.Count != 0)
            {
                bool Cross = Db.SingleBool("SELECT CrossSelling FROM MS_KONTRAK a INNER JOIN MS_AGENT b on a.NoAgent = b.NoAgent WHERE NoKontrak='"+ NoKontrak +"'");
                if (Cross == true)
                {
                    cross1.Visible = true;
                    cross2.Visible = true;
                }

                GeneralManager.Text = rs.Rows[0]["GeneralManager"].ToString();
                SalesManager.Text = rs.Rows[0]["SalesManager"].ToString();
                AdminSales.Text = rs.Rows[0]["AdminSales"].ToString();
                ProjectManager.Text = rs.Rows[0]["ProjectManager"].ToString();
                AdminSales.Text = rs.Rows[0]["AdminSales"].ToString();
                KepalaUnitSales.Text = rs.Rows[0]["KepalaUnitSales"].ToString();
                MarketingSupport.Text = rs.Rows[0]["MarketingSupport"].ToString();
                Collection.Text = rs.Rows[0]["BillingCollection"].ToString();
                GMCross.Text = rs.Rows[0]["GeneralManagerCross"].ToString();
                SMCross.Text = rs.Rows[0]["SalesManagerCross"].ToString();

            }
            string strSql = "SELECT "
                + " MS_KONTRAK.*"
                + ",MS_CUSTOMER.Nama AS Cs"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";

            DataTable rsHeader = Db.Rs(strSql);

            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nokontrak.Text = rsHeader.Rows[0]["NoKontrak"].ToString();
                unit.Text = rsHeader.Rows[0]["NoUnit"].ToString();
                customer.Text = rsHeader.Rows[0]["Cs"].ToString();
                agent.Text = rsHeader.Rows[0]["Ag"].ToString();
            }

        }
        protected void save_Click(object sender, EventArgs e)
        {
            
            DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'");

            if (rs.Rows.Count != 0)
            {
                Db.Execute("UPDATE MS_KOMISI_OVER SET"
                    + " GeneralManager='" + GeneralManager.Text + "'"
                    + " ,SalesManager='" + SalesManager.Text + "'"
                    + " ,AdminSales='" + AdminSales.Text + "'"
                    + " ,ProjectManager='" + ProjectManager.Text + "'"
                    + " ,KepalaUnitSales='" + KepalaUnitSales.Text + "'"
                    + " ,MarketingSupport='" + MarketingSupport.Text + "'"
                    + " ,BillingCollection='" + Collection.Text + "'"
                    + " ,GeneralManagerCross='" + GMCross.Text + "'"
                    + " ,SalesManagerCross='" + SMCross.Text + "'"
                    + " WHERE NoKontrak='" + NoKontrak + "'"
                    );
            }
            else
            {
                Db.Execute("EXEC spKontrakKomisiOverDaftar"
                        + "'" + NoKontrak + "'"
                        + ", '" + GeneralManager.Text + "'"
                        + ", '" + SalesManager.Text + "'"
                        + ", '" + AdminSales.Text + "'"
                        + ", '" + ProjectManager.Text + "'"
                        + ", '" + KepalaUnitSales.Text + "'"
                        + ", '" + MarketingSupport.Text + "'"
                        + ", '" + Collection.Text + "'"
                        + ", '" + GMCross.Text +"'"
                        + ", '" + SMCross.Text + "'"
                        );
            }
            
            Response.Redirect("KomisiEditDataOver.aspx?NoKontrak=" + NoKontrak + "&done=1");
        }
    }
}
