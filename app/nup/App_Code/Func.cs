using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;

namespace ISC064.NUP
{
    public class Func
    {
        #region public static void KontrakHeader(string NoKontrak, Label nokontrakl, Label unit, Label customer, Label agent)
        public static void KontrakHeader(string NoKontrak, Label nokontrakl, Label unit, Label customer, Label agent)
        {
            string strSql = "SELECT MS_KONTRAK.*"
                + ",MS_CUSTOMER.Nama AS Cs"
                + ",MS_AGENT.Nama + ' - ' + MS_AGENT.Principal AS Ag"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                System.Web.HttpContext.Current.Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nokontrakl.Text = rs.Rows[0]["NoKontrak"].ToString();
                unit.Text = rs.Rows[0]["NoUnit"].ToString();
                customer.Text = rs.Rows[0]["Cs"].ToString();
                agent.Text = rs.Rows[0]["Ag"].ToString();
            }
        }
        #endregion

        #region public static decimal SetelahDiskon(string RumusDiskon, decimal Gross)
        public static decimal SetelahDiskon(string RumusDiskon, decimal Gross2)
        {
            string[] diskon = RumusDiskon.Split('+');
            decimal netto = Gross2;

            if (RumusDiskon != "")
            {
                for (int i = 0; i <= diskon.GetUpperBound(0); i++)
                {
                    decimal nominal = Convert.ToDecimal(diskon[i]) * (decimal)-1;
                    netto = netto + (netto * (nominal / 100)); //persen
                }
            }

            return netto;
        }

        public static decimal SetelahDiskon2(string RumusDiskon, decimal Gross)
        {
            string[] diskon = RumusDiskon.Split('+');
            decimal netto = Gross;

            if (RumusDiskon != "")
            {
                for (int i = 0; i <= diskon.GetUpperBound(0); i++)
                {
                    decimal nominal = Convert.ToDecimal(diskon[i]);
                    netto = netto + (netto * (nominal / 100)); //persen
                }
            }

            return netto;
        }

        public static decimal NominalDiskon(string RumusDiskon, decimal Gross)
        {
            string[] diskon = RumusDiskon.Split('+');
            decimal netto = Gross;
            decimal nilaiDiskon = 0;

            if (RumusDiskon != "")
            {
                for (int i = 0; i <= diskon.GetUpperBound(0); i++)
                {
                    decimal nominal = Convert.ToDecimal(diskon[i]) * (decimal)-1;
                    nilaiDiskon = (netto * (nominal / 100)); //persen

                }
            }

            return nilaiDiskon;
        }
        public static decimal NominalDiskon2(string RumusDiskon, decimal Gross)
        {
            string[] diskon = RumusDiskon.Split('+');
            decimal netto = Gross;
            decimal nilaiDiskon = 0;

            if (RumusDiskon != "")
            {
                for (int i = 0; i <= diskon.GetUpperBound(0); i++)
                {
                    decimal nominal = Convert.ToDecimal(diskon[i]);
                    nilaiDiskon = (netto * (nominal / 100)); //persen

                }
            }

            return nilaiDiskon;
        }
        #endregion

        #region public static string[,] Breakdown(int Nomor, decimal Netto, DateTime TglKontrak)
        public static string[,] Breakdown(int Nomor, decimal Netto, DateTime TglKontrak)
        {
            DataTable rs = Db.Rs("SELECT * "
                + " FROM REF_SKEMA_DETAIL"
                + " WHERE Nomor = " + Nomor
                + " ORDER BY Baris"
                );

            string[,] x = new string[rs.Rows.Count, 6];

            decimal totalbf = 0;
            int potongbf = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                x[i, 0] = rs.Rows[i]["Baris"].ToString();
                x[i, 1] = rs.Rows[i]["Tipe"].ToString();
                x[i, 2] = rs.Rows[i]["Nama"].ToString();

                //jadwal
                DateTime Tgl = TglKontrak;
                if (rs.Rows[i]["TglFix"] is DBNull)
                {
                    string tipejadwal = rs.Rows[i]["TipeJadwal"].ToString();
                    int intjadwal = Convert.ToInt32(rs.Rows[i]["IntJadwal"]);
                    int refjadwal = Convert.ToInt32(rs.Rows[i]["RefJadwal"]);

                    DateTime TglReferensi = TglKontrak;
                    if (refjadwal != 0)
                    {
                        try
                        {
                            TglReferensi = Convert.ToDateTime(x[refjadwal - 1, 3]);
                        }
                        catch { }
                    }

                    if (tipejadwal == "M")
                        Tgl = TglReferensi.AddMonths(intjadwal); //bulanan
                    else
                        Tgl = TglReferensi.AddDays(intjadwal); //harian
                }
                else
                    Tgl = Convert.ToDateTime(rs.Rows[i]["TglFix"]); //Fix

                x[i, 3] = Cf.Day(Tgl);

                //nominal
                string tipenominal = rs.Rows[i]["TipeNominal"].ToString();
                decimal nominal = Convert.ToDecimal(rs.Rows[i]["Nominal"]);

                decimal n = nominal;
                if (tipenominal == "%") n = Netto * (nominal / 100);

                x[i, 4] = Cf.Num(n);

                if (rs.Rows[i]["Tipe"].ToString() == "BF")
                    totalbf = totalbf + n;

                if ((bool)rs.Rows[i]["BF"])
                    potongbf++;

                x[i, 5] = Cf.BoolToSql(Convert.ToBoolean(rs.Rows[i]["KPR"])).ToString();
            }

