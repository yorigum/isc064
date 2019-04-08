using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY.Laporan
{
	public partial class DaftarUser : System.Web.UI.Page
	{
        //private string NoCustomer { get { return (Request.QueryString["NoCustomer"]); } }
        //private string Aktivitas { get { return (Request.QueryString["aktivitas"]); } }
        //private string User { get { return (Request.QueryString["user"]); } }
        private string AttachmentID { get { return Request.QueryString["id"]; } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        //private string IP { get { return (Request.QueryString["ip"]); } }
        protected void Page_Load(object sender, System.EventArgs e)
		{
            Report();
		}



		private void Report()
		{
			param.Visible = false;
			rpt.Visible = true;

			Header();
			Fill();
		}

		private void Header()
		{
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            if (StatusS != "")
            {
                Rpt.SubJudul(x
                    , "Status : SEMUA"
                    );
            }
            else if (StatusA != "")
            {
                Rpt.SubJudul(x
                    , "Status : AKTIF"
                    );
            }
            if (StatusB != "")
            {
                Rpt.SubJudul(x
                    , "Status : BLOKIR"
                    );
            }

            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

		private void Fill()
		{
            string Status = "";
            if (StatusA != "")
            {
                Status = " WHERE Status = 'A'";
            }
            else if (StatusB != "")
            {
                Status = " WHERE Status = 'B'";
            }
            else
            {
                Status = "";
            }


            string strSql = "SELECT * FROM " + Mi.DbPrefix + "SECURITY..USERNAME "
                + Status;

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["UserID"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["SecLevel"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
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
