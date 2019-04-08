using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace ISC064.NUP
{
    public partial class DaftarCustomer3 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Js.ConfirmKeyword(this, keyword);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["status"] == "a")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "i")
                    metode.SelectedIndex = 2;

                if (metode.SelectedIndex != 0) metode.Enabled = false;
            }
        }

        protected void search_Click(object sender, System.EventArgs e)
        {
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

            string strSql = "SELECT * "
                + " FROM MS_CUSTOMER "
                + " WHERE Nama + NamaBisnis + JenisBisnis + MerekBisnis "
                + " + Alamat1 + Alamat2 + Alamat3 "
                + " + Kantor1 + Kantor2 + Kantor3 "
                + " + NoTelp + NoHP + NoKantor + NoFax + Email + NPWP"
                + " + NoKTP + KTP1 + KTP2 + KTP3 + KTP4"
                + " + UnitLama +  TokoLama + AkteLama + TeleponLama"
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + addq
                + customsec
                + " ORDER BY Nama, NoCustomer";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ditemukan data customer dengan keyword diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                string lama = "";
                if (rs.Rows[i]["UnitLama"].ToString() != ""
                    || rs.Rows[i]["TokoLama"].ToString() != ""
                    || rs.Rows[i]["ZoningLama"].ToString() != ""
                    || rs.Rows[i]["GedungLama"].ToString() != ""
                    || rs.Rows[i]["TeleponLama"].ToString() != ""
                    || rs.Rows[i]["AkteLama"].ToString() != ""
                    || (decimal)rs.Rows[i]["LuasLama"] != 0)
                {
                    lama = "<tr><td colspan=3><br><b>Customer Lama</b></td></tr>"
                        + "<tr><td>Gedung</td><td>:</td><td>" + rs.Rows[i]["GedungLama"] + "</td></tr>"
                        + "<tr><td>Unit</td><td>:</td><td>" + rs.Rows[i]["UnitLama"]
                        + " (" + Cf.Num(rs.Rows[i]["LuasLama"]) + " M2)</td></tr>"
                        + "<tr><td>Toko</td><td>:</td><td>" + rs.Rows[i]["TokoLama"] + "</td></tr>"
                        + "<tr><td>Zoning</td><td>:</td><td>" + rs.Rows[i]["ZoningLama"] + "</td></tr>"
                        + "<tr><td>Telepon</td><td>:</td><td>" + rs.Rows[i]["TeleponLama"] + "</td></tr>"
                        + "<tr><td>Akte</td><td>:</td><td>" + rs.Rows[i]["AkteLama"] + "</td></tr>"
                        ;
                }
                string sifatvalue = "Koresponden :"
                    + "<div style=padding-left:20>"
                    + rs.Rows[i]["AlamatKorespon1"]
                    + "<br/>" + rs.Rows[i]["AlamatKorespon2"]
                    + "<br>" + rs.Rows[i]["AlamatKorespon3"]
                    + "<br/>" + rs.Rows[i]["AlamatKorespon4"]
                    + "</div>"
                    ;

                int sifat = 0;
                if (rs.Rows[i]["KTP1"].ToString() == rs.Rows[i]["AlamatKorespon1"].ToString() || rs.Rows[i]["KTP2"].ToString() == rs.Rows[i]["AlamatKorespon2"].ToString())
                {
                    sifat = 1;
                    sifatvalue = "<br/> Koresponden :"
                    + "<div style=padding-left:20>"
                    + "SAMA DENGAN KTP"
                    + "</div>";
                }

                c = new TableCell();
                c.Text = "<a href=\"javascript:call('"
                    + rs.Rows[i]["NoCustomer"] + "','"
                    + rs.Rows[i]["Nama"] + "','"
                    + rs.Rows[i]["NoHP"] + "','"
                    + rs.Rows[i]["NoTelp"] + "','"
                    + rs.Rows[i]["Email"] + "','"
                    + Cf.Day(rs.Rows[i]["TglLahir"]) + "','"
                    + rs.Rows[i]["NoKTP"] + "','"
                    + rs.Rows[i]["NPWP"] + "','"
                    + rs.Rows[i]["KTP1"] + "','"
                    + rs.Rows[i]["KTP2"] + "','"
                    + rs.Rows[i]["KTP3"] + "','"
                    + rs.Rows[i]["KTP4"] + "','"
                    + sifat + "','"
                    + rs.Rows[i]["Alamat1"] + "','"
                    + rs.Rows[i]["Alamat2"] + "','"
                    + rs.Rows[i]["Alamat3"] + "','"
                    + rs.Rows[i]["Alamat4"] + "','"
                    + rs.Rows[i]["RekBank"] + "','"
                    + rs.Rows[i]["RekCabang"] + "','"
                    + rs.Rows[i]["RekNo"] + "','"
                    + rs.Rows[i]["RekNama"] + "')\"><b>"
                    + rs.Rows[i]["Nama"].ToString()
                    + " (" + rs.Rows[i]["NoCustomer"].ToString().PadLeft(5, '0') + ")"
                    + "</b></a>"
                    + "<table cellpadding=0 cellspacing=0>"
                    + "<tr><td>Tipe</td><td>:</td><td>" + rs.Rows[i]["TipeCs"] + "</td></tr>"
                    + "<tr><td>NPWP</td><td>:</td><td>" + rs.Rows[i]["NPWP"] + "</td></tr>"
                    + "<tr style='display:none'><td>Tgl Lahir</td><td>:</td><td>" + Cf.Day(rs.Rows[i]["TglLahir"]) + "</td></tr>"
                    + "<tr><td colspan=3><br></td></tr>"
                    + "<tr><td>Telp</td><td>:</td><td>" + rs.Rows[i]["NoTelp"] + "</td></tr>"
                    + "<tr><td>HP</td><td>:</td><td>" + rs.Rows[i]["NoHP"] + "</td></tr>"
                    + "<tr><td>Email</td><td>:</td><td>" + rs.Rows[i]["Email"] + "</td></tr>"
                    + "<tr><td colspan=3><br></td></tr>"
                    + "<tr><td>Bank Refund</td><td>:</td><td>" + rs.Rows[i]["RekBank"] + "&nbsp;" + rs.Rows[i]["RekCabang"] + "</td></tr>"
                    + lama
                    + "</table>"
                    ;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "KTP :"
                    + "<div style=padding-left:20>"
                    + rs.Rows[i]["NoKTP"]
                    + "<br>" + rs.Rows[i]["KTP1"]
                    + "<br>" + rs.Rows[i]["KTP2"]
                    + "<br>" + rs.Rows[i]["KTP3"]
                    + "<br>" + rs.Rows[i]["KTP4"]
                    + "</div>"
                    + sifatvalue
                    ;

                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
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
