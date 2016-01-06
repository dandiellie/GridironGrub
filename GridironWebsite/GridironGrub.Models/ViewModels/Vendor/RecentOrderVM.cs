using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Vendor
{
    public class RecentOrderVM
    {
        public int Id { get; set; }
        public DateTime? TimeOrdered { get; set; }
        public DateTime? TimePrepared { get; set; }
        public List<ItemVM> Items { get; set; }
    }
}
