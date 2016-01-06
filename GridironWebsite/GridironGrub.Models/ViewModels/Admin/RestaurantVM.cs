using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Admin
{
    public class RestaurantVM
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string Name { get; set; }
        public bool IsRetired { get; set; }
    }
}
