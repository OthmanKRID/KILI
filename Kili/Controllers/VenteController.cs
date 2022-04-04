using Microsoft.AspNetCore.Mvc;
using Kili.Models.Vente;
using Kili.Models.Services;
using System.Linq;
using System.Collections.Generic;
using Kili.Helpers;
using Kili.Models;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kili.Models.General;
using Kili.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
namespace Kili.Controllers
{
    public class VenteController : Controller
    {
        private CatalogueServices _CatalogueServices;
        private ProduitServices _ProduitServices;
        private PanierServices _PanierServices;
        private CommandeServices _CommandeServices;
        private LivraisonServices _LivraisonServices;
        private CoordonneesAcheteur _CoordonneesAcheteur;
        private BddContext _bddContext;
        private IWebHostEnvironment _WebEnv
;


        public VenteController(IWebHostEnvironment webEv)
        {
            _CatalogueServices = new CatalogueServices();
            _ProduitServices = new ProduitServices();
            _PanierServices = new PanierServices();
            _CommandeServices = new CommandeServices();
            _LivraisonServices = new LivraisonServices();
            _CoordonneesAcheteur = new CoordonneesAcheteur();
            _bddContext = new BddContext();
            _WebEnv = webEv;
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
                Catalogue cata = catservice.ObtenirAllCatalogues().FirstOrDefault(c => c.CatalogueID == id.Value);
                if (cata == null)
                    return NotFound();
                return View(cata);
            }
            else
                return NotFound();
        }

        [HttpPost]

        public IActionResult ModifierCatalogue(Catalogue catalogue)
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

