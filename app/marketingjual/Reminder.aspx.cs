using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
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

                if(Request.QueryString["project"] != null)
                {
                    project.SelectedValue = Request.QueryString["project"];
                }
            }
            countCus.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_CUSTOMER WHERE Project = '" + project.SelectedValue + "' AND "
                + "(NoTelp = '' OR NoHP = '' OR "
                + "NoKTP = '' OR KTP1 = '' OR KTP2 = '' OR KTP3 = '' OR KTP4 = '')"
                ).ToString();

            countNPWP.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_CUSTOMER WHERE Project = '" + project.SelectedValue + "' AND "
                + "(NPWP = '' OR "
                + "NPWPAlamat1 = '' OR NPWPAlamat2 = '' OR NPWPAlamat3 = '')"
                ).ToString();

            countObs.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_RESERVASI_OBS WHERE Reminder = 0"
                ).ToString();

            countExpire.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_RESERVASI a INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock WHERE a.NoUrut = 1 AND a.Status = 'E' AND b.Project = '" + project.SelectedValue + "'"
                ).ToString();

            //countPPJB.Text = Db.SingleInteger(
            //	"SELECT COUNT(*) FROM MS_KONTRAK WHERE PPJB = 'B' AND Status = 'A' AND PersenLunas >= 30 "
            //	).ToString();

            //countAJB.Text = Db.SingleInteger(
            //	"SELECT COUNT(*) FROM MS_KONTRAK WHERE AJB = 'B' AND Status = 'A' AND PersenLunas >= 100 "
            //	).ToString();

            //countST.Text = Db.SingleInteger(
            //	"SELECT COUNT(*) FROM MS_KONTRAK WHERE ST = 'T' AND Status = 'A'"
            //	).ToString();

            countOB.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE OutBalance <> 0 AND Status = 'A' AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countGross.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE FlagGross <> 0 AND Status = 'A' AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countKom.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE FlagKomisi = 0 AND Status = 'A' AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countBF.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK"
                + " WHERE "
                + "("
                + "SELECT COUNT(*) FROM MS_PELUNASAN"
                + " INNER JOIN MS_TAGIHAN ON MS_PELUNASAN.NoKontrak = MS_TAGIHAN.NoKontrak AND MS_PELUNASAN.NoTagihan = MS_TAGIHAN.NoUrut"
                + " WHERE MS_TAGIHAN.Tipe NOT IN ('BF')"
                + " AND MS_PELUNASAN.NoKontrak = MS_KONTRAK.NoKontrak"
                + ") = 0"
                + " AND Status = 'A' AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countKomisi.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK"
                + " WHERE FlagKomisi = 2  AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countPaketInvest.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK"
                + " WHERE PaketInvestasi=1 AND TglPaketInvestasi <'" + Cf.Tgl112(DateTime.Today) + "' AND Status='A'"
                + " OR (MS_KONTRAK.PaketInvestasi = 1 AND MS_KONTRAK.TglPaketInvestasi <'" + Cf.Tgl112(DateTime.Today.AddDays(30)) + "' AND MS_KONTRAK.Status='A')"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            cus.HRef = "ReminderCus.aspx?Project=" + project.SelectedValue;
            cus2.HRef = "ReminderCus.aspx?Project=" + project.SelectedValue;
            npwp.HRef = "ReminderCusNPWP.aspx?Project=" + project.SelectedValue;
            npwp2.HRef = "ReminderCusNPWP.aspx?Project=" + project.SelectedValue;
            expire.HRef = "ReminderExpire.aspx?Project=" + project.SelectedValue;
            expire2.HRef = "ReminderExpire.aspx?Project=" + project.SelectedValue;
            OB.HRef = "ReminderOB.aspx?Project=" + project.SelectedValue;
            OB2.HRef = "ReminderOB.aspx?Project=" + project.SelectedValue;
            Gross.HRef = "ReminderGross.aspx?Project=" + project.SelectedValue;
            Gross2.HRef = "ReminderGross.aspx?Project=" + project.SelectedValue;
            Kom.HRef = "ReminderKom.aspx?Project=" + project.SelectedValue;
            Kom2.HRef = "ReminderKom.aspx?Project=" + project.SelectedValue;
            BF.HRef = "ReminderBF.aspx?Project=" + project.SelectedValue;
            BF2.HRef = "ReminderBF.aspx?Project=" + project.SelectedValue;
            Invest.HRef = "ReminderPaketInvest.aspx?Project=" + project.SelectedValue;
            Invest2.HRef = "ReminderPaketInvest.aspx?Project=" + project.SelectedValue;
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
