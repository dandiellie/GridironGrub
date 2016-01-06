using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Customer
{
    public class CartVM
    {
        public List<ItemVM> Items { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Taxes { get; set; }
        public decimal ConvenienceFee { get; set; }
        public decimal OutOfAreaFee { get; set; }
        public decimal Total { get; set; }
        public string PersDesc { get; set; }
        public DateTime? TimeOrdered { get; set; }
        public DateTime? TimePrepared { get; set; }
        public DateTime? TimeDelivered { get; set; }
    }

    public class ItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public bool IsAlcohol { get; set; }
    }
}
