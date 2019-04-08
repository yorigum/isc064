using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class DaftarCustomer2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            //Js.ConfirmKeyword(this, keyword);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["status"] == "a")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "i")
                    metode.SelectedIndex = 2;

               

                if (Request.QueryString["project"] == "" || Request.QueryString["project"] == "SEMUA")
                {
                    Act.ProjectList(project);
                }
                else
                {
                    project.Items.Clear();
                    string v = Request.QueryString["project"].ToString();
                    string nproject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project='" + v + "'");
                    //Response.Write("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project='" + v + "'");
                    string t = v + " - " + nproject;
                    project.Items.Add(new ListItem(t, v));
                }
                if (metode.SelectedIndex != 0) metode.Enabled = false;
            }
        }

        protected void search_Click(object sender, System.EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
        }

        private void Fill()
        {
            string addq = "";
            if (metode.SelectedIndex == 1)
                addq = " AND Status = 'A'";
            else if (metode.SelectedIndex == 2)
                addq = " AND Status = 'I'";

            string customsec = "";
            if (Act.SecLevel == "AG")
                customsec = " AND AgentInput = '" + Cf.Str(Act.UserID) + "'";

            string nav = 
                "'<a href=\"javascript:call("
                    + "''' + CONVERT(varchar(50),NoCustomer) + '''"
                    + ",''' + Nama + '''"
                    + ",''' + NoHP + '''"
                    + ",''' + NoTelp + '''"
                    + ",''' + Email + '''"
                    + ",''' + CASE WHEN TglLahir <> NULL THEN 'CONVERT(VARCHAR,TglLahir,112)' ELSE '' END + '''"
                    + ",''' + NoKTP + '''"
                    + ",''' + NPWP + '''"
                    + ",''' + KTP1 + '''"
                    + ",''' + KTP2 + '''"
                    + ",''' + KTP3 + '''"
                    + ",''' + KTP4 + '''"
                    + ",''' + KTP5 + '''"
                    + ",''' + CASE WHEN KTP1 = Alamat1 THEN '1' WHEN KTP2 = Alamat2 THEN '1' ELSE '0' END + '''"
                    + ",''' + Alamat1 + '''"
                    + ",''' + Alamat2 + '''"
                    + ",''' + Alamat3 + '''"
                    + ",''' + Alamat4 + '''"
                    + ",''' + Alamat5 + '''"
                    + ",''' + RekBank + '''"
                    + ",''' + RekCabang + '''"
                    + ",''' + RekNo + '''"
                    + ",''' + RekNama + ''')\"><b>'"                    
                    + " + Nama + '(' + FORMAT(NoCustomer,'0000#') + ')</b></a><br>"                    
                    + "<div>Tipe :' + TipeCs + '</div>"
                    + "<div>NPWP :' + NPWP + '</div>"
                    + "<div>Telp :' + NoTelp + '</div>"
                    + "<div>HP :' + NoHP + '</div>"
                    + "<div>Email :' + NoHP + '</div>"
                    + "<div>Bank Refund :' + RekBank + '&nbsp;' + RekCabang + '</div>'";

            

            string nav2 = ",'KTP :"
                    + "<div style=padding-left:20>"
                    + "<br>' + NoKTP +"
                    + "'<br>' + KTP1 +"
                    + "'<br>' + KTP2 +"
                    + "'<br>' + KTP3 +"
                    + "'<br>' + KTP4 + '</div>'"
                    + "+ CASE WHEN KTP1 = Alamat1 THEN 'Koresponden :<div style=padding-left:20>SAMA DENGAN KTP</div>'  WHEN KTP2 = Alamat2 THEN 'Koresponden :<div style=padding-left:20>SAMA DENGAN KTP</div>' ELSE 'Koresponden :<div style=padding-left:20>' + ''+Alamat1+'' + '<br>' + ''+Alamat2+'' + '<br>' + ''+Alamat3+'' + '<br>' + ''+Alamat4+'' + '</div>' END "
                    ;

            string strSql = "SELECT "
                + nav
                + " AS Nama"
                + nav2
                + "  AS Alamat"
                + " FROM MS_CUSTOMER "
                + " WHERE Nama + NamaBisnis + JenisBisnis + MerekBisnis "
                + " + Alamat1 + Alamat2 + Alamat3 "
                + " + Kantor1 + Kantor2 + Kantor3 "
                + " + NoTelp + NoHP + NoKantor + NoFax + Email + NPWP"
                + " + NoKTP + KTP1 + KTP2 + KTP3 + KTP4"
                + " + UnitLama +  TokoLama + AkteLama + TeleponLama"
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                 + " AND Project = '" + project.SelectedValue + "'"
                + addq
                + customsec
                + " ORDER BY Nama, NoCustomer";
            //Response.Write(strSql);
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
