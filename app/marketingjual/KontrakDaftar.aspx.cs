using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakDaftar : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                hasil.Visible = false;
                FillTerbaru();
            }

			Js.Focus(this, unit);
			//Js.ConfirmKeyword(this,unit);
		}

        private void FillTerbaru()
        {
            string strSql = "SELECT TOP 10 NoKontrak, NoUnit "
                + " FROM MS_KONTRAK "
                + " WHERE Project = '" + project.SelectedValue + "'"
                + " ORDER BY TglInput DESC, NoKontrak DESC";

			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["NoKontrak"].ToString();
				string t = v + " ("+rs.Rows[i]["NoUnit"] +")";

				baru.Items.Add(new ListItem(t,v));
			}

			if(rs.Rows.Count!=0)
			{
				baru.SelectedIndex = 0;
				baru.Attributes["ondblclick"] = "location.href="
					+ "'KontrakDaftar3.aspx?NoKontrak='+this.options[this.selectedIndex].value";
			}
		}

		protected void display_Click(object sender, System.EventArgs e)
		{
            Cf.SetGrid(tb);
			Fill();
			hasil.Visible = true;
		}

        private void Fill()
        {
            string next = "'<a href=KontrakDaftar2.aspx?NoStock=''' + CONVERT(varchar(10),MS_RESERVASI.NoStock) +'''> Next...</a>'";
            string nav = ",'<a href=\"javascript:popUnit('''+ CONVERT(varchar(10),MS_RESERVASI.NoStock) +''')\">' + MS_RESERVASI.NoStock + '</a>'";

            string strSql = "SELECT"
                + next
                + " AS Next"
                + nav
                + " AS No"
                + ",MS_RESERVASI.NoUnit + '<br /><i>' + MS_UNIT.Jenis + '</i>' AS Unit"
                + ",CASE WHEN MS_RESERVASI.Status = 'E' THEN 'Expire' ELSE CONVERT(VARCHAR,TglExpire,113) END AS Exp"
                + ",CONVERT(VARCHAR,MS_RESERVASI.Tgl,106) AS Tgl "
                + ",MS_CUSTOMER.Nama AS Customer "
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Sales"
                + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
                + " INNER JOIN MS_UNIT ON MS_RESERVASI.NoStock = MS_UNIT.NoStock"
                + " WHERE NoUrut = 1"
                + " AND MS_RESERVASI.NoUnit LIKE '%" + Cf.Str(unit.Text) + "%'"
                + " AND MS_UNIT.Project = '" + project.SelectedValue + "'"
                + " ORDER BY NoReservasi";

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
