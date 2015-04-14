using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain.Model.BLL;
using HeatingWebApplication.Models;

namespace HeatingWebApplication
{
    public partial class _Default : Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IEnumerable<Domain.Model.BLL.Room> HeatingListView_GetData()
        {
            //try
            //{
            return Service.GetRooms();
            //}
            //catch (Exception)
            //{
            //    ModelState.AddModelError(String.Empty, "Ett fel inträffande vid hämtning av medlemmarna.");
            //    return null;
            //}
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void HeatingListView_UpdateItem(int RoomID)
        {
            try
            {
                var room = Service.GetRoomByID(RoomID);
                room.Heating = !room.Heating;

                if (room == null)
                {
                    Response.Clear();
                    Response.StatusCode = 404;
                    Response.StatusDescription = "Payment not found.";
                    Response.End();
                }

                if (TryUpdateModel(room))
                {
                    Service.UpdateRoom(room);

                    //Page.SetTempData("SuccessMessage", "Betalningen uppdaterades.");
                    //Response.RedirectToRoute("VisaMedlem", new { id = room.MemberID });
                    //Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då värmen på rummet skulle uppdateras.");
            }
        }
    }
}