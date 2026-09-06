using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.SocialPlatformDTO
{
    public class SocialCreateDTO
    {
        [Required]
        public string SocialName { get; set; }
    }
}
