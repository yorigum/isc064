using System;
using System.Data;

namespace ISC064
{
    public partial class AutoDenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_TAGIHAN SET Denda = 0");
            decimal NilaiP = 0;

            DataTable rs = Db.Rs(" SELECT"
                                  + " ISC064_MARKETINGJUAL..MS_TAGIHAN.NoKontrak "
                                  + ",NoUrut"
                                  + ",Project"
                                  + ",TglJT"
                                  + ",NilaiTagihan"
                                  + ",NilaiTagihan -"
                                  + "("
                                  + "   SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN"
                                  + "	WHERE NoKontrak = ISC064_MARKETINGJUAL..MS_TAGIHAN.NoKontrak AND NoTagihan = ISC064_MARKETINGJUAL..MS_TAGIHAN.NoUrut AND ISC064_MARKETINGJUAL..MS_PELUNASAN.SudahCair=1"
                                  + ")"
                                  + " AS SisaTagihan"
                                  + ", CASE WHEN TglBatal Is NULL"
                                  + "  THEN DATEDIFF(d,TglJT, GETDATE())"
                                  + " ELSE"
                                  + "   DATEDIFF(d,TglJT, TglBatal)"
                                  + "  End AS Telat"
                                  + ",PutihDenda"
                                  + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN"
                                  + " INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK ON ISC064_MARKETINGJUAL..MS_TAGIHAN.NoKontrak = ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak WHERE ISC064_MARKETINGJUAL..MS_KONTRAK.Project IN (" + Act.ProjectListSql + ")");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string ParamID1 = "RumusDenda" + rs.Rows[i]["Project"];
                string ParamID2 = "RumusDenda2" + rs.Rows[i]["Project"];
                string ParamID3 = "BerlakuDenda" + rs.Rows[i]["Project"];
                string ParamID4 = "GracePeriodDenda" + rs.Rows[i]["Project"];

                decimal Rumus1 = Db.SingleDecimal("SELECT ISNULL(Value, 0) FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID1 + "'");
                decimal Rumus2 = Db.SingleDecimal("SELECT ISNULL(Value, 0) FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID2 + "'");
                decimal Berlaku = Db.SingleDecimal("SELECT ISNULL(Value, 0) FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID3 + "'");
                decimal GracePeriod = Db.SingleDecimal("SELECT ISNULL(Value, 0) FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID4 + "'");

                string NoKontrak = rs.Rows[i]["NoKontrak"].ToString();
                int NoUrut = Convert.ToInt16(rs.Rows[i]["NoUrut"]);
                NilaiP = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a "
                        + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN b ON a.NoKontrak = b.NoKontrak AND a.NoTagihan = b.NoUrut"
                        + " WHERE a.NoKontrak = '" + NoKontrak + "' AND a.NoTagihan = '" + NoUrut + "' AND SudahCair = 1");
                DateTime TglJT = Convert.ToDateTime(rs.Rows[i]["TglJT"]);
                decimal SisaTagihan = Convert.ToDecimal(rs.Rows[i]["SisaTagihan"]);
                DateTime MaxTglPelunasan = Db.SingleTime("Select ISNULL(MAX(TglPelunasan),0) From " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "'");

                int ada = Db.SingleInteger("SELECT Count(*) FROM ISC064_MARKETINGJUAL..MS_Pelunasan WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + NoUrut);
                DateTime TLunas = Convert.ToDateTime(DateTime.Now);
                decimal A = 0;
                if (ada > 0)
                {
                    A = (Decimal)rs.Rows[i]["NilaiTagihan"];
                    TLunas = Db.SingleTime("Select TgLPelunasan from ISC064_marketingjual..ms_pelunasan where nokontrak = '" + NoKontrak + "' and NoTagihan = " + NoUrut);
                }
                else
                {
                    A = (decimal)rs.Rows[i]["NilaiTagihan"];
                }
                DateTime TJT = Convert.ToDateTime(rs.Rows[i]["TglJT"]);
                TimeSpan Spanhari = TLunas.Subtract(TJT); //mengetahui jarak antara tgl pelunasan dan jatuh tempo

                decimal PutihDenda = Convert.ToDecimal(rs.Rows[i]["PutihDenda"]);
                decimal denda = 0;
                decimal dendapelunasan = 0; //baru
                decimal dendareal = 0;

                double num2 = Convert.ToDouble(Spanhari.Days);
                if (num2 <= 0)
                    num2 = 0;

                if ((decimal)num2 >= GracePeriod && ((decimal)num2 - Berlaku) > 0)
                    denda = (SisaTagihan / Rumus2) * (Rumus1 * ((decimal)num2 - Berlaku));
                decimal NilaiPelunasan = 0, telatPelunasan = 0;

                DataTable rsDetil = Db.Rs("SELECT NilaiPelunasan, DATEDIFF(d, '" + TglJT + "', TglPelunasan) AS TelatLunas"
                                           + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + NoUrut + " AND SudahCair = 1");

                for (int j = 0; j < rsDetil.Rows.Count; j++)
                {
                    NilaiPelunasan = Convert.ToDecimal(rsDetil.Rows[j]["NilaiPelunasan"]);
                    telatPelunasan = Convert.ToDecimal(rsDetil.Rows[j]["TelatLunas"]);

                    if (telatPelunasan < 0)
                        telatPelunasan = 0;
                    double num3 = Convert.ToDouble(1.001);
                    double num4 = Convert.ToDouble(telatPelunasan);

                    if ((decimal)num4 >= GracePeriod && ((decimal)num4 - Berlaku) > 0)
                        dendapelunasan += (NilaiPelunasan / Rumus2) * (Rumus1 * ((decimal)num4 - Berlaku));

                    dendareal = denda + dendapelunasan;
                    if (dendareal <= 0)
                        dendareal = 0;
                }

                //kondisi baru
                if (SisaTagihan > 0)
                {
                    DateTime HariIni = DateTime.Now;
                    TimeSpan Spanhari2 = HariIni.Subtract(TJT);
                    double num5 = Convert.ToDouble(1.001);
                    double num6 = Convert.ToDouble(Spanhari2.Days);
                    if (num6 <= 0)
                        num6 = 0;

                    if ((decimal)num6 >= GracePeriod && ((decimal)num6 - Berlaku) > 0)
                        dendareal += (SisaTagihan / Rumus2) * (Rumus1 * ((decimal)num6 - Berlaku));

                    if (dendareal <= 0)
                        dendareal = 0;
                }


                if (PutihDenda == 1)
                    dendareal = 0;

                Db.Rs("UPDATE ISC064_MARKETINGJUAL..MS_TAGIHAN SET Denda = " + dendareal
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    + " AND NoUrut = " + NoUrut
                    + " AND KPR = 0"
                    + " AND Tipe <> 'ADM'"
                    );
            }

        }
    }
}