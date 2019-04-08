using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace ISC064.MARKETINGJUAL
{
    public partial class PrintJadwalTagihanReservasi : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			SetTemplate();
			
			if(!Page.IsPostBack)
			{
				Fill();
			}

			if(reprint.Visible)
				RegisterOnSubmitStatement(
					"md5Script"
					, "document.getElementById('pass').value=hex_md5(document.getElementById('pass').value);"
					);
		}

		private void SetTemplate()
		{
            PrintJadwalTagihanReservasiTemplate uc = (PrintJadwalTagihanReservasiTemplate)Page.LoadControl("PrintJadwalTagihanReservasiTemplate.ascx");
            uc.NoReservasi = NoReservasi;
			list.Controls.Add(uc);
		}

		private void Fill()
		{
            cancel.Attributes["onclick"] = "location.href='ReservasiEdit.aspx?NoReservasi=" + NoReservasi + "'";
            cancel2.Attributes["onclick"] = "location.href='ReservasEdit.aspx?NoReservasi=" + NoReservasi + "'";

            string strSql = "SELECT PrintJadwalTagihan FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "'";
			DataTable rs = Db.Rs(strSql);
			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/NoPrint.html");
			else
			{
                count.Text = rs.Rows[0]["PrintJadwalTagihan"].ToString();
                if ((int)rs.Rows[0]["PrintJadwalTagihan"] == 0)
                {
                    Tampil(); //langsung tampil
                    ConvertPdf();
                    Response.Redirect(Param.PathLinkFilePDFMarketingJual + NoReservasi + "_JadwalTagihanReservasi.pdf");
                }
				else
				{
					//mekanisme reprint
					list.Visible = false;
					reprint.Visible = true;
					Js.Focus(this,username);

					if(Session["SalahPass"]==null)
						Session["SalahPass"] = "0"; //Hitung password salah berapa kali
					else
					{
						if(Session["SalahPass"].ToString()!="0")
							salah.Text = Session["SalahPass"] + "x salah";
					}
                }
			}
		}

		private void Tampil()
		{
			list.Visible = true;
			reprint.Visible = false;
			Js.AutoPrint(this);

			//increment
            Db.Execute("UPDATE MS_RESERVASI SET PrintJadwalTagihan = PrintJadwalTagihan + 1 WHERE NoReservasi = '" + NoReservasi + "'");

			//Logfile
			DataTable rs = Db.Rs("SELECT "
                + " MS_RESERVASI.NoReservasi AS [No. Reservasi]"
				+ ",MS_RESERVASI.NoUnit AS [Unit]"
				+ ",MS_CUSTOMER.Nama AS [Customer]"
				+ ",PrintJadwalTagihan AS [Print Counter]"
				+ " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER"
				+ " ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_RESERVASI.NoReservasi = '" + NoReservasi + "'");

			Db.Execute("EXEC spLogReservasi"
				+ " 'P-JadwalTagihan'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Cf.LogCapture(rs) + "'"
				+ ",'" + NoReservasi.ToString() + "'"
				);

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_RESERVASI_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = (SELECT NoStock FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "')");
            Db.Execute("UPDATE MS_RESERVASI_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

        }

        private void ConvertPdf()
        {
            Process p = new System.Diagnostics.Process();

            string myHtml = "http://localhost:8034/marketingjual/PrintJadwalTagihanReservasi1.aspx?NoReservasi=" + NoReservasi;

            string save = Param.PathFilePDFMarketingJual + NoReservasi + "_JadwalTagihanReservasi.pdf";
            string link = Param.PathLinkFilePDFMarketingJual + NoReservasi + ".pdf";

            p.StartInfo.Arguments = "--orientation portrait --page-width 8.5in --page-height 11in --margin-left 1cm --margin-right 1cm --margin-top 1.25cm --margin-bottom 1 " + myHtml + " " + save;
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();
            p.WaitForExit(60000);
        }

        protected void btn_Click(object sender, System.EventArgs e)
		{
			string pid = "RP:"+Request.PhysicalPath;
			string Username = Cf.Str(username.Text);
			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM " + Mi.DbPrefix + "SECURITY..USERNAME "
				+ " WHERE UserID = '" + Username + "'"
				+ " AND Pass = '" + pass.Text + "'"
				+ " AND Status = 'A'"
				+ " AND "
				+ " (" //cek sec. level untuk reprint
				+ "	SecLevel IN "
				+ "		(SELECT Kode FROM " + Mi.DbPrefix + "SECURITY..PAGESEC WHERE Halaman = '"+pid+"')"
				+ "	OR UserID IN "
				+ "		(SELECT UserID FROM " + Mi.DbPrefix + "SECURITY..PAGEDENY WHERE Halaman = '"+pid+"' AND Sifat=0)"
				+ " )"
				);

			if(c!=0)
				Valid(Username);
			else
				Invalid();
		}

		private void Valid(string Username)
		{
			Session["SalahPass"] = null;

			//Logfile otorisasi
			DataTable rs = Db.Rs("SELECT "
                + " MS_RESERVASI.NoReservasi AS [No. Reservasi]"
                + ",MS_RESERVASI.NoUnit AS [Unit]"
				+ ",MS_CUSTOMER.Nama AS [Customer]"
                + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER"
                + " ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_RESERVASI.NoReservasi = '" + NoReservasi + "'");

			Db.Execute("EXEC spLogReservasi"
                + " 'R-JadwalTagihan'"
				+ ",'" + Username + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Cf.LogCapture(rs) + "'"
				+ ",'" + NoReservasi.ToString() + "'"
				);

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_RESERVASI_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = (SELECT NoStock FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "')");
            Db.Execute("UPDATE MS_RESERVASI_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Tampil();
            string file = Param.PathFilePDFMarketingJual + NoReservasi + "_JadwalTagihanReservasi.pdf";
            bool exist = System.IO.File.Exists(file);
            if (exist)
            {
                System.IO.File.Delete(file);
            }
            ConvertPdf();
            Response.Redirect(Param.PathLinkFilePDFMarketingJual + NoReservasi + "_JadwalTagihanReservasi.pdf");
        }

		private void Invalid()
		{
			//3x salah password akan mengakibatkan sign-out otomatis;
			int x = Convert.ToInt32(Session["SalahPass"]) + 1;
			salah.Text = x.ToString() + "x salah";
			Session["SalahPass"] = x;
			
			if(x>=3)
				Response.Redirect("SignOut.aspx?pass=1");

			Js.Alert(
				this
				, "Otorisasi Gagal "+x+"x.\\n"
				+ "Username akan Sign-Out otomatis apabila salah 3x."
				, "document.getElementById('pass').focus();"
				);
		}

		private string NoReservasi
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoReservasi"]);
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
