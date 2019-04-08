namespace ISC064.MARKETINGJUAL
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

	public partial class PrintJadwalTagihanTemplate2 : System.Web.UI.UserControl
	{
		public string nomor;
		public string NoKontrak
		{
			set{ nomor = value; }
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Fill();
            Fill2();
		}

        private void Fill2()
        {
            string strSql = "SELECT "
                + " (SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('BF','DP','ANG')) AS TotalTagihan"
                + ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('ADM')) AS TotalBiaya"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan <> 0 AND SudahCair = 1) AS TotalPelunasan"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan <> 0) AS TotalPembayaran"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan = 0) AS Unallocated"
                + ",PersenLunas"
                + ",NoPPJB"
                + ",Lokasi"
                + ",NoUnit"
                + ", NoCustomer"
                + ", NoAgent"
                + " FROM MS_KONTRAK"
                + " WHERE NoKontrak = '" + nomor + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                //ppjb.Text = Cf.Str(rs.Rows[0]["NoPPJB"]);
                //lokasi.Text = Db.SingleString("select Nama from REF_LOKASI where Lokasi = '" + rs.Rows[0]["Lokasi"].ToString() + "'");
                //nounit.Text = Cf.Str(rs.Rows[0]["NoUnit"]);
                //adm.Text = Db.SingleString("SELECT Nama From ISC007_SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");
                //mar.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[0]["NoAgent"].ToString() + "'"); 
                //cus.Text = Db.SingleString("SELECT Nama From MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"].ToString() + "'");
                //gm.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_SIGN WHERE Dokumen = 'Jadwal Pembayaran' AND SN = 1");
                //jabatangm.Text = Db.SingleString("SELECT Jabatan FROM " + Mi.DbPrefix + "SECURITY..REF_SIGN WHERE Dokumen = 'Jadwal Pembayaran' AND SN = 1");
            }
        }

		protected void Fill()
		{
			string strSql = "SELECT a.*, b.Nama AS Cs, c.Nama AS Ag"
				+ " FROM MS_KONTRAK a"
				+ " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
				+ " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
				+ " WHERE NoKontrak = '" + nomor + "'"
				;
			DataTable rs = Db.Rs(strSql);
			
			if(rs.Rows.Count > 0)
			{
				//FillTb();
			}
		}

		//protected void FillTb()
		//{
		//	string strSql = "SELECT "
		//		+ " NamaTagihan"
		//		+ ",TglJT"
		//		+ ",NilaiTagihan"
		//		+ ",NoUrut"
		//		+ ",Tipe"
		//		+ ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + nomor + "') ) AS SisaTagihan"
		//		+ " FROM MS_TAGIHAN"
		//		+ " WHERE NoKontrak = '" + nomor + "' and Tipe != 'ADM'"
		//		+ " ORDER BY NoUrut";
			
		//	DataTable rs = Db.Rs(strSql);
		//	Rpt.NoData(rpt, rs, "Daftar tagihan untuk kontrak tersebut masih kosong.");

		//	decimal t1 = 0;
		//	decimal t2 = 0;
		//	decimal t3 = 0;

		//	for(int i=0;i<rs.Rows.Count;i++)
		//	{
		//		if(!Response.IsClientConnected) break;

		//		TableRow r = new TableRow();
		//		TableCell c;

		//		t1 = t1 + (decimal)rs.Rows[i]["NilaiTagihan"];
		//		t2 = t2 + (decimal)rs.Rows[i]["SisaTagihan"];

		//		c = new TableCell();
		//		c.Text = nomor + "." + rs.Rows[i]["NoUrut"];
		//		c.Wrap = false;
		//		r.Cells.Add(c);

		//		c = new TableCell();
  //              c.Text = rs.Rows[i]["NamaTagihan"].ToString();
		//		r.Cells.Add(c);

  //              decimal NilaiKontrak = Db.SingleDecimal("select ISNULL(SUM(NilaiTagihan),0) from MS_TAGIHAN where NoKontrak = '" + nomor + "'");
  //              decimal Prosentase = Math.Round((Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]) / NilaiKontrak * 100),2);
  //              c = new TableCell();
  //              c.Text = Prosentase.ToString();
  //              r.Cells.Add(c);
  //              t3 = t3 + (decimal)Convert.ToDecimal(Prosentase);

		//		c = new TableCell();
		//		c.Text = Cf.Day(rs.Rows[i]["TglJT"]);
		//		r.Cells.Add(c);

		//		c = new TableCell();
  //              c.Text = Cf.NumBulat(rs.Rows[i]["NilaiTagihan"]);
		//		c.HorizontalAlign = HorizontalAlign.Right;
		//		r.Cells.Add(c);

		//		c = new TableCell();
		//		c.Text = "";
		//		r.Cells.Add(c);

		//		Rpt.Border(r);
  //              rpt.Rows.Add(r);

		//		if(i == rs.Rows.Count-1)
		//			SubTotal(t1, t2, t3);
		//	}
		//}

		//private decimal Lunas(int NoTagihan)
		//{
		//	string strSql = "SELECT "
		//		+ " CaraBayar"
		//		+ ",TglPelunasan"
		//		+ ",Ket"
		//		+ ",NilaiPelunasan"
		//		+ ",NoUrut"
		//		+ ",SudahCair"
		//		+ " FROM MS_PELUNASAN"
		//		+ " WHERE NoKontrak = '" + nomor + "' AND NoTagihan = " + NoTagihan
		//		+ " ORDER BY NoUrut";
			
		//	decimal t = 0;

		//	DataTable rs = Db.Rs(strSql);
		//	for(int i=0;i<rs.Rows.Count;i++)
		//	{
		//		if(!Response.IsClientConnected) break;

		//		if(NoTagihan==0 && i==0)
		//		{
		//			TableRow r1 = new TableRow();
		//			TableCell c1 = new TableCell();

		//			c1.Text = "<b>PELUNASAN TIDAK TERALOKASI</b>";
		//			c1.ColumnSpan = 7;
		//			r1.Cells.Add(c1);
		//			rpt.Rows.Add(r1);
		//		}

		//		TableRow r = new TableRow();
		//		TableCell c;

		//		string sudahcair = "";
		//		if(!(bool)rs.Rows[i]["SudahCair"])
		//			sudahcair = " <u style='color:orange'>BELUM CAIR</u>";

		//		c = new TableCell();
		//		c.ColumnSpan = 3;
		//		c.Text = rs.Rows[i]["CaraBayar"]
		//			+ ", " + Cf.Day(rs.Rows[i]["TglPelunasan"])
		//			+ " " + rs.Rows[i]["Ket"]
		//			+ sudahcair;
		//		r.Cells.Add(c);

		//		c = new TableCell();
		//		c.Text = "";
		//		r.Cells.Add(c);

		//		c = new TableCell();
  //              c.Text = Cf.NumBulat(rs.Rows[i]["NilaiPelunasan"]);
		//		c.HorizontalAlign = HorizontalAlign.Right;
		//		r.Cells.Add(c);

		//		c = new TableCell();
		//		c.Text = "";
		//		r.Cells.Add(c);

		//		Rpt.Border(r);
		//		r.Cells[0].Attributes["style"] = r.Cells[0].Attributes["style"] + ";padding-left:40";
		//		rpt.Rows.Add(r);

		//		t = t + (decimal)rs.Rows[i]["NilaiPelunasan"];
		//	}

		//	return t;
		//}

		//private void SubTotal(decimal t1, decimal t2, decimal t3)
		//{
		//	TableRow r = new TableRow();
		//	TableCell c;

		//	c = new TableCell();
		//	c.ColumnSpan = 2;
		//	c.Text = "<b>GRAND TOTAL</b>";
		//	r.Cells.Add(c);

  //          c = new TableCell();
  //          c.Font.Bold = true;
  //          c.Text = "<b>" + Cf.NumBulat(t3).ToString() + "</b>"; //Cf.NumBulat(t1);
  //          //c.HorizontalAlign = HorizontalAlign.Right;
  //          r.Cells.Add(c);

  //          c = new TableCell();
  //          c.Text = "";
  //          r.Cells.Add(c);

		//	c = new TableCell();
		//	c.Font.Bold = true;
  //          c.Text = "<b>" + Cf.NumBulat(Math.Round(RoundThousand(t1))).ToString() + "</b>"; //Cf.NumBulat(t1);
		//	c.HorizontalAlign = HorizontalAlign.Right;
		//	r.Cells.Add(c);

  //          //c = new TableCell();
  //          //c.Font.Bold = true;
  //          //c.Text = Cf.Num(t3);
  //          //c.HorizontalAlign = HorizontalAlign.Right;
  //          //r.Cells.Add(c);

  //          //c = new TableCell();
  //          //c.Font.Bold = true;
  //          //c.Text = Cf.Num(t2);
  //          //c.HorizontalAlign = HorizontalAlign.Right;
  //          //r.Cells.Add(c);

		//	rpt.Rows.Add(r);
		//}

        //round up
        private static decimal RoundThousand(decimal input)
        {
            if (input < 100000)
            {
                return 100000;
            }
            else
            {
                input = RoundUp(input);
                if ((input % 100000) > 0)
                {
                    input = (input - (input % 100000)) + 100000;
                }
                return input;
            }
        }

        private static decimal RoundUp(decimal input)
        {
            string x = input.ToString();
            string[] arr = x.Split(new char[] { '.' });

            if (arr.Length > 1)
            {
                if (decimal.Parse(arr[1]) > 0)
                {
                    decimal dc = decimal.Parse(arr[0]) + 1;
                    return dc;
                }
                else
                {
                    return decimal.Parse(arr[0]);
                }
            }
            else
            {
                return input;
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
