using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KwitansiGabung : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				backbtn.Visible = false;
				nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&ajb=1');";

				if(Request.QueryString["NoKontrak"]!=null)
				{
					dariReminder.Checked = true;
					nokontrak.Text = Request.QueryString["NoKontrak"];
					LoadKontrak();

					cancel.Attributes["onclick"] = "location.href='ReminderKwitansiGabung.aspx'";
				}
				else
				{
					Js.Focus(this,nokontrak);
					frm.Visible = false;
				}
			}

			FeedBack();
			if(frm.Visible) Js.Confirm(this,"Lanjutkan proses pencatatan aktivitas Kwitansi Gabung?");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "<a href=\"javascript:popEditKontrak('"+Request.QueryString["done"]+"')\">"
						+ "Kwitansi Gabung Berhasil..."
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
					+ "3. Prosedur Kwitansi Gabung sudah dijalankan.\\n"
					, "document.getElementById('nokontrak').focus();"
					+ "document.getElementById('nokontrak').select();"
					);

			return x;
		}

		private void LoadKontrak()
		{
			if(valid())
			{
				pilih.Visible = false;
				frm.Visible = true;

				Fill();

				Js.Focus(this, save);
				Js.Confirm(this,"Lanjutkan proses pencatatan aktivitas Kwitansi Gabung?");
			}
			else
			{
				backbtn.Visible = true;
				Js.Focus(this,nokontrak);
				frm.Visible = false;
			}
		}

		protected void next_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				pilih.Visible = false;
				frm.Visible = true;

				Fill();

				Js.Focus(this, save);
				Js.Confirm(this,"Lanjutkan proses pencatatan aktivitas Kwitansi Gabung?");
			}
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
				string Skema = rs.Rows[0]["Skema"].ToString();
							
				if (Skema.StartsWith("KPR"))
				{
					string sSQL = "SELECT NoUrut FROM MS_TAGIHAN WHERE Tipe = 'DP' AND NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut DESC" ;
					DataTable rsTagihan = Db.Rs(sSQL);
					for(int i = 0; i < rsTagihan.Rows.Count; i++)
					{
						int NoUrut = 0;
						NoUrut = Convert.ToInt32(rsTagihan.Rows[0]["NoUrut"]);
				
						string sSQL1 = "SELECT NilaiPelunasan FROM MS_PELUNASAN WHERE NoTagihan = '" + NoUrut + "' AND NoKontrak = '" + NoKontrak + "'";
						DataTable rsLunas = Db.Rs(sSQL1);
						if(rsLunas.Rows.Count != 0)
						{
							decimal NilaiLunas = Convert.ToDecimal(rsLunas.Rows[0]["NilaiPelunasan"]);
							if (NilaiLunas == 0)
								lunasinfo.Text = "DP BELUM LUNAS";
							else
								save.Enabled = true;
						}
						else 
						{
							lunasinfo.Text = "DP BELUM LUNAS"; 
						}
					}
				}
				else
				{
					save.Enabled = true;
				}
				persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);
			}
		}

		private bool isUnique(string kodebaru)
		{
			bool x = true;

			int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKwitansiGabung = '"+kodebaru+"'");
			if(c!=0)
				x = false;

			return x;
		}

		private string AutoID()
		{
			string x = "";
			int c = Db.SingleInteger("SELECT COUNT(NoKwitansiGabung) FROM MS_KONTRAK "
				);
			
			bool hasfound = false;
			while(!hasfound)
			{
				if(!Response.IsClientConnected) break;

				c++;
				x = c.ToString().PadLeft(7,'0');
				
				if(isUnique(x)) hasfound = true;
			}

			return x;
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			string NoKwitansiGabung = AutoID();
			
			Db.Execute("UPDATE MS_KONTRAK SET "
				+ " NoKwitansiGabung = '" + NoKwitansiGabung + "' "
				+ " WHERE NoKontrak = '" + NoKontrak + "'");

			DataTable rs = Db.Rs("SELECT"
				+ " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
				+ ",MS_KONTRAK.NoUnit AS [Unit]"
				+ ",MS_CUSTOMER.Nama AS [Customer]"
				+ ",NoKwitansiGabung AS [No. Kwitansi]"
				+ ",PersenLunas AS [Prosentase Pelunasan]"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
				+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

			Db.Execute("EXEC spLogKontrak "
				+ " 'Kwitansi Gabungan'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Cf.LogCapture(rs) + "'"
				+ ",'" + NoKontrak + "'"
				);

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            if (dariReminder.Checked)
				Response.Redirect("ReminderKwitansiGabung.aspx?done="+NoKontrak);
			else
				Response.Redirect("KwitansiGabung.aspx?done="+NoKontrak);
			
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
