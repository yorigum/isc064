using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class FloorPlan : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Act.Pass();
            //Act.NoCache();

            if (!Page.IsPostBack)
            {
                //if (Request.QueryString["can"] != null)
                //    Js.Alert(this, "Unit sedang di pilih Customer lain.", "");
            }

            Display();
        }

        private void Display()
        {

            string NamaPeta = Db.SingleString("Select Nama from ms_siteplan where id='" + PetaID + "'");

            var Peta = Db.Rs("Select ID,NAMA,PathGambarDasar,PathGambarTransparent from ms_siteplan where id='" + PetaID + "'");

            string strSql = "SELECT DISTINCT NoUnit,NoStock,Koordinat,Jenis FROM MS_UNIT WHERE "
                + " Peta = '" + NamaPeta + "' AND NoStock= '" + NoStock + "'";
            DataTable rs = Db.Rs(strSql);

            var siteplan = new SitePlan(130, 1000, "/marketingjual/FP/Base/PETA_" + PetaID + ".jpg", "/marketingjual/FP/Base/PETA_" + PetaID + ".png");

            //var siteplan = new SitePlan(1000, 1000, "FP/Base/PETA_" + PetaID + ".jpg", "FP/Base/PETA_" + PetaID + ".png");
            foreach (DataRow r in rs.Rows)
            {
                siteplan.Draw(r[2].ToString(), Color(NoStock), "", "tooltip-url", "", NoStock);
                //siteplan.Draw(r[2].ToString(), Color(r["NoStock"].ToString()), href(r["NoStock"].ToString()), "tooltip-url", "TooltipSiteplan.aspx?NoStock=" + r["NoStock"]);
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
                var adaPriority = Db.SingleInteger("Select count(*) from MS_NUP_PRIORITY where NoStock='" + NoStock + "'") > 0;
                //decimal Bayar = 0;
                //string NoNUPPriority = Db.SingleString("SELECT NoNUP FROM MS_NUP_PRIORITY WHERE NoStock='" + NoStock + "'");
                //decimal TotBayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNup = '" + NoNUPPriority + "' AND Tipe = '" + Tipe + "'");

                if (adaKontrak)
                    return "RGBa(255,0, 0,0.5)"; //sold //Merah
                else if (adaPriority)
                {
                    return "RGBa(255, 255, 0,0.5)";//NUP sdh pilih unit //Kuning
                }
                else
                    return "RGBa(255, 255, 0,0.5)";//NUP sdh pilih unit //Kuning
            }
            else if (Unit.Status == "H")
            {
                return "RGBa(255, 255, 0,0.5)"; //Unit DI Pilih //Kuning
            }
            else
            {
                int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_RESERVASI WHERE Status = 'A' AND NoStock = '" + NoStock + "'");
                if (c != 0)
                    return "RGBa(255, 255, 0,0.5)"; //Unit DI Pilih //Kuning
                else
                    //return "RGBa(255, 255, 255,0.5)"; //available //Putih
                    return "RGBa(0, 0, 255,0.5)"; //booked //Biru
            }

            //return "black";
        }

        private string PetaID
        {
            get
            {
                return Cf.Pk(Request.QueryString["id"]);
            }
        }
        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }
        private string ID
        {
            get
            {
                return Cf.Pk(Request.QueryString["id"]);
            }
        }
        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
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
