using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
{
	public partial class ReminderCairKom : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
			Fill();
            ok.HRef = "Reminder.aspx?project=" + Project;
		}
        private void Fill()
        {
            //string strSql = "SELECT "
            //    + " NoUnit AS Unit"
            //    + ",MS_KONTRAK.NoKontrak AS Kontrak"
            //    + ",MS_CUSTOMER.Nama AS Customer"
            //    + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Sales"
            //    + ",CONVERT(varchar, TglKontrak, 112)"
            //    + ",FORMAT(NilaiKontrak,'#,###') AS Nilai"
            //    + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
            //    + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
            //    + " INNER JOIN MS_AGENT"
            //    + " ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
            //    + " WHERE 1=1"
            //    + " AND MS_KONTRAK.Status = 'A' AND MS_KONTRAK.KomisiID = '' AND CFID != '' AND MS_KONTRAK.PersenLunas >= 100"
            //    + " ORDER BY MS_KONTRAK.NoKontrak";
            string strSql = "SELECT a.*, b.NoKontrak, b.NoUnit, b.NamaAgent, b.NamaCust"
                + " FROM MS_KOMISI_TERM a"
                + " INNER JOIN MS_KOMISI b ON a.NoKomisi = b.NoKomisi"
                + " INNER JOIN MS_KONTRAK c ON b.NoKontrak = c.NoKontrak"
                + " WHERE 1=1 "
                + " AND (SELECT COUNT(*) FROM MS_KOMISIP_DETAIL WHERE NoKomisi = a.NoKomisi AND SN_KomisiTermin = a.SN) = 0"
                + " AND c.PersenLunas >= a.PersenLunas"
                + " AND c.Status = 'A'"
                + " AND c.Project = '" + Project + "'"
                + " ORDER BY b.NoKomisi";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ada logfile untuk periode tanggal tersebut.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<a href=\"KomisiPRegis1.aspx?kom=1&NoKontrak='''+NoKontrak+'''\">" + rs.Rows[i]["NoKontrak"].ToString() + "</a>";
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["NoUnit"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaCust"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaAgent"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiCair"]);
                c.Wrap = false;
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
            if (rpt.Rows.Count == 0) kosong.InnerText = "Reminder untuk topik diatas masih kosong.";
        }
        private string Project
        {
            get
            {
                return Cf.Str(Request.QueryString["Project"]);
            }
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
