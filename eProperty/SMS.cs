using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for SMS
/// </summary>

namespace ISC064
{
    public class SMS //SMS Blast
    {
        public static string SMSEditor(string Tipe, string Project)
        {
            return Db.SingleString("SELECT ISNULL(Format, '') FROM " + Mi.DbPrefix + "SECURITY..SmsFormat WHERE Project = '" + Project + "' AND Tipe = '" + Tipe + "'");
        }
        public static string Template(string Tipe, string Identity, string Satuan, decimal Waktu, string Database, string IP, string User, string Pass, string Project)
        {
            int ada = Db.SingleInteger("SELECT COUNT(*) FROM SmsFormat WHERE Project = '" + Project + "' AND Tipe = '" + Tipe + "'");
            if (ada == 0)
                return "";

            // Ambil bahan2 yang diperlukan
            DataTable source = Db.RsAll(query(Tipe, Identity, Satuan, Waktu, Project), Database, IP, User, Pass);
            DataTable Param = parameter(Tipe);
            string SMS = Regex.Replace(SMSEditor(Tipe, Project), @"\n", ""); // Replace whitespace. kalau ngga di giniin, ga bisa buat looping table

            // Perulangan
            SMS = Perulangan(SMS, source, Param);

            // Seleksi kondisi
            SMS = Kondisi(SMS);

            string output = HitungRumus(SMS).Replace("<!--", "").Replace("-->", "").Replace("$StartLooping", "").Replace("$EndLooping", "")
                .Replace("$StartIF", "").Replace("$EndIF", "");

            return output;
        }
        public static string Template2(string Tipe, string Identity, string Satuan, decimal Waktu, string Project)
        {
            int ada = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "SECURITY..SmsFormat WHERE Project = '" + Project + "' AND Tipe = '" + Tipe + "'");
            if (ada == 0)
                return "";

            // Ambil bahan2 yang diperlukan
            DataTable source = Db.Rs(query(Tipe, Identity, Satuan, Waktu, Project));
            DataTable Param = parameter(Tipe);
            string SMS = Regex.Replace(SMSEditor(Tipe, Project), @"\n", ""); // Replace whitespace. kalau ngga di giniin, ga bisa buat looping table

            // Perulangan
            SMS = Perulangan(SMS, source, Param);

            // Seleksi kondisi
            SMS = Kondisi(SMS);

            string output = HitungRumus(SMS).Replace("<!--", "").Replace("-->", "").Replace("$StartLooping", "").Replace("$EndLooping", "")
                .Replace("$StartIF", "").Replace("$EndIF", "");

            return output;
        }

