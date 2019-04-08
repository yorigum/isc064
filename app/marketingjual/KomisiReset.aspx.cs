using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KomisiReset : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Js.Focus(this,nokontrak);
				nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a');";
				frm.Visible = false;
			}

			FeedBack();
			if(frm.Visible) Js.Confirm(this, "Lanjutkan proses reset jadwal komisi?");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "<a href=\"javascript:popJadwalKomisi('"+Request.QueryString["done"]+"')\">"
						+ "Reset Komisi Berhasil..."
						+ "</a>";
			}
		}

		private bool valid()
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");

			if(c==0)
				x = false;

			if(!x)
				Js.Alert(
					this
					, "Kontrak Tidak Valid.\\n\\n"
					+ "Kemungkinan Sebab :\\n"
					+ "1. Kontrak tersebut tidak terdaftar.\\n"
					+ "2. Kontrak tersebut sudah dibatalkan.\\n"
					, "document.getElementById('nokontrak').focus();"
					+ "document.getElementById('nokontrak').select();"
					);

			return x;
		}

		protected void next_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				pilih.Visible = false;
				frm.Visible = true;

				InitForm();
				Fill();

				Js.Confirm(this,"Lanjutkan proses reset jadwal komisi?");
			}
		}

		private void InitForm()
		{
			DataTable rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKOM WHERE Status = 'A' ORDER BY Nama");
			skema.Items.Add(new ListItem("*** RESET KOMISI","0"));

			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["Nomor"].ToString();
				string t = rs.Rows[i]["Nama"] + " ("+v.PadLeft(3,'0')+")";
				skema.Items.Add(new ListItem(t,v));
			}
			skema.SelectedIndex = 0;
		}

		private void Fill()
		{
			Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

			string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				principal.Text = Db.SingleString(
					"SELECT Principal FROM MS_AGENT WHERE NoAgent = " + rs.Rows[0]["NoAgent"]);
				netto.Text = Cf.Num(rs.Rows[0]["NilaiKontrak"]);

				skemal.Text = rs.Rows[0]["SkemaKomisi"].ToString();
			}
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			DataTable rsHeaderBef = Db.Rs("SELECT "
				+ " SkemaKomisi AS [Skema Komisi]"
				+ " FROM MS_KONTRAK"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				);

			DataTable rsBef = Db.Rs("SELECT "
				+ "CONVERT(VARCHAR,NoUrut) + '.  ' + NamaKomisi + ' ('+Tipe+')   CAIR:' + CONVERT(VARCHAR,TermCair,1) + '% (' + Jadwal + ')  ' + CONVERT(VARCHAR,NilaiKomisi,1) "
				+ "FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

			SaveKomisi();

			DataTable rsHeaderAft = Db.Rs("SELECT "
				+ " SkemaKomisi AS [Skema Komisi]"
				+ " FROM MS_KONTRAK"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				);

			DataTable rsAft = Db.Rs("SELECT "
				+ "CONVERT(VARCHAR,NoUrut) + '.  ' + NamaKomisi + ' ('+Tipe+')   CAIR:' + CONVERT(VARCHAR,TermCair,1) + '% (' + Jadwal + ')  ' + CONVERT(VARCHAR,NilaiKomisi,1) "
				+ "FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

			DataTable rsDetail = Db.Rs("SELECT"
				+ " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
				+ ",MS_KONTRAK.NoUnit AS [Unit]"
				+ ",MS_CUSTOMER.Nama AS [Customer]"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
				+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

			string Ket = Cf.LogCapture(rsDetail)
				+ Cf.LogCompare(rsHeaderBef,rsHeaderAft)
				+ Cf.LogList(rsBef, rsAft, "JADWAL KOMISI");
				
			Db.Execute("EXEC spLogKontrak"
				+ " 'RK'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Ket + "'"
				+ ",'" + NoKontrak + "'"
				);
				
			Response.Redirect("KomisiReset.aspx?done="+NoKontrak);
		}

		private void SaveKomisi()
		{
			//reset komisi
			Db.Execute("EXEC spKomisiReset '"+NoKontrak+"'");

			string SkemaKomisi = Cf.Str(skema.SelectedItem.Text);
			Db.Execute("UPDATE MS_KONTRAK SET SkemaKomisi = '"+SkemaKomisi+"'"
				+ " WHERE NoKontrak = '"+NoKontrak+"'");
			
			decimal Netto = Db.SingleDecimal(
				"SELECT NilaiKontrak FROM MS_KONTRAK WHERE NoKontrak = '"+NoKontrak+"'"
				);
			int Skema = Convert.ToInt32(skema.SelectedValue);
			
			//skema 0 = customize
			if(Skema!=0)
			{
				string[,] x = Func.BreakdownKomisi(Skema,Netto);
				for(int i=0;i<=x.GetUpperBound(0);i++)
				{
					if(!Response.IsClientConnected) break;

					Db.Execute("EXEC spKomisiDaftar"
						+ " '" + NoKontrak + "'"
						+ ",'" + x[i,5] + "'"
						+ ",'" + x[i,1] + "'"
						+ ", " + Convert.ToDecimal(x[i,2])
						+ ", " + Convert.ToDecimal(x[i,3])
						+ ",'" + x[i,4] + "'"
						);
				}
			}
		}

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(nokontrak.Text);
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
