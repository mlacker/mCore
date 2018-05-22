using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mCore.Services.IdentityServer.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Hosting;

namespace mCore.Services.IdentityServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly IHostingEnvironment environment;

        public HomeController(
            IIdentityServerInteractionService interaction, 
            IHostingEnvironment environment)
        {
            this.interaction = interaction;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;

                if (!environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }

            return View("Error", vm);
        }
    }
}
