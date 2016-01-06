using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Admin
{
    public class OrderVM
    {
        public int Id { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Taxes { get; set; }
        public decimal ConvenienceFee { get; set; }
        public decimal OutOfAreaFee { get; set; }
        public decimal Total { get; set; }
        public DateTime? TimeOrdered { get; set; }
        public DateTime? TimePrepared { get; set; }
        public DateTime? TimeDelivered { get; set; }
        public string Runner { get; set; }
        public string Restaurant { get; set; }
        public string Area { get; set; }
        public string Section { get; set; }
        public string Row { get; set; }
        public string Chair { get; set; }
        public List<OrderItemVM> Items { get; set; }
    }

    public class OrderItemVM
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
