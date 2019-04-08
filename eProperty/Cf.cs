using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064
{
    /// <summary>
    /// Common functions
    /// </summary>
    public class Cf
    {
        public static string Bulan(object r)
        {
            if (r is DBNull)
                return "";
            else
            {
                DateTime x = Convert.ToDateTime(r);
                return x.ToString("MM");
            }
        }

        public static string Year(object r)
        {
            if (r is DBNull)
                return "";
            else
            {
                DateTime x = Convert.ToDateTime(r);
                return x.ToString("yy");
            }
        }

        #region public static string Tgl113(DateTime x)
        public static string Tgl113(DateTime x)
        {
            return x.Day.ToString().PadLeft(2, '0')
                + " " + Monthname(x.Month)
                + " " + x.Year;
        }
        #endregion
        public static string MonthnameSingkat(int m)
        {
            string x = "";
            switch (m)
            {
                case 1:
                    x = "Jan";
                    break;
                case 2:
                    x = "Feb";
                    break;
                case 3:
                    x = "Mar";
                    break;
                case 4:
                    x = "Apr";
                    break;
                case 5:
                    x = "Mei";
                    break;
                case 6:
                    x = "Jun";
                    break;
                case 7:
                    x = "Jul";
                    break;
                case 8:
                    x = "Agu";
                    break;
                case 9:
                    x = "Sep";
                    break;
                case 10:
                    x = "Okt";
                    break;
                case 11:
                    x = "Nov";
                    break;
                case 12:
                    x = "Des";
                    break;
            }
            return x;
        }

        #region public static string Tgl114(DateTime x)
        public static string Tgl114(DateTime x)
        {
            return x.Day.ToString().PadLeft(2, '0')
                + " " + MonthnameSingkat(x.Month)
                + " " + x.Year;
        }
        #endregion

        #region public static string Month(object r)
        public static string Month(object r)
        {
            if (r is DBNull)
                return "";
            else
            {
                DateTime x = Convert.ToDateTime(r);
                return x.ToString("MMM yy");
            }
        }
        #endregion

        #region public static string Month2(object r)
        public static string Month2(object r)
        {
            if (r is DBNull)
                return "";
            else
            {
                DateTime x = Convert.ToDateTime(r);
                return x.ToString("MMM");
            }
        }
        #endregion

        #region public static string Periode(object r1, object r2)
        public static string Periode(object r1, object r2)
        {
            if (r1 is DBNull || r2 is DBNull)
                return "";
            else
            {
                DateTime x1 = Convert.ToDateTime(r1);
                DateTime x2 = Convert.ToDateTime(r2);

                if (x1 == x2)
                    return Cf.Day(x1);
                else
                {
                    string s1 = "";
                    string s2 = "";

                    if (x1.Day != 1)
                        s1 = x1.Day + " " + Cf.Month(x1);
                    else
                        s1 = Cf.Month(x1);

                    if (x2.Day != AkhirBulan(x2.Month, x2.Year).Day)
                        s2 = x2.Day + " " + Cf.Month(x2);
                    else
                        s2 = Cf.Month(x2);

                    return s1 + " - " + s2;
                }
            }
        }
        #endregion
        #region public static string Date(object r)
        public static string Date(object r)
        {
            if (r is DBNull)
                return "";
            else
                return Convert.ToDateTime(r).ToString("dd MMM yyyy HH:mm:ss");
        }
        #endregion
        #region public static string Day(object r)
        public static string Day(object r)
        {
            if (r is DBNull)
                return "";
            else
                return Convert.ToDateTime(r).ToString("dd MMM yyyy");
        }
        public static string DayIndo(object r)
        {
            if (r is DBNull)
                return "";
            else
            {
                DateTime Tgl = Convert.ToDateTime(r);

                return Tgl.Day + " " + Monthname(Tgl.Month) + " " + Tgl.Year;
            }

        }
        public static string DayIndoShort(object r)
        {
            if (r is DBNull)
                return "";
            else
            {
                DateTime Tgl = Convert.ToDateTime(r);

                return Tgl.Day + " " + MonthnameShort(Tgl.Month) + " " + Tgl.Year;
            }

        }
        public static string DayEng(object r)
        {
            if (r is DBNull)
                return "";
            else
            {
                DateTime Tgl = Convert.ToDateTime(r);

                return Tgl.Day + " " + MonthnameEnglish(Tgl.Month) + " " + Tgl.Year;
            }

        }
        #endregion
        #region public static string DaySlash(object r)
        public static string DaySlash(object r)
        {
            if (r is DBNull)
                return "";
            else
                return Convert.ToDateTime(r).ToString("dd/MM/yyyy");
        }
        #endregion
        #region public static string DaySlash1(object r)
        public static string DaySlash1(object r)
        {
            if (r is DBNull)
                return "";
            else
                return Convert.ToDateTime(r).ToString("dd/MM/yy");
        }
        #endregion
        #region public static string Time(object r)
        public static string Time(object r)
        {
            if (r is DBNull)
                return "";
            else
                return Convert.ToDateTime(r).ToString("HH:mm:ss");
        }
        #endregion

        #region public static string TglSurat(DateTime x)
        public static string TglSurat(DateTime x)
        {
            return "Hari " + IndoWeek(x).ToUpper() + ", "
                + " Tanggal " + Money.Str(Convert.ToDecimal(x.Day)).ToUpper() + " (" + x.Day + ")"
                + " Bulan " + Monthname(x.Month).ToUpper()
                + " Tahun " + Money.Str(Convert.ToDecimal(x.Year)).ToUpper() + " (" + x.Year + ")"
                ;
        }
        #endregion
        #region public static string Tgl112(DateTime x)
        public static string Tgl112(DateTime x)
        {
            return x.Year
                + x.Month.ToString().PadLeft(2, '0')
                + x.Day.ToString().PadLeft(2, '0');
        }
        #endregion
        #region public static string Tgl(DateTime Input)
        public static string Tgl(DateTime Input)
        {
            return Input.ToString("yyyy-MM-dd");
        }
        #endregion
        #region public static string TglStr(DateTime Input)
        public static string TglStr(DateTime Input)
        {
            return Input.ToString("dd MMM yyyy");
        }
        #endregion
        #region public static string TglJam(DateTime? Input)
        public static string TglJam(DateTime? Input)
        {
            if (Input.HasValue)
            {
                DateTime x = Input.GetValueOrDefault();
                return x.ToString("dd-MM-yyyy HH:mm:ss");
            }
            else
                return "";
        }
        #endregion
        #region public static string TglNamaHari(DateTime Input)
        public static string TglNamaHari(DateTime Input)
        {
            string x = String.Empty;

            switch (Input.DayOfWeek)
            {
                case DayOfWeek.Monday: x = "Monday"; break;
                case DayOfWeek.Tuesday: x = "Tuesday"; break;
                case DayOfWeek.Wednesday: x = "Wednesday"; break;
                case DayOfWeek.Thursday: x = "Thursday"; break;
                case DayOfWeek.Friday: x = "Friday"; break;
                case DayOfWeek.Saturday: x = "Saturday"; break;
                case DayOfWeek.Sunday: x = "Sunday"; break;
            }

            return x;
        }
        #endregion
        #region public static string TglNamaBln(int m, bool Roman)
        public static string TglNamaBln(int m, bool Roman)
        {
            string x = "";
            if (!Roman)
            {
                switch (m)
                {
                    case 1: x = "Januari"; break;
                    case 2: x = "Februari"; break;
                    case 3: x = "Maret"; break;
                    case 4: x = "April"; break;
                    case 5: x = "Mei"; break;
                    case 6: x = "Juni"; break;
                    case 7: x = "Juli"; break;
                    case 8: x = "Agustus"; break;
                    case 9: x = "September"; break;
                    case 10: x = "Oktober"; break;
                    case 11: x = "Nopember"; break;
                    case 12: x = "Desember"; break;
                }
            }
            else
            {
                switch (m)
                {
                    case 1: x = "I"; break;
                    case 2: x = "II"; break;
                    case 3: x = "III"; break;
                    case 4: x = "IV"; break;
                    case 5: x = "V"; break;
                    case 6: x = "VI"; break;
                    case 7: x = "VII"; break;
                    case 8: x = "VIII"; break;
                    case 9: x = "IX"; break;
                    case 10: x = "X"; break;
                    case 11: x = "XI"; break;
                    case 12: x = "XII"; break;
                }
            }
            return x;
        }
        #endregion
        #region public static string IndoWeek(System.DateTime myInput)
        public static string IndoWeek(System.DateTime myInput)
        {
            string x = String.Empty;

            if (myInput.DayOfWeek == DayOfWeek.Monday)
                x = "Senin";
            else if (myInput.DayOfWeek == DayOfWeek.Tuesday)
                x = "Selasa";
            else if (myInput.DayOfWeek == DayOfWeek.Wednesday)
                x = "Rabu";
            else if (myInput.DayOfWeek == DayOfWeek.Thursday)
                x = "Kamis";
            else if (myInput.DayOfWeek == DayOfWeek.Friday)
                x = "Jumat";
            else if (myInput.DayOfWeek == DayOfWeek.Saturday)
                x = "Sabtu";
            else if (myInput.DayOfWeek == DayOfWeek.Sunday)
                x = "Minggu";

            return x;
        }
        #endregion
        //06-02-2018
        #region public static string EnWeek(System.DateTime myInput)
        public static string EnWeek(System.DateTime myInput)
        {
            string x = String.Empty;

            if (myInput.DayOfWeek == DayOfWeek.Monday)
                x = "Monday";
            else if (myInput.DayOfWeek == DayOfWeek.Tuesday)
                x = "Tuesday";
            else if (myInput.DayOfWeek == DayOfWeek.Wednesday)
                x = "Wednesday";
            else if (myInput.DayOfWeek == DayOfWeek.Thursday)
                x = "Thursday";
            else if (myInput.DayOfWeek == DayOfWeek.Friday)
                x = "Friday";
            else if (myInput.DayOfWeek == DayOfWeek.Saturday)
                x = "Saturday";
            else if (myInput.DayOfWeek == DayOfWeek.Sunday)
                x = "Sunday";

            return x;
        }
        #endregion

        #region public static string Monthname(int m)
        public static string Monthname(int m)
        {
            string x = "";
            switch (m)
            {
                case 1:
                    x = "Januari";
                    break;
                case 2:
                    x = "Februari";
                    break;
                case 3:
                    x = "Maret";
                    break;
                case 4:
                    x = "April";
                    break;
                case 5:
                    x = "Mei";
                    break;
                case 6:
                    x = "Juni";
                    break;
                case 7:
                    x = "Juli";
                    break;
                case 8:
                    x = "Agustus";
                    break;
                case 9:
                    x = "September";
                    break;
                case 10:
                    x = "Oktober";
                    break;
                case 11:
                    x = "November";
                    break;
                case 12:
                    x = "Desember";
                    break;
            }
            return x;
        }
        public static string MonthnameEnglish(int m)
        {
            string x = "";
            switch (m)
            {
                case 1:
                    x = "January";
                    break;
                case 2:
                    x = "February";
                    break;
                case 3:
                    x = "March";
                    break;
                case 4:
                    x = "April";
                    break;
                case 5:
                    x = "May";
                    break;
                case 6:
                    x = "June";
                    break;
                case 7:
                    x = "July";
                    break;
                case 8:
                    x = "August";
                    break;
                case 9:
                    x = "September";
                    break;
                case 10:
                    x = "October";
                    break;
                case 11:
                    x = "November";
                    break;
                case 12:
                    x = "December";
                    break;
            }
            return x;
        }

        public static string MonthnameShort(int m)
        {
            string x = "";
            switch (m)
            {
                case 1:
                    x = "Jan";
                    break;
                case 2:
                    x = "Feb";
                    break;
                case 3:
                    x = "Mar";
                    break;
                case 4:
                    x = "Apr";
                    break;
                case 5:
                    x = "Mei";
                    break;
                case 6:
                    x = "Jun";
                    break;
                case 7:
                    x = "Jul";
                    break;
                case 8:
                    x = "Agu";
                    break;
                case 9:
                    x = "Sep";
                    break;
                case 10:
                    x = "Okt";
                    break;
                case 11:
                    x = "Nov";
                    break;
                case 12:
                    x = "Des";
                    break;
            }
            return x;
        }
        #endregion

        #region public static string Monthname(string m)
        public static string Monthname(string m)
        {
            string x = "";
            switch (m)
            {
                case "January":
                    x = "Januari";
                    break;
                case "February":
                    x = "Februari";
                    break;
                case "March":
                    x = "Maret";
                    break;
                case "April":
                    x = "April";
                    break;
                case "May":
                    x = "Mei";
                    break;
                case "June":
                    x = "Juni";
                    break;
                case "July":
                    x = "Juli";
                    break;
                case "August":
                    x = "Agustus";
                    break;
                case "September":
                    x = "September";
                    break;
                case "October":
                    x = "Oktober";
                    break;
                case "November":
                    x = "November";
                    break;
                case "December":
                    x = "Desember";
                    break;
            }
            return x;
        }
        #endregion

        #region public static string NamaBln(string m)
        public static string NamaBln(string m)
        {
            string x = "";
            switch (m)
            {
                case "1":
                    x = "Januari";
                    break;
                case "2":
                    x = "Februari";
                    break;
                case "3":
                    x = "Maret";
                    break;
                case "4":
                    x = "April";
                    break;
                case "5":
                    x = "Mei";
                    break;
                case "6":
                    x = "Juni";
                    break;
                case "7":
                    x = "Juli";
                    break;
                case "8":
                    x = "Agustus";
                    break;
                case "9":
                    x = "September";
                    break;
                case "10":
                    x = "Oktober";
                    break;
                case "11":
                    x = "November";
                    break;
                case "12":
                    x = "Desember";
                    break;
            }
            return x;
        }
        #endregion
        #region public static string Roman(int myInput)
        public static string Roman(int myInput)
        {
            string x = String.Empty;

            switch (myInput)
            {
                case 1:
                    x = "I";
                    break;
                case 2:
                    x = "II";
                    break;
                case 3:
                    x = "III";
                    break;
                case 4:
                    x = "IV";
                    break;
                case 5:
                    x = "V";
                    break;
                case 6:
                    x = "VI";
                    break;
                case 7:
                    x = "VII";
                    break;
                case 8:
                    x = "VIII";
                    break;
                case 9:
                    x = "IX";
                    break;
                case 10:
                    x = "X";
                    break;
                case 11:
                    x = "XI";
                    break;
                case 12:
                    x = "XII";
                    break;
                case 13:
                    x = "XIII";
                    break;
                case 14:
                    x = "XIV";
                    break;
                case 15:
                    x = "XV";
                    break;
                case 16:
                    x = "XVI";
                    break;
                case 17:
                    x = "XVII";
                    break;
                case 18:
                    x = "XVIII";
                    break;
                case 19:
                    x = "XIX";
                    break;
                case 20:
                    x = "XX";
                    break;
                case 21:
                    x = "XXI";
                    break;
                case 22:
                    x = "XXII";
                    break;
                case 23:
                    x = "XXIII";
                    break;
                case 24:
                    x = "XXIV";
                    break;
                case 25:
                    x = "XXV";
                    break;
                case 26:
                    x = "XXVI";
                    break;
                case 27:
                    x = "XXVII";
                    break;
                case 28:
                    x = "XXVIII";
                    break;
                case 29:
                    x = "XXIX";
                    break;
                case 30:
                    x = "XXX";
                    break;
                case 31:
                    x = "XXXI";
                    break;
            }

            return x;
        }
        #endregion
        #region public static void MarkError(TextBox t)
        public static void MarkError(TextBox t)
        {
            t.Attributes["style"] += "background-color:pink;";
        }
        #endregion
        #region public static void MarkError(DropDownList ddl)
        public static void MarkError(DropDownList ddl)
        {
            ddl.Attributes["style"] += "background-color:pink;";
        }
        #endregion
        #region public static void ClrError(TextBox t)
        public static void ClrError(TextBox t)
        {
            if (t.Attributes["style"] != null)
                t.Attributes["style"] = t.Attributes["style"].Replace("background-color:pink;", "");
        }
        #endregion
        #region public static void ClrError(DropDownList t)
        public static void ClrError(DropDownList t)
        {
            if (t.Attributes["style"] != null)
                t.Attributes["style"] = t.Attributes["style"].Replace("background-color:pink;", "");
        }
        #endregion

        #region public static DateTime AwalBulan()
        public static DateTime AwalBulan()
        {
            return AwalBulan(
                DateTime.Today.Month
                , DateTime.Today.Year
                );
        }
        #endregion
        #region public static DateTime AkhirBulan()
        public static DateTime AkhirBulan()
        {
            return AkhirBulan(
                DateTime.Today.Month
                , DateTime.Today.Year
                );
        }
        #endregion
        public static DateTime AwalBulan1(int m, int y,int d)
        {
            return Convert.ToDateTime(
                m
                + "/"
                + d
                + "/"
                + y
                );
        }
        public static DateTime AkhirBulan1(int m, int y)
        {
            return Convert.ToDateTime(
                m
                + "/"
                + DateTime.DaysInMonth(y, m)
                + "/"
                + y
                );
        }
        #region public static DateTime AwalBulan(int m, int y)
        public static DateTime AwalBulan(int m, int y)
        {
            return Convert.ToDateTime(
                m
                + "/1/"
                + y
                );
        }
        #endregion
        #region public static DateTime AkhirBulan(int m, int y)
        public static DateTime AkhirBulan(int m, int y)
        {
            return Convert.ToDateTime(
                m
                + "/"
                + DateTime.DaysInMonth(y, m)
                + "/"
                + y
                );
        }
        #endregion

        #region public static string NumBulat(object r)
        public static string NumBulat(object r)
        {
            if (r is DBNull)
                return "0.00";
            else
            {
                string x = Convert.ToDecimal(r).ToString("N0");

                return x;
            }
        }
        #endregion

        #region public static string Num(object r)
        public static string Num(object r)
        {
            if (r is DBNull)
                return "0.00";
            else
            {
                string x = Convert.ToDecimal(r).ToString("N4");

                if (x.EndsWith("00"))
                    x = Convert.ToDecimal(r).ToString("N2");

                if (x.EndsWith("00"))
                    x = Convert.ToDecimal(r).ToString("N0");

                return x;
            }
        }
        #endregion
        #region public static string NumAbs(object r)
        public static string NumAbs(object r)
        {
            if (r is DBNull)
                return "0.00";
            else
            {
                decimal x = Convert.ToDecimal(r);

                if (x < 0)
                    return "(" + Math.Abs(x).ToString("N2") + ")";
                else
                    return x.ToString("N2");
            }
        }
        #endregion
        #region public static string Pk(object r)
        public static string Pk(object r)
        {
            //Primary key tidak boleh ada karakter single quote (')
            if (r is DBNull)
                return "";
            else
                return r.ToString().Replace("'", "").ToUpper().Trim();
        }
        #endregion
        #region public static string Str(object r)
        public static string Str(object r)
        {
            if (r is DBNull)
                return "";
            else
                return r.ToString().Replace("'", "''").ToUpper().Trim();
        }
        #endregion
        #region public static string StrKet(object r)
        public static string StrKet(object r)
        {
            return r.ToString().Replace("\n", "<br />");
        }
        #endregion
        #region public static string FileSafe(object r)
        public static string FileSafe(object r)
        {
            if (r is DBNull)
                return "";
            else
                return r.ToString()
                    .Replace("/", "")
                    .Replace("\\", "")
                    .Replace("'", "")
                    .Replace("?", "")
                    .Replace("<", "")
                    .Replace(">", "")
                    .Replace("|", "")
                    .Replace("*", "")
                    .Replace(":", "")
                    .ToUpper().Trim()
                    ;
        }
        #endregion
        #region public static string Cut(object r, int len)
        public static string Cut(object r, int len)
        {
            if (r is DBNull)
                return "";
            else
            {
                int l = r.ToString().Length;
                if (l > len)
                    return r.ToString().Substring(0, len) + "...";
                else
                    return r.ToString();
            }
        }
        #endregion
        #region public static string TextToHtml(object r)
        public static string TextToHtml(object r)
        {
            if (r is DBNull)
                return "";
            else
            {
                return Convert.ToString(r).Replace("\n", "<br>");
            }
        }
        #endregion

        #region public static bool isEmpty(TextBox txt)
        public static bool isEmpty(TextBox txt)
        {
            if (txt.Text == "")
                return true;
            else
                return false;
        }
        #endregion
        #region public static bool isInt(TextBox txt)
        public static bool isInt(TextBox txt)
        {
            bool x = true;
            try
            {
                txt.Text = txt.Text.Replace(",", "");
                int z = Convert.ToInt32(txt.Text);
            }
            catch
            {
                x = false;
            }
            return x;
        }
        public static bool isInt(object txt)
        {
            try
            {
                int z = Convert.ToInt32(txt);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region public static bool isMoney(TextBox txt)
        public static bool isMoney(TextBox txt)
        {
            bool x = true;
            try
            {
                decimal z = Convert.ToDecimal(txt.Text);
            }
            catch
            {
                x = false;
            }
            return x;
        }
        #endregion
        #region public static bool isTgl(TextBox txt)
        public static bool isTgl(TextBox txt)
        {
            bool x = true;
            try
            {
                DateTime z = Convert.ToDateTime(txt.Text);
            }
            catch
            {
                x = false;
            }
            return x;
        }
        #endregion
        #region public static bool isTgl(Object)
        public static bool isTgl(object txt)
        {
            bool x = true;
            try
            {
                DateTime z = Convert.ToDateTime(txt);
            }
            catch
            {
                x = false;
            }
            return x;
        }
        #endregion
        #region public static bool isEmail(string t)
        public static bool isEmail(string t)
        {
            Regex re = new Regex(Email);
            return t == "" ? true : re.IsMatch(t);
        }
        #endregion
        #region public static bool isNumerik(string t)
        public static bool isNumerik(string t)
        {
            Regex re = new Regex(Numerik);
            return t == "" ? true : re.IsMatch(t);
        }
        #endregion
        #region public static bool isPilih(CheckBoxList cb)
        public static bool isPilih(CheckBoxList cb)
        {
            bool x = false;
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (cb.Items[i].Selected)
                {
                    x = true;
                    break;
                }
            }
            return x;
        }
        #endregion
        #region public static bool isPilih(DropDownList ddl)
        public static bool isPilih(DropDownList ddl)
        {
            bool x = true;

            if (ddl.SelectedIndex == 0)
                x = false;

            return x;
        }
        #endregion
        #region public static bool isPilih(ListBox list)
        public static bool isPilih(ListBox list)
        {
            bool x = true;

            if (list.SelectedIndex == -1)
                x = false;

            return x;
        }
        #endregion

        #region public static int BoolToSql(bool x)
        public static int BoolToSql(bool x)
        {
            if (x)
                return 1;
            else
                return 0;
        }
        #endregion

        #region public static void BindTahun(DropDownList ddl)
        public static void BindTahun(DropDownList ddl)
        {
            int y = DateTime.Today.Year;

            for (int i = y + 50; i >= y - 50; i--)
            {
                ddl.Items.Add(new ListItem(i.ToString()));
            }
        }
        #endregion
        #region public static void BindTahun2(DropDownList ddl)
        public static void BindTahun2(DropDownList ddl)
        {
            int y = DateTime.Today.Year;

            for (int i = y; i >= y - 50; i--)
            {
                ddl.Items.Add(new ListItem(i.ToString()));
            }
        }
        #endregion
        #region public static void BindBulan(DropDownList ddl)
        public static void BindBulan(DropDownList ddl)
        {
            for (int i = 1; i <= 12; i++)
                ddl.Items.Add(new ListItem(Monthname(i), i.ToString()));
        }
        #endregion
        #region public static void BindBulan(ListBox ddl)
        public static void BindBulan(ListBox ddl)
        {
            for (int i = 1; i <= 12; i++)
                ddl.Items.Add(new ListItem(Monthname(i), i.ToString()));
        }
        #endregion
        #region public static void BindMinggu(ListBox ddl)
        public static void BindMinggu(ListBox ddl)
        {
            ddl.Items.Add(new ListItem("I (1 s/d 7)", "1"));
            ddl.Items.Add(new ListItem("II (8 s/d 15)", "2"));
            ddl.Items.Add(new ListItem("III (16 s/d 23)", "3"));
            ddl.Items.Add(new ListItem("IV (24 s/d EOM)", "4"));
        }
        #endregion
        #region public static void GetMinggu(ListBox minggu)
        public static void GetMinggu(ListBox minggu)
        {
            switch (DateTime.Today.Day)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    minggu.SelectedValue = "1";
                    break;

                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                    minggu.SelectedValue = "2";
                    break;

                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                    minggu.SelectedValue = "3";
                    break;

                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                    minggu.SelectedValue = "4";
                    break;
            }
        }
        #endregion
        #region public static void SelectedValue(DropDownList container, string v)
        public static void SelectedValue(DropDownList container, string v)
        {
            string t = "Sekarang : " + v;
            container.Items.Add(new ListItem(t, v));
            container.SelectedValue = v;
        }
        #endregion

        #region public static string NullMD5()
        public static string NullMD5()
        {
            return "d41d8cd98f00b204e9800998ecf8427e";
        }
        #endregion
        #region public static string Random(int panjang)
        public static string Random(int panjang)
        {
            string _allowedChars = "0123456789";
            Byte[] randomBytes = new Byte[panjang];
            char[] chars = new char[panjang];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < panjang; i++)
            {
                randomizer.NextBytes(randomBytes);
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }
        private static Random randomizer = new Random();
        #endregion

        #region public static string LogCapture(DataTable rs)
        public static string LogCapture(DataTable rs)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            if (rs.Rows.Count != 0)
            {
                for (int i = 0; i < rs.Columns.Count; i++)
                {
                    string data = "";
                    if (rs.Columns[i].DataType.ToString() == "System.Decimal")
                        data = Convert.ToDecimal(rs.Rows[0][i]).ToString("N2");
                    else if (rs.Columns[i].DataType.ToString() == "System.Boolean")
                    {
                        if ((bool)rs.Rows[0][i])
                            data = "YES";
                        else
                            data = "NO";
                    }
                    else
                        data = rs.Rows[0][i].ToString();

                    x.Append(rs.Columns[i].ColumnName + " : ");
                    x.Append(data.Replace("'", "''"));
                    x.Append("<br>");
                }
            }

            return x.ToString();
        }
        #endregion
        #region public static string LogCompare(DataTable bef, DataTable aft)
        public static string LogCompare(DataTable bef, DataTable aft)
        {
            System.Text.StringBuilder xBef = new System.Text.StringBuilder();
            System.Text.StringBuilder xAft = new System.Text.StringBuilder();

            if (bef.Rows.Count != 0 && aft.Rows.Count != 0)
            {
                for (int i = 0; i < bef.Columns.Count; i++)
                {
                    if (bef.Rows[0][i].ToString() != aft.Rows[0][i].ToString())
                    {
                        string data1 = "";
                        string data2 = "";
                        if (bef.Columns[i].DataType.ToString() == "System.Decimal")
                        {
                            data1 = Convert.ToDecimal(bef.Rows[0][i]).ToString("N2");
                            data2 = Convert.ToDecimal(aft.Rows[0][i]).ToString("N2");
                        }
                        else if (bef.Columns[i].DataType.ToString() == "System.Boolean")
                        {
                            if ((bool)bef.Rows[0][i])
                                data1 = "YES";
                            else
                                data1 = "NO";

                            if ((bool)aft.Rows[0][i])
                                data2 = "YES";
                            else
                                data2 = "NO";
                        }
                        else
                        {
                            data1 = bef.Rows[0][i].ToString();
                            data2 = aft.Rows[0][i].ToString();
                        }

                        xBef.Append(bef.Columns[i].ColumnName + " : ");
                        xBef.Append(data1.Replace("'", "''"));
                        xBef.Append("<br>");

                        xAft.Append(aft.Columns[i].ColumnName + " : ");
                        xAft.Append(data2.Replace("'", "''"));
                        xAft.Append("<br>");
                    }
                }
            }

            if (xBef.ToString() != "")
            {
                return "<br>***SEBELUM :<br>"
                    + xBef.ToString()
                    + "<br>***SETELAH :<br>"
                    + xAft.ToString()
                    ;
            }
            else
            {
                return "<br>";
            }
        }
        #endregion
        #region public static string LogCompare(string Before, string After)
        public static string LogCompare(string Before, string After)
        {
            bool Edit = false;
            string[] b = Before.Replace(Environment.NewLine, "~").Split('~');
            string[] a = After.Replace(Environment.NewLine, "~").Split('~');

            StringBuilder lb = new StringBuilder();
            StringBuilder la = new StringBuilder();

            lb.Append("*** SEBELUM : " + Environment.NewLine);
            la.Append("*** SETELAH : " + Environment.NewLine);

            for (int i = 0; i < b.GetUpperBound(0); i++)
            {
                if (b[i] != a[i])
                {
                    Edit = true;
                    lb.Append(b[i] + Environment.NewLine);
                    la.Append(a[i] + Environment.NewLine);
                }
            }

            StringBuilder x = new StringBuilder();
            x.Append(lb);
            x.Append(Environment.NewLine);
            x.Append(la);

            return Edit ? x.ToString() : "";
        }
        #endregion
        #region public static string LogList(DataTable bef, DataTable aft, string Keterangan)
        public static string LogList(DataTable bef, DataTable aft, string Keterangan)
        {
            System.Text.StringBuilder xBef = new System.Text.StringBuilder();
            for (int i = 0; i < bef.Rows.Count; i++)
            {
                string data = bef.Rows[i][0].ToString();
                xBef.Append(data.Replace("'", "''"));
                xBef.Append("<br>");
            }

            System.Text.StringBuilder xAft = new System.Text.StringBuilder();
            for (int i = 0; i < aft.Rows.Count; i++)
            {
                string data = aft.Rows[i][0].ToString();
                xAft.Append(data.Replace("'", "''"));
                xAft.Append("<br>");
            }

            return "<br>***" + Keterangan + " SEBELUM :<br>"
                + xBef.ToString()
                + "<br>***" + Keterangan + " SETELAH :<br>"
                + xAft.ToString()
                ;
        }
        #endregion
        #region public static string LogList(DataTable rs, string Keterangan)
        public static string LogList(DataTable rs, string Keterangan)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string data = rs.Rows[i][0].ToString();
                x.Append(data.Replace("'", "''"));
                x.Append("<br>");
            }

            return "<br>***" + Keterangan + " :<br>"
                + x.ToString()
                ;
        }
        #endregion

        #region public string[] SplitByString(string testString, string split)
        public static string[] SplitByString(string testString, string split)
        {
            int offset = 0;
            int index = 0;
            int[] offsets = new int[testString.Length + 1];

            while (index < testString.Length)
            {
                int indexOf = testString.IndexOf(split, index);
                if (indexOf != -1)
                {
                    offsets[offset++] = indexOf;
                    index = (indexOf + split.Length);
                }
                else
                {
                    index = testString.Length;
                }
            }

            string[] final = new string[offset + 1];
            if (offset == 0)
            {
                final[0] = testString;
            }
            else
            {
                offset--;
                final[0] = testString.Substring(0, offsets[0]);
                for (int i = 0; i < offset; i++)
                {
                    final[i + 1] = testString.Substring(offsets[i] + split.Length, offsets[i + 1] - offsets[i] - split.Length);
                }
                final[offset + 1] = testString.Substring(offsets[offset] + split.Length);
            }
            return final;
        }
        #endregion

        //Monetary system
        #region public static decimal NilaiDPP(decimal pokok, bool include, decimal persen)
        public static decimal NilaiDPP(decimal pokok, bool include)
        {
            if (include)
                return pokok / ((decimal)1.1);
            else
                return pokok;
        }
        #endregion
        #region public static void TaxRounding(ref decimal DPP, ref decimal PPN, bool Include)
        public static void TaxRounding(ref decimal DPP, ref decimal PPN, bool Include)
        {
            if (Include)
            {
                decimal Netto = DPP + PPN;
                PPN = Math.Round(PPN);
                DPP = Netto - PPN;
            }
            else
            {
                PPN = Math.Round(PPN);
            }
        }
        #endregion

        //Validation SECURITY..REF_MANDATORY
        #region public static bool ValidMandatory(Page p, string Halaman)
        public static bool ValidMandatory(Page p, string Halaman, string Project)
        {
            bool x = true;

            DataTable rs = Db.Rs("SELECT NamaKolom, TipeData FROM " + Mi.DbPrefix + "SECURITY..REF_MANDATORY WHERE Halaman = '" + Halaman + "' AND Project='" + Project + "' AND HarusIsi = 1");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string Nama = rs.Rows[i]["NamaKolom"].ToString();
                byte TipeData = Convert.ToByte(rs.Rows[i]["TipeData"]);

                switch (TipeData)
                {
                    case (0): //teks
                        {
                            TextBox tb = (TextBox)p.FindControl(Nama);
                            if (tb != null)
                            {
                                if (Cf.isEmpty(tb))
                                {
                                    Cf.MarkError(tb);
                                    x = false;
                                }
                                else
                                    Cf.ClrError(tb);
                            }
                        }
                        break;
                    case (1): //tanggal
                        {
                            TextBox tb = (TextBox)p.FindControl(Nama);
                            if (tb != null)
                            {
                                if (!Cf.isTgl(tb))
                                {
                                    Cf.MarkError(tb);
                                    x = false;
                                }
                                else
                                    Cf.ClrError(tb);
                            }
                        }
                        break;
                    case (2): //angka
                        {
                            TextBox tb = (TextBox)p.FindControl(Nama);
                            if (tb != null)
                            {
                                if (!Cf.isMoney(tb))
                                {
                                    Cf.MarkError(tb);
                                    x = false;
                                }
                                else
                                    Cf.ClrError(tb);
                            }
                        }
                        break;
                    case (3): //email
                        {
                            TextBox tb = (TextBox)p.FindControl(Nama);
                            if (tb != null)
                            {
                                if (!Cf.isEmail(tb.Text))
                                {
                                    Cf.MarkError(tb);
                                    x = false;
                                }
                                else
                                    Cf.ClrError(tb);
                            }
                        }
                        break;
                    case (4): //numerik
                        {
                            TextBox tb = (TextBox)p.FindControl(Nama);
                            if (tb != null)
                            {
                                if (!Cf.isNumerik(tb.Text))
                                {
                                    Cf.MarkError(tb);
                                    x = false;
                                }
                                else
                                    Cf.ClrError(tb);
                            }
                        }
                        break;
                }
            }

            if (!x)
                Js.Alert(p, "", "Harap lengkapi mandatori yang harus diisi.");

            return x;
        }
        #endregion

        //Regex String
        public static string Email
        {
            get
            {
                return @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            }
        }
        public static string Numerik
        {
            get
            {
                return "^[0-9]*$";
            }
        }

        public static void SetGrid(GridView tb)
        {
            foreach (BoundField f in tb.Columns)
                f.HtmlEncode = false;
        }

        public static bool Valid(Page p, string Halaman, string Project, string tb)
        {
            bool x = true;
                        
            DataTable rs = Db.Rs("SELECT HarusIsi FROM " + Mi.DbPrefix + "SECURITY..REF_MANDATORY WHERE Halaman = '" + Halaman + "' AND Project = '" + Project + "' AND NamaKolom = '" + tb + "'");

            if (rs.Rows.Count == 0)
            {
                x = false;
            }
            else
            {
                if (Convert.ToByte(rs.Rows[0]["HarusIsi"]) == 0)
                {
                    x = false;
                }
            }

            return x;
        }
    }
}
