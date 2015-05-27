using HeatingWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HeatingWebApplication
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!bool.Parse(Session["loginSuccess"].ToString()) == true)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                NotLoggedIn();
            }

            SuccessMessageLiteral.Text = Page.GetTempData("SuccessMessage") as string;
            SuccessMessagePanel.Visible = !String.IsNullOrWhiteSpace(SuccessMessageLiteral.Text);
        }

        private void NotLoggedIn()
        {
            Page.SetTempData("WarningMessage", "Du är inte inloggad.");
            Response.Redirect("~/Login.aspx/?ReturnURL=" + HttpContext.Current.Request.Url.AbsoluteUri);
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IEnumerable<Domain.Model.BLL.Room> HeatingListView_GetData()
        {
            try
            {
                return Service.GetRooms();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett fel inträffande vid hämtning av rummen från databasen.");
                return null;
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void HeatingListView_UpdateItem(int RoomID)
        {
            try
            {
                var room = Service.GetRoomByID(RoomID);

                if (room == null)
                {
                    Response.Clear();
                    Response.StatusCode = 404;
                    Response.StatusDescription = "Room not found.";
                    Response.End();
                }

                room.Heating = !room.Heating;

                if (TryUpdateModel(room))
                {
                    Service.UpdateRoom(room);

                    Page.SetTempData("SuccessMessage", "Värmen i " + room.RoomDescription + " ändrades.");
                    Response.Redirect("~/Dashboard.aspx");
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då värmen på rummet skulle uppdateras.");
            }
        }

        protected void AutoManLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (LinkButton)sender;
                int RoomID = Convert.ToInt32(button.CommandArgument.ToString());

                var room = Service.GetRoomByID(RoomID);

                if (room == null)
                {
                    Response.Clear();
                    Response.StatusCode = 404;
                    Response.StatusDescription = "Room not found.";
                    Response.End();
                }

                room.AutomaticControl = !room.AutomaticControl;

                var tempBool = false;
                var tempInt = 0;

                // Validate the room object
                if (!bool.TryParse(room.Heating.ToString(), out tempBool))
                {
                    throw new Exception();
                }
                if (!bool.TryParse(room.AutomaticControl.ToString(), out tempBool))
                {
                    throw new Exception();
                }
                if (!int.TryParse(room.RoomID.ToString(), out tempInt))
                {
                    throw new Exception();
                }
                if (string.IsNullOrEmpty(room.RoomDescription))
                {
                    throw new Exception();
                }

                Service.UpdateRoom(room);

                Page.SetTempData("SuccessMessage", "Regleringen för " + room.RoomDescription + " ändrades.");
                Response.Redirect("~/Dashboard.aspx");
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då regleringen för rummet skulle uppdateras.");
            }
        }
    }
}