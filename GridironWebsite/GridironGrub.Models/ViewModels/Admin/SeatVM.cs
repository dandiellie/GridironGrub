using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models.ViewModels.Admin
{
    public class ParkSeatVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public List<AreaSeatVM> Areas { get; set; }
    }

    public class AreaSeatVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SeatVM> Seats { get; set; }
    }

    public class SeatVM
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string Section { get; set; }
        public string Row { get; set; }
        public string Chair { get; set; }
        public bool IsRetired { get; set; }
    }
}
