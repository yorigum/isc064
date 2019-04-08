using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class ReminderPJT7 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
            ok.HRef = "Reminder.aspx?project=" + Project;
        }

        private void Fill()
        {

            DateTime Dari = DateTime.Now;
            DateTime TglPJT = Dari.AddDays(7);
            string nav = "'<a href=PJTRegistrasiMarketing.aspx?Ref='''+a.NoKontrak+'''&Tipe=Jual>' + a.NoKontrak + '</a>' AS Kontrak";
            String strSql = "SELECT "
                + nav
                + ",(SELECT Nama FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = b.NoCustomer) + '<br>' +"
                + " (SELECT Nama FROM ISC064_MARKETINGJUAL..MS_AGENT WHERE NoAgent = b.NoAgent) + '<br>' +"
                + " b.NoUnit AS Cs"
                + ",(SELECT NoHP + '<br>' + NoTelp FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = b.NoCustomer) AS HP"
                + ",a.NamaTagihan AS Tagihan"
                + ",CONVERT(VARCHAR,TglJt,106) AS JT"
                + ",'Tagihan<br>Pelunasan<br>Sisa<br>' AS Lbl"
                + ",':<br>:<br>:<br>' AS Lbl2"
                + ",FORMAT(NilaiTagihan,'#,###') + '<br>' + "
                + " CASE WHEN (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak=a.NoKontrak AND NoTagihan=a.NoUrut) > 0"
                + " THEN FORMAT((SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak=a.NoKontrak AND NoTagihan=a.NoUrut),'#,###') ELSE '0' END + '<br><hr>' +"
                + " FORMAT((NilaiTagihan-(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak=a.NoKontrak AND NoTagihan=a.NoUrut)),'#,###')"
                + " AS Nilai"                
                + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN a"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " AND CONVERT(varchar, TglJT , 112) <= '" + Cf.Tgl112(TglPJT) + "'"
                + " AND CONVERT(varchar, TglJT , 112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND a.NilaiTagihan - ( SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN  c WHERE c.NoKontrak = a.NoKontrak "
                + " AND c.NoTagihan = a.NoUrut ) > 0 "
                + " AND a.NoUrut NOT IN (SELECT NoTagihan FROM " + Mi.DbPrefix + "FINANCEAR..MS_PJT_DETIL d WHERE d.NoPJT IN (SELECT NoPJT FROM " + Mi.DbPrefix + "FINANCEAR..MS_PJT WHERE MS_PJT.Ref = a.NoKontrak)) "
                + " AND b.Status='A' AND b.Project = '" + Project + "'"
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
