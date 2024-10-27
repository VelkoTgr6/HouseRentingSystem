using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Infrastructure.Constants.DataConstants;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
    /// <summary>
    /// House to rent Class
    /// </summary>
    [Comment("House to rent")]
    public class House
    {
        /// <summary>
        /// House Identifier
        /// </summary>
        [Key]
        [Comment("House Identifier")]
        public int Id { get; set; }

        /// <summary>
        /// House Title
        /// </summary>
        [Required]
        [MaxLength(HouseTitleMaxLength)]
        [Comment("House Title")]
        public required string Title {  get; set; }

        /// <summary>
        /// House Address
        /// </summary>
        [Required]
        [MaxLength(HouseAddressMaxLength)]
        [Comment("House Address")]
        public required string Address {  get; set; }

        /// <summary>
        /// House Description
        /// </summary>
        [Required]
        [MaxLength(HouseDescriptionMaxLength)]
        [Comment("House Description")]
        public required string Description { get; set; }

        /// <summary>
        /// House Image Url
        /// </summary>
        [Required]
        [Comment("House ImageUrl")]
        public required string ImageUrl {  get; set; }

        /// <summary>
        /// House Price Per Month
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("House Price Per Month")]
        //[Range(typeof(decimal),HousePricePerMonthMinValue, HousePricePerMonthMaxValue,ConvertValueInInvariantCulture = true)]
        public decimal PricePerMonth { get; set; }

        /// <summary>
        /// Category Identifier
        /// </summary>
        [Required]
        [Comment("Category Identifier")]
        public required int CategoryId {  get; set; }

        /// <summary>
        /// Agent Identifier
        /// </summary>
        [Required]
        [Comment("Agent Identifier")]
        public required int AgentId { get; set; }

        /// <summary>
        /// Renter Identifier
        /// </summary>
        [Comment("Renter Identifier")]
        public string? RenterId { get; set; }

        /// <summary>
        /// Navigation Property for Category with Category Id
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        /// <summary>
        /// Navigation Property for Agent with Agent Id
        /// </summary>
        public Agent Agent { get; set; } = null!;
    }
}
