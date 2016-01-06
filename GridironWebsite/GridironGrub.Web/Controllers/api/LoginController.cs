using GridironGrub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using GridironGrub.Models.ViewModels.User;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace GridironGrub.Web.Controllers.api
{
    public class LoginController : ApiController
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

        public LoginController()
        {
            _unit = new UnitOfWork();
        }

        [ActionName("loginInfo")]
        public IHttpActionResult GetLoginInfo(int seatId)
        {
            var vm = _unit.Customer.getLoginInfo(seatId);

            return Ok(vm);
        }

        [Authorize]
        [ActionName("infoIds")]
        public IHttpActionResult GetInfoIds()
        {
            string userId = User.Identity.GetUserId();
            InfoIdVM vm = _unit.User.getUserInfoIds(userId);

            return Ok(vm);
        }

        [Authorize]
        [ActionName("roles")]
        public IHttpActionResult GetRoles()
        {
            string userId = User.Identity.GetUserId();
            RolesVM vm = _unit.User.GetRoles(userId);

            return Ok(vm);
        }

        [ActionName("forgotPassword")]
        public IHttpActionResult GetForgotPassword(string email)
        {
            _unit.User.SendPasswordReset(email);

            return Ok();
        }

        [ActionName("resetPassword")]
        public IHttpActionResult PostResetPassword(ResetPasswordVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unit.User.ClearPassword(vm.UserId);

            IdentityResult result = UserManager.AddPassword(vm.UserId, vm.NewPassword);

            return Ok();
        }
    }
}
