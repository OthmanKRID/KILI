using Kili.Models;
using Kili.Models.General;
using Kili.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Kili.Controllers
{
    public class AssociationController : Controller
    {
        private Association_Services Association_Services;
        private UserAccount_Services UserAccount_Services;
        private Adresse_Services Adresse_Services;
        private Abonnement_Services Abonnement_Services;

        public AssociationController()
        {
            Association_Services = new Association_Services();
            UserAccount_Services = new UserAccount_Services();
            Adresse_Services = new Adresse_Services();
            Abonnement_Services = new Abonnement_Services();
        }



        //Fonction GET permettant d'afficher le formulaire de création du compte associé à l'association
        public IActionResult AjouterCompteAssociation()
        {
            ViewModels.UserAccountViewModel viewModelUser = new ViewModels.UserAccountViewModel() { Authentifie = HttpContext.User.Identity.IsAuthenticated }; ;
            return View("CreerUserAccount", viewModelUser);
        }

        //Fonction POST permettant de récupérer les données du formulaire et de créer le compte associé à l'association
        [HttpPost]
        public IActionResult AjouterCompteAssociation(UserAccountViewModel viewModelUser, string returnUrl)
        {
            Association association = new Association();
            association.UserAccountId = UserAccount_Services.CreerUserAccount(viewModelUser.UserAccount.UserName, viewModelUser.UserAccount.Password, viewModelUser.UserAccount.Mail, TypeRole.Association);
            return View("AjouterAssociation", association); 
        }

        //Fonction GET permettant d'afficher le formulaire de création de l'association
        public IActionResult AjouterAssociation(Association association)
        {            
            return View(association);
        }

        //Fonction POST permettant de récupérer les données du formulaire et de créer l'association
        [HttpPost]
        public IActionResult AjouterAssociation(Association association, string returnUrl)
        {
         
            Association_Services.CreerAssociation(association.Nom, association.Adresse, association.Theme, association.UserAccountId);

            ViewModels.UserAccountViewModel viewModelUser = new ViewModels.UserAccountViewModel() { Authentifie = HttpContext.User.Identity.IsAuthenticated/*, Urlretour = "../Association/VoirAssociation" */};
            return RedirectToAction("Authentification","Login");
        }

        //Fonction GET permettant d'afficher les informations de l'association du compte connecté
        public IActionResult VoirInfosAssociation()
        {
            Association association = Association_Services.ObtenirAssociationDuCompteConnecte(HttpContext.User.Identity.Name);
            association.Adresse = Adresse_Services.ObtenirAdresses().Where(r => r.Id == association.AdresseId).FirstOrDefault();
           
            return View(association);

        }

        //Fonction GET permettant de modifier les informations de l'association du compte connecté
        public IActionResult ModifierAssociation()
        {
            Association association = Association_Services.ObtenirAssociationDuCompteConnecte(HttpContext.User.Identity.Name);
            association.Adresse = Adresse_Services.ObtenirAdresses().Where(r => r.Id == association.AdresseId).FirstOrDefault();
            
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View(association);               
            }
            return RedirectToAction("Authentification", "Login");          
        }

        //Fonction POST permettant de récupérer les modifications sur les informations de l'association du compte connecté
        [HttpPost]
        public IActionResult ModifierAssociation(Association association)
        {
           Association_Services.ModifierAssociation(association.Id, association.Nom, association.Adresse, association.Theme);
           return RedirectToAction("Authentification", "Login");
        }


        //////////////////////////////////////////////////////////////////////////////////////////
        /// Gérér les services d'une association
        //////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult VoirServices()
        {
            Abonnement abonnement = Abonnement_Services.ObtenirAbonnementDuCompteConnecte(HttpContext.User.Identity.Name);
            ServicesViewModel viewModel = new ServicesViewModel() { abonnement = abonnement };        
            return View(viewModel);
        }

        public IActionResult GererServices()
        {
            Abonnement abonnement = Abonnement_Services.ObtenirAbonnementDuCompteConnecte(HttpContext.User.Identity.Name);
            ServicesViewModel viewModel = new ServicesViewModel() { abonnement = abonnement};
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult GererServices(ServicesViewModel viewModel)
        {
            Abonnement abonnement = Abonnement_Services.ObtenirAbonnementDuCompteConnecte(HttpContext.User.Identity.Name);
            Abonnement_Services.AjouterServiceAdhesion(abonnement.Id, viewModel.ServiceAdhesion.duree);
            viewModel.abonnement = abonnement;
            viewModel.ServiceAdhesion = abonnement.serviceAdhesion;
            return View("VoirServices", viewModel);
        }

    }
}
