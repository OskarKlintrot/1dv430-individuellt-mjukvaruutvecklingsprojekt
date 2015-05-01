using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HeatingWebApplication
{
    public partial class History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetCurrentTime(string name)
        {
            return "Hello " + name + "!" + Environment.NewLine + "The Current Time is: "
                + DateTime.Now.ToString();
        }
    }
}