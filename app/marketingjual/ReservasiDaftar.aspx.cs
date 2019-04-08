using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class ReservasiDaftar : System.Web.UI.Page
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

				if(Request.QueryString["NoUnit"]!=null)
				{
					//dari floor plan
					unit.Text = Request.QueryString["NoUnit"];
					Fill();
					hasil.Visible = true;
				}
			}
			
			Js.Focus(this, unit);
			//Js.ConfirmKeyword(this, unit);
		}

        private void FillTerbaru()
        {
            string strSql = "SELECT TOP 10 A.NoReservasi, A.NoUnit"
                + " FROM MS_RESERVASI A"
                + " JOIN MS_UNIT B ON A.NoStock = B.NoStock"
                + " WHERE B.Project = '" + project.SelectedValue + "'"
                + " ORDER BY NoReservasi DESC"
                ;

			DataTable rs = Db.Rs(strSql);

			for(int i=0;i<rs.Rows.Count;i++)
			{
				string a = rs.Rows[i]["NoReservasi"].ToString().PadLeft(5,'0')
					+ " (" + rs.Rows[i]["NoUnit"] + ")";
				string b = rs.Rows[i]["NoReservasi"].ToString();

				baru.Items.Add(new ListItem(a, b));
			}

			if(rs.Rows.Count!=0)
			{
				baru.SelectedIndex = 0;
				baru.Attributes["ondblclick"] = "location.href="
					+ "'ReservasiDaftar3.aspx?NoReservasi='+this.options[this.selectedIndex].value";
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
            string next = "'<a href=ReservasiDaftar2.aspx?NoStock=''' + CONVERT(varchar(10),NoStock) +'''> Next...</a>'";
            string nav = ",'<a href=\"javascript:popUnit('''+ CONVERT(varchar(10),NoStock) +''')\">' + NoStock + '</a>'";

            string strSql = "SELECT"
                + next
                + "AS Next"
                + nav
                + "AS No"
                + ",NoUnit AS Unit"
                + ",Luas AS Luas"
                + ",FORMAT(LuasNett, '#,###') AS LuasNett"
                + ",FORMAT(LuasSG, '#,###') AS LuasSG"
                + ",FORMAT(PriceList, '#,###') AS PL"
                + ",Jenis AS Jenis"
                + " FROM MS_UNIT "
                + " WHERE Status = 'A'"
                + " AND FlagSPL = 1" //price list sudah di-set
                + " AND NoUnit LIKE '%" + Cf.Str(unit.Text) + "%'"
                + " AND Project = '" + project.SelectedValue + "'"
                + " ORDER BY NoUnit, NoStock";

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
