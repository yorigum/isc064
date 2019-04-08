using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ISC064.NUP
{
    public partial class TabelStokView : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Fill();
        }

        protected void Fill()
        {
            DataTable a = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI WHERE Project ='" + Project + "' AND (SELECT COUNT(JenisProperti) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE Lokasi = " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI.Lokasi AND JenisProperti = '" + Jenis + "') > 0");

            for (int j = 0; j < a.Rows.Count; j++)
            {
                Table tb = new Table();
                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<h1><a href=TabelStokViewB.aspx?Nama=Tower&Tower=" + a.Rows[j]["Lokasi"] + "&NoNUP=" + NoNUP + "&Tipe=" + Jenis + "&project=" + Project + ">"
                          + a.Rows[j]["Nama"] + "</h1></a><br>";
                r.Cells.Add(c);
                list.Controls.Add(r);
            }
        }

        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
        private string Jenis
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }
    }

}
