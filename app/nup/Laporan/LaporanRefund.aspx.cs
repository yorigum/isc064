using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP.Laporan
{
    public partial class LaporanRefund : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				comp.InnerHtml = Mi.Pt;
				rpt.Visible = false;
				Js.Focus(this,scr);
				//init();
				if(!Act.Sec("DownloadExcel")) xls.Enabled = false;
			}
		}

        //private void init()
        //{
        //    DataTable rs;

        //    rs = Db.Rs("SELECT DISTINCT Principal FROM MS_AGENT WHERE Status = 'A' ORDER BY Principal");
        //    for (int i = 0; i < rs.Rows.Count; i++)
        //        principal.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

        //    principal.SelectedIndex = 0;
        //    input.SelectedIndex = 0;
        //}

        //private bool valid()
        //{
        //    string s = "";
        //    bool x = true;

        //    if (!Cf.isPilih(nama))
        //    {
        //        x = false;
        //        namac.Text = "Pilih Minimum Satu";
        //    }
        //    else
        //        namac.Text = "";

        //    if (!x && s != "")
        //    {
        //        RegisterStartupScript("err"
        //            , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
        //    }

        //    return x;
        //}

		protected void scr_Click(object sender, System.EventArgs e)
		{
            //if(valid())
            //{
				Report();
			//}
		}
		protected void xls_Click(object sender, System.EventArgs e)
		{
            //if(valid())
            //{
				Report();
                //Rpt.ToExcel(this,rpt);
                Rpt.ToExcel(this, headReport, rpt);
            //}
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

            //string status = "";
            //if(statusS.Checked) status = statusS.Text;
            //if(statusA.Checked) status = statusA.Text;
            //if(statusI.Checked) status = statusI.Text;

            //Rpt.SubJudul(x
            //    , "Status : " + status
            //    );

            //Rpt.SubJudul(x
            //    , "Principal : " + principal.SelectedItem.Text
            //    );

            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

		private void Fill()
		{
            //string Status = "";
            //if(statusA.Checked) Status = " AND Status = 'A'";
            //if(statusI.Checked) Status = " AND Status = 'I'";

            //string Principal = "";
            //if(principal.SelectedIndex != 0)
            //{
            //    Principal = " AND Principal = '" + Cf.Str(principal.SelectedValue) + "'";
            //}

			string strSql = "SELECT "
				+ " a.NoNUP"
				+ ",a.NoCustomer"
				+ ",a.NilaiBayar"
				+ ",a.Status"
                + ",b.Nama AS Nama"
                + ",b.NoKTP AS NoKTP"
                + ",b.KTP1 AS KTP1"
                + ",b.KTP2 AS KTP2"
                + ",b.KTP3 AS KTP3"
                + ",b.KTP4 AS KTP4"
                + ",b.NoHP AS NoHP"
                + ",b.RekBank AS RekBank"
                + ",b.RekNo AS RekNo"
                + ",b.RekNama AS RekNama"
                + ",b.RekCabang AS RekCabang"
				+ " FROM MS_NUP a inner join MS_CUSTOMER b on a.NoCustomer = b.NoCustomer"
				+ " WHERE 1=1 AND a.Status = 5" //status Refund
				//+ Status
				//+ Principal
				+ " ORDER BY a.NoNUP";
			
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;
				r.Attributes["ondblclick"] = "popEditNUP('"+rs.Rows[i]["NoNUP"]+"')";
				
				c = new TableCell();
				c.Text = (i + 1).ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Str(rs.Rows[i]["NoNUP"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["Nama"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["NoKTP"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
                c.Text = rs.Rows[i]["KTP1"].ToString() + " &nbsp; " +Cf.Str(rs.Rows[i]["KTP2"]); ;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["KTP3"]) + " &nbsp; " + Cf.Str(rs.Rows[i]["KTP4"]); ;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["NoHP"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                // Master tama nilai refund 50% dari NUP
                decimal NilaiRefund = Convert.ToDecimal(rs.Rows[i]["NilaiBayar"]) / 2;
                if (NilaiRefund < 0)
                    NilaiRefund = 0;
                c = new TableCell();
                c.Text = "Rp. " + Cf.Num(NilaiRefund);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["RekBank"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["RekNo"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["RekNama"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["RekCabang"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

				rpt.Rows.Add(r);
			}
		}

        //protected void namaCheck_CheckedChanged(object sender, System.EventArgs e)
        //{
        //    for(int i=0;i<nama.Items.Count;i++)
        //    {
        //        nama.Items[i].Selected = namaCheck.Checked;
        //    }

        //    Js.Focus(this, namaCheck);
        //    namac.Text = "";
        //}

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
