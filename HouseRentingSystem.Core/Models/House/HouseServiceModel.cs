using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Constants.DataConstants;
using static HouseRentingSystem.Core.Constants.ErrorConstants;

namespace HouseRentingSystem.Core.Models.House
{
    public class HouseServiceModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(HouseTitleMaxLength,
            MinimumLength = HouseTitleMinLength,
            ErrorMessage = InvalidLengthMessage)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(HouseDescriptionMaxLength,
            MinimumLength = HouseDescriptionMinLength,
            ErrorMessage = InvalidLengthMessage)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(HouseAddressMaxLength,
            MinimumLength = HouseAddressMinLength,
            ErrorMessage = InvalidLengthMessage)]
        public string Address { get; set; } = null!;

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Price Per Month")]
        [Range(typeof(decimal),
            HousePricePerMonthMinValue,
            HousePricePerMonthMaxValue,
            ConvertValueInInvariantCulture = true,
            ErrorMessage = "Price must be a positive number and less than {2} leva.")]
        public decimal PricePerMonth { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }
    }
}
