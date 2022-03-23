using Kili.Models.General;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kili.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var Role = User.FindFirst(ClaimTypes.Role);

            bool isAdmin = User.IsInRole(TypeRole.Admin.ToString());
            return View();
        }
    }
}
