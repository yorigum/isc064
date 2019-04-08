using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{
    public partial class NumeratorFile : System.Web.UI.Page
    {
        protected string SN { get { return Request.QueryString["id"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                fill();
        }

        protected void fill()
        {
            DataTable rs = Db.Rs("SELECT * FROM Numerator WHERE SN = '" + SN + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");

            var r = rs.Rows[0];

            project.Text = r["Project"].ToString();
            prefix.Text = r["Prefix"].ToString();
            resetnum.SelectedValue = r["ResetNum"].ToString();
            digitnum.Text = r["DigitNum"].ToString();
            formatbulan.SelectedValue = r["FormatBulan"].ToString();
            formattahun.SelectedValue = r["FormatTahun"].ToString();
            pemisah.Text = r["Pemisah"].ToString();
            komposisi.Text = r["Komposisi"].ToString();
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if (valid)
            {
                DataTable rs = Db.Rs("SELECT * FROM Numerator WHERE SN = '" + SN + "'");

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");

                Db.Execute("UPDATE Numerator SET "
                    + " Prefix = '" + prefix.Text + "'"
                    + ",ResetNum = " + Convert.ToInt16(resetnum.SelectedValue)
                    + ",DigitNum = " + Convert.ToInt16(digitnum.Text)
                    + ",FormatBulan = " + Convert.ToInt16(formatbulan.Text)
                    + ",FormatTahun = " + Convert.ToInt16(formattahun.Text)
                    + ",Pemisah = '" + pemisah.Text + "'"
                    + ",Komposisi = '" + komposisi.Text + "'"
                    + " WHERE SN = '" + SN + "'"
                    );

                Js.CloseAndReload(this);
            }
        }

        protected bool valid
        {
            get
            {
                bool x = true;

                if (Cf.isEmpty(prefix))
                {
                    x = false;
                    Cf.MarkError(prefix);
                }
                else
                    Cf.ClrError(prefix);

                if (!Cf.isInt(digitnum))
                {
                    x = false;
                    Cf.MarkError(digitnum);
                }
                else
                    Cf.ClrError(digitnum);

                if (Cf.isEmpty(pemisah))
                {
                    x = false;
                    Cf.MarkError(pemisah);
                }
                else
                    Cf.ClrError(pemisah);

                string prefixinput = komposisi.Text.Trim().ToLower().Replace("{prefix}", "").Replace("{thn}", "").Replace("{bln}", "").Replace("{num}", "").Replace("{project}", "");
                if (prefixinput.Length > 0)
                {
                    x = false;
                    Cf.MarkError(komposisi);
                }
                else if (Cf.isEmpty(komposisi))
                {
                    x = false;
                    Cf.MarkError(komposisi);
                }
                else if(!komposisi.Text.ToLower().Contains("{project}"))
                {
                    x = false;
                    Cf.MarkError(komposisi);
                }
                else
                    Cf.ClrError(komposisi);

                if (!x)
                    Js.Alert(this, "", "Input data tidak sesuai.");

                return x;
            }
        }
    }
}