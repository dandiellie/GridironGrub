using GridironGrub.Models;
using GridironGrub.Models.ViewModels.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Data.Repositories
{
    public class ManagerRepository
    {
        private ApplicationDbContext _db;

        public ManagerRepository()
        {
            _db = new ApplicationDbContext();
        }

        public ParkVM GetVendors(string id)
        {
            int? managerId = _db.Users.Where(u => u.Id == id).FirstOrDefault().ManagerInfoId;

            ParkVM vm = null;
            List<VendorVM> vendors = null;

            if(managerId != null)
            {
                vendors = _db.RestaurantManagers.OrderBy(rm => rm.Restaurant.Name).Where(rm => rm.ManagerInfoId == managerId && !rm.IsRetired).Select(v => new VendorVM
                {
                    Id = v.Restaurant.Id,
                    Name = v.Restaurant.Name,
                    Description = v.Restaurant.Description,
                    Categories = v.Restaurant.Categories.OrderBy(c => c.Name).Where(c => !c.IsRetired).Select(c => new CategoryVM
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Items = c.Items.OrderBy(i => i.Name).Where(i => !i.IsRetired).Select(i => new ItemVM
                        {
                            Id = i.Id,
                            Name = i.Name,
                            ImageUrl = i.ImageUrl,
                            Price = i.Price,
                            IsAlcohol = i.IsAlcohol
                        }).ToList()
                    }).ToList()
                }).ToList();

                vm = _db.Parks.Select(p => new ParkVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    LogoUrl = p.LogoUrl,
                    TaxRate = p.TaxRate
                }).FirstOrDefault();

                vm.Vendors = vendors;
            }

            return vm;
        }

        public VendorVM GetVendor(int vendorId)
        {
            VendorVM vm = null;

            if (vendorId > 0)
            {
                vm = _db.Restaurants.OrderBy(r => r.Name).Where(r => r.Id == vendorId && !r.IsRetired).Select(v => new VendorVM
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    Categories = v.Categories.OrderBy(c => c.Name).Where(c => !c.IsRetired).Select(c => new CategoryVM
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Items = c.Items.OrderBy(i => i.Name).Where(i => !i.IsRetired).Select(i => new ItemVM
                        {
                            Id = i.Id,
                            Name = i.Name,
                            ImageUrl = i.ImageUrl,
                            Price = i.Price,
                            IsAlcohol = i.IsAlcohol
                        }).ToList()
                    }).ToList()
                }).FirstOrDefault();
            }

            return vm;
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
    }
}
