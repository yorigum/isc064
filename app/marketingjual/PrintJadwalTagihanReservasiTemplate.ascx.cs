namespace ISC064.MARKETINGJUAL
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

    public partial class PrintJadwalTagihanReservasiTemplate : System.Web.UI.UserControl
	{
		public string nomor;
		public string NoReservasi
		{
			set{ nomor = value; }
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            Fill();
            //Fill2();
		}

        //private void Fill2()
        //{
        // //   edit.Attributes["onclick"] = "location.href='TagihanEdit.aspx?NoKontrak=" + NoKontrak + "'";

        //    string strSql = "SELECT "
        //        + " (SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('BF','DP','ANG')) AS TotalTagihan"
        //        + ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('ADM')) AS TotalBiaya"
        //        + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan <> 0 AND SudahCair = 1) AS TotalPelunasan"
        //        + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan <> 0) AS TotalPembayaran"
        //        + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan = 0) AS Unallocated"
        //        + ",PersenLunas"
        //        + ",NilaiKontrak"
        //        + ",OutBalance"
        //        + ",Skema"
        //        + ", NoCustomer"
        //        + ", NoAgent"
        //        + " FROM MS_KONTRAK"
        //        + " WHERE NoKontrak = '" + nomor + "'";
        //    DataTable rs = Db.Rs(strSql);

        //    if (rs.Rows.Count == 0)
        //        Response.Redirect("/CustomError/Deleted.html");
        //    else
        //    {
        //        nilai.Text = Cf.Num(rs.Rows[0]["NilaiKontrak"]);
        //        totaltagihan.Text = Cf.Num(rs.Rows[0]["TotalTagihan"]);
        //        totalbiaya.Text = Cf.Num(rs.Rows[0]["TotalBiaya"]);
        //        tagihanbiaya.Text = Cf.Num((decimal)rs.Rows[0]["TotalTagihan"] + (decimal)rs.Rows[0]["TotalBiaya"]);
        //        pelunasan.Text = Cf.Num(rs.Rows[0]["TotalPelunasan"]);
        //        pembayaran.Text = Cf.Num(rs.Rows[0]["TotalPembayaran"]);
        //        persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);

        //        skema.Text = rs.Rows[0]["Skema"].ToString();

        //        adm.Text = Db.SingleString("SELECT Nama From AM106_SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");
        //        //mgr.Text = Db.SingleString("SELECT Principal FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[0]["NoAgent"].ToString() + "'");
        //        mar.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[0]["NoAgent"].ToString() + "'"); 
        //        cus.Text = Db.SingleString("SELECT Nama From MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"].ToString() + "'");
        //        gm.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_SIGN WHERE Dokumen = 'Jadwal Pembayaran' AND SN = 1");
        //        jabatangm.Text = Db.SingleString("SELECT Jabatan FROM " + Mi.DbPrefix + "SECURITY..REF_SIGN WHERE Dokumen = 'Jadwal Pembayaran' AND SN = 1");

        //        telp.Text = Db.SingleString("SELECT NoTelp From MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"].ToString() + "'");
        //        nomorktp.Text = Db.SingleString("SELECT NoKTP From MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"].ToString() + "'");
        //        hp.Text = Db.SingleString("SELECT NoHp From MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"].ToString() + "'");
        //        cus.Text = Db.SingleString("SELECT Nama From MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"].ToString() + "'");
        //        alamat.Text = Db.SingleString("SELECT KTP1 From MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"].ToString() + "'") + "<BR/>" + Db.SingleString("SELECT KTP2 From MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"].ToString() + "'") + "<BR/>" + Db.SingleString("SELECT KTP3 From MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"].ToString() + "'") + "<BR/>" + Db.SingleString("SELECT KTP4 From MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"].ToString() + "'");
        //      //  FillTb();
        //    }
        //}

        protected void Fill()
        {
            string strSql = "SELECT a.*, b.Nama AS Cs, c.Nama AS Ag"
                + " FROM MS_RESERVASI a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
                + " WHERE NoReservasi = '" + nomor + "'"
                ;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count > 0)
            {
                skema.Text = skema2.Text = rs.Rows[0]["Skema"].ToString();
                customer.Text = customer2.Text = rs.Rows[0]["Cs"].ToString();
                //unit.Text = "unit";
                nounit.Text = rs.Rows[0]["NoUnit"].ToString();

                size.Text = "";

                decimal DPP = Convert.ToDecimal(rs.Rows[0]["NilaiDPP"]);
                harga.Text = Cf.Num(DPP);

                decimal NPPN = Convert.ToDecimal(rs.Rows[0]["NilaiPPN"]);                

                ppn.Text = Cf.Num(NPPN);

                decimal total = DPP + Convert.ToDecimal(ppn.Text);
                hargajual.Text = Cf.Num(total);

                DataTable un = Db.Rs("SELECT * FROM MS_UNIT WHERE NoUnit = '" + rs.Rows[0]["NoUnit"] + "'");
                if (un.Rows.Count > 0)
                {
                    tipe.Text = un.Rows[0]["Jenis"].ToString();
                    view.Text = un.Rows[0]["Panorama"].ToString();
                    decimal Luas = Convert.ToDecimal(un.Rows[0]["Luas"]);
                    size.Text = Cf.Num(Luas);
                }

                FillTb();
            }
        }

        protected void FillTb()
        {
            string strSql = "SELECT "
                + " NamaTagihan"
                + ",TglJT"
                + ",NilaiTagihan"
                + ",NoUrut"
                + ",Tipe"
                //+ ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + nomor + "') ) AS SisaTagihan"
                + " FROM MS_RESERVASI_TAGIHAN"
                + " WHERE NoReservasi = '" + nomor + "'"
                + " ORDER BY NoUrut";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(tb, rs, "Daftar tagihan untuk kontrak tersebut masih kosong.");

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                t1 = t1 + (decimal)rs.Rows[i]["NilaiTagihan"];
                //t2 = t2 + (decimal)rs.Rows[i]["SisaTagihan"];

                //c = new TableCell();
                //c.Text = nomor + "." + rs.Rows[i]["NoUrut"];
                //c.Wrap = false;
                //r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaTagihan"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglJT"]);
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                //c = new TableCell();
                //c.Text = Cf.Num(rs.Rows[i]["SisaTagihan"]);
                //c.HorizontalAlign = HorizontalAlign.Right;
                //r.Cells.Add(c);

                Rpt.Border(r);
                tb.Rows.Add(r);

                //	t3 = t3 + Lunas((int)rs.Rows[i]["NoUrut"]);

                if (i == rs.Rows.Count - 1)
                    SubTotal(t1, t3);
            }
        }

        //private decimal Lunas(int NoTagihan)
        //{
        //    string strSql = "SELECT "
        //        + " CaraBayar"
        //        + ",TglPelunasan"
        //        + ",Ket"
        //        + ",NilaiPelunasan"
        //        + ",NoUrut"
        //        + ",SudahCair"
        //        + " FROM MS_PELUNASAN"
        //        + " WHERE NoKontrak = '" + nomor + "' AND NoTagihan = " + NoTagihan
        //        + " ORDER BY NoUrut";

        //    decimal t = 0;

        //    DataTable rs = Db.Rs(strSql);
        //    for (int i = 0; i < rs.Rows.Count; i++)
        //    {
        //        if (!Response.IsClientConnected) break;

        //        if (NoTagihan == 0 && i == 0)
        //        {
        //            TableRow r1 = new TableRow();
        //            TableCell c1 = new TableCell();

        //            c1.Text = "<b>PELUNASAN TIDAK TERALOKASI</b>";
        //            c1.ColumnSpan = 7;
        //            r1.Cells.Add(c1);
        //            tb.Rows.Add(r1);
        //        }

        //        TableRow r = new TableRow();
        //        TableCell c;

        //        string sudahcair = "";
        //        if (!(bool)rs.Rows[i]["SudahCair"])
        //            sudahcair = " <u style='color:orange'>BELUM CAIR</u>";

        //        c = new TableCell();
        //        c.ColumnSpan = 3;
        //        c.Text = rs.Rows[i]["CaraBayar"]
        //            + ", " + Cf.Day(rs.Rows[i]["TglPelunasan"])
        //            + " " + rs.Rows[i]["Ket"]
        //            + sudahcair;
        //        r.Cells.Add(c);

        //        c = new TableCell();
        //        c.Text = "";
        //        r.Cells.Add(c);

        //        c = new TableCell();
        //        c.Text = Cf.Num(rs.Rows[i]["NilaiPelunasan"]);
        //        c.HorizontalAlign = HorizontalAlign.Right;
        //        r.Cells.Add(c);

        //        c = new TableCell();
        //        c.Text = "";
        //        r.Cells.Add(c);

        //        Rpt.Border(r);
        //        r.Cells[0].Attributes["style"] = r.Cells[0].Attributes["style"] + ";padding-left:40";
        //        rpt.Rows.Add(r);

        //        t = t + (decimal)rs.Rows[i]["NilaiPelunasan"];
        //    }

        //    return t;
        //}

        private void SubTotal(decimal t1, decimal t3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            //c.ColumnSpan = 3;
            c.Text = "<b>GRAND TOTAL</b>";
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            //c.Font.Bold = true;
            c.Text = "";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            //c = new TableCell();
            //c.Font.Bold = true;
            //c.Text = Cf.Num(t2);
            //c.HorizontalAlign = HorizontalAlign.Right;
            //r.Cells.Add(c);

            tb.Rows.Add(r);
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
