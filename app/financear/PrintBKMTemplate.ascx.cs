namespace ISC064.FINANCEAR
{
	using System;
    using System.Data;

	public partial class PrintBKMTemplate : System.Web.UI.UserControl
	{		
		//Passing parameter
		public string nomor;
        public string pro;
        public string NoTTS
		{
			set{nomor = value;}
		}
        public string Project
        {
            set { pro = value; }
        }
        private string Halaman { get { return "Kuitansi"; } }
		protected void Page_Load(object sender, System.EventArgs e)
		{
            Fill();
        }

        private void Fill()
        {
            string strSql = "SELECT *"
                + ",CASE CaraBayar"
                + "		WHEN 'TN' THEN 'TUNAI'"
                + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                + "		WHEN 'BG' THEN 'CEK GIRO'"
                + "		WHEN 'UJ' THEN 'UANG JAMINAN'"
                + "		WHEN 'DN' THEN 'DISKON'"
                + " END AS CaraBayar2"
                + " FROM MS_TTS  WHERE NoTTS = " + nomor;

            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count != 0)
            {
                nobkm.Text = rs.Rows[0]["Nobkm2"].ToString();
                cs.Text = rs.Rows[0]["Customer"].ToString();
                jumlah.Text = Money.Str((decimal)rs.Rows[0]["Total"]) + " RUPIAH";
                jumlahbayar.Text = Cf.Num(rs.Rows[0]["Total"]);
                tglcetak.Text = Cf.DayIndo(Convert.ToDateTime(rs.Rows[0]["TglBKM"]));
                pt.Text = Mi.Pt;
                alamatpt.Text = Db.SingleString("Select AlamatPers From ISC064_SECURITY..REF_DATA WHERE Project = '" + pro + "'");
                notelp.Text = Db.SingleString("Select NoTelp From ISC064_SECURITY..REF_DATA WHERE Project = '" + pro + "'");
                string Tipe = rs.Rows[0]["Tipe"].ToString();
                FillTable(Tipe, rs.Rows[0]["Unit"].ToString());

                if (rs.Rows[0]["CaraBayar"].ToString() == "BG")
                {
                    gr.Text = " <b>X</b> ";
                    cc.Text = "&nbsp;";
                    tr.Text = "";
                }
                else if (rs.Rows[0]["CaraBayar"].ToString() == "KK")
                {
                    gr.Text = "&nbsp;";
                    cc.Text = " <b>X</b> ";
                    tr.Text = "&nbsp;";
                }
                else if (rs.Rows[0]["CaraBayar"].ToString() == "KD")
                {
                    gr.Text = "&nbsp;";
                    cc.Text = " <b>X</b> ";
                    tr.Text = "&nbsp;";
                }
                else
                {
                    gr.Text = "&nbsp;";
                    cc.Text = "&nbsp;";
                    tr.Text = " <b>X</b> ";
                }

            }
        }

        private void FillTable(string Tipe, string Unit)
        {
            int nobkm = Db.SingleInteger("SELECT NoBKM FROM MS_TTS WHERE NoTTS = " + nomor);
            string NoKontrak = Db.SingleString("SELECT b.NoKontrak FROM ISC064_MARKETINGJUAL..MS_TAGIHAN a JOIN ISC064_MARKETINGJUAL..MS_PELUNASAN b ON a.NoUrut = b.NoTagihan WHERE NoTTS = " + nomor);
            string NoKontrak2 = Db.SingleString("SELECT Ref FROM MS_TTS WHERE NoTTS = " + nomor);
            decimal t = 0;
            decimal Total = Db.SingleDecimal("SELECT Total FROM MS_TTS WHERE NoTTS = " + nomor);

            string Tb = Sc.MktTb(Tipe);
            string strSql = "SELECT l.Nilaipelunasan AS Nilai"
                + ",CONVERT(VARCHAR,NoTagihan) AS RefTagihan"
                + ",CASE NoTagihan"
                + "		WHEN 0 THEN 'UNALLOCATED'"
                + "		ELSE (SELECT NamaTagihan FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
                + " END AS NamaTagihan"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN AS l "
                + " WHERE NoTTS IN (SELECT NoTTS FROM MS_TTS WHERE NoBKM = " + nobkm + ")";

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs(strSql);
            string Ket = "";

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                DataTable NoTTS = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTTS = '" + nomor + "'");
                DataTable NoUrut = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTTS = '" + nomor + "'");
                string QueryNilaiPelunasan = "SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak2 + "' AND NoTTS <= '" + NoTTS.Rows[i]["NoTTS"] + "' AND NoTagihan = " + NoUrut.Rows[i]["NoTagihan"] + "";
                string QueryNilaiTagihan = "SELECT NilaiTagihan FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak2 + "' AND NoUrut = " + NoUrut.Rows[i]["NoTagihan"] + "";
                decimal NilaiTagihan = Db.SingleDecimal(QueryNilaiTagihan);
                decimal NilaiPelunasan = Db.SingleDecimal(QueryNilaiPelunasan);

                int a = Db.SingleInteger("Select Count(NoTagihan) From " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + NoUrut.Rows[i]["NoTagihan"] + " AND NilaiPelunasan != 0");

                if (i != (rs.Rows.Count - 1))
                {
                    if (NilaiTagihan != NilaiPelunasan)
                    {
                        Ket += "SEBAGIAN " + rs.Rows[i]["NamaTagihan"].ToString() + ", ";
                    }
                    else
                    {
                        if (a == 1)
                        {
                            Ket += " " + rs.Rows[i]["NamaTagihan"].ToString() + ", ";
                        }
                        else
                        {
                            Ket += "PELUNASAN " + rs.Rows[i]["NamaTagihan"].ToString() + ", ";
                        }
                    }
                }
                else
                {
                    if (NilaiTagihan != NilaiPelunasan)
                    {
                        Ket += "SEBAGIAN " + rs.Rows[i]["NamaTagihan"].ToString() + " ";
                    }
                    else
                    {
                        if (a == 1)
                        {
                            Ket += " " + rs.Rows[i]["NamaTagihan"].ToString() + " ";
                        }
                        else
                        {
                            Ket += "PELUNASAN " + rs.Rows[i]["NamaTagihan"].ToString() + " ";
                        }
                    }

                }
            }
            pembayaran.Text = Ket + "Unit " + Unit;
            t = Convert.ToDecimal(Total);
        }
    }
}
