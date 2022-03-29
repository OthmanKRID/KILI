using Kili.Models;
using Kili.Models.Dons;
using Kili.Models.General;
using Kili.Models.Services;
using Kili.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kili.Controllers
{
    public class DonateurController : Controller
    {
        public IActionResult CreerDonateur()
        {

            return View();
        }


        [HttpPost]
        public IActionResult CreerDonateur(DonateurViewModel viewModel)
        {
            //if (!ModelState.IsValid) { 
            UserAccount_Services userAccount_Services = new UserAccount_Services();
            
            DonServices donServices = new DonServices();
            {
                int id = donServices.CreerDonateur(viewModel.Donateur.AdresseFacuration, viewModel.Donateur.Telephone);
                UserAccount ua =  userAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name);
                userAccount_Services.ModifierUserAccount(ua.Id, ua.Prenom, ua.Nom, ua.Mail, ua.Role, ua.AssociationId, id);

                return Redirect("/collecte/AfficherCollectes");
            }
            // } return View();
        }


        public IActionResult AfficherDonateur(int id)
        {
            if (id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    Donateur donateur = donServices.ObtenirDonateur(id);
                    if (donateur == null)
                    {
                        return View("Error");
                    }
                    return View(new DonateurViewModel() { Donateur = donateur });
                }

            }
            return View("Error");
        }

        public IActionResult AfficherDonateurs()
        {
            DonServices donServices = new DonServices();
            {
                List<Donateur> listdonateur = donServices.ObtenirDonateurs();

                return View(listdonateur);
            }
        }

        public IActionResult ModifierDonateur(int id)
        {
            if (id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    Donateur donateur = donServices.ObtenirDonateur(id);

                    if (donateur == null)
                    {
                        return View("Error");
                    }
                    return View(new DonateurViewModel() { Donateur = donateur });
                }

            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifierDonateur(DonateurViewModel viewModel)
        {

            if (viewModel.Donateur.Id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    donServices.ModifierDonateur(viewModel.Donateur.Id, viewModel.Donateur.AdresseFacuration, viewModel.Donateur.Telephone);

                    return RedirectToAction("ModifierDonateur", new { @id = viewModel.Donateur.Id });
                }
            }
            else
            {
                return View("Error");
            }
        }



        public IActionResult SupprimerDonateur(int id)
        {
            if (id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    Donateur donateur = donServices.ObtenirDonateur(id);
                    if (donateur == null)
                    {
                        return View("Error");
                    }
                    donServices.SupprimerDonateur(id);
                    return Redirect("/home/index");
                }

            }
            return View("Error");
        }
    }
}
