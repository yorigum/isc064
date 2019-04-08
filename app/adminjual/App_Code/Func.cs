using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC056.ADMINJUAL
{
	public class Func
	{
		#region public static void GenerateFP(string Peta)
		/// <summary>
		/// Redrawing process
		/// </summary>
		public static void GenerateFP(string Peta)
		{
			if(Peta!="")
			{
				string loc = System.Web.HttpContext.Current.Request.PhysicalApplicationPath.Replace("admin","marketing")
					+ "FP\\";
				string Src = loc + "Base\\" + Cf.FileSafe(Peta) + ".jpg";
				string Target = loc + Cf.FileSafe(Peta) + ".jpg";
					
				if(System.IO.File.Exists(Src))
				{
					Bitmap objBitmap = new Bitmap(Src);
					Graphics objGraphics = Graphics.FromImage(objBitmap);

					string strSql = "SELECT Status, Koordinat"
						+ " FROM MS_UNIT "
						+ " WHERE Peta = '"+Peta+"'";

					DataTable rs = Db.Rs(strSql);
					for(int i=0;i<rs.Rows.Count;i++)
					{
						if(!System.Web.HttpContext.Current.Response.IsClientConnected) break;

						string Coordinate = rs.Rows[i]["Koordinat"].ToString();
						string status = rs.Rows[i]["Status"].ToString();

						string[] arrCoordinate = Coordinate.Split(new char[] {','});
						System.Collections.ArrayList myPointList = new System.Collections.ArrayList();
			
						for(int ix=0;ix<arrCoordinate.GetUpperBound(0);ix=ix+2)
							myPointList.Add(new Point(
								Convert.ToInt32(arrCoordinate[ix])
								, Convert.ToInt32(arrCoordinate[ix+1])));

						Point[] ArrPoint = new Point[myPointList.Count];

						for(int jx=0;jx<myPointList.Count;jx++)
							ArrPoint[jx] = (Point) myPointList[jx];

						if(status=="B")
						{
							Color ColorPoly = new Color();
							ColorPoly = Color.Red;

							Color myColor = Color.FromArgb(100, Color.White);

							objGraphics.FillPolygon(
								new HatchBrush(HatchStyle.ForwardDiagonal, ColorPoly, myColor)
								,ArrPoint);
						}
					}

					objBitmap.Save(Target);

					objGraphics.Dispose();
					objBitmap.Dispose();
				}
			}
		}
		#endregion
	}
}
