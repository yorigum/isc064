using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class LaporanPenjualan2 : System.Web.UI.Page
    {
        private string NoCustomer { get { return (Request.QueryString["NoCustomer"]); } }
        private string TipePro { get { return (Request.QueryString["tipepro"]); } }
        private string Tipe { get { return (Request.QueryString["tipe"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Input { get { return Request.QueryString["input"]; } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string TitipJual { get { return (Request.QueryString["titipjual"]); } }
        private string Agent { get { return (Request.QueryString["agent"]); } }
        private string UserID { get { return (Request.QueryString["userid"]); } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");

            return a;
        }

        private Table fillAll(DateTime Dari, DateTime Sampai, string Tipe, string strAdd)
        {

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND a.Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string nTitipJual = "";
            if (TitipJual != "SEMUA")
                nTitipJual = " AND TitipJual=" + TitipJual.ToString();

            string nTipePro = "";
            if (TipePro != "SEMUA")
            {
                nTipePro = " AND JenisProperti='" + TipePro + "'";
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",","','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers='" + Perusahaan + "'";

            //change parameter tipe
            string akt = String.Empty;
            akt = Tipe.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("%", " ");
            akt = akt.Replace(",", "','");
            akt = "'" + akt + "'";


            //query tabel all summary
            string strSql1 = "SELECT a.*, b.Nama AS Customer, c.Nama AS Agent, c.Principal, d.Jenis, d.LuasSG, d.JenisProperti, d.ArahHadap"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
                + " INNER JOIN MS_UNIT d ON a.NoStock = d.NoStock"
                + " WHERE a.TglKontrak >= '" + Convert.ToDateTime(Dari) + "'"
                + " AND a.TglKontrak <= '" + Convert.ToDateTime(Sampai) + "'"
                + " AND a.JENIS IN (" + akt + ")"
                + nProject
                + nPerusahaan
                + nLokasi
                //+ Tipe
                + strAdd
                + nTitipJual
                + nTipePro
                + " ORDER BY TglKontrak, NoKontrak"
                ;
            DataTable rs1 = Db.Rs(strSql1);

            sA.Text = " <h3> ( " + rs1.Rows.Count.ToString() + " ) </h3>";

            decimal jumPlist = 0;
            decimal jumDisc = 0;
            decimal jumBunga = 0;
            decimal jumNilaiKontrakA = 0;
            decimal jumDPP = 0;
            decimal jumPPN = 0;
            decimal jumLuasSG = 0;

            decimal jumDiscTambahan = 0;
            decimal jumHargaGimmick = 0;
            decimal jumHargaLainLain = 0;

            for (int i = 0; i < rs1.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r1 = new TableRow();
                r1.Attributes["ondblclick"] = "popEditKontrak('" + rs1.Rows[i]["NoKontrak"] + "')";

                TableCell c1;
                // tabel summary
                c1 = new TableCell();
                c1.Text = (i + 1).ToString();
                c1.HorizontalAlign = HorizontalAlign.Center;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Day(rs1.Rows[i]["TglKontrak"]);
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["NoKontrak"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Customer"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Agent"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs1.Rows[i]["Project"].ToString() + "'");
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["NoUnit"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Jenis"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["JenisProperti"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Num(rs1.Rows[i]["LuasSG"]);
                jumLuasSG += Convert.ToDecimal(rs1.Rows[i]["LuasSG"]);
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);
                //
                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["ArahHadap"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Num(rs1.Rows[i]["Gross"]);
                jumPlist += Convert.ToDecimal(rs1.Rows[i]["Gross"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["DiskonRupiah"]);//Cf.Num(Disc1);
                jumDisc += Convert.ToDecimal(rs1.Rows[i]["DiskonRupiah"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["DiskonTambahan"]);//Cf.Num(Disc1);
                jumDiscTambahan += Convert.ToDecimal(rs1.Rows[i]["DiskonTambahan"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["HargaGimmick"]);//Cf.Num(Disc1);
                jumHargaGimmick += Convert.ToDecimal(rs1.Rows[i]["HargaGimmick"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["HargaLainLain"]);//Cf.Num(Disc1);
                jumHargaLainLain += Convert.ToDecimal(rs1.Rows[i]["HargaLainLain"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                // decimal NominalBunga = Db.SingleDecimal("SELECT BungaNominal FROM MS_KONTRAK WHERE NoKontrak = '"++"' ");
                c1.Text = Cf.Num(rs1.Rows[i]["BungaNominal"]);//Cf.Num(Disc1);
                jumBunga += Convert.ToDecimal(rs1.Rows[i]["BungaNominal"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                decimal NilaiKontrak1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]);
                c1.Text = Cf.Num(NilaiKontrak1);
                jumNilaiKontrakA += NilaiKontrak1;
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                //hitung DPP + PPN
                decimal bn = 0;
                if (!rs1.Rows[i]["BungaNominal"].Equals(System.DBNull.Value))
                    bn = Convert.ToDecimal(rs1.Rows[i]["BungaNominal"]);
                decimal DPP1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]);
                DPP1 = Math.Round(DPP1 / (decimal)1.1);
                c1 = new TableCell();
                if (!Convert.ToBoolean(rs1.Rows[i]["PPN"]))
                {
                    DPP1 = Convert.ToDecimal(rs1.Rows[i]["Gross"]) - Convert.ToDecimal(rs1.Rows[i]["DiskonRupiah"]) + bn;
                }
                if (rs1.Rows[0]["JenisPPN"].ToString() == "PEMERINTAH")
                {
                    DPP1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]);
                }

                DPP1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]) - Convert.ToDecimal(rs1.Rows[i]["NilaiPPN"]);
                c1.Text = Cf.Num(DPP1);//Cf.Num(TotalBayar);
                jumDPP += DPP1;
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                decimal PPN1 = 0;
                //				PPN1 = Math.Round(DPP1 * (decimal)0.1);
                PPN1 = Convert.ToDecimal(rs1.Rows[i]["NilaiPPN"]);
                jumPPN += PPN1;
                c1.Text = Cf.Num(PPN1);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Skema"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);


                rptA.Rows.Add(r1);

                if (i == rs1.Rows.Count - 1)
                {
                    TableRow r4 = new TableRow();

                    TableCell c4;

                    c4 = new TableCell();
                    c4.Text = "<strong>TOTAL</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Left;
                    c4.ColumnSpan = 6;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + rs1.Rows.Count + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    c4.ColumnSpan = 2;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>&nbsp;</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumLuasSG) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black; text-align:left;";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    c4.ColumnSpan = 2;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumPlist) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumDisc) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumDiscTambahan) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumHargaGimmick) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumHargaLainLain) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumBunga) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumNilaiKontrakA) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumDPP) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumPPN) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "";
                    r4.Cells.Add(c4);

                    rptA.Rows.Add(r4);
                }
            }

            sumall.Text = Convert.ToString(rs1.Rows.Count);

            TableRow r0 = new TableRow();
            rptA.Rows.Add(r0);

            return rptA;
        }

        private Table fillBatal(DateTime Dari, DateTime Sampai, string Tipe, string strAdd)
        {

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND a.Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string nTitipJual = "";
            if (TitipJual != "SEMUA")
                nTitipJual = " AND TitipJual=" + TitipJual.ToString();

            string nTipePro = "";
            if (TipePro != "SEMUA")
            {
                nTipePro = " AND JenisProperti='" + TipePro + "'";
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers='" + Perusahaan + "'";

            //change parameter tipe
            string akt = String.Empty;
            akt = Tipe.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("%", " ");
            akt = akt.Replace(",", "','");
            akt = akt.Replace("%", " ");
            akt = "'" + akt + "'";


            //query tabel batal
            string strSql2 = "SELECT a.*, b.Nama AS Customer, c.Nama AS Agent, c.Principal, d.Jenis, d.LuasSG, d.JenisProperti"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
                + " INNER JOIN MS_UNIT d ON a.NoStock = d.NoStock"
                + " WHERE a.TglBatal >= '" + Convert.ToDateTime(Dari) + "'"
                + " AND a.TglBatal <= '" + Convert.ToDateTime(Sampai) + "'"
                + " AND a.Status = 'B'"
                + " AND a.JENIS IN (" + akt + ")"
                + nProject
                + nPerusahaan
                + nLokasi
                + strAdd
                + nTitipJual
                + nTipePro
                + " AND ( a.TglBatal is not null )" //OR a.TglBatal '"+ Convert.ToDateTime(Sampai) +"') "//Status
                + " ORDER BY TglKontrak, NoKontrak"
                ;
            DataTable rs2 = Db.Rs(strSql2);

            sumB.Text = " <h3> ( " + rs2.Rows.Count.ToString() + " ) </h3>";

            decimal jumNilaiKontrakB = 0;
            decimal jumAdmB = 0;
            decimal jumPembayaran = 0;
            decimal jumKembali = 0;
            decimal jumLuasSG = 0;
            //
            decimal jumDiskonRupiahB = 0;
            decimal jumDiskonTambahanB = 0;
            decimal jumHargaGimmickB = 0;
            decimal jumHargaLainLainB = 0;
            decimal jumNominalBungaB = 0;

            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r2 = new TableRow();
                r2.Attributes["ondblclick"] = "popEditKontrak('" + rs2.Rows[i]["NoKontrak"] + "')";

                TableCell c2;
                // tabel batal
                c2 = new TableCell();
                c2.Text = (i + 1).ToString();
                c2.HorizontalAlign = HorizontalAlign.Center;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Day(rs2.Rows[i]["TglKontrak"]);
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = rs2.Rows[i]["NoKontrak"].ToString();
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = rs2.Rows[i]["Customer"].ToString();
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = rs2.Rows[i]["Agent"].ToString();
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs2.Rows[i]["Project"].ToString() + "'");
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = rs2.Rows[i]["NoUnit"].ToString();
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = rs2.Rows[i]["JenisProperti"].ToString();
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(rs2.Rows[i]["LuasSG"]);
                jumLuasSG += Convert.ToDecimal(rs2.Rows[i]["LuasSG"]);
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Day(rs2.Rows[i]["TglBatal"]);
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[i]["DiskonRupiah"]));
                jumDiskonRupiahB += Convert.ToDecimal(rs2.Rows[i]["DiskonRupiah"]);
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[i]["DiskonTambahan"]));
                jumDiskonTambahanB += Convert.ToDecimal(rs2.Rows[i]["DiskonTambahan"]);
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[i]["HargaGimmick"]));
                jumHargaGimmickB += Convert.ToDecimal(rs2.Rows[i]["HargaGimmick"]);
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[i]["HargaLainLain"]));
                jumHargaLainLainB += Convert.ToDecimal(rs2.Rows[i]["HargaLainLain"]);
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[i]["BungaNominal"]));
                jumNominalBungaB += Convert.ToDecimal(rs2.Rows[i]["BungaNominal"]);
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                decimal NilaiKontrak2 = Convert.ToDecimal(rs2.Rows[i]["NilaiKontrak"]);
                c2.Text = Cf.Num(NilaiKontrak2);
                jumNilaiKontrakB += NilaiKontrak2;
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                // nilai pembatalan
                decimal nBatal = Db.SingleDecimal("SELECT NilaiTagihan FROM MS_TAGIHAN WHERE NamaTagihan = 'BIAYA ADM. PEMBATALAN' AND NoKontrak='" + rs2.Rows[i]["NoKontrak"] + "'");
                c2 = new TableCell();
                c2.Text = Cf.Num(nBatal);
                jumAdmB += nBatal;
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                //jumlah pembayaran
                c2 = new TableCell();
                decimal NilaiPembayaran = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) AS TotalPembayaran FROM MS_PELUNASAN WHERE NoKontrak = '" + rs2.Rows[i]["NoKontrak"] + "'");
                c2.Text = Cf.Num(NilaiPembayaran);
                jumPembayaran += NilaiPembayaran;
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(rs2.Rows[i]["NilaiPulang"]);//Cf.Num(TotalBayar);
                jumKembali += Convert.ToDecimal(rs2.Rows[i]["NilaiPulang"]);
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                rptB.Rows.Add(r2);

                if (i == rs2.Rows.Count - 1)
                {
                    TableRow r3 = new TableRow();

                    TableCell c3;

                    c3 = new TableCell();
                    c3.Text = "<strong>TOTAL</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Left;
                    c3.ColumnSpan = 6;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + rs2.Rows.Count + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Center;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.Text = "&nbsp;";
                    c3.HorizontalAlign = HorizontalAlign.Center;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumLuasSG) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Center;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.Text = "&nbsp;";
                    c3.HorizontalAlign = HorizontalAlign.Center;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumDiskonRupiahB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumDiskonTambahanB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumHargaGimmickB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumHargaLainLainB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumNominalBungaB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);
                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumNilaiKontrakB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumAdmB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumPembayaran.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumKembali.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    rptB.Rows.Add(r3);
                }
            }

            sumbatal.Text = Convert.ToString(rs2.Rows.Count);
            TableRow r0 = new TableRow();
            rptB.Rows.Add(r0);

            return rptB;
        }

        private Table fillTitipJual(DateTime Dari, DateTime Sampai, string Tipe, string strAdd)
        {

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND a.Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string nTitipJual = "";
            if (TitipJual != "SEMUA")
            {
                nTitipJual = " AND TitipJual=" + TitipJual.ToString();

            }
            else
            {
                nTitipJual = " AND TitipJual=1";
            }

            string nTipePro = "";
            if (TipePro != "SEMUA")
            {
                nTipePro = " AND JenisProperti='" + TipePro + "'";
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers='" + Perusahaan + "'";

            //change parameter tipe
            string akt = String.Empty;
            akt = Tipe.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("%", " ");
            akt = akt.Replace(",", "','");
            akt = akt.Replace("%", " ");
            akt = "'" + akt + "'";


            //query tabel all summary
            string strSql1 = "SELECT a.*, b.Nama AS Customer, c.Nama AS Agent, c.Principal, d.Jenis, d.LuasSG, d.JenisProperti"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
                + " INNER JOIN MS_UNIT d ON a.NoStock = d.NoStock"
                + " WHERE a.TglKontrak >= '" + Convert.ToDateTime(Dari) + "'"
                + " AND a.TglKontrak <= '" + Convert.ToDateTime(Sampai) + "'"
                + " AND a.JENIS IN (" + akt + ")"
                + nProject
                + nPerusahaan
                + nLokasi
                + strAdd
                + nTitipJual
                + nTipePro
                + " ORDER BY TglKontrak, NoKontrak"
                ;
            DataTable rs1 = Db.Rs(strSql1);

            sumD.Text = " <h3> ( " + rs1.Rows.Count.ToString() + " ) </h3>";

            decimal jumPlist = 0;
            decimal jumDisc = 0;
            decimal jumBunga = 0;
            decimal jumNilaiKontrakA = 0;
            decimal jumDPP = 0;
            decimal jumPPN = 0;
            decimal jumLuasSG = 0;

            decimal jumDiscTambahan = 0;
            decimal jumHargaGimmick = 0;
            decimal jumHargaLainLain = 0;

            for (int i = 0; i < rs1.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r1 = new TableRow();
                r1.Attributes["ondblclick"] = "popEditKontrak('" + rs1.Rows[i]["NoKontrak"] + "')";

                TableCell c1;
                // tabel summary
                c1 = new TableCell();
                c1.Text = (i + 1).ToString();
                c1.HorizontalAlign = HorizontalAlign.Center;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Day(rs1.Rows[i]["TglKontrak"]);
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["NoKontrak"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Customer"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Agent"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs1.Rows[i]["Project"].ToString() + "'");
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["NoUnit"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Jenis"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["JenisProperti"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Num(rs1.Rows[i]["LuasSG"]);
                jumLuasSG += Convert.ToDecimal(rs1.Rows[i]["LuasSG"]);
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Num(rs1.Rows[i]["Gross"]);
                jumPlist += Convert.ToDecimal(rs1.Rows[i]["Gross"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["DiskonRupiah"]);//Cf.Num(Disc1);
                jumDisc += Convert.ToDecimal(rs1.Rows[i]["DiskonRupiah"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["DiskonTambahan"]);//Cf.Num(Disc1);
                jumDiscTambahan += Convert.ToDecimal(rs1.Rows[i]["DiskonTambahan"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["HargaGimmick"]);//Cf.Num(Disc1);
                jumHargaGimmick += Convert.ToDecimal(rs1.Rows[i]["HargaGimmick"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["HargaLainLain"]);//Cf.Num(Disc1);
                jumHargaLainLain += Convert.ToDecimal(rs1.Rows[i]["HargaLainLain"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                // decimal NominalBunga = Db.SingleDecimal("SELECT BungaNominal FROM MS_KONTRAK WHERE NoKontrak = '"++"' ");
                c1.Text = Cf.Num(rs1.Rows[i]["BungaNominal"]);//Cf.Num(Disc1);
                jumBunga += Convert.ToDecimal(rs1.Rows[i]["BungaNominal"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                decimal NilaiKontrak1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]);
                c1.Text = Cf.Num(NilaiKontrak1);
                jumNilaiKontrakA += NilaiKontrak1;
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                //hitung DPP + PPN
                decimal bn = 0;
                if (!rs1.Rows[i]["BungaNominal"].Equals(System.DBNull.Value))
                    bn = Convert.ToDecimal(rs1.Rows[i]["BungaNominal"]);
                decimal DPP1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]);
                DPP1 = Math.Round(DPP1 / (decimal)1.1);
                c1 = new TableCell();
                if (!Convert.ToBoolean(rs1.Rows[i]["PPN"]))
                {
                    DPP1 = Convert.ToDecimal(rs1.Rows[i]["Gross"]) - Convert.ToDecimal(rs1.Rows[i]["DiskonRupiah"]) + bn;
                }
                if (rs1.Rows[0]["JenisPPN"].ToString() == "PEMERINTAH")
                {
                    DPP1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]);
                }

                DPP1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]) - Convert.ToDecimal(rs1.Rows[i]["NilaiPPN"]);
                c1.Text = Cf.Num(DPP1);//Cf.Num(TotalBayar);
                jumDPP += DPP1;
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                decimal PPN1 = 0;
                //				PPN1 = Math.Round(DPP1 * (decimal)0.1);
                PPN1 = Convert.ToDecimal(rs1.Rows[i]["NilaiPPN"]);
                jumPPN += PPN1;
                c1.Text = Cf.Num(PPN1);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);


                rptD.Rows.Add(r1);

                if (i == rs1.Rows.Count - 1)
                {
                    TableRow r4 = new TableRow();

                    TableCell c4;

                    c4 = new TableCell();
                    c4.Text = "<strong>TOTAL</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Left;
                    c4.ColumnSpan = 6;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + rs1.Rows.Count + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>&nbsp;</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    c4.ColumnSpan = 2;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumLuasSG) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumPlist) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumDisc) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumDiscTambahan) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumHargaGimmick) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumHargaLainLain) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumBunga) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumNilaiKontrakA) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumDPP) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumPPN) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    rptD.Rows.Add(r4);
                }
            }

            sumD.Text = Convert.ToString(rs1.Rows.Count);

            TableRow r0 = new TableRow();
            rptD.Rows.Add(r0);

            return rptD;
        }

        private void Fill()
        {

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND a.Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

           
            string strAdd = "";

            if (Agent != "SEMUA")
                strAdd += " AND a.NoAgent = " + Agent;
            else
            {
                if (UserAgent() > 0)
                    strAdd += " AND a.NoAgent = " + UserAgent();
            }


            fillAll(Dari, Sampai, Tipe, strAdd);
            fillBatal(Dari, Sampai, Tipe, strAdd);

            string nTitipJual = "";
            if (TitipJual != "SEMUA")
            {
                nTitipJual = " AND TitipJual=" + TitipJual.ToString();
                fillTitipJual(Dari, Sampai, Tipe, strAdd);

            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",","','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers='" + Perusahaan + "'";

            //change parameter tipe
            string akt = String.Empty;
            akt = Tipe.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("%", " ");
            akt = akt.Replace(",", "','");
            akt = akt.Replace("%", " ");
            akt = "'" + akt + "'";


            string nTipePro = "";
            if (TipePro != "SEMUA")
            {
                nTipePro = " AND JenisProperti='" + TipePro + "'";
            }

            string strSql = "SELECT a.*, b.Nama AS Customer, c.Nama AS Agent, c.Principal, d.Jenis, d.LuasSG, d.JenisProperti"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
                + " INNER JOIN MS_UNIT d ON a.NoStock = d.NoStock"
                + " WHERE a.TglKontrak >= '" + Convert.ToDateTime(Dari) + "'"
                + " AND a.TglKontrak <= '" + Convert.ToDateTime(Sampai) + "'"
                + " AND a.STATUS='A'"
                + " AND a.JENIS IN (" + akt + ")"
                + nProject
                + nPerusahaan
                + nLokasi
                + strAdd
                + nTitipJual
                + nTipePro
                + " ORDER BY TglKontrak, NoKontrak"
                ;
            DataTable rs = Db.Rs(strSql);
            
            sumC.Text = " <h3> ( " + rs.Rows.Count.ToString() + " ) </h3>";

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, tBunga = 0, tDiskonTambahan = 0, tHargaGimmick = 0, tHargaLainLain = 0, tLuasSG = 0;
            decimal gt1 = 0, gt2 = 0, gt3 = 0, gt4 = 0, gt5 = 0, gt6 = 0, gt7 = 0, gtBunga = 0, gtDiskonTambahan = 0, gtHargaGimmick = 0, gtHargaLainLain = 0, gtLuasSG = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";
                TableCell c;

                t1 += 1;
                gt1 += 1;

                //tabel Netto
                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Customer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Agent"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["JenisProperti"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LuasSG"]);
                tLuasSG += Convert.ToDecimal(rs.Rows[i]["LuasSG"]);
                gtLuasSG += Convert.ToDecimal(rs.Rows[i]["LuasSG"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["CaraBayar"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Gross"]);
                t2 += Convert.ToDecimal(rs.Rows[i]["Gross"]);
                gt2 += Convert.ToDecimal(rs.Rows[i]["Gross"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal Disc = 0, AfterDisc = 0;
                if (rs.Rows[i]["JenisPPN"].ToString() == "KONSUMEN")
                    Disc = ((decimal)1.1 * Convert.ToDecimal(rs.Rows[i]["Gross"]) - Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"])) / (decimal)1.1;
                else
                    Disc = Convert.ToDecimal(rs.Rows[i]["Gross"]) - Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                AfterDisc = Convert.ToDecimal(rs.Rows[i]["Gross"]) - Disc;
                c.Text = Cf.Num(rs.Rows[i]["DiskonRupiah"]);//Cf.Num(Disc);
                t3 += Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]);//Disc;
                gt3 += Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]);//Disc;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["DiskonTambahan"]);
                tDiskonTambahan += Convert.ToDecimal(rs.Rows[i]["DiskonTambahan"]);
                gtDiskonTambahan += Convert.ToDecimal(rs.Rows[i]["DiskonTambahan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["HargaGimmick"]);
                tHargaGimmick += Convert.ToDecimal(rs.Rows[i]["HargaGimmick"]);
                gtHargaGimmick += Convert.ToDecimal(rs.Rows[i]["HargaGimmick"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["HargaLainLain"]);
                tHargaLainLain += Convert.ToDecimal(rs.Rows[i]["HargaLainLain"]);
                gtHargaLainLain += Convert.ToDecimal(rs.Rows[i]["HargaLainLain"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["BungaNominal"]);
                tBunga += Convert.ToDecimal(rs.Rows[i]["BungaNominal"]);
                gtBunga += Convert.ToDecimal(rs.Rows[i]["BungaNominal"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal NilaiKontrak = Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                c.Text = Cf.Num(NilaiKontrak);
                t5 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                gt5 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                //hitung DPP + PPN
                decimal DPP = Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                DPP = Math.Round(DPP / (decimal)1.1);

                c = new TableCell();
                decimal bn = 0;
                if (!rs.Rows[i]["BungaNominal"].Equals(System.DBNull.Value))
                    bn = Convert.ToDecimal(rs.Rows[i]["BungaNominal"]);

                decimal TotalBayar = Db.SingleDecimal(
                    "SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM MS_PELUNASAN"
                    + " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'");
                if (!Convert.ToBoolean(rs.Rows[i]["PPN"]))
                {
                    DPP = Convert.ToDecimal(rs.Rows[i]["Gross"]) - Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]) + bn;
                }
                if (rs.Rows[i]["JenisPPN"].ToString() == "PEMERINTAH")
                {
                    DPP = Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                }
                DPP = Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]);
                c.Text = Cf.Num(DPP);//Cf.Num(TotalBayar);
                t6 += DPP;//TotalBayar;
                gt6 += DPP;//TotalBayar;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal PPN = 0;
                PPN = Convert.ToDecimal(rs.Rows[i]["NilaiPPN"]);
                t4 += PPN;
                gt4 += PPN;
                c.Text = Cf.Num(PPN);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                rptC.Rows.Add(r);

                bool x = false;

                if (i < (rs.Rows.Count - 1))
                {
                    if (Convert.ToDateTime(rs.Rows[i + 1]["TglKontrak"]) > Convert.ToDateTime(rs.Rows[i]["TglKontrak"]))
                        x = true;
                }
                else if (i == (rs.Rows.Count - 1))
                    x = true;

                //				if(x)
                //				{
                //					SubTotal(t1, t2, t3, t4, t5, t6, t7, Convert.ToDateTime(rs.Rows[i]["TglKontrak"]));
                //					t1 = t2 = t3 = t4 = t5 = t6 = t7 = 0;
                //				}
                bool y = false;
                if (i == (rs.Rows.Count - 1))
                    y = true;

                sumnetto.Text = Convert.ToString(rs.Rows.Count);

                if (y)
                {
                    GrandTotal(gt1, gt2, gt3, gt4, gt5, gt6, gt7, gtBunga, gtDiskonTambahan, gtHargaGimmick, gtHargaLainLain, gtLuasSG, Convert.ToDateTime(rs.Rows[i]["TglKontrak"]));
                }

            }
            //grafik(Dari, Sampai, Principal, Tipe, strAdd);
        }

        private void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, DateTime TglKontrak)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "SUBTOTAL UNIT PENJUALAN " + Cf.Day(TglKontrak);
            c.ColumnSpan = 6;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = t1.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
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
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t6);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            //			c = Rpt.Foot();
            //			c.Text = Cf.Num(t7);
            //			c.HorizontalAlign = HorizontalAlign.Right;
            //			r.Cells.Add(c);
            //
            //			c = Rpt.Foot();
            //			c.Text = "&nbsp;";
            //			r.Cells.Add(c);

            rptC.Rows.Add(r);
        }

        private void GrandTotal(decimal gt1, decimal gt2, decimal gt3, decimal gt4, decimal gt5, decimal gt6, decimal gt7, decimal gtBunga, decimal gtDiskonTambahan, decimal gtHargaGimmick, decimal gtHargaLainLain, decimal gtLuasSG, DateTime TglKontrak)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "TOTAL ";
            c.ColumnSpan = 5;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = gt1.ToString();
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gtLuasSG);
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gt2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gt3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gtDiskonTambahan);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gtHargaGimmick);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gtHargaLainLain);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gtBunga);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gt5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gt6);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gt4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rptC.Rows.Add(r);
        }

        private void grafik(string dari, string sampai)
        {
            string strAdd = "";

            if (Agent != "SEMUA")
                strAdd += " AND NoAgent = " + Agent;
            else
            {
                if (UserAgent() > 0)
                    strAdd += " AND NoAgent = " + UserAgent();
            }

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND a.Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string nTitipJual = "";
            if (TitipJual != "SEMUA")
                nTitipJual = " AND a.TitipJual=" + TitipJual.ToString();

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN('" + Project.Replace(",","','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers='" + Perusahaan + "'";

            //change parameter tipe
            string akt = String.Empty;
            akt = Tipe.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("%", " ");
            akt = akt.Replace(",", "','");
            akt = akt.Replace("%", " ");
            akt = "'" + akt + "'";


            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");



            string nTipePro = "";
            if (TipePro != "SEMUA")
            {
                nTipePro = " AND JenisProperti='" + TipePro + "'";
            }

            //penjualan perbulan
            TableRow r = new TableRow();
            TableRow r1 = new TableRow();

            int counting = 0;

            //hitung jumlah bulan
            string dbulan = Convert.ToString(Convert.ToDateTime(dari).Month);
            string sbulan = Convert.ToString(Convert.ToDateTime(sampai).Month);
            string dtahun = Convert.ToString(Convert.ToDateTime(dari).Year);
            string stahun = Convert.ToString(Convert.ToDateTime(sampai).Year);

            int y = 0;
            int jumlahBulan = 0;
            if ((Convert.ToInt32(stahun) - Convert.ToInt32(dtahun)) >= 1)
            {
                y = (int)12 * ((Convert.ToInt32(stahun) - Convert.ToInt32(dtahun)) - (int)1);
                jumlahBulan = (12 - Convert.ToInt32(Convert.ToDateTime(dari).Month)) + y + Convert.ToInt32(Convert.ToDateTime(sampai).Month);
            }
            else
            {
                jumlahBulan = Convert.ToInt32(Convert.ToDateTime(sampai).Month) - Convert.ToInt32(Convert.ToDateTime(dari).Month);
            }

            int batasulang = Convert.ToInt32(Convert.ToDateTime(dari).Month) + jumlahBulan;
            int bl = Convert.ToInt32(Convert.ToDateTime(dari).Month);
            int thn = Convert.ToInt32(Convert.ToDateTime(dari).Year);

            for (int i = Convert.ToInt32(Convert.ToDateTime(dari).Month); i <= batasulang; i++)
            {
                TableCell c;

                if (bl > 12)
                {
                    thn++;
                    bl = 1;
                }
                string sampaitgl = bl
                                + "/"
                                + DateTime.DaysInMonth(thn, bl)
                                + "/"
                                + thn;
                //Response.Write(sampaitgl + "<br />");
                int d = Convert.ToDateTime(dari).Day;
                if (counting > 1)
                    d = 1;

                string daritgl = bl
                    + "/"
                    + d
                    + "/"
                    + thn;

                int qbatalBulan = Db.SingleInteger("SELECT ISNULL(COUNT(*),0)"
                    + " FROM MS_KONTRAK a INNER JOIN MS_UNIT u ON a.NoUnit = u.NoUnit"
                    + " WHERE TglBatal >= '" + Convert.ToDateTime(daritgl) + "'"
                    + " AND TglBatal <= '" + Convert.ToDateTime(sampaitgl) + "'"
                    + " AND a.Status = 'B'"
                    + " AND a.Jenis IN (" + akt + ")"
                    + nProject
                    + nPerusahaan
                    + strAdd
                    //+ nTipe
                    + nLokasi
                    + nTitipJual
                    + nTipePro
                    + " AND TglBatal is not null");

                int qallBulan = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK a INNER JOIN MS_UNIT u ON a.NoUnit = u.NoUnit"
                    + " WHERE TglKontrak >= '" + Convert.ToDateTime(daritgl) + "'"
                    + " AND TglKontrak <= '" + Convert.ToDateTime(sampaitgl) + "'"
                    + " AND a.Jenis IN (" + akt + ")"
                    + nProject
                    + nPerusahaan
                    + strAdd
                    //+ nTipe
                    + nLokasi
                    + nTitipJual
                    + nTipePro
                    );

                int qnetBulan = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK a INNER JOIN MS_UNIT u ON a.NoUnit = u.NoUnit"
                    + " WHERE TglKontrak >= '" + Convert.ToDateTime(daritgl) + "'"
                    + " AND TglKontrak <= '" + Convert.ToDateTime(sampaitgl) + "'"
                    + " AND a.Jenis IN (" + akt + ")"
                    + nProject
                    + nPerusahaan
                    + strAdd
                    //+ Tipe
                    + nLokasi
                    + nTitipJual
                    + nTipePro
                    );

                c = new TableCell();
                c.Text = "<p>" + qallBulan + "</p><img src='/Media/g2.jpg' height='" + qallBulan * (int)5 + "' width='15px' />";
                c.HorizontalAlign = HorizontalAlign.Center;
                c.VerticalAlign = VerticalAlign.Bottom;
                c.Attributes["style"] = "margin:0px; padding:0px; height:200px";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<p>" + qbatalBulan + "</p><img src='/Media/g1.jpg' height='" + qbatalBulan * (int)5 + "' width='15px' />";
                c.HorizontalAlign = HorizontalAlign.Center;
                c.VerticalAlign = VerticalAlign.Bottom;
                c.Attributes["style"] = "margin:0px; padding:0px";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<p>" + qnetBulan + "</p><img src='/Media/g3.jpg' height='" + qnetBulan * (int)5 + "' width='15px' />";
                c.HorizontalAlign = HorizontalAlign.Center;
                c.VerticalAlign = VerticalAlign.Bottom;
                c.Attributes["style"] = "margin:0px; padding:0px";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "&nbsp;&nbsp;";
                //c.Attributes["style"] = "padding-right:20px";
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Attributes["style"] = "margin:0px; padding:0px";
                r.Cells.Add(c);

                TableCell c1;

                c1 = new TableCell();
                c1.Text = Cf.Monthname(bl) + "<br />" + thn;
                c1.HorizontalAlign = HorizontalAlign.Center;
                c1.Attributes["style"] = "border-top:solid black 2px";
                c1.ColumnSpan = 4;

                r1.Cells.Add(c1);

                counting++;
                bl++;
            }
            graph.Rows.Add(r);
            graph.Rows.Add(r1);
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            //Rpt.Judul(x, comp, judul);


            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            Rpt.SubJudul(x
                , "Tanggal : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            string strAgent = "SEMUA";
            if (Agent != "SEMUA")
                strAgent = Agent;
            Rpt.SubJudul(x
                , "Sales : " + strAgent
                );
            Rpt.SubJudul(x
                , "Lokasi : " + Lokasi
                );

            string strPrincipal = "SEMUA";
            System.Text.StringBuilder z = new System.Text.StringBuilder();
            bool isFirst = true;


            string strTipe = "SEMUA";
            z = new System.Text.StringBuilder();
            isFirst = true;

            if (z.ToString() != "")
                strTipe = z.ToString();
            Rpt.SubJudul(x
                , "Tipe : " + Tipe
                );

            Rpt.Header(rptC, x);
        }

        private void newHeader()
        {


            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string header = "<h2>" + Mi.Pt + "</h2>";
            header += "<h1 class='title'>LAPORAN PENJUALAN</h1>";
            header += "Periode : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai);
            if (TitipJual != "SEMUA")
            {
                header += "<br/> Status Titip Jual : " + TitipJual;
            }
            else
            {
                header += "<br/> Status Titip Jual : SEMUA";
            }
            header += "<br/> Project : " + Project;
            header += "<br/> Perusahaan : " + Perusahaan;
            header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br />";
            headJudul.Text = header;
            //Response.Write(header);
        }


        private void Report()
        {
            //	param.Visible = false;

            lblA.Text = "<h3>A. Summary Penjualan</h3>";
            rptA.Visible = true;

            lblB.Text = "<h3>B. Pembatalan Unit</h3>";
            rptB.Visible = true;

            lblC.Text = "<h3>C. Penjualan Netto</h3>";
            rptC.Visible = true;

            lblD.Text = "<h3>D. Titip Jual</h3>";
            rptD.Visible = true;

            newHeader();
            //Header();
            Fill();

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            grafik(Dari.ToString(), Sampai.ToString());
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
