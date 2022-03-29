using Kili.Models;
using Kili.Models.General;
using Kili.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;
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

        public IActionResult Index(IndexViewModel viewModel)
        {
            viewModel.Authentifie = HttpContext.User.Identity.IsAuthenticated;

            viewModel.Associations = Association_Services.Obtenir3DernièresAssociations();

            var Role = User.FindFirst(ClaimTypes.Role);

            bool isAdmin = User.IsInRole(TypeRole.Admin.ToString());
            return View(viewModel);
        }

        public IActionResult Explorer()
        {
           return View(new AssociationViewModel());
        }


        public IActionResult VoirAssociations(AssociationViewModel viewModel)
        {
            viewModel.Associations = Association_Services.ObtenirAssociations(); ;
            return View(viewModel);
        }

        [HttpPost]
        //Fonction permettant d'afficher les association en fonction de la localisation et du theme
        public IActionResult VoirAssociationsTriees(AssociationViewModel viewModel)
        {
            List<Association> listeAssociations = new List<Association>();
            if (viewModel.association.Adresse.Ville == null)
            {
                if (viewModel.RechercheTheme == RechercheTheme.Tous)
                {
                    if (viewModel.association.Nom == null)
                    {
                        listeAssociations = Association_Services.ObtenirAssociations();
                    }
                    else
                    {
                        listeAssociations = Association_Services.ObtenirAssociationsParNom(viewModel.association.Nom);
                    }
                }
                else
                {
                    if (viewModel.association.Nom == null)
                    {
                        listeAssociations = Association_Services.ObtenirAssociationsParTheme(viewModel.RechercheTheme);
                    }
                    else
                    {
                        listeAssociations = Association_Services.ObtenirAssociationsParThemeEtNom(viewModel.association.Nom, viewModel.RechercheTheme);
                    }
                }
            }
            else
            {
                if (viewModel.RechercheTheme == RechercheTheme.Tous)
                {
                    if (viewModel.association.Nom == null)
                    {
                        listeAssociations = Association_Services.ObtenirAssociationsParLocalisation(viewModel.association.Adresse.Ville);
                    }
                    else
                    {
                        listeAssociations = Association_Services.ObtenirAssociationsParVilleEtNom(viewModel.association.Adresse.Ville, viewModel.association.Nom);
                    }
                }
                else
                {
                    if (viewModel.association.Nom == null)
                    {
                        listeAssociations = Association_Services.ObtenirAssociationsParThemeEtVille(viewModel.association.Adresse.Ville, viewModel.RechercheTheme);
                    }
                    else
                    {
                        listeAssociations = Association_Services.ObtenirAssociationsParThemeEtVilleEtNom(viewModel.association.Adresse.Ville, viewModel.association.Nom, viewModel.RechercheTheme);
                    }
                }
            }

            viewModel.Associations = listeAssociations;
            return View("VoirAssociations", viewModel);
        }

        public FileResult GeneratePdf(string html)
        {
            html = html.Replace("strtTag", "<").Replace("EndTag", ">");
            HtmlToPdf objhtml = new HtmlToPdf();
            PdfDocument objdoc = objhtml.ConvertHtmlString(html);
            byte[] pdf = objdoc.Save();
            objdoc.Close();
            return File(pdf, "application/pdf", "HtmlContent.pdf");
        }

    }
}
