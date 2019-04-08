using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
	public partial class Customer : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			Js.Focus(this, keyword);
			Js.ConfirmKeyword(this, keyword);
		}

		protected void search_Click(object sender, System.EventArgs e)
		{
			string[] x = Sc.MktCatalog();
			for(int i=0;i<=x.GetUpperBound(0);i++)
			{
				string[] xdetil = x[i].Split(';');
				Fill(xdetil[0],xdetil[1]);
			}
		}

		private void Fill(string Tb, string Ket)
		{
			string strSql = "";

			if(Ket=="TENANT")
			{
				strSql = "SELECT "
					+ " NoPenghuni AS Ref"
					+ ",NoUnit"
					+ ",Nama AS Cs"
					+ ",MS_PENGHUNI.Status"
					+ " FROM "+Tb+"..MS_PENGHUNI AS MS_PENGHUNI "
					+ " WHERE NoPenghuni + NoUnit + Nama"
					+ " LIKE '%" + Cf.Str(keyword.Text) +"%'"
					+ " ORDER BY NoPenghuni";
			}
			else
			{
				
				strSql = "SELECT "
					+ " NoKontrak AS Ref"
					+ ",NoUnit"
					+ ",Nama AS Cs"
					+ ",MS_KONTRAK.Status"
					+ " FROM "+Tb+"..MS_KONTRAK AS MS_KONTRAK INNER JOIN "+Tb+"..MS_CUSTOMER AS MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer "
					+ " WHERE NoKontrak + NoUnit + Nama"
					+ " LIKE '%" + Cf.Str(keyword.Text) +"%'"
					+ " ORDER BY NoKontrak";
			}

			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;
				
				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href=\"javascript:call('"+rs.Rows[i]["Ref"]+"','"+Ket+"')\">"
					+ rs.Rows[i]["Ref"] + "</a>";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
				r.Cells.Add(c);
				
				c = new TableCell();
				c.Text = Ket;
				r.Cells.Add(c);
				
				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Cs"].ToString();
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
