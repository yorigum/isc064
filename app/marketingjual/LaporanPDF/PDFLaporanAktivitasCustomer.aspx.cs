using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanGantiNama : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ListBox lokasi;
		protected System.Web.UI.WebControls.ListBox agent;
		protected System.Web.UI.WebControls.RadioButton statusS;
		protected System.Web.UI.WebControls.RadioButton statusA;
		protected System.Web.UI.WebControls.RadioButton statusB;
		protected System.Web.UI.WebControls.CheckBox jenisCheck;
		protected System.Web.UI.WebControls.Label jenisc;
		protected System.Web.UI.WebControls.CheckBoxList jenis;
		protected System.Web.UI.WebControls.RadioButton bfS;
		protected System.Web.UI.WebControls.RadioButton bf1;
		protected System.Web.UI.WebControls.RadioButton bf2;
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            Report();
		}

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");

            return a;
        }

	
		private void Report()
        {
            lblA.Text = "<h3>A. Customer Reservasi</h3>";
            rptA.Visible = true;

            lblB.Text = "<h3>B. Customer Batal</h3>";
            rptB.Visible = true;

            lblC.Text = "<h3>C. Customer Pindah Unit</h3>";
            rpt.Visible = true;

            lblD.Text = "<h3>D. Customer Pengalihan Hak</h3>";
            rptD.Visible = true;
            //Header();

            newHeader();
            Fill();
		}
        private void newHeader()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            string header = "<h2>" + Mi.Pt + "</h2>";
            header += "<h1 class='title'>LAPORAN AKTIVITAS CUSTOMER</h1>";
            header += "<h4>Project : " + Project + "</h4>";
            header += "Periode : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai);
            header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br />";
            headJudul.Text = header;
        }

        private void fillReserv(DateTime Dari, DateTime Sampai)
        {
            string aa = "";
            if (UserAgent() > 0)
                aa = " AND a.NoAgent = " + UserAgent();

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND b.Project IN ('" + Project.Replace(",", "','") + "')";

            string strSql = "SELECT a.*, b.LuasSG, CASE WHEN a.Status = 'A' THEN 'AKTIF' ELSE 'BLOKIR' END AS Status2, b.Project"
                + " FROM MS_RESERVASI a"
                + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                + " WHERE a.Tgl >= '" + Dari + "'"
                + " AND a.Tgl <= '" + Sampai + "'"
                + nProject
                + aa
                ;
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status2"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT b.Nama"
                    + " FROM MS_RESERVASI a"
                    + " INNER JOIN MS_CUSTOMER b"
                    + " ON a.NoCustomer = b.NoCustomer"
                    + " WHERE a.NoReservasi ='" + rs.Rows[i]["NoReservasi"].ToString() + "'"
                    ;
                //DataTable reserv = Db.Rs(strSql);
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                string agentR = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent='" + rs.Rows[i]["NoAgent"] + "'");
                c.Text = agentR;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LuasSG"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoReservasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglExpire"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoQueue"].ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Netto"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT USERNAME.Nama"
                       + " FROM ISC064_SECURITY..USERNAME"
                       + " WHERE UserID in (select UserID from MS_RESERVASI_LOG"
                       + " WHERE ket like"
                       + " '%No. Reservasi : " + rs.Rows[i]["NoReservasi"].ToString() + "%')"
                        ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rptA.Rows.Add(r);
            }
            //return 0;
        }

        private void fillBatal(DateTime Dari, DateTime Sampai)
        {
            string aa = "";
            if (UserAgent() > 0)
                aa = " AND b.NoAgent = " + UserAgent();

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND b.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND b.Pers = '" + Perusahaan + "'";

            string strSql = "SELECT a.*"
                + " FROM MS_KONTRAK_LOG a"
                + " INNER JOIN MS_KONTRAK b ON a.Pk = b.NoKontrak"
                + " WHERE a.Tgl >= '" + Dari + "'"
                + " AND a.Tgl <= '" + Sampai + "'"
                + " AND a.Aktivitas = 'BATAL'"
                + nProject
                + nPerusahaan
                + aa;
            ;
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Pk"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT b.Nama"
                    + " FROM MS_KONTRAK a"
                    + " INNER JOIN MS_CUSTOMER b"
                    + " ON a.NoCustomer = b.NoCustomer"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT b.Nama"
                    + " FROM MS_KONTRAK a"
                    + " INNER JOIN MS_AGENT b"
                    + " ON a.NoAgent = b.NoAgent"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT NoUnit"
                    + " FROM MS_KONTRAK"
                    + " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT b.LuasSG"
                    + " FROM MS_KONTRAK a"
                    + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Cf.Num(Db.SingleDecimal(strSql));
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                string[] strTemp = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
                string Reason = "";
                for (int j = 0; j < strTemp.Length; j++)
                {
                    if (strTemp[j].StartsWith("Alasan"))
                        Reason = strTemp[j].ToString().Replace("Alasan Pembatalan : ", "");
                }
                c = new TableCell();
                c.Text = Reason;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT NilaiKontrak"
                    + " FROM MS_KONTRAK"
                    + " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Convert.ToString(Cf.Num(Db.SingleDecimal(strSql)));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                string biaya = "";
                string[] strTemp1 = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
                for (int j = 0; j < strTemp1.Length; j++)
                {
                    if (strTemp1[j].StartsWith("Biaya"))
                        biaya = strTemp1[j].ToString().Replace("Biaya Administrasi : ", "");
                }
                c = new TableCell();
                c.Text = biaya;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT USERNAME.Nama"
                       + " FROM ISC064_SECURITY..USERNAME"
                       + " WHERE UserID in (select UserID from MS_KONTRAK_LOG"
                       + " WHERE Aktivitas = 'BATAL' AND "
                       + " Pk ='" + Cf.Pk(rs.Rows[i]["Pk"]) + "')"
                        ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rptB.Rows.Add(r);
            }
            //return 0;
        }

        private void fillGN(DateTime Dari, DateTime Sampai)
        {
            string aa = "";
            if (UserAgent() > 0)
                aa = " AND b.NoAgent = " + UserAgent();

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND b.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND b.Pers = '" + Perusahaan + "'";

            string strSql = "SELECT a.*"
                + " FROM MS_KONTRAK_LOG a"
                + " INNER JOIN MS_KONTRAK b ON a.Pk = b.NoKontrak"
                + " WHERE a.Tgl >= '" + Dari + "'"
                + " AND a.Tgl <= '" + Sampai + "'"
                + " AND a.Aktivitas = 'GN'"
                + nProject
                + nPerusahaan
                + aa
                ;
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Pk"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT NoUnit"
                    + " FROM MS_KONTRAK "
                    + " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT a.LuasSG"
                    + " FROM MS_UNIT a"
                    + " INNER JOIN MS_KONTRAK b ON a.NoStock = b.NoStock"
                    + " WHERE b.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Cf.Num(Db.SingleDecimal(strSql));
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT b.Nama"
                    + " FROM MS_KONTRAK a"
                    + " INNER JOIN MS_AGENT b"
                    + " ON a.NoAgent = b.NoAgent"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                //Cek nama sebelum dan sesudah Pengalihan Hak
                string strBef = "", strAft = "";
                string[] strTemp = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
                bool isNext = false;

                for (int j = 0; j < strTemp.Length; j++)
                {
                    if (!isNext)
                    {
                        if (strTemp[j].StartsWith("Nama Customer"))
                        {
                            strBef = strTemp[j].ToString().Replace("Nama Customer : ", "");
                            isNext = true;
                        }
                    }
                    else
                    {
                        if (strTemp[j].StartsWith("Nama Customer"))
                        {
                            strAft = strTemp[j].ToString().Replace("Nama Customer : ", "");
                            break;
                        }
                    }
                }
                c = new TableCell();
                c.Text = strBef;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT b.Nama"
                    + " FROM MS_KONTRAK a"
                    + " INNER JOIN MS_CUSTOMER b"
                    + " ON a.NoCustomer = b.NoCustomer"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT NilaiKontrak"
                    + " FROM MS_KONTRAK"
                    + " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Cf.Num(Db.SingleDecimal(strSql));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                string biaya = "";
                string[] strTemp1 = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
                for (int j = 0; j < strTemp1.Length; j++)
                {
                    if (strTemp1[j].StartsWith("Biaya"))
                        biaya = strTemp1[j].ToString().Replace("Biaya Administrasi : ", "");
                }
                c = new TableCell();
                c.Text = biaya;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT USERNAME.Nama"
                       + " FROM ISC064_SECURITY..USERNAME"
                       + " WHERE UserID in (select UserID from MS_KONTRAK_LOG"
                       + " WHERE Aktivitas = 'GN' AND "
                       + " Pk ='" + Cf.Pk(rs.Rows[i]["Pk"]) + "')"
                        ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rptD.Rows.Add(r);
            }
        }

        private void Fill()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            fillReserv(Dari, Sampai);
            fillBatal(Dari, Sampai);
            fillGN(Dari, Sampai);

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND b.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND b.Pers = '" + Perusahaan + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND b.NoAgent = " + UserAgent();

            string strSql = "SELECT a.*"
                + " FROM MS_KONTRAK_LOG a"
                + " INNER JOIN MS_KONTRAK b ON a.Pk = b.NoKontrak"
                + " WHERE a.Tgl >= '" + Dari + "'"
                + " AND a.Tgl <= '" + Sampai + "'"
                + " AND a.Aktivitas = 'GU'"
                + nProject
                + nPerusahaan
                + aa
                ;
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                string strBef = "", strAft = "", Reason = "";
                string[] strTemp = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
                bool isNext = false;

                for (int j = 0; j < strTemp.Length; j++)
                {
                    if (!isNext)
                    {
                        if (strTemp[j].StartsWith("Unit"))
                        {
                            strBef = strTemp[j].ToString().Replace("Unit : ", "");
                            isNext = true;
                        }
                    }
                    else
                    {
                        if (strTemp[j].StartsWith("Unit"))
                        {
                            strAft = strTemp[j].ToString().Replace("Unit : ", "");
                            break;
                        }
                    }
                }

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT *"
                    + " FROM MS_KONTRAK"
                    + " WHERE NoKontrak = '" + rs.Rows[i]["Pk"].ToString() + "'"
                    ;
                DataTable tglK = Db.Rs(strSql);
                c.Text = Cf.Day(tglK.Rows[0]["TglKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Pk"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT b.Nama"
                    + " FROM MS_KONTRAK a"
                    + " INNER JOIN MS_CUSTOMER b"
                    + " ON a.NoCustomer = b.NoCustomer"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT b.Nama"
                    + " FROM MS_KONTRAK a"
                    + " INNER JOIN MS_AGENT b"
                    + " ON a.NoAgent = b.NoAgent"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = strBef;
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT LuasSG"
                    + " FROM MS_UNIT"
                    + " WHERE NoUnit = '" + strBef + "'"
                    ;
                c.Text = Cf.Num(Db.SingleDecimal(strSql));
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = strAft;
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT LuasSG"
                    + " FROM MS_UNIT"
                    + " WHERE NoUnit = '" + strAft + "'"
                    ;
                c.Text = Cf.Num(Db.SingleDecimal(strSql));
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT NilaiKontrak"
                    + " FROM MS_KONTRAK"
                    + " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Cf.Num(Db.SingleDecimal(strSql));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                string biaya = "";
                string[] strTemp1 = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
                for (int j = 0; j < strTemp1.Length; j++)
                {
                    if (strTemp1[j].StartsWith("Biaya"))
                        biaya = strTemp1[j].ToString().Replace("Biaya Administrasi : ", "");
                }
                c = new TableCell();
                c.Text = biaya;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT USERNAME.Nama"
                       + " FROM ISC064_SECURITY..USERNAME"
                       + " WHERE UserID in (select UserID from MS_KONTRAK_LOG"
                       + " WHERE Aktivitas = 'GU' AND "
                       + " Pk ='" + Cf.Pk(rs.Rows[i]["Pk"]) + "')"
                        ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
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
