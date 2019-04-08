using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Drawing;

namespace ISC064.MARKETINGJUAL
{
    public partial class ClosingLangsungGimmick : System.Web.UI.Page
    {
        protected short baris
        {
            get { return Convert.ToInt16(Session["ClosingLangsungGimmick"]); }
            set { Session["ClosingLangsungGimmick"] = value; }
        }
        protected void add_Click(object sender, EventArgs e)
        {
            baris++;
            tambahbaris(baris);
            add.Focus();
            reload();
        }
        protected void fill()
        {
            DataTable rs = Db.Rs("SELECT a.NoKontrak,a.TglKontrak,a.NilaiPPN,a.NilaiDPP,a.NilaiKontrak,a.Carabayar,b.Nama as CS,c.Nama as AG FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b on a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_AGENT c on a.NoAgent = c.NoAgent"
                + " WHERE a.NoKontrak = '" + NoKontrak + "'");
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nokon.Text = NoKontrak;
                tglkontrak.Text = Cf.Day(rs.Rows[0]["TglKontrak"]);
                cs.Text = rs.Rows[0]["CS"].ToString();
                ag.Text = rs.Rows[0]["AG"].ToString();
                nilaikon.Text = Cf.Num(rs.Rows[0]["NilaiKontrak"]);
                Ndpp.Text = Cf.Num(rs.Rows[0]["NilaiDPP"]);
                Nppn.Text = Cf.Num(rs.Rows[0]["NilaiPPN"]);
                carabayar.Text = rs.Rows[0]["Carabayar"].ToString();
                sum.Text = Sumber.ToString();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                fill();
                baris = 5;
            }

            fillbaris();
        }

        protected void tambahbaris(short i)
        {
            HtmlTableRow tr;
            HtmlTableCell c;
            TextBox tb;
            TextBox tb2;
            TextBox tb4;
            TextBox tb5;
            TextBox tb6;
            TextBox tb7;
            TextBox tb8;
            HtmlInputButton btn;
            Label l;

            tr = new HtmlTableRow();
            tr.VAlign = "top";
            list.Controls.Add(tr);

            c = new HtmlTableCell();
            c.InnerHtml = i.ToString() + ".";
            tr.Cells.Add(c);

            tb = new TextBox();
            tb.ID = "kodebr_" + i;
            tb.Width = 120;

            tb8 = new TextBox();
            tb8.ID = "kodebr2_" + i;
            tb8.Attributes["style"] = "display:none";

            btn = new HtmlInputButton();
            btn.ID = "btnacc_" + i;
            btn.Value = "...";
            btn.Attributes["class"] = "search";

            tb2 = new TextBox();
            tb2.ID = "namabr_" + i;
            tb2.Width = 200;
            tb2.ReadOnly = true;

            tb4 = new TextBox();
            tb4.ID = "satuanbr_" + i;
            tb4.Width = 70;
            tb4.ReadOnly = true;

            tb5 = new TextBox();
            tb5.ID = "hrgabr_" + i;
            Js.NumberFormat(tb5);
            tb5.Width = 140;
            tb5.ReadOnly = true;

            tb6 = new TextBox();
            tb6.ID = "qtybr_" + i;
            tb6.Text = "0";
            tb6.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            tb6.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            tb6.Attributes["onblur"] = "CalcBlur(this);hitungaja('" + i + "');";
            tb6.Width = 50;

            tb7 = new TextBox();
            tb7.ID = "totalbr_" + i;
            tb7.Width = 140;
            tb7.ReadOnly = true;

            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            tb.Attributes["ondblclick"] = btn.Attributes["onclick"] =
                "callgimmick('" + tb.ID + "','" + tb2.ID + "','" + tb4.ID + "','" + tb5.ID + "','" + tb8.ID + "','" + Project + "')";

            c = new HtmlTableCell();
            c.Controls.Add(tb);
            c.Controls.Add(tb8);
            c.Controls.Add(btn);
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Controls.Add(tb2);
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Controls.Add(tb4);
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Controls.Add(tb5);
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Controls.Add(tb6);
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Controls.Add(tb7);
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            l = new Label();
            l.Text = "<i class='fa fa-trash'></i>";
            l.CssClass = "btn btn-cal";
            StringBuilder x = new StringBuilder();
            x.Append("ClearGimmick('kodebr_" + i + "','kodebr2_" + i + "','namabr_" + i + "','satuanbr_" + i + "','hrgabr_" + i + "','qtybr_" + i + "','totalbr_" + i + "');");
            l.Attributes["onclick"] = x.ToString();
            c.Controls.Add(l);
            tr.Cells.Add(c);
        }

