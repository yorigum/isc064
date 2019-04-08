using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace ISC064
{
	/// <summary>
	/// Database functions
	/// </summary>
	public class Akun
	{
		public static string CnnString
		{
			get
			{
				System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
				string x = (string) s.GetValue("cnnStringAcc", typeof(string));
				s = null;
				return x;
			}
		}

		#region public static void Execute(string strSql)
		/// <summary>
		/// Execute Sql-Query(strSql) command without result such as INSERT, UPDATE and DELETE.
		/// </summary>
		public static void Execute(string strSql)
		{
			SqlConnection sqlCnn = new SqlConnection (CnnString);
			SqlCommand sqlCmd = new SqlCommand (strSql, sqlCnn);
			sqlCnn.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCnn.Close();
		}
		#endregion
		
		#region public static string SingleString(string strSql)
		/// <summary>
		/// Return top data with type of nvarchar,ntext,nchar,varchar,text,char from Sql-Query(strSql).
		/// </summary>
		public static string SingleString(string strSql)
		{
			SqlConnection sqlCnn = new SqlConnection (CnnString);
			SqlCommand sqlCmd = new SqlCommand (strSql, sqlCnn);
			sqlCnn.Open();
			string x = "";
			x = (string) sqlCmd.ExecuteScalar();
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
			SqlConnection sqlCnn = new SqlConnection (CnnString);
			SqlCommand sqlCmd = new SqlCommand (strSql, sqlCnn);
			sqlCnn.Open();
			int x = (int) sqlCmd.ExecuteScalar();
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

		#region public static DataTable Rs(string strSql)
		/// <summary>
		/// Recordset for Sql-Query(strSql) of SELECT.
		/// </summary>
		public static DataTable Rs(string strSql)
		{
			SqlConnection sqlCnn = new SqlConnection (CnnString);
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql,sqlCnn);
			DataSet objDS = new DataSet();
			sqlAdapter.Fill(objDS,"data");
			sqlCnn.Close();

			DataTable rst = new DataTable();
			rst = objDS.Tables["data"];

			return rst;
		}
		#endregion
		#region public static DataSet Ds(string strSql)
		/// <summary>
		/// DataSet for Sql-Query(strSql) of SELECT.
		/// </summary>
		public static DataSet Ds(string strSql)
		{
			SqlConnection sqlCnn = new SqlConnection (CnnString);
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql,sqlCnn);
			DataSet objDS = new DataSet();
			sqlAdapter.Fill(objDS,"data");

			return objDS;
		}
		#endregion

		#region public static string NewCBID(string AccountID, int Plus)
		public static string NewCBID(string AccountID, int Plus)
		{
			int count = 0;
			string num = "", CBID = "", sVoucher = "";

			if(Plus == 0)
			{
				count = SingleInteger(
					"SELECT COUNT(*) FROM CB WHERE AccountID = '" + AccountID + "' AND Plus = " + Plus);
				num = "W";
				sVoucher = "BK";
			}
			else
			{
				count = SingleInteger(
					"SELECT COUNT(*) FROM CB WHERE AccountID = '" + AccountID + "' AND Plus = " + Plus);
				num = "D";
				sVoucher = "BM";
			}

			bool hasfound = false;
			while(!hasfound)
			{
				count++;
//				CBID = "CB." + AccountID
//					+ "." + num + "." + count.ToString().PadLeft(5, '0');
				CBID = sVoucher + "." + AccountID
					+ "." + num + "." + count.ToString().PadLeft(5, '0');

				if(Akun.SingleInteger("SELECT COUNT(CBID) FROM CB WHERE CBID = '" + CBID + "'") == 0)
					hasfound = true;
			}

			return CBID;
		}
		#endregion
		#region public static string NewJournalID()
		public static string NewJournalID()
		{
			int count = SingleInteger(
				"SELECT COUNT(*) FROM Journal WHERE Source = 'SL'");
			bool hasfound = false;
			string JournalID = "";

			while(!hasfound)
			{
				count++;
				JournalID = "SL." + count.ToString().PadLeft(5, '0');
				
				if(Akun.SingleInteger("SELECT COUNT(JournalID) FROM Journal WHERE JournalID = '" + JournalID + "'") == 0)
					hasfound = true;
			}

			return JournalID;
		}
		#endregion

		#region public static void Journal(string JournalID, string CurrencyID, decimal CurrencyRate, DateTime VoucherDate, string JournalMemo, string Source, string SourceID)
		/// <summary>
		/// Journal
		/// </summary>
		public static void Journal(string JournalID, string CurrencyID, decimal CurrencyRate, DateTime VoucherDate, string JournalMemo, string Source, string SourceID)
		{
			string strSql = "INSERT INTO "
				+ " Journal "
				+ "("
				+ " JournalID"
				+ ",UserID"
				+ ",InputDate"
				+ ",CurrencyID"
				+ ",CurrencyRate"
				+ ",VoucherDate"
				+ ",JournalMemo"
				+ ",Source"
				+ ",SourceID"
				+ ") VALUES ("
				+ " '" + JournalID + "'"
				+ ",'AUTO'"
				+ ",'" + DateTime.Today + "'"
				+ ",'" + CurrencyID + "'"
				+ ", " + CurrencyRate
				+ ",'" + VoucherDate + "'"
				+ ",'" + JournalMemo + "'"
				+ ",'" + Source + "'"
				+ ",'" + SourceID + "'"
				+ ")"
				;

			Execute(strSql);
		}
		#endregion
		#region public static void JournalDetail(string JournalID, int SN, string AccountID, decimal Debit, decimal Credit, decimal ForexDebit, decimal ForexCredit, string Notes, DateTime VoucherDate)
		/// <summary>
		/// JournalDetail
		/// </summary>
		public static void JournalDetail(string JournalID, int SN, string AccountID, decimal Debit, decimal Credit, decimal ForexDebit, decimal ForexCredit, string Notes, DateTime VoucherDate)
		{
			string strSql = "INSERT INTO "
				+ " JournalDetail "
				+ "("
				+ " JournalID"
				+ ",SN"
				+ ",AccountID"
				+ ",Debit"
				+ ",Credit"
				+ ",ForexDebit"
				+ ",ForexCredit"
				+ ",Notes"
				+ ") VALUES ("
				+ " '" + JournalID + "'"
				+ ", " + SN
				+ ",'" + AccountID + "'"
				+ ", " + Debit
				+ ", " + Credit
				+ ", " + ForexDebit
				+ ", " + ForexCredit
				+ ",'" + Notes + "'"
				+ ")"
				;

			Execute(strSql);

			if(SingleBool("SELECT BS FROM Account WHERE AccountID = '"+AccountID+"'"))
			{
				UpdateAccountBalance(
					AccountID
					,Debit
					,Credit
					,VoucherDate.Month
					,VoucherDate.Year
					,true
					);
			}
			else
			{
				string centerid = SingleString(
					"SELECT CenterID FROM Account WHERE AccountID = '"+AccountID+"'");
				string prefix = SingleString(
					"SELECT Prefix FROM Account WHERE AccountID = '"+AccountID+"'");
				
				UpdateAccountProfit(
					AccountID
					,Debit
					,Credit
					,VoucherDate.Month
					,VoucherDate.Year
					,true
					,centerid
					);

				decimal GrossEarning = Credit - Debit;
				decimal OperatingEarning = Credit - Debit;
				decimal NettoEarning = Credit - Debit;

				switch(prefix)
				{
					case "6":
						GrossEarning = 0;
						break;
					case "7":
					case "8":
						OperatingEarning = 0;
						GrossEarning = 0;
						break;
				}

				UpdateEarning(
					VoucherDate.Month
					,VoucherDate.Year
					,NettoEarning
					,GrossEarning
					,OperatingEarning
					,true
					,centerid
					);
			}
		}
		#endregion

		#region public static void CB(string CBID, string CurrencyID, decimal CurrencyRate, DateTime VoucherDate, string JournalMemo, string OtherParty, string AccountID, decimal Deposit, decimal Withdrawal, decimal ForexDeposit, decimal ForexWithdrawal, bool Plus, string SourceID, string CustomerID, string VendorID, bool Direct)
		/// <summary>
		/// CB
		/// </summary>
		public static void CB(string CBID, string CurrencyID, decimal CurrencyRate, DateTime VoucherDate, string JournalMemo, string OtherParty, string AccountID, decimal Deposit, decimal Withdrawal, decimal ForexDeposit, decimal ForexWithdrawal, bool Plus, string SourceID, string CustomerID, string VendorID, bool Direct, string SumberEksternal)
		{
			string strSql = "INSERT INTO "
				+ " CB "
				+ "("
				+ " CBID"
				+ ", UserID"
				+ ", InputDate"
				+ ", CurrencyID"
				+ ", CurrencyRate"
				+ ", VoucherDate"
				+ ", JournalMemo"
				+ ", OtherParty"
				+ ", AccountID"
				+ ", Deposit"
				+ ", Withdrawal"
				+ ", ForexDeposit"
				+ ", ForexWithdrawal"
				+ ", Plus"
				+ ", SourceID"
				+ ", CustomerID"
				+ ", VendorID"
				+ ", Direct"
				+ ", SumberEksternal"
				+ ") VALUES ("
				+ " '" + CBID + "'"
				+ ", 'AUTO'"
				+ ", '" + DateTime.Today + "'"
				+ ", '" + CurrencyID + "'"
				+ ", " + CurrencyRate
				+ ", '" + VoucherDate + "'"
				+ ", '" + JournalMemo + "'"
				+ ", '" + OtherParty + "'"
				+ ", '" + AccountID + "'"
				+ ", " + Deposit
				+ ", " + Withdrawal
				+ ", " + ForexDeposit
				+ ", " + ForexWithdrawal
				+ ", " + BoolToSql(Plus)
				+ ", '" + SourceID + "'"
				+ ", '" + CustomerID + "'"
				+ ", '" + VendorID + "'"
				+ ", " + BoolToSql(Direct)
				+ ", '" + SumberEksternal + "'"
				+ ")"
				;

			Execute(strSql);

			UpdateActualFund(AccountID, (Deposit - Withdrawal));
		}
		#endregion
		#region public static void CBDetail(string CBID, int SN, string AccountID, decimal Debit, decimal Credit, decimal ForexDebit, decimal ForexCredit, string Notes)
		/// <summary>
		/// CBDetail
		/// </summary>
		public static void CBDetail(string CBID, int SN, string AccountID, decimal Debit, decimal Credit, decimal ForexDebit, decimal ForexCredit, string Notes)
		{
			string strSql = "INSERT INTO "
				+ " CBDetail "
				+ "("
				+ " CBID"
				+ ", SN"
				+ ", AccountID"
				+ ", Debit"
				+ ", Credit"
				+ ", ForexDebit"
				+ ", ForexCredit"
				+ ", Notes"
				+ ") VALUES ("
				+ " '" + CBID + "'"
				+ ", " + SN
				+ ",'" + AccountID + "'"
				+ ", " + Debit
				+ ", " + Credit
				+ ", " + ForexDebit
				+ ", " + ForexCredit
				+ ",'" + Notes + "'"
				+ ")"
				;

			Execute(strSql);

//			if(Credit!=0 && Notes.StartsWith("AR : "))
//			{
//				string ARID = Fv.RemoveQuot(Notes.Replace("AR : ",""));
//
//				Db.Execute("UPDATE CBDetail SET ARID = '" + ARID + "'"
//					+ " WHERE CBID = '" + CBID + "'" + " AND SN = " + SN
//					);
//
//				ArDrv.GainLoss(ARID,CBID,ForexCredit,true);
//				ArDrv.Recal(ARID);
//			}
//
//			if(Debit!=0 && Notes.StartsWith("AP : "))
//			{
//				string APID = Fv.RemoveQuot(Notes.Replace("AP : ",""));
//
//				Db.Execute("UPDATE CBDetail SET APID = '" + APID + "'"
//					+ " WHERE CBID = '" + CBID + "'" + " AND SN = " + SN
//					);
//
//				ApDrv.GainLoss(APID,CBID,ForexDebit,true);
//				ApDrv.Recal(APID);
//			}
		}
		#endregion
		#region public static void UpdateActualFund(string AccountID, decimal Netto)
		/// <summary>
		/// Update actual fund
		/// </summary>
		public static void UpdateActualFund(string AccountID, decimal Netto)
		{
			if(Netto > 0)
			{
				Execute("UPDATE Account SET ActualFund = ActualFund + " + Netto
					+ " WHERE AccountID = '" + AccountID + "'");
			}
			else if(Netto < 0)
			{
				Netto = Netto * -1;
				Execute("UPDATE Account SET ActualFund = ActualFund - " + Netto
					+ " WHERE AccountID = '" + AccountID + "'");
			}
		}
		#endregion

		#region public static string BoolToSql(bool myInput)
		public static string BoolToSql(bool myInput)
		{
			if(myInput)
				return "1";
			else
				return "0";
		}
		#endregion

		#region public static bool AccountBalanceExist(string AccountID, int BalanceMonth, int BalanceYear)
		/// <summary>
		/// Account Balance inserted data status
		/// </summary>
		public static bool AccountBalanceExist(string AccountID, int BalanceMonth, int BalanceYear)
		{
			if(SingleInteger(
				"SELECT COUNT(*) FROM AccountBalance WHERE AccountID = '"+AccountID+"'"
				+ " AND BalanceMonth = "+BalanceMonth+" AND BalanceYear = "+BalanceYear)
				!=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
		#region public static bool AccountProfitExist(string AccountID, int ProfitMonth, int ProfitYear, string CenterID)
		/// <summary>
		/// Account Profit inserted data status
		/// </summary>
		public static bool AccountProfitExist(string AccountID, int ProfitMonth, int ProfitYear, string CenterID)
		{
			if(SingleInteger(
				"SELECT COUNT(*) FROM AccountProfit WHERE AccountID = '"+AccountID+"' AND CenterID = '"+CenterID+"'"
				+ " AND ProfitMonth = "+ProfitMonth+" AND ProfitYear = "+ProfitYear)
				!=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
		#region public static bool EarningBalanceExist(int EarningMonth, int EarningYear, string CenterID)
		/// <summary>
		/// Earning Balance inserted data status
		/// </summary>
		public static bool EarningBalanceExist(int EarningMonth, int EarningYear, string CenterID)
		{
			if(SingleInteger(
				"SELECT COUNT(*) FROM Earning WHERE "
				+ " EarningMonth = " + EarningMonth
				+ " AND EarningYear = " + EarningYear
				+ " AND CenterID = '" + CenterID + "'")
				!=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

		#region public static void InsertAccountBalance(string AccountID, int BalanceMonth, int BalanceYear)
		/// <summary>
		/// Insert into AccountBalance
		/// </summary>
		public static void InsertAccountBalance(string AccountID, int BalanceMonth, int BalanceYear)
		{
			decimal Balance = AccountBalanceAsof(AccountID,BalanceMonth,BalanceYear);
			if(AccountE == AccountID)
			{
				Balance = Balance - EarningAsof(BalanceMonth,BalanceYear,"#","Amount");
			}

			string strSql = "INSERT INTO "
				+ " AccountBalance "
				+ "("
				+ " AccountID"
				+ ",BalanceMonth"
				+ ",BalanceYear"
				+ ",Balance"
				+ ") VALUES ("
				+ " '" + AccountID + "'"
				+ ", " + BalanceMonth
				+ ", " + BalanceYear
				+ ", " + Balance
				+ ")"
				;

			Execute(strSql);
		}
		#endregion
		#region public static void InsertAccountProfit(string AccountID, int ProfitMonth, int ProfitYear, string CenterID)
		/// <summary>
		/// Insert into AccountProfit
		/// </summary>
		public static void InsertAccountProfit(string AccountID, int ProfitMonth, int ProfitYear, string CenterID)
		{
			string strSql = "INSERT INTO "
				+ " AccountProfit "
				+ "("
				+ " AccountID"
				+ ",ProfitMonth"
				+ ",ProfitYear"
				+ ",CenterID"
				+ ") VALUES ("
				+ " '" + AccountID + "'"
				+ ", " + ProfitMonth
				+ ", " + ProfitYear
				+ ",'" + CenterID + "'"
				+ ")"
				;

			Execute(strSql);
		}
		#endregion
		#region public static void InsertEarning(int EarningMonth, int EarningYear, string CenterID)
		/// <summary>
		/// Insert into Earning
		/// </summary>
		public static void InsertEarning(int EarningMonth, int EarningYear, string CenterID)
		{
			string strSql = "INSERT INTO "
				+ " Earning "
				+ "("
				+ " EarningMonth"
				+ ",EarningYear"
				+ ",CenterID"
				+ ") VALUES ("
				+ "  " + EarningMonth
				+ ", " + EarningYear
				+ ",'" + CenterID + "'"
				+ ")"
				;

			Execute(strSql);
		}
		#endregion
		
		#region private static decimal TotalDebit(string AccountID, int MonthFrom, int YearFrom, int MonthTo, int YearTo)
		/// <summary>
		/// Get total debit
		/// </summary>
		private static decimal TotalDebit(string AccountID, int MonthFrom, int YearFrom, int MonthTo, int YearTo)
		{
			return SingleDecimal("SELECT"
				+ " ISNULL("
				+ "		(SELECT SUM(TotalDebit) FROM AccountBalance"
				+ "		WHERE "
				+ "		AccountID = '"+AccountID+"' "
				+ "		AND"
				+ "			("
				+ "				(BalanceYear > "+YearFrom+")"
				+ "				OR (BalanceYear = "+YearFrom+" AND BalanceMonth >= "+MonthFrom+")"
				+ "			)"
				+ "		AND"
				+ "			("
				+ "				(BalanceYear < "+YearTo+")"
				+ "				OR (BalanceYear = "+YearTo+" AND BalanceMonth <= "+MonthTo+")"
				+ "			)"
				+ "		)"
				+ " ,0)"
				);
		}	
		#endregion
		#region private static decimal TotalCredit(string AccountID, int MonthFrom, int YearFrom, int MonthTo, int YearTo)
		/// <summary>
		/// Get total credit
		/// </summary>
		private static decimal TotalCredit(string AccountID, int MonthFrom, int YearFrom, int MonthTo, int YearTo)
		{
			return SingleDecimal("SELECT"
				+ " ISNULL("
				+ "		(SELECT SUM(TotalCredit) FROM AccountBalance"
				+ "		WHERE "
				+ "		AccountID = '"+AccountID+"' "
				+ "		AND"
				+ "			("
				+ "				(BalanceYear > "+YearFrom+")"
				+ "				OR (BalanceYear = "+YearFrom+" AND BalanceMonth >= "+MonthFrom+")"
				+ "			)"
				+ "		AND"
				+ "			("
				+ "				(BalanceYear < "+YearTo+")"
				+ "				OR (BalanceYear = "+YearTo+" AND BalanceMonth <= "+MonthTo+")"
				+ "			)"
				+ "		)"
				+ " ,0)"
				);
		}	
		#endregion
		
		#region public static decimal AccountBalance(string AccountID, int m, int y)
		/// <summary>
		/// Get account balance [exact]
		/// </summary>
		public static decimal AccountBalance(string AccountID, int m, int y)
		{
			return SingleDecimal("SELECT"
				+ " Balance"
				+ " FROM AccountBalance "
				+ " WHERE"
				+ " AccountID = '" + AccountID + "'"
				+ " AND BalanceMonth = " + m
				+ " AND BalanceYear = " + y
				);
		}
		#endregion
		#region public static decimal AccountBalanceAsof(string AccountID, int BalanceMonth, int BalanceYear)
		/// <summary>
		/// Get account balance (AS OF)
		/// </summary>
		public static decimal AccountBalanceAsof(string AccountID, int BalanceMonth, int BalanceYear)
		{
			decimal x = SingleDecimal("SELECT"
				+ " ISNULL("
				+ "		(SELECT TOP 1 Balance FROM AccountBalance"
				+ "		WHERE "
				+ "		AccountID = '"+AccountID+"' "
				+ "		AND"
				+ "			("
				+ "				(BalanceYear < " + BalanceYear + ")"
				+ "				OR (BalanceYear = "+BalanceYear+" AND BalanceMonth <= "+BalanceMonth+")"
				+ "			)"
				+ "		ORDER BY BalanceYear DESC, BalanceMonth DESC"
				+ "		)"
				+ " ,0)"
				);

			if(AccountE == AccountID)
				x = x + EarningAsof(BalanceMonth,BalanceYear,"#","Amount");
			
			else if(AccountCE == AccountID)
				x = x + EarningPeriod("#",1,BalanceYear,BalanceMonth,BalanceYear,"Amount");
			
			else if(AccountRE == AccountID)
				x = x + EarningAsof(12,BalanceYear-1,"#","Amount");

			return x;
		}	
		#endregion

		#region public static decimal AccountProfitNettoPeriod(string AccountID, string CenterID, int MonthFrom, int YearFrom, int MonthTo, int YearTo)
		/// <summary>
		/// Get netto amount from AccountProfit by period
		/// </summary>
		public static decimal AccountProfitNettoPeriod(string AccountID, string CenterID, int MonthFrom, int YearFrom, int MonthTo, int YearTo)
		{
			string addq = "";
			if(CenterID!="#")
				addq = " CenterID = '"+CenterID+"' AND ";

			return SingleDecimal("SELECT"
				+ " ISNULL("
				+ "		(SELECT SUM(Netto) FROM AccountProfit"
				+ "		WHERE "+addq+" AccountID = '"+AccountID+"' AND"
				+ "			("
				+ "				(ProfitYear > "+YearFrom+")"
				+ "				OR (ProfitYear = "+YearFrom+" AND ProfitMonth >= "+MonthFrom+")"
				+ "			)"
				+ "		AND"
				+ "			("
				+ "				(ProfitYear < "+YearTo+")"
				+ "				OR (ProfitYear = "+YearTo+" AND ProfitMonth <= "+MonthTo+")"
				+ "			)"
				+ "		)"
				+ " ,0)"
				);
		}	
		#endregion
		
		#region public static decimal Earning(int m, int y, string CenterID, string Field)
		/// <summary>
		/// Get earning balance [exact]
		/// </summary>
		public static decimal Earning(int m, int y, string CenterID, string Field)
		{
			string addq = "";
			if(CenterID!="#")
				addq = " AND CenterID = '"+CenterID+"'";

			return SingleDecimal("SELECT"
				+ " ISNULL(SUM(" + Field + "),0)"
				+ " FROM Earning "
				+ " WHERE"
				+ " EarningMonth = " + m
				+ " AND EarningYear = " + y
				+ addq
				);
		}
		#endregion
		#region public static decimal EarningAsof(int EarningMonth, int EarningYear, string CenterID, string Field)
		/// <summary>
		/// Get earning (AS OF)
		/// </summary>
		public static decimal EarningAsof(int EarningMonth, int EarningYear, string CenterID, string Field)
		{
			string addq = "";
			if(CenterID!="#")
				addq = " CenterID = '"+CenterID+"' AND ";

			return SingleDecimal("SELECT"
				+ " ISNULL("
				+ "		(SELECT SUM("+Field+") FROM Earning"
				+ "		WHERE "+addq+" ("
				+ "			(EarningYear < " + EarningYear + ")"
				+ "			OR (EarningYear = "+EarningYear+" AND EarningMonth <= "+EarningMonth+")"
				+ "		)"
				+ "		)"
				+ " ,0)"
				);
		}
		#endregion
		#region public static decimal EarningPeriod(string CenterID, int MonthFrom, int YearFrom, int MonthTo, int YearTo, string Field)
		/// <summary>
		/// Get earning within period
		/// </summary>
		public static decimal EarningPeriod(string CenterID, int MonthFrom, int YearFrom, int MonthTo, int YearTo, string Field)
		{
			string addq = "";
			if(CenterID!="#")
				addq = " CenterID = '"+CenterID+"' AND ";

			return SingleDecimal("SELECT"
				+ " ISNULL("
				+ "		(SELECT SUM("+Field+") FROM Earning"
				+ "		WHERE " + addq
				+ "			("
				+ "				(EarningYear > "+YearFrom+")"
				+ "				OR (EarningYear = "+YearFrom+" AND EarningMonth >= "+MonthFrom+")"
				+ "			)"
				+ "		AND"
				+ "			("
				+ "				(EarningYear < "+YearTo+")"
				+ "				OR (EarningYear = "+YearTo+" AND EarningMonth <= "+MonthTo+")"
				+ "			)"
				+ "		)"
				+ " ,0)"
				);
		}	
		#endregion
		
		#region public static void UpdateAccountBalance(string AccountID, decimal Debit, decimal Credit, int BalanceMonth, int BalanceYear, bool OperandIsPlus)
		/// <summary>
		/// Update AccountBalance
		/// </summary>
		public static void UpdateAccountBalance(string AccountID, decimal Debit, decimal Credit, int BalanceMonth, int BalanceYear, bool OperandIsPlus)
		{
			string operand;
			if(OperandIsPlus) operand = "+"; else operand = "-";

			decimal Netto = 0;
			if(SingleBool("SELECT DebitNature FROM Account WHERE AccountID = '"+AccountID+"'"))
				Netto = Debit - Credit;
			else
				Netto = Credit - Debit;

			if(OperandIsPlus)
			{
				if(!AccountBalanceExist(AccountID,BalanceMonth,BalanceYear))
				{
					InsertAccountBalance(AccountID,BalanceMonth,BalanceYear);
				}
			}

			Execute("UPDATE AccountBalance SET"
				+ " Balance		= (Balance " + operand + " (" + Netto + "))"
				+ ",TotalDebit	= (TotalDebit " + operand + " (" + Debit + "))"
				+ ",TotalCredit	= (TotalCredit " + operand + " (" + Credit + "))"
				+ " WHERE "
				+ " AccountID = '" + AccountID + "'"
				+ " AND BalanceMonth = " + BalanceMonth
				+ " AND BalanceYear = " + BalanceYear
				);

			Execute("UPDATE AccountBalance SET"
				+ " Balance	= (Balance " + operand + " (" + Netto  + "))"
				+ " WHERE "
				+ " AccountID = '" + AccountID + "'"
				+ " AND"
				+ "	("
				+ "		(BalanceYear > " + BalanceYear + ")"
				+ "		OR (BalanceYear = "+BalanceYear+" AND BalanceMonth > "+BalanceMonth+")"
				+ " )"
				);
			
			string strSql = "SELECT ParentID FROM AccountLink WHERE ChildID = '"+AccountID+"'";
			DataTable rs = Rs(strSql);
			int j = rs.Rows.Count;

			for(int i=0;i<=j-1;i++)
			{
				UpdateAccountBalance(
					(string) rs.Rows[i][0]
					,Debit
					,Credit
					,BalanceMonth
					,BalanceYear
					,OperandIsPlus
					);
			}
		}
		#endregion
		#region public static void UpdateAccountProfit(string AccountID, decimal Debit, decimal Credit, int ProfitMonth, int ProfitYear, bool OperandIsPlus, string CenterID)
		/// <summary>
		/// Update AccountProfit
		/// </summary>
		public static void UpdateAccountProfit(string AccountID, decimal Debit, decimal Credit, int ProfitMonth, int ProfitYear, bool OperandIsPlus, string CenterID)
		{
			string operand;
			if(OperandIsPlus) operand = "+"; else operand = "-";

			decimal Netto = 0;
			if(SingleBool("SELECT DebitNature FROM Account WHERE AccountID = '"+AccountID+"'"))
				Netto = Debit - Credit;
			else
				Netto = Credit - Debit;

			if(OperandIsPlus)
			{
				if(!AccountProfitExist(AccountID,ProfitMonth,ProfitYear,CenterID))
				{
					InsertAccountProfit(AccountID,ProfitMonth,ProfitYear,CenterID);
				}
			}

			Execute("UPDATE AccountProfit SET"
				+ " Netto		= (Netto " + operand + " (" + Netto + "))"
				+ ",TotalDebit	= (TotalDebit " + operand + " (" + Debit + "))"
				+ ",TotalCredit	= (TotalCredit " + operand + " (" + Credit + "))"
				+ " WHERE "
				+ " AccountID = '" + AccountID + "'"
				+ " AND CenterID = '" + CenterID + "'"
				+ " AND ProfitMonth = " + ProfitMonth
				+ " AND ProfitYear = " + ProfitYear
				);

			string strSql = "SELECT ParentID FROM AccountLink WHERE ChildID = '"+AccountID+"'";
			DataTable rs = Rs(strSql);
			int j = rs.Rows.Count;

			for(int i=0;i<=j-1;i++)
			{
				UpdateAccountProfit(
					(string) rs.Rows[i][0]
					,Debit
					,Credit
					,ProfitMonth
					,ProfitYear
					,OperandIsPlus
					,CenterID
					);
			}
		}
		#endregion
		#region public static void UpdateEarning(int EarningMonth, int EarningYear, decimal Amount, decimal Gross, decimal OP, bool OperandIsPlus, string CenterID)
		/// <summary>
		/// Update Earning
		/// </summary>
		public static void UpdateEarning(int EarningMonth, int EarningYear, decimal Amount, decimal Gross, decimal OP, bool OperandIsPlus, string CenterID)
		{
			string operand;
			if(OperandIsPlus) operand = "+"; else operand = "-";

			if(OperandIsPlus)
			{
				if(!EarningBalanceExist(EarningMonth,EarningYear,CenterID))
				{
					InsertEarning(EarningMonth,EarningYear,CenterID);
				}
			}

			Execute("UPDATE Earning SET"
				+ " Amount	= (Amount " + operand + "(" + Amount + "))"
				+ ",Gross	= (Gross " + operand + "(" + Gross + "))"
				+ ",OP		= (OP " + operand + "(" + OP + "))"
				+ " WHERE "
				+ " EarningMonth = " + EarningMonth
				+ " AND EarningYear = " + EarningYear
				+ " AND CenterID = '" + CenterID + "'"
				);
		}
		#endregion
		
		#region public static string AccountE
		/// <summary>
		/// Earning Account
		/// </summary>
		public static string AccountE
		{
			get
			{
				return Gets("AccountE");
			}
		}
		#endregion
		#region public static string AccountCE
		/// <summary>
		/// Current Earning Account
		/// </summary>
		public static string AccountCE
		{
			get
			{
				return Gets("AccountCE");
			}
		}
		#endregion
		#region public static string AccountRE
		/// <summary>
		/// Retained Earning Account
		/// </summary>
		public static string AccountRE
		{
			get
			{
				return Gets("AccountRE");
			}
		}
		#endregion
		#region public static string AccountBE
		/// <summary>
		/// Balancing Account
		/// </summary>
		public static string AccountBE
		{
			get
			{
				return Gets("AccountBE");
			}
		}
		#endregion
		#region private static string Gets(string myID)
		private static string Gets(string myID)
		{
			return SingleString("SELECT Param FROM SystemParam WHERE ID = '" + myID + "'");
		}
		#endregion

		#region public static void InsertAnomali()
		public static void InsertAnomali(string Sumber, string Ref, string Bef, string Aft, string Ket, string Jurnal, string NoVoucher)
		{
			long NoAnomali = SingleLong("SELECT ISNULL(MAX(NoAnomali), 0) FROM MS_ANOMALI");
			NoAnomali++;

			Execute(
				"INSERT INTO MS_ANOMALI"
				+ "("
				+ "NoAnomali"
				+ ", Tgl"
				+ ", Sumber"
				+ ", Referensi"
				+ ", Sebelum"
				+ ", Setelah"
				+ ", Keterangan"
				+ ", Jurnal"
				+ ", NoVoucher"
				+ ")"
				+ "VALUES"
				+ "("
				+ NoAnomali
				+ ", '" + DateTime.Today + "'"
				+ ", '" + Sumber + "'"
				+ ", '" + Ref + "'"
				+ ", '" + Bef + "'"
				+ ", '" + Aft + "'"
				+ ", '" + Ket + "'"
				+ ", '" + Jurnal + "'"
				+ ", '" + NoVoucher + "'"
				+ ")"
				);
		}
		#endregion
	}
}
