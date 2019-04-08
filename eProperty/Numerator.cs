using System;
using System.Data;

namespace ISC064
{
    public class Numerator
    {
        public static byte ResetNumbering(string Kode, string project)
        {
            return Db.SingleByte(
                "SELECT ISNULL(MAX(ResetNum),0) FROM " + Mi.DbPrefix + "SECURITY..Numerator WHERE Kode = '" + Kode + "' AND Project = '" + project + "'");
        }
        public static string NoInvoice(int Bulan, int Tahun)
        {
            string pk = "";
            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM ISC064_MARKETINGSEWA..MS_TAGIHAN WHERE MONTH(TglJT) = " + Bulan + " AND YEAR(TglJT) = " + Tahun + " AND NoInvoice <> ''"
                );

            c += 1;

            while (true)
            {
                pk = c.ToString().PadLeft(5, '0') + "/BM/FA/INV-AS/" + Cf.Roman(Bulan) + "/" + Tahun;
                int cek = Db.SingleInteger(
                    "SELECT COUNT(*) FROM ISC064_MARKETINGSEWA..MS_TAGIHAN WHERE NoInvoice = '" + pk + "'"
                    );

                if (cek > 0)
                    c++;

                break;
            }

            return pk;
        }

        public static string NoInvoice(string Tipe, int Bulan, int Tahun)
        {
            string pk = "", x = "";

            if (Tipe == "ELE")
                x = "LT";
            else if (Tipe == "AIR")
                x = "AR";
            else
                x = Tipe;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM ISC064_TENANT..MS_TAGIHAN WHERE Tipe = '" + Tipe + "' AND MONTH(TglJT) = " + Bulan + " AND YEAR(TglJT) = " + Tahun + " AND NoInvoice <> ''"
                );

            c += 1;

            while (true)
            {
                pk = c.ToString().PadLeft(5, '0') + "/BM/FA/INV-" + x + "/" + Cf.Roman(Bulan) + "/" + Tahun;
                int cek = Db.SingleInteger(
                    "SELECT COUNT(*) FROM ISC064_TENANT..MS_TAGIHAN WHERE NoInvoice = '" + pk + "'"
                    );

                if (cek > 0)
                    c++;

                break;
            }

