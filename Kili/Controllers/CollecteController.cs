using Kili.Models;
using Kili.Models.Dons;
using Kili.Models.General;
using Kili.Models.Services;
using Kili.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using static Kili.ViewModels.PaiementViewModel;

namespace Kili.Controllers
{
    public class CollecteController : Controller
    {

        private IWebHostEnvironment _webEnv;

        public CollecteController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
        }

        // Création d'une collecte (à mettre depuis la page association)
        public IActionResult CreerCollecte()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreerCollecte(Collecte collecte)
        {
            //if (!ModelState.IsValid) { 
            UserAccount_Services userAccount_Services = new UserAccount_Services();

            DonServices donServices = new DonServices();
            {

                if (collecte.Image != null)
                {
                    if (collecte.Image.Length != 0)
                    {
                        string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                        string filePath = Path.Combine(uploads, collecte.Image.FileName);
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            collecte.Image.CopyTo(fileStream);
                        }

                        donServices.CreerCollecte(collecte.Nom, collecte.MontantCollecte, collecte.Descriptif, userAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).Association.Abonnement.ServiceDonId, "/images/" + collecte.Image.FileName);
                                            }
                }
                else
                {
                    donServices.CreerCollecte(collecte.Nom, collecte.MontantCollecte, collecte.Descriptif, userAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).Association.Abonnement.ServiceDonId,null);
                }

               return Redirect("/collecte/AfficherCollectesDonsCompteConnecte"); 
            }
            // } return View();
        }

        // Affiche une collecte à partir de son id
        public IActionResult AfficherCollecte(int id)
        {
            Collecte collecte = new DonServices().ObtenirCollecte(id);
            if (collecte != null)
            {            
                DonViewModel viewModel = new DonViewModel() { Don = new Don(), Collecte =collecte, IdCollecte = collecte.Id};

                return View(viewModel);

            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult AfficherCollecte(DonViewModel viewModel)
        {
            UserAccount_Services UserAccount_Services = new UserAccount_Services();

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                DonServices donservices = new DonServices();

                //Fonction plus utilisé car nous ne requèrons finalement pas un champ adresse pour etre donateur. 
                /*
                //Regarde si l'utilisateur n'a pas d'adresse enregistrée et alors le redirige vers la création d'une adresse
                if (UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).AdresseId == null)
                {
                    return Redirect("/adresse/creeradresse");
                }
                */

                //Regarde si l'utilisateur n'est pas enregistré comme donateur et alors crée un donateur en y associant l'Id de l'adresse
                if (UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).DonateurId == null)
                {

                    UserAccount ua = UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name);

                    // ATTENTION FONCTION A TESTER : il s'agit d'une rustine temporaire du fait qu'une adresse n'est plus nécessaire.
                    int idtemp = donservices.CreerDonateur();

                    UserAccount_Services.ModifierUserAccount(ua.Id, ua.Prenom, ua.Nom, ua.Mail, ua.Telephone, ua.Role, ua.AssociationId, idtemp, ua.AdresseId, ua.ImagePath);

                }

                //Fonction plus utilisée car on ne demande plus nécessairement l'adresse.
                /*
               //Si l'utilisateur est déjà donateur, alors on modifie celui-ci en y associant l'adresse
               else
               {
                   donservices.ModifierDonateur((int)UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).DonateurId, UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).AdresseId);

               }*/

                int id = donservices.CreerDon(viewModel.Don.Montant, viewModel.Don.Recurrence, UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).DonateurId, viewModel.IdCollecte);

                return RedirectToAction("CreerPaiement", "Paiement", new { actionID = id, montant = viewModel.Don.Montant, typeaction = TypeAction.Don });
            }
            else
            {
                return RedirectToAction("Authentification", "Login");
            }
        }


        // Affiche la liste des collectes de la BDD
        public IActionResult AfficherCollectes()
        {
            DonServices donServices = new DonServices();
            {
                List<Collecte> listcollecte = donServices.ObtenirCollectesActives();

                return View(listcollecte);
            }
        }

        // Affiche la liste des collecte d'une association à partir de son Id
        public IActionResult AfficherCollectesAssociation(int id)
        {

            UserAccount_Services UserAccount_Services = new UserAccount_Services();
            List<Collecte> listcollecteAsso = new List<Collecte>();
            {

                foreach (Collecte collecte in UserAccount_Services.ObtenirUserAccount(id).Association.Abonnement.serviceDon.Collectes)
                {
                    listcollecteAsso.Add(collecte);
                }

                return View(listcollecteAsso);
            }
        }

        // Liste de collecte visible par le compte utilisateur 

        public IActionResult AfficherCollectesCompteConnecte()
        {
            
            UserAccount_Services UserAccount_Services = new UserAccount_Services();
            List<Collecte> listcollecteAsso = new List<Collecte>();
            {

                foreach (Collecte collecte in UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).Association.Abonnement.serviceDon.Collectes)
                {
                    listcollecteAsso.Add(collecte);
                }

                return View(listcollecteAsso);
            }
        }

        // Liste de collectes et de dons visibles par le compte utilisateur
        public IActionResult AfficherCollectesDonsCompteConnecte()
        {

            UserAccount_Services UserAccount_Services = new UserAccount_Services();

            CollecteDonViewModel collecteDonViewModel = new CollecteDonViewModel() { listecollecte = new List<Collecte>(), listedon = new List<Don>(), montantglobalcollectes=0};

            collecteDonViewModel.association = UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).Association;

            foreach (Collecte collecte in UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).Association.Abonnement.serviceDon.Collectes)
                {
                
                if (collecte.Actif == true) { 
                   collecteDonViewModel.listecollecte.Add(collecte);
                }

                collecteDonViewModel.montantglobalcollectes += collecte.MontantCollecte;


                if (collecte.Dons != null)
                {

                foreach (Don don in collecte.Dons) {

                    collecteDonViewModel.listedon.Add(don);                    
                    }         
                }

                }
            

            return View(collecteDonViewModel);            
        }

        // Modifie une collecte à partir de son Id
        public IActionResult ModifierCollecte(int id)
        {
            if (id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    Collecte collecte = donServices.ObtenirCollecte(id);

                    if (collecte == null)
                    {
                        return View("Error");
                    }
                    return View(collecte);
                }

            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifierCollecte(Collecte collecte)
        {

            if (collecte.Id != 0)
            {
                DonServices donServices = new DonServices();


                if (collecte.Image != null)
                {
                    if (collecte.Image.Length != 0)
                    {
                        string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                        string filePath = Path.Combine(uploads, collecte.Image.FileName);
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            collecte.Image.CopyTo(fileStream);
                        }
                        donServices.ModifierCollecte(collecte.Id, collecte.Nom, collecte.MontantCollecte, collecte.Descriptif, collecte.Date, "/images/" + collecte.Image.FileName);
                    }
                }
                else
                {
                    donServices.ModifierCollecte(collecte.Id, collecte.Nom, collecte.MontantCollecte, collecte.Descriptif, collecte.Date, collecte.ImagePath);
                }
       
                return RedirectToAction("AfficherCollectesDonsCompteConnecte");
               
            }
            else
            {
                return View("Error");
            }
        }

        // Supprime une collecte à partir de son Id
        public IActionResult SupprimerCollecte(int id)
        {
            if (id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    Collecte collecte = donServices.ObtenirCollecte(id);
                    if (collecte == null)
                    {
                        return View("Error");
                    }
                    donServices.DesactiverCollecte(id);
                    return RedirectToAction("AfficherCollectesDonsCompteConnecte");
                }

            }
            return View("Error");
        }




    }
}
