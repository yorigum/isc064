using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ISC064
{
    public class He //html editor
    {
        public static string HtmlEditor(string Halaman, string Project)
        {
            return Db.SingleString("SELECT ISNULL(Html, '') FROM " + Mi.DbPrefix + "SECURITY..HTMLEDITOR WHERE Halaman = '" + Halaman + "' AND Project = '" + Project + "'");
        }
        public static string Template(string Halaman, string Identity, string Project)
        {
            /*
            ** Rizal
            ** 2018 01 30
            ** 
            ** Param dari Security..HTMLEDITORPARAMETER
            ** Html dari Security..HTMLEDITOR sesuai dengan halaman yang dituju
            */

            int ada = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "SECURITY..HTMLEDITOR WHERE Halaman = '" + Halaman + "' AND Project = '" + Project + "'");
            if (ada == 0)
                return "";

            // Ambil bahan2 yang diperlukan
            DataTable source = Db.Rs(query(Halaman, Identity));
            DataTable Param = parameter(Halaman);
            string Html = Regex.Replace(HtmlEditor(Halaman,Project), @"\n", ""); // Replace whitespace. kalau ngga di giniin, ga bisa buat looping table

            // Perulangan
            Html = Perulangan(Html, source, Param);

            //Load Another Template 
            Html = LoadTemplate(Html, Identity, Project);


            for (int j = 0; j < source.Rows.Count; j++)
            {

                for (int i = 0; i < Param.Rows.Count; i++)
                {
                    string Parameter = Param.Rows[i]["Parameter"].ToString();
                    string Kolom = Param.Rows[i]["Kolom"].ToString();
                    DataColumnCollection adakolom = source.Columns;
                    if (adakolom.Contains(Kolom)) //cek apakah kolom tersebut ada atau tidak
                    {
                        byte TipeData = Convert.ToByte(Param.Rows[i]["TipeData"]);

                        string newString = Format(source.Rows[0][Kolom].ToString(), TipeData);

                        Html = Html.Replace("@@" + Parameter, newString);
                    }
                }
            }

            // Seleksi kondisi
            Html = Kondisi(Html);

            string output = HitungRumus(Html).Replace("<!--", "").Replace("-->", "").Replace("$StartLooping", "").Replace("$EndLooping", "")
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
                    case (3): Hasil = Cf.Day(Convert.ToDateTime(Data)); break; // Tipe Data tanggal
                    case (4): Hasil = Cf.IndoWeek(Convert.ToDateTime(Data)); break; //Tipe Data tanggal, Convert menjadi hari (Contoh : Senin)
                    case (5): Hasil = Money.Str(Convert.ToDecimal(Data)); break; //Terbilang
                    case (6): Hasil = Cf.Date(Convert.ToDateTime(Data)); break;//Tgl dengan format : 01 01 2018 08:05
                    case (7): Hasil = Cf.DayIndo(Convert.ToDateTime(Data)); break; // Tipe Data tanggal
                    case (8): Hasil = Cf.NumBulat(Convert.ToDecimal(Data)); break; // Tipe Data decimal tanpa koma di belakang
                    case (9): Hasil = Money.StrEng(Convert.ToDecimal(Data),"ENG"); break; //Terbilang
                    default: Hasil = Data; break; // kalau Tipe belum ada sama sekali
                }
            }
            catch { Hasil = Data; }
            return Hasil;
        }
        private static string LoadTemplate(string Html, string Identity, string Project)
        {
            var template = Regex.Matches(Html, @"\$StartLoad(.*?)\$EndLoad");
            foreach (Match m in template)
            {
                string tmp = m.Groups[1].Value;
                string NamaTemplate = tmp.Replace("<!--", "").Replace("-->", "").Replace("###", "").Trim();

                Html = Html.Replace("$StartLoad" + tmp + "$EndLoad", Template(NamaTemplate, Identity, Project));
            }

            return Html;
        }
        private static string Perulangan(string Html, DataTable source, DataTable Param)
        {
            string newRows = "";
            string looping = Regex.Match(Html, @"\$StartLooping(.*?)\$EndLooping").Groups[1].Value;

            List<Variabel> summary = new List<Variabel>();

            var newVar = Regex.Matches(Html, @"\$Var\((.*?)\)");

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
                        string Parameter = Param.Rows[i]["Parameter"].ToString();
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

                            r.Value = He.HitungRumus2(rumusan);
                            newRows = newRows.Replace("$" + r.Key + "={{" + m.Groups[1].Value + "}}", "");
                        }

                        newRows = newRows.Replace("$Print(" + r.Key + ")", Cf.Num(r.Value));
                    }
                }

                Html = Html.Replace(looping, newRows);

                foreach (var r in test)
                {
                    Html = Html.Replace("$Print(" + r.Key + ")", Cf.Num(r.Value)).Replace("$Var(" + r.Key + ")", "");
                }
            }
            else
            {
            }

            return Html;
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
                        string[] jika = He.arrIF(kodisi);

                        if (He.Compare(jika[2], jika[0].Trim(), jika[1].Trim()))
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
                        string[] jika = He.arrIF(kodisi);

                        if (He.Compare(jika[2], jika[0].Trim(), jika[1].Trim()))
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
        public static string HitungRumus(string Html)
        {
            string output = Html;

            //Cari teks yg di apit oleh {{ }}
            var formulas = Regex.Matches(Html, @"\{{(.*?)\}}");

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
        public static decimal HitungRumus2(string Html)
        {
            StringToFormula stf = new StringToFormula();

            double hasilHitung = 0;

            try
            {
                hasilHitung = stf.Eval(Html);
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

        private static string query(string halaman, string identity)
        {
            string s = "";

            switch (halaman)
            {
                case ("SuratPesanan"):
                    s = "SELECT "
                        + " Kontrak.NoKontrak"
                        + ",Kontrak.TglKontrak AS Tanggalkontrak"
                        + ",Kontrak.NilaiKontrak"
                        + ",Kontrak.NilaiDPP"
                        + ",Kontrak.CaraBayar"
                        + ",Kontrak.NoUnit"
                        + ",Kontrak.Jenis"
                        + ",Kontrak.SumberDana"
                        + ",Kontrak.TujuanKontrak"
                        + ",FORMAT(Kontrak.Gross,'#,##0.00') AS Gross"
                        + ",FORMAT((Kontrak.Gross - Kontrak.DiskonRupiah),'#,##0.00') AS HargaSepakat"
                        + ",FORMAT(((Kontrak.Gross - Kontrak.DiskonRupiah) + Kontrak.NilaiPPN),'#,##0.00') AS TotalHarga"
                        + ",FORMAT(Kontrak.NilaiPPN,'#,##0.00') AS NilaiPPN "
                        + ",Kontrak.NamaProject AS NamaProject"
                        + ",Kontrak.NamaPers AS NamaPers"
                        + ",Kontrak.Lokasi AS Lokasi"

                        //tagihan dan pelunasan
                        + ",Tag.NamaTagihan"
                        + ",CONVERT(VARCHAR,TglJT,106) AS TanggalJT"
                        + ",FORMAT(NilaiTagihan,'#,###') AS NilaiTagihan"
                        + ",FORMAT((SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = Kontrak.NoKontrak AND Tipe='BF'),'#,##0.00') AS BF"
                        + ",FORMAT((SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = Kontrak.NoKontrak AND Tipe='DP') + (SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = Kontrak.NoKontrak AND Tipe='BF'),'#,##0.00') AS DP"
                        + ",FORMAT((SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = Kontrak.NoKontrak AND Tipe='DP'),'#,##0.00') AS nDP"
                        + ",FORMAT((SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = Kontrak.NoKontrak AND Tipe='ANG'),'#,##0.00') AS Sisa"
                        + ",(SELECT COUNT(*) FROM MS_TAGIHAN WHERE NoKontrak=Kontrak.NoKontrak AND Tipe='DP') AS CountDp"
                        + ",(SELECT CONVERT(VARCHAR,TglJT,106) + ', '  FROM MS_TAGIHAN WHERE NoKontrak = Kontrak.NoKontrak AND Tipe='DP' FOR XML PATH('')) AS Tgl"
                        + ",FORMAT(((SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = Kontrak.NoKontrak AND Tipe='DP') + (SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = Kontrak.NoKontrak AND Tipe='BF')) / Kontrak.NilaiKontrak * 100,'#.##') AS Persen"
                        + ",FORMAT((SELECT NilaiPelunasan FROM MS_PELUNASAN a INNER JOIN MS_TAGIHAN b ON a.NoKontrak = b.NoKontrak AND a.NoTagihan = b.NoUrut INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_TTS c ON a. NoTTS = c.NoTTS WHERE a.NoKontrak = Kontrak.NoKontrak AND b.Tipe='BF' ),'#,###') AS NilaiPelunasan"
                        + ",(SELECT TglPelunasan FROM MS_PELUNASAN a INNER JOIN MS_TAGIHAN b ON a.NoKontrak = b.NoKontrak AND a.NoTagihan = b.NoUrut INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_TTS c ON a. NoTTS = c.NoTTS WHERE a.NoKontrak = Kontrak.NoKontrak AND b.Tipe='BF' ) AS TglPelunasan"
                        + ",(SELECT c.CaraBayar FROM MS_PELUNASAN a INNER JOIN MS_TAGIHAN b ON a.NoKontrak = b.NoKontrak AND a.NoTagihan = b.NoUrut INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_TTS c ON a. NoTTS = c.NoTTS WHERE a.NoKontrak = Kontrak.NoKontrak AND b.Tipe='BF' ) AS CaraBayarLunas"

                        //customer
                        + ",Cus.Nama AS NamaCustomer"
                        + ",Cus.Alamat1 + '<br>' + Cus.Alamat2 + '<br>' + Cus.Alamat3 + '<br>' + Cus.Alamat4 + '<br>' + Cus.Alamat5 AS Alamat"
                        + ",Cus.KTP1 + '<br>' + Cus.KTP2 + '<br>' + Cus.KTP3 + '<br>' + Cus.KTP4 + '<br>' + Cus.KTP5 AS KTPAlamat"
                        + ",Cus.PenanggungjawabKorp AS Penanggungjawab"
                        + ",Cus.JabatanKorp AS JabatanKorp"
                        + ",Cus.NoSKKorp AS NoSKKorp"
                        + ",Cus.Nama2 AS NamaKorp"
                        + ",Cus.BentukKorp AS BentukKorp"
                        + ",Cus.Kewarganegaraan AS Kewarganegaraan"
                        + ",Cus.Pekerjaan AS Pekerjaan"
                        + ",Cus.NoHP AS NoHP"
                        + ",Cus.NoKTP AS NoKTP"
                        + ",Cus.NoTelp AS NoTelp"
                        + ",Cus.NPWP AS NoNPWP"
                        + ",Cus.Email AS Email"

                        //lain-lain
                        + ",(Select count(*) from " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK_LOG where Pk = Kontrak.NoKontrak and Aktivitas = 'APR-GU' or Aktivitas = 'APR-GN') AS KontrakPerubahan"
                        + ",(Select count(*) from " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK_GIMMICK where NoKontrak = Kontrak.NoKontrak) AS CountGimmick"
                        + ",FORMAT((SELECT Luas FROM MS_UNIT WHERE NoStock = Kontrak.NoStock),'#,###') AS LuasNett"
                        + ",FORMAT((SELECT LuasSG FROM MS_UNIT WHERE NoStock = Kontrak.NoStock),'#,###') AS LuasSG"
                        + ",(SELECT Lantai FROM MS_UNIT WHERE NoStock = Kontrak.NoStock) AS Lantai"
                        + ",(SELECT JenisProperti FROM MS_UNIT WHERE NoStock = Kontrak.NoStock) AS Tipe"
                        + ",(SELECT NamaJalan FROM MS_UNIT WHERE NoStock = Kontrak.NoStock) AS Namajalan"
                        + ",(SELECT Lokasi FROM MS_UNIT WHERE NoStock = Kontrak.NoStock) AS Cluster" //beda nama ini

                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK Kontrak"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN Tag ON Tag.NoKontrak = Kontrak.NoKontrak"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_Customer Cus ON Kontrak.NoCustomer = Cus.NoCustomer"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT Ag ON Ag.NoAgent = Kontrak.NoAgent"
                        + " WHERE Kontrak.NoKontrak = '" + identity + "'";
                    break;
                case ("SuratPesananGimmick"):
                    {
                        s = "SELECT "
                            + " a.ItemID AS ItemID"
                            + ",a.Nama AS Nama"
                            + ",a.Stock AS Quantity"
                            + ",a.Satuan AS Satuan"
                            + ", (SELECT Nama From " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE ID = a.Tipe) AS Tipe"
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK_GIMMICK a"
                            + " WHERE a.NoKontrak = '" + identity + "'";
                    }
                    break;
                case ("JadwalTagihan"):
                    s = "SELECT "
                        + " a.NamaTagihan"
                        + ",FORMAT(a.NilaiTagihan,'#,###') AS NilaiTagihan"
                        + ",a.TglJT AS TanggalJT"
                        + ",b.NoKontrak AS NoKontrak"
                        + ",b.TglKontrak AS TglKontrak"
                        + ",b.Skema AS Skema"
                        + ",b.CaraBayar AS CaraBayar"
                        + ",(SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE MS_CUSTOMER.NoCustomer = b.NoCustomer) AS NamaCustomer"
                        + ",b.NoUnit AS NoUnit"
                        + ",FORMAT(b.Gross,'#,###') AS Gross"
                        + ",FORMAT(b.NilaiDPP,'#,###') AS NilaiDPP"
                        + ",b.DiskonRupiah AS Diskon"
                        + ",FORMAT(b.NilaiKontrak,'#,###') AS NilaiKontrak"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON b.NoKontrak = a.NoKontrak"
                        + " WHERE a.NoKontrak = '" + identity + "'";
                    break;
                case ("NUP"):
                    string[] a = identity.Split(':');
                    s = "SELECT CASE WHEN MS_NUP.Revisi > 0 THEN MS_NUP.NoNUP +'R' ELSE MS_NUP.NoNUP END AS NoNUP"
                       + ",MS_NUP.TglDaftar AS TglDaftar"
                       + ",FORMAT((SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNUP = '" + a[0].ToString() + "' AND Tipe = '" + a[1].ToString() + "' AND Project = '" + a[2].ToString() + "'),'#,###') AS NilaiNUP"
                       + ",MS_CUSTOMER.Nama AS Nama"
                       + ",MS_CUSTOMER.Alamat1 + '<br>' +MS_CUSTOMER.Alamat2 + '<br>' +MS_CUSTOMER.Alamat3+ '<br>' +MS_CUSTOMER.Alamat4 AS Alamat"
                       + ",MS_CUSTOMER.NoKTP AS NoKTP"
                       + ",MS_CUSTOMER.KTP1 + '<br>' +MS_CUSTOMER.KTP2 + '<br>' +MS_CUSTOMER.KTP3+ '<br>' +MS_CUSTOMER.KTP4 AS KTPAlamat"
                       + ",MS_CUSTOMER.NoTelp + '/' + MS_CUSTOMER.NoHP AS Telp"
                       + ",MS_AGENT.Nama AS Agent"
                       + ",MS_NUP.Tipe AS Tipe"
                       + ",MS_AGENT.Kontak AS AgentTelp"
                       + ",MS_CUSTOMER.RekNama AS RekNama"
                       + ",MS_CUSTOMER.RekBank AS Bank"
                       + ",MS_CUSTOMER.RekCabang AS Cabang"
                       + ",MS_CUSTOMER.RekNo AS NoRek"
                       + " FROM MS_NUP"
                       + " INNER JOIN MS_CUSTOMER ON MS_NUP.NoCustomer = MS_CUSTOMER.NoCustomer"
                       + " INNER JOIN MS_AGENT ON MS_NUP.NoAgent = MS_AGENT.NoAgent"
                       + " WHERE MS_NUP.NoNUP = '" + a[0].ToString() + "' AND MS_NUP.Tipe = '" + a[1].ToString() + "' AND MS_NUP.Project = '" + a[2].ToString() + "'";
                    break;
                case ("PPJB"):
                    s = "SELECT "
                        + " Kontrak.NoKontrak"
                        + ",Kontrak.CaraBayar"
                        + ",Kontrak.NoPPJB"
                        + ",DAY(Kontrak.TglPPJB) AS [TglPPJB]"
                        + ",MONTH(Kontrak.TglPPJB) AS [BulanPPJB]"
                        + ",YEAR(Kontrak.TglPPJB) AS [TahunPPJB]"
                        + ",TglPPJB AS [TglLengkapPPJB]"
                        + ",Cus.Nama AS [NamaCustomer]"
                        + ",Cus.Alamat1 + ' ' + Cus.Alamat2 + ' ' + Cus.Alamat3 + ' ' + Cus.Alamat4 + ' ' + Cus.Alamat5 AS [Alamat]"
                        + ",Cus.KTP1 + ' ' + Cus.KTP2 + ' ' + Cus.KTP3 + ' ' + Cus.KTP4 + ' ' + Cus.KTP5 AS [KTPAlamat]"
                        + ",Cus.NoHP AS [NoHP]"
                        + ",Cus.NPWP AS [NoNPWP]"
                        + ",Cus.KTP4 AS [KotaLahir]"
                        + ",Cus.TglLahir"
                        + ",Cus.Alamat3 AS [KotaTinggal]"
                        + ",Cus.NoKTP"
                        + ",Cus.TglKTP AS [TglBerlakuKTP]"
                        + ",FORMAT((SELECT LuasNett FROM MS_UNIT WHERE NoStock = Kontrak.NoStock),'#,###') AS LuasNett"
                        + ",FORMAT((SELECT LuasSG FROM MS_UNIT WHERE NoStock = Kontrak.NoStock),'#,###') AS LuasSG"
                        + ",(SELECT Lantai FROM MS_UNIT WHERE NoStock = Kontrak.NoStock) AS Lantai"
                        + ",(SELECT Jenis FROM MS_UNIT WHERE NoStock = Kontrak.NoStock) AS Jenis"
                        + ",(SELECT NamaJalan FROM MS_UNIT WHERE NoStock = Kontrak.NoStock) AS Namajalan"
                        + ",FORMAT(Kontrak.NilaiKontrak,'#,##0') AS NilaiKontrak "
                        + ",FORMAT(Kontrak.NilaiDPP,'#,##0') AS NilaiDPP "
                        + ",FORMAT(Kontrak.HargaTanah,'#,##0') AS HargaTanah "
                        + ",FORMAT(Kontrak.NilaiPPN,'#,##0') AS NilaiPPN "
                        + ",Kontrak.TargetST AS [TargetST]"
                        + ",Kontrak.NoUnit"
                        + ",Kontrak.NoVA"
                        + ",Cus.NoHP AS NoHP"
                        + ",Cus.NoKTP AS NoKTP"
                        + ",Cus.NoTelp AS NoTelp"
                        + ",Cus.NPWP AS NoNPWP"
                        + ",Cus.Email AS Email"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK Kontrak"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_Customer Cus ON Kontrak.NoCustomer = Cus.NoCustomer"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT Ag ON Ag.NoAgent = Kontrak.NoAgent"
                        + " WHERE Kontrak.NoKontrak = '" + identity + "'";
                    break;
                case ("BAST"):
                    s = "SELECT "
                        + " Kontrak.NoKontrak"
                        + ",Kontrak.NoST AS [NoBAST]"
                        + ",Kontrak.TglST AS [TanggalBAST]"
                        + ",Cus.Nama AS [NamaCustomer]"
                        + ",Cus.Alamat1 AS [AlamatCustomer]"
                        + ",Cus.NoKTP"
                        + ",Unit.NoUnit"
                        + ",Unit.Lantai"
                        + ",Unit.Nomor"
                        + ",Unit.Jenis"
                        + ",Unit.LuasSG AS [LuasTanah]"
                        + ",Unit.LuasNett AS [LuasBangunan]"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK Kontrak"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_Customer Cus ON Kontrak.NoCustomer = Cus.NoCustomer"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT Unit ON Kontrak.NoStock = Unit.NoStock"
                        + " WHERE Kontrak.NoKontrak = '" + identity + "'";
                    break;
                case ("WaitingList"):
                    s = "SELECT Res.NoReservasi AS [NoNUP]"
                        + ",Res.NoUrut"
                        + ",Res.Skema AS [CaraBayar]"
                        + ",Res.Tgl"
                        + ",Res.TglExpire"
                        + ",Cus.Nama AS [NamaCustomer]"
                        + ",Cus.Alamat1 AS [AlamatCustomer]"
                        + ",Cus.NoTelp AS [NoTelp]"
                        + ",Unit.NoUnit"
                        + ",Unit.PriceList AS [HargaJual]"
                        + ",Ag.Nama AS [Sales]"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI Res"
                        + " JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER Cus ON Res.NoCustomer = Cus.NoCustomer"
                        + " JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT Unit ON Unit.NoStock = Res.NoStock"
                        + " JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT Ag ON Res.NoAgent = Ag.NoAgent"
                        + " WHERE NoReservasi = " + identity;
                    break;
                case ("BookingForm"):
                    s = "SELECT "
                        + " Res.Gross"
                        + ",Res.DiskonRupiah AS NilaiDiskon"
                        + ",((Res.Gross - Res.DiskonRupiah) * 10/100) AS NilaiPPN"
                        + ",(Res.Gross - Res.DiskonRupiah + (Res.Gross - Res.DiskonRupiah) * 10/100) AS Netto"
                        + ",Res.Netto AS HargaNett"
                        + ",Ske.Nama AS CaraBayar"
                        + ",Ttr.Total AS NilaiBF"
                        + ",Cus.Nama AS NamaCustomer"
                        + ",Cus.Alamat1 AS AlamatSurat"
                        + ",Cus.KTP1 AS AlamatKTP"
                        + ",Cus.NoKTP"
                        + ",Cus.NPWP AS NoNPWP"
                        + ",Cus.NoTelp AS [NoTelp]"
                        + ",Cus.Pekerjaan"
                        + ",Cus.Email"
                        + ",Cus.NoFax AS [Fax]"
                        + ",Cus.NoHP AS [HP]"
                        + ",Cus.NoTelp AS [Telepon]"
                        + ",Cus.Pekerjaan"
                        + ",Cus.NamaBisnis AS [Perusahaan]"
                        + ",Ag.Nama AS Sales"
                        + ",Unit.NoUnit"
                        + ",Unit.Jenis AS [Tipe]"
                        + ",Unit.Lantai"
                        + ",Unit.Luas"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI Res"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER Cus ON Res.NoCustomer = Cus.NoCustomer"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT Unit ON Unit.NoStock = Res.NoStock"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT Ag ON Res.NoAgent = Ag.NoAgent"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TTR Ttr ON Res.NoReservasi = Ttr.NoReservasi"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA Ske ON Res.RefSkema = Ske.Nomor"
                        + " WHERE Res.NoReservasi = " + identity
                        ;
                    break;
                ////FINANCEAR
                case ("Kuitansi"):
                    s = "SELECT "
                        + " TTS.NoBKM AS [NoKuitansi]"
                        + ",TTS.Customer AS [NamaCustomer]"
                        + ",TTS.Total AS [NilaiKuitansi]"
                        + ",TTS.TglBKM AS [TglKuitansi]"
                        + ",CASE TTS.CaraBayar"
                        + "		WHEN 'TN' THEN 'TUNAI'"
                        + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                        + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                        + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                        + "		WHEN 'BG' THEN 'CEK GIRO'"
                        + "		WHEN 'UJ' THEN 'UANG JAMINAN'"
                        + "		WHEN 'DN' THEN 'DISKON'"
                        + " END AS CaraBayar"
                        + ",TTS.Unit"
                        + ",Ac.Rekening AS [NoRekening]"
                        + ",Ac.Bank"
                        + ",TTS.Lb AS [Lb]" //lebih bayar
                        + ",TTS.AdminBank AS [AdminBank]" //Admin Bank
                        + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS TTS"
                        + " INNER JOIN " + Mi.DbPrefix + "FINANCEAR..REF_ACC Ac ON Ac.Acc = TTS.Acc"
                        + " WHERE TTS.NoTTS = " + identity
                        ;
                    break;
                case ("TTS"):
                    s = "SELECT "
                        + " Customer AS [NamaCustomer]"
                        + ",Total AS [Nilai]"
                        + ",Unit"
                        + ",TglTTS"
                        + ",Lb AS [Lb]" //lebih bayar
                        + ",AdminBank AS [AdminBank]" //Admin Bank
                        + ",CASE CaraBayar"
                        + "		WHEN 'TN' THEN 'TUNAI'"
                        + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                        + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                        + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                        + "		WHEN 'BG' THEN 'CEK GIRO'"
                        + "		WHEN 'UJ' THEN 'UANG JAMINAN'"
                        + "		WHEN 'DN' THEN 'DISKON'"
                        + " END AS CaraBayar"
                        + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + identity;
                    break;
                case ("DetilTTS"):
                    s = "SELECT "
                        + " NilaiPelunasan AS Nilai"
                        + ",CONVERT(VARCHAR,NoTagihan) AS NoTagihan"
                        + ",CASE NoTagihan"
                        + "		WHEN 0 THEN 'UNALLOCATED'"
                        + "		ELSE (SELECT NamaTagihan FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
                        + " END AS NamaTagihan"
                        + ", (select Lb from MS_TTS where NoTTS = '" + identity + "') AS Lb" //lebih bayar
                        + ", (select AdminBank from MS_TTS where NoTTS = '" + identity + "') AS AdminBank"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN AS l "
                        + " WHERE NoTTS = " + identity;
                    break;
                case ("DetilKuitansi"):
                    {
                        int nobkm = Db.SingleInteger("SELECT NoBKM FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + identity);
                        string NoKontrak = Db.SingleString("SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + identity);
                        string Ket = Db.SingleString("SELECT Ket FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + identity);

                        //Willy 26 Juni 2018
                        s = "SELECT "
                            + " NilaiPelunasan"
                            + ", '" + Ket + "' AS Keterangan"
                            + ", (SELECT NoUrut From " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak) AS NoUrut"
                            + ",(Select TglBKM FROM MS_TTS WHERE NoBKM = " + nobkm + ") AS TglBKM"
                            + ",CONVERT(VARCHAR,NoTagihan) AS NoTagihan"
                            + ",CASE NoTagihan"
                            + "		WHEN 0 THEN 'UNALLOCATED'"
                            + " ELSE("
                            + " CASE "
                            + " (Select Count(NoTagihan) From ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = l.NoKontrak AND NoTagihan = l.NoTagihan AND NilaiPelunasan != 0) "
                            + " WHEN 1 "
                                + " THEN "
                                    + " CASE(SELECT NilaiTagihan FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak) "
                                      + "   WHEN(SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = l.NoTagihan AND NoKontrak = l.NoKontrak AND NoTTS <= l.NoTTS) "
                                        + " THEN 'PELUNASAN ' + (SELECT NamaTagihan FROM  ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak) "
                                       + "  ELSE "
                                       + "  'SEBAGIAN ' + (SELECT NamaTagihan FROM  ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak) "
                                      + "   END "
                            + " ELSE "
                            + " CASE "
                            + " (SELECT NilaiTagihan FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak) "
                                        + " WHEN(SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = l.NoTagihan AND NoKontrak = l.NoKontrak AND NoTTS <= l.NoTTS) "
                                        + " THEN 'PELUNASAN ' + (SELECT NamaTagihan FROM  ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak) "
                                       + "  ELSE "
                                        + " 'SEBAGIAN ' + (SELECT NamaTagihan FROM  ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak) "
                                        + " END "
                            + " END "
                            + " ) "
                            + " END AS NamaTagihan "

                            //update by Anggi 05-07-2018
                            + ", (select ISNULL(SUM(Lb),0) from MS_TTS where NoTTS = '" + identity + "') AS Lb" //lebih bayar
                            + ", (select ISNULL(SUM(AdminBank),0) from MS_TTS where NoTTS = '" + identity + "' AND NoTTS IN (select NoTTS from MS_MEMO)) AS AdminBank"
                            + ",(SELECT TglJT From " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN Tag WHERE Tag.NoKontrak =l.NoKontrak AND Tag.NoUrut= l.NoTagihan) AS TglJT"
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN AS l "
                            + " WHERE NoTTS IN (SELECT NoTTS FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoBKM = " + nobkm + ")";
                    }
                    break;
                case ("Memo"):
                    s = "SELECT"
                        + " NoMEMO AS [NoMemoSistem]"
                        + ",NoMEMO2 AS [NoMemo]"
                        + ",TglMEMO AS [TglMemo]"
                        + ",Customer AS [NamaCustomer]"
                        + ",Total"
                        + ",Unit AS [NoUnit]"
                        + ",Ket AS [Keterangan]"
                        + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO MEMO"
                        + " WHERE MEMO.NoMemo = " + identity
                        ;
                    break;
                case ("DetilMemo"):
                    {
                        s = "SELECT "
                            + " NilaiPelunasan AS Nilai"
                            + ", (SELECT NoUrut From " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak) AS NoUrut"
                            + ",CASE NoTagihan"
                            + "		WHEN 0 THEN 'UNALLOCATED'"
                            + "		ELSE (SELECT NamaTagihan FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
                            + " END AS NamaTagihan"
                            + ",Tagihan.TglJT"
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN AS l "
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS Tagihan ON Tagihan.NoUrut = l.NoTagihan AND Tagihan.NoKontrak = l.NoKontrak"
                            + " WHERE NoMEMO = " + identity;
                    }
                    break;
                ///COLLECTION
                case ("Invoice"):
                    s = "SELECT "
                        + " Pjt.NoPJT AS [NoInvoice]"
                        + ",Pjt.TglPJT AS [TglInvoice]"
                        + ",Pjt.TglJT"
                        + ",Pjt.Unit AS [NoUnit]"
                        + ",Pjt.Total"
                        + ",Pjt.Customer AS [NamaCustomer]"
                        + ",Pjt.NoTelp"
                        + ",Kontrak.NoKontrak"
                        + ",Kontrak.TglKontrak"
                        + ",Kontrak.NilaiKontrak"
                        + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_PJT Pjt"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK Kontrak ON Kontrak.NoKontrak = Pjt.Ref"
                        + " WHERE NoPJT = '" + identity + "'"
                        ;
                    break;
                case ("DetilInvoice"):
                    s = "SELECT "
                        + " NoUrut"
                        + ",NamaTagihan"
                        + ",Nilai"
                        + ",TglJT"
                        + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_PJT_DETIL"
                        + " WHERE NoPJT = '" + identity + "'"
                        ;
                    break;
                case ("SuratPeringatan"):
                    s = "SELECT "
                        + " Tunggakan.ManualTunggakan as [NoSP]"
                        + ",Tunggakan.Customer as [NamaCustomer]"
                        + ",Tunggakan.Alamat1 as [AlamatCustomer1]"
                        + ",Tunggakan.Alamat2 as [AlamatCustomer2]"
                        + ",Tunggakan.Alamat3 as [AlamatCustomer3]"
                        //+ ",Tunggakan.LevelTunggakan as[LevelTGK]"
                        + ",CASE Tunggakan.LevelTunggakan"
                        + "		WHEN '1' THEN 'Peringatan I'"
                        + "		WHEN '2' THEN 'Peringatan II'"
                        + "		WHEN '3' THEN 'Peringatan III'"
                        + "		WHEN '4' THEN 'Somasi'"
                        + " END AS [SuratPeringatanKe]"
                        + ",Unit.Lokasi + ', ' + Unit.NamaJalan + ', ' + Unit.Jenis + ' ' + Cast(Unit.LuasNett As varchar) + '/' + Cast(Unit.LuasSG As varchar) as [NoUnitJalan]"
                        + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN Tunggakan"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK Kontrak ON Tunggakan.Ref = Kontrak.NoKontrak"
                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT Unit ON Kontrak.NoStock = Unit.NoStock"
                        + " WHERE Tunggakan.NoTunggakan = " + identity;
                    break;
                case ("DetilTunggakan"):
                    {
                        string NoKontrak = Db.SingleString("SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN WHERE NoTunggakan = " + identity);
                        s = "SELECT "
                            + " Tunggakan.NoUrut"
                            + ",Tunggakan.NamaTagihan"
                            + ",Tunggakan.Nilai AS [NilaiTunggakan]"
                            + ",Tunggakan.Telat AS [DendaHari]"
                            + ",Tunggakan.Denda AS [DendaNilai]"
                            + ",Tagihan.NoUrut AS [NoTagihan]"
                            + ",Tagihan.NilaiTagihan"
                            + ",Tagihan.TglJT"
                            + ",FORMAT((SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = Tagihan.NoUrut AND SudahCair !=0),'#,##0.00') AS [NilaiPelunasan]"
                            + ",FORMAT((SELECT CAST(NoBKM AS Int) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = Tagihan.NoUrut AND SudahCair !=0),'#,##') AS [NoBKM]"
                            + ",(select TglBKM from ISC064_FINANCEAR..MS_TTS where NoBKM = (FORMAT((SELECT CAST(NoBKM AS INT) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = Tagihan.NoUrut AND SudahCair != 0),'#,##'))) AS [TglBayar]"
                            + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN_DETIL Tunggakan"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN Tagihan ON Tagihan.NoUrut = Tunggakan.NoTagihan"
                            + " WHERE Tunggakan.NoTunggakan = " + identity
                            + " AND Tagihan.NoKontrak = '" + NoKontrak + "'"
                            ;
                    }
                    break;
                case ("DetilDenda"):
                    {
                        string NoKontrak = Db.SingleString("SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN WHERE NoTunggakan = " + identity);
                        s = "SELECT "
                            + " NamaTagihan"
                            + ",NilaiTagihan"
                            + ",TglJT"
                            + ",Denda"
                            + ",DATEDIFF(d, TglJT, GETDATE()) AS TelatHari"
                            + ",NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) "
                            + "             FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN "
                            + "             WHERE NoTagihan = " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN.NoUrut "
                            + "             AND NoKontrak = '" + NoKontrak + "') AS SisaTagihan"
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN"
                            + " WHERE NoKontrak = '" + NoKontrak + "'"
                            + " And TglJT <= Getdate() ";
                    }
                    break;
                case ("SuratKeteranganLunas"):
                    {
                        s = "SELECT "
                            + " SKL.NoSKL AS [NoSurat]"
                            + ",SKL.TglSKL AS [TanggalSKL]"
                            + ",Kontrak.NilaiKontrak"
                            + ",Kontrak.TglKontrak AS [TanggalKontrak]"
                            + ",Kontrak.NoKontrak"
                            + ",Unit.Lantai"
                            + ",Unit.LuasSG"
                            + ",Unit.NoUnit"
                            + ",Unit.Jenis AS [Tipe]"
                            + ",("
                            + " SELECT TOP 1 Tagihan.TglJT FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN Tagihan"
                            + " WHERE Tagihan.NoKontrak = Kontrak.NoKontrak"
                            + " ORDER BY TglJT DESC"
                            + " ) AS [TanggalLunas]"
                            + ",("
                            + " SELECT TOP 1 Tagihan.NilaiTagihan FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN Tagihan"
                            + " WHERE Tagihan.NoKontrak = Kontrak.NoKontrak"
                            + " ORDER BY TglJT DESC"
                            + " ) AS [TagihanTerakhir]"
                            + ",Customer.Nama AS [NamaCustomer]"
                            + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_SKL SKL "
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK KONTRAK ON KONTRAK.NoKontrak = SKL.Ref"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT UNIT ON Kontrak.NoStock = Unit.NoStock"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER CUSTOMER ON CUSTOMER.NoCustomer = Kontrak.NoCUstomer"
                            + " WHERE SKL.NoSKL = '" + identity + "'"
                            ;
                    }
                    break;
            }

            return s;
        }
        private static DataTable parameter(string halaman)
        {
            return Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..HTMLEDITORPARAMETER"
                + " WHERE Halaman = '" + halaman + "'");
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
            else if (data.Contains("!="))
            {
                result = new List<string>(Cf.SplitByString(data, "!="));
                result.Add("!=");
            }
            else if (data.Contains(">"))
            {
                result = new List<string>(Cf.SplitByString(data, ">"));
                result.Add(">");
            }
            else if (data.Contains(">="))
            {
                result = new List<string>(Cf.SplitByString(data, ">="));
                result.Add(">=");
            }
            else if (data.Contains("<"))
            {
                result = new List<string>(Cf.SplitByString(data, "<"));
                result.Add("<");
            }
            else if (data.Contains("<="))
            {
                result = new List<string>(Cf.SplitByString(data, "<="));
                result.Add("<=");
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
