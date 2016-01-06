using GridironGrub.Data.Repositories;
using GridironGrub.Models;
using GridironGrub.Models.ViewModels.Admin;
using GridironGrub.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GridironGrub.Web.Controllers.api
{
    [Authorize]
    public class AdminController : ApiController
    {
        private UnitOfWork _unit;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public AdminController()
        {
            _unit = new UnitOfWork();
        }

        [ActionName("open")]
        public IHttpActionResult GetOpen()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId))
            {
                var vm = _unit.Admin.GetOpenOrders();

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("recent")]
        public IHttpActionResult GetRecent(DateTime? date)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId))
            {
                var vm = _unit.Admin.GetRecentOrders(date);

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("specific")]
        public IHttpActionResult GetSpecific(int orderId)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId))
            {
                var vm = _unit.Admin.GetSpecificOrders(orderId);

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("park")]
        public IHttpActionResult GetPark()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId))
            {
                var vm = _unit.Admin.GetPark();

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("vendor")]
        public IHttpActionResult GetVendor(int restId)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId) || _unit.User.IsManager(userId))
            {
                var vm = _unit.Admin.GetVendor(restId);

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("seats")]
        public IHttpActionResult GetSeats()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId))
            {
                var vm = _unit.Admin.GetSeats();

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("restaurants")]
        public IHttpActionResult GetRestaurants()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId))
            {
                var vm = _unit.Admin.GetRestaurants();

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("employees")]
        public IHttpActionResult GetEmployees()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId))
            {
                var vm = _unit.Admin.GetEmployees();

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("employee")]
        public IHttpActionResult GetEmployee(string username)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId))
            {
                var vm = _unit.Admin.GetEmployee(username);

                return Ok(vm);
            }
            return Unauthorized();
        }

        //[ActionName("seat")]
        //public async Task<IHttpActionResult> PostSeat(SeatVM vm)
        //{
        //    int seatId = _unit.Admin.PostSeat(vm);

        //    if(vm.Id == 0)
        //    {
        //        //create new user
        //        RegisterBindingModel rbm = new RegisterBindingModel
        //        {
        //            Email = seatId.ToString() + "@gmail.com",
        //            Password = "Password#1",
        //            ConfirmPassword = "Password#1"
        //        };
        //        AccountController ac = new AccountController();
        //        var result = await ac.Register(rbm);

        //        //set new user SeatInfo
        //        _unit.Admin.AttachSeatToUser(seatId);
        //    }

        //    return Ok();
        //}

        [ActionName("seat")]
        public IHttpActionResult PostSeat(SeatVM vm)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId))
            {
                int seatId = _unit.Admin.PostSeat(vm);

                if (vm.Id == 0)
                {
                    //create new user
                    RegisterBindingModel rbm = new RegisterBindingModel
                    {
                        Email = seatId.ToString() + "@gmail.com",
                        Password = "Password#1"
                    };

                    var user = new ApplicationUser()
                    {
                        UserName = rbm.Email,
                        Email = rbm.Email
                    };

                    IdentityResult result = UserManager.Create(user, rbm.Password);

                    //set new user SeatInfo
                    _unit.Admin.AttachSeatToUser(seatId);
                }

                return Ok();
            }
            return Unauthorized();
        }

        [ActionName("park")]
        public IHttpActionResult PostPark(SaveParkVM vm)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId))
            {
                _unit.Admin.UpdatePark(vm);

                return Ok();
            }
            return Unauthorized();
        }

        [ActionName("area")]
        public IHttpActionResult PostArea(SaveAreaVM vm)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId))
            {
                _unit.Admin.SaveArea(vm);

                return Ok();
            }
            return Unauthorized();
        }

        [ActionName("restaurant")]
        public IHttpActionResult PostRestaurant(SaveVendorVM vm)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId) || _unit.User.IsManager(userId))
            {
                _unit.Admin.SaveVendor(vm);

                return Ok();
            }
            return Unauthorized();
        }

        [ActionName("category")]
        public IHttpActionResult PostCategory(SaveCategoryVM vm)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId) || _unit.User.IsManager(userId))
            {
                _unit.Admin.SaveCategory(vm);

                return Ok();
            }
            return Unauthorized();
        }

        [ActionName("item")]
        public IHttpActionResult PostItem(SaveItemVM vm)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId) || _unit.User.IsManager(userId))
            {
                _unit.Admin.SaveItem(vm);

                return Ok();
            }
            return Unauthorized();
        }

        [ActionName("updateEmployee")]
        public IHttpActionResult PostUpdateEmployee(EmployeeVM vm)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsAdmin(userId))
            {
                _unit.Admin.PostEmployee(vm);

                return Ok();
            }
            return Unauthorized();
        }
    }
}