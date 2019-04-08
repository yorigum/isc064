using System;

namespace ISC064
{
	/// <summary>
	/// Manipulate decimal input into indonesian money text.
	/// </summary>
	public class Money
	{
		#region public static string Str(decimal Nilai)
		public static string Str(decimal Nilai)
		{
			bool isNegatif = false;
			if(Nilai<0)
			{
				Nilai = Nilai * -1;
				isNegatif = true;
			}

			string[] nilai = Nilai.ToString("").Split('.');
			
			long absolut = Convert.ToInt64(nilai[0]);
			long desimal = 0;
			try
			{
				desimal = Convert.ToInt64(nilai[1].TrimEnd('0'));
			}
			catch{}

			if(desimal==0)
			{
				if(isNegatif)
					return "MINUS " + Str(absolut);
				else
					return Str(absolut);
			}
			else
			{
				if(isNegatif)
					return "MINUS " + Str(absolut) + " KOMA " + Str(desimal);
				else
					return Str(absolut) + " KOMA " + Str(desimal);
			}
		}
		#endregion
		
		#region private static string Str(long IntA)
		/// <summary>
		/// Convert datainput into money string.
		/// </summary>
		private static string Str(long IntA)
		{
			string cetakAngka = String.Empty;

			if(BacaTigaAngka(System.Convert.ToInt64(AmbilTri(IntA)))!="")
				cetakAngka = cetakAngka + " " + BacaTigaAngka(System.Convert.ToInt64(AmbilTri(IntA))) + " trilyun";

			if(BacaTigaAngka(System.Convert.ToInt64(AmbilMil(IntA)))!="")
				cetakAngka = cetakAngka + " " + BacaTigaAngka(System.Convert.ToInt64(AmbilMil(IntA))) + " milyar";

			if(BacaTigaAngka(System.Convert.ToInt64(AmbilJuta(IntA)))!="")
				cetakAngka = cetakAngka + " " + BacaTigaAngka(System.Convert.ToInt64(AmbilJuta(IntA))) + " juta";

			if(BacaTigaAngka(System.Convert.ToInt64(AmbilRibu(IntA)))!="")
			{
				if (BacaTigaAngka(System.Convert.ToInt64(AmbilRibu(IntA)))==" satu")
					cetakAngka = cetakAngka + " seribu";
				else
					cetakAngka = cetakAngka + " " + BacaTigaAngka(System.Convert.ToInt64(AmbilRibu(IntA))) + " ribu";
			}

			if(BacaTigaAngka(System.Convert.ToInt64(AmbilSisa(IntA)))!="")
				cetakAngka = cetakAngka + " " + BacaTigaAngka(System.Convert.ToInt64(AmbilSisa(IntA)));

			return cetakAngka.ToUpper();
		}
		#endregion
		#region private static string BacaTigaAngka(long IntA)
		private static string BacaTigaAngka(long IntA)
		{
			string cetak = "",x;

			bool flagTerminate = false;

			string temp = LeadingZeros(IntA,3);
			string d1 = temp.Substring(0,1);
			string d2 = temp.Substring(1,1);
			string d3 = temp.Substring(2,1);

			switch(d1)
			{
				case "0": cetak = ""; break;
				case "1": cetak = "seratus"; break;
				case "2": cetak = "dua ratus"; break;
				case "3": cetak = "tiga ratus"; break;
				case "4": cetak = "empat ratus"; break;
				case "5": cetak = "lima ratus"; break;
				case "6": cetak = "enam ratus"; break;
				case "7": cetak = "tujuh ratus"; break;
				case "8": cetak = "delapan ratus"; break;
				case "9": cetak = "sembilan ratus"; break;
			}
			switch(d2)
			{
				case "0": cetak += ""; break;
				case "1":
				switch(d3)
				{
					case "0": cetak += " sepuluh"; break;
					case "1": cetak += " sebelas"; break;
					case "2": cetak += " dua belas"; break;
					case "3": cetak += " tiga belas"; break;
					case "4": cetak += " empat belas"; break;
					case "5": cetak += " lima belas"; break;
					case "6": cetak += " enam belas"; break;
					case "7": cetak += " tujuh belas"; break;
					case "8": cetak += " delapan belas"; break;
					case "9": cetak += " sembilan belas"; break;
				}
					flagTerminate = true; break;
				case "2": cetak += " dua puluh"; break;
				case "3": cetak += " tiga puluh"; break;
				case "4": cetak += " empat puluh"; break;
				case "5": cetak += " lima puluh"; break;
				case "6": cetak += " enam puluh"; break;
				case "7": cetak += " tujuh puluh"; break;
				case "8": cetak += " delapan puluh"; break;
				case "9": cetak += " sembilan puluh"; break;
			}
			if (flagTerminate != true)
			{
				switch(d3)
				{
					case "0": cetak += ""; break;
					case "1": cetak += " satu"; break;
					case "2": cetak += " dua"; break;
					case "3": cetak += " tiga"; break;
					case "4": cetak += " empat"; break;
					case "5": cetak += " lima"; break;
					case "6": cetak += " enam"; break;
					case "7": cetak += " tujuh"; break;
					case "8": cetak += " delapan"; break;
					case "9": cetak += " sembilan"; break;
				}
			}
			x = cetak;
			return x;
		}
		#endregion
		#region private static string AmbilAngka(long intA, int intDigitAkhir)
		private static string AmbilAngka(long intA, int intDigitAkhir)
		{
			long x;
			int posisiangka;
			string temp;

			if(intA.ToString().Length >= (intDigitAkhir))
			{
				posisiangka = intA.ToString().Length - (intDigitAkhir-1);
				temp = intA.ToString().Substring(posisiangka-1,3);
			}
			else if(intA.ToString().Length>(intDigitAkhir-3))
			{
				temp = intA.ToString().Substring(0,((intA.ToString().Length)%3));
			}
			else
			{
				temp = "";
			}

			if(temp!="")
				x = (System.Convert.ToInt64(temp) % 1000);
			else
				x = 0;

			return x.ToString();
		}
		#endregion
		#region private static string AmbilSisa(long IntA)
		private static string AmbilSisa(long IntA)
		{
			return AmbilAngka(IntA,3);
		}
		#endregion
		#region private static string AmbilRibu(long IntA)
		private static string AmbilRibu(long IntA)
		{
			return AmbilAngka(IntA,6);
		}
		#endregion
		#region private static string AmbilJuta(long IntA)
		private static string AmbilJuta(long IntA)
		{
			return AmbilAngka(IntA,9);
		}
		#endregion
		#region private static string AmbilMil(long IntA)
		private static string AmbilMil(long IntA)
		{
			return AmbilAngka(IntA,12);
		}
		#endregion
		#region private static string AmbilTri(long IntA)
		private static string AmbilTri(long IntA)
		{
			return AmbilAngka(IntA,15);
		}
		#endregion
		#region private static string LeadingZeros(long intNumber, int intSize)
		private static string LeadingZeros(long intNumber, int intSize)
		{
			string x = String.Empty;

			if(intSize <= intNumber.ToString().Length)
				x = intNumber.ToString();
			else
				x = "0" + LeadingZeros(intNumber,intSize-1);

			return x;
		}
		#endregion

