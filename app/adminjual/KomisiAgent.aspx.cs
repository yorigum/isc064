using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ISC064.ADMINJUAL
{
    public partial class KomisiAgent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
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
        private void Fill()
        {
            agent.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            agent.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            agent.Attributes["onblur"] = "CalcBlur(this);";

            AgentReff.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            AgentReff.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            AgentReff.Attributes["onblur"] = "CalcBlur(this);";

            refferal.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            refferal.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            refferal.Attributes["onblur"] = "CalcBlur(this);";

            agent.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(Nilai,0) FROM REF_KOMISI WHERE Kategori='Agent'"));
            AgentReff.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(Nilai,0) FROM REF_KOMISI WHERE Kategori='AgentReff'"));
            refferal.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(Nilai,0) FROM REF_KOMISI WHERE Kategori='Reff'"));
        }
        protected void save_Click(object sender, EventArgs e)
        {
            decimal NilaiAgent = (agent.Text != "") ? Convert.ToDecimal(agent.Text) : 0;
            decimal NilaiAgenReff = (AgentReff.Text != "") ? Convert.ToDecimal(AgentReff.Text) : 0;
            decimal NilaiRefferal = (refferal.Text != "") ? Convert.ToDecimal(refferal.Text) : 0;
            Db.Execute("UPDATE REF_KOMISI SET Nilai=" + NilaiAgent + " WHERE Kategori='Agent'");
            Db.Execute("UPDATE REF_KOMISI SET Nilai=" + NilaiAgenReff + " WHERE Kategori='AgentReff'");
            Db.Execute("UPDATE REF_KOMISI SET Nilai=" + NilaiRefferal + " WHERE Kategori='Reff'");

            Response.Redirect("KomisiAgent.aspx?done=1");

        }
}
}