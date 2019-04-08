using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class UnitInfo : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Fill();
			}
		}

		private void Fill()
		{
			string strSql = "SELECT * FROM MS_UNIT WHERE NoStock = '" + NoStock + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				jenis.Text = rs.Rows[0]["Jenis"].ToString();

				pl.Text = Cf.Num(rs.Rows[0]["PriceList"]);
                
				luassg.Text = Cf.Num(rs.Rows[0]["LuasSG"]);
				luasnett.Text = Cf.Num(rs.Rows[0]["LuasNett"]);

				zoning.Text = rs.Rows[0]["Zoning"].ToString();
				arahhadap.Text = rs.Rows[0]["ArahHadap"].ToString();
				panorama.Text = rs.Rows[0]["Panorama"].ToString();

				System.Text.StringBuilder strategis = new System.Text.StringBuilder();
				if((bool)rs.Rows[0]["HadapAtrium"])
				{
					strategis.Append("Atrium/Void");
				}
				if((bool)rs.Rows[0]["HadapEntrance"])
				{
					if(strategis.Length!=0) strategis.Append(", ");
					strategis.Append("Entrance");
				}
				if((bool)rs.Rows[0]["HadapEskalator"])
				{
					if(strategis.Length!=0) strategis.Append(", ");
					strategis.Append("Eskalator");
				}
				if((bool)rs.Rows[0]["HadapLift"])
				{
					if(strategis.Length!=0) strategis.Append(", ");
					strategis.Append("Lift");
				}
				if((bool)rs.Rows[0]["HadapParkir"])
				{
					if(strategis.Length!=0) strategis.Append(", ");
					strategis.Append("Parkir");
				}
				if((bool)rs.Rows[0]["HadapAxis"])
				{
					if(strategis.Length!=0) strategis.Append(", ");
					strategis.Append("Axis");
				}
				if((bool)rs.Rows[0]["Hook"])
				{
					if(strategis.Length!=0) strategis.Append(", ");
					strategis.Append("Hook");
				}
				nilaistrategis.Text = strategis.ToString();
                
				FillRsv();
				FillTb();

				//kalkulator skema
				kalk.Attributes["onclick"] = "location.href='Skema.aspx"
					+ "?pl=" + pl.Text
					+ "&tgl=" + DateTime.Today
					+ "'";

				//floor plan
				lokasi.Attributes["onclick"] = "location.href='UnitLokasi.aspx"
					+ "?NoUnit="+rs.Rows[0]["NoUnit"]
					+ "&Peta="+rs.Rows[0]["Peta"]
					+ "'";
			}
		}

		private void FillRsv()
		{	
			string strSql = "SELECT "
				+ " NoUrut"
				+ ",Tgl"
				+ ",TglExpire"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
				+ ",MS_RESERVASI.Status"
				+ ",MS_RESERVASI.NoReservasi"
                + ",MS_RESERVASI.NoReservasi2"
                + ",MS_RESERVASI.TglExpire"
				+ " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
				+ " WHERE NoStock = '" + NoStock + "'"
				+ " ORDER BY NoUrut";
			
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href='ReservasiEdit.aspx?NoReservasi="+rs.Rows[i]["NoReservasi"]+"'>"
					+ rs.Rows[i]["NoReservasi2"].ToString() + "</a>";
                c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Cs"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Ag"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = "W.List : " + rs.Rows[i]["NoUrut"]
					+ ", " + Cf.Date(rs.Rows[i]["TglExpire"]);
				c.Wrap = false;
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		private void FillTb()
		{
			string strSql = "SELECT "
				+ " NoKontrak"
				+ ",TglKontrak"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
				+ ",MS_KONTRAK.Status"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
				+ " WHERE NoStock = '" + NoStock + "'"
				+ " ORDER BY TglKontrak, NoKontrak";
			
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href='KontrakEdit.aspx?NoKontrak="+rs.Rows[i]["NoKontrak"]+"'>"
					+ rs.Rows[i]["NoKontrak"].ToString() + "</a>";
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Cs"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Ag"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = "";
                c.Wrap = false;
                r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		private string NoStock
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoStock"]);
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
