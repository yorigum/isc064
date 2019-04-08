using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class HistoryFolUp : System.Web.UI.Page
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
                Bind();
                InitForm();
            }
            Filltb();
            Js.Confirm(this, "Lanjutkan proses registrasi follow up?");
        }

        private void Bind()
        {
            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_FOLLOWUP");
            if (rs.Rows.Count != 0)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string v = rs.Rows[i]["No"].ToString();
                    string t = rs.Rows[i]["NamaGrouping"].ToString();

                    //rblgrup.Items.Add(new ListItem(t, t));

                }
            }
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
                + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak='" + Ref + "') ) AS SisaTagihan"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0)FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') AS NilaiPelunasan"
                + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN"
                + " WHERE NoKontrak = '" + Ref + "'"
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

                string sql = "SELECT NoFU,TglFU,NamaGrouping,Ket,TglJanjiBayar,Collector FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoKontrak = '" + Ref + "' AND NoTagihan='" + rsTagihan.Rows[i]["NoUrut"] + "'";
                DataTable rs = Db.Rs(sql);
                if (rs.Rows.Count > 0)
                {
                    for (int a = 0; a < rs.Rows.Count; a++)
                    {

                        if (a > 0)
                        {
                            l = new Label();
                            l.Text = "<tr>";
                            list.Controls.Add(l);

                            l = new Label();
                            l.Text = "<td colspan='7'>";
                            list.Controls.Add(l);

                            l = new Label();
                            l.Text = "</td>";
                            list.Controls.Add(l);
                        }

                        l = new Label();
                        l.Text = "<td>";
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = Cf.Day(rs.Rows[a]["TglFU"]);
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = "</td>";
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = "<td>";
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = rs.Rows[a]["NamaGrouping"].ToString();
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = "</td>";
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = "<td>";
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = rs.Rows[a]["Ket"].ToString();
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = "</td>";
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = "<td>";
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = Cf.Day(rs.Rows[a]["TglJanjiBayar"]);
                        l.Width = 300;
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = "</td>";
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = "<td>";
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = Db.SingleString("SELECT Nama FROM ISC064_SECURITY..USERNAME WHERE UserID= '" + rs.Rows[a]["Collector"].ToString() + "'");
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = "</td>";
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = "<td>";
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = "<a href =\"javascript:Hapus('" + Ref + "','" + rs.Rows[a]["NoFU"].ToString() + "')\">Delete</a>";
                        list.Controls.Add(l);

                        l = new Label();
                        l.Text = "</td>";
                        list.Controls.Add(l);

                    }
                }
                else
                {
                    l = new Label();
                    l.Text = "<td colspan='6'>";
                    list.Controls.Add(l);

                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);

                }

                //l = new Label();
                //l.Text = "</tr>";
                //list.Controls.Add(l);


                //rblgrup.SelectedValue = rs.Rows[0]["Grouping"].ToString();
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

    }
}
