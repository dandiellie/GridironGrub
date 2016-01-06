using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Customer
{
    public class RestaurantVM
    {
        public int SeatId { get; set; }
        public string Name { get; set; }
        public List<CategoryListVM> Categories { get; set; }
    }

    public class CategoryListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemListVM> Items { get; set; }
    }

    public class ItemListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public bool IsAlcohol { get; set; }
    }
}
