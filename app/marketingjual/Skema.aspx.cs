using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class Skema : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				noskema.Visible = false;
				hasil.Visible = false;

				InitForm(); //javascript
				Bind(); //ddl cara bayar
				Fill(); //querystring

				if(pilih.Visible)
				{
					SetHarga(); //perhitungan price list
					Js.Focus(this, hitung);
				}
			}
		}

		private void InitForm()
		{
			carabayar.Attributes["onchange"] = "load()";

			gross.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempx=this.value;";
			gross.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			gross.Attributes["onblur"] = "CalcBlur(this);"
				+ "if(this.value!=tempx){load()}";

			nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempx=this.value";
			nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);"
				+ "if(this.value!=tempx){document.getElementById('disc').value=''}";
			nilai.Attributes["onblur"] = "CalcBlur(this);";

			disc.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempdisc=this.value;"
				+ "nohitung();";
			disc.Attributes["onblur"] = "if(this.value!=tempdisc){"
				+ "recaldisc( "
				+ "		document.getElementById('gross')"
				+ "		,document.getElementById('disc')"
				+ "		,document.getElementById('nilai')"
				+ ");"
				+ "}"
				+ "okhitung();"
				;
		}

		private void Bind()
		{
			DataTable rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' ORDER BY Nama");
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["Nomor"].ToString();
				string t = rs.Rows[i]["Nama"] + " ("+v.PadLeft(3,'0')+")";
				carabayar.Items.Add(new ListItem(t,v));
			}

			if(rs.Rows.Count==0)
			{
				pilih.Visible = false;
				noskema.Visible = true;
			}
			else
			{
				if(Request.QueryString["Nomor"]!=null)
				{
					try
					{
						carabayar.SelectedValue = Request.QueryString["Nomor"];
					}
					catch
					{
						carabayar.SelectedIndex = 0;
					}
				}
				else
					carabayar.SelectedIndex = 0;
			}
		}

		private void Fill()
		{
			try{
				DateTime tgl = Convert.ToDateTime(Request.QueryString["tgl"]);
				tglkontrak.Text = Cf.Day(tgl);
			}
			catch {
				tglkontrak.Text = Cf.Day(DateTime.Today);
			}

			try {
				decimal z = Convert.ToDecimal(Request.QueryString["pl"]);
				gross.Text = Cf.Num(z);
			}
			catch {
				gross.Text = "1,000,000,000";
			}
		}

		private void SetHarga()
		{
			decimal Gross = Convert.ToDecimal(gross.Text);

			string RumusDiskon = Db.SingleString(
				"SELECT Diskon FROM REF_SKEMA WHERE Nomor = " + carabayar.SelectedValue);
//			disc.Text = RumusDiskon;
			string[] x = RumusDiskon.Split('+');

			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			for(int i = 0; i < x.Length; i++)
			{
				if(x[i] != "")
				{
					decimal y = Convert.ToDecimal(x[i]) * (decimal)-1;
					if(i < (x.Length - 1))
						sb.Append(y.ToString() + "+");
					else
						sb.Append(y.ToString());
				}
			}

			disc.Text = sb.ToString();

			decimal netto = Func.SetelahDiskon(RumusDiskon,Gross);
			nilai.Text = Cf.Num(netto);
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(tglkontrak))
			{
				x = false;
				if(s=="") s = tglkontrak.ID;
				tglkontrakc.Text = "Tanggal";
			}
			else
				tglkontrakc.Text = "";

			if(!Cf.isMoney(nilai))
			{
				x = false;
				if(s=="") s = nilai.ID;
				nilaic.Text = "Angka";
			}
			else
				nilaic.Text = "";

			if(!x && s!="")
			{
				RegisterStartupScript("err"
					,"<script language='javascript'>document.getElementById('"+s+"').select()</script>");
			}

			return x;
		}

		protected void hitung_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				pilih.Visible = false;
				hasil.Visible = true;

				Jadwal();
			}
		}

		private void Jadwal()
		{
			DateTime tgl = Convert.ToDateTime(tglkontrak.Text);
			decimal netto = Convert.ToDecimal(nilai.Text);

			string[,] x = Func.Breakdown(
				Convert.ToInt32(carabayar.SelectedValue), netto, tgl);

			decimal t = 0;
			for(int i=0;i<=x.GetUpperBound(0);i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = x[i,0] + ".";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = x[i,2];
				r.Cells.Add(c);

				//jadwal
				c = new TableCell();
				c.Text = x[i,3];
				r.Cells.Add(c);

				//nominal
				c = new TableCell();
				c.Text = Cf.Num(x[i,4]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				t = t + Convert.ToDecimal(x[i,4]);
				
				Rpt.Border(r);
				rpt.Rows.Add(r);

				if(i==x.GetUpperBound(0))
				{
					r = new TableRow();
					
					c = new TableCell();
					c.Text = "Grand Total";
					c.ColumnSpan = 2;
					c.Font.Bold = true;
					c.Font.Size = 10;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Cf.Num(t);
					c.ColumnSpan = 2;
					c.HorizontalAlign = HorizontalAlign.Right;
					c.Font.Bold = true;
					c.Font.Size = 10;
					r.Cells.Add(c);

					rpt.Rows.Add(r);
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
