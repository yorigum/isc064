namespace ISC064.FINANCEAR
{
	using System;
    using System.Data;
    using System.Web.UI.WebControls;

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
                jumlah.Text = Cf.Num(rs.Rows[0]["Total"]);//
                
                //jumlahbayar.Text = Cf.Num(rs.Rows[0]["Total"]);
                tglcetak.Text = Cf.DayIndo(Convert.ToDateTime(rs.Rows[0]["TglBKM"]));
                //pt.Text = Mi.Pt;
                //alamatpt.Text = Db.SingleString("Select AlamatPers From ISC064_SECURITY..REF_DATA WHERE Project = '" + pro + "'");
                //notelp.Text = Db.SingleString("Select NoTelp From ISC064_SECURITY..REF_DATA WHERE Project = '" + pro + "'");
                string Tipe = rs.Rows[0]["Tipe"].ToString();
                FillTable(Tipe, rs.Rows[0]["Unit"].ToString());

                //if (rs.Rows[0]["CaraBayar"].ToString() == "BG")
                //{
                //    gr.Text = " <b>X</b> ";
                //    cc.Text = "&nbsp;";
                //    tr.Text = "";
                //}
                //else if (rs.Rows[0]["CaraBayar"].ToString() == "KK")
                //{
                //    gr.Text = "&nbsp;";
                //    cc.Text = " <b>X</b> ";
                //    tr.Text = "&nbsp;";
                //}
                //else if (rs.Rows[0]["CaraBayar"].ToString() == "KD")
                //{
                //    gr.Text = "&nbsp;";
                //    cc.Text = " <b>X</b> ";
                //    tr.Text = "&nbsp;";
                //}
                //else
                //{
                //    gr.Text = "&nbsp;";
                //    cc.Text = "&nbsp;";
                //    tr.Text = " <b>X</b> ";
                //}

            }
        }

        private void FillTable(string Tipe, string Unit)
        {
            int nobkm = Db.SingleInteger("SELECT NoBKM FROM MS_TTS WHERE NoTTS = " + nomor);
            string NoKontrak = Db.SingleString("SELECT b.NoKontrak FROM ISC064_MARKETINGJUAL..MS_TAGIHAN a JOIN ISC064_MARKETINGJUAL..MS_PELUNASAN b ON a.NoUrut = b.NoTagihan WHERE NoTTS = " + nomor);
            string NoKontrak2 = Db.SingleString("SELECT Ref FROM MS_TTS WHERE NoTTS = " + nomor);
            decimal t = 0;
            decimal Total2 = 0;
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

            tbTagihan.Style["border-collapse"] = "collapse";

            TableRow trh = new TableRow();
            trh.HorizontalAlign = HorizontalAlign.Center;

            tbTagihan.Rows.Add(trh);
            TableCell ch;
            ch = new TableCell();
            ch.BorderWidth = 2;
            ch.CssClass = "tbTagihan";
            ch.Text = "<b>NO<b/>";
            trh.Cells.Add(ch);

            ch = new TableCell();
            ch.Text = "<b>KETERANGAN<b/>";
            ch.CssClass = "tbTagihan";
            ch.BorderWidth = 2;
            trh.Cells.Add(ch);

            ch = new TableCell();
            ch.Text = "<b>JUMLAH<b/>"; ;
            ch.CssClass = "tbTagihan";
            ch.BorderWidth = 2;
            trh.Cells.Add(ch);

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
                        Ket = "SEBAGIAN " + rs.Rows[i]["NamaTagihan"].ToString() + ", ";
                    }
                    else
                    {
                        if (a == 1)
                        {
                            Ket = " " + rs.Rows[i]["NamaTagihan"].ToString() + ", ";
                        }
                        else
                        {
                            Ket = "PELUNASAN " + rs.Rows[i]["NamaTagihan"].ToString() + ", ";
                        }
                    }
                }
                else
                {
                    if (NilaiTagihan != NilaiPelunasan)
                    {
                        Ket = "SEBAGIAN " + rs.Rows[i]["NamaTagihan"].ToString() + " ";
                    }
                    else
                    {
                        if (a == 1)
                        {
                            Ket = " " + rs.Rows[i]["NamaTagihan"].ToString() + " ";
                        }
                        else
                        {
                            Ket = "PELUNASAN " + rs.Rows[i]["NamaTagihan"].ToString() + " ";
                        }
                    }

                }

                TableRow tr = new TableRow();
                tr.HorizontalAlign = HorizontalAlign.Center;
                
                tbTagihan.Rows.Add(tr);
                TableCell c;
                c = new TableCell();
                c.Text = NoUrut.Rows[i]["NoUrut"].ToString();
                c.BorderWidth = 2;
                tr.Cells.Add(c);

                c = new TableCell();
                c.BorderWidth = 2;
                c.Text = Ket;
                tr.Cells.Add(c);

                c = new TableCell();
                c.BorderWidth = 2;
                c.Text = "Rp. " + Cf.Num(NilaiTagihan);
                c.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(c);

                Total2 += NilaiTagihan;

            }
            TableRow tr2 = new TableRow();
            tbTagihan.Rows.Add(tr2);
            TableCell c2;
            c2 = new TableCell();
            c2.ColumnSpan = 2;
            c2.Text = "<b>Total</b>";
            c2.Style["padding-right"] = "5px";
            c2.BorderStyle = BorderStyle.None;
            c2.HorizontalAlign = HorizontalAlign.Right;
            tr2.Cells.Add(c2);

            c2 = new TableCell();
            c2.BorderWidth = 2;
            c2.Text = "Rp. " + Cf.Num(Total2);
            c2.HorizontalAlign = HorizontalAlign.Center;
            tr2.Cells.Add(c2);



            // pembayaran.Text = Ket + "Unit " + Unit;
            //ketkwi.Text = Ket;
            noUnit.Text = Unit;
            terbilang.Text = Money.Str(Total2) + " RUPIAH";
            t = Convert.ToDecimal(Total);
        }
    }
}
