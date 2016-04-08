using System.Web.Mvc;

namespace HandymanTools.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PickUpReservation()
        {
            return View();
        }
    }
}