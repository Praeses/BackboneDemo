﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BackboneDemo
{
    public partial class _default : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            Page.Response.Redirect("Demo");
            base.OnPreInit(e);
        }

    }
}