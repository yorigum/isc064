using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	public partial class ReminderKurang : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            Cf.SetGrid(tb);
			string[] x = Sc.MktCatalog();
			for(int i=0;i<=x.GetUpperBound(0);i++)
			{
				string[] xdetil = x[i].Split(';');
				
				if(xdetil[1]!="TENANT")
					Fill(xdetil[1]);
			}
            ok.HRef = "Reminder.aspx?project=" + Project;
        }

        private void Fill(string Tipe)
        {
            string Tb = Sc.MktTb(Tipe);

            string strSql = "SELECT "
                + "'"+Tipe+"' + '<br>' + MS_KONTRAK.NoKontrak AS Tipe"
                + ",NoUnit + '<br>' + Nama AS Unit"                
                + ",NamaTagihan + '<br>' + CONVERT(VARCHAR,TglJT,106) AS JT"                
                + ",FORMAT(NilaiTagihan,'#,###') AS Tagihan"                
                + ",FORMAT((SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoKontrak = MS_TAGIHAN.NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut),'#,###') AS Pelunasan"
                + ",FORMAT((NilaiTagihan-(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoKontrak = MS_TAGIHAN.NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut)),'#,###') AS Sisa"
                + " FROM " + Tb + "..MS_KONTRAK AS MS_KONTRAK INNER JOIN " + Tb + "..MS_CUSTOMER AS MS_CUSTOMER"
                + "		ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN " + Tb + "..MS_TAGIHAN AS MS_TAGIHAN"
                + "		ON MS_KONTRAK.NoKontrak = MS_TAGIHAN.NoKontrak"
                + " WHERE 1=1"
                + " AND MS_KONTRAK.Project = '" + Project + "'"
                + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN"
                + " WHERE NoKontrak = MS_TAGIHAN.NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut) > 0)"
                + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN"
                + " WHERE NoKontrak = MS_TAGIHAN.NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut) != NilaiTagihan)"
                + " ORDER BY TglJT";

            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
            if (tb.PageCount == 0) kosong.InnerText = "Reminder untuk topik diatas masih kosong.";
		}

        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["Project"]);
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

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            string[] x = Sc.MktCatalog();
            for (int i = 0; i <= x.GetUpperBound(0); i++)
            {
                string[] xdetil = x[i].Split(';');

                if (xdetil[1] != "TENANT")
                    Fill(xdetil[1]);
            }
        }
    }
}