        protected void fillbaris()
        {
            for (short i = 1; i <= baris; i++)
            {
                if (!Response.IsClientConnected) break;
                tambahbaris(i);
            }
        }

        protected void reload()
        {
            StringBuilder js = new StringBuilder();
            js.Append("<script language=\"javascript\">");

            decimal TotalSemua = 0;
            for (short i = 1; i <= baris; i++)
            {
                if (!Response.IsClientConnected) break;

                TextBox itemid = (TextBox)list.FindControl("kodebr_" + i);
                TextBox itemid2 = (TextBox)list.FindControl("kodebr2_" + i);
                TextBox nama = (TextBox)list.FindControl("namabr_" + i);
                //TextBox tipe = (TextBox)list.FindControl("tipebr_" + i);
                TextBox satuan = (TextBox)list.FindControl("satuanbr_" + i);
                TextBox harga = (TextBox)list.FindControl("hrgabr_" + i);
                TextBox Qty = (TextBox)list.FindControl("qtybr_" + i);
                TextBox Total = (TextBox)list.FindControl("totalbr_" + i);

                if (itemid2.Text != "")
                {
                    DataTable rk = Db.Rs("SELECT * FROM MS_GIMMICK WHERE ItemID = '" + itemid2.Text + "'");

                    itemid.Text = rk.Rows[0]["ItemID"].ToString() == null ? "" : rk.Rows[0]["ItemID"].ToString();
                    nama.Text = rk.Rows[0]["Nama"].ToString() == null ? "" : rk.Rows[0]["Nama"].ToString();
                    string TipeBr = Db.SingleString("SELECT Nama FROM REF_TIPE_GIMMICK WHERE ID = '" + rk.Rows[0]["Tipe"].ToString() + "'");
                    //tipe.Text = TipeBr == null ? "" : TipeBr;
                    satuan.Text = rk.Rows[0]["satuan"].ToString() == null ? "" : rk.Rows[0]["satuan"].ToString();
                    harga.Text = rk.Rows[0]["HargaSatuan"] == null ? "" : Cf.Num(rk.Rows[0]["HargaSatuan"]);

                    decimal hasil = Math.Round(Convert.ToDecimal(harga.Text) * Convert.ToDecimal(Qty.Text));
                    Total.Text = Cf.Num(hasil);
                    TotalSemua += hasil;
                }

                total.Text = Cf.Num(TotalSemua);
            }

            js.Append("</script>");
            Page.ClientScript.RegisterStartupScript(GetType(), "reloadScript", js.ToString());
        }

