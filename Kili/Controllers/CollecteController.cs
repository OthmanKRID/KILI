using Kili.Models;
using Kili.Models.Dons;
using Kili.Models.General;
using Kili.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kili.Controllers
{
    public class CollecteController : Controller
    {
        public IActionResult CreerCollecte()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreerCollecte(Collecte collecte)
        {
            //if (!ModelState.IsValid) { 
            UserAccount_Services userAccount_Services = new UserAccount_Services();

            DonServices donServices = new DonServices();
            {
                int id = donServices.CreerCollecte(collecte.Nom, collecte.MontantCollecte, collecte.Descriptif); // , collecte.Date

                //Modifier la fonction pour faire le lien avec ServiceDon à partir de UserAccount
                //userAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).Association.Paiement.ServiceDon.Id;



                return Redirect("/home/index"); 
            }
            // } return View();
        }

        public IActionResult AfficherCollecte(int id)
        {
            if (id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    Collecte collecte = donServices.ObtenirCollecte(id);
                    if (collecte == null)
                    {
                        return View("Error");
                    }
                    return View(collecte);
                }

            }
            return View("Error");
        }

        public IActionResult AfficherCollectes()
        {
            DonServices donServices = new DonServices();
            {
                List<Collecte> listcollecte = donServices.ObtenirCollectes();

                return View(listcollecte);
            }
        }

        public IActionResult ModifierCollecte(int id)
        {
            if (id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    Collecte collecte = donServices.ObtenirCollecte(id);

                    if (collecte == null)
                    {
                        return View("Error");
                    }
                    return View(collecte);
                }

            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifierCollecte(Collecte collecte)
        {

            if (collecte.Id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    donServices.ModifierCollecte(collecte.Id, collecte.Nom, collecte.MontantCollecte, collecte.Descriptif, collecte.Date);

                    return RedirectToAction("AfficherCollectes");
                }
            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult SupprimerCollecte(int id)
        {
            if (id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    Collecte collecte = donServices.ObtenirCollecte(id);
                    if (collecte == null)
                    {
                        return View("Error");
                    }
                    donServices.SupprimerCollecte(id);
                    return Redirect("AfficherCollectes");
                }

            }
            return View("Error");
        }




    }
}
