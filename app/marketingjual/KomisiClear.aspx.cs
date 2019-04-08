using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KomisiClear : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSql = "SELECT a.NoKontrak FROM MS_KOMISI a"
                + " INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " WHERE a.NilaiBayar = '0' AND b.NoAgent = " + NoAgent
                + " AND a.SudahBayar='0'"
                + " AND CONVERT(VARCHAR,TglKontrak,112) >= '" + Cf.Tgl112(Convert.ToDateTime(Dari)) + "'"
                + " AND CONVERT(VARCHAR,TglKontrak,112) <= '" + Cf.Tgl112(Convert.ToDateTime(Sampai)) + "'"
            ;
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string NoKontrak = rs.Rows[i]["NoKontrak"].ToString();
               
                //DELETE KOMISI SALES
                Db.Execute("DELETE FROM MS_KOMISI"
                + " WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK SET FlagKomisi=0 WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("DELETE FROM MS_KOMISI_DETAIL"
                + " WHERE NilaiBayar = '0' AND NoKontrak = '" + NoKontrak + "'"
                + " AND SudahBayar='0'");
            }
           // Response.Write(strSql);
            Response.Redirect("KomisiGeneratePeriode.aspx?Clear=1&NoAgent="+NoAgent+"");
        }

        private string NoAgent
        {
            get
            {
                return Request.QueryString["NoAgent"];
            }
        }
        private string Dari
        {
            get
            {
                return Cf.Pk(Request.QueryString["Dari"]);
            }
        }
        private string Sampai
        {
            get
            {
                return Cf.Pk(Request.QueryString["Sampai"]);
            }
        }
    }
}