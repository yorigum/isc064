using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class IlustrasiKPR : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            months.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            months.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            months.Attributes["onblur"] = "CalcBlur(this);";

            rate.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            rate.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            rate.Attributes["onblur"] = "CalcBlur(this);";

            loan.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            loan.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            loan.Attributes["onblur"] = "CalcBlur(this);";

            decimal NilaiPlafond = 0;
            NilaiPlafond = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");
            loan.Value = Cf.Num(NilaiPlafond);

            rate.Value = Cf.Num(9);
        }



        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
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
