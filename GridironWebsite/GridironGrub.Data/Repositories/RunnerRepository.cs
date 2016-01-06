using GridironGrub.Models;
using GridironGrub.Models.ViewModels.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Data.Repositories
{
    public class RunnerRepository
    {
        private ApplicationDbContext _db;

        public RunnerRepository()
        {
            _db = new ApplicationDbContext();
        }

        public ProfileVM GetProfile(string id)
        {
            ProfileVM vm = _db.Users.Where(u => u.Id == id).Select(u => new ProfileVM
            {
                Id = u.RunnerInfo.Id,
                Name = u.RunnerInfo.Name
            }).FirstOrDefault();

            return vm;
        }

        public List<OrderVM> GetOpenOrders()
        {
            List<OrderVM> vm = _db.Orders
                .Where(o => o.TimePrepared != null && o.RunnerId == null)
                .Select(o => new OrderVM
                {
                    Id = o.Id,
                    Restaurant = o.Restaurant.Name,
                    Area = o.SeatInfo.Area.Name,
                    Section = o.SeatInfo.Section,
                    Row = o.SeatInfo.Row,
                    Chair = o.SeatInfo.Chair,
                    Subtotal = o.Subtotal,
                    Taxes = o.Taxes,
                    ConvenienceFee = o.ConvenienceFee,
                    OutOfAreaFee = o.OutOfAreaFee,
                    Total = o.Total,
                    TimeOrdered = o.TimeOrdered,
                    TimePrepared = o.TimePrepared,
                    Items = o.OrderItems.Where(i => !i.IsRetired).Select(i => new ItemVM
                    {
                        Id = i.Item.Id,
                        Name = i.Item.Name,
                        ImageUrl = i.Item.ImageUrl,
                        Quantity = i.Quantity,
                        Price = i.Item.Price,
                        Discount = i.Item.Discount,
                        IsAlcohol = i.Item.IsAlcohol
                    }).ToList()
                }).ToList();

            if(vm != null)
            {
                foreach(OrderVM order in vm)
                {
                    if (order.Items != null)
                    {
                        foreach(ItemVM item in order.Items)
                        {
                            if (item.IsAlcohol) order.ContainsAlcohol = true;
                        }
                    }
                }
            }

            return vm;
        }

        public List<OrderVM> GetActiveOrders(string id)
        {
            int? runnerId = _db.Users.Where(u => u.Id == id).FirstOrDefault().RunnerInfoId;

            List<OrderVM> vm = null;

            if(runnerId != null)
            {
                vm = _db.Orders
               .Where(o => o.TimePrepared != null && o.RunnerId == runnerId && o.TimeDelivered == null)
               .Select(o => new OrderVM
               {
                   Id = o.Id,
                   PersDesc = o.PersDesc,
                   Restaurant = o.Restaurant.Name,
                   Area = o.SeatInfo.Area.Name,
                   Section = o.SeatInfo.Section,
                   Row = o.SeatInfo.Row,
                   Chair = o.SeatInfo.Chair,
                   Subtotal = o.Subtotal,
                   Taxes = o.Taxes,
                   ConvenienceFee = o.ConvenienceFee,
                   OutOfAreaFee = o.OutOfAreaFee,
                   Total = o.Total,
                   TimeOrdered = o.TimeOrdered,
                   TimePrepared = o.TimePrepared,
                   Items = o.OrderItems.Where(i => !i.IsRetired).Select(i => new ItemVM
                   {
                       Id = i.Item.Id,
                       Name = i.Item.Name,
                       ImageUrl = i.Item.ImageUrl,
                       Quantity = i.Quantity,
                       Price = i.Item.Price,
                       Discount = i.Item.Discount,
                       IsAlcohol = i.Item.IsAlcohol
                   }).ToList()
               }).ToList();
            }

            if (vm != null)
            {
                foreach (OrderVM order in vm)
                {
                    if (order.Items != null)
                    {
                        foreach (ItemVM item in order.Items)
                        {
                            if (item.IsAlcohol) order.ContainsAlcohol = true;
                        }
                    }
                }
            }

            return vm;
        }

        public List<OrderVM> GetOrderHistory(string id)
        {
            int? runnerId = _db.Users.Where(u => u.Id == id).FirstOrDefault().RunnerInfoId;

            List<OrderVM> vm = null;

            if (runnerId != null)
            {
                vm = _db.Orders
                .Where(o => o.RunnerId == runnerId && o.TimeDelivered != null)
                .Select(o => new OrderVM
                {
                    Id = o.Id,
                    Restaurant = o.Restaurant.Name,
                    Area = o.SeatInfo.Area.Name,
                    Section = o.SeatInfo.Section,
                    Row = o.SeatInfo.Row,
                    Chair = o.SeatInfo.Chair,
                    Subtotal = o.Subtotal,
                    Taxes = o.Taxes,
                    ConvenienceFee = o.ConvenienceFee,
                    OutOfAreaFee = o.OutOfAreaFee,
                    Total = o.Total,
                    TimeOrdered = o.TimeOrdered,
                    TimePrepared = o.TimePrepared,
                    Items = o.OrderItems.Where(i => !i.IsRetired).Select(i => new ItemVM
                    {
                        Id = i.Item.Id,
                        Name = i.Item.Name,
                        ImageUrl = i.Item.ImageUrl,
                        Quantity = i.Quantity,
                        Price = i.Item.Price,
                        Discount = i.Item.Discount,
                        IsAlcohol = i.Item.IsAlcohol
                    }).ToList()
                }).ToList();
            }

            if (vm != null)
            {
                foreach (OrderVM order in vm)
                {
                    if (order.Items != null)
                    {
                        foreach (ItemVM item in order.Items)
                        {
                            if (item.IsAlcohol) order.ContainsAlcohol = true;
                        }
                    }
                }
            }

            return vm;
        }

        public void AcceptOrder(string id, int orderId)
        {
            int? runnerId = _db.Users.Where(u => u.Id == id).FirstOrDefault().RunnerInfoId;
            int numOrders = GetActiveOrders(id).Count();

            Order order = _db.Orders.Where(o => o.Id == orderId).FirstOrDefault();

            if(runnerId != null && order != null && numOrders < 3)
            {
                order.RunnerId = runnerId;

                _db.SaveChanges();
            }
        }

        public void DeliverOrder(int orderId)
        {
            Order order = _db.Orders.Where(o => o.Id == orderId).FirstOrDefault();

            if (order != null)
            {
                order.TimeDelivered = DateTime.Now;

                _db.SaveChanges();
            }
        }
    }
}
