using GridironGrub.Models;
using GridironGrub.Models.ViewModels.Customer;
using Microsoft.AspNet.Identity;
using SendGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Data.Repositories
{
    public class CustomerRepository
    {
        private ApplicationDbContext _db;

        public CustomerRepository()
        {
            _db = new ApplicationDbContext();
        }

        public LoginInfoVM getLoginInfo(int seatId)
        {
            string username = _db.Users.Where(u => u.SeatInfoId == seatId).FirstOrDefault().UserName;

            LoginInfoVM vm = null;

            if(seatId < 4)
            {
                vm = new LoginInfoVM
                {
                    Username = username,
                    Password = "123456"
                };
            }
            else
            {
                vm = new LoginInfoVM
                {
                    Username = username,
                    Password = "Password#1"
                };
            }
            

            return vm;
        }

        public void Register(string email, DateTime birthday)
        {
            CustomerInfo customerInfo = new CustomerInfo
            {
                Birthday = birthday
            };

            _db.CustomerInfos.Add(customerInfo);

            _db.Users.Where(u => u.Email == email).FirstOrDefault().CustomerInfo = customerInfo;

            _db.SaveChanges();       
        }

        public CustomerProfileVM getCustomerProfile(string id)
        {
            CustomerProfileVM vm = _db.Users
                .Where(c => c.Id == id)
                .Select(c => new CustomerProfileVM
                {
                    FirstName = c.CustomerInfo.FirstName,
                    LastName = c.CustomerInfo.LastName,
                    ImageUrl = c.CustomerInfo.ImageUrl,
                    Gender = c.CustomerInfo.Gender,
                    Email = c.Email,
                    Birthday = c.CustomerInfo.Birthday
                }).FirstOrDefault();

            return vm;
        }

        public WelcomeVM getWelcome(int seatId)
        {
            SeatInfo seatInfo = _db.SeatInfos.Where(s => s.Id == seatId).FirstOrDefault();

            WelcomeVM vm = new WelcomeVM
            {
                Park = seatInfo.Area.Park.Name,
                Section = seatInfo.Section,
                Row = seatInfo.Row,
                Seat = seatInfo.Chair
            };

            return vm;
        }

        public ScanSeatVM getCustomerScan(int seatId)
        {
            int areaId = _db.SeatInfos.Where(s => s.Id == seatId).FirstOrDefault().AreaId;
            Park park = _db.Areas.Where(a => a.Id == areaId).FirstOrDefault().Park;

            List<RestaurantListVM> listInArea = _db.Restaurants
                .Where(r => r.AreaId == areaId)
                .Select(r => new RestaurantListVM
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    ImageUrl = r.ImageUrl
                }).ToList();

            List<RestaurantListVM> listOutArea = _db.Restaurants
                .Where(r => r.AreaId != areaId)
                .Select(r => new RestaurantListVM
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    ImageUrl = r.ImageUrl
                }).ToList();

            ScanSeatVM vm = new ScanSeatVM
            {
                Park = park.Name,
                LogoUrl = park.LogoUrl,
                RestsInArea = listInArea,
                RestsOutArea = listOutArea
            };

            return vm;
        }

        public RestaurantVM getRestaurant(string id, int restaurantId)
        {
            int seatId = _db.Users.Where(u => u.Id == id).FirstOrDefault().SeatInfo.Id;

            //create a new order to go along with this restaurant
            postAddItem(id, restaurantId, null);

            //get restaurant info
            RestaurantVM vm = _db.Restaurants
                .Where(r => r.Id == restaurantId)
                .Select(r => new RestaurantVM
                {
                    SeatId = seatId,
                    Name = r.Name,
                    Categories = r.Categories.Select(c => new CategoryListVM
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Items = c.Items.Select(i => new ItemListVM
                        {
                            Id = i.Id,
                            Name = i.Name,
                            ImageUrl = i.ImageUrl,
                            Description = i.Description,
                            Price = i.Price,
                            Discount = i.Discount,
                            IsAlcohol = i.IsAlcohol
                        }).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return vm;
        }

        public CartVM getCart(string id)
        {
            int SeatInfoId = _db.Users.Where(u => u.Id == id).FirstOrDefault().SeatInfo.Id;

            CartVM vm = _db.Orders
                .Where(o => o.SeatInfoId == SeatInfoId && !o.IsRetired && o.TimeOrdered == null)
                .Select(o => new CartVM
                {
                    Items = o.OrderItems.Where(i => !i.IsRetired).Select(i => new ItemVM
                    {
                        Id = i.Item.Id,
                        Name = i.Item.Name,
                        ImageUrl = i.Item.ImageUrl,
                        Description = i.Item.Description,
                        Price = i.Item.Price,
                        Discount = i.Item.Discount,
                        Quantity = i.Quantity,
                        IsAlcohol = i.Item.IsAlcohol
                    }).ToList(),
                    Subtotal = o.Subtotal,
                    Taxes = o.Taxes,
                    ConvenienceFee = o.ConvenienceFee,
                    OutOfAreaFee = o.OutOfAreaFee,
                    Total = o.Total,
                    PersDesc = o.PersDesc,
                    TimeOrdered = o.TimeOrdered,
                    TimePrepared = o.TimePrepared,
                    TimeDelivered = o.TimeDelivered
                }).FirstOrDefault();

            return vm;
        }

        public int getNavbarBadge(string id)
        {
            int SeatInfoId = _db.Users.Where(u => u.Id == id).FirstOrDefault().SeatInfo.Id;
            Order order = _db.Orders.Where(o => o.SeatInfoId == SeatInfoId && !o.IsRetired && o.TimeOrdered == null).Include(i => i.OrderItems).FirstOrDefault();

            int counter = 0;
            if (order.OrderItems != null)
            {
                foreach (OrderItem orderItem in order.OrderItems)
                {
                    if (!orderItem.IsRetired)
                    {
                        counter += orderItem.Quantity;
                    }
                }
            }

            return counter;
        }

        public bool ContainsAlcohol(string id)
        {
            int SeatInfoId = _db.Users.Where(u => u.Id == id).FirstOrDefault().SeatInfo.Id;
            List<OrderItem> items = _db.OrderItems.Where(oi => !oi.Item.IsRetired && oi.Order.SeatInfoId == SeatInfoId && !oi.Order.IsRetired && oi.Order.TimeOrdered == null).ToList();
            bool containsAlcohol = false;

            foreach(OrderItem item in items)
            {
                if(item.Item.IsAlcohol)
                {
                    containsAlcohol = true;
                }
            }

            return containsAlcohol;
        }

        public void postProfile(string id, CustomerProfileVM vm)
        {
            CustomerInfo CustInfo = _db.Users.Where(u => u.Id == id).FirstOrDefault().CustomerInfo;

            if(CustInfo != null)
            {
                if (vm.FirstName != null) CustInfo.FirstName = vm.FirstName;
                if (vm.LastName != null) CustInfo.LastName = vm.LastName;
                if (vm.ImageUrl != null) CustInfo.ImageUrl = vm.ImageUrl;
                if (vm.Gender != null) CustInfo.Gender = vm.Gender;
            }

            _db.SaveChanges();
        }

        public void postAddItem(string id, int restaurantId, AddItemVM vm)
        {
            int SeatInfoId = _db.Users.Where(u => u.Id == id).FirstOrDefault().SeatInfo.Id;
            Order order = _db.Orders.Where(o => o.SeatInfoId == SeatInfoId && !o.IsRetired && o.TimeOrdered == null).FirstOrDefault();

            if(order == null)
            {
                order = new Order
                {
                    SeatInfoId = SeatInfoId,
                    RestaurantId = restaurantId,
                    ConvenienceFee = 4m
                };

                if (RestaurantOutOfArea(id, restaurantId)) order.OutOfAreaFee = 2m;

                _db.Orders.Add(order);
            }
            else
            {
                if (restaurantId != 0 && order.RestaurantId != restaurantId)
                {
                    order.IsRetired = true;

                    order = new Order
                    {
                        SeatInfoId = SeatInfoId,
                        RestaurantId = restaurantId
                    };

                    _db.Orders.Add(order);
                }
                else if(vm != null)
                {
                    if (vm.PersDesc != null) order.PersDesc = vm.PersDesc;
                    if (vm.ReceiptEmail != null) order.ReceiptEmail = vm.ReceiptEmail;
                    if (vm.ItemId > 0)
                    {
                        OrderItem orderItem = _db.OrderItems.Where(i => i.ItemId == vm.ItemId && i.OrderId == order.Id).FirstOrDefault();
                        if (orderItem == null)
                        {
                            orderItem = new OrderItem
                            {
                                ItemId = vm.ItemId,
                                Quantity = vm.Quantity,
                                OrderId = order.Id
                            };
                            if (vm.Quantity <= 0) orderItem.Quantity = 1;

                            _db.OrderItems.Add(orderItem);
                        }
                        else
                        {
                            if(orderItem.IsRetired)
                            {
                                orderItem.IsRetired = false;
                                orderItem.Quantity = 1;
                            }
                            else if (vm.Quantity > 0) orderItem.Quantity = vm.Quantity;
                            else orderItem.Quantity++;
                        }
                    }
                    if (vm.RemoveItemId > 0)
                    {
                        order.OrderItems.Where(o => o.ItemId == vm.RemoveItemId).FirstOrDefault().IsRetired = true;
                    }
                    if (vm.SubmitOrder)
                    {
                        order.TimeOrdered = DateTime.Now;
                    }
                    order.IsRetired = vm.IsRetired;
                }
            }

            _db.SaveChanges();

            if(order.OrderItems != null) updateTotals(id);
        }

        public void RetireOrder(string id)
        {
            int SeatInfoId = _db.Users.Where(u => u.Id == id).FirstOrDefault().SeatInfo.Id;
            Order order = _db.Orders.Where(o => o.SeatInfoId == SeatInfoId && !o.IsRetired && o.TimeOrdered == null).FirstOrDefault();

            if(order != null)
            {
                order.IsRetired = true;
            }

            _db.SaveChanges();
        }

        public void updateTotals(string id)
        {
            int SeatInfoId = _db.Users.Where(u => u.Id == id).FirstOrDefault().SeatInfo.Id;
            Order order = _db.Orders.Where(o => o.SeatInfoId == SeatInfoId && !o.IsRetired && o.TimeOrdered == null).Include(o => o.OrderItems.Select(oi => oi.Item)).FirstOrDefault();

            //update Subtotal
            decimal subtotal = 0;
            if(order.OrderItems != null)
            {
                foreach (OrderItem orderItem in order.OrderItems)
                {
                    if(!orderItem.IsRetired) subtotal += orderItem.Item.Price * orderItem.Quantity;
                }
            }
            order.Subtotal = subtotal;

            //update Taxes
            decimal taxRate = order.SeatInfo.Area.Park.TaxRate;
            order.Taxes = subtotal * taxRate;

            //update Total
            order.Total = order.Subtotal + order.Taxes + order.ConvenienceFee + order.OutOfAreaFee;

            _db.SaveChanges();
        }

        public bool RestaurantOutOfArea(string id, int restaurantId)
        {
            Area seatArea = _db.Users.Where(u => u.Id == id).FirstOrDefault().SeatInfo.Area;
            Area restaurantArea = _db.Restaurants.Where(r => r.Id == restaurantId).FirstOrDefault().Area;

            if(seatArea != null && restaurantArea != null)
            {
                if (seatArea == restaurantArea) return false;
            }

            return true;
        }

    }

    public static class RepositoryExtensions
    {
        public static IQueryable<T> Include<T, TProperty>(this IQueryable<T> queryable, Expression<Func<T, TProperty>> relatedEntity) where T : class
        {
            return System.Data.Entity.QueryableExtensions.Include<T, TProperty>(queryable, relatedEntity);
        }
    }
}
