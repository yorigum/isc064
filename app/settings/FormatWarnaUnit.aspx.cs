using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{

    public partial class FormatWarnaUnit : System.Web.UI.Page
    {
        private string ParamID { get { return "WarnaUnitJual" + project.SelectedValue; } }
        private string ParamID2 { get { return "WarnaUnitBooked" + project.SelectedValue; } }
        private string ParamID3 { get { return "WarnaUnitCancel" + project.SelectedValue; } }
        private string ParamID4 { get { return "WarnaUnitHold" + project.SelectedValue; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                init();
                fill();
            }
            FeedBack();
        }


        protected void fill()
        {
            string value = "SELECT Value FROM REF_PARAM WHERE ParamID = '" + ParamID + "'";
            DataTable rs = Db.Rs(value);
            if (rs.Rows.Count == 0)
            {
                jual.Value = "#ffffff";
                booked.Value = "#ffffff";
                cancel.Value = "#ffffff";
                hold.Value = "#ffffff";
            }
            else
            {
                if (!String.IsNullOrEmpty(rs.Rows[0]["Value"].ToString()))
                {
                    jual.Value = rs.Rows[0]["Value"].ToString();
                    //Response.Write(rs.Rows[0]["Value"].ToString());
                }
                else
                {
                    jual.Value = " ";
                }

                string value2 = "SELECT Value FROM REF_PARAM WHERE ParamID = '" + ParamID2 + "'";
                DataTable rs2 = Db.Rs(value2);

                if (!String.IsNullOrEmpty(rs2.Rows[0]["Value"].ToString()))
                {
                    booked.Value = rs2.Rows[0]["Value"].ToString();
                }
                else
                {
                    booked.Value = " ";
                }

                string value3 = "SELECT Value FROM REF_PARAM WHERE ParamID = '" + ParamID3 + "'";
                DataTable rs3 = Db.Rs(value3);

                if (!String.IsNullOrEmpty(rs3.Rows[0]["Value"].ToString()))
                {
                    cancel.Value = rs3.Rows[0]["Value"].ToString();
                }
                else
                {
                    cancel.Value = " ";
                }

                string value4 = "SELECT Value FROM REF_PARAM WHERE ParamID = '" + ParamID4 + "'";
                DataTable rs4 = Db.Rs(value4);

                if (!String.IsNullOrEmpty(rs4.Rows[0]["Value"].ToString()))
                {
                    hold.Value = rs4.Rows[0]["Value"].ToString();
                }
                else
                {
                    hold.Value = " ";
                }
            }

        }

        protected void init()
        {
            //Js.NumberFormat(batas);
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit berhasil..."
                        ;
                project.SelectedValue = Request.QueryString["project"];
                fill();
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {

            if (valid)
            {
                int ada = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM REF_PARAM WHERE ParamID = '" + ParamID + "'");
                int ada2 = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM REF_PARAM WHERE ParamID = '" + ParamID2 + "'");
                int ada3 = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM REF_PARAM WHERE ParamID = '" + ParamID3 + "'");
                int ada4 = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM REF_PARAM WHERE ParamID = '" + ParamID4 + "'");
                if (ada == 0) //kalau ga ada
                {
                    Db.Execute("INSERT INTO REF_PARAM(ParamID,Keterangan,Value) VALUES('" + ParamID + "', 'Format Warna Jual Unit.', '" + Convert.ToString(jual.Value) + "')");
                }
                else
                {
                    Db.Execute("UPDATE REF_PARAM SET Value = '" + Convert.ToString(jual.Value) + "' WHERE ParamID = '" + ParamID + "'");
                }
                if (ada2 == 0) //kalau ga ada
                {
                    Db.Execute("INSERT INTO REF_PARAM(ParamID,Keterangan,Value) VALUES('" + ParamID2 + "', 'Format Warna Booked Unit.', '" + Convert.ToString(booked.Value) + "')");
                }
                else
                {
                    Db.Execute("UPDATE REF_PARAM SET Value = '" + Convert.ToString(booked.Value) + "' WHERE ParamID = '" + ParamID2 + "'");
                }
                if (ada3 == 0) //kalau ga ada
                {
                    Db.Execute("INSERT INTO REF_PARAM(ParamID,Keterangan,Value) VALUES('" + ParamID3 + "', 'Format Warna Cancel Unit.', '" + Convert.ToString(cancel.Value) + "')");
                }
                else
                {
                    Db.Execute("UPDATE REF_PARAM SET Value = '" + Convert.ToString(cancel.Value) + "' WHERE ParamID = '" + ParamID3 + "'");
                }
                if (ada4 == 0) //kalau ga ada
                {
                    Db.Execute("INSERT INTO REF_PARAM(ParamID,Keterangan,Value) VALUES('" + ParamID4 + "', 'Format Warna Hold Unit.', '" + Convert.ToString(hold.Value) + "')");
                }
                else
                {
                    Db.Execute("UPDATE REF_PARAM SET Value = '" + Convert.ToString(hold.Value) + "' WHERE ParamID = '" + ParamID4 + "'");
                }
                Response.Redirect("FormatWarnaUnit.aspx?done=1&project="+project.SelectedValue);
            }
        }

        protected bool valid
        {
            get
            {
                bool x = true;

                //x = !Cf.isEmpty(jual) ? x : false;

                //if (!x)
                //    Js.Alert(this, "", "Harus diisi!.");

                //x = !Cf.isEmpty(booked) ? x : false;

                //if (!x)
                //    Js.Alert(this, "", "Harus diisi!.");

                //x = !Cf.isEmpty(cancel) ? x : false;

                //if (!x)
                //    Js.Alert(this, "", "Harus diisi!.");

                return x;
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}