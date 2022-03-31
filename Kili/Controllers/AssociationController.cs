using Kili.Models;
using Kili.Models.General;
using Kili.Models.Services;
using Kili.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Kili.Controllers
{
    public class AssociationController : Controller
    {
        private Association_Services Association_Services;
        private UserAccount_Services UserAccount_Services;
        private Adresse_Services Adresse_Services;
        private Abonnement_Services Abonnement_Services;
        private IWebHostEnvironment _webEnv;

        public AssociationController(IWebHostEnvironment environment)
        {
            Association_Services = new Association_Services();
            UserAccount_Services = new UserAccount_Services();
            Adresse_Services = new Adresse_Services();
            Abonnement_Services = new Abonnement_Services();
            _webEnv = environment;
        }



        //Fonction GET permettant d'afficher le formulaire de création du compte associé à l'association
        public IActionResult AjouterCompteAssociation()
        {
            ViewModels.UserAccountViewModel viewModelUser = new ViewModels.UserAccountViewModel() { Authentifie = HttpContext.User.Identity.IsAuthenticated, UserAccount = new UserAccount() };
            //return View("../UserAccount/CreerUserAccount", viewModelUser);
            return View("../UserAccount/CreerUtilisateur", viewModelUser);
        }

        //Fonction POST permettant de récupérer les données du formulaire et de créer le compte associé à l'association
        [HttpPost]
        public IActionResult AjouterCompteAssociation(UserAccountViewModel viewModelUser, string returnUrl)
        {
            Association association = new Association();
            UserAccount_Services.CreerUserAccountAssociation(viewModelUser.UserAccount.Prenom, viewModelUser.UserAccount.Nom, viewModelUser.UserAccount.Password, viewModelUser.UserAccount.Mail);
            UserAccount utilisateur = UserAccount_Services.Authentifier(viewModelUser.UserAccount.Mail, viewModelUser.UserAccount.Password);
            //On se connecte avec le compte créé
            if (utilisateur != null)
            {
                var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, utilisateur.Id.ToString()),
                        new Claim(ClaimTypes.Role, utilisateur.Role.ToString()),
                    };

                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                HttpContext.SignInAsync(userPrincipal);

                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return View("AjouterAssociation", association);
            }

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

            Association_Services.CreerAssociation(association.Nom, association.Adresse, association.Theme, UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name));

            ViewModels.UserAccountViewModel viewModelUser = new ViewModels.UserAccountViewModel() { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            //return View("ConfirmationCreationAssociation", association);
            return RedirectToAction("VoirProfilAssociation", "Association");
        }


        public IActionResult VoirDetailsAssociation(int id)
        {
            Association asso = Association_Services.ObtenirAssociation(id);
            return View(asso);
        }


        //Fonction GET permettant d'afficher les informations de l'association du compte connecté
        public IActionResult VoirProfilAssociation()
        {
            UserAccount CompteConnecte = UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name);
            AssociationViewModel viewModel = new AssociationViewModel() { compteConnecte = UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name), association = CompteConnecte.Association };      
            return View(viewModel);

        }

        //Fonction GET permettant de modifier les informations de l'association du compte connecté
        public IActionResult ModifierAssociation()
        {
            UserAccount CompteConnecte = UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name);
                                
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("voirProfilAssociation");
               // return View(CompteConnecte.Association);
            }
            //return View(CompteConnecte.Association);
            return RedirectToAction("voirProfilAssociation");
        }

        //Fonction POST permettant de récupérer les modifications sur les informations de l'association du compte connecté
        [HttpPost]
        public IActionResult ModifierAssociation(AssociationViewModel viewModel)
        {
            if (viewModel.association.Image != null)
            {
                if (viewModel.association.Image.Length > 0)
                {
                    string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                    uploads = Path.Combine(uploads, "Associations");
                    string filePath = Path.Combine(uploads, viewModel.association.Image.FileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        viewModel.association.Image.CopyTo(stream);

                    }
                }
                viewModel.association.ImagePath = "/images/Associations/" + viewModel.association.Image.FileName;
            }
            Association_Services.ModifierAssociation(viewModel.association);
            return RedirectToAction("voirProfilAssociation");
            //return View("VoirProfilAssociation", UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name).Association);
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
            viewModel.ServiceDon = CompteConnecte.Association.Abonnement.serviceDon;
            return View(viewModel);
        }

        public IActionResult GererServices()
        {
            UserAccount CompteConnecte = UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name);
            //CompteConnecte.Association.Abonnement.serviceAdhesion.IsActive = true;

            ServicesViewModel viewModel = new ServicesViewModel() {abonnement= CompteConnecte.Association.Abonnement, listesServicesProposes = Abonnement_Services.ObtenirToutLesServicesDansOffre()};
            viewModel.abonnement = CompteConnecte.Association.Abonnement;
            viewModel.ServiceAdhesion = CompteConnecte.Association.Abonnement.serviceAdhesion;
            viewModel.ServiceDon = CompteConnecte.Association.Abonnement.serviceDon;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult GererServices(ServicesViewModel viewModel)
        {
            UserAccount CompteConnecte = UserAccount_Services.ObtenirUserAccountConnecte(HttpContext.User.Identity.Name);
            Abonnement_Services.AjouterService(CompteConnecte.Association.Abonnement.Id, viewModel.listesServicesProposes[1]);
            //Test
            //.Association.Abonnement.serviceAdhesion.IsActive = true;
            viewModel.abonnement = CompteConnecte.Association.Abonnement;
            viewModel.ServiceAdhesion = CompteConnecte.Association.Abonnement.serviceAdhesion;
            viewModel.ServiceDon = CompteConnecte.Association.Abonnement.serviceDon;
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
            viewModel.ServiceDon.EstActif = true;
            return View("GererServices", viewModel);
        }
        /*
        [HttpPost]
        public IActionResult Telecharger(IFormFile fichier, string chemin)
        {

            if (fichier.Length > 0)
            {
                string uploads = Path.Combine(_webEnv.WebRootPath, chemin);
                string filePath = Path.Combine(uploads, fichier.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    Image.CopyTo(stream);

                }
            }

            return Redirect("/"); ;
        }
        */
        /*public IActionResult Telecharger()
        {
            return View();
        }*/

    }
}
