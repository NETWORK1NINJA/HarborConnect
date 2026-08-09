using HarborConnect.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HarborConnect.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // ==========================================
        // ADMIN DASHBOARD
        // ==========================================

        public async Task<ActionResult> Index()
        {
            // Count boats waiting for approval
            ViewBag.PendingBoats = await db.Boats
                .CountAsync(b => b.ApprovalStatus == "Pending");

            // Count all boats
            ViewBag.TotalBoats = await db.Boats.CountAsync();

            // Count all bookings
            ViewBag.TotalBookings = await db.Bookings.CountAsync();

            // Count pending bookings
            ViewBag.PendingBookings = await db.Bookings
                .CountAsync(b => b.BookingStatus == "Pending");

            return View();
        }


        // ==========================================
        // DISPOSE
        // ==========================================

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}