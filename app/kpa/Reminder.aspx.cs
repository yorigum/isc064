using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA
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

            countBerkas.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK"
                + " WHERE StatusBerkas = 0"
                + " AND CaraBayar = 'KPR'"
                + " AND Status = 'A'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            lblReminderWawancara1.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE StatusWawancara = ''"
                + " AND Status = 'A'"
                + " AND CaraBayar = 'KPR'"
                + " AND StatusBerkas = 1"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            lblReminderWawancara2.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE StatusWawancara = 'DIJADWALKAN'"
                + " AND Status = 'A'"
                + " AND CaraBayar = 'KPR'"
                + " AND StatusBerkas = 1"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countOTS1.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK"
                + " WHERE StatusOTS = ''"
                + " AND Status = 'A'"
                + " AND CaraBayar = 'KPR'"
                + " AND StatusBerkas = 1"
                + " AND StatusWawancara = 'SELESAI'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countOTS2.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK"
                + " WHERE StatusOTS = 'DIJADWALKAN'"
                + " AND Status = 'A'"
                + " AND CaraBayar = 'KPR'"
                + " AND StatusBerkas = 1"
                + " AND StatusWawancara = 'SELESAI'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countOTS3.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK"
                + " WHERE HasilOTS = 'TOLAK'"
                + " AND Status = 'A'"
                + " AND CaraBayar = 'KPR'"
                + " AND StatusBerkas = 1"
                + " AND StatusWawancara = 'SELESAI'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            lblReminderLPA1.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE StatusLPA = ''"
                + " AND Status = 'A'"
                + " AND CaraBayar = 'KPR'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            lblReminderLPA2.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE StatusLPA = 'DIJADWALKAN'"
                + " AND Status = 'A'"
                + " AND CaraBayar = 'KPA'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            lblReminderSP3K1.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE StatusSP3K = ''"
                + " AND Status = 'A'"
                + " AND CaraBayar = 'KPR'"
                + " AND StatusBerkas = 1"
                + " AND StatusOTS = 'SELESAI'"
                + " AND HasilOTS = 'SETUJU'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            lblReminderSP3K2.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE StatusSP3K = 'DIJADWALKAN'"
                + " AND Status = 'A'"
                 + " AND CaraBayar = 'KPR'"
                + " AND StatusBerkas = 1"
                + " AND StatusOTS = 'SELESAI'"
                + " AND HasilOTS = 'SETUJU'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            lblReminderSP3K3.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE StatusSP3K = 'DIAJUKAN'"
                + " AND Status = 'A'"
                + " AND CaraBayar = 'KPR'"
                + " AND StatusBerkas = 1"
                + " AND StatusOTS = 'SELESAI'"
                + " AND HasilOTS = 'SETUJU'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            lblReminderSP3K4.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE HasilSP3K = 'TOLAK'"
                + " AND Status = 'A'"
                + " AND CaraBayar = 'KPR'"
                + " AND StatusBerkas = 1"
                + " AND StatusOTS = 'SELESAI'"
                + " AND HasilOTS = 'SETUJU'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            lblReminderAkad1.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE StatusAkad = ''"
                + " AND Status = 'A'"
                + " AND CaraBayar = 'KPR'"
                + " AND StatusBerkas = 1"
                + " AND StatusSP3K = 'SELESAI'"
                + " AND HasilSP3K = 'SETUJU'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            lblReminderAkad2.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE StatusAkad = 'DIJADWALKAN'"
                + " AND Status = 'A'"
                + " AND CaraBayar = 'KPR'"
                + " AND StatusBerkas = 1"
                + " AND StatusSP3K = 'SELESAI'"
                + " AND HasilSP3K = 'SETUJU'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            berkas.HRef = "ReminderBerkas.aspx?Project=" + project.SelectedValue;
            berkas2.HRef = "ReminderBerkas.aspx?Project=" + project.SelectedValue;
            wawancara.HRef = "ReminderWawancara1.aspx?Project=" + project.SelectedValue;
            wawancara2.HRef = "ReminderWawancara1.aspx?Project=" + project.SelectedValue;
            remwawancara.HRef = "ReminderWawancara2.aspx?Project=" + project.SelectedValue;
            remwawancara2.HRef = "ReminderWawancara2.aspx?Project=" + project.SelectedValue;
            ots.HRef = "ReminderOTS1.aspx?Project=" + project.SelectedValue;
            ots2.HRef = "ReminderOTS1.aspx?Project=" + project.SelectedValue;
            otsjadwal.HRef = "ReminderOTS2.aspx?Project=" + project.SelectedValue;
            otsjadwal2.HRef = "ReminderOTS2.aspx?Project=" + project.SelectedValue;
            otstolak.HRef = "ReminderOTS3.aspx?Project=" + project.SelectedValue;
            otstolak2.HRef = "ReminderOTS3.aspx?Project=" + project.SelectedValue;
            lpa.HRef = "ReminderLPA1.aspx?Project=" + project.SelectedValue;
            lpa2.HRef = "ReminderLPA1.aspx?Project=" + project.SelectedValue;
            lpajadwal.HRef = "ReminderLPA2.aspx?Project=" + project.SelectedValue;
            lpajadwal2.HRef = "ReminderLPA2.aspx?Project=" + project.SelectedValue;
            sp3k.HRef = "ReminderSP3K1.aspx?Project=" + project.SelectedValue;
            sp3k2.HRef = "ReminderSP3K1.aspx?Project=" + project.SelectedValue;
            sp3kjadwal.HRef = "ReminderSP3K2.aspx?Project=" + project.SelectedValue;
            sp3kjadwal2.HRef = "ReminderSP3K2.aspx?Project=" + project.SelectedValue;
            sp3kblm.HRef = "ReminderSP3K3.aspx?Project=" + project.SelectedValue;
            sp3kblm2.HRef = "ReminderSP3K3.aspx?Project=" + project.SelectedValue;
            sp3ktolak.HRef = "ReminderSP3K4.aspx?Project=" + project.SelectedValue;
            sp3ktolak2.HRef = "ReminderSP3K4.aspx?Project=" + project.SelectedValue;
            akad.HRef = "ReminderAkad1.aspx?Project=" + project.SelectedValue;
            akad2.HRef = "ReminderAkad1.aspx?Project=" + project.SelectedValue;
            akadjadwal2.HRef = "ReminderAkad2.aspx?Project=" + project.SelectedValue;
            akadjadwal.HRef = "ReminderAkad2.aspx?Project=" + project.SelectedValue;

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