        private static string Format(string Data, byte Tipe)
        {
            string Hasil = "";
            try
            {
                switch (Tipe)
                {
                    case (0): Hasil = Data; break; // Tipe data string
                    case (1): Hasil = Cf.Num(Convert.ToDecimal(Data)); break; // Tipe Data decimal
                    case (2): Hasil = Cf.Num(Convert.ToDouble(Data)); break; // Tipe Data double
                    case (3): Hasil = Cf.TglStr(Convert.ToDateTime(Data)); break; // Tipe Data tanggal
                    case (4): Hasil = Cf.TglNamaHari(Convert.ToDateTime(Data)); break; //Tipe Data tanggal, Convert menjadi hari (Contoh : Senin)
                    case (5): Hasil = Money.Str(Convert.ToDecimal(Data)); break; //Terbilang
                    case (6): Hasil = Cf.TglJam(Convert.ToDateTime(Data)); break;//Tgl dengan format : 01 01 2018 08:05
                    default: Hasil = Data; break; // kalau Tipe belum ada sama sekali
                }
            }
            catch { Hasil = Data; }
            return Hasil;
        }
        private static string Perulangan(string SMS, DataTable source, DataTable Param)
        {
            string newRows = "";
            string looping = Regex.Match(SMS, @"\$StartLooping(.*?)\$EndLooping").Groups[1].Value;

            List<Variabel> summary = new List<Variabel>();

            var newVar = Regex.Matches(SMS, @"\$Var\((.*?)\)");

            List<Variabel> test = new List<Variabel>();
            List<string> key = new List<string>();
            foreach (Match m in newVar)
            {
                test.Add(new Variabel { Key = m.Groups[1].Value, Value = 0 });
                key.Add(m.Groups[1].Value);
            }

            //Buat bikin perulangan
            if (looping.Length > 0)
            {
                for (int j = 0; j < source.Rows.Count; j++)
                {
                    newRows += looping;

                    for (int i = 0; i < Param.Rows.Count; i++)
                    {
                        string Parameter = Param.Rows[i]["Param"].ToString();
                        string Kolom = Param.Rows[i]["Kolom"].ToString();
                        DataColumnCollection adakolom = source.Columns;
                        if (adakolom.Contains(Kolom)) //cek apakah kolom tersebut ada atau tidak
                        {
                            byte TipeData = Convert.ToByte(Param.Rows[i]["TipeData"]);

                            string newString = Format(source.Rows[j][Kolom].ToString(), TipeData);

                            newRows = newRows.Replace("@@" + Parameter, newString);
                        }
                    }
                    foreach (var r in test)
                    {
                        var perhitungan = Regex.Matches(newRows, @"\$" + r.Key + "={{(.*?)}}");
                        foreach (Match m in perhitungan)
                        {
                            string rumusan = m.Groups[1].Value.Replace("$" + r.Key, r.Value.ToString());

                            r.Value = HitungRumus2(rumusan);
                            newRows = newRows.Replace("$" + r.Key + "={{" + m.Groups[1].Value + "}}", "");
                        }

                        newRows = newRows.Replace("$Print(" + r.Key + ")", Cf.Num(r.Value));
                    }
                }

                SMS = SMS.Replace(looping, newRows);

                foreach (var r in test)
                {
                    SMS = SMS.Replace("$Print(" + r.Key + ")", Cf.Num(r.Value)).Replace("$Var(" + r.Key + ")", "");
                }
            }
            else
            {
                for (int j = 0; j < source.Rows.Count; j++)
                {

                    for (int i = 0; i < Param.Rows.Count; i++)
                    {
                        string Parameter = Param.Rows[i]["Param"].ToString();
                        string Kolom = Param.Rows[i]["Kolom"].ToString();
                        DataColumnCollection adakolom = source.Columns;
                        if (adakolom.Contains(Kolom)) //cek apakah kolom tersebut ada atau tidak
                        {
                            byte TipeData = Convert.ToByte(Param.Rows[i]["TipeData"]);

                            string newString = Format(source.Rows[0][Kolom].ToString(), TipeData);

                            SMS = SMS.Replace("@@" + Parameter, newString);
                        }
                    }
                }
            }

            return SMS;
        }
        class Variabel
        {
            public string Key;
            public decimal Value;
        }
        private static string Kondisi(string data)
        {
            var arr = Regex.Matches(data, @"\$StartIF(.*?)\$EndIF");
            foreach (Match m in arr)
            {
                string hasilif = "";
                string kondisi = m.Groups[1].Value;
                string kondisiawal = kondisi;

                bool Found = false;
                while (!Found)
                {
                    int index = kondisi.IndexOf("}}}");
                    if (kondisi.Contains("@@IF"))
                    {
                        string kodisi = Regex.Match(kondisi, @"\((.*?)\)").Groups[1].Value;
                        string[] jika = arrIF(kodisi);

                        if (Compare(jika[2], jika[0].Trim(), jika[1].Trim()))
                        {
                            //Jika benar
                            hasilif = Regex.Match(kondisi, @"\{{{(.*?)\}}}").Groups[1].Value;
                            Found = true;
                        }
                        else
                        {
                            // Jikas salah, hapus kondisi yg salah
                            kondisi = kondisi.Substring(index + 3);
                        }
                    }
                    else if (kondisi.Contains("@@ELSEIF"))
                    {
                        string kodisi = Regex.Match(kondisi, @"\((.*?)\)").Groups[1].Value;
                        string[] jika = arrIF(kodisi);

                        if (Compare(jika[2], jika[0].Trim(), jika[1].Trim()))
                        {
                            //Jika benar
                            hasilif = Regex.Match(kondisi, @"\{{{(.*?)\}}}").Groups[1].Value;
                            Found = true;
                        }
                        else
                        {
                            // Jikas salah, hapus kondisi yg salah
                            kondisi = kondisi.Substring(index + 3);
                        }
                    }
                    else if (kondisi.Contains("@@ELSE"))
                    {
                        string kodisi = Regex.Match(kondisi, @"\((.*?)\)").Groups[1].Value;
                        // Kondisi terakhir

                        hasilif = Regex.Match(kondisi, @"\{{{(.*?)\}}}").Groups[1].Value;
                        Found = true;
                    }
                    else //tidak ada lagi
                    {
                        Found = true;
                        break;
                    }
                }
                data = data.Replace(kondisiawal, hasilif);
            }

            return data;
        }
        /// <summary>
        /// Mohamad Fahrizal
        /// 2018 01 31
        /// Untuk merubah semua rumus, menjadi nilai
        /// Contoh : Nilai PPN adalah : {{ @@NilaiKontrak - @@NilaiDPP }}
        /// Maka akan menghasilkan    :        55.000.000 - 50.000.000
        ///        Hasil Akhir          Nilai PPN adalah : 5.000.000
        ///        
        /// Note : Rumusan perhitungan hanya bisa di eksekusi apabila berada di dalam double kurung kurawal {{ a + b }}
        /// </summary>
        public static string HitungRumus(string SMS)
        {
            string output = SMS;

            //Cari teks yg di apit oleh {{ }}
            var formulas = Regex.Matches(SMS, @"\{{(.*?)\}}");

            if (formulas.Count > 0)
                foreach (Match m in formulas)
                {
                    StringToFormula stf = new StringToFormula();

                    double hasilHitung = 0;

                    try
                    {
                        hasilHitung = stf.Eval(m.Groups[1].Value);
                    }
                    catch { }

                    output = output.Replace(m.Groups[1].Value, Cf.Num(hasilHitung));
                }

            return output.Replace("{{", "").Replace("}}", "");
        }
        public static decimal HitungRumus2(string SMS)
        {
            StringToFormula stf = new StringToFormula();

            double hasilHitung = 0;

            try
            {
                hasilHitung = stf.Eval(SMS);
            }
            catch { }
            if (Double.IsInfinity(hasilHitung))
                hasilHitung = 0;

            return Convert.ToDecimal(hasilHitung);
        }

