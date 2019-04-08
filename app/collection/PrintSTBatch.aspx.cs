using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace ISC064.COLLECTION
{
	public partial class PrintSTBatch : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				InitForm();
				Js.Focus(this, print);
                Act.ProjectList(project);
				BelumPrint();//daftar Tunggakan yang belum print
			}
		}

        private void BelumPrint()
        {
            DataTable rs = Db.Rs("SELECT TglTunggakan, COUNT(NoTunggakan) AS BlmPrint FROM MS_TUNGGAKAN "
                + " WHERE PrintST = 0 AND Project = '" + project.SelectedValue + "'"
                + " GROUP BY TglTunggakan "
                + " ORDER BY TglTunggakan"
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglTunggakan"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = ":";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["BlmPrint"].ToString();
				r.Cells.Add(c);

				belumprint.Rows.Add(r);
			}
		}

		private void InitForm()
		{
			dari.Text = Cf.Day(DateTime.Today);
			sampai.Text = Cf.Day(DateTime.Today);
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(dari))
			{
				x = false;
				if(s=="") s = dari.ID;
				daric.Text = "Tanggal";
			}
			else
				daric.Text = "";

			if(!Cf.isTgl(sampai))
			{
				x = false;
				if(s=="") s = sampai.ID;
				sampaic.Text = "Tanggal";
			}
			else
				sampaic.Text = "";

			if(!x && s!="")
			{
				RegisterStartupScript("err"
					,"<script language='javascript'>document.getElementById('"+s+"').select()</script>");
			}

			return x;
		}

		protected void print_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				reprint.Visible = false;
				Js.AutoPrint(this);
                Fill();
            }
        }

        private void ConvertPdf()
        {
            Process p = new System.Diagnostics.Process();
            //DateTime Dari = Convert.ToDateTime(Request.QueryString["dari"]);
            //DateTime Sampai = Convert.ToDateTime(Request.QueryString["sampai"]);
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            string myHtml = "http://localhost:8034/collection/PrintSTBatch1.aspx?Dari="+ Cf.Tgl(Dari) + "&Sampai=" + Cf.Tgl(Sampai) + "&project=" + project.SelectedValue;

            string save = Param.PathFilePDFCollection + Cf.Tgl(Dari) +"&"+ Cf.Tgl(Sampai) + "&" + project.SelectedValue + "_STBatch.pdf";
            string link = Param.PathLinkFilePDFCollection + Cf.Tgl(Dari) +"&"+ Cf.Tgl(Sampai) + "&" + project.SelectedValue + ".pdf";

            p.StartInfo.Arguments = "--orientation portrait --page-width 8.5in --page-height 11in --margin-left 2cm --margin-right 2cm --margin-top 1.25cm --margin-bottom 0 " + myHtml + " " + save;
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();
            p.WaitForExit(60000);
        }

		private void Fill()
		{
			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			if(Dari>Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = x;
			}

			DataTable rs = Db.Rs("SELECT "
				+ " NoTunggakan"
				+ " FROM MS_TUNGGAKAN a JOIN "+Mi.DbPrefix+"MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
				+ " WHERE 1=1"
				+ " AND CONVERT(varchar,TglTunggakan,112) >= '" + Cf.Tgl112(Dari) + "'"
				+ " AND CONVERT(varchar,TglTunggakan,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND b.Project = '"+project.SelectedValue+"'"
				+ " ORDER BY NoTunggakan");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				Print((int)rs.Rows[i]["NoTunggakan"]);

				if(i!=rs.Rows.Count-1)
				{
					Label pb = new Label();
					pb.Text = "<div style='page-break-after:always'></div>";
					list.Controls.Add(pb);
				}
			}
		}

		private void Print(int NoTunggakan)
		{
			//increment
			Db.Execute("UPDATE MS_TUNGGAKAN SET PrintST = PrintST + 1 "
				+ " WHERE NoTunggakan = " + NoTunggakan);

			//Logfile
			DataTable rs = Db.Rs("SELECT "
				+ " CONVERT(varchar, TglTunggakan, 106) AS [Tanggal]"
				+ ",Tipe"
				+ ",Ref AS [Ref.]"
				+ ",Unit"
				+ ",Customer"
				+ ",Total"
				+ ",LevelTunggakan AS [Level]"
				+ " FROM MS_TUNGGAKAN WHERE NoTunggakan = " + NoTunggakan);

            Db.Execute("EXEC spLogTunggakan"
                + " 'P-ST'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + NoTunggakan.ToString().PadLeft(7, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TUNGGAKAN_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TUNGGAKAN WHERE NoTunggakan = '" + NoTunggakan + "') ");
            Db.Execute("UPDATE MS_TUNGGAKAN_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            //ConvertPdf();
            //Response.Redirect(Param.PathLinkFilePDFCollection + Cf.Tgl(Dari) + "&" + Cf.Tgl(Sampai) + "_STBatch.pdf");

            string file = Param.PathFilePDFCollection + Cf.Tgl(Dari) + "&" + Cf.Tgl(Sampai) + "&" + project.SelectedValue + "_STBatch.pdf";
            bool exist = System.IO.File.Exists(file);
            if (exist)
            {
                System.IO.File.Delete(file);
            }
            ConvertPdf();
            Response.Redirect(Param.PathLinkFilePDFCollection + Cf.Tgl(Dari) + "&" + Cf.Tgl(Sampai) + "&" + project.SelectedValue + "_STBatch.pdf");

            //Template
            PrintSTTemplate uc = (PrintSTTemplate) Page.LoadControl("PrintSTTemplate.ascx"); 
			uc.NoTunggakan = NoTunggakan.ToString();
            uc.Project = project.SelectedValue;
			list.Controls.Add(uc);
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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            BelumPrint();
        }
    }
}
