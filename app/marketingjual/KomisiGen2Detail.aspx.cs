using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KomisiGen2Detail : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label periode;
		protected System.Web.UI.WebControls.Label periodesampai;
		
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			string strSql = "SELECT * FROM MS_AGENT"
				+ " WHERE NoAgent = '" + NoAgent + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				agent.Text = rs.Rows[0]["Nama"].ToString() ;
			}
			
			if(!Page.IsPostBack)
			{
				
			}

            FillTb(NoAgent);
		}

		private void FillTb(string NoAgent)
		{
			DateTime Dari = Convert.ToDateTime(Request.QueryString["Dari"]);
			DateTime Sampai = Convert.ToDateTime(Request.QueryString["Sampai"]);

			string strSql = "SELECT "
				+ " NoKontrak"
				+ ",MS_KONTRAK.NoUnit"
				+ ",NilaiKontrak"
				+ ",TglKontrak"
                + ",PersenLunas"
				+ ",Nama AS Cs"
				+ ",MS_UNIT.Lokasi"
                + ",NilaiPPN"
                + ",NilaiDPP"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_UNIT ON MS_KONTRAK.NoUnit = MS_UNIT.NoUnit "
				+ " WHERE NoAgent = " + NoAgent
				+ " AND MS_KONTRAK.Status  = 'A'"
				+ " AND FlagKomisi = '0' " 
				+ " AND TglKontrak >= '"+Dari+"' and TglKontrak <= '"+Sampai+"' "
				+ " ORDER BY NoKontrakManual";
			
			DataTable rs = Db.Rs(strSql);
			//Rpt.NoData(rpt, rs, "Agen ini belum pernah melakukan penjualan / Komisi untuk periode ini sudah digenerate ");

			if (rs.Rows.Count==0)
			{
				save.Enabled = false ;
			}

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

                HtmlTableRow r = new HtmlTableRow();
				HtmlTableCell c;
                TextBox tb;

                c = new HtmlTableCell();
                c.InnerHtml = "<a href=\"javascript:popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')\">"
					+ rs.Rows[i]["NoKontrak"].ToString()
					+ "</a>";
                c.ID = "pk_" + i;
                c.Attributes["title"] = rs.Rows[i]["NoKontrak"].ToString();
				r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Day(rs.Rows[i]["TglKontrak"]);
				r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
				r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Cs"].ToString();
				r.Cells.Add(c);

                decimal DPP = Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]));

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(DPP);
                c.Align = "right";
				r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(rs.Rows[i]["PersenLunas"]) + " %";
                c.Align = "right";
                r.Cells.Add(c);

                string Tipe = Db.SingleString("SELECT Tipe FROM MS_AGENT WHERE NoAgent = '" + NoAgent + "'");
                string RefEm = Db.SingleString("SELECT NoRefferatorAgent FROM MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'");
                string RefCust = Db.SingleString("SELECT NoRefferatorCustomer FROM MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'");

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Komisi(NoAgent, Tipe, RefEm, RefCust, rs.Rows[i]["NoKontrak"].ToString(), DPP));
                c.Align = "right";
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = TipeReff(rs.Rows[i]["NoKontrak"].ToString(), RefEm, RefCust);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = NamaReff(RefEm, RefCust);
                r.Cells.Add(c);

                tb = new TextBox();
                tb.ID = "persen_" + i;
                tb.Text = TipeReff(rs.Rows[i]["NoKontrak"].ToString(), RefEm, RefCust) == "BUYER" ? "1" : "0.3";
                tb.Visible = TipeReff(rs.Rows[i]["NoKontrak"].ToString(), RefEm, RefCust) != "" ? true : false;
                tb.Enabled = TipeReff(rs.Rows[i]["NoKontrak"].ToString(), RefEm, RefCust) == "BUYER" ? true : false;
                tb.Width = 50;
                tb.CssClass = "right";

                c = new HtmlTableCell();
                c.Controls.Add(tb);
                c.Align = "right";
                r.Cells.Add(c);

				rpt.Rows.Add(r);
			}
		}

        protected decimal Komisi(string NoAgent, string Tipe, string RefEm, string RefCust, string NoKontrak, decimal NilaiDPP)
        {
            decimal Komisi = 0;
            if (Tipe == "INHOUSE")
            {
                if (RefEm == "" && RefCust == "")
                {
                    Komisi = (decimal)0.007 * NilaiDPP;
                }
                else if (RefEm != "" && RefCust == "")
                {
                    Komisi = (decimal)0.003 * NilaiDPP;
                }
                else if (RefEm == "" && RefCust != "")
                {
                    Komisi = (decimal)0.005 * NilaiDPP;
                }
            }
            else if (Tipe == "AGENCY")
            {
                Komisi = (decimal)0.025 * NilaiDPP;
            }

            return Math.Round(Komisi);
        }

        protected string TipeReff(string NoKontrak, string RefEm, string RefCust)
        {
            string Tipe = "";
            if (RefEm != "" && RefCust == "")
            {
                Tipe = "EMPLOYEE";
            }
            else if (RefEm == "" && RefCust != "")
            {
                Tipe = "BUYER";
            }

            return Tipe;
        }

        protected string NamaReff(string RefEm, string RefCust)
        {
            string Nama = "";
            if (RefEm != "" && RefCust == "")
            {
                Nama = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + RefEm + "'");
            }
            else if (RefEm == "" && RefCust != "")
            {
                Nama = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = '" + RefCust + "'");
            }

            return Nama;
        }

        protected void GenKomisi(string NoKontrak, TextBox tb)
        {
            decimal Persen = Convert.ToDecimal(tb.Text) / (decimal)100;
            string strSql = "SELECT "
                + " NoKontrak "
                + ",MS_KONTRAK.NoUnit "
                + ",NilaiKontrak "
                + ",TglKontrak "
                + ",Nama AS Cs "
                + ",MS_UNIT.Lokasi "
                + ", NilaiPPN "
                + ", NilaiDPP "
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_UNIT ON MS_KONTRAK.NoUnit = MS_UNIT.NoUnit "
                + " WHERE NoAgent = " + NoAgent
                + " AND MS_KONTRAK.Status  = 'A'"
                + " AND FlagKomisi = '0' "
                + " AND NoKontrak = '" + NoKontrak + "'";

            DataTable rs = Db.Rs(strSql);

            if(rs.Rows.Count > 0)
            {
                decimal DPP = Convert.ToDecimal(rs.Rows[0]["NilaiDPP"]);
                int NoUrut = 0;

                string Tipe = Db.SingleString("SELECT Tipe FROM MS_AGENT WHERE NoAgent = '" + NoAgent + "'");
                string RefEm = Db.SingleString("SELECT NoRefferatorAgent FROM MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[0]["NoKontrak"] + "'");
                string RefCust = Db.SingleString("SELECT NoRefferatorCustomer FROM MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[0]["NoKontrak"] + "'");

                //Komisi Marketing Inhouse/Agency
                decimal NilaiKomisiSales = Komisi(NoAgent, Tipe, RefEm, RefCust,NoKontrak, DPP);
                NilaiKomisiSales = Math.Round(NilaiKomisiSales / 2);

                NoUrut = Db.SingleInteger("SELECT ISNULL(MAX(NoUrut),0)+1 FROM MS_KOMISI WHERE NoKontrak = '" + rs.Rows[0]["NoKontrak"] + "' ");
                Db.Execute("INSERT INTO MS_KOMISI (NoKontrak, NoUrut, NamaKomisi, NilaiKomisi) VALUES ('" + rs.Rows[0]["NoKontrak"] + "', '" + NoUrut + "', 'Komisi Sales Inhouse 1', '" + NilaiKomisiSales + "'  )");

                NoUrut = Db.SingleInteger("SELECT ISNULL(MAX(NoUrut),0)+1 FROM MS_KOMISI WHERE NoKontrak = '" + rs.Rows[0]["NoKontrak"] + "' ");
                Db.Execute("INSERT INTO MS_KOMISI (NoKontrak, NoUrut, NamaKomisi, NilaiKomisi) VALUES ('" + rs.Rows[0]["NoKontrak"] + "', '" + NoUrut + "', 'Komisi Sales Inhouse 2', '" + NilaiKomisiSales + "'  )");

                //Komisi Refferator Employee
                if (RefEm != "" || RefCust != "")
                {
                    decimal NilaiKomisiReff = Persen * DPP;
                    NilaiKomisiReff = Math.Round(NilaiKomisiReff / 2);

                    string Ket1 = "", Ket2  = "";
                    if (RefEm != "")
                    {
                        Ket1 = "Komisi Refferator Employee 1";
                        Ket2 = "Komisi Refferator Employee 2";
                    }
                    else
                    {
                        Ket1 = "Komisi Refferator Customer 1";
                        Ket2 = "Komisi Refferator Customer 2";
                    }

                    NoUrut = Db.SingleInteger("SELECT ISNULL(MAX(NoUrut),0)+1 FROM MS_KOMISI WHERE NoKontrak = '" + rs.Rows[0]["NoKontrak"] + "' ");
                    Db.Execute("INSERT INTO MS_KOMISI (NoKontrak, NoUrut, NamaKomisi, NilaiKomisi) VALUES ('" + rs.Rows[0]["NoKontrak"] + "', '" + NoUrut + "', '" + Ket1 + "', '" + NilaiKomisiReff + "'  )");

                    NoUrut = Db.SingleInteger("SELECT ISNULL(MAX(NoUrut),0)+1 FROM MS_KOMISI WHERE NoKontrak = '" + rs.Rows[0]["NoKontrak"] + "' ");
                    Db.Execute("INSERT INTO MS_KOMISI (NoKontrak, NoUrut, NamaKomisi, NilaiKomisi) VALUES ('" + rs.Rows[0]["NoKontrak"] + "', '" + NoUrut + "', '" + Ket2 + "', '" + NilaiKomisiReff + "'  )");
                }
                
                Db.Execute("UPDATE MS_KONTRAK SET FlagKomisi = '1' WHERE NoKontrak = '" + rs.Rows[0]["NoKontrak"] + "' ");
            }
        }

		protected void Save_Click(object sender, System.EventArgs e)
		{
            int index = 0;
            foreach (Control tr in rpt.Controls)
            {
                HtmlTableCell c = (HtmlTableCell)rpt.FindControl("pk_" + index);
                TextBox tb = (TextBox)rpt.FindControl("persen_" + index);

                if (c != null)
                {
                    GenKomisi(c.Attributes["title"], tb);
                }

                index++;
            }
            Response.Redirect("KomisiGen2.aspx?done=1");
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

		private string NoAgent
		{
			get
			{
				return Request.QueryString["NoAgent"] ;
			}
		}

		private string LevelAgent
		{
			get
			{
				return Db.SingleString("SELECT Level FROM MS_AGENT WHERE NoAgent = '"+NoAgent+"' ") ;
			}
		}

		private string Principal
		{
			get
			{
				return Db.SingleString("SELECT Principal FROM MS_AGENT WHERE NoAgent = '"+NoAgent+"' ") ;
			}
		}

		private string Supervisor
		{
			get
			{
				return Db.SingleString("SELECT Supervisor FROM MS_AGENT WHERE NoAgent = '"+NoAgent+"' ") ;
			}
		}


	}
}
