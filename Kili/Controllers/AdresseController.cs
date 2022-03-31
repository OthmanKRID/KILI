using Kili.Models;
using Kili.Models.General;
using Kili.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kili.Controllers
{
    public class AdresseController : Controller
    {

        //Création d'une adresse
        public IActionResult CreerAdresse()
        {

            return View();
        }


        [HttpPost]
        public IActionResult CreerAdresse(Adresse adresse)
        {
            //if (!ModelState.IsValid) { 
            UserAccount_Services userAccount_Services = new UserAccount_Services();

            Adresse_Services adresseServices = new Adresse_Services();
            {
                int id = adresseServices.CreerAdresse(adresse.Numero, adresse.Voie, adresse.Complement, adresse.CodePostal, adresse.Ville);
                UserAccount ua = userAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name);
                userAccount_Services.ModifierUserAccount(ua.Id, ua.Prenom, ua.Nom, ua.Mail, ua.Telephone, ua.Role, ua.AssociationId, ua.DonateurId, id, ua.ImagePath);

                return Redirect("/");
            }
            // } return View();
        }


        public IActionResult AfficherAdresse(int id)
        {
            if (id != 0)
            {
                Adresse_Services adresseServices = new Adresse_Services();
                {
                    Adresse adresse = adresseServices.ObtenirAdresse(id);
                    if (adresse == null)
                    {
                        return View("Error");
                    }
                    return View(adresse);
                }
            }
            return View("Error");
        }

        public IActionResult AfficherAdresses()
        {
            Adresse_Services adresseServices = new Adresse_Services();
            {
                List<Adresse> listadresse = adresseServices.ObtenirAdresses();

                return View(listadresse);
            }
        }

        // Modification d'une adresse depuis son id
        public IActionResult ModifierAdresse(int id)
        {
            if (id != 0)
            {
                Adresse_Services adresseServices = new Adresse_Services();
                {
                    Adresse adresse = adresseServices.ObtenirAdresse(id);

                    if (adresse == null)
                    {
                        return View("Error");
                    }
                    return View(adresse);
                }

            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifierAdresse(Adresse adresse)
        {

            if (adresse.Id != 0)
            {
                Adresse_Services adresseServices = new Adresse_Services();
                {
                    adresseServices.ModifierAdresse(adresse.Id, adresse);

                    return RedirectToAction("ModifierAdresse", new { @id = adresse.Id });
                }
            }
            else
            {
                return View("Error");
            }
        }


        // Suppression d'une adresse depuis son Id.
        public IActionResult SupprimerAdresse(int id)
        {
            if (id != 0)
            {
                Adresse_Services adresseServices = new Adresse_Services();
                {
                    Adresse adresse = adresseServices.ObtenirAdresse(id);
                    if (adresse == null)
                    {
                        return View("Error");
                    }                    
                    adresseServices.SupprimerAdresse(id);
                    return Redirect("/home/index");
                }

            }
            return View("Error");
        }
    }
}
