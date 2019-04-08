using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	public partial class PrintPJTBatch : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
                Fill();
			}
		}

		protected void print_Click(object sender, System.EventArgs e)
		{
				Js.AutoPrint(this);
				Fill();
		}

		private void Fill()
		{
            string dari = Request.QueryString["Dari"];
            string sampai = Request.QueryString["Sampai"];
            string project = Request.QueryString["project"];

            DateTime Dari = Convert.ToDateTime(dari);
            DateTime Sampai = Convert.ToDateTime(sampai);
            if (Dari>Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = x;
			}

            string Project = "";
            if (project != "")
            {                
                Project = " AND b.Project IN ('" + project.Replace(",","','") + "')";
            }

            DataTable rs = Db.Rs("SELECT "
				+ " NoPJT"
                + " FROM MS_PJT a JOIN ISC064_MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                + " WHERE 1=1"
				+ " AND CONVERT(varchar,TglPJT,112) >= '" + Cf.Tgl112(Dari) + "'"
				+ " AND CONVERT(varchar,TglPJT,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + Project
				+ " ORDER BY Unit");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

                Print(rs.Rows[i]["NoPJT"].ToString(), project);

				if(i!=rs.Rows.Count-1)
				{
					Label pb = new Label();
					pb.Text = "<div style='page-break-before:after'>&nbsp;</div>";
					list.Controls.Add(pb);
				}
			}
		}

		private void Print(string NoPJT,string project)
		{
			//increment
			Db.Execute("UPDATE MS_PJT SET PrintPJT = PrintPJT + 1 "
				+ " WHERE NoPJT = '" + NoPJT + "'");

			//Logfile
			DataTable rs = Db.Rs("SELECT "
				+ " CONVERT(varchar, TglPJT, 106) AS [Tanggal]"
				+ ",Tipe"
				+ ",Ref AS [Ref]"
				+ ",Unit"
				+ ",Customer"
				+ ",Total"
				+ " FROM MS_PJT WHERE NoPJT = '" + NoPJT + "'");

			Db.Execute("EXEC spLogPJT"
				+ " 'P-PJT'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Cf.LogCapture(rs) + "'"
				+ ",'" + NoPJT.ToString() + "'"
				);

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_PJT_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_PJT WHERE NoPJT = '" + NoPJT + "') ");
            Db.Execute("UPDATE MS_PJT_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            //Template
            PrintPJTTemplate uc = (PrintPJTTemplate) Page.LoadControl("PrintPJTTemplate.ascx"); 
			uc.NoPJT = NoPJT.ToString();
            uc.Project = project.ToString();
            list.Controls.Add(uc);
		}
	}
}
