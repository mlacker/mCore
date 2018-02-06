namespace mCore.Services.Identity.API.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using mCore.Services.Identity.API.Models;
    using mCore.Services.Identity.API.Models.AccountViewModels;
    using mCore.Services.Identity.API.Services;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ILoginService<ApplicationUser> loginService;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(
            ILoginService<ApplicationUser> loginService,
            UserManager<ApplicationUser> userManager)
        {
            this.loginService = loginService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await loginService.FindByUsername(model.Username);

                if (await loginService.ValidateCredentials(user, model.Password))
                {
                    AuthenticationProperties props = null;
                    if (model.RememberMe)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddYears(10)
                        };
                    }

                    await loginService.SignIn(user);

                    return Redirect("~/");
                }

                ModelState.AddModelError(string.Empty, "Invalid username or password.");
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task Logout()
        {
            // delete authentication cookie
            await HttpContext.SignOutAsync();

            // set this so UI rendering sees an anonymous user
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<ApplicationUser>(model);

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Errors.Count() > 0)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return BadRequest(ModelState);
                }
            }

            return Ok();
        }
    }
}
