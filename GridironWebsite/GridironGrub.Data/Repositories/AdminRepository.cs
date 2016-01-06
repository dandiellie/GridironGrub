using GridironGrub.Models;
using GridironGrub.Models.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Data.Repositories
{
    public class AdminRepository
    {
        private ApplicationDbContext _db;

        public AdminRepository()
        {
            _db = new ApplicationDbContext();
        }

        public List<OrderVM> GetOpenOrders()
        {
            List<OrderVM> vm = _db.Orders.Where(o => o.TimeOrdered != null && o.TimeDelivered == null).Select(o => new OrderVM
            {
                Id = o.Id,
                Subtotal = o.Subtotal,
                Taxes = o.Taxes,
                ConvenienceFee = o.ConvenienceFee,
                OutOfAreaFee = o.OutOfAreaFee,
                Total = o.Total,
                TimeOrdered = o.TimeOrdered,
                TimePrepared = o.TimePrepared,
                TimeDelivered = o.TimeDelivered,
                Runner = o.RunnerInfo.Name,
                Restaurant = o.Restaurant.Name,
                Area = o.SeatInfo.Area.Name,
                Section = o.SeatInfo.Section,
                Row = o.SeatInfo.Row,
                Chair = o.SeatInfo.Chair,
                Items = o.OrderItems.Select(i => new OrderItemVM
                {
                    Name = i.Item.Name,
                    ImageUrl = i.Item.ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            }).ToList();

            return vm;
        }

        public List<OrderVM> GetRecentOrders(DateTime? date)
        {
            if(date == null)
            {
                date = DateTime.Now;
            }

            List<OrderVM> vm = _db.Orders.Where(o => o.TimeDelivered != null).Select(o => new OrderVM
            {
                Id = o.Id,
                Subtotal = o.Subtotal,
                Taxes = o.Taxes,
                ConvenienceFee = o.ConvenienceFee,
                OutOfAreaFee = o.OutOfAreaFee,
                Total = o.Total,
                TimeOrdered = o.TimeOrdered,
                TimePrepared = o.TimePrepared,
                TimeDelivered = o.TimeDelivered,
                Runner = o.RunnerInfo.Name,
                Restaurant = o.Restaurant.Name,
                Area = o.SeatInfo.Area.Name,
                Section = o.SeatInfo.Section,
                Row = o.SeatInfo.Row,
                Chair = o.SeatInfo.Chair,
                Items = o.OrderItems.Select(i => new OrderItemVM
                {
                    Name = i.Item.Name,
                    ImageUrl = i.Item.ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            }).OrderBy(o => o.TimeDelivered).ToList();

            return vm;
        }

        public OrderVM GetSpecificOrders(int orderId)
        {
            OrderVM vm = _db.Orders.Where(o => o.Id == orderId).Select(o => new OrderVM
            {
                Id = o.Id,
                Subtotal = o.Subtotal,
                Taxes = o.Taxes,
                ConvenienceFee = o.ConvenienceFee,
                OutOfAreaFee = o.OutOfAreaFee,
                Total = o.Total,
                TimeOrdered = o.TimeOrdered,
                TimePrepared = o.TimePrepared,
                TimeDelivered = o.TimeDelivered,
                Runner = o.RunnerInfo.Name,
                Restaurant = o.Restaurant.Name,
                Area = o.SeatInfo.Area.Name,
                Section = o.SeatInfo.Section,
                Row = o.SeatInfo.Row,
                Chair = o.SeatInfo.Chair,
                Items = o.OrderItems.Select(i => new OrderItemVM
                {
                    Name = i.Item.Name,
                    ImageUrl = i.Item.ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            }).FirstOrDefault();

            return vm;
        }

        public ParkVM GetPark()
        {
            ParkVM vm = _db.Parks.Select(p => new ParkVM
            {
                Id = p.Id,
                Name = p.Name,
                LogoUrl = p.LogoUrl,
                TaxRate = p.TaxRate,
                Areas = p.Areas.Where(a => !a.IsRetired).OrderBy(a => a.Name).Select(a => new AreaVM
                {
                    Id = a.Id,
                    Name = a.Name,
                    Vendors = a.Restaurants.Where(r => !r.IsRetired).OrderBy(r => r.Name).Select(r => new VendorVM
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description,
                        Categories = r.Categories.Where(c => !c.IsRetired).OrderBy(c => c.Name).Select(c => new CategoryVM
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Items = c.Items.Where(i => !i.IsRetired).OrderBy(i => i.Name).Select(i => new ItemVM
                            {
                                Id = i.Id,
                                Name = i.Name,
                                ImageUrl = i.ImageUrl,
                                Description = i.Description,
                                Price = i.Price,
                                Discount = i.Discount,
                                //Views = i.Views.Count,
                                //Purchases = i.Purchases.Count,
                                IsAlcohol = i.IsAlcohol
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            }).FirstOrDefault();

            return vm;
        }

        public VendorVM GetVendor(int restId)
        {
            VendorVM vm = _db.Restaurants.Where(r => r.Id == restId).Select(r => new VendorVM
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                ImageUrl = r.ImageUrl,
                Categories = r.Categories.Where(c => !c.IsRetired).OrderBy(c => c.Name).Select(c => new CategoryVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    Items = c.Items.Where(i => !i.IsRetired).OrderBy(i => i.Name).Select(i => new ItemVM
                    {
                        Id = i.Id,
                        Name = i.Name,
                        ImageUrl = i.ImageUrl,
                        Description = i.Description,
                        Price = i.Price,
                        Discount = i.Discount,
                        //Views = i.Views.Count,
                        //Purchases = i.Purchases.Count,
                        IsAlcohol = i.IsAlcohol
                    }).ToList()
                }).ToList()
            }).FirstOrDefault();

            return vm;
        }

        public ParkSeatVM GetSeats()
        {
            ParkSeatVM vm = _db.Parks.Select(p => new ParkSeatVM
            {
                Id = p.Id,
                Name = p.Name,
                LogoUrl = p.LogoUrl,
                Areas = p.Areas.Where(a => !a.IsRetired).OrderBy(a => a.Name).Select(a => new AreaSeatVM
                {
                    Id = a.Id,
                    Name = a.Name,
                    Seats = a.SeatInfos.Where(s => !s.IsRetired).OrderBy(s => s.Section).ThenBy(s => s.Row).ThenBy(s => s.Chair).Select(s => new SeatVM
                    {
                        Id = s.Id,
                        Section = s.Section,
                        Row = s.Row,
                        Chair = s.Chair
                    }).ToList()
                }).ToList()
            }).FirstOrDefault();

            return vm;
        }
        
        public List<RestaurantVM> GetRestaurants()
        {
            List<RestaurantVM> vm = _db.Restaurants.Where(r => !r.IsRetired).Select(r => new RestaurantVM
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return vm;
        }

        public List<EmployeeVM> GetEmployees()
        {
            List<EmployeeVM> temp = _db.Users.Where(u => u.RunnerInfoId != null || u.VendorInfoId != null || u.ManagerInfoId != null || u.AdminInfoId != null).Select(u => new EmployeeVM
            {
                Username = u.UserName,
            }).ToList();

            List<EmployeeVM> vm = new List<EmployeeVM>();

            foreach(EmployeeVM emp in temp)
            {
                vm.Add(GetEmployee(emp.Username));
            }

            return vm;
        }

        public EmployeeVM GetEmployee(string username)
        {
            if (username == null) return null;

            ApplicationUser user = _db.Users.Where(u => u.UserName == username).FirstOrDefault();

            EmployeeVM vm = new EmployeeVM { Username = user.UserName };
            if(user.RunnerInfo != null)
            {
                if (!user.RunnerInfo.IsRetired)
                {
                    vm.Name = user.RunnerInfo.Name;
                    vm.IsRunner = true;
                }
            }
            if(user.VendorInfo != null)
            {
                if (!user.VendorInfo.IsRetired)
                {
                    vm.Name = user.VendorInfo.Name;
                    vm.IsVendor = true;
                    vm.Restaurant = new RestaurantVM
                    {
                        Id = user.VendorInfo.Restaurant.Id,
                        Name = user.VendorInfo.Restaurant.Name
                    };
                }
            }
            if(user.ManagerInfo != null)
            {
                if (!user.ManagerInfo.IsRetired)
                {
                    vm.Name = user.ManagerInfo.Name;
                    vm.IsManager = true;
                    vm.Restaurants = new List<RestaurantVM>();
                    if(user.ManagerInfo.RestaurantManagers != null)
                    {
                        foreach (RestaurantManager rm in user.ManagerInfo.RestaurantManagers)
                        {
                            if(!rm.IsRetired)
                            {
                                vm.Restaurants.Add(new RestaurantVM
                                {
                                    Id = rm.Id,
                                    Name = rm.Restaurant.Name
                                });
                            }
                        }
                    }
                }
            }
            if(user.AdminInfo != null)
            {
                if (!user.AdminInfo.IsRetired)
                {
                    vm.Name = user.AdminInfo.Name;
                    vm.IsAdmin = true;
                }
            }
            
            return vm;
        }

        public int PostSeat(SeatVM vm)
        {
            SeatInfo seat = null;
            if (vm.Id > 0)
            {
                seat = _db.SeatInfos.Where(s => s.Id == vm.Id).FirstOrDefault();

                if (vm.Section != null) seat.Section = vm.Section;
                if (vm.Row != null) seat.Row = vm.Row;
                if (vm.Chair != null) seat.Chair = vm.Chair;
                if (vm.IsRetired) seat.IsRetired = vm.IsRetired;
            }
            else if (vm.AreaId > 0)
            {
                seat = new SeatInfo
                {
                    AreaId = vm.AreaId,
                    Section = vm.Section,
                    Row = vm.Row,
                    Chair = vm.Chair
                };

                _db.SeatInfos.Add(seat);
            }

            _db.SaveChanges();

            return seat.Id;
        }

        public void AttachSeatToUser(int seatId)
        {
            ApplicationUser user = _db.Users.Where(u => u.Email == seatId.ToString() + "@gmail.com").FirstOrDefault();

            if(user != null)
            {
                user.SeatInfoId = seatId;
            }

            _db.SaveChanges();
        }

        public void UpdatePark(SaveParkVM vm)
        {
            Park park = _db.Parks.Select(p => p).FirstOrDefault();

            if (vm.Name != null) park.Name = vm.Name;
            if (vm.LogoUrl != null) park.LogoUrl = vm.LogoUrl;
            if (vm.TaxRate > 0) park.TaxRate = vm.TaxRate;

            _db.SaveChanges();
        }

        public void SaveArea(SaveAreaVM vm)
        {
            Area area = null;
            int parkId = _db.Parks.FirstOrDefault().Id;

            if (vm.Id > 0)
            {
                area = _db.Areas.Where(a => a.Id == vm.Id).FirstOrDefault();

                if (vm.Name != null) area.Name = vm.Name;
                if (vm.IsRetired) area.IsRetired = vm.IsRetired;
            }
            else
            {
                area = new Area
                {
                    ParkId = parkId,
                    Name = vm.Name
                };

                _db.Areas.Add(area);
            }

            _db.SaveChanges();
        }

        public void SaveVendor(SaveVendorVM vm)
        {
            Restaurant vendor = null;

            if (vm.Id > 0)
            {
                vendor = _db.Restaurants.Where(v => v.Id == vm.Id).FirstOrDefault();

                if (vm.Name != null) vendor.Name = vm.Name;
                if (vm.Description != null) vendor.Description = vm.Description;
                if (vm.IsRetired) vendor.IsRetired = vm.IsRetired;
            }
            else if (vm.AreaId > 0)
            {
                vendor = new Restaurant
                {
                    AreaId = vm.AreaId,
                    Name = vm.Name,
                    Description = vm.Description
                };

                _db.Restaurants.Add(vendor);
            }

            _db.SaveChanges();
        }

        public void SaveCategory(SaveCategoryVM vm)
        {
            Category category = null;

            if (vm.Id > 0)
            {
                category = _db.Categories.Where(v => v.Id == vm.Id).FirstOrDefault();

                if (vm.Name != null) category.Name = vm.Name;
                if (vm.IsRetired) category.IsRetired = vm.IsRetired;
            }
            else if (vm.VendorId > 0)
            {
                category = new Category
                {
                    RestaurantId = vm.VendorId,
                    Name = vm.Name
                };

                _db.Categories.Add(category);
            }

            _db.SaveChanges();
        }

        public void SaveItem(SaveItemVM vm)
        {
            Item item = null;

            if (vm.Id > 0)
            {
                item = _db.Items.Where(v => v.Id == vm.Id).FirstOrDefault();

                if (vm.Name != null) item.Name = vm.Name;
                if (vm.ImageUrl != null) item.ImageUrl = vm.ImageUrl;
                if (vm.Price > 0) item.Price = vm.Price;
                item.IsAlcohol = vm.IsAlcohol;
                if (vm.IsRetired) item.IsRetired = vm.IsRetired;
            }
            else if (vm.CategoryId > 0)
            {
                item = new Item
                {
                    CategoryId = vm.CategoryId,
                    Name = vm.Name,
                    ImageUrl = vm.ImageUrl,
                    Price = vm.Price,
                    IsAlcohol = vm.IsAlcohol
                };

                _db.Items.Add(item);
            }

            _db.SaveChanges();
        }

        public void PostEmployee(EmployeeVM vm)
        {
            ApplicationUser user = _db.Users.Where(u => u.UserName == vm.Username).FirstOrDefault();
            Park park = _db.Parks.FirstOrDefault();
            RestaurantManager rm = null;

            if(user != null)
            {
                if (vm.IsRunner)
                {
                    if (user.RunnerInfoId != null)
                    {
                        user.RunnerInfo.IsRetired = false;
                        user.RunnerInfo.Name = vm.Name;
                    }
                    else user.RunnerInfo = new RunnerInfo { Name = vm.Name };
                }
                else if (user.RunnerInfoId != null) user.RunnerInfo.IsRetired = true;

                if (vm.IsVendor)
                {
                    if (user.VendorInfoId != null)
                    {
                        user.VendorInfo.IsRetired = false;
                        user.VendorInfo.Name = vm.Name;
                        if(vm.Restaurant != null) user.VendorInfo.RestaurantId = vm.Restaurant.Id;
                    }
                    else user.VendorInfo = new VendorInfo { Name = vm.Name, RestaurantId = vm.Restaurant.Id };
                }
                else if (user.VendorInfoId != null) user.VendorInfo.IsRetired = true;

                if (vm.IsManager)
                {
                    if (user.ManagerInfoId != null)
                    {
                        user.ManagerInfo.IsRetired = false;
                        user.ManagerInfo.Name = vm.Name;
                        if (vm.RestaurantIds != null)
                        {
                            if(user.ManagerInfo.RestaurantManagers != null)
                            {
                                foreach (RestaurantManager restMan in user.ManagerInfo.RestaurantManagers)
                                {
                                    restMan.IsRetired = true;
                                }
                            }
                            
                            foreach (int restId in vm.RestaurantIds)
                            {
                                rm = null;
                                rm = _db.RestaurantManagers.Where(r => r.ManagerInfoId == user.ManagerInfoId && r.RestaurantId == restId).FirstOrDefault();
                                if (rm != null)
                                {
                                    rm.IsRetired = false;
                                }
                                else
                                {
                                    user.ManagerInfo.RestaurantManagers.Add(new RestaurantManager
                                    {
                                        ManagerInfoId = user.ManagerInfo.Id,
                                        RestaurantId = restId
                                    });
                                }
                            }
                        }
                    }
                    else
                    {
                        user.ManagerInfo = new ManagerInfo
                        {
                            Name = vm.Name,
                        };

                        foreach (int restId in vm.RestaurantIds)
                        {
                            _db.RestaurantManagers.Add(new RestaurantManager
                            {
                                ManagerInfoId = user.ManagerInfo.Id,
                                RestaurantId = restId
                            });
                        }
                    }
                }
                else if (user.ManagerInfoId != null) user.ManagerInfo.IsRetired = true;

                if (vm.IsAdmin)
                {
                    if (user.AdminInfoId != null)
                    {
                        user.AdminInfo.IsRetired = false;
                        user.AdminInfo.Name = vm.Name;
                    }
                    else user.AdminInfo = new AdminInfo { Name = vm.Name, ParkId = park.Id };
                }
                else if (user.AdminInfoId != null) user.AdminInfo.IsRetired = true;
            }

            _db.SaveChanges();
        }
    }
}
