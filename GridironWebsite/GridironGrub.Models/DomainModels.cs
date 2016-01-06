using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models
{
    public class Universe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRetired { get; set; }
    }

    public class Park : Universe
    {
        public string LogoUrl { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public decimal TaxRate { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<AdminInfo> AdminInfos { get; set; }
    }

    public class Area : Universe
    {
        [ForeignKey("Park")]
        public int ParkId { get; set; }
        public virtual Park Park { get; set; }

        public virtual ICollection<SeatInfo> SeatInfos { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }

    public class Restaurant : Universe
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<string> Section { get; set; }

        [ForeignKey("Area")]
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<RestaurantManager> RestaurantManagers { get; set; }
        public virtual ICollection<VendorInfo> VendorInfos { get; set; }
    }

    public class Category : Universe
    {
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }

    public class Item : Universe
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public ICollection<DateTime> Views { get; set; }
        public ICollection<DateTime> Purchases { get; set; }
        public bool IsAlcohol { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }

    public class SeatInfo : Universe
    {
        public string Section { get; set; }
        public string Row { get; set; }
        public string Chair { get; set; }
        public string BraintreeCustomerId { get; set; }

        [ForeignKey("Area")]
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
    }

    public class CustomerInfo : Universe
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public SeatInfo SeatInfo { get; set; }
        public string BraintreeCustomerId { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

    public class Order : Universe
    {
        public decimal Subtotal { get; set; }
        public decimal Taxes { get; set; }
        public decimal ConvenienceFee { get; set; }
        public decimal OutOfAreaFee { get; set; }
        public decimal Total { get; set; }
        public string PersDesc { get; set; }
        public DateTime? TimeOrdered { get; set; }
        public DateTime? TimePrepared { get; set; }
        public DateTime? TimeDelivered { get; set; }
        public string ReceiptEmail { get; set; }

        [ForeignKey("CustomerInfo")]
        public int? CustomerId { get; set; }
        public virtual CustomerInfo CustomerInfo { get; set; }

        [ForeignKey("RunnerInfo")]
        public int? RunnerId { get; set; }
        public virtual RunnerInfo RunnerInfo { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        [ForeignKey("SeatInfo")]
        public int SeatInfoId { get; set; }
        public virtual SeatInfo SeatInfo { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }

    public class RunnerInfo : Universe
    {
        public virtual ICollection<Order> Orders { get; set; }
    }

    public class OrderItem : Universe
    {
        public int Quantity { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }

    public class ManagerInfo : Universe
    {
        public virtual ICollection<RestaurantManager> RestaurantManagers { get; set; }
    }

    public class VendorInfo : Universe
    {
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }

    public class AdminInfo : Universe
    {
        [ForeignKey("Park")]
        public int ParkId { get; set; }
        public virtual Park Park { get; set; }
    }

    public class RestaurantManager : Universe
    {
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        [ForeignKey("ManagerInfo")]
        public int ManagerInfoId { get; set; }
        public virtual ManagerInfo ManagerInfo { get; set; }
    }
}
