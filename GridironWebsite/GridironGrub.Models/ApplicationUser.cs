using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("CustomerInfo")]
        public int? CustomerInfoId { get; set; }
        public virtual CustomerInfo CustomerInfo { get; set; }

        [ForeignKey("SeatInfo")]
        public int? SeatInfoId { get; set; }
        public virtual SeatInfo SeatInfo { get; set; }

        [ForeignKey("RunnerInfo")]
        public int? RunnerInfoId { get; set; }
        public virtual RunnerInfo RunnerInfo { get; set; }

        [ForeignKey("ManagerInfo")]
        public int? ManagerInfoId { get; set; }
        public virtual ManagerInfo ManagerInfo { get; set; }

        [ForeignKey("VendorInfo")]
        public int? VendorInfoId { get; set; }
        public virtual VendorInfo VendorInfo { get; set; }

        [ForeignKey("AdminInfo")]
        public int? AdminInfoId { get; set; }
        public virtual AdminInfo AdminInfo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
