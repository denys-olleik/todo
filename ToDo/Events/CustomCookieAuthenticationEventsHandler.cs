using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ToDo.Models.User;

namespace ToDo.Events
{
  public class CustomCookieAuthenticationEventsHandler : CookieAuthenticationEvents
  {
    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
      var principal = context.Principal;

      int userId = Convert.ToInt32(principal!.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);

      string password = principal.Claims.Single(x => x.Type == CustomClaimTypeConstants.Password).Value;

      UserService userService = new UserService();

      User user;

      user = await userService.GetAsync(userId);

      if (user == null || user.Password != password)
      {
        context.RejectPrincipal();

        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await base.ValidatePrincipal(context);
      }
      else
      {
        context.Principal = principal;
        await base.ValidatePrincipal(context);
      }
    }
  }
}