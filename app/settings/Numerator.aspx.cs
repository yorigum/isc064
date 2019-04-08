using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{
    public partial class Numerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Act.Pass();
            //Act.NoCache();
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Init();
                fill();
            }
            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Berhasil...";
            }
        }

        protected void Init()
        {
            int cek = Db.SingleInteger("SELECT COUNT(*) FROM Numerator WHERE Project = '" + project.SelectedValue + "'");
            if (cek == 0)
            {
                DataTable rs = Db.Rs("SELECT * FROM Numerator WHERE Project = ''");
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    decimal SN = Db.SingleDecimal("SELECT TOP 1 SN FROM Numerator ORDER BY SN DESC");
                    Db.Execute("INSERT INTO Numerator (Kode,Modul,Nama,ResetNum,DigitNum,FormatBulan,FormatTahun,Pemisah,Prefix,Komposisi,SN,Project) VALUES "
                            + "('" + rs.Rows[i]["Kode"] + "','" + rs.Rows[i]["Modul"] + "','" + rs.Rows[i]["Nama"] + "'," + rs.Rows[i]["ResetNum"] + ","
                            + "" + rs.Rows[i]["DigitNum"] + "," + rs.Rows[i]["FormatBulan"] + "," + rs.Rows[i]["FormatTahun"] + ",'" + rs.Rows[i]["Pemisah"] + "',"
                            + "'" + rs.Rows[i]["Prefix"] + "','" + rs.Rows[i]["Komposisi"] + "'," + (SN + 1) + ",'" + project.SelectedValue + "')");
                }
            }
        }
        protected void fill()
        {
            DataTable rs = Db.Rs("SELECT * FROM Numerator WHERE Project = '" + project.SelectedValue + "' ORDER BY Modul");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                var r = rs.Rows[i];

                HtmlTableRow tr = new HtmlTableRow();
                list.Controls.Add(tr);

                HtmlTableCell c;

                c = new HtmlTableCell();
                c.InnerHtml = "<a onclick=\"PopNumerator('" + r["SN"] + "')\">" + r["Nama"] + "</a>";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = r["Modul"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = r["Komposisi"].ToString();
                tr.Cells.Add(c);
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            list.Controls.Clear();
            Init();
            fill();
        }
    }
}