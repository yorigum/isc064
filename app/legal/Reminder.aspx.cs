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
                if (Request.QueryString["project"] != null)
                {
                    project.SelectedValue = Request.QueryString["project"];
                }
            }
            //countCus.Text = Db.SingleInteger(
            //	"SELECT COUNT(*) FROM MS_CUSTOMER WHERE "
            //	+ "NoTelp = '' OR NoHP = '' OR "
            //	+ "NoKTP = '' OR KTP1 = '' OR KTP2 = '' OR KTP3 = '' OR KTP4 = ''"
            //	).ToString();

            //countNPWP.Text = Db.SingleInteger(
            //    "SELECT COUNT(*) FROM MS_CUSTOMER WHERE "
            //    + "NPWP = '' OR "
            //    + "NPWPAlamat1 = '' OR NPWPAlamat2 = '' OR NPWPAlamat3 = ''"
            //    ).ToString();

            //countObs.Text = Db.SingleInteger(
            //	"SELECT COUNT(*) FROM MS_RESERVASI_OBS WHERE Reminder = 0"
            //	).ToString();

            //countExpire.Text = Db.SingleInteger(
            //	"SELECT COUNT(*) FROM MS_RESERVASI WHERE NoUrut = 1 AND Status = 'E'"
            //	).ToString();

            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + project.SelectedValue + "'");
            string ParamID = "FormatPPJB" + Project;
            string a = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");
            decimal minppjb = Convert.ToDecimal(a);
            persenppjb.InnerHtml = Convert.ToString(minppjb);

            string ParamID2 = "FormatAJB" + Project;
            string b = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID2 + "'");
            decimal minajb = Convert.ToDecimal(b);
            persenajb.InnerHtml = Convert.ToString(minajb);

            countPPJB.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE PPJB = 'B' AND Status = 'A' AND Project = '" + project.SelectedValue + "' AND PersenLunas >= " + minppjb
                ).ToString();

            countAJB.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE AJB = 'B' AND Status = 'A' AND Project = '" + project.SelectedValue + "' AND PersenLunas >= " + minajb
                ).ToString();

            countST.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE ST = 'B' AND Status = 'A' AND Project = '" + project.SelectedValue + "' AND CONVERT(VARCHAR,TargetST,112) < '" + Cf.Tgl112(DateTime.Today) + "'"
                ).ToString();

            countOB.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE OutBalance <> 0 AND Status = 'A' AND Project IN (" + Act.ProjectListSql + ")"
                ).ToString();

            countGross.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE FlagGross <> 0 AND Status = 'A' AND Project IN (" + Act.ProjectListSql + ")"
                ).ToString();

            countKom.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE FlagKomisi = 0 AND Status = 'A' AND Project IN (" + Act.ProjectListSql + ")"
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
                + " AND Status = 'A'"
                ).ToString();

            countKomisi.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK"
                + " WHERE FlagKomisi = 2"
                ).ToString();

            countPaketInvest.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK"
                + " WHERE PaketInvestasi=1 AND TglPaketInvestasi <'" + Cf.Tgl112(DateTime.Today) + "' AND Status='A'"
                + " OR (MS_KONTRAK.PaketInvestasi = 1 AND MS_KONTRAK.TglPaketInvestasi <'" + Cf.Tgl112(DateTime.Today.AddDays(30)) + "' AND MS_KONTRAK.Status='A')"
                ).ToString();

            ppjb.HRef = "ReminderPPJB.aspx?Project=" + Project;
            ppjb2.HRef = "ReminderPPJB.aspx?Project=" + Project;
            ajb.HRef = "ReminderAJB.aspx?Project=" + Project;
            ajb2.HRef = "ReminderAJB.aspx?Project=" + Project;
            st.HRef = "ReminderST.aspx?Project=" + Project;
            st1.HRef = "ReminderST.aspx?Project=" + Project;
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
