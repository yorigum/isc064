using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class TTSBelumPrint : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Fill();

			if(Request.QueryString["done"]!=null)
			{
				feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
					+ "<a href=\"javascript:popEditKontrak('"+Request.QueryString["done"]+"')\">"
					+ "PPJB Berhasil..."
					+ "</a>";
			}
		}

		private void Fill()
		{
            string strSql = "SELECT A.NoTTS,A.NoNUP,B.Total FROM MS_NUP_PELUNASAN A"
                            + " INNER JOIN ISC064_FINANCEAR..MS_TTS B ON A.NoTTS = B.NoTTS"
                            + " WHERE B.PrintTTS = 0";
			
			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Reminder untuk topik diatas masih kosong.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

                c = new TableCell();
                c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoNUP"] + "')\">"
                    + rs.Rows[i]["NoNUP"] + "</a>";
                r.Cells.Add(c);

				c = new TableCell();
                c.Text = rs.Rows[i]["NoTTS"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["Total"]));
                r.Cells.Add(c);


				
				Rpt.Border(r);
				rpt.Rows.Add(r);
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
