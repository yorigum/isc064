using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class UnitPetaDetil : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();


           // fillLegend();
            Display();
        }
        protected void fillLegend()
        {
            string ParamID = "WarnaUnitJual" + Project;
            string ParamID2 = "WarnaUnitBooked" + Project;
            string ParamID3 = "WarnaUnitCancel" + Project;
            string ParamID4 = "WarnaUnitHold" + Project;
            string jual = Db.SingleString("SELECT ISNULL(MAX(Value), 'FFFFFF') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "' ");
            string booked = Db.SingleString("SELECT ISNULL(MAX(Value), 'FFFFFF') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID2 + "' ");
            string cancel = Db.SingleString("SELECT ISNULL(MAX(Value), 'FFFFFF') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID3 + "' ");
            string hold = Db.SingleString("SELECT ISNULL(MAX(Value), 'FFFFFF') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID4 + "' ");
            legend.Text = "<table><tr>";

            addLegendColor(jual, "Sold");
            addLegendColor(booked, "Reserved");
            addLegendColor(cancel, "Available");
            addLegendColor(hold, "Hold Internal");

            legend.Text += "</tr><table>";
        }

        protected void addLegendColor(string code, string name)
        {

            bool isValidCode = true;

            code = code.Trim();
            name = name.Trim();

            if (isValidCode)
            {
                legend.Text += "<td style='width:20px; padding:0 0 0 5px; border:solid 1px black; background-color:" + code + ";'>&nbsp;</td><td>&nbsp;:&nbsp;</td><td style='padding:0 5px 0 0;'>" + name + "</td>";
            }
        }

        private void Display()
        {
            string NamaPeta = Db.SingleString("Select Nama from ms_siteplan where id='" + PetaID + "'");

            var Peta = Db.Rs("Select ID,NAMA,PathGambarDasar,PathGambarTransparent from ms_siteplan where id='" + PetaID + "'");

            string strSql = "SELECT DISTINCT NoUnit,NoStock,Koordinat FROM MS_UNIT WHERE "
                + " Peta = '" + NamaPeta + "'";
            DataTable rs = Db.Rs(strSql);

            var siteplan = new SitePlan(1000, 603, "FP/Base/PETA_" + PetaID + ".jpg", "FP/Base/PETA_" + PetaID + ".png");
            foreach (DataRow r in rs.Rows)
            {
                siteplan.Draw(r[2].ToString(), Color(r["NoStock"].ToString()), TableStock.Href(r["NoStock"].ToString()), "tooltip-url", "TooltipSiteplan.aspx?NoStock=" + r["NoStock"], r["NoStock"].ToString());
            }
            siteplan.Attributes.Add("style", "position:relative;top:50px;left:50px;");
            list.Controls.Add(siteplan);
        }

        private string Color(string NoStock)
        {
            var Unit = Models.Data.Unit(NoStock);
            if (Unit == null) return "black";
            if (Unit.Status == "B")
            {
                var adaKontrak = Db.SingleInteger("Select count(*) from ms_kontrak where NoStock='" + NoStock + "' and Status='A'") > 0;
                int adaPriority = Db.SingleInteger("Select count(*) from MS_NUP_PRIORITY where NoStock='" + NoStock + "' AND Tipe='RUMAH'");

                string NoNUPPriority = Db.SingleString("SELECT NoNUP FROM MS_NUP_PRIORITY WHERE NoStock='" + NoStock + "'");
                decimal TotBayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNup = '" + NoNUPPriority + "' AND Tipe = 'RUMAH'");

                int CountLun = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP_PELUNASAN WHERE NoNUP = '" + NoNUPPriority + "'");
                string Kon = Db.SingleString("SELECT Nokontrak FROM MS_NUP_PRIORITY WHERE NoNup = '" + NoNUPPriority + "'");
                if (CountLun == 1 && Kon == "")
                {
                    return "RGBa(255, 192, 203,0.5)"; //Unit DI Pilih //Pink
                }
                else
                {
                    return "RGBa(255,0, 0,0.5)";
                }
            }
            else if (Unit.Status == "H")
            {
                var adaPriority = Db.SingleInteger("Select count(*) from MS_NUP_PRIORITY where NoStock='" + NoStock + "'") > 0;
                if (adaPriority)
                    return "RGBa(255, 192, 203,0.6)"; //Unit DI Pilih //Pink
                else
                    return "RGBa(255, 0, 0, 0.5)";//Hold Internal //Gray
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
                    return "RGBa(255, 255, 255, 0.5)";//Yellow
                }
            }
        }
        
        private int ColorHexToInt(string source, string target)
        {
            int startindex = 0;
            if (target == "G")
                startindex = 2;
            else if (target == "B")
                startindex = 4;

            return int.Parse(source.Replace("#", "").Substring(startindex, 2), System.Globalization.NumberStyles.HexNumber);
        }
        private string PetaID
        {
            get
            {
                return Cf.Pk(Request.QueryString["id"]);
            }
        }
        private string Project
        {
            get
            {
                return Db.SingleString("SELECT ISNULL(Project, '') FROM MS_SITEPLAN WHERE ID = " + PetaID);
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
