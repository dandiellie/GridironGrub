using GridironGrub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace GridironGrub.Web.Controllers.api
{
    [Authorize]
    public class VendorController : ApiController
    {
        private UnitOfWork _unit;

        public VendorController()
        {
            _unit = new UnitOfWork();
        }

        [ActionName("open")]
        public IHttpActionResult GetOpen()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsVendor(userId))
            {
                var vm = _unit.Vendor.GetOpenOrders(userId);

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("recent")]
        public IHttpActionResult GetRecent()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsVendor(userId))
            {
                var vm = _unit.Vendor.GetRecentOrders(userId);

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("complete")]
        public IHttpActionResult GetComplete(int orderId)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsVendor(userId))
            {
                _unit.Vendor.CompleteOrder(orderId);

                return Ok();
            }
            return Unauthorized();
        }
    }
}