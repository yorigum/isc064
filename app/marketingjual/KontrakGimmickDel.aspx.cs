using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakGimmickDel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Act.CekInt(NoUrut);

            int adSN = Db.SingleInteger("SELECT COunt(*) FROM MS_KONTRAK_GIMMICK WHERE NoKontrak = '" + NoKontrak + "' AND SN > " + NoUrut);
            if (adSN > 0)
            {
                frm.Visible = false;
                ale.Visible = true;
            }
            else
            {
                frm.Visible = true;
                ale.Visible = false;
            }


            Js.Confirm(this,
                    "Apakah anda ingin menghapus Gimmick : " + NoUrut + " ?\\n"
                    + "Perhatian bahwa data akan dihapus secara PERMANEN."
                    );
        }

        protected void delbtn_Click(object sender, System.EventArgs e)
        {
            DataTable rs = Db.Rs(
                "SELECT * FROM MS_KONTRAK_GIMMICK WHERE NoKontrak = '" + NoKontrak + "' AND SN = " + NoUrut);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                + "<br><br>***Data Sebelum Delete :<br>"
                + NoKontrak + " " + NoUrut + " " + rs.Rows[0]["ItemID"].ToString() + " " + rs.Rows[0]["Nama"].ToString() + " " + rs.Rows[0]["Tipe"].ToString() + " " 
                + rs.Rows[0]["Satuan"].ToString() + " " + Cf.Num(rs.Rows[0]["Stock"]) + " " + Cf.Num(rs.Rows[0]["HargaSatuan"]) + " " + Cf.Num(rs.Rows[0]["HargaTotal"]);

                Db.Execute("DELETE FROM MS_KONTRAK_GIMMICK WHERE NoKontrak = '" + NoKontrak + "' AND SN = " + NoUrut);

                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM MS_KONTRAK_GIMMICK WHERE NoKontrak = '" + NoKontrak + "' AND SN = " + NoUrut);

                if (c == 0)
                {
                    //Log
                    Db.Execute("EXEC spLogKontrak "
                        + " 'D-Gi'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    Js.Close(this);
                }
            }
        }

        private string NoKontrak
        {
            get { return Cf.Pk(Request.QueryString["NoKontrak"]); }
        }
        private string NoUrut
        {
            get { return Cf.Pk(Request.QueryString["SN"]); }
        }
    }
}