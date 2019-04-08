using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class FollowUpEdit : System.Web.UI.Page
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

                    rblgrup.Items.Add(new ListItem(t, t));

                }
            }
        }

        private void InitForm()
        {
            btndel.Attributes["onclick"] = "location.href ='FollowUpDel.aspx?NoFU="+NoFU+"&NoKontrak="+Ref+"'";
            NoFolup.Text = NoFU;
            referensi.Text = Ref;                        
            unit.Text = Db.SingleString("SELECT NoUnit "
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK "
                + " WHERE NoKontrak = '" + Ref + "'");

            DataTable rs = Db.Rs("SELECT Nama, NoHP, Alamat1, Alamat2, Alamat3 "
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK "
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER "
                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE NoKontrak = '" + Ref + "'");
            if (rs.Rows.Count != 0)
            {

                customer.Text = rs.Rows[0]["Nama"].ToString();
                notelp.Text = rs.Rows[0]["NoHP"].ToString();
                alamat1.Text = rs.Rows[0]["Alamat1"].ToString();
                alamat2.Text = rs.Rows[0]["Alamat2"].ToString();
                alamat3.Text = rs.Rows[0]["Alamat3"].ToString();
            }

            rs = Db.Rs("Select * From " + Mi.DbPrefix + "MARKETINGJUAL..MS_FOLLOWUP WHERE NoKontrak = '" + Ref + "' AND NoFU = '"+NoFU+"'");

            if (rs.Rows.Count != 0)
            {


                //decimal lunas = Db.SingleDecimal("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM "
                //    + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
                //    + " WHERE NoKontrak = '" + Ref + "'))"
                //    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN "
                //    + " WHERE NoKontrak = '" + Ref + "'");


                if (rs.Rows[0]["NamaGrouping"].ToString() != "")
                {
                    string NamaGrup = rs.Rows[0]["NamaGrouping"].ToString();
                    int NoGrup = Db.SingleInteger("SELECT ISNULL(No,0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_FOLLOWUP WHERE NamaGrouping = '" + NamaGrup + "'");
                    rblgrup.SelectedValue = Convert.ToString(NoGrup);
                }
                else
                {
                    rblgrup.SelectedValue = "1";
                }

                tgl.Text = Cf.Day(rs.Rows[0]["TglFU"].ToString());
                keterangan.Text = rs.Rows[0]["Ket"].ToString();
                rblgrup.SelectedValue = rs.Rows[0]["NamaGrouping"].ToString();
            }


        }
        private void Filltb()
        {            
            string sql = "SELECT * FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP_DETIL WHERE NoKontrak = '" + Ref+"' and NoFU ='"+NoFU+"'";
            rsTagihan = Db.Rs(sql); 


            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {               

                if (!Response.IsClientConnected) break;

                Label l;
                TextBox t;

                l = new Label();
                l.Text = "<tr valign=top>"
                    + "<td>" + rsTagihan.Rows[i]["NoKontrak"] + "." + rsTagihan.Rows[i]["NoTagihan"] + "</td>"
                    + "<td>" + rsTagihan.Rows[i]["NamaTagihan"] + "</td>"
                    + "<td>" + rsTagihan.Rows[i]["Tipe"] + "</td>"
                    + "<td style='white-space:nowrap'>" + Cf.Day(rsTagihan.Rows[i]["TglJT"]) + "</td>"
                    + "<td align=right>" + Cf.Num(rsTagihan.Rows[i]["Nilai"]) + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "tgl_" + i;
                bx.Width = 75;
                bx.CssClass = "txt_center";
                bx.Attributes["style"] = "font:8pt";
                bx.Text = Cf.Day(rsTagihan.Rows[i]["TglJanjiBayar"].ToString());
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "&nbsp;";
                list.Controls.Add(l);

                bt = new HtmlInputButton();
                bt.ID = "tgljanji_" + i;
                bt.Attributes["onclick"] = "openCalendar('tgl_" + i.ToString() + "')";
                bt.Attributes["class"] = "btn btn-cal";
                bt.Attributes["style"] = "font-family: 'fontawesome'";
                //bt.Disabled = true;                            
                list.Controls.Add(bt);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);
            }
        }
        private bool datavalid()
        {
            string s = "";
            bool x = true;

            if (tgl.ToString() == "")
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Tanggal / Bulan / Tahun.\\n"
                    + "2. Tanggal Tidak Boleh Kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if (datavalid())
            {                
                string Ket = Cf.Str(keterangan.Text);
                string NoTelp = Cf.Str(notelp.Text);
                string Alamat = Cf.Str(alamat1.Text + " " + alamat2.Text + " " + alamat3.Text);
                string Ref = Cf.Str(referensi.Text);
                string NoFU = Cf.Str(NoFolup.Text);
                DateTime TglUpdate = Convert.ToDateTime(DateTime.Today);
                string Grouping = rblgrup.SelectedValue;

                DataTable rsBef = Db.Rs("SELECT "
                    + " a.NamaGrouping [Grouping]"
                    + ",a.Ket AS [Keterangan]"
                    + ",a.NoHP AS [No. Telepon]"
                    + ",a.Alamat"
                    + ",b.TglJanjiBayar AS [Tgl Janji Bayar]"
                    + " FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP a JOIN ISC064_MARKETINGJUAL..MS_FOLLOWUP_DETIL b ON a.NoFU = b.NoFU"
                    + " WHERE a.NoKontrak = '" + Ref + "'"
                    );


                Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_FOLLOWUP SET NamaGrouping = '"+Grouping+"', NoHP = '"+NoTelp+"', Ket = '"+Ket+"',Alamat = '"+Alamat+"',TglUpdate = '"+TglUpdate+"' WHERE NoKontrak = '"+Ref+"' AND NoFU = '"+NoFU+"'");
                
                System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                        string NoTagihan = rsTagihan.Rows[i]["NoUrut"].ToString();
                        string NamaTagihan = rsTagihan.Rows[i]["NamaTagihan"].ToString();
                        string Tipe = rsTagihan.Rows[i]["Tipe"].ToString();
                        DateTime TglJT = Convert.ToDateTime(rsTagihan.Rows[i]["TglJT"].ToString());
                        decimal Nilai = Convert.ToDecimal(rsTagihan.Rows[i]["Nilai"]);
                        TextBox TglJanji = (TextBox)list.FindControl("tgl_" + i);
                        DateTime TglJanji1 = Convert.ToDateTime(TglJanji.Text);

                        Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_FOLLOWUP_DETIL SET TglJanjiBayar = '"+TglJanji1+"' WHERE NoFU = '"+NoFU+"' AND NoUrut = '"+NoTagihan+"' ");

                        alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");                    
                }
                
                DataTable rsAft = Db.Rs("SELECT "
                        + " a.NamaGrouping [Grouping]"
                        + ",a.Ket AS [Keterangan]"
                        + ",a.NoHP AS [No. Telepon]"
                        + ",a.Alamat"
                        + ",b.TglJanjiBayar AS [Tgl Janji Bayar]"
                        + " FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP a JOIN ISC064_MARKETINGJUAL..MS_FOLLOWUP_DETIL b ON a.NoFU = b.NoFU"
                        + " WHERE a.NoKontrak = '" + Ref + "'"
                        );

                string ketlog = Cf.LogCompare(rsBef, rsAft)
                            +"<br>***DETIL FOLLOW UP:<br>"
                            + alokasi.ToString();

                Db.Execute("EXEC ISC064_MARKETINGJUAL..spLogFU"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ketlog + "'"
                    + ",'" + NoFU.ToString() + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_FOLLOWUP_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT NoKontrak FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_FOLLOWUP WHERE NoFU = '" + NoFU + "') ");
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_FOLLOWUP_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("MasterFollowUp.aspx");
            }
        }

        private string Ref
        {
            get
            {
                return Cf.Pk(Request.QueryString["Ref"]);
            }
        }

        private string NoFU
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoFU"]);
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