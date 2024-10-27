using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Constants.DataConstants;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
    /// <summary>
    /// House Category Class
    /// </summary>
    [Comment("House Category")]
    public class Category
    {
        /// <summary>
        /// Category Identifier
        /// </summary>
        [Key]
        [Comment("Category Identifier")]
        public int Id { get; init; }

        /// <summary>
        /// Category Name
        /// </summary>
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Category Name")]
        public required string Name { get; set; }

        /// <summary>
        /// Navigational Property of Houses
        /// </summary>
        [Comment("Navigation Property Houses")]
        public List<House> Houses { get; init; } = new List<House>();
    }
}
