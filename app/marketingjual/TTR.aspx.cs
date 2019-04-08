using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class TTR : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            Cf.SetGrid(tb);
			if(!Page.IsPostBack)
			{
				init();
			}

			Js.Focus(this,search);
		}

		private void init()
		{
			dari.Text = Cf.Day(DateTime.Today);
			sampai.Text = Cf.Day(DateTime.Today);

			DataTable rs;

			rs = Db.Rs("SELECT DISTINCT UserID FROM MS_TTR ORDER BY UserID");
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["UserID"].ToString();
				user.Items.Add(new ListItem(v,v));
			}
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(dari))
			{
				daric.Text = "Tanggal";
				if(s=="") s = dari.ID;
				x = false;
			}
			else
				daric.Text = "";

			if(!Cf.isTgl(sampai))
			{
				sampaic.Text = "Tanggal";
				if(s=="") s = sampai.ID;
				x = false;
			}
			else
				sampaic.Text = "";

			if(!x)
				RegisterStartupScript("err"
					,"<script language='javascript'>document.getElementById('"+s+"').select()</script>");
			
			return x;
		}

		protected void display_Click(object sender, System.EventArgs e)
		{
            if (valid())
            {
                Cf.SetGrid(tb);
                Fill();
            }				
		}

		private void Fill()
		{
			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			if(Dari > Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = x;
			}

            string UserID = "";
            if (user.SelectedIndex != 0)
                UserID = " AND MS_TTR.UserID = '" + user.SelectedValue + "'";

            string CaraBayar = "";
            if (carabayar.SelectedIndex != 0)
                CaraBayar = " AND MS_TTR.CaraBayar = '" + carabayar.SelectedValue + "'";

            string Status = "";
            if (statusB.Checked) Status = " AND MS_TTR.Status = 'BARU'";
            if (statusP.Checked) Status = " AND MS_TTR.Status = 'POST'";
            if (statusV.Checked) Status = " AND MS_TTR.Status = 'VOID'";

            string nav = "'<a href=\"javascript:call('''+ CONVERT(varchar(50),MS_TTR.NoTTR) +''')\">' + MS_TTR.NoTTR + '</a><br><i>' +  MS_TTR.Status + '</i>' ";

            string strSql = "SELECT"
                + nav
                + " AS TTR"
                + ",CONVERT(VARCHAR,MS_TTR.TglTTR,106) + '<br>' + "
                + "(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = MS_TTR.UserID) AS Tgl"
                + ",'No. ' + MS_TTR.NoReservasi + '<br>' + MS_TTR.Unit + '<br>' + MS_TTR.Customer AS Customer"
                + ",MS_TTR.Ket AS Keterangan"
                + ",CASE MS_TTR.CaraBayar"
                + "		WHEN 'TN' THEN 'TUNAI'"
                + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                + "		WHEN 'BG' THEN 'CEK GIRO'"
                + " END AS CaraBayar"
                + ",FORMAT(MS_TTR.Total,'#,###') AS Total"
                + " FROM MS_TTR INNER JOIN MS_UNIT ON MS_TTR.Unit = MS_UNIT.NoUnit"
                + " WHERE 1=1 "
                + " AND MS_UNIT.Project IN (" + Act.ProjectListSql + ")"
                + " AND CONVERT(VARCHAR, TglTTR, 112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(VARCHAR, TglTTR, 112) <= '" + Cf.Tgl112(Sampai) + "'"
                + UserID
                + CaraBayar
                + Status
                + " ORDER BY NoTTR";

            DataTable rs = Db.Rs(strSql);            

            tb.DataSource = rs;
            tb.DataBind();
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

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}
