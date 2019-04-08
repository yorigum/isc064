using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
	public partial class AccDel : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
                string[] x = Cf.SplitByString(Acc, ";");
                DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC"
                    + " WHERE Acc = '" + x[0] + "' AND SubID='"+ x[1] +"'"
                    );
                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else if (!Act.AksesProject(rs.Rows[0]["Project"].ToString()))
                    Response.Redirect("/CustomError/SecLevel.html");

                Js.Focus(this, ket);
				Js.Confirm(this,
					"Apakah anda ingin menghapus account : "+Acc+" ?\\n"
					+ "Perhatian bahwa data akan dihapus secara PERMANEN."
					);
			}
		}

		protected void delbtn_Click(object sender, System.EventArgs e)
		{
            string[] x = Cf.SplitByString(Acc, ";");
			DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                + " WHERE Acc ='" + x[0] + "' AND SubID='" + x[1] + "'"
                );

            if (rs.Rows.Count != 0)
            {
                //Response.Redirect("/CustomError/Deleted.html");
                frm.Visible = false;
                nodel.Visible = true;
            }
            //else if (!Act.AksesProject(rs.Rows[0]["Project"].ToString()))
            //    Response.Redirect("/CustomError/SecLevel.html");
            else
            {
                string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                    + "<br><br>***Data Sebelum Delete :<br>"
                    + Cf.LogCapture(rs);

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spAccDel '" + x[0] + "', '" + x[1] + "'");

                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE Acc = '" + x[0] + "' AND SubID='" + x[1] + "'");

                if (c == 0)
                {
                    //Log
                    Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogAcc "
                        + " 'DELETE'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + Acc + "'"
                        );

                    Js.Close(this);
                }
                else
                {
                    //Tidak bisa dihapus
                    frm.Visible = false;
                    nodel.Visible = true;
                }
            }
		}

		private string Acc
		{
			get
			{
				return Cf.Pk(Request.QueryString["Acc"]);
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
