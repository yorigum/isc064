using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class PrintSkema : System.Web.UI.Page
	{
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string CaraBayar { get { return (Request.QueryString["carabayar"]); } }
        private string NoStok { get { return (Request.QueryString["nostok"]); } }
        private string Nilai { get { return (Request.QueryString["nilai"]); } }
        private string Tgl { get { return (Request.QueryString["tgl"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
		{
            Jadwal();
        }

        private void Jadwal()
        {
            DateTime tgl = Convert.ToDateTime(Tgl);
            decimal netto = Convert.ToDecimal(Nilai);

            //string[,] x = Func.Breakdown(
            //    Convert.ToInt32(carabayar.SelectedValue), netto, tgl);

            decimal t = 0;
            //for (int i = 0; i <= x.GetUpperBound(0); i++)
            //{
            //    if (!Response.IsClientConnected) break;
            int index = 0;
            var d = Func.ListTagihan(CaraBayar, netto, tgl);
            foreach (var rb in d)
            {

                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (index + 1) + ".";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rb.NamaTagihan;
                r.Cells.Add(c);

                //jadwal
                c = new TableCell();
                c.Text = Cf.Day(rb.TglJt);
                r.Cells.Add(c);

                //nominal
                c = new TableCell();
                c.Text = Cf.Num(rb.NilaiTagihan);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                if (rb.PotongBF)
                    c.Text = "MIN";

                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rb.TipeTagihan;
                c.Visible = false;
                r.Cells.Add(c);

                t = t + Convert.ToDecimal(rb.NilaiTagihan);
                index++;
                Rpt.Border(r);
                rpt.Rows.Add(r);
                int CountJum = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM REF_SKEMA_DETAIL WHERE Nomor='" + CaraBayar + "'");
                if (index == CountJum)
                {
                    r = new TableRow();

                    c = new TableCell();
                    c.Text = "Grand Total";
                    c.ColumnSpan = 2;
                    c.Font.Bold = true;
                    c.Font.Size = 10;
                    r.Cells.Add(c);

                    //c = new TableCell();
                    //c.Text = Cf.Num(d.Sum(p => p.NilaiTagihan));
                    //c.ColumnSpan = 2;
                    //c.HorizontalAlign = HorizontalAlign.Right;
                    //c.Font.Bold = true;
                    //c.Font.Size = 10;
                    //r.Cells.Add(c);

                    rpt.Rows.Add(r);
                }
            }
        }

        //private void BelumPrint()
        //{
        //    DataTable rs = Db.Rs("SELECT TglTunggakan, COUNT(NoTunggakan) AS BlmPrint FROM MS_TUNGGAKAN "
        //        + " WHERE PrintST = 0 "
        //        + " GROUP BY TglTunggakan "
        //        + " ORDER BY TglTunggakan"
        //        );
        //    for (int i = 0; i < rs.Rows.Count; i++)
        //    {
        //        if (!Response.IsClientConnected) break;

        //        TableRow r = new TableRow();
        //        TableCell c;

        //        c = new TableCell();
        //        c.Text = Cf.Day(rs.Rows[i]["TglTunggakan"]);
        //        r.Cells.Add(c);

        //        c = new TableCell();
        //        c.Text = ":";
        //        r.Cells.Add(c);

        //        c = new TableCell();
        //        c.Text = rs.Rows[i]["BlmPrint"].ToString();
        //        r.Cells.Add(c);

        //        belumprint.Rows.Add(r);
        //    }
        //}

        //private void InitForm()
        //{
        //    dari.Text = Cf.Day(DateTime.Today);
        //    sampai.Text = Cf.Day(DateTime.Today);
        //}

        //private bool valid()
        //{
        //    string s = "";
        //    bool x = true;

        //    if (!Cf.isTgl(dari))
        //    {
        //        x = false;
        //        if (s == "") s = dari.ID;
        //        daric.Text = "Tanggal";
        //    }
        //    else
        //        daric.Text = "";

        //    if (!Cf.isTgl(sampai))
        //    {
        //        x = false;
        //        if (s == "") s = sampai.ID;
        //        sampaic.Text = "Tanggal";
        //    }
        //    else
        //        sampaic.Text = "";

        //    if (!x && s != "")
        //    {
        //        RegisterStartupScript("err"
        //            , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
        //    }

        //    return x;
    //    //}
    //    private void Fill()
    //    {
    //        //DateTime Dari = Convert.ToDateTime(dari.Text);
    //        //DateTime Sampai = Convert.ToDateTime(sampai.Text);

    //        string dari = Request.QueryString["Dari"];
    //        string sampai = Request.QueryString["Sampai"];

    //        DateTime Dari = Convert.ToDateTime(dari);
    //        DateTime Sampai = Convert.ToDateTime(sampai);
    //        if (Dari > Sampai)
    //        {
    //            DateTime x = Sampai;
    //            Sampai = Dari;
    //            Dari = x;
    //        }

    //        DataTable rs = Db.Rs("SELECT "
    //            + " NoTunggakan"
    //            + " FROM MS_TUNGGAKAN"
    //            + " WHERE 1=1"
    //            + " AND CONVERT(varchar,TglTunggakan,112) >= '" + Cf.Tgl112(Dari) + "'"
    //            + " AND CONVERT(varchar,TglTunggakan,112) <= '" + Cf.Tgl112(Sampai) + "'"
    //            + " ORDER BY NoTunggakan");

    //        for (int i = 0; i < rs.Rows.Count; i++)
    //        {
    //            if (!Response.IsClientConnected) break;

    //            Print((int)rs.Rows[i]["NoTunggakan"]);

    //            if (i != rs.Rows.Count - 1)
    //            {
    //                Label pb = new Label();
    //                pb.Text = "<div style='page-break-after:always'></div>";
    //                list.Controls.Add(pb);
    //            }
    //        }
    //    }

    //    private void Print(int NoTunggakan)
    //    {
    //        //increment
    //        Db.Execute("UPDATE MS_TUNGGAKAN SET PrintST = PrintST + 1 "
    //            + " WHERE NoTunggakan = " + NoTunggakan);

    //        //Logfile
    //        DataTable rs = Db.Rs("SELECT "
    //            + " CONVERT(varchar, TglTunggakan, 106) AS [Tanggal]"
    //            + ",Tipe"
    //            + ",Ref AS [Ref.]"
    //            + ",Unit"
    //            + ",Customer"
    //            + ",Total"
    //            + ",LevelTunggakan AS [Level]"
    //            + " FROM MS_TUNGGAKAN WHERE NoTunggakan = " + NoTunggakan);

    //        Db.Execute("EXEC spLogTunggakan"
    //            + " 'P-ST'"
    //            + ",'" + Act.UserID + "'"
    //            + ",'" + Act.IP + "'"
    //            + ",'" + Cf.LogCapture(rs) + "'"
    //            + ",'" + NoTunggakan.ToString().PadLeft(7, '0') + "'"
    //            );

    //        //Template
    //        PrintSTTemplate uc = (PrintSTTemplate)Page.LoadControl("PrintSTTemplate.ascx");
    //        uc.NoTunggakan = NoTunggakan.ToString();
    //        list.Controls.Add(uc);
    //    }
    }
}