            //pemotongan booking fee
            if (potongbf > 0)
            {
                decimal bf = totalbf / potongbf;
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if ((bool)rs.Rows[i]["BF"])
                    {
                        decimal tagihan = Convert.ToDecimal(x[i, 4]);
                        x[i, 4] = Cf.Num(tagihan - bf);
                    }
                }
            }

            //pembulatan ribuan
            bool RThousand = Db.SingleBool("SELECT RThousand FROM REF_SKEMA WHERE Nomor = " + Nomor);
            decimal t = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (i != rs.Rows.Count - 1)
                {
                    decimal native = Convert.ToDecimal(x[i, 4]);
                    decimal rounded = 0;
                    if (RThousand)
                        rounded = RoundThousand(native);
                    else
                        rounded = RoundSatuan(native);
                    t = t + rounded;

                    x[i, 4] = Cf.Num(rounded);
                }
                else
                {
                    decimal sisa = Netto - t;

                    x[i, 4] = Cf.Num(sisa);
                }
            }

            return x;
        }
        private static decimal RoundSatuan(decimal input)
        {
            return Math.Round(input);
        }
        private static decimal RoundThousand(decimal input)
        {
            if (input < 1000)
            {
                return 1000;
            }
            else
            {
                input = RoundUp(input);
                if ((input % 1000) > 0)
                {
                    input = (input - (input % 1000)) + 1000;
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
        #endregion
        #region public static string[,] BreakdownKomisi(int Nomor, decimal Netto)
        public static string[,] BreakdownKomisi(int Nomor, decimal Netto)
        {
            DataTable rs = Db.Rs("SELECT * "
                + " FROM REF_SKOM_DETAIL"
                + " WHERE Nomor = " + Nomor
                + " ORDER BY Baris"
                );

            string[,] x = new string[rs.Rows.Count, 6];

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                x[i, 0] = rs.Rows[i]["Baris"].ToString();
                x[i, 1] = rs.Rows[i]["Nama"].ToString();

                //nominal
                string tipenominal = rs.Rows[i]["TipeNominal"].ToString();
                decimal nominal = Convert.ToDecimal(rs.Rows[i]["Nominal"]);

                decimal n = nominal;
                if (tipenominal == "%") n = Netto * (nominal / 100);

                x[i, 2] = Cf.Num(n);
                x[i, 3] = Cf.Num(rs.Rows[i]["TermCair"]);

                //jadwal
                string Tgl = "";
                if (rs.Rows[i]["TglFix"] is DBNull)
                {
                    string tipejadwal = rs.Rows[i]["TipeJadwal"].ToString();
                    int intjadwal = Convert.ToInt32(rs.Rows[i]["IntJadwal"]);

                    if (tipejadwal == "M")
                        Tgl = "+ " + intjadwal + " Bulan";
                    else
                        Tgl = "+ " + intjadwal + " Hari";
                }
                else
                    Tgl = Cf.Day(rs.Rows[i]["TglFix"]); //Fix

                x[i, 4] = Tgl;

                //tipe
                x[i, 5] = rs.Rows[i]["Tipe"].ToString();
            }

            return x;
        }
        #endregion

        #region public static string[,] BreakdownFO(int Nomor, int FOCounter, decimal FONominal)
        public static string[,] BreakFO(string Nomor, int FOCounter, decimal FONominal)
        {


            string[,] x = new string[FOCounter, 5];

            for (int i = 0; i < FOCounter; i++)
            {
                x[i, 1] = Cf.Num(FONominal / FOCounter);
                x[i, 2] = Cf.Num(i + 1);
            }
            return x;
        }
        #endregion

        #region public static void GenerateFP(string Peta)
        /// <summary>
        /// Redrawing process
        /// </summary>
        public static void GenerateFP(string Peta)
        {
            if (Peta != "")
            {

                //string loc = System.Web.HttpContext.Current.Request.PhysicalApplicationPath.Replace("launching", "marketingjual")
                //    + "FP\\";
                //string Src = loc + "Base\\" + Cf.FileSafe(Peta) + ".jpg";
                //string Target = loc + Cf.FileSafe(Peta) + ".jpg";

                string loc = System.Web.HttpContext.Current.Request.PhysicalApplicationPath
                   + "FP\\";
                string Src = loc + "Base\\" + Cf.FileSafe(Peta) + ".jpg";
                string Target = loc + Cf.FileSafe(Peta) + ".jpg";

                if (System.IO.File.Exists(Src))
                {
                    Bitmap objBitmap = new Bitmap(Src);
                    Graphics objGraphics = Graphics.FromImage(objBitmap);

                    string strSql = "SELECT Status, Koordinat, NoStock"
                        + " FROM MS_UNIT "
                        + " WHERE Peta = '" + Peta + "'";

                    DataTable rs = Db.Rs(strSql);
                    for (int i = 0; i < rs.Rows.Count; i++)
                    {
                        if (!System.Web.HttpContext.Current.Response.IsClientConnected) break;

                        string Coordinate = rs.Rows[i]["Koordinat"].ToString();
                        string status = rs.Rows[i]["Status"].ToString();
                        string NoStock = rs.Rows[i]["NoStock"].ToString();

                        string[] arrCoordinate = Coordinate.Split(new char[] { ',' });
                        System.Collections.ArrayList myPointList = new System.Collections.ArrayList();

                        for (int ix = 0; ix < arrCoordinate.GetUpperBound(0); ix = ix + 2)
                            myPointList.Add(new Point(
                                Convert.ToInt32(arrCoordinate[ix])
                                , Convert.ToInt32(arrCoordinate[ix + 1])));

                        Point[] ArrPoint = new Point[myPointList.Count];

                        for (int jx = 0; jx < myPointList.Count; jx++)
                            ArrPoint[jx] = (Point)myPointList[jx];

                        Color ColorPoly = new Color();
                        Color myColor = Color.FromArgb(50, Color.White);

                        if (status == "B")
                        {

                            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE Status = 'A' AND NoStock = '" + NoStock + "'");
                            if (c != 0)
                                ColorPoly = Color.Red; //sold
                        }
                        else if (status == "P")
                        {
                            ColorPoly = Color.Yellow; //hold internal
                        }
                        else if (status == "H")
                        {
                            ColorPoly = Color.Black; //reservasi pindah unit
                        }
                        else
                        {
                            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_RESERVASI WHERE Status = 'A' AND NoStock = '" + NoStock + "'");
                            if (c != 0)
                                ColorPoly = Color.Blue; //booked
                            else
                                ColorPoly = Color.White; //available
                        }

                        //objGraphics.FillPolygon(
                        //    new HatchBrush(HatchStyle.DarkUpwardDiagonal, ColorPoly, myColor)
                        //    , ArrPoint);

                        objGraphics.FillPolygon(new SolidBrush(ColorPoly), ArrPoint);
                        objGraphics.DrawPolygon(new Pen(Color.Black), ArrPoint);
                    }

                    objBitmap.Save(Target);

                    objGraphics.Dispose();
                    objBitmap.Dispose();
                }
            }
        }
        #endregion

        #region public static void GenerateKomisi(string NoKontrak, CheckBox SpecialEvent)
        public static void GenerateKomisi(string NoKontrak, CheckBox SpecialEvent)
        {
            int NoAgent = Db.SingleInteger("SELECT NoAgent FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            decimal NilaiKontrak = Db.SingleDecimal("SELECT NilaiKontrak FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            string Jenis = Db.SingleString("SELECT Jenis FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            string SkemaBayar = Db.SingleString("SELECT Skema FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            decimal Komisi = 0;
            SkemaBayar = SkemaBayar.Substring(SkemaBayar.Length - 4, 3);

            if (SkemaBayar != "021") //TUNAI BERTAHAP, kpr
            {
                if (Jenis == "STUDIO") //22
                {
                    Komisi = (decimal)1110000;
                }
                else if (Jenis == "EXECUTIVE") //36
                {
                    Komisi = (decimal)2000000;
                }
                else if (Jenis == "PENTHOUSE") //72
                {
                    Komisi = (decimal)0.025 * NilaiKontrak;
                }

                Db.Execute("EXEC spKomisiDaftar '" + NoKontrak + "'"
                    + ", 'PF', 'KOMISI TERMIJN 1'"
                    + ", " + (Komisi / (decimal)2)
                    + ", 10, '7'");
                Db.Execute("EXEC spKomisiDaftar '" + NoKontrak + "'"
                    + ", 'PF', 'KOMISI TERMIJN 1'"
                    + ", " + (Komisi / (decimal)2)
                    + ", 20, '7'");
            }
            else
            {
                if (Jenis == "STUDIO") //22
                {
                    Komisi = (decimal)1110000;
                }
                else if (Jenis == "EXECUTIVE") //36
                {
                    Komisi = (decimal)2000000;
                }
                else if (Jenis == "PENTHOUSE") //72
                {
                    Komisi = (decimal)0.025 * NilaiKontrak;
                }

                Db.Execute("EXEC spKomisiDaftar '" + NoKontrak + "'"
                    + ", 'PF', 'KOMISI'"
                    + ", " + Komisi
                    + ", 100, '7'");
            }
        }

        private static int HitungKum(int bln, int tahun, int NoAgent)
        {
            return Db.SingleInteger(
                "SELECT COUNT(NoKontrak) FROM MS_KONTRAK WHERE NoAgent = " + NoAgent + " AND Status = 'A'"
                + " AND MONTH(TglKontrak) = " + bln + " AND YEAR(TglKontrak) = " + tahun);
        }
        #endregion

        #region public static void CekKomisi(string NoKontrak)
        public static void CekKomisi(string NoKontrak)
        {
            Db.Execute("UPDATE MS_KONTRAK"
                + " SET FlagKomisi = 2"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " AND FlagKomisi = 1"
                );
        }
        #endregion

        #region public static bool CekAkunting(string NoKontrak, string NoTagihan)
        public static bool CekAkunting(string NoKontrak, string NoTagihan)
        {
            bool isAkunting = false;
            string strSql = "SELECT NoTTS FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + NoTagihan;
            DataTable rsTTS = Db.Rs(strSql);

            for (int j = 0; j < rsTTS.Rows.Count; j++)
            {
                if (Db.SingleInteger("SELECT Akunting FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + Cf.Pk(rsTTS.Rows[j]["NoTTS"])) > 0)
                {
                    isAkunting = true;
                    break;
                }
            }

            return isAkunting;
        }
        #endregion
        #region public static bool CekAkunting(string NoKontrak)
        public static bool CekAkunting(string NoKontrak)
        {
            bool isAkunting = false;
            string strSql = "SELECT NoTTS FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE Ref = '" + NoKontrak + "'";
            DataTable rsTTS = Db.Rs(strSql);

            for (int j = 0; j < rsTTS.Rows.Count; j++)
            {
                if (Db.SingleInteger("SELECT Akunting FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + Cf.Pk(rsTTS.Rows[j]["NoTTS"])) > 0)
                {
                    isAkunting = true;
                    break;
                }
            }

            return isAkunting;
        }
        #endregion
        #region public static bool CekAkunting(int NoTTS)
        public static bool CekAkunting(int NoTTS)
        {
            bool isAkunting = false;
            int Akunting = Db.SingleInteger("SELECT Akunting FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

            if (Akunting > 0)
                isAkunting = true;

            return isAkunting;
        }
        #endregion
        #region public static void CustomerPassword(string NoCustomer)
        public static void CustomerPassword(string NoCustomer)
        {
            string strSql = "SELECT * FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                HttpContext.Current.Response.Redirect("/CustomError/Deleted.html");
            else
            {
                //Custom Security
                if (Act.SecLevel == "AG" && rs.Rows[0]["AgentInput"].ToString() != Act.UserID)
                    HttpContext.Current.Response.Redirect("/CustomError/SecLevel.html");
            }
        }
        #endregion

        public static decimal SetelahBunga(string RumusBunga, decimal Gross2)
        {
            string[] bunga = RumusBunga.Split('+');
            decimal netto = Gross2;

            if (RumusBunga != "")
            {
                for (int i = 0; i <= bunga.GetUpperBound(0); i++)
                {
                    decimal nominal = Convert.ToDecimal(bunga[i]) * (decimal)-1;
                    netto = netto - (netto * (nominal / 100)); //persen
                }
            }

            return netto;
        }

        public static decimal NominalBunga(string RumusBunga, decimal Gross)
        {
            string[] bunga = RumusBunga.Split('+');
            decimal netto = Gross;
            decimal nilaiBunga = 0;

            if (RumusBunga != "")
            {
                for (int i = 0; i <= bunga.GetUpperBound(0); i++)
                {
                    decimal nominal = Convert.ToDecimal(bunga[i]) * (decimal)-1;
                    nilaiBunga += (netto * (nominal / 100)); //persen
                    netto += nilaiBunga;
                }
            }

            return nilaiBunga;
        }

        public static decimal NominalBunga2(string RumusBunga, decimal Gross)
        {
            string[] bunga = RumusBunga.Split('+');
            decimal netto = Gross;
            decimal nilaiBunga = 0;

            if (RumusBunga != "")
            {
                for (int i = 0; i <= bunga.GetUpperBound(0); i++)
                {
                    decimal nominal = Convert.ToDecimal(bunga[i]);
                    nilaiBunga += (netto * (nominal / 100)); //persen
                    netto += nilaiBunga;
                }
            }

            return nilaiBunga;
        }
    }
}
