using System;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064
{
	/// <summary>
	/// Reporting common functions
	/// </summary>
	public class Rpt
	{
		#region public static void NoData(Table rpt, System.Data.DataTable rs, string Notes)
		public static void NoData(Table rpt, System.Data.DataTable rs, string Notes)
		{
			if(rs.Rows.Count==0)
			{
				TableRow r = new TableRow();
				TableCell c;
				
				c = new TableCell();
				c.Text = Notes;
				c.ColumnSpan = 100;
				r.Cells.Add(c);

				rpt.Rows.Add(r);
			}
		}
		#endregion
		#region public static void NoData(System.Text.StringBuilder x, System.Data.DataTable rs, string Notes)
		public static void NoData(System.Text.StringBuilder x, System.Data.DataTable rs, string Notes)
		{
			if(rs.Rows.Count==0)
			{
				x.Append(Notes);
			}
		}
		#endregion
		#region public static void NoData(PlaceHolder list, System.Data.DataTable rs, string Notes)
		public static void NoData(PlaceHolder list, System.Data.DataTable rs, string Notes)
		{
			if(rs.Rows.Count==0)
			{
				Label l = new Label();
				l.Text = "<tr><td colspan=100>"+Notes+"</td></tr>";
				list.Controls.Add(l);
			}
		}
		#endregion

		#region public static void Judul(StringBuilder x, HtmlGenericControl comp, HtmlGenericControl judul)
		public static void Judul(StringBuilder x, HtmlGenericControl comp, HtmlGenericControl judul)
		{
			x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
				+ comp.InnerHtml
				+ "</h2>");

			x.Append("<h1 class='title' style='margin:0;font:bold 20pt'>"
                + judul.InnerHtml
				+ "</h1>");
		}
        #endregion
        #region public static void JudulReport(StringBuilder x, HtmlGenericControl comp, HtmlGenericControl judul)
        public static void JudulReport(StringBuilder x, HtmlGenericControl comp, HtmlGenericControl judul)
        {
            x.Append("<h2 style='margin:0;font:bold 10pt'>"
                + comp.InnerHtml
                + "</h2>");

            x.Append("<h1 class='title' style='margin:0;'>"
                + judul.InnerHtml
                + "</h1>");
        }
        #endregion
        #region public static void SubJudul(StringBuilder x, string txt)
        public static void SubJudul(StringBuilder x, string txt)
		{
			x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
				+ txt
				+ "</h2>");
		}
        #endregion
        #region public static void SubJudulReport(StringBuilder x, string txt)
        public static void SubJudulReport(StringBuilder x, string txt)
        {
            x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + txt
                + "</h2>");
        }
        #endregion
        #region public static void Header(Table rpt, StringBuilder x)
        public static void Header(Table rpt, StringBuilder x)
		{
			TableRow legend = rpt.Rows[0];
			legend.Cells[0].Text = "Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
				+ ", " + Cf.Date(DateTime.Now)
				+ " dari workstation : " + Act.IP
				+ " dan username : " + Act.UserID
				+ "<br />"
				+ legend.Cells[0].Text;
			
			TableRow th = rpt.Rows[1];
			for(int i=0;i<th.Cells.Count;i++)
				th.Cells[i].Attributes["style"] = "background-color:gray;color:white;";
			
			TableRow title = new TableRow();

			TableCell c = new TableCell();
			c.Text = x.ToString();
			c.ColumnSpan = th.Cells.Count;
			title.Cells.Add(c);

			rpt.Rows.Add(title);
			rpt.Rows.Add(legend);
			rpt.Rows.Add(th);
		}
        #endregion
        #region public static void HeaderReport( HtmlGenericControl rpt,HtmlGenericControl legend, StringBuilder x)
        public static void HeaderReport(HtmlGenericControl rpt, string legend, StringBuilder x)
        {

            rpt.InnerHtml = x.ToString() + "<br />";
            rpt.InnerHtml += "Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
                + ", " + Cf.Date(DateTime.Now)
                + "<br />"
                + " dari workstation : " + Act.IP
                + " dan username : " + Act.UserID
                + "<br />"
                + legend + "<br />";
        }
        #endregion
        #region public static TableCell Foot()
        public static TableCell Foot()
		{
			TableCell c = new TableCell();
			c.Font.Bold = true;
			c.Wrap = false;
			c.Attributes["style"] = "border-top:1px solid black";

			return c;
		}
		#endregion

		#region public static string inSql(CheckBoxList cb)
		public static string inSql(CheckBoxList cb)
		{
			StringBuilder x = new StringBuilder();
			for(int i=0;i<cb.Items.Count;i++)
			{
				if(cb.Items[i].Selected)
				{
					if(x.ToString()!="") x.Append(",");

					x.Append("'"+Cf.Str(cb.Items[i].Value)+"'");
				}
			}

			return x.ToString();
		}
		#endregion
		#region public static string inSqlMinggu(ListBox minggu)
		public static string inSqlMinggu(ListBox minggu)
		{
			string x = "";
			switch(minggu.SelectedValue)
			{
				case "1":
					x = "1,2,3,4,5,6,7";
					break;
				case "2":
					x = "8,9,10,11,12,13,14,15";
					break;
				case "3":
					x = "16,17,18,19,20,21,22,23";
					break;
				case "4":
					x = "24,25,26,27,28,29,30,31";
					break;
			}

			return x;
		}
		#endregion

		#region public static void Border(TableRow r)
		public static void Border(TableRow r)
		{
			r.VerticalAlign = VerticalAlign.Top;

			for(int i=0;i<r.Cells.Count;i++)
			{
				r.Cells[i].Attributes["style"] = "border-bottom:1px dashed silver";

                //Cell yang kosong harus diisi spasi kosong agar bisa menampilkan border
                if (r.Cells[i].Text == "")
                {
                    r.Cells[i].Text = "&nbsp;";
                }
			}
		}
        #endregion

        #region public static void BorderNoList(TableRow r)
        public static void BorderNoList(TableRow r)
        {
            r.VerticalAlign = VerticalAlign.Top;

            for (int i = 0; i < r.Cells.Count; i++)
            {
                //r.Cells[i].Attributes["style"] = "border-bottom:1px dashed silver";

                //Cell yang kosong harus diisi spasi kosong agar bisa menampilkan border
                if (r.Cells[i].Text == "")
                {
                    r.Cells[i].Text = "&nbsp;";
                }
            }
        }
        #endregion

        #region public static void ToExcel(System.Web.UI.Page p, Table rpt)
        public static void ToExcel(System.Web.UI.Page p, Table rpt)
		{
			string filename = System.IO.Path.GetFileNameWithoutExtension(
				System.Web.HttpContext.Current.Request.PhysicalPath);

			p.Response.Clear();
			
			p.Response.AddHeader("content-disposition","attachment;filename="+filename+".xls");
			p.Response.ContentType = "application/ms-excel";
			
			System.IO.StringWriter sw = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
			rpt.RenderControl(hw);
			p.Response.Write(sw.ToString());
			
			p.Response.End();
		}
        #endregion
        #region public static void ToExcel(System.Web.UI.Page p, HtmlGenericControl rpt)
        public static void ToExcel(System.Web.UI.Page p, HtmlGenericControl rpt)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(
                System.Web.HttpContext.Current.Request.PhysicalPath);

            p.Response.Clear();

            p.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            p.Response.ContentType = "application/ms-excel";

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            rpt.RenderControl(hw);
            p.Response.Write(sw.ToString());

            p.Response.End();
        }
        #endregion
        #region public static void ToExcel(System.Web.UI.Page p, PlaceHolder rpt)
        public static void ToExcel(System.Web.UI.Page p, PlaceHolder rpt)
		{
			string filename = System.IO.Path.GetFileNameWithoutExtension(
				System.Web.HttpContext.Current.Request.PhysicalPath);

			p.Response.Clear();
			
			p.Response.AddHeader("content-disposition","attachment;filename="+filename+".xls");
			p.Response.ContentType = "application/ms-excel";
			
			System.IO.StringWriter sw = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
			rpt.RenderControl(hw);
			p.Response.Write(sw.ToString());
			
			p.Response.End();
		}
        #endregion
        #region public static void ToExcel(System.Web.UI.Page p, HtmlGenericControl rpt)
        public static void ToExcel(System.Web.UI.Page p, HtmlGenericControl x, Table rpt)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(
                System.Web.HttpContext.Current.Request.PhysicalPath);

            HtmlGenericControl space = new HtmlGenericControl("div");
            space.InnerHtml = "<br />";

            p.Response.Clear();

            p.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            p.Response.ContentType = "application/ms-excel";

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            x.RenderControl(hw);
            space.RenderControl(hw);
            rpt.RenderControl(hw);
            p.Response.Write(sw.ToString());

            p.Response.End();
        }
        #endregion
        #region public static bool ValidateXls(DataTable rs, Table rule, Table gagal)
        public static bool ValidateXls(DataTable rs, Table rule, Table gagal)
		{
			bool x = true;
			gagal.Rows.Clear();

			if(rs.Columns.Count==0)
			{
				Gagal(gagal, "File Excel yang di-upload tidak menggunakan format standard.<br>"
					+ "Silakan download template standard terlebih dahulu.");
				x = false;
			}

			if(x)
			{
				for(int i=1;i<rule.Rows.Count;i++)
				{
					string NamaExcel = rs.Columns[i-1].ColumnName.Replace("#",".");
					string NamaRule = rule.Rows[i].Cells[1].Text;
					
					if(NamaExcel!=NamaRule)
					{
						Gagal(gagal, "File Excel yang di-upload tidak menggunakan format standard.<br>"
							+ "Silakan download template standard terlebih dahulu..");
						x = false;
						break;
					}
				}
			}

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!System.Web.HttpContext.Current.Response.IsClientConnected) break;

				for(int j=0;j<rs.Columns.Count;j++)
				{
					string FormatRule = rule.Rows[j+1].Cells[2].Text;
					string Panjang = rule.Rows[j+1].Cells[3].Text;
					string Ket = rule.Rows[j+1].Cells[4].Text;

					//validasi format
					if(FormatRule=="ANGKA BULAT")
					{
						try
						{
							int z = Convert.ToInt32(rs.Rows[i][j]);
						}
						catch
						{
							Gagal(gagal, "<b>Baris " + (i+2) + ", Kolom "+rs.Columns[j].ColumnName+"</b> : Format ANGKA BULAT");
							x = false;
						}
					}
					else if(FormatRule=="ANGKA")
					{
						try
						{
							decimal z = Convert.ToDecimal(rs.Rows[i][j]);
						}
						catch
						{
							Gagal(gagal, "<b>Baris " + (i+2) + ", Kolom "+rs.Columns[j].ColumnName+"</b> : Format ANGKA");
							x = false;
						}
					}
					else if(FormatRule=="TANGGAL")
					{
						try
						{
							DateTime z = Convert.ToDateTime(rs.Rows[i][j]);
						}
						catch
						{
							Gagal(gagal, "<b>Baris " + (i+2) + ", Kolom "+rs.Columns[j].ColumnName+"</b> : Format TANGGAL");
							x = false;
						}
					}
					else if(FormatRule=="TEKS")
					{
						int l = rs.Rows[i][j].ToString().Length;

						if(l<=0)
						{
							Gagal(gagal, "<b>Baris " + (i+2) + ", Kolom "+rs.Columns[j].ColumnName+"</b> : Kosong");
							x = false;
						}
						else
						{
							if(Panjang!="")
							{
								if(l>Convert.ToInt32(Panjang))
								{
									Gagal(gagal, "<b>Baris " + (i+2) + ", Kolom "+rs.Columns[j].ColumnName+"</b> : Panjang maksimum adalah " + Panjang + " karakter");
									x = false;
								}
							}
						}
					}
					else if(FormatRule=="RANGE")
					{
						string z = rs.Rows[i][j].ToString();
						string[] allow = Ket.Split('/');
						bool samaDenganSatu = false;

						for(int ix=0;ix<=allow.GetUpperBound(0);ix++)
						{
							if(z.ToUpper()==allow[ix].ToUpper())
								samaDenganSatu = true;
						}

						if(!samaDenganSatu || z=="")
						{
							Gagal(gagal, "<b>Baris " + (i+2) + ", Kolom "+rs.Columns[j].ColumnName+"</b> : Data harus ada dalam RANGE");
							x = false;
						}
					}
				}
			}

			return x;
		}
		private static void Gagal(Table gagal, string ket)
		{
			TableRow r = new TableRow();
			TableCell c = new TableCell();
			c.Text = ket;
			c.Attributes["style"] = "padding-left:50";
			r.Cells.Add(c);
			gagal.Rows.Add(r);
		}
        #endregion
        //06-02-2018 untuk Laporan Master Unit di setup sales
        #region public static void EnHeader(Table rpt, StringBuilder x)
        public static void EnHeader(Table rpt, StringBuilder x)
        {
            TableRow legend = rpt.Rows[0];
            legend.Cells[0].Text = "This Report created on : " + Cf.EnWeek(DateTime.Today)
                + ", " + Cf.Date(DateTime.Now)
                + " from workstation : " + Act.IP
                + " and username : " + Act.UserID
                + "<br>"
                + legend.Cells[0].Text;

            TableRow th = rpt.Rows[1];
            for (int i = 0; i < th.Cells.Count; i++)
                th.Cells[i].Attributes["style"] = "background-color:gray;color:white;";

            TableRow title = new TableRow();

            TableCell c = new TableCell();
            c.Text = x.ToString();
            c.ColumnSpan = th.Cells.Count;
            title.Cells.Add(c);

            rpt.Rows.Add(title);
            rpt.Rows.Add(legend);
            rpt.Rows.Add(th);
        }
        #endregion
    }
}
