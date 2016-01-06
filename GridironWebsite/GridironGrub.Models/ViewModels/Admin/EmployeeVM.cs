using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Admin
{
    public class EmployeeVM
    {
        public string Username { get; set; }
        public string Name { get; set; }

        public bool IsRunner { get; set; }

        public bool IsVendor { get; set; }
        [Required]
        public RestaurantVM Restaurant { get; set; }

        public bool IsManager { get; set; }
        public List<int> RestaurantIds { get; set; }
        public List<RestaurantVM> Restaurants { get; set; }

        public bool IsAdmin { get; set; }
    }
}
