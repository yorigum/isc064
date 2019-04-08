using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP.Laporan
{
    public partial class TerpilihNUP : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				comp.InnerHtml = Mi.Pt;
				rpt.Visible = false;
				Js.Focus(this,scr);
				init();
				if(!Act.Sec("DownloadExcel")) xls.Enabled = false;
			}
		}

		private void init()
		{
			DataTable rs;
			rs = Db.Rs("SELECT * FROM REF_JENIS ORDER BY SN");
			for(int i=0;i<rs.Rows.Count;i++)
			{
                string v = rs.Rows[i]["Nama"].ToString();
				string t = rs.Rows[i]["Nama"].ToString();
				jenis.Items.Add(new ListItem(t,v));
				jenis.Items[i].Selected = true;
                jenis.Width = 130;
			}

			rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_UNIT ORDER BY Lokasi");
			for(int i=0;i<rs.Rows.Count;i++)
				lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

			lokasi.SelectedIndex = 0;
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isPilih(jenis))
			{
				x = false;
				jenisc.Text = " Pilih Minimum Satu";
			}
			else
				jenisc.Text = "";

			if(!x && s!="")
			{
				RegisterStartupScript("err"
					,"<script language='javascript'>document.getElementById('"+s+"').select()</script>");
			}

			return x;
		}

		protected void scr_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				Report();
			}
		}

		protected void xls_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				Report();
				Rpt.ToExcel(this,rpt);
			}
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

			Rpt.SubJudul(x, "Status : " + statusS.Text);

            //Rpt.SubJudul(x
            //    , "Periode : " + periode.SelectedItem.Text
            //    );

			Rpt.SubJudul(x
				, "Jenis : " + Rpt.inSql(jenis).Replace("'","")
				);

			Rpt.SubJudul(x
				, "Lokasi : " + lokasi.SelectedItem.Text
				);
			
			Rpt.Header(rpt, x);
		}

		private void Fill()
		{
			string Status = "";
			if (statusS.Checked) Status = " AND Status = 'P'";

            //string Periode = "";
            //if(periode.SelectedIndex!=0)
            //{
            //    string[] z = periode.SelectedValue.Split(',');
            //    Periode = " AND YEAR(TglInput) = " + z[0]
            //        + " AND MONTH(TglInput) = " + z[1];
            //}

			string Lokasi = "";
			if(lokasi.SelectedIndex != 0)
			{
				Lokasi = " AND MS_UNIT.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
			}

			decimal t1 = 0;

				string strSql = "SELECT "
					+ "	NoStock"
					+ ",Jenis"
					+ ",Lokasi"
					+ ",NoUnit"
					+ ",Luas"
					+ ",PriceList"
					+ ",PriceListMin"
					+ ",TglInput"
					+ ",Status"
					+ " FROM MS_UNIT"
					+ " WHERE 1=1"
                    + " AND Jenis IN (" + Rpt.inSql(jenis) + ")"
					+ Status
					+ Lokasi
					//+ Periode
                    + " AND NoStock NOT IN (SELECT NoStock FROM MS_KONTRAK)"
					+ " ORDER BY NoStock";
             //   Response.Write(strSql);	
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;
				//r.Attributes["ondblclick"] = "popEditUnit('"+rs.Rows[i]["NoStock"]+"')";

				DateTime p = Convert.ToDateTime(rs.Rows[i]["TglInput"]);

				c = new TableCell();
				c.Text = (i+1).ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Db.SingleString("SELECT NoKontrak FROM MS_KONTRAK WHERE NoStock = '" + rs.Rows[i]["NoStock"].ToString() + "'");
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglInput"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Lokasi"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);
				
				c = new TableCell();
				c.Text = rs.Rows[i]["Jenis"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

                DataTable dtNUP = Db.Rs("SELECT B.*, C.Nama AS NamaCS, D.Nama AS NamaAG FROM MS_PRIORITY A"
                        + " INNER JOIN MS_NUP B ON A.NoNUP = B.NoNUP"
                        + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                        + " INNER JOIN MS_AGENT D ON B.NoAgent = D.NoAgent"
                        + " WHERE A.NoStock = '" + rs.Rows[i]["NoStock"].ToString() + "'"
                    );

                for (int j = 0; j < dtNUP.Rows.Count; j++)
                {
                    if (!Response.IsClientConnected) break;

                    c = new TableCell();
                    c.Text = dtNUP.Rows[j]["NoNUP"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    decimal bayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNUP='" + dtNUP.Rows[j]["NoNUP"].ToString() + "'");
                    c.Text = Cf.Num(bayar);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = dtNUP.Rows[j]["NamaCS"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = dtNUP.Rows[j]["NamaAG"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    t1 = t1 + bayar;
                }
				rpt.Rows.Add(r);                
				
				if(i==rs.Rows.Count-1)
					SubTotal("TOTAL PEMBAYARAN", t1);
			}
		}

		private void SubTotal(string txt, decimal t1)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = txt;
			c.ColumnSpan = 8;
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.ColumnSpan = 4;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		protected void jenisCheck_CheckedChanged(object sender, System.EventArgs e)
		{
			for(int i = 0; i < jenis.Items.Count; i++)
			{
				jenis.Items[i].Selected = jenisCheck.Checked;
			}

			Js.Focus(this, jenisCheck);
			jenisc.Text = "";
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
