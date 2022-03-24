using Kili.Models.Vente;
using Kili.Models.Services.Vente_Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;


namespace Kili.Controllers.Vente
{
    public class Cata : Controller
    {
        private Catalogue_Services _Catalogue_Services;

        public Cata()
        {
            _Catalogue_Services = new Catalogue_Services();
        }

        //FONCTIONS GET

        //GET Afficher la liste des catalogues
        public IActionResult Index()
        {
            Catalogue_Services catservice = new Catalogue_Services();
            List<Catalogue> catalogues = catservice.ObtenirAllCatalogues();
            return View(catalogues);
        }

        //GET Formulaire création de catalogue
        public IActionResult CreerCatalogue()
        {
            return View();
        }
        //GET Vue catalogue à modifier
        public IActionResult ModifierCatalogue(int catalogueid)
        {
            if(catalogueid != 0)
            {
                {
                    Catalogue_Services catservice = new Catalogue_Services();
                    Catalogue catalogues = catservice.ObtenirAllCatalogues().Where(c=> c.CatalogueID == catalogueid).FirstOrDefault();
                    if (catalogues == null)
                    {
                        return View("CataNonTrouve");
                    }
                    return View(catalogues);
                }

            }
            return View("CataNonTrouve");



        }


        [HttpPost]
        public IActionResult CreerCatalogue(Catalogue catalogue)
        {
            if (!ModelState.IsValid)
                return View();
            Catalogue_Services catalogueService = new Catalogue_Services();
            {
                catalogueService.CreerCatalogue(catalogue.CatalogueName, catalogue.Description);
                return RedirectToAction("Index");
            }
        }
    }
    
}
