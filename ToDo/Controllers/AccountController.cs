using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models.Account;

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

    }
  }
}