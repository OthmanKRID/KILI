using Microsoft.AspNetCore.Mvc;
using Kili.ViewModels;
using Kili.Models;
using Kili.Models.General;
using static Kili.ViewModels.PaiementViewModel;
using Kili.Models.Services;
using Kili.Models.Dons;

namespace Kili.Controllers
{
    public class PaiementController : Controller
    {

        //Méthode pour créer un paiement à partir d'une vue d'un type d'action (don, achat...).
        public IActionResult CreerPaiement(int actionID, int montant, TypeAction typeAction)
        {                   
            UserAccountViewModel viewModel = new UserAccountViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            PaiementViewModel paiementviewModel = new PaiementViewModel() { ActionID = actionID, Paiement = new Paiement() { Montant = montant }, Action = typeAction };
            if (viewModel.Authentifie)
            {
                return View(paiementviewModel);
            }

            return Redirect("/login/authentification");
        }

        
        [HttpPost]
        public IActionResult CreerPaiement(PaiementViewModel viewModel)
        {
            //if (!ModelState.IsValid) { 

            UserAccount_Services UserAccount_Services = new UserAccount_Services();
            PaiementServices paiementServices= new PaiementServices();
            {
                int id;
                if (viewModel.MoyenPaiement.Identifiant == null){


                    int idCarte = paiementServices.CreerMoyenPaiement(viewModel.MoyenPaiement.NomTitulaire, viewModel.MoyenPaiement.Numero, viewModel.MoyenPaiement.Cryptogramme, viewModel.MoyenPaiement.DateExpiration);
                    id = paiementServices.CreerPaiement(viewModel.Paiement.Montant, idCarte);
                    viewModel.MoyenPaiement.moyenPaiement = MoyenPaiement.TypeMoyenPaiement.CarteBancaire;
                }
                else
                {
                    int idPaypal = paiementServices.CreerMoyenPaiement(viewModel.MoyenPaiement.Identifiant);
                    id = paiementServices.CreerPaiement(viewModel.Paiement.Montant, idPaypal);
                    viewModel.MoyenPaiement.moyenPaiement = MoyenPaiement.TypeMoyenPaiement.Paypal;
                }
                
                if (viewModel.Action == TypeAction.Commande)
                {

                }
                else if (viewModel.Action == TypeAction.Cotisation)
                {

                }
                else if (viewModel.Action == TypeAction.Don)
                {
                    // Fonction permettant de retrouver l'association bénéficiaire liée au paiement.
                    DonServices donServices = new DonServices();
                    donServices.ModifierDonPaiementID(viewModel.ActionID, id);
                    ServiceDon sd = paiementServices.ObtenirPaiement(id).Don.Collecte.ServiceDon;

                    Abonnement_Services abonnement_Services = new Abonnement_Services();
                    Abonnement abonnement = abonnement_Services.ObtenirAbonnementviaServiceDon(sd.Id);         

                    Association_Services association_Services = new Association_Services();
                    Association association = association_Services.ObtenirAssociation(abonnement.Id);
                    paiementServices.ModifierPaiement(id, viewModel.Paiement.Montant, association.Id);
                }

                var viewmodel2 = viewModel;

                return RedirectToAction("creerattestation", "paiement", viewmodel2); //new { ActionID = viewModel.ActionID, Action = viewModel.Action, Paiement = new Paiement() { Montant = viewModel.Paiement.Montant, DatePaiement = viewModel.Paiement.DatePaiement }, MoyenPaiement = new MoyenPaiement() { moyenPaiement = viewModel.MoyenPaiement.moyenPaiement } }
            }
            // } return View();
        }


        public IActionResult CreerAttestation(PaiementViewModel viewModel)
        {

            return View(viewModel);
            
        }

        [HttpPost]
        public IActionResult CreerAttestation()
        {

            return Redirect("/login/authentification");
        }

        /*
        public IActionResult AfficherDon(int id)
        {
            if (id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    Don don = donServices.ObtenirDon(id);
                    if (don == null)
                    {
                        return View("Error");
                    }
                    return View(new DonViewModel() { Don = don });
                }

            }
            return View("Error");
        }

        public IActionResult AfficherDons()
        {
            DonServices donServices = new DonServices();
            {
                List<Don> listdon = donServices.ObtenirDons();

                return View(listdon);
            }
        }

        public IActionResult ModifierDon(int id)
        {
            if (id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    Don don = donServices.ObtenirDons().Where(r => r.Id == id).FirstOrDefault();

                    if (don == null)
                    {
                        return View("Error");
                    }
                    return View(new DonViewModel() { Don = don });
                }

            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifierDon(DonViewModel viewModel)
        {

            if (viewModel.Don.Id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    donServices.ModifierDon(viewModel.Don.Id, viewModel.Don.Montant, viewModel.Don.Recurrence);

                    return RedirectToAction("ModifierDon", new { @id = viewModel.Don.Id });
                }
            }
            else
            {
                return View("Error");
            }
        }



        public IActionResult SupprimerDon(int id)
        {
            if (id != 0)
            {
                DonServices donServices = new DonServices();
                {
                    Don don = donServices.ObtenirDons().Where(r => r.Id == id).FirstOrDefault();
                    if (don == null)
                    {
                        return View("Error");
                    }
                    donServices.SupprimerDon(id);
                    return Redirect("/home/index");
                }

            }
            return View("Error");
        }

        */


    }
}
