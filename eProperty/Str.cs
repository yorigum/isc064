using System;

namespace ISC064
{
	/// <summary>
	/// Fungsi-fungsi spesifik per proyek
	/// </summary>
	public class Str
	{
		#region public static string Lantai(string NoUnit)
		public static string Lantai(string NoUnit)
		{
			try
			{
				string x = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoUnit = '" + NoUnit + "'");
				string Lantai = "";
				string[] arr = x.Split('/');
				Lantai = arr[0];

				return Lantai;
			}
			catch
			{
				return "";
			}

		}
		#endregion
		#region public static string Blok(string NoUnit)
		public static string Blok(string NoUnit)
		{
			try
			{
				string x = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoUnit = '" + NoUnit + "'");
				string Blok = "";
				string[] arr = x.Split('/');
				Blok = arr[1];

				return Blok;
			}
			catch
			{
				return "";
			}
		}
		#endregion
		#region public static string NoUnit(string NoUnit)
		public static string NoUnit(string nounit)
		{
			try
			{
				string x = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoUnit = '" + nounit + "'");
				string NoUnit = "";
				string[] arr = x.Split('/');
				NoUnit = arr[2];

				return NoUnit;
			}
			catch
			{
				return "";
			}
		}
        #endregion
        #region public static string Approval(string Tipe) 
        public static string Approval(string Tipe)
        {
            string Sumber = "";
            switch (Tipe)
            {
                case "1": Sumber = "GN"; break;
                case "2": Sumber = "GU"; break;
                case "3": Sumber = "BATAL"; break;
                case "4": Sumber = "DISKON"; break;
                case "5": Sumber = "ADJUSTMENT"; break;
                case "6": Sumber = "RESCHEDULE"; break;
                case "7": Sumber = "CUSTOM"; break;
            }
            return Sumber;
        }
        #endregion
    }
}
