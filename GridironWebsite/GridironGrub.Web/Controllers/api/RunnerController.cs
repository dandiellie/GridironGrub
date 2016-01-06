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
    public class RunnerController : ApiController
    {
        private UnitOfWork _unit;

        public RunnerController()
        {
            _unit = new UnitOfWork();
        }

        [ActionName("profile")]
        public IHttpActionResult GetProfile()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsRunner(userId))
            {
                var vm = _unit.Runner.GetProfile(userId);

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("open")]
        public IHttpActionResult GetOpen()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsRunner(userId))
            {
                var vm = _unit.Runner.GetOpenOrders();

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("active")]
        public IHttpActionResult GetRecent()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsRunner(userId))
            {
                var vm = _unit.Runner.GetActiveOrders(userId);

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("history")]
        public IHttpActionResult GetHistory()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsRunner(userId))
            {
                var vm = _unit.Runner.GetOrderHistory(userId);

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("accept")]
        public IHttpActionResult PostAccept(int orderId)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsRunner(userId))
            {
                _unit.Runner.AcceptOrder(userId, orderId);

                return Ok();
            }
            return Unauthorized();
        }

        [ActionName("deliver")]
        public IHttpActionResult PostDeliver(int orderId)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsRunner(userId))
            {
                _unit.Runner.DeliverOrder(orderId);

                return Ok();
            }
            return Unauthorized();
        }
    }
}
