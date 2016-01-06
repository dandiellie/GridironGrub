using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Customer
{
    public class AddItemVM
    {
        public string PersDesc { get; set; }
        public string ReceiptEmail { get; set; }
        public bool SubmitOrder { get; set; }
        public int CardId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public bool IsRetired { get; set; }
        public int RemoveItemId { get; set; }
    }
}
