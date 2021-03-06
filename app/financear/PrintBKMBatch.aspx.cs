using System;
using System.Drawing;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Diagnostics;

namespace ISC064.FINANCEAR
{
	public partial class PrintBKMBatch : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				InitForm();
				Js.Focus(this, display);

                kui.Visible = false;
			}

            Fill2();
		}

		private void InitForm()
		{
			dari.Text = Cf.Day(DateTime.Today);
			sampai.Text = Cf.Day(DateTime.Today);
            Act.ProjectList(project);
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
				ClientScript.RegisterStartupScript(GetType(), "err"
					,"<script type='text/javascript'>document.getElementById('"+s+"').select()</script>");
			}

			return x;
		}

        protected void display_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                kui.Visible = true;
                Fill2();
            }
        }

		protected void print_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
                kui.Visible = false;
				reprint.Visible = false;
				Js.AutoPrint(this);

				Fill();
			}
		}

        private void Fill2()
        {
            list2.Controls.Clear();

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string Project = " AND ISC064_MARKETINGJUAL..MS_UNIT.Project = '" + project.SelectedValue + "'";

            DataTable rs = Db.Rs("SELECT "
                + " *"
                + " FROM MS_TTS"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_UNIT on MS_TTS.Unit = MS_UNIT.NoUnit"
                + " WHERE MS_TTS.Status = 'POST'"
                + " AND CONVERT(varchar,TglBKM,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,TglBKM,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + Project
                + " ORDER BY NoBKM");

            if (rs.Rows.Count == 0)
                print.Enabled = false;
            else
                print.Enabled = true;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow tr;
                HtmlTableCell c;
                CheckBox cb;

                tr = new HtmlTableRow();
                list2.Controls.Add(tr);

                cb = new CheckBox();
                cb.ID = "notts_" + i;

                c = new HtmlTableCell();
                c.ID = "pk_" + i;
                c.Attributes["title"] = rs.Rows[i]["NoTTS"].ToString();
                c.Controls.Add(cb);
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = "<a href=\"javascript:popEditTTS('" + rs.Rows[i]["NoTTS"] + "')\">"
                    + rs.Rows[i]["NoTTS"].ToString().PadLeft(7, '0')
                    + "</a>";
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Day(rs.Rows[i]["TglBKM"]);
                c.Width = "200px";
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Ref"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Unit"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Customer"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(rs.Rows[i]["Total"]);
                c.Align = "right";
                tr.Cells.Add(c);
            }
        }

        private void ConvertPdf(string NoTTS)
        {
            Process p = new System.Diagnostics.Process();
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            string Project = project.SelectedValue;

            string myHtml = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/financear/PrintBKMBatch1.aspx?id=" + NoTTS + "&project=" + Project;

            string save = Param.PathFilePDFFinanceAR + Cf.Tgl(Dari) + "&" + Cf.Tgl(Sampai) + "&" + Project + "_BKMBatch.pdf";
            string link = Param.PathLinkFilePDFFinanceAR + Cf.Tgl(Dari) + "&" + Cf.Tgl(Sampai) + "&" + Project + ".pdf";

            p.StartInfo.Arguments = "--orientation portrait --page-width 8.5in --page-height 11in --margin-left 2cm --margin-right 2cm --margin-top 1.25cm --margin-bottom 0 " + myHtml + " " + save;
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();
            p.WaitForExit(60000);
        }

        private void Fill()
		{
            int index = 0, i = 0;
            string NoTTS = "";
            foreach (Control tr in list2.Controls)
            {
                HtmlTableCell c = (HtmlTableCell)list2.FindControl("pk_" + index);
                CheckBox cb = (CheckBox)list2.FindControl("notts_" + index);

                if (c != null)
                {
                    if (cb.Checked)
                    {
                        Print(Convert.ToInt32(c.Attributes["title"]));
                        NoTTS += c.Attributes["title"] + ";";
                        Label pb = new Label();

                        if (i % 2 != 0)
                        {
                            pb.Text = "<div style='page-break-after:always'>&nbsp;</div>";
                            list.Controls.Add(pb);
                        }
                        else
                        {
                            pb.Text = "<div style='margin-top:15px;'>&nbsp;</div>";
                            list.Controls.Add(pb);
                        }

                        i++;
                    }
                }

                index++;
            }
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            string Project = project.SelectedValue;

            string file = Param.PathFilePDFFinanceAR + Cf.Tgl(Dari) + "&" + Cf.Tgl(Sampai) + "&" + Project + "_BKMBatch.pdf";
            bool exist = System.IO.File.Exists(file);
            if (exist)
            {
                System.IO.File.Delete(file);
            }
            ConvertPdf(NoTTS);
            Response.Redirect(Param.PathLinkFilePDFFinanceAR + Cf.Tgl(Dari) + "&" + Cf.Tgl(Sampai) + "&" + Project + "_BKMBatch.pdf");
        }

		private void Print(int NoTTS)
		{
			//increment
			Db.Execute("UPDATE MS_TTS SET PrintBKM = PrintBKM + 1 WHERE NoTTS = " + NoTTS);

			//Logfile
			DataTable rs = Db.Rs("SELECT "
				+ " CONVERT(varchar, TglTTS, 106) AS [Tanggal]"
				+ ",Tipe"
				+ ",Ref AS [Ref.]"
				+ ",Unit"
				+ ",Customer"
				+ ",CaraBayar AS [Cara Bayar]"
				+ ",Ket AS [Keterangan]"
				+ ",Total"
				+ ",NoBG AS [No. BG]"
				+ ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
				+ ",NoBKM AS [No. BKM]"
				+ ",CONVERT(varchar, TglBKM, 106) AS [Tanggal BKM]"
				+ " FROM MS_TTS WHERE NoTTS = " + NoTTS);

			Db.Execute("EXEC spLogTTS"
				+ " 'P-BKM'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Cf.LogCapture(rs) + "'"
				+ ",'" + NoTTS.ToString().PadLeft(7,'0') + "'"
				);

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TTS WHERE NoTTS = '" + NoTTS + "')");
            Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            //Template
            PrintBKMTemplate uc = (PrintBKMTemplate)Page.LoadControl("PrintBKMTemplate.ascx");
            uc.NoTTS = NoTTS.ToString();
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
}
}
