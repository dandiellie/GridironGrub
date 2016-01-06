using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Runner
{
    public class OrderVM
    {
        public int Id { get; set; }
        public string Restaurant { get; set; }
        public string Area { get; set; }
        public string Section { get; set; }
        public string Row { get; set; }
        public string Chair { get; set; }
        public string PersDesc { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Taxes { get; set; }
        public decimal ConvenienceFee { get; set; }
        public decimal OutOfAreaFee { get; set; }
        public decimal Total { get; set; }
        public bool ContainsAlcohol { get; set; }
        public DateTime? TimeOrdered { get; set; }
        public DateTime? TimePrepared { get; set; }
        public List<ItemVM> Items { get; set; }
    }

    public class ItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public bool IsAlcohol { get; set; }
    }
}
