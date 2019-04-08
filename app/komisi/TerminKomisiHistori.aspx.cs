using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
{
	public partial class TerminKomisiHistori : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("Nomor");

			if(!Page.IsPostBack)
			{
				FillTermin(); //Skema Komisi yang make termin ini
			}
		}

		private void FillTermin()
		{	
			string strSql = "SELECT "
				+ " b.NoSkema"
				+ ",b.Nama"
                + ",b.Inaktif"
                + ",b.Dari"
				+ ",b.Sampai"
				+ ",b.Rumus"
				+ ",b.DasarHitung"
				+ ",b.CaraBayar"
				+ " From ref_skom_term a inner join REF_SKOM b on a.NoTermin = b.NoTermin"
                + " WHERE b.NoTermin = '" + Nomor + "'"
				+ " ORDER BY b.NoSkema";


			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = rs.Rows[i]["NoSkema"].ToString().PadLeft(5,'0');
                c.Wrap = false;
				r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                if(Convert.ToBoolean(rs.Rows[i]["Inaktif"]) != true)
                {
                    c.Text = "Aktif";
                }
                else
                {
                    c.Text = "Non Aktif";
                }
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Dari"]);
				r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Sampai"]);
                r.Cells.Add(c);

                c = new TableCell();
				c.Text = rs.Rows[i]["Rumus"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["DasarHitung"].ToString();
				r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["CaraBayar"].ToString();
                r.Cells.Add(c);

                Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		private string Nomor
		{
			get
			{
				return Cf.Pk(Request.QueryString["Nomor"]);
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
