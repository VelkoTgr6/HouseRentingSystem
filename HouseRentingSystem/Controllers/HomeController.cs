using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Home;
using HouseRentingSystem.Infrastructure.Data.Common;
using HouseRentingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HouseRentingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IHouseService houseService;

        public HomeController(
            ILogger<HomeController> _logger,
            IHouseService _houseService)
        {
            logger = _logger;
            houseService = _houseService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await houseService.LastThreeHouses();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
