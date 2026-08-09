using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HarborConnect.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in
            // CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(
                this,
                DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // ==========================================
        // HARBOR CONNECT DATABASE TABLES
        // ==========================================

        public DbSet<Boat> Boats { get; set; }

        public DbSet<BoatImage> BoatImages { get; set; }

        public DbSet<BoatDocument> BoatDocuments { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<BoatCategory> BoatCategories { get; set; }

        public DbSet<PINVerification> PINVerifications { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<TripTracking> TripTrackings { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Notification> Notifications { get; set; }


        // ==========================================
        // ENTITY FRAMEWORK RELATIONSHIPS
        // ==========================================

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // BOAT RELATIONSHIPS
            // ==========================================

            // Boat -> Owner
            modelBuilder.Entity<Boat>()
                .HasRequired(b => b.Owner)
                .WithMany()
                .HasForeignKey(b => b.OwnerId)
                .WillCascadeOnDelete(false);

            // Boat -> BoatCategory
            modelBuilder.Entity<Boat>()
                .HasRequired(b => b.Category)
                .WithMany(c => c.Boats)
                .HasForeignKey(b => b.CategoryId)
                .WillCascadeOnDelete(false);


            // ==========================================
            // BOOKING RELATIONSHIPS
            // ==========================================

            // Booking -> Customer
            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.CustomerId)
                .WillCascadeOnDelete(false);

            // Booking -> Boat
            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.Boat)
                .WithMany(b => b.Bookings)
                .HasForeignKey(b => b.BoatId)
                .WillCascadeOnDelete(false);

            // Booking -> BoatCategory
            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.Category)
                .WithMany()
                .HasForeignKey(b => b.CategoryId)
                .WillCascadeOnDelete(false);


            // ==========================================
            // PAYMENT
            // ==========================================

            // Payment -> Booking
            modelBuilder.Entity<Payment>()
                .HasRequired(p => p.Booking)
                .WithMany()
                .HasForeignKey(p => p.BookingId)
                .WillCascadeOnDelete(false);


            // ==========================================
            // PIN VERIFICATION
            // ==========================================

            // PINVerification -> Booking
            modelBuilder.Entity<PINVerification>()
                .HasRequired(p => p.Booking)
                .WithMany()
                .HasForeignKey(p => p.BookingId)
                .WillCascadeOnDelete(false);


            // ==========================================
            // TRIP
            // ==========================================

            // Trip -> Booking
            modelBuilder.Entity<Trip>()
                .HasRequired(t => t.Booking)
                .WithMany()
                .HasForeignKey(t => t.BookingId)
                .WillCascadeOnDelete(false);

            // Trip -> Driver
            modelBuilder.Entity<Trip>()
                .HasRequired(t => t.Driver)
                .WithMany()
                .HasForeignKey(t => t.DriverId)
                .WillCascadeOnDelete(false);


            // ==========================================
            // TRIP TRACKING
            // ==========================================

            // TripTracking -> Trip
            modelBuilder.Entity<TripTracking>()
                .HasRequired(t => t.Trip)
                .WithMany()
                .HasForeignKey(t => t.TripId)
                .WillCascadeOnDelete(true);


            // ==========================================
            // FEEDBACK
            // ==========================================

            // Feedback -> Booking
            modelBuilder.Entity<Feedback>()
                .HasRequired(f => f.Booking)
                .WithMany()
                .HasForeignKey(f => f.BookingId)
                .WillCascadeOnDelete(false);

            // Feedback -> User
            modelBuilder.Entity<Feedback>()
                .HasRequired(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .WillCascadeOnDelete(false);


            // ==========================================
            // NOTIFICATIONS
            // ==========================================

            // Notification -> User
            modelBuilder.Entity<Notification>()
                .HasRequired(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .WillCascadeOnDelete(false);
        }


        // ==========================================
        // CREATE DATABASE CONTEXT
        // ==========================================

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}