using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR.Laporan
{
    public partial class MasterTTS : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Tipe { get { return (Request.QueryString["tipe"]); } }
        private string CaraBayar { get { return (Request.QueryString["carabayar"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusP { get { return (Request.QueryString["status_p"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string StatusV { get { return (Request.QueryString["status_v"]); } }
        private string Kasir { get { return (Request.QueryString["kasir"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string Rekening { get { return (Request.QueryString["rek"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Tanggal { get { return (Request.QueryString["tanggal"]); } }
        private string nDetil { get { return (Request.QueryString["detil"]); } }

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
            param.Visible = false;
            rpt.Visible = true;

            Header();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);
            

            //Rpt.SubJudul(x
            //    , "Tipe : " + Rpt.inSql(tipe).Replace("'", "")
            //    );

            //Rpt.SubJudul(x
            //    , "Cara Bayar : " + Rpt.inSql(carabayar).Replace("'", "")
            //    );


            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            //Cara Bayar
            string akt = String.Empty;
            akt = CaraBayar.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("+", " ");
            akt = akt.Replace(",", ",");
           

            Rpt.SubJudul(x
                , "Tanggal" + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );
            Rpt.SubJudul(x
            , "Cara Bayar : " + akt
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
            Rpt.SubJudul(x
               , "Project : " + Project
               );

            string pers = (Perusahaan == "SEMUA") ? "SEMUA" : Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Perusahaan + "'");

            Rpt.SubJudul(x
               , "Perusahaan : " + pers
               );

            if (StatusP != "")
                Rpt.SubJudul(x, "Status : " + StatusP);
            else if (StatusV != "")
                Rpt.SubJudul(x, "Status : " + StatusV);
            else if (StatusB != "")
                Rpt.SubJudul(x, "Status : " + StatusB);
            else if (StatusS != "")
                Rpt.SubJudul(x, "Status : " + StatusS);

            string legend = "Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer Bank / BG = Cek Giro / UJ = Uang Jaminan / DN = Diskon.";
            //Rpt.Header(rpt, x);
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
            if (Project != "SEMUA") nProject = " AND c.Project IN ('" + Project.Replace(",","','") + "')";
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




            string tgl = "";
            if (Tanggal == "tgltts") tgl = "TglTTS";
            if (Tanggal == "tglinput") tgl = "a.TglInput";
            if (Tanggal == "tglbkm") tgl = "TglBKM";
            if (Tanggal == "tglbg") tgl = "TglBG";

            string strSql = "SELECT a.*,b.Lokasi "
                + " FROM ISC064_FINANCEAR..MS_TTS a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT b ON a.Unit =  b.NoUnit "
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON a.Ref = c.NoKontrak"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND Tipe IN (" + type + ")"
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
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT b ON a.Unit =  b.NoUnit "
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON a.Ref = c.NoKontrak"
                + " WHERE 1=1"
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                 + " AND Tipe IN (" + type + ")"
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
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoTTS2"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                if ((int)rs.Rows[i]["NoBKM"] != 0)
                    c.Text = rs.Rows[i]["NoBKM2"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglTTS"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglBKM"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["UserID"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT DISTINCT TIPE FROM ISC064_MARKETINGJUAL..MS_TAGIHAN"
                       + " WHERE NOURUT in (SELECT NOTAGIHAN FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NOTTS='" + rs.Rows[i]["NoTTS"].ToString() + "')"
                       + " AND NoKontrak = '" + rs.Rows[i]["Ref"] + "'"
                       ;
                DataTable rs1 = Db.Rs(strSql);
                string w ="";
                if (rs1.Rows.Count > 1)
                {
                    for (int j = 0; j < rs1.Rows.Count; j++)
                    {
                        w += rs1.Rows[j]["Tipe"].ToString() + ",";
                    }
                }
                else
                {
                    for (int j = 0; j < rs1.Rows.Count; j++)
                    {
                        w += rs1.Rows[j]["Tipe"].ToString();
                    }
                }
                c.Text = w;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Ref"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Unit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Customer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["CaraBayar"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Ket"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoBG"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglBG"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Acc"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string Bank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + rs.Rows[i]["Acc"] + "' AND SubID = '" + rs.Rows[i]["SubID"] + "' ");
                c = new TableCell();
                c.Text = Bank;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                if (nDetil == "false")
                    c.Text = Cf.Num(rs.Rows[i]["Total"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LebihBayar"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal t = Convert.ToDecimal(rs.Rows[i]["Total"]) + Convert.ToDecimal(rs.Rows[i]["LebihBayar"]);

                c = new TableCell();
                c.Text = Cf.Num(t);//Cf.Num(rs.Rows[i]["Total2"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                if (nDetil == "true")
                    Detil(
                         rs.Rows[i]["NoTTS"].ToString()
                        , rs.Rows[i]["Tipe"].ToString()
                        , (decimal)rs.Rows[i]["Total"]
                        );

                t1 = t1 + (decimal)rs.Rows[i]["Total"];
                t2 = t2 + (decimal)rs.Rows[i]["LebihBayar"];
                t3 = t3 + t;

                if (i == rs.Rows.Count - 1)
                {
                    SubTotal("GRAND TOTAL", t1, t2, t3);
                    Giro(LembarGiro);
                }
            }
        }

        private void Giro(int LembarGiro)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.ColumnSpan = 21;
            c.Text = "<strong>Lembar Giro: </strong>" + LembarGiro.ToString();
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void Detil(string NoTTS, string Tipe, decimal Total)
        {
            string Tb = Sc.MktTb(Tipe);
            string strSql = "";

            if (Tipe != "TENANT")
            {
                strSql = "SELECT "
                    + " NilaiPelunasan AS Nilai"
                    + ",CASE NoTagihan"
                    + "		WHEN 0 THEN 'UNALLOCATED'"
                    + "		ELSE (SELECT NamaTagihan FROM " + Tb + "..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
                    + " END AS NamaTagihan"
                    + " FROM " + Tb + "..MS_PELUNASAN AS l "
                    + " WHERE NoTTS = " + NoTTS;
            }
            else
            {
                strSql = "SELECT "
                    + " NilaiTagihan+LebihBayar AS Nilai"
                    + ",NamaTagihan"
                    + ",Tipe"
                    + " FROM " + Tb + "..MS_TAGIHAN AS l "
                    + " WHERE NoTTS = " + NoTTS;
            }

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.ColumnSpan = 8;
                r.Cells.Add(c);

                c = new TableCell();
                c.ColumnSpan = 10;
                c.Text = rs.Rows[i]["NamaTagihan"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Nilai"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

            
                rpt.Rows.Add(r);

                if (i == rs.Rows.Count - 1)
                {
                    r = new TableRow();

                    c = new TableCell();
                    c.ColumnSpan = 17;
                    c.Text = "Total :";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Total);
                    c.Font.Bold = true;
                    c.Attributes["style"] = "border-top:1px solid black";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    rpt.Rows.Add(r);
                }
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 17;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
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
