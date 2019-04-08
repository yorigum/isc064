using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class Reminder : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                if (Request.QueryString["project"] != null)
                {
                    project.SelectedValue = Request.QueryString["project"];
                }
            }

            int c = 0;
            string[] x = Sc.MktCatalog();
            for (int i = 0; i <= x.GetUpperBound(0); i++)
            {
                string[] xdetil = x[i].Split(';');

                if (xdetil[1] != "TENANT")
                    c = c + Db.SingleInteger("SELECT COUNT(*) FROM " + xdetil[0] + "..MS_TAGIHAN INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK ON MS_TAGIHAN.NoKontrak = MS_KONTRAK.NoKontrak WHERE 1=1 "
                        + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + xdetil[0] + "..MS_PELUNASAN"
                        + "		WHERE NoKontrak = " + xdetil[0] + "..MS_TAGIHAN.NoKontrak AND NoTagihan = " + xdetil[0] + "..MS_TAGIHAN.NoUrut) "
                        + "		> 0)" //kurang bayar
                        + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + xdetil[0] + "..MS_PELUNASAN"
                        + "		WHERE NoKontrak = " + xdetil[0] + "..MS_TAGIHAN.NoKontrak AND NoTagihan = " + xdetil[0] + "..MS_TAGIHAN.NoUrut) "
                        + "		!= NilaiTagihan)" //belum dibayar sama sekali
                        + " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'"
                        );
            }
            countKurang.Text = c.ToString();

            countPJT.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_PJT a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref=b.NoKontrak WHERE a.PrintPJT = 0 AND (SELECT COUNT(*) FROM MS_PJT_JURNAL WHERE NoPJT = a.NoPJT) = 0 AND b.Project = '" + project.SelectedValue + "'"
                ).ToString();

            countTunggakan.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_TUNGGAKAN a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref=b.NoKontrak WHERE a.PrintST = 0 AND (SELECT COUNT(*) FROM MS_TUNGGAKAN_JURNAL WHERE NoTunggakan = a.NoTunggakan) = 0 AND b.Project = '" + project.SelectedValue + "'"
                ).ToString();

            countBelumSettle.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_TUNGGAKAN a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref=b.NoKontrak WHERE a.Status = 'A' AND b.Project = '" + project.SelectedValue + "'"
                ).ToString();

            DateTime Dari = DateTime.Now;
            DateTime TglPJT = Dari.AddDays(7);
            coutPJT7.Text = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_marketingjual..MS_TAGIHAN a"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " AND CONVERT(varchar, TglJT , 112) <= '" + Cf.Tgl112(TglPJT) + "'"
                + " AND CONVERT(varchar, TglJT , 112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND a.NilaiTagihan - ( SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN  c WHERE c.NoKontrak = a.NoKontrak "
                + " AND c.NoTagihan = a.NoUrut ) > 0 "
                + " AND a.NoUrut NOT IN (SELECT NoTagihan FROM " + Mi.DbPrefix + "FINANCEAR..MS_PJT_DETIL d WHERE d.NoPJT IN (SELECT NoPJT FROM " + Mi.DbPrefix + "FINANCEAR..MS_PJT WHERE MS_PJT.Ref = a.NoKontrak)) "
                + " AND b.Status='A' "
                + " AND b.Project = '" + project.SelectedValue + "'").ToString();//+ " WHERE a.TglJT BETWEEN CURRENT_TIMESTAMP AND dateadd(day,+7,getdate())"
            
            countFU.Text = Db.SingleInteger(
            "SELECT COUNT(*)"
                + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN a"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " AND CONVERT(varchar, TglJT , 112) <= '" + Cf.Tgl112(TglPJT) + "'"
                + " AND CONVERT(varchar, TglJT , 112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND a.NilaiTagihan - ( SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN  c WHERE c.NoKontrak = a.NoKontrak "
                + " AND c.NoTagihan = a.NoUrut ) > 0 "
                + " AND a.NoUrut NOT IN (SELECT NoTagihan FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP d WHERE d.NoKontrak = a.NoKontrak) "
                + " AND b.Status='A'"
                + " AND b.Project = '" + project.SelectedValue + "'"
                ).ToString();

            kurang.HRef = "ReminderKurang.aspx?Project=" + project.SelectedValue;
            kurang2.HRef = "ReminderKurang.aspx?Project=" + project.SelectedValue;
            pjt.HRef = "ReminderPJTBaru.aspx?Project=" + project.SelectedValue;
            pjt2.HRef = "ReminderPJTBaru.aspx?Project=" + project.SelectedValue;
            tunggakan.HRef = "ReminderSTBaru.aspx?Project=" + project.SelectedValue;
            tunggakan2.HRef = "ReminderSTBaru.aspx?Project=" + project.SelectedValue;
            settle.HRef = "ReminderBelumSettle.aspx?Project=" + project.SelectedValue;
            settle2.HRef = "ReminderBelumSettle.aspx?Project=" + project.SelectedValue;
            pjt7.HRef = "ReminderPJT7.aspx?Project=" + project.SelectedValue;
            pjt72.HRef = "ReminderPJT7.aspx?Project=" + project.SelectedValue;
            fu.HRef = "ReminderFollowUp.aspx?Project=" + project.SelectedValue;
            fu2.HRef = "ReminderFollowUp.aspx?Project=" + project.SelectedValue;
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
