using Kili.Models.General;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static Kili.Models.General.UserAccount;
using Microsoft.AspNetCore.Http;
using System.Data.Entity;
using Kili.Models.Dons;

namespace Kili.Models
{
    public class Abonnement_Services
    {
        private BddContext _bddContext;

        private UserAccount_Services UserAccount_Services;
        private Association_Services Association_Services;
        public Abonnement_Services()
        {
            _bddContext = new BddContext();
            UserAccount_Services = new UserAccount_Services();
            Association_Services = new Association_Services();
        }

        public Abonnement ObtenirAbonnement(int id)
        {
            return _bddContext.Abonnements.Find(id);
        }

        //Fonction permettant de récupérer l'abonnement d'une Association
        public Abonnement ObtenirAbonnement(Association association)
        {
            return _bddContext.Abonnements.Where(m => m.Id == association.AbonnementId).FirstOrDefault();
        }

        public Abonnement ObtenirAbonnementviaServiceDon(int id)
        {
            return _bddContext.Abonnements.Where(m => m.ServiceDonId == id).FirstOrDefault();
        }

        //Fonction permettant de créer un abonnement
        public int CreerAbonnement()
         {
             Abonnement abonnement = new Abonnement();                     
             _bddContext.Abonnements.Add(abonnement);
             _bddContext.SaveChanges();
             return abonnement.Id;
         }

        //Supprimer un abonnement à partir de son ID
        public void SupprimerAbonnement(int id)
        {
            Abonnement Abonnement = _bddContext.Abonnements.Find(id);

            if (Abonnement != null)
            {
                _bddContext.Abonnements.Remove(Abonnement);
                _bddContext.SaveChanges();
            }
        }

        // Ajouter une service Adhésion à un abonnement
        // On passe en argument l'ID de l'abonnnement et la duree de l'adhésion au service
        public void AjouterService(int id, Service service)
        {

            if (service.TypeService == TypeService.Adhesion)
            {
                //Abonnement Abonnement = _bddContext.Abonnements.Include(abo => abo.serviceAdhesion).FirstOrDefault(abo => abo.Id == id);
                ServiceAdhesion serviceAdhesion = _bddContext.ServicesAdhesion.Find(id);
                serviceAdhesion.IsActive = true;
                serviceAdhesion.duree = service.duree_mois;
                serviceAdhesion.dateAbonnement = DateTime.Today;
                serviceAdhesion.dateFinAbonnement = DateTime.Today.AddDays(service.duree_mois);
                //Abonnement.ServiceAdhesionId = Abonnement.serviceAdhesion.Id;
            }

            if (service.TypeService == TypeService.Don)
            {
                //Abonnement Abonnement = _bddContext.Abonnements.Include(abo => abo.serviceDon).FirstOrDefault(abo => abo.Id == id);
                ServiceDon serviceDon = _bddContext.ServicesDon.Find(id);
                serviceDon.IsActive = true;
                serviceDon.duree = service.duree_mois;
                serviceDon.dateAbonnement = DateTime.Today;
                serviceDon.dateFinAbonnement = DateTime.Today.AddDays(service.duree_mois);
                //Abonnement.ServiceDonId = Abonnement.serviceAdhesion.Id;
            }

            if (service.TypeService == TypeService.Boutique)
            {
                //Abonnement Abonnement = _bddContext.Abonnements.Include(abo => abo.serviceBoutique).FirstOrDefault(abo => abo.Id == id);
                ServiceBoutique serviceBoutique = _bddContext.ServicesBoutique.Find(id);
                serviceBoutique.IsActive = true;
                serviceBoutique.duree = service.duree_mois;
                serviceBoutique.dateAbonnement = DateTime.Today;
                serviceBoutique.dateFinAbonnement = DateTime.Today.AddDays(service.duree_mois);
                // Abonnement.ServiceBoutiqueId = Abonnement.serviceBoutique.Id;
            }
            //_bddContext.ServicesAdhesion.Add(adhesion);
            _bddContext.SaveChanges();
    
       
            //_bddContext.SaveChanges();
        }

       public void ModifierServiceAdhesion(int id, ServiceAdhesion serviceAdhesion)
        {
            ServiceAdhesion nouveauServiceAdhesion = _bddContext.ServicesAdhesion.Find(id);
            nouveauServiceAdhesion.IsActive = serviceAdhesion.IsActive;
            nouveauServiceAdhesion.duree = serviceAdhesion.duree;
            nouveauServiceAdhesion.dateAbonnement = serviceAdhesion.dateAbonnement;
            nouveauServiceAdhesion.dateFinAbonnement = serviceAdhesion.dateFinAbonnement;
            _bddContext.SaveChanges();
        }

        //Supprimer un service d'un Abonnnemnt donnée
        public void SupprimerServiceAdhesion(int id)
        {
            Abonnement Abonnement = _bddContext.Abonnements.Find(id);
            ServiceAdhesion adhesion = _bddContext.ServicesAdhesion.Find(Abonnement.ServiceAdhesionId);
            _bddContext.ServicesAdhesion.Remove(adhesion);
            Abonnement.ServiceAdhesionId = null;
            _bddContext.SaveChanges();
        }


        //////////////////////////////////////////////////////////////////////////////////////////////
        ///  
        ///                    Gestion des service proposé dans l'offre
        ///                    
        //////////////////////////////////////////////////////////////////////////////////////////////


        //Fonctions permettant d'obtenir toutes les offres de services proposées
        public List<Service> ObtenirToutLesServicesDansOffre()
        {
            return _bddContext.Services.ToList();
        }

        //Fonctions permettant d'obtenir un offre de service proposée
        public Service ObtenirServiceDansOffre(int id)
        {
            return _bddContext.Services.Where(m => m.Id == id).FirstOrDefault();
        }

        //Fonctions permettant d'ajouter un offre de service 
        public int AjouterServiceDansOffre(double prix, int duree_mois, TypeService Typeservice)
        {
            Service service = new Service() { prix = prix, duree_mois = duree_mois, TypeService = Typeservice };        
            _bddContext.Services.Add(service);
            _bddContext.SaveChanges();
            return service.Id;
        }

        //Fonctions permettant de supprimer un offre de service 
        public void SupprimerServiceDansOffre(int Id)
        {
            Service service = ObtenirServiceDansOffre(Id);
            _bddContext.Services.Remove(service);
            _bddContext.SaveChanges();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
