using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakPPJB : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				backbtn.Visible = false;
				nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&ppjb=1');";

				if(Request.QueryString["NoKontrak"]!=null)
				{
					dariReminder.Checked = true;
					nokontrak.Text = Request.QueryString["NoKontrak"];
					LoadKontrak();

					cancel.Attributes["onclick"] = "location.href='ReminderPPJB.aspx'";
				}
				else
				{
					Js.Focus(this,nokontrak);
					frm.Visible = false;
				}
			}

			FeedBack();
			if(frm.Visible) Js.Confirm(this,"Lanjutkan proses pencatatan aktivitas PPJB?");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "<a href=\"javascript:popEditKontrak('"+Request.QueryString["done"]+"')\">"
						+ "PPJB Berhasil..."
						+ "</a>";
			}
		}

		private bool valid()
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");// AND PPJB <> 'D'");

			if(c==0)
				x = false;

			if(!x)
				Js.Alert(
					this
					, "Kontrak Tidak Valid.\\n\\n"
					+ "Kemungkinan Sebab :\\n"
					+ "1. Kontrak tersebut tidak terdaftar.\\n"
					+ "2. Kontrak tersebut sudah dibatalkan.\\n"
					+ "3. Prosedur PPJB sudah dijalankan.\\n"
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
				Js.Confirm(this,"Lanjutkan proses pencatatan aktivitas PPJB?");
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
				Js.Confirm(this,"Lanjutkan proses pencatatan aktivitas PPJB?");
			}
		}

		private void Fill()
		{
			nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

			nilaikpa.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			nilaikpa.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			nilaikpa.Attributes["onblur"] = "CalcBlur(this);";

			Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

			string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				tglppjb.Text = Cf.Day(DateTime.Today);

				persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);
				if((decimal)rs.Rows[0]["PersenLunas"] < 30)
					lunasinfo.Text = "PELUNASAN BELUM MENCAPAI 30%";
			}
		}

		private bool datavalid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(tglppjb))
			{
				x = false;
				if(s=="") s = tglppjb.ID;
				tglppjbc.Text = "Tanggal";
			}
			else
				tglppjbc.Text = "";

			if(!Cf.isMoney(nilaibiaya))
			{
				x = false;
				if(s=="") s = nilaibiaya.ID;
				nilaibiayac.Text = "Angka";
			}
			else
				nilaibiayac.Text = "";

			if(!Cf.isMoney(nilaikpa))
			{
				x = false;
				if(s=="") s = nilaikpa.ID;
				nilaikpac.Text = "Angka";
			}
			else
				nilaikpac.Text = "";

            //if (lunasinfo.Text == "PELUNASAN BELUM MENCAPAI 30%")
            //{
            //    x = false;
            //}

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					+ "2. Biaya Administrasi harus berupa angka.\\n"
                    + "3. Peluansan Belum Mencapai 30%."
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool isUnique(string kodebaru)
		{
			bool x = true;

			int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoPPJB = '"+kodebaru+"'");
			if(c!=0)
				x = false;

			return x;
		}

		private string AutoID(DateTime TglPPJB)
		{
			string x = "";
			int c = Db.SingleInteger("SELECT COUNT(NoPPJB) FROM MS_KONTRAK "
				+ " WHERE PPJB = 'D'"
				);
            int d = Db.SingleInteger("SELECT COUNT(NoPPJB) FROM MS_KONTRAK "
				+ " WHERE PPJB = 'D' AND MONTH(TglPPJB)='" + TglPPJB.Month + "' AND YEAR(TglPPJB)='" + TglPPJB.Year + "'"
				);
			
			bool hasfound = false;
			while(!hasfound)
			{
				if(!Response.IsClientConnected) break;

				c++;
                d++;
                x = c.ToString().PadLeft(4, '0') + "/DU/" + Act.UserID + "/AMT/PPJB." + d.ToString() + "/" + Cf.Roman(TglPPJB.Month) + "/" + TglPPJB.Year.ToString();
				
				if(isUnique(x)) hasfound = true;
			}

			return x;
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(datavalid())
			{
				DateTime TglPPJB = Convert.ToDateTime(tglppjb.Text);

				string NoPPJB = Db.SingleString("SELECT NoPPJB FROM MS_KONTRAK WHERE NoKontrak = '"+ NoKontrak +"'");
				if(NoPPJB == "")
                    NoPPJB = AutoID(TglPPJB);

				Db.Execute("EXEC spKontrakPPJB "
					+ " '" + NoKontrak + "'"
					+ ",'" + NoPPJB + "'"
					+ ",'" + TglPPJB + "'"
					);

				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET NilaiRealisasiKPR = " + Convert.ToDecimal(nilaikpa.Text)
					+ ", RekeningCairKPR = '" + rekcair.SelectedValue + "'"
                    + ", NoPPJBm = '"+ noppjbm.Text +"'"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);
				
				DataTable rs = Db.Rs("SELECT"
					+ " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
					+ ",MS_KONTRAK.NoUnit AS [Unit]"
					+ ",MS_CUSTOMER.Nama AS [Customer]"
					+ ",NoPPJB AS [No. PPJB]"
					+ ",CONVERT(varchar, TglPPJB, 106) AS [Tanggal PPJB]"
					+ ",PersenLunas AS [Prosentase Pelunasan]"
					+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
					+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
					+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

				decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
				if(NilaiBiaya!=0)
				{
					Db.Execute("EXEC spTagihanDaftar "
						+ " '" + NoKontrak + "'"
						+ ",'BIAYA ADM. PPJB'"
						+ ",'" + TglPPJB + "'"
						+ ", " + NilaiBiaya
						+ ",'ADM'"
						);
				}

				string ket = Cf.LogCapture(rs)
					+ "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
					;

				Db.Execute("EXEC spLogKontrak "
					+ " 'PPJB'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + ket + "'"
					+ ",'" + NoKontrak + "'"
					);

				if(dariReminder.Checked)
					Response.Redirect("ReminderPPJB.aspx?done="+NoKontrak);
				else
					Response.Redirect("KontrakPPJB.aspx?done="+NoKontrak);
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
