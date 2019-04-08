namespace ISC064.FINANCEAR
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

	public partial class PrintFPSTemplate : System.Web.UI.UserControl
	{

		//Passing parameter
		public string nomor;
		public string NoTTS
		{
			set{nomor = value;}
		}
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Fill();
		}

		protected void Fill()
		{
			string strSql = "SELECT a.*, b.NoCustomer, c.NPWP"
				+ " FROM MS_TTS a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER c ON b.NoCustomer = c.NoCustomer"
				+ " WHERE NoTTS = " + nomor
				;
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count != 0)
			{
				System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
				string HeaderPajak = "";
				string JenisPPN = Db.SingleString("SELECT JenisPPN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Cf.Pk(rs.Rows[0]["Ref"]) + "'");
				if(JenisPPN == "PEMERINTAH")
					HeaderPajak = (string) s.GetValue("NoFPSPemerintah", typeof(string));
				else if(JenisPPN == "KONSUMEN")
					HeaderPajak = (string) s.GetValue("NoFPSKonsumen", typeof(string));

                nopajak.Text = nopajak2.Text = nopajak3.Text = rs.Rows[0]["NoFPS"].ToString();// HeaderPajak + rs.Rows[0]["NoFPS"];


                npwp.Text = npwp2.Text = npwp3.Text = rs.Rows[0]["NPWP"].ToString();
				nama.Text = nama2.Text = nama3.Text = rs.Rows[0]["Customer"].ToString();
				strSql = "SELECT NPWPAlamat1, NPWPAlamat2, NPWPAlamat3"
					+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER"
					+ " WHERE NoCustomer = " + Cf.Pk(rs.Rows[0]["NoCustomer"])
					;
				DataTable rsCs = Db.Rs(strSql);
				alamat.Text = alamat2.Text = alamat3.Text = rsCs.Rows[0]["NPWPAlamat1"]
					+ "<br />"
					+ rsCs.Rows[0]["NPWPAlamat2"]
					+ "<br />"
					+ rsCs.Rows[0]["NPWPALamat3"]
					;

				tgl.Text = tgl2.Text = tgl3.Text = Convert.ToDateTime(rs.Rows[0]["TglBKM"]).Day
					+ " "
					+ Cf.Monthname(Convert.ToDateTime(rs.Rows[0]["TglBKM"]).Month)
					+ " "
					+ Convert.ToDateTime(rs.Rows[0]["TglBKM"]).Year
					;

                ttd.Text = ttd2.Text = ttd3.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_SIGN WHERE Dokumen = 'Faktur Pajak' AND SN = 1");

                DataTable aa = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_DATA WHERE No = 1");
                if (aa.Rows.Count > 0)
                {
                    npwpnama.Text = npwpnama2.Text = npwpnama3.Text = aa.Rows[0]["NPWPNama"].ToString();
                    npwpno.Text = npwpno2.Text = npwpno3.Text = aa.Rows[0]["NPWP"].ToString();
                    npwpalamat.Text = npwpalamat2.Text = npwpalamat3.Text = Cf.StrKet(aa.Rows[0]["AlamatNPWP"]);
                }

				FillTable();
                FillTable2();
                FillTable3();
			}
		}

		protected void FillTable()
		{
			string ManualBKM = Db.SingleString("SELECT ManualBKM FROM MS_TTS WHERE NoTTS = " + nomor);
			string strSql = "SELECT "
				+ " NilaiPelunasan AS Nilai"
				+ ", NoKontrak"
				+ ",NoKontrak + '.' + CONVERT(VARCHAR,NoTagihan) AS RefTagihan"
				+ ",CASE NoTagihan"
				+ "		WHEN 0 THEN 'UNALLOCATED'"
				+ "		ELSE (SELECT NamaTagihan FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
				+ " END AS NamaTagihan"
				+ " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN AS l "
				+ " WHERE NoTTS = " + nomor;
			DataTable rs = Db.Rs(strSql);

			decimal t = 0, t2 = 0;

            

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				Label l;

				l = new Label();
				l.Text = "<tr><td style='border: 1px solid Black;' valign='top'>";
				list.Controls.Add(l);

				l = new Label();
				l.Text = (i + 1).ToString();
				list.Controls.Add(l);

				l = new Label();
				l.Text = "</td><td  style='border:1px solid black;' valign='top'>";
				list.Controls.Add(l);

                string minheight = "";
                int n = 300 / rs.Rows.Count;
                if ((i == (rs.Rows.Count - 1)) && rs.Rows.Count <= 5)
                    minheight = "style='height:" + n + "px'";
				
				l = new Label();
				l.Text = "<table>"
					+ "<tr>"
					+ "<td>No. Unit</td>"
					+ "<td>:</td>"
					+ "<td>" + Db.SingleString("SELECT NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'") + "</td>"
					+ "</tr>"
					+ "<tr valign='top' " + minheight + ">"
					+ "<td>Pembayaran</td>"
					+ "<td>:</td>"
					+ "<td>" + rs.Rows[i]["NamaTagihan"].ToString() + "</td>"
					+ "</tr>"
					+ "</table>"
					;
				list.Controls.Add(l);
                
				l = new Label();
				l.Text = "</td><td align='right' valign='top' style='border:1px solid black;'>";
				list.Controls.Add(l);

                decimal DPP = Math.Round(Convert.ToDecimal(rs.Rows[i]["Nilai"]) / (decimal)1.1);
				
				l = new Label();
                l.Text = Cf.Num(DPP); //Cf.Num(rs.Rows[i]["Nilai"]);
				list.Controls.Add(l);

				l = new Label();
				l.Text = "</td></tr>";
				list.Controls.Add(l);

				t += Convert.ToDecimal(rs.Rows[i]["Nilai"]);
                t2 += DPP;

				if(i == (rs.Rows.Count - 1))
					SubTotal(t, t2);
			}
		}

        protected void FillTable2()
        {
            string ManualBKM = Db.SingleString("SELECT ManualBKM FROM MS_TTS WHERE NoTTS = " + nomor);
            string strSql = "SELECT "
                + " NilaiPelunasan AS Nilai"
                + ", NoKontrak"
                + ",NoKontrak + '.' + CONVERT(VARCHAR,NoTagihan) AS RefTagihan"
                + ",CASE NoTagihan"
                + "		WHEN 0 THEN 'UNALLOCATED'"
                + "		ELSE (SELECT NamaTagihan FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
                + " END AS NamaTagihan"
                + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN AS l "
                + " WHERE NoTTS = " + nomor;
            DataTable rs = Db.Rs(strSql);

            decimal t = 0, t2 = 0;



            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                Label l;

                l = new Label();
                l.Text = "<tr><td style='border: 1px solid Black;' valign='top'>";
                list2.Controls.Add(l);

                l = new Label();
                l.Text = (i + 1).ToString();
                list2.Controls.Add(l);

                l = new Label();
                l.Text = "</td><td  style='border:1px solid black;' valign='top'>";
                list2.Controls.Add(l);

                string minheight = "";
                int n = 300 / rs.Rows.Count;
                if ((i == (rs.Rows.Count - 1)) && rs.Rows.Count <= 5)
                    minheight = "style='height:" + n + "px'";

                l = new Label();
                l.Text = "<table>"
                    + "<tr>"
                    + "<td>No. Unit</td>"
                    + "<td>:</td>"
                    + "<td>" + Db.SingleString("SELECT NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'") + "</td>"
                    + "</tr>"
                    + "<tr valign='top' " + minheight + ">"
                    + "<td>Pembayaran</td>"
                    + "<td>:</td>"
                    + "<td>" + rs.Rows[i]["NamaTagihan"].ToString() + "</td>"
                    + "</tr>"
                    + "</table>"
                    ;
                list2.Controls.Add(l);
                
                l = new Label();
                l.Text = "</td><td align='right' valign='top' style='border:1px solid black;'>";
                list2.Controls.Add(l);

                decimal DPP = Math.Round(Convert.ToDecimal(rs.Rows[i]["Nilai"]) / (decimal)1.1);

                l = new Label();
                l.Text = Cf.Num(DPP); //Cf.Num(rs.Rows[i]["Nilai"]);
                list2.Controls.Add(l);

                l = new Label();
                l.Text = "</td></tr>";
                list2.Controls.Add(l);

                t += Convert.ToDecimal(rs.Rows[i]["Nilai"]);
                t2 += DPP;

                if (i == (rs.Rows.Count - 1))
                    SubTotal2(t, t2);
            }
        }

        protected void FillTable3()
        {
            string ManualBKM = Db.SingleString("SELECT ManualBKM FROM MS_TTS WHERE NoTTS = " + nomor);
            string strSql = "SELECT "
                + " NilaiPelunasan AS Nilai"
                + ", NoKontrak"
                + ",NoKontrak + '.' + CONVERT(VARCHAR,NoTagihan) AS RefTagihan"
                + ",CASE NoTagihan"
                + "		WHEN 0 THEN 'UNALLOCATED'"
                + "		ELSE (SELECT NamaTagihan FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
                + " END AS NamaTagihan"
                + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN AS l "
                + " WHERE NoTTS = " + nomor;
            DataTable rs = Db.Rs(strSql);

            decimal t = 0, t2 = 0;



            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                Label l;

                l = new Label();
                l.Text = "<tr><td style='border: 1px solid Black;' valign='top'>";
                list3.Controls.Add(l);

                l = new Label();
                l.Text = (i + 1).ToString();
                list3.Controls.Add(l);

                l = new Label();
                l.Text = "</td><td  style='border:1px solid black;' valign='top'>";
                list3.Controls.Add(l);

                string minheight = "";
                int n = 300 / rs.Rows.Count;
                if ((i == (rs.Rows.Count - 1)) && rs.Rows.Count <= 5)
                    minheight = "style='height:" + n + "px'";

                l = new Label();
                l.Text = "<table>"
                    + "<tr>"
                    + "<td>No. Unit</td>"
                    + "<td>:</td>"
                    + "<td>" + Db.SingleString("SELECT NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'") + "</td>"
                    + "</tr>"
                    + "<tr valign='top' " + minheight + ">"
                    + "<td>Pembayaran</td>"
                    + "<td>:</td>"
                    + "<td>" + rs.Rows[i]["NamaTagihan"].ToString() + "</td>"
                    + "</tr>"
                    + "</table>"
                    ;
                list3.Controls.Add(l);
                
                l = new Label();
                l.Text = "</td><td align='right' valign='top' style='border:1px solid black;'>";
                list3.Controls.Add(l);

                decimal DPP = Math.Round(Convert.ToDecimal(rs.Rows[i]["Nilai"]) / (decimal)1.1);

                l = new Label();
                l.Text = Cf.Num(DPP); //Cf.Num(rs.Rows[i]["Nilai"]);
                list3.Controls.Add(l);

                l = new Label();
                l.Text = "</td></tr>";
                list3.Controls.Add(l);

                t += Convert.ToDecimal(rs.Rows[i]["Nilai"]);
                t2 += DPP;

                if (i == (rs.Rows.Count - 1))
                    SubTotal3(t, t2);
            }
        }

		protected void SubTotal(decimal t, decimal t2)
		{
			string NoKontrak = Db.SingleString("SELECT Ref FROM MS_TTS WHERE NoTTS = " + nomor);
			string JenisPPN = Db.SingleString("SELECT JenisPPN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            decimal PPN = t - t2;

			Label l;

			l = new Label();
			l.Text = "<tr><td style='border-top: 1px solid Black; border: 1px solid Black;' colspan='2'>Jumlah Harga Jual/Penggantian/Uang Muka/Termin *)</td>";
			list.Controls.Add(l);
			
			l = new Label();
			l.Text = "<td align='right' style='border: 1px solid Black;'>" + Cf.Num(t2) + "</td></tr>";
			list.Controls.Add(l);

			l = new Label();
			l.Text = "<tr><td style='border: 1px solid Black; border: 1px solid Black;' colspan='2'>Dikurangi potongan harga/uang muka yang telah diterima *)</td>";
			list.Controls.Add(l);
			
			l = new Label();
			l.Text = "<td align='right' style='border: 1px solid Black;'>&nbsp;</td></tr>";
			list.Controls.Add(l);

			l = new Label();
			l.Text = "<tr><td style='border: 1px solid Black; border-right: 1px dashed Black;' colspan='2'>Dasar Pengenaan Pajak = [ Harga Jual ]</td>";
			list.Controls.Add(l);
			
			l = new Label();
			if(JenisPPN == "PEMERINTAH")
				l.Text = "<td align='right' style='border: 1px solid Black;'>0</td></tr>";
			else if(JenisPPN == "KONSUMEN")
                l.Text = "<td align='right' style='border: 1px solid Black;'>" + Cf.Num(t2) + "</td></tr>";
			list.Controls.Add(l);

			l = new Label();
			l.Text = "<tr><td style='border: 1px solid Black; border: 1px solid Black;' colspan='2'>PPN = 10% x Dasar Pengenaan Pajak</td>";
			list.Controls.Add(l);
			
			l = new Label();
			if(JenisPPN == "PEMERINTAH")
				l.Text = "<td align='right' style='border-top: 1px dashed Black;'>0</td></tr>";
			else if(JenisPPN == "KONSUMEN")
                l.Text = "<td align='right' style='border-top: 1px dashed Black;'>" + Cf.Num(PPN) + "</td></tr>";
			list.Controls.Add(l);
		}

        protected void SubTotal2(decimal t, decimal t2)
        {
            string NoKontrak = Db.SingleString("SELECT Ref FROM MS_TTS WHERE NoTTS = " + nomor);
            string JenisPPN = Db.SingleString("SELECT JenisPPN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            decimal PPN = t - t2;

            Label l;

            l = new Label();
            l.Text = "<tr><td style='border-top: 1px solid Black; border: 1px solid Black;' colspan='2'>Jumlah Harga Jual/Penggantian/Uang Muka/Termin *)</td>";
            list2.Controls.Add(l);

            l = new Label();
            l.Text = "<td align='right' style='border: 1px solid Black;'>" + Cf.Num(t2) + "</td></tr>";
            list2.Controls.Add(l);

            l = new Label();
            l.Text = "<tr><td style='border: 1px solid Black; border: 1px solid Black;' colspan='2'>Dikurangi potongan harga/uang muka yang telah diterima *)</td>";
            list2.Controls.Add(l);

            l = new Label();
            l.Text = "<td align='right' style='border: 1px solid Black;'>&nbsp;</td></tr>";
            list2.Controls.Add(l);

            l = new Label();
            l.Text = "<tr><td style='border: 1px solid Black; border-right: 1px dashed Black;' colspan='2'>Dasar Pengenaan Pajak = [ Harga Jual ]</td>";
            list2.Controls.Add(l);

            l = new Label();
            if (JenisPPN == "PEMERINTAH")
                l.Text = "<td align='right' style='border: 1px solid Black;'>0</td></tr>";
            else if (JenisPPN == "KONSUMEN")
                l.Text = "<td align='right' style='border: 1px solid Black;'>" + Cf.Num(t2) + "</td></tr>";
            list2.Controls.Add(l);

            l = new Label();
            l.Text = "<tr><td style='border: 1px solid Black; border: 1px solid Black;' colspan='2'>PPN = 10% x Dasar Pengenaan Pajak</td>";
            list2.Controls.Add(l);

            l = new Label();
            if (JenisPPN == "PEMERINTAH")
                l.Text = "<td align='right' style='border-top: 1px dashed Black;'>0</td></tr>";
            else if (JenisPPN == "KONSUMEN")
                l.Text = "<td align='right' style='border-top: 1px dashed Black;'>" + Cf.Num(PPN) + "</td></tr>";
            list2.Controls.Add(l);
        }

        protected void SubTotal3(decimal t, decimal t2)
        {
            string NoKontrak = Db.SingleString("SELECT Ref FROM MS_TTS WHERE NoTTS = " + nomor);
            string JenisPPN = Db.SingleString("SELECT JenisPPN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            decimal PPN = t - t2;

            Label l;

            l = new Label();
            l.Text = "<tr><td style='border-top: 1px solid Black; border: 1px solid Black;' colspan='2'>Jumlah Harga Jual/Penggantian/Uang Muka/Termin *)</td>";
            list3.Controls.Add(l);

            l = new Label();
            l.Text = "<td align='right' style='border: 1px solid Black;'>" + Cf.Num(t2) + "</td></tr>";
            list3.Controls.Add(l);

            l = new Label();
            l.Text = "<tr><td style='border: 1px solid Black; border: 1px solid Black;' colspan='2'>Dikurangi potongan harga/uang muka yang telah diterima *)</td>";
            list3.Controls.Add(l);

            l = new Label();
            l.Text = "<td align='right' style='border: 1px solid Black;'>&nbsp;</td></tr>";
            list3.Controls.Add(l);

            l = new Label();
            l.Text = "<tr><td style='border: 1px solid Black; border-right: 1px dashed Black;' colspan='2'>Dasar Pengenaan Pajak = [ Harga Jual ]</td>";
            list3.Controls.Add(l);

            l = new Label();
            if (JenisPPN == "PEMERINTAH")
                l.Text = "<td align='right' style='border: 1px solid Black;'>0</td></tr>";
            else if (JenisPPN == "KONSUMEN")
                l.Text = "<td align='right' style='border: 1px solid Black;'>" + Cf.Num(t2) + "</td></tr>";
            list3.Controls.Add(l);

            l = new Label();
            l.Text = "<tr><td style='border: 1px solid Black; border: 1px solid Black;' colspan='2'>PPN = 10% x Dasar Pengenaan Pajak</td>";
            list3.Controls.Add(l);

            l = new Label();
            if (JenisPPN == "PEMERINTAH")
                l.Text = "<td align='right' style='border-top: 1px dashed Black;'>0</td></tr>";
            else if (JenisPPN == "KONSUMEN")
                l.Text = "<td align='right' style='border-top: 1px dashed Black;'>" + Cf.Num(PPN) + "</td></tr>";
            list3.Controls.Add(l);
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
