using System;
using System.Web.UI;
using System.Drawing;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakGimmick : System.Web.UI.Page
    {
        private string NoKontrak { get { return Cf.Pk(Request.QueryString["NoKontrak"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                fill();
                
            }
            else
            {
                
            }
            FeedBack();
            filldetil();
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

                catatan.Text = Db.SingleString("select TOP 1 ISNULL(Catatan, '') from MS_KONTRAK_GIMMICK where NoKontrak = '" + NoKontrak + "'");

                int countTglDiterima = Db.SingleInteger("select count(*) from ms_kontrak_gimmick where TglDiterima is not null and NoKontrak = '" + NoKontrak + "'");
                if(countTglDiterima != 0)
                {
                    tglditerima.Text = Cf.Day(Db.SingleTime("select TOP 1 ISNULL(TglDiterima, '') from MS_KONTRAK_GIMMICK where NoKontrak = '" + NoKontrak + "'"));
                }

                printGimmick.InnerHtml = printGimmick.InnerHtml;
            }
        }
        protected void filldetil()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_KONTRAK_GIMMICK WHERE NoKontrak = '" + NoKontrak + "' Order By SN ASC");

            decimal tot = 0;
            for (int i = 0; i < rs.Rows.Count;i++)
            {
                HtmlTableRow tr;
                HtmlTableCell c;
                TextBox tb;
                TextBox tb2;
                TextBox tb4;
                TextBox tb5;
                TextBox tb6;
                TextBox tb7;
                LinkButton lbdel;
                CheckBox cb;
                HtmlInputButton btn;

                tr = new HtmlTableRow();
                tr.VAlign = "top";
                list.Controls.Add(tr);

                c = new HtmlTableCell();
                c.InnerHtml = (i+1).ToString() + ".";
                tr.Cells.Add(c);

                tb = new TextBox();
                tb.ID = "kodebr_" + i;
                tb.Text = rs.Rows[i]["ItemID"].ToString();
                tb.Width = 100;

                btn = new HtmlInputButton();
                btn.ID = "btnacc_" + i;
                btn.Value = "...";
                btn.Attributes["class"] = "search";

                tb2 = new TextBox();
                tb2.ID = "namabr_" + i;
                tb2.Text = rs.Rows[i]["Nama"].ToString();
                tb2.Width = 200;
                tb2.ReadOnly = true;

                tb4 = new TextBox();
                tb4.ID = "satuanbr_" + i;
                tb4.Text = rs.Rows[i]["Satuan"].ToString();
                tb4.Width = 70;
                tb4.ReadOnly = true;

                tb5 = new TextBox();
                tb5.ID = "hrgabr_" + i;
                tb5.Text = Cf.Num(rs.Rows[i]["HargaSatuan"]);
                tb5.Width = 140;
                tb5.ReadOnly = true;

                tb6 = new TextBox();
                tb6.ID = "qtybr_" + i;
                tb6.Text = Cf.Num(rs.Rows[i]["Stock"]);
                tb6.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                tb6.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                tb6.Attributes["onblur"] = "CalcBlur(this);hitungaja('" + i + "');";
                tb6.Width = 50;

                tb7 = new TextBox();
                tb7.ID = "totalbr_" + i;
                tb7.Text = Cf.Num(rs.Rows[i]["HargaTotal"]);
                tb7.Width = 140;
                tb7.ReadOnly = true;

                lbdel = new LinkButton();
                lbdel.Text = "<a href='KontrakGimmickDel.aspx?NoKontrak=" + NoKontrak + "&SN=" + (i+1) + "'>Delete...</a>";

                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                tb.Attributes["ondblclick"] = btn.Attributes["onclick"] =
                    "callgimmick('" + tb.ID + "','" + tb2.ID + "','" + tb4.ID + "','" + tb5.ID + "','" + Project + "')";

                cb = new CheckBox();
                cb.ID = "diterima_" + i;
                if(Convert.ToInt32(rs.Rows[i]["Diterima"]) != 0)
                {
                    cb.Checked = true;
                }
                else
                {
                    cb.Checked = false;
                }

                c = new HtmlTableCell();
                c.ID = "pk_" + i;
                c.Controls.Add(cb);
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.Controls.Add(tb);
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
                c.Controls.Add(lbdel);
                tr.Cells.Add(c);

                tot += Convert.ToDecimal(rs.Rows[i]["HargaTotal"]);
            }

            ////biar ke postback
            //if (Convert.ToInt32(Request.QueryString["alertc"]) != 0)
            //{
            //    total.Text = Cf.Num(Db.SingleDecimal("select ISNULL(SUM(HargaTotal),0) from MS_KONTRAK_GIMMICK where NoKontrak = '" + NoKontrak + "'"));
            //}
            //else
            //{
                total.Text = Cf.Num(tot);
            //}
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                {
                    //if (Convert.ToInt32(Request.QueryString["alertc"]) != 0)
                    //{
                    //    string NamaGimmickAlert = Db.SingleString("select ISNULL(Nama,'') from MS_GIMMICK where ItemID = '" + Request.QueryString["alertc"].ToString() + "'");
                    //    alertc.Text = "<img src='/Media/alert.gif' align=absmiddle> "
                    //        + "<font style='color:red'>Nilai Quantity Item " + NamaGimmickAlert + " melebihi Nilai Stock yang tersedia...</font>";
                    //}
                    //else
                    //{
                        alertc.Text = "";
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Gimmick Berhasil...";
                    //}
                }
                else
                {
                    //if (Convert.ToInt32(Request.QueryString["alertc"]) != 0)
                    //{
                    //    string NamaGimmickAlert = Db.SingleString("select ISNULL(Nama,'') from MS_GIMMICK where ItemID = '" + Request.QueryString["alertc"].ToString() + "'");
                    //    alertc.Text = "<img src='/Media/alert.gif' align=absmiddle> "
                    //        + "<font style='color:red'>Nilai Quantity Item " + NamaGimmickAlert + " melebihi Nilai Stock yang tersedia...</font>";
                    //}
                    //else
                    //{
                        alertc.Text = "";
                        feed.Text = "";
                    //}
                }
            }
        }

        private bool valid()
        {

            string s = "";
            bool x = true;

            //Tanggal Diterima
            if (!Cf.isTgl(tglditerima))
            {
                x = false;
                if (s == "") s = tglditerima.ID;
                tglditerimac.Text = "Tanggal";
            }
            else
                tglditerimac.Text = "";
            
            return x;
        }

        protected void save_Click(object sender, EventArgs e)
        {
            int FeedBackAlert = 0;

            DataTable rsBfr = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,SN) + '. ' + Nama + '. Status Gimmick :' +  CASE Diterima When 0 Then 'Pending'  Else 'Diterima ' END + ' '"
                    + " + ' (' + (Select Nama from REF_TIPE_GIMMICK where ID = MS_KONTRAK_GIMMICK.Tipe) + ') Stock Permintaan : ' + CONVERT(VARCHAR,Stock,1) + ' ' + Satuan + ' '"
                    + " + CONVERT(VARCHAR,HargaSatuan,1) +  ' Total : ' + CONVERT(VARCHAR,HargaTotal,1) "
                    + " FROM MS_KONTRAK_GIMMICK WHERE NoKontrak = '" + NoKontrak + "' Order By SN");
            
            DataTable rsb = Db.Rs("SELECT * FROM MS_KONTRAK_GIMMICK WHERE NoKontrak = '" + NoKontrak + "'");
            for (int k = 0; k < rsb.Rows.Count; k++)
            {
                TextBox Kode = (TextBox)list.FindControl("kodebr_" + k);
                TextBox qty = (TextBox)list.FindControl("qtybr_" + k);

                DataTable rsj = Db.Rs("SELECT * FROM MS_GIMMICK WHERE ItemID = '" + Kode.Text + "'");

                decimal Total = (Convert.ToDecimal(rsj.Rows[0]["HargaSatuan"]) * Convert.ToDecimal(qty.Text));

                Db.Execute("UPDATE MS_KONTRAK_GIMMICK SET"
                    + " ItemID = '" + Kode.Text + "'"
                    + ",Nama = '" + rsj.Rows[0]["Nama"].ToString() + "'"
                    + ",Tipe = '" + rsj.Rows[0]["Tipe"].ToString() + "'"
                    + ",Satuan = '" + rsj.Rows[0]["Satuan"].ToString() + "'"
                    + ",HargaSatuan = " + Convert.ToDecimal(rsj.Rows[0]["HargaSatuan"])
                    + ",HargaTotal = " + Total
                    + " WHERE NoKontrak = '" + NoKontrak + "' AND SN = " + (k + 1)
                    );

                #region hitung_stock_gimmick_edit 
                
                decimal Stock = Db.SingleDecimal("select ISNULL(SUM(Stock),0) from MS_GIMMICK where ItemID = '" + Kode.Text + "'");
                decimal KontrakStock = Db.SingleDecimal("select ISNULL(SUM(Stock),0) from MS_KONTRAK_GIMMICK where ItemID = '" + Kode.Text + "'");
                decimal ValueStock = Stock - KontrakStock;
                //decimal TotalStock = ValueStock + Convert.ToDecimal(qty.Text);

                //decimal SisaStock = Convert.ToDecimal(rsj.Rows[0]["Stock"]) - TotalStock;
                
                if (ValueStock >= Convert.ToDecimal(qty.Text))
                {
                    Db.Execute("UPDATE MS_KONTRAK_GIMMICK SET "
                        + " Stock = " + Convert.ToInt32(qty.Text)
                        + " WHERE NoKontrak = '" + NoKontrak + "' AND SN = " + (k + 1)
                    );
                }
                else
                {
                    FeedBackAlert = Convert.ToInt32(Kode.Text);  //ini buat kalo nilai stock lebih gede dari masternya
                }
                #endregion

                #region save_gimmick_diterima 
                //status gimmick diterima
                CheckBox cb = (CheckBox)list.FindControl("diterima_" + k);
                if (cb.Checked)
                {
                    Db.Execute("UPDATE MS_KONTRAK_GIMMICK SET"
                        + " Diterima = 1"
                        + " WHERE NoKontrak = '" + NoKontrak + "' AND SN = " + (k + 1)
                    );
                }
                else
                {
                    Db.Execute("UPDATE MS_KONTRAK_GIMMICK SET"
                        + " Diterima = 0"
                        + " WHERE NoKontrak = '" + NoKontrak + "' AND SN = " + (k + 1)
                    );
                }
                #endregion
            }

            string KodeBaru = Cf.Str(kodebaru.Text);
            int QtyBaru = Convert.ToInt32(qtybaru.Text);

            if (KodeBaru != "" && QtyBaru != 0)
            {
                DataTable rs = Db.Rs("SELECT * FROM MS_GIMMICK WHERE ItemID = '" + KodeBaru + "'");

                string NamaBaru = Cf.Str(rs.Rows[0]["Nama"].ToString());
                string TipeBaru = Cf.Str(rs.Rows[0]["Tipe"].ToString());
                string SatuanBaru = Cf.Str(rs.Rows[0]["satuan"].ToString());
                decimal HargaBaru = Convert.ToDecimal(rs.Rows[0]["HargaSatuan"]);
                decimal TotalBaru = Math.Round(HargaBaru * QtyBaru);

                #region hitung_stock_gimmick_baru
                
                decimal KontrakStockBaru = Db.SingleDecimal("select ISNULL(SUM(Stock),0) from MS_KONTRAK_GIMMICK where ItemID = '" + KodeBaru + "'");
                decimal TotalStockBaru = KontrakStockBaru + Convert.ToDecimal(QtyBaru);

                if (Convert.ToDecimal(rs.Rows[0]["Stock"]) >=  TotalStockBaru)
                {
                    Db.Execute("EXEC spKontrakGimmick"
                                + " '" + NoKontrak + "'"
                                + ",'" + (Db.SingleInteger("SELECT ISNULL(MAX(SN),0) FROM MS_KONTRAK_GIMMICK WHERE NoKontrak = '" + NoKontrak + "'") + 1) + "'"
                                + ",'" + KodeBaru + "'"
                                + ",'" + NamaBaru + "'"
                                + ",'" + TipeBaru + "'"
                                + ",'" + SatuanBaru + "'"
                                + ",'" + QtyBaru + "'"
                                + ",'" + Convert.ToDecimal(HargaBaru) + "'"
                                + ",'" + Convert.ToDecimal(TotalBaru) + "'");
                }
                else
                {
                    FeedBackAlert = Convert.ToInt32(KodeBaru);  //ini buat kalo nilai stock lebih gede dari masternya -- diakalin ke feedback
                }
                #endregion
            }

            //kalo ambil data catatan-nya di TOP 1 aja
            Db.Execute("UPDATE MS_KONTRAK_GIMMICk SET Catatan = '" + catatan.Text + "' WHERE NoKontrak  = '" + NoKontrak + "'");
            string LogCatatan = Db.SingleString("select TOP 1 ISNULL(Catatan, '') from MS_KONTRAK_GIMMICK where NoKontrak = '" + NoKontrak + "'");

            if (tglditerima.Text != "") //tgl gimmick diterima oleh customer
            {
                if(valid())
                {
                    Db.Execute("UPDATE MS_KONTRAK_GIMMICk SET TglDiterima = '" + tglditerima.Text + "' WHERE NoKontrak  = '" + NoKontrak + "'");
                }
            }

            DataTable rsAft = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,SN) + '. ' + Nama + '. Status Gimmick :' +  CASE Diterima When 0 Then 'Pending'  Else 'Diterima ' END + ' '"
                    + " + ' (' + (Select Nama from REF_TIPE_GIMMICK where ID = MS_KONTRAK_GIMMICK.Tipe) + ') Stock Permintaan : ' + CONVERT(VARCHAR,Stock,1) + ' ' + Satuan + ' '"
                    + " + CONVERT(VARCHAR,HargaSatuan,1) +  ' Total : ' + CONVERT(VARCHAR,HargaTotal,1) "
                    + " FROM MS_KONTRAK_GIMMICK WHERE NoKontrak = '" + NoKontrak + "' Order By SN");

            string Ket = "<br>---EDIT GIMMICK---<br>"
                + Cf.LogList(rsBfr, rsAft, LogCatatan);

            Db.Execute("EXEC spLogKontrak"
                + " 'E-GIM'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + NoKontrak + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Response.Redirect("KontrakGimmick.aspx?Nokontrak=" + NoKontrak + "&alertc=" + FeedBackAlert + "&done=1");
        }
    }
}