using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Admin
{
    public class SaveParkVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public decimal TaxRate { get; set; }
    }

    public class SaveAreaVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRetired { get; set; }
    }

    public class SaveVendorVM
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsRetired { get; set; }
    }

    public class SaveCategoryVM
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public bool IsRetired { get; set; }
    }

    public class SaveItemVM
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsAlcohol { get; set; }
        public bool IsRetired { get; set; }
    }
}
