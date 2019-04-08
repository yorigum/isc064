using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
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
            countKom.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE KomisiID = '' AND CFID != '' AND Status = 'A' AND Project = '" + project.SelectedValue + "'"
                ).ToString();
            //countCairKom.Text = Db.SingleInteger(
            //    "SELECT COUNT(*) FROM MS_KONTRAK WHERE KomisiID = '' AND CFID != '' AND PersenLunas >= 100 AND Status = 'A' AND Project = '" + project.SelectedValue + "'"
            //    ).ToString();
            countCairKom.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KOMISI_TERM a"
                + " INNER JOIN MS_KOMISI b ON a.NoKomisi = b.NoKomisi"
                + " INNER JOIN MS_KONTRAK c ON b.NoKontrak = c.NoKontrak"
                + " WHERE "
                + "("
                + "(SELECT COUNT(*) FROM MS_KOMISIP_DETAIL WHERE NoKomisi = a.NoKomisi AND SN_KomisiTermin = a.SN) = 0)"
                + " AND c.Status = 'A' AND c.Project = '" + project.SelectedValue + "'"
                + " AND c.PersenLunas >= a.PersenLunas"
                ).ToString();
            countKomP.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KOMISI_TERM a"
                + " INNER JOIN MS_KOMISI b ON a.NoKomisi = b.NoKomisi"
                + " INNER JOIN MS_KONTRAK c ON b.NoKontrak = c.NoKontrak"
                + " WHERE "
                + "("
                + "(SELECT COUNT(*) FROM MS_KOMISIP_DETAIL WHERE NoKomisi = a.NoKomisi AND SN_KomisiTermin = a.SN) = 0)"
                + " AND c.Status = 'A' AND c.Project = '" + project.SelectedValue + "'"
                + " AND c.PersenLunas >= a.PersenLunas"
                ).ToString();

            Kom.HRef = "ReminderGenKom.aspx?Project=" + project.SelectedValue;
            Kom2.HRef = "ReminderGenKom.aspx?Project=" + project.SelectedValue;
            CairKom.HRef = "ReminderCairKom.aspx?Project=" + project.SelectedValue;
            CairKom2.HRef = "ReminderCairKom.aspx?Project=" + project.SelectedValue;
            KomP.HRef = "ReminderKomP.aspx?Project=" + project.SelectedValue;
            KomP2.HRef = "ReminderKomP.aspx?Project=" + project.SelectedValue;
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
