using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class ReminderUnallocated : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            ok.HRef = "Reminder.aspx?project=" + Project;
            string[] x = Sc.MktCatalog();
            for (int i = 0; i <= x.GetUpperBound(0); i++)
            {
                string[] xdetil = x[i].Split(';');

                if (xdetil[1] != "TENANT")
                    Fill(xdetil[1]);
            }
        }

        private void Fill(string Tipe)
        {
            string Tb = Sc.MktTb(Tipe);
            string nav = "'<a href=CustomerLunas.aspx?Tipe=" + Tipe + "&Ref='''+MS_KONTRAK.NoKontrak+''' style=font-size:14px>' + MS_KONTRAK.NoKontrak + '</a>' AS Kontrak";
            string strSql = "SELECT "
                + nav
                + ",'"+Tipe+"' AS Tipe"                
                + ",NoUnit AS Unit"
                + ",Nama AS Cs"
                + ",NilaiPelunasan AS Nilai"
                + ",MS_PELUNASAN.CaraBayar +', '+ CONVERT(VARCHAR,TglPelunasan,106) + ' ' + MS_PELUNASAN.Ket AS Keterangan"                                
                + " FROM " + Tb + "..MS_KONTRAK AS MS_KONTRAK INNER JOIN " + Tb + "..MS_CUSTOMER AS MS_CUSTOMER"
                + "		ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN " + Tb + "..MS_PELUNASAN AS MS_PELUNASAN"
                + "		ON MS_KONTRAK.NoKontrak = MS_PELUNASAN.NoKontrak"
                + " WHERE 1=1"
                + " AND MS_PELUNASAN.NoTagihan = 0"
                + " ORDER BY MS_KONTRAK.NoKontrak";

            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
            if (tb.PageCount == 0) kosong.InnerText = "Reminder untuk topik diatas masih kosong.";
        }
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
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
