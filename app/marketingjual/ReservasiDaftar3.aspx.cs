using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class ReservasiDaftar3 : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoReservasi");

			if(!Page.IsPostBack)
				FillHeader();
		}
        private void FillHeader()
        {
            string nounit = Db.SingleString("SELECT NoUnit FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "'");
            string project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoUnit = '" + nounit + "'");
            string NoReservasi2 = Db.SingleString("SELECT ISNULL(NoReservasi2, '') FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "'");

            //print ttr -- req-panahome
            if (Request.QueryString["NoTTR"] != null)
            {
                attr.HRef = "javascript:openPopUp('PrintTTR.aspx?NoTTR=" + Cf.Pk(Request.QueryString["NoTTR"]) + "&project=" + project + "', '920', '650')";
                attr.Visible = false;
            }
            else
            {
                attr.Visible = false;
            }

            //print tts
            if (Request.QueryString["NoTTS"] != null)
                atts.HRef = "javascript:openPopUp('PrintTTS.aspx?NoTTS=" + Cf.Pk(Request.QueryString["NoTTS"]) + "&project=" + project + "', '920', '650')";
            else
                atts.Visible = false;

            boform.HRef = "javascript:openPopUp('PrintBForm.aspx?NoReservasi=" + Cf.Pk(Request.QueryString["NoReservasi"]) + "&project=" + project + "', '920', '650')";
			no.Text = "<a href=\"javascript:popEditReservasi('"+NoReservasi+"')\">"
				+ NoReservasi2 + "</a>";
			
			DataTable rs = Db.Rs("SELECT "
				+ " MS_CUSTOMER.Nama AS Cs"
				+ ",MS_CUSTOMER.NoCustomer"
				+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
				+ ",MS_RESERVASI.NoUnit"
				+ ",MS_RESERVASI.NoStock"
				+ ",MS_RESERVASI.NoUrut"
				+ " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
				+ " WHERE MS_RESERVASI.NoReservasi = " + NoReservasi
				);
			
			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				aKontrak.HRef = "KontrakDaftar2.aspx?NoStock="+rs.Rows[0]["NoStock"];

				unit.Text = "<a href=\"javascript:popUnit('" + rs.Rows[0]["NoStock"] +"')\">"
					+ rs.Rows[0]["NoUnit"] + "</a>";
				customer.Text = "<a href=\"javascript:popEditCustomer('" + rs.Rows[0]["NoCustomer"] +"')\">"
					+ rs.Rows[0]["Cs"] + "</a>";
				agent.Text = rs.Rows[0]["Ag"].ToString();

				int totalwl = Db.SingleInteger(
					"SELECT COUNT(*) FROM MS_RESERVASI WHERE NoStock = '"+rs.Rows[0]["NoStock"]+"'");
				nourut.Text = rs.Rows[0]["NoUrut"] + "/" + totalwl;
			}
		}

		private string NoReservasi
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoReservasi"]);
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
