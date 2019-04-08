namespace ISC064.MARKETINGJUAL
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

	public partial class PrintFBatalTemplate : System.Web.UI.UserControl
	{
		public string nomor;
		public string NoKontrak
		{
			set{ nomor = value; }
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Fill();
		}

		protected void Fill()
		{
			string strSql = "SELECT a.*"
                + ", b.Nama AS Cs"
                + ", b.NoKTP AS NoKTP"
                + ", b.Alamat1"
                + ", b.Alamat2"
                + ", b.Alamat3"
                + ", b.NoTelp"
                + ", b.NoKantor"
                + ", c.Nama AS Ag"
                + " FROM MS_KONTRAK a"
				+ " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
				+ " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
				+ " WHERE NoKontrak = '" + nomor + "'"
				;
			DataTable rs = Db.Rs(strSql);
			
			if(rs.Rows.Count > 0)
			{
				unit.Text = rs.Rows[0]["NoUnit"].ToString();
                nomor2.Text = rs.Rows[0]["NoKontrak"].ToString();

                string[] NoBatal = rs.Rows[0]["NoKontrak"].ToString().Split('/');
                nobatal1.Text = NoBatal[0];

                noktp1.Text = rs.Rows[0]["NoKTP"].ToString();
				cs.Text = rs.Rows[0]["Cs"].ToString();
				ag.Text = rs.Rows[0]["Ag"].ToString();
				tglbatal.Text = Cf.Day(rs.Rows[0]["TglBatal"]);
                tipe1.Text = rs.Rows[0]["Jenis"].ToString();
                alasan1.Text = rs.Rows[0]["AlasanBatal"].ToString();

                string almt = "";
                if (rs.Rows[0]["Alamat1"].ToString() != "")
                {
                    almt += rs.Rows[0]["Alamat1"];
                    if (rs.Rows[0]["Alamat2"] != "") almt += "&nbsp;";
                }
                if (rs.Rows[0]["Alamat2"].ToString() != "")
                {
                    almt += rs.Rows[0]["Alamat2"];
                    if (rs.Rows[0]["Alamat3"].ToString() != "") almt += "&nbsp;";
                }
                if (rs.Rows[0]["Alamat3"].ToString() != "")
                {
                    almt += rs.Rows[0]["Alamat3"];
                }
                alamatktp.Text = almt;

                string telpcs = "";
                if (rs.Rows[0]["NoTelp"].ToString() != "")
                {
                    telpcs += rs.Rows[0]["NoTelp"];
                    if (rs.Rows[0]["NoKantor"] != "") telpcs += "&nbsp;/&nbsp";
                }
                if (rs.Rows[0]["NoKantor"].ToString() != "")
                {
                    telpcs += rs.Rows[0]["NoKantor"];
                }
                telphpfax.Text = telpcs;
				//nilaiklaim.Text = Cf.Num(rs.Rows[0]["NilaiKlaim"]);
				//tgl.Text = Cf.Day(DateTime.Today);

                //if(Convert.ToBoolean(rs.Rows[0]["FOBO"]))
                //    fobo.Text = "Sudah Pengakuan";

				//FillTb();
			}
		}

        //protected void FillTb()
        //{
        //    string strSql = "SELECT "
        //        + " NamaTagihan"
        //        + ",TglJT"
        //        + ",NilaiTagihan"
        //        + ",NoUrut"
        //        + ",Tipe"
        //        + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + nomor + "') ) AS SisaTagihan"
        //        + " FROM MS_TAGIHAN"
        //        + " WHERE NoKontrak = '" + nomor + "'"
        //        + " ORDER BY NoUrut";
			
        //    DataTable rs = Db.Rs(strSql);
        //    Rpt.NoData(rpt, rs, "Daftar tagihan untuk kontrak tersebut masih kosong.");

        //    decimal t1 = 0;
        //    decimal t2 = 0;
        //    decimal t3 = 0;

        //    for(int i=0;i<rs.Rows.Count;i++)
        //    {
        //        if(!Response.IsClientConnected) break;

        //        TableRow r = new TableRow();
        //        TableCell c;

        //        t1 = t1 + (decimal)rs.Rows[i]["NilaiTagihan"];
        //        t2 = t2 + (decimal)rs.Rows[i]["SisaTagihan"];

        //        c = new TableCell();
        //        c.Text = nomor + "." + rs.Rows[i]["NoUrut"];
        //        c.Wrap = false;
        //        r.Cells.Add(c);

        //        c = new TableCell();
        //        c.Text = rs.Rows[i]["NamaTagihan"].ToString();
        //        r.Cells.Add(c);

        //        c = new TableCell();
        //        c.Text = Cf.Day(rs.Rows[i]["TglJT"]);
        //        r.Cells.Add(c);

        //        c = new TableCell();
        //        c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
        //        c.HorizontalAlign = HorizontalAlign.Right;
        //        r.Cells.Add(c);

        //        c = new TableCell();
        //        c.Text = "";
        //        r.Cells.Add(c);

        //        c = new TableCell();
        //        c.Text = Cf.Num(rs.Rows[i]["SisaTagihan"]);
        //        c.HorizontalAlign = HorizontalAlign.Right;
        //        r.Cells.Add(c);

        //        Rpt.Border(r);
        //        rpt.Rows.Add(r);

        //        t3 = t3 + Lunas((int)rs.Rows[i]["NoUrut"]);

        //        if(i == rs.Rows.Count-1)
        //            SubTotal(t1, t2, t3);
        //    }
        //}

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
        //    for(int i=0;i<rs.Rows.Count;i++)
        //    {
        //        if(!Response.IsClientConnected) break;

        //        if(NoTagihan==0 && i==0)
        //        {
        //            TableRow r1 = new TableRow();
        //            TableCell c1 = new TableCell();

        //            c1.Text = "<b>PELUNASAN TIDAK TERALOKASI</b>";
        //            c1.ColumnSpan = 7;
        //            r1.Cells.Add(c1);
        //            rpt.Rows.Add(r1);
        //        }

        //        TableRow r = new TableRow();
        //        TableCell c;

        //        string sudahcair = "";
        //        if(!(bool)rs.Rows[i]["SudahCair"])
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

        //private void SubTotal(decimal t1, decimal t2, decimal t3)
        //{
        //    TableRow r = new TableRow();
        //    TableCell c;

        //    c = new TableCell();
        //    c.ColumnSpan = 3;
        //    c.Text = "<b>GRAND TOTAL</b>";
        //    r.Cells.Add(c);

        //    c = new TableCell();
        //    c.Font.Bold = true;
        //    c.Text = Cf.Num(t1);
        //    c.HorizontalAlign = HorizontalAlign.Right;
        //    r.Cells.Add(c);

        //    c = new TableCell();
        //    c.Font.Bold = true;
        //    c.Text = Cf.Num(t3);
        //    c.HorizontalAlign = HorizontalAlign.Right;
        //    r.Cells.Add(c);

        //    c = new TableCell();
        //    c.Font.Bold = true;
        //    c.Text = Cf.Num(t2);
        //    c.HorizontalAlign = HorizontalAlign.Right;
        //    r.Cells.Add(c);

        //    rpt.Rows.Add(r);
        //}

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
