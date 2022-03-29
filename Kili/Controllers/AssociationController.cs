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
            ViewModels.UserAccountViewModel viewModelUser = new ViewModels.UserAccountViewModel() { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            return View("../UserAccount/CreerUserAccount", viewModelUser);
        }

        //Fonction POST permettant de récupérer les données du formulaire et de créer le compte associé à l'association
        [HttpPost]
        public IActionResult AjouterCompteAssociation(UserAccountViewModel viewModelUser, string returnUrl)
        {
            Association association = new Association();
            UserAccount_Services.CreerUserAccount(viewModelUser.UserAccount.Prenom, viewModelUser.UserAccount.Nom, viewModelUser.UserAccount.Password, viewModelUser.UserAccount.Mail, TypeRole.Association);
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
         
            Association_Services.CreerAssociation(association.Nom, association.Adresse, association.Theme, UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name)/*, association.UserAccountId*/);

            ViewModels.UserAccountViewModel viewModelUser = new ViewModels.UserAccountViewModel() { Authentifie = HttpContext.User.Identity.IsAuthenticated/*, Urlretour = "../Association/VoirAssociation" */};
            return RedirectToAction("Authentification","Login");
        }

        //Fonction GET permettant d'afficher les informations de l'association du compte connecté
        public IActionResult VoirInfosAssociation()
        {
            UserAccount CompteConnecte = UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name);
            return View(CompteConnecte.Association);

        }

        //Fonction GET permettant de modifier les informations de l'association du compte connecté
        public IActionResult ModifierAssociation()
        {
            UserAccount CompteConnecte = UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name);
                                
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View(CompteConnecte.Association);
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
            UserAccount CompteConnecte = UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name);
            ServicesViewModel viewModel = new ServicesViewModel() { abonnement = CompteConnecte.Association.Abonnement };
            viewModel.abonnement = CompteConnecte.Association.Abonnement;
            viewModel.ServiceAdhesion = CompteConnecte.Association.Abonnement.serviceAdhesion;
            viewModel.ServiceDon = CompteConnecte.Association.Abonnement.ServiceDon;
            return View(viewModel);
        }

        public IActionResult GererServices()
        {
            UserAccount CompteConnecte = UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name);
            //CompteConnecte.Association.Abonnement.serviceAdhesion.IsActive = true;

            ServicesViewModel viewModel = new ServicesViewModel() {abonnement= CompteConnecte.Association.Abonnement, listesServicesProposes = Abonnement_Services.ObtenirToutLesServicesDansOffre()};
            viewModel.abonnement = CompteConnecte.Association.Abonnement;
            viewModel.ServiceAdhesion = CompteConnecte.Association.Abonnement.serviceAdhesion;
            viewModel.ServiceDon = CompteConnecte.Association.Abonnement.ServiceDon;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult GererServices(ServicesViewModel viewModel)
        {
            UserAccount CompteConnecte = UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name);
            Abonnement_Services.AjouterServiceAdhesion(CompteConnecte.Association.Abonnement.Id, viewModel.ServiceAdhesion.duree);
            //Test
            //.Association.Abonnement.serviceAdhesion.IsActive = true;
            viewModel.abonnement = CompteConnecte.Association.Abonnement;
            viewModel.ServiceAdhesion = CompteConnecte.Association.Abonnement.serviceAdhesion;
            viewModel.ServiceDon = CompteConnecte.Association.Abonnement.ServiceDon;
            return View("VoirServices", viewModel);
        }

 /*       public IActionResult MiseAJourSouscriptionAjoutAdhesion(int idservice, int idabonnement)
        {
            Service service = Abonnement_Services.ObtenirServiceDansOffre(idservice);
            Abonnement abonnement = Abonnement_Services.ObtenirAbonnement(idabonnement);
            abonnement.serviceAdhesion.IsActive = true;
            return View("GererServices", viewModel);
        }*/

        public IActionResult MiseAJourSouscriptionAjoutDon(ServicesViewModel viewModel)
        {
            viewModel.ServiceDon.IsActive = true;
            return View("GererServices", viewModel);
        }

    }
}
