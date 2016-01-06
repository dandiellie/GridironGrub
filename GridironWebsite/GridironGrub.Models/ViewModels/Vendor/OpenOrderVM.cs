using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Vendor
{
    public class OpenOrderVM
    {
        public int Id { get; set; }
        public DateTime? TimeOrdered { get; set; }
        public List<ItemVM> Items { get; set; }
    }

    public class ItemVM
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public bool IsAlcohol { get; set; }
    }
}
