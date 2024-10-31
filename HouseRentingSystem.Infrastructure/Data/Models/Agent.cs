using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Infrastructure.Constants.DataConstants;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
    /// <summary>
    /// House Agent Class
    /// </summary>
    [Index(nameof(PhoneNumber), IsUnique = true)]
    [Comment("House Agent")]
    public class Agent
    {
        /// <summary>
        /// Agent Identifier
        /// </summary>
        [Key]
        [Comment("Agent Identifier")]
        public int Id { get; set; }

        /// <summary>
        /// Agent's Phone Number
        /// </summary>
        [Required]
        [MaxLength(AgentPhoneNumberMaxLength)]
        [Comment("Agent's Phone Number")]
        public required string PhoneNumber {  get; set; }

        /// <summary>
        /// User's Identifier
        /// </summary>
        [Required]
        [Comment("User Identifier")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        /// <summary>
        /// Navigation property to Agent's Houses
        /// </summary>
        [Comment("Navigation property to Agent's Houses")]
        public List<House> Houses { get; set; } = new List<House>();
    }
}
