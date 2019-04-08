using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{
    public partial class KontrakStatus : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
            }

            FillHistori();
            Js.Confirm(this, "Lanjutkan proses edit status kontrak?");
        }

        private void Fill()
        {
            //string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            string strSql = "SELECT "
                + " A.NoKontrak"
                + ",A.TglKontrak"
                + ",A.Status"
                + ",A.NoUnit"
                + ",B.Nama AS Cs"
                + ",C.Nama + ' ' + C.Principal AS Ag"
                + ",F.ST"
                + ",D.NoPPJB"
                + ",D.PPJB"
                + ",E.NoAJB"
                + ",E.AJB"
                + ",D.PPJB"
                + ",G.NoIMB"
                + ",H.NoSertifikat"
                + " FROM MS_KONTRAK A INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer "
                + " INNER JOIN MS_AGENT C ON A.NoAgent = C.NoAgent"
                + " LEFT JOIN MS_PPJB D ON A.NoKontrak = D.NoKontrak"
                + " LEFT JOIN MS_AJB E ON A.NoKontrak = E.NoKontrak"
                + " LEFT JOIN MS_BAST F ON A.NoKontrak = F.NoKontrak"
                + " LEFT JOIN MS_IMB G ON A.NoKontrak = G.NoKontrak"
                + " LEFT JOIN MS_SERTIFIKAT H ON A.NoKontrak = H.NoKontrak"
                + " WHERE A.NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                string status = rs.Rows[0]["Status"].ToString();
                switch (status)
                {
                    case "A":
                        statusA.Checked = true;
                        statusB.Enabled = false;
                        break;
                    case "B":
                        statusB.Checked = true;
                        break;
                        //case "E":
                        //    statusE.Checked = true;
                        //    break;
                }

                if (rs.Rows[0]["ST"].ToString() != "D")
                    resetst.Enabled = false;
                if (rs.Rows[0]["NoPPJB"].ToString() == "")
                    resetPPJB.Enabled = false;
                if (rs.Rows[0]["NoAJB"].ToString() == "")
                    resetAJB.Enabled = false;
            }
        }

        private void FillHistori()
        {
            string NoStock = Db.SingleString("SELECT NoStock FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            //string strSql = "SELECT MS_KONTRAK.*,MS_CUSTOMER.Nama AS Cs FROM MS_KONTRAK "
            //    + " INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
            //    + " WHERE NoStock = '" + NoStock + "' ORDER BY NoKontrak";

            string strSql = "SELECT "
                + " A.NoKontrak"
                + ",A.TglKontrak"
                + ",A.Status"
                + ",A.NoUnit"
                + ",B.Nama AS Cs"
                + ",C.Nama + ' ' + C.Principal AS Ag"
                + ",F.ST"
                + ",D.NoPPJB"
                + ",D.PPJB"
                + ",E.NoAJB"
                + ",E.AJB"
                + ",D.PPJB"
                + ",G.NoIMB"
                + ",H.NoSertifikat"
                + " FROM MS_KONTRAK A INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer "
                + " INNER JOIN MS_AGENT C ON A.NoAgent = C.NoAgent"
                + " LEFT JOIN MS_PPJB D ON A.NoKontrak = D.NoKontrak"
                + " LEFT JOIN MS_AJB E ON A.NoKontrak = E.NoKontrak"
                + " LEFT JOIN MS_BAST F ON A.NoKontrak = F.NoKontrak"
                + " LEFT JOIN MS_IMB G ON A.NoKontrak = G.NoKontrak"
                + " LEFT JOIN MS_SERTIFIKAT H ON A.NoKontrak = H.NoKontrak"
                + " WHERE A.NoStock = '" + NoStock + "'"
                + " ORDER BY A.NoKontrak";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                if (rs.Rows[i]["NoKontrak"].ToString() == NoKontrak)
                    r.ForeColor = Color.Blue;

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            bool editGagal = false;
            bool adaedit = false;

            string statusLama = Db.SingleString("SELECT Status FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            string statusBaru = "";
            if (statusA.Checked) statusBaru = "A";
            if (statusB.Checked) statusBaru = "B";
            //            if(statusE.Checked) statusBaru = "E";

            if (statusBaru != statusLama)
            {
                if (statusLama == "C")
                    Db.Execute("EXEC spKontrakUndoPanjang '" + NoKontrak + "'");
                if (statusLama == "B")
                    Db.Execute("EXEC spKontrakUndoBatal '" + NoKontrak + "'");

                //if (statusBaru == "E")
                //{
                //    Db.Execute("UPDATE MS_KONTRAK SET Status = 'E' WHERE NoKontrak = '" + NoKontrak + "' ");
                //}

                //if (statusBaru == "A" && statusLama == "E")
                //{
                //    Db.Execute("UPDATE MS_KONTRAK SET Status = 'A' WHERE NoKontrak = '" + NoKontrak + "' ");
                //}

                string statUpdate = Db.SingleString("SELECT Status FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                if (statUpdate != statusLama)
                    adaedit = true;
                else
                {
                    //tidak berhasil update
                    err.Text = "<b>Edit Status Gagal...</b>";
                    editGagal = true;
                }
            }

            if (!editGagal)
            {
                if (resetst.Checked)
                {
                    //reset serah terima
                    adaedit = true;
                    Db.Execute("EXEC spKontrakUndoST '" + NoKontrak + "'");
                    Db.Execute("DELETE FROM MS_BAST WHERE NoKontrak = '" + NoKontrak + "'");
                }
            }

            if (!editGagal)
            {
                if (resetPPJB.Checked)
                {
                    //reset PPJB
                    adaedit = true;
                    Db.Execute("EXEC spKontrakUndoPPJB '" + NoKontrak + "'");
                    Db.Execute("DELETE FROM MS_PPJB WHERE NoKontrak = '" + NoKontrak + "'");

                }
            }

            if (!editGagal)
            {
                if (resetAJB.Checked)
                {
                    //reset AJB
                    adaedit = true;
                    Db.Execute("EXEC spKontrakUndoAJB '" + NoKontrak + "'");
                    Db.Execute("DELETE FROM MS_AJB WHERE NoKontrak = '" + NoKontrak + "'");
                }
            }

            if (!editGagal)
            {
                if (adaedit)
                {
                    System.Text.StringBuilder x = new System.Text.StringBuilder();

                    if (statusBaru != statusLama)
                        x.Append("<br>***EDIT STATUS : " + statusLama + " --> " + statusBaru);
                    if (resetPPJB.Checked)
                        x.Append("<br>***RESET PPJB");
                    if (resetAJB.Checked)
                        x.Append("<br>***RESET AJB");
                    if (resetst.Checked)
                        x.Append("<br>***RESET SERAH TERIMA");

                    //logfile
                    DataTable rs = Db.Rs("SELECT"
                        + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                        + ",MS_KONTRAK.NoUnit AS [Unit]"
                        + ",MS_CUSTOMER.Nama AS [Customer]"
                        + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                        + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                        + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                    string ket = Cf.LogCapture(rs)
                        + x.ToString();

                    Db.Execute("EXEC spLogKontrak "
                        + " 'STATUS'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    RegisterStartupScript("closerfr"
                        , "<script language='javascript'>"
                        + " dialogArguments.location.href='KontrakEdit.aspx?done=1&NoKontrak=" + NoKontrak + "';"
                        + " window.close();"
                        + "</script>"
                        );
                }
                else
                    Js.Close(this);
            }
            Js.Close(this);
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
    }
}