            CatalogueServices catservice = new CatalogueServices();
            List<Catalogue> catalogues = catservice.ObtenirAllCatalogues();
            ViewBag.Catalogue = new SelectList(catalogues, "CatalogueID", "CatalogueName"); ;
            return View();
        }

        [HttpPost]
        public IActionResult CreerProduit(Produit produit)
        {
            if (!ModelState.IsValid)
                return View();

            if (produit.Image.Length > 0)
            {
                string uploads = Path.Combine(_WebEnv.WebRootPath, "images");
                uploads = Path.Combine(uploads, "Produit");
                string filePath = Path.Combine(uploads, produit.Image.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    produit.Image.CopyTo(stream);

                }

                produit.ImagePath = "/images/Produit/" + produit.Image.FileName;


                ProduitServices produit_Services = new ProduitServices();

                produit_Services.CreerProduit(produit.DateCreation, produit.Designation, produit.Format, produit.Description, produit.ImagePath, produit.PrixUnitaire, produit.Devise, produit.CatalogueID);
                return RedirectToAction("IndexProduit");

            }
            return View(produit);
        }





        //GET Vue produit à modifier
        public IActionResult ModifierProduit(int? id)
        {
            if (id != 0)
            {
                ProduitServices prodservice = new ProduitServices();
                {
                    Produit produit = prodservice.ObtenirAllProduits().Where(p => p.ProduitID == id).FirstOrDefault();
                    if (produit == null)
                    {
                        return NotFound();
                    }
                    return View(produit);
                }

            }
            return NotFound();
        }

        //POST Vue produit à modifier
        [HttpPost]
        public IActionResult ModifierProduit(Produit produit)
        {
            if (!ModelState.IsValid)
                return View(produit);
            ProduitServices produitServices = new ProduitServices();
            produitServices.ModifierProduit(produit.ProduitID, produit.DateCreation, produit.Designation, produit.Format, produit.ImagePath, produit.Description, produit.PrixUnitaire, produit.Devise);
            return RedirectToAction("IndexProduit");
        }

        //Supprimer Produit

        public IActionResult SupprimerProduit(int id)
        {
            ProduitServices prodservice = new ProduitServices();
            prodservice.SupprimerProduit(id);
            return RedirectToAction("IndexProduit");
        }

        //Fonctions GET POUR PANIER

        //Afficher la liste des produits disponibles pour achat

        public IActionResult IndexBoutique()
        {
            var bdd = new BddContext();
            var produits = bdd.Produits.ToList();
            ViewBag.Produits = produits;
            return View();
        }
        //Afficher panier
        public IActionResult Panier()
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
            return RedirectToAction("Panier");
        }

        public IActionResult SupprimerArticle(int id)
        {
            var panierID = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "panierID");
            new PanierServices().SupprimerArticle(panierID, id);
            return RedirectToAction("Panier");
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

        //Fonctions pour la livraison

        //GET Afficher le choix de livraisons possibles

        public IActionResult Livraison()
        {
            UserAccount_Services userAccount_Services = new UserAccount_Services();
            UserAccount ua = userAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name);
            LivraisonServices livservice = new LivraisonServices();
            CommandeServices commandeServices = new CommandeServices();
            commandeServices.CreerCommande(this.GetSessionCart());

            LivraisonViewModel livraisonViewModel = new LivraisonViewModel();
            CoordonneesAcheteur coordonneesAcheteur = new CoordonneesAcheteur();
            coordonneesAcheteur.Useraccount = HttpContext.User.Identity.IsAuthenticated ? ua : new UserAccount();
            livraisonViewModel.CoordonneesAcheteur = coordonneesAcheteur;
            livraisonViewModel.Livraisons = livservice.ObtenirAllLivraisons();
            return View(livraisonViewModel);
        }

        //Fonctions pour la commande

        //Afficher les options de livraisons possibles
        public IActionResult IndexCommande()
        {            
            Livraison livraison = new Livraison();
            CommandeViewModel cvm = new CommandeViewModel
            {
                Livraison = livraison
            };
            return View(cvm);
        }

        //POST pour remplissage coordonnées de l'acheteur

        [HttpPost]
        public IActionResult Livraison(CommandeViewModel viewModel)
        {
            //if (!ModelState.IsValid) { 
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("Livraison");
            }
            CommandeServices commandeServices = new CommandeServices();
            {
                int id = commandeServices.CreerCoordonnees(viewModel.coordonneesAcheteur.Useraccount, viewModel.coordonneesAcheteur.AdresseLivraison,
                    viewModel.coordonneesAcheteur.AdresseFacturation);
                    

                return Redirect("Paiement");
            }
        }
        /*{
            UserAccount_Services userAccount_Services = new UserAccount_Services();
            CommandeServices commandeServices = new CommandeServices();
            {
                int id = commandeServices.CreerCoordonnées(viewModel.coordonneesAcheteur.AdresseID);
                 
                UserAccount ua = userAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name);
                userAccount_Services.ModifierUserAccount(ua.Id, ua.Prenom, ua.Nom, ua.Mail, ua.Telephone, ua.Role, ua.AssociationId, id, ua.AdresseId, ua.ImagePath);
                return Redirect("/Vente/Livraison");
            }
        }*/

        public IActionResult Paiement()
        {
            return View(this.GetSessionCart());
        }


        [HttpPost]
        public async Task<IActionResult> Telecharger(IFormFile fichier)
        {
            if (fichier.Length > 0)
            {
                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await fichier.CopyToAsync(stream);

                }
            }

            return Ok();
        }

        public IActionResult Telecharger()
        {
            return View();
        }

        private Panier GetSessionCart()
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
            return panier;
        }

        [ActionName("CreerPaiement")]
        [HttpPost]
        public IActionResult PayerAchat(Paiement paiement)
        {
            UserAccount_Services UserAccount_Services = new UserAccount_Services();
            PanierServices panierServices = new PanierServices();
            PaiementServices paiementServices = new PaiementServices();

            paiement.DatePaiement = System.DateTime.Today;
            {
                int id;
                if(paiement.MoyenPaiement.Identifiant == null)
                {
                    int idCarte = paiementServices.CreerMoyenPaiement(paiement.MoyenPaiement.NomTitulaire, paiement.MoyenPaiement.Numero, paiement.MoyenPaiement.Cryptogramme,
                        paiement.MoyenPaiement.DateExpiration);
                    id = paiementServices.CreerPaiement(paiement.Montant, idCarte);
                    paiement.MoyenPaiement.moyenPaiement = MoyenPaiement.TypeMoyenPaiement.CarteBancaire;
                }
                else
                {
                    int idPaypal = paiementServices.CreerMoyenPaiement(paiement.MoyenPaiement.Identifiant);
                    id = paiementServices.CreerPaiement(paiement.Montant, idPaypal);
                    paiement.MoyenPaiement.moyenPaiement = MoyenPaiement.TypeMoyenPaiement.Paypal;
                }

                return Redirect("/Vente/Paiement");
            }
            
        }

    }
}


