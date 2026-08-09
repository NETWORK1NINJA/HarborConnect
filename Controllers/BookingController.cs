using HarborConnect.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;



namespace HarborConnect.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Booking/Create
        public ActionResult Create(int categoryId)
        {
            var category = db.BoatCategories
                .FirstOrDefault(c => c.CategoryId == categoryId);

            if (category == null)
            {
                return HttpNotFound();
            }

            var boats = db.Boats
                .Where(b => b.CategoryId == categoryId
                         && b.Status == "Available"
                         && b.ApprovalStatus == "Approved")
                .ToList();

            ViewBag.AvailableBoats = boats;

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
    int categoryId,
    int BoatId,
    DateTime TripDate,
    TimeSpan StartTime,
    TimeSpan EndTime,
    int NumberOfPassengers,
    string SpecialRequest)
        {
            // Find the selected boat
            var boat = db.Boats.FirstOrDefault(b =>
                b.BoatId == BoatId &&
                b.CategoryId == categoryId &&
                b.Status == "Available" &&
                b.ApprovalStatus == "Approved");

            if (boat == null)
            {
                ModelState.AddModelError("", "The selected boat is not available.");

                var categoryError = db.BoatCategories
                    .FirstOrDefault(c => c.CategoryId == categoryId);

                ViewBag.AvailableBoats = db.Boats
                    .Where(b => b.CategoryId == categoryId &&
                                b.Status == "Available" &&
                                b.ApprovalStatus == "Approved")
                    .ToList();

                return View(categoryError);
            }

            // Validate passengers
            if (NumberOfPassengers > boat.Capacity)
            {
                ModelState.AddModelError(
                    "NumberOfPassengers",
                    "The number of passengers cannot exceed the boat capacity.");

                var categoryError = db.BoatCategories
                    .FirstOrDefault(c => c.CategoryId == categoryId);

                ViewBag.AvailableBoats = db.Boats
                    .Where(b => b.CategoryId == categoryId &&
                                b.Status == "Available" &&
                                b.ApprovalStatus == "Approved")
                    .ToList();

                return View(categoryError);
            }

            // Validate time
            if (EndTime <= StartTime)
            {
                ModelState.AddModelError(
                    "EndTime",
                    "End time must be later than start time.");

                var categoryError = db.BoatCategories
                    .FirstOrDefault(c => c.CategoryId == categoryId);

                ViewBag.AvailableBoats = db.Boats
                    .Where(b => b.CategoryId == categoryId &&
                                b.Status == "Available" &&
                                b.ApprovalStatus == "Approved")
                    .ToList();

                return View(categoryError);
            }

            // Create booking
            var booking = new Booking
            {
                CustomerId = User.Identity.GetUserId(),
                BoatId = boat.BoatId,
                CategoryId = categoryId,

                BookingDate = DateTime.Now,
                TripDate = TripDate,

                StartTime = StartTime,
                EndTime = EndTime,

                NumberOfPassengers = NumberOfPassengers,

                TotalAmount = boat.PricePerTrip,

                BookingStatus = "Pending",
                PaymentStatus = "Unpaid",

                SpecialRequest = SpecialRequest,

                CreatedDate = DateTime.Now
            };

            db.Bookings.Add(booking);

            await db.SaveChangesAsync();

            return RedirectToAction("Details", new { id = booking.BookingId });
        }

        // ==========================================
        // BOOKING DETAILS
        // ==========================================

        public async Task<ActionResult> Details(int id)
        {
            var customerId = User.Identity.GetUserId();

            var booking = await db.Bookings
                .Include(b => b.Boat)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b =>
                    b.BookingId == id &&
                    b.CustomerId == customerId);

            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        }

        // ==========================================
        // CUSTOMER MY BOOKINGS
        // ==========================================

        public async Task<ActionResult> MyBookings()
        {
            var customerId = User.Identity.GetUserId();

            var bookings = await db.Bookings
                .Include(b => b.Boat)
                .Include(b => b.Category)
                .Where(b => b.CustomerId == customerId)
                .OrderByDescending(b => b.CreatedDate)
                .ToListAsync();

            return View(bookings);
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
    
