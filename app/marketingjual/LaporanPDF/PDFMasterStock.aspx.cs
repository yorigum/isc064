using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;


namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class MasterStock : System.Web.UI.Page
	{
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string nLokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
		{
            Report();
		}

		private void Report()
		{
			
			rpt.Visible = true;
			
			newHeader();
			//Header();
			Fill();
		}

		private void newHeader()
		{
			string header = "<h2>"+Mi.Pt+"</h2>";
			header += "<h1 class='title'>LAPORAN MASTER STOCK PER LANTAI</h1>";
            header += "<h4>Lokasi : " + nLokasi;
            header += "<br>Project : " + Project + "</h4>";
            header += "Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
			header += ", " + Cf.Date(DateTime.Now)+" dari workstation "+Act.IP+" oleh user "+Act.UserID+"<br /><br />";
			headJudul.Text = header;
		}

		

		private void Fill()
		{
            string nProject = "";
            if (Project != "SEMUA") nProject = " AND c.Project IN ('" + Project.Replace(",", "','") + "')";

            string Lokasi = "";

            if (nLokasi == "SEMUA")
            {
                Lokasi = " AND c.Lokasi IN (SELECT Lokasi FROM REF_LOKASI WHERE Project IN ('" + Project.Replace(",", "','") + "'))";
            }
            else
            {
                Lokasi = " AND c.Lokasi = '" + nLokasi + "'";
            }

            string strSql = "SELECT *"
				+ " FROM MS_UNIT c "
				+ " WHERE 1 = 1"
                + nProject
				+ Lokasi
                + " ORDER BY NoUnit"
				;

			DataTable rs = Db.Rs(strSql);

            DataTable lantai = Db.Rs("SELECT DISTINCT(Lantai) FROM MS_UNIT c WHERE 1 = 1 " + nProject + Lokasi);

			//Show table per lantai
			int jumUnitA = 0, jumUnitB = 0, jumUnitD = 0, grandtotal = 0;
			for(int a = 0; a < lantai.Rows.Count; a ++)
			{
				if(!Response.IsClientConnected)
					 break;

				TableRow r = new TableRow();
				TableCell c;

                int pembagiUnit = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT c WHERE Lantai = '" + lantai.Rows[a]["Lantai"] + "' " + Lokasi);
                int jumA = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT c WHERE Lantai = '" + lantai.Rows[a]["Lantai"] + "' AND Status = 'A' " + Lokasi);
                int jumB = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK a INNER JOIN MS_UNIT c ON a.NoStock=c.NoStock WHERE a.Status = 'A' AND c.Lantai = '" + lantai.Rows[a]["Lantai"] + "' " + Lokasi);
                int jumD = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT c LEFT JOIN MS_KONTRAK b ON c.NoUnit=b.NoUnit WHERE c.Lantai = '" + lantai.Rows[a]["Lantai"] + "' AND c.Status='B' AND b.NoUnit IS NULL " + Lokasi);
                r.VerticalAlign = VerticalAlign.Top;

				c = new TableCell();
				string tower = Db.SingleString("SELECT Lokasi FROM MS_UNIT c WHERE NoUnit LIKE '%"+ lantai.Rows[a]["Lantai"] + "%' " + Lokasi);
				c.Text = tower;
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

                //lantai
				c = new TableCell();
				c.Text = lantai.Rows[a]["Lantai"].ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[a]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                //jum Available/lantai
				c = new TableCell();
				c.Text = Convert.ToString(jumA);
				jumUnitA += jumA;
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

                //persenAvailable/lantai
				c = new TableCell();
                if (jumA == 0)
                {
                    c.Text = "0";
                }
                else
                    c.Text = String.Format("{0:0.00}", (((decimal)jumA / (decimal)(jumA + jumB + jumD)) * (decimal)100)) + " %";
                c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);
				

                //jumSold/lantai

				c = new TableCell();
				c.Text = Convert.ToString(jumB);
				jumUnitB += jumB;
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);
                
                //persenSold/lantai
				c = new TableCell();
                if (jumB == 0)
                {
                    c.Text = "0";
                }
                else
                    c.Text = String.Format("{0:0.00}", ((decimal)jumB / (decimal)(jumA + jumB + jumD)) * (decimal)100) + " %"; ;
                c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);
				

                //jumHold/lantai
				c = new TableCell();
				c.Text = jumD.ToString();
				jumUnitD += jumD;
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);
				

                //persenHold/lantai
				c= new TableCell();
                if (jumD == 0)
                {
                    c.Text = "0";
                }
                else
                    c.Text = String.Format("{0:0.00}", ((decimal)jumD / (decimal)(jumA + jumB + jumD)) * (decimal)100) + " %";
                c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);
				
				c = new TableCell();
				c.Text = Convert.ToString(jumA + jumB + jumD);
				grandtotal += (jumA + jumB + jumD);
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

				c = new TableCell();
                if ((jumA == 0 && jumD == 0))
                {
                    c.Text = "0";
                }
                else
                    c.Text = Cf.Num(((jumA + jumB + jumD) / (jumA + jumB + jumD)) * (decimal)100) + " %"; ;
                c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

				rpt.Rows.Add(r);
				
				// Grand Total
				if (a == lantai.Rows.Count-1)
				{
					TableRow r1 = new TableRow();
					TableCell c1;

					c1 = new TableCell();
					c1.Text = "<strong>TOTAL</strong>";
					c1.ColumnSpan = 3;
					c1.Attributes["style"] = "border-top:1px solid black";
					r1.Cells.Add(c1);
					
					c1 = new TableCell();
					c1.Text = "<strong>"+jumUnitA.ToString()+"</strong>";
					c1.Attributes["style"] = "border-top:1px solid black";
					c1.HorizontalAlign = HorizontalAlign.Center;
					r1.Cells.Add(c1);

					c1 = new TableCell();
					c1.Text = "<strong>"+String.Format("{0:0.00}",((decimal)jumUnitA / (decimal)(jumUnitA+jumUnitB+jumUnitD))*(decimal)100) + "% </strong>";
					c1.Attributes["style"] = "border-top:1px solid black";
					c1.HorizontalAlign = HorizontalAlign.Center;
					r1.Cells.Add(c1);

					c1 = new TableCell();
					c1.Text = "<strong>"+jumUnitB.ToString()+"</strong>";
					c1.Attributes["style"] = "border-top:1px solid black";
					c1.HorizontalAlign = HorizontalAlign.Center;
					r1.Cells.Add(c1);

					c1 = new TableCell();
					c1.Text = "<strong>"+String.Format("{0:0.00}",((decimal)jumUnitB / (decimal)(jumUnitA+jumUnitB+jumUnitD))*(decimal)100) + "% </strong>";
					c1.Attributes["style"] = "border-top:1px solid black";
					c1.HorizontalAlign = HorizontalAlign.Center;
					r1.Cells.Add(c1);
					
					c1 = new TableCell();
					c1.Text = "<strong>"+jumUnitD.ToString()+"</strong>";
					c1.Attributes["style"] = "border-top:1px solid black";
					c1.HorizontalAlign = HorizontalAlign.Center;
					r1.Cells.Add(c1);

					c1 = new TableCell();
					c1.Text = "<strong>"+String.Format("{0:0.00}",((decimal)jumUnitD / (decimal)(jumUnitA+jumUnitB+jumUnitD))*(decimal)100) + "% </strong>";
					c1.Attributes["style"] = "border-top:1px solid black";
					c1.HorizontalAlign = HorizontalAlign.Center;
					r1.Cells.Add(c1);
					
					c1 = new TableCell();
					c1.Text = "<strong>"+grandtotal.ToString()+"</strong>";
					c1.Attributes["style"] = "border-top:1px solid black";
					c1.HorizontalAlign = HorizontalAlign.Center;
					r1.Cells.Add(c1);
					
					c1 = new TableCell();
					c1.Text = "<strong>"+String.Format("{0:0.00}",((decimal)(jumUnitA+jumUnitB+jumUnitD) / (decimal)(jumUnitA+jumUnitB+jumUnitD))*(decimal)100) + "% </strong>";
					c1.Attributes["style"] = "border-top:1px solid black";
					c1.HorizontalAlign = HorizontalAlign.Center;
					r1.Cells.Add(c1);

					rpt.Rows.Add(r1);
				}
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
