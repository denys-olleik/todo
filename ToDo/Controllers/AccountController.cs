using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Models.Account;
using ToDo.Models.User;
using ToDo.Validators;

namespace ToDo.Controllers
{
  [Authorize]
  [Route("a")]
  public class AccountController : Controller
  {
    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Login()
    {
      return View();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
      LoginViewModelValidator loginViewModelValidator = new LoginViewModelValidator();
      ValidationResult validationResult = await loginViewModelValidator.ValidateAsync(model);

      if (!validationResult.IsValid)
      {
        model.ValidationResult = validationResult;
        return View(model);
      }

      UserService userService = new UserService();
      User user = await userService.GetAsync(model.Email);  

      if (user != null && !string.IsNullOrEmpty(user.Password) && PasswordStorage.VerifyPassword(model.Password, user.Password))
      {
        ClaimsPrincipal claimsPrincipal = CreateClaimsPricipal(user);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            new AuthenticationProperties()
            {
              IsPersistent = true
            });

        return RedirectToAction("ChooseOrganization", "Account");
      }
      else
      {
        model.ValidationResult = new ValidationResult(new List<ValidationFailure>()
                {
                    new ValidationFailure("Email", "'Email' or 'password' is incorrect.")
                });
        return View(model);
      }
    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync();

      return RedirectToAction("Index", "Home");
    }

    private ClaimsPrincipal CreateClaimsPricipal(User user, int? organizatonId = null, string? organizationName = null)
    {
      List<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>()
            {
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new System.Security.Claims.Claim(ClaimTypes.Name, $"{user.FirstName}".Trim()),
                new System.Security.Claims.Claim(CustomClaimTypeConstants.Password, user.Password)
            };

      if (organizatonId.HasValue)
      {
        claims.Add(new System.Security.Claims.Claim(CustomClaimTypeConstants.OrganizationId, organizatonId.Value.ToString()));
      }

      if (!string.IsNullOrEmpty(organizationName))
      {
        claims.Add(new System.Security.Claims.Claim(CustomClaimTypeConstants.OrganizationName, organizationName));
      }

      ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
      return new ClaimsPrincipal(identity);
    }
  }
}