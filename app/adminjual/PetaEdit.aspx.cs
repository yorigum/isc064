
using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.IO;
using System.Web;

namespace ISC064.ADMINJUAL
{
	public partial class PetaEdit : Peta
	{

        protected string IDx { get { return Request.QueryString["id"]; } }
        protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            if(!Page.IsPostBack)
                Fill();
            if (Request.QueryString["done"]!=null)
			{
				feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
					+ "Proses Berhasil...";
			}
		}

		private void Fill()
		{

            var d = Db.Rs("Select * from ms_siteplan where id='" + IDx + "'");
            if (d.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");

            var r = d.Rows[0];
            nama.Text = r["Nama"].ToString();
            if (File.Exists( Server.MapPath(".").ToLower().Replace("adminjual", "")  + r["PathGambarDasar"].ToString()))
            {
                imgdasar.ImageUrl = Path.Combine(Request.PhysicalApplicationPath.ToLower(), r["PathGambarDasar"].ToString());// r["PathGambarDasar"].ToString();
            }
            //if (File.Exists(Server.MapPath(".").ToLower().Replace("adminjual", "") + r["PathGambarTransparent"].ToString()))
            //{
            //    imgtransparent.ImageUrl =r["PathGambarTransparent"].ToString();
            //}
            if ((bool)r["isParent"])
            {
                tr1.Visible = tr2.Visible = rpt.Visible = false;
                
            }
            string strSql = "SELECT DISTINCT"
                + " NoUnit"
                + ",Koordinat"
                + " FROM MS_UNIT"
                + " WHERE Peta = '" + r["Nama"] + "'"
                + " ORDER BY NoUnit";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Floor plan ini belum memiliki koordinat.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow tr = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Cut(rs.Rows[i]["Koordinat"], 100);
                tr.Cells.Add(c);

                Rpt.Border(tr);
                rpt.Rows.Add(tr);
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

        protected void save_Click(object sender, EventArgs e)
        {
            if (Valid)
            {
                Db.Execute("update ms_siteplan set Nama='" + nama.Text + "' where ID='" + IDx + "'");
                string NamaLama = Db.SingleString("Select Nama from ms_siteplan where id='"+ IDx +"'");
                string ProjectPeta = Db.SingleString("Select Project from ms_siteplan where Nama = '" + NamaLama + "'");
                string ProjectUnit = Db.SingleString("Select DISTINCT Project from ms_unit where Peta = '" + nama.Text + "'");

                if(ProjectPeta == ProjectUnit)
                {
                    Db.Execute("update ms_siteplan set Nama='" + nama.Text + "' where ID='" + IDx + "'");
                    Db.Execute("update ms_unit set Peta='" + nama.Text + "' where Peta='" + NamaLama + "'");
                }

                string NamaPeta = "PETA_" + IDx;

                string pdasar = Request.PhysicalApplicationPath.ToLower().Replace("admin", "marketing")
                    + "FP\\Base\\" + NamaPeta + ".jpg"; //file dasar
                string ptransparent = Request.PhysicalApplicationPath.ToLower().Replace("admin", "marketing")
                    + "FP\\Base\\" + NamaPeta + ".png"; //file dasar
                string pstatus = Request.PhysicalApplicationPath.ToLower().Replace("admin", "marketing")
                    + "FP\\" + NamaPeta + ".jpg"; //file status


                string urlpdasar = @"\marketingjual\FP\Base\" + NamaPeta + ".jpg"; //file dasar
                string urlptransparent = @"\marketingjual\FP\Base\" + NamaPeta + ".png"; //file dasar
                string urlpstatus = @"\marketingjual\FP\" + NamaPeta + ".jpg"; //file status

                if (file1.HasFile)
                {
                    if (File.Exists(pdasar))
                        File.Delete(pdasar);
                    if (File.Exists(pstatus))
                        File.Delete(pstatus);
                    Dfc.UploadFilePeta(".jpg", pdasar, file1);
                    Dfc.CopyFile(pdasar, pstatus);
                    Db.Execute("update ms_siteplan set PathGambarDasar='" + urlpdasar + "' where ID='" + IDx + "'");

                }
                //if (file2.HasFile)
                //{
                //    if (File.Exists(ptransparent))
                //        File.Delete(ptransparent);
                //    Dfc.UploadFile(".png", ptransparent, file2);
                //    Db.Execute("update ms_siteplan set PathGambarTransparent='" + urlptransparent + "' where ID='" + IDx + "'");
                //}

            
                Response.Redirect("PetaEdit.aspx?done=1&id=" + IDx);
            }
            else
            {
                feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                    + "Proses Gagal...";
            }
        }
        bool Valid
        {
            get
            {
                bool x = true;
                Cf.ClrError(nama);
                if (Cf.isEmpty(nama))
                {
                    x = false;
                    Cf.MarkError(nama);
                }
                if (file1.HasFile)
                {
                    if (file1.FileName.EndsWith(".jpg") || file1.FileName.EndsWith(".jpeg"))
                    {
                    }else
                    {
                        x = false;
                        file1c.Text = "file harus berextensi *.jpg";
                    }
                }
                //if (file2.HasFile)
                //{
                //    if (!file2.FileName.EndsWith(".png"))
                //    {
                //        x = false;
                //        file2c.Text = "file harus berextensi *.png";
                //    }
                //}
                if (!x) Js.Alert(this, "Data tidak valid", "");
                return x;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("PetaDel.aspx?id=" + IDx);
        }
    }
}
