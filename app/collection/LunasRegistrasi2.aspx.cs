using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.COLLECTION
{
    public partial class LunasRegistrasi2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Fill();
                cancel.Attributes["onclick"] = "location.href='LunasRegistrasi.aspx'";
            }
        }
        private void Fill()
        {
            nosklm.Enabled = false;
            string strSql = "SELECT " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.*"
                + "," + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER.Nama AS Cs"
                + "," + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT.Nama + ' ' + " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT.Principal AS Ag"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER ON " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.NoCustomer = " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER.NoCustomer"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT ON " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.NoAgent = " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT.NoAgent"
                + " WHERE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                tgl.Text = Cf.Day(DateTime.Today);
                nokontrakl.Text = "<a href=\"javascript:popEditKontrak('" + rs.Rows[0]["NoKontrak"] + "')\">"
                    + rs.Rows[0]["NoKontrak"].ToString() + "</a>";
                unit.Text = "<a href=\"javascript:popUnit('" + rs.Rows[0]["NoStock"] + "')\">"
                    + rs.Rows[0]["NoUnit"].ToString() + "</a>";
                customer.Text = "<a href=\"javascript:popEditCustomer('" + rs.Rows[0]["NoCustomer"] + "')\">"
                    + rs.Rows[0]["Cs"].ToString() + "</a>";
                agent.Text = rs.Rows[0]["Ag"].ToString();
                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);
                if ((decimal)rs.Rows[0]["PersenLunas"] < 100)
                {
                    lunasinfo.Text = "PELUNASAN BELUM MENCAPAI 100%";
                    save.Enabled = false;
                }
                decimal pernahskl = Db.SingleInteger("SELECT COUNT(NoSKL) FROM MS_SKL WHERE Ref='" + NoKontrak + "'");
                if (pernahskl > 0)
                {
                    lunasinfo.Text += "SUDAH PERNAH DIREGISTRASIKAN";
                    nod.Enabled = false;
                    save.Enabled = false;
                    nosklm.Enabled = false;
                    DataTable rs3 = Db.Rs("SELECT * FROM MS_SKL WHERE Ref = '" + NoKontrak + "'");
                    nod.SelectedValue = Convert.ToString(rs3.Rows[0]["Used"]);
                    if(nod.SelectedValue == "1") nosklm.Text= rs3.Rows[0]["NoSKLManual"].ToString();
                }

            }
        }
        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }
        protected void save_Click(object sender, EventArgs e)
        {
            DateTime Tgl = Convert.ToDateTime(tgl.Text);
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            //Numerator
            string NoSKL = Numerator.SKL(Tgl.Month, Tgl.Year, Project);

            Db.Execute("EXEC spSKLRegistrasi"
                    + " '" + NoSKL + "'"
                    + ",'" + Tgl + "'"
                    + ",'" + NoKontrak + "'"
                    + ",'" + nosklm.Text + "'"
                    );

            Db.Execute("UPDATE MS_SKL SET Used='" + nod.SelectedValue + "',Project = '" + Project + "' WHERE NoSKL = '" + NoSKL + "'");
            DataTable rs = Db.Rs("SELECT "
                          + " CONVERT(varchar, TglSKL, 106) AS [Tanggal]"
                          + ",NoSKL"
                          + ",Ref AS [Ref.]"
                          + ",NoSKLManual"
                          + ",Used AS [No.YgDigunakan]"
                          + " FROM MS_SKL WHERE NoSKL = '" + NoSKL + "'");
            Db.Execute("EXEC spLogSKL "
                    + " '" + DateTime.Now + "'"
                    + ",'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + NoSKL + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    );

            Response.Redirect("LunasRegistrasi.aspx?done=" + NoSKL);
        }

        protected void nod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nod.SelectedIndex != 0)
            {
                nosklm.Enabled = true;
            }
        }
    }
}
