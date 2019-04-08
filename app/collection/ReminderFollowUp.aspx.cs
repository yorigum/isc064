using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class ReminderFollowUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
            ok.HRef = "Reminder.aspx?project=" + Project;
        }

        private void Fill()
        {
            DateTime Dari = DateTime.Now;
            DateTime Tgl = Dari.AddDays(7);

            string nav = "'<a href=RegistrasiFollowUp.aspx?Ref='''+a.NoKontrak+'''&Tipe=Jual>' + a.NoKontrak + '</a>' AS Kontrak";
            String strSql = "SELECT " 
                + nav
                + ",(SELECT Nama FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer =b.NoCustomer) AS Cs"
                + ",(SELECT NoHP FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer =b.NoCustomer) AS HP"
                + ",b.NoUnit AS Unit"
                + ",a.NamaTagihan AS Nama"
                + ",CONVERT(VARCHAR,TglJT,106) AS Tgl"
                + ",FORMAT(a.NilaiTagihan,'#,###') AS Nilai"                
                + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN a"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " AND CONVERT(varchar, TglJT , 112) <= '" + Cf.Tgl112(Tgl) + "'"
                + " AND CONVERT(varchar, TglJT , 112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND a.NilaiTagihan - ( SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN  c WHERE c.NoKontrak = a.NoKontrak "
                + " AND c.NoTagihan = a.NoUrut ) > 0 "
                + " AND a.NoUrut NOT IN (SELECT NoTagihan FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP d WHERE d.NoKontrak = a.NoKontrak) "
                + " AND b.Status='A' AND b.Project = '" + Project + "' ORDER BY TglJT ASC"
                ;

            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
            if (tb.PageCount == 0) kosong.InnerText = "Reminder untuk topik diatas masih kosong.";
        }

        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["Project"]);
            }
        }


        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}