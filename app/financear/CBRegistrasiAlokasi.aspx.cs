using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
namespace ISC064.FINANCEAR
{
    public partial class CBRegistrasiAlokasi : System.Web.UI.Page
    {
        DataTable rs;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Fill();
            fillTagihan();
            if (!Page.IsPostBack)
            {


                if (Request.QueryString["NoKontrak"] != null)
                {
                    LoadKontrak();
                }
                else
                {
                    frm.Visible = false;
                }
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses pencatatan Cashback?");
        }

        private void FeedBack()
        {
            //feed.Text = "";
            if (!Page.IsPostBack)
            {
                // if (Request.QueryString["done"] != null)
                //feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                //+ "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                //+ "Proses Berhasil..."
                //+ "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");// AND PPJB <> 'D'");

            if (c == 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );

            return x;
        }

        private void LoadKontrak()
        {
            if (valid())
            {
                frm.Visible = true;

                Js.Confirm(this, "Lanjutkan proses pencatatan Cashback?");
            }
            else
            {
                frm.Visible = false;
            }
        }

        decimal LebihBayar1 = 0;
        private void Fill()
        {
            Js.NumberFormat(sisa);
            Js.NumberFormat(lb);

            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            unit.Text = Db.SingleString("SELECT NoUnit "
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK "
                + " WHERE NoKontrak = '" + NoKontrak + "'");

            customer.Text = Db.SingleString("SELECT Nama "
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK "
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER "
                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE NoKontrak = '" + NoKontrak + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                decimal nilaitagihan = 0;
                decimal nilaipelunasan1 = 0;
                decimal nilaipelunasan2 = 0;
                decimal sisatagihan = 0;
                decimal lebihbayar = 0;
                decimal bankkeluar = 0;

                DataTable rs1 = Db.Rs("SELECT * "
                + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN "
                + " WHERE NoKontrak = '" + NoKontrak + "'");

                for (int i = 0; i < rs1.Rows.Count; i++)
                {
                    decimal x = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = '" + rs1.Rows[i]["Nourut"] + "'");// AND CaraBayar!='AL'

                    nilaitagihan += Convert.ToDecimal(rs1.Rows[i]["NilaiTagihan"]);

                    if (x > Convert.ToDecimal(rs1.Rows[i]["NilaiTagihan"]))
                    {
                        nilaipelunasan1 += Convert.ToDecimal(rs1.Rows[i]["NilaiTagihan"]);
                    }
                    else
                    {
                        nilaipelunasan1 += x;
                    }

                    nilaipelunasan2 += x;
                }
                decimal sumAlokasi = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND CaraBayar='AL'");
                decimal MemoCB = 0;// Db.SingleDecimal("SELECT ISNULL(SUM(LebihBayar),0) FROM MS_CASHBACK_MEMO where nokontrak='" + NoKontrak + "'");
                sisatagihan = nilaitagihan - nilaipelunasan1;
                bankkeluar = nilaitagihan - nilaipelunasan2;

                lebihbayar = Db.SingleDecimal("SELECT ISNULL(SUM(LB),0) FROM MS_TTS WHERE Ref = '" + NoKontrak + "' AND Status <> 'VOID'");
                decimal BK = Db.SingleDecimal("SELECT ISNULL(SUM(BankKeluar),0) FROM MS_CASHBACK WHERE NoKontrak = '" + NoKontrak + "'");

                sisa.Text = Cf.Num(sisatagihan);
                lb.Text = Cf.Num(lebihbayar - BK - sumAlokasi - MemoCB);

                LebihBayar1 = lebihbayar - BK;
            }
        }
        private bool datavalid()
        {
            string s = "";
            bool x = true;


            if (!Cf.isMoney(sisa))
            {
                x = false;
                if (s == "") s = sisa.ID;
                sisac.Text = "Angka";
            }
            else
                sisac.Text = "";

            if (!Cf.isMoney(lb))
            {
                x = false;
                if (s == "") s = lb.ID;
                lbc.Text = "Angka";
            }
            else
                lbc.Text = "";


            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Sisa Tagihan harus berupa angka.\\n"
                    + "3. Lebih Bayar harus berupa angka.\\n"
                    + "4. Bank harus berupa angka."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }


        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
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

        void fillTagihan()
        {

            decimal LB = Convert.ToDecimal(lb.Text); ;
            decimal total = 0;
            string query = "SELECT (a.Nokontrak + '.' + Cast(a.NoUrut AS VARCHAR)) AS NoTagihan, "
                        + "         a.NamaTagihan, "
                        + "         a.Tipe, "
                        + "         a.TglJT, "
                        + "         a.NoUrut, "
                        + "         (a.NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = a.NoKontrak and NoTagihan = a.NoUrut)) AS SisaTagihan "
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN A "
                        + " WHERE 1=1 "
                        + " AND A.Nokontrak = '" + NoKontrak + "'"
                        + " AND (A.NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = a.NoKontrak AND NoTagihan = a.NoUrut)) >0 ";

            rs = Db.Rs(query);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow r = new HtmlTableRow();
                HtmlTableCell c = new HtmlTableCell();
                TextBox t;
                HtmlButton bt;
                Label l;

                c.InnerHtml = rs.Rows[i]["NoTagihan"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NamaTagihan"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Tipe"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Day(rs.Rows[i]["TglJT"]);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["SisaTagihan"]));
                c.Align = "Right";
                r.Cells.Add(c);

                c = new HtmlTableCell();
                t = new TextBox();
                t.ID = "bayar_" + i;
                t.Attributes.Add("style", "text-align:right;");
                if (Convert.ToDecimal(rs.Rows[i]["SisaTagihan"]) <= LB)
                {
                    t.Text = Cf.Num(rs.Rows[i]["SisaTagihan"]);
                    total += Convert.ToDecimal(rs.Rows[i]["SisaTagihan"]);
                    totalBayar.Text = Cf.Num(total);
                }
                else
                {
                    if (LB > 0)
                    {
                        t.Text = Cf.Num(LB);
                        total += LB;
                        LB -= Convert.ToDecimal(rs.Rows[i]["SisaTagihan"]);
                        totalBayar.Text = Cf.Num(total);
                    }
                }
                Js.NumberFormat(t);
                t.Attributes["onblur"] += "hitunggt();";
                c.Controls.Add(t);
                r.Cells.Add(c);

                // Tanggal
                c = new HtmlTableCell();

                t = new TextBox();
                t.ID = "tgl_" + Convert.ToString(i);
                t.Width = 75;
                t.CssClass = "txt_center";
                t.Text = Cf.Day(rs.Rows[i]["TglJT"]);
                t.Attributes["style"] = "font:8pt";
                c.Controls.Add(t);

                l = new Label();
                l.Text = "&nbsp;";
                c.Controls.Add(l);

                bt = new HtmlButton();
                bt.InnerHtml = "&#xf073;";
                bt.Attributes["onclick"] = "openCalendar('" + t.ID + "')";
                bt.Attributes["class"] = "btn";
                bt.Attributes["style"] = "font-family: 'fontawesome'";

                c.Controls.Add(bt);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                CheckBox cb = new CheckBox();
                cb.Attributes.Add("onclick", "tagihan('bayar_" + i + "','" + Cf.Num(rs.Rows[i]["SisaTagihan"]) + "',this)");
                c.Controls.Add(cb);
                r.Cells.Add(c);


                c = new HtmlTableCell();
                c.Attributes.Add("style", "display:none;");
                c.InnerHtml = rs.Rows[i]["NoUrut"].ToString();
                c.ID = "NoTagihan_" + i;
                r.Cells.Add(c);

                list.Controls.Add(r);
            }

