using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HeatingWebApplication
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogInButton_Click(object sender, EventArgs e)
        {
            var username = "appUser";
            var password = "1Br@Lösen=rd?";
            if (UsernameTextBox.Text == username && PasswordTextBox.Text == password)
            {
                Session["loginSuccess"] = true;
                if (String.IsNullOrEmpty(Request.QueryString["ReturnURL"]))
                {
                    Response.Redirect("~/");
                }
                Response.Redirect(Request.QueryString["ReturnURL"]);
            }
        }
    }
}