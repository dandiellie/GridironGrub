using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Admin
{
    public class ParkVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public decimal TaxRate { get; set; }
        public List<AreaVM> Areas { get; set; }
    }

    public class AreaVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<VendorVM> Vendors { get; set; }
    }

    public class VendorVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<CategoryVM> Categories { get; set; }
    }

    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemVM> Items { get; set; }
    }

    public class ItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Views { get; set; }
        public int Purchases { get; set; }
        public bool IsAlcohol { get; set; }
    }
}
