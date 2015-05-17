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
            SuccessMessageLiteral.Text = Page.GetTempData("SuccessMessage") as string;
            SuccessMessagePanel.Visible = !String.IsNullOrWhiteSpace(SuccessMessageLiteral.Text);
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
    }
}