using GridironGrub.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<Park> Parks { get; set; }
        public IDbSet<Area> Areas { get; set; }
        public IDbSet<Restaurant> Restaurants { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Item> Items { get; set; }
        public IDbSet<SeatInfo> SeatInfos { get; set; }
        public IDbSet<CustomerInfo> CustomerInfos { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<RunnerInfo> RunnerInfos { get; set; }
        public IDbSet<OrderItem> OrderItems { get; set; }
        public IDbSet<ManagerInfo> ManagerInfos { get; set; }
        public IDbSet<VendorInfo> VendorInfos { get; set; }
        public IDbSet<AdminInfo> AdminInfos { get; set; }
        public IDbSet<RestaurantManager> RestaurantManagers { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
