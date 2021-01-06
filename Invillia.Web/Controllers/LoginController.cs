using Invillia.ExternalService.App;
using Invillia.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Invillia.Web.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {
            InvilliaApp.InitializeConfig("https://localhost:44310/");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> IndexAsync(LoginViewModel loginViewModel, string returnUrl)
        {
            var user = InvilliaApp.Login(loginViewModel.Login, loginViewModel.Password);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.User.Name),
                new Claim(ClaimTypes.Expiration, user.Expires.ToString()),
                new Claim(ClaimTypes.Role, user.Token),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }
    }
}