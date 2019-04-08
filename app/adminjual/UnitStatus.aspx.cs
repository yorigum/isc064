using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.AspNet.SignalR;

namespace ISC064.ADMINJUAL
{
	public partial class UnitStatus : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Fill();
			}

			Js.Confirm(this,"Lanjutkan proses edit status unit?");
		}

        private void Fill()
        {
            string strSql = "SELECT * FROM MS_UNIT WHERE NoStock = '" + NoStock + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                if ((string)rs.Rows[0]["Status"] == "A")
                    statusA.Checked = true;
                else if ((string)rs.Rows[0]["Status"] == "B")
                    statusB.Checked = true;
                else
                    statusC.Checked = true;

				int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE Status = 'A' AND NoStock = '"+NoStock+"'");
                if (c != 0) { 
					statusA.Enabled = false; //unit sudah terjual, tidak bisa edit status
                                             //statusC.Enabled = false;
                save.Enabled = false;
                }
                else
                {
                    save.Enabled = true;
                    statusB.Enabled = false;
                }
                //save.Visible = false;
			}
		}

        protected void save_Click(object sender, System.EventArgs e)
        {
            string statusLama = Db.SingleString("SELECT Status FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            string statusBaru = "";
            if (statusA.Checked) statusBaru = "A";
            if (statusB.Checked) statusBaru = "B";
            if (statusC.Checked) statusBaru = "H";

			if(statusBaru!=statusLama)
			{
				Db.Execute("EXEC spUnitEditStatus "
					+ " '" + NoStock + "'"
					+ ",'" + statusBaru + "'"
					);

				string Ket = "<br>***EDIT STATUS : " + statusLama + " --> " + statusBaru;

				Db.Execute("EXEC spLogUnit "
					+ " 'STATUS'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Ket + "'"
					+ ",'" + NoStock + "'"
					);

				//redraw floor plan
				string Peta = Db.SingleString("SELECT Peta FROM MS_UNIT WHERE NoStock='"+NoStock+"'");
				Func.GenerateFP(Peta);

				ClientScript.RegisterStartupScript(GetType(), "closerfr"
					,"<script type='text/javascript'>"
					+ " window.opener.location.href='UnitEdit.aspx?done=1&NoStock="+NoStock+"';"
					+ " window.close();"
					+ "</script>"
					);
			}
			else
				Js.Close(this);
		}

		private string NoStock
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoStock"]);
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