            //HtmlTableRow tr = new HtmlTableRow();
            //HtmlTableCell td = new HtmlTableCell();
            //td.InnerHtml = "<b>Grand Total</b>";
            //td.ColSpan = 5;
            //tr.Cells.Add(td);

            //td = new HtmlTableCell();
            //TextBox te = new TextBox();
            //te.ID = "totalBayar";
            //te.Attributes.Add("style", "text-align:right;");
            //te.Text = Cf.Num(total);
            //td.Controls.Add(te);
            //tr.Cells.Add(td);

            //td = new HtmlTableCell();
            //td.ColSpan = 2;
            //tr.Cells.Add(td);

            //list.Controls.Add(tr);
        }


        private bool validBayar
        {
            get
            {
                bool x = true;
                int i = 0;
                decimal total = 0;

                foreach (Control r in list.Controls)
                {
                    HtmlTableCell NoTagihan = (HtmlTableCell)list.FindControl("NoTagihan_" + i);
                    TextBox NilaiBayar = (TextBox)list.FindControl("bayar_" + i);

                    if (NoTagihan != null)
                    {
                        if (Cf.isMoney(NilaiBayar))
                        {
                            total += Convert.ToDecimal(NilaiBayar.Text);
                        }
                    }

                    i++;
                }

                string err = "";

                if (total > Convert.ToDecimal(lb.Text))
                {
                    err = "Pembayaran melebihi saldo lebih bayar.";
                    x = false;
                }
                if (total == 0)
                {
                    err = "Alokasi tidak bisa 0.";
                    x = false;
                }

                if (!x)
                    Js.Alert(this, "", err);

                return x;
            }
        }

        protected bool ValidNilai()
        {
            bool x = true;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TextBox lunas = (TextBox)list.FindControl("bayar_" + i);

                if (lunas.Text != "")
                {
                    decimal NilaiTagihan = Convert.ToDecimal(rs.Rows[i]["SisaTagihan"]);
                    decimal NilaiBayar = Convert.ToDecimal(lunas.Text);

                    if (NilaiBayar > NilaiTagihan)
                    {
                        x = false;
                        lunas.ForeColor = Color.Red;
                    }
                    else
                    {
                        lunas.ForeColor = Color.Black;
                    }
                }
            }