        /// <summary>
        /// 
        /// Mohamad Fahrizal
        /// 2018 01 31
        /// 
        /// Credit : https://stackoverflow.com/questions/21750824/how-to-convert-a-string-to-a-mathematical-expression-programmatically
        /// 
        /// Untuk generate formula dari text
        /// Contoh di html editor : {{ @@NilaiTagihan - @@NilaiPembayaran }}
        /// Maka function ini akan menghasilkan nilai pengurangan dari nilai tagihan dikurangi nilai pembayaran
        /// </summary>
        public class StringToFormula
        {
            private string[] _operators = { "-", "+", "/", "*", "^" };
            private Func<double, double, double>[] _operations = {
                (a1, a2) => a1 - a2,
                (a1, a2) => a1 + a2,
                (a1, a2) => a1 / a2,
                (a1, a2) => a1 * a2,
                (a1, a2) => Math.Pow(a1, a2)
            };

            public double Eval(string expression)
            {
                List<string> tokens = getTokens(expression);
                Stack<double> operandStack = new Stack<double>();
                Stack<string> operatorStack = new Stack<string>();
                int tokenIndex = 0;

                while (tokenIndex < tokens.Count)
                {
                    string token = tokens[tokenIndex];
                    if (token == "(")
                    {
                        string subExpr = getSubExpression(tokens, ref tokenIndex);
                        operandStack.Push(Eval(subExpr));
                        continue;
                    }
                    if (token == ")")
                    {
                        throw new ArgumentException("Mis-matched parentheses in expression");
                    }
                    //If this is an operator  
                    if (Array.IndexOf(_operators, token) >= 0)
                    {
                        while (operatorStack.Count > 0 && Array.IndexOf(_operators, token) < Array.IndexOf(_operators, operatorStack.Peek()))
                        {
                            string op = operatorStack.Pop();
                            double arg2 = operandStack.Pop();
                            double arg1 = operandStack.Pop();
                            operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
                        }
                        operatorStack.Push(token);
                    }
                    else
                    {
                        operandStack.Push(double.Parse(token));
                    }
                    tokenIndex += 1;
                }

                while (operatorStack.Count > 0)
                {
                    string op = operatorStack.Pop();
                    double arg2 = operandStack.Pop();
                    double arg1 = operandStack.Pop();
                    operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
                }
                return operandStack.Pop();
            }

