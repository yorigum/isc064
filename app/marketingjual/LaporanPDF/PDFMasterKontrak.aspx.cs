using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class MasterKontrak : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        //private string nAgent { get { return (Request.QueryString["agent"]); } }
        private string nLokasi { get { return (Request.QueryString["lokasi"]); } }
        private string BF1 { get { return (Request.QueryString["bf1"]); } }
        private string BF2 { get { return (Request.QueryString["bf2"]); } }
        private string BF3 { get { return (Request.QueryString["bf3"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string StatusLunas { get { return (Request.QueryString["statuspelunasan"]); } }
        private string nCaraBayar { get { return (Request.QueryString["carabayar"]); } }
        private string Tipe { get { return (Request.QueryString["tipe"]); } }

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
            rpt.Visible = true;

            Header();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            if (StatusA != "")
                Rpt.SubJudul(x, "Status : " + StatusA);
            else if (StatusB != "")
                Rpt.SubJudul(x, "Status : " + StatusB);
            else
                Rpt.SubJudul(x, "Status : " + StatusS);

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            Rpt.SubJudul(x
                , "Tanggal Kontrak" + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            Rpt.SubJudul(x
                , "Jenis : " + Tipe.Replace("-", ",").Replace("%", " ").TrimEnd(',')
                );

            Rpt.SubJudul(x
                , "Lokasi : " + nLokasi
                );

            //Rpt.SubJudul(x
            //    , "Principal : " + nAgent
            //    );
            Rpt.SubJudul(x
                , "Project : " + Project
                );
            Rpt.SubJudul(x
                , "Perusahaan : " + Perusahaan
                );

            //Rpt.Header(rpt, x);
            string legend = "Status: A = Aktif / B = Batal." + "<br>"
                        + "Luas dalam meter persegi. Gross adalah harga sebelum diskon.";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            string nStatus = "";
            if (StatusA != "") nStatus = " AND MS_KONTRAK.Status = 'A'";
            else if (StatusB != "") nStatus = " AND MS_KONTRAK.Status = 'B'";

            string Lunas = "";
            if (StatusLunas == "SEMUA") Lunas = "";
            else if (StatusLunas == "statusL0") Lunas = "AND MS_KONTRAK.PersenLunas = '0'";
            else if (StatusLunas == "statusL") Lunas = " AND MS_KONTRAK.PersenLunas > '0'";
            else if (StatusLunas == "statusL1") Lunas = " AND MS_KONTRAK.PersenLunas >= '20'";
            if (StatusLunas == "statusL2") Lunas = " AND MS_KONTRAK.PersenLunas >= '100'";

            string Bf = "";
            if (BF1 != "") Bf = " AND (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) < 10000000";
            if (BF2 != "") Bf = " AND (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) >= 10000000";

            string tgl = "";
            tgl = "TglKontrak";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string Lokasi = "";
            if (nLokasi != "SEMUA")
            {
                Lokasi = " AND Lokasi = '" + Cf.Str(nLokasi) + "'";
            }

            //string Agent = "";
            //if (nAgent != "SEMUA")
            //{
            //    Agent = " AND Principal = '" + Cf.Str(nAgent) + "'";
            //}

            string aa = "";
            if (UserAgent() > 0)
            {
                aa = " AND MS_KONTRAK.NoAgent = " + UserAgent();
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND MS_KONTRAK.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND MS_KONTRAK.Pers = '" + Perusahaan + "'";

            //change parameter carabayar
            string cb = String.Empty;
            cb = nCaraBayar.Replace("-", ",").TrimEnd(',');
            cb = cb.Replace("+", " ");
            cb = cb.Replace(",", "','");
            cb = "'" + cb + "'";


            string carabayar = "";

            if (cb != "")
                carabayar = " AND MS_KONTRAK.CaraBayar IN (" + cb + ")";


            //change parameter tipe
            string akt = String.Empty;
            akt = Tipe.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("%", " ");
            akt = akt.Replace(",", "','");
            akt = "'" + akt + "'";


            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;
            decimal t6 = 0;
            decimal tBunga = 0;
            decimal t7 = 0, t8 = 0;

            decimal tDiskonTambahan = 0;
            decimal tHargaGimmick = 0;
            decimal tHargaLainLain = 0;

            string strSql = "SELECT "
                + " NoKontrak"
                + ",TglKontrak"
                + ",MS_KONTRAK.TglInput"
                + ",Jenis"
                + ",Lokasi"
                + ",NoUnit"
                + ",NUP"
                + ", Skema"
                + ",MS_CUSTOMER.Nama AS Cs"
                + ",MS_AGENT.Nama AS Ag"
                + ",MS_AGENT.Principal"
                + ",Luas"
                + ",Gross"
                + ",DiskonRupiah"
                + ",BungaNominal"
                + ",NilaiKontrak"
                + ",MS_KONTRAK.Status AS Status"
                + ",NoST"
                + ",PersenLunas"
                + ",TglST"
                + ",TglPPJB"
                + ",NoPPJB"
                + ",TglAJB"
                + ",NoAJB"
                + ",DiskonTambahan "
                + ",HargaGimmick "
                + ",HargaLainLain "
                + ",MS_KONTRAK.NoVA as nono"
                + ",MS_KONTRAK.Project"
                + ", CaraBayar "
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) AS NilaiTTS"
                + ", JenisPPN"
                + ",NoStock"
                + " FROM MS_KONTRAK"
                + " INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent "
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND Jenis IN (" + akt + ")"
                + nProject
                + nPerusahaan
                + Lokasi
                + Lunas
                 //+ " AND MS_KONTRAK.PersenLunas >= '20'"
                 + nStatus
                 //+ Agent
                 + Bf
                 + carabayar
                 + aa
                + " ORDER BY " + "NoKontrakManual";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";

                c = new TableCell();
                c.Text = Cf.Num(i + 1);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglInput"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Lokasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NUP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                decimal LuasSG = Db.SingleDecimal("SELECT LuasSG FROM MS_UNIT WHERE NoStock = '" + rs.Rows[i]["NoStock"] + "'");
                c = new TableCell();
                c.Text = Cf.Num(LuasSG);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Skema"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["CaraBayar"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["nono"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Gross"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["DiskonRupiah"]);//Cf.Num(Disc);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["BungaNominal"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["DiskonTambahan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);


                decimal adm = Db.SingleDecimal("SELECT ISNULL(SUM(NILAITAGIHAN),0) FROM MS_TAGIHAN WHERE TIPE='ADM' AND NOKONTRAK='" + rs.Rows[i]["NoKontrak"] + "'");

                c = new TableCell();
                c.Text = Cf.Num(adm);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal fo = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' AND NamaTagihan LIKE '%FITTING OUT%'");
                c.Text = Cf.Num(fo);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiTTS"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal Sisa = (Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]) + adm) - Convert.ToDecimal(rs.Rows[i]["NilaiTTS"]);
                c.Text = Cf.Num(Sisa);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["PersenLunas"].ToString()) + " %";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Ag"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Principal"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglPPJB"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoPPJB"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglST"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoST"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglAJB"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoAJB"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 = t1 + LuasSG;
                t2 = t2 + (decimal)rs.Rows[i]["Gross"];
                t3 += (decimal)rs.Rows[i]["DiskonRupiah"];
                t4 = t4 + (decimal)rs.Rows[i]["NilaiKontrak"];
                t5 = t5 + (decimal)rs.Rows[i]["NilaiTTS"];
                t6 = t6 + Sisa;
                t7 += fo;
                t8 = t8 + adm;
                tBunga += (decimal)rs.Rows[i]["BungaNominal"];

                tDiskonTambahan += (decimal)rs.Rows[i]["DiskonTambahan"];
                tHargaGimmick += (decimal)rs.Rows[i]["HargaGimmick"];
                tHargaLainLain += (decimal)rs.Rows[i]["HargaLainLain"];

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", t1, t2, t3, t4, t5, t6, t7, tBunga, tDiskonTambahan, tHargaGimmick, tHargaLainLain, t8);
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, decimal tBunga, decimal tDiskonTambahan, decimal tHargaGimmick, decimal tHargaLainLain, decimal t8)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 10;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(tBunga);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(tDiskonTambahan);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t8);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t7);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t6);
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

    }
}
