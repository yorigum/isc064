using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace ISC064.LaporanApi
{
    public partial class CallDailyReportApi : System.Web.UI.Page
    {
        protected DateTime Tgl { get { return Convert.ToDateTime(Request.QueryString["Tgl"]); } }
        protected string UserID { get { return Request.QueryString["UserID"]; } }
        protected string Project { get { return Request.QueryString["Project"]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            LibApi.CreateFilePDFDailyReport(Tgl, UserID, Project, false);
        }        
    }
}