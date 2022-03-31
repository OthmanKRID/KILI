using Kili.Models;
using Kili.Models.GestionAdhesion;
using Kili.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kili.Controllers
{
    public class AdhesionController : Controller
    {
        private Adhesion_Services Adhesion_Services;

        public AdhesionController()
        {
            Adhesion_Services = new Adhesion_Services();
        }

        public IActionResult CreerAdhesion()
        {
            return View();
        }


        // Crée un type d'adhésion en passant en attribuant l'ID du service adhésion du useraccount connecté
        [HttpPost]
        public IActionResult CreerAdhesion(Adhesion adhesion)
        {
            //if (!ModelState.IsValid) { 
            UserAccount_Services userAccount_Services = new UserAccount_Services();

            Adhesion_Services adhesionServices = new Adhesion_Services();
            {
                int id = adhesionServices.CreerAdhesion(adhesion.nom, adhesion.prix, adhesion.duree, adhesion.description, adhesion.typeAdhesion, userAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).Association.Abonnement.serviceAdhesion.Id); 

                return Redirect("/home/index");
            }
            // } return View();
        }

        // Méthode pour afficher un type d'adhésion en fonction de son id
        public IActionResult AfficherTypeAdhesion(int id)
        {
            if (id != 0)
            {
                Adhesion_Services AdhesionServices = new Adhesion_Services();
                {
                    Adhesion adhesion = AdhesionServices.ObtenirTypeAdhesion(id);
                    if (adhesion == null)
                    {
                        return View("Error");
                    }
                    return View(adhesion);
                }

            }
            return View("Error");
        }


        // Méthode pour afficher l'ensemble des types adhésions de toutes les associations
        public IActionResult AfficherTypesAdhesion()
        {
            Adhesion_Services donServices = new Adhesion_Services();
            {
                List<Adhesion> listadhesion = donServices.ObtenirTypeAdhesions();

                return View(listadhesion);
            }
        }

        // Liste des types d'adhesion en fonction de l'id d'une association
        public IActionResult AfficherTypesAdhesiondeAssociation(int idassociation)
        {

            Association_Services Association_Services = new Association_Services();
            List<Adhesion> listtypesadhesionAsso = new List<Adhesion>();
            {

                foreach (Adhesion adhesion in Association_Services.ObtenirAssociation(idassociation).Abonnement.serviceAdhesion.adhesions)
                {
                    listtypesadhesionAsso.Add(adhesion);
                }

                return View(listtypesadhesionAsso);
            }
        }

        // Modifier les types d'adhesion en fonction de son id
        public IActionResult ModifierAdhesion(int id)
        {
            if (id != 0)
            {
                Adhesion_Services AdhesionServices = new Adhesion_Services();
                {
                    Adhesion Adhesion = AdhesionServices.ObtenirTypeAdhesion(id);

                    if (Adhesion == null)
                    {
                        return View("Error");
                    }
                    return View(Adhesion);
                }

            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifierAdhesion(Adhesion adhesion)
        {

            UserAccount_Services userAccount_Services = new UserAccount_Services();

            if (adhesion.Id != 0)
            {
                Adhesion_Services AdhesionServices = new Adhesion_Services();
                {
                    AdhesionServices.ModifierAdhesion(adhesion.Id, adhesion.nom, adhesion.prix, adhesion.duree, adhesion.description, adhesion.typeAdhesion);

                    return RedirectToAction("AfficherAdhesions", userAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).Id);
                }
            }
            else
            {
                return View("Error");
            }
        }

        // Supprimer les types d'adhesion en fonction de son id
        public IActionResult SupprimerAdhesion(int id)
        {
            if (id != 0)
            {
                UserAccount_Services userAccount_Services = new UserAccount_Services();

                Adhesion_Services AdhesionServices = new Adhesion_Services();
                {
                    Adhesion Adhesion = AdhesionServices.ObtenirTypeAdhesion(id);
                    if (Adhesion == null)
                    {
                        return View("Error");
                    }
                    AdhesionServices.SupprimerAdhesion(id);
                    return RedirectToAction("AfficherAdhesions", userAccount_Services.ObtenirUserAccount(HttpContext.User.Identity.Name).Id);
                }

            }
            return View("Error");
        }








    }
}
