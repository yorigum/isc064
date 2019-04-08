using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.FINANCEAR
{
    public partial class tts2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_TTS WHERE NOBKM!=''");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                //DateTime TglTTS = Convert.ToDateTime(rs.Rows[i]["TglTTS"]);
                //string noTTS = rs.Rows[i]["NoTTS"].ToString();
                
                //string formatMonth = Cf.Roman(TglTTS.Month);
                //string formatTahun = TglTTS.Year.ToString().Substring(2, 2);
                //string NoTTS2 = "";
               
                
                //    int num = Db.SingleInteger("SELECT COUNT(NoTTS2) FROM MS_TTS WHERE MONTH(TglTTS)='" + TglTTS.Month + "' AND YEAR(TglTTS)='" + TglTTS.Year + "' and NoTTS2!=''");

                //    int increment = num + 1;
                //    string no = increment.ToString().PadLeft(7, '0');
                        
                //        NoTTS2 = "TTS/" + formatTahun + "/" + formatMonth + "/" + no;

                   

                //Db.Execute("UPDATE MS_TTS SET NoTTS2 = '" + NoTTS2 + "' WHERE NoTTS ='" + noTTS + "'");
                //Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_PELUNASAN SET NoTTS2='" + NoTTS2 + "' WHERE NoTTS ='" + noTTS + "'");

                DateTime TglBKM = Convert.ToDateTime(rs.Rows[i]["TglBKM"]);
                string noTTS = rs.Rows[i]["NoTTS"].ToString();

                string formatMonth = Cf.Roman(TglBKM.Month);
                string formatTahun = TglBKM.Year.ToString().Substring(2, 2);
                string NoBKM2 = "";


                int num = Db.SingleInteger("SELECT COUNT(NoBKM2) FROM MS_TTS WHERE Status='POST' AND MONTH(TglBKM)='" + TglBKM.Month + "' AND YEAR(TglBKM)='" + TglBKM.Year + "' AND NOBKM != ''");

                //BKM Pertama
                int increment = num + 1;
                string no = increment.ToString().PadLeft(7, '0');
                NoBKM2 = "KW/" + formatTahun + "/" + formatMonth + "/" + no;



                Db.Execute("UPDATE MS_TTS SET NoBKM2 = '" + NoBKM2 + "' WHERE NoTTS ='" + noTTS + "'");
                Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_PELUNASAN SET NoBKM2='" + NoBKM2 + "' WHERE NoTTS ='" + noTTS + "'");

                Response.Write("a");
            }
        }
        private bool isUnique(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_TTS WHERE NoTTS2 = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
        }
    }
}