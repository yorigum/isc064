using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISC064
{
    public class TableStock
    {
        private static string Conn  = "server=.; uid=batavianet;pwd=iNDigo100; database=ISC064_MARKETINGJUAL";
        public static Color WarnaUnit(string NoStock)
        {
            string strSql = "SELECT Status,Project"
                + " FROM ISC064_MARKETINGJUAL..MS_UNIT "
                + " WHERE NoStock = '" + NoStock + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count != 0)
            {
                Color color = new Color();
                string WarnaJual = Db.SingleString("SELECT ISNULL(MAX(Value), '#FFFFFF') FROM ISC064_SECURITY..REF_PARAM WHERE ParamID='WarnaUnitJual" + rs.Rows[0]["Project"] + "'", Conn);
                string WarnaBooked = Db.SingleString("SELECT ISNULL(MAX(Value), '#FFFFFF') FROM ISC064_SECURITY..REF_PARAM WHERE ParamID='WarnaUnitBooked" + rs.Rows[0]["Project"] + "'", Conn);
                string WarnaAvailable = Db.SingleString("SELECT ISNULL(MAX(Value), '#FFFFFF') FROM ISC064_SECURITY..REF_PARAM WHERE ParamID='WarnaUnitCancel" + rs.Rows[0]["Project"] + "'", Conn);
                string WarnaHold = Db.SingleString("SELECT ISNULL(MAX(Value), '#FFFFFF') FROM ISC064_SECURITY..REF_PARAM WHERE ParamID='WarnaUnitHold" + rs.Rows[0]["Project"] + "'", Conn);

                if (rs.Rows[0]["Status"].ToString() == "B")
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_KONTRAK"
                        + " WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);
                    if (c != 0)
                    {
                        string NoKontrak = Db.SingleString("SELECT NoKontrak FROM "
                            + " ISC064_MARKETINGJUAL..MS_KONTRAK WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);
                        color = Color.FromName(WarnaJual); //sold
                    }
                    else
                        color = Color.FromName(WarnaHold); //hold internal
                }
                else if (rs.Rows[0]["Status"].ToString() == "H")
                {
                    string seclevel = Db.SingleString("SELECT SecLevel FROM ISC064_SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'", Conn);
                    if (seclevel == "SALES")
                        color = Color.FromName(WarnaHold); //reserved
                    else
                        color = Color.FromName(WarnaHold); //reserved
                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_RESERVASI"
                        + " WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);
                    if (c != 0)
                    {
                        string seclevel = Db.SingleString("SELECT SecLevel FROM ISC064_SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'", Conn);
                        if (seclevel == "SALES")
                            color = Color.FromName(WarnaJual); //reserved
                        else
                            color = Color.FromName(WarnaBooked); //reserved
                    }
                    else
                        color = Color.FromName(WarnaAvailable); //available
                }

                return color;
            }
            else
                return Color.White;
        }

        public static string Href(string NoStock)
        {
            string x = "";

            string strSql = "SELECT Status, NoUnit FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE NoStock = '" + NoStock + "'";
            DataTable rs = Db.Rs(strSql, Conn);
            if (rs.Rows.Count != 0)
            {
                if (rs.Rows[0]["Status"].ToString() == "B")
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_KONTRAK"
                        + " WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);
                    if (c != 0)
                    {
                        string NoKontrak = Db.SingleString("SELECT TOP 1 NoKontrak FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);

                        int NoTTS = 0;
                        DataTable tts = Db.Rs("SELECT TOP 1 NoTTS FROM ISC064_FINANCEAR..MS_TTS WHERE Ref = '" + NoKontrak + "' AND Status <> 'VOID' ORDER BY NoTTS ASC", Conn);
                        if (tts.Rows.Count != 0)
                            NoTTS = Convert.ToInt32(tts.Rows[0][0]);

                        x = "TabelStok3.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS; //sold
                    }
                    else
                        x = "javascript:popUnit(\"" + NoStock + "\")"; //hold internal
                }
                else if (rs.Rows[0]["Status"].ToString() == "H")
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_HOLD"
                         + " WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);
                    if (c != 0)
                    {
                        string NoHold = Db.SingleString("SELECT TOP 1 NoHOLD FROM ISC064_MARKETINGJUAL..MS_HOLD WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);
                        x = "HoldUnitDaftarDone.aspx?NoHold=" + NoHold + "";
                    }
                    else
                    {
                        x = " ";
                    }
                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_RESERVASI"
                        + " WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);
                    if (c != 0)
                        x = "TabelStok2.aspx?NoStock=" + NoStock;//"KontrakDaftar2.aspx?NoStock=" + rs.Rows[0]["NoStock"]; //booked
                    else
                    {
                        if (Act.SecLevel == "AG")
                            x = "javascript:popUnit(\"" + NoStock + "\")"; //available
                        else
                            x = "TabelStok2.aspx?NoStock=" + NoStock; //available
                    }
                }
            }

            return x;
        }
    }
}
