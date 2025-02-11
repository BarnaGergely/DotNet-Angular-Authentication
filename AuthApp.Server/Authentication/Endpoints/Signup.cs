using AuthApp.Server.Common.Api;
using AuthApp.Server.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Server.Authentication.Endpoints;

public class Signup : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("signup", async (UserManager<AppUser> userManager, [FromBody] UserRegistrationRequest userRegistrationRequest) =>
        {
            AppUser user = new AppUser
            {
                Email = userRegistrationRequest.Email,
                UserName = userRegistrationRequest.Email,
                FullName = userRegistrationRequest.FullName
            };
            var result = await userManager.CreateAsync(user, userRegistrationRequest.Password);

            if (result.Succeeded)
            {
                return Results.Ok(result);
            }
            else
            {
                return Results.BadRequest(result.Errors);
            }
        });
    }
}

public class UserRegistrationRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }

}
