namespace HarborConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<HarborConnect.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HarborConnect.Models.ApplicationDbContext context)
        {
            // ==========================================
            // CREATE HARBOR CONNECT ROLES
            // ==========================================

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context)
            );

            string[] roles =
            {
        "Admin",
        "BoatOwner",
        "Customer",
        "Driver"
    };

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }


            // ==========================================
            // CREATE ADMIN USER
            // ==========================================

            var userManager = new UserManager<HarborConnect.Models.ApplicationUser>(
                new UserStore<HarborConnect.Models.ApplicationUser>(context)
            );

            string adminEmail = "admin@harborconnect.com";
            string adminPassword = "Admin@12345";

            var adminUser = userManager.FindByEmail(adminEmail);

            if (adminUser == null)
            {
                adminUser = new HarborConnect.Models.ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                var result = userManager.Create(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }
            else
            {
                if (!userManager.IsInRole(adminUser.Id, "Admin"))
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }


            // ==========================================
            // SMALL BOAT CATEGORY
            // ==========================================

            if (!context.BoatCategories.Any(c => c.CategoryName == "Small"))
            {
                context.BoatCategories.Add(new HarborConnect.Models.BoatCategory
                {
                    CategoryName = "Small",
                    Price = 500.00m,
                    Description = "Small boat suitable for smaller groups."
                });
            }


            // ==========================================
            // MEDIUM BOAT CATEGORY
            // ==========================================

            if (!context.BoatCategories.Any(c => c.CategoryName == "Medium"))
            {
                context.BoatCategories.Add(new HarborConnect.Models.BoatCategory
                {
                    CategoryName = "Medium",
                    Price = 800.00m,
                    Description = "Medium boat suitable for medium-sized groups."
                });
            }


            // ==========================================
            // LARGE BOAT CATEGORY
            // ==========================================

            if (!context.BoatCategories.Any(c => c.CategoryName == "Large"))
            {
                context.BoatCategories.Add(new HarborConnect.Models.BoatCategory
                {
                    CategoryName = "Large",
                    Price = 1200.00m,
                    Description = "Large boat suitable for larger groups."
                });
            }


            context.SaveChanges();
        }
    }
}