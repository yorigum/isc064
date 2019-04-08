using System;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class DaftarCustomer : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["status"] == "a")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "i")
                    metode.SelectedIndex = 2;

                Act.ProjectList(project);

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

            if(Request.QueryString["project"] != "" && Request.QueryString["project"] != "SEMUA")
            {
                project.SelectedValue = Request.QueryString["project"];
            }

            string customsec = "";
            if (Act.SecLevel == "AG")
                customsec = " AND AgentInput = '" + Cf.Str(Act.UserID) + "'";

            string Cus = "'<a href=\"javascript:call(''' + CONVERT(VARCHAR(10), NoCustomer) + ''', ''' + Nama + ''')\"><b>"
                    + "' + Nama + '"
                    + " (' + CONVERT(VARCHAR(10), NoCustomer) + ')"
                    + "</b></a><br /><br />"
                    + "<div>Tipe : ' + TipeCs + '</div>"
                    + "<div>Nama Bisnis : ' + NamaBisnis + '</div>"
                    + "<div>Jenis Bisnis : ' + JenisBisnis + '</div>"
                    + "<div>Merek Bisnis : ' + MerekBisnis + '</div>"
                    + "<br /><br />"
                    + "<div>Telp : ' + NoTelp + '</div>"
                    + "<div>HP : ' + NoHP + '</div>"
                    + "<div>Kantor : ' + NoKantor + '</div>"
                    + "<div>Fax : ' + NoFax + '</div>"
                    + "<div>Email : ' + Email + '</div>'"
                    + " + "
                    + "CONVERT(VARCHAR(MAX), " // di convert supaya bisa di gabung
                    + "     CASE WHEN (UnitLama <> '' "
                    + "       OR TokoLama <> '' "
                    + "       OR ZoningLama <> '' "
                    + "       OR GedungLama <> '' "
                    + "       OR TeleponLama <> '' "
                    + "       OR AkteLama <> '' "
                    + "       OR LuasLama <> 0 "
                    + "       )"
                    + "         THEN "
                    + " '<div><b>Customer Lama</b></div>"
                    + " <div>Gedung : ' + GedungLama + '</div>"
                    + " <div>Unit : ' + UnitLama + ' ' + LuasLama + ' M2)</div>"
                    + " <div>Toko : ' + TokoLama + '</div>"
                    + " <div>Zoning : ' + ZoningLama + '</div>"
                    + " <div>Telepon : ' + TeleponLama + '</div>"
                    + " <div>Akte : ' + AkteLama + '</div>"
                    + " ' ELSE '' " // jika bukan cs lama, digabung dengan string kosong
                    + " END)"
                    + " AS Nama"
                    ;

            string Alamat = ",'Surat Menyurat :"
                    + "<div style=padding-left:20>"
                    + "' + Alamat1 + '"
                    + "<br /> ' + Alamat2 + '"
                    + "<br /> ' + Alamat3 + '"
                    + "</div>"

                    + "Kantor :"
                    + "<div style=padding-left:20>"
                    + "' + Kantor1 + '"
                    + "<br>' + Kantor2 + '"
                    + "<br>' + Kantor3 + '"
                    + "</div>"

                    + "KTP :"
                    + "<div style=padding-left:20>"
                    + "' + NoKTP + '"
                    + "<br> ' + KTP1 + '"
                    + "<br> ' + KTP2 + '"
                    + "<br> ' + KTP3 + '"
                    + "<br> ' + KTP4 + '"
                    + "</div>'"
                    + " AS Alamat"
                    ;

            string strSql = "SELECT "
                + Cus
                + Alamat
                + " FROM MS_CUSTOMER "
                + " WHERE Nama + NamaBisnis + JenisBisnis + MerekBisnis "
                + " + Alamat1 + Alamat2 + Alamat3 "
                + " + Kantor1 + Kantor2 + Kantor3 "
                + " + NoTelp + NoHP + NoKantor + NoFax + Email "
                + " + NoKTP + KTP1 + KTP2 + KTP3 + KTP4"
                + " + UnitLama +  TokoLama + AkteLama + TeleponLama"
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND Project = '" + project.SelectedValue + "'"
                + addq
                + customsec
                + " ORDER BY NoCustomer";

            DataTable rs = new DataTable();
            Db.Fill(rs, strSql);
            if (rs.Rows.Count > 0)
            {
                tb.DataSource = rs;
                tb.DataBind();
                info.InnerText = "";
            }
            else
            {
                tb.Controls.Clear();
                info.InnerText = "Tidak ada data yang ditampilkan";
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

        protected void tb_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}