            if (!x)
            {
                Js.Alert(this, "Nilai Pembayaran Melebihi Tagihan!", "");
            }

            return x;
        }

        protected void SaveAlokasi_Click(object sender, EventArgs e)
        {
            if (validBayar && ValidNilai())
            {
                Table x = new Table();

                string query = " SELECT (a.Nokontrak + '.' + Cast(a.NoUrut AS VARCHAR)) AS NoTagihan, "
                            + "         a.NamaTagihan, "
                            + "         a.Tipe, "
                            + "         a.TglJT, "
                            + "         a.NoUrut, "
                            + "         (a.NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = a.NoKontrak AND NoTagihan = a.NoUrut)) AS SisaTagihan "
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a "
                            + " WHERE 1=1 "
                            + " AND a.Nokontrak = '" + NoKontrak + "'"
                            + " AND (a.NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = a.NoKontrak AND NoTagihan = a.NoUrut)) > 0 ";

                DataTable rs = Db.Rs(query);
                DateTime TglAlokasi = DateTime.Now;

                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                //Numerator
                string NoMEMO2 = Numerator.MEMO(TglAlokasi.Month, TglAlokasi.Year, Project);
                Db.Execute("EXEC spMEMORegistrasi"
                    + " '" + TglAlokasi + "'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'JUAL'"
                    + ",'" + NoKontrak + "'"
                    + ",'" + unit.Text + "'"
                    + ",'" + customer.Text + "'"
                    + ",'AL'"
                    + ",''"
                    + ",0"
                    );

                int noAlokasi = Db.SingleInteger("SELECT TOP 1 NoMEMO FROM MS_MEMO ORDER BY NoMEMO DESC");
                Db.Execute("UPDATE MS_MEMO SET NoMEMO2 = '" + NoMEMO2 + "', Project = '" + Project + "', NamaProject = '" + NamaProject + "' WHERE NoMEMO ='" + noAlokasi + "'");
                System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    TextBox lunas = (TextBox)list.FindControl("bayar_" + i);
                    TextBox tgl = (TextBox)list.FindControl("tgl_" + i);
                    HtmlTableCell nourut = (HtmlTableCell)list.FindControl("NoTagihan_" + i);

                    if (lunas != null && Cf.isMoney(lunas))
                    {
                        int NoTagihan = Convert.ToInt16(nourut.InnerHtml);
                        string NamaTagihan = Cf.Str(rs.Rows[i]["NamaTagihan"])
                            + " (" + rs.Rows[i]["Tipe"] + ")";
                        decimal Nilai = Convert.ToDecimal(lunas.Text);
                        DateTime Tgl = Convert.ToDateTime(tgl.Text);

                        Db.Execute("EXEC spMEMOAlokasi "
                            + "  '" + noAlokasi + "'"
                            + ", " + NoTagihan
                            + ", " + Nilai
                            );
                        alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");
                    }
                }

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    TextBox lunas = (TextBox)list.FindControl("bayar_" + i);
                    TextBox tgl = (TextBox)list.FindControl("tgl_" + i);

                    if (lunas != null && Cf.isMoney(lunas))
                    {
                        int NoTagihan = (int)rs.Rows[i]["NoUrut"];
                        string NamaTagihan = Cf.Str(rs.Rows[i]["NamaTagihan"])
                            + " (" + rs.Rows[i]["Tipe"] + ")";
                        decimal Nilai = Convert.ToDecimal(lunas.Text);
                        DateTime Tgl = Convert.ToDateTime(tgl.Text);
                        Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_PELUNASAN"
                             + " SET"
                             + " TglPelunasan ='" + Tgl + "'"
                             + " ,SudahCair = 1"
                             + " WHERE NoKontrak='" + NoKontrak + "' AND NoMemo='" + noAlokasi + "' AND NoTagihan='" + NoTagihan + "'"
                            );
                        Db.Execute("UPDATE MS_MEMO SET Status='POST', TglBKM=TglMemo WHERE NoMemo='" + noAlokasi + "'");
                    }
                }

                DataTable rsLog = Db.Rs("SELECT "
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
                    + " FROM MS_MEMO WHERE NoMEMO = " + noAlokasi);

                string KetLog = Cf.LogCapture(rsLog)
                    + "<br>***ALOKASI PEMBAYARAN:<br>"
                    + alokasi.ToString();

                Db.Execute("EXEC spLogMEMO"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + noAlokasi.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_MEMO_LOG ORDER BY LogID DESC");
                
                Db.Execute("UPDATE MS_MEMO_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Db.Execute("EXEC ISC064_MARKETINGJUAL..spProsentasePelunasan '" + NoKontrak + "'");

                Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_KONTRAK SET FlagMemo=1 WHERE NoKontrak='" + NoKontrak + "'");
                Response.Redirect("CbRegistrasi1.aspx?done=" + noAlokasi + "&memo=1");
            }
        }

    }
}
