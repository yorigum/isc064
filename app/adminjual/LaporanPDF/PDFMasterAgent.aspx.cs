using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL.Laporan
{
    public partial class MasterAgent : System.Web.UI.Page
    {
        private string NoCustomer { get { return (Request.QueryString["NoCustomer"]); } }
        private string Principal { get { return (Request.QueryString["principal"]); } }
        private string Input { get { return Request.QueryString["input"]; } }
        private string Tipe { get { return Request.QueryString["tipe"]; } }
        private string Nama { get { return (Request.QueryString["nama"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string IP { get { return (Request.QueryString["ip"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusI { get { return (Request.QueryString["status_i"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }



        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            FillHeader();
            Fill();
        }

        private void FillHeader()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);



            string status = "";
            if (StatusS != "") status = StatusS;
            if (StatusA != "") status = StatusA;
            if (StatusI != "") status = StatusI;

            Rpt.SubJudul(x
                , "Status : " + status
                );

            Rpt.SubJudul(x
                , "Nama : " + Nama.Replace("-", ",").TrimEnd(',')
                );

            if (Input != "SEMUA")
            {
                string[] str = Input.Split('-');


                Rpt.SubJudul(x
                    , "Periode Input : " + Cf.NamaBln(str[1]) + " " + str[0]
                    );
            }
            else
            {
                Rpt.SubJudul(x
                  , "Periode Input : SEMUA"
                  );
            }

            Rpt.SubJudul(x
                , "Principal : " + Principal
                );

            Rpt.SubJudul(x
                , "Tipe : " + Tipe
                );

            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            string nStatus = "";
            if (StatusA != "") nStatus = " AND A.Status = 'A'";
            if (StatusI != "") nStatus = " AND A.Status = 'I'";


            //change parameter
            string str = String.Empty;
            str = Nama.Replace("-", "").ToLower();
            char[] characters = str.ToCharArray();


            string str2 = "";
            foreach (var t in characters)
            {
                str2 += "'" + t + "',";
            }
            str2 = str2.TrimEnd(',');


            string aq = "";
            if (Nama != "")
                aq = aq + " AND LEFT(A.Nama,1) IN (" + str2 + ")";


            string nInput = "";
            if (Input != "SEMUA")
            {
                string[] z = Input.Split('-');
                nInput = " AND YEAR(A.TglInput) = " + z[0]
                    + " AND MONTH(A.TglInput) = " + z[1];
            }

            string nTipe = "";
            if (Tipe != "SEMUA")
            {
                nTipe = " AND A.SalesTipe = '" + Cf.Str(Tipe) + "'";
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND A.Project IN ('" + Project.Replace(",", "','") + "')";


            string strSql = "SELECT "
                + " A.NoAgent"
                //+ ",A.Nama"
                + ",A.KodeSales AS [Kode Sales]"
                + ",A.Nama AS [Nama Lengkap]"
                + ",A.SalesTipe"
                + ",A.SalesLevel"
                + ",A.Atasan"
                + ",A.Alamat"
                + ",A.Kontak"
                + ",A.Handphone"
                + ",A.Whatsapp"
                + ",A.NPWP"
                + ",A.Email"
                + ",A.RekBank"
                + ",A.Rekening"
                + ",A.AtasNama"
                + ",A.TglInput"
                + ",A.Status"
                + ",A.Principal"
                //+ ",Skema0"
                + ",B.Tipe AS [Tipe Agent]"
                + ",C.Nama AS [Nama Agent]"
                + " FROM MS_AGENT A INNER JOIN REF_AGENT_TIPE B ON A.SalesTipe = B.ID"
                + " INNER JOIN REF_AGENT_LEVEL C ON A.SalesLevel = C.LevelID"
                + " WHERE 1=1 "
                + nProject
                + nStatus
                + aq
                + nInput
                + nTipe
                + " ORDER BY A.Nama, A.NoAgent";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditAgent('" + rs.Rows[i]["NoAgent"] + "')";

                string inaktif = "";
                if (rs.Rows[i]["Status"].ToString() == "I")
                    inaktif = " **";

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Tipe Agent"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama Agent"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE Atasan = '" + rs.Rows[i]["Atasan"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoAgent"].ToString().PadLeft(5, '0');
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama Lengkap"].ToString()
                    + inaktif;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Alamat"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Email"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Kontak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Handphone"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Whatsapp"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Rekening"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["RekBank"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["AtasNama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);
                rpt.Rows.Add(r);
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
