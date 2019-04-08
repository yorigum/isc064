using System;
using System.Data;
using System.Data.SqlClient;

namespace ISC064
{
	/// <summary>
	/// Fungsi report builder
	/// </summary>
	public class Sc
	{
		public static string[] Catalog()
		{
			//daftar catalog yang bisa dibuat report builder
			string[] x = new string[3];
			
			x[0] = "" + Mi.DbPrefix + "SECURITY";
			x[1] = "" + Mi.DbPrefix + "MARKETINGJUAL";
			x[2] = "" + Mi.DbPrefix + "FINANCEAR";

			return x;
		}

		public static string[] MktCatalog()
		{
			//daftar catalog yang link dengan modul finance dan collection
			string[] x = new string[1];
			
			x[0] = "" + Mi.DbPrefix + "MARKETINGJUAL;JUAL;Customer Jual";

			return x;
		}

		private static string uid()
		{
			return "uid=batavianet;pwd=iNDigo100;";
		}

		#region public static DataTable Tb(string Catalog)
		public static DataTable Tb(string Catalog)
		{
			string strSql = "SELECT "
				+ "	table_name " //0
				+ " FROM information_schema.tables "
				+ " WHERE "
				+ " table_type = 'BASE TABLE' "
				+ " AND table_name <> 'dtproperties'"
				+ " ORDER BY table_name ASC";

			return rst(strSql, Catalog);
		}
		#endregion
		#region public static DataTable Col(string Catalog, string Tb)
		public static DataTable Col(string Catalog, string Tb)
		{
			string strSql = "SELECT "
				+ " column_name" //0
				+ ",data_type" //1
				+ " FROM information_schema.columns "
				+ " WHERE "
				+ " table_name = '"+Tb+"'"
				+ " ORDER BY ordinal_position ASC";

			return rst(strSql, Catalog);
		}
		#endregion
		#region private static DataTable rst(string strSql, string Catalog)
		private static DataTable rst(string strSql, string Catalog)
		{
			SqlConnection sqlCnn = new SqlConnection (
				"server=(local); "+uid()+" database="+Catalog);

			SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql,sqlCnn);
			DataSet objDS = new DataSet();
			sqlAdapter.Fill(objDS,"data");
			sqlCnn.Close();

			DataTable rst = new DataTable();
			rst = objDS.Tables["data"];

			return rst;
		}
		#endregion

		#region public static string Sql(int NoRpt)
		public static string Sql(int NoRpt)
		{
			DataTable rpt = Db.Rs("SELECT "
				+ " SelTop,SelDis,FilterStatis FROM " + Mi.DbPrefix + "RPT..RPT "
				+ " WHERE NoRpt = " + NoRpt);

			if(rpt.Rows.Count==0)
				return "";
			else
			{
				DataTable tb = Db.Rs("SELECT "
					+ " Cat,Tb,Fk,NoUrut FROM " + Mi.DbPrefix + "RPT..RPT_TB "
					+ " WHERE NoRpt = " + NoRpt
					+ " ORDER BY NoUrut");

				if(tb.Rows.Count==0)
					return "";
				else
				{
					System.Text.StringBuilder x = new System.Text.StringBuilder();

					x.Append("SELECT ");
					if((int)rpt.Rows[0]["SelTop"]!=0)
						x.Append(" TOP " + rpt.Rows[0]["SelTop"]);
					else if((bool)rpt.Rows[0]["SelDis"])
						x.Append(" DISTINCT ");
					x.Append("\n");
					
					DataTable col = Db.Rs("SELECT "
						+ " Cat,Tb,Col,Alias,Cnv FROM " + Mi.DbPrefix + "RPT..RPT_COL "
						+ " WHERE NoRpt = " + NoRpt
						+ " AND Data = 1 "
						+ " ORDER BY NoUrut");

					if(col.Rows.Count==0)
					{
						//all column
						x.Append(" * ");
						x.Append("\n");
					}
					else
					{
						//column definition
						for(int i=0;i<col.Rows.Count;i++)
						{
							if((string)col.Rows[i]["Cnv"]!="")
								x.Append("CONVERT(varchar,"+col.Rows[i]["Col"]+","+col.Rows[i]["Cnv"]+")");
							else
								x.Append((string)col.Rows[i]["Col"]);
							
							x.Append(" AS ["+col.Rows[i]["Alias"]+"]");
							
							if(i!=col.Rows.Count-1)
								x.Append(",");
							
							x.Append("\n");
						}
					}

					x.Append(" FROM ");
					x.Append("\n");

					for(int i=0;i<tb.Rows.Count;i++)
					{
						//table definitions
						x.Append(tb.Rows[i]["Cat"]+ ".." + tb.Rows[i]["Tb"] + " AS TB"
							+ tb.Rows[i]["NoUrut"]);
						if(i!=tb.Rows.Count-1)
							x.Append(",");
						x.Append("\n");
					}

					x.Append(" WHERE 1=1 ");
					x.Append("\n");

					for(int i=0;i<tb.Rows.Count;i++)
					{
						//table inner join definitions
						if(tb.Rows[i]["Fk"].ToString()!="")
						{
							x.Append(" AND " + tb.Rows[i]["Fk"]);
							x.Append("\n");
						}
					}

					if(rpt.Rows[0]["FilterStatis"].ToString()!="")
						x.Append(" AND ("+rpt.Rows[0]["FilterStatis"]+")");

					return x.ToString();
				}
			}
		}
		#endregion
		#region public static string SqlLog(int NoRpt)
		public static string SqlLog(int NoRpt)
		{
			System.Text.StringBuilder sql = new System.Text.StringBuilder();
			sql.Append(Sql(NoRpt));

			System.Text.StringBuilder filter = new System.Text.StringBuilder();
			System.Text.StringBuilder sort = new System.Text.StringBuilder();
			System.Text.StringBuilder gt = new System.Text.StringBuilder();

			DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "RPT..RPT_COL WHERE NoRpt = " + NoRpt);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string x = rs.Rows[i]["Col"]+ " (" + rs.Rows[i]["Alias"] + ")\n";
				if((bool)rs.Rows[i]["Filter"])
					filter.Append(x);
				if((bool)rs.Rows[i]["Sort"])
					sort.Append(x);
				if((bool)rs.Rows[i]["Gt"])
					gt.Append(x);
			}

			if(gt.Length!=0)
				sql.Append("\n***TOTAL :\n" + gt.ToString());
			if(sort.Length!=0)
				sql.Append("\n***SORT :\n" + sort.ToString());
			if(filter.Length!=0)
				sql.Append("\n***FILTER :\n" + filter.ToString());

			return sql.ToString().Replace("'","''");
		}
		#endregion

		#region public static string MktTb(string Tipe)
		public static string MktTb(string Tipe)
		{
			string tb = "";
			string[] x = MktCatalog();

			for(int i=0;i<=x.GetUpperBound(0);i++)
			{
				string[] xdetil = x[i].Split(';');
				if(xdetil[1]==Tipe)
					tb = xdetil[0];
			}

			return tb;
		}
		#endregion
	}
}
