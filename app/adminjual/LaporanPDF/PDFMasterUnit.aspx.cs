using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL.Laporan
{
    public partial class MasterUnit : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Jenis { get { return Request.QueryString["jenis"]; } }
        private string Perusahaan { get { return Request.QueryString["pers"]; } }
        private string Project { get { return Request.QueryString["project"]; } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string StatusH { get { return (Request.QueryString["status_h"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }



        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            Header();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            if (StatusA != "")
                Rpt.SubJudul(x, "Status : " + StatusA);
            else if (StatusB != "")
                Rpt.SubJudul(x, "Status : " + StatusB);
            else if (StatusH != "")
                Rpt.SubJudul(x, "Status : " + StatusH);
            else
                Rpt.SubJudul(x, "Status : " + StatusS);

            Rpt.SubJudul(x
                , "Tipe : " + Jenis
                );

            Rpt.SubJudul(x
                , "Lokasi : " + Lokasi
                );

            Rpt.SubJudul(x
                , "Project : " + Project
                );

            Rpt.SubJudul(x
                , "Perusahaan : " + Perusahaan
                );

            string legend = "Status: A = Available / S = Sold / H = Hold."
                            + "<br />"
                            + "Luas dalam meter persegi.Harga price list adalah dalam rupiah.";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            string nStatus = "";
            if (StatusA != "") nStatus = " AND Status = 'A'";
            if (StatusB != "") nStatus = " AND Status = 'B'";
            if (StatusH != "") nStatus = " AND Status = 'H'";

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND Lokasi = '" + Lokasi + "'";
            }

            string str = String.Empty;

            string nProject = "";
            if (Project != "SEMUA")
            {
                nProject = " AND Project IN ('" + Project.Replace(",", "','") + "')";
            }

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA")
            {
                nPerusahaan = " AND MS_KONTRAK.Pers = '" + Cf.Str(Perusahaan) + "'";
            }


            string[] str3 = Jenis.Replace(".", " ").Split('-');
            for (int i = 0; i < str3.Length; i++)
            {
                if (i == str3.Length - 1)
                {
                    str += "'" + str3[i] + "'";
                }
                else
                {
                    str += "'" + str3[i] + "',";
                }
            }

            //string str2 = "";
            //foreach (var t in characters)
            //{
            //    str2 += "." + t + "',";
            //}
            //str2 = str2.TrimEnd(',');



            decimal t1 = 0;

            string strSql = "SELECT "
                + "	NoStock"
                + ",Jenis"
                + ",Lokasi"
                + ",NoUnit"
                + ",Luas"
                + ",PriceList"
                + ",PriceListMin"
                + ",TglInput"
                + ",Status"
                + ",Panorama"
                + ",LuasSG"
                + ",LuasNett"
                + ",JenisProperti"
                + ",HadapAtrium"
                + ",HadapEntrance"
                + ",HadapEskalator"
                + ",HadapLift"
                + ",HadapParkir"
                + ",HadapAxis"
                + " FROM MS_UNIT"
                + " WHERE Jenis IN (" + str + ")"
                + nProject
                + nStatus
                + nLokasi
                //+ Periode
                ;
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditUnit('" + rs.Rows[i]["NoStock"] + "')";

                DateTime p = Convert.ToDateTime(rs.Rows[i]["TglInput"]);

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoStock"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM REF_LOKASI WHERE Lokasi = '" + rs.Rows[i]["Lokasi"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["JenisProperti"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                decimal luassg = Convert.ToDecimal(rs.Rows[i]["LuasSG"]);
                c.Text = Math.Round(luassg, 2).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                decimal luasnet = Convert.ToDecimal(rs.Rows[i]["LuasNett"]);
                c.Text = Math.Round(luasnet, 2).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (rs.Rows[i]["HadapAtrium"].ToString() == "1")
                {
                    c.Text = "Hadap Atrium";
                }
                else if (rs.Rows[i]["HadapEntrance"].ToString() == "1")
                {
                    c.Text = "Hadap Eskalator";
                }
                else if (rs.Rows[i]["HadapEskalator"].ToString() == "1")
                {
                    c.Text = "Hadap Eskalator";
                }

                else if (rs.Rows[i]["HadapLift"].ToString() == "1")
                {
                    c.Text = "Hadap Lift";
                }
                else if (rs.Rows[i]["HadapParkir"].ToString() == "1")
                {
                    c.Text = "Hadap Parkir";
                }
                else if (rs.Rows[i]["HadapAxis"].ToString() == "1")
                {
                    c.Text = "Hadap Axis";
                }
                else
                {
                    c.Text = "";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Panorama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (rs.Rows[i]["Status"].ToString() == "B")
                {
                    c.Text = "S";
                }
                else if(rs.Rows[i]["Status"].ToString() == "A")
                {
                    c.Text = "A";
                }
                else
                {
                    c.Text = "H";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT NoKontrak FROM MS_KONTRAK WHERE NoStock = '" + rs.Rows[i]["NoStock"].ToString() + "'" + nPerusahaan + " AND Status = 'A'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 = t1 + (decimal)rs.Rows[i]["PriceList"];

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", t1);
            }
        }

        private void SubTotal(string txt, decimal t1)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            //c = Rpt.Foot();
            //c.Text = Cf.Num(t1);
            //c.HorizontalAlign = HorizontalAlign.Right;
            //r.Cells.Add(c);

            rpt.Rows.Add(r);
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
