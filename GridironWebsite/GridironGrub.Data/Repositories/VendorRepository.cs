using GridironGrub.Models;
using GridironGrub.Models.ViewModels.Vendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Data.Repositories
{
    public class VendorRepository
    {
        private ApplicationDbContext _db;

        public VendorRepository()
        {
            _db = new ApplicationDbContext();
        }

        public List<OpenOrderVM> GetOpenOrders(string id)
        {
            int? vendorId = _db.Users.Where(u => u.Id == id).FirstOrDefault().VendorInfo.RestaurantId;

            List<OpenOrderVM> vm = null;

            if(vendorId != null)
            {
                vm = _db.Orders
                    .Where(o => o.RestaurantId == vendorId && o.TimeOrdered != null && o.TimePrepared == null)
                    .Select(o => new OpenOrderVM
                    {
                        Id = o.Id,
                        TimeOrdered = o.TimeOrdered,
                        Items = o.OrderItems.Where(i => !i.IsRetired).Select(i => new ItemVM
                        {
                            Name = i.Item.Name,
                            ImageUrl = i.Item.ImageUrl,
                            Quantity = i.Quantity,
                            IsAlcohol = i.Item.IsAlcohol
                        }).ToList()
                    }).ToList();
            }

            return vm;
        }

        public List<RecentOrderVM> GetRecentOrders(string id)
        {
            int? vendorId = _db.Users.Where(u => u.Id == id).FirstOrDefault().VendorInfo.RestaurantId;

            List<RecentOrderVM> vm = null;

            if (vendorId != null)
            {
                vm = _db.Orders
                    .Where(o => o.RestaurantId == vendorId && o.TimePrepared != null)
                    .Select(o => new RecentOrderVM
                    {
                        Id = o.Id,
                        TimeOrdered = o.TimeOrdered,
                        TimePrepared = o.TimePrepared,
                        Items = o.OrderItems.Where(i => !i.IsRetired).Select(i => new ItemVM
                        {
                            Name = i.Item.Name,
                            ImageUrl = i.Item.ImageUrl,
                            Quantity = i.Quantity,
                            IsAlcohol = i.Item.IsAlcohol
                        }).ToList()
                    }).ToList();
            }

            return vm;
        }

        public void CompleteOrder(int orderId)
        {
            Order order = _db.Orders.Where(o => o.Id == orderId).FirstOrDefault();

            if(order != null)
            {
                order.TimePrepared = DateTime.Now;

                _db.SaveChanges();
            }
        }
    }
}
