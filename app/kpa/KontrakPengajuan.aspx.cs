using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KPA
{
    public partial class KontrakPengajuan : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();


            Js.Focus(this, keyword);
            Js.ConfirmKeyword(this, keyword);

            FeedBack();
            Fill();
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);

                DataTable rs = Db.Rs("SELECT * FROM REF_RETENSI WHERE Project = '" + project.SelectedValue + "'");
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    tipe.Items.Add(new ListItem(rs.Rows[i]["NamaKategori"].ToString(), rs.Rows[i]["Kode"].ToString()));
                }
            }
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                {
                    transaksi.Visible = false;

                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditPengajuan('" + Request.QueryString["done"] + "')\">"
                        + "Registrasi Berhasil.."
                        + "</a>"
                        ;
                }
            }
        }

        protected void search_Click(object sender, System.EventArgs e)
        {
            list.Controls.Clear();
            Fill();
        }

        private void Fill()
        {
            string Tipe = "";
            if (tipe.SelectedIndex != 0)
            {
                Tipe = " AND Tipe ='" + tipe.SelectedValue.ToString() + "'";
            }

            string strSql = "SELECT * "
                + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN_KPA WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = MS_TAGIHAN.NoKontrak) ) AS SisaTagihan"
                + " FROM MS_TAGIHAN_KPA AS MS_TAGIHAN "
                + " INNER JOIN MS_KONTRAK AS MS_KONTRAK ON MS_TAGIHAN.NoKontrak = MS_KONTRAK.NoKontrak"
                + " INNER JOIN MS_CUSTOMER AS MS_CUSTOMER ON MS_CUSTOMER.NoCustomer = MS_KONTRAK.NoCustomer"
                + " WHERE MS_KONTRAK.NoKontrak + MS_KONTRAK.NoUnit + MS_CUSTOMER.Nama"
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND FlagPengajuanKPA=0"
                + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN_KPA WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = MS_TAGIHAN.NoKontrak) ) > 0"
                + " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'"
                + Tipe
                + " ORDER BY TglJT, NoUrut";
            
            DataTable rsTagihan = Db.Rs(strSql);

            if (rsTagihan.Rows.Count == 0)
            {
                tb_fill.Visible = false;
            }
            else
            {
                tb_fill.Visible = true;
                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    Label l;
                    TextBox t;
                    HtmlTableCell c;
                    HtmlTableRow r;

                    r = new HtmlTableRow();
                    list.Controls.Add(r);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.ID = "nokontrak_" + i;
                    l.Text = rsTagihan.Rows[i]["NoKontrak"].ToString();
                    c.Controls.Add(l);
                    c.NoWrap = true;
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.ID = "notagihan_" + i;
                    l.Text = rsTagihan.Rows[i]["NoUrut"].ToString();
                    c.Controls.Add(l);
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.ID = "nama_" + i;
                    l.Text = rsTagihan.Rows[i]["Nama"].ToString();
                    c.Controls.Add(l);
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.ID = "nounit_" + i;
                    l.Text = rsTagihan.Rows[i]["NoUnit"].ToString();
                    c.Controls.Add(l);
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.ID = "namatagihan_" + i;
                    l.Text = rsTagihan.Rows[i]["NamaTagihan"].ToString();
                    c.Controls.Add(l);
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.ID = "tipe_" + i;
                    l.Text = rsTagihan.Rows[i]["Tipe"].ToString();
                    c.Controls.Add(l);
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.ID = "tgljt_" + i;
                    l.Text = Cf.Day(rsTagihan.Rows[i]["TglJT"]);
                    c.Controls.Add(l);
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.ID = "sisatagihan_" + i;
                    l.Text = Cf.Num(rsTagihan.Rows[i]["SisaTagihan"]);
                    c.Controls.Add(l);
                    c.Align = "Right";
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    t = new TextBox();
                    t.ID = "lunas_" + i;
                    t.Width = 150;
                    t.CssClass = "txt_num";
                    t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                    t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                    t.Attributes["onblur"] = "CalcBlur(this);hitunggt();";
                    c.Controls.Add(t);
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.Text = ""
                        + "<input type='checkbox' onclick=\"tagihan('" + i + "','" + Cf.Num(rsTagihan.Rows[i]["SisaTagihan"]) + "',this)\">"
                        + "</tr>";
                    c.Controls.Add(l);
                    r.Cells.Add(c);
                }
            }
        }
        private bool valid()
        {
            bool x = true;
            if (tglform.Text == "")
            {
                x = false;
                tglformc.Text = "Kosong";
            }
            else if (!Cf.isTgl(tglform))
            {
                x = false;
                tglformc.Text = "Format Tanggal";
            }
            else
            {
                tglformc.Text = "";
            }

            if (tglcair.Text == "")
            {
                x = false;
                tglcairc.Text = "Kosong";
            }
            else if (!Cf.isTgl(tglcair))
            {
                x = false;
                tglcairc.Text = "Format Tanggal";
            }
            else
            {
                tglcairc.Text = "";
            }

            if (total.Text == "" || total.Text == "0")
            {
                x = false;
                err.Text = "Belum ada tagihan yang diajukan";
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "1. Cek kembali field field di atas /n"
                    , "2. Belum ada tagihan yang diisi pengajuannya");

            }

            return x;

        }
        protected void save_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                //save pengajuan

                Db.Execute("EXEC ISC064_FINANCEAR..spPengajuanRegistrasi"
                        + " '" + Convert.ToDateTime(tglform.Text) + "'"
                        + ",'" + Convert.ToDateTime(tglcair.Text) + "'"
                        + ",'" + keterangan.Text + "'"
                        + "," + Convert.ToDecimal(total.Text)
                        + ",'" + DateTime.Now + "'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                    );
                int No = Db.SingleInteger("SELECT TOP 1 NoPengajuan FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA ORDER BY NoPengajuan DESC");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA SET Project = '" + project.SelectedValue + "' WHERE NoPengajuan  = " + No);
                //detil
                System.Text.StringBuilder alokasi = new System.Text.StringBuilder();
                int NoPengajuan = Db.SingleInteger("SELECT MAX(NoPengajuan) FROM ISC064_FINANCEAR..MS_PENGAJUAN_KPA");
                Db.Execute("UPDATE ISC064_FINANCEAR..MS_PENGAJUAN_KPA SET NoSurat='" + nosurat.Text + "' WHERE NoPengajuan = " + NoPengajuan);

                int index = 0;
                foreach (Control r in list.Controls)
                {
                    Label nokontrak = (Label)list.FindControl("nokontrak_" + index);
                    Label nama = (Label)list.FindControl("nama_" + index);
                    Label nounit = (Label)list.FindControl("nounit_" + index);
                    Label notagihan = (Label)list.FindControl("notagihan_" + index);
                    Label namatagihan = (Label)list.FindControl("namatagihan_" + index);
                    TextBox lunas = (TextBox)list.FindControl("lunas_" + index);

                    if (lunas.Text != "")
                    {
                        Db.Execute("EXEC ISC064_FINANCEAR..spPengajuanDetil"
                        + " '" + NoPengajuan + "'"
                        + ",'" + nokontrak.Text + "'"
                        + ",'" + nama.Text + "'"
                        + ",'" + nounit.Text + "'"
                        + "," + Convert.ToInt32(notagihan.Text)
                        + ",'" + namatagihan.Text + "'"
                        + "," + Convert.ToDecimal(lunas.Text)
                        );
                        


                        Db.Execute("UPDATE MS_TAGIHAN_KPA SET FlagPengajuanKPA=1 WHERE NoKontrak='" + nokontrak.Text + "' AND NoUrut='" + Convert.ToInt32(notagihan.Text) + "'");

                        alokasi.Append(nokontrak.Text + "." + notagihan.Text + "    " + namatagihan.Text + "    " + Cf.Num(Convert.ToDecimal(lunas.Text)) + "<br>");
                    }

                    index++;
                }

                //log
                DataTable rs = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglInput, 106) AS [Tanggal]"
                    + ",NoPengajuan"
                    + ",NoSurat"
                    + ",CONVERT(varchar, TglFormulir, 106) AS [Tanggal Formulir]"
                    + ",CONVERT(varchar, TglRencanaCair, 106) AS [Tanggal Rencana Cair]"
                    + ",Total"
                    + ",Keterangan"
                    + " FROM ISC064_FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = " + NoPengajuan);

                string KetLog = Cf.LogCapture(rs)
                    + "<br>***PENGAJUAN TAGIHAN:<br>"
                    + alokasi.ToString();

                Db.Execute("EXEC ISC064_FINANCEAR..spLogPengajuanKPA"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + NoPengajuan + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                Response.Redirect("KontrakPengajuan.aspx?done=" + NoPengajuan);
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


        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipe.Items.Clear();

            if (project.SelectedIndex != 0)
            {
                tipe.Items.Add(new ListItem("Kategori Retensi :"));
                DataTable rs = Db.Rs("SELECT * FROM REF_RETENSI WHERE Project = '" + project.SelectedValue + "'");
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    tipe.Items.Add(new ListItem(rs.Rows[i]["NamaKategori"].ToString(), rs.Rows[i]["Kode"].ToString()));
                }
            }
            else
            {
                tb_fill.Visible = false;
                tipe.Items.Add(new ListItem("Kategori Retensi :"));
            }
        }
    }
}
