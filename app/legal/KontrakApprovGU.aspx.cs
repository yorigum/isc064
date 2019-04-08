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

			if(!Page.IsPostBack)
			{
                InitForm();      
			}

            Fill();
            FeedBack();
			if(frm.Visible) Js.Confirm(this, "Jalankan prosedur APPROVAL Pindah Unit?\\nProses ini akan merubah data kepemilikan unit properti.");
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
            if (rs.Rows.Count == 0)
                save.Enabled = false;

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

                string unitbaru = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock='"+rs.Rows[i]["TempGU"].ToString()+"'");
                
                c = new HtmlTableCell();
                c.InnerHtml = unitbaru + " (" + rs.Rows[i]["TempGU"].ToString() + ")";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["TempBiayaGU"]));
                c.Align = "right";
                tr.Cells.Add(c);
 
            }
               
		}

        private bool datavalid()
		{
			bool x = true;
            string s = "";

            if(!Cf.isTgl(tglot))
            {
                x=false;
                if(s=="") s = tglot.ID;
                tglotc.Text = "Format Tanggal";
            }
            else
                tglotc.Text ="";

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
			if(datavalid())
			{

                int index = 0;
                foreach (Control tr in list.Controls)
                {
                    HtmlTableCell c = (HtmlTableCell)list.FindControl("pk_" + index);
                    CheckBox cb = (CheckBox)list.FindControl("nokontrak_" + index);
                    DateTime Tgl = Convert.ToDateTime(tglot.Text);

                    if (c != null)
                    {
                        SaveApproval(c.Attributes["title"], cb, Tgl);
                    }

                    index++;
                }
                Response.Redirect("KontrakApprovGU.aspx?done=yes");
            }
		}

        private void SaveApproval(String NoKontrak, CheckBox cb, DateTime Tgl)
        {
            if (cb.Checked)
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

                Db.Execute("UPDATE MS_KONTRAK "
                            + " SET NilaiPPN='" + NilaiPPN + "'"
                            + ", NilaiKontrak='" + NilaiKontrak + "'"
                            //+ ", PPNPemerintah = '" + PPNDitanggungPemerintah + "'"
                            + ", ApprovalGU = '"+Convert.ToBoolean(0)+"'"
                            + ", Revisi = Revisi + 1"
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

                string Ket = Cf.LogCompare(rsBef, rsAft)
                    + "<br>Biaya Administrasi : " + Cf.Num(biaya)
                    + "<br>Tgl Approval GU : " + Cf.Day(Convert.ToDateTime(tglot.Text));
                    ;

                Db.Execute("EXEC spLogKontrak "
                    + " 'APR-GU'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoKontrak + "'"
                    );
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
