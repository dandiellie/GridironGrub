using GridironGrub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using GridironGrub.Models.ViewModels.Manager;

namespace GridironGrub.Web.Controllers.api
{
    [Authorize]
    public class ManagerController : ApiController
    {
        private UnitOfWork _unit;

        public ManagerController()
        {
            _unit = new UnitOfWork();
        }

        [ActionName("vendors")]
        public IHttpActionResult GetVendors()
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsManager(userId))
            {
                var vm = _unit.Manager.GetVendors(userId);

                return Ok(vm);
            }
            return Unauthorized();
        }

        [ActionName("vendor")]
        public IHttpActionResult GetVendor(int vendorId)
        {
            var userId = User.Identity.GetUserId();
            if (_unit.User.IsManager(userId))
            {
                var vm = _unit.Manager.GetVendor(vendorId);

                return Ok(vm);
            }
            return Unauthorized();
        }

        //[ActionName("vendor")]
        //public IHttpActionResult PostVendor(SaveVendorVM vm)
        //{
        //    _unit.Manager.SaveVendor(vm);

        //    return Ok();
        //}

        //[ActionName("category")]
        //public IHttpActionResult PostCategory(SaveCategoryVM vm)
        //{
        //    _unit.Manager.SaveCategory(vm);

        //    return Ok();
        //}

        //[ActionName("item")]
        //public IHttpActionResult PostItem(SaveItemVM vm)
        //{
        //    _unit.Manager.SaveItem(vm);

        //    return Ok();
        //}
    }
}
