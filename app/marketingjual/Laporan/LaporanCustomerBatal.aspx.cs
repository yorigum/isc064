using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanCustomerBatal : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ListBox lokasi;
		protected System.Web.UI.WebControls.ListBox agent;
		protected System.Web.UI.WebControls.RadioButton statusS;
		protected System.Web.UI.WebControls.RadioButton statusA;
		protected System.Web.UI.WebControls.RadioButton statusB;
		protected System.Web.UI.WebControls.CheckBox jenisCheck;
		protected System.Web.UI.WebControls.Label jenisc;
		protected System.Web.UI.WebControls.CheckBoxList jenis;
		protected System.Web.UI.WebControls.RadioButton bfS;
		protected System.Web.UI.WebControls.RadioButton bf1;
		protected System.Web.UI.WebControls.RadioButton bf2;
		
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
			dari.Text = Cf.Day(Cf.AwalBulan());
			sampai.Text = Cf.Day(Cf.AkhirBulan());
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(dari))
			{
				x = false;
				if(s=="") s = dari.ID;
				daric.Text = "Tanggal";
			}
			else
				daric.Text = "";

			if(!Cf.isTgl(sampai))
			{
				x = false;
				if(s=="") s = sampai.ID;
				sampaic.Text = "Tanggal";
			}
			else
				sampaic.Text = "";

			if(!x && s!="")
			{
				RegisterStartupScript("err"
					,"<script type='text/javascript'>document.getElementById('"+s+"').select()</script>");
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

			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			Rpt.SubJudul(x
				, "Tanggal : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
				);

            //Rpt.Header(rpt, x);
            string legend = "";
            Rpt.HeaderReport(headReport, legend, x);
		}

		private void Fill()
		{	
			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			if(Dari>Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = x;
			}

			string strSql = "SELECT *"
				+ " FROM MS_KONTRAK_LOG"
				+ " WHERE Tgl >= '" + Dari + "'"
				+ " AND Tgl <= '" + Sampai + "'"
				+ " AND Aktivitas = 'BATAL'"
				;
			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				string strUnit = "", strCustomer = "";
				string Ag = "", Reason = "";
				string[] strTemp = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");

				for(int j = 0; j < strTemp.Length; j++)
				{
					if(strTemp[j].StartsWith("Unit"))
						strUnit = strTemp[j].ToString().Replace("Unit : ", "");

					if(strTemp[j].StartsWith("Customer"))
						strCustomer = strTemp[j].ToString().Replace("Customer : ", "");

					if(strTemp[j].StartsWith("Agent"))
						Ag = strTemp[j].ToString().Replace("Agent : ", "");

					if(strTemp[j].StartsWith("Alasan"))
						Reason = strTemp[j].ToString().Replace("Alasan Pembatalan : ", "");
				}

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = (i + 1).ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Pk"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = strCustomer;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = strUnit;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Ag;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Reason;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

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
