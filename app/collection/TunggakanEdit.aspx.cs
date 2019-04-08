using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	public partial class TunggakanEdit : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoTunggakan");

			if(!Act.Sec("ED:"+Request.PhysicalPath))
			{
				ok.Enabled = false;
				save.Enabled = false;
			}

			if(!Page.IsPostBack)
			{
				Fill();
			}

			FeedBack();
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Edit Berhasil...";
			}
		}

		private void Fill()
		{
            trTglSuratKuasa.Visible = false;

			btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_TUNGGAKAN_LOG&Pk="+NoTunggakan.PadLeft(7,'0')+"'";
			btnsettle.Attributes["onclick"] = "if(confirm('"
                + "Jalankan proses settlement surat peringatan nomor : " + NoTunggakan + " ?\\n"
				+ "Perhatian bahwa proses ini TIDAK bisa dibalik."
				+ "'))"
				+ "{location.href='TunggakanSettle.aspx?NoTunggakan="+NoTunggakan+"'}";			

			string strSql = "SELECT *"
				+ ",CASE a.Status "
				+ "		WHEN 'A' THEN 'AKTIF' "
				+ "		WHEN 'S' THEN 'SETTLED' "
				+ "		WHEN 'U' THEN 'UPGRADED' "
				+ " END AS Status1"
				+ " FROM MS_TUNGGAKAN a JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak WHERE NoTunggakan = " + NoTunggakan;
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				unit.Text = rs.Rows[0]["Unit"].ToString();
				customer.Text = rs.Rows[0]["Customer"].ToString();
				notelp.Text = rs.Rows[0]["NoTelp"].ToString();
				alamat1.Text = rs.Rows[0]["Alamat1"].ToString();
				alamat2.Text = rs.Rows[0]["Alamat2"].ToString();
				alamat3.Text = rs.Rows[0]["Alamat3"].ToString();
                hp.Text = Db.SingleString("SELECT b.NoHP FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b on a.NoCustomer=b.NoCustomer WHERE a.NoKontrak = '" + rs.Rows[0]["Ref"].ToString() + "'");
                manuTunggakan.Text = rs.Rows[0]["ManualTunggakan"].ToString();
				tgl.Text = Cf.Day(rs.Rows[0]["TglTunggakan"]);
				total.Text = Cf.Num(rs.Rows[0]["Total"]);

				lv.Text = rs.Rows[0]["LevelTunggakan"].ToString();
                //Label Print
                if ((int)rs.Rows[0]["LevelTunggakan"] == 1)
                    lblSP.Text = "Surat Peringatan Ke-1";
                else if ((int)rs.Rows[0]["LevelTunggakan"] == 2)
                    lblSP.Text = "Surat Peringatan Ke-2";
                else if ((int)rs.Rows[0]["LevelTunggakan"] == 3)
                    lblSP.Text = "Surat Peringatan Ke-3";
                else if ((int)rs.Rows[0]["LevelTunggakan"] == 4)
                    lblSP.Text = "Surat Somasi";


                // Tanggal Kuasa Somasi
                if (rs.Rows[0]["LevelTunggakan"].ToString() == "4")
                {
                    trTglSuratKuasa.Visible = true;
                    tglKuasa.Text = Cf.Day(rs.Rows[0]["TglKuasaSomasi"]);
                }
                else
                {
                    tglKuasa.Text = Cf.Day(DateTime.Today);
                }

				status.Text = rs.Rows[0]["Status1"].ToString();

				if(rs.Rows[0]["Status"].ToString()!="A")
					btnsettle.Disabled = true; //no-edit

                printSP.Text = " (" + rs.Rows[0]["PrintST"] + ")";
				//printST.InnerHtml = printST.InnerHtml + " ("+rs.Rows[0]["PrintST"]+")";

				FillTb();
			}
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[0]["Ref"] + "'");
            printST.HRef = "PrintST.aspx?NoTunggakan=" + NoTunggakan + "&project=" + Project;
        }

		private void FillTb()
		{
			DataTable rs = Db.Rs("SELECT * FROM MS_TUNGGAKAN_DETIL WHERE NoTunggakan = " + NoTunggakan);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c = new TableCell();
				
				c.Text = rs.Rows[i]["NamaTagihan"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglJT"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Telat"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["Nilai"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["Denda"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				detil.Rows.Add(r);
			}
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

			if(Cf.isEmpty(unit))
			{
				x = false;
				if(s=="") s = unit.ID;
				unitc.Text = "Kosong";
			}
			else
				unitc.Text = "";

			if(Cf.isEmpty(customer))
			{
				x = false;
				if(s=="") s = customer.ID;
				customerc.Text = "Kosong";
			}
			else
				customerc.Text = "";

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					+ "2. Unit Properti tidak boleh kosong.\\n"
					+ "3. Customer tidak boleh kosong.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool Save()
		{
			if(valid())
			{
                DateTime TglTunggakan = Convert.ToDateTime(tgl.Text);
				string Unit = Cf.Str(unit.Text);
				string Customer = Cf.Str(customer.Text);
				string NoTelp = Cf.Str(notelp.Text);
				string Alamat1 = Cf.Str(alamat1.Text);
				string Alamat2 = Cf.Str(alamat2.Text);
				string Alamat3 = Cf.Str(alamat3.Text);
                string NoHP = Cf.Str(hp.Text);
				DataTable rs = Db.Rs("SELECT "
					+ " CONVERT(varchar, TglTunggakan, 106) AS [Tanggal]"
					+ ",Tipe"
					+ ",Ref AS [Ref.]"
					+ ",Total"
					+ ",LevelTunggakan AS [Level]"
					+ " FROM MS_TUNGGAKAN"
					+ " WHERE NoTunggakan = " + NoTunggakan
					);

				DataTable rsBef = Db.Rs("SELECT "
					+ " Unit"
					+ ",Customer"
					+ ",NoTelp AS [No. Telepon]"
					+ ",Alamat1 AS [Alamat #1]"
					+ ",Alamat2 AS [Alamat #2]"
					+ ",Alamat3 AS [Alamat #3]"
                    + ",TglTunggakan AS [Tgl]"
					+ " FROM MS_TUNGGAKAN"
					+ " WHERE NoTunggakan = " + NoTunggakan
					);

				Db.Execute("EXEC spTunggakanEdit"
					+ " '" + NoTunggakan + "'"
					+ ",'" + Unit + "'"
					+ ",'" + Customer + "'"
					+ ",'" + NoTelp + "'"
					+ ",'" + Alamat1 + "'"
					+ ",'" + Alamat2 + "'"
					+ ",'" + Alamat3 + "'"
					);

                Db.Execute("UPDATE MS_TUNGGAKAN SET TglTunggakan = '" + TglTunggakan + "' WHERE NoTunggakan = '" + NoTunggakan + "'");

				DataTable rsAft = Db.Rs("SELECT "
					+ " Unit"
					+ ",Customer"
					+ ",NoTelp AS [No. Telepon]"
					+ ",Alamat1 AS [Alamat #1]"
					+ ",Alamat2 AS [Alamat #2]"
					+ ",Alamat3 AS [Alamat #3]"
                    + ",TglTunggakan AS [Tgl]"
					+ " FROM MS_TUNGGAKAN"
					+ " WHERE NoTunggakan = " + NoTunggakan
					);

                string REP = Db.SingleString("SELECT Ref FROM MS_TUNGGAKAN WHERE NoTunggakan = '" + NoTunggakan + "'");

                int NOCUS = Db.SingleInteger("SELECT NOCUSTOMER FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NOKONTRAK='" + REP + "'");

                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER"
                   + " SET ALAMAT1 ='" + Alamat1 + "'"
                   + ", ALAMAT2 ='" + Alamat2 + "'"
                   + ", ALAMAT3 ='" + Alamat3 + "'"
                   + ", NoTelp ='" + NoTelp + "'"
                   + ", NoHP ='" + NoHP + "'"
                   + " WHERE NoCustomer=" + NOCUS
                   );
                string strSql = "SELECT NoKontrak FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoCustomer = " + NOCUS;
                DataTable rs1 = Db.Rs(strSql);
                for (int i = 0; i < rs1.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN"
                           + " SET ALAMAT1 ='" + Alamat1 + "'"
                           + ", ALAMAT2 ='" + Alamat2 + "'"
                           + ", ALAMAT3 ='" + Alamat3 + "'"
                           + ", NoTelp ='" + NoTelp + "'"
                           + " WHERE REF='" + rs1.Rows[i]["NoKontrak"] + "'"
                           );

                    Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PJT"
                           + " SET ALAMAT1 ='" + Alamat1 + "'"
                           + ", ALAMAT2 ='" + Alamat2 + "'"
                           + ", ALAMAT3 ='" + Alamat3 + "'"
                           + ", NoTelp ='" + NoTelp + "'"
                           + " WHERE REF='" + rs1.Rows[i]["NoKontrak"] + "'"
                           );

                }


                //Update Manual Tgl Kuasa Somasi
                DateTime TanggalKuasa = Convert.ToDateTime(tglKuasa.Text);
                Db.Execute("UPDATE MS_TUNGGAKAN SET TglKuasaSomasi = '" + TanggalKuasa + "' WHERE LevelTunggakan = 4 AND NoTunggakan = " + NoTunggakan);
                //Manual Update 
                Db.Execute("UPDATE MS_TUNGGAKAN SET ManualTunggakan='" + manuTunggakan.Text + "' WHERE NoTunggakan='" + NoTunggakan + "'");
                //Db.Execute("UPDATE MS_TUNGGAKAN_DETIL SET ManualTunggakan='" + manuTunggakan.Text + "' WHERE NoTunggakan='" + NoTunggakan + "'");
				
				//Logfile
				string ketlog = Cf.LogCapture(rs)
					+ Cf.LogCompare(rsBef,rsAft);

				Db.Execute("EXEC spLogTunggakan"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + ketlog + "'"
					+ ",'" + NoTunggakan.ToString().PadLeft(7,'0') + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TUNGGAKAN_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TUNGGAKAN WHERE NoTunggakan = '" + NoTunggakan + "') ");
                Db.Execute("UPDATE MS_TUNGGAKAN_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                return true;
			}
			else
				return false;
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Save()) Js.Close(this);
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(Save()) Response.Redirect("TunggakanEdit.aspx?done=1&NoTunggakan="+NoTunggakan);
		}

		private string NoTunggakan
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoTunggakan"]);
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
