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
            WarningMessageLiteral.Text = Page.GetTempData("WarningMessage") as string;
            WarningMessagePanel.Visible = !String.IsNullOrWhiteSpace(WarningMessageLiteral.Text);

            if (Session["loginSuccess"] != null)
            {
                loginDiv.Visible = false;
            }
            else
            {
                logoutDiv.Visible = false;
            }
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
                else
                {
                    Response.Redirect(Request.QueryString["ReturnURL"]);
                }
            }
            else
            {
                Page.SetTempData("WarningMessage", "Felaktigt användarnamn eller lösenord.");
                Response.Redirect(Request.RawUrl);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void LogOutButton_Click(object sender, EventArgs e)
        {
            Session.Remove("loginSuccess");
            Response.Redirect(Request.RawUrl);
        }
    }
}