        protected void skip_Click(object sender, EventArgs e)
        {
            if(Sumber == "APPROVE")
            {
                Response.Redirect("ClosingLangsungApprov.aspx?NoKontrak=" + NoKontrak);
            }
            else
            {
                Response.Redirect("TabelStok4.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS);
            }
        }

        private bool validstock()
        {
            reload();
            bool x = true;

            //StringBuilder err = new StringBuilder();
            //for (int i = 1; i <= baris; i++)
            //{
            //    if (!Response.IsClientConnected) break;

            //    TextBox Kode = (TextBox)list.FindControl("kodebr_" + i);
            //    TextBox Kode2 = (TextBox)list.FindControl("kodebr2_" + i);
            //    TextBox Qty = (TextBox)list.FindControl("qtybr_" + i);

            //    //qty
            //    if (Qty.Text == "")
            //    {
            //        x = false;
            //        Cf.MarkError(Qty);
            //        err.Append("Baris ke-" + i + " Quantity barang tidak boleh kosong \\n");
            //    }

            //    if (Kode2.Text != "")
            //    {
            //        if(Convert.ToDecimal(Qty.Text) != 0 || Qty.Text != "")
            //        {
            //            decimal TotalStock = Db.SingleDecimal("SELECT ISNULL(SUM(Stock),0) FROM MS_KONTRAK_GIMMICK WHERE ITEMID = '" + Kode2.Text + "'");
            //            decimal StockAda = Db.SingleDecimal("SELECT ISNULL(Stock,0) FROM MS_GIMMICK WHERE ITEMID = '" + Kode2.Text + "'");
            //            decimal TotStock = (TotalStock + Convert.ToDecimal(Qty.Text));

            //            if (TotStock > StockAda)
            //            {
            //                x = false;
            //                Cf.MarkError(Qty);
            //                err.Append("Baris ke-" + i + " Request Barang melebihi dari Stock\\n");
            //            }
            //        }
            //    }
            //}

            //if (!x) Js.Alert(this, err.ToString(), "");

            return x;
        }

        protected void save_Click(object sender, EventArgs e)
        {
            //if (validstock())
            //{
                for (int i = 1; i < baris; i++)
                {
                    TextBox Kode2 = (TextBox)list.FindControl("kodebr2_" + i);
                    TextBox Nama = (TextBox)list.FindControl("namabr_" + i);
                    //TextBox Tipe = (TextBox)list.FindControl("tipebr_" + i);
                    TextBox Satuan = (TextBox)list.FindControl("satuanbr_" + i);
                    TextBox Harga = (TextBox)list.FindControl("hrgabr_" + i);
                    TextBox Qty = (TextBox)list.FindControl("qtybr_" + i);
                    TextBox Total = (TextBox)list.FindControl("totalbr_" + i);

                    string Kode = Kode2.Text;
                    decimal Stock = Convert.ToDecimal(Qty.Text);
                    string NamaBarang = Cf.Str(Nama.Text);
                    string TipeBarang = Db.SingleString("select ISNULL(Tipe,'') from MS_KONTRAK_GIMMICK where ItemID = '" + Kode + "'");
                    string SatuanBarang = Cf.Str(Satuan.Text);
                    string HargaSatuan = Harga.Text.Replace(",", "");
                    string TotalHarga = Total.Text.Replace(",", "");
                   
                    if (Kode2.Text != "")
                    {
                        if(Stock != 0 || Qty.Text != "")
                        {
                            Db.Execute("EXEC spKontrakGimmick"
                                    + " '" + NoKontrak + "'"
                                    + ",'" + i + "'"
                                    + ",'" + Kode + "'"
                                    + ",'" + NamaBarang + "'"
                                    + ",'" + TipeBarang + "'"
                                    //+ ",'" + Db.SingleString("select ISNULL(,'') from REF_TIPE_GIMMICK where ID = '" + TipeBarang +"'") + "'"
                                    + ",'" + SatuanBarang + "'"
                                    + ",'" + Stock + "'"
                                    + ",'" + Convert.ToDecimal(HargaSatuan) + "'"
                                    + ",'" + Convert.ToDecimal(TotalHarga) + "'");
                        }
                    }
                }

                #region
                //ini buat log -- kalo gak ada.. dia gak bikin
                int countGimmick = Db.SingleInteger("select count(*) from MS_KONTRAK_GIMMICK where NoKontrak = '" + NoKontrak + "'");
                if (countGimmick != 0)
                {
                    //kalo ambil data-nya di TOP 1 aja
                    Db.Execute("UPDATE MS_KONTRAK_GIMMICk SET Catatan = '" + catatan.Text + "' WHERE NoKontrak  = '" + NoKontrak + "'");

                    string LogCatatan = Db.SingleString("select TOP 1 ISNULL(Catatan, '') from MS_KONTRAK_GIMMICK where NoKontrak = '" + NoKontrak + "'");

                    DataTable rsgimmick = Db.Rs("SELECT "
                        + "CONVERT(VARCHAR,SN) + '. ' + Nama + ' ('+Tipe+') '"
                        + " + Satuan + ' Stock Permintaan : ' + CONVERT(VARCHAR,Stock,1) + ' '"
                        + " + CONVERT(VARCHAR,HargaSatuan,1) +  ' Total : ' + CONVERT(VARCHAR,HargaTotal,1) "
                        + " FROM MS_KONTRAK_GIMMICK WHERE NoKontrak = '" + NoKontrak + "' Order By SN");

                    //Logfile
                    string Ket = Cf.LogList(rsgimmick, LogCatatan);

                    Db.Execute("EXEC spLogKontrak"
                        + " 'D-GIM'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);
                }
                #endregion

                if (Sumber == "APPROVE")
                {
                    Response.Redirect("ClosingLangsungApprov.aspx?NoKontrak=" + NoKontrak);
                }
                else
                {
                    Response.Redirect("TabelStok4.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS);
                }
            //}
        }
        private string NoKontrak
        {
            get
            {
                return Request.QueryString["NoKontrak"];
            }
        }
        private string NoTTS
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoTTS"]);
            }
        }
        private string Sumber
        {
            get
            {
                return Cf.Pk(Request.QueryString["Sumber"]);
            }
        }
    }
}