using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class ShowPetaTV : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {

            if (!Page.IsPostBack)
            {

            }

            Display();
            Count();
        }
        private void Count()
        {
            decimal Total = Db.SingleDecimal("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT WHERE Lokasi='RK'");
            decimal Belum = Db.SingleDecimal("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT WHERE Status='H' AND Lokasi='RK' AND NoStock NOT IN (SELECT NoStock FROM MS_NUP_PRIORITY WHERE NoStock=MS_UNIT.NoStock)");
            decimal StockBergerak = (Total - Belum);

            decimal Terjual = Db.SingleDecimal("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT WHERE Lokasi='RK' AND NoStock IN (SELECT NoStock FROM MS_NUP_PRIORITY WHERE NoStock=MS_UNIT.NoStock)");
            
            total.InnerText = Total.ToString();
            belum.InnerText = Belum.ToString();

            stok.InnerText = (StockBergerak - Terjual).ToString();
            terjual.InnerText = Terjual.ToString();
            jumlah.InnerText = StockBergerak.ToString();
            //196
        }
        private void Display()
        {

            string NamaPeta = Db.SingleString("Select Nama from ms_siteplan where id='" + PetaID + "'");

            var Peta = Db.Rs("Select ID,NAMA,PathGambarDasar,PathGambarTransparent from ms_siteplan where id='" + PetaID + "'");

            string strSql = "SELECT DISTINCT NoUnit,NoStock,Koordinat,Jenis FROM MS_UNIT WHERE "
                + " Peta = '" + NamaPeta + "'";
            DataTable rs = Db.Rs(strSql);
            //width height
            var siteplan = new SitePlan(900, 500, "/marketingjual/FP/Base/PETA_" + PetaID + ".jpg", "/marketingjual/FP/Base/PETA_" + PetaID + ".png");


            foreach (DataRow r in rs.Rows)
            {
                siteplan.Draw(r[2].ToString(), Color(r["NoStock"].ToString()),"", "tooltip-url", "TooltipSiteplan.aspx?NoStock=" + r["NoStock"], r["NoStock"].ToString());
            }
            siteplan.Attributes.Add("style", "position:relative;top:50px;left:50px;");
            list.Controls.Add(siteplan);
        }

        private string href(string NoStock)
        {
            string x = "";

            string strSql = "SELECT Status, NoStock, NoUnit FROM MS_UNIT WHERE NoStock = '" + NoStock + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count != -0)
            {
                if (rs.Rows[0]["Status"].ToString() == "B")
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK"
                        + " WHERE Status = 'A' AND NoStock = '" + rs.Rows[0]["NoStock"] + "'");
                    if (c != 0)
                    {
                        string NoKontrak = Db.SingleString("SELECT TOP 1 NoKontrak FROM MS_KONTRAK WHERE Status = 'A' AND NoStock = '" + rs.Rows[0]["NoStock"] + "'");

                        string NoTTS = "";
                        DataTable tts = Db.Rs("SELECT TOP 1 NoTTS FROM ISC064_FINANCEAR..MS_TTS WHERE Ref = '" + NoKontrak + "' AND Status <> 'VOID' ORDER BY NoTTS ASC");
                        if (tts.Rows.Count != 0)
                            NoTTS = tts.Rows[0][0].ToString();

                        x = "";// "TabelStok3.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS; //sold
                    }
                    else
                        x = ""; //hold internal
                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_RESERVASI"
                        + " WHERE NoStock = '" + NoStock + "'");
                    if (c != 0)
                        //x = "KontrakDaftar2.aspx?NoStock=" + rs.Rows[0]["NoStock"]; //booked
                        x = "";
                    else
                    {
                        x = "";
                    }
                }
            }

            return x;
        }
        private string Color(string NoStock)
        {
            var Unit = Models.Data.Unit(NoStock);
            if (Unit == null) return "black";
            if (Unit.Status == "B")
            {
                var adaKontrak = Db.SingleInteger("Select count(*) from ms_kontrak where NoStock='" + NoStock + "' and Status='A'") > 0;
                int adaPriority = Db.SingleInteger("Select count(*) from MS_NUP_PRIORITY where NoStock='" + NoStock + "' AND Tipe='RK'");

                string NoNUPPriority = Db.SingleString("SELECT NoNUP FROM MS_NUP_PRIORITY WHERE NoStock='" + NoStock + "'");
                decimal TotBayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNup = '" + NoNUPPriority + "' AND Tipe = 'RK'");

                int CountLun = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP_PELUNASAN WHERE NoNUP = '" + NoNUPPriority + "'");
                string Kon = Db.SingleString("SELECT Nokontrak FROM MS_NUP_PRIORITY WHERE NoNup = '"+ NoNUPPriority + "'");

                if (CountLun == 2)
                {
                    return "RGBa(255,0, 0,0.6)"; //Merah //Sudah Bayar Lunas tp blm terjadi kontrak
                }
                else if(CountLun == 1 && Kon == "")
                {
                    return "RGBa(255, 192, 203,0.6)"; //Unit DI Pilih //Pink
                }
                else
                {
                    string Nomor = Db.SingleString("SELECT Lantai FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                    if (Nomor == "2")
                    {
                        decimal Panjang = Db.SingleDecimal("SELECT Panjang FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                        decimal Lebar = Db.SingleDecimal("SELECT Lebar FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                        if (Panjang == 6 && Lebar == 11)
                        {
                            return "RGBa(249,173,102,0.6)";//Orange
                        }
                        else
                        {
                            if (NoStock == "0000086" || NoStock == "0000070")
                            {
                                return "RGBa(249,173,102,0.6)";//Orange
                            }
                            else
                                return "RGBa(119,204,221,0.6)";//Blue
                        }
                    }
                    else if (Nomor == "6")
                    {
                        return "RGBa(163,207,97,0.6)";//Green
                    }
                    else
                    {
                        return "RGBa(249,238,88,0.6)";//Yellow
                    }
                }
            }
            else if (Unit.Status == "H")
            {
                var adaPriority = Db.SingleInteger("Select count(*) from MS_NUP_PRIORITY where NoStock='" + NoStock + "'") > 0;
                if (adaPriority)
                    return "RGBa(255, 192, 203,0.6)"; //Unit DI Pilih //Pink
                else
                    return "RGBa(128, 128, 128,0.6)";//Hold Internal //Gray
            }
            else
            {
                string Nomor = Db.SingleString("SELECT Lantai FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                if (Nomor == "2")
                {
                    decimal Panjang = Db.SingleDecimal("SELECT Panjang FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                    decimal Lebar = Db.SingleDecimal("SELECT Lebar FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                    if (Panjang == 6 && Lebar == 11)
                    {
                        return "RGBa(249,173,102,0.6)";//Orange
                    }
                    else
                    {
                        if (NoStock == "0000086" || NoStock == "0000070")
                        {
                            return "RGBa(249,173,102,0.6)";//Orange
                        }
                        else
                            return "RGBa(119,204,221,0.6)";//Blue
                    }
                }
                else if (Nomor == "6")
                {
                    return "RGBa(163,207,97,0.6)";//Green
                }
                else
                {
                    return "RGBa(249,238,88,0.6)";//Yellow
                }
            }
        }
        private string PetaID
        {
            get
            {
                return Cf.Pk(Request.QueryString["id"]);
            }
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}
