using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ISC064.MARKETINGJUAL
{
    public partial class KomisiPengajuan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Js.Focus(this, keyword);
            Js.ConfirmKeyword(this, keyword);
            Fill();
            FeedBack();
        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["d"] != null)
                {
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Pengajuan Berhasil.."
                        ;
                }
            }
        }
        private bool valid()
        {
            bool x = true;

            if (dari.Text == "")
            {
                x = false;
            }
            if (sampai.Text == "")
            {
                x = false;
            }
            if (!x)
            {
                Js.Alert(
                    this
                    , "1. Mohon isi filter tanggal "
                    , ""
                    );

            }
            return x;
        }
        protected void Fill()
        {
            DataTable rs;

            list.Controls.Clear();

            if (dari.Text != "" && sampai.Text != "")
            {
                DateTime Dari = Convert.ToDateTime(dari.Text);
                DateTime Sampai = Convert.ToDateTime(sampai.Text);

                rs = Db.Rs("SELECT A.NoKontrak,A.NoUrut,B.NoUnit,C.Nama,B.PersenLunas,B.NilaiDPP,A.NamaKomisi,A.NilaiKomisi,A.SudahBayar, A.NamaPenerima, A.TermCair, A.NoUrut"
                           + " FROM MS_KOMISI A "
                           + " INNER JOIN MS_KONTRAK B ON A.NoKontrak = B.NoKontrak"
                           + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                           + " WHERE A.SudahBayar=0 "
                           + " AND B.TglKontrak >= '" + Dari + "' and B.TglKontrak <= '" + Sampai + "'"
                           + " AND B.NoKontrak+B.NoUnit+C.Nama LIKE '%" + Cf.Str(keyword.Text) + "%'"
                           );
            }
            else
            {
                rs = Db.Rs("SELECT A.NoKontrak,A.NoUrut,B.NoUnit,C.Nama,B.PersenLunas,B.NilaiDPP,A.NamaKomisi,A.NilaiKomisi,A.SudahBayar, A.NamaPenerima, A.TermCair, A.NoUrut"
                         + " FROM MS_KOMISI A "
                         + " INNER JOIN MS_KONTRAK B ON A.NoKontrak = B.NoKontrak"
                         + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                         + " WHERE A.SudahBayar=0 "
                         + " AND B.NoKontrak+B.NoUnit+C.Nama LIKE '%" + Cf.Str(keyword.Text) + "%'"
                         );
            }
            if (rs.Rows.Count == 0)
            {
                notfound.Visible = true;
            }
            else
            {
                notfound.Visible = false;
            }

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                TextBox t;
                HtmlTableCell c;
                HtmlTableRow r;
                CheckBox cb;

                r = new HtmlTableRow();
                list.Controls.Add(r);

                c = new HtmlTableCell();
                cb = new CheckBox();
                cb.ID = "cb_" + i;
                cb.Attributes.Add("title", rs.Rows[i]["NoKontrak"].ToString() + ";" + rs.Rows[i]["NoUrut"].ToString());
                if (Convert.ToDecimal(rs.Rows[i]["PersenLunas"]) < Convert.ToDecimal(rs.Rows[i]["TermCair"]))
                {
                    cb.Enabled = false;
                }
                c.Controls.Add(cb);
                c.NoWrap = true;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "nokontrak_" + i;
                l.Text = "<a href=\"javascript:popEditKontrak('" + rs.Rows[i]["NoKontrak"].ToString() + "')\">" + rs.Rows[i]["NoKontrak"].ToString() + "</a>";
                c.Controls.Add(l);
                c.NoWrap = true;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "customer_" + i;
                l.Text = rs.Rows[i]["Nama"].ToString();
                c.Controls.Add(l);
                c.NoWrap = true;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "unit_" + i;
                l.Text = rs.Rows[i]["NoUnit"].ToString();
                c.Controls.Add(l);
                c.NoWrap = true;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "dpp_" + i;
                l.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]));
                c.Controls.Add(l);
                c.NoWrap = true;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "persen_" + i;
                c.Align = "Right";
                l.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["PersenLunas"]));
                c.Controls.Add(l);
                c.NoWrap = true;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "namakomisi_" + i;
                l.Text = rs.Rows[i]["NamaKomisi"].ToString();
                c.Controls.Add(l);
                c.NoWrap = true;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "namapenerima_" + i;
                l.Text = rs.Rows[i]["NamaPenerima"].ToString();
                c.Controls.Add(l);
                c.NoWrap = true;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "cair_" + i;
                l.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["TermCair"]));
                c.Controls.Add(l);
                c.Align = "Right";
                c.NoWrap = true;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "nilai_" + i;
                l.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiKomisi"]));
                c.Controls.Add(l);
                c.Align = "Right";
                c.NoWrap = true;
                r.Cells.Add(c);
            }
        }
        protected void display_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                Fill();
            }
        }
        private bool valid2()
        {
            bool x = true;
            int index = 0;
            decimal check = 0;
            foreach (Control r in list.Controls)
            {
                CheckBox cb = (CheckBox)list.FindControl("cb_" + index);
                if (cb.Checked)
                {
                    check++;
                }
                index++;
            }
            if (check == 0)
            {
                x = false;
            }
            
            if (!x)
            {
                Js.Alert(
                    this
                    , "1. Belum ada komisi yang dipilih "
                    , ""
                    );

            }
            return x;
        }
        protected void save_Click(object sender, EventArgs e)
        {
            if (valid2())
            {
                Save();
                Response.Redirect("KomisiPengajuan.aspx?d=1");
            }
        }
        protected void Save()
        {
            int index = 0;
            foreach (Control r in list.Controls)
            {
                CheckBox cb = (CheckBox)list.FindControl("cb_" + index);
                if (cb.Checked)
                {
                    string[] a = Cf.SplitByString(cb.Attributes["title"].ToString(), ";");
                    int NoNota = Db.SingleInteger("SELECT ISNULL(MAX(NoNota), 0) FROM MS_KOMISI");
                    NoNota += 1;
                    DateTime TglBayar = DateTime.Today;

                    DataTable rsBef = Db.Rs("SELECT * FROM MS_KOMISI WHERE NoKontrak = '" + a[0] + "' AND NoUrut = " + a[1]);

                    string strSql = "UPDATE MS_KOMISI"
                        + " SET NoNota = " + NoNota
                        + ", TglBayar = '" + TglBayar + "'"
                        + ", NilaiBayar = NilaiKomisi"
                        + ", SudahBayar = 1"
                        + " WHERE NoKontrak = '" + a[0] + "'"
                        + " AND NoUrut = " + a[1]
                        ;
                    Db.Execute(strSql);

                    DataTable rsAft = Db.Rs("SELECT * FROM MS_KOMISI WHERE NoKontrak = '" + a[0] + "' AND NoUrut = " + a[1]);

                    string Ket = "";


                    Ket = "---BAYAR KOMISI---<br>"
                        + Cf.LogCapture(Db.Rs("SELECT * FROM MS_KOMISI WHERE NoKontrak = '" + a[0] + "' AND NoUrut = " + a[1]))
                        ;


                    Db.Execute("EXEC spLogKontrak"
                        + " 'EJK'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + a[0] + "'"
                        );
                }
                index++;
            }
        }
    }
}