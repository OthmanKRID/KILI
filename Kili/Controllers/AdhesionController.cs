using Kili.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kili.Controllers
{
    public class AdhesionController : Controller
    {
        private Adhesion_Services Adhesion_Services;

        public AdhesionController()
        {
            Adhesion_Services = new Adhesion_Services();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AjouterAdherent()
        {
            return View();
        }

    }
}
