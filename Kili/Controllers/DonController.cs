using Kili.Models;
using Kili.Models.Dons;
using Kili.Models.Services;
using Kili.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Kili.Models.General;
using static Kili.ViewModels.PaiementViewModel;

namespace Kili.Controllers
{
    //[Authorize]
    public class DonController : Controller 
    {

        // Fonction permettant de creer un don à partir dun id de collecte.

        public IActionResult CreerDon(int idCollecte)
        {
            UserAccount_Services UserAccount_Services = new UserAccount_Services();

            UserAccountViewModel viewModel = new UserAccountViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            DonViewModel donviewModel = new DonViewModel { IdCollecte = idCollecte, Authentifie = HttpContext.User.Identity.IsAuthenticated };

            // valide si l'utilisateur est authentifié
            if (viewModel.Authentifie)
            {
                //Regarde si l'utilisateur n'a pas d'adresse enregistrée et alors le redirige vers la création d'une adresse
                if (UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).AdresseId == null)
                {
                    return Redirect("/adresse/creeradresse");
                }

                DonServices donservices = new DonServices();

                //Regarde si l'utilisateur n'est pas enregistré comme donateur et alors crée un donateur en y associant l'Id de l'adresse
                if (UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).DonateurId == null) {

                    UserAccount ua = UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name);

                    int id = donservices.CreerDonateur(ua.AdresseId);

                    UserAccount_Services.ModifierUserAccount(ua.Id, ua.Prenom, ua.Nom, ua.Mail, ua.Telephone, ua.Role, ua.AssociationId, id, ua.AdresseId, ua.ImagePath );

                }

                //Si l'utilisateur est déjà donateur, alors on modifie celui-ci en y associant l'adresse
                else
                {
                    donservices.ModifierDonateur((int)UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).DonateurId, UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).AdresseId);
                
                }

                return View(donviewModel);
            }
            return Redirect("/login/authentification");
        }


        [HttpPost]
        public IActionResult CreerDon(DonViewModel viewModel)
        {
            //if (!ModelState.IsValid) { 

            UserAccount_Services UserAccount_Services = new UserAccount_Services();
            DonServices donServices = new DonServices();
            {
                int id = donServices.CreerDon(viewModel.Don.Montant, viewModel.Don.Recurrence, UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).DonateurId, viewModel.IdCollecte);               
                
                return RedirectToAction("creerpaiement", "paiement", new { actionID = id, montant = viewModel.Don.Montant, typeaction = TypeAction.Don });
            }
            // } return View();


        }

        // Affiche un don à partir d'un id
        public IActionResult AfficherDon(int id)
        {
            if (id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    Don don = donServices.ObtenirDon(id);
                    if (don == null)
                    {
                        return View("Error");
                    }
                    return View(new DonViewModel() { Don = don });
                }

            }
            return View("Error");
        }


        // Affiche l'ensemble des dons de la bdd. 
        public IActionResult AfficherDons()
        {
            DonServices donServices = new DonServices();
                {
                    List<Don> listdon = donServices.ObtenirDons();
                    
                return View(listdon);
                }
        }

        // Modifie un don à partir de son Id. 
        public IActionResult ModifierDon(int id)
        {
            if (id != 0)
            {
                    DonServices donServices = new DonServices();
                    {
                    Don don = donServices.ObtenirDons().Where(r => r.Id == id).FirstOrDefault();

                    if (don == null)
                    {
                        return View("Error");
                    }
                    return View(new DonViewModel() { Don = don });
                }

            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifierDon(DonViewModel viewModel)
        {

            if (viewModel.Don.Id != 0)
            {
                    DonServices donServices = new DonServices();
                    {
                    donServices.ModifierDon(viewModel.Don.Id, viewModel.Don.Montant, viewModel.Don.Recurrence);

                    return RedirectToAction("ModifierDon", new { @id = viewModel.Don.Id });
                }
            }
            else
            {
                return View("Error");
            }
        }

           
        // Supprimer un don à partir de son Id
            public IActionResult SupprimerDon(int id)
        {
            if (id != 0)
            {
                    DonServices donServices = new DonServices();
                    {
                    Don don = donServices.ObtenirDons().Where(r => r.Id == id).FirstOrDefault();
                    if (don == null)
                    {
                        return View("Error");
                    }
                    donServices.SupprimerDon(id);
                    return Redirect("/home/index");
                }

            }
            return View("Error");
        }





}
}
