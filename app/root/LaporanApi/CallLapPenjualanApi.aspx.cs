using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.LaporanApi
{
    public partial class CallLapPenjualanApi : System.Web.UI.Page
    {
        protected string UserID { get { return Request.QueryString["UserID"]; } }
        protected string LokasiID { get { return Request.QueryString["LokasiID"]; } }
        protected string TipeID { get { return Request.QueryString["TipeID"]; } }
        protected DateTime Dari { get { return Convert.ToDateTime(Request.QueryString["Dari"]); } }
        protected DateTime Sampai { get { return Convert.ToDateTime(Request.QueryString["Sampai"]); } }
        protected string Project { get { return Request.QueryString["Project"]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            LibApi.CreateFilePDFLapPenjualan(UserID, LokasiID, TipeID, "SEMUA", Dari, Sampai, Project, false);
        }
    }
}