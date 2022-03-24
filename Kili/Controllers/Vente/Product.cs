using Kili.Models.Vente;
using Kili.Models.Services.Vente_Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Kili.Controllers.Vente
{
    public class Product : Controller
    {
        private Produit_Services _Produit_Services;
        public Product()
        {
            _Produit_Services = new Produit_Services();
        }

        //Fonctions GET
        public IActionResult Index()
        {
            Produit_Services prodservice = new Produit_Services();
            List<Produit> produits = prodservice.ObtenirAllProduits();
            return View(produits);
        }
        public IActionResult CreerProduit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreerProduit(Produit produit)
        {
            if (!ModelState.IsValid)
                return View();
            Produit_Services produit_Services = new Produit_Services();
            {
                produit_Services.CreerProduit(produit.Designation, produit.Format, produit.Description, produit.PrixUnitaire, produit.Devise, produit.CatalogueID);
                return RedirectToAction("Index");
            }
        }
    }
}
