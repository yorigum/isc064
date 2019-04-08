using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ISC064.financear
{
    public partial class VAUpload2 : System.Web.UI.Page
    {
        protected DataTable rsTagihan = new DataTable();
        protected int index = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!IsPostBack)
            {
                div1.Visible = true;
                div2.Visible = false;
            }

            Init();

            Js.Confirm(this, "Lanjutkan proses upload transaksi virtual account?");
        }
        protected void Init()
        {
            if (File.Exists(FilePath))
            {
                StreamReader sr = null;
                string line;
                try
                {
                    sr = new StreamReader(FilePath);
                    int row = 1, x = 1;
                    while (sr.Peek() != -1)
                    {
                        line = sr.ReadLine() + "\r\n";
                        string[] hasil = Regex.Split(line, @"\s{1,}");

                        string NoVA = "";
                        decimal Nilai = 0;
                        DateTime Tgl = new DateTime();
                        int index = 0;

                        try
                        {
                            NoVA = hasil[4];
                            Nilai = Convert.ToDecimal(hasil[5], CultureInfo.CreateSpecificCulture("id-ID").NumberFormat);
                            //Tgl = Convert.ToDateTime(hasil[1], CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat);
                            Tgl = new DateTime(Convert.ToInt32(hasil[1].Substring(0, 4)), Convert.ToInt32(hasil[1].Substring(4, 2)), Convert.ToInt32(hasil[1].Substring(6, 2)));
                        }
                        catch { }

                        DataTable rs = Db.Rs("SELECT a.*, b.NoKontrak, b.NoUnit"
                            + ", (SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = b.NoCustomer) AS Cs"
                            + " FROM REF_VA a"
                            + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.NoVA = b.NoVA"
                            + " WHERE a.NoVA = '" + NoVA + "'");
                        if (rs.Rows.Count > 0)
                        {
                            FillTb(rs.Rows[0]["NoKontrak"].ToString(), rs.Rows[0]["Cs"].ToString(), rs.Rows[0]["NoUnit"].ToString(),
                                NoVA, Tgl, Nilai, x);
                        }

                        x++;
                    }
                }
                finally
                {
                    if (sr != null)
                        sr.Close();
                }
            }
        }
        protected void FillTb(string NoKontrak, string Cs, string NoUnit, string NoVA, DateTime Tgl, decimal Nilai, int n)
        {
            decimal x = Nilai;
            int j = 0;

            string strSql = "SELECT * "
                + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + NoKontrak + "') ) AS SisaTagihan"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'"
                + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + NoKontrak + "') ) > 0"
                + " ORDER BY TglJT, NoUrut";
            DataTable rs = Db.Rs(strSql);
            rsTagihan.Merge(rs);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                if (i == 0) j = index;

                Label l;
                TextBox t;

                l = new Label();
                string s = "";
                if (i == 0)
                    s = "<td align=center>" + n + "</td>"
                        + "<td>" + NoVA + "</td>"
                        + "<td>" + Cf.Day(Tgl) + "</td>"
                        + "<td>" + NoKontrak + "</td>"
                        + "<td>" + Cs + "</td>"
                        + "<td>" + NoUnit + "</td>"
                        + "<td align=right>" + Cf.Num(Nilai) + "</td>";
                else
                    s = "<td colspan=7 />";
                l.Text = "<tr valign=top>"
                    + s
                    + "<td>" + rs.Rows[i]["NamaTagihan"] + "</td>"
                    + "<td>" + rs.Rows[i]["Tipe"] + "</td>"
                    + "<td style='white-space:nowrap'>" + Cf.Day(rs.Rows[i]["TglJT"]) + "</td>"
                    + "<td align=right>" + Cf.Num(rs.Rows[i]["SisaTagihan"]) + "</td>"
                    + "<td>"
                    ;
                ph.Controls.Add(l);

                t = new TextBox();
                t.ID = "lunas_" + index;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "va_" + index;
                t.Visible = false;
                t.Text = NoVA;
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "tgl_" + index;
                t.Visible = false;
                t.Text = Cf.Day(Tgl);
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "ref_" + index;
                t.Visible = false;
                t.Text = NoKontrak;
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "cs_" + index;
                t.Visible = false;
                t.Text = Cs;
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "unit_" + index;
                t.Visible = false;
                t.Text = NoUnit;
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "tag_" + index;
                t.Visible = false;
                t.Text = rs.Rows[i]["NoUrut"]
                    + ";" + rs.Rows[i]["NamaTagihan"]
                    + ";" + rs.Rows[i]["Tipe"];
                ph.Controls.Add(t);

                l = new Label();
                l.Text = "</td>"
                    + "<td><input type='checkbox' onclick=\"tagihan('" + index + "','" + Cf.Num(rs.Rows[i]["SisaTagihan"]) + "', this)\"></td>"
                    + "</tr>";
                ph.Controls.Add(l);

                index++;
            }

            //Alokasi
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TextBox lunas = (TextBox)ph.FindControl("lunas_" + j);
                decimal SisaTagihan = (decimal)rs.Rows[i]["SisaTagihan"];

                if (i == rs.Rows.Count - 1)
                    lunas.Text = Cf.Num(x);
                else
                {
                    if (SisaTagihan >= x)
                    {
                        lunas.Text = Cf.Num(x);
                        break;
                    }
                    else
                        lunas.Text = Cf.Num(SisaTagihan);
                }
                x -= SisaTagihan;
                j++;
            }
        }
        protected bool datavalid()
        {
            bool x = true;
            string s = "";

            bool adasatu = false;
            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                TextBox lunas = (TextBox)ph.FindControl("lunas_" + i);
                if (lunas.Text != "")
                {
                    adasatu = true;
                    try
                    {
                        decimal z = Convert.ToDecimal(lunas.Text);
                    }
                    catch
                    {
                        x = false;
                        if (s == "") s = lunas.ID;
                    }
                }
            }

            if (!adasatu) x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Pembayaran harus berupa angka dan minimal untuk satu tagihan.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    );

            return x;
        }
        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    TextBox lunas = (TextBox)ph.FindControl("lunas_" + i);
                    TextBox va = (TextBox)ph.FindControl("va_" + i);
                    TextBox tgl = (TextBox)ph.FindControl("tgl_" + i);
                    TextBox nokontrak = (TextBox)ph.FindControl("ref_" + i);
                    TextBox cs = (TextBox)ph.FindControl("cs_" + i);
                    TextBox unit = (TextBox)ph.FindControl("unit_" + i);
                    TextBox tag = (TextBox)ph.FindControl("tag_" + i);

                    if (lunas.Text != "")
                    {
                        Db.Execute("EXEC spTTSRegistrasi"
                            + " '" + Convert.ToDateTime(tgl.Text) + "'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'JUAL'"
                            + ",'" + Cf.Str(nokontrak.Text) + "'"
                            + ",'" + Cf.Str(unit.Text) + "'"
                            + ",'" + Cf.Str(cs.Text) + "'"
                            + ",'TR'"
                            + ",'VA: " + Cf.Str(va.Text) + "'"
                            );

                        int NoTTS = Db.SingleInteger("SELECT TOP 1 NoTTS FROM MS_TTS ORDER BY NoTTS DESC");
                        Db.Execute("UPDATE MS_TTS"
                            + " SET Acc = '" + Bank + "'"
                            + " WHERE NoTTS = " + NoTTS
                            );

                        string[] arr = tag.Text.Split(';');
                        Db.Execute("EXEC spTTSAlokasi "
                            + "  " + NoTTS
                            + ", " + arr[0]
                            + ", " + Convert.ToDecimal(lunas.Text)
                            );

                        Db.Execute("EXEC spPostingTTS " + NoTTS + ", '" + Convert.ToDateTime(tgl.Text) + "'");
                        Db.Execute("UPDATE MS_TTS"
                            + " SET ManualBKM = ManualTTS"
                            + ", TanggalUangDiterima = TglBKM"
                            + " WHERE NoTTS = " + NoTTS);

                        //Ambil Stok No. FP
                        DataTable fp = Db.Rs("SELECT * FROM REF_FP WHERE Status = 0");
                        if (fp.Rows.Count > 0)
                        {
                            Db.Execute("UPDATE MS_TTS SET"
                                + " NoFPS = '" + fp.Rows[0]["NoFPS"].ToString() + "'"
                                + " WHERE NoTTS = " + NoTTS);

                            Db.Execute("UPDATE REF_FP SET"
                                + " Status = 1"
                                + " WHERE NoFPS = '" + fp.Rows[0]["NoFPS"].ToString() + "'");
                        }

                        int NoBKM = Db.SingleInteger("SELECT NoBKM FROM MS_TTS WHERE NoTTS = " + NoTTS);

                        DataTable rsLog = Db.Rs("SELECT "
                            + " CONVERT(varchar, TglTTS, 106) AS [Tanggal]"
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
                            + " FROM MS_TTS WHERE NoTTS = " + NoTTS);

                        string KetLog = Cf.LogCapture(rsLog)
                            + "<br>***ALOKASI PEMBAYARAN:<br>"
                            + arr[1] + " (" + arr[2] + ")";

                        Db.Execute("EXEC spLogTTS"
                            + " 'VA'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + KetLog + "'"
                            + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                            );

                        decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
                        string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                        Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                        //		Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spDenda");

                        //Result
                        TableRow r = new TableRow();
                        TableCell c;

                        c = new TableCell();
                        c.Text = va.Text;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = Cf.Day(tgl.Text);
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = "<a href=\"javascript:call('" + NoTTS + "')\">"
                            + NoTTS.ToString().PadLeft(7, '0') + "</a>"
                            + "<br /><i>POST</i>"
                            + "<br />BKM: " + NoBKM;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = "JUAL No. " + nokontrak.Text
                            + "<br />" + unit.Text
                            + "<br />" + cs.Text;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = Cf.Num(lunas.Text);
                        c.HorizontalAlign = HorizontalAlign.Right;
                        r.Cells.Add(c);

                        Rpt.Border(r);
                        rpt.Rows.Add(r);
                    }
                }

                div1.Visible = false;
                div2.Visible = true;
                feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                    + "Upload Berhasil.."
                    ;

                Dfc.DeleteFile(FilePath);
            }
        }
        protected string FilePath
        {
            get
            {
                return Request.QueryString["path"];
            }
        }
        protected string Bank
        {
            get
            {
                return Request.QueryString["bank"];
            }
        }
        protected string Project
        {
            get
            {
                return Request.QueryString["project"];
            }
        }
    }
}
