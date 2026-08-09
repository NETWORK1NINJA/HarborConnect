namespace HarborConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoatCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Boats",
                c => new
                    {
                        BoatId = c.Int(nullable: false, identity: true),
                        BoatName = c.String(nullable: false, maxLength: 100),
                        CategoryId = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                        Capacity = c.Int(nullable: false),
                        PricePerTrip = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Location = c.String(nullable: false, maxLength: 200),
                        Status = c.String(nullable: false, maxLength: 30),
                        ApprovalStatus = c.String(nullable: false, maxLength: 30),
                        AdminComment = c.String(maxLength: 500),
                        ApprovedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        OwnerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.BoatId)
                .ForeignKey("dbo.BoatCategories", t => t.CategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .Index(t => t.CategoryId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.BoatDocuments",
                c => new
                    {
                        BoatDocumentId = c.Int(nullable: false, identity: true),
                        BoatId = c.Int(nullable: false),
                        DocumentType = c.String(nullable: false, maxLength: 100),
                        DocumentPath = c.String(nullable: false, maxLength: 300),
                        UploadedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BoatDocumentId)
                .ForeignKey("dbo.Boats", t => t.BoatId, cascadeDelete: true)
                .Index(t => t.BoatId);
            
            CreateTable(
                "dbo.BoatImages",
                c => new
                    {
                        BoatImageId = c.Int(nullable: false, identity: true),
                        BoatId = c.Int(nullable: false),
                        ImagePath = c.String(nullable: false, maxLength: 300),
                        UploadedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BoatImageId)
                .ForeignKey("dbo.Boats", t => t.BoatId, cascadeDelete: true)
                .Index(t => t.BoatId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        BoatId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                        TripDate = c.DateTime(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        NumberOfPassengers = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BookingStatus = c.String(nullable: false, maxLength: 30),
                        PaymentStatus = c.String(nullable: false, maxLength: 30),
                        SpecialRequest = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        Payment_PaymentId = c.Int(),
                        PINVerification_PINVerificationId = c.Int(),
                        Trip_TripId = c.Int(),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Boats", t => t.BoatId)
                .ForeignKey("dbo.BoatCategories", t => t.CategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .ForeignKey("dbo.Payments", t => t.Payment_PaymentId)
                .ForeignKey("dbo.PINVerifications", t => t.PINVerification_PINVerificationId)
                .ForeignKey("dbo.Trips", t => t.Trip_TripId)
                .Index(t => t.CustomerId)
                .Index(t => t.BoatId)
                .Index(t => t.CategoryId)
                .Index(t => t.Payment_PaymentId)
                .Index(t => t.PINVerification_PINVerificationId)
                .Index(t => t.Trip_TripId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentMethod = c.String(nullable: false, maxLength: 50),
                        TransactionReference = c.String(maxLength: 100),
                        PaymentStatus = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.PINVerifications",
                c => new
                    {
                        PINVerificationId = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        PINHash = c.String(nullable: false, maxLength: 255),
                        GeneratedDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        VerifiedDate = c.DateTime(),
                        IsVerified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PINVerificationId)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        TripId = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        DriverId = c.String(nullable: false, maxLength: 128),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        StartLocation = c.String(maxLength: 200),
                        EndLocation = c.String(maxLength: 200),
                        TripStatus = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.TripId)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .ForeignKey("dbo.AspNetUsers", t => t.DriverId)
                .Index(t => t.BookingId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(maxLength: 1000),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FeedbackId)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.BookingId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 150),
                        Message = c.String(nullable: false, maxLength: 1000),
                        IsRead = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TripTrackings",
                c => new
                    {
                        TrackingId = c.Int(nullable: false, identity: true),
                        TripId = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Speed = c.Double(),
                        RecordedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TrackingId)
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .Index(t => t.TripId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TripTrackings", "TripId", "dbo.Trips");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Notifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Boats", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Boats", "CategoryId", "dbo.BoatCategories");
            DropForeignKey("dbo.Bookings", "Trip_TripId", "dbo.Trips");
            DropForeignKey("dbo.Trips", "DriverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Trips", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "PINVerification_PINVerificationId", "dbo.PINVerifications");
            DropForeignKey("dbo.PINVerifications", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "Payment_PaymentId", "dbo.Payments");
            DropForeignKey("dbo.Payments", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "CategoryId", "dbo.BoatCategories");
            DropForeignKey("dbo.Bookings", "BoatId", "dbo.Boats");
            DropForeignKey("dbo.BoatImages", "BoatId", "dbo.Boats");
            DropForeignKey("dbo.BoatDocuments", "BoatId", "dbo.Boats");
            DropIndex("dbo.TripTrackings", new[] { "TripId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropIndex("dbo.Feedbacks", new[] { "UserId" });
            DropIndex("dbo.Feedbacks", new[] { "BookingId" });
            DropIndex("dbo.Trips", new[] { "DriverId" });
            DropIndex("dbo.Trips", new[] { "BookingId" });
            DropIndex("dbo.PINVerifications", new[] { "BookingId" });
            DropIndex("dbo.Payments", new[] { "BookingId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Bookings", new[] { "Trip_TripId" });
            DropIndex("dbo.Bookings", new[] { "PINVerification_PINVerificationId" });
            DropIndex("dbo.Bookings", new[] { "Payment_PaymentId" });
            DropIndex("dbo.Bookings", new[] { "CategoryId" });
            DropIndex("dbo.Bookings", new[] { "BoatId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropIndex("dbo.BoatImages", new[] { "BoatId" });
            DropIndex("dbo.BoatDocuments", new[] { "BoatId" });
            DropIndex("dbo.Boats", new[] { "OwnerId" });
            DropIndex("dbo.Boats", new[] { "CategoryId" });
            DropTable("dbo.TripTrackings");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Notifications");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Trips");
            DropTable("dbo.PINVerifications");
            DropTable("dbo.Payments");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Bookings");
            DropTable("dbo.BoatImages");
            DropTable("dbo.BoatDocuments");
            DropTable("dbo.Boats");
            DropTable("dbo.BoatCategories");
        }
    }
}
