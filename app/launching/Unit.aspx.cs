using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
	public partial class Unit : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Js.Focus(this,search);
				Bind();
			}
		}

		private void Bind()
		{
			DataTable rs;
			string strSql;

			strSql = "SELECT * FROM REF_JENIS ORDER BY SN";
			rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["Jenis"].ToString();
				string t = v + " - " + rs.Rows[i]["Nama"].ToString();
				jenis.Items.Add(new ListItem(t,v));
			}

			strSql = "SELECT DISTINCT Lokasi FROM MS_UNIT ORDER BY Lokasi";
			rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
				lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));
		}

		protected void display_Click(object sender, System.EventArgs e)
		{
			string Jenis = "";
			if(jenis.SelectedIndex != 0)
				Jenis = " AND Jenis = '" + jenis.SelectedValue + "'";

			string Lokasi = "";
			if(lokasi.SelectedIndex != 0)
				Lokasi = " AND Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";

			string Status = "";
			if(status.SelectedIndex==1)
				Status = " AND Status = 'A'";
			else if(status.SelectedIndex==2)
				Status = " AND Status = 'B'";
            else if (status.SelectedIndex == 3)
                Status = " AND Status = 'H'";

			string strSql = "SELECT"
				+ " NoStock"
				+ ",Status"
				+ ",NoUnit"
				+ ",Luas"
                + ",LuasSG"
                + ",LuasNett"
				+ ",PriceList"
				+ ",Jenis AS Ket"
				+ " FROM MS_UNIT"
				+ " WHERE 1=1"
                + " AND NoStock NOT IN (SELECT NoStock FROM MS_RESERVASI WHERE Status='A')" 
				+ Jenis
				+ Lokasi
				+ Status
				+ " ORDER BY NoUnit";
			
			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Tidak terdapat unit dengan kriteria seperti tersebut diatas.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href=\"javascript:call('"+rs.Rows[i]["NoStock"]+"')\">"
					+ rs.Rows[i]["NoStock"].ToString() + "</a>";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				r.Cells.Add(c);

                string Type = Db.SingleString("SELECT Nama FROM REF_JENIS WHERE Jenis = '" + rs.Rows[i]["Ket"].ToString() + "'");
                c = new TableCell();
                c.Text = Type;
                r.Cells.Add(c);

                //c = new TableCell();
                //c.Text = Cf.Num(rs.Rows[i]["Luas"]);
                //c.HorizontalAlign = HorizontalAlign.Right;
                //r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LuasSG"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LuasNett"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

				c = new TableCell();
				string StatusUnit = Db.SingleString("SELECT Status FROM MS_UNIT WHERE NoUnit = '" + Cf.Pk(rs.Rows[i]["NoUnit"]) + "'");
				if(StatusUnit == "A")
					c.Text = Cf.Num(rs.Rows[i]["PriceList"]);
				else
					c.Text = "";
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				if(StatusUnit == "A")
					c.Text = "<a href=\"TabelStok2.aspx?NoUnit=" + rs.Rows[i]["NoUnit"] + "\">"
						+ "Reservasi...</a>";
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
