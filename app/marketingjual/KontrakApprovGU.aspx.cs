using System;
using System.Drawing;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakApprovGU : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                InitForm();
            }

            id id = new id();
            id.index = 0;

            if (HakApp().Rows.Count > 0)
            {
                DataTable hakapp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 2 AND UserID = '" + Act.UserID + "'");
                for (int i = 0; i < hakapp.Rows.Count; i++)
                {
                    Fill(Convert.ToInt16(hakapp.Rows[i]["Lvl"]), id);
                }
            }
            else
            {
                Fill();
            }
            if (frm.Visible) Js.Confirm(this, "Jalankan prosedur APPROVAL Pindah Unit?\\nProses ini akan merubah data kepemilikan unit properti.");
        }

        private class id
        {
            public int index { get; set; }
        }

        private static DataTable HakApp()
        {
            DataTable hakapp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 2");

            return hakapp;
        }

        private void InitForm()
        {
            tglot.Text = Cf.Day(Convert.ToDateTime(DateTime.Today));
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Approval Pindah Unit Selesai..."
                        ;
            }
        }

        private void Fill(int lvl, id id)
        {
            string w = "AND NoKontrak NOT IN (SELECT NoKontrak FROM MS_KONTRAK_APP_LOG WHERE NoKontrak = a.NoKontrak AND Tipe = 2 AND Lvl = " + lvl + " AND Finish = 0)";

            if (lvl > 1)
            {
                w += "AND NoKontrak IN (SELECT NoKontrak FROM MS_KONTRAK_APP_LOG WHERE NoKontrak = a.NoKontrak AND Tipe = 2 AND Lvl = " + (lvl - 1) + " AND Approve = 1 AND Finish = 0)";
            }

            string strSql = " SELECT a.*, b.NoUnit, c.Nama, d.Nama AS Agent, d.Principal FROM MS_KONTRAK a"
                            + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                            + " INNER JOIN MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                            + " INNER JOIN MS_AGENT d ON a.NoAgent = d.NoAgent"
                            + " WHERE a.ApprovalGU = 1"
                            + " AND a.Status <> 'B'"
                            + w
                            ;

            DataTable rs = Db.Rs(strSql);
            //if (rs.Rows.Count == 0)
            //    save.Enabled = false;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow tr;
                HtmlTableCell c;
                CheckBox cb;

                tr = new HtmlTableRow();
                list.Controls.Add(tr);

                cb = new CheckBox();
                cb.ID = "nokontrak_" + id.index;

                c = new HtmlTableCell();
                c.ID = "pk_" + id.index;
                c.Attributes["title"] = rs.Rows[i]["NoKontrak"].ToString();
                c.Controls.Add(cb);
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.ID = "lvl_" + id.index;
                c.Attributes["title"] = lvl.ToString();
                c.Visible = false;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = "<a href=\"javascript:popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')\">"
                    + rs.Rows[i]["NoKontrak"].ToString()
                    + "</a>";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Nama"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Agent"].ToString() + "-" + rs.Rows[i]["Principal"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString() + " (" + rs.Rows[i]["NoStock"].ToString() + ")";
                tr.Cells.Add(c);

                string unitbaru = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock='" + rs.Rows[i]["TempGU"].ToString() + "'");

                c = new HtmlTableCell();
                c.InnerHtml = unitbaru + " (" + rs.Rows[i]["TempGU"].ToString() + ")";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["TempBiayaGU"]));
                c.Align = "right";
                tr.Cells.Add(c);

                id.index += 1;
            }

            if (list.Controls.Count > 0)
                save.Enabled = true;

        }

        private void Fill()
        {
            string strSql = " SELECT a.*, b.NoUnit, c.Nama, d.Nama AS Agent, d.Principal FROM MS_KONTRAK a"
                            + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                            + " INNER JOIN MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                            + " INNER JOIN MS_AGENT d ON a.NoAgent = d.NoAgent"
                            + " WHERE a.ApprovalGU = 1"
                            + " AND a.Status <> 'B'"
                            ;

            DataTable rs = Db.Rs(strSql);
            //if (rs.Rows.Count == 0)
            //    save.Enabled = false;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow tr;
                HtmlTableCell c;
                CheckBox cb;

                tr = new HtmlTableRow();
                list.Controls.Add(tr);

                cb = new CheckBox();
                cb.ID = "nokontrak_" + i;

                c = new HtmlTableCell();
                c.ID = "pk_" + i;
                c.Attributes["title"] = rs.Rows[i]["NoKontrak"].ToString();
                c.Controls.Add(cb);
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = "<a href=\"javascript:popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')\">"
                    + rs.Rows[i]["NoKontrak"].ToString()
                    + "</a>";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Nama"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Agent"].ToString() + "-" + rs.Rows[i]["Principal"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString() + " (" + rs.Rows[i]["NoStock"].ToString() + ")";
                tr.Cells.Add(c);

                string unitbaru = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock='" + rs.Rows[i]["TempGU"].ToString() + "'");

                c = new HtmlTableCell();
                c.InnerHtml = unitbaru + " (" + rs.Rows[i]["TempGU"].ToString() + ")";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["TempBiayaGU"]));
                c.Align = "right";
                tr.Cells.Add(c);
            }

            if (list.Controls.Count > 0)
                save.Enabled = true;

        }

        private bool datavalid()
        {
            bool x = true;
            string s = "";

            if (!Cf.isTgl(tglot))
            {
                x = false;
                if (s == "") s = tglot.ID;
                tglotc.Text = "Format Tanggal";
            }
            else
                tglotc.Text = "";

            if (!x)
            {
                this.RegisterStartupScript(
                    "focusScript"
                    , "<script language='javascript' type='text/javascript'>"
                    + "document.getElementById('" + s + "').focus();"
                    + "</script>"
                    );
            }

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {

                int index = 0;
                foreach (Control tr in list.Controls)
                {
                    HtmlTableCell c = (HtmlTableCell)list.FindControl("pk_" + index);
                    CheckBox cb = (CheckBox)list.FindControl("nokontrak_" + index);
                    DateTime Tgl = Convert.ToDateTime(tglot.Text);

                    string lvl = "0";
                    if (HakApp().Rows.Count > 0)
                    {
                        HtmlTableCell c2 = (HtmlTableCell)list.FindControl("lvl_" + index);
                        lvl = c2.Attributes["title"];
                    }

                    if (c != null)
                    {
                        SaveApproval(c.Attributes["title"], cb, Tgl, lvl);
                    }

                    index++;
                }
                Response.Redirect("KontrakApprovGU.aspx?done=yes");
            }
        }

        private void SaveApproval(String NoKontrak, CheckBox cb, DateTime Tgl, string lvl)
        {
            int Lvl = Convert.ToInt16(lvl);
            int MaxApp = 0;
            if (HakApp().Rows.Count > 0)
                MaxApp = Db.SingleByte("SELECT TOP 1 Lvl FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 2 ORDER BY Lvl DESC");

            string Ket = "";

            if (cb.Checked)
            {
                if (Lvl < MaxApp)
                {
                    Ket = "Tgl Approval GU : " + Cf.Day(Convert.ToDateTime(Tgl));

                    //Push notif ke Approval selanjutnya
                    DataTable rsNextApp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 2 "
                        + " AND Lvl = " + (Lvl + 1));

                    for (int i = 0; i < rsNextApp.Rows.Count; i++)
                    {
                        string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                        LibApi.PushNotif("APR-GU", "Permohonan Approval Pindah Unit " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                    }
                }
                else
                {
                    DataTable rsBef = Db.Rs("SELECT "
                            + " NoStock AS [No. Stock]"
                            + ",NoUnit AS [Unit]"
                            + ",Luas AS [Luas]"
                            + ",Gross AS [Nilai Gross]"
                            //+ ",Surcharge AS [Surcharge]"
                            + ",NilaiKontrak AS [Nilai Kontrak]"
                            + ",DiskonRupiah AS [Diskon dalam Rupiah]"
                            + ",DiskonPersen AS [Diskon dalam Persen]"
                            + ",NilaiPPN AS [Nilai PPN]"
                            + ",NoVA AS [No. VA]"
                            + ",Harga Tanah AS [Harga Tanah]"
                            //+ ",PPNPemerintah AS [PPN Pemerintah]"
                            //+ ",CONVERT(varchar,TglApGU,106) AS [Tgl Approval]"
                            + " FROM MS_KONTRAK"
                            + " WHERE NoKontrak = '" + NoKontrak + "'");

                    string NoStockOld = Db.SingleString(
                        "SELECT NoStock FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                    string NoStockTemp = Db.SingleString(
                        "SELECT TempGU FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                    decimal biaya = Db.SingleDecimal("SELECT TempBiayaGU FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                    //Surcharge
                    //string nounit = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock='" + NoStockTemp + "'");
                    //string Lantai = nounit.Substring(3, 2);
                    //if (Lantai == "08" || Lantai == "09")
                    //{
                    //    Db.Execute("UPDATE MS_KONTRAK"
                    //    + " SET Surcharge='" + (decimal)8000000 + "'"
                    //    + " WHERE NoKontrak='" + NoKontrak + "'"
                    //    );
                    //}
                    //else
                    //{
                    // Db.Execute("UPDATE MS_KONTRAK"
                    // + " SET Surcharge='" + (decimal)0 + "'"
                    // + " WHERE NoKontrak='" + NoKontrak + "'"
                    // );
                    //}
                    
                    Db.Execute("EXEC spKontrakGantiUnit "
                        + " '" + NoKontrak + "'"
                        + ",'" + NoStockTemp + "'"
                        // + ",'" + Tgl + "'"
                        );
                    
                    //Insert tagihan
                    if (biaya != 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA ADM. Pindah Unit'"
                            + ",'" + Cf.Day(DateTime.Today) + "'"
                            + ", " + biaya
                            + ",'ADM'"
                            );
                    }


                    //UPDATE Nilai PPN , Nilai Kontrak Terbaru, PPNPemerintah, ApprovalGU
                    string jenisppn = Db.SingleString("SELECT JenisPPN FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");
                    decimal DPP = Db.SingleDecimal("SELECT NilaiDPP FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");
                    decimal NilaiPPN = 0;
                    decimal NilaiKontrak = Db.SingleDecimal("SELECT NilaiKontrak FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");
                    decimal PPNDitanggungPemerintah = 0;
                    if (jenisppn == "KONSUMEN")
                    {
                        PPNDitanggungPemerintah = 0;
                        NilaiPPN = NilaiKontrak - DPP;
                        //NilaiKontrak = DPP + NilaiPPN;
                    }
                    else
                    {
                        PPNDitanggungPemerintah = DPP * (decimal)0.1;
                        NilaiPPN = 0;
                        //NilaiKontrak = DPP + NilaiPPN;
                    }

                    //VA
                    string NoStockTerbaru = Db.SingleString("SELECT ISNULL(NoStock, '') FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    string NoVA = "";
                    string LokasiVA = Db.SingleString("SELECT Lokasi FROM MS_KONTRAK WHERE NoStock = '" + NoStockTerbaru + "'");

                    string VA = "8151";

                    //di kurang 1 karena sudah save kontrak di ms kontrak
                    //int CountUnit = Db.SingleInteger("SELECT Count(*) FROM MS_KONTRAK WHERE NoStock = '" + NoStock + "'") - 1;
                    int CountUnit = Db.SingleInteger("SELECT Count(*) FROM MS_KONTRAK WHERE NoStock = '" + NoStockTerbaru + "'");
                    string Nomor = Db.SingleString("SELECT Nomor FROM MS_UNIT WHERE NoStock = '" + NoStockTerbaru + "'");
                    string Lantai = Db.SingleString("SELECT Lantai FROM MS_UNIT WHERE NoStock = '" + NoStockTerbaru + "'");
                    string NoLantai = "";
                    if (Lantai == "BLV")
                    {
                        NoLantai = "99";
                    }
                    else
                    {
                        NoLantai = Lantai.PadLeft(2, '0');
                    }

                    //Tambahan Richard Harga Tanah dan Bangunan 6 Des 2018
                    int NoSkema = Db.SingleInteger("select ISNULL(RefSkema, 0) FROM MS_KONTRAK where NoKontrak = '" + NoKontrak + "'");
                    string RumusBunga = Db.SingleDecimal("Select ISNULL(Bunga, 0) FROM REF_SKEMA where Nomor = '" + NoSkema  + "'").ToString(); //bunga2.Text;

                    decimal HargaTanah = Db.SingleDecimal("Select ISNULL(HargaTanah, 0) From MS_UNIT Where NoStock = '" + NoStockTerbaru + "'");
                    decimal HargaTanahAfterBunga = Func.SetelahBunga(RumusBunga, HargaTanah) - Math.Round((Func.SetelahBunga(RumusBunga, HargaTanah) / (decimal)1.1));
                    //End of Tambahan

                    int Lokasii = Db.SingleInteger("SELECT SNVA FROM REF_LOKASI WHERE Lokasi = '" + LokasiVA + "'");
                    NoVA = VA + CountUnit.ToString().PadLeft(2, '0') + Lokasii.ToString().PadLeft(2, '0') + NoLantai + Nomor.PadLeft(2, '0');

                    Db.Execute("UPDATE MS_KONTRAK "
                                + " SET NilaiPPN='" + NilaiPPN + "'"
                                + ", NilaiKontrak='" + NilaiKontrak + "'"
                                //+ ", PPNPemerintah = '" + PPNDitanggungPemerintah + "'"
                                + ", ApprovalGU = '" + Convert.ToBoolean(0) + "'"
                                + ", Revisi = Revisi + 1"
                                + ", NoVA = '" + NoVA + "'"
                                + ", HargaTanah = " + HargaTanahAfterBunga
                                + " WHERE NoKontrak='" + NoKontrak + "'"
                                );

                    DataTable rsAft = Db.Rs("SELECT "
                                + " NoStock AS [No. Stock]"
                                + ",NoUnit AS [Unit]"
                                + ",Luas AS [Luas]"
                                + ",Gross AS [Nilai Gross]"
                                //+ ",Surcharge AS [Surcharge]"
                                + ",NilaiKontrak AS [Nilai Kontrak]"
                                + ",DiskonRupiah AS [Diskon dalam Rupiah]"
                                + ",DiskonPersen AS [Diskon dalam Persen]"
                                + ",NilaiPPN AS [Nilai PPN]"
                                + ",NoVA AS [No. VA]"
                                + ",Harga Tanah AS [Harga Tanah]"
                                //+ ",PPNPemerintah AS [PPN Pemerintah]"
                                //+ ",CONVERT(varchar,TglApGU,106) [Tgl Approval]" 
                                + " FROM MS_KONTRAK"
                                + " WHERE NoKontrak = '" + NoKontrak + "'");


                    /*Ganti nomor unit di MS_TTS*/
                    string strNoUnit = Cf.Str(Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock = '" + NoStockTemp + "'"));
                    string strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                        + " SET Unit = '" + strNoUnit + "'"
                        + " WHERE Ref = '" + NoKontrak + "'"
                        + " AND Tipe = 'JUAL'"
                        ;
                    Db.Execute(strSql);
                    /*******************************/

                    /*Ganti nomor unit di MS_MEMO*/
                    strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_MEMO"
                        + " SET Unit = '" + strNoUnit + "'"
                        + " WHERE Ref = '" + NoKontrak + "'"
                        + " AND Tipe = 'JUAL'"
                        ;
                    Db.Execute(strSql);
                    /*******************************/

                    /*Ganti nomor unit di MS_PJT*/
                    strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PJT"
                        + " SET Unit = '" + strNoUnit + "'"
                        + " WHERE Ref = '" + NoKontrak + "'"
                        + " AND Tipe = 'JUAL'"
                        ;
                    Db.Execute(strSql);
                    /*******************************/

                    /*Ganti nomor unit di MS_TUNGGAKAN*/
                    strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN"
                        + " SET Unit = '" + strNoUnit + "'"
                        + " WHERE Ref = '" + NoKontrak + "'"
                        + " AND Tipe = 'JUAL'"
                        ;
                    Db.Execute(strSql);
                    /*******************************/

                    Ket = Cf.LogCompare(rsBef, rsAft)
                        + "<br>Biaya Administrasi : " + Cf.Num(biaya)
                        + "<br>Tgl Approval GU : " + Cf.Day(Convert.ToDateTime(tglot.Text));
                    ;
                }

                if (HakApp().Rows.Count > 0)
                {
                    Db.Execute("EXEC spLogKontrakApp "
                    + " '" + NoKontrak + "'"
                    + ",'" + Act.UserID + "'"
                    + "," + 1 //Approve
                    + ",'" + Convert.ToDateTime(Cf.Day(Tgl)) + "'"
                    + "," + Lvl
                    + "," + 2 //Tipe
                    + ",''"
                    );

                    if (Lvl == MaxApp)
                    {
                        Db.Execute("UPDATE MS_KONTRAK_APP_LOG Set Finish = 1 "
                            + " WHERE NoKontrak = '" + NoKontrak + "'"
                            + " AND Tipe = 2");
                    }
                }

                Db.Execute("EXEC spLogKontrak "
                    + " 'APR-GU'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoKontrak + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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
