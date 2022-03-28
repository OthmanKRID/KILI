using Microsoft.AspNetCore.Mvc;
using Kili.Models.Vente;
using Kili.Models.Services;
using System.Linq;
using System.Collections.Generic;
using Kili.Helpers;
using Kili.Models;
using System.Threading.Tasks;
using System.IO;

namespace Kili.Controllers
{
    public class VenteController : Controller
    {
        private CatalogueServices _CatalogueServices;
        private ProduitServices _ProduitServices;
        private PanierServices _PanierServices;
        private BddContext _bddContext;
        public VenteController()
        {
            _CatalogueServices = new CatalogueServices();
            _ProduitServices = new ProduitServices();
            _PanierServices = new PanierServices();
            _bddContext = new BddContext();
        }

        //FONCTIONS POUR LES CATALOGUES

        //GET Afficher la liste des catalogues
        public IActionResult IndexCatalogue()
        {
            CatalogueServices catservice = new CatalogueServices();
            List<Catalogue> catalogues = catservice.ObtenirAllCatalogues();
            return View(catalogues);
        }

        //GET Formulaire création de catalogue
        public IActionResult CreerCatalogue()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreerCatalogue(Catalogue catalogue)
        {
            if (!ModelState.IsValid)
                return View();
            CatalogueServices catalogueService = new CatalogueServices();
            {
                catalogueService.CreerCatalogue(catalogue.CatalogueName, catalogue.Description);
                return RedirectToAction("IndexCatalogue");
            }
        }

        //GET Vue catalogue à modifier

        public IActionResult ModifierCatalogue(int? id)
        {
           // int testint = (int) id;
            if (id.HasValue)
            {
                CatalogueServices catservice = new CatalogueServices();
                Catalogue cata = catservice.ObtenirAllCatalogues().FirstOrDefault(c=> c.CatalogueID == id.Value);
                if(cata == null)
                    return NotFound();
                return View(cata);
            }
            else
                return NotFound();
        }

        [HttpPost]

        public IActionResult ModifierCatalogue (Catalogue catalogue)
        {
            if (!ModelState.IsValid)
                return View(catalogue);
            CatalogueServices catservice = new CatalogueServices();
            catservice.ModifierCatalogue(catalogue.CatalogueID, catalogue.CatalogueName, catalogue.Description);
            return RedirectToAction("IndexCatalogue");

        }

        public IActionResult SupprimerCatalogue(int id)
        {
            
            CatalogueServices catservice = new CatalogueServices();
            catservice.SupprimerCatalogue(id);
            return RedirectToAction("IndexCatalogue");
        }



        //FONCTIONS POUR LES PRODUITS

        //Fonctions GET
        public IActionResult IndexProduit()
        {
            ProduitServices prodservice = new ProduitServices();
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
            ProduitServices produit_Services = new ProduitServices();
            {
                produit_Services.CreerProduit(produit.Designation, produit.Format, produit.Description, produit.PrixUnitaire, produit.Devise, produit.CatalogueID);
                return RedirectToAction("IndexProduit");
            }
        }

        //Fonctions GET POUR PANIER

        //Afficher la liste des produits disponibles pour achat

        public IActionResult IndexBoutique()
        {
            var bdd = new BddContext();
            return View(bdd.Produits.ToList());
        }
        //Afficher panier
        public IActionResult IndexPanier()
        {
            var panierID = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "panierID");
            Panier panier;
            if (panierID != 0)
            {
                panier = new PanierServices().ObtenirPanier(panierID);
            }
            else
            {
                panier = new Panier() { Articles = new List<Article>() };
            }
            return View(panier);

        }

        public IActionResult Acheter(int id)
        {
            var panierID = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "panierID");
            PanierServices panierServices = new PanierServices();
            if (panierID == 0)
            {
                //Si le panier n'existe pas, créer le panier avant d'ajouter un article de produit à l'intérieur
                panierID = panierServices.CreerPanier();
                panierServices.AjouterArticle(panierID, new Article { ProduitID = id, Quantite = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "panierID", panierID);
            }
            else
            {
                //Si le panier existe, rajouter un article à l'intérieur
                Panier panier = panierServices.ObtenirPanier(panierID);
                int res = ProduitExistantDansPanier(panier, id);
                if (res != -1)
                {
                    panierServices.MettreAjourQuantite(res);
                }
                else
                {
                    panierServices.AjouterArticle(panierID, new Article { ProduitID = id, Quantite = 1 });
                }
            }
            return RedirectToAction("IndexPanier");
        }



        [HttpPost]

        //SupprimerArticle
        public IActionResult SupprimerArticle(int id)
        {
            var panierID = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "panierID");
            new PanierServices().SupprimerArticle(panierID, id);
            return RedirectToAction("IndexPanier");
        }

        //Renvoyer ArticleID si le produit est dans l'article, sinon renvoyer -1

        private int ProduitExistantDansPanier(Panier panier, int produitID)
        {
            foreach (var article in panier.Articles)
            {
                if (article.ProduitID == produitID)
                {
                    return article.ArticleId;
                }
            }
            return -1;
        }
    }
}
