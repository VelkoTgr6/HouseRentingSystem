using HouseRentingSystem.Attributes;
using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static HouseRentingSystem.Core.Constants.ErrorConstants;

namespace HouseRentingSystem.Controllers
{
    public class HouseController : BaseController
    {
        private readonly IHouseService houseService;
        private readonly IAgentService agentService;

        public HouseController(
            IHouseService _houseService,
            IAgentService _agentService)
        {
            houseService = _houseService;
            agentService = _agentService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {
            var model = await houseService.AllAsync(
               query.Category,
               query.SearchTerm,
               query.Sorting,
               query.CurrentPage,
               query.HousesPerPage);

            query.TotalHousesCount = model.TotalHousesCount;
            query.Houses = model.Houses;
            query.Categories = await houseService.AllCategoriesNamesAsync();

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var userId = User.GetId();
            IEnumerable<HouseServiceModel> model;

            if (await agentService.ExistByIdAsync(userId))
            {
                var agentId = await agentService.GetAgentIdAsync(userId) ?? 0;
                model = await houseService.AllHousesByAgentIdAsync(agentId);
            }
            else
            {
                model = await houseService.AllHousesByUserIdAsync(userId);
            }


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await houseService.ExistAsync(id) == false)
            {
                return BadRequest();
            }

            var model = await houseService.HouseDetailsByIdAsync(id);

            return View(model);
        }

        [HttpGet]
        [MustBeAgent]
        public async Task<IActionResult> Add()
        {
            var model = new HouseFormModel()
            {
                Categories = await houseService.AllCategoriesAsync()
            };
            return View(model);
        }

        [HttpPost]
        [MustBeAgent]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if (await houseService.CategoryExistAsync(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), InvalidCategoryMessage);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await houseService.AllCategoriesAsync();
                return View(model);
            }

            int? agentId = await agentService.GetAgentIdAsync(User.GetId());

            var newHouseId = await houseService.CreateAsync(model, agentId ?? 0);

            return RedirectToAction(nameof(Details), new { id = newHouseId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await houseService.ExistAsync(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithId(id, User.GetId()) == false)
            {
                return Unauthorized();
            }

            var model = await houseService.GetHouseFormModelByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseFormModel model)
        {
            if (await houseService.ExistAsync(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithId(id, User.GetId()) == false)
            {
                return Unauthorized();
            }

            if (await houseService.CategoryExistAsync(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), InvalidCategoryMessage);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await houseService.AllCategoriesAsync();
                return View(model);
            }

            await houseService.EditAsync(id, model);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await houseService.ExistAsync(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithId(id, User.GetId()) == false)
            {
                return Unauthorized();
            }
            var house = await houseService.HouseDetailsByIdAsync(id);

            var model = new HouseDetailsViewModel()
            {
                Id = house.Id,
                Title = house.Title,
                Description = house.Description,
                Address = house.Address,
                ImageUrl = house.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel model)
        {
            if (await houseService.ExistAsync(model.Id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithId(model.Id, User.GetId()) == false)
            {
                return Unauthorized();
            }

            await houseService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (await houseService.ExistAsync(id) == false)
            {
                return BadRequest();
            }

            if (await agentService.ExistByIdAsync(User.GetId()))
            {
                return Unauthorized();
            }

            if (await houseService.IsRentedAsync(id))
            {
                return BadRequest();
            }

            houseService.RentAsync(id, User.GetId());

            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (await houseService.ExistAsync(id) == false ||
                await houseService.IsRentedAsync(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.IsRentedByUserWithIdAsync(id, User.GetId()) == false)
            {
                return Unauthorized();
            }

            await houseService.LeaveAsync(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
