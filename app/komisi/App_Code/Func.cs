using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Collections.Generic;
using System.Linq;

namespace ISC064.KOMISI
{
    public class Func
    {        
        #region public static bool CekCFR(string NoCFP)
        public static bool CekCFR(string NoCFP)
        {
            bool isProses = false;
            string strSql = "SELECT NoCFP FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_CFR WHERE NoCFP = '" + NoCFP + "'";
            DataTable rsKon = Db.Rs(strSql);

            if (rsKon.Rows.Count > 0)
            {
                isProses = true;
            }

            return isProses;
        }
        #endregion
    }
}
