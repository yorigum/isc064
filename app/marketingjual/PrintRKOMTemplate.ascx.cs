namespace ISC064.MARKETINGJUAL
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

	public partial class PrintRKOMTemplate : System.Web.UI.UserControl
	{
		
		//Passing parameter
		public string nomor;
		public string NoKontrak
		{
			set{nomor = value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Fill();
		}

		private void Fill()
		{
			string strSql = "SELECT * FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE NoKontrak = '" + nomor + "'";
			DataTable rs = Db.Rs(strSql);
			if(rs.Rows.Count!=0)
			{
				nomorl.Text = rs.Rows[0]["NoKontrak"].ToString();
				tgl.Text = Cf.Day(rs.Rows[0]["TglKontrak"]);

				nama.Text = rs.Rows[0]["Nama"].ToString();
				alamatsurat.Text = rs.Rows[0]["Alamat1"]
					+ "<br />" + rs.Rows[0]["Alamat2"]
					+ "<br />" + rs.Rows[0]["Alamat3"];

				telp.Text = rs.Rows[0]["NoTelp"].ToString();
				hp.Text = rs.Rows[0]["NoHP"].ToString();
				
				tipe.Text = Db.SingleString("SELECT Nama FROM REF_JENIS WHERE Jenis = '"+rs.Rows[0]["Jenis"]+"'");
				lokasi.Text = rs.Rows[0]["Lokasi"].ToString();
				unit.Text = rs.Rows[0]["NoUnit"].ToString();
				
				netto.Text = Cf.Num(Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]));
				netto2.Text = Money.Str(Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]));
				skema.Text = rs.Rows[0]["Skema"].ToString();

				DataTable rsag = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent = "+Cf.Pk(rs.Rows[0]["NoAgent"]));
				if(rsag.Rows.Count!=0)
				{
					ag.Text = rsag.Rows[0]["Nama"].ToString();
					agid.Text = rsag.Rows[0]["NoAgent"].ToString().PadLeft(5,'0');
					agalamat.Text = rsag.Rows[0]["Alamat"].ToString();
					agtelp.Text = rsag.Rows[0]["Kontak"].ToString();
					principal.Text = rsag.Rows[0]["Principal"].ToString();
				}

                decimal totalkomisi = Db.SingleDecimal("SELECT ISNULL(SUM(Nilai),0) FROM MS_KOMISIR_DETAIL WHERE NoKomisi IN (SELECT NoKomisi FROM MS_KOMISI WHERE NoKontrak = '" + nomor + "')");
                kom.Text = Cf.Num(totalkomisi);
                kom2.Text = Money.Str(totalkomisi);
                skemakom.Text = rs.Rows[0]["SkemaKomisi"].ToString();

				decimal net = Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]);
				if(net!=0)
				{
					kompersen.Text = Cf.Num((totalkomisi/net)*100);
				}

				DataTable rsLunas = Db.Rs("SELECT TOP 1 *"
					+ ",CASE CaraBayar"
					+ "		WHEN 'TN' THEN 'TUNAI'"
					+ "		WHEN 'KK' THEN 'KARTU KREDIT'"
					+ "		WHEN 'KD' THEN 'KARTU DEBIT'"
					+ "		WHEN 'TR' THEN 'TRANSFER BANK'"
					+ "		WHEN 'BG' THEN 'CEK GIRO'"
					+ "		WHEN 'UJ' THEN 'UANG JAMINAN'"
					+ "		WHEN 'DN' THEN 'DISKON'"
					+ " END AS CaraBayar2"
					+ " FROM MS_PELUNASAN WHERE NoKontrak = '"+nomor+"' AND NoTagihan = 1");
				if(rsLunas.Rows.Count!=0)
				{
					bftgl.Text = Cf.Day(rsLunas.Rows[0]["TglPelunasan"]);
					bf.Text = Cf.Num(Convert.ToDecimal(rsLunas.Rows[0]["NilaiPelunasan"]));
					bf2.Text = Money.Str(Convert.ToDecimal(rsLunas.Rows[0]["NilaiPelunasan"]));
					carabayar.Text = rsLunas.Rows[0]["CaraBayar2"].ToString()
						+ " " + rsLunas.Rows[0]["Ket"].ToString();
				}

				tagno.InnerHtml = nomor;
				tagcs.InnerHtml = nama.Text;
				tagunit.InnerHtml = unit.Text;
				tagag.InnerHtml = ag.Text;

				FillTb();
			}
		}

        private void FillTb()
        {
            //string strSql = "SELECT "
            //	+ " NamaKomisi"
            //	+ ",NilaiKomisi"
            //	+ ",NoUrut"
            //	+ ",Jadwal"
            //	+ ",TermCair"
            //	+ ",Tipe"
            //	+ " FROM MS_KOMISI"
            //	+ " WHERE NoKontrak = '" + nomor + "'"
            //	+ " ORDER BY NoUrut";
            string strSql = "SELECT *,a.NoAgent AS Ag,a.SN AS sn FROM MS_KOMISI_TERM a JOIN MS_KOMISI b ON a.NoKomisi = b.NoKomisi WHERE b.NoKontrak = '" + nomor + "'";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Daftar komisi untuk kontrak tersebut masih kosong.");

			decimal t = 0;
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

                t = t + (decimal)rs.Rows[i]["NilaiCair"];

                c = new TableCell();
                c.Text = Convert.ToString(i + 1);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Tipe FROM REF_AGENT_TIPE WHERE ID = (SELECT SalesTipe FROM MS_AGENT WHERE NoAgent = " + rs.Rows[i]["NoAgent"] + ")");
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaSkema"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                DateTime tgl = Db.SingleTime("SELECT Tgl FROM MS_KOMISIR WHERE NoKomisiR IN (SELECT NoKomisiR FROM MS_KOMISIR_DETAIL WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"] + "' AND NoAgent = '" + rs.Rows[i]["Ag"] + "')");
                c.Text = Cf.Day(tgl);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiCair"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);

				if(i==rs.Rows.Count-1)
					SubTotal(t);
			}
		}

		private void SubTotal(decimal t)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = new TableCell();
			c.ColumnSpan = 4;
			c.Text = "<b>GRAND TOTAL</b>";
			r.Cells.Add(c);

			c = new TableCell();
			c.Font.Bold = true;
			c.ColumnSpan = 2;
			c.Text = Cf.Num(t);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
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
