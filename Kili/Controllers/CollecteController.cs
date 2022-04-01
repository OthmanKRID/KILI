using Kili.Models;
using Kili.Models.Dons;
using Kili.Models.General;
using Kili.Models.Services;
using Kili.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kili.Controllers
{
    public class CollecteController : Controller
    {

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
                int id = donServices.CreerCollecte(collecte.Nom, collecte.MontantCollecte, collecte.Descriptif, userAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).Association.Abonnement.ServiceDonId); // , collecte.Date

               return Redirect("/collecte/AfficherCollectesDonsCompteConnecte"); 
            }
            // } return View();
        }

        // Affiche une collecte à partir de son id
        public IActionResult AfficherCollecte(int id)
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

        // Affiche la liste des collectes de la BDD
        public IActionResult AfficherCollectes()
        {
            DonServices donServices = new DonServices();
            {
                List<Collecte> listcollecte = donServices.ObtenirCollectes();

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

            DonServices donServices = new DonServices();


            foreach (Collecte collecte in UserAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).Association.Abonnement.serviceDon.Collectes)
                {
                        collecteDonViewModel.listecollecte.Add(collecte);

                collecteDonViewModel.montantglobalcollectes += collecte.MontantCollecte;

               if (collecte.Dons != null) {     
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
                {
                    donServices.ModifierCollecte(collecte.Id, collecte.Nom, collecte.MontantCollecte, collecte.Descriptif, collecte.Date);

                    return RedirectToAction("AfficherCollectesDonsCompteConnecte");
                }
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
                    donServices.SupprimerCollecte(id);
                    return RedirectToAction("AfficherCollectesDonsCompteConnecte");
                }

            }
            return View("Error");
        }




    }
}
