using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web;
using System.Web.UI;

namespace ISC064.FINANCEAR
{
    public partial class VADownload : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {

            }

            FillTag();


        }

        protected void FillCust()
        {
            DataTable rs = Db.Rs("SELECT a.NoVA, a.NoUnit, b.Nama, b.Status FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                    + " WHERE a.NoVA <> '' "
                    + " ORDER BY a.NoUnit ASC"
                    );

            if (rs.Rows.Count == 0)
                save.Enabled = false;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow tr;
                HtmlTableCell c;
                RadioButtonList rb;

                tr = new HtmlTableRow();
                list.Controls.Add(tr);

                c = new HtmlTableCell();
                c.InnerHtml = (i + 1).ToString();
                c.ID = "pk_" + i;
                c.Attributes["title"] = rs.Rows[i]["NoVA"] + ";" + rs.Rows[i]["Nama"];
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Nama"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Status"].ToString();
                c.Align = "center";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoVA"].ToString();
                tr.Cells.Add(c);

                rb = new RadioButtonList();
                rb.ID = "perintah_" + i;
                rb.RepeatDirection = RepeatDirection.Horizontal;
                rb.Items.Add("ADD/UPDATE");
                //rb.Items[0].Attributes["class"] = "igroup-radio";
                rb.Items.Add("DEL");
                rb.SelectedValue = "ADD/UPDATE";

                c = new HtmlTableCell();
                c.Controls.Add(rb);
                tr.Cells.Add(c);
            }
        }

        protected void FillTag()
        {
            DataTable rs = Db.Rs("SELECT b.NoKontrak, b.NoUnit, b.Status, c.Nama AS Cust, b.NoVA"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER c ON b.NoCustomer = c.NoCustomer"
                + " AND b.NoVA <> ''"
                + " ORDER BY b.NoUnit ASC"
                );

            if (rs.Rows.Count == 0)
                save.Enabled = false;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                decimal Tagihan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN"
                    + " WHERE CONVERT(VARCHAR,TglJT,112) <= '" + Cf.Tgl112(Cf.AkhirBulan()) + "'"
                    + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'");
                DataTable rsx = Db.Rs("SELECT DISTINCT NamaTagihan FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN"
                    + " WHERE CONVERT(VARCHAR,TglJT,112) <= '" + Cf.Tgl112(Cf.AkhirBulan()) + "'"
                    + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'");
                string namatagihan = "";
                for (int a = 0; a < rsx.Rows.Count; a++)
                {
                    namatagihan += rsx.Rows[a]["NamaTagihan"] + ",";
                }
                decimal Bayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                                               + " AND NOTAGIHAN IN (SELECT NOURUT FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE CONVERT(VARCHAR,TglJT,112) <= '" + Cf.Tgl112(Cf.AkhirBulan()) + "'"
                                               + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "')");
                decimal Denda = Db.SingleDecimal("SELECT ISNULL(SUM(Denda-DendaReal-NilaiPutihDenda),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN"
                    + " WHERE CONVERT(VARCHAR,TglJT,112) <= '" + Cf.Tgl112(Cf.AkhirBulan()) + "'"
                    + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'");
                decimal Sisa = (Tagihan + Denda) - Bayar;

                HtmlTableRow tr;
                HtmlTableCell c;
                RadioButtonList rb;

                tr = new HtmlTableRow();
                list2.Controls.Add(tr);

                c = new HtmlTableCell();
                c.InnerHtml = (i + 1).ToString();
                c.ID = "pk2_" + i;
                c.Attributes["title"] = rs.Rows[i]["NoVA"] + ";" + Math.Round(Sisa) + ";" + rs.Rows[i]["Cust"] + ";" + namatagihan;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoKontrak"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                rb = new RadioButtonList();
                rb.ID = "order_" + i;
                rb.RepeatDirection = RepeatDirection.Horizontal;
                rb.Items.Add("ADD");
                rb.Items.Add("UPDATE");
                rb.Items.Add("DEL");
                rb.SelectedValue = "ADD";

                c = new HtmlTableCell();
                c.Controls.Add(rb);
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Cust"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoVA"].ToString();
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Sisa);
                c.Align = "right";
                c.NoWrap = true;
                tr.Cells.Add(c);
            }
        }

        protected void dlcust_Click(object sender, EventArgs e)
        {
            cust.Visible = dltag.Visible = true;
            tag.Visible = dlcust.Visible = false;
        }

        protected void dltag_Click(object sender, EventArgs e)
        {
            cust.Visible = dltag.Visible = false;
            tag.Visible = dlcust.Visible = true;
        }

        protected void save_Click(object sender, EventArgs e)
        {
            string tgl = Cf.Day(DateTime.Today);
            string sFileName = System.IO.Path.GetRandomFileName();
            string sGenName = "REQ-UPD-CUSTOMER" + tgl + ".txt";

            int j = 0;

            string trancode = "0005"; //add-update
            string instcode = "767   ";
            string deptcode = "0001";
            string currency = "360"; //IDR

            using (System.IO.StreamWriter SW = new System.IO.StreamWriter(
                   Server.MapPath("Template/" + sFileName + ".txt")))
            {
                int index = 0;
                foreach (Control tr in list.Controls)
                {
                    HtmlTableCell c = (HtmlTableCell)list.FindControl("pk_" + index);
                    RadioButtonList rb = (RadioButtonList)list.FindControl("perintah_" + index);

                    if (c != null)
                    {
                        string[] aa = c.Attributes["title"].Split(';');

                        string nourut = (index + 1).ToString().PadLeft(6, '0');
                        string nova = aa[0];
                        string nama = aa[1].PadRight(30, ' ');
                        string birthdate = "00000000";
                        string joindate = "00000000";

                        if (rb.SelectedValue == "DEL")
                            trancode = "0013";
                        else
                            trancode = "0005";

                        if (nama.Length > 30)
                            nama = nama.Remove(30);

                        SW.WriteLine(trancode + nourut + instcode + deptcode + nova + nova + nama + birthdate + joindate + currency);
                    }

                    index++;
                }

                j = index;

                SW.Close();
            }

            if (j > 0)
            {
                System.IO.FileStream fs = null;
                fs = System.IO.File.Open(Server.MapPath("Template/" +
                         sFileName + ".txt"), System.IO.FileMode.Open);
                byte[] btFile = new byte[fs.Length];
                fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" +
                                   sGenName);
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(btFile);
                Response.End();
            }
        }

        protected void save2_Click(object sender, EventArgs e)
        {
            string tgl = DateTime.Now.ToString("MMddyyyyhhmmss");
            string sFileName = System.IO.Path.GetRandomFileName();
            string sGenName = "FGVASHR2XXX" + tgl + ".txt";

            int j = 0;

            string trancode = "";
            string instcode = "757   ";
            string deptcode = "0001";
            string pymncode = "0001";
            string currency = "360";
            string priority = "01";

            using (System.IO.StreamWriter SW = new System.IO.StreamWriter(
                   Server.MapPath("Template/" + sFileName + ".txt")))
            {
                int index = 0;
                foreach (Control tr in list2.Controls)
                {
                    HtmlTableCell c = (HtmlTableCell)list.FindControl("pk2_" + index);
                    RadioButtonList rb = (RadioButtonList)list.FindControl("order_" + index);

                    if (c != null)
                    {
                        string[] aa = c.Attributes["title"].Split(';');

                        string nova = aa[0];
                        string nilai = aa[1];
                        if (nilai.Length < 13)
                        {
                            nilai = nilai.PadLeft(13, '0');
                        }
                        string nama = aa[2];
                        if (nama.Length > 30)
                        {
                            nama = nama.Substring(0, 30);
                        }
                        else if (nama.Length < 30)
                        {
                            nama = nama.PadRight(30);
                        }
                        string tagihan = aa[3];
                        if (tagihan.Length > 30)
                        {
                            tagihan = tagihan.Substring(0, 30);
                        }
                        else if (tagihan.Length < 30)
                        {
                            tagihan = tagihan.PadRight(30);
                        }
                        if (rb.SelectedValue == "DEL")
                            trancode = "D";
                        else if (rb.SelectedValue == "UPDATE")
                            trancode = "U";
                        else if (rb.SelectedValue == "ADD")
                            trancode = "A";



                        SW.WriteLine(trancode + instcode + deptcode + pymncode + nama + nova + nova + currency + nilai + tagihan + priority);

                    }

                    index++;
                }

                j = index;

                SW.Close();
            }

            if (j > 0)
            {
                System.IO.FileStream fs = null;
                fs = System.IO.File.Open(Server.MapPath("Template/" +
                         sFileName + ".txt"), System.IO.FileMode.Open);
                byte[] btFile = new byte[fs.Length];
                fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" +
                                   sGenName);
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(btFile);
                Response.End();
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
