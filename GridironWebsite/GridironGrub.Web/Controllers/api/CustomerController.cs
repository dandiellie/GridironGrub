using GridironGrub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using GridironGrub.Models.ViewModels.Customer;
using Microsoft.Owin;
using Braintree;

namespace GridironGrub.Web.Controllers.api
{
    [Authorize]
    public class CustomerController : ApiController
    {
        private UnitOfWork _unit;

        public CustomerController()
        {
            _unit = new UnitOfWork();
        }

        //[ActionName("profile")]
        //public IHttpActionResult GetProfile()
        //{
        //    var userId = User.Identity.GetUserId();
        //    var vm = _unit.Customer.getCustomerProfile(userId);

        //    return Ok(vm);
        //}

        [ActionName("welcome")]
        public IHttpActionResult GetWelcome(int seatId)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsSeat(userId) || _unit.User.IsCustomer(userId))
            {
                var vm = _unit.Customer.getWelcome(seatId);

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("scan")]
        public IHttpActionResult GetScan(int seatId)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsSeat(userId) || _unit.User.IsCustomer(userId))
            {
                var vm = _unit.Customer.getCustomerScan(seatId);

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("restaurant")]
        public IHttpActionResult GetRestaurant(int restaurantId)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsSeat(userId) || _unit.User.IsCustomer(userId))
            {
                var vm = _unit.Customer.getRestaurant(userId, restaurantId);

                return Ok(vm);
            }
            return Unauthorized();            
        }
        
        [ActionName("cart")]
        public IHttpActionResult GetCart()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsSeat(userId) || _unit.User.IsCustomer(userId))
            {
                var vm = _unit.Customer.getCart(userId);

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("navbarBadge")]
        public IHttpActionResult GetNavbarBadge()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsSeat(userId) || _unit.User.IsCustomer(userId))
            {
                int NumItems = _unit.Customer.getNavbarBadge(userId);

                return Ok(NumItems);
            }
            return Unauthorized();
        }

        [ActionName("alcohol")]
        public IHttpActionResult GetAlcohol()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsSeat(userId) || _unit.User.IsCustomer(userId))
            {
                bool containsAlcohol = _unit.Customer.ContainsAlcohol(userId);

                return Ok(containsAlcohol);
            }
            return Unauthorized();
        }

        [ActionName("token")]
        public IHttpActionResult GetToken()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsSeat(userId) || _unit.User.IsCustomer(userId))
            {
                string token = _unit.Payment.getClientToken(userId);

                return Ok(token);
            }
            return Unauthorized();
        }

        [ActionName("postProfile")]
        public IHttpActionResult PostProfile(CustomerProfileVM vm)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsSeat(userId) || _unit.User.IsCustomer(userId))
            {
                _unit.Customer.postProfile(userId, vm);

                return Ok();
            }
            return Unauthorized();
        }

        [ActionName("addItem")]
        public IHttpActionResult PostAddItem(AddItemVM vm)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsSeat(userId) || _unit.User.IsCustomer(userId))
            {
                _unit.Customer.postAddItem(userId, 0, vm);

                return Ok();
            }
            return Unauthorized();
        }

        [ActionName("retireOrder")]
        public IHttpActionResult GetRetireOrder()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsSeat(userId) || _unit.User.IsCustomer(userId))
            {
                _unit.Customer.RetireOrder(userId);

                return Ok();
            }
            return Unauthorized();
        }

        [ActionName("purchase")]
        public IHttpActionResult GetPurchase(string nonce)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsSeat(userId) || _unit.User.IsCustomer(userId))
            {
                // Use payment method nonce here
                Result<Transaction> result = _unit.Payment.postPurchaseNonce(userId, nonce);

                return Ok(result);
            }
            return Unauthorized();
        }

        //[ActionName("receipt")]
        //public IHttpActionResult GetReceipt(string email)
        //{
        //    var userId = User.Identity.GetUserId();
        //    if (_unit.User.IsSeat(userId) || _unit.User.IsCustomer(userId))
        //    {
        //        _unit.Customer.sendReceipt(0, email);

        //        return Ok(email);
        //    }
        //    return Unauthorized();
        //}
    }
}
