// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreSecurity.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Google(string returnUrl)
        {
            if (!Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/";
            }

            var props = new AuthenticationProperties
            {
                RedirectUri = "/Account/ExternalCallback"
            };
            props.Items.Add("realReturnUrl", returnUrl);
            
            return Challenge(props, "Google");
        }

        public async Task<IActionResult> ExternalCallback()
        {
            var result = await HttpContext.AuthenticateAsync("Temp");
            if (!result.Succeeded) return Redirect("~/");

            var nameId = result.Principal.FindFirst(ClaimTypes.NameIdentifier);
            var externalUserId = nameId.Value;
            var provider = nameId.Issuer;

            // TODO: look up user based on provider & externalUserId

            var claims = new[]{
                new Claim("sub", "89378937893"),
                new Claim("name", "Brock Allen"),
                new Claim("email", "BrockAllen@gmail.com"),
                new Claim("language", "en-US"),
                new Claim("role", "Geek"),
                new Claim("role", "Nerd"),
                new Claim("officeLocation", "Orlando"),
                new Claim("idp", provider),
            };
            var ci = new ClaimsIdentity(claims, "external", "name", "role");
            var user = new ClaimsPrincipal(ci);

            await HttpContext.SignInAsync("Cookies", user);
            await HttpContext.SignOutAsync("Temp");

            var url = result.Properties.Items["realReturnUrl"];
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }

            return Redirect("~/");
        }

        // TODO: get a real developer to code this and only support POST
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return Redirect("~/");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            string userName, 
            string password, 
            string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!string.IsNullOrWhiteSpace(userName) &&
                userName == password)
            {
                var claims = new[]{
                    new Claim("sub", "89378937893"),
                    new Claim("name", "Brock Allen"),
                    new Claim("email", "BrockAllen@gmail.com"),
                    new Claim("language", "en-US"),
                    new Claim("officeLocation", "Orlando"),
                    new Claim("role", "Geek"),
                    new Claim("role", "Nerd"),
                };
                var ci = new ClaimsIdentity(claims, "pwd", "name", "role");
                var user = new ClaimsPrincipal(ci);

                await HttpContext.SignInAsync("Cookies", user);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("~/");
                }
            }

            return View();
        }

        public IActionResult AccessDenied() => View();
    }
}