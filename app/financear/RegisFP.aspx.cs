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

namespace ISC064.FINANCEAR
{
    public partial class RegisFP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                init();
            }

            Js.Focus(this, keyword);
            Js.ConfirmKeyword(this, keyword);

            FeedBack();

            Fill();
        }

        private void init()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                {
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        //+ "<a href=\"javascript:popEditTTS('"+Request.QueryString["done"]+"')\">"
                        + "Registrasi Berhasil.."
                        //+ "</a>"
                        ;
                }
            }
        }

        private void search_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Fill();
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(dari))
            {
                daric.Text = "Tanggal";
                if (s == "") s = dari.ID;
                x = false;
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                sampaic.Text = "Tanggal";
                if (s == "") s = sampai.ID;
                x = false;
            }
            else
                sampaic.Text = "";

            if (!Cf.isInt(jumlah) || Cf.isEmpty(jumlah))
            {
                jumlahc.Text = "jumlah";
                if (s == "") s = sampai.ID;
                x = false;
            }
            else
                jumlahc.Text = "";

            if (!x)
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");

            return x;
        }

        private bool validisi()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(nopt))
            {
                x = false;
                if (s == "") s = nopt.ID;
                noptc.Text = "Kosong";
            }
            else
                noptc.Text = "";

            if (Cf.isEmpty(tahunfp))
            {
                x = false;
                if (s == "") s = tahunfp.ID;
                tahunfpc.Text = "Kosong";
            }
            else
                tahunfpc.Text = "";

            if (Cf.isEmpty(nofp))
            {
                x = false;
                if (s == "") s = nofp.ID;
                nofpc.Text = "Kosong";
            }
            else
                nofpc.Text = "";

            if (!x)
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");

            return x;
        }

        private bool validfaktur
        {
            get
            {
                bool x = true;

                int startnumber = Convert.ToInt32(nofp.Text);
                string akhirNo = startnumber.ToString().PadLeft(8, '0');
                string nofaktur = "010." + nopt.Text.ToUpper() + "-" + tahunfp.Text + "." + akhirNo;

                int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_TTS WHERE NoFPS = '" + nofaktur + "'");
                x = (c > 0) ? false : x;

                if (!x) Js.Alert(this, "No. Faktur Pajak Duplikat", "");

                return x;
            }
        }

        private void Fill()
        {
            list.Controls.Clear();

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string strSql = "SELECT  a.*"
                + ",CASE a.CaraBayar"
                + "		WHEN 'TN' THEN 'TUNAI'"
                + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                + "		WHEN 'BG' THEN 'CEK GIRO'"
                + "		WHEN 'UJ' THEN 'UANG JAMINAN'"
                + "		WHEN 'DN' THEN 'DISKON'"
                + "		WHEN 'MB' THEN 'MERCHANT BANKING'"
                + " END AS CaraBayar2"
                + " FROM MS_TTS a"
                //				+ " INNER JOIN AM049_MARKETINGJUAL..MS_PELUNASAN b ON a.NoBKM = b.NoBKM"
                //				+ " LEFT JOIN AM049_MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND a.Ref = c.NoKontrak"
                + " WHERE a.Status = 'POST'"
                + " AND a.NoFPS = ''"
                + " AND CONVERT(varchar,a.TglTTS,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,a.TglTTS,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Ref + a.Unit + a.Customer LIKE '%" + Cf.Str(keyword.Text) + "%'"
                //				+ " AND c.Tipe != 'ADM'"
                + " ORDER BY a.TglTTS, a.NoTTS"
                ;

            DataTable rs = Db.Rs(strSql);
            int baris = 0;
            if (jumlah.Text != "" && Cf.isInt(jumlah))
            {
                if (rs.Rows.Count >= Convert.ToInt32(jumlah.Text))
                    baris = Convert.ToInt32(jumlah.Text);
                else
                    baris = rs.Rows.Count;
            }
            int counter = 0;
            for (int i = 0; i < baris; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow r;
                HtmlTableCell c;
                CheckBox cb;

                DataTable rstagihan = Db.Rs("SELECT NoTagihan FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoTTS = " + rs.Rows[i]["NoTTS"]);
                bool notADM = false;
                for (int j = 0; j < rstagihan.Rows.Count; j++)
                {
                    string tipetagihan = Db.SingleString("SELECT Tipe FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["Ref"] + "' AND NoUrut = '" + rstagihan.Rows[j]["NoTagihan"] + "'");
                    if (tipetagihan != "ADM")
                    {
                        notADM = true;
                        break;
                    }
                }

                if (notADM)
                {
                    r = new HtmlTableRow();
                    r.VAlign = "top";
                    list.Controls.Add(r);

                    cb = new CheckBox();
                    cb.ID = "tts_" + counter;

                    c = new HtmlTableCell();
                    c.InnerHtml = "&nbsp;";
                    c.ID = "pk_" + counter;
                    c.Attributes["title"] = rs.Rows[i]["NoTTS"].ToString();
                    c.Controls.Add(cb);
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    c.InnerHtml = "<a href=\"javascript:call('" + rs.Rows[i]["NoTTS"] + "')\">"
                        + rs.Rows[i]["NoTTS"].ToString().PadLeft(7, '0') + "</a>"
                        + "<br>BKM:" + rs.Rows[i]["NoBKM"];
                    r.Cells.Add(c);

                    string userid = rs.Rows[i]["UserID"].ToString();

                    c = new HtmlTableCell();
                    c.InnerHtml = Cf.Day(rs.Rows[i]["TglTTS"])
                        + "<br>" + userid;
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    c.InnerHtml = rs.Rows[i]["Tipe"] + " No. " + rs.Rows[i]["Ref"]
                        + "<br>" + rs.Rows[i]["Unit"]
                        + "<br>" + rs.Rows[i]["Customer"];
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    c.InnerHtml = rs.Rows[i]["Ket"].ToString();
                    if (rs.Rows[i]["Titip"].ToString() != "")
                        c.InnerHtml = c.InnerHtml + "<br>Pengelola : " + rs.Rows[i]["Titip"];
                    if (rs.Rows[i]["Tolak"].ToString() != "")
                        c.InnerHtml = c.InnerHtml + "<br>Tolakan : " + rs.Rows[i]["Tolak"];
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    c.InnerHtml = rs.Rows[i]["CaraBayar2"].ToString();
                    if (rs.Rows[i]["CaraBayar"].ToString() == "BG")
                        c.InnerHtml = c.InnerHtml
                            + "<br>" + rs.Rows[i]["NoBG"]
                            + "<br><font style='white-space:nowrap'>Tgl. BG : " + Cf.Day(rs.Rows[i]["TglBG"]) + "</font>";
                    r.Cells.Add(c);

                    decimal Total = Convert.ToDecimal(rs.Rows[i]["Total"]);
                    decimal DPP = Total / (decimal)1.1;

                    c = new HtmlTableCell();
                    c.InnerHtml = Cf.Num(DPP);
                    c.Align = "right";
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    c.InnerHtml = Cf.Num(Total - DPP);
                    c.Align = "right";
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    c.InnerHtml = Cf.Num(Total);
                    c.Align = "right";
                    r.Cells.Add(c);

                    counter++;
                }
            }
            jmlrow.Text = Cf.Num(counter);
        }

        private void proses(string PK, CheckBox cb, string noFaktur)
        {
            if (cb.Checked)
            {
                DataTable c = Db.Rs("SELECT * FROM MS_TTS WHERE NoTTS = '" + PK + "'");
                if (c.Rows.Count > 0)
                {
                    Db.Execute("UPDATE MS_TTS SET NoFPS = '" + noFaktur + "' WHERE NoTTS = '" + c.Rows[0]["NoTTS"] + "'");
                }
            }
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (validisi() && validfaktur)
            {
                int index = 0;
                int startnumber = Convert.ToInt32(nofp.Text);
                string nofaktur = "";
                string akhirNo = "";
                foreach (Control tr in list.Controls)
                {
                    HtmlTableCell c = (HtmlTableCell)list.FindControl("pk_" + index);
                    CheckBox cb = (CheckBox)list.FindControl("tts_" + index);

                    if (c != null)
                    {
                        akhirNo = startnumber.ToString().PadLeft(8, '0');
                        nofaktur = "010" + nopt.Text.ToUpper() + "-" + tahunfp.Text + akhirNo; //"010." + nopt.Text.ToUpper() + "-" + tahunfp.Text + "." + akhirNo;
                        proses(c.Attributes["title"], cb, nofaktur);
                        startnumber++;
                    }

                    index++;
                }

                Response.Redirect("RegisFP.aspx?done=1");
            }
        }
    }
}
