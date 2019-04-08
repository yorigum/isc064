using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace ISC064
{
	/// <summary>
	/// Database functions
	/// </summary>
	public class Db
	{
		#region public static string CnnString
		public static string CnnString
		{
			get
			{
				System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
				string x = (string) s.GetValue("cnnString", typeof(string));
				s = null;
				return x;
			}
		}
		#endregion

        #region public static string CnnClient(string Db, string IP, string User, string Pass)
        public static string CnnClient(string Db, string IP, string User, string Pass)
        {
            return "Data Source=" + IP + ",1433;Initial Catalog=" + Db + ";Persist Security Info=True;User ID=" + User + ";Password=" + Pass + ";Connection Timeout=1000";
        }
        #endregion
		#region public static void Execute(string strSql)
		/// <summary>
		/// Execute Sql-Query(strSql) command without result such as INSERT, UPDATE and DELETE.
		/// </summary>
		public static void Execute(string strSql)
		{
            Execute(strSql, CnnString);
		}
        #endregion
        #region public static void Execute(string strSql, string Conn)
        /// <summary>
        /// Execute Sql-Query(strSql) command without result such as INSERT, UPDATE and DELETE.
        /// </summary>
        public static void Execute(string strSql, string Conn)
        {
            SqlConnection sqlCnn = new SqlConnection(Conn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCnn.Close();
        }
        #endregion
        #region public static DataTable Rs(string strSql)
        /// <summary>
        /// Recordset for Sql-Query(strSql) of SELECT.
        /// </summary>
        public static DataTable Rs(string strSql)
		{
            return Rs(strSql, CnnString);
        }
        public static DataTable Rs(string strSql, string Conn)
        {
            SqlConnection sqlCnn = new SqlConnection(Conn);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql, sqlCnn);
            DataSet objDS = new DataSet();
            sqlAdapter.Fill(objDS, "data");
            sqlCnn.Close();

            DataTable rst = new DataTable();
            rst = objDS.Tables["data"];

            return rst;
        }
        #endregion
        #region public static DataTable RsAll(string strSql, string Db, string IP, string User, string Pass)
        public static DataTable RsAll(string strSql, string Db, string IP, string User, string Pass)
        {
            SqlConnection sqlCnn = new SqlConnection(CnnClient(Db, IP, User, Pass));
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql, sqlCnn);
            DataSet objDS = new DataSet();
            sqlAdapter.Fill(objDS, "data");
            sqlCnn.Close();

            DataTable rs = new DataTable();
            rs = objDS.Tables["data"];

            return rs;
        }
        #endregion
        #region public static DataTable xls(string strSql, string path)
        /// <summary>
        /// Recordset for opening excel workbook
        /// </summary>
        public static DataTable xls(string strSql, string path)
		{
			string cnn = "Provider=Microsoft.ACE.OLEDB.12.0;" +
				"Data Source=" + path + ";" +
				"Extended Properties=\"Excel 12.0;HDR=YES;Password=Aivon\"";

			OleDbConnection sqlCnn = new OleDbConnection (cnn);
			OleDbDataAdapter sqlAdapter = new OleDbDataAdapter(strSql,sqlCnn);
			DataSet objDS = new DataSet();
			sqlAdapter.Fill(objDS,"data");
			sqlCnn.Close();

			DataTable rst = new DataTable();
			rst = objDS.Tables["data"];

			return rst;
		}
		#endregion

		#region public static string SingleString(string strSql)
		/// <summary>
		/// Return top data with type of nvarchar,ntext,nchar,varchar,text,char from Sql-Query(strSql).
		/// </summary>
		public static string SingleString(string strSql)
		{
            return SingleString(strSql, CnnString);
		}
        #endregion
        #region public static string SingleString(string strSql, string Conn)
        /// <summary>
        /// Return top data with type of nvarchar,ntext,nchar,varchar,text,char from Sql-Query(strSql).
        /// </summary>
        public static string SingleString(string strSql, string Conn)
        {
            SqlConnection sqlCnn = new SqlConnection(Conn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            string x = "";
            x = (string)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        #endregion
        #region public static int SingleInteger(string strSql)
        /// <summary>
        /// Return top data with type of int from Sql-Query(strSql).
        /// </summary>
        public static int SingleInteger(string strSql)
		{
            return SingleInteger(strSql, CnnString);
		}
        #endregion
        #region public static int SingleInteger(string strSql, string Conn)
        /// <summary>
        /// Return top data with type of int from Sql-Query(strSql).
        /// </summary>
        public static int SingleInteger(string strSql, string Conn)
        {
            SqlConnection sqlCnn = new SqlConnection(Conn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            int x = (int)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        #endregion
        #region public static long SingleLong(string strSql)
        /// <summary>
        /// Return top data with type of bigint from Sql-Query(strSql).
        /// </summary>
        public static long SingleLong(string strSql)
		{
			SqlConnection sqlCnn = new SqlConnection (CnnString);
			SqlCommand sqlCmd = new SqlCommand (strSql, sqlCnn);
			sqlCnn.Open();
			long x = Convert.ToInt64(sqlCmd.ExecuteScalar());
			sqlCnn.Close();

			return x;
		}
		#endregion
		#region public static decimal SingleDecimal(string strSql)
		/// <summary>
		/// Return top data with type of decimal,money from Sql-Query(strSql).
		/// </summary>
		public static decimal SingleDecimal(string strSql)
		{
			SqlConnection sqlCnn = new SqlConnection (CnnString);
			SqlCommand sqlCmd = new SqlCommand (strSql, sqlCnn);
			sqlCnn.Open();
			decimal x = Convert.ToDecimal(sqlCmd.ExecuteScalar());
			sqlCnn.Close();

			return x;
		}
		#endregion
		#region public static bool SingleBool(string strSql)
		/// <summary>
		/// Return top data with type of bit from Sql-Query(strSql).
		/// </summary>
		public static bool SingleBool(string strSql)
		{
			SqlConnection sqlCnn = new SqlConnection (CnnString);
			SqlCommand sqlCmd = new SqlCommand (strSql, sqlCnn);
			sqlCnn.Open();
			bool x = (bool) sqlCmd.ExecuteScalar();
			sqlCnn.Close();

			return x;
		}
		#endregion
		#region public static byte SingleByte(string strSql)
		/// <summary>
		/// Return top data with type of byte from Sql-Query(strSql).
		/// </summary>
		public static byte SingleByte(string strSql)
		{
			SqlConnection sqlCnn = new SqlConnection (CnnString);
			SqlCommand sqlCmd = new SqlCommand (strSql, sqlCnn);
			sqlCnn.Open();
			byte x = (byte) sqlCmd.ExecuteScalar();
			sqlCnn.Close();

			return x;
		}
		#endregion
		#region public static System.DateTime SingleTime(string strSql)
		/// <summary>
		/// Return top data with type of datetime from Sql-Query(strSql).
		/// </summary>
		public static System.DateTime SingleTime(string strSql)
		{
			SqlConnection sqlCnn = new SqlConnection (CnnString);
			SqlCommand sqlCmd = new SqlCommand (strSql, sqlCnn);
			sqlCnn.Open();
			DateTime x = (DateTime) sqlCmd.ExecuteScalar();
			sqlCnn.Close();

			return x;
		}
        #endregion
        #region public static void Fill(DataTable dt, string strSql) // supaya GridView bisa dari DataTable
        public static void Fill(DataTable dt, string strSql)
        {
            SqlConnection sqlCnn = new SqlConnection(CnnString);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

            sqlDa.Fill(dt);
        }
        #endregion
    }
}
