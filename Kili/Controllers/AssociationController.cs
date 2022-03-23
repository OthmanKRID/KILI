using Kili.Models;
using Kili.Models.General;
using Kili.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Kili.Controllers
{
    public class AssociationController : Controller
    {
        private Association_Services Association_Services;
        private UserAccount_Services UserAccount_Services;
        private Adresse_Services Adresse_Services;

        public AssociationController()
        {
            Association_Services = new Association_Services();
            UserAccount_Services = new UserAccount_Services();
            Adresse_Services = new Adresse_Services();
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

                ViewModels.CreerAssociationViewModel viewModelAsso = new CreerAssociationViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated, association = new Association() };
                viewModelAsso.association.UserAccountId = UserAccount_Services.CreerUserAccount(viewModelUser.UserAccount.UserName, viewModelUser.UserAccount.Password, viewModelUser.UserAccount.Mail, TypeRole.Association);
                return View("AjouterAssociation", viewModelAsso); 
        }

        //Fonction GET permettant d'afficher le formulaire de création de l'association
        public IActionResult AjouterAssociation(CreerAssociationViewModel viewModelAsso)
        {            
            return View(viewModelAsso);
        }

        //Fonction POST permettant de récupérer les données du formulaire et de créer l'association
        [HttpPost]
        public IActionResult AjouterAssociation(CreerAssociationViewModel viewModel, string returnUrl)
        {
            //if (ModelState.IsValid)
            //{                
               Association_Services.CreerAssociation(viewModel.association.Nom, viewModel.association.Adresse, viewModel.association.Theme, viewModel.association.UserAccountId);
            //}

            ViewModels.UserAccountViewModel viewModelUser = new ViewModels.UserAccountViewModel() { Authentifie = HttpContext.User.Identity.IsAuthenticated/*, Urlretour = "../Association/VoirAssociation" */};
            return RedirectToAction("Authentification","Login");
        }

        //Fonction GET permettant d'afficher les informations de l'association
        public IActionResult VoirAssociation()
        {
            UserAccount compteConnecte = new UserAccount();
            compteConnecte = UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name);

            CreerAssociationViewModel viewModelAsso = new CreerAssociationViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated, association = Association_Services.ObtenirAssociations().Where(r => r.UserAccountId == compteConnecte.Id).FirstOrDefault() };
            viewModelAsso.association.Adresse = Adresse_Services.ObtenirAdresses().Where(r => r.Id == viewModelAsso.association.AdresseId).FirstOrDefault();
          
            if (viewModelAsso.Authentifie)
            {
                return View(viewModelAsso);
            }
            return RedirectToAction("Authentification", "Login");
        }

       public IActionResult ModifierAssociation()
        {
            UserAccount compteConnecte = new UserAccount();
            compteConnecte = UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name);

            CreerAssociationViewModel viewModelAsso = new CreerAssociationViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated, association = Association_Services.ObtenirAssociations().Where(r => r.UserAccountId == compteConnecte.Id).FirstOrDefault() };
            viewModelAsso.association.Adresse = Adresse_Services.ObtenirAdresses().Where(r => r.Id == viewModelAsso.association.AdresseId).FirstOrDefault();
            
            if (viewModelAsso.Authentifie)
            {
                return View(viewModelAsso);               
            }
            return RedirectToAction("Authentification", "Login");          
        }

        [HttpPost]
        public IActionResult ModifierAssociation(CreerAssociationViewModel viewModelAsso)
        {
           Association_Services.ModifierAssociation(viewModelAsso.association.Id, viewModelAsso.association.Nom, viewModelAsso.association.Adresse, viewModelAsso.association.Theme);
           //Adresse_Services.ModifierAdresse(viewModelAsso.association.AdresseId, viewModelAsso.association.Adresse);
            return RedirectToAction("Authentification", "Login");
        }

    }
}
