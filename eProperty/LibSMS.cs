using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for LibSMS
/// </summary>

namespace ISC064
{
    public class LibSMS
    {
        //private static string AppModul { get { return "SMS"; } }

        //Masking
        public static string[] Tipe
        {
            get
            {
                string[] x =
                {   "UlangTahun;Ulang Tahun;",
                "Invoice;Invoice",
                "Kuitansi;Kuitansi",
            };

                return x;
            }
        }
        public static void ListSMS(DropDownList container)
        {
            DataTable rs;

            rs = Db.Rs("SELECT * FROM SmsVendors");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                container.Items.Add(new ListItem(rs.Rows[i]["SMSID"].ToString() + " - " +rs.Rows[i]["Nama"], rs.Rows[i]["SMSID"].ToString()));
            }
        }
    }
}