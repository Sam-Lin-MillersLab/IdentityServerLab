// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AspNetCoreSecurity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authz;

        public HomeController(IAuthorizationService authz)
        {
            _authz = authz;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize("ManageCustomers")]
        [Authorize]
        public async Task<IActionResult> Secure()
        {
            //var result = await _authz.AuthorizeAsync(User, "ManageCustomer");
            //if (!result.Succeeded)
            //{
            //    if (User.Identity.IsAuthenticated) return Forbid();
            //    return Challenge();
            //}
            //var name = User.FindFirst("name").Value;

            return View();
        }
    }
}
