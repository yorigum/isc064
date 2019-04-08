using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class PetaUploadKoord : System.Web.UI.Page
	{
        int Berhasil = 0;
        int Gagal = 0;
        string BarisGagal = "";
        protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

            if (!Page.IsPostBack) { 
                Act.ProjectList(project);
            }

            Bind();
            Js.Confirm(this,"Lanjutkan proses upload koordinat unit properti?");
			feed.Text = "";
		}

		private void Bind()
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

            var d = Db.Rs("Select * from ms_siteplan WHERE isParent=0");
            foreach (DataRow r in d.Rows)
            {
                x.Append(r["Nama"]);
                x.Append("/");
            }

            TableCell c;
            c = rule.Rows[2].Cells[4]; c.Text = x.ToString();
        }

		protected void upload_Click(object sender, System.EventArgs e)
		{
			if(!file.PostedFile.FileName.EndsWith(".xls"))
			{
				Js.Alert(
					this
					, "Proses Upload Gagal.\\n"
					+ "File yang boleh di-upload adalah file dengan extension .xls saja."
					, ""
					);
			}
			else
			{
				string path = Request.PhysicalApplicationPath
					+ "Template\\Koordinat_" + Session.SessionID + ".xls";
				
				Dfc.UploadFile(".xls",path,file);

				Cek(path);
				
				//Hapus file sementara tersebut dari hard-disk server
				Dfc.DeleteFile(path);
				
			}
		}

		private void Cek(string path)
		{
			string strSql = "SELECT * FROM [Koordinat$]";
			DataTable rs = new DataTable();

			try
			{
				rs = Db.xls(strSql,path);
			}
			catch {}

			if(Rpt.ValidateXls(rs, rule, gagal))
				Save(path);
		}

		private void Save(string path)
		{
			System.Text.StringBuilder log = new System.Text.StringBuilder();
			int total = 0;

			string strSql = "SELECT * FROM [Koordinat$]";
			DataTable rs = Db.xls(strSql,path);

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				if(Save(rs,i))
				{
					total++;
					log.Append(
						Cf.Str(rs.Rows[i][0])
						+ " ; " + Cf.Str(rs.Rows[i][1])
						+ " ; " + Cf.Str(rs.Rows[i][2])
						+ "<br>");
				}
			}

			string Ket = "***UPLOAD KOORDINAT" + "<br><br>" + log.ToString();
			Db.Execute("EXEC spLogUnit"
				+ " 'FP'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Ket + "'"
				+ ",''"
				);

            //feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
            //	+ "Upload Berhasil  : " + total + " baris data";
            if (Berhasil == 0)
            {
                feed2.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Gagal, No.Unit Bukan milik Peta  : " + Gagal + " Baris Data, " + BarisGagal;
            }
            else
            {
                feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Berhasil  : " + Berhasil + " Baris Data";
                //feed2.Text = "<img src='/Media/db.gif' align=absmiddle> "
                //+ "Upload Gagal, No.Unit Bukan milik Peta   : " + Gagal + " Baris Data, " + BarisGagal;
            }
        }

		private bool Save(DataTable rs, int i)
		{
			string Unit = Cf.Str(rs.Rows[i][0]);
			if(Db.SingleInteger("SELECT COUNT(*) FROM MS_UNIT WHERE NoUnit = '"+ Unit + "' AND Project ='" +project.SelectedValue+ "'") != 0)
            {
				string Peta = Cf.Str(rs.Rows[i][1]);
				string Koordinat = Cf.Str(rs.Rows[i][2]).Replace("'","");

                string ProjectUnit = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoUnit = '" + Unit + "'");
                string ProjectPeta = Db.SingleString("SELECT Project FROM MS_SITEPLAN WHERE Nama = '" + Peta + "'");

                if (Db.SingleInteger("SELECT COUNT (*) FROM MS_SITEPLAN WHERE isParent=0 AND Nama = '" + Peta + "' AND Project = '" + project.SelectedValue + "'") != 0)
                    if (ProjectUnit == ProjectPeta)
                    {
                        //Jika data NA maka diisi dengan string kosong saja di dalam database
                        Db.Execute("EXEC spUnitEditKoordinat "
                    + " '" + Unit + "'"
                    + ",'" + Peta + "'"
                    + ",'" + Koordinat + "'"
                    );

                        Berhasil++;
                    }
                    else
                    {
                        //return false;
                        Gagal++;
                        BarisGagal += "Baris Ke-" + (i + 1).ToString() + ", ";
                    }
            }
				return true;
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
