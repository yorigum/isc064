using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISC064
{
    public class LibKom
    {
        public static string CFID(int Bulan, int Tahun, string Project)
        {
            string Kode = "KOM.CF/" + Project;

            int jum = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_CF WHERE MONTH(Tgl) = " + Bulan + " AND YEAR(Tgl) = " + Tahun + " AND Project = '" + Project + "'");

            int num = jum + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = Kode + "/" + Tahun + "/" + Bulan.ToString().PadLeft(2, '0') + "/" + num.ToString().PadLeft(5, '0');
                if (Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_CF WHERE NoCF = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }

        public static string CFPID(int Bulan, int Tahun, string Project)
        {
            string Kode = "KOM.CFP/" + Project;

            int jum = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_CFP WHERE MONTH(Tgl) = " + Bulan + " AND YEAR(Tgl) = " + Tahun + " AND Project = '" + Project + "'");

            int num = jum + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = Kode + "/" + Tahun + "/" + Bulan.ToString().PadLeft(2, '0') + "/" + num.ToString().PadLeft(5, '0');
                if (Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_CFP WHERE NoCFP = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        public static string CFRID(int Bulan, int Tahun, string Project)
        {
            string Kode = "KOM.CFR/" + Project;

            int jum = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_CFR WHERE MONTH(Tgl) = " + Bulan + " AND YEAR(Tgl) = " + Tahun + " AND Project = '" + Project + "'");

            int num = jum + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = Kode + "/" + Tahun + "/" + Bulan.ToString().PadLeft(2, '0') + "/" + num.ToString().PadLeft(5, '0');
                if (Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_CFR WHERE NoCFR = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        public static string KomisiID(int Bulan, int Tahun, string Project)
        {
            string Kode = "KOM.K/" + Project;

            int jum = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI WHERE MONTH(Tgl) = " + Bulan + " AND YEAR(Tgl) = " + Tahun + " AND Project = '" + Project + "'");

            int num = jum + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = Kode + "/" + Tahun + "/" + Bulan.ToString().PadLeft(2, '0') + "/" + num.ToString().PadLeft(5, '0');
                if (Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI WHERE NoKomisi = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        public static string KPID(int Bulan, int Tahun)
        {
            string Kode = "KOM.KP";

            int jum = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISIP WHERE MONTH(Tgl) = " + Bulan + " AND YEAR(Tgl) = " + Tahun);

            int num = jum + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = Kode + "/" + Tahun + "/" + Bulan.ToString().PadLeft(2, '0') + "/" + num.ToString().PadLeft(5, '0');
                if (Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISIP WHERE NoKomisiP = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        public static string KRID(int Bulan, int Tahun)
        {
            string Kode = "KOM.KR";

            int jum = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISIR WHERE MONTH(Tgl) = " + Bulan + " AND YEAR(Tgl) = " + Tahun);

            int num = jum + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = Kode + "/" + Tahun + "/" + Bulan.ToString().PadLeft(2, '0') + "/" + num.ToString().PadLeft(5, '0');
                if (Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISIR WHERE NoKomisiR = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        public static string RewardID(int Bulan, int Tahun)
        {
            string Kode = "KOM.R";

            int jum = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_REWARD WHERE MONTH(Tgl) = " + Bulan + " AND YEAR(Tgl) = " + Tahun);

            int num = jum + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = Kode + "/" + Tahun + "/" + Bulan.ToString().PadLeft(2, '0') + "/" + num.ToString().PadLeft(5, '0');
                if (Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_REWARD WHERE NoReward = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        public static string RPID(int Bulan, int Tahun)
        {
            string Kode = "KOM.RP";

            int jum = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_REWARD_P WHERE MONTH(Tgl) = " + Bulan + " AND YEAR(Tgl) = " + Tahun);

            int num = jum + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = Kode + "/" + Tahun + "/" + Bulan.ToString().PadLeft(2, '0') + "/" + num.ToString().PadLeft(5, '0');
                if (Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_REWARD_P WHERE NoRP = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
        public static string RRID(int Bulan, int Tahun)
        {
            string Kode = "KOM.RR";

            int jum = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_REWARD_R WHERE MONTH(Tgl) = " + Bulan + " AND YEAR(Tgl) = " + Tahun);

            int num = jum + 1;
            bool found = false;
            string pk = "";

            while (!found)
            {
                pk = Kode + "/" + Tahun + "/" + Bulan.ToString().PadLeft(2, '0') + "/" + num.ToString().PadLeft(5, '0');
                if (Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_REWARD_R WHERE NoRR = '" + pk + "'") == 0)
                    break;

                num++;
            }

            return pk;
        }
    }
}
