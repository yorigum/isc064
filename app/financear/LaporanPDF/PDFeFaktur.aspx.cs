using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR.Laporan
{
    public partial class eFaktur : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Tipe { get { return (Request.QueryString["tipe"]); } }
        private string CaraBayar { get { return (Request.QueryString["carabayar"]); } }
        private string Tanggal { get { return (Request.QueryString["tanggal"]); } }
        private string Kasir { get { return (Request.QueryString["kasir"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string Rekening { get { return (Request.QueryString["rek"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusP { get { return (Request.QueryString["status_p"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string StatusV { get { return (Request.QueryString["status_v"]); } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");

            return a;
        }

        protected string BersihKoma(string kata)
        {
            if (kata != null)
            {
                if (kata.Contains(","))
                {
                    kata = kata.Replace(",", "");
                }
            }

            return kata;
        }

        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            Header();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            Rpt.SubJudul(x
                , "Tipe : " + Tipe.Replace("-",",").TrimEnd(',')
                );

            Rpt.SubJudul(x
                , "Cara Bayar : " + CaraBayar.Replace("-", ",").TrimEnd(',')
                );

            string tgl = "";
            if (Tanggal == "tgltts") tgl = "TglTTS";
            if (Tanggal == "tglinput") tgl = "a.TglInput";
            if (Tanggal == "tglbkm") tgl = "TglBKM";
            if (Tanggal == "tglbg") tgl = "TglBG";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            Rpt.SubJudul(x
                , "Kasir : " + Kasir
                );

            Rpt.SubJudul(x
                , "Rekening Bank : " + Rekening
                );
            Rpt.SubJudul(x
                , "Lokasi : " + Lokasi
                );

                if (StatusP != "")
                    Rpt.SubJudul(x, "Status : " + StatusP);
                else if (StatusV != "")
                    Rpt.SubJudul(x, "Status : " + StatusV);
                else if (StatusB != "")
                    Rpt.SubJudul(x, "Status : " + StatusB);
                else if (StatusS != "")
                    Rpt.SubJudul(x, "Status : " + StatusS);


            //Rpt.Header(rpt, x);
            string legend = "";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            string UserID = "";
            if (Kasir != "SEMUA")
                UserID = " AND UserID = '" + Kasir + "'";


            string Status = "";
            if (StatusP != "") Status = " AND a.Status = 'POST'";
            if (StatusB != "") Status = " AND a.Status = 'BARU'";
            if (StatusV != "") Status = " AND a.Status = 'VOID'";

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND c.Project IN('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND c.Pers = '" + Perusahaan + "'";        

            string strAcc = "";
            if (Rekening != "SEMUA")
                strAcc = " AND Acc = '" + Cf.Str(Rekening) + "'";

            string nLokasi = "";

            if (Lokasi != "SEMUA")// ;
                nLokasi = " AND b.Lokasi = '" + Cf.Str(Lokasi) + "'";


            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = a.Ref) = " + UserAgent();

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;


            string tgl = "";
            if (Tanggal == "tgltts") tgl = "TglTTS";
            if (Tanggal == "tglinput") tgl = "a.TglInput";
            if (Tanggal == "tglbkm") tgl = "TglBKM";
            if (Tanggal == "tglbg") tgl = "TglBG";



            //Tipe
            string type = String.Empty;
            type = Tipe.Replace("-", ",").TrimEnd(',');
            type = type.Replace("+", " ");
            type = type.Replace(",", "','");
            type = "'" + type + "'";

            //Cara Bayar
            string akt = String.Empty;
            akt = CaraBayar.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("+", " ");
            akt = akt.Replace(",", "','");
            akt = "'" + akt + "'";



            string strSql = "SELECT a.*, b.Lokasi, b.Jenis "
                + " FROM ISC064_FINANCEAR..MS_TTS a"
                + " INNER JOIN [ISC064_MARKETINGJUAL].[dbo].[MS_UNIT] b ON a.Unit =  b.NoUnit "
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON a.Ref = c.NoKontrak"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Tipe IN (" + type + ")"
                + " AND a.CaraBayar IN (" + akt + ")"
                + nProject
                + nPerusahaan
                + UserID
                + Status
                + strAcc
                + nLokasi
                + agent
                + " ORDER BY NoTTS";

            DataTable rs = Db.Rs(strSql);

            DataTable rsGiro = Db.Rs(
                "SELECT a.*,b.Lokasi "
                + " FROM ISC064_FINANCEAR..MS_TTS a"
                + " INNER JOIN [ISC064_MARKETINGJUAL].[dbo].[MS_UNIT] b ON a.Unit =  b.NoUnit "
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON a.Ref = c.NoKontrak"
                + " WHERE 1=1"
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Tipe IN (" + type + ")"
                + " AND a.CaraBayar IN (" + akt + ")"
                + nProject
                + nPerusahaan
                + UserID
                + Status
                + strAcc
                + nLokasi
                + " AND NoBG <> ''"
                );
            int LembarGiro = rsGiro.Rows.Count;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditTTS('" + rs.Rows[i]["NoTTS"] + "')";

                c = new TableCell();
                c.Text = "FK" + "<br />" + "FAPR" + "<br />" + "OF";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string strSql2 = "";
                strSql2 = "SELECT DISTINCT NamaTagihan FROM ISC064_MARKETINGJUAL..MS_TAGIHAN"
                     + " WHERE NOURUT in (SELECT NOTAGIHAN FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NOTTS='" + rs.Rows[i]["NoTTS"].ToString() + "')"
                     + " AND NoKontrak = '" + rs.Rows[i]["Ref"] + "'"
                     ;
                DataTable rs12 = Db.Rs(strSql2);
                string w2 = "";
                if (rs12.Rows.Count > 1)
                {
                    for (int j = 0; j < rs12.Rows.Count; j++)
                    {
                        w2 += rs12.Rows[j]["NamaTagihan"].ToString() + ";";
                    }
                }
                else
                {
                    for (int j = 0; j < rs12.Rows.Count; j++)
                    {
                        w2 += rs12.Rows[j]["NamaTagihan"].ToString();
                    }
                }

                string Kode_Objek = Db.SingleString("SELECT ISNULL(KODE,' ') FROM REF_EFAKTUR WHERE Uraian='" + w2 + "'");
                string NamaNPWP = Db.SingleString("SELECT ISNULL(NPWPNama,' ') FROM ISC064_SECURITY..REF_DATA");
                string AlamatNPWP = Db.SingleString("SELECT ISNULL(AlamatNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                string NomorNPWP = Db.SingleString("SELECT ISNULL(NomorNPWP,' ') FROM ISC064_SECURITY..REF_DATA");

                c = new TableCell();
                c.Text = "01" + "<br />" + BersihKoma(NamaNPWP) + "<br />" + BersihKoma(Kode_Objek);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string NoFPS = rs.Rows[i]["NoFPS"].ToString();
                string[] noFG = NoFPS.Split('.');
                string printFG = "", printFP = "";

                if (noFG.Length > 0)
                {
                    //printFG = noFG.Length.ToString();
                    //printFP = noFG[1].ToString();

                    for (int count = 0; count <= noFG.Length - 1; count++)
                    {
                        if (count == 0)
                            printFG = noFG[count].ToString();

                        if (count > 0)
                        {
                            if (count == 1)
                                printFP += noFG[count].ToString();
                            else
                                printFP += "." + noFG[count].ToString();
                        }
                    }
                }

                c = new TableCell();
                c.Text = BersihKoma(printFG) + "<br />" + BersihKoma(AlamatNPWP) + "<br />" + w2;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal NilaiBayar = Convert.ToDecimal(rs.Rows[i]["Total"]);
                decimal DPP = NilaiBayar / (decimal)1.1;

                c = new TableCell();
                string Jalan = Db.SingleString("SELECT ISNULL(AlamatProject,' ') FROM ISC064_SECURITY..REF_DATA");
                printFP = printFP.Replace(".", "");                
                c.Text = BersihKoma(printFP) + "<br />" + BersihKoma(Jalan) + "<br />" + Math.Round(DPP).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                DateTime TglKwitasi = Convert.ToDateTime(rs.Rows[i]["TglBKM"]);
                int BulanKwitansi = TglKwitasi.Month;
                int TahunKwitansi = TglKwitasi.Year;
                string BlokNPWP = Db.SingleString("SELECT ISNULL(BlokNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                c = new TableCell();
                c.Text = BulanKwitansi + "<br />" + BersihKoma(BlokNPWP) + "<br />" + "1";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = TahunKwitansi + "<br />" + " " + "<br />" + Math.Round(DPP).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string RTNPWP = Db.SingleString("SELECT ISNULL(RTNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                c = new TableCell();
                c.Text = Cf.DaySlash(TglKwitasi) + "<br />" + BersihKoma(RTNPWP) + "<br />" + "0";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string QueryNamaCS = "SELECT ISNULL(NPWP,' ') FROM ISC064_MARKETINGJUAL..MS_KONTRAK A"
                     + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER  B ON A.NoCustomer = B.NoCustomer"
                     + " WHERE NoKontrak = '" + rs.Rows[i]["Ref"] + "'";

                string NPWPCS = Db.SingleString(QueryNamaCS);
                string RWNPWP = Db.SingleString("SELECT ISNULL(RWNPWP,'') FROM ISC064_SECURITY..REF_DATA");
                c = new TableCell();                
                c.Text = BersihKoma(NPWPCS) + "<br />" + BersihKoma(RWNPWP) + "<br />" + Math.Round(DPP).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string KecamatanNPWP = Db.SingleString("SELECT ISNULL(KecamatanNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                c = new TableCell();
                c.Text = BersihKoma(rs.Rows[i]["Customer"].ToString()) + "<br />" + BersihKoma(KecamatanNPWP) + "<br />" + Math.Round(NilaiBayar - DPP).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                // DATA CUSTOMER
                decimal NoCustomer = Db.SingleDecimal("SELECT NoCustomer FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak='" + rs.Rows[i]["Ref"] + "'");
                DataTable dtCustomer = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer ="+ NoCustomer);
                string Alm_NPWP1 = "", Alm_NPWP2 = "", Alm_NPWP3 = "";
                string Alm_KTP1 = "", Alm_KTP2 = "", Alm_KTP3 = "", Alm_KTP4 = "";
                string AlamatCS = " ";

                if (dtCustomer.Rows.Count > 0)
                {
                    Alm_NPWP1 = dtCustomer.Rows[0]["NPWPAlamat1"].ToString().Trim();
                    Alm_NPWP2 = dtCustomer.Rows[0]["NPWPAlamat2"].ToString().Trim();
                    Alm_NPWP3 = dtCustomer.Rows[0]["NPWPAlamat3"].ToString().Trim();
                    Alm_KTP1 = dtCustomer.Rows[0]["KTP1"].ToString().Trim();
                    Alm_KTP2 = dtCustomer.Rows[0]["KTP2"].ToString().Trim();
                    Alm_KTP3 = dtCustomer.Rows[0]["KTP3"].ToString().Trim();
                    Alm_KTP4 = dtCustomer.Rows[0]["KTP4"].ToString().Trim();

                    if (Alm_NPWP1 == " " || Alm_NPWP2 == " " || Alm_NPWP3 == " " || Alm_NPWP1 == "-" || Alm_NPWP2 == "-" || Alm_NPWP3 == "-")
                    {
                        if (Alm_KTP1 == " " || Alm_KTP2 == " " || Alm_KTP3 == " " || Alm_KTP4 == " " || Alm_KTP1 == "-" || Alm_KTP2 == "-" || Alm_KTP3 == "-" || Alm_KTP4 == "-")
                            AlamatCS = " ";
                        else
                            AlamatCS = Alm_KTP1 + " " + Alm_KTP2 + " " + Alm_KTP3 + " " + Alm_KTP4;
                    }
                    else
                        AlamatCS = Alm_NPWP1 + " " + Alm_NPWP2 + " " + Alm_NPWP3;

                    AlamatCS = AlamatCS.Trim();

                    if (AlamatCS == "")
                        AlamatCS = "&nbsp;";
                }

                string KelurahanNPWP = Db.SingleString("SELECT ISNULL(KelurahanNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                c = new TableCell();
                c.Text = BersihKoma(AlamatCS) + "<br />" + BersihKoma(KelurahanNPWP) + "<br />" + "0";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string KabupatenNPWP = Db.SingleString("SELECT ISNULL(KabupatenNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                c = new TableCell();
                c.Text = Math.Round(DPP).ToString() + "<br />" + BersihKoma(KabupatenNPWP) + "<br />" + "0";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string PropinsiNPWP = Db.SingleString("SELECT ISNULL(PropinsiNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                c = new TableCell();
                c.Text = Math.Round(NilaiBayar - DPP).ToString() + "<br />" + BersihKoma(PropinsiNPWP) + "<br />" + "0";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //JUMLAH_PPNBM  -  KODE_POS
                string KodePOS = Db.SingleString("SELECT ISNULL(KodePosNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                c = new TableCell();
                c.Text = "0" + "<br />" + BersihKoma(KodePOS) + "<br />" + " ";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //ID_KETERANGAN_TAMBAHAN   -   NOMOR_TELEPON   -   <<KOSONG>>
                string NoTelp = Db.SingleString("SELECT ISNULL(NoTelp,' ') FROM ISC064_SECURITY..REF_DATA");
                c = new TableCell();                
                c.Text = "&nbsp;" + "<br />" + BersihKoma(NoTelp) + "<br />" + "&nbsp;";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "0" + "<br />" + " " + "<br />" + " ";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //UANG_MUKA_DPP
                c = new TableCell();
                c.Text = "0" + "<br />" + " " + "<br />" + " ";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //UANG_MUKA_PPN
                c = new TableCell();
                c.Text = "0" + "<br />" + " " + "<br />" + " ";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //UANG_MUKA_PPNBM
                c = new TableCell();
                c.Text = "0" + "<br />" + " " + "<br />" + " ";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //REFERENSI
                string sRef = w2 + " " + rs.Rows[0]["Jenis"].ToString() + " " + "SL-" + rs.Rows[0]["Unit"].ToString();
                c = new TableCell();
                c.Text = BersihKoma(sRef) + "<br />" + " " + "<br />" + " ";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }
        }

        private void FillCsv()
        {
            string csv = string.Empty;
            //FK	KD_JENIS_TRANSAKSI	FG_PENGGANTI	NOMOR_FAKTUR	MASA_PAJAK	TAHUN_PAJAK	TANGGAL_FAKTUR
            //NPWP	NAMA	ALAMAT_LENGKAP	JUMLAH_DPP	JUMLAH_PPN	JUMLAH_PPNBM	
            //ID_KETERANGAN_TAMBAHAN	FG_UANG_MUKA	UANG_MUKA_DPP	UANG_MUKA_PPN	
            //UANG_MUKA_PPNBM	REFERENSI
            csv += "FK,";
            csv += "KD_JENIS_TRANSAKSI,";
            csv += "FG_PENGGANTI,";
            csv += "NOMOR_FAKTUR,";
            csv += "MASA_PAJAK,";
            csv += "TAHUN_PAJAK,";
            csv += "TANGGAL_FAKTUR,";
            csv += "NPWP,";
            csv += "NAMA,";
            csv += "ALAMAT_LENGKAP,";
            csv += "JUMLAH_DPP,";
            csv += "JUMLAH_PPN,";
            csv += "JUMLAH_PPNBM,";
            csv += "ID_KETERANGAN_TAMBAHAN,";
            csv += "FG_UANG_MUKA,";
            csv += "UANG_MUKA_DPP,";
            csv += "UANG_MUKA_PPN,";
            csv += "UANG_MUKA_PPNBM,";
            csv += "REFERENSI";


            //Add new line.
            csv += "\r\n";

            //LT	NPWP	NAMA	JALAN	BLOK	NOMOR	RT	RW	KECAMATAN	
            //KELURAHAN	KABUPATEN	PROPINSI	KODE_POS	NOMOR_TELEPON					

            csv += "LT,";
            csv += "NPWP,";
            csv += "NAMA,";
            csv += "JALAN,";
            csv += "BLOK,";
            csv += "RT,";
            csv += "RW,";
            csv += "KECAMATAN,";
            csv += "KELURAHAN,";
            csv += "KABUPATEN,";
            csv += "PROPINSI,";
            csv += "KODE_POS,";
            csv += "NOMOR_TELEPON";

            csv += "\r\n";
            //OF	KODE_OBJEK	NAMA	HARGA_SATUAN	JUMLAH_BARANG	HARGA_TOTAL	DISKON	DPP	PPN	TARIF_PPNBM	PPNBM								
            csv += "OF,";
            csv += "KODE_OBJEK,";
            csv += "NAMA,";
            csv += "HARGA_SATUAN,";
            csv += "JUMLAH_BARANG,";
            csv += "HARGA_TOTAL,";
            csv += "DISKON,";
            csv += "DPP,";
            csv += "PPN,";
            csv += "TARIF_PPNBM,";
            csv += "PPNBM";
            csv += "\r\n";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string UserID = "";
            if (Kasir != "SEMUA")
                UserID = " AND UserID = '" + Kasir + "'";


            string Status = "";
            if (StatusP != "") Status = " AND a.Status = 'POST'";
            if (StatusB != "") Status = " AND a.Status = 'BARU'";
            if (StatusV != "") Status = " AND a.Status = 'VOID'";


            string strAcc = "";
            if (Rekening != "SEMUA")
                strAcc = " AND Acc = '" + Cf.Str(Rekening) + "'";

            string nLokasi = "";

            if (Lokasi != "SEMUA")// ;
                nLokasi = " AND b.Lokasi = '" + Cf.Str(Lokasi) + "'";

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = a.Ref) = " + UserAgent();

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            string tgl = "";
            if (Tanggal == "tgltts") tgl = "TglTTS";
            if (Tanggal == "tglinput") tgl = "a.TglInput";
            if (Tanggal == "tglbkm") tgl = "TglBKM";
            if (Tanggal == "tglbg") tgl = "TglBG";

            //Tipe
            string type = String.Empty;
            type = Tipe.Replace("-", ",").TrimEnd(',');
            type = type.Replace("+", " ");
            type = type.Replace(",", "','");
            type = "'" + type + "'";

            //Cara Bayar
            string akt = String.Empty;
            akt = CaraBayar.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("+", " ");
            akt = akt.Replace(",", "','");
            akt = "'" + akt + "'";




            string strSql = "SELECT a.*, b.Lokasi, b.Jenis "
                + " FROM ISC064_FINANCEAR..MS_TTS a"
                + " INNER JOIN [ISC064_MARKETINGJUAL].[dbo].[MS_UNIT] b ON a.Unit =  b.NoUnit "
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Tipe IN (" + type + ")"
                + " AND CaraBayar IN (" + akt + ")"
                + UserID
                + Status
                + strAcc
                + nLokasi
                + agent
                + " ORDER BY NoTTS";

            DataTable rs = Db.Rs(strSql);

            string NPWPPT = Db.SingleString("SELECT ISNULL(NPWP,' ') FROM ISC064_SECURITY..REF_DATA");
            string NamaNPWP = Db.SingleString("SELECT ISNULL(NPWPNama,' ') FROM ISC064_SECURITY..REF_DATA");
            string AlamatNPWP = Db.SingleString("SELECT ISNULL(AlamatNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
            string NomorNPWP = Db.SingleString("SELECT ISNULL(NomorNPWP,' ') FROM ISC064_SECURITY..REF_DATA");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;


                //string strSql2 = "";
                //strSql2 = "SELECT DISTINCT NamaTagihan FROM ISC064_MARKETINGJUAL..MS_TAGIHAN"
                //     + " WHERE NOURUT in (SELECT NOTAGIHAN FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NOTTS='" + rs.Rows[i]["NoTTS"].ToString() + "')"
                //     + " AND NoKontrak = '" + rs.Rows[i]["Ref"] + "'"
                //     ;
                //DataTable rs12 = Db.Rs(strSql2);
                //string w2 = "";
                //if (rs12.Rows.Count > 1)
                //{
                //    for (int j = 0; j < rs12.Rows.Count; j++)
                //    {
                //        w2 += rs12.Rows[j]["NamaTagihan"].ToString() + ";";
                //    }
                //}
                //else
                //{
                //    for (int j = 0; j < rs12.Rows.Count; j++)
                //    {
                //        w2 += rs12.Rows[j]["NamaTagihan"].ToString();
                //    }
                //}

                string strSql2 = "";
                strSql2 = "SELECT DISTINCT NoUnit FROM ISC064_MARKETINGJUAL..MS_KONTRAK"
                     + " WHERE NoKontrak = '" + rs.Rows[i]["Ref"] + "'"
                     ;
                DataTable rs12 = Db.Rs(strSql2);
                string w2 = "";
                if (rs12.Rows.Count > 1)
                {
                    for (int j = 0; j < rs12.Rows.Count; j++)
                    {
                        w2 += rs12.Rows[j]["NoUnit"].ToString() + ";";
                    }
                }
                else
                {
                    for (int j = 0; j < rs12.Rows.Count; j++)
                    {
                        w2 += rs12.Rows[j]["NoUnit"].ToString();
                    }
                }
                string Kode_Objek = Db.SingleString("SELECT ISNULL(KODE,' ') FROM REF_EFAKTUR WHERE Uraian='" + w2 + "'");
                //Baris 1
                string NoFPS = rs.Rows[i]["NoFPS"].ToString();
                string[] noFG = NoFPS.Split('.');
                string printFG = "", printFP = "";

                if (noFG.Length > 0)
                {
                    //printFG = noFG.Length.ToString();
                    //printFP = noFG[1].ToString();

                    for (int count = 0; count <= noFG.Length - 1; count++)
                    {
                        if (count == 0)
                            printFG = noFG[count].ToString();

                        if (count > 0)
                        {
                            if (count == 1)
                                printFP += noFG[count].ToString();
                            else
                                printFP += "." + noFG[count].ToString();
                        }
                    }
                }
                DateTime TglKwitasi = Convert.ToDateTime(rs.Rows[i]["TglBKM"]);
                int BulanKwitansi = TglKwitasi.Month;
                int TahunKwitansi = TglKwitasi.Year;
                string QueryNamaCS = "SELECT ISNULL(NPWP,' ') FROM ISC064_MARKETINGJUAL..MS_KONTRAK A"
                  + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER  B ON A.NoCustomer = B.NoCustomer"
                  + " WHERE NoKontrak = '" + rs.Rows[i]["Ref"] + "'";

                string NPWPCS = Db.SingleString(QueryNamaCS);


                decimal NoCustomer = Db.SingleDecimal("SELECT NoCustomer FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak='" + rs.Rows[i]["Ref"] + "'");
                DataTable dtCustomer = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + NoCustomer);
                string Alm_NPWP1 = "", Alm_NPWP2 = "", Alm_NPWP3 = "";
                string Alm_KTP1 = "", Alm_KTP2 = "", Alm_KTP3 = "", Alm_KTP4 = "";
                string AlamatCS = " ";

                if (dtCustomer.Rows.Count > 0)
                {
                    Alm_NPWP1 = dtCustomer.Rows[0]["NPWPAlamat1"].ToString().Trim();
                    Alm_NPWP2 = dtCustomer.Rows[0]["NPWPAlamat2"].ToString().Trim();
                    Alm_NPWP3 = dtCustomer.Rows[0]["NPWPAlamat3"].ToString().Trim();
                    Alm_KTP1 = dtCustomer.Rows[0]["KTP1"].ToString().Trim();
                    Alm_KTP2 = dtCustomer.Rows[0]["KTP2"].ToString().Trim();
                    Alm_KTP3 = dtCustomer.Rows[0]["KTP3"].ToString().Trim();
                    Alm_KTP4 = dtCustomer.Rows[0]["KTP4"].ToString().Trim();

                    if (Alm_NPWP1 == " " || Alm_NPWP2 == " " || Alm_NPWP3 == " " || Alm_NPWP1 == "-" || Alm_NPWP2 == "-" || Alm_NPWP3 == "-")
                    {
                        if (Alm_KTP1 == " " || Alm_KTP2 == " " || Alm_KTP3 == " " || Alm_KTP4 == " " || Alm_KTP1 == "-" || Alm_KTP2 == "-" || Alm_KTP3 == "-" || Alm_KTP4 == "-")
                            AlamatCS = " ";
                        else
                            AlamatCS = Alm_KTP1 + " " + Alm_KTP2 + " " + Alm_KTP3 + " " + Alm_KTP4;
                    }
                    else
                        AlamatCS = Alm_NPWP1 + " " + Alm_NPWP2 + " " + Alm_NPWP3;

                    AlamatCS = AlamatCS.Trim();

                    if (AlamatCS == "")
                        AlamatCS = "";
                }
                decimal NilaiBayar = Convert.ToDecimal(rs.Rows[i]["Total"]);
                decimal DPP = NilaiBayar / (decimal)1.1;
                string sRef = w2 + " " + rs.Rows[0]["Jenis"].ToString() + " " + "SL-" + rs.Rows[0]["Unit"].ToString();

                string KelurahanNPWP = Db.SingleString("SELECT ISNULL(KelurahanNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                string KabupatenNPWP = Db.SingleString("SELECT ISNULL(KabupatenNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                string KodePOS = Db.SingleString("SELECT ISNULL(KodePosNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                string NoTelp = Db.SingleString("SELECT ISNULL(NoTelp,' ') FROM ISC064_SECURITY..REF_DATA");
                string PropinsiNPWP = Db.SingleString("SELECT ISNULL(PropinsiNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                string BlokNPWP = Db.SingleString("SELECT ISNULL(BlokNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                string RTNPWP = Db.SingleString("SELECT ISNULL(RTNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                string RWNPWP = Db.SingleString("SELECT ISNULL(RWNPWP,'') FROM ISC064_SECURITY..REF_DATA");
                string KecamatanNPWP = Db.SingleString("SELECT ISNULL(KecamatanNPWP,' ') FROM ISC064_SECURITY..REF_DATA");
                csv += "FK".Replace(",", ";") + ',' + "01".Replace(",", ";") + ',' + "0".Replace(",", ";") + ',' + printFP.Replace(".", "").Replace(",", ";") + ',';
                csv += BulanKwitansi.ToString().Replace(",", ";") + ',' + TahunKwitansi.ToString().Replace(",", ";") + ',' + Cf.DaySlash(TglKwitasi) +',' + NPWPCS + ',';
                csv += rs.Rows[i]["Customer"].ToString().Replace(",", ";") + ',' + AlamatCS.Replace(",", ";") + ',' + Math.Round(DPP).ToString().Replace(",", ";") + ',' + Math.Round(NilaiBayar - DPP).ToString().Replace(",", ";") + ',';
                csv += "0".Replace(",", ";") + ',' + "".Replace(",", ";") + ',' + "0".Replace(",", ";") + ',' + "0".Replace(",", ";") + ',';
                csv += "0".Replace(",", ";") + ',' + "0".Replace(",", ";") + ',' + sRef.Replace(",", ";");
                csv += "\r\n";

                //Baris 2



                //csv += "FAPR".Replace(",", ";") + ',' + NPWPPT.Replace(",", ";") + ',' + NamaNPWP.Replace(",", ";") + ',' + AlamatNPWP.Replace(",", ";") + ',';
                //csv += BlokNPWP.Replace(",", ";") + ',' + RTNPWP.Replace(",", ";") + ',' + RWNPWP.Replace(",", ";") + ',' + KecamatanNPWP.Replace(",", ";") + ',';
                //csv += KelurahanNPWP.Replace(",", ";") + ',' + KabupatenNPWP.Replace(",", ";") + ',' + PropinsiNPWP.Replace(",", ";") + ',' + KodePOS.Replace(",", ";") + ',';
                //csv += NoTelp.Replace(",", ";");
                //csv += "\r\n";

                //Baris 3
                csv += "OF".ToString().Replace(",", ";") + ',' + Kode_Objek + ',' + w2.Replace(",", ";") + ',' + Math.Round(DPP).ToString().Replace(",", ";") + ',';
                csv += "1".Replace(",", ";") + ',' + Math.Round(DPP).ToString().Replace(",", ";") + ',' + "0".ToString().Replace(",", ";") + ',' + Math.Round(DPP).ToString().Replace(",", ";") + ',' + Math.Round(NilaiBayar - DPP).ToString().Replace(",", ";") + ',' + "0".ToString().Replace(",", ";") + ',' + "0".ToString().Replace(",", ";");
                csv += "\r\n";
            }


            string NamaFileCsv = "";
            NamaFileCsv = "eFaktur" + DateTime.Now.Month + DateTime.Now.Year;

            //Download the CSV file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + NamaFileCsv + ".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
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

