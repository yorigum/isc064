using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class ReservasiEdit : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoReservasi");

			if(!Act.Sec("ED:"+Request.PhysicalPath))
			{
				ok.Enabled = false;
				save.Enabled = false;
			}

			if(!Page.IsPostBack)
			{
				Bind();
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

		private void Bind()
		{
			nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			nilai.Attributes["onblur"] = "CalcBlur(this);";

            string Project = Db.SingleString("select ISNULL(Project, '') from MS_RESERVASI where NoReservasi = '" + NoReservasi + "'");

			DataTable rs = Db.Rs("SELECT Nama,Principal,NoAgent FROM MS_AGENT WHERE Status = 'A'"
				+ " ORDER BY Nama,NoAgent");
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["NoAgent"].ToString();
				string t = rs.Rows[i]["Nama"].ToString();
                //if(rs.Rows[i]["Principal"].ToString()!="")
                //    t = t + " ("+rs.Rows[i]["Principal"]+")";
				agent.Items.Add(new ListItem(t,v));
			}

            //Skema bayar
            rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKEMA WHERE Status = 'A' AND Project = '" + Project + "' ORDER BY Nama");
            skema.Items.Add(new ListItem("*** CUSTOMIZE / PENDING", "0")); //cara bayar customize

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Nomor"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                skema.Items.Add(new ListItem(t, v));
            }
            skema.SelectedIndex = 0;
            skema.Attributes["ondblclick"] = "kalk(this)";
        }

		private void Fill()
		{
			btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_RESERVASI_LOG&Pk="+NoReservasi.ToString().PadLeft(5,'0')+"'";
			btndel.Attributes["onclick"] = "location.href='ReservasiDel.aspx?NoReservasi="+NoReservasi+"'";

			DataTable rs = Db.Rs("SELECT a.*, b.NoCustomer, b.Nama, DateDiff(Day,a.Tgl,a.TglExpire) as SisaWaktu "
                + " FROM MS_RESERVASI a"
				+ " INNER JOIN MS_CUSTOMER b"
				+ " ON a.NoCustomer = b.NoCustomer"
				+ " WHERE a.NoReservasi = " + NoReservasi
				);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				wl.Attributes["onclick"] = "location.href='UnitWL.aspx?NoStock="+rs.Rows[0]["NoStock"]+"'";
				if(rs.Rows[0]["Status"].ToString() == "A")
				{
					status.ForeColor = Color.Green;
					status.Text = "Aktif";
				}
				else
				{
					status.ForeColor = Color.Red;
					status.Text = "Expire";
				}

				noreservasi.Text = rs.Rows[0]["NoReservasi"].ToString();
                noreservasifull.Text = rs.Rows[0]["NoReservasi2"].ToString();

                int totalwl = Db.SingleInteger(
					"SELECT COUNT(*) FROM MS_RESERVASI WHERE NoStock = '"+rs.Rows[0]["NoStock"]+"'");
				nourut.Text = rs.Rows[0]["NoUrut"] + "/" + totalwl;
				
				tgl.Text = Cf.Day(rs.Rows[0]["Tgl"]);
				batas.Text = Cf.Date(rs.Rows[0]["TglExpire"]);
                sisawaktu.Text = Cf.NumBulat(rs.Rows[0]["SisaWaktu"]);
                noqueue.Text = rs.Rows[0]["NoQueue"].ToString();
				
				nilai.Text = Cf.Num(rs.Rows[0]["Netto"]);
				skema.SelectedValue = rs.Rows[0]["Skema"].ToString();
                skema.Enabled = false;

				agent.Items.Add(new ListItem(
					"Tidak Berubah : " + rs.Rows[0]["NoAgent"].ToString().PadLeft(5,'0')
					,rs.Rows[0]["NoAgent"].ToString()));
				agent.SelectedValue = rs.Rows[0]["NoAgent"].ToString();
                agent.Enabled = false;

                supervisor.Text = rs.Rows[0]["Supervisor"].ToString();
                manager.Text = rs.Rows[0]["Manager"].ToString();

				//tanggal input, edit dan follow-up
				tglInput.Text = Cf.Date(rs.Rows[0]["TglInput"]);
				tglEdit.Text = Cf.Date(rs.Rows[0]["TglEdit"]);

				printWL.InnerHtml = printWL.InnerHtml + " ("+rs.Rows[0]["PrintWL"]+")";
                printJadwalTagihan.InnerHtml = printJadwalTagihan.InnerHtml + " (" + rs.Rows[0]["PrintJadwalTagihan"] + ")";
                printbform.InnerHtml = printbform.InnerHtml + " (" + rs.Rows[0]["printbform"] + ")";
                
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoStock = '" + rs.Rows[0]["NoStock"] + "'");
                printWL.HRef = "PrintWL.aspx?NoReservasi=" + NoReservasi + "&project=" + Project;
                printJadwalTagihan.HRef = "PrintJadwalTagihanReservasi.aspx?NoReservasi=" + NoReservasi + "&project=" + Project;
                printbform.HRef = "PrintBForm.aspx?NoReservasi=" + NoReservasi + "&project=" + Project;

                //req panahome
                int countTTS = Db.SingleInteger("select Count(*) from " + Mi.DbPrefix + "FINANCEAR..MS_TTS where NoReservasi = '" + NoReservasi + "' and Project = '" + Project + "'");

                if(countTTS != 0)
                {
                    int NoTTS = Db.SingleInteger("select ISNULL(NoTTS, 0) from " + Mi.DbPrefix + "FINANCEAR..MS_TTS where NoReservasi = '" + NoReservasi + "' and Project = '" + Project + "'");
                    int PrintTTS = Db.SingleInteger("select ISNULL(PrintTTS, 0) from " + Mi.DbPrefix + "FINANCEAR..MS_TTS where NoReservasi = '" + NoReservasi + "' and Project = '" + Project + "'");
                    printtts.HRef = "PrintTTS.aspx?NoTTS=" + NoTTS + "&project=" + Project;
                    printtts.InnerHtml = printtts.InnerHtml + " (" + PrintTTS + ")";
                }
                else
                {
                    printtts.Visible = true;
                }
            }
        }

		private bool valid()
		{
			bool x = true;
			string s = "";

			if(!Cf.isTgl(tgl))
			{
				x = false;
				if(s == "") s = tgl.ID;
				tglc.Text = "Tanggal";
			}
			else
				tglc.Text = "";

			if(!Cf.isTgl(batas))
			{
				x = false;
				if(s == "") s = batas.ID;
				batasc.Text = "Tanggal";
			}
			else
				batasc.Text = "";

			if(!Cf.isInt(noqueue))
			{
				x = false;
				if(s=="") s = noqueue.ID;
				noqueuec.Text = "Angka Bulat";
			}
			else
				noqueuec.Text = "";

			if(!Cf.isMoney(nilai))
			{
				x = false;
				if(s == "") s = nilai.ID;
				nilaic.Text = "Angka";
			}
			else
				nilaic.Text = "";

            if (Cf.isEmpty(supervisor))
            {
                x = false;
                if (s == "") s = supervisor.ID;
                spv.Text = "Kosong";
            }
            else
                spv.Text = "";

            if (Cf.isEmpty(manager))
            {
                x = false;
                if (s == "") s = manager.ID;
                mgr.Text = "Kosong";
            }
            else
                mgr.Text = "";

			
			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					+ "2. Nilai pengikatan harus diisi dengan angka.\\n"
					+ "3. NUP harus berupa angka bulat.\\n"
					, "document.getElementById('" + s + "').focus();"
					+ "document.getElementById('" + s + "').select();"
					);

			return x;
		}
		
		private bool Save()
		{
			if(valid())
			{
                string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = (SELECT NoStock FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "')");
				DateTime Tgl = Convert.ToDateTime(tgl.Text);
				DateTime TglExpire = Convert.ToDateTime(batas.Text);
				int NoQueue = Convert.ToInt32(noqueue.Text);
				decimal Netto = Convert.ToDecimal(nilai.Text);
				string Skema = Cf.Str(skema.Text);
				int NoAgent = Convert.ToInt32(agent.SelectedValue);
                string Supervisor = Cf.Str(supervisor.Text);
                string Manager = Cf.Str(manager.Text);
			
				DataTable rsBef = Db.Rs("SELECT"
					+ " CONVERT(varchar,MS_RESERVASI.Tgl,106) AS [Tanggal]"
					+ ",CONVERT(varchar,MS_RESERVASI.TglExpire,100) AS [Batas Waktu]"
					+ ",MS_RESERVASI.Netto AS [Nilai Pengikatan]"
					+ ",MS_RESERVASI.Skema AS [Skema]"
					+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Agent"
					+ ",NoQueue AS [NUP]"
                    + ",Supervisor AS [Supervisor]"
                    + ",MS_AGENT.Manager AS [Manager]"
					+ " FROM MS_RESERVASI"
					+ " INNER JOIN MS_AGENT"
					+ " ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
					+ " WHERE NoReservasi = " + NoReservasi
					);
			
				Db.Execute("EXEC spReservasiEdit"
					+ "  " + NoReservasi
					+ ", " + NoAgent
					+ ",'" + Tgl + "'"
					+ ",'" + TglExpire + "'"
					+ ", " + Netto
					+ ",'" + Skema + "'"
					+ ", " + NoQueue
					);

                Db.Execute("UPDATE MS_RESERVASI SET "
                    + " Supervisor ='" + Supervisor + "'"
                    + ",Manager ='" + Manager + "'"
                    + " WHERE NoReservasi = '" + NoReservasi + "'"
                    );

				DataTable rsAft = Db.Rs("SELECT"
					+ " CONVERT(varchar,MS_RESERVASI.Tgl,106) AS [Tanggal]"
					+ ",CONVERT(varchar,MS_RESERVASI.TglExpire,100) AS [Batas Waktu]"
					+ ",MS_RESERVASI.Netto AS [Nilai Pengikatan]"
					+ ",MS_RESERVASI.Skema AS [Skema]"
					+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Agent"
					+ ",NoQueue AS [NUP]"
                    + ",Supervisor AS [Supervisor]"
                    + ",MS_AGENT.Manager AS [Manager]"
					+ " FROM MS_RESERVASI"
					+ " INNER JOIN MS_AGENT"
					+ " ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
					+ " WHERE NoReservasi = " + NoReservasi
					);

				Db.Execute("EXEC spLogReservasi"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Cf.LogCompare(rsBef, rsAft) + "'"
					+ ",'" + NoReservasi.ToString().PadLeft(5,'0') + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_RESERVASI_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_RESERVASI_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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
			if(Save()) Response.Redirect("ReservasiEdit.aspx?done=1&NoReservasi=" + NoReservasi);
		}

        protected void GantiTipeSales(object sender, System.EventArgs e)
        {
            supervisor.Text = Db.SingleString("SELECT Principal FROM MS_AGENT WHERE NoAgent = '" + agent.SelectedValue + "'");
            //manager.Text = Db.SingleString("SELECT Manager FROM MS_AGENT WHERE NoAgent = '" + agent.SelectedValue + "'");
           
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
