using Microsoft.AspNetCore.Mvc;

namespace ToDo.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}