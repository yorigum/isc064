using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;

namespace ISC064.FINANCEAR
{
    public partial class VAEksporSinarmas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {

            }

            FillCust();
        }

        protected void FillCust()
        {
            DateTime Dari = Convert.ToDateTime(Request.QueryString["dari"]);
            DateTime Sampai = Convert.ToDateTime(Request.QueryString["sampai"]);

            dari.Text = Cf.Tgl(Dari);
            sampai.Text = Cf.Tgl(Sampai);

            DataTable rs = Db.Rs("SELECT "
                + " a.* "
                + ", c.NoVA "
                + ", c.NoUnit "
                + ", c.NoCustomer "
                + ", c.NoStock "
                + ", (a.NilaiTagihan - (Select ISNULL(SUM(d.NilaiPelunasan), 0) from " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN d where a.NoKontrak = d.NoKontrak and a.NoUrut = d.NoTagihan and a.Tipe != 'BF')) AS SisaTagihan"
                + ", (select SUBSTRING(NoVA, 5, 15) from " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK where NoKontrak = a.NoKontrak) AS NoVAstr"
                + ", (select Lokasi from " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT where NoStock = c.NoStock) AS Lokasi"
                + ", (select Lantai from " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT where NoStock = c.NoStock) AS Blok"
                + ", (select Nomor from " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT where NoStock = c.NoStock) AS Nomor"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK C ON a.NoKontrak = c.NoKontrak"
                + " WHERE c.Status = 'A' AND c.NoVa != '' AND a.Tipe != 'BF'"
                + " AND a.NilaiTagihan - (Select ISNULL(SUM(d.NilaiPelunasan), 0) from " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN d where a.NoKontrak = d.NoKontrak and a.NoUrut = d.NoTagihan and a.Tipe != 'BF') != 0"
                + " AND CONVERT(varchar,a.TglJT,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,a.TglJT,112) <= '" + Cf.Tgl112(Sampai) + "'"
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

                string NamaCs = Db.SingleString("select ISNULL(Nama, '') from " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER where NoCustomer = '" + rs.Rows[i]["NoCustomer"] + "'");
                string UnitVA = rs.Rows[i]["Lokasi"].ToString() + rs.Rows[i]["Blok"].ToString() + rs.Rows[i]["Nomor"].ToString();

                //string pattern = @"^[.,:;!?€¥£¢$-]{0,2048}$";
                string[] input = { NamaCs };

                string pattern = "(\\.|,|>|=)";

                string NamaBuatVA = "";
                foreach (string name in input)
                {
                    NamaBuatVA = Regex.Replace(name, pattern, String.Empty);
                }

                if (NamaBuatVA.Length > 30)
                {
                    NamaBuatVA = NamaBuatVA.Substring(0, 30);
                }

                c = new HtmlTableCell();
                c.InnerHtml = (i + 1).ToString();
                c.ID = "pk_" + i;
                c.Attributes["title"] = rs.Rows[i]["NoVAstr"] + ";" + Math.Round(Convert.ToDecimal(rs.Rows[i]["SisaTagihan"])) + ";" + NamaBuatVA + ";" + rs.Rows[i]["NamaTagihan"].ToString() + " " + UnitVA;
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = NamaBuatVA;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoVA"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = "PT PANAHOME DELTAMAS INDONESIA";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = "SAVASA";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NamaTagihan"].ToString() + " " + rs.Rows[i]["NoUnit"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(rs.Rows[i]["SisaTagihan"]);
                tr.Cells.Add(c);
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            string tgl = DateTime.Now.ToString("MMddyyyyhhmmss");
            string sFileName = System.IO.Path.GetRandomFileName();
            string sGenName = "ADD-8151-" + DateTime.Today.ToString("yyyyMMdd") + "-001.txt";

            int j = 0;

            using (System.IO.StreamWriter SW = new System.IO.StreamWriter(
                   Server.MapPath("Template/" + sFileName + ".txt")))
            {
                int index = 0;
                SW.WriteLine("Name|Member ID|Company Unit|Address|Description|Currency|Amount");
                foreach (Control tr in list.Controls)
                {
                    HtmlTableCell c = (HtmlTableCell)list.FindControl("pk_" + index);

                    if (c != null)
                    {
                        string[] aa = c.Attributes["title"].Split(';');

                        string nourut = (index + 1).ToString().PadLeft(6, '0');
                        string nova = aa[0];
                        string nama = aa[2];
                        string namatagihan = aa[3];
                        string nilai = aa[1];

                        SW.WriteLine(nama + "|" + nova + "|" + "PT PANAHOME DELTAMAS INDONESIA" + "|" + "SAVASA" + "|" + namatagihan + "|IDR|" + nilai + "");
                    }

                    index++;

                }
                //SW.WriteLine(index.ToString());
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
