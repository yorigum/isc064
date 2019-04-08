using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakApprovBatal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                DataTable hakapp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 3 AND UserID = '" + Act.UserID + "'");
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
            if (frm.Visible) Js.Confirm(this, "Jalankan prosedur APPROVAL PEMBATALAN KONTRAK?\\nProses ini akan merubah data kepemilikan unit properti.");
        }

        private class id
        {
            public int index { get; set; }
        }

        private static DataTable HakApp()
        {
            DataTable hakapp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 3");

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
                        + "Approval Batal Selesai..."
                        ;
            }
        }

        private void Fill(int lvl, id id)
        {
            string w = "AND NoKontrak NOT IN (SELECT NoKontrak FROM MS_KONTRAK_APP_LOG WHERE NoKontrak = a.NoKontrak AND Tipe = 3 AND Lvl = " + lvl + " AND Finish = 0)";

            if (lvl > 1)
            {
                w += "AND NoKontrak IN (SELECT NoKontrak FROM MS_KONTRAK_APP_LOG WHERE NoKontrak = a.NoKontrak AND Tipe = 3 AND Lvl = " + (lvl - 1) + " AND Approve = 1 AND Finish = 0)";
            }

            string strSql = " SELECT a.*, b.NoUnit, c.Nama, d.Nama AS Agent FROM MS_KONTRAK a"
                            + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                            + " INNER JOIN MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                            + " INNER JOIN MS_AGENT d ON a.NoAgent = d.NoAgent"
                            + " WHERE a.ApprovalBatal = 1"
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
                c.NoWrap = true;
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
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Nama"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Agent"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["AlasanBatal"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["KetAlasanBatal"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["BiayaBatal"]));
                c.Align = "right";
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["TotalLunasBatal"]));
                c.Align = "right";
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Day(rs.Rows[i]["TglKembali"]);
                c.Align = "right";
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiPulang"]));
                c.Align = "right";
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiKlaim"]));
                c.Align = "right";
                c.NoWrap = true;
                tr.Cells.Add(c);

                id.index += 1;
            }

            if (list.Controls.Count > 0)
                save.Enabled = true;
        }
        private void Fill()
        {
            string strSql = " SELECT a.*, b.NoUnit, c.Nama, d.Nama AS Agent FROM MS_KONTRAK a"
                            + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                            + " INNER JOIN MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                            + " INNER JOIN MS_AGENT d ON a.NoAgent = d.NoAgent"
                            + " WHERE a.ApprovalBatal = 1"
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
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = "<a href=\"javascript:popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')\">"
                    + rs.Rows[i]["NoKontrak"].ToString()
                    + "</a>";
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Nama"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Agent"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["AlasanBatal"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["KetAlasanBatal"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["BiayaBatal"]));
                c.Align = "right";
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["TotalLunasBatal"]));
                c.Align = "right";
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Day(rs.Rows[i]["TglKembali"]);
                c.Align = "right";
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiPulang"]));
                c.Align = "right";
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiKlaim"]));
                c.Align = "right";
                c.NoWrap = true;
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
                    , "<script type='text/javascript'>"
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
                Response.Redirect("KontrakApprovBatal.aspx?done=yes");
            }
        }

        private void SaveApproval(string NoKontrak, CheckBox cb, DateTime Tgl, string lvl)
        {
            int Lvl = Convert.ToInt16(lvl);
            int MaxApp = 0;
            if (HakApp().Rows.Count > 0)
                MaxApp = Db.SingleByte("SELECT TOP 1 Lvl FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 3 ORDER BY Lvl DESC");

            string Ket = "";

            if (cb.Checked)
            {
                if (Lvl < MaxApp)
                {
                    Ket = "Tgl. Batal : " + Cf.Day(Convert.ToDateTime(Tgl));

                    //Push notif ke Approval selanjutnya
                    DataTable rsNextApp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 3 "
                        + " AND Lvl = " + (Lvl + 1));

                    for (int i = 0; i < rsNextApp.Rows.Count; i++)
                    {
                        string UserIDNextApp = rsNextApp.Rows[i]["UserID"].ToString();
                        LibApi.PushNotif("BATAL", "Permohonan Approval Pembatalan Kontrak " + NoKontrak, UserIDNextApp, NoKontrak, 1);
                    }
                }
                else
                {
                    Db.Execute("EXEC spKontrakBatal "
                    + " '" + NoKontrak + "'"
                    );

                    if (Db.SingleString("SELECT Status FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'") == "B")
                    {
                        DataTable rs = Db.Rs("SELECT"
                            + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                            + ",MS_KONTRAK.NoUnit AS [Unit]"
                            + ",MS_CUSTOMER.Nama AS [Customer]"
                            + ",MS_AGENT.Nama AS [Agent]"
                            + ",AlasanBatal AS [Alasan Pembatalan]"
                            + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                            + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                            + " INNER JOIN MS_AGENT"
                            + " ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                            + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                        decimal NilaiBiaya = Db.SingleDecimal("SELECT BiayaBatal FROM MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");

                        if (NilaiBiaya != 0)
                        {
                            Db.Execute("EXEC spTagihanDaftar "
                                + " '" + NoKontrak + "'"
                                + ",'BIAYA ADM. PEMBATALAN'"
                                + ",'" + Cf.Day(DateTime.Today) + "'"
                                + ", " + NilaiBiaya
                                + ",'ADM'"
                                );
                        }
                        // decimal NomorPembatalan = Db.SingleDecimal("SELECT ISNULL(MAX(NomorPembatalan),0) + 1 FROM MS_KONTRAK");

                        // Db.Execute(" UPDATE MS_KONTRAK "
                        // + " SET NomorPembatalan = STUFF( " + NomorPembatalan + ", 1, 0, REPLICATE('0', 7 - LEN(" + NomorPembatalan + ")))"
                        // + ", TglKuasaPembatalan = '" + DateTime.Today + "'" //tglApprov batal
                        // + " WHERE NoKontrak = '" + NoKontrak + "'"
                        // );

                        decimal NilaiMasuk = Db.SingleDecimal(
                            "SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "'");
                        Db.Execute("UPDATE MS_KONTRAK SET BatalMasuk = "
                            + NilaiMasuk + " WHERE NoKontrak = '" + NoKontrak + "'");
                        decimal NilaiKlaim = Db.SingleDecimal("SELECT NilaiKlaim FROM MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                        decimal NilaiPengembalian = Db.SingleDecimal("SELECT NilaiPulang FROM MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                        decimal TotalLunas = Db.SingleDecimal("SELECT TotalLunasBatal FROM MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                        string acc = Db.SingleString("SELECT AccBatal FROM MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");

                        Ket = Cf.LogCapture(rs)
                            + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                            + "<br>Uang Masuk : " + Cf.Num(NilaiMasuk)
                            + "<br>Nilai Klaim : " + Cf.Num(NilaiKlaim)
                            + "<br>Tgl. Batal : " + Cf.Day(DateTime.Today)
                            + "<br>Total Pelunasan : " + Cf.Num(TotalLunas)
                            + "<br>Nilai Kembali : " + Cf.Num(NilaiPengembalian)
                            + "<br>Rekening Pembatalan : " + acc
                            ;

                        Func.CekKomisi(NoKontrak);

                        //floor plan
                        string Peta = Db.SingleString("SELECT Peta "
                            + " FROM MS_UNIT INNER JOIN MS_KONTRAK ON MS_UNIT.NoStock = MS_KONTRAK.NoStock "
                            + " WHERE NoKontrak = '" + NoKontrak + "'");
                        Func.GenerateFP(Peta);

                        //SA01
                        string CnnEsales = "Data Source=.;Initial Catalog=SA01;Persist Security Info=True;User ID=batavianet;Password=iNDigo100";
                        string ClosingID = Db.SingleString("SELECT ISNULL(ClosingID,'') FROM MS_Kontrak WHERE NoKontrak='" + NoKontrak + "'");
                        string AlasanBatal = Db.SingleString("SELECT ISNULL(AlasanBatal,'') FROM MS_Kontrak WHERE NoKontrak='" + NoKontrak + "'");

                        if (ClosingID != "")
                        {
                            Execute("UPDATE SalesClosing SET"
                                    + " Status=1"
                                    + ", AlasanCancel='" + AlasanBatal + "'"
                                    + ", TglCancel='" + Tgl + "'"
                                    + " WHERE ClosingID='" + ClosingID + "'"
                                    , CnnEsales);


                            StringBuilder x = new StringBuilder();
                            x.Append("Closing dengan kode " + ClosingID);
                            x.Append("<br/>");
                            x.Append("<i>Dibatalkan setelah kontrak terdaftar</i>");

                            string Closinger = "";
                            Closinger = SingleString("SELECT ISNULL(UserID,'') FROM SalesClosing WHERE ClosingID='" + ClosingID + "'", CnnEsales);
                            if (Closinger != "")
                            {
                                string Role = "";
                                Role = SingleString("SELECT ISNULL(RoleID,'') FROM SecUser WHERE UserID='" + Closinger + "'", CnnEsales);
                                if (Role == "SA.OPR")
                                {
                                    Execute("EXEC InsertSecNotification "
                                       + "'Pembatalan Kontrak'"
                                       + ",'" + x.ToString() + "'"
                                       + ",'" + Closinger + "'"
                                       + ",'../Sales/ClosingFile.aspx?id=" + ClosingID + "'"
                                       , CnnEsales);
                                }
                                else if (Role == "SA.MGR")
                                {
                                    Execute("EXEC InsertSecNotification "
                                    + "'Pembatalan Kontrak'"
                                    + ",'" + x.ToString() + "'"
                                    + ",'" + Closinger + "'"
                                    + ",'../SM/ClosingFile.aspx?id=" + ClosingID + "'"
                                    , CnnEsales);
                                }
                            }
                        }
                    }
                }

                if (HakApp().Rows.Count > 0)
                {
                    Db.Execute("EXEC spLogKontrakApp "
                    + " '" + NoKontrak + "'"
                    + ",'" + Act.UserID + "'"
                    + "," + 1 //Approve
                    + ",'" + Convert.ToDateTime(Cf.Day(Tgl)) + "'"
                    + "," + Lvl
                    + "," + 3 //Tipe
                    + ",''"
                    );

                    decimal LogID2 = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_APP_LOG ORDER BY LogID DESC");
                    string Project2 = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_APP_LOG SET Project = '" + Project2 + "' WHERE LogID  = " + LogID2);

                    if (Lvl == MaxApp)
                    {
                        Db.Execute("UPDATE MS_KONTRAK_APP_LOG Set Finish = 1 "
                                + " WHERE NoKontrak = '" + NoKontrak + "'"
                                + " AND Tipe = 3");
                    }
                }

                Db.Execute("EXEC spLogKontrak "
                    + " 'APR-BA'"
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

        //Common Driver
        protected void Execute(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCnn.Close();
        }
        protected DataTable Rs(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql, sqlCnn);
            DataSet objDS = new DataSet();
            sqlAdapter.Fill(objDS, "data");
            sqlCnn.Close();

            DataTable rs = new DataTable();
            rs = objDS.Tables["data"];

            return rs;
        }
        protected string SingleString(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            string x = "";
            x = (string)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected int SingleInteger(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            int x = (int)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected long SingleLong(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            long x = Convert.ToInt64(sqlCmd.ExecuteScalar());
            sqlCnn.Close();

            return x;
        }
        protected decimal SingleDecimal(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            decimal x = Convert.ToDecimal(sqlCmd.ExecuteScalar());
            sqlCnn.Close();

            return x;
        }
        protected bool SingleBool(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            bool x = (bool)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected byte SingleByte(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            byte x = (byte)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected DateTime SingleTime(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            DateTime x = (DateTime)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
    }

}
