using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakAJB : System.Web.UI.Page
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

					cancel.Attributes["onclick"] = "location.href='ReminderAJB.aspx'";
				}
				else
				{
					Js.Focus(this,nokontrak);
					frm.Visible = false;
				}
			}

			FeedBack();
			if(frm.Visible) Js.Confirm(this,"Lanjutkan proses pencatatan aktivitas AJB?");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "<a href=\"javascript:popEditKontrak('"+Request.QueryString["done"]+"')\">"
						+ "AJB Berhasil..."
						+ "</a>";
			}
		}

		private bool valid()
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");// AND AJB <> 'D'");

			if(c==0)
				x = false;

			if(!x)
				Js.Alert(
					this
					, "Kontrak Tidak Valid.\\n\\n"
					+ "Kemungkinan Sebab :\\n"
					+ "1. Kontrak tersebut tidak terdaftar.\\n"
					+ "2. Kontrak tersebut sudah dibatalkan.\\n"
					+ "3. Prosedur AJB sudah dijalankan.\\n"
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
				Js.Confirm(this,"Lanjutkan proses pencatatan aktivitas AJB?");
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
				Js.Confirm(this,"Lanjutkan proses pencatatan aktivitas AJB?");
			}
		}

		private void Fill()
		{
			nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

			Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

			string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				tglajb.Text = Cf.Day(DateTime.Today);

				persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);
				if((decimal)rs.Rows[0]["PersenLunas"] < 100)
					lunasinfo.Text = "PELUNASAN BELUM MENCAPAI 100%";
			}
		}

		private bool datavalid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(tglajb))
			{
				x = false;
				if(s=="") s = tglajb.ID;
				tglajbc.Text = "Tanggal";
			}
			else
				tglajbc.Text = "";

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
					+ "2. Biaya Administrasi harus berupa angka."
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool isUnique(string kodebaru)
		{
			bool x = true;

			int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoAJB = '"+kodebaru+"'");
			if(c!=0)
				x = false;

			return x;
		}

		private string AutoID()
		{
			string x = "";
			int c = Db.SingleInteger("SELECT COUNT(NoAJB) FROM MS_KONTRAK "
				+ " WHERE AJB = 'D'"
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

        private bool isUnique2(string kodebaru)
        {
            bool x = true;

            int d = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoFPS = '" + kodebaru + "'");
            if (d != 0)
                x = false;

            return x;
        }

        private string AutoIDFPS()
        {
            string x = "";
            int d = Db.SingleInteger("SELECT COUNT(NoFPS) FROM MS_KONTRAK "
                + " WHERE AJB = 'D'"
                );
            d = d - 1;

            string status0 = "010";
            string tahun = DateTime.Today.Year.ToString().Substring(2, 2);

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                d++;
                x = d.ToString().PadLeft(8, '0');

                if (isUnique2(x)) hasfound = true;
            }
            //default normal=> 0 0 0. 0 0 0 – 0 0 .0 0 0 0 0 0 0 0
            x = status0 + ".000" + "-" + tahun + "." + x;

            return x;
        }

       

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(datavalid())
			{
				DateTime TglAJB = Convert.ToDateTime(tglajb.Text);
				
				string NoAJB = Db.SingleString("SELECT NoAJB FROM MS_KONTRAK WHERE NoKontrak = '"+ NoKontrak +"'");
				if(NoAJB == "")
					NoAJB = AutoID();

				Db.Execute("EXEC spKontrakAJB "
					+ " '" + NoKontrak + "'"
					+ ",'" + NoAJB + "'"
					+ ",'" + TglAJB + "'"
					);

                Db.Execute("UPDATE MS_KONTRAK SET StatusAJB = 'SELESAI' WHERE NoKontrak = '"+NoKontrak+"' ");

				DataTable rs = Db.Rs("SELECT"
					+ " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
					+ ",MS_KONTRAK.NoUnit AS [Unit]"
					+ ",MS_CUSTOMER.Nama AS [Customer]"
					+ ",NoAJB AS [No. AJB]"
					+ ",CONVERT(varchar, TglAJB, 106) AS [Tanggal AJB]"
					+ ",PersenLunas AS [Prosentase Pelunasan]"
					+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
					+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
					+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

				decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
				if(NilaiBiaya!=0)
				{
					Db.Execute("EXEC spTagihanDaftar "
						+ " '" + NoKontrak + "'"
						+ ",'BIAYA ADM. AJB'"
						+ ",'" + TglAJB + "'"
						+ ", " + NilaiBiaya
						+ ",'ADM'"
						);
				}
                //Kode dan Nomor Seri FPS
                string NoFPS = Db.SingleString("SELECT NoFPS FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                if (NoFPS == "")
                    NoFPS = AutoIDFPS();

                Db.Execute("UPDATE MS_KONTRAK SET NoFPS= '" + NoFPS + "' WHERE NoKontrak = '" + NoKontrak + "' ");

				string ket = Cf.LogCapture(rs)
					+ "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
					;

				Db.Execute("EXEC spLogKontrak "
					+ " 'AJB'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + ket + "'"
					+ ",'" + NoKontrak + "'"
					);

				if(dariReminder.Checked)
					Response.Redirect("ReminderAJB.aspx?done="+NoKontrak);
				else
					Response.Redirect("KontrakAJB.aspx?done="+NoKontrak);
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
