using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Components
{
    public class MainMenuComponent : ViewComponent
    {
        /// <summary>
        /// Component for Main Menu
        /// </summary>
        /// <returns>
        /// View(Default.cshtml)
        /// View(otherName.cshtml)
        /// </returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View());
        }
    }
}
