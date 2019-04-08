using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class RegistrasiFollowUp : System.Web.UI.Page
    {
        protected DataTable rsTagihan;
        TextBox bx;
        HtmlInputButton bt;
        CheckBox cb;

        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                InitForm();
            }
            Filltb();
            Js.Confirm(this, "Lanjutkan proses registrasi follow up?");
        }

        private void InitForm()
        {
            //tagihan.Text = "ANGSURAN " + NoUrutJT;

            tipe.Text = Tipe;
            nokontrak.Text = Ref;

            unit.Text = Db.SingleString("SELECT NoUnit "
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK "
                + " WHERE NoKontrak = '" + Ref + "'");

            DataTable rs = Db.Rs("SELECT * "
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK "
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER "
                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE NoKontrak = '" + Ref + "'");
            if (rs.Rows.Count != 0)
            {

                customer.Text = rs.Rows[0]["Nama"].ToString();
                hp1.Text = rs.Rows[0]["NoHP"].ToString();
                hp2.Text = rs.Rows[0]["NoHP2"].ToString();
                marketing.Text = Db.SingleString("SELECT Nama FROM ISC064_MARKETINGJUAL..MS_AGENT WHERE NoAgent = '" + rs.Rows[0]["Noagent"].ToString() + "'");
                alamat.Text = rs.Rows[0]["Alamat1"].ToString() + rs.Rows[0]["Alamat2"].ToString() + rs.Rows[0]["Alamat3"].ToString();
            }

            string strSql = "SELECT "
                + " (SELECT ISNULL(SUM(NilaiTagihan),0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('BF','DP','ANG')) AS TotalTagihan"
                + ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('ADM')) AS TotalBiaya"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan <> 0 AND SudahCair = 1) AS TotalPelunasan"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan <> 0) AS TotalPembayaran"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan = 0) AS Unallocated"
                + ",PersenLunas"
                + ",NilaiKontrak"
                + ",OutBalance"
                + ",Skema"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK"
                + " WHERE NoKontrak = '" + Ref + "'";
            DataTable rs2 = Db.Rs(strSql);

            if (rs2.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nilaikontrak.Text = Cf.Num(rs2.Rows[0]["NilaiKontrak"]);
                tagihan.Text = Cf.Num(rs2.Rows[0]["TotalTagihan"]);
                adm.Text = Cf.Num(rs2.Rows[0]["TotalBiaya"]);
                tagadm.Text = Cf.Num((decimal)rs2.Rows[0]["TotalTagihan"] + (decimal)rs2.Rows[0]["TotalBiaya"]);
                pembayaran.Text = Cf.Num(rs2.Rows[0]["OutBalance"]);
                lunas.Text = Cf.Num(rs2.Rows[0]["TotalPelunasan"]);
            }           
        }
        private void Filltb()
        {
            string strSql = "SELECT "
                + "NamaTagihan"
                + ",Tipe"
                + ",TglJT"
                + ",NoUrut"
                + ",NilaiTagihan"
                + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak='" + Ref + "') ) AS SisaTagihan"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0)FROM " + tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') AS NilaiPelunasan"
                + " FROM "+tb+"..MS_TAGIHAN"
                + " WHERE NoKontrak = '" + Ref + "'"
                + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak='" + Ref + "') ) > 0"
                + " ORDER BY TglJT";

             rsTagihan = Db.Rs(strSql);
   
            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                TextBox t;

                l = new Label();
                l.Text = "<tr valign=top>"
                    + "<td>" + (i + 1) + "</td>"
                    + "<td>" + rsTagihan.Rows[i]["NamaTagihan"] + "</td>"
                    + "<td>" + rsTagihan.Rows[i]["Tipe"] + "</td>"
                    + "<td style='white-space:nowrap'>" + Cf.Day(rsTagihan.Rows[i]["TglJT"]) + "</td>"
                    + "<td align=right>" + Cf.Num(rsTagihan.Rows[i]["NilaiTagihan"]) + "</td>"
                    + "<td align=right>" + Cf.Num(rsTagihan.Rows[i]["NilaiPelunasan"]) + "</td>"
                    + "<td align=right>" + Cf.Num(rsTagihan.Rows[i]["SisaTagihan"]) + "</td>";                    
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<a href=\"javascript:folup('" + Ref + "','" + rsTagihan.Rows[i]["NoUrut"] + "')\"> Follow Up </a>"; ;
                list.Controls.Add(l);
               
                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<a href=\"javascript:history('" + Ref + "','" + rsTagihan.Rows[i]["NoUrut"] + "')\"> History </a>"; ;
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);
            }


        }
        private string tb
        {
            get
            {
                return Sc.MktTb(Tipe);
            }
        }

        private string Ref
        {
            get
            {
                return Cf.Pk(Request.QueryString["Ref"]);
            }
        }

        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
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