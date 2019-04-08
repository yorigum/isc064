using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class MasterCustomer : System.Web.UI.Page
	{
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusI { get { return (Request.QueryString["status_i"]); } }
        private string nSifat { get { return (Request.QueryString["sifat"]); } }
        private string Nama { get { return (Request.QueryString["nama"]); } }
        private string Agama { get { return (Request.QueryString["agama"]); } }
        private string SumberData { get { return (Request.QueryString["sumberdata"]); } }
        private string Input { get { return (Request.QueryString["input"]); } }
        private string nLahir { get { return (Request.QueryString["blnlahir"]); } }
        private string nAgentInput { get { return (Request.QueryString["agentinput"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        protected void Page_Load(object sender, System.EventArgs e)
		{
            Report();
		}

		
		private void Report()
		{
			param.Visible = false;
			rpt.Visible = true;

			Header();
			Fill();
		}

		private void Header()
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			Rpt.Judul(x, comp, judul);

            if (StatusA != "")
                Rpt.SubJudul(x, "Status : " + StatusA);
            else if (StatusI != "")
                Rpt.SubJudul(x, "Status : " + StatusI);
            else
                Rpt.SubJudul(x, "Status : " + StatusS);

			string sifat = "";

            if (nSifat == "sifatALL") sifat = "SEMUA";
            else if (nSifat == "sifatSUDAH") sifat = "sifatSUDAH";
            else if (nSifat == "sifatBELUM") sifat = "BELUM BELI";

                Rpt.SubJudul(x
                    , "Sifat : " + sifat
                    );

			Rpt.SubJudul(x
				, "Nama : " + Nama.Replace("-", ",").TrimEnd(',')
				);

			Rpt.SubJudul(x
                , "Agama : " + Agama.Replace("-", ",").TrimEnd(',')
				);

			Rpt.SubJudul(x
                , "Sumber Data : " + SumberData.Replace("-", ",").TrimEnd(',')
				);

			Rpt.SubJudul(x
				, "Periode Input : " + Input
				);

			Rpt.SubJudul(x
				, "Bulan Lahir : " + nLahir
				);

			Rpt.SubJudul(x
				, "Sales Account : " + nAgentInput
				);

            Rpt.SubJudul(x
                , "Project : " + Project
                );

            //Rpt.Header(rpt, x);
            string legend = "* * = Inaktif";
            Rpt.HeaderReport(headReport, legend, x);
		}

		private void Fill()
		{
			string order = "Nama, NoCustomer";

            string nStatus = "";
            if (StatusA != "") nStatus = " AND Status = 'A'";
            if (StatusI != "") nStatus = " AND Status = 'I'";

			string Sifat = "";
            if (nSifat == "sifatSUDAH") Sifat = " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoCustomer = MS_CUSTOMER.NoCustomer) <> 0";
			if(nSifat == "sifatBELUM") Sifat = " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoCustomer = MS_CUSTOMER.NoCustomer) = 0";

            //change parameter
            string str = String.Empty;
            str = Nama.Replace("-", "").ToLower();
            char[] characters = str.ToCharArray();
            string str2 = "";
            foreach (var t in characters)
            {
                str2 += "'" + t + "',";
            }
            str2 = str2.TrimEnd(',');

            //change parameter
            string stragama = String.Empty;
            stragama = Agama.Replace("-", "").ToLower();


            string aq = "";
            if (Nama != "")
                aq = aq + " AND LEFT(Nama,1) IN (" + str2 + ")";


            string nInput = "";
            if (Input != "SEMUA")
            {
                string[] z = Input.Split('-');
                nInput = " AND YEAR(TglInput) = " + z[0]
                    + " AND MONTH(TglInput) = " + z[1];
            }

			string Lahir = "";
			if(nLahir != "SEMUA")
			{
				order = " DAY(TglLahir), Nama";
				Lahir = " AND MONTH(TglLahir) = " + nLahir;
			}

			string AgentInput = "";
			if(nAgentInput != "SEMUA")
			{
				AgentInput = " AND AgentInput = '" + nAgentInput + "'";
			}

            string nProject = "";
            if (Project != "SEMUA")
            {
                nProject = " AND Project IN ('" + Project.Replace(",", "','") + "')";
            }

            string strdata = String.Empty;
            string [] str3 = SumberData.Split('-');
            for (int i = 0; i < str3.Length; i++)
            {
                if (i == str3.Length - 1)
                {
                    strdata += "'" + str3[i] + "'";
                }
                else
                {
                    strdata += "'" + str3[i] + "',";
                }
            }
            //nSumberData = nSumberData.TrimEnd('-');
            //nSumberData = nSumberData.Replace("-", "','");
            //nSumberData = "'" + nSumberData;            

            //string nSumberData2 = nSumberData;
            //nSumberData2 = nSumberData + "'";

            string stringAgama = Agama;
            stringAgama = stringAgama.TrimEnd('-');
            stringAgama = stringAgama.Replace("-", "','");
            stringAgama = "'" + stringAgama;

            string agama = stringAgama;
            agama = stringAgama + "'";
            
            string strSql = "SELECT *"
                + ",CASE (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoCustomer = MS_CUSTOMER.NoCustomer)"
                + "		WHEN 0 THEN 'BELUM BELI'"
                + "		ElSE 'SUDAH BELI'"
                + " END AS Sifat"
                + " FROM MS_CUSTOMER"
                + " WHERE "
                + " SumberData IN (" + strdata + ")"
                //+ " AND Agama IN (" + agama + ")"
                + nProject
                + nStatus
                + aq
                + nInput
                + Lahir
                + AgentInput
                + Sifat
                + " ORDER BY " + order
            ;
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;
				r.Attributes["ondblclick"] = "popEditCustomer('"+rs.Rows[i]["NoCustomer"]+"')";

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditCustomer('" + rs.Rows[i]["NoCustomer"] + "')";
				c = new TableCell();
				c.Text = (i+1).ToString() + ".";
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);
				
				string inaktif = "";
				if(rs.Rows[i]["Status"].ToString()=="I")
					inaktif = " **";

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["TipeCs"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["SumberData"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);
				
				c = new TableCell();
				string unit = "SELECT NoUnit FROM MS_KONTRAK WHERE NoCustomer = '" + rs.Rows[i]["NoCustomer"].ToString() + "'";
				DataTable rsUnit = Db.Rs(unit);
				string addUnit = "";
				for (int j = 0 ; j < rsUnit.Rows.Count; j++)
				{
					addUnit = rsUnit.Rows[j]["NoUnit"] + ", ";
				}
				if (addUnit != "")
					addUnit = addUnit.Substring(0,addUnit.Length-2);
				c.Text = addUnit;
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["AgentInput"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NamaBisnis"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["JenisBisnis"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["MerekBisnis"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Agama"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglLahir"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoTelp"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoHP"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKantor"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoFax"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Email"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKTP"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Alamat1"] + "&nbsp;"
					+ rs.Rows[i]["Alamat2"] + "&nbsp;"
					+ rs.Rows[i]["Alamat3"] + "&nbsp;"
					;
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["KTP1"] + "&nbsp;"
					+ rs.Rows[i]["KTP2"] + "&nbsp;"
					+ rs.Rows[i]["KTP3"] + "&nbsp;"
					+ rs.Rows[i]["KTP4"] + "&nbsp;"
					;
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NPWPAlamat1"] + "&nbsp;"
                    + rs.Rows[i]["NPWPAlamat2"] + "&nbsp;"
                    + rs.Rows[i]["NPWPAlamat3"] + "&nbsp;"
                    ;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Sifat"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				if(rs.Rows[i]["Sifat"].ToString()=="SUDAH BELI")
				{
					PrintSales((int)rs.Rows[i]["NoCustomer"],r);
				}
				else
				{
					c = new TableCell();
					c.Text = "";
					c.ColumnSpan = 5;
					c.Wrap = false;
					r.Cells.Add(c);
				}

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglInput"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglTransaksi"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
			}
		}

		private void PrintSales(int NoCustomer, TableRow r)
		{
			TableCell c;

			DataTable rs = Db.Rs("SELECT TOP 1 * FROM MS_KONTRAK WHERE NoCustomer = "+NoCustomer
				+ " ORDER BY NoKontrak DESC");
			for(int i=0;i<rs.Rows.Count;i++)
			{
				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

                decimal LuasSG = Db.SingleDecimal("SELECT LuasSG FROM MS_UNIT WHERE NoStock = '" + rs.Rows[i]["NoStock"] + "'");
				c = new TableCell();
				c.Text = Cf.Num(LuasSG);
				c.HorizontalAlign = HorizontalAlign.Right;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Skema"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);
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
