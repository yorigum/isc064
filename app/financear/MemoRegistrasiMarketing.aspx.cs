using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class MemoRegistrasiMarketing : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Table rpt;
        protected DataTable rsTagihan;
        protected string Cnn { get { return "Data Source=.;Initial Catalog=43GOLD;Persist Security Info=True;User ID=batavianet;Password=iNDigo100"; } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                InitForm();

                detildiv.Visible = false;
                Js.NumberFormat(nilai);
                Js.NumberFormat(admBank);
            }

            if (detildiv.Visible)
                Js.Confirm(this, "Lanjutkan proses registrasi memo pelunasan?");

            FillTb();
        }

        private void InitForm()
        {
            tglmemo.Text = Cf.Day(Convert.ToDateTime(DateTime.Today));
            gt.Attributes["style"] = "border:0px;font:bold;";
            tipe.Text = Tipe;
            referensi.Text = Ref;

            unit.Text = Db.SingleString("SELECT NoUnit "
                + " FROM " + Tb + "..MS_KONTRAK "
                + " WHERE NoKontrak = '" + Ref + "'");

            customer.Text = Db.SingleString("SELECT Nama "
                + " FROM " + Tb + "..MS_KONTRAK AS MS_KONTRAK "
                + " INNER JOIN " + Tb + "..MS_CUSTOMER AS MS_CUSTOMER "
                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE NoKontrak = '" + Ref + "'");
        }

        private void FillTb()
        {
            string strSql = "SELECT * "
                + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') ) AS SisaTagihan"
                + " FROM " + Tb + "..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + Ref + "'"
                + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') ) > 0"
                + " ORDER BY NoUrut";
            rsTagihan = Db.Rs(strSql);

            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                TextBox t;
                TextBox tgl;
                HtmlInputButton btn;
                HtmlGenericControl div;
                HtmlGenericControl span;
                HtmlGenericControl label;
                HtmlGenericControl italic;

                l = new Label();
                l.Text = "<tr valign=top>"
                    + "<td>" + rsTagihan.Rows[i]["NoKontrak"] + "." + rsTagihan.Rows[i]["NoUrut"] + "</td>"
                    + "<td>" + rsTagihan.Rows[i]["NamaTagihan"] + "</td>"
                    + "<td>" + rsTagihan.Rows[i]["Tipe"] + "</td>"
                    + "<td style='white-space:nowrap'>" + Cf.Day(rsTagihan.Rows[i]["TglJT"]) + "</td>"
                    + "<td align=right>" + Cf.Num(rsTagihan.Rows[i]["SisaTagihan"]) + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                t = new TextBox();
                t.ID = "lunas_" + i;
                t.Width = 100;
                t.CssClass = "txt_num";
                Js.NumberFormat(t);
                t.Attributes["onblur"] += "hitunggt();";
                list.Controls.Add(t);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                //Tanggal

                div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "input-group input-small");
                div.Attributes.Add("style", "margin-top: 0px; margin-left: 0px;");

                tgl = new TextBox();
                tgl.ID = "tgl_" + i;
                tgl.CssClass = "tgl form-control form-control-small";
                tgl.Text = Cf.Day(Convert.ToDateTime(DateTime.Today));
                tgl.Attributes["style"] = "font:8pt; width:65%";
                div.Controls.Add(tgl);

                span = new HtmlGenericControl("span");
                span.Attributes.Add("style", "height: 34px; display: block;");
                span.Attributes.Add("class", "input-group-btn");

                label = new HtmlGenericControl("label");
                label.Attributes.Add("for", tgl.ID);
                label.Attributes.Add("class", "btn-a default btn-cal");

                italic = new HtmlGenericControl("i");
                italic.Attributes.Add("class", "fa fa-calendar");
                label.Controls.Add(italic);
                span.Controls.Add(label);

                div.Controls.Add(span);
                list.Controls.Add(div);

                l = new Label();
                l.Text = "</td><td><input type='checkbox' onclick=\"tagihan('" + i + "','" + Cf.Num(rsTagihan.Rows[i]["SisaTagihan"]) + "',this)\"></td></tr>";
                list.Controls.Add(l);


            }
        }

        private bool datavalid()
        {

            if (tipememo.SelectedIndex == -1)
            {
                Js.Alert(
                    this
                    , "Tipe Memo Tidak Valid.\\n"
                    + "Silakan pilih salah satu tipe memo yang tersedia."
                    , ""
                    );

                return false;
            }
            else
            {
                string s = "", strFocus = "";
                bool x = true;


                bool adasatu = false;
                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    TextBox lunas = (TextBox)list.FindControl("lunas_" + i);
                    TextBox tgl = (TextBox)list.FindControl("tgl_" + i);

                    if (rsTagihan.Rows.Count > 0)
                    {
                        adasatu = true;
                        //if (!Cf.isTgl(tgl))
                        //{
                        //    x = false;

                        //    if (s == "")
                        //        s = tgl.ID;

                        //    break;
                        //}

                        //if (Cf.isEmpty(nilai))
                        //{
                        //    x = false;

                        //    if (s == "")
                        //        s = nilai.ID;

                        //    break;
                        //}
                    }



                }
                if (!Cf.isTgl(tglmemo))
                {
                    x = false;
                    if (s == "") s = tglmemo.ID;
                    tglmemoc.Text = "Tanggal";
                }
                else
                    tglmemoc.Text = "";
                if (!adasatu)
                {
                    x = false;
                    if (s == "") s = gt.ID;
                    gtc.Attributes["style"] = "color:red";
                }
                else
                    gtc.Attributes["style"] = "color:black";

                if (s != "")
                    strFocus = "document.getElementById('" + s + "').focus();";

                if (!x)
                    Js.Alert(
                        this
                        , "Input Tidak Valid.\\n\\n"
                        + "Aturan Proses :\\n"
                        + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                        + "2. Pembayaran harus berupa angka dan minimal untuk satu tagihan.\\n"
                        , "document.getElementById('" + s + "').focus();"
                        );

                return x;
            }
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                DateTime TglMEMO = Convert.ToDateTime(tglmemo.Text);
                string Unit = Cf.Str(unit.Text);
                string Customer = Cf.Str(customer.Text);
                string Keterangan = Cf.Str(Ket.Text);
                string TipeMemo = tipememo.SelectedValue;
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Ref + "'");
                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                //Numerator
                string NoMEMO2 = Numerator.MEMO(TglMEMO.Month, TglMEMO.Year,Project);

                Db.Execute("EXEC spMEMORegistrasi"
                    + " '" + TglMEMO + "'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Tipe + "'"
                    + ",'" + Ref + "'"
                    + ",'" + Unit + "'"
                    + ",'" + Customer + "'"
                    + ",'" + TipeMemo + "'"
                    + ",'" + Keterangan + "'"
                    + ",0"
                    );

                int NoMEMO = Db.SingleInteger("SELECT TOP 1 NoMEMO FROM MS_MEMO ORDER BY NoMEMO DESC");
                Db.Execute("UPDATE MS_MEMO SET NoMEMO2 = '" + NoMEMO2 + "', Project = '" + Project + "', NamaProject = '" + NamaProject + "' WHERE NoMEMO ='" + NoMEMO + "'");
                System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    TextBox lunas = (TextBox)list.FindControl("lunas_" + i);
                    TextBox tgl = (TextBox)list.FindControl("tgl_" + i);
                    if (lunas.Text != "")
                    {
                        int NoTagihan = (int)rsTagihan.Rows[i]["NoUrut"];
                        string NamaTagihan = Cf.Str(rsTagihan.Rows[i]["NamaTagihan"])
                            + " (" + rsTagihan.Rows[i]["Tipe"] + ")";
                        decimal Nilai = Convert.ToDecimal(lunas.Text);
                        DateTime Tgl = Convert.ToDateTime(tgl.Text);

                        Db.Execute("EXEC spMEMOAlokasi "
                            + "  '" + NoMEMO + "'"
                            + ", " + NoTagihan
                            + ", " + Nilai
                            );
                        alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");
                    }
                }

                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    TextBox lunas = (TextBox)list.FindControl("lunas_" + i);
                    TextBox tgl = (TextBox)list.FindControl("tgl_" + i);
                    if (lunas.Text != "")
                    {
                        int NoTagihan = (int)rsTagihan.Rows[i]["NoUrut"];
                        string NamaTagihan = Cf.Str(rsTagihan.Rows[i]["NamaTagihan"])
                            + " (" + rsTagihan.Rows[i]["Tipe"] + ")";
                        decimal Nilai = Convert.ToDecimal(lunas.Text);
                        DateTime Tgl = Convert.ToDateTime(tgl.Text);
                        Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_PELUNASAN"
                             + " SET"
                             + " TglPelunasan ='" + Tgl + "'"
                             + " ,SudahCair = 1"
                             + " WHERE NoKontrak='" + referensi.Text + "' AND NoMemo='" + NoMEMO + "' AND NoTagihan='" + NoTagihan + "'"
                            );
                        Db.Execute("UPDATE MS_MEMO SET Status='POST', TglBKM=TglMemo WHERE NoMemo='" + NoMEMO + "'");
                    }
                }

                DataTable rs = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglMEMO, 106) AS [Tanggal]"
                    + ",Tipe"
                    + ",Ref AS [Ref.]"
                    + ",Unit"
                    + ",Customer"
                    + ",CaraBayar AS [Cara Bayar]"
                    + ",Ket AS [Keterangan]"
                    + ",Total"
                    + ",NoBG AS [No. BG]"
                    + ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
                    + ", Acc AS [Rekening Bank]"
                    + " FROM MS_MEMO WHERE NoMEMO = " + NoMEMO);

                string KetLog = Cf.LogCapture(rs)
                    + "<br>***ALOKASI PEMBAYARAN:<br>"
                    + alokasi.ToString();

                Db.Execute("EXEC spLogMEMO"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + NoMEMO.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_MEMO_LOG ORDER BY LogID DESC");                
                Db.Execute("UPDATE MS_MEMO_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Db.Execute("EXEC ISC064_MARKETINGJUAL..spProsentasePelunasan '" + referensi.Text + "'");

                Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_KONTRAK SET FlagMemo=1 WHERE NoKontrak='" + referensi.Text + "'");
                Response.Redirect("MEMORegistrasi.aspx?done=" + NoMEMO);

            }

        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (Cf.isMoney(nilai))
            {
                detildiv.Visible = true;
                nilaitr.Visible = false;

                Alokasi(Convert.ToDecimal(nilai.Text));

                Js.Confirm(this, "Lanjutkan proses registrasi memo pelunasan?");
            }
            else
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Nilai harus berupa angka.\\n"
                    , "document.getElementById('" + nilai.ID + "').focus();"
                    + "document.getElementById('" + nilai.ID + "').select();"
                    );
        }
        private void Alokasi(decimal total)
        {
            decimal x = total;

            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                TextBox lunas = (TextBox)list.FindControl("lunas_" + i);
                decimal SisaTagihan = (decimal)rsTagihan.Rows[i]["SisaTagihan"];

                if (i == rsTagihan.Rows.Count - 1)
                {
                    //last row
                    lunas.Text = Cf.Num(x);
                }
                else
                {
                    if (SisaTagihan >= x)
                    {
                        //break, soalnya total udah abis
                        lunas.Text = Cf.Num(x);
                        break;
                    }
                    else
                    {
                        lunas.Text = Cf.Num(SisaTagihan);
                    }
                }
                x = x - SisaTagihan;
            }

            gt.Text = Cf.Num(total);
        }

        private bool isUnique(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_MEMO WHERE NoMEMO2 = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
        }
        private string Tb
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
