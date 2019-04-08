using System;
using System.Drawing;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakApprovGN : System.Web.UI.Page
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
                DataTable hakapp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 1 AND UserID = '" + Act.UserID + "'");
                for (int i = 0; i < hakapp.Rows.Count; i++)
                {
                    Fill(Convert.ToInt16(hakapp.Rows[i]["Lvl"]), id);
                }
            }
            else
            {
                Fill();
            }
            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Jalankan prosedur APPROVAL Pengalihan Hak?\\nProses ini akan merubah data kepemilikan unit properti.");
        }

        private class id
        {
            public int index { get; set; }
        }

        private static DataTable HakApp()
        {
            DataTable hakapp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 1");

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
                        + "Approval Pengalihan Hak Selesai..."
                        ;
            }
        }

        private void Fill(int lvl, id id)
        {
            string w = "AND NoKontrak NOT IN (SELECT NoKontrak FROM MS_KONTRAK_APP_LOG WHERE NoKontrak = a.NoKontrak AND Tipe = 1 AND Lvl = " + lvl + " AND Finish = 0)";

            if (lvl > 1)
            {
                w += "AND NoKontrak IN (SELECT NoKontrak FROM MS_KONTRAK_APP_LOG WHERE NoKontrak = a.NoKontrak AND Tipe = 1 AND Lvl = " + (lvl - 1) + " AND Approve = 1 AND Finish = 0)";
            }

            string strSql = " SELECT a.*, b.NoUnit, c.Nama, d.Nama AS Agent, d.Principal FROM MS_KONTRAK a"
                            + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                            + " INNER JOIN MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                            + " INNER JOIN MS_AGENT d ON a.NoAgent = d.NoAgent"
                            + " WHERE a.ApprovalGN = 1"
                            + " AND a.Status <> 'B'"
                            + w
                            ;

            DataTable rs = Db.Rs(strSql);
            //if (rs.Rows.Count > 0)
            //    save.Enabled = true;

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
                c.InnerHtml = rs.Rows[i]["Nama"].ToString() + " (" + rs.Rows[i]["NoCustomer"].ToString() + ")";
                tr.Cells.Add(c);

                string customerbaru = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer='" + rs.Rows[i]["TempGN"].ToString() + "'");

                c = new HtmlTableCell();
                c.InnerHtml = customerbaru + " (" + rs.Rows[i]["TempGN"].ToString() + ")";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["TempBiayaGN"]));
                c.Align = "right";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["TempBiayaPPH"]));
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
                            + " WHERE a.ApprovalGN = 1"
                            + " AND a.Status <> 'B'"
                            ;

            DataTable rs = Db.Rs(strSql);
            //if (rs.Rows.Count > 0)
            //    save.Enabled = true;

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
                c.InnerHtml = rs.Rows[i]["Nama"].ToString() + " (" + rs.Rows[i]["NoCustomer"].ToString() + ")";
                tr.Cells.Add(c);

                string customerbaru = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer='" + rs.Rows[i]["TempGN"].ToString() + "'");

                c = new HtmlTableCell();
                c.InnerHtml = customerbaru + " (" + rs.Rows[i]["TempGN"].ToString() + ")";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["TempBiayaGN"]));
                c.Align = "right";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["TempBiayaPPH"]));
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
                Response.Redirect("KontrakApprovGN.aspx?done=yes");
            }
        }

        private void SaveApproval(string NoKontrak, CheckBox cb, DateTime Tgl, string lvl)
        {
            int Lvl = Convert.ToInt16(lvl);
            int MaxApp = 0;
            if (HakApp().Rows.Count > 0)
                MaxApp = Db.SingleByte("SELECT TOP 1 Lvl FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 1 ORDER BY Lvl DESC");

            string Ket = "";

            if (cb.Checked)
            {
                if (Lvl < MaxApp)
                {
                    Ket = "Tgl Approval GN : " + Cf.Day(Convert.ToDateTime(Tgl));

                    //Push notif ke Approval selanjutnya
                    DataTable rsNextApp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 1 "
                        + " AND Lvl = " + (Lvl + 1));

                    for (int i = 0; i < rsNextApp.Rows.Count; i++)
                    {
                        string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                        LibApi.PushNotif("APR-GN", "Permohonan Approval Pengalihan Hak " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                    }
                }
                else
                {
                    DataTable rsBef = Db.Rs("SELECT "
                            + " MS_CUSTOMER.NoCustomer AS [No. Customer]"
                            + ",MS_CUSTOMER.Nama AS [Nama Customer]"
                            //+ ",CONVERT(varchar,MS_KONTRAK.TglApGN,106) AS [Tgl Approval Pengalihan Hak]"
                            + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER "
                            + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                            + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                    /*Ambil No Customer Baru dari TempGN*/
                    int NoCustomer = Db.SingleInteger("SELECT TempGN FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");

                    Db.Execute("EXEC spKontrakGantiNama "
                            + " '" + NoKontrak + "'"
                            + ", '" + NoCustomer + "'"
                            // + ", '" + Tgl + "'"
                            );

                    /*Update Flag ApprovalGN*/
                    Db.Execute("UPDATE MS_KONTRAK "
                        + " SET ApprovalGN = 0"
                        + " ,Revisi = Revisi + 1"
                        + " WHERE NoKontrak='" + NoKontrak + "'"
                        );

                    DataTable rsAft = Db.Rs("SELECT "
                            + " MS_CUSTOMER.NoCustomer AS [No. Customer]"
                            + ",MS_CUSTOMER.Nama AS [Nama Customer]"
                            //+ ",CONVERT(varchar,MS_KONTRAK.TglApGN,106) AS [Tgl Approval Pengalihan Hak]"
                            + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER "
                            + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                            + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                    /* Ambil Nilai Biaya dari TempBiayaGN */
                    decimal NilaiBiaya = Db.SingleDecimal("SELECT TempBiayaGN FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");

                    if (NilaiBiaya != 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA ADM. Pengalihan Hak'"
                            + ",'" + Cf.Day(DateTime.Today) + "'"
                            + ", " + NilaiBiaya
                            + ",'ADM'"
                            );
                    }

                    /* Ambil Nilai PPH Pengalihan Hak dari TempBiayaPPH */
                    decimal NilaiPPH = Db.SingleDecimal("SELECT TempBiayaPPH FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");

                    if (NilaiPPH != 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA PPH PENGALIHAN HAK'"
                            + ",'" + Cf.Day(DateTime.Today) + "'"
                            + ", " + NilaiPPH
                            + ",'ADM'"
                            );
                    }


                    /*Pengalihan Hak customer di MS_TTS*/
                    string strNamaCs = Cf.Str(Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer));
                    string strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                        + " SET Customer = '" + strNamaCs + "'"
                        + " WHERE Ref = '" + NoKontrak + "'"
                        + " AND Tipe = 'JUAL'"
                        ;
                    Db.Execute(strSql);
                    /*******************************/

                    /*Pengalihan Hak customer di MS_MEMO*/
                    strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_MEMO"
                        + " SET Customer = '" + strNamaCs + "'"
                        + " WHERE Ref = '" + NoKontrak + "'"
                        + " AND Tipe = 'JUAL'"
                        ;
                    Db.Execute(strSql);
                    /*******************************/

                    /*Pengalihan Hak customer di MS_PJT*/
                    strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PJT"
                        + " SET Customer = '" + strNamaCs + "'"
                        + " WHERE Ref = '" + NoKontrak + "'"
                        + " AND Tipe = 'JUAL'"
                        ;
                    Db.Execute(strSql);
                    /*******************************/

                    /*Pengalihan Hak customer di MS_TUNGGAKAN*/
                    strSql = "UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN"
                        + " SET Customer = '" + strNamaCs + "'"
                        + " WHERE Ref = '" + NoKontrak + "'"
                        + " AND Tipe = 'JUAL'"
                        ;
                    Db.Execute(strSql);
                    /*******************************/

                    Ket = Cf.LogCompare(rsBef, rsAft)
                        + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                        + "<br>Biaya PPH Pengalihan Hak : " + Cf.Num(NilaiPPH)
                        + "<br>Tgl Approval GN : " + Cf.Day(Convert.ToDateTime(tglot.Text))
                        ;
                }

                if (HakApp().Rows.Count > 0)
                {
                    Db.Execute("EXEC spLogKontrakApp "
                    + " '" + NoKontrak + "'"
                    + ",'" + Act.UserID + "'"
                    + "," + 1
                    + ",'" + Convert.ToDateTime(Cf.Day(Tgl)) + "'"
                    + "," + Lvl
                    + "," + 1
                    + ",''"
                    );

                    decimal LogID2 = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_APP_LOG ORDER BY LogID DESC");
                    string Project2 = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_APP_LOG SET Project = '" + Project2 + "' WHERE LogID  = " + LogID2);

                    if (Lvl == MaxApp)
                    {
                        Db.Execute("UPDATE MS_KONTRAK_APP_LOG Set Finish = 1 "
                            + " WHERE NoKontrak = '" + NoKontrak + "'"
                            + " AND Tipe = 1");
                    }
                }

                Db.Execute("EXEC spLogKontrak "
                    + " 'APR-GN'"
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