            return pk;
        }

        //Numerator
        //Marketingjual
        #region public static string SuratPesanan(int Bulan, int Tahun,string Project)
        public static string SuratPesanan(int Bulan, int Tahun, string Project)
        {
            string Kode = "SP";

            string w1 = "1=1";
            byte ResetNum = ResetNumbering(Kode, Project);
            if (ResetNum == 1) w1 += " AND Month(TglKontrak) = " + Bulan;
            if (ResetNum == 1 || ResetNum == 2) w1 += " AND Year(TglKontrak) = " + Tahun;

            DataTable rs = Db.Rs("SELECT NoKontrak FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE "
                + w1 + " AND Project = '" + Project + "'"
                );

            int num = rs.Rows.Count + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = PK(Kode, Tahun, Bulan, num, Project);
                if (Db.SingleInteger("SELECT COUNT(NoKontrak) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        #endregion

        #region public static string SuratReservasi(int Bulan, int Tahun,string Project)
        public static string SuratReservasi(int Bulan, int Tahun, string Project)
        {
            string Kode = "RSV";

            string w1 = "1=1";
            byte ResetNum = ResetNumbering(Kode, Project);
            if (ResetNum == 1) w1 += " AND Month(Tgl) = " + Bulan;
            if (ResetNum == 1 || ResetNum == 2) w1 += " AND Year(Tgl) = " + Tahun;

            DataTable rs = Db.Rs("SELECT NoReservasi2 FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI WHERE "
                + w1 + " AND Project = '" + Project + "'"
                );

            int num = rs.Rows.Count + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = PK(Kode, Tahun, Bulan, num, Project);
                if (Db.SingleInteger("SELECT COUNT(NoReservasi2) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI WHERE NoReservasi2 = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        #endregion

        #region public static string Approval(int Bulan, int Tahun,string Project)
        public static string Approval(int Bulan, int Tahun, string Project)
        {
            string Kode = "SP";

            string w1 = "1=1";
            byte ResetNum = ResetNumbering(Kode, Project);
            if (ResetNum == 1) w1 += " AND Month(TglKontrak) = " + Bulan;
            if (ResetNum == 1 || ResetNum == 2) w1 += " AND Year(TglKontrak) = " + Tahun;

            DataTable rs = Db.Rs("SELECT NoKontrak FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK_APPROVAL WHERE "
                + w1 + " AND Project = '" + Project + "'"
                );

            int num = rs.Rows.Count + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = PK(Kode, Tahun, Bulan, num, Project);
                if (Db.SingleInteger("SELECT COUNT(NoKontrak) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK_APPROVAL WHERE NoKontrak = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        #endregion
        //Legal
        #region public static string PPJB(int Bulan, int Tahun,string Project)
        public static string PPJB(int Bulan, int Tahun, string Project)
        {
            string Kode = "PPJB";

            string w1 = "1=1";
            byte ResetNum = ResetNumbering(Kode, Project);
            if (ResetNum == 1) w1 += " AND Month(TglPPJB) = " + Bulan;
            if (ResetNum == 1 || ResetNum == 2) w1 += " AND Year(TglPPJB) = " + Tahun;

            DataTable rs = Db.Rs("SELECT ISNULL(NoPPJB, 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PPJB WHERE "
                + w1 + " AND Project = '" + Project + "'"
                );

            int num = rs.Rows.Count + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = PK(Kode, Tahun, Bulan, num, Project);
                if (Db.SingleInteger("SELECT COUNT(PPJB) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PPJB WHERE NoPPJB = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        #endregion
        #region public static string BAST(int Bulan, int Tahun,string Project)
        public static string BAST(int Bulan, int Tahun, string Project)
        {
            string Kode = "BAST";

            string w1 = "1=1";
            byte ResetNum = ResetNumbering(Kode, Project);
            if (ResetNum == 1) w1 += " AND Month(TglST) = " + Bulan;
            if (ResetNum == 1 || ResetNum == 2) w1 += " AND Year(TglST) = " + Tahun;

            DataTable rs = Db.Rs("SELECT NoST FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_BAST WHERE ST = 'D' AND "
                + w1 + " AND Project = '" + Project + "'"
                );

            int num = rs.Rows.Count + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = PK(Kode, Tahun, Bulan, num, Project);
                if (Db.SingleInteger("SELECT COUNT(NoST) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_BAST WHERE NoST = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        #endregion
        #region public static string AJB(int Bulan, int Tahun,string Project)
        public static string AJB(int Bulan, int Tahun, string Project)
        {
            string Kode = "AJB";

            string w1 = "1=1";
            byte ResetNum = ResetNumbering(Kode, Project);
            if (ResetNum == 1) w1 += " AND Month(TglAJB) = " + Bulan;
            if (ResetNum == 1 || ResetNum == 2) w1 += " AND Year(TglAJB) = " + Tahun;

            DataTable rs = Db.Rs("SELECT NoST FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_AJB WHERE AJB = 'D' AND "
                + w1 + " AND Project = '" + Project + "'"
                );

            int num = rs.Rows.Count + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = PK(Kode, Tahun, Bulan, num, Project);
                if (Db.SingleInteger("SELECT COUNT(NoAJB) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_AJB WHERE NoAJB = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        #endregion
        //Collection
        #region public static string PJT(int Bulan, int Tahun,string Project)
        public static string PJT(int Bulan, int Tahun, string Project)
        {
            string Kode = "PJT";

            string w1 = "1=1";
            byte ResetNum = ResetNumbering(Kode, Project);
            if (ResetNum == 1) w1 += " AND Month(TglPJT) = " + Bulan;
            if (ResetNum == 1 || ResetNum == 2) w1 += " AND Year(TglPJT) = " + Tahun;

            DataTable rs = Db.Rs("SELECT NoPJT FROM " + Mi.DbPrefix + "FINANCEAR..MS_PJT WHERE "
                + w1 + " AND Project = '" + Project + "'"
                );

            int num = rs.Rows.Count + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = PK(Kode, Tahun, Bulan, num, Project);
                if (Db.SingleInteger("SELECT COUNT(NoPJT) FROM " + Mi.DbPrefix + "FINANCEAR..MS_PJT WHERE NoPJT = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        #endregion
        #region public static string ST(int Bulan, int Tahun,string Project)
        public static string ST(int Bulan, int Tahun, string Project)
        {
            string Kode = "ST";

            string w1 = "1=1";
            byte ResetNum = ResetNumbering(Kode, Project);
            if (ResetNum == 1) w1 += " AND Month(TglTunggakan) = " + Bulan;
            if (ResetNum == 1 || ResetNum == 2) w1 += " AND Year(TglTunggakan) = " + Tahun;

            DataTable rs = Db.Rs("SELECT NoTunggakan FROM " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN WHERE "
                + w1 + " AND Project = '" + Project + "'"
                );

            int num = rs.Rows.Count + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = PK(Kode, Tahun, Bulan, num, Project);
                if (Db.SingleInteger("SELECT COUNT(ManualTunggakan) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN WHERE ManualTunggakan = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        #endregion
        #region public static string SKL(int Bulan, int Tahun,string Project)
        public static string SKL(int Bulan, int Tahun, string Project)
        {
            string Kode = "SKL";

            string w1 = "1=1";
            byte ResetNum = ResetNumbering(Kode, Project);
            if (ResetNum == 1) w1 += " AND Month(TglSKL) = " + Bulan;
            if (ResetNum == 1 || ResetNum == 2) w1 += " AND Year(TglSKL) = " + Tahun;

            DataTable rs = Db.Rs("SELECT NoSKLManual FROM " + Mi.DbPrefix + "FINANCEAR..MS_SKL WHERE "
                + w1 + " AND Project = '" + Project + "'"
                );

            int num = rs.Rows.Count + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = PK(Kode, Tahun, Bulan, num, Project);
                if (Db.SingleInteger("SELECT COUNT(NoSKLManual) FROM " + Mi.DbPrefix + "FINANCEAR..MS_SKL WHERE NoSKLManual = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        #endregion
        //Financear
        #region public static string TTS(int Bulan, int Tahun,string Project)
        public static string TTS(int Bulan, int Tahun, string Project)
        {
            string Kode = "TTS";

            string w1 = "1=1";
            byte ResetNum = ResetNumbering(Kode, Project);
            if (ResetNum == 1) w1 += " AND Month(TglTTS) = " + Bulan;
            if (ResetNum == 1 || ResetNum == 2) w1 += " AND Year(TglTTS) = " + Tahun;

            DataTable rs = Db.Rs("SELECT NoTTS2 FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE "
                + w1 + " AND Project = '" + Project + "'"
                );

            int num = rs.Rows.Count + 1;
            bool found = false;
            string pk = "";
            //khusus panahome
           
            while (!found)
            {
                pk = PK(Kode, Tahun, Bulan, num, Project);
                if (Db.SingleInteger("SELECT COUNT(NoTTS2) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS2 = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        #endregion
        #region public static string BKM(int Bulan, int Tahun,string Project)
        public static string BKM(int Bulan, int Tahun, string Project)
        {
            string Kode = "BKM";

            string w1 = "1=1";
            byte ResetNum = ResetNumbering(Kode, Project);
            if (ResetNum == 1) w1 += " AND Month(TglBKM) = " + Bulan;
            if (ResetNum == 1 || ResetNum == 2) w1 += " AND Year(TglBKM) = " + Tahun;

            DataTable rs = Db.Rs("SELECT NoBKM2 FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE Status = 'POST' AND "
                + w1 + " AND Project = '" + Project + "'"
                );

            int num = rs.Rows.Count + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = PK(Kode, Tahun, Bulan, num, Project);
                if (Db.SingleInteger("SELECT COUNT(NoBKM2) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoBKM2 = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        #endregion
        #region public static string MEMO(int Bulan, int Tahun,string Project)
        public static string MEMO(int Bulan, int Tahun, string Project)
        {
            string Kode = "MEMO";

            string w1 = "1=1";
            byte ResetNum = ResetNumbering(Kode, Project);
            if (ResetNum == 1) w1 += " AND Month(TglMemo) = " + Bulan;
            if (ResetNum == 1 || ResetNum == 2) w1 += " AND Year(TglMemo) = " + Tahun;

            DataTable rs = Db.Rs("SELECT NoMEMO2 FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO WHERE "
                + w1 + " AND Project = '" + Project + "'"
                );

            int num = rs.Rows.Count + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = PK(Kode, Tahun, Bulan, num, Project);
                if (Db.SingleInteger("SELECT COUNT(NoMEMO2) FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO WHERE NoMEMO2 = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        #endregion


        //Numerator Builder
        //public static string PK(string Kode, int Count)
        //{
        //    return PK(Kode, 0, 0, Count);
        //}
        public static string PK(string Kode, int Tahun, int Bulan, int Count, string Project)
        {
            string x = "";

            var rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..Numerator WHERE Kode = '" + Kode + "' AND Project = '" + Project + "'");
            if (rs.Rows.Count > 0)
            {
                var r = rs.Rows[0];
                string Pemisah = r["Pemisah"].ToString();

                x = r["Komposisi"].ToString();

                string Prefix = r["Prefix"].ToString();
                string Num = Count.ToString().PadLeft(Convert.ToInt16(r["DigitNum"]), '0');

                string Thn = "";
                if (Tahun != 0)
                {
                    if (Convert.ToInt16(r["FormatTahun"]) == 0) Thn = Tahun.ToString().Substring(2, 2);
                    if (Convert.ToInt16(r["FormatTahun"]) == 1) Thn = Tahun.ToString();
                }

                string Bln = "";
                if (Bulan != 0)
                {
                    if (Convert.ToInt16(r["FormatBulan"]) == 0) Bln = Bulan.ToString().PadLeft(2, '0');
                    if (Convert.ToInt16(r["FormatBulan"]) == 1) Bln = Cf.TglNamaBln(Bulan, true);
                    if (Convert.ToInt16(r["FormatBulan"]) == 2) Bln = Cf.TglNamaBln(Bulan, false).Substring(0, 3).ToUpper();
                }
                //khusus panahome
                string Pers = Db.SingleString("SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project='" + Project + "'");

                x = x.Replace("{Prefix}", Prefix + Pemisah);
                x = x.Replace("{Project}", Pers + Pemisah);

                if (Tahun != 0) x = x.Replace("{Thn}", Thn + Pemisah); else x = x.Replace("{Thn}", "");
                if (Bulan != 0) x = x.Replace("{Bln}", Bln + Pemisah); else x = x.Replace("{Bln}", "");
                x = x.Replace("{Num}", Num + Pemisah);

                if (x.EndsWith(Pemisah)) x = x.Remove(x.Length - 1);
            }

            return x;
        }
    }
}
