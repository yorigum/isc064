namespace ISC064.MARKETINGJUAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;

    public partial class PrintFPSTemplate : System.Web.UI.UserControl
    {

        //Passing parameter
        public string nomor;
        public string NoKontrak
        {
            set { nomor = value; }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Fill();
        }

        protected void Fill()
        {
            string strSql = "SELECT a.*, b.*"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " WHERE NoKontrak = '" + nomor+"'"
                ;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count != 0)
            {
                //System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
                //string HeaderPajak = "";
                //string JenisPPN = Db.SingleString("SELECT JenisPPN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Cf.Pk(rs.Rows[0]["NoKontrak"]) + "'");
                //if (JenisPPN == "PEMERINTAH")
                //    HeaderPajak = (string)s.GetValue("NoFPSPemerintah", typeof(string));
                //else if (JenisPPN == "KONSUMEN")
                //    HeaderPajak = (string)s.GetValue("NoFPSKonsumen", typeof(string));

                nama.Text = rs.Rows[0]["Nama"].ToString();
                nopajak.Text = rs.Rows[0]["NoFPS"].ToString();
                strSql = "SELECT KTP1, KTP2, KTP3, KTP4"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER"
                    + " WHERE NoCustomer = " + Cf.Pk(rs.Rows[0]["NoCustomer"])
                    ;
                DataTable rsCs = Db.Rs(strSql);
                alamat.Text = rsCs.Rows[0]["KTP1"]
                    + "<br />"
                    + rsCs.Rows[0]["KTP2"]
                    + "<br />"
                    + rsCs.Rows[0]["KTP3"]
                    + "<br />"
                    + rsCs.Rows[0]["KTP4"]
                    ;
                //Tanggal FPS
                //DateTime dt = DateTime.Now;
                //string tglFPS = Cf.Day(dt);
                //tgl.Text = Convert.ToDateTime(dt).Day
                //    + " "
                //    + Cf.Monthname(Convert.ToDateTime(dt).Month)
                //    + " "
                //    + Convert.ToDateTime(dt).Year
                //    ;
               
                tgl.Text = Convert.ToDateTime(rs.Rows[0]["TglAJB"]).Day
                + " "
                + Cf.Monthname(Convert.ToDateTime(rs.Rows[0]["TglAJB"]).Month)
                + " "
                + Convert.ToDateTime(rs.Rows[0]["TglAJB"]).Year
                ;

                //Db.Execute("UPDATE MS_KONTRAK SET TglFPS='" + rs.Rows[0]["TglBKM"] + "' WHERE NoKontrak ='" + nomor + "'");

                FillTable();
            }
        }

        protected void FillTable()
        {
            //string ManualBKM = Db.SingleString("SELECT ManualBKM FROM MS_TTS WHERE NoTTS = " + nomor);
            //string strSql = "SELECT "
            //    + " NilaiPelunasan AS Nilai"
            //    + ", NoKontrak"
            //    + ",NoKontrak + '.' + CONVERT(VARCHAR,NoTagihan) AS RefTagihan"
            //    + ",CASE NoTagihan"
            //    + "		WHEN 0 THEN 'UNALLOCATED'"
            //    + "		ELSE (SELECT NamaTagihan FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
            //    + " END AS NamaTagihan"
            //    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN AS l "
            //    + " WHERE NoTTS = " + nomor;
            string strSql = "SELECT NoUnit,Gross,DiskonRupiah FROM MS_KONTRAK WHERE NoKontrak = '"+ nomor +"'";
            DataTable rs = Db.Rs(strSql);

            decimal t = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                Label l;

                l = new Label();
                l.Text = "<tr><td style='border-right: 1ps solid Black;' valign='top'>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = (i + 1).ToString();
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<br/><br/></br><br/><br/></br></td><td style='border-right: 1ps solid Black;' valign='top'>";
                list.Controls.Add(l);

                l = new Label();
                //l.Text = "<table>"
                //    + "<tr>"
                //    + "<td><br/>1 Unit Apartemen Pancoran Riverside</td>"
                //    + "<td><br/>:</td>"
                //    + "<td><br/>" + rs.Rows[0]["NoUnit"] + "</td>"
                //    + "</tr>"
                //    //+ "<tr>"
                //    //+ "<td>Pembayaran</td>"
                //    //+ "<td>:</td>"
                //    //+ "<td>" + rs.Rows[i]["NamaTagihan"].ToString() + "</td>"
                //    //+ "</tr>"
                //    + "</table><br/><br/><br/><br/><br/><br/>"
                //    ;
                l.Text = "1 Unit Apartemen Pancoran Riverside : " + rs.Rows[i]["NoUnit"].ToString();
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<br/><br/><br/></td><td align='right' valign='top'>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = Cf.Num(rs.Rows[i]["Gross"])+"</td></tr>";
                list.Controls.Add(l);

                //l = new Label();
                //l.Text = "</td><td style='border-right: 1ps dashed Black;' valign='top'>";
                //list.Controls.Add(l);

                //l = new Label();
                //l.Text = "unit";
                //list.Controls.Add(l);

                //l = new Label();
                //l.Text = "</td><td align='right' valign='top'>";
                //list.Controls.Add(l);

                //l = new Label();
                //l.Text = Cf.Num(rs.Rows[i]["Gross"])+"</td></tr>";
                //list.Controls.Add(l);

                //l = new Label();
                //l.Text = "</td><td align='right' valign='top'><br/>";
                //list.Controls.Add(l);

                //l = new Label();
                //l.Text = Cf.Num(rs.Rows[i]["Gross"]);
                //list.Controls.Add(l);

                //l = new Label();
                //l.Text = "</td></tr>";
                //list.Controls.Add(l);

                t += Convert.ToDecimal(rs.Rows[i]["Gross"]);

                if (i == (rs.Rows.Count - 1))
                    SubTotal(t);
            }
        }

        protected void SubTotal(decimal t)
        {
            //string NoKontrak = Db.SingleString("SELECT Ref FROM MS_TTS WHERE NoTTS = " + nomor);
            string JenisPPN = Db.SingleString("SELECT JenisPPN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + nomor + "'");

            string strSql = "SELECT DiskonRupiah FROM MS_KONTRAK WHERE NoKontrak = '" + nomor + "'";
            DataTable rs = Db.Rs(strSql);

            Label l;

            l = new Label();
            l.Text = "<tr><td style='border-top: 1px solid Black; border-right: 1px solid Black;' colspan='2'>Harga Jual/Penggantian/Uang Muka/Termin *)</td>";
            list.Controls.Add(l);

            l = new Label();
            l.Text = "<td align='right' style='border-top: 1px solid Black;'>" + Cf.Num(t) + "</td></tr>";
            list.Controls.Add(l);

            l = new Label();
            l.Text = "<tr><td style='border-top: 1px solid Black; border-right: 1px solid Black;' colspan='2'>Dikurangi Potongan Harga</td>";
            list.Controls.Add(l);

            l = new Label();
            l.Text = "<td align='right' style='border-top: 1px solid Black;'>" + Cf.Num(Convert.ToDecimal(rs.Rows[0]["DiskonRupiah"])) + "</td></tr>";
            list.Controls.Add(l);

            //Harga setelah dikurangi diskon
            decimal afterdiscon = (t-(Convert.ToDecimal(rs.Rows[0]["DiskonRupiah"])));
            //l = new Label();
            //l.Text = "<tr><td style='border-top: 1px dashed Black; border-right: 1px solid Black;' colspan='3'>&</td>";
            //list.Controls.Add(l);

            //l = new Label();
            //l.Text = "<td align='right' style='border-top: 1px solid Black;'>" + Cf.Num(afterdiscon) + "</td></tr>";
            //list.Controls.Add(l);

            l = new Label();
            l.Text = "<tr><td style='border-top: 1px solid Black; border-right: 1px solid Black;' colspan='2'>Dikurangi Uang Muka yang telah diterima</td>";
            list.Controls.Add(l);


            //Nominal Bunga
            decimal bungaNominal = Db.SingleDecimal("SELECT ISNULL(BungaNominal,0) FROM MS_KONTRAK WHERE NoKontrak = '" + nomor + "'");
            
            l = new Label();
            l.Text = "<td align='right' style='border-top: 1px solid Black;'>&nbsp;</td></tr>";
            list.Controls.Add(l);

            //Harga setelah ditambah bunga
            decimal afterbunga = afterdiscon + bungaNominal;
            l = new Label();
            l.Text = "<tr><td style='border-top: 1px solid Black; border-right: 1px solid Black;' colspan='2'>Dasar Pengenaan Pajak</td>";
            list.Controls.Add(l);

            l = new Label();
            l.Text = "<td align='right' style='border-top: 1px solid Black;'>" + Cf.Num(afterbunga) + "</td></tr>";
            list.Controls.Add(l);

            l = new Label();
            l.Text = "<tr><td style='border-top: 1px solid Black; border-right: 1px solid Black;' colspan='2'>PPN = 10% x Dasar Pengenaan Pajak</td>";
            list.Controls.Add(l);

            l = new Label();
            l.Text = "<td align='right' style='border-top: 1px solid Black;'>" + Cf.Num((decimal)0.1 * afterbunga) + "</td></tr>";
            list.Controls.Add(l);

            //l = new Label();
            //if (JenisPPN == "PEMERINTAH")
            //    l.Text = "<td align='right' style='border-top: 1px dashed Black;'>" + Cf.Num(t) + "</td></tr>";
            //else if (JenisPPN == "KONSUMEN")
            //    l.Text = "<td align='right' style='border-top: 1px dashed Black;'>0</td></tr>";
            //list.Controls.Add(l);

            //l = new Label();
            //l.Text = "<tr><td style='border-top: 1px dashed Black; border-right: 1px dashed Black;' colspan='5'>PPN = 10% x Dasar Pengenaan Pajak</td>";
            //list.Controls.Add(l);

            //l = new Label();
            //if (JenisPPN == "PEMERINTAH")
            //    l.Text = "<td align='right' style='border-top: 1px dashed Black;'>" + Cf.Num((decimal)0.1 * t) + "</td></tr>";
            //else if (JenisPPN == "KONSUMEN")
            //    l.Text = "<td align='right' style='border-top: 1px dashed Black;'>0</td></tr>";
            //list.Controls.Add(l);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}