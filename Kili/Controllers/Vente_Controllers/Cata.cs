using Kili.Models.Vente;
using Kili.Models.Vente.Vente_Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Kili.ViewModels.VenteViewModels;
using Kili.Models;
using Kili.Models.General;


namespace Kili.Controllers.Vente_Controllers
{
    public class Cata : Controller
    {
        private Catalogue_Services _Catalogue_Services;
        private UserAccount_Services UserAccount_Services;

        public Cata()
        {
            _Catalogue_Services = new Catalogue_Services();
            UserAccount_Services = new UserAccount_Services();
        }

        //Fonctions GET

        //Afficher la liste de tous les catalogues
        public IActionResult Index()
        {
            UserAccount compteConnecte = new UserAccount();
            compteConnecte = UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name);

            CataViewModel cataViewModel = new CataViewModel() { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            Catalogue_Services catservice = new Catalogue_Services();
            List<Catalogue> catalogues = catservice.ObtenirAllCatalogues();

            return View(cataViewModel);
            /*Catalogue_Services catservice = new Catalogue_Services();
            List<Catalogue> catalogues = catservice.ObtenirAllCatalogues();
            return View(catalogues);*/
        }
        //Afficher le formulaire de création de catalogues
        public IActionResult CreerCatalogue()
        {
            return View();
        }
    }
}
