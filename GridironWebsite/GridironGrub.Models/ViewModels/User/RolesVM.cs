using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.User
{
    public class RolesVM
    {
        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
        public bool IsVendor { get; set; }
        public bool IsRunner { get; set; }
        public bool IsSeat { get; set; }
        public bool IsCustomer { get; set; }
    }

    public class RolesAsStringsVM
    {
        public string IsAdmin { get; set; }
        public string IsManager { get; set; }
        public string IsVendor { get; set; }
        public string IsRunner { get; set; }
        public string IsSeat { get; set; }
        public string IsCustomer { get; set; }
    }
}
