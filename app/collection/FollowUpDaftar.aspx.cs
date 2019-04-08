using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class FollowUpDaftar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Bind();
            }
            Js.Confirm(this, "Lanjutkan proses registrasi follow up?");
        }
        private bool valid()
        {
            string s = "";
            bool x = true;

            if (keterangan.Text == "")
            {
                x = false;
                if (s == "") s = keterangan.ID;
                keteranganc.Text = "Keterangan Tidak Boleh Kosong";
            }
            else
                keteranganc.Text = "";

            if (rblgrup.SelectedValue == "")
            {
                x = false;
                if (s == "") s = rblgrup.ID;
                rblgrupc.Text = "Pilih Salah Satu";
            }
            else
                rblgrupc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Kategori Harus Dipilih.\\n"
                    + "2. Keterangan Tidak Boleh Kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;

        }
        private void Bind()
        {
            tglfu.Text = Cf.Day(DateTime.Today);
            string Project = Db.SingleString("SELECT Project FROM "+Mi.DbPrefix+"MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Ref + "'");
            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_FOLLOWUP WHERE Project  = '" + Project + "'");
            if (rs.Rows.Count != 0)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string v = rs.Rows[i]["No"].ToString();
                    string t = rs.Rows[i]["NamaGrouping"].ToString();                    
                    rblgrup.Items.Add(new ListItem(" " + t, t));
                    rblgrup.CellSpacing = 10;
                }
            }
        }


        protected void save_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                DataTable rs = Db.Rs("SELECT NoHP, Alamat1, Alamat2, Alamat3 "
                               + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK "
                               + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER "
                               + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                               + " WHERE NoKontrak = '" + Ref + "'");
                if (rs.Rows.Count != 0)
                {
                    DateTime TglFU = Convert.ToDateTime(DateTime.Today);
                    DateTime TglInput = Convert.ToDateTime(DateTime.Today);
                    string Ket = keterangan.Text;
                    string Grouping = rblgrup.SelectedValue;
                    string Alamat = rs.Rows[0]["Alamat1"].ToString() + " " + rs.Rows[0]["Alamat2"].ToString() + " " + rs.Rows[0]["Alamat3"].ToString();
                    string NoHP = rs.Rows[0]["NoHP"].ToString();

                    Db.Execute("EXEC ISC064_MARKETINGJUAL..spFollowUp "
                            + " '" + Ref + "'"
                            + ",'" + TglFU + "'"
                            + ",'" + Grouping + "'"
                            + ",'" + Ket + "'"
                            + ",'" + NoHP + "'"
                            + ",'" + Alamat + "'"
                            + ",'" + TglInput + "'"
                            );
                    int NoFU = Db.SingleInteger("SELECT TOP 1 NoFU FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoKontrak = '" + Ref + "' ORDER BY NoFU DESC");
                    System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

                    Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_FOLLOWUP SET NoTagihan = '" + NoUrut + "',Collector = '" + Act.UserID + "' WHERE NoFU = '" + NoFU + "' AND NoKontrak = '" + Ref + "'");

                    if (tgl.Text != "")
                    {
                        DateTime TglJanjiBayar = Convert.ToDateTime(tgl.Text);
                        Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_FOLLOWUP SET TglJanjiBayar = '" + TglJanjiBayar + "' WHERE NoFU = '" + NoFU + "' AND NoKontrak = '" + Ref + "'");
                    }

                    DataTable rs2 = Db.Rs("SELECT NamaTagihan,NilaiTagihan,TglJT FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + Ref + "' AND NoUrut = '" + NoUrut + "'");
                    alokasi.Append(rs2.Rows[0]["NamaTagihan"].ToString() + "    " + Cf.Num(rs2.Rows[0]["NilaiTagihan"].ToString()) + "    " + Cf.Day(rs2.Rows[0]["TglJT"].ToString()) + "<br>");

                    DataTable rsLog = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglFU, 106) AS [Tanggal Follow UP]"
                    + ",NoKontrak AS [Ref.]"
                    + ",CONVERT(varchar, TglJanjiBayar, 106) AS [Tanggal Janji Bayar]"
                    + ",NamaGrouping AS [Kategori]"
                    + ",Ket"
                    + " FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoFU = '" + NoFU + "'");

                    string KetLog = Cf.LogCapture(rsLog)
                        + "<br>***DETIL FOLLOW UP:<br>"
                        + alokasi.ToString();


                    Db.Execute("EXEC ISC064_MARKETINGJUAL..spLogFU"
                        + " 'REGIS'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + NoFU.ToString() + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_FOLLOWUP_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Ref + "'");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_FOLLOWUP_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    Js.Close(this);
                }
            }
        }
        private string Ref
        {
            get
            {
                return Cf.Pk(Request.QueryString["Ref"]);
            }
        }

        private string NoUrut
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoUrut"]);
            }
        }

    }
}