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



        public IActionResult CreerPaiementServices(double montant)
        {
            UserAccountViewModel viewModel = new UserAccountViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            PaiementViewModel paiementviewModel = new PaiementViewModel() { Paiement = new Paiement() { Montant = montant } };
            if (viewModel.Authentifie)
            {
                return View(paiementviewModel);
            }

            return Redirect("/login/authentification");
        }

        //Méthode pour créer un paiement à partir d'une vue d'un type d'action (don, achat...).
        public IActionResult CreerPaiement(int actionID, double montant, TypeAction typeAction)

        {                   
            UserAccountViewModel viewModel = new UserAccountViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            PaiementViewModel paiementviewModel = new PaiementViewModel() { ActionID = actionID, Paiement = new Paiement() { Montant = montant }, Action = typeAction };
           
            // Verification que le compte est authentifié
            if (viewModel.Authentifie)
            {
                return View(paiementviewModel);
            }

            return Redirect("/login/authentification");
        }

        [ActionName("CreerPaiement")]
        [HttpPost]
        public IActionResult CreerPaiementPost(PaiementViewModel viewModel)
        {
            //if (!ModelState.IsValid) { 

            UserAccount_Services UserAccount_Services = new UserAccount_Services();
            PaiementServices paiementServices= new PaiementServices();

            viewModel.Paiement.DatePaiement = System.DateTime.Today;                       

            {
                int id;

                // Verifie que le champ "identifiant du formulaire est null". Si c'est la cas, cela indique que l'utilisateur a rempli les données cartes bancaire
                if (viewModel.MoyenPaiement.Identifiant == null){


                    int idCarte = paiementServices.CreerMoyenPaiement(viewModel.MoyenPaiement.NomTitulaire, viewModel.MoyenPaiement.Numero, viewModel.MoyenPaiement.Cryptogramme, viewModel.MoyenPaiement.DateExpiration);
                    id = paiementServices.CreerPaiement(viewModel.Paiement.Montant, idCarte);
                    viewModel.MoyenPaiement.moyenPaiement = MoyenPaiement.TypeMoyenPaiement.CarteBancaire;
                }
                //Autrement il a rempli les données paypal
                else
                {
                    int idPaypal = paiementServices.CreerMoyenPaiement(viewModel.MoyenPaiement.Identifiant);
                    id = paiementServices.CreerPaiement(viewModel.Paiement.Montant, idPaypal);
                    viewModel.MoyenPaiement.moyenPaiement = MoyenPaiement.TypeMoyenPaiement.Paypal;
                }
                
                // Verifie le type d'action donné par la fonction qui a appelé le paiement (commande, cotisation ou don) et en fonction créé un paiement et l'associe à l'association bénéficiaire
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
                

                return View("creerattestation", viewModel); //new { ActionID = viewModel.ActionID, Action = viewModel.Action, Paiement = new Paiement() { Montant = viewModel.Paiement.Montant, DatePaiement = viewModel.Paiement.DatePaiement }, MoyenPaiement = new MoyenPaiement() { moyenPaiement = viewModel.MoyenPaiement.moyenPaiement } }
            }
            // } return View();
        }

        // Appel de la fonction attestation
        public IActionResult CreerAttestation(PaiementViewModel viewModel)
        {

            return View(viewModel);
            
        }

        [HttpPost]
        public IActionResult CreerAttestation()
        {

            return Redirect("/login/authentification");
        }


    }
}
