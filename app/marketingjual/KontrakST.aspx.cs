using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakST : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				backbtn.Visible = false;
				nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&st=1');";

				if(Request.QueryString["NoKontrak"]!=null)
				{
					dariReminder.Checked = true;
					nokontrak.Text = Request.QueryString["NoKontrak"];
					LoadKontrak();

					cancel.Attributes["onclick"] = "location.href='ReminderST.aspx'";
				}
				else
				{
					Js.Focus(this,nokontrak);
					frm.Visible = false;
				}
			}

			FeedBack();
			if(frm.Visible) Js.Confirm(this,"Lanjutkan proses serah terima unit properti?");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "<a href=\"javascript:popEditKontrak('"+Request.QueryString["done"]+"')\">"
						+ "Serah Terima Berhasil..."
						+ "</a>";
			}
		}

		private bool valid()
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");// AND ST <> 'D'");

			if(c==0)
				x = false;

			if(!x)
				Js.Alert(
					this
					, "Kontrak Tidak Valid.\\n\\n"
					+ "Kemungkinan Sebab :\\n"
					+ "1. Kontrak tersebut tidak terdaftar.\\n"
					+ "2. Kontrak tersebut sudah dibatalkan.\\n"
					+ "3. Prosedur serah terima sudah dijalankan.\\n"
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

				InitForm();
				Fill();

				Js.Focus(this, luas);
				Js.Confirm(this,"Lanjutkan proses serah terima unit properti?");
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

				InitForm();
				Fill();

				Js.Focus(this, luas);
				Js.Confirm(this,"Lanjutkan proses serah terima unit properti?");
			}
		}

		private void InitForm()
		{
			//kalkulator
			luas.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			luas.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			luas.Attributes["onblur"] = "CalcBlur(this);";

			nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";
		}

		private void Fill()
		{
			Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

			string strSql = "SELECT *"
				+ " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				luas.Text = Cf.Num(rs.Rows[0]["Luas"]);
				targetst.Text = Cf.Day(rs.Rows[0]["TargetST"]);
				
				if(rs.Rows[0]["TglST"] is DBNull)
				{
					tglst.Text = Cf.Day(DateTime.Today);
				}
				else
				{
					tglst.Text = Cf.Day(rs.Rows[0]["TglST"]);
				}

				persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);
				if((decimal)rs.Rows[0]["PersenLunas"] < 100)
					lunasinfo.Text = "PELUNASAN BELUM MENCAPAI 100%";
			}
		}

		private bool datavalid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(tglst))
			{
				x = false;
				if(s=="") s = tglst.ID;
				tglstc.Text = "Tanggal";
			}
			else
				tglstc.Text = "";

			if(!Cf.isMoney(luas))
			{
				x = false;
				if(s=="") s = luas.ID;
				luasc.Text = "Angka";
			}
			else
				luasc.Text = "";

			if(!Cf.isMoney(nilaibiaya))
			{
				x = false;
				if(s=="") s = nilaibiaya.ID;
				nilaibiayac.Text = "Angka";
			}
			else
				nilaibiayac.Text = "";

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					+ "2. Luas harus berupa angka.\\n"
					+ "3. Biaya Administrasi harus berupa angka."
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool isUnique(string kodebaru)
		{
			bool x = true;

			int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoST = '"+kodebaru+"'");
			if(c!=0)
				x = false;

			return x;
		}

		private string AutoID()
		{
			string x = "";
			int c = Db.SingleInteger("SELECT COUNT(NoST) FROM MS_KONTRAK "
				+ " WHERE ST = 'D'"
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
			if(datavalid())
			{
				DateTime TglST = Convert.ToDateTime(tglst.Text);
				decimal Luas = Convert.ToDecimal(luas.Text);
				
				
				string NoST = Db.SingleString("SELECT NoST FROM MS_KONTRAK WHERE NoKontrak = '"+ NoKontrak +"'");
				if(NoST == "")
					NoST = AutoID();

				DataTable rsBef = Db.Rs("SELECT"
					+ " Luas"
					+ ",Gross AS [Nilai Gross]"
					+ ",DiskonRupiah AS [Diskon dalam Rupiah]"
					+ ",DiskonPersen AS [Diskon dalam Persen]"
					+ " FROM MS_KONTRAK"
					+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

				Db.Execute("EXEC spKontrakST "
					+ " '" + NoKontrak + "'"
					+ ",'" + NoST + "'"
					+ ",'" + TglST + "'"
					+ ", " + Luas
					);

				DataTable rsAft = Db.Rs("SELECT"
					+ " Luas"
					+ ",Gross AS [Nilai Gross]"
					+ ",DiskonRupiah AS [Diskon dalam Rupiah]"
					+ ",DiskonPersen AS [Diskon dalam Persen]"
					+ " FROM MS_KONTRAK"
					+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

				DataTable rs = Db.Rs("SELECT"
					+ " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
					+ ",MS_KONTRAK.NoUnit AS [Unit]"
					+ ",MS_CUSTOMER.Nama AS [Customer]"
					+ ",CONVERT(varchar, TargetST, 106) AS [Jadwal Serah Terima]"
					+ ",NoST AS [No. BAST]"
					+ ",CONVERT(varchar, TglST, 106) AS [Tanggal BAST]"
					+ ",PersenLunas AS [Prosentase Pelunasan]"
					+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
					+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
					+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

				decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
				if(NilaiBiaya!=0)
				{
					Db.Execute("EXEC spTagihanDaftar "
						+ " '" + NoKontrak + "'"
						+ ",'BIAYA ADM. SERAH TERIMA'"
						+ ",'" + TglST + "'"
						+ ", " + NilaiBiaya
						+ ",'ADM'"
						);
				}

				string ket = Cf.LogCapture(rs)
					+ Cf.LogCompare(rsBef,rsAft)
					+ "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
					;

				Db.Execute("EXEC spLogKontrak "
					+ " 'ST'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + ket + "'"
					+ ",'" + NoKontrak + "'"
					);

				if(dariReminder.Checked)
					Response.Redirect("ReminderST.aspx?done="+NoKontrak);
				else
					Response.Redirect("KontrakST.aspx?done="+NoKontrak);
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
