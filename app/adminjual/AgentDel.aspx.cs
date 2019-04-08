using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class AgentDel : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoAgent");

            int countKontrak = Db.SingleInteger("select count(*) from ms_kontrak where NoAgent = '" + NoAgent + "'");
            if (countKontrak != 0)
            {
                delbtn.Enabled = false;
                nodel.Visible = true;
            }

            if (!Page.IsPostBack)
			{
				Js.Focus(this, ket);
				Js.Confirm(this,
					"Apakah anda ingin menghapus sales : "+NoAgent+" ?\\n"
					+ "Perhatian bahwa data akan dihapus secara PERMANEN."
					);
			}
		}

		protected void delbtn_Click(object sender, System.EventArgs e)
		{
			DataTable rs = Db.Rs(
				"SELECT * FROM MS_AGENT WHERE NoAgent = " + NoAgent);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
					+ "<br><br>***Data Sebelum Delete :<br>"
					+ Cf.LogCapture(rs);

				Db.Execute("EXEC spAgentDel " + NoAgent);

				int c = Db.SingleInteger(
					"SELECT COUNT(*) FROM MS_AGENT WHERE NoAgent = " + NoAgent);

				if(c==0)
				{
					//Log
					Db.Execute("EXEC spLogAgent "
						+ " 'DELETE'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + Ket + "'"
						+ ",'" + NoAgent.PadLeft(5,'0') + "'"
						);

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_AGENT_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_AGENT WHERE NoAgent = '" + NoAgent + "'");
                    Db.Execute("UPDATE MS_AGENT_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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

		private string NoAgent
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoAgent"]);
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
