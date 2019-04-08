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

namespace ISC064
{
    public partial class AutoDailyApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_PROJECT");
            //Daily Report
            LibApi.DeleteReport(LibApi.TipeReport(1));
            LibApi.DeleteFolderPDF(Param.FolderPDF + LibApi.TipeReport(1));

            //Laporan Penjualan
            LibApi.DeleteReport(LibApi.TipeReport(2));
            LibApi.DeleteFolderPDF(Param.FolderPDF + LibApi.TipeReport(2));

            //Laporan Batal Kontrak
            LibApi.DeleteReport(LibApi.TipeReport(3));
            LibApi.DeleteFolderPDF(Param.FolderPDF + LibApi.TipeReport(3));

            //Management Report
            LibApi.DeleteReport(LibApi.TipeReport(4));
            LibApi.DeleteFolderPDF(Param.FolderPDF + LibApi.TipeReport(4));

            for (int i=0; i < rs.Rows.Count; i++)
            {
                string project = Cf.Str(rs.Rows[i]["Project"]).ToString();

                //Daily Report
                LibApi.CreateFilePDFDailyReport(DateTime.Today.AddDays(-1), "server", project , true);

                //Laporan Penjualan
                LibApi.CreateFilePDFLapPenjualan("server", "SEMUA", "SEMUA", "SEMUA", Cf.AwalBulan(), Cf.AkhirBulan(), project, true);

                //Laporan Batal Kontrak
                LibApi.CreateFilePDFLapBatalKontrak("server", "SEMUA", "SEMUA", Cf.AwalBulan(), Cf.AkhirBulan(), project, true);

                //Management Report
                LibApi.CreateFilePDFManagementReport(DateTime.Today.AddDays(-1), "server",project, true);
            }      
        }        
    }
}