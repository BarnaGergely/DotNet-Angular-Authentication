using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthApp.Server.Data.Models;

public class AppUser: IdentityUser
{
    [Required]
    [PersonalData]
    public string FullName { get; set; }
}