            private string getSubExpression(List<string> tokens, ref int index)
            {
                StringBuilder subExpr = new StringBuilder();
                int parenlevels = 1;
                index += 1;
                while (index < tokens.Count && parenlevels > 0)
                {
                    string token = tokens[index];
                    if (tokens[index] == "(")
                    {
                        parenlevels += 1;
                    }

                    if (tokens[index] == ")")
                    {
                        parenlevels -= 1;
                    }

                    if (parenlevels > 0)
                    {
                        subExpr.Append(token);
                    }

                    index += 1;
                }

                if ((parenlevels > 0))
                {
                    throw new ArgumentException("Mis-matched parentheses in expression");
                }
                return subExpr.ToString();
            }

            private List<string> getTokens(string expression)
            {
                string operators = "()^*/+-";
                List<string> tokens = new List<string>();
                StringBuilder sb = new StringBuilder();

                foreach (char c in expression.Replace(" ", string.Empty))
                {
                    if (operators.IndexOf(c) >= 0)
                    {
                        if ((sb.Length > 0))
                        {
                            tokens.Add(sb.ToString());
                            sb.Length = 0;
                        }
                        tokens.Add(Convert.ToString(c));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }

                if ((sb.Length > 0))
                {
                    tokens.Add(sb.ToString());
                }
                return tokens;
            }
        }
        private static string kondisi(string kolom, string satuan, int waktu)
        {
            string s = "";

            switch (satuan)
            {
                case ("detik"):
                    s = " AND CONVERT(varchar," + kolom + ",112) = '" + Cf.Tgl112(DateTime.Today.AddSeconds(waktu)) + "'";
                    break;
                case ("menit"):
                    s = " AND CONVERT(varchar," + kolom + ",112) = '" + Cf.Tgl112(DateTime.Today.AddMinutes(waktu)) + "'";
                    break;
                case ("jam"):
                    s = " AND CONVERT(varchar," + kolom + ",112) = '" + Cf.Tgl112(DateTime.Today.AddHours(waktu)) + "'";
                    break;
                case ("hari"):
                    s = " AND CONVERT(varchar," + kolom + ",112) = '" + Cf.Tgl112(DateTime.Today.AddDays(waktu)) + "'";
                    break;
                case ("bulan"):
                    s = " AND CONVERT(varchar," + kolom + ",112) = '" + Cf.Tgl112(DateTime.Today.AddMonths(waktu)) + "'";
                    break;
                case ("tahun"):
                    s = " AND CONVERT(varchar," + kolom + ",112) = '" + Cf.Tgl112(DateTime.Today.AddYears(waktu)) + "'";
                    break;
            }

            return s;
        }
        public static string query(string halaman, string identity, string satuan, decimal waktu, string Project)
        {
            string s = "";

            string w1 = "", w2 = "", w3 = "";
            if (identity != "")
            {
                w1 = " AND b.NoCustomer = '" + identity + "'";
                w2 = " AND TTS.NoTTS = '" + identity + "'";
                w3 = " AND A.NoCustomer = '" + identity + "'";
            }

            switch (halaman)
            {
                case ("INVOICE"):
                    s = "SELECT TOP 2 a.NamaTagihan, a.TglJT, a.NilaiTagihan, c.NoHP AS NomorHP, b.NoUnit, b.NoCustomer, b.NoVA AS VA, c.Nama AS Cust"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER c ON b.NoCustomer = c.NoCustomer"
                        + " WHERE 1=1 "
                        + " AND b.Status = 'A' AND a.KPR=0 "
                        + kondisi("a.TglJT", satuan, Convert.ToInt32(waktu))
                        + w1
                        + " ORDER BY b.NoUnit";
                    break;
                case ("KUITANSI"):
                    s = "SELECT TOP 2"
                        + " TTS.NoBKM AS NoKuitansi"
                        + ",TTS.Customer AS Cust"
                        + ",TTS.Total AS NilaiBayar"
                        + ",TTS.TglBKM AS TglBayar"
                        + ",CASE TTS.CaraBayar"
                        + "		WHEN 'TN' THEN 'TUNAI'"
                        + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                        + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                        + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                        + "		WHEN 'BG' THEN 'CEK GIRO'"
                        + " END AS CaraBayar"
                        + ",TTS.Unit AS NoUnit"
                        + ",Ac.Rekening AS NoRekening"
                        + ",Ac.Bank"
                        + ",d.NoHP AS NomorHP"
                        + ",TTS.NoTTS"
                        + " FROM MS_TTS TTS"
                        + " INNER JOIN REF_ACC Ac ON Ac.Acc = TTS.Acc"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON TTS.Ref = c.NoKontrak"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER d ON c.NoCustomer = d.NoCustomer"
                        + " WHERE 1=1"
                        + kondisi("TTS.TglBKM", satuan, Convert.ToInt32(waktu))
                        + w2
                        ;
                    break;
                case ("ULANGTAHUN"):
                    s = "SELECT TOP 2 A.Nama AS Cust ,A.NoHP AS NomorHP, A.NoCustomer FROM ISC064_MARKETINGJUAL..MS_CUSTOMER A"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK B ON A.NOCUSTOMER = B.NOCUSTOMER"
                        + " WHERE B.STATUS = 'A'"
                        + " AND DAY(A.TglLahir) = CONVERT(VARCHAR,DAY(GETDATE()))"
                        + " AND MONTH(A.TglLahir) = CONVERT(VARCHAR,MONTH(GETDATE()))"
                        + w3
                        + " ORDER BY NoCustomer";
                    break;
            }

            return s;
        }
        private static DataTable parameter(string halaman)
        {
            return Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..SmsParam"
                + " WHERE Tipe = '" + halaman + "'");
        }
        /// <summary>
        /// Compare antara 2 value
        /// </summary>
        public static bool Compare<T>(string op, T x, T y) where T : IComparable
        {
            switch (op)
            {
                case "==": return x.CompareTo(y) == 0;
                case "!=": return x.CompareTo(y) != 0;
                case ">": return x.CompareTo(y) > 0;
                case ">=": return x.CompareTo(y) >= 0;
                case "<": return x.CompareTo(y) < 0;
                case "<=": return x.CompareTo(y) <= 0;
                default: return x.CompareTo(y) == 0;
            }
        }
        /// <summary>
        /// pisahkan antara operator dengan 2 value
        /// </summary>
        /// <returns>
        /// [0] => Value 1
        /// [1] => Value 2
        /// [2] => Operator
        /// </returns>
        public static string[] arrIF(string data)
        {
            List<string> result;
            if (data.Contains("=="))
            {
                result = new List<string>(Cf.SplitByString(data, "=="));
                result.Add("==");
            }
            else
            {
                result = new List<string>(Cf.SplitByString(data, "=="));
                result.Add("==");
            }

            return result.ToArray();
        }
    }
}