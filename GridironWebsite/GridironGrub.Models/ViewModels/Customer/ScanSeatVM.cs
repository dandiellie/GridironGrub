using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Customer
{
    public class ScanSeatVM
    {
        public string Park { get; set; }
        public string LogoUrl { get; set; }
        public List<RestaurantListVM> RestsInArea { get; set; }
        public List<RestaurantListVM> RestsOutArea { get; set; }
    }

    public class RestaurantListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
