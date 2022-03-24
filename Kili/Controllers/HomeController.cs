using Kili.Models;
using Kili.Models.General;
using Kili.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Kili.Controllers
{
    public class HomeController : Controller
    {
        private Association_Services Association_Services;
        private UserAccount_Services UserAccount_Services;
        private Adresse_Services Adresse_Services;
        private Abonnement_Services Abonnement_Services;

        public HomeController()
        {            
            Association_Services = new Association_Services();
            UserAccount_Services = new UserAccount_Services();
            Adresse_Services = new Adresse_Services();
            Abonnement_Services = new Abonnement_Services();
        }

        public IActionResult Index()
        {
            var Role = User.FindFirst(ClaimTypes.Role);

            bool isAdmin = User.IsInRole(TypeRole.Admin.ToString());
            return View();
        }


        //Fonction permettant d'afficher les association en fonction de la localisation et du theme
        public IActionResult VoirAssociations(string ville, RechercheTheme Theme)
        {
            List<Association> listeAssociations = new List<Association>();
            if (ville == null & Theme == RechercheTheme.Tous)
            { 
                listeAssociations = Association_Services.ObtenirAssociations();
            }
            else if (ville != null & Theme == RechercheTheme.Tous)
            {
                listeAssociations = Association_Services.ObtenirAssociationsParLocalisation(ville);
            }
            else if (ville == null & Theme != RechercheTheme.Tous)
            {
                listeAssociations = Association_Services.ObtenirAssociationsParTheme(Theme);
            }
            else
            {
                listeAssociations = Association_Services.ObtenirAssociationsParThemeEtVille(ville, Theme);
            }

            foreach (Association association in listeAssociations)
            {
                association.Adresse = Adresse_Services.ObtenirAdresse((int)association.AdresseId);
            }
            return View(listeAssociations);
        }

    }
}
