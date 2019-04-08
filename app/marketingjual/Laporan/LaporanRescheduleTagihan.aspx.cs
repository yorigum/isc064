using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanRescheduleTagihan : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				comp.InnerHtml = Mi.Pt;
				rpt.Visible = false;
				Js.Focus(this,scr);
				init();
				if(!Act.Sec("DownloadExcel")) xls.Enabled = false;
			}
		}

		private void init()
		{
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());
		}

		private bool valid()
		{
            string s = "";
            bool x = true;

            if (!Cf.isTgl(dari))
            {
                x = false;
                if (s == "") s = dari.ID;
                daric.Text = "Tanggal";
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                x = false;
                if (s == "") s = sampai.ID;
                sampaic.Text = "Tanggal";
            }
            else
                sampaic.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
		}

		protected void scr_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				Report();
			}
		}
		protected void xls_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				Report();
				Rpt.ToExcel(this,rpt);
			}
		}

		private void Report()
		{
			param.Visible = false;
			rpt.Visible = true;

            newHeader();
			Fill();
		}

        private void newHeader()
        {
            string header = "<h2>" + Mi.Pt + "</h2>";
            header += "<h1>LAPORAN CUSTOM TAGIHAN</h1>";
            header += "Periode : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text);
            header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br /><br />";
            headJudul.Text = header;
        }

		private void Fill()
		{
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string strSql = "SELECT *"
                + " FROM MS_KONTRAK"
                + " WHERE "
                + " NoKontrak IN "
                + "(SELECT NoKontrak from MS_TAGIHAN_LAPORAN where TglInput >= '" + Dari + "' and TglInput <= '" + Sampai + "')"
                ;

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";

                c = new TableCell();
                c.Text = (i + 1).ToString() + ".";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("select Nama from MS_customer where nocustomer = '" + rs.Rows[i]["NoCustomer"] + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                DateTime TglInput = Db.SingleTime("select MAX(TglInput) from MS_Tagihan_laporan"
                + " where NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                + " and TagihanKe = "
                + "(select MAX(TagihanKe) from MS_TAGIHAN_LAPORAN where NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' and TglInput >= '" + Dari + "' and TglInput <= '" + Sampai + "')"
                );
                c = new TableCell();
                c.Text = Cf.Day(TglInput);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string userinput = Db.SingleString("select MAX(UserInput) from MS_Tagihan_laporan"
                + " where NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                + " and TagihanKe = "
                + "(select MAX(TagihanKe) from MS_TAGIHAN_LAPORAN where NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' and TglInput >= '" + Dari + "' and TglInput <= '" + Sampai + "')"
                );
                c = new TableCell();
                c.Text = Db.SingleString("select Nama from ISC007_SECURITY..USERNAME where UserID = '" + userinput + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //tagihan sekarang
                c = new TableCell();
                c.Text = AftSkema(rs.Rows[i]["NoKontrak"].ToString(), Dari.ToString(), Sampai.ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = AftNoUrut(rs.Rows[i]["NoKontrak"].ToString(), Dari.ToString(), Sampai.ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = AftNama(rs.Rows[i]["NoKontrak"].ToString(), Dari.ToString(), Sampai.ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = AftTglJT(rs.Rows[i]["NoKontrak"].ToString(), Dari.ToString(), Sampai.ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = AftNilai(rs.Rows[i]["NoKontrak"].ToString(), Dari.ToString(), Sampai.ToString());
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "&nbsp; &nbsp;";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //tagihan sebelumnya
                c = new TableCell();
                c.Text = PrevSkema(rs.Rows[i]["NoKontrak"].ToString(), Dari.ToString(), Sampai.ToString());
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = PrevNoUrut(rs.Rows[i]["NoKontrak"].ToString(), Dari.ToString(), Sampai.ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = PrevNama(rs.Rows[i]["NoKontrak"].ToString(), Dari.ToString(), Sampai.ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = PrevTglJT(rs.Rows[i]["NoKontrak"].ToString(), Dari.ToString(), Sampai.ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = PrevNilai(rs.Rows[i]["NoKontrak"].ToString(), Dari.ToString(), Sampai.ToString());
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);


                rpt.Rows.Add(r);
            }
		}
        private string AftSkema(string NoKontrak, string Dari, string Sampai)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT TOP 1 Skema FROM MS_TAGIHAN_LAPORAN"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " and TagihanKe = "
                + "(select MAX(TagihanKe) from MS_TAGIHAN_LAPORAN where NoKontrak = '" + NoKontrak + "' and TglInput >= '" + Convert.ToDateTime(Dari) + "' and TglInput <= '" + Convert.ToDateTime(Sampai) + "')"
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br />");
                x.Append(rs.Rows[i]["Skema"].ToString());
            }

            return x.ToString();
        }

        private string AftNoUrut(string NoKontrak, string Dari, string Sampai)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT NoUrut FROM MS_TAGIHAN_LAPORAN"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " and TagihanKe = "
                + "(select MAX(TagihanKe) from MS_TAGIHAN_LAPORAN where NoKontrak = '" + NoKontrak + "' and TglInput >= '" + Convert.ToDateTime(Dari) + "' and TglInput <= '" + Convert.ToDateTime(Sampai) + "')"
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br />");
                x.Append(rs.Rows[i]["NoUrut"].ToString());
            }

            return x.ToString();
        }

        private string AftNama(string NoKontrak, string Dari, string Sampai)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT NamaTagihan FROM MS_TAGIHAN_LAPORAN"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " and TagihanKe = "
                + "(select MAX(TagihanKe) from MS_TAGIHAN_LAPORAN where nokontrak = '" + NoKontrak + "' and TglInput >= '" + Convert.ToDateTime(Dari) + "' and TglInput <= '" + Convert.ToDateTime(Sampai) + "')"
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br />");
                x.Append(rs.Rows[i]["NamaTagihan"]);
            }

            return x.ToString();
        }

        private string AftTglJT(string NoKontrak, string Dari, string Sampai)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT TglJT FROM MS_TAGIHAN_LAPORAN"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " and TagihanKe = "
                + "(select MAX(TagihanKe) from MS_TAGIHAN_LAPORAN where nokontrak = '" + NoKontrak + "' and TglInput >= '" + Convert.ToDateTime(Dari) + "' and TglInput <= '" + Convert.ToDateTime(Sampai) + "')"
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br />");
                x.Append(Cf.Day(rs.Rows[i]["TglJT"]));
            }

            return x.ToString();
        }

        private string AftNilai(string NoKontrak, string Dari, string Sampai)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT NilaiTagihan FROM MS_TAGIHAN_LAPORAN"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " and TagihanKe = "
                + "(select MAX(TagihanKe) from MS_TAGIHAN_LAPORAN where nokontrak = '" + NoKontrak + "' and TglInput >= '" + Convert.ToDateTime(Dari) + "' and TglInput <= '" + Convert.ToDateTime(Sampai) + "')"
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br />");
                x.Append(Cf.Num(rs.Rows[i]["NilaiTagihan"]));
            }

            return x.ToString();
        }

        private string PrevSkema(string NoKontrak, string Dari, string Sampai)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT TOP 1 Skema FROM MS_TAGIHAN_LAPORAN"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " and TagihanKe = "
                + "(select MAX(TagihanKe) - 1 from MS_TAGIHAN_LAPORAN where NoKontrak = '" + NoKontrak + "' and TglInput >= '" + Convert.ToDateTime(Dari) + "' and TglInput <= '" + Convert.ToDateTime(Sampai) + "')"
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br />");
                x.Append(rs.Rows[i]["Skema"].ToString());
            }

            return x.ToString();
        }

        private string PrevNoUrut(string NoKontrak, string Dari, string Sampai)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT NoUrut FROM MS_TAGIHAN_LAPORAN"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " and TagihanKe = "
                + "(select MAX(TagihanKe) - 1 from MS_TAGIHAN_LAPORAN where nokontrak = '" + NoKontrak + "' and TglInput >= '" + Convert.ToDateTime(Dari) + "' and TglInput <= '" + Convert.ToDateTime(Sampai) + "')"
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br />");
                x.Append(rs.Rows[i]["NoUrut"].ToString());
            }

            return x.ToString();
        }

        private string PrevNama(string NoKontrak, string Dari, string Sampai)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT NamaTagihan FROM MS_TAGIHAN_LAPORAN"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " and TagihanKe = "
                + "(select MAX(TagihanKe) - 1 from MS_TAGIHAN_LAPORAN where nokontrak = '" + NoKontrak + "' and TglInput >= '" + Convert.ToDateTime(Dari) + "' and TglInput <= '" + Convert.ToDateTime(Sampai) + "')"
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br />");
                x.Append(rs.Rows[i]["NamaTagihan"]);
            }

            return x.ToString();
        }

        private string PrevTglJT(string NoKontrak, string Dari, string Sampai)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT TglJT FROM MS_TAGIHAN_LAPORAN"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " and TagihanKe = "
                + "(select MAX(TagihanKe) - 1 from MS_TAGIHAN_LAPORAN where nokontrak = '" + NoKontrak + "' and TglInput >= '" + Convert.ToDateTime(Dari) + "' and TglInput <= '" + Convert.ToDateTime(Sampai) + "')"
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br>");
                x.Append(Cf.Day(rs.Rows[i]["TglJT"]));
            }

            return x.ToString();
        }

        private string PrevNilai(string NoKontrak, string Dari, string Sampai)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT NilaiTagihan FROM MS_TAGIHAN_LAPORAN"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " and TagihanKe = "
                + "(select MAX(TagihanKe) - 1 from MS_TAGIHAN_LAPORAN where nokontrak = '" + NoKontrak + "' and TglInput >= '" + Convert.ToDateTime(Dari) + "' and TglInput <= '" + Convert.ToDateTime(Sampai) + "')"
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br>");
                x.Append(Cf.Num(rs.Rows[i]["NilaiTagihan"]));
            }

            return x.ToString();
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