		#region public static decimal PenaltyNormal(decimal permil, double day, decimal prime)
		public static decimal PenaltyNormal(decimal permil, double day, decimal prime)
		{
			return (permil * prime) * Convert.ToDecimal(day);
		}
		#endregion
		#region public static decimal PenaltyProgressive(decimal permil, double day, decimal prime)
		public static decimal PenaltyProgressive(decimal permil, double day, decimal prime)
		{
			double x = Convert.ToDouble(1) + Convert.ToDouble(permil);
			double pow = Math.Pow(x,day);

			return (Convert.ToDecimal(pow) * prime)
				- prime;
		}
        #endregion




        #region String Conversion English
        public static string StrEng(decimal Nilai, string strMataUang)
        {
            return StrEng(Nilai, strMataUang, true);
        }
        public static string StrEng(decimal Nilai, string MataUang, bool cek)
        {
            bool isNegatif = false;
            if (Nilai < 0)
            {
                Nilai = Nilai * -1;
                isNegatif = true;
            }

            string[] nilai = Nilai.ToString("").Split('.');

            long absolut = Convert.ToInt64(nilai[0]);
            long desimal = 0;
            try
            {
                desimal = Convert.ToInt64(nilai[1].TrimEnd('0'));
            }
            catch { }

            string x = "";
            if (desimal == 0)
            {
                if (isNegatif)
                    x = "MINUS " + StrEng(absolut);
                else
                    x = StrEng(absolut);
            }
            else
            {
                if (isNegatif)
                    x = "MINUS " + StrEng(absolut) + " POINT " + StrEng(desimal);
                else
                    x = StrEng(absolut) + " POINT " + StrEng(desimal);
            }
            return x + " " + MataUang.ToUpper();
        }
        private static string StrEng(long IntA)
        {
            string cetakAngka = String.Empty;

            if (BacaTigaAngkaEng(System.Convert.ToInt64(AmbilTriEng(IntA))) != "")
                cetakAngka = cetakAngka + " " + BacaTigaAngkaEng(System.Convert.ToInt64(AmbilTriEng(IntA))) + " trillion";

            if (BacaTigaAngkaEng(System.Convert.ToInt64(AmbilMil(IntA))) != "")
                cetakAngka = cetakAngka + " " + BacaTigaAngkaEng(System.Convert.ToInt64(AmbilMilEng(IntA))) + " billion";

            if (BacaTigaAngkaEng(System.Convert.ToInt64(AmbilJutaEng(IntA))) != "")
                cetakAngka = cetakAngka + " " + BacaTigaAngkaEng(System.Convert.ToInt64(AmbilJutaEng(IntA))) + " million";

            if (BacaTigaAngkaEng(System.Convert.ToInt64(AmbilRibuEng(IntA))) != "")
            {
                if (BacaTigaAngkaEng(System.Convert.ToInt64(AmbilRibuEng(IntA))) == " one")
                    cetakAngka = cetakAngka + " one thousan";
                else
                    cetakAngka = cetakAngka + " " + BacaTigaAngkaEng(System.Convert.ToInt64(AmbilRibu(IntA))) + " thousand";
            }

            if (BacaTigaAngka(System.Convert.ToInt64(AmbilSisaEng(IntA))) != "")
                cetakAngka = cetakAngka + " " + BacaTigaAngkaEng(System.Convert.ToInt64(AmbilSisaEng(IntA)));

            return cetakAngka.ToUpper();
        }
        private static string BacaTigaAngkaEng(long IntA)
        {
            string cetak = "", x;

            bool flagTerminate = false;

            string temp = LeadingZerosEng(IntA, 3);
            string d1 = temp.Substring(0, 1);
            string d2 = temp.Substring(1, 1);
            string d3 = temp.Substring(2, 1);

            switch (d1)
            {
                case "0": cetak = ""; break;
                case "1": cetak = "one hundred"; break;
                case "2": cetak = "two hundred"; break;
                case "3": cetak = "three hundred"; break;
                case "4": cetak = "four hundred"; break;
                case "5": cetak = "five hundred"; break;
                case "6": cetak = "six hundred"; break;
                case "7": cetak = "seven hundred"; break;
                case "8": cetak = "eight hundred"; break;
                case "9": cetak = "nine hundred"; break;
            }
            switch (d2)
            {
                case "0": cetak += ""; break;
                case "1":
                    switch (d3)
                    {
                        case "0": cetak += " ten"; break;
                        case "1": cetak += " eleven"; break;
                        case "2": cetak += " twelve"; break;
                        case "3": cetak += " thriteen"; break;
                        case "4": cetak += " fourteen"; break;
                        case "5": cetak += " fifteen"; break;
                        case "6": cetak += " sixteen"; break;
                        case "7": cetak += " seventeen"; break;
                        case "8": cetak += " eightteen"; break;
                        case "9": cetak += " nineteen"; break;
                    }
                    flagTerminate = true; break;
                case "2": cetak += " twenty"; break;
                case "3": cetak += " thirty"; break;
                case "4": cetak += " fourty"; break;
                case "5": cetak += " fifty"; break;
                case "6": cetak += " sixty"; break;
                case "7": cetak += " seventy"; break;
                case "8": cetak += " eighty"; break;
                case "9": cetak += " ninety"; break;
            }
            if (flagTerminate != true)
            {
                switch (d3)
                {
                    case "0": cetak += ""; break;
                    case "1": cetak += " one"; break;
                    case "2": cetak += " two"; break;
                    case "3": cetak += " three"; break;
                    case "4": cetak += " four"; break;
                    case "5": cetak += " five"; break;
                    case "6": cetak += " six"; break;
                    case "7": cetak += " seven"; break;
                    case "8": cetak += " eight"; break;
                    case "9": cetak += " nine"; break;
                }
            }
            x = cetak;
            return x;
        }
        private static string AmbilAngkaEng(long intA, int intDigitAkhir)
        {
            long x;
            int posisiangka;
            string temp;

            if (intA.ToString().Length >= (intDigitAkhir))
            {
                posisiangka = intA.ToString().Length - (intDigitAkhir - 1);
                temp = intA.ToString().Substring(posisiangka - 1, 3);
            }
            else if (intA.ToString().Length > (intDigitAkhir - 3))
            {
                temp = intA.ToString().Substring(0, ((intA.ToString().Length) % 3));
            }
            else
            {
                temp = "";
            }

            if (temp != "")
                x = (System.Convert.ToInt64(temp) % 1000);
            else
                x = 0;

            return x.ToString();
        }
        private static string AmbilSisaEng(long IntA)
        {
            return AmbilAngka(IntA, 3);
        }
        private static string AmbilRibuEng(long IntA)
        {
            return AmbilAngka(IntA, 6);
        }
        private static string AmbilJutaEng(long IntA)
        {
            return AmbilAngka(IntA, 9);
        }
        private static string AmbilMilEng(long IntA)
        {
            return AmbilAngka(IntA, 12);
        }
        private static string AmbilTriEng(long IntA)
        {
            return AmbilAngka(IntA, 15);
        }
        private static string LeadingZerosEng(long intNumber, int intSize)
        {
            string x = String.Empty;

            if (intSize <= intNumber.ToString().Length)
                x = intNumber.ToString();
            else
                x = "0" + LeadingZerosEng(intNumber, intSize - 1);

            return x;
        }
        #endregion

    }
}
