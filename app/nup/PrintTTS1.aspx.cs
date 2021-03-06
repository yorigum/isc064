﻿using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace ISC064.NUP
{
    public partial class PrintTTS1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetTemplate();

            if (!Page.IsPostBack)
            {
                Fill();
            }
        }

        private void SetTemplate()
        {
            PrintTTSTemplate2 uc = (PrintTTSTemplate2)Page.LoadControl("PrintTTSTemplate2.ascx");
            uc.NoTTS = NoTTS;
            //uc.Project = Project;
            list.Controls.Add(uc);
        }

        private void Fill()
        {
            Tampil();
        }

        private void Tampil()
        {
            list.Visible = true;
        }

        private string NoTTS
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoTTS"]);
            }
        }
        //private string Project
        //{
        //    get
        //    {
        //        return Cf.Pk(Request.QueryString["project"]);
        //    }
        //}
    }
